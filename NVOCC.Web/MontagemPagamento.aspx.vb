Public Class MontagemPagamento
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
            '    If Request.QueryString("id") <> "" Then
            '        txtID_BL.Text = Request.QueryString("id")

            '    End If

            txtVencimento.Text = Now.Date.ToString("dd-MM-yyyy")
            txtDataFatura.Text = Now.Date.ToString("dd-MM-yyyy")

        End If
        Con.Fechar()

    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As EventArgs) Handles btnDesmarcar.Click
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbCalculado = CType(Me.dgvTaxas.Rows(i).FindControl("ckbCalculado"), CheckBox)
            ckbCalculado.Checked = False
        Next
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        For i As Integer = 0 To Me.dgvTaxas.Rows.Count - 1
            Dim ckbCalculado = CType(Me.dgvTaxas.Rows(i).FindControl("ckbCalculado"), CheckBox)
            ckbCalculado.Checked = True
        Next
    End Sub
End Class