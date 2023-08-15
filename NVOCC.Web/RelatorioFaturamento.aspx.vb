Public Class RelatorioFaturamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""

        Dim tabela As String = ""


        If Session("RelFat") <> "" Then
            FILTRO = Session("RelFat")
        End If

        If Session("filtros") <> "" Then
            lblPesquisa.Text = Session("filtros")
        End If

        Dim titulo As String = ""
        Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_VENCIMENTO,NR_PROCESSO,NM_CLIENTE,REFERENCIA_CLIENTE,VL_NOTA_DEBITO,NR_NOTA_DEBITO,DT_NOTA_DEBITO,NR_RPS,DT_RPS,
NR_RECIBO,DT_RECIBO,NR_NOTA_FISCAL,DT_NOTA_FISCAL,DT_LIQUIDACAO,DT_CANCELAMENTO,NOSSONUMERO,ARQ_REM,NM_TIPO_FATURAMENTO FROM View_Faturamento " & FILTRO & " ORDER BY CONVERT(DATE,DT_CANCELAMENTO,103), CONVERT(DATE,DT_NOTA_DEBITO,103) ")
        If ds.Tables(0).Rows.Count > 0 Then
            Dim contador As Integer = 0
            Dim valores As Decimal = 0
            Dim valoresNF As Decimal = 0
            tabela &= "<table border='1' style='font-size:10px;'>"
            tabela &= "<tr><td><strong>Vencimento</strong></td>"
            tabela &= "<td style='width:100px !important'><strong>Processo</strong></td>"
            tabela &= "<td><strong>Cliente</strong></td>"
            tabela &= "<td><strong>Ref. Cliente</strong></td>"
            tabela &= "<td><strong>Valor Nota de Deb.</strong></td>"
            tabela &= "<td><strong>Nota de Deb.</strong></td>"
            tabela &= "<td><strong>Data de Nota de Deb.</strong></td>"
            tabela &= "<td><strong>RPS</strong></td>"
            tabela &= "<td><strong>Data RPS</strong></td>"
            tabela &= "<td><strong>Recibo</strong></td>"
            tabela &= "<td><strong>Data Recibo</strong></td>"
            tabela &= "<td><strong>Nota Fiscal</strong></td>"
            tabela &= "<td><strong>Data Nota Fiscal</strong></td>"
            tabela &= "<td><strong>Liquidação</strong></td>"
            tabela &= "<td><strong>Cancelamento</strong></td>"
            tabela &= "<td><strong>Nosso Número</strong></td>"
            tabela &= "<td><strong>Remessa</strong></td>"
            tabela &= "<td><strong>Faturamento</strong></td></tr>"


            For Each linhadados As DataRow In ds.Tables(0).Rows

                contador = contador + 1

                tabela &= "<tr><td>" & linhadados("DT_VENCIMENTO") & "</td>"
                tabela &= "<td>" & linhadados("NR_PROCESSO") & "</td>"
                tabela &= "<td>" & linhadados("NM_CLIENTE") & "</td>"
                tabela &= "<td>" & linhadados("REFERENCIA_CLIENTE") & "</td>"
                tabela &= "<td>" & linhadados("VL_NOTA_DEBITO") & "</td>"
                tabela &= "<td>" & linhadados("NR_NOTA_DEBITO") & "</td>"
                tabela &= "<td>" & linhadados("DT_NOTA_DEBITO") & "</td>"
                tabela &= "<td>" & linhadados("NR_RPS") & "</td>"
                tabela &= "<td>" & linhadados("DT_RPS") & "</td>"
                tabela &= "<td>" & linhadados("NR_RECIBO") & "</td>"
                tabela &= "<td>" & linhadados("DT_RECIBO") & "</td>"
                tabela &= "<td>" & linhadados("NR_NOTA_FISCAL") & "</td>"
                tabela &= "<td>" & linhadados("DT_NOTA_FISCAL") & "</td>"
                tabela &= "<td>" & linhadados("DT_LIQUIDACAO") & "</td>"
                tabela &= "<td>" & linhadados("DT_CANCELAMENTO") & "</td>"
                tabela &= "<td>" & linhadados("NOSSONUMERO") & "</td>"
                tabela &= "<td>" & linhadados("ARQ_REM") & "</td>"
                tabela &= "<td>" & linhadados("NM_TIPO_FATURAMENTO") & "</td></tr>"

                valores = valores + linhadados("VL_NOTA_DEBITO")


            Next



            ds = Con.ExecutarQuery("SELECT SUM (VL_NOTA)VL_NOTA FROM View_Faturamento " & FILTRO & " ")
            If ds.Tables(0).Rows.Count > 0 Then
                valoresNF = ds.Tables(0).Rows(0).Item("VL_NOTA")
            End If

            Dim Total As String = valores
            tabela &= "<tr style='border:none;font-weight:bold'><td>Qtd: " & contador & "</td><td></td><td></td><td></td></td><td>" & Total & "</td><td></td><td></td><td></td><td></td><td></td><td></td><td>" & valoresNF & "</td><td></td><td></td><td></td><td></td><td></td><td></tr>"
            tabela &= "</table>"
            divConteudoDinamico.InnerHtml &= tabela

        End If

        ds = Con.ExecutarQuery("SELECT NOME FROM TB_USUARIO WHERE ID_USUARIO = " & Session("ID_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then
            lblUsuario.Text = ds.Tables(0).Rows(0).Item("NOME")
        End If

        lblDataImpressao.Text = Now.ToString
        Con.Fechar()
    End Sub

End Class