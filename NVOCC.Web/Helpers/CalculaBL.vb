Public Class CalculaBL
    Public Function Calcular(ID_BL_TAXA As String) As String

        Dim msg0 As String = "BL calculada com sucesso!"
        Dim Taxa As String = ""
        Dim x As Double
        Dim y As Double
        Dim z As Double
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT B.ID_BL,B.ID_BL_TAXA, isnull(B.VL_TAXA,0)VL_TAXA,B.ID_MOEDA,isnull(B.VL_CAMBIO,0)VL_CAMBIO,B.ID_BASE_CALCULO_TAXA,isnull(A.VL_M3,0)VL_M3,isnull(A.VL_PESO_BRUTO,0)VL_PESO_BRUTO,
CASE WHEN B.ID_MOEDA = 124 THEN CONVERT(VARCHAR, GETDATE(), 103) ELSE
(SELECT CONVERT(VARCHAR,MAX(DT_CAMBIO),103) FROM TB_MOEDA_FRETE WHERE ID_MOEDA = B.ID_MOEDA) END DT_CAMBIO
FROM TB_BL A
INNER JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL 
 WHERE ID_BASE_CALCULO_TAXA IS NOT NULL 
AND ID_MOEDA <> 0
AND ID_BASE_CALCULO_TAXA <> 1 
AND ID_BL_TAXA NOT IN (SELECT ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON B.ID_CONTA_PAGAR_RECEBER= A.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL  AND ID_BL_TAXA IS NOT NULL) AND B.ID_BL_TAXA = " & ID_BL_TAXA)

        If ds.Tables(0).Rows.Count > 0 Then
            Dim ID_BL As String = ds.Tables(0).Rows(0).Item("ID_BL")

            If ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 2 Then
                '% VR DO FRETE
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_FRETE),0) * 1/100 )QTD
        FROM TB_BL A
        WHERE A.ID_BL =  " & ID_BL)
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 3 Then
                '% VR DO FRETE
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_FRETE),0) * 1/100 )QTD
        FROM TB_BL A
        WHERE A.ID_BL =  " & ID_BL)
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 4 Then
                'TOTAL DO HOUSE
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(DISTINCT ID_BL)QTD FROM TB_BL where ID_BL = " & ID_BL)
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 5 Then
                'VALOR FIXO
                Taxa = ds.Tables(0).Rows(0).Item("VL_TAXA").ToString()



            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 6 Then
                'POR M³


                x = ds.Tables(0).Rows(0).Item("VL_M3")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = x * y
                Taxa = z.ToString



            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 7 Then
                'POR TON - peso bruto de todos os conteineres do processo /1000

                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT count(ID_CNTR_BL)QTD
        FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ")")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                z = y * x
                z = z / 1000

                Taxa = z.ToString



            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 10 Then
                'POR MAFI 20'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND ID_TIPO_CNTR IN (19)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString




            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 11 Then
                'POR CNTR 20'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND ID_TIPO_CNTR IN (5,6,2,9,10,12,16,18,19)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString



            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 12 Then
                'POR CNTR 40'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND ID_TIPO_CNTR IN (17,13,14,15,11,3,4,7,8,1)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 13 Then
                'POR TON / M³

                x = ds.Tables(0).Rows(0).Item("VL_M3")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = x * y
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 14 Then
                'POR KG
                x = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")

                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = x * y
                Taxa = z



            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 15 Then
                '% VR DA MERCADORIA
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_PESO_BRUTO),0) * 1/100 ) AS VALOR
        FROM TB_CARGA_BL A WHERE A.ID_BL = " & ID_BL)
                x = ds1.Tables(0).Rows(0).Item("VALOR")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 16 Then
                '% HOUSE COLLECT

                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT COUNT(DISTINCT ID_BL)QTD FROM TB_BL where ID_BL = " & ID_BL & " And ID_TIPO_PAGAMENTO = 1 ")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 20 Then
                'POR HC 20'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR = 10")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 21 Then
                'POR FLAT RACK 40'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR  in (15)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString



            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 22 Then
                ' POR OPEN TOP 20'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_B)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR in (9)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 23 Then
                'POR OPEN TOP 40'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_B)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR in (8)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 24 Then
                'POR FLAT RACK 20'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR in (16)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 25 Then
                'POR REEFER 20'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR in (5)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 26 Then
                'POR REEFER 40
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR in (4)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 28 Then
                'POR MAFI 40'
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND AND ID_TIPO_CNTR IN (13)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString


            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 29 Then
                'VALOR POR EMBARQUE- valor fixo digitado
                Taxa = ds.Tables(0).Rows(0).Item("VL_TAXA").ToString()

            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 30 Then
                'POR UNIDADE - quantidade de conteineres do processo

                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") ")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString

            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 31 Then
                'POR HAWB(AEREO)
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(COUNT(ID_BL),0)QTD
FROM TB_BL A
WHERE A.ID_BL = " & ID_BL & " AND ID_SERVICO IN (3,5) AND GRAU = 'C' ")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString

            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 32 Then
                'POR HBL (MARITIMO)
                Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(COUNT(ID_BL),0)QTD
FROM TB_BL A
WHERE A.ID_BL = " & ID_BL & " AND ID_SERVICO IN (1,4) AND GRAU = 'C' ")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString

            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 33 Then
                'POR DOCUMENTO
                Taxa = ds.Tables(0).Rows(0).Item("VL_TAXA").ToString()

            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 34 Then
                'POR CNTR 
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER =(SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") ")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                y = ds.Tables(0).Rows(0).Item("VL_TAXA")
                z = y * x
                Taxa = z.ToString



            ElseIf ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA") = 35 Then
                ' POR TEU

                'Para cada conteiner de 20' corresponde 1 teu
                Dim ds1 As DataSet = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ") AND ID_TIPO_CNTR in (5,6,2,9,10,12,16,18)")
                y = ds1.Tables(0).Rows(0).Item("QTD")


                'Para cada conteiner de 40' corresponde a 2 teus

                ds1 = Con.ExecutarQuery("Select count(ID_CNTR_BL)QTD FROM TB_CNTR_BL A
        WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID_BL & ")  AND ID_TIPO_CNTR In (19,17,13,14,15,11,3,4,7,8,1)")
                x = ds1.Tables(0).Rows(0).Item("QTD")
                x = x * 2

                z = x + y
                Taxa = z.ToString

            Else

                Return "Base de Calculo não encontrada"
                Exit Function

            End If

            Taxa = Taxa.Replace(".", String.Empty).Replace(",", ".")

            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_CALCULADO = 1, VL_TAXA_CALCULADO = '" & Taxa & "' , DT_CALCULO = GetDate() WHERE ID_BL_TAXA = " & ds.Tables(0).Rows(0).Item("ID_BL_TAXA") & " ; UPDATE TB_BL SET FL_CALCULADO = 1 WHERE ID_BL =" & ID_BL)

            Return "BL calculada com sucesso!"
        Else

            Return "Taxa não encontrada"
            Exit Function

        End If

    End Function
End Class
