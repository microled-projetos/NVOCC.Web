
Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim RotinaDoc As New RotinaDeletaDoc
        RotinaDoc.DeletaArquivos()
    End Sub


End Class