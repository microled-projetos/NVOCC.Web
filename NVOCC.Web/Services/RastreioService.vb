Imports System.Net.Http
Imports System.Threading.Tasks

Public Class RastreioService


    Public Async Function PostAsyncBl(ByVal blNumber As String, ByVal cnpjConsignatario As String, ByVal email As String, ByVal agenciaMaritima As String) As Task

            Dim jsonString As String
            Dim client As HttpClient
            With client
                .BaseAddress = New Uri("https://api.logcomex.io/api/v3/rastreamento/maritimo/novo")
                .DefaultRequestHeaders.Accept.Clear()
                '.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))
                .DefaultRequestHeaders.Add("x-api-key", "7b86d436a5d89ac4c8be11553b432bad")
            End With
            jsonString = "{
            ""bl_number"": """ & blNumber & """,
            ""reference"": """",
            ""consignee_cnpj"": """ & cnpjConsignatario & """,
            ""emails"": """ & email & ";"",
            ""shipowner"": 185
            }
            }"
            Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync("", content)

        Dim result As String = Await response.Content.ReadAsStringAsync()


    End Function

End Class
