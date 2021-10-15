Public Class RotinaUpdate
    Sub UpdateInfoBasicas(ID_COTACAO As String, NR_PROCESSO As String)
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            ''INFORMACOES BASICAS
            Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_SERVICO,ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR,ID_TIPO_PAGAMENTO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,
CASE WHEN ID_PARCEIRO_IMPORTADOR IS NULL THEN ID_CLIENTE WHEN ID_PARCEIRO_IMPORTADOR = 0 THEN ID_CLIENTE ELSE ID_PARCEIRO_IMPORTADOR END ID_PARCEIRO_IMPORTADOR, 
(SELECT (ISNULL(SUM(VL_CARGA),0)) FROM TB_COTACAO_MERCADORIA B WHERE A.ID_COTACAO = B.ID_COTACAO )VL_CARGA 
 FROM TB_COTACAO A WHERE A.ID_COTACAO = " & ID_COTACAO)
            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_PARCEIRO_TRANSPORTADOR,ID_COTACAO,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,VL_FRETE,ID_MOEDA_FRETE,ID_PARCEIRO_VENDEDOR,ID_TIPO_PAGAMENTO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR,VL_CARGA FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = " & NR_PROCESSO)
            Dim ID_BL As String = dsProcesso.Tables(0).Rows(0).Item("ID_BL").ToString()

            If dsCotacao.Tables(0).Rows(0).Item("ID_SERVICO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_SERVICO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_SERVICO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_SERVICO").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_CLIENTE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_CLIENTE = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_AGENTE_INTERNACIONAL = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_INCOTERM").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_INCOTERM").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_INCOTERM = '" & dsCotacao.Tables(0).Rows(0).Item("ID_INCOTERM").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_ESTUFAGEM = '" & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PORTO_ORIGEM = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PORTO_DESTINO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_CARGA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_TRANSPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO = '" & dsCotacao.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO").ToString.Replace(",", ".") & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PROFIT_DIVISAO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_FRETE").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_FRETE = '" & dsCotacao.Tables(0).Rows(0).Item("VL_FRETE").ToString.Replace(",", ".") & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA_FRETE = '" & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_VENDEDOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_VENDEDOR = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_VENDEDOR").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("FL_FREE_HAND").ToString <> dsProcesso.Tables(0).Rows(0).Item("FL_FREE_HAND").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET FL_FREE_HAND = '" & dsCotacao.Tables(0).Rows(0).Item("FL_FREE_HAND").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_STATUS_FRETE_AGENTE = '" & dsCotacao.Tables(0).Rows(0).Item("ID_STATUS_FRETE_AGENTE").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_INDICADOR = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICADOR").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EXPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_IMPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR").ToString & "' WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_CARGA").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_CARGA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_CARGA = '" & dsCotacao.Tables(0).Rows(0).Item("VL_CARGA").ToString.Replace(",", ".") & "' WHERE ID_BL = " & ID_BL)
            End If



            ''TAXAS COMPRA
            dsProcesso = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF FROM TB_BL_TAXA A WHERE CD_ORIGEM_INF='COTA' AND CD_PR='P' AND ID_ITEM_DEPESA <> 14 AND A.ID_BL = " & ID_BL)
            If dsProcesso.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In dsProcesso.Tables(0).Rows

                    dsCotacao = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO =" & ID_COTACAO & " AND ID_ITEM_DESPESA = " & linha.Item("FL_DECLARADO"))
                    If dsCotacao.Tables(0).Rows.Count > 0 Then

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_ORIGEM_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_DESTINATARIO_COBRANCA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_BASE_CALCULO_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString <> linha.Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString <> linha.Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_CALCULADO = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_MIN = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString <> linha.Item("FL_TAXA_TRANSPORTADOR").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_TAXA_TRANSPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EMPRESA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                    End If

                Next


            End If




            ''TAXAS VENDA
            dsProcesso = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF FROM TB_BL_TAXA A WHERE CD_ORIGEM_INF='COTA' AND CD_PR='R' AND ID_ITEM_DEPESA <> 14 AND A.ID_BL = " & ID_BL)
            If dsProcesso.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In dsProcesso.Tables(0).Rows

                    dsCotacao = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA <> 0 AND ID_COTACAO =" & ID_COTACAO & " AND ID_ITEM_DESPESA = " & linha.Item("FL_DECLARADO"))
                    If dsCotacao.Tables(0).Rows.Count > 0 Then

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_ORIGEM_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_DESTINATARIO_COBRANCA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_BASE_CALCULO_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString <> linha.Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString <> linha.Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_CALCULADO = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_MIN = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString <> linha.Item("FL_TAXA_TRANSPORTADOR").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_TAXA_TRANSPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EMPRESA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                    End If

                Next


            End If

        End If
    End Sub

    Sub UpdateTaxas(ID_COTACAO_TAXA As String, NR_PROCESSO As String)
        If ID_COTACAO_TAXA = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim dsCotacao As DataSet
            Dim dsProcesso As DataSet

            ''TAXAS COMPRA
            dsProcesso = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF FROM TB_BL_TAXA A WHERE CD_ORIGEM_INF='COTA' AND CD_PR='P' AND ID_ITEM_DEPESA <> 14 AND A.ID_COTACAO_TAXA = " & ID_COTACAO_TAXA)
            If dsProcesso.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In dsProcesso.Tables(0).Rows

                    dsCotacao = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND ID_ITEM_DESPESA = " & linha.Item("ID_ITEM_DESPESA"))
                    If dsCotacao.Tables(0).Rows.Count > 0 Then

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_ORIGEM_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_DESTINATARIO_COBRANCA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_BASE_CALCULO_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString <> linha.Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString <> linha.Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_CALCULADO = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_MIN = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString <> linha.Item("FL_TAXA_TRANSPORTADOR").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_TAXA_TRANSPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EMPRESA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                    End If

                Next


            End If




            ''TAXAS VENDA
            dsProcesso = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF FROM TB_BL_TAXA A WHERE CD_ORIGEM_INF='COTA' AND CD_PR='R' AND ID_ITEM_DEPESA <> 14 AND A.ID_COTACAO_TAXA = " & ID_COTACAO_TAXA)
            If dsProcesso.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In dsProcesso.Tables(0).Rows

                    dsCotacao = Con.ExecutarQuery("SELECT ID_BL_TAXA,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS,FL_TAXA_TRANSPORTADOR,ID_FORNECEDOR FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA <> 0 AND ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND ID_ITEM_DESPESA = " & linha.Item("ID_ITEM_DESPESA"))
                    If dsCotacao.Tables(0).Rows.Count > 0 Then

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString <> linha.Item("FL_DECLARADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_ORIGEM_PAGAMENTO = '" & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_DESTINATARIO_COBRANCA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_BASE_CALCULO_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString <> linha.Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString <> linha.Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_CALCULADO = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_MIN = '" & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString <> linha.Item("FL_TAXA_TRANSPORTADOR").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_TAXA_TRANSPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EMPRESA = '" & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & "' WHERE ID_BL = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                    End If

                Next


            End If

        End If
    End Sub

    Sub UpdateCarga(ID_COTACAO_TAXA As String, NR_PROCESSO As String)
        If ID_COTACAO_TAXA = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim dsCotacao As DataSet
            Dim dsProcesso As DataSet

        End If
    End Sub

End Class
