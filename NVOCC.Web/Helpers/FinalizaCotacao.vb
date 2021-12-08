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

    Function TaxaBloqueada(ID As String, Tipo As String, Optional CD_PR As String = "") As Boolean

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

            If CD_PR <> "" Then
                ds = Con.ExecutarQuery("SELECT * FROM View_Taxa_Bloqueada WHERE CD_PR= '" & CD_PR & "' AND ID_COTACAO_TAXA = " & ID)
            Else
                ds = Con.ExecutarQuery("SELECT * FROM View_Taxa_Bloqueada WHERE ID_COTACAO_TAXA = " & ID)
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
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
