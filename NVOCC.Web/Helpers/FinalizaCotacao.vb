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
End Class
