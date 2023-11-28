Public Class CadastrarUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        divmsg.Visible = False



        If Not Page.IsPostBack Then
            CarregaCampos()

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    Sub CarregaCampos()
        If Request.QueryString("id") <> "" Then
            Dim Con As New Conexao_sql
            Dim ID As String = Request.QueryString("id")
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_USUARIO, LOGIN, SENHA,NOME, EMAIL, TELEFONE,CPF, ID_TIPO_USUARIO,FL_ATIVO,FL_EXTERNO,CELULAR,FL_GRAVAR_MASTER_BASICO,FL_GRAVAR_MASTER_CONTAINER,FL_GRAVAR_MASTER_TAXAS,FL_GRAVAR_MASTER_VINCULAR,FL_GRAVAR_HOUSE_BASICO,FL_GRAVAR_HOUSE_CARGA,FL_GRAVAR_HOUSE_TAXAS FROM [dbo].[TB_USUARIO] WHERE ID_USUARIO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtID.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
                txtLogin.Text = ds.Tables(0).Rows(0).Item("LOGIN").ToString()
                txtNome.Text = ds.Tables(0).Rows(0).Item("NOME").ToString()
                txtCPF.Text = ds.Tables(0).Rows(0).Item("CPF").ToString()
                txtEmail.Text = ds.Tables(0).Rows(0).Item("EMAIL").ToString()
                txtTelefone.Text = ds.Tables(0).Rows(0).Item("TELEFONE").ToString()
                txtCelular.Text = ds.Tables(0).Rows(0).Item("CELULAR").ToString()
                ckbAtivo.Checked = ds.Tables(0).Rows(0).Item("FL_ATIVO")
                ckbExterno.Checked = ds.Tables(0).Rows(0).Item("FL_EXTERNO")
                ckbMasterBasico.Checked = ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_BASICO")
                ckbMasterCNTR.Checked = ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_CONTAINER")
                ckbMasterTaxas.Checked = ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_TAXAS")
                ckbMasterVinculo.Checked = ds.Tables(0).Rows(0).Item("FL_GRAVAR_MASTER_VINCULAR")
                ckbHouseBasico.Checked = ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_BASICO")
                ckbHouseCarga.Checked = ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_CARGA")
                ckbHouseTaxas.Checked = ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_TAXAS")

                divTipoUsuario.Attributes.CssStyle.Add("display", "block")

                If ds.Tables(0).Rows(0).Item("FL_EXTERNO") = True Then
                    divEmpresa.Attributes.CssStyle.Add("display", "block")
                Else
                    divEmpresa.Attributes.CssStyle.Add("display", "none")
                End If
            End If
            Con.Fechar()
        Else
            ckbAtivo.Checked = True
        End If
    End Sub
    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divmsg.Visible = False
        diverro.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtNome.Text = "" Then
            diverro.Visible = True
            lblerro.Text = "Preencha o campo de nome."

        ElseIf txtTelefone.Text = "" Then
            diverro.Visible = True
            lblerro.Text = "Preencha o campo de telefone."

        ElseIf txtCelular.Text = "" Then
            diverro.Visible = True
            lblerro.Text = "Preencha o campo de celular."

        ElseIf txtLogin.Text = "" Then
            diverro.Visible = True
            lblerro.Text = "Preencha o campo de login."

        ElseIf ValidaCPF.Validar(txtCPF.Text) = False Then
            diverro.Visible = True
            lblerro.Text = "CPF Inválido."
        ElseIf ValidaEmail.Validar(txtEmail.Text) = False Then
            diverro.Visible = True
            lblerro.Text = "e-Mail Inválido."
        Else
            If txtID.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    diverro.Visible = True
                    lblerro.Text = "Usuário não possui permissão para cadastrar."
                Else

                    ds = Con.ExecutarQuery("SELECT LOGIN FROM [TB_USUARIO] where LOGIN = '" & txtLogin.Text & "'")
                    If ds.Tables(0).Rows.Count > 0 Then
                        diverro.Visible = True
                        lblerro.Text = "Já existe usuário cadastrado com esse login"

                    Else

                        ds = Con.ExecutarQuery("SELECT EMAIL FROM [TB_USUARIO] where EMAIL = '" & txtEmail.Text & "'")
                        If ds.Tables(0).Rows.Count > 0 Then
                            diverro.Visible = True
                            lblerro.Text = "Já existe usuário cadastrado com esse email"
                        Else
                            ds = Con.ExecutarQuery("SELECT CPF FROM [TB_USUARIO] where CPF = '" & txtCPF.Text & "'")
                            If ds.Tables(0).Rows.Count > 0 Then
                                diverro.Visible = True
                                lblerro.Text = "Já existe usuário cadastrado com esse CPF"

                            Else

                                If txtSenha.Text <> txtConfirmaSenha.Text Then
                                    diverro.Visible = True
                                    lblerro.Text = "As senhas não correspondem"
                                Else

                                    Dim Criptografar As New Criptografia

                                    Dim Ativo As String
                                    Dim Senha As String = Criptografar.CriptografarSenha(txtSenha.Text)

                                    If ckbAtivo.Checked = True Then
                                        Ativo = 1
                                    Else
                                        Ativo = 0
                                    End If

                                    If txtSenha.Text = "" Then
                                        diverro.Visible = True
                                        lblerro.Text = "É necessário preencher o campo de senha"

                                    Else


                                        Con.Conectar()
                                        ds = Con.ExecutarQuery("INSERT INTO [dbo].[TB_USUARIO] (LOGIN, SENHA,NOME, EMAIL, TELEFONE,CPF,FL_ATIVO,DT_CADASTRO, FL_EXTERNO, CELULAR,FL_GRAVAR_MASTER_BASICO,FL_GRAVAR_MASTER_CONTAINER,FL_GRAVAR_MASTER_TAXAS,FL_GRAVAR_MASTER_VINCULAR,FL_GRAVAR_HOUSE_BASICO,FL_GRAVAR_HOUSE_CARGA,FL_GRAVAR_HOUSE_TAXAS) VALUES ('" & txtLogin.Text & "' ,'" & Senha & "','" & txtNome.Text & "','" & txtEmail.Text & "' , '" & txtTelefone.Text & "' ,'" & txtCPF.Text & "' , " & Ativo & ", GetDate(), '" & ckbExterno.Checked & "','" & txtCelular.Text & "','" & ckbMasterBasico.Checked & "','" & ckbMasterCNTR.Checked & "','" & ckbMasterTaxas.Checked & "','" & ckbMasterVinculo.Checked & "','" & ckbHouseBasico.Checked & "','" & ckbHouseCarga.Checked & "','" & ckbHouseTaxas.Checked & "'); SELECT SCOPE_IDENTITY() as ID_USUARIO ")
                                        If ds.Tables(0).Rows.Count > 0 Then
                                            txtID.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO")
                                        End If

                                        Dim Esquema As String = ConfigurationManager.ConnectionStrings("NVOCC").ConnectionString
                                        Esquema = Esquema.Substring(Esquema.IndexOf("Catalog="))
                                        Esquema = Esquema.Substring(0, Esquema.IndexOf(";User ID"))
                                        Esquema = Esquema.Replace("Catalog=", "")

                                        Con.CriaDeletaUsuario("USE master; 
CREATE LOGIN [" & txtLogin.Text & "] WITH PASSWORD = N'gflcoablaolg!@2023', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
USE " & Esquema & "; 
CREATE USER [" & txtLogin.Text & "]FOR LOGIN [" & txtLogin.Text & "];
ALTER ROLE [FCA_NVOCC_ROLE] ADD MEMBER [" & txtLogin.Text & "];
")

                                        Con.Fechar()
                                        divmsg.Visible = True
                                        dgvUsuarios.DataBind()
                                        divTipoUsuario.Attributes.CssStyle.Add("display", "block")

                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                If ckbExterno.Checked = True Then
                    divEmpresa.Attributes.CssStyle.Add("display", "block")
                Else
                    divEmpresa.Attributes.CssStyle.Add("display", "none")
                End If
            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    diverro.Visible = True
                    lblerro.Text = "Usuário não possui permissão."
                Else

                    Dim ID As String = txtID.Text

                    ds = Con.ExecutarQuery("SELECT LOGIN FROM [TB_USUARIO] where LOGIN = '" & txtLogin.Text & "' AND ID_USUARIO <> " & ID)
                    If ds.Tables(0).Rows.Count > 0 Then
                        diverro.Visible = True
                        lblerro.Text = "Já existe usuário cadastrado com esse login"

                    Else

                        ds = Con.ExecutarQuery("SELECT EMAIL FROM [TB_USUARIO] where EMAIL = '" & txtEmail.Text & "' AND ID_USUARIO <> " & ID)
                        If ds.Tables(0).Rows.Count > 0 Then
                            diverro.Visible = True
                            lblerro.Text = "Já existe usuário cadastrado com esse email"
                        Else
                            ds = Con.ExecutarQuery("SELECT CPF FROM [TB_USUARIO] where CPF = '" & txtCPF.Text & "' AND ID_USUARIO <> " & ID)
                            If ds.Tables(0).Rows.Count > 0 Then
                                diverro.Visible = True
                                lblerro.Text = "Já existe usuário cadastrado com esse CPF"

                            Else

                                If txtSenha.Text <> txtConfirmaSenha.Text Then
                                    diverro.Visible = True
                                    lblerro.Text = "As senhas não correspondem"
                                Else
                                    Dim Ativo As String

                                    If ckbAtivo.Checked = True Then
                                        Ativo = 1
                                    Else
                                        Ativo = 0
                                    End If
                                    Con.Conectar()

                                    If txtSenha.Text = "" Then
                                        Con.ExecutarQuery("UPDATE [dbo].[TB_USUARIO] SET [NOME] = '" & txtNome.Text & "' , FL_ATIVO =  " & Ativo & ", FL_EXTERNO =  '" & ckbExterno.Checked & "', [EMAIL] = '" & txtEmail.Text & "' , [TELEFONE] = '" & txtTelefone.Text & "' ,[CPF] = '" & txtCPF.Text & "' , [LOGIN] = '" & txtLogin.Text & "' , [CELULAR] = '" & txtCelular.Text & "', FL_GRAVAR_MASTER_BASICO = '" & ckbMasterBasico.Checked & "',FL_GRAVAR_MASTER_CONTAINER = '" & ckbMasterCNTR.Checked & "',FL_GRAVAR_MASTER_TAXAS = '" & ckbMasterTaxas.Checked & "',FL_GRAVAR_MASTER_VINCULAR = '" & ckbMasterVinculo.Checked & "',FL_GRAVAR_HOUSE_BASICO =  '" & ckbHouseBasico.Checked & "',FL_GRAVAR_HOUSE_CARGA = '" & ckbHouseCarga.Checked & "',FL_GRAVAR_HOUSE_TAXAS = '" & ckbHouseTaxas.Checked & "'WHERE ID_USUARIO =" & ID & "; SELECT CAST(SCOPE_IDENTITY() AS INT)")

                                    Else
                                        Dim Criptografar As New Criptografia
                                        Dim Senha As String = Criptografar.CriptografarSenha(txtSenha.Text)
                                        Con.ExecutarQuery("UPDATE [dbo].[TB_USUARIO] SET [SENHA] = '" & Senha & "' , [NOME] = '" & txtNome.Text & "' , FL_ATIVO =  " & Ativo & ", FL_EXTERNO =  '" & ckbExterno.Checked & "' , [EMAIL] = '" & txtEmail.Text & "' , [TELEFONE] = '" & txtTelefone.Text & "' ,[CPF] = '" & txtCPF.Text & "' , [LOGIN] = '" & txtLogin.Text & "' , [CELULAR] = '" & txtCelular.Text & "',FL_GRAVAR_MASTER_BASICO = '" & ckbMasterBasico.Checked & "',FL_GRAVAR_MASTER_CONTAINER = '" & ckbMasterCNTR.Checked & "',FL_GRAVAR_MASTER_TAXAS = '" & ckbMasterTaxas.Checked & "',FL_GRAVAR_MASTER_VINCULAR = '" & ckbMasterVinculo.Checked & "',FL_GRAVAR_HOUSE_BASICO =  '" & ckbHouseBasico.Checked & "',FL_GRAVAR_HOUSE_CARGA = '" & ckbHouseCarga.Checked & "',FL_GRAVAR_HOUSE_TAXAS = '" & ckbHouseTaxas.Checked & "' WHERE ID_USUARIO =" & ID & "; SELECT CAST(SCOPE_IDENTITY() AS INT)")
                                    End If
                                    divmsg.Visible = True
                                    Con.Fechar()
                                End If

                            End If
                        End If
                    End If


                End If

                If ckbExterno.Checked = True Then
                    divEmpresa.Attributes.CssStyle.Add("display", "block")
                Else
                    divEmpresa.Attributes.CssStyle.Add("display", "none")
                End If
            End If

        End If
        dgvUsuarios.DataBind()

    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("CadastrarUsuario.aspx")

    End Sub
    Public Sub Limpar(ByVal controlP As Control)

        Dim ctl As Control

        For Each ctl In controlP.Controls

            If TypeOf ctl Is TextBox Then

                DirectCast(ctl, TextBox).Text = String.Empty

            ElseIf ctl.Controls.Count > 0 Then

                Limpar(ctl)

            End If

        Next

    End Sub
    Private Sub dgvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvUsuarios.RowCommand
        divmsg.Visible = False
        diverro.Visible = False
        If e.CommandName = "Excluir" Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                diverro.Visible = True
                lblerro.Text = "Usuário não possui permissão para excluir"
            Else

                Dim ID As String = e.CommandArgument

                ds = Con.ExecutarQuery("SELECT LOGIN FROM [dbo].[TB_USUARIO] WHERE ID_USUARIO = " & ID)
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("LOGIN") <> "" Then
                        Dim Esquema As String = ConfigurationManager.ConnectionStrings("NVOCC").ConnectionString
                        Esquema = Esquema.Substring(Esquema.IndexOf("Catalog="))
                        Esquema = Esquema.Substring(0, Esquema.IndexOf(";User ID"))
                        Esquema = Esquema.Replace("Catalog=", "")
                        Con.CriaDeletaUsuario("USE [master] DROP LOGIN [" & ds.Tables(0).Rows(0).Item("LOGIN").ToString & "] USE " & Esquema & "; DROP USER [" & ds.Tables(0).Rows(0).Item("LOGIN").ToString & "]")
                    End If
                End If
                Con.ExecutarQuery("DELETE FROM [dbo].[TB_USUARIO] WHERE ID_USUARIO =" & ID)
                Con.Fechar()
                dgvUsuarios.DataBind()
                divmsg.Visible = True

            End If
        End If
    End Sub

    Private Sub dgvUsuarios_PreRender(sender As Object, e As EventArgs) Handles dgvUsuarios.PreRender
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet
        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            dgvUsuarios.Columns(7).Visible = False

        End If

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            dgvUsuarios.Columns(8).Visible = False

        End If

        Con.Fechar()
    End Sub
    Protected Sub dgvUsuarios_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvUsuarios.DataSource = Session("TaskTable")
            dgvUsuarios.DataBind()
            dgvUsuarios.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String
        Dim sortDirection As String = "ASC"
        Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then

            If sortExpression = column Then
                Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

                If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column
        Return sortDirection
    End Function

    Private Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        diverro.Visible = False
        divmsg.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If cbTipoUsuario.SelectedValue = 0 Then
            diverro.Visible = True
            lblerro.Text = "Selecione um tipo de usuário"
        ElseIf ckbExterno.Checked = True And ddlEmpresa.SelectedValue = 0 Then
            diverro.Visible = True
            lblerro.Text = "É necessário preencher a empresa do usuário"
        Else

            Dim empresa As Integer

            If ddlEmpresa.SelectedValue = 0 Then
                empresa = 1
            Else
                empresa = ddlEmpresa.SelectedValue
            End If

            ds = Con.ExecutarQuery("SELECT ID_VINCULO FROM TB_VINCULO_USUARIO WHERE ID_USUARIO = " & txtID.Text & " AND ID_PARCEIRO = " & empresa & "  AND ID_TIPO_USUARIO = " & cbTipoUsuario.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                diverro.Visible = True
                lblerro.Text = "Tipo de usuário já cadastrado"
            Else
                Con.ExecutarQuery("INSERT INTO TB_VINCULO_USUARIO (ID_USUARIO,ID_TIPO_USUARIO,ID_PARCEIRO) VALUES (" & txtID.Text & "," & cbTipoUsuario.SelectedValue & "," & empresa & ")")
                divmsg.Visible = True
                gdvTipoUsuario.DataBind()

                If cbTipoUsuario.SelectedValue = 1 Then
                    Con.ExecutarQuery("UPDATE TB_USUARIO SET FL_GRAVAR_MASTER_BASICO = 1,FL_GRAVAR_MASTER_CONTAINER = 1,FL_GRAVAR_MASTER_TAXAS = 1,FL_GRAVAR_MASTER_VINCULAR = 1,FL_GRAVAR_HOUSE_BASICO = 1,FL_GRAVAR_HOUSE_CARGA = 1,FL_GRAVAR_HOUSE_TAXAS = 1 WHERE ID_USUARIO = " & txtID.Text)
                End If

                cbTipoUsuario.SelectedValue = 0
            End If

            Con.Fechar()
        End If
    End Sub

    Private Sub gdvTipoUsuario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvTipoUsuario.RowCommand
        divmsg.Visible = False
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument
            Dim Con As New Conexao_sql
            Con.Conectar()
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VINCULO_USUARIO] WHERE ID_VINCULO =" & ID)
            Con.Fechar()
            gdvTipoUsuario.DataBind()
            divmsg.Visible = True
        End If
    End Sub




End Class