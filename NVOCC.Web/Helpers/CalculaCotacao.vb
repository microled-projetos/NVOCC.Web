Public Class CalculaCotacao
    Public Function CalculaCotacao(ID_COTACAO As Integer) As String
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim VENDA_MIN As Decimal
        Dim M3 As Decimal
        Dim PESO_BRUTO As Decimal
        Dim QT_CONTAINER As Integer
        Dim PESO_TAXADO As Decimal
        Dim CLA As Decimal
        Dim CBM As Decimal

        'Calcula Frete
        Dim ds As DataSet = Con.ExecutarQuery("Select A.ID_TIPO_ESTUFAGEM,
A.ID_SERVICO,
isnull(A.VL_PESO_TAXADO,0)VL_PESO_TAXADO,
isnull(A.VL_TOTAL_M3,0)VL_M3, 
isnull(A.VL_TOTAL_PESO_BRUTO,0)VL_PESO_BRUTO,
isnull(A.VL_TOTAL_FRETE_VENDA_MIN,0)VL_TOTAL_FRETE_VENDA_MIN,
isnull(A.VL_TOTAL_FRETE_VENDA,0)VL_TOTAL_FRETE_VENDA,
(select sum(isnull(QT_CONTAINER,0)) FROM TB_COTACAO_MERCADORIA B WHERE B.ID_COTACAO = A.ID_COTACAO )QT_CONTAINER,
(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO,
(select sum(isnull(VL_CBM,0)) FROM TB_COTACAO_MERCADORIA B WHERE B.ID_COTACAO = A.ID_COTACAO )* 1.67 AS CBM,
(isnull(D.QTD_CAIXA,0) * isnull(D.VL_COMPRIMENTO,0) * isnull(D.VL_ALTURA,0) * isnull(D.VL_LARGURA,0))/5988 AS CLA
from TB_COTACAO A  
left join TB_COTACAO_MERCADORIA_DIMENSAO D ON D.ID_COTACAO = A.ID_COTACAO
Where A.ID_COTACAO = " & ID_COTACAO)

        PESO_TAXADO = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
        M3 = ds.Tables(0).Rows(0).Item("VL_M3")
        PESO_BRUTO = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
        VENDA_MIN = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN")
        If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_CONTAINER")) Then
            QT_CONTAINER = ds.Tables(0).Rows(0).Item("QT_CONTAINER")
        End If



        '        CÁLCULO DO PESO TAXADO
        'If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
        '    'AEREO
        '    CBM = ds.Tables(0).Rows(0).Item("CBM")

        '    For Each linha As DataRow In ds.Tables(0).Rows
        '        CLA = CLA + linha.Item("CLA")
        '    Next

        '    If CLA = 0 Then
        '        CLA = CBM
        '    End If
        '    Dim CLAFinal As String = CLA.ToString
        '    CLAFinal = CLAFinal.ToString.Replace(".", "")
        '    CLAFinal = CLAFinal.ToString.Replace(",", ".")
        '    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_M3 = " & CLAFinal & " WHERE ID_COTACAO = " & ID_COTACAO)
        '    Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_M3 = " & CLAFinal & " WHERE ID_COTACAO = " & ID_COTACAO)

        '    If CLA >= PESO_BRUTO Then
        '        PESO_TAXADO = CLA
        '    Else
        '        PESO_TAXADO = PESO_BRUTO
        '    End If


        '    Dim PrimeiraCasa As String = PESO_TAXADO.ToString
        '    PrimeiraCasa = PrimeiraCasa.Substring(PrimeiraCasa.IndexOf(","), 2)
        '    PrimeiraCasa = PrimeiraCasa.Replace(",", "")

        '    If PrimeiraCasa = 5 Then
        '        Dim SegundaCasa As String = PESO_TAXADO.ToString
        '        SegundaCasa = SegundaCasa.Substring(SegundaCasa.IndexOf("," & PrimeiraCasa), 3)
        '        SegundaCasa = SegundaCasa.Replace("," & PrimeiraCasa, "")
        '        If SegundaCasa > 0 Then
        '            PESO_TAXADO = Math.Ceiling(PESO_TAXADO)
        '        End If

        '    ElseIf PrimeiraCasa > 5 Then
        '        PESO_TAXADO = Math.Ceiling(PESO_TAXADO)

        '    ElseIf PrimeiraCasa < 5 And PrimeiraCasa > 0 Then

        '        Dim PESO_TAXADO_INTEIRO As Decimal = Math.Truncate(PESO_TAXADO)
        '        PESO_TAXADO = PESO_TAXADO_INTEIRO + 0.5
        '    End If

        'End If

        If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
            'MARITIMO
            PESO_BRUTO = PESO_BRUTO / 1000

            If PESO_BRUTO >= M3 Then
                PESO_TAXADO = PESO_BRUTO
            Else
                PESO_TAXADO = M3
            End If


            Dim Peso_Final As String = PESO_TAXADO.ToString
            Peso_Final = Peso_Final.ToString.Replace(".", "")
            Peso_Final = Peso_Final.ToString.Replace(",", ".")
            Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_PESO_TAXADO = " & Peso_Final & "  WHERE ID_COTACAO = " & ID_COTACAO)

        End If


        Dim FRETE_CALCULADO As Decimal = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA")


        If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
            'ID_BASE_CALCULO 34 - POR CNTR
            If FRETE_CALCULADO < VENDA_MIN Then
                FRETE_CALCULADO = VENDA_MIN
            End If
        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
            'ID_BASE_CALCULO 13 - POR TON / M³
            FRETE_CALCULADO = (FRETE_CALCULADO * PESO_TAXADO)
            If FRETE_CALCULADO < VENDA_MIN Then
                FRETE_CALCULADO = VENDA_MIN
            End If

        End If


        Dim frete_Final As String = FRETE_CALCULADO.ToString
        frete_Final = frete_Final.ToString.Replace(".", "")
        frete_Final = frete_Final.ToString.Replace(",", ".")

        Con.ExecutarQuery("UPDATE TB_COTACAO SET  VL_TOTAL_FRETE_VENDA_CALCULADO = " & frete_Final & "  WHERE ID_COTACAO = " & ID_COTACAO)


        'Calcula Taxas
        Dim dataatual As Date = Now.Date.ToString("dd/MM/yyyy")

        ds = Con.ExecutarQuery("SELECT a.ID_SERVICO,b.ID_COTACAO_TAXA,isnull(A.VL_PESO_TAXADO,0) VL_PESO_TAXADO,isnull(A.ID_MOEDA_FRETE,0)ID_MOEDA_FRETE,isnull(A.ID_INCOTERM,0)ID_INCOTERM,
isnull(B.VL_TAXA_COMPRA,0)VL_TAXA_COMPRA,
isnull(B.VL_TAXA_VENDA,0)VL_TAXA_VENDA,
B.ID_BASE_CALCULO_TAXA,isnull(A.VL_TOTAL_M3,0)VL_M3, 
isnull(A.VL_TOTAL_PESO_BRUTO,0)VL_PESO_BRUTO, 
(select CASE WHEN A.ID_MOEDA_FRETE = 124 THEN CONVERT(varchar,GETDATE(),103) ELSE (select CONVERT(varchar,MAX(DT_CAMBIO),103) FROM TB_MOEDA_FRETE WHERE ID_MOEDA = A.ID_MOEDA_FRETE) END)DT_CAMBIO,
isnull(B.VL_TAXA_COMPRA_MIN,0)VL_TAXA_COMPRA_MIN,
isnull(B.VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN,
isnull(B.ID_MOEDA_COMPRA,0)ID_MOEDA_COMPRA,
isnull(B.ID_MOEDA_VENDA,0)ID_MOEDA_VENDA,
isnull(B.QTD_BASE_CALCULO,0)QTD_BASE_CALCULO
From TB_COTACAO A 
Left Join TB_COTACAO_TAXA B ON A.ID_COTACAO = B.ID_COTACAO 
Left Join TB_BASE_CALCULO_TAXA C ON B.ID_BASE_CALCULO_TAXA = C.ID_BASE_CALCULO_TAXA
WHERE A.ID_COTACAO =" & ID_COTACAO & " ORDER BY NM_BASE_CALCULO_TAXA desc")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Dim COMPRA_MIN As Decimal = linha.Item("VL_TAXA_COMPRA_MIN")
                VENDA_MIN = linha.Item("VL_TAXA_VENDA_MIN")
                Dim QTD_BASE_CALCULO As Integer = linha.Item("QTD_BASE_CALCULO")
                Dim x As Double
                Dim y As Double
                Dim z As Double
                Dim CompraCalc As String = "0"
                Dim VendaCalc As String = "0"
                If IsDBNull(linha.Item("ID_COTACAO_TAXA")) Then
                    Return "Não há taxas vinculadas a essa cotação"
                    Exit Function


                ElseIf IsDBNull(linha.Item("ID_BASE_CALCULO_TAXA")) Then

                    Return "Base de Calculo não informada."
                    Exit Function

                ElseIf linha.Item("ID_MOEDA_FRETE") = 0 Then

                    Return "Moeda de frete não informada."
                    Exit Function

                ElseIf linha.Item("ID_MOEDA_COMPRA") = 0 And linha.Item("VL_TAXA_COMPRA") <> 0 Then

                    Return "Moeda não informada."
                    Exit Function

                ElseIf linha.Item("ID_MOEDA_VENDA") = 0 And linha.Item("VL_TAXA_VENDA") <> 0 Then

                    Return "Moeda não informada."
                    Exit Function

                ElseIf IsDBNull(linha.Item("DT_CAMBIO")) Then

                    Return "Não há valor de moeda de câmbio cadastrado com a data atual."
                    Exit Function

                ElseIf linha.Item("DT_CAMBIO") < dataatual Then

                    Return "Não há valor de moeda de câmbio cadastrado com a data atual."
                    Exit Function

                ElseIf linha.Item("DT_CAMBIO") > dataatual Then

                    Return "Não há valor de moeda de câmbio cadastrado com a data atual."
                    Exit Function

                Else
                    If linha.Item("ID_BASE_CALCULO_TAXA") = 1 Then

                        Return "Base de Calculo não informada."
                        Exit Function

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 2 Then
                        '% VR DO FRETE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 as QTD
FROM TB_COTACAO A
WHERE A.ID_COTACAO = " & ID_COTACAO)

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 3 Then
                        'VR DO FRETE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 as QTD
FROM TB_COTACAO A
WHERE A.ID_COTACAO =  " & ID_COTACAO)

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 4 Then
                        '% TOTAL DO HOUSE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT 
   sum(VL_TAXA_VENDA_CALCULADO) + VL_TOTAL_FRETE_VENDA_CALCULADO TOTAL_VENDA, 
   
   sum(VL_TAXA_COMPRA_CALCULADO) + VL_TOTAL_FRETE_COMPRA TOTAL_COMPRA
FROM TB_COTACAO A
INNER JOIN TB_COTACAO_TAXA B ON B.ID_COTACAO = A.ID_COTACAO
WHERE B.FL_DECLARADO = 1
AND A.ID_MOEDA_FRETE = B.ID_MOEDA_VENDA 
AND A.ID_COTACAO = " & ID_COTACAO & "
GROUP BY A.ID_COTACAO,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_COMPRA")

                        If ds1.Tables(0).Rows.Count > 0 Then
                            If Not IsDBNull(ds1.Tables(0).Rows(0).Item("TOTAL_COMPRA")) Then
                                x = ds1.Tables(0).Rows(0).Item("TOTAL_COMPRA") / 100
                            Else
                                x = 0 / 100
                            End If
                        Else
                            x = 0 / 100
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString


                        If ds1.Tables(0).Rows.Count > 0 Then
                            If Not IsDBNull(ds1.Tables(0).Rows(0).Item("TOTAL_VENDA")) Then
                                x = ds1.Tables(0).Rows(0).Item("TOTAL_VENDA") / 100
                            Else
                                x = 0 / 100
                            End If
                        Else
                            x = 0 / 100
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 5 Then
                        'VALOR FIXO

                        VendaCalc = linha.Item("VL_TAXA_VENDA").ToString()
                        CompraCalc = linha.Item("VL_TAXA_COMPRA").ToString()


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 6 Then
                        'POR M³

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 7 Then
                        'POR TON

                        x = linha.Item("VL_PESO_BRUTO")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x / 1000
                        z = y * z
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString


                        x = linha.Item("VL_PESO_BRUTO")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = x / 1000
                        z = y * z
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString



                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 10 Then
                        'POR MAFI 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (19)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 11 Then
                        'POR CNTR 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (5,6,2,9,10,12,16,18,19)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 12 Then
                        'POR CNTR 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (17,13,14,15,11,3,4,7,8,1)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 13 Then
                        'POR TON / M³
                        If linha.Item("ID_SERVICO") = 1 Or linha.Item("ID_SERVICO") = 4 Then
                            'MARITIMO


                            x = linha.Item("VL_M3")
                            y = linha.Item("VL_PESO_BRUTO") / 1000



                            If x > y Then
                                x = x
                            Else
                                x = y
                            End If



                            y = linha.Item("VL_TAXA_VENDA")
                            z = x * y
                            If VENDA_MIN < 0 Then
                                If z > VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            ElseIf VENDA_MIN > 0 Then
                                If z < VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            End If
                            VendaCalc = z.ToString

                            y = linha.Item("VL_TAXA_COMPRA")
                            z = x * y
                            If COMPRA_MIN < 0 Then
                                If z > COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            ElseIf COMPRA_MIN > 0 Then
                                If z < COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            End If
                            CompraCalc = z.ToString


                        ElseIf linha.Item("ID_SERVICO") = 2 Or linha.Item("ID_SERVICO") = 5 Then
                            'AEREO


                            x = linha.Item("VL_M3")

                            y = linha.Item("VL_TAXA_VENDA")
                            z = x * y
                            If VENDA_MIN < 0 Then
                                If z > VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            ElseIf VENDA_MIN > 0 Then
                                If z < VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            End If
                            VendaCalc = z.ToString

                            y = linha.Item("VL_TAXA_COMPRA")
                            z = x * y
                            If COMPRA_MIN < 0 Then
                                If z > COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            ElseIf COMPRA_MIN > 0 Then
                                If z < COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            End If
                            CompraCalc = z.ToString
                        End If



                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 14 Then
                        'POR KG

                        'If linha.Item("ID_SERVICO") = 1 Or linha.Item("ID_SERVICO") = 4 Then
                        '    'MARITIMO
                        '    x = linha.Item("VL_PESO_BRUTO")

                        'ElseIf linha.Item("ID_SERVICO") = 2 Or linha.Item("ID_SERVICO") = 5 Then
                        '    'AEREO
                        '    x = linha.Item("VL_PESO_TAXADO")

                        'End If


                        x = linha.Item("VL_PESO_BRUTO")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z


                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 15 Then
                        '% VR DA MERCADORIA
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_CARGA),0)) AS VALOR
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " ")

                        x = ds1.Tables(0).Rows(0).Item("VALOR") / 100
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("VALOR") / 100
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 16 Then
                        '% HOUSE COLLECT

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT 
      isnull((sum(VL_TAXA_VENDA_CALCULADO) 
   + CASE WHEN A.ID_TIPO_PAGAMENTO = 1 THEN VL_TOTAL_FRETE_VENDA_CALCULADO ELSE 0 END),0)   
   TOTAL_VENDA, 
   
   isnull((sum(VL_TAXA_COMPRA_CALCULADO) + 
      + CASE WHEN A.ID_TIPO_PAGAMENTO = 1 THEN VL_TOTAL_FRETE_COMPRA ELSE 0 END),0) 
	  TOTAL_COMPRA
