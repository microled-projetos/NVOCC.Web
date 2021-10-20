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
            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_BL,A.ID_SERVICO,A.ID_PARCEIRO_CLIENTE,A.ID_PARCEIRO_AGENTE_INTERNACIONAL,A.ID_INCOTERM,A.ID_TIPO_ESTUFAGEM,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.ID_TIPO_CARGA,A.ID_PARCEIRO_TRANSPORTADOR,A.ID_COTACAO,A.VL_PROFIT_DIVISAO,A.ID_PROFIT_DIVISAO,A.VL_FRETE,A.ID_MOEDA_FRETE,A.ID_PARCEIRO_VENDEDOR,A.ID_TIPO_PAGAMENTO,A.FL_FREE_HAND,A.ID_STATUS_FRETE_AGENTE,A.ID_PARCEIRO_INDICADOR,A.ID_PARCEIRO_EXPORTADOR,A.ID_PARCEIRO_IMPORTADOR,A.VL_CARGA FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsProcesso.Tables(0).Rows(0).Item("ID_BL").ToString()

            If dsCotacao.Tables(0).Rows(0).Item("ID_SERVICO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_SERVICO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_SERVICO = " & dsCotacao.Tables(0).Rows(0).Item("ID_SERVICO").ToString & " WHERE ID_BL = " & ID_BL)
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


        End If
    End Sub

    Sub UpdateFrete(ID_COTACAO As String, NR_PROCESSO As String)
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            ''INFORMACOES DE FRETE
            Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PORTO_ESCALA1 ,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3 ,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,ID_TIPO_PAGAMENTO FROM TB_COTACAO A WHERE A.ID_COTACAO = " & ID_COTACAO)
            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_BL,A.ID_SERVICO,A.ID_PARCEIRO_CLIENTE,A.ID_PARCEIRO_AGENTE_INTERNACIONAL,A.ID_INCOTERM,A.ID_TIPO_ESTUFAGEM,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.ID_PORTO_1T,A.ID_PORTO_2T,A.ID_PORTO_3T,A.ID_TIPO_CARGA,A.ID_PARCEIRO_TRANSPORTADOR,A.ID_COTACAO,A.VL_PROFIT_DIVISAO,A.ID_PROFIT_DIVISAO,A.VL_FRETE,A.ID_MOEDA_FRETE,A.ID_PARCEIRO_VENDEDOR,A.ID_TIPO_PAGAMENTO,A.FL_FREE_HAND,A.ID_STATUS_FRETE_AGENTE,A.ID_PARCEIRO_INDICADOR,A.ID_PARCEIRO_EXPORTADOR,A.ID_PARCEIRO_IMPORTADOR,A.VL_CARGA FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
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
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_TRANSPORTADOR = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_DIVISAO_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET VL_PROFIT_DIVISAO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PROFIT_DIVISAO").ToString.Replace(",", ".") & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_PROFIT_DIVISAO = " & dsCotacao.Tables(0).Rows(0).Item("ID_PROFIT_DIVISAO").ToString & " WHERE ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_BL = " & ID_BL)
            End If


        End If
    End Sub

    Sub UpdateFreteTaxa(ID_COTACAO As String, NR_PROCESSO As String)
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            ''INFORMACOES DE FRETE
            Dim dsCotacao As DataSet = Con.ExecutarQuery("SELECT ID_TIPO_ESTUFAGEM,ID_TRANSPORTADOR,ID_COTACAO,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN,VL_TOTAL_FRETE_COMPRA_CALCULADO,ID_MOEDA_FRETE,ID_VENDEDOR,ID_TIPO_PAGAMENTO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,
(SELECT (ISNULL(SUM(VL_CARGA),0)) FROM TB_COTACAO_MERCADORIA B WHERE A.ID_COTACAO = B.ID_COTACAO )VL_CARGA,  CASE WHEN (ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6) AND ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA, ISNULL((SELECT isnull(FL_PROFIT_FRETE,0) FROM [dbo].TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT = A.ID_TIPO_DIVISAO_FRETE),0)FL_PROFIT_FRETE
 FROM TB_COTACAO A WHERE A.ID_COTACAO = " & ID_COTACAO)
            Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT ID_BL WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_BL As String = dsProcesso.Tables(0).Rows(0).Item("ID_BL").ToString()
            Dim dsFreteTaxa As DataSet

            'FRETE COMPRA
            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM").ToString = 1 Then

                dsFreteTaxa = Con.ExecutarQuery("SELECT ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA FROM TB_BL_TAXA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL =" & ID_BL)

                If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_MOEDA").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_MIN").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_MIN").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_MIN").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_CALCULADO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_COMPRA_CALCULADO").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString Then
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='P' AND ID_BL = " & ID_BL)
                End If





            End If

            'FRETE VENDA
            dsFreteTaxa = Con.ExecutarQuery("SELECT ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA FROM TB_BL_TAXA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL =" & ID_BL)

            If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_MOEDA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_MIN").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO").ToString.Replace(",", ".") & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
            End If

            If dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString <> dsFreteTaxa.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString Then
                Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_PROFIT_FRETE").ToString & "' WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS) AND CD_PR='R' AND ID_BL = " & ID_BL)
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
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString <> linha.Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_COMPRA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA").ToString <> linha.Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_CALCULADO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_COMPRA_MIN").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString <> linha.Item("FL_TAXA_TRANSPORTADOR").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_TAXA_TRANSPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
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
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DECLARADO = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DECLARADO").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString <> linha.Item("FL_DIVISAO_PROFIT").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_DIVISAO_PROFIT = '" & dsCotacao.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString <> linha.Item("ID_TIPO_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_TIPO_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString <> linha.Item("ID_ORIGEM_PAGAMENTO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_ORIGEM_PAGAMENTO = " & dsCotacao.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString <> linha.Item("ID_DESTINATARIO_COBRANCA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_DESTINATARIO_COBRANCA = " & dsCotacao.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString <> linha.Item("ID_BASE_CALCULO_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_BASE_CALCULO_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString <> linha.Item("ID_MOEDA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_MOEDA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MOEDA_VENDA").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA").ToString <> linha.Item("VL_TAXA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString <> linha.Item("VL_TAXA_CALCULADO").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_CALCULADO = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_CALCULADO").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString <> linha.Item("VL_TAXA_MIN").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET VL_TAXA_MIN = " & dsCotacao.Tables(0).Rows(0).Item("VL_TAXA_VENDA_MIN").ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString <> linha.Item("OB_TAXAS").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET OB_TAXAS = '" & dsCotacao.Tables(0).Rows(0).Item("OB_TAXAS").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString <> linha.Item("FL_TAXA_TRANSPORTADOR").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_TAXA_TRANSPORTADOR = '" & dsCotacao.Tables(0).Rows(0).Item("FL_TAXA_TRANSPORTADOR").ToString & "' WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND  ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                        If dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString <> linha.Item("ID_PARCEIRO_EMPRESA").ToString Then
                            Con.ExecutarQuery("UPDATE TB_BL SET ID_PARCEIRO_EMPRESA = " & dsCotacao.Tables(0).Rows(0).Item("ID_FORNECEDOR").ToString & " WHERE ID_COTACAO_TAXA =" & ID_COTACAO_TAXA & " AND ID_BL_TAXA = " & linha.Item("ID_BL_TAXA").ToString)
                        End If

                    End If

                Next


            End If

        End If
    End Sub

    Sub UpdateCarga(ID_COTACAO As String, ID_COTACAO_MERCADORIA As String, NR_PROCESSO As String)
        If ID_COTACAO = "" Or NR_PROCESSO = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim dsCotacao As DataSet
            Dim dsProcesso As DataSet
            Dim dsInfo As DataSet = Con.ExecutarQuery("SELECT C.ID_TIPO_ESTUFAGEM,A.ID_BL FROM TB_BL A INNER JOIN TB_COTACAO C ON A.NR_PROCESSO=C.NR_PROCESSO_GERADO AND A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & ID_COTACAO & " AND A.NR_PROCESSO = '" & NR_PROCESSO & "'")
            Dim ID_TIPO_ESTUFAGEM As String = dsInfo.Tables(0).Rows(0).Item("ID_MOEDA_FRETE").ToString
            Dim ID_BL As String = dsInfo.Tables(0).Rows(0).Item("ID_BL").ToString

            If ID_TIPO_ESTUFAGEM = 1 Then

                If ID_COTACAO_MERCADORIA = "" Then
                    Exit Sub
                End If

                Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA,ISNULL(QT_CONTAINER,0)QT_CONTAINER FROM TB_COTACAO_MERCADORIA
                WHERE  ID_COTACAO = " & ID_COTACAO & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)


                If ds.Tables(0).Rows(0).Item("QT_CONTAINER") > 1 Then

                    dsProcesso = Con.ExecutarQuery("SELECT ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_TIPO_CNTR FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    dsCotacao = Con.ExecutarQuery("SELECT ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3,ID_TIPO_CONTAINER FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & ID_COTACAO & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)


                    If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_EMBALAGEM = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET QT_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_M3").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_CNTR").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_TIPO_CNTR = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                Else
                    dsProcesso = Con.ExecutarQuery("SELECT ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_TIPO_CNTR,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    dsCotacao = Con.ExecutarQuery("SELECT ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_TIPO_CONTAINER,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & ID_COTACAO & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)

                    If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_EMBALAGEM = " & dsCotacao.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET QT_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_M3").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_ALTURA").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_ALTURA").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_ALTURA = " & dsCotacao.Tables(0).Rows(0).Item("VL_ALTURA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_LARGURA").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_LARGURA").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_LARGURA = " & dsCotacao.Tables(0).Rows(0).Item("VL_LARGURA").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("VL_COMPRIMENTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_COMPRIMENTO").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_COMPRIMENTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_COMPRIMENTO").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                    If dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString <> dsProcesso.Tables(0).Rows(0).Item("ID_TIPO_CNTR").ToString Then
                        Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_TIPO_CNTR = " & dsCotacao.Tables(0).Rows(0).Item("ID_TIPO_CONTAINER").ToString & " WHERE ID_BL = " & ID_BL & " AND ID_COTACAO_MERCADORIA = " & ID_COTACAO_MERCADORIA)
                    End If

                End If

            ElseIf ID_TIPO_ESTUFAGEM = 2 Then

                dsProcesso = Con.ExecutarQuery("SELECT QT_MERCADORIA,VL_PESO_BRUTO,VL_M3 FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL)
                dsCotacao = Con.ExecutarQuery("SELECT SUM(QT_MERCADORIA)QT_MERCADORIA,SUM(VL_PESO_BRUTO)VL_PESO_BRUTO,SUM(VL_M3)VL_M3 FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & ID_COTACAO)

                If dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString Then
                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_PESO_BRUTO = " & dsCotacao.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString & " WHERE ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString <> dsProcesso.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString Then
                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET QT_MERCADORIA = " & dsCotacao.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString & " WHERE ID_BL = " & ID_BL)
                End If

                If dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString <> dsProcesso.Tables(0).Rows(0).Item("VL_M3").ToString Then
                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET VL_M3 = " & dsCotacao.Tables(0).Rows(0).Item("VL_M3").ToString & " WHERE ID_BL = " & ID_BL)
                End If


            End If


        End If
    End Sub

End Class
