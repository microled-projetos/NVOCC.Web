Public Class TextoCotacao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Not Page.IsPostBack Then
            carregatexto()

        End If
    End Sub
    Sub CarregaTexto()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim SQL As String = ""
        If rdTipo.SelectedValue = 2 Then
            SQL = "SELECT ISNULL(TEXTO_COTACAO_INGLES,'')TEXTO FROM TB_PARAMETROS"
        ElseIf rdTipo.SelectedValue = 1 Then
            SQL = "SELECT ISNULL(TEXTO_COTACAO_PORTUGUES,'')TEXTO FROM TB_PARAMETROS"
        End If

        Dim ds As DataSet = Con.ExecutarQuery(SQL)
        If ds.Tables(0).Rows.Count > 0 Then
            txtTexto.Text = ds.Tables(0).Rows(0).Item("TEXTO").ToString()
            txtTexto.Text = txtTexto.Text.Replace("<br/>", vbNewLine)
            txtTexto.Text = txtTexto.Text.Replace("<div style='text-align:center' >", "")
            txtTexto.Text = txtTexto.Text.Replace("<div>", "")
            txtTexto.Text = txtTexto.Text.Replace("</div>", "")

        End If
        Con.Fechar()

    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        txtTexto.Text = txtTexto.Text.Replace(vbNewLine, "<br/>")
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim SQL As String = ""
        If rdTipo.SelectedValue = 2 Then
            SQL = "UPDATE TB_PARAMETROS SET TEXTO_COTACAO_INGLES = '" & txtTexto.Text & "'"
        ElseIf rdTipo.SelectedValue = 1 Then
            SQL = "UPDATE TB_PARAMETROS SET TEXTO_COTACAO_PORTUGUES= '" & txtTexto.Text & "'"
        End If
        Con.ExecutarQuery(SQL)
        Con.Fechar()

    End Sub
End Class