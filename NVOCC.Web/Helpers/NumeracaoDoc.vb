Public Class NumeracaoDoc
    Public Function Numerar(Tipo_Doc As Integer) As String

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_DEBITO,NR_RECIBO,NR_RPS FROM TB_NUMERACAO")
        Dim numero As Integer
        Dim numeroFinal As String = ""
        If ds.Tables(0).Rows.Count > 0 Then
            If Tipo_Doc = 1 Then
                'NR_NOTA_DEBITO
                numero = ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")
                numero = numero + 1
                numeroFinal = numero.ToString.PadLeft(6, "0")
            ElseIf Tipo_Doc = 2 Then
                'NR_RECIBO
                numero = ds.Tables(0).Rows(0).Item("NR_RECIBO")
                numero = numero + 1
                numeroFinal = numero.ToString.PadLeft(6, "0")
            ElseIf Tipo_Doc = 3 Then
                'NR_RPS
                numero = ds.Tables(0).Rows(0).Item("NR_RPS")
                numero = numero + 1
                numeroFinal = numero.ToString.PadLeft(6, "0")

            End If

        End If
        Return numeroFinal
    End Function
End Class
