Public Class ProcessosPeriodo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""


        If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
            FILTRO &= " AND A.ID_PARCEIRO_AGENTE_INTERNACIONAL = " & Request.QueryString("ag")
        End If

        If Request.QueryString("p") <> "" And Request.QueryString("p") <> 0 Then
            FILTRO &= " AND A.ID_BL = " & Request.QueryString("p")
        End If

        Dim tabela As String = ""


        Dim dsdados As DataSet = Con.ExecutarQuery("SELECT NR_PROCESSO,BL_MASTER,PAGAMENTO_BL_MASTER,NR_BL AS BL_HOUSE,TIPO_PAGAMENTO ,TIPO_ESTUFAGEM,
CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM) = '' THEN ORIGEM ELSE (SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_ORIGEM)
END ORIGEM,
CASE WHEN (SELECT ISNULL(CD_SIGLA,'') FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO) = '' THEN DESTINO ELSE (SELECT CD_SIGLA FROM dbo.TB_PORTO WHERE ID_PORTO = ID_PORTO_DESTINO)
END DESTINO,
(SELECT NM_RAZAO FROM dbo.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_CLIENTE)CLIENTE,
(SELECT NM_RAZAO FROM DBO.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_AGENTE_INTERNACIONAL)AGENTE_INTERNACIONAL,
(SELECT NM_RAZAO FROM DBO.TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_TRANSPORTADOR)TRANSPORTADOR,
CONVERT(VARCHAR,DT_PREVISAO_EMBARQUE_MASTER,103)DT_PREVISAO_EMBARQUE_MASTER,CONVERT(VARCHAR,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER,CONVERT(VARCHAR,DT_PREVISAO_CHEGADA_MASTER,103)DT_PREVISAO_CHEGADA_MASTER,CONVERT(VARCHAR,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER , B.VL_CAMBIO,B.DT_LIQUIDACAO
FROM [dbo].[View_House] A
LEFT JOIN [VW_PROCESSO_RECEBIDO] B ON A.ID_BL = B.ID_BL
  WHERE CONVERT(DATE,DT_EMBARQUE_MASTER,103) BETWEEN CONVERT(DATE,'" & Session("DataInicial") & "',103) AND CONVERT(DATE,'" & Session("DataFinal") & "',103)" & FILTRO & " ORDER BY NR_PROCESSO DESC ")

        If dsdados.Tables(0).Rows.Count > 0 Then
            If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
                lblAgente.Text = " - " & dsdados.Tables(0).Rows(0).Item("AGENTE_INTERNACIONAL").ToString()
            End If

            tabela &= "<table  class='tabelaDinamica' border='1'>"
            tabela &= "<tr><td><strong>PROCESSO &nbsp; &nbsp; &nbsp; &nbsp;</strong></td>"
            tabela &= "<td><strong>TX RECBTO</strong></td>"
            tabela &= "<td><strong>DT RECBTO</strong></td>"
            tabela &= "<td><strong>MASTER</strong></td>"
            tabela &= "<td><strong>TIPO FRETE MASTER</strong></td>"
            tabela &= "<td><strong>HOUSE</strong></td>"
            tabela &= "<td><strong>TIPO FRETE HOUSE</strong></td>"
            tabela &= "<td><strong>ESTUFAGEM</strong></td>"
            tabela &= "<td><strong>ORIGEM</strong></td>"
            tabela &= "<td><strong>DESTINO</strong></td>"
            tabela &= "<td><strong>CLIENTE</strong></td>"
            tabela &= "<td><strong>TRANSPORTADOR</strong></td>"
            tabela &= "<td><strong>EMBARQUE</strong></td>"
            tabela &= "<td><strong>PREV.CHEGADA</strong></td>"
            tabela &= "<td><strong>CHEGADA</strong></td></tr>"

            For Each linhadados As DataRow In dsdados.Tables(0).Rows


                tabela &= "<tr><td>" & linhadados("NR_PROCESSO") & "</td>"
                tabela &= "<td>" & linhadados("VL_CAMBIO") & "</td>"
                tabela &= "<td>" & linhadados("DT_LIQUIDACAO") & "</td>"
                tabela &= "<td>" & linhadados("BL_MASTER") & "</td>"
                tabela &= "<td>" & linhadados("PAGAMENTO_BL_MASTER") & "</td>"
                tabela &= "<td>" & linhadados("BL_HOUSE") & "</td>"
                tabela &= "<td>" & linhadados("TIPO_PAGAMENTO") & "</td>"
                tabela &= "<td>" & linhadados("TIPO_ESTUFAGEM") & "</td>"
                tabela &= "<td>" & linhadados("ORIGEM") & "</td>"
                tabela &= "<td>" & linhadados("DESTINO") & "</td>"
                tabela &= "<td>" & linhadados("CLIENTE") & "</td>"
                tabela &= "<td>" & linhadados("TRANSPORTADOR") & "</td>"
                tabela &= "<td>" & linhadados("DT_EMBARQUE_MASTER") & "</td>"
                tabela &= "<td>" & linhadados("DT_PREVISAO_CHEGADA_MASTER") & "</td>"
                tabela &= "<td>" & linhadados("DT_CHEGADA_MASTER") & "</td></tr>"


            Next
            tabela &= "</table>"

        End If


        divConteudoDinamico.InnerHtml &= tabela

        Con.Fechar()
    End Sub

End Class