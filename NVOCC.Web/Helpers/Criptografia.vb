Imports System.Security.Cryptography
Imports System.Text
Imports System
Imports System.Text.StringBuilder
Public Class Criptografia
    Public Function CriptografarSenha(pass)
        If pass = "" Then
            Return ""
        Else
            Using hasher As MD5 = MD5.Create()
                Dim dbytes As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(pass))

                Dim sBuilder As New StringBuilder()

                For n As Integer = 0 To dbytes.Length - 1
                    sBuilder.Append(dbytes(n).ToString("X2"))
                Next n
                Return sBuilder.ToString()
            End Using
        End If
    End Function

End Class