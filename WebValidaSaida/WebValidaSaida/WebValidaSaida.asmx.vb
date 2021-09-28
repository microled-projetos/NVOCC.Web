Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Xml

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WebValidaSaida
    Inherits System.Web.Services.WebService
    <WebMethod()> _
    Public Function ValidaSaida_cntr(xml_conteiner As String) As String

        Dim xmlDoc As New XmlDocument
        Dim retorno_sql As String
        Dim str_dados(5) As String

        retorno_sql = ""
        ' xmlDoc.LoadXml(xml_conteiner)

        xmlDoc.Load("d:\testesaida.xml")
        Dim node1 As XmlNode = xmlDoc.SelectSingleNode("//registros")

        If node1 IsNot Nothing Then
            For Each No As XmlNode In node1.ChildNodes
                If (No.ChildNodes IsNot Nothing) Then
                    For Each subNode As XmlNode In No.ChildNodes
                        Select Case LCase(subNode.Name)
                            Case "numeroconteiner"
                                str_dados(1) = subNode.InnerText
                            Case "tipoteste"
                                str_dados(2) = subNode.InnerText
                        End Select
                    Next subNode
                    Dim ValidaOBJ As New Valida
                    retorno_sql = ValidaOBJ.ValidaSaida_cntr(str_dados(0).ToString, "", str_dados(1).ToString)
                    For I = 1 To 2
                        str_dados(I) = ""
                    Next
                End If
            Next
        End If
        Return "OK"
    End Function

    <WebMethod()> _
    Public Function ValidaSaida_CS(xml_lote As String) As String

        Dim xmlDoc As New XmlDocument
        Dim retorno_sql As String
        Dim str_dados(5) As String

        retorno_sql = ""
        xmlDoc.LoadXml(xml_lote)

        Dim node1 As XmlNode = xmlDoc.SelectSingleNode("//conteiner")

        If node1 IsNot Nothing Then
            For Each No As XmlNode In node1.ChildNodes
                If (No.ChildNodes IsNot Nothing) Then
                    For Each subNode As XmlNode In No.ChildNodes
                        Select Case LCase(subNode.Name)
                            Case "numeroconteiner"
                                str_dados(1) = subNode.InnerText
                            Case "tipoteste"
                                str_dados(2) = subNode.InnerText
                        End Select
                    Next subNode
                    Dim ValidaOBJ As New Valida
                    retorno_sql = ValidaOBJ.ValidaSaida_Cs(str_dados(0).ToString, str_dados(1).ToString, str_dados(2).ToString, 1)
                    For I = 1 To 2
                        str_dados(I) = ""
                    Next
                End If
            Next
        End If
        Return "OK"
    End Function


End Class