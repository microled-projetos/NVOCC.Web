Public Class NumeracaoDoc
    Public Function Numerar(Tipo_Doc As Integer) As String

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim numero As Integer
        Dim numeroFinal As String = ""

        If Tipo_Doc = 1 Then
            'NR_NOTA_DEBITO
            ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Nota_Debito NR_NOTA_DEBITO")
            numero = ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")
            numeroFinal = numero.ToString.PadLeft(6, "0")
        ElseIf Tipo_Doc = 2 Then
            'NR_RECIBO
            ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Recibo NR_RECIBO")
            numero = ds.Tables(0).Rows(0).Item("NR_RECIBO")
            numeroFinal = numero.ToString.PadLeft(6, "0")
        ElseIf Tipo_Doc = 3 Then
            'NR_RPS
            ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_RPS NR_RPS")
            numero = ds.Tables(0).Rows(0).Item("NR_RPS")
            numeroFinal = numero.ToString.PadLeft(6, "0")

        End If


        Return numeroFinal
    End Function
End Class
