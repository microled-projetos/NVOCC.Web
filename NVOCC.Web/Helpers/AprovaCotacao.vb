﻿Public Class AprovaCotacao
    Public Function AprovaCotacao(ID_COTACAO As Integer, ID_SERVICO As Integer, ID_ESTUFAGEM As Integer, ID_DIVISAO_PROFIT As Integer) As String
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim PROCESSO_FINAL As String = ""
        Dim ID_BL As String
        Dim ID_BL_OLD As String = 0
        Dim OB_CLIENTE As String = ""
        Dim OB_AGENTE_INTERNACIONAL As String = ""
        Dim OB_COMERCIAL As String = ""
        Dim OB_OPERACIONAL_INTERNA As String = ""
        Dim HBL As String = "0"



        ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Processo_" & Now.Year.ToString & " NRSEQUENCIALPROCESSO")
        Dim NRSEQUENCIALPROCESSO As Integer = ds.Tables(0).Rows(0).Item("NRSEQUENCIALPROCESSO")
        Dim ano_atual = Now.Year.ToString.Substring(2)
        Dim SIGLA_PROCESSO As String
        Dim mes_atual As String
        If Now.Month < 10 Then
            mes_atual = "0" & Now.Month.ToString
        Else
            mes_atual = Now.Month.ToString
        End If

        ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO
                            from TB_COTACAO A Where A.ID_COTACAO = " & ID_COTACAO)

        SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")


        PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

        Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "', ANOSEQUENCIALPROCESSO = year(getdate()) ")

        Con.ExecutarQuery("UPDATE TB_COTACAO SET NR_PROCESSO_GERADO = '" & PROCESSO_FINAL & "' WHERE ID_COTACAO = " & ID_COTACAO)




        Dim dsBL As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,GRAU,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,
ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_PARCEIRO_TRANSPORTADOR,ID_COTACAO,DT_ABERTURA,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,VL_FRETE,ID_MOEDA_FRETE,ID_PARCEIRO_VENDEDOR,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_TIPO_PAGAMENTO,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR,VL_CARGA,FINAL_DESTINATION, ID_PARCEIRO_RODOVIARIO,VL_PESO_TAXADO,VL_M3,FL_EMAIL_COTACAO, EMAIL_COTACAO, NR_CONTRATO_ARMADOR,FL_TC4,FL_TC6,ID_TIPO_AERONAVE) 
SELECT '" & PROCESSO_FINAL & "','C', " & ID_SERVICO & ",ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM, ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,GETDATE(),VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_TIPO_PAGAMENTO,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR, (SELECT (ISNULL(SUM(VL_CARGA),0))
        FROM TB_COTACAO_MERCADORIA B WHERE A.ID_COTACAO = B.ID_COTACAO ),FINAL_DESTINATION,ID_PARCEIRO_RODOVIARIO,VL_PESO_TAXADO,VL_TOTAL_M3,FL_EMAIL_COTACAO, EMAIL_COTACAO, NR_CONTRATO_ARMADOR, FL_TC4,FL_TC6,ID_TIPO_AERONAVE FROM TB_COTACAO A WHERE A.ID_COTACAO = " & ID_COTACAO & " Select SCOPE_IDENTITY() as ID_BL ")

        ID_BL = dsBL.Tables(0).Rows(0).Item("ID_BL").ToString()


        'UPDATE INSERINDO ID_BL NAS REFERENCIAS DA COTAÇÃO
        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_BL = " & ID_BL & " WHERE ID_COTACAO = " & ID_COTACAO)

        'UPDATE INSERINDO ID_BL NOS ARQUIVOS DE UPLOAD DA COTAÇÃO
        Con.ExecutarQuery("UPDATE TB_UPLOADS SET ID_BL = " & ID_BL & " WHERE ID_COTACAO = " & ID_COTACAO)


        'TAXAS COMPRAS
        Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF,ID_COTACAO_TAXA,QTD_BASE_CALCULO ) 
SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'P',ID_FORNECEDOR,'COTA',ID_COTACAO_TAXA,QTD_BASE_CALCULO FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO = " & ID_COTACAO)

        'TAXAS VENDA
        Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_COTACAO_TAXA,QTD_BASE_CALCULO) 
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
 
 WHEN ID_DESTINATARIO_COBRANCA = 8 and (SELECT isnull(ID_PARCEIRO_EXPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") <> 0 then
 (SELECT ID_PARCEIRO_EXPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") 
 
 ELSE NULL
 END ID_PARCEIRO_EMPRESA,
 

CASE
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN 1
 ELSE ID_DESTINATARIO_COBRANCA
 END ID_DESTINATARIO_COBRANCA,'COTA',ID_COTACAO_TAXA,QTD_BASE_CALCULO

FROM TB_COTACAO_TAXA WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA <> 0 AND  ID_COTACAO = " & ID_COTACAO)

        Dim FL_PROFIT_FRETE As Integer = 0
        If ID_DIVISAO_PROFIT <> 0 Then
            Dim dsProfit As DataSet = Con.ExecutarQuery("SELECT isnull(FL_PROFIT_FRETE,0)FL_PROFIT_FRETE FROM [dbo].TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT = " & ID_DIVISAO_PROFIT)
            FL_PROFIT_FRETE = dsProfit.Tables(0).Rows(0).Item("FL_PROFIT_FRETE")
        End If



        Dim ID_BASE_CALCULO As Integer

        If ID_ESTUFAGEM = 1 Then

            ID_BASE_CALCULO = 5 'VALOR FIXO
            'FRETE COMPRA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,FL_TAXA_TRANSPORTADOR,CD_ORIGEM_INF)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P', " & FL_PROFIT_FRETE & ",
 
 ID_TRANSPORTADOR AS ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA,
 1,'COTA'
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)

        ElseIf ID_ESTUFAGEM = 2 Then

            ID_BASE_CALCULO = 13 'POR TON / M³
        Else
            ID_BASE_CALCULO = 0
        End If


        If ID_SERVICO = 2 Or ID_SERVICO = 5 Then
            ID_BASE_CALCULO = 42

            'FRETE COMPRA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,FL_TAXA_TRANSPORTADOR,CD_ORIGEM_INF,FL_DECLARADO,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA * VL_PESO_TAXADO,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P',  ISNULL(FL_FRETE_PROFIT,0)FL_FRETE_PROFIT ,
 
 ID_TRANSPORTADOR AS ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA,
 1,'COTA',ISNULL(FL_FRETE_DECLARADO,0)FL_FRETE_DECLARADO,ID_TIPO_PAGAMENTO,
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
end ID_ORIGEM_PAGAMENTO
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)


            'FRETE VENDA AEREO
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_ORIGEM_PAGAMENTO,FL_DECLARADO)

 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, ISNULL(FL_FRETE_PROFIT,0)FL_FRETE_PROFIT ,

 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA ,'COTA',


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
end ID_ORIGEM_PAGAMENTO,ISNULL(FL_FRETE_DECLARADO,0)FL_FRETE_DECLARADO
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)


        Else



            'FRETE VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF,ID_ORIGEM_PAGAMENTO)

 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & " ,

 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA ,'COTA',
 

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
end ID_ORIGEM_PAGAMENTO
 
 FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO)

        End If

        Dim dsCarga As DataSet
        If ID_SERVICO = 2 Or ID_SERVICO = 5 Then

            dsCarga = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & ID_COTACAO)
            If dsCarga.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In dsCarga.Tables(0).Rows
                    Dim dsInsertCarga As DataSet = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,VL_PESO_BRUTO,VL_M3,ID_BL,ID_COTACAO_MERCADORIA,DS_MERCADORIA,ID_TIPO_CARGA) 
