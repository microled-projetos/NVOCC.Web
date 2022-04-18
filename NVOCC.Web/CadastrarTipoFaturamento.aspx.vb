Public Class CadastrarTipoFaturamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Request.QueryString("p") = "" Then

            Response.Redirect("Default.aspx")
        Else

            If Not IsPostBack Then
                txtIDParceiro.Text = Request.QueryString("p")
                gvTiposFaturamentoParceiro.DataBind()
                Dim Con As New Conexao_sql
                Dim ds As DataSet
                Con.Conectar()
                ds = Con.ExecutarQuery("SELECT NM_FANTASIA FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO = " & txtIDParceiro.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    lblRazaoSocial.Text = ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
                End If
                If Request.QueryString("id") <> "" Then
                    txtID.Text = Request.QueryString("id")

                    Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ID_TIPO_FATURAMENTO, ID_SERVICO, ID_INCOTERM, FL_FREE_HAND FROM [dbo].[TB_PARCEIRO_TIPO_FATURAMENTO] WHERE ID_PARCEIRO = " & txtIDParceiro.Text & " AND ID = " & txtID.Text)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        ddlTipoFaturamento.SelectedValue = ds1.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO").ToString()
                        ddlServico.SelectedValue = ds1.Tables(0).Rows(0).Item("ID_SERVICO").ToString()
                        ddlIncoterm.SelectedValue = ds1.Tables(0).Rows(0).Item("ID_INCOTERM").ToString()
                        ckbFreeHand.Checked = ds1.Tables(0).Rows(0).Item("FL_FREE_HAND").ToString()
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divSuccess.Visible = False
        divErro.Visible = False
        divSuccessExclusao.Visible = False

        If txtIDParceiro.Text = "" Then
            msgErro.Text = "Parceiro não informado."
            divErro.Visible = True

        ElseIf ddlTipoFaturamento.SelectedValue = 0 Then
            msgErro.Text = "Selecione um tipo de faturamento."
            divErro.Visible = True

        ElseIf ddlServico.SelectedValue = 0 Then
            msgErro.Text = "Tipo de serviço é obrigatorio!"
            divErro.Visible = True

        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim SQL As String = ""

            'insert 
            If txtID.Text = "" Then

                Dim ds As DataSet = Con.ExecutarQuery("SELECT Count(*) QTD FROM [dbo].[TB_PARCEIRO_TIPO_FATURAMENTO] WHERE ID_TIPO_FATURAMENTO = " & ddlTipoFaturamento.SelectedValue & " AND ID_SERVICO = " & ddlServico.SelectedValue & " AND ID_INCOTERM =" & ddlIncoterm.SelectedValue & " AND FL_FREE_HAND = '" & ckbFreeHand.Checked & "' AND ID_PARCEIRO = " & txtIDParceiro.Text)
                If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                    msgErro.Text = "Já existe cadastro de tipo de faturamento com os parametros selecionados!"
                    divErro.Visible = True

                Else
                    SQL = "INSERT INTO [dbo].[TB_PARCEIRO_TIPO_FATURAMENTO] (ID_PARCEIRO, ID_SERVICO, ID_TIPO_FATURAMENTO, ID_INCOTERM, FL_FREE_HAND) VALUES (" & txtIDParceiro.Text & ", " & ddlServico.SelectedValue & "," & ddlTipoFaturamento.SelectedValue & "," & ddlIncoterm.SelectedValue & ",'" & ckbFreeHand.Checked & "' )"
                    Con.ExecutarQuery(SQL)
                    limpar()
                    divSuccess.Visible = True
                End If

            Else
                'update
                SQL = "UPDATE [dbo].[TB_PARCEIRO_TIPO_FATURAMENTO] SET ID_SERVICO = " & ddlServico.SelectedValue & ", ID_TIPO_FATURAMENTO = " & ddlTipoFaturamento.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm.SelectedValue & ", FL_FREE_HAND = '" & ckbFreeHand.Checked & "' WHERE ID_PARCEIRO = " & txtIDParceiro.Text & " AND ID = " & txtID.Text
                Con.ExecutarQuery(SQL)
                divSuccess.Visible = True
            End If


            Con.Fechar()
            gvTiposFaturamentoParceiro.DataBind()
        End If
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("CadastrarTipoFaturamento.aspx?p=" & txtIDParceiro.Text)
    End Sub

    Sub limpar()
        ddlTipoFaturamento.SelectedValue = 0
        ddlServico.SelectedValue = 0
        ddlIncoterm.SelectedValue = 0
        ckbFreeHand.Checked = False
        txtID.Text = ""
    End Sub
    Private Sub gvTiposFaturamentoParceiro_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTiposFaturamentoParceiro.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        divSuccessExclusao.Visible = False
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument
            Dim Con As New Conexao_sql
            Con.Conectar()
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_PARCEIRO_TIPO_FATURAMENTO] WHERE ID =" & ID)
            Con.Fechar()
            gvTiposFaturamentoParceiro.DataBind()
            divSuccessExclusao.Visible = True
            lblSuccessExclusao.Text = "Registro deletado com sucesso!"
        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument
            Dim Con As New Conexao_sql
            Con.Conectar()
            Con.ExecutarQuery("INSERT INTO [dbo].[TB_PARCEIRO_TIPO_FATURAMENTO] (ID_PARCEIRO,ID_TIPO_FATURAMENTO, ID_SERVICO,ID_INCOTERM,FL_FREE_HAND) SELECT ID_PARCEIRO, ID_TIPO_FATURAMENTO,ID_SERVICO,ID_INCOTERM,FL_FREE_HAND FROM TB_PARCEIRO_TIPO_FATURAMENTO WHERE ID =" & ID)
            Con.Fechar()
            gvTiposFaturamentoParceiro.DataBind()
            divSuccessExclusao.Visible = True
            lblSuccessExclusao.Text = "Registro duplicado com sucesso!"
        End If
    End Sub
End Class