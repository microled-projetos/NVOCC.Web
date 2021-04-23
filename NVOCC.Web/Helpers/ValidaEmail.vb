Public Class ValidaEmail
    Public Shared Function Validar(ByVal email As String) As Boolean
        Dim regExpEmail As Regex = New Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$")
        Dim match As Match = regExpEmail.Match(email)
        Return match.Success
    End Function
End Class