FROM TB_COTACAO A
INNER JOIN TB_COTACAO_TAXA B ON B.ID_COTACAO = A.ID_COTACAO
WHERE B.FL_DECLARADO = 1
AND B.ID_MOEDA_VENDA = B.ID_MOEDA_VENDA 
AND A.ID_COTACAO = " & ID_COTACAO & "
AND B.ID_TIPO_PAGAMENTO = 1
GROUP BY A.ID_COTACAO,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_COMPRA,A.ID_TIPO_PAGAMENTO")
                        If ds1.Tables(0).Rows.Count = 0 Then
                            CompraCalc = COMPRA_MIN.ToString
                            VendaCalc = VENDA_MIN.ToString
                        Else
                            x = ds1.Tables(0).Rows(0).Item("TOTAL_COMPRA") / 100
                            y = linha.Item("VL_TAXA_COMPRA")
                            z = y * x
                            If COMPRA_MIN < 0 Then
                                If z > COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            ElseIf COMPRA_MIN > 0 Then
                                If z < COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            End If

                            CompraCalc = z.ToString

                            x = ds1.Tables(0).Rows(0).Item("TOTAL_VENDA") / 100
                            y = linha.Item("VL_TAXA_VENDA")
                            z = y * x
                            If VENDA_MIN < 0 Then
                                If z > VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            ElseIf VENDA_MIN > 0 Then
                                If z < VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            End If
                            VendaCalc = z.ToString
                        End If


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 20 Then
                        'POR HC 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER = 10")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 21 Then
                        'POR FLAT RACK 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER in (15)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 22 Then
                        ' POR OPEN TOP 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER in (9)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 23 Then
                        'POR OPEN TOP 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER in (8)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 24 Then
                        'POR FLAT RACK 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER in (16)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 25 Then
                        'POR REEFER 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER in (5)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 26 Then
                        'POR REEFER 40
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER in (4)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 28 Then
                        'POR MAFI 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (13)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 29 Then
                        'VALOR POR EMBARQUE- valor fixo digitado

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString



                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 30 Then
                        'POR UNIDADE 
                        Dim ds1 As DataSet

                        If linha.Item("ID_SERVICO") = 1 Or linha.Item("ID_SERVICO") = 4 Then
                            'MARITIMO -quantidade de conteineres do processo
                            ds1 = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO & "")

                            x = ds1.Tables(0).Rows(0).Item("QTD")

                        ElseIf linha.Item("ID_SERVICO") = 2 Or linha.Item("ID_SERVICO") = 5 Then
                            'AEREO  - quantidade de caixas de mercadoria do processo
                            '                            ds1 = Con.ExecutarQuery("SELECT ISNULL(SUM(QTD_CAIXA),0)QTD
                            'FROM TB_COTACAO_MERCADORIA_DIMENSAO A
                            'WHERE A.ID_COTACAO = " & ID_COTACAO & "")

                            ds1 = Con.ExecutarQuery("SELECT SUM(QT_MERCADORIA)QTD FROM TB_COTACAO_MERCADORIA WHERE ID_COTACAO = " & ID_COTACAO & "")

                            x = ds1.Tables(0).Rows(0).Item("QTD")
                        End If



                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 31 Then
                        'POR HAWB (AEREO)- na cotação é 1 por 1

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 32 Then
                        'POR HBL (MARITIMO) - na cotação é 1 por 1

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 33 Then
                        'POR DOCUMENTO

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf (linha.Item("ID_BASE_CALCULO_TAXA") = 38 Or linha.Item("ID_BASE_CALCULO_TAXA") = 40 Or linha.Item("ID_BASE_CALCULO_TAXA") = 41) Then
                        'POR DOC/SHIPPER   -   POR ENTRADA    -   POR CARGA

                        z = linha.Item("VL_TAXA_VENDA") * QTD_BASE_CALCULO
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA") * QTD_BASE_CALCULO
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 34 Then
                        'POR CNTR 
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & ID_COTACAO)

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x

                        If x = 0 Then
                            x = 1
                        End If


                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 35 Then
                        ' POR TEU

                        'Para cada conteiner de 20' corresponde 1 teu
                        Dim ds1 As DataSet = Con.ExecutarQuery(" Select ISNULL(SUM(QT_CONTAINER), 0)QTD
From TB_COTACAO_MERCADORIA A
Where a.ID_COTACAO = " & ID_COTACAO & " And ID_TIPO_CONTAINER In (5,6,2,9,10,12,16,18)")
                        y = ds1.Tables(0).Rows(0).Item("QTD")


                        'Para cada conteiner de 40' corresponde a 2 teus

                        ds1 = Con.ExecutarQuery("Select ISNULL(SUM(QT_CONTAINER), 0)QTD
From TB_COTACAO_MERCADORIA A
Where a.ID_COTACAO = " & ID_COTACAO & " And ID_TIPO_CONTAINER In (19,17,13,14,15,11,3,4,7,8,1)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        x = x * 2
                        Dim total As Integer = x + y

                        z = total * linha.Item("VL_TAXA_VENDA")
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = total * linha.Item("VL_TAXA_COMPRA")
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 36 Then
                        'POR REEFER
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (4,5)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 37 Then
                        'SEGURO

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_CARGA),0)) AS VALOR_CARGA, (ISNULL(SUM(B.VL_TOTAL_FRETE_VENDA_CALCULADO),0)) AS FRETE_VENDA_CALCULADO
FROM TB_COTACAO_MERCADORIA A
INNER JOIN TB_COTACAO B ON A.ID_COTACAO = B.ID_COTACAO
WHERE A.ID_COTACAO = " & ID_COTACAO & " ")
                        Dim TAXAS_DECLARADAS As Decimal = 0
                        Dim FOB As Decimal = ds1.Tables(0).Rows(0).Item("VALOR_CARGA")
                        Dim FRETE As Decimal = ds1.Tables(0).Rows(0).Item("FRETE_VENDA_CALCULADO")

                        If linha.Item("ID_INCOTERM") = 10 Then
                            ds1 = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_TAXA_VENDA_CALCULADO),0)) AS VALOR_TAXA
