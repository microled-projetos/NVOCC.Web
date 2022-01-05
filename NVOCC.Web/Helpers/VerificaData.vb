Imports System.Globalization

Public Class VerificaData
    Function ValidaData(data As String) As Boolean
        data = data & " 00:00"
        Dim validador As Regex = New Regex("^([1-9]|([012][0-9])|(3[01]))\/([0]{0,1}[1-9]|1[012])\/\d\d\d\d [012]{0,1}[0-9]:[0-6][0-9]$")
        Dim match As Match = validador.Match(data)
        Return match.Success
    End Function
End Class
