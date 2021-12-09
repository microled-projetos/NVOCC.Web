Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports iTextSharp.tool.xml

Public Class GeraPDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New System.Net.WebClient()
            Dim url As String = ""

            Dim funcao As String = Request.QueryString("f")
            Dim cotacao As String = Request.QueryString("c")
            Dim Linguagem As String = Request.QueryString("l")
            If Linguagem = "p" Then
                url = "http://" & Request.ServerVariables("HTTP_HOST") & "/CotacaoPDF_PT.aspx?c=" & cotacao
            ElseIf Linguagem = "i" Then
                url = "http://" & Request.ServerVariables("HTTP_HOST") & "/CotacaoPDF_ING.aspx?c=" & cotacao
            End If

            Dim html As String = obj.DownloadString(url)
            Dim conteudo As TextReader = New StringReader(html)

            Dim fs As New FileStream(Server.MapPath("/Content/PDFAuxiliar.pdf"), FileMode.Create, FileAccess.Write, FileShare.None)
            '    Dim fs As New FileStream(AppContext.BaseDirectory & "/Content/PDFAuxiliar.pdf", FileMode.Create, FileAccess.Write, FileShare.None)
            Dim ms As New MemoryStream()


            Dim Documento As New Document(PageSize.A4, 10, 10, 160, 90)
            Dim writer As PdfWriter = PdfWriter.GetInstance(Documento, fs)
            Dim writer1 As PdfWriter = PdfWriter.GetInstance(Documento, ms)

            Documento.Open()
            Documento.AddTitle("NVOCC")
            Documento.AddAuthor("NVOCC")
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, Documento, conteudo)

            Documento.Close()

            Dim reader As New PdfReader(Server.MapPath("/Content/PDFAuxiliar.pdf"))
            '            Dim reader As New PdfReader(AppContext.BaseDirectory & "/Content/PDFAuxiliar.pdf")




            'limpa diretorio de boletos
            Dim di As System.IO.DirectoryInfo = New DirectoryInfo(Server.MapPath("/Content/cotacoes"))
            For Each file As FileInfo In di.GetFiles()
                file.Delete()
            Next
            For Each dir As DirectoryInfo In di.GetDirectories()
                dir.Delete(True)
            Next


            Dim fs_ As New FileStream(Server.MapPath("/Content/cotacoes/CotacaoPDF_" & cotacao & ".pdf"), FileMode.Create, FileAccess.Write, FileShare.None)
            ' Dim fs_ As New FileStream(Server.MapPath("/Content/CotacaoPDF.pdf"), FileMode.Create, FileAccess.Write, FileShare.None)
            Using stamper = New PdfStamper(reader, fs_)
                Dim contador As Integer = reader.NumberOfPages
                Dim layer As New PdfLayer("WatermarkLayer", stamper.Writer)
                For i As Integer = 1 To contador
                    Dim rec As New Rectangle(reader.GetPageSize(i))
                    Dim cb As PdfContentByte = stamper.GetUnderContent(i)

                    cb.BeginLayer(layer)
                    cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 50)

                    Dim gstate As New PdfGState
                    cb.SetGState(gstate)

                    cb.SetColorFill(BaseColor.BLACK)
                    cb.BeginText()
                    Dim png As Image = Image.GetInstance(Server.MapPath("/Content/imagens/Capa-proposta-FCA-Log.png"))
                    ' Dim png As Image = Image.GetInstance(AppContext.BaseDirectory & "/Content/imagens/teste.png")
                    png.SetAbsolutePosition(0, 0)
                    png.Alignment = Image.UNDERLYING
                    cb.AddImage(png)


                    cb.EndText()
                    cb.EndLayer()
                Next

                stamper.Close()
            End Using
            reader.Close()
            If funcao = "e" Then
                Response.Redirect("OUTLOOK.aspx?c=" & cotacao)


            ElseIf funcao = "i" Then
                Response.Redirect("~/" & "content/cotacoes/CotacaoPDF_" & cotacao & ".pdf")
                ' Response.Redirect("~/" & "content/CotacaoPDF.pdf")
            End If

            fs.Close()
            fs_.Close()

        Catch ex As Exception
            'stamper.Close()

            'reader.Close()

            'Documento.Close()

            'fs.Close()
            'fs_.Close()


            Response.Write("erro: " & ex.Message)
        End Try


    End Sub

End Class