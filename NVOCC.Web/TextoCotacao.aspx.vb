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




        If rdTipoLinguagem.SelectedValue = 1 Then
            'PORTUGUES

            If rdTipoTransporte.SelectedValue = 1 Then
                'MARITIMO
                SQL = "SELECT ISNULL(TEXTO_COTACAO_PT_MAR,'')TEXTO FROM TB_PARAMETROS"
            ElseIf rdTipoTransporte.SelectedValue = 2 Then
                'AEREO
                SQL = "SELECT ISNULL(TEXTO_COTACAO_PT_AER,'')TEXTO FROM TB_PARAMETROS"

            End If

        ElseIf rdTipoLinguagem.SelectedValue = 2 Then
            'INGLES

            If rdTipoTransporte.SelectedValue = 1 Then
                'MARITIMO
                SQL = "SELECT ISNULL(TEXTO_COTACAO_ING_MAR,'')TEXTO FROM TB_PARAMETROS"
            ElseIf rdTipoTransporte.SelectedValue = 2 Then
                'AEREO
                SQL = "SELECT ISNULL(TEXTO_COTACAO_ING_AER,'')TEXTO FROM TB_PARAMETROS"

            End If
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
        If rdTipoLinguagem.SelectedValue = 1 Then
            'PORTUGUES

            If rdTipoTransporte.SelectedValue = 1 Then
                'MARITIMO
                SQL = "UPDATE TB_PARAMETROS SET TEXTO_COTACAO_PT_MAR = '" & txtTexto.Text & "'"
            ElseIf rdTipoTransporte.SelectedValue = 2 Then
                'AEREO
                SQL = "UPDATE TB_PARAMETROS SET TEXTO_COTACAO_PT_AER = '" & txtTexto.Text & "'"

            End If

        ElseIf rdTipoLinguagem.SelectedValue = 2 Then
            'INGLES

            If rdTipoTransporte.SelectedValue = 1 Then
                'MARITIMO
                SQL = "UPDATE TB_PARAMETROS SET TEXTO_COTACAO_ING_MAR = '" & txtTexto.Text & "'"
            ElseIf rdTipoTransporte.SelectedValue = 2 Then
                'AEREO
                SQL = "UPDATE TB_PARAMETROS SET TEXTO_COTACAO_ING_AER = '" & txtTexto.Text & "'"

            End If
        End If
        Con.ExecutarQuery(SQL)
        Con.Fechar()
        txtTexto.Text = txtTexto.Text.Replace("<br/>", vbNewLine)
        txtTexto.Text = txtTexto.Text.Replace("<div style='text-align:center' >", "")
        txtTexto.Text = txtTexto.Text.Replace("<div>", "")
        txtTexto.Text = txtTexto.Text.Replace("</div>", "")

    End Sub

    Private Sub rdTipordTipoLinguagem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdTipoLinguagem.SelectedIndexChanged
        CarregaTexto()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        txtTexto.Text = ""
    End Sub

    Private Sub rdTipoTransporte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdTipoTransporte.SelectedIndexChanged
        CarregaTexto()
    End Sub
End Class