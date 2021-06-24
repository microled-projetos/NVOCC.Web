Public Class SOA_I
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""


        If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
            FILTRO = "WHERE ID_PARCEIRO_AGENTE = " & Request.QueryString("ag")
        Else
            FILTRO = ""
        End If
        Dim tabela As String = ""



        Dim titulo As String = ""
        Dim ds As DataSet = Con.ExecutarQuery("SELECT DISTINCT ID_PARCEIRO_AGENTE,ID_MOEDA,NM_AGENTE,SIGLA_MOEDA FROM FN_ACCOUNT_INVOICE('" & Session("Vencimento_Inicial") & "','" & Session("Vencimento_Final") & "') " & FILTRO)

        For Each linhaTitulo As DataRow In ds.Tables(0).Rows
            Dim PARCEIRO As String = linhaTitulo("ID_PARCEIRO_AGENTE")
            Dim MOEDA As String = linhaTitulo("ID_MOEDA")

            tabela &= "<h5>AGENTE: " & linhaTitulo("NM_AGENTE") & "<br/>CURRENCY: " & linhaTitulo("SIGLA_MOEDA") & "</h5>"

            tabela &= "<table style='font-size:10px'>"
            tabela &= "<tr><td><strong>CUSTOMER</strong></td>"
            tabela &= "<td><strong>POL</strong></td>"
            tabela &= "<td><strong>DEST</strong></td>"
            tabela &= "<td><strong>NR_BL</strong></td>"
            tabela &= "<td><strong>ETD</strong></td>"
            tabela &= "<td><strong>ETA</strong></td>"
            tabela &= "<td><strong>INVOICE</strong></td>"
            tabela &= "<td><strong>DATE</strong></td>"
            tabela &= "<td><strong>CURR</strong></td>"
            tabela &= "<td><strong>TOTAL</strong></td>"
            tabela &= "<td><strong>TOTAL R$</strong></td>"
            tabela &= "<td><strong>C/D</strong></td></tr>"



            Dim dsdados As DataSet = Con.ExecutarQuery("SELECT DISTINCT B.ID_ACCOUNT_INVOICE,PARCEIRO_CLIENTE,B.NR_INVOICE,DT_INVOICE,SIGLA_MOEDA,NM_ACCOUNT_TIPO_FATURA,ORIGEM,DESTINO,NR_BL,GRAU,DT_EMBARQUE,DT_CHEGADA,SUM(ISNULL(VL_TAXA,0))VL_TAXA,SUM(ISNULL(VL_TAXA_BR,0))VL_TAXA_BR
FROM [dbo].[View_BL]  A 
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("Vencimento_Inicial") & "','" & Session("Vencimento_Final") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
WHERE B.ID_PARCEIRO_AGENTE = " & PARCEIRO & " AND ID_MOEDA = " & MOEDA & "
GROUP BY B.ID_ACCOUNT_INVOICE,B.NR_INVOICE,ORIGEM,DESTINO,NR_BL,GRAU,DT_EMBARQUE,DT_CHEGADA,PARCEIRO_CLIENTE,DT_INVOICE,SIGLA_MOEDA,NM_ACCOUNT_TIPO_FATURA")
            For Each linhadados As DataRow In dsdados.Tables(0).Rows

                tabela &= "<tr><td>" & linhadados("PARCEIRO_CLIENTE") & "</td>"
                tabela &= "<td>" & linhadados("ORIGEM") & "</td>"
                tabela &= "<td>" & linhadados("DESTINO") & "</td>"
                tabela &= "<td>" & linhadados("NR_BL") & "</td>"
                tabela &= "<td>" & linhadados("DT_EMBARQUE") & "</td>"
                tabela &= "<td>" & linhadados("DT_CHEGADA") & "</td>"
                tabela &= "<td>" & linhadados("NR_INVOICE") & "</td>"
                tabela &= "<td>" & linhadados("DT_INVOICE") & "</td>"
                tabela &= "<td>" & linhadados("SIGLA_MOEDA") & "</td>"
                tabela &= "<td>" & linhadados("VL_TAXA") & "</td>"
                tabela &= "<td>" & linhadados("VL_TAXA_BR") & "</td>"
                tabela &= "<td>" & linhadados("NM_ACCOUNT_TIPO_FATURA") & "</td></tr>"


            Next


        Next
        tabela &= "</table></div>"
        divConteudoDinamico.InnerHtml &= tabela

        Con.Fechar()
    End Sub

End Class