FROM TB_COTACAO_TAXA A
WHERE  FL_DECLARADO = 1 AND A.ID_COTACAO = " & ID_COTACAO & " ")
                            TAXAS_DECLARADAS = ds1.Tables(0).Rows(0).Item("VALOR_TAXA")
                        End If

                        Dim DESPESA As Decimal = FOB + FRETE + TAXAS_DECLARADAS
                        DESPESA = DESPESA / 100
                        DESPESA = DESPESA * 10

                        Dim TOTAL As Decimal = DESPESA + FRETE + TAXAS_DECLARADAS + FOB

                        x = TOTAL / 100
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = TOTAL / 100
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 42 Then
                        'POR PESO TAXADO

                        x = linha.Item("VL_PESO_TAXADO")

                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 43 Then
                        ' 40 HC


                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (1)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 44 Then
                        ' 20 DRY
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (2)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 45 Then
                        ' 40 DRY
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (3)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 46 Then
                        ' 40 NOR
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (14)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 47 Then
                        ' 20 ISOTANK
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (18)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 48 Then
                        ' 20 SOC
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (29)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 49 Then
                        ' 40 SOC
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & ID_COTACAO & " AND ID_TIPO_CONTAINER IN (30)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    End If


                    CompraCalc = CompraCalc.Replace(".", String.Empty)
                    CompraCalc = CompraCalc.Replace(",", ".")
                    VendaCalc = VendaCalc.Replace(".", String.Empty)
                    VendaCalc = VendaCalc.Replace(",", ".")


                    Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET VL_TAXA_COMPRA_CALCULADO = '" & CompraCalc & "', VL_TAXA_VENDA_CALCULADO = '" & VendaCalc & "' WHERE ID_COTACAO_TAXA = " & linha.Item("ID_COTACAO_TAXA"))


                End If
            Next

        End If


        Con.ExecutarQuery("UPDATE TB_COTACAO SET Dt_Calculo_Cotacao = GETDATE() WHERE ID_COTACAO = " & ID_COTACAO)

        Return "Calculo realizado com sucesso"

    End Function

End Class