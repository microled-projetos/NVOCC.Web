Public Class BaixasCancelamentos
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
            If Request.QueryString("t") = "p" Then
                lblTipo.Text = "CONTAS A PAGAR"
                botoesPagamento.Visible = True
                botoesRecebimento.Visible = False
                gridPagar.Visible = True
                gridReceber.Visible = False

            ElseIf Request.QueryString("t") = "r" Then
                lblTipo.Text = "CONTAS A RECEBER"
                botoesPagamento.Visible = False
                botoesRecebimento.Visible = True
                gridPagar.Visible = False
                gridReceber.Visible = True

            End If
        End If
        Con.Fechar()
    End Sub

End Class