Public Class ValidaCNPJ
    Public Shared Function Validar(ByVal cnpj As String) As Boolean
        If String.IsNullOrEmpty(cnpj) Then Return False
        Dim multiplicador1 As Integer() = New Integer(11) {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim multiplicador2 As Integer() = New Integer(12) {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim soma As Integer
        Dim resto As Integer
        Dim digito As String
        Dim tempCnpj As String
        cnpj = cnpj.Trim()
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "")
        If cnpj.Length <> 14 Then Return False
        tempCnpj = cnpj.Substring(0, 12)
        soma = 0

        For i As Integer = 0 To 12 - 1
            soma += Integer.Parse(tempCnpj(i).ToString()) * multiplicador1(i)
        Next

        resto = (soma Mod 11)

        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = resto.ToString()
        tempCnpj = tempCnpj & digito
        soma = 0

        For i As Integer = 0 To 13 - 1
            soma += Integer.Parse(tempCnpj(i).ToString()) * multiplicador2(i)
        Next

        resto = (soma Mod 11)

        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = digito & resto.ToString()
        Return cnpj.EndsWith(digito)
    End Function
End Class
