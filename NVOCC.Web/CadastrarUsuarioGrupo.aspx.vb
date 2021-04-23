Public Class CadastrarUsuarioGrupo
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

                Response.Redirect("Default.aspx")


            End If


        Else
            Response.Redirect("Default.aspx")
        End If
        Con.Fechar()

    End Sub

    Sub CarregaCampos()
        If Request.QueryString("id") <> "" Then
            Dim Con As New Conexao_sql
            Dim ID As String = Request.QueryString("id")
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TIPO_USUARIO as Id, NM_TIPO_USUARIO as Descricao, FL_ADMIN FROM [dbo].[TB_TIPO_USUARIO] WHERE ID_TIPO_USUARIO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtID.Text = ds.Tables(0).Rows(0).Item("Id").ToString()
                txtDescricao.Text = ds.Tables(0).Rows(0).Item("Descricao").ToString()
                ckbAdmin.Checked = ds.Tables(0).Rows(0).Item("FL_ADMIN")
            End If
            Con.Fechar()
        End If
    End Sub
    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divmsg.Visible = False
        msgErro.Visible = False
        diverro.Visible = False
        divInfo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtID.Text = "" Then
            ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                    msgErro.Visible = True
                    lblErro.Text = "Usuário não possui permissão para cadastrar."
                Else
                    ds = Con.ExecutarQuery("SELECT ID_TIPO_USUARIO FROM TB_TIPO_USUARIO WHERE NM_TIPO_USUARIO = '" & txtDescricao.Text & "'")
                    If ds.Tables(0).Rows.Count > 0 Then
                        msgErro.Visible = True
                        lblErro.Text = "Já existe grupo de usuário cadastrado com nome"
                    Else



                        Dim Admin As String
                        If ckbAdmin.Checked = True Then
                            Admin = 1
                        Else
                            Admin = 0
                        End If
                        ds = Con.ExecutarQuery("INSERT INTO [dbo].[TB_TIPO_USUARIO] ([NM_TIPO_USUARIO],[DT_CADASTRO], FL_ADMIN) VALUES ('" & txtDescricao.Text & "' ,GetDate(), " & Admin & "); Select SCOPE_IDENTITY() as ID_TIPO_USUARIO")
                        Con.Fechar()
                        Dim ID_TIPO_USUARIO As String = ds.Tables(0).Rows(0).Item("ID_TIPO_USUARIO").ToString()
                        If ckbAdmin.Checked Then


                            Call Permissoes(ID_TIPO_USUARIO)
                        End If
                        txtDescricao.Text = ""
                        divmsg.Visible = True
                        dgvUsuariosGrupos.DataBind()
                    End If

                End If
            Else
                msgErro.Visible = True
                lblErro.Text = "Usuário não possui permissão para cadastrar."
            End If

        Else

            ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                    msgErro.Visible = True
                    lblErro.Text = "Usuário não possui permissão para alterar."
                Else
                    ds = Con.ExecutarQuery("SELECT ID_TIPO_USUARIO FROM TB_TIPO_USUARIO WHERE NM_TIPO_USUARIO = '" & txtDescricao.Text & "' and ID_TIPO_USUARIO <> " & txtID.Text)
                    If ds.Tables(0).Rows.Count > 0 Then
                        msgErro.Visible = True
                        lblErro.Text = "Já existe grupo de usuário cadastrado com nome"
                    Else
                        Dim ID As String = txtID.Text
                        Dim Admin As String
                        If ckbAdmin.Checked = True Then
                            Admin = 1
                        Else
                            Admin = 0
                        End If

                        ds = Con.ExecutarQuery("UPDATE [dbo].[TB_TIPO_USUARIO] SET [NM_TIPO_USUARIO] = '" & txtDescricao.Text & "' , FL_ADMIN =  " & Admin & " WHERE ID_TIPO_USUARIO =" & ID & "; SELECT CAST(SCOPE_IDENTITY() AS INT)")
                        Dim ID_TIPO_USUARIO As String = ID
                        If ckbAdmin.Checked Then
                            Call Permissoes(ID_TIPO_USUARIO)
                        End If
                        Con.Fechar()
                        txtDescricao.Text = ""
                        txtID.Text = ""
                        divmsg.Visible = True
                        dgvUsuariosGrupos.DataBind()
                    End If

                End If

            Else
                msgErro.Visible = True
                lblErro.Text = "Usuário não possui permissão para alterar."
            End If

        End If


    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("CadastrarUsuarioGrupo.aspx")

    End Sub

    Private Sub dgvUsuariosGrupos_PreRender(sender As Object, e As EventArgs) Handles dgvUsuariosGrupos.PreRender
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_EXCLUIR,FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then

                dgvUsuariosGrupos.Columns(2).Visible = False

            End If
            If ds.Tables(0).Rows(0).Item("FL_EXCLUIR") <> True Then

                dgvUsuariosGrupos.Columns(3).Visible = False

            End If

        Else
            dgvUsuariosGrupos.Columns(2).Visible = False
            dgvUsuariosGrupos.Columns(3).Visible = False
        End If
        Con.Fechar()

    End Sub

    Protected Sub dgvUsuariosGrupos_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvUsuariosGrupos.DataSource = Session("TaskTable")
            dgvUsuariosGrupos.DataBind()
            dgvUsuariosGrupos.HeaderRow.TableSection = TableRowSection.TableHeader
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

    Sub Permissoes(ID_TIPO_USUARIO)
        dsMenus.SelectCommand = dsMenus.SelectCommand.Replace("0", ID_TIPO_USUARIO)
        dgvMenus.DataBind()

        Dim Con As New Conexao_sql
        Dim grupo As Integer = ID_TIPO_USUARIO
        Con.Conectar()
        For Each linha As GridViewRow In dgvMenus.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(*)Qtd FROM [TB_GRUPO_PERMISSAO] where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)

            If ds.Tables(0).Rows(0).Item("Qtd") = 0 Then

                ID = CType(linha.FindControl("lblID"), Label).Text

                Con.ExecutarQuery("INSERT INTO [dbo].[TB_GRUPO_PERMISSAO](ID_TIPO_USUARIO, ID_MENU, DT_CADASTRO, FL_Acessar,FL_Cadastrar,FL_Atualizar,FL_Excluir) VALUES (" & grupo & "," & ID & ",GetDate(),1,1,1,1)")
            Else
                ID = CType(linha.FindControl("lblID"), Label).Text

                Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Acessar] = 1,[FL_Cadastrar] = 1,[FL_Atualizar] = 1, [FL_Excluir] = 1  where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)
            End If

        Next
        Con.Fechar()

    End Sub


    Private Sub dgvUsuariosGrupos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvUsuariosGrupos.RowCommand
        diverro.Visible = False
        divInfo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ID_TIPO_USUARIO As String = e.CommandArgument
        If e.CommandName = "excluir" Then
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_USUARIO FROM [TB_USUARIO] where ID_TIPO_USUARIO =" & ID_TIPO_USUARIO)

            If ds.Tables(0).Rows.Count > 0 Then
                diverro.Visible = True
                lblErroExcluir.Text = "Não foi possível excluir o registro: existe um ou mais usuários vinculados a este tipo de usuário"
            Else
                Con.ExecutarQuery(" DELETE From [dbo].[TB_TIPO_USUARIO] Where ID_TIPO_USUARIO = " & ID_TIPO_USUARIO)
                dgvUsuariosGrupos.DataBind()
                divInfo.Visible = True
                lblInfo.Text = "O grupo de usuário foi excluído com sucesso"
            End If
            Con.Fechar()
        End If

    End Sub

End Class