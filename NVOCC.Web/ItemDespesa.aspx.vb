Public Class ItemDespesa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        divmsg.Visible = False


        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            CarregaCampos()
        Else
            ckbAtivo.Checked = True
            ckbIntegraPA.Checked = True
        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        'Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        'If ds.Tables(0).Rows.Count > 0 Then

        '    If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

        '        Response.Redirect("Default.aspx")

        '    End If

        'Else
        '    Response.Redirect("Default.aspx")
        'End If

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    Public Sub CarregaCampos()
        divInfo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        ds = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA, NM_ITEM_DESPESA, ID_TIPO_ITEM_DESPESA, CD_NATUREZA, FL_INTEGRA_PA, FL_ATIVO,FL_PREMIACAO FROM [dbo].[TB_ITEM_DESPESA] WHERE ID_ITEM_DESPESA = " & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then
            txtIDItemDespesa.Text = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA").ToString()
            txtNome.Text = ds.Tables(0).Rows(0).Item("NM_ITEM_DESPESA").ToString()
            txtNatureza.Text = ds.Tables(0).Rows(0).Item("CD_NATUREZA").ToString()
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ITEM_DESPESA")) Then
                ddlTipoItemDespesa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ITEM_DESPESA").ToString()
            Else
                ddlTipoItemDespesa.SelectedValue = 0
            End If

            ckbAtivo.Checked = ds.Tables(0).Rows(0).Item("FL_ATIVO").ToString()
            ckbIntegraPA.Checked = ds.Tables(0).Rows(0).Item("FL_INTEGRA_PA").ToString()
            ckbPremiacao.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO").ToString()

        End If
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("ItemDespesa.aspx")
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divErro.Visible = False
        divmsg.Visible = False
        divInfo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtNome.Text = "" Then
            lblErro.Text = "Preencha o campo Nome."
            divErro.Visible = True

        Else

            If txtNatureza.Text = "" Then
                txtNatureza.Text = "NULL"
            Else
                txtNatureza.Text = "'" & txtNatureza.Text & "'"
            End If

            Dim TipoItemDespesa As String = ""
            If ddlTipoItemDespesa.SelectedValue = 0 Then
                TipoItemDespesa = " NULL"
            Else
                TipoItemDespesa = ddlTipoItemDespesa.SelectedValue
            End If


            If txtIDItemDespesa.Text = "" Then
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblErro.Text = "Usuário não possui permissão para cadastrar."
                Else

                    ds = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA FROM [TB_ITEM_DESPESA] WHERE NM_ITEM_DESPESA = '" & txtNome.Text & "' AND FL_ATIVO = 1")

                    If ds.Tables(0).Rows.Count > 0 Then

                        lblErro.Text = "Já existe despesa com este nome."
                        divErro.Visible = True
                        txtNatureza.Text = ""


                    Else
                        Con.ExecutarQuery("INSERT INTO [dbo].[TB_ITEM_DESPESA] (ID_TIPO_ITEM_DESPESA,NM_ITEM_DESPESA, CD_NATUREZA, FL_ATIVO,FL_INTEGRA_PA,FL_PREMIACAO) VALUES (" & TipoItemDespesa & " , '" & txtNome.Text & "' , " & txtNatureza.Text & " , '" & ckbAtivo.Checked & "', '" & ckbIntegraPA.Checked & "','" & ckbPremiacao.Checked & "')")
                        Con.Fechar()

                        divmsg.Visible = True
                        dgvItemDespesa.DataBind()
                        txtIDItemDespesa.Text = ""
                        txtNome.Text = ""
                        txtNatureza.Text = ""
                        ddlTipoItemDespesa.SelectedValue = 0
                    End If
                End If


                'ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                'If ds.Tables(0).Rows.Count > 0 Then

                '    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                '        divErro.Visible = True
                '        lblErro.Text = "Usuário não possui permissão para cadastrar."
                '    Else

                '        ds = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA FROM [TB_ITEM_DESPESA] WHERE NM_ITEM_DESPESA = '" & txtNome.Text & "' AND FL_ATIVO = 1")

                '        If ds.Tables(0).Rows.Count > 0 Then

                '            lblErro.Text = "Já existe despesa com este nome."
                '            divErro.Visible = True
                '            txtNatureza.Text = ""


                '        Else
                '            Con.ExecutarQuery("INSERT INTO [dbo].[TB_ITEM_DESPESA] (ID_TIPO_ITEM_DESPESA,NM_ITEM_DESPESA, CD_NATUREZA, FL_ATIVO,FL_INTEGRA_PA,FL_PREMIACAO) VALUES (" & TipoItemDespesa & " , '" & txtNome.Text & "' , " & txtNatureza.Text & " , '" & ckbAtivo.Checked & "', '" & ckbIntegraPA.Checked & "','" & ckbPremiacao.Checked & "')")
                '            Con.Fechar()

                '            divmsg.Visible = True
                '            dgvItemDespesa.DataBind()
                '            txtIDItemDespesa.Text = ""
                '            txtNome.Text = ""
                '            txtNatureza.Text = ""
                '            ddlTipoItemDespesa.SelectedValue = 0
                '        End If
                '    End If
                'Else
                '    divErro.Visible = True
                '    lblErro.Text = "Usuário não possui permissão para cadastrar."
                'End If

            Else
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblErro.Text = "Usuário não possui permissão para alterar."
                Else
                    ds = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA FROM [TB_ITEM_DESPESA] WHERE NM_ITEM_DESPESA = '" & txtNome.Text & "' AND FL_ATIVO = 1 AND ID_ITEM_DESPESA <> " & txtIDItemDespesa.Text)
                    If ds.Tables(0).Rows.Count > 0 Then

                        lblErro.Text = "Já existe despesa com este nome."
                        divErro.Visible = True
                        txtNatureza.Text = ""

                    Else
                        Con.ExecutarQuery("UPDATE [dbo].[TB_ITEM_DESPESA] SET ID_TIPO_ITEM_DESPESA = " & TipoItemDespesa & " , NM_ITEM_DESPESA = '" & txtNome.Text & "' , CD_NATUREZA = " & txtNatureza.Text & " , FL_ATIVO = '" & ckbAtivo.Checked & "' ,FL_INTEGRA_PA = '" & ckbIntegraPA.Checked & "', FL_PREMIACAO = '" & ckbPremiacao.Checked & "' WHERE ID_ITEM_DESPESA = " & txtIDItemDespesa.Text)
                        Con.Fechar()
                        txtIDItemDespesa.Text = ""
                        txtNome.Text = ""
                        txtNatureza.Text = ""
                        ddlTipoItemDespesa.SelectedValue = 0
                        divmsg.Visible = True
                        dgvItemDespesa.DataBind()
                    End If

                End If


            End If


        End If

    End Sub

    Private Sub dgvItemDespesa_PreRender(sender As Object, e As EventArgs) Handles dgvItemDespesa.PreRender
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        '= Con.ExecutarQuery("SELECT FL_EXCLUIR,FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvItemDespesa.Columns(4).Visible = False
        End If

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 22 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvItemDespesa.Columns(5).Visible = False
        End If



        Con.Fechar()
    End Sub


    Protected Sub dgvItemDespesa_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvItemDespesa.DataSource = Session("TaskTable")
            dgvItemDespesa.DataBind()
            dgvItemDespesa.HeaderRow.TableSection = TableRowSection.TableHeader
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
    Private Sub dgvItemDespesa_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dgvItemDespesa.RowDeleted
        dgvItemDespesa.DataBind()
        divInfo.Visible = True
        lblInfo.Text = "Registro excluído com sucesso"
    End Sub

    Private Sub dgvItemDespesa_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvItemDespesa.RowCommand
        divErro.Visible = False
        divInfo.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "desativar" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ATIVO FROM [TB_ITEM_DESPESA] WHERE ID_ITEM_DESPESA =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("FL_ATIVO") <> True Then

                    Con.ExecutarQuery("UPDATE [dbo].[TB_ITEM_DESPESA] SET FL_ATIVO = 1 WHERE ID_ITEM_DESPESA =" & ID)
                    dgvItemDespesa.DataBind()
                    divInfo.Visible = True
                    lblInfo.Text = "Item ativado com sucesso"
                Else
                    Con.ExecutarQuery("UPDATE [dbo].[TB_ITEM_DESPESA] SET FL_ATIVO = 0 WHERE ID_ITEM_DESPESA =" & ID)
                    dgvItemDespesa.DataBind()
                    divInfo.Visible = True
                    lblInfo.Text = "Item desativado com sucesso"

                End If
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub ddlConsulta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConsulta.SelectedIndexChanged

        If ddlConsulta.SelectedValue = 1 Then
            txtConsulta.Text = ""
            divPesquisa.Visible = True
            txtConsulta.CssClass = "form-control ApenasNumeros"
        ElseIf ddlConsulta.SelectedValue = 2 Then
            txtConsulta.Text = ""
            divPesquisa.Visible = True
            txtConsulta.CssClass = "form-control"
        Else
            divPesquisa.Visible = False
        End If
    End Sub

    Private Sub txtConsulta_TextChanged(sender As Object, e As EventArgs) Handles txtConsulta.TextChanged
        Dim FILTRO As String = ""

        If txtConsulta.Text = "" Then
            dsItemDespesa.SelectCommand = "SELECT A.ID_ITEM_DESPESA as Id,A.NM_ITEM_DESPESA,A.ID_TIPO_ITEM_DESPESA,B.NM_TIPO_ITEM_DESPESA,A.CD_NATUREZA,A.FL_INTEGRA_PA,A.FL_ATIVO, 
CASE WHEN A.FL_ATIVO = 1 THEN 'Sim' ELSE 'Não' end ATIVO,
CASE WHEN A.FL_INTEGRA_PA = 1 THEN 'Sim' ELSE 'Não' end INTEGRA_PA
FROM [dbo].[TB_ITEM_DESPESA] A
        LEFT JOIN [dbo].[TB_TIPO_ITEM_DESPESA] B ON B.ID_TIPO_ITEM_DESPESA =  A.ID_TIPO_ITEM_DESPESA /*FILTRO*/ "
            dgvItemDespesa.DataBind()
        Else
            If ddlConsulta.SelectedValue = 1 Then

                FILTRO = " WHERE ID_ITEM_DESPESA = " & txtConsulta.Text
                dsItemDespesa.SelectCommand = dsItemDespesa.SelectCommand.Replace("/*FILTRO*/ ", FILTRO)
                dgvItemDespesa.DataBind()


            Else

                FILTRO = " WHERE NM_ITEM_DESPESA = '" & txtConsulta.Text & "'"
                dsItemDespesa.SelectCommand = dsItemDespesa.SelectCommand.Replace("/*FILTRO*/ ", FILTRO)
                dgvItemDespesa.DataBind()


            End If
        End If

    End Sub
End Class