Public Class CotacaoParceiro

    Public Property IdParceiro As String
    Public Property RazaoParceito As String
    Public Property CpfParceiro As String
    Public Property CnpjParceiro As String
    Public Property StatusParceiro As String
    Public Property TotalCotacaoParceiro As String

    Public Sub New(ByVal IdParceiro As String, ByVal RazaoParceito As String, ByVal CpfParceiro As String, ByVal CnpjParceiro As String, ByVal StatusParceiro As String, ByVal TotalCotacaoParceiro As String)
        Me.IdParceiro = IdParceiro
        Me.RazaoParceito = RazaoParceito
        Me.CpfParceiro = CpfParceiro
        Me.CnpjParceiro = CnpjParceiro
        Me.StatusParceiro = StatusParceiro
        Me.TotalCotacaoParceiro = TotalCotacaoParceiro
    End Sub

End Class

Public Class content
    Public Property content As List(Of CotacaoParceiro)

    Public Sub New(ByVal content As List(Of CotacaoParceiro))
        Me.content = content
    End Sub
End Class

