Imports IronPdf

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Create a PDF from an existing HTML using C#
        Dim Renderer = New IronPdf.HtmlToPdf()
        Dim PDF = Renderer.RenderHTMLFileAsPdf("C:\DropMicroled\Dropbox\EudMarco .Net\NVOCC.Web\NVOCC.Web\NVOCC.Web\CotacaoPDF.aspx")
        Dim OutputPath = "Invoice.pdf"
        PDF.SaveAs(OutputPath)
    End Sub

End Class