SELECT ID_MERCADORIA, ID_MERCADORIA, VL_PESO_BRUTO, VL_M3, " & ID_BL & " , ID_COTACAO_MERCADORIA,DS_MERCADORIA, (SELECT ID_TIPO_CARGA FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ") FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO_MERCADORIA =" & linha.Item("ID_COTACAO_MERCADORIA") & " Select SCOPE_IDENTITY() as ID_CARGA_BL")

                    Dim ID_CARGA_BL As String = dsInsertCarga.Tables(0).Rows(0).Item("ID_CARGA_BL")

                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL_DIMENSAO (QTD_CAIXA,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,ID_COTACAO_MERCADORIA,ID_COTACAO_MERCADORIA_DIMENSAO,ID_BL,ID_CARGA_BL) 
SELECT B.QTD_CAIXA,B.VL_ALTURA,B.VL_COMPRIMENTO,B.VL_LARGURA, A.ID_COTACAO_MERCADORIA, ID , " & ID_BL & " , " & ID_CARGA_BL & "
FROM TB_COTACAO_MERCADORIA A
INNER JOIN TB_COTACAO_MERCADORIA_DIMENSAO B ON A.ID_COTACAO_MERCADORIA = B.ID_COTACAO_MERCADORIA AND A.ID_COTACAO = B.ID_COTACAO
WHERE A.ID_COTACAO_MERCADORIA =" & linha.Item("ID_COTACAO_MERCADORIA"))

                Next


            End If

        Else

            If ID_ESTUFAGEM = 1 Then

                dsCarga = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA,QT_CONTAINER FROM TB_COTACAO_MERCADORIA
         WHERE QT_CONTAINER is not null and QT_CONTAINER <> 0 and ID_COTACAO = " & ID_COTACAO)
                If dsCarga.Tables(0).Rows.Count > 0 Then
                    Dim QT_CONTAINER As Integer
                    For Each linha As DataRow In dsCarga.Tables(0).Rows
                        QT_CONTAINER = linha.Item("QT_CONTAINER")

                        For i As Integer = 1 To QT_CONTAINER Step 1
                            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_TIPO_CNTR,ID_COTACAO_MERCADORIA,ID_TIPO_CARGA) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3," & ID_BL & ",ID_TIPO_CONTAINER, ID_COTACAO_MERCADORIA, (SELECT ID_TIPO_CARGA FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")  FROM TB_COTACAO_MERCADORIA
                WHERE ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))
                        Next
                    Next
                Else
                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,ID_BL,ID_COTACAO_MERCADORIA,ID_TIPO_CARGA) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO," & ID_BL & ",ID_COTACAO_MERCADORIA, (SELECT ID_TIPO_CARGA FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")  FROM TB_COTACAO_MERCADORIA
         WHERE ID_COTACAO =  " & ID_COTACAO)
                End If


            ElseIf ID_ESTUFAGEM = 2 Then

                Dim ID_MERCADORIA As Integer = 11
                dsCarga = Con.ExecutarQuery("SELECT DISTINCT ID_MERCADORIA FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & ID_COTACAO)
                If dsCarga.Tables(0).Rows.Count = 1 Then
                    ID_MERCADORIA = dsCarga.Tables(0).Rows(0).Item("ID_MERCADORIA")
                End If

                Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_MERCADORIA,ID_EMBALAGEM,ID_TIPO_CARGA) 
        SELECT SUM(QT_MERCADORIA)QT_MERCADORIA,SUM(VL_PESO_BRUTO)VL_PESO_BRUTO,SUM(VL_M3)VL_M3," & ID_BL & ", " & ID_MERCADORIA & "," & ID_MERCADORIA & ",(SELECT ID_TIPO_CARGA FROM TB_COTACAO WHERE ID_COTACAO = " & ID_COTACAO & ")  FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO =  " & ID_COTACAO)

            End If
        End If





        Return PROCESSO_FINAL

    End Function

End Class