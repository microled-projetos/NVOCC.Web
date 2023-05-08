Namespace Classes

    Public Class Excel
        Public Shared Sub exportaExcelDuplo(ByVal sql1 As String, ByVal sql2 As String, ByVal sNomeArquivo As String, Optional camposTextos As List(Of String) = Nothing)

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim oDt As DataTable = Con.List(sql1)
            Dim colunas As New List(Of String)
            Dim linhas As New List(Of String)

            For Each col As DataColumn In oDt.Columns
                colunas.Add(col.ColumnName.Replace(";", ","))
            Next
            linhas.Add(String.Join(";", colunas.ToArray()))
            Dim linhaProcessada As List(Of String)
            Dim campo As String
            For Each linha As DataRow In oDt.Rows
                linhaProcessada = New List(Of String)
                For Each col As DataColumn In oDt.Columns
                    campo = linha(col).ToString().Replace(vbCr, " ").Replace(vbCrLf, " ")
                    campo = campo.Replace(vbLf, " ")
                    campo = campo.Replace(";", ",")
                    campo = tiraCaracEsp(campo.ToString())

                    If camposTextos IsNot Nothing Then
                        If camposTextos.Contains(col.ColumnName) Then
                            campo = "=""" & campo & """"
                        End If
                    End If
                    linhaProcessada.Add(campo)
                Next
                linhas.Add(String.Join(";", linhaProcessada.ToArray))
            Next



            oDt = Con.List(sql2)
            For Each linha As DataRow In oDt.Rows
                linhaProcessada = New List(Of String)
                For Each col As DataColumn In oDt.Columns
                    campo = linha(col).ToString().Replace(vbCr, " ").Replace(vbCrLf, " ")
                    campo = campo.Replace(vbLf, " ")
                    campo = campo.Replace(";", ",")
                    campo = tiraCaracEsp(campo.ToString())

                    If camposTextos IsNot Nothing Then
                        If camposTextos.Contains(col.ColumnName) Then
                            campo = "=""" & campo & """"
                        End If
                    End If
                    linhaProcessada.Add(campo)
                Next
                linhas.Add(String.Join(";", linhaProcessada.ToArray))
            Next

            Dim construtor As New StringBuilder
            construtor.Append(String.Join(vbCrLf, linhas.ToArray))


            Dim oResponse As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            oResponse.Clear()
            oResponse.ContentEncoding = Encoding.GetEncoding(28591)
            oResponse.AddHeader("Content-Disposition", "attachment;filename=" & sNomeArquivo & ".csv")
            oResponse.ContentType = "text/csv"
            oResponse.Write(construtor)
            oResponse.End()

        End Sub

        Public Shared Sub exportaExcel(ByVal sql As String, ByVal sNomeArquivo As String, Optional camposTextos As List(Of String) = Nothing)

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim oDt As DataTable = Con.List(sql)
            Dim colunas As New List(Of String)
            Dim linhas As New List(Of String)

            For Each col As DataColumn In oDt.Columns
                colunas.Add(col.ColumnName.Replace(";", ","))
                Dim x As String = col.Caption
            Next
            linhas.Add(String.Join(";", colunas.ToArray()))
            Dim linhaProcessada As List(Of String)
            Dim campo As String
            For Each linha As DataRow In oDt.Rows
                linhaProcessada = New List(Of String)
                For Each col As DataColumn In oDt.Columns
                    campo = linha(col).ToString().Replace(vbCr, " ").Replace(vbCrLf, " ")
                    campo = campo.Replace(vbLf, " ")
                    campo = campo.Replace(";", ",")
                    campo = tiraCaracEsp(campo.ToString())

                    If camposTextos IsNot Nothing Then
                        If camposTextos.Contains(col.ColumnName) Then
                            campo = "=""" & campo & """"
                        End If
                    End If
                    linhaProcessada.Add(campo)
                Next
                linhas.Add(String.Join(";", linhaProcessada.ToArray))
            Next

            Dim construtor As New StringBuilder
            construtor.Append(String.Join(vbCrLf, linhas.ToArray))

            Dim oResponse As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            oResponse.Clear()
            oResponse.ContentEncoding = Encoding.GetEncoding(28591)
            oResponse.AddHeader("Content-Disposition", "attachment;filename=" & sNomeArquivo & ".csv")
            oResponse.ContentType = "text/csv"
            oResponse.Write(construtor)
            oResponse.End()

        End Sub

        Public Shared Function tiraCaracEsp(ByVal Txt As String) As String

            Txt = Replace(Txt, "&", "E")

            Txt = Replace(Txt, "ƒ", "C")
            Txt = Replace(Txt, "¨", "''")

            Txt = Replace(Txt, "ã", "a")
            Txt = Replace(Txt, "â", "a")
            Txt = Replace(Txt, "á", "a")
            Txt = Replace(Txt, "à", "a")
            Txt = Replace(Txt, "é", "e")
            Txt = Replace(Txt, "è", "e")
            Txt = Replace(Txt, "ê", "e")
            Txt = Replace(Txt, "í", "i")
            Txt = Replace(Txt, "ì", "i")
            Txt = Replace(Txt, "î", "i")
            Txt = Replace(Txt, "õ", "o")
            Txt = Replace(Txt, "ó", "o")
            Txt = Replace(Txt, "ò", "o")
            Txt = Replace(Txt, "ô", "o")
            Txt = Replace(Txt, "ú", "u")
            Txt = Replace(Txt, "ù", "u")
            Txt = Replace(Txt, "û", "u")
            Txt = Replace(Txt, "ç", "c")

            Txt = Replace(Txt, "Ã", "A")
            Txt = Replace(Txt, "Â", "A")
            Txt = Replace(Txt, "Á", "A")
            Txt = Replace(Txt, "À", "A")
            Txt = Replace(Txt, "É", "E")
            Txt = Replace(Txt, "È", "E")
            Txt = Replace(Txt, "Ê", "E")
            Txt = Replace(Txt, "Í", "I")
            Txt = Replace(Txt, "Ì", "I")
            Txt = Replace(Txt, "Î", "I")
            Txt = Replace(Txt, "Õ", "O")
            Txt = Replace(Txt, "Ó", "O")
            Txt = Replace(Txt, "Ò", "O")
            Txt = Replace(Txt, "Ô", "O")
            Txt = Replace(Txt, "Ú", "U")
            Txt = Replace(Txt, "Ù", "U")
            Txt = Replace(Txt, "Û", "U")
            Txt = Replace(Txt, "Ç", "C")

            Txt = Replace(Txt, "°", ".")
            Txt = Replace(Txt, "ª", ".")

            Txt = Replace(Txt, "'", " ")

            tiraCaracEsp = Txt
            Return tiraCaracEsp
        End Function

    End Class
End Namespace
