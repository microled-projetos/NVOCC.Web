Public Class ValorExtenso

    Public Function NumeroToExtenso(ByVal number As Decimal) As String
        Dim cent As Integer
        Try
            ' se for =0 retorna 0 reais
            If number = 0 Then
                Return "Zero Reais"
            End If
            ' Verifica a parte decimal, ou seja, os centavos
            cent = Decimal.Round((number - Int(number)) * 100, MidpointRounding.ToEven)
            ' Verifica apenas a parte inteira
            number = Int(number)
            ' Caso existam centavos
            If cent > 0 Then
                ' Caso seja 1 não coloca "Reais" mas sim "Real"
                If number = 1 Then
                    Return "Um Real e " + getDecimal(cent) + "centavos"
                    ' Caso o valor seja inferior a 1 Real
                ElseIf number = 0 Then
                    Return getDecimal(cent) + "centavos"
                Else
                    Return getInteger(number) + "Reais e " + getDecimal(cent) + "centavos"
                End If
            Else
                ' Caso seja 1 não coloca "Reais" mas sim "Real"
                If number = 1 Then
                    Return "Um Real"
                Else
                    Return getInteger(number) + "Reais"
                End If
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
    ''' <summary>
    ''' Função auxiliar - Parte decimal a converter
    ''' </summary>
    ''' <param name="number">Parte decimal a converter</param>
    Public Function getDecimal(ByVal number As Byte) As String
        Try
            Select Case number
                Case 0
                    Return ""
                Case 1 To 19
                    Dim strArray() As String =
                       {"Um", "Dois", "Três", "Quatro", "Cinco", "Seis",
                        "Sete", "Oito", "Nove", "Dez", "Onze",
                        "Doze", "Treze", "Quatorze", "Quinze",
                        "Dezesseis", "Dezessete", "Dezoito", "Dezenove"}
                    Return strArray(number - 1) + " "
                Case 20 To 99
                    Dim strArray() As String =
                        {"Vinte", "Trinta", "Quarenta", "Cinquenta",
                        "Sessenta", "Setenta", "Oitenta", "Noventa"}
                    If (number Mod 10) = 0 Then
                        Return strArray(number \ 10 - 2) + " "
                    Else
                        Return strArray(number \ 10 - 2) + " e " + getDecimal(number Mod 10) + " "
                    End If
                Case Else
                    Return ""
            End Select
        Catch ex As Exception
            Return ""
        End Try
    End Function
    ''' <summary>
    ''' Função auxiliar - Parte inteira a converter
    ''' </summary>
    ''' <param name="number">Parte inteira a converter</param>
    Public Function getInteger(ByVal number As Decimal) As String
        Try
            number = Int(number)
            Select Case number
                Case Is < 0
                    Return "-" & getInteger(-number)
                Case 0
                    Return ""
                Case 1 To 19
                    Dim strArray() As String =
                        {"Um", "Dois", "Três", "Quatro", "Cinco", "Seis",
                        "Sete", "Oito", "Nove", "Dez", "Onze", "Doze",
                        "Treze", "Quatorze", "Quinze", "Dezesseis",
                        "Dezessete", "Dezoito", "Dezenove"}
                    Return strArray(number - 1) + " "
                Case 20 To 99
                    Dim strArray() As String =
                        {"Vinte", "Trinta", "Quarenta", "Cinquenta",
                        "Sessenta", "Setenta", "Oitenta", "Noventa"}
                    If (number Mod 10) = 0 Then
                        Return strArray(number \ 10 - 2)
                    Else
                        Return strArray(number \ 10 - 2) + " e " + getInteger(number Mod 10)
                    End If
                Case 100
                    Return "Cem"
                Case 101 To 999
                    Dim strArray() As String =
                           {"Cento", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos",
                           "Seiscentos", "Setecentos", "Oitocentos", "Novecentos"}
                    If (number Mod 100) = 0 Then
                        Return strArray(number \ 100 - 1) + " "
                    Else
                        Return strArray(number \ 100 - 1) + " e " + getInteger(number Mod 100)
                    End If
                Case 1000 To 1999
                    Select Case (number Mod 1000)
                        Case 0
                            Return "Mil"
                        Case Is <= 100
                            Return "Mil e " + getInteger(number Mod 1000)
                        Case Else
                            Return "Mil, " + getInteger(number Mod 1000)
                    End Select
                Case 2000 To 999999
                    Select Case (number Mod 1000)
                        Case 0
                            Return getInteger(number \ 1000) & "Mil"
                        Case Is <= 100
                            Return getInteger(number \ 1000) & "Mil e " & getInteger(number Mod 1000)
                        Case Else
                            Return getInteger(number \ 1000) & "Mil, " & getInteger(number Mod 1000)
                    End Select
                Case 1000000 To 1999999
                    Select Case (number Mod 1000000)
                        Case 0
                            Return "Um Milhão"
                        Case Is <= 100
                            Return getInteger(number \ 1000000) + "Milhão e " & getInteger(number Mod 1000000)
                        Case Else
                            Return getInteger(number \ 1000000) + "Milhão, " & getInteger(number Mod 1000000)
                    End Select
                Case 2000000 To 999999999
                    Select Case (number Mod 1000000)
                        Case 0
                            Return getInteger(number \ 1000000) + " Milhões"
                        Case Is <= 100
                            Return getInteger(number \ 1000000) + "Milhões e " & getInteger(number Mod 1000000)
                        Case Else
                            Return getInteger(number \ 1000000) + "Milhões, " & getInteger(number Mod 1000000)
                    End Select
                Case 1000000000 To 1999999999
                    Select Case (number Mod 1000000000)
                        Case 0
                            Return "Um Bilhão"
                        Case Is <= 100
                            Return getInteger(number \ 1000000000) + "Bilhão e " + getInteger(number Mod 1000000000)
                        Case Else
                            Return getInteger(number \ 1000000000) + "Bilhão, " + getInteger(number Mod 1000000000)
                    End Select
                Case Else
                    Select Case (number Mod 1000000000)
                        Case 0
                            Return getInteger(number \ 1000000000) + " Bilhões"
                        Case Is <= 100
                            Return getInteger(number \ 1000000000) + "Bilhões e " + getInteger(number Mod 1000000000)
                        Case Else
                            Return getInteger(number \ 1000000000) + "Bilhões, " + getInteger(number Mod 1000000000)
                    End Select
            End Select
        Catch ex As Exception
            Return ""
        End Try
    End Function



End Class
