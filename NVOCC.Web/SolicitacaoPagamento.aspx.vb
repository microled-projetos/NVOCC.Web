Public Class SolicitacaoPagamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2027 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            If Request.QueryString("id") <> "" Then
                txtID_BL.Text = Request.QueryString("id")
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT NR_BL FROM TB_BL WHERE ID_BL = " & txtID_BL.Text)
                If ds1.Tables(0).Rows.Count > 0 Then
                    lblMBL.Text = ds1.Tables(0).Rows(0).Item("NR_BL")
                End If
            End If
        End If
        Con.Fechar()


    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Financeiro.aspx")
    End Sub

    Private Sub ddlFornecedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFornecedor.SelectedIndexChanged
        If ddlFornecedor.SelectedValue <> 0 Then
            dsTaxas.SelectCommand = "SELECT * FROM [dbo].[View_BL_TAXAS]
WHERE ID_BL = " & txtID_BL.Text & "  AND ID_PARCEIRO_EMPRESA = " & ddlFornecedor.SelectedValue
            dgvTaxas.DataBind()


            dsTaxas.SelectParameters("ID_BL").DefaultValue = txtID_BL.Text
            dgvTaxas.DataBind()
        Else
            dgvTaxas.DataBind()
        End If

    End Sub


End Class