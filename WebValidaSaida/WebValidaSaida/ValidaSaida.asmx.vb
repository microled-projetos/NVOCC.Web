Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Xml

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ValidaSaida
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function ValidaSaida_cntr(ByVal xml_conteiner As String) As String

        Dim retorno As String
        Dim ValidaOBJ As New Valida

        retorno = ValidaOBJ.ValidaSaida_cntr(xml_conteiner.ToString)
        Return retorno
    End Function


    <WebMethod()> _
    Public Function ValidaSaida_CS(ByVal xml_lote As String) As String

        Dim retorno As String
        Dim ValidaOBJ As New Valida

        retorno = ValidaOBJ.ValidaSaida_Cs(xml_lote.ToString)
        Return retorno
    End Function



    <WebMethod()>
    Public Function Lote_Rps() As String

        Dim sSql As String
        Dim retorno As String
        Dim RSAux As New ADODB.Recordset


        sSql = "SELECT SGIPA.SEQ_LOTE_NFSE.NEXTVAL as QUAL FROM DUAL "

        PRSet(RSAux, sSql.ToString())
        retorno = RSAux.Fields("QUAL").Value


        Return retorno
    End Function

End Class