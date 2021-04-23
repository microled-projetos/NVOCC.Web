Public Class Login
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Cria e seta valores iniciais para sessões
        Session("Logado") = "False"
        Session("ID_TIPO_USUARIO") = ""
        msgErro.Visible = False

        'Se nao for postback verifica parametros da tela de login
        If Not Page.IsPostBack Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT [MensagemUsuarioRegistrado],[MensagemUsuarioRegistrado],[ExibirLinkCadastre_se] FROM [TB_PARAMETROS]")
            If ds.Tables(0).Rows.Count > 0 Then
                Session("MensagemEmailRedefinicaoSenha") = ds.Tables(0).Rows(0).Item("MensagemUsuarioRegistrado").ToString()
                Session("MensagemUsuarioRegistrado") = ds.Tables(0).Rows(0).Item("MensagemUsuarioRegistrado").ToString()
                Session("ExibirLinkCadastre_se") = ds.Tables(0).Rows(0).Item("ExibirLinkCadastre_se").ToString()

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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_USUARIO,SENHA,LOGIN,ID_TIPO_USUARIO,ISNULL(FL_ATIVO,0)FL_ATIVO FROM [dbo].[TB_USUARIO] WHERE LOGIN = '" & txtUsuario.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            'Salva login, ID do usuario e criptografa a senha digitada pelo usuario
            Dim ID As String = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
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
                        ' Session("ID_TIPO_USUARIO") = ds.Tables(0).Rows(0).Item("ID_TIPO_USUARIO").ToString()
                        ' Response.Redirect("SelecionaPerfil.aspx?ID=" & ID)
                        ' Response.Redirect("Default.aspx?ID=" & ID)

                        ds = Con.ExecutarQuery("SELECT ID_TIPO_USUARIO FROM [dbo].[TB_VINCULO_USUARIO] WHERE ID_USUARIO = " & Session("ID_USUARIO"))
                        If ds.Tables(0).Rows.Count = 0 Then
                            msgErro.Visible = True
                            lblMsg.Text = "Usuário sem grupo vinculado"
                        ElseIf ds.Tables(0).Rows.Count = 1 Then
                            Session("ID_TIPO_USUARIO") = ds.Tables(0).Rows(0).Item("ID_TIPO_USUARIO").ToString()
                            Response.Redirect("Default.aspx?ID=" & ID)
                        Else
                            Session("ID_TIPO_USUARIO") = 0
                            Response.Redirect("SelecionaPerfil.aspx?ID=" & ID)
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

    'Private Sub txtUsuario_TextChanged(sender As Object, e As EventArgs) Handles txtUsuario.TextChanged
    '    Dim Con As New Conexao_sql
    '    Con.Conectar()
    '    'Faz uma busca no banco de dados pelo usuario
    '    Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_USUARIO FROM [dbo].[TB_USUARIO] WHERE LOGIN = '" & txtUsuario.Text & "'")
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        'Salva login, ID do usuario e criptografa a senha digitada pelo usuario
    '        Dim ID As String = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
    '        'dsContato.SelectCommand = Sql
    '        'ddlContato.DataBind()
    '        dsVinculo.SelectParameters("ID_USUARIO").DefaultValue = ID
    '        ddlVinculo.DataBind()
    '        DivEmpresa.Attributes.CssStyle.Add("display", "block")

    '    End If

    'End Sub
End Class