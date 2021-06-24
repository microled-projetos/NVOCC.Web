Public Class SOA_II
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""


        If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
            FILTRO = "WHERE B.ID_PARCEIRO_AGENTE = " & Request.QueryString("ag")
        Else
            FILTRO = ""
        End If
        Dim tabela As String = ""

        tabela = "<table class='subtotal table table-bordered'>"
        tabela &= "<tr><td><strong>INVOICE</strong></td>"
        tabela &= "<td><strong>POL</strong></td>"
        tabela &= "<td><strong>DEST</strong></td>"
        tabela &= "<td><strong>NR_BL</strong></td>"
        tabela &= "<td><strong>ETD</strong></td>"
        tabela &= "<td><strong>ETA</strong></td>"
        tabela &= "<td><strong>FEE ITEM</strong></td>"
        tabela &= "<td><strong>CURR</strong></td>"
        tabela &= "<td><strong>VALUE</strong></td>"
        tabela &= "<td><strong>TYPE</strong></td></tr>"

        Dim titulo As String = ""
        Dim ds As DataSet = Con.ExecutarQuery("SELECT DISTINCT B.ID_ACCOUNT_INVOICE,B.NR_INVOICE,ORIGEM,DESTINO,NR_BL,GRAU,DT_EMBARQUE,DT_CHEGADA
FROM [dbo].[View_BL]  A 
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("Vencimento_Inicial") & "','" & Session("Vencimento_Final") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL " & FILTRO)

        For Each linhaTitulo As DataRow In ds.Tables(0).Rows
            Dim ID_INVOICE As String = linhaTitulo("ID_ACCOUNT_INVOICE")

            tabela &= "<tr><td>" & linhaTitulo("NR_INVOICE") & "</td>"
            tabela &= "<td>" & linhaTitulo("ORIGEM") & "</td>"
            tabela &= "<td>" & linhaTitulo("DESTINO") & "</td>"
            tabela &= "<td>" & linhaTitulo("NR_BL") & "</td>"
            tabela &= "<td>" & linhaTitulo("DT_EMBARQUE") & "</td>"
            tabela &= "<td>" & linhaTitulo("DT_CHEGADA") & "</td></tr>"




            Dim dsdados As DataSet = Con.ExecutarQuery("SELECT ID_ACCOUNT_INVOICE,NM_ITEM_DESPESA,SIGLA_MOEDA,VL_TAXA,NM_ACCOUNT_TIPO_FATURA
FROM [dbo].[View_BL]  A 
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("Vencimento_Inicial") & "','" & Session("Vencimento_Final") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL WHERE ID_ACCOUNT_INVOICE = " & ID_INVOICE)
            For Each linhadados As DataRow In dsdados.Tables(0).Rows
                tabela &= "<tr><td></td>"
                tabela &= "<td></td>"
                tabela &= "<td></td>"
                tabela &= "<td></td>"
                tabela &= "<td></td>"
                tabela &= "<td></td>"
                tabela &= "<td>" & linhadados("NM_ITEM_DESPESA") & "</td>"
                tabela &= "<td>" & linhadados("SIGLA_MOEDA") & "</td>"
                tabela &= "<td>" & linhadados("VL_TAXA") & "</td>"
                tabela &= "<td>" & linhadados("NM_ACCOUNT_TIPO_FATURA") & "</td></tr>"



            Next


        Next
        tabela &= "</table></div>"
        divConteudoDinamico.InnerHtml &= tabela

        Con.Fechar()
    End Sub

End Class