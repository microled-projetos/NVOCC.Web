Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Outlook
Public Class OUTLOOK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim mapi As SendFileTo.MAPI = New SendFileTo.MAPI()
        'mapi.AddAttachment("C:\Users\Grace\Desktop\Desenvolvimento\NVOCC.Web\NVOCC.Web\Content\CotacaoPDF.pdf")
        'mapi.AddRecipientTo("juliane@microled.com.br")
        'mapi.SendMailPopup("testing", "body text")
        SurroundingSub()
    End Sub

    Private Sub SurroundingSub()
        Dim app As Application = New Application()
        Dim mail As MailItem = CType(app.CreateItem(OlItemType.olMailItem), MailItem)
        mail.[To] = "thiago.amaro.r@gmail.com"
        mail.Subject = "Teste"
        mail.Body = "Teste"
        mail.Importance = OlImportance.olImportanceNormal
        mail.Attachments.Add("C:\Users\Grace\Desktop\Desenvolvimento\NVOCC.Web\NVOCC.Web\Content\CotacaoPDF.pdf", OlAttachmentType.olByValue, Type.Missing, Type.Missing)
        mail.Display(True)
    End Sub
End Class