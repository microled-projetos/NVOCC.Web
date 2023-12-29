Public Class AtualizaStatusFreteAgente
    Function AtualizaStatusFreteAgenteHouse(ID_BL As Integer, ID_TIPO_PAGAMENTO_HOUSE As Integer, FL_FREE_HAND As Boolean) As Integer
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        ds = Con.ExecutarQuery("SELECT ISNULL(NR_PROCESSO,0)NR_PROCESSO, ISNULL(ID_SERVICO,0)ID_SERVICO, ISNULL(ID_TIPO_ESTUFAGEM,0)ID_TIPO_ESTUFAGEM, ISNULL(FL_FREE_HAND,0)FL_FREE_HAND, ISNULL(ID_TIPO_PAGAMENTO,0)ID_TIPO_PAGAMENTO, ISNULL((SELECT ID_TIPO_PAGAMENTO FROM TB_BL B WHERE B.ID_BL =A.ID_BL_MASTER),0)ID_TIPO_PAGAMENTO_MASTER FROM TB_BL A WHERE ID_BL = " & ID_BL)
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                ' AGENCIAMENTO DE IMPORTACAO MARITIMA
                ' AGENCIAMENTO DE EXPORTACAO MARITIMA

                If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                    'ESTUFAGEM FCL


                    If FL_FREE_HAND = True Then
                        'É FREE HAND

                        'TIPO PAGAMENTO MASTER
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 1 Then
                            'PAGAMENTO COLLECT

                            'TIPO PAGAMENTO HOUSE
                            If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                                'PAGAMENTO COLLECT

                                Return 4 ' DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                            ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                                'PAGAMENTO PREPAID

                                Return 2 'DEVOLUÇÃO/ RECEBIMENTO DO FRETE DE COMPRA

                            End If

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 2 Then
                            'PAGAMENTO PREPAID

                            'TIPO PAGAMENTO HOUSE
                            If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                                'PAGAMENTO COLLECT


                                Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                            ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                                'PAGAMENTO PREPAID

                                Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                            End If

                        End If

                    ElseIf FL_FREE_HAND = False Then
                        'NÃO É FREE HAND

                        'TIPO PAGAMENTO MASTER
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 1 Then
                            'PAGAMENTO COLLECT

                            'TIPO PAGAMENTO HOUSE
                            If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                                'PAGAMENTO COLLECT

                                Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                            ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                                'PAGAMENTO PREPAID

                                Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                            End If

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 2 Then
                            'PAGAMENTO PREPAID

                            'TIPO PAGAMENTO HOUSE
                            If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                                'PAGAMENTO COLLECT

                                Return 2 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE COMPRA

                            ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                                'PAGAMENTO PREPAID

                                Return 4 'DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                            End If

                        End If

                    End If

                Else ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2
                    'ESTUFAGEM LCL

                    If FL_FREE_HAND = True Then
                        'É FREE HAND


                        'TIPO PAGAMENTO HOUSE
                        If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                            'PAGAMENTO COLLECT

                            Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                        ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                            'PAGAMENTO PREPAID

                            Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                        End If


                    ElseIf FL_FREE_HAND = False Then
                        'NÃO É FREE HAND

                        'TIPO PAGAMENTO HOUSE
                        If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                            'PAGAMENTO COLLECT

                            Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                        ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                            'PAGAMENTO PREPAID

                            Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                        End If
                    End If
                End If

            ElseIf ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                ' AGENCIAMENTO DE IMPORTACAO AEREO
                ' AGENCIAMENTO DE EXPORTAÇÃO AEREO

                If FL_FREE_HAND = True Then
                    'É FREE HAND

                    'TIPO PAGAMENTO MASTER
                    If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 1 Then
                        'PAGAMENTO COLLECT

                        'TIPO PAGAMENTO HOUSE
                        If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                            'PAGAMENTO COLLECT

                            Return 4 'DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                        ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                            'PAGAMENTO PREPAID

                            Return 2 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE COMPRA

                        End If

                    ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 2 Then
                        'PAGAMENTO PREPAID

                        'TIPO PAGAMENTO HOUSE
                        If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                            'PAGAMENTO COLLECT

                            Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                        ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                            'PAGAMENTO PREPAID

                            Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                        End If

                    End If

                ElseIf FL_FREE_HAND = False Then
                    'NÃO É FREE HAND

                    'TIPO PAGAMENTO MASTER
                    If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 1 Then
                        'PAGAMENTO COLLECT

                        'TIPO PAGAMENTO HOUSE
                        If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                            'PAGAMENTO COLLECT

                            Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                        ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                            'PAGAMENTO PREPAID

                            Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                        End If


                    ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_MASTER") = 2 Then
                        'PAGAMENTO PREPAID

                        'TIPO PAGAMENTO HOUSE
                        If ID_TIPO_PAGAMENTO_HOUSE = 1 Then
                            'PAGAMENTO COLLECT

                            Return 2 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE COMPRA

                        ElseIf ID_TIPO_PAGAMENTO_HOUSE = 2 Then
                            'PAGAMENTO PREPAID

                            Return 4 'DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                        End If


                    End If

                End If


            End If




        End If

    End Function

    Function AtualizaStatusFreteAgenteMaster(ID_BL As Integer, ID_TIPO_PAGAMENTO_MASTER As Integer) As Integer
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        ds = Con.ExecutarQuery("SELECT ISNULL(NR_PROCESSO,0)NR_PROCESSO, ISNULL(ID_SERVICO,0)ID_SERVICO, ISNULL(ID_TIPO_ESTUFAGEM,0)ID_TIPO_ESTUFAGEM, 
ISNULL(FL_FREE_HAND,0)FL_FREE_HAND,
ISNULL((SELECT TOP 1 FL_FREE_HAND FROM TB_BL A WHERE ID_BL_MASTER =" & ID_BL & " ),0)FL_FREE_HAND_HOUSE,
ISNULL((SELECT TOP 1 ID_TIPO_PAGAMENTO FROM TB_BL A WHERE ID_BL_MASTER =" & ID_BL & " ),0)ID_TIPO_PAGAMENTO_HOUSE, 
ISNULL(ID_TIPO_PAGAMENTO,0)ID_TIPO_PAGAMENTO 
FROM TB_BL A WHERE ID_BL = " & ID_BL)
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                ' AGENCIAMENTO DE IMPORTACAO MARITIMA
                ' AGENCIAMENTO DE EXPORTACAO MARITIMA

                If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                    'ESTUFAGEM FCL


                    If ds.Tables(0).Rows(0).Item("FL_FREE_HAND_HOUSE") = True Then
                        'É FREE HAND

                        'TIPO PAGAMENTO MASTER
                        If ID_TIPO_PAGAMENTO_MASTER = 1 Then
                            'PAGAMENTO COLLECT

                            'TIPO PAGAMENTO HOUSE
                            If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                                'PAGAMENTO COLLECT

                                Return 4 ' DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                            ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                                'PAGAMENTO PREPAID

                                Return 2 'DEVOLUÇÃO/ RECEBIMENTO DO FRETE DE COMPRA

                            End If

                        ElseIf ID_TIPO_PAGAMENTO_MASTER = 2 Then
                            'PAGAMENTO PREPAID

                            'TIPO PAGAMENTO HOUSE
                            If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                                'PAGAMENTO COLLECT


                                Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                            ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                                'PAGAMENTO PREPAID

                                Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                            End If

                        End If

                    ElseIf ds.Tables(0).Rows(0).Item("FL_FREE_HAND_HOUSE") = False Then
                        'NÃO É FREE HAND

                        'TIPO PAGAMENTO MASTER
                        If ID_TIPO_PAGAMENTO_MASTER = 1 Then
                            'PAGAMENTO COLLECT

                            'TIPO PAGAMENTO HOUSE
                            If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                                'PAGAMENTO COLLECT

                                Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                            ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                                'PAGAMENTO PREPAID

                                Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                            End If

                        ElseIf ID_TIPO_PAGAMENTO_MASTER = 2 Then
                            'PAGAMENTO PREPAID

                            'TIPO PAGAMENTO HOUSE
                            If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                                'PAGAMENTO COLLECT

                                Return 2 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE COMPRA

                            ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                                'PAGAMENTO PREPAID

                                Return 4 'DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                            End If

                        End If

                    End If

                Else ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2
                    'ESTUFAGEM LCL


                    'TIPO PAGAMENTO HOUSE
                    If ID_TIPO_PAGAMENTO_MASTER = 1 Then
                        'PAGAMENTO COLLECT

                        Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                    ElseIf ID_TIPO_PAGAMENTO_MASTER = 2 Then
                        'PAGAMENTO PREPAID

                        Return 2 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE COMPRA

                    End If


                End If

            ElseIf ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                ' AGENCIAMENTO DE IMPORTACAO AEREO
                ' AGENCIAMENTO DE EXPORTAÇÃO AEREO

                If ds.Tables(0).Rows(0).Item("FL_FREE_HAND_HOUSE") = True Then
                    'É FREE HAND

                    'TIPO PAGAMENTO MASTER
                    If ID_TIPO_PAGAMENTO_MASTER = 1 Then
                        'PAGAMENTO COLLECT

                        'TIPO PAGAMENTO HOUSE
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                            'PAGAMENTO COLLECT

                            Return 4 'DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                            'PAGAMENTO PREPAID

                            Return 2 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE COMPRA

                        End If

                    ElseIf ID_TIPO_PAGAMENTO_MASTER = 2 Then
                        'PAGAMENTO PREPAID

                        'TIPO PAGAMENTO HOUSE
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                            'PAGAMENTO COLLECT

                            Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                            'PAGAMENTO PREPAID

                            Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                        End If

                    End If

                ElseIf ds.Tables(0).Rows(0).Item("FL_FREE_HAND_HOUSE") = False Then
                    'NÃO É FREE HAND

                    'TIPO PAGAMENTO MASTER
                    If ID_TIPO_PAGAMENTO_MASTER = 1 Then
                        'PAGAMENTO COLLECT

                        'TIPO PAGAMENTO HOUSE
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                            'PAGAMENTO COLLECT

                            Return 1 'SEM DEVOLUÇÃO/RECEBIMENTO DE FRETE

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                            'PAGAMENTO PREPAID

                            Return 3 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE VENDA

                        End If


                    ElseIf ID_TIPO_PAGAMENTO_MASTER = 2 Then
                        'PAGAMENTO PREPAID

                        'TIPO PAGAMENTO HOUSE
                        If ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 1 Then
                            'PAGAMENTO COLLECT

                            Return 2 'DEVOLUÇÃO/RECEBIMENTO DO FRETE DE COMPRA

                        ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO_HOUSE") = 2 Then
                            'PAGAMENTO PREPAID

                            Return 4 'DEVOLUÇÃO/RECEBIMENTO DA DIFERENÇA FRETE

                        End If


                    End If

                End If


            End If




        End If

    End Function

End Class
