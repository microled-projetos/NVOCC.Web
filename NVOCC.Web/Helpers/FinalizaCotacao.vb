Public Class FinalizaCotacao

    Sub Finalizar()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO FROM TB_COTACAO WHERE ID_COTACAO IN (select ID_COTACAO from tb_bl where grau='C' and ID_BL in (select ID_BL from TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER where B.DT_CANCELAMENTO is null)) AND ID_STATUS_COTACAO IN (9,15)")

        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = 12 WHERE ID_COTACAO = " & linha.Item("ID_COTACAO"))
            Next

        End If
        Con.Fechar()
    End Sub

    Function TaxaBloqueada(ID As String, Tipo As String) As Boolean

        Dim Con As New Conexao_sql
        Con.Conectar()

        'SELECT D.ID_STATUS_COTACAO, B.ID_BL_TAXA, C.ID_COTACAO_TAXA, F.ID_CONTA_PAGAR_RECEBER_ITENS, F.ID_CONTA_PAGAR_RECEBER, E.DT_CANCELAMENTO
        'FROM TB_BL A 
        'INNER JOIN TB_BL_TAXA B ON A.ID_BL = B.ID_BL
        'INNER JOIN TB_COTACAO_TAXA C ON B.ID_COTACAO_TAXA = C.ID_COTACAO_TAXA
        'INNER JOIN TB_COTACAO D ON D.ID_COTACAO = C.ID_COTACAO AND A.ID_COTACAO = D.ID_COTACAO
        'INNER JOIN TB_CONTA_PAGAR_RECEBER_ITENS F ON F.ID_BL_TAXA = B.ID_BL_TAXA
        'INNER JOIN TB_CONTA_PAGAR_RECEBER E ON F.ID_CONTA_PAGAR_RECEBER = E.ID_CONTA_PAGAR_RECEBER
        'WHERE E.DT_CANCELAMENTO IS NULL AND D.ID_STATUS_COTACAO NOT IN (9,12,15) 

        Dim ds As DataSet
        If Tipo = "COTACAO" Then
            ds = Con.ExecutarQuery("SELECT * FROM View_Taxa_Bloqueada WHERE ID_COTACAO_TAXA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else

                'Dim ID_BASE_CALCULO_TAXA As String = 0
                'Dim ID_ITEM_DESPESA As String = 0
                'Dim ID_COTACAO As String = 0
                'Dim ID_BL As String = 0

                'ds = Con.ExecutarQuery("SELECT ID_BASE_CALCULO_TAXA,ID_ITEM_DESPESA,ID_COTACAO, (SELECT B.ID_BL FROM TB_BL B WHERE B.ID_COTACAO = ID_COTACAO AND GRAU = 'C' )ID_BL FROM TB_COTACAO_TAXA WHERE ID_COTACAO_TAXA =" & ID)
                'If ds.Tables(0).Rows.Count > 0 Then
                '    ID_ITEM_DESPESA = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                '    ID_BASE_CALCULO_TAXA = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                '    ID_COTACAO = ds.Tables(0).Rows(0).Item("ID_COTACAO")
                '    ID_BL = ds.Tables(0).Rows(0).Item("ID_BL")
                'End If

                '    If ID_ITEM_DESPESA <> "" And ID_BASE_CALCULO_TAXA <> "" And ID_BL <> "" And ID_COTACAO <> "" Then

                '        Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT A.ID_BL_TAXA,isnull(B.ID_CONTA_PAGAR_RECEBER,0)ID_CONTA_PAGAR_RECEBER,C.DT_CANCELAMENTO,isnull(A.ID_BL_MASTER,0)ID_BL_MASTER 
                'FROM TB_BL_TAXA A
                'LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON A.ID_BL_TAXA= B.ID_BL_TAXA
                'LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER  
                'WHERE ISNULL(CD_ORIGEM_INF,'COTA') = 'COTA' AND A.ID_ITEM_DESPESA = " & ID_ITEM_DESPESA & " AND isnull(A.ID_COTACAO_TAXA,0) = 0 and A.ID_BASE_CALCULO_TAXA = " & ID_BASE_CALCULO_TAXA & " AND A.ID_BL = " & ID_BL)

                '        If dsProcesso.Tables(0).Rows.Count > 0 Then


                '            For Each linha As DataRow In dsProcesso.Tables(0).Rows

                '                If linha.Item("ID_BL_MASTER").ToString = 0 Then


                '                    If linha.Item("ID_CONTA_PAGAR_RECEBER").ToString = 0 Or Not IsDBNull(linha.Item("DT_CANCELAMENTO")) Then


                '                        Con.ExecutarQuery("DELETE FROM TB_BL_TAXA WHERE ID_BL_TAXA = " & linha.Item("ID_BL_TAXA") & " AND ID_BL =" & ID_BL & " AND ID_BL_TAXA NOT IN (SELECT  isnull(ID_BL_TAXA,0)ID_BL_TAXA FROM TB_CONTA_PAGAR_RECEBER_ITENS A INNER JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER =  B.ID_CONTA_PAGAR_RECEBER WHERE B.DT_CANCELAMENTO IS NULL) AND ID_BL_MASTER IS NULL AND ID_BL_TAXA_MASTER IS NULL")


                '                    End If
                '                End If

                '            Next



                '        End If

                '    End If

                ds = Con.ExecutarQuery("SELECT * FROM View_Verifica_Taxa_Cotacao WHERE ID_COTACAO_TAXA = " & ID)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else

                    Return False
                End If

            End If


        ElseIf Tipo = "BL" Then
            ds = Con.ExecutarQuery("SELECT * FROM View_Taxa_Bloqueada WHERE ID_BL_TAXA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End If


    End Function
End Class
