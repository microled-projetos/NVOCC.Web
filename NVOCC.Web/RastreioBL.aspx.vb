Imports Newtonsoft.Json

Public Class RastreioBL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        nr_bl.Text = Session("NR_BL")
        Dim Json As String = Session("TRAKING_BL")
        Dim consignee As Transporte = JsonConvert.DeserializeObject(Of Transporte)(Json)
        Dim city As String = consignee.consignee.city
        pais_procedencia.Text = city



    End Sub
    'Private Function DeserializarNewtonsoft() As Consignee
    '    Dim Json = Session("TRAKING_BL")
    '    Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of Consignee)(Json)
    'End Function

End Class
Public Class Consignee
    Public Property name As String
    Public Property code As String
    Public Property region As String
    Public Property city As String
    Public Property address As String
    Public Property address_complement As String
    Public Property zip_code As String
    Public Property number As String
    Public Property phone As String
    Public Property email As String
    Public Property activity As String
    Public Property legal_nature As String
    Public Property type As String
    Public Property type_title As String
End Class

Public Class Transporte
    Public Property consignee As Consignee
End Class
