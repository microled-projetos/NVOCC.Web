Public Class ContatoParceiro
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
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
    End Sub

    Sub CarregaCampos()
        'PREENCHE CAMPOS DE CONTATO
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT [ID_CONTATO],[ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO] FROM [dbo].[TB_CONTATO] WHERE ID_PARCEIRO =" & ID)
        If ds.Tables(0).Rows.Count > 0 Then
            txtNomeContato.Text = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
            txtDepartamento.Text = ds.Tables(0).Rows(0).Item("NM_DEPARTAMENTO").ToString()
            txtTelContato.Text = ds.Tables(0).Rows(0).Item("TELEFONE_CONTATO").ToString()
            txtEmailContato.Text = ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
            txtCelularContato.Text = ds.Tables(0).Rows(0).Item("CELULAR_CONTATO").ToString()

        End If
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divmsg.Visible = False
        divmsg1.Visible = False

        If txtNomeContato.Text = "" Then
            msgErro.Text = "Campo Nome é obrigatório."
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf ddlParceiro.SelectedValue = 0 Then
            msgErro.Text = "Contato sem parceiro vinculado"
            divmsg1.Visible = True
            msgErro.Visible = True
        Else


            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet
            If txtID.Text <> "" Then
                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para alterar."
                Else
                    If txtNomeContato.Text = "" Then
                        txtNomeContato.Text = "NULL"
                    Else
                        txtNomeContato.Text = "'" & txtNomeContato.Text & "'"
                    End If

                    If txtTelContato.Text = "" Then
                        txtTelContato.Text = "NULL"
                    Else
                        txtTelContato.Text = "'" & txtTelContato.Text & "'"
                    End If

                    If txtEmailContato.Text = "" Then
                        txtEmailContato.Text = "NULL"
                    Else
                        txtEmailContato.Text = "'" & txtEmailContato.Text & "'"
                    End If

                    If txtDepartamento.Text = "" Then
                        txtDepartamento.Text = "NULL"
                    Else
                        txtDepartamento.Text = "'" & txtDepartamento.Text & "'"
                    End If

                    If txtCelularContato.Text = "" Then
                        txtCelularContato.Text = "NULL"
                    Else
                        txtCelularContato.Text = "'" & txtCelularContato.Text & "'"
                    End If



                    Dim ID As String = txtID.Text

                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [NM_CONTATO] = " & txtNomeContato.Text & " ,[TELEFONE_CONTATO] =" & txtTelContato.Text & ",[EMAIL_CONTATO] =  " & txtEmailContato.Text & ",[NM_DEPARTAMENTO] =" & txtDepartamento.Text & ", [CELULAR_CONTATO] =" & txtCelularContato.Text & " where ID_CONTATO = " & ID)

                    divmsg.Visible = True

                    txtNomeContato.Text = txtTelContato.Text.Replace("NULL", "")
                    txtTelContato.Text = txtTelContato.Text.Replace("NULL", "")
                    txtEmailContato.Text = txtEmailContato.Text.Replace("NULL", "")
                    txtDepartamento.Text = txtDepartamento.Text.Replace("NULL", "")
                    txtCelularContato.Text = txtCelularContato.Text.Replace("NULL", "")
                End If


            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para cadastrar."
                Else

                    If txtNomeContato.Text = "" Then
                        txtNomeContato.Text = "NULL"
                    Else
                        txtNomeContato.Text = "'" & txtNomeContato.Text & "'"
                    End If

                    If txtTelContato.Text = "" Then
                        txtTelContato.Text = "NULL"
                    Else
                        txtTelContato.Text = "'" & txtTelContato.Text & "'"
                    End If

                    If txtEmailContato.Text = "" Then
                        txtEmailContato.Text = "NULL"
                    Else
                        txtEmailContato.Text = "'" & txtEmailContato.Text & "'"
                    End If

                    If txtDepartamento.Text = "" Then
                        txtDepartamento.Text = "NULL"
                    Else
                        txtDepartamento.Text = "'" & txtDepartamento.Text & "'"
                    End If

                    If txtCelularContato.Text = "" Then
                        txtCelularContato.Text = "NULL"
                    Else
                        txtCelularContato.Text = "'" & txtCelularContato.Text & "'"
                    End If


                    Con.ExecutarQuery("INSERT INTO TB_CONTATO ([ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO]) VALUES (" & ddlParceiro.SelectedValue & "," & txtNomeContato.Text & "," & txtTelContato.Text & "," & txtEmailContato.Text & "," & txtDepartamento.Text & ", " & txtCelularContato.Text & ")")

                    txtID.Text = ""
                    txtNomeContato.Text = ""
                    txtTelContato.Text = ""
                    txtCelularContato.Text = ""
                    txtEmailContato.Text = ""
                    txtDepartamento.Text = ""

                    divmsg.Visible = True
                End If


            End If
            Con.Fechar()
        End If
        dgvContato.DataBind()

    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("ClienteFinal.aspx")
    End Sub


    Protected Sub dgvContato_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvContato.DataSource = Session("TaskTable")
            dgvContato.DataBind()
            dgvContato.HeaderRow.TableSection = TableRowSection.TableHeader
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
    Private Sub dgvContato_PreRender(sender As Object, e As EventArgs) Handles dgvContato.PreRender

        Dim Con As New Conexao_sql
        Con.Conectar()

        'verifica se o usuario tem permissão de exclusão de Cliente Final
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvContato.Columns(4).Visible = False
        End If
    End Sub

    Private Sub dgvContato_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dgvContato.RowDeleted
        lblSuccesgrid.Text = "Registro deletado com sucesso!"
        divSuccesgrid.Visible = True
    End Sub
End Class