Public Class RotinaUpdate
    Sub UpdateInfoBasicas(ID_COTACAO As String, NR_PROCESSO As String)
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            ''INFORMACOES BASICAS
            Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_SERVICO,ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,FINAL_DESTINATION,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR,ID_TIPO_PAGAMENTO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_RODOVIARIO,
CASE WHEN ID_PARCEIRO_IMPORTADOR IS NULL THEN ID_CLIENTE WHEN ID_PARCEIRO_IMPORTADOR = 0 THEN ID_CLIENTE ELSE ID_PARCEIRO_IMPORTADOR END ID_PARCEIRO_IMPORTADOR, 
(SELECT (ISNULL(SUM(VL_CARGA),0)) FROM TB_COTACAO_MERCADORIA B WHERE A.ID_COTACAO = B.ID_COTACAO )VL_CARGA ,isnull(A.VL_PESO_TAXADO,0)VL_PESO_TAXADO 
 FROM TB_COTACAO A WHERE A.ID_COTACAO = " & ID_COTACAO)
            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_BL,A.ID_SERVICO,A.ID_PARCEIRO_CLIENTE,A.ID_PARCEIRO_AGENTE_INTERNACIONAL,A.ID_INCOTERM,A.FINAL_DESTINATION,A.ID_TIPO_ESTUFAGEM,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.ID_TIPO_CARGA,A.ID_PARCEIRO_TRANSPORTADOR,A.ID_COTACAO,A.VL_PROFIT_DIVISAO,A.ID_PROFIT_DIVISAO,A.VL_FRETE,A.ID_MOEDA_FRETE,A.ID_PARCEIRO_VENDEDOR,A.ID_TIPO_PAGAMENTO,A.FL_FREE_HAND,A.ID_STATUS_FRETE_AGENTE,A.ID_PARCEIRO_INDICADOR,A.ID_PARCEIRO_EXPORTADOR,A.ID_PARCEIRO_IMPORTADOR,A.VL_CARGA,A.ID_PARCEIRO_RODOVIARIO, isnull(A.VL_PESO_TAXADO,0)VL_PESO_TAXADO  FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsProcesso.Tables(0).Rows(0).Item("ID_BL").ToString()

            If dsCotacao.Tables(0).Rows(0).Item("ID_SERVICO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_SERVICO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_SERVICO = " & dsCotacao.Tables(0).Rows(0).Item("ID_SERVICO").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("FINAL_DESTINATION").ToString <> dsProcesso.Tables(0).Rows(0).Item("FINAL_DESTINATION").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET FINAL_DESTINATION = " & dsCotacao.Tables(0).Rows(0).Item("FINAL_DESTINATION").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_CLIENTE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_CLIENTE = " & dsCotacao.Tables(0).Rows(0).Item("ID_CLIENTE").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_AGENTE_INTERNACIONAL = " & dsCotacao.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_INCOTERM").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_INCOTERM").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_INCOTERM = " & dsCotacao.Tables(0).Rows(0).Item("ID_INCOTERM").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_ESTUFAGEM = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_VENDEDOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_VENDEDOR = " & dsCotacao.Tables(0).Rows(0).Item("ID_VENDEDOR").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("FL_FREE_HAND").ToString <> dsProcesso.Tables(0).Rows(0).Item("FL_FREE_HAND").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET FL_FREE_HAND = '" & dsCotacao.Tables(0).Rows(0).Item("FL_FREE_HAND").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_STATUS_FRETE_AGENTE = " & dsCotacao.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_INDICADOR = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EXPORTADOR = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_IMPORTADOR = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_CARGA").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_CARGA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_CARGA = " & dsCotacao.Tables(0).Rows(0).Item("VL_CARGA").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_TAXADO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_TAXADO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_PESO_TAXADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_TAXADO").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_RODOVIARIO = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO").ToString & " WHERE ID_BL = " & ID_BL)
            End If
        End If
    End Sub

    Sub UpdateFrete(ID_COTACAO As String, NR_PROCESSO As String)
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            ''INFORMACOES DE FRETE
            Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,FINAL_DESTINATION,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_MOEDA_FRETE ,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,ID_TIPO_PAGAMENTO FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)
            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_BL,A.ID_SERVICO,A.ID_PARCEIRO_CLIENTE,A.ID_PARCEIRO_AGENTE_INTERNACIONAL,A.FINAL_DESTINATION,A.ID_INCOTERM,A.ID_TIPO_ESTUFAGEM,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.ID_PORTO_1T,A.ID_PORTO_2T,A.ID_PORTO_3T,A.ID_TIPO_CARGA,A.ID_PARCEIRO_TRANSPORTADOR,A.ID_COTACAO,A.VL_PROFIT_DIVISAO,A.ID_PROFIT_DIVISAO,A.VL_FRETE,A.ID_MOEDA_FRETE,A.ID_PARCEIRO_VENDEDOR,A.ID_TIPO_PAGAMENTO,A.FL_FREE_HAND,A.ID_STATUS_FRETE_AGENTE,A.ID_PARCEIRO_INDICADOR,A.ID_PARCEIRO_EXPORTADOR,A.ID_PARCEIRO_IMPORTADOR,A.VL_CARGA FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsProcesso.Tables(0).Rows(0).Item("ID_BL").ToString()

            If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PORTO_ORIGEM = " & dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PORTO_DESTINO = " & dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ESCALA1").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PORTO_1T").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PORTO_1T = " & dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ESCALA1").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ESCALA2").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PORTO_2T").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PORTO_2T = " & dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PORTO_3T").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PORTO_3T = " & dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ESCALA3").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_CARGA = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_TRANSPORTADOR = " & dsCotacao.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO = " & dsCotacao.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PROFIT_DIVISAO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA_FRETE = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("FINAL_DESTINATION").ToString <> dsProcesso.Tables(0).Rows(0).Item("FINAL_DESTINATION").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET FINAL_DESTINATION = " & dsCotacao.Tables(0).Rows(0).Item("FINAL_DESTINATION").ToString & " WHERE ID_BL = " & ID_BL)
            End If
        End If
    End Sub

    Sub UpdateFreteTaxa(ID_COTACAO As String, NR_PROCESSO As String)
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim finaliza As New FinalizaCotacao

            ''INFORMACOES DE FRETE
            Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_SERVICO,ID_TIPO_ESTUFAGEM,ID_TRANSPORTADOR,ID_COTACAO,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN,ID_MOEDA_FRETE,ID_VENDEDOR,ID_TIPO_PAGAMENTO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,
