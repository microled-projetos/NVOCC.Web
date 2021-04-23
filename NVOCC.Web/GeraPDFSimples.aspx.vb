Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports iTextSharp.tool.xml

Public Class GeraPDFSimples
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New System.Net.WebClient()
            obj.Headers.Add("Content-Type", "text/xhtml")
            obj.Headers.Add("Method", "application/x-www-form-urlencoded")
            obj.Headers.Add("UserAgent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.0.3705; Media Center PC 4.0; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)")
            obj.Headers.Add("UAcceptEncoding", "gzip, deflate")
            obj.Headers.Add("Encoding", "UTF-8")
            obj.Headers.Add("UAccept", "image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/xaml+xml, application/x-ms-xbap, application/x-ms-application, application/vnd.ms-xpsdocument, */*")
            obj.Headers.Add("UKeepAlive", "False")
            obj.Headers.Add("Content-Type", "text/xhtml")
            obj.Headers.Add("Cache-Control", "no-cache")
            obj.Headers.Add("Expires", "0")
            obj.Headers.Add("Pragma", "no-store")
            obj.Headers.Add("Pragma", "no-cache")

            Dim url As String = "http://" & Request.ServerVariables("HTTP_HOST") & "/BL_PDF.aspx"

            Dim html As String = obj.DownloadString(url)

            Dim Documento As New Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10)
            Dim objeto As New HTMLWorker(Documento)

            Dim ms As New MemoryStream()
            Dim writer As PdfWriter = PdfWriter.GetInstance(Documento, ms)
            Documento.Open()
            Documento.AddTitle("NVOCC")
            Documento.AddAuthor("NVOCC")
            Dim conteudo As TextReader = New StringReader(html)
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, Documento, conteudo)

            Documento.Close()
            Dim nomeArquivo As String = Now.Ticks() & ".pdf"
            Response.Buffer = False
            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/pdf"

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()

            Response.AddHeader("content-disposition", "inline; filename=" & nomeArquivo)
            Response.AddHeader("Expires", "-1")
            Response.AddHeader("Pragma", "no-cache, no-store")
            Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate, max-age=0")

            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()

            HttpContext.Current.ApplicationInstance.CompleteRequest()

            'Response.End()
        Catch ex As Exception
            Response.Write("erro: " & ex.Message)
        End Try


    End Sub


End Class