Public Class RastreioService

    Public Function IniciarRastreio(ByVal blNumber As String, ByVal cnpjConsignatario As String, ByVal email As String, ByVal agenciaMaritima As String)
        Dim request = TryCast(System.Net.WebRequest.Create("https://api.logcomex.io/api/v3/rastreamento/maritimo/novo"), System.Net.HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        request.Headers.Add("x-api-key", "7b86d436a5d89ac4c8be11553b432bad")

        Using writer = New System.IO.StreamWriter(request.GetRequestStream())
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes("{
            \""bl_number\"": \""BLEXAMPLE001\"",
            \""reference\"": \""exRef\"",
            \""consignee_cnpj\"": \""74724727328321\"",
            \""emails\"": \""user@logcomex.com;\"",
            \""shipowner\"": 0
            }
            }")
            request.ContentLength = byteArray.Length
            writer.Write(byteArray)
            writer.Close()
        End Using
        Dim responseContent As String
        Using response = TryCast(request.GetResponse(), System.Net.HttpWebResponse)
            Using reader = New System.IO.StreamReader(response.GetResponseStream())
                responseContent = reader.ReadToEnd()
            End Using
        End Using
        Return responseContent

    End Function

End Class