(SELECT (ISNULL(SUM(VL_CARGA),0)) FROM TB_COTACAO_MERCADORIA B WHERE A.ID_COTACAO = B.ID_COTACAO )VL_CARGA,  

CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA, 

ISNULL((SELECT isnull(FL_PROFIT_FRETE,0) FROM [dbo].TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT = A.ID_TIPO_DIVISAO_FRETE),0)FL_PROFIT_FRETE,
CASE 
 WHEN ID_SERVICO in (1,2) and ID_TIPO_PAGAMENTO = 1
 THEN 1

WHEN ID_SERVICO  in (1,2) and ID_TIPO_PAGAMENTO = 2
THEN 2

 WHEN ID_SERVICO in (4,5) and ID_TIPO_PAGAMENTO = 1
 THEN 2

WHEN ID_SERVICO  in (4,5) and ID_TIPO_PAGAMENTO = 2
THEN 1

ELSE 0
end ID_ORIGEM_PAGAMENTO,ISNULL(FL_FRETE_DECLARADO,0)FL_FRETE_DECLARADO, ISNULL(FL_FRETE_PROFIT,0)FL_FRETE_PROFIT  
 FROM TB_COTACAO A WHERE A.ID_COTACAO = " & ID_COTACAO)
            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_BL, ID_TIPO_ESTUFAGEM FROM TB_BL A WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsProcesso.Tables(0).Rows(0).Item("ID_BL").ToString()
            Dim ID_TIPO_ESTUFAGEM As String = dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString()
            Dim ID_SERVICO As String = dsCotacao.Tables(0).Rows(0).Item("ID_SERVICO").ToString()
            Dim ID_BASE_CALCULO As String = 0
            Dim FL_PROFIT_FRETE As Integer = 0
            Dim dsProfit As DataSet = Con.ExecutarQuery("SELECT isnull(FL_PROFIT_FRETE,0)FL_PROFIT_FRETE FROM [dbo].TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT IN (SELECT ID_TIPO_DIVISAO_FRETE FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & " ) ")
            If dsProfit.Tables(0).Rows.Count > 0 Then
                FL_PROFIT_FRETE = dsProfit.Tables(0).Rows(0).Item("FL_PROFIT_FRETE")
            End If
            Dim dsFreteTaxa As DataSet


            'FRETE COMPRA MARITIMO
            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString = 1 Then

                dsFreteTaxa = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA, ID_ORIGEM_PAGAMENTO, isnull(ID_BL_MASTER,0)ID_BL_MASTER FROM TB_BL_TAXA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL =" & ID_BL & " AND ID_BL_TAXA NOT IN (SELECT ISNULL(ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS B INNER JOIN TB_CONTA_PAGAR_RECEBER A ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE A.DT_CANCELAMENTO IS NULL)")

                If dsFreteTaxa.Tables(0).Rows.Count > 0 Then

                    If finaliza.TaxaBloqueada(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString, "BL") = False Then

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & " AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & " AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_MIN").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_MIN").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If


                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & " AND ID_BL = " & ID_BL)
                        End If

                        Dim calculaBL As New CalculaBL
                        calculaBL.Calcular(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString)
                    End If

                    '               Else
                    '                   If ID_TIPO_ESTUFAGEM = 1 Then

                    '                       ID_BASE_CALCULO = 5 'VALOR FIXO

                    '                       'FRETE COMPRA
                    '                       Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,FL_TAXA_TRANSPORTADOR,CD_ORIGEM_INF)
                    'SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P', " & FL_PROFIT_FRETE & ",

                    'ID_TRANSPORTADOR AS ID_PARCEIRO_EMPRESA, 

                    'CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
                    'THEN 4
                    'ELSE 1
                    'END ID_DESTINATARIO_COBRANCA,
                    '1,'COTA'

                    'FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)


                End If


            End If

            If ID_SERVICO = 2 Or ID_SERVICO = 5 Then
                'FRETE VENDA AEREO
                dsFreteTaxa = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_ORIGEM_PAGAMENTO FROM TB_BL_TAXA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL =" & ID_BL & " AND ID_BL_TAXA NOT IN (SELECT ISNULL(ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS B INNER JOIN TB_CONTA_PAGAR_RECEBER A ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE A.DT_CANCELAMENTO IS NULL)")
                If dsFreteTaxa.Tables(0).Rows.Count > 0 Then
                    If finaliza.TaxaBloqueada(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString, "BL") = False Then

                        Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BASE_CALCULO_TAXA = 42 WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_PROFIT").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_PROFIT").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_DECLARADO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DECLARADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_DECLARADO").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & " AND ID_BL = " & ID_BL)
                        End If


                        Dim calculaBL As New CalculaBL
                        calculaBL.Calcular(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString)

                    End If

                End If


                'FRETE COMPRA AEREO
                dsFreteTaxa = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_ORIGEM_PAGAMENTO FROM TB_BL_TAXA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL =" & ID_BL & " AND ID_BL_TAXA NOT IN (SELECT ISNULL(ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS B INNER JOIN TB_CONTA_PAGAR_RECEBER A ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE A.DT_CANCELAMENTO IS NULL)")
                If dsFreteTaxa.Tables(0).Rows.Count > 0 Then
                    If finaliza.TaxaBloqueada(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString, "BL") = False Then

                        Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BASE_CALCULO_TAXA = 42 WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_MIN").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_MIN").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_PROFIT").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_PROFIT").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_DECLARADO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DECLARADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_FRETE_DECLARADO").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & " AND ID_BL = " & ID_BL)
                        End If


                        Dim calculaBL As New CalculaBL
                        calculaBL.Calcular(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString)

                    End If

                    ' Else

                    'If ID_TIPO_ESTUFAGEM = 1 Then
                    '    ID_BASE_CALCULO = 5
                    'ElseIf ID_TIPO_ESTUFAGEM = 2 Then
                    '    ID_BASE_CALCULO = 13
                    'End If



                    ''' FRETE VENDA
                    'Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_ORIGEM_PAGAMENTO)

                    ' SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & " ,

                    ' CASE WHEN (ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6) AND ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
                    ' THEN ID_PARCEIRO_IMPORTADOR
                    ' ELSE ID_CLIENTE
                    ' END ID_PARCEIRO_EMPRESA, 

                    ' CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
                    ' THEN 4
                    ' ELSE 1
                    ' END ID_DESTINATARIO_COBRANCA ,'COTA',


                    ' CASE 
                    ' WHEN ID_SERVICO in (1,2) and ID_TIPO_PAGAMENTO = 1
                    ' THEN 1

                    'WHEN ID_SERVICO  in (1,2) and ID_TIPO_PAGAMENTO = 2
                    'THEN 2

                    ' WHEN ID_SERVICO in (4,5) and ID_TIPO_PAGAMENTO = 1
                    ' THEN 2

                    'WHEN ID_SERVICO  in (4,5) and ID_TIPO_PAGAMENTO = 2
                    'THEN 1

                    'ELSE 0
                    'end ID_ORIGEM_PAGAMENTO

                    ' FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)


                End If


            Else
                'FRETE VENDA MARITIMO
                dsFreteTaxa = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,ID_ORIGEM_PAGAMENTO FROM TB_BL_TAXA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL =" & ID_BL & " AND ID_BL_TAXA NOT IN (SELECT ISNULL(ID_BL_TAXA,0) FROM TB_CONTA_PAGAR_RECEBER_ITENS B INNER JOIN TB_CONTA_PAGAR_RECEBER A ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE A.DT_CANCELAMENTO IS NULL)")
                If dsFreteTaxa.Tables(0).Rows.Count > 0 Then
                    If finaliza.TaxaBloqueada(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString, "BL") = False Then

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & "  AND ID_BL = " & ID_BL)
                        End If


                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL_TAXA = " & dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString & " AND ID_BL = " & ID_BL)
                        End If


                        Dim calculaBL As New CalculaBL
                        calculaBL.Calcular(dsFreteTaxa.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString)

                    End If

                    ' Else

                    'If ID_TIPO_ESTUFAGEM = 1 Then
                    '    ID_BASE_CALCULO = 5
                    'ElseIf ID_TIPO_ESTUFAGEM = 2 Then
                    '    ID_BASE_CALCULO = 13
                    'End If



                    ''' FRETE VENDA
                    'Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_ORIGEM_PAGAMENTO)

                    ' SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & " ,

                    ' CASE WHEN (ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6) AND ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
                    ' THEN ID_PARCEIRO_IMPORTADOR
                    ' ELSE ID_CLIENTE
                    ' END ID_PARCEIRO_EMPRESA, 

                    ' CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
                    ' THEN 4
                    ' ELSE 1
                    ' END ID_DESTINATARIO_COBRANCA ,'COTA',


                    ' CASE 
                    ' WHEN ID_SERVICO in (1,2) and ID_TIPO_PAGAMENTO = 1
                    ' THEN 1

                    'WHEN ID_SERVICO  in (1,2) and ID_TIPO_PAGAMENTO = 2
                    'THEN 2

                    ' WHEN ID_SERVICO in (4,5) and ID_TIPO_PAGAMENTO = 1
                    ' THEN 2

                    'WHEN ID_SERVICO  in (4,5) and ID_TIPO_PAGAMENTO = 2
                    'THEN 1

                    'ELSE 0
                    'end ID_ORIGEM_PAGAMENTO

                    ' FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)


                End If
            End If
        End If
    End Sub

    Sub UpdateTaxas(ID_COTACAO As String, ID_COTACAO_TAXA As String, NR_PROCESSO As String)
        If ID_COTACAO_TAXA = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim finaliza As New FinalizaCotacao

            Dim dsCotacao As DataSet
            Dim dsProcesso As DataSet
            Dim dsInfo As DataSet = Con.ExecutarQuery("SELECT C.ID_TIPO_ESTUFAGEM,A.ID_BL FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsInfo.Tables(0).Rows(0).Item("ID_BL").ToString

            ''TAXAS COMPRA COM VALOR ZERADO
            dsCotacao = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,isnull(VL_TAXA_COMPRA,0)VL_TAXA_COMPRA,isnull(VL_TAXA_COMPRA_CALCULADO,0)VL_TAXA_COMPRA_CALCULADO,isnull(VL_TAXA_COMPRA_MIN,0)VL_TAXA_COMPRA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA = 0 AND ID_COTACAO_TAXA =" & ID_COTACAO_TAXA)

            If dsCotacao.Tables(0).Rows.Count > 0 Then

                dsProcesso = Con.ExecutarQuery("SELECT isnull(ID_BL_MASTER,0)ID_BL_MASTER,A.ID_BL_TAXA,A.ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,A.ID_MOEDA,isnull(VL_TAXA,0)VL_TAXA,isnull(A.VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO,isnull(VL_TAXA_MIN,0)VL_TAXA_MIN,OB_TAXAS,A.ID_BL,FL_TAXA_TRANSPORTADOR,A.CD_PR,A.ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,
isnull(B.ID_CONTA_PAGAR_RECEBER,0)ID_CONTA_PAGAR_RECEBER,C.DT_CANCELAMENTO
FROM TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL_TAXA= B.ID_BL_TAXA
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER  WHERE A.CD_ORIGEM_INF='COTA' AND A.CD_PR='P' AND A.ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & " AND A.ID_BL = " & ID_BL)

                If dsProcesso.Tables(0).Rows.Count > 0 Then

                    For Each linha As DataRow In dsProcesso.Tables(0).Rows

                        If linha.Item("ID_BL_MASTER").ToString = 0 Then

                            If linha.Item("ID_CONTA_PAGAR_RECEBER").ToString = 0 Or Not IsDBNull(linha.Item("DT_CANCELAMENTO")) Then
                                If finaliza.TaxaBloqueada(linha.Item("ID_BL_TAXA").ToString, "BL") = False Then

                                    ' DELETE
                                    Con.ExecutarQuery("DELETE FROM TB_BL_TAXA WHERE ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & " AND CD_PR='P' AND ID_BL =" & ID_BL & " AND  CD_ORIGEM_INF='COTA' AND ID_BL_TAXA NOT IN (SELECT  isnull(ID_BL_TAXA,0)ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER =  B.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL) AND ID_BL_MASTER IS NULL AND ID_BL_TAXA_MASTER IS NULL  AND ID_BL_TAXA NOT IN (SELECT I.ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS I WHERE I.ID_BL_TAXA IS NOT NULL)")

                                End If
                            End If
                        End If

                    Next
                End If

            Else

                ''TAXAS COMPRA COM VALOR PREENCHIDO

                dsCotacao = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,isnull(VL_TAXA_COMPRA,0)VL_TAXA_COMPRA,isnull(VL_TAXA_COMPRA_CALCULADO,0)VL_TAXA_COMPRA_CALCULADO,isnull(VL_TAXA_COMPRA_MIN,0)VL_TAXA_COMPRA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR,QTD_BASE_CALCULO FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO_TAXA =" & ID_COTACAO_TAXA)

                If dsCotacao.Tables(0).Rows.Count > 0 Then

                    dsProcesso = Con.ExecutarQuery("SELECT isnull(ID_BL_MASTER,0)ID_BL_MASTER,A.ID_BL_TAXA,A.ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,A.ID_MOEDA,isnull(VL_TAXA,0)VL_TAXA,isnull(A.VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO,isnull(VL_TAXA_MIN,0)VL_TAXA_MIN,OB_TAXAS,A.ID_BL,FL_TAXA_TRANSPORTADOR,A.CD_PR,A.ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,
isnull(B.ID_CONTA_PAGAR_RECEBER,0)ID_CONTA_PAGAR_RECEBER,C.DT_CANCELAMENTO,QTD_BASE_CALCULO 
FROM TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL_TAXA= B.ID_BL_TAXA
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER  WHERE A.CD_ORIGEM_INF='COTA' AND A.CD_PR='P' AND A.ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & " AND A.ID_BL = " & ID_BL)

                    If dsProcesso.Tables(0).Rows.Count > 0 Then

                        For Each linha As DataRow In dsProcesso.Tables(0).Rows

                            If linha.Item("ID_BL_MASTER").ToString = 0 Then

                                If linha.Item("ID_CONTA_PAGAR_RECEBER").ToString = 0 Or Not IsDBNull(linha.Item("DT_CANCELAMENTO")) Then


                                    If finaliza.TaxaBloqueada(linha.Item("ID_BL_TAXA").ToString, "BL") = False Then

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_ITEM_DESPESA").ToString <> linha.Item("ID_ITEM_DESPESA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ITEM_DESPESA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_ITEM_DESPESA").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString <> linha.Item("QTD_BASE_CALCULO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET QTD_BASE_CALCULO = " & dsCotacao.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString <> linha.Item("ID_MOEDA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA").ToString <> linha.Item("VL_TAXA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        Dim calculaBL As New CalculaBL
                                        calculaBL.Calcular(linha.Item("ID_BL_TAXA").ToString)

                                    End If
                                End If
                            End If

                        Next

                        '                    Else


                        '                        dsProcesso = Con.ExecutarQuery("SELECT isnull(ID_BL_MASTER,0)ID_BL_MASTER,A.ID_BL_TAXA,A.ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,A.ID_MOEDA,isnull(VL_TAXA,0)VL_TAXA,isnull(A.VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO,isnull(VL_TAXA_MIN,0)VL_TAXA_MIN,OB_TAXAS,A.ID_BL,FL_TAXA_TRANSPORTADOR,A.CD_PR,A.ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,
                        'isnull(B.ID_CONTA_PAGAR_RECEBER,0)ID_CONTA_PAGAR_RECEBER,C.DT_CANCELAMENTO
                        'FROM TB_BL_TAXA A
                        'LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL_TAXA= B.ID_BL_TAXA
                        'LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER  WHERE ISNULL(CD_ORIGEM_INF,'COTA') = 'COTA' AND A.CD_PR='P' AND A.ID_ITEM_DESPESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_ITEM_DESPESA") & " AND isnull(A.ID_COTACAO_TAXA,0) = 0 and A.ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") & " AND A.ID_BL = " & ID_BL)

                        '                        If dsProcesso.Tables(0).Rows.Count > 0 Then

                        '                            For Each linha As DataRow In dsProcesso.Tables(0).Rows

                        '                                If linha.Item("ID_BL_MASTER").ToString = 0 Then

                        '                                    If linha.Item("ID_CONTA_PAGAR_RECEBER").ToString = 0 Or Not IsDBNull(linha.Item("DT_CANCELAMENTO")) Then

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString <> linha.Item("ID_MOEDA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA").ToString <> linha.Item("VL_TAXA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA").ToString.Replace(",", ".") & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString.Replace(",", ".") & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString.Replace(",", ".") & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & " WHERE  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        Dim calculaBL As New CalculaBL
                        '                                        calculaBL.Calcular(linha.Item("ID_BL_TAXA").ToString)
                        '                                    End If
                        '                                End If

                        '                            Next


                    Else

                        Dim dsInsert As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,ID_COTACAO_TAXA,QTD_BASE_CALCULO ) SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'P',ID_FORNECEDOR,'COTA',ID_COTACAO_TAXA,QTD_BASE_CALCULO FROM TB_COTACAO_TAXA  WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO = " & ID_COTACAO & " AND ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & "  Select SCOPE_IDENTITY() as ID_BL_TAXA ")


                        Dim calculaBL As New CalculaBL
                        calculaBL.Calcular(dsInsert.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString)

                    End If

                End If

            End If

            ''----------------------------------------------------------------------------------------------------------------------------------------''


            ''TAXAS VENDA COM VALOR ZERADO
            dsCotacao = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,isnull(VL_TAXA_VENDA,0)VL_TAXA_VENDA,isnull(VL_TAXA_VENDA_CALCULADO,0)VL_TAXA_VENDA_CALCULADO,isnull(VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR, CASE 
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 
 
 WHEN ID_DESTINATARIO_COBRANCA = 2
 THEN (SELECT ID_AGENTE_INTERNACIONAL FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")

 WHEN ID_DESTINATARIO_COBRANCA = 7
 THEN (SELECT ID_PARCEIRO_RODOVIARIO FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 

 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") = 0
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")
 
 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") <> 0 then
 (SELECT ID_PARCEIRO_IMPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 

 ELSE NULL
 END AS ID_PARCEIRO_EMPRESA FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA = 0 AND ID_COTACAO_TAXA =" & ID_COTACAO_TAXA)

            If dsCotacao.Tables(0).Rows.Count > 0 Then

                dsProcesso = Con.ExecutarQuery("SELECT isnull(ID_BL_MASTER,0)ID_BL_MASTER,A.ID_BL_TAXA,A.ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,A.ID_MOEDA,isnull(VL_TAXA,0)VL_TAXA,isnull(A.VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO,isnull(VL_TAXA_MIN,0)VL_TAXA_MIN,OB_TAXAS,A.ID_BL,FL_TAXA_TRANSPORTADOR,A.CD_PR,A.ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,
isnull(B.ID_CONTA_PAGAR_RECEBER,0)ID_CONTA_PAGAR_RECEBER,C.DT_CANCELAMENTO
FROM TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL_TAXA= B.ID_BL_TAXA
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE A.CD_ORIGEM_INF='COTA' AND A.CD_PR='R' AND A.ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & " AND A.ID_BL = " & ID_BL)

                If dsProcesso.Tables(0).Rows.Count > 0 Then

                    For Each linha As DataRow In dsProcesso.Tables(0).Rows

                        If linha.Item("ID_BL_MASTER").ToString = 0 Then

                            If linha.Item("ID_CONTA_PAGAR_RECEBER").ToString = 0 Or Not IsDBNull(linha.Item("DT_CANCELAMENTO")) Then
                                If finaliza.TaxaBloqueada(linha.Item("ID_BL_TAXA").ToString, "BL") = False Then
                                    'DELETE
                                    Con.ExecutarQuery("DELETE FROM TB_BL_TAXA WHERE ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & " AND CD_PR='R' AND ID_BL =" & ID_BL & " AND  CD_ORIGEM_INF='COTA' AND ID_BL_TAXA NOT IN (SELECT  isnull(ID_BL_TAXA,0)ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER =  B.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL) AND ID_BL_MASTER IS NULL AND ID_BL_TAXA_MASTER IS NULL AND ID_BL_TAXA NOT IN (SELECT I.ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS I WHERE I.ID_BL_TAXA IS NOT NULL) ")
                                End If

                            End If
                        End If
                    Next
                End If

            Else


                ''TAXAS VENDA COM VALOR PREENCHIDO
                dsCotacao = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,isnull(VL_TAXA_VENDA,0)VL_TAXA_VENDA,isnull(VL_TAXA_VENDA_CALCULADO,0)VL_TAXA_VENDA_CALCULADO,isnull(VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR, CASE 
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 
 
 WHEN ID_DESTINATARIO_COBRANCA = 2
 THEN (SELECT ID_AGENTE_INTERNACIONAL FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")
 
 WHEN ID_DESTINATARIO_COBRANCA = 7
 THEN (SELECT ID_PARCEIRO_RODOVIARIO FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 

 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") = 0
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")
 
 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") <> 0 then
 (SELECT ID_PARCEIRO_IMPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 

 ELSE NULL
 END AS ID_PARCEIRO_EMPRESA,
 QTD_BASE_CALCULO 
 FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA <> 0 AND ID_COTACAO_TAXA =" & ID_COTACAO_TAXA)

                If dsCotacao.Tables(0).Rows.Count > 0 Then

                    dsProcesso = Con.ExecutarQuery("SELECT isnull(ID_BL_MASTER,0)ID_BL_MASTER,A.ID_BL_TAXA,A.ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,A.ID_MOEDA,isnull(VL_TAXA,0)VL_TAXA,isnull(A.VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO,isnull(VL_TAXA_MIN,0)VL_TAXA_MIN,OB_TAXAS,A.ID_BL,FL_TAXA_TRANSPORTADOR,A.CD_PR,A.ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,
isnull(B.ID_CONTA_PAGAR_RECEBER,0)ID_CONTA_PAGAR_RECEBER,C.DT_CANCELAMENTO,QTD_BASE_CALCULO 
FROM TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL_TAXA= B.ID_BL_TAXA
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE A.CD_ORIGEM_INF='COTA' AND A.CD_PR='R' AND A.ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & " AND A.ID_BL = " & ID_BL)

                    If dsProcesso.Tables(0).Rows.Count > 0 Then

                        For Each linha As DataRow In dsProcesso.Tables(0).Rows

                            If linha.Item("ID_BL_MASTER").ToString = 0 Then

                                If linha.Item("ID_CONTA_PAGAR_RECEBER").ToString = 0 Or Not IsDBNull(linha.Item("DT_CANCELAMENTO")) Then

                                    If finaliza.TaxaBloqueada(linha.Item("ID_BL_TAXA").ToString, "BL") = False Then

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_ITEM_DESPESA").ToString <> linha.Item("ID_ITEM_DESPESA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ITEM_DESPESA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_ITEM_DESPESA").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString <> linha.Item("QTD_BASE_CALCULO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET QTD_BASE_CALCULO = " & dsCotacao.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString <> linha.Item("ID_MOEDA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA").ToString <> linha.Item("VL_TAXA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                                        End If

                                        Dim calculaBL As New CalculaBL
                                        calculaBL.Calcular(linha.Item("ID_BL_TAXA").ToString)

                                    End If
                                End If
                            End If
                        Next

                        'Else

                        '                        dsProcesso = Con.ExecutarQuery("SELECT isnull(ID_BL_MASTER,0)ID_BL_MASTER,A.ID_BL_TAXA,A.ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,A.ID_MOEDA,isnull(VL_TAXA,0)VL_TAXA,isnull(A.VL_TAXA_CALCULADO,0)VL_TAXA_CALCULADO,isnull(VL_TAXA_MIN,0)VL_TAXA_MIN,OB_TAXAS,A.ID_BL,FL_TAXA_TRANSPORTADOR,A.CD_PR,A.ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,
                        'isnull(B.ID_CONTA_PAGAR_RECEBER,0)ID_CONTA_PAGAR_RECEBER,C.DT_CANCELAMENTO
                        'FROM TB_BL_TAXA A
                        'LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL_TAXA= B.ID_BL_TAXA
                        'LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER WHERE A.CD_PR='R' AND ISNULL(CD_ORIGEM_INF,'COTA') = 'COTA' AND A.ID_ITEM_DESPESA= " & dsCotacao.Tables(0).Rows(0).Item("ID_ITEM_DESPESA") & " AND isnull(A.ID_COTACAO_TAXA,0) = 0 and A.ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") & " AND A.ID_BL = " & ID_BL)

                        '                        If dsProcesso.Tables(0).Rows.Count > 0 Then

                        '                            For Each linha As DataRow In dsProcesso.Tables(0).Rows

                        '                                If linha.Item("ID_BL_MASTER").ToString = 0 Then


                        '                                    If linha.Item("ID_CONTA_PAGAR_RECEBER").ToString = 0 Or Not IsDBNull(linha.Item("DT_CANCELAMENTO")) Then

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString <> linha.Item("ID_MOEDA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA").ToString <> linha.Item("VL_TAXA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA").ToString.Replace(",", ".") & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString.Replace(",", ".") & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString.Replace(",", ".") & " WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE   ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If

                        '                                        If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                        '                                            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString & " WHERE  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        '                                        End If


                        '                                        Dim calculaBL As New CalculaBL
                        '                                        calculaBL.Calcular(linha.Item("ID_BL_TAXA").ToString)
                        '                                    End If
                        '                                End If

                        '                            Next

                    Else

                        'TAXAS VENDA
                        Dim dsInsert As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_COTACAO_TAXA,QTD_BASE_CALCULO) 
                         SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'R',

                         CASE 
                         WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
                         THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 

                         WHEN ID_DESTINATARIO_COBRANCA = 2
                         THEN (SELECT ID_AGENTE_INTERNACIONAL FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")

                         WHEN ID_DESTINATARIO_COBRANCA = 7
                         THEN (SELECT ID_PARCEIRO_RODOVIARIO FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 

                         WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") = 0
                         THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")

                         WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") <> 0 then
                         (SELECT ID_PARCEIRO_IMPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 

                         ELSE NULL
                         END ID_PARCEIRO_EMPRESA,


                        CASE
                         WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
                         THEN 1
                         ELSE ID_DESTINATARIO_COBRANCA
                         END ID_DESTINATARIO_COBRANCA,'COTA',ID_COTACAO_TAXA,QTD_BASE_CALCULO

                        FROM TB_COTACAO_TAXA WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA <> 0 AND ID_COTACAO = " & ID_COTACAO & " AND ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & "  Select SCOPE_IDENTITY() as ID_BL_TAXA ")


                        Dim calculaBL As New CalculaBL
                        calculaBL.Calcular(dsInsert.Tables(0).Rows(0).Item("ID_BL_TAXA").ToString)
                    End If

                End If

            End If

        End If


    End Sub


    Sub UpdateCarga(ID_COTACAO As String, ID_COTACAO_MERCADORIA As String, NR_PROCESSO As String, Optional RefPeso As String = "", Optional RefVolume As String = "", Optional RefPesoSum As String = "", Optional RefVolumeSum As String = "")
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim dsCotacao As DataSet
            Dim dsProcesso As DataSet
            Dim dsInfo As DataSet = Con.ExecutarQuery("SELECT C.ID_SERVICO,C.ID_TIPO_ESTUFAGEM,A.ID_BL,ISNULL(A.VL_CARGA,0)VL_CARGA FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_TIPO_ESTUFAGEM As String = dsInfo.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString
            Dim ID_SERVICO As String = dsInfo.Tables(0).Rows(0).Item("ID_SERVICO").ToString
            Dim ID_BL As String = dsInfo.Tables(0).Rows(0).Item("ID_BL").ToString
            dsCotacao = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_CARGA),0)VL_CARGA FROM TB_COTACAO_MERCADORIA B 
INNER JOIN TB_COTACAO A ON A.ID_COTACAO = B.ID_COTACAO
WHERE A.ID_COTACAO = " & ID_COTACAO)

            If dsCotacao.Tables(0).Rows(0).Item("VL_CARGA").ToString <> dsInfo.Tables(0).Rows(0).Item("VL_CARGA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_CARGA = " & dsCotacao.Tables(0).Rows(0).Item("VL_CARGA").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL)
            End If


            If ID_SERVICO = 2 Or ID_SERVICO = 5 Then

                'CARGA AEREO 

                dsProcesso = Con.ExecutarQuery("SELECT ID_CARGA_BL,ID_MERCADORIA, ID_EMBALAGEM, VL_PESO_BRUTO, VL_M3 FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)

                If dsProcesso.Tables(0).Rows.Count > 0 Then
                    Dim dsCotacaoMercadoria As DataSet = Con.ExecutarQuery("SELECT ID_MERCADORIA, VL_PESO_BRUTO, VL_M3 FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO_MERCADORIA =" & ID_COTACAO_MERCADORIA)


                    If dsCotacao.Tables(0).Rows.Count > 0 Then

                        If dsCotacaoMercadoria.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                            If RefPesoSum <> "" Then
                                If RefPesoSum <> dsCotacaoMercadoria.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacaoMercadoria.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_CARGA_BL = " & dsProcesso.Tables(0).Rows(0).Item("ID_CARGA_BL"))
                                End If
                            End If
                        End If

                        If dsCotacaoMercadoria.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_MERCADORIA = " & dsCotacaoMercadoria.Tables(0).Rows(0).Item("ID_MERCADORIA") & " WHERE ID_BL = " & ID_BL & " AND ID_CARGA_BL = " & dsProcesso.Tables(0).Rows(0).Item("ID_CARGA_BL"))
                        End If

                        If dsCotacaoMercadoria.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString Then
                            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_EMBALAGEM = " & dsCotacaoMercadoria.Tables(0).Rows(0).Item("ID_MERCADORIA") & " WHERE ID_BL = " & ID_BL & " AND ID_CARGA_BL = " & dsProcesso.Tables(0).Rows(0).Item("ID_CARGA_BL"))
                        End If

                        If dsCotacaoMercadoria.Tables(0).Rows(0).Item("VL_M3").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_M3").ToString Then
                            If RefVolumeSum <> "" Then
                                If RefVolumeSum <> dsCotacaoMercadoria.Tables(0).Rows(0).Item("VL_M3").ToString Then
                                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & dsCotacaoMercadoria.Tables(0).Rows(0).Item("VL_M3").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_CARGA_BL = " & dsProcesso.Tables(0).Rows(0).Item("ID_CARGA_BL"))
                                End If
                            End If
                        End If


                    End If

                End If

                Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 = (SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; 
UPDATE TB_BL SET VL_PESO_BRUTO = (SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; 
UPDATE TB_BL SET QT_MERCADORIA = (SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL)

            Else
                If ID_TIPO_ESTUFAGEM = 1 Then

                    If ID_COTACAO_MERCADORIA = "" Then
                        Exit Sub
                    End If

                    If ID_COTACAO_MERCADORIA = 0 Then
                        Exit Sub
                    End If

                    Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA,ISNULL(QT_CONTAINER,0)QT_CONTAINER FROM TB_COTACAO_MERCADORIA
                WHERE  ID_COTACAO = " & ID_COTACAO & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)


                    If ds.Tables(0).Rows(0).Item("QT_CONTAINER") > 1 Then

                        dsProcesso = Con.ExecutarQuery("SELECT ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_TIPO_CNTR FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                        dsCotacao = Con.ExecutarQuery("SELECT ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3,ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & ID_COTACAO & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)

                        If dsProcesso.Tables(0).Rows.Count > 0 Then


                            If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_EMBALAGEM = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                                If RefPeso <> "" Then
                                    If RefPeso <> dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                                    End If
                                End If
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET QT_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_M3").ToString Then
                                If RefVolume <> "" Then
                                    If RefVolume <> dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                                    End If
                                End If
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_CNTR").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_TIPO_CNTR = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL)

                        End If

                    Else

                        dsProcesso = Con.ExecutarQuery("SELECT ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_TIPO_CNTR,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                        dsCotacao = Con.ExecutarQuery("SELECT ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_TIPO_CONTAINER,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & ID_COTACAO & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                        If dsProcesso.Tables(0).Rows.Count > 0 Then
                            If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_EMBALAGEM = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                                If RefPeso <> "" Then
                                    If RefPeso <> dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                                    End If
                                End If
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET QT_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_M3").ToString Then
                                If RefVolume <> "" Then
                                    If RefVolume <> dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString Then
                                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                                    End If
                                End If
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("VL_ALTURA").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_ALTURA").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_ALTURA = " & dsCotacao.Tables(0).Rows(0).Item("VL_ALTURA").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("VL_LARGURA").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_LARGURA").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_LARGURA = " & dsCotacao.Tables(0).Rows(0).Item("VL_LARGURA").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("VL_COMPRIMENTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_COMPRIMENTO").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_COMPRIMENTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_COMPRIMENTO").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If

                            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_CNTR").ToString Then
                                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_TIPO_CNTR = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                            End If


                            Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL)


                        End If

                    End If

                ElseIf ID_TIPO_ESTUFAGEM = 2 Then

                    Dim dsCarga As DataSet
                    Dim ID_MERCADORIA As Integer = 11
                    dsCarga = Con.ExecutarQuery("SELECT DISTINCT ID_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & ID_COTACAO)
                    If dsCarga.Tables(0).Rows.Count = 1 Then
                        ID_MERCADORIA = dsCarga.Tables(0).Rows(0).Item("ID_MERCADORIA")
                    End If

                    dsProcesso = Con.ExecutarQuery("SELECT ISNULL(ID_MERCADORIA,0)ID_MERCADORIA, ISNULL(ID_EMBALAGEM,0)ID_EMBALAGEM,SUM(QT_MERCADORIA)QT_MERCADORIA,SUM(VL_PESO_BRUTO)VL_PESO_BRUTO,SUM(VL_M3)VL_M3 FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & " group by ID_EMBALAGEM,ID_MERCADORIA")
                    dsCotacao = Con.ExecutarQuery("SELECT SUM(QT_MERCADORIA)QT_MERCADORIA,SUM(VL_PESO_BRUTO)VL_PESO_BRUTO,SUM(VL_M3)VL_M3 FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & ID_COTACAO)

                    If dsProcesso.Tables(0).Rows.Count > 0 Then

                        If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                            If RefPesoSum <> "" Then
                                If RefPesoSum <> dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL)
                                End If
                            End If
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET QT_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL)
                        End If

                        If ID_MERCADORIA <> dsProcesso.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_MERCADORIA = " & ID_MERCADORIA & " WHERE ID_BL = " & ID_BL)
                        End If

                        If ID_MERCADORIA <> dsProcesso.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString Then
                            Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_EMBALAGEM = " & ID_MERCADORIA & " WHERE ID_BL = " & ID_BL)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_M3").ToString Then
                            If RefVolumeSum <> "" Then
                                If RefVolumeSum <> dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString Then
                                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL)
                                End If
                            End If
                        End If

                        Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & ID_BL & ") WHERE ID_BL =  " & ID_BL)
                    End If


                End If
            End If

        End If
    End Sub

    Sub DeletaTaxas(ID_COTACAO As String, ID_COTACAO_TAXA As String, NR_PROCESSO As String, Optional ID_BASE_CALCULO_TAXA As String = "", Optional ID_ITEM_DESPESA As String = "")
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim dsInfo As DataSet = Con.ExecutarQuery("SELECT C.ID_TIPO_ESTUFAGEM,A.ID_BL FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsInfo.Tables(0).Rows(0).Item("ID_BL").ToString

            Con.ExecutarQuery("DELETE FROM TB_BL_TAXA WHERE ID_COTACAO_TAXA = " & ID_COTACAO_TAXA & " AND ID_BL =" & ID_BL & " AND  CD_ORIGEM_INF='COTA' AND ID_BL_TAXA NOT IN (SELECT  isnull(ID_BL_TAXA,0)ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER =  B.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL) AND ID_BL_MASTER IS NULL AND ID_BL_TAXA_MASTER IS NULL AND ID_BL_TAXA NOT IN (SELECT I.ID_BL_TAXA FROM TB_ACCOUNT_INVOICE_ITENS I WHERE I.ID_BL_TAXA IS NOT NULL)")


        End If
    End Sub


    Sub DeletaDimensaoCarga(ID_COTACAO As String, ID_COTACAO_MERCADORIA As String, NR_PROCESSO As String, Optional ID_COTACAO_MERCADORIA_DIMENSAO As String = "")
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim dsInfo As DataSet = Con.ExecutarQuery("SELECT C.ID_TIPO_ESTUFAGEM,A.ID_BL FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsInfo.Tables(0).Rows(0).Item("ID_BL").ToString

            Con.ExecutarQuery("DELETE FROM TB_CARGA_BL_DIMENSAO WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA & " AND ID_COTACAO_MERCADORIA_DIMENSAO =" & ID_COTACAO_MERCADORIA_DIMENSAO)

        End If

    End Sub

    Sub InsereDimensaoCarga(ID_COTACAO As String, ID_COTACAO_MERCADORIA As String, NR_PROCESSO As String, Optional ID_COTACAO_MERCADORIA_DIMENSAO As String = "")
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ID_CARGA_BL As String
            Dim dsInfo As DataSet = Con.ExecutarQuery("SELECT C.ID_TIPO_ESTUFAGEM,A.ID_BL FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsInfo.Tables(0).Rows(0).Item("ID_BL").ToString

            dsInfo = Con.ExecutarQuery("SELECT ID_CARGA_BL FROM TB_CARGA_BL WHERE ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA & " AND ID_BL = " & ID_BL & "")

            If dsInfo.Tables(0).Rows.Count > 0 Then
                ID_CARGA_BL = dsInfo.Tables(0).Rows(0).Item("ID_CARGA_BL").ToString

            Else
                Dim dsInsertCarga As DataSet = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,VL_PESO_BRUTO,VL_M3,ID_BL,ID_COTACAO_MERCADORIA) 
SELECT ID_MERCADORIA, ID_MERCADORIA, VL_PESO_BRUTO, VL_M3, " & ID_BL & " , ID_COTACAO_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO_MERCADORIA =" & ID_COTACAO_MERCADORIA & " Select SCOPE_IDENTITY() as ID_CARGA_BL")

                ID_CARGA_BL = dsInsertCarga.Tables(0).Rows(0).Item("ID_CARGA_BL")
            End If


            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL_DIMENSAO (ID_BL, ID_CARGA_BL, ID_COTACAO_MERCADORIA,ID_COTACAO_MERCADORIA_DIMENSAO, QTD_CAIXA,VL_LARGURA,VL_ALTURA,VL_COMPRIMENTO) SELECT " & ID_BL & "," & ID_CARGA_BL & "," & ID_COTACAO_MERCADORIA & "," & ID_COTACAO_MERCADORIA_DIMENSAO & ", QTD_CAIXA,VL_LARGURA,VL_ALTURA,VL_COMPRIMENTO FROM TB_COTACAO_MERCADORIA_DIMENSAO WHERE ID = " & ID_COTACAO_MERCADORIA_DIMENSAO & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)

        End If

    End Sub
End Class