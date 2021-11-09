Imports System.Net.Http
Imports System.Threading.Tasks
Imports RestSharp
Imports System.ServiceModel
Imports System.Runtime.Serialization
Imports Newtonsoft.Json



Public Class RastreioService

    'metodo descontinuado
    Public Async Function PostAsyncBl(ByVal blNumber As String, ByVal cnpjConsignatario As String, ByVal email As String, ByVal agenciaMaritima As String) As Task
        Dim jsonString As String
        Dim client As HttpClient
        With client
            .BaseAddress = New Uri("https://api.logcomex.io/api/v3/rastreamento/maritimo/novo")
            .DefaultRequestHeaders.Accept.Clear()
            .DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))
            .DefaultRequestHeaders.Add("x-api-key", "7b86d436a5d89ac4c8be11553b432bad")
        End With

        jsonString = "{            
            ""bl_number"": """ & blNumber & """,
            ""reference"": ANVB """",
            ""consignee_cnpj"": """ & cnpjConsignatario & """,
            ""emails"": """ & email & ";"",
            ""shipowner"": 185
            }
            }"
        Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync("", content)

        Dim result As String = Await response.Content.ReadAsStringAsync()

    End Function

    Public Function GetDadosJsonBL(ByVal bl As String, ByVal cnpj As String, ByVal email As String, ByVal agencia As String)


        Dim url = "https://api.logcomex.io/api/v3/rastreamento/maritimo/novo"
        Dim rest As RestClient = New RestClient
        Dim Ret As String = ""
        Dim Ref As String = "ABCTEste"

        rest.BaseUrl = New Uri(url)
        rest.Timeout = 480000

        cnpj = ""

        Dim request = New RestRequest(Method.POST)

        Dim Jstr = New blTracking
        With Jstr
            .bl_number = bl
            .reference = Ref
            .consignee_cnpj = cnpj
            .emails = email & ";"
            .shipowner = 0
        End With

        request.AddHeader("x-api-key", "7b86d436a5d89ac4c8be11553b432bad")
        request.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader)
        request.AddJsonBody(Jstr)

        Dim response = rest.Execute(request)


        Dim Content = response.Content

        Dim obj As blToken = JsonConvert.DeserializeObject(Of blToken)(Content)

        Return obj.token

    End Function

    Public Function AtualizarRastreamentoLogComex(Token As String)

        Dim Url As String = "https://api.logcomex.io/api/v3/"
        Dim rest As RestClient = New RestClient

        rest.BaseUrl = New Uri(Url + "rastreamento/" & Token)
        rest.Timeout = 480000

        Dim request = New RestRequest(Method.GET)

        request.AddHeader("x-api-key", "7b86d436a5d89ac4c8be11553b432bad")
        request.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader)

        Dim response = rest.Execute(request)

        Dim Content = response.Content

        Return Content

    End Function

    Sub trackingbl(idBl As String)

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim cnpj As String = ""
        Dim NR_BL As String = ""
        Dim token_bl As String = ""



        If Not String.IsNullOrEmpty(idBl) Then

            ds = Con.ExecutarQuery(" select NR_BL, isnull(BL_TOKEN, '') as BL_TOKEN from tb_bl where ID_BL =  " & idBl)

            If ds.Tables(0).Rows.Count > 0 Then
                NR_BL = ds.Tables(0).Rows(0).Item("NR_BL")
                token_bl = ds.Tables(0).Rows(0).Item("BL_TOKEN")


                If String.IsNullOrEmpty(token_bl) Then
                    ds = Con.ExecutarQuery("select CNPJ from TB_PARCEIRO where ID_PARCEIRO IN (select ID_PARCEIRO_TRANSPORTADOR from tb_bl where ID_BL =" & idBl & ") ")
                    If ds.Tables(0).Rows.Count > 0 Then
                        cnpj = ds.Tables(0).Rows(0).Item("CNPJ")
                    End If

                    Dim tokenAPi = GetDadosJsonBL(NR_BL, cnpj, "andre.rodrigues@abainfra.com.br", "185")

                    Dim token_bl_format As String = tokenAPi

                    If token_bl_format <> Nothing Then

                        Con.ExecutarQuery(" UPDATE TB_BL SET BL_TOKEN = '" & token_bl_format & "' WHERE ID_BL = " & idBl)

                        Dim trackingBL As String = AtualizarRastreamentoLogComex(token_bl_format)

                        Con.ExecutarQuery("  UPDATE TB_BL SET TRAKING_BL = '" & trackingBL.ToString().Replace("'", "") & "' where ID_BL =   " & idBl)

                    End If
                Else
                    Dim trackingBL As String = AtualizarRastreamentoLogComex(token_bl)

                    Con.ExecutarQuery("  UPDATE TB_BL SET TRAKING_BL = '" & trackingBL.ToString().Replace("'", "") & "' where ID_BL =   " & idBl)
                End If

            End If

        End If
    End Sub


End Class
Public Class blTracking
    Public Property bl_number As String
    Public Property reference As String
    Public Property consignee_cnpj As String
    Public Property emails As String
    Public Property shipowner As Integer

End Class

Public Class blToken
    Public Property token As String
End Class
