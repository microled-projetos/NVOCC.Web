Public Class ContratosFirmados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        Else
            lblDatas.Text = "De " & Session("DataInicial") & " Até " & Session("DataFinal")
            Dim dsMoeda As DataSet = Con.ExecutarQuery("SELECT 
DISTINCT ID_MOEDA
FROM TB_ACCOUNT_FECHAMENTO 
WHERE Convert(date,DT_FECHAMENTO,103) BETWEEN Convert(date,'" & Session("DataInicial") & "',103) AND Convert(date,'" & Session("DataFinal") & "',103) 
ORDER BY ID_MOEDA")
            If dsMoeda.Tables(0).Rows.Count > 0 Then

                Dim TotalTarifaFinal As Decimal = 0
                Dim TotalIOFFinal As Decimal = 0
                Dim TotalContratoFinal As Decimal = 0
                Dim TotalContratoBRFinal As Decimal = 0

                Dim tabela As String = "<br/><table>"
                tabela &= "<tr><td><strong>Instituição Financeira</strong></td>"
                tabela &= "<td><strong>Nº Contrato</strong></td>"
                tabela &= "<td><strong>Data</strong></td>"
                tabela &= "<td><strong>Tarifa</strong></td>"
                tabela &= "<td><strong>IOF</strong></td>"
                tabela &= "<td><strong>Agente</strong></td>"
                tabela &= "<td><strong>Moeda</strong></td>"
                tabela &= "<td><strong>Taxa Câmbio</strong></td>"
                tabela &= "<td><strong>Valor</strong></td>"
                tabela &= "<td><strong>Valor R$</strong></td></tr>"

                For Each linhaMoeda As DataRow In dsMoeda.Tables(0).Rows
                    Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT 
A.ID_ACCOUNT_FECHAMENTO, 
E.NM_RAZAO AS NM_AGENTE,
E1.NM_CONTA_BANCARIA AS NM_CORRETOR,
F.SIGLA_MOEDA,
A.NR_CONTRATO,
Convert(varchar,A.DT_FECHAMENTO,103)DT_FECHAMENTO, 
Convert(varchar,A.DT_TAXA_CAMBIO,103)DT_TAXA_CAMBIO, 
A.VL_TAXA_CAMBIO,
A.VL_TARIFA_CORRETOR,
A.VL_IOF, 
A.VL_CONTRATO,
A.VL_CONTRATO_BR,
Convert(varchar,A.DT_LIQUIDACAO,103)DT_LIQUIDACAO
FROM TB_ACCOUNT_FECHAMENTO A
LEFT JOIN TB_PARCEIRO E ON A.ID_PARCEIRO_AGENTE=E.ID_PARCEIRO
LEFT JOIN TB_CONTA_BANCARIA E1 ON A.ID_PARCEIRO_CORRETOR=E1.ID_CONTA_BANCARIA 
LEFT JOIN TB_MOEDA F ON A.ID_MOEDA=F.ID_MOEDA
WHERE A.ID_MOEDA = " & linhaMoeda.Item("ID_MOEDA") & " AND Convert(date,A.DT_FECHAMENTO,103) BETWEEN Convert(date,'" & Session("DataInicial") & "',103) AND Convert(date,'" & Session("DataFinal") & "',103) ")

                    Dim TotalContrato As Decimal = 0
                    Dim TotalContratoBR As Decimal = 0

                    Dim TotalIOF As Decimal = 0
                    Dim TotalTarifa As Decimal = 0

                    If dsTaxas.Tables(0).Rows.Count > 0 Then



                        For Each linha As DataRow In dsTaxas.Tables(0).Rows
                            tabela &= "<tr><td>" & linha("NM_CORRETOR") & "</td>"
                            tabela &= "<td>" & linha("NR_CONTRATO") & "</td>"
                            tabela &= "<td>" & linha("DT_FECHAMENTO") & "</td>"
                            tabela &= "<td>" & linha("VL_TARIFA_CORRETOR") & "</td>"
                            tabela &= "<td>" & linha("VL_IOF") & "</td>"
                            tabela &= "<td>" & linha("NM_AGENTE") & "</td>"
                            tabela &= "<td>" & linha("SIGLA_MOEDA") & "</td>"
                            tabela &= "<td>" & linha("VL_TAXA_CAMBIO") & "</td>"
                            tabela &= "<td>" & linha("VL_CONTRATO") & "</td>"
                            tabela &= "<td>" & linha("VL_CONTRATO_BR") & "</td></tr>"

                            TotalTarifa = TotalTarifa + linha("VL_TARIFA_CORRETOR")
                            TotalIOF = TotalIOF + linha("VL_IOF")
                            TotalContrato = TotalContrato + linha("VL_CONTRATO")
                            TotalContratoBR = TotalContratoBR + linha("VL_CONTRATO_BR")


                        Next


                        tabela &= "<tr><td><strong>Totais  " & dsTaxas.Tables(0).Rows(0).Item("SIGLA_MOEDA") & "</strong></td><td></td><td></td></td><td><strong> " & TotalTarifa & "</strong></td><td><strong> " & TotalIOF & "</strong></td><td></td><td></td><td></td><td><strong> " & TotalContrato & "</strong></td><td><strong> " & TotalContratoBR & "</strong></td></tr><tr><td></td><td><br/></td></tr>"

                    End If

                    TotalTarifaFinal = TotalTarifaFinal + TotalTarifa
                    TotalIOFFinal = TotalIOFFinal + TotalIOF
                    TotalContratoFinal = TotalContratoFinal + TotalContrato
                    TotalContratoBRFinal = TotalContratoBRFinal + TotalContratoBR
                Next
                tabela &= "<tr><td><strong>Total Geral</strong></td><td></td><td></td></td><td><strong> " & TotalTarifaFinal & "</strong></td><td><strong> " & TotalIOFFinal & "</strong></td><td></td><td></td><td></td><td><strong> " & TotalContratoFinal & "</strong></td><td><strong> " & TotalContratoBRFinal & "</strong></td></tr><tr><td></td><td><br/></td></tr>"
                tabela &= "</table>"
                divConteudoDinamico.InnerHtml = tabela
            End If

        End If





        Con.Fechar()
    End Sub

End Class