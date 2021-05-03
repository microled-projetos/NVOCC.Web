Imports System.Net.Http
Imports System.Threading.Tasks

Public Class RastreioService


    Private Async Function PostAsyncBl(ByVal jsonString As String) As Task

        Dim client As HttpClient
        With client
            .BaseAddress = New Uri("https://api.logcomex.io/api/v3/rastreamento/maritimo/novo")
            .DefaultRequestHeaders.Accept.Clear()
            '.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))
            .DefaultRequestHeaders.Add("x-api-key", "7b86d436a5d89ac4c8be11553b432bad")
        End With
        jsonString = "{
            \""bl_number\"": \""BLEXAMPLE001\"",
            \""reference\"": \""exRef\"",
            \""consignee_cnpj\"": \""74724727328321\"",
            \""emails\"": \""user@logcomex.com;\"",
            \""shipowner\"": 0
            }
            }"
        Dim content As New StringContent(jsonString, Encoding.UTF8, "application/json")
        Dim response As HttpResponseMessage = Await client.PostAsync("", content)

        Dim result As String = Await response.Content.ReadAsStringAsync()



    End Function

End Class
