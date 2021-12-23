Public Class Login
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Cria e seta valores iniciais para sessões
        Session("Logado") = "False"
        Session("ID_TIPO_USUARIO") = ""
        Session("RefPeso") = ""
        Session("RefVolume") = ""
        Session("RefPesoSum") = ""
        Session("RefVolumeSum") = ""
        msgErro.Visible = False

        'Se nao for postback verifica parametros da tela de login
        If Not Page.IsPostBack Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT [MensagemUsuarioRegistrado],[MensagemUsuarioRegistrado],[ExibirLinkCadastre_se],VL_ALIQUOTA_ISS, VL_ALIQUOTA_PIS, VL_ALIQUOTA_COFINS FROM [TB_PARAMETROS]")
            If ds.Tables(0).Rows.Count > 0 Then
                Session("MensagemEmailRedefinicaoSenha") = ds.Tables(0).Rows(0).Item("MensagemUsuarioRegistrado").ToString()
                Session("MensagemUsuarioRegistrado") = ds.Tables(0).Rows(0).Item("MensagemUsuarioRegistrado").ToString()
                Session("ExibirLinkCadastre_se") = ds.Tables(0).Rows(0).Item("ExibirLinkCadastre_se").ToString()

                Session("VL_ALIQUOTA_PIS") = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS").ToString()
                Session("VL_ALIQUOTA_COFINS") = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS").ToString()
                Session("VL_ALIQUOTA_ISS") = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS").ToString()

                If Session("ExibirLinkCadastre_se") = True Then
                    lnkCadastre_se.Visible = True
                Else
                    lnkCadastre_se.Visible = False
                End If
            End If

            Con.Fechar()
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim Criptografar As New Criptografia
        Dim Con As New Conexao_sql
        Con.Conectar()

        'Faz uma busca no banco de dados pelo usuario
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_USUARIO,SENHA,LOGIN,ID_TIPO_USUARIO,ISNULL(FL_ATIVO,0)FL_ATIVO,ISNULL(FL_EXTERNO,0)FL_EXTERNO FROM [dbo].[TB_USUARIO] WHERE LOGIN = '" & txtUsuario.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            'Salva login, ID do usuario e criptografa a senha digitada pelo usuario
            Dim ID As String = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
            Dim Externo As Boolean = ds.Tables(0).Rows(0).Item("FL_EXTERNO").ToString()
            Dim Login As String = ds.Tables(0).Rows(0).Item("LOGIN").ToString()
            Dim Senha As String = Criptografar.CriptografarSenha(txtSenha.Text)

            'verifica se o usuario está ativo
            If ds.Tables(0).Rows(0).Item("FL_ATIVO").ToString() <> True Then
                msgErro.Visible = True
                lblMsg.Text = "Usuário Inativo!"
            Else
                'verifica se a senha digitada é a mesma do banco de dados
                If txtUsuario.Text = Login Then
                    If Senha = ds.Tables(0).Rows(0).Item("SENHA").ToString() Then
                        Session("Logado") = "True"
                        Session("ID_USUARIO") = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
                        Session("Externo") = Externo
                        Session("USER") = txtUsuario.Text
                        'Verifica se é usuario externo 
                        If Externo = True Then
                            If ddlEmpresa.SelectedValue = 0 Then
                                msgErro.Visible = True
                                lblMsg.Text = "Selecione a empresa à qual pertence!"
                            Else
                                'Verifica se pertence a mais de um grupo de usuario
                                ds = Con.ExecutarQuery("SELECT ID_TIPO_USUARIO FROM [dbo].[TB_VINCULO_USUARIO] WHERE ID_USUARIO = " & Session("ID_USUARIO") & " AND ID_PARCEIRO = " & ddlEmpresa.SelectedValue)
                                If ds.Tables(0).Rows.Count = 0 Then
                                    'Caso seja usuario externo, se o mesmo pertencer nao pertencer a grupo de usuario retorna msg de erro
                                    msgErro.Visible = True
                                    lblMsg.Text = "Usuário sem grupo vinculado"
                                    Session("Logado") = "False"
                                ElseIf ds.Tables(0).Rows.Count = 1 Then
                                    'Caso seja usuario externo e se pertenca a um unico grupo de usuario é direcionado a tela default
                                    Session("ID_TIPO_USUARIO") = ds.Tables(0).Rows(0).Item("ID_TIPO_USUARIO").ToString()
                                    Session("ID_EMPRESA") = ddlEmpresa.SelectedValue
                                    Response.Redirect("Default.aspx?ID=" & ID)
                                Else
                                    'Caso seja usuario externo e se pertenca a mais de grupo de usuario é direcionado a tela de seleção de perfil
                                    Session("ID_TIPO_USUARIO") = 0
                                    Session("ID_EMPRESA") = ddlEmpresa.SelectedValue
                                    Response.Redirect("SelecionaPerfil.aspx?ID=" & ID)
                                End If
                            End If


                        ElseIf Externo = False Then
                            'Caso seja usuario interno, se o mesmo pertencer a mais de um grupo de usuario e concatena todos os tipos na session
                            ds = Con.ExecutarQuery("Select A.ID_TIPO_USUARIO FROM TB_VINCULO_USUARIO A 
Left Join TB_TIPO_USUARIO C ON C.ID_TIPO_USUARIO = A.ID_TIPO_USUARIO
WHERE a.ID_USUARIO = " & Session("ID_USUARIO"))
                            If ds.Tables(0).Rows.Count > 0 Then
                                For Each linha As DataRow In ds.Tables(0).Rows
                                    If Session("ID_TIPO_USUARIO") <> "" Then
                                        Session("ID_TIPO_USUARIO") &= "," & linha.Item("ID_TIPO_USUARIO").ToString()
                                    Else
                                        Session("ID_TIPO_USUARIO") &= linha.Item("ID_TIPO_USUARIO").ToString()
                                    End If
                                Next
                                Session("ID_EMPRESA") = 1
                                Response.Redirect("Default.aspx?ID=" & ID)
                            Else
                                'Caso seja usuario interno, se o mesmo pertencer nao pertencer a grupo de usuario retorna msg de erro
                                msgErro.Visible = True
                                lblMsg.Text = "Usuário sem grupo vinculado"
                                Session("ID_TIPO_USUARIO") = 0
                                Session("Logado") = "False"
                            End If




                        End If


                    Else
                        msgErro.Visible = True
                        lblMsg.Text = "Login/Senha inválidos"
                    End If
                Else
                    msgErro.Visible = True
                    lblMsg.Text = "Usuário não encontrado"
                End If
            End If

        Else
            msgErro.Visible = True
            lblMsg.Text = "Usuário não encontrado"
        End If

        Con.Fechar()
    End Sub

    Private Sub txtUsuario_TextChanged(sender As Object, e As EventArgs) Handles txtUsuario.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        'Faz uma busca no banco de dados pelo usuario
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_USUARIO FROM [dbo].[TB_USUARIO] WHERE LOGIN = '" & txtUsuario.Text & "' AND FL_EXTERNO = 1")
        If ds.Tables(0).Rows.Count > 0 Then
            'Salva login, ID do usuario e criptografa a senha digitada pelo usuario
            Dim ID As String = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
            'dsContato.SelectCommand = Sql
            'ddlContato.DataBind()
            dsEmpresa.SelectParameters("ID_USUARIO").DefaultValue = ID
            ddlEmpresa.DataBind()
            DivEmpresa.Attributes.CssStyle.Add("display", "block")
        Else
            DivEmpresa.Attributes.CssStyle.Add("display", "none")
        End If

    End Sub
End Class