Public Class ClienteFinal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            ddlParceiro.SelectedValue = Request.QueryString("id")
        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 23 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
    End Sub

    Private Sub txtCNPJ_TextChanged(sender As Object, e As EventArgs) Handles txtCNPJ.TextChanged
         Dim Con As New Conexao_sql
            Dim ID As String = Request.QueryString("id")
            Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO,NR_CNPJ,NM_CLIENTE_FINAL FROM [dbo].[TB_CLIENTE_FINAL] WHERE NR_CNPJ ='" & txtCNPJ.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            txtNome.Text = ds.Tables(0).Rows(0).Item("NM_CLIENTE_FINAL").ToString()
        End If

    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divmsg.Visible = False
        divmsg1.Visible = False

        If txtNome.Text = "" Then
            msgErro.Text = "Campo Nome é obrigatório."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtCNPJ.Text = "" Then
            msgErro.Text = "Campo CNPJ é obrigatório."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ValidaCNPJ.Validar(txtCNPJ.Text) = False Then
            msgErro.Text = "CNPJ Inválido."
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf ddlParceiro.SelectedValue = 0 Then
            msgErro.Text = "Selecione um parceiro"
            divmsg1.Visible = True
            msgErro.Visible = True
        Else


            Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_CLIENTE_FINAL,ID_PARCEIRO,NR_CNPJ,NM_CLIENTE_FINAL FROM [dbo].[TB_CLIENTE_FINAL] WHERE NR_CNPJ ='" & txtCNPJ.Text & "' and ID_PARCEIRO = " & ddlParceiro.SelectedValue)
            If ds.Tables(0).Rows.Count > 0 Then
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 23 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para alterar."
                Else
                    Dim ID As String = ds.Tables(0).Rows(0).Item("ID_CLIENTE_FINAL").ToString()
                    ds = Con.ExecutarQuery("UPDATE [dbo].[TB_CLIENTE_FINAL] SET [NR_CNPJ] = '" & txtCNPJ.Text & "', ID_PARCEIRO = " & ddlParceiro.SelectedValue & ", NM_CLIENTE_FINAL = '" & txtNome.Text & "' WHERE ID_CLIENTE_FINAL =" & ID)
                    txtNome.Text = ""
                    txtCNPJ.Text = ""
                    ddlParceiro.SelectedValue = 0
                    divmsg.Visible = True

                End If


            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 23 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para cadastrar."
                Else

                    ds = Con.ExecutarQuery("INSERT INTO [dbo].[TB_CLIENTE_FINAL] (ID_PARCEIRO,NR_CNPJ, NM_CLIENTE_FINAL) VALUES (" & ddlParceiro.SelectedValue & ",'" & txtCNPJ.Text & "' ,'" & txtNome.Text & "'); SELECT CAST(SCOPE_IDENTITY() AS INT)")
                    txtNome.Text = ""
                    txtCNPJ.Text = ""
                    ddlParceiro.SelectedValue = 0
                    divmsg.Visible = True
                End If


            End If
            Con.Fechar()
        End If
        dgvCliente.DataBind()

    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("ClienteFinal.aspx")
    End Sub


    Protected Sub dgvCliente_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvCliente.DataSource = Session("TaskTable")
            dgvCliente.DataBind()
            dgvCliente.HeaderRow.TableSection = TableRowSection.TableHeader
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
    Private Sub dgvCliente_PreRender(sender As Object, e As EventArgs) Handles dgvCliente.PreRender

        Dim Con As New Conexao_sql
        Con.Conectar()

        'verifica se o usuario tem permissão de exclusão de Cliente Final
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 23 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvCliente.Columns(4).Visible = False
        End If
    End Sub
End Class