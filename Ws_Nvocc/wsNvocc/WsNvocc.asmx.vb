Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WsNvocc
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function IntegraNFePrefeitura(ByVal RPS As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String) As String
        Return "00000000RSP NAO ENCONTRADA NO BANCO DE DADOS"
    End Function

End Class