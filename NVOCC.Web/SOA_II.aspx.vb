Public Class SOA_II
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""
        lblDatas.Text = "ETD: " & Session("DataInicial") & " - " & Session("DataFinal")
        Dim ds As DataSet = Con.ExecutarQuery("SELECT NOME FROM TB_USUARIO WHERE ID_USUARIO = " & Session("ID_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then
            lblUsuario.Text = "ATTN: " & ds.Tables(0).Rows(0).Item("NOME")
        End If

        If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
            FILTRO = "WHERE ID_PARCEIRO_AGENTE = " & Request.QueryString("ag")
        Else
            FILTRO = ""
        End If
        Dim tabela As String = ""



        Dim titulo As String = ""
        Dim dsAgente As DataSet = Con.ExecutarQuery("SELECT DISTINCT NM_AGENTE,ID_PARCEIRO_AGENTE,(SELECT TELEFONE FROM TB_PARCEIRO A WHERE ID_PARCEIRO_AGENTE = A.ID_PARCEIRO)TELEFONE,(SELECT EMAIL FROM TB_PARCEIRO A WHERE ID_PARCEIRO_AGENTE = A.ID_PARCEIRO)EMAIL FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "') " & FILTRO)
        For Each linhaAgente As DataRow In dsAgente.Tables(0).Rows
            tabela &= "<center><p><strong><h5>" & linhaAgente("NM_AGENTE") & "</h5>"
            If Not IsDBNull(linhaAgente("TELEFONE")) Then
                tabela &= "TELEFONE :  " & linhaAgente("TELEFONE") & "<br/>"
            End If
            If Not IsDBNull(linhaAgente("EMAIL")) Then
                tabela &= "EMAIL: " & linhaAgente("EMAIL") & "<br/>"
            End If

            tabela &= "</strong></p><br/><h5>DEBIT/CREDIT NOTE</h5></center>"

            tabela &= "<table>"
            tabela &= "<tr style='border: ridge 1px;'><td><strong>JOB NO</strong></td>"
            tabela &= "<td><strong>POL</strong></td>"
            tabela &= "<td><strong>DEST</strong></td>"
            tabela &= "<td><strong>MBL/NO</strong></td>"
            tabela &= "<td><strong>ETD</strong></td>"
            tabela &= "<td><strong>ETA</strong></td>"
            tabela &= "<td><strong>FEE ITEM</strong></td>"
            tabela &= "<td><strong>CURR</strong></td>"
            tabela &= "<td><strong>VALUE</strong></td>"
            tabela &= "<td><strong>TYPE</strong></td></tr>"

            Dim dsBL As DataSet = Con.ExecutarQuery("SELECT distinct ORIGEM,DESTINO,ID_BL,NR_BL,GRAU,DT_EMBARQUE,DT_CHEGADA,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)BL_MASTER
FROM [dbo].[View_BL]  A  
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL 
WHERE B.ID_PARCEIRO_AGENTE = " & linhaAgente("ID_PARCEIRO_AGENTE"))


            For Each linhaBL As DataRow In dsBL.Tables(0).Rows

                tabela &= "<tr><td style='border: ridge 1px;'>" & linhaBL("NR_BL") & "</td>"
                tabela &= "<td style='border: ridge 1px;'>" & linhaBL("ORIGEM") & "</td>"
                tabela &= "<td style='border: ridge 1px;'>" & linhaBL("DESTINO") & "</td>"
                tabela &= "<td style='border: ridge 1px;'>" & linhaBL("BL_MASTER") & "</td>"
                tabela &= "<td style='border: ridge 1px;'>" & linhaBL("DT_EMBARQUE") & "</td>"
                tabela &= "<td style='border: ridge 1px;'>" & linhaBL("DT_CHEGADA") & "</td>"
                tabela &= "<td></td>"
                tabela &= "<td></td>"
                tabela &= "<td></td>"
                tabela &= "<td></td></tr>"

                Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT ID_ACCOUNT_INVOICE,NM_ITEM_DESPESA,SIGLA_MOEDA,VL_TAXA,NM_ACCOUNT_TIPO_FATURA
FROM [dbo].[View_BL]  A 
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL WHERE ID_BL = " & linhaBL("ID_BL") & " AND B.ID_PARCEIRO_AGENTE = " & linhaAgente("ID_PARCEIRO_AGENTE"))

                For Each linhaTaxas As DataRow In dsTaxas.Tables(0).Rows
                    tabela &= "<tr><td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td style='border: ridge 1px;'>" & linhaTaxas("NM_ITEM_DESPESA") & "</td>"
                    tabela &= "<td style='border: ridge 1px;'>" & linhaTaxas("SIGLA_MOEDA") & "</td>"
                    tabela &= "<td style='border: ridge 1px;'>" & linhaTaxas("VL_TAXA") & "</td>"
                    tabela &= "<td style='border: ridge 1px;'>" & linhaTaxas("NM_ACCOUNT_TIPO_FATURA") & "</td></tr>"



                Next
                Dim dsTotal As DataSet = Con.ExecutarQuery("SELECT SIGLA_MOEDA, ISNULL(SUM(VL_TAXA),0)VL_TAXA FROM [dbo].[View_BL]  A 
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL 
WHERE ID_BL = " & linhaBL("ID_BL") & " AND B.ID_PARCEIRO_AGENTE = " & linhaAgente("ID_PARCEIRO_AGENTE") & " 
GROUP BY SIGLA_MOEDA")
                For Each linhaTotal As DataRow In dsTotal.Tables(0).Rows
                    tabela &= "<tr><td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"
                    tabela &= "<td style='border: ridge 1px;'><strong>BALANCE</strong></td>"
                    tabela &= "<td style='border: ridge 1px;'>" & linhaTotal("SIGLA_MOEDA") & "</td>"
                    tabela &= "<td style='border: ridge 1px;'>" & linhaTotal("VL_TAXA") & "</td>"
                    tabela &= "<td></td></tr>"
                Next

            Next
            tabela &= "</table><br/><div style='break-after:page'></div>"
        Next

        divConteudoDinamico.InnerHtml &= tabela

        Con.Fechar()
    End Sub

End Class