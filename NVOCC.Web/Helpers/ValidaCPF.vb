Public Class ValidaCPF
    Public Shared Function Validar(ByVal CPF As String) As Boolean
        Dim mt1 As Integer() = New Integer(8) {10, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim mt2 As Integer() = New Integer(9) {11, 10, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim TempCPF As String
        Dim Digito As String
        Dim soma As Integer
        Dim resto As Integer
        CPF = CPF.Trim()
        CPF = CPF.Replace(".", "").Replace("-", "")
        If CPF.Length <> 11 Then Return False

        Select Case CPF
            Case "11111111111"
                Return False
            Case "00000000000"
                Return False
            Case "2222222222"
                Return False
            Case "33333333333"
                Return False
            Case "44444444444"
                Return False
            Case "55555555555"
                Return False
            Case "66666666666"
                Return False
            Case "77777777777"
                Return False
            Case "88888888888"
                Return False
            Case "99999999999"
                Return False
        End Select

        TempCPF = CPF.Substring(0, 9)
        soma = 0

        For i As Integer = 0 To 9 - 1
            soma += Integer.Parse(TempCPF(i).ToString()) * mt1(i)
        Next

        resto = soma Mod 11

        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        Digito = resto.ToString()
        TempCPF = TempCPF & Digito
        soma = 0

        For i As Integer = 0 To 10 - 1
            soma += Integer.Parse(TempCPF(i).ToString()) * mt2(i)
        Next

        resto = soma Mod 11

        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        Digito = Digito & resto.ToString()
        Return CPF.EndsWith(Digito)
    End Function
End Class
