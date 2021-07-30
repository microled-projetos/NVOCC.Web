Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Outlook
Public Class OUTLOOK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
<<<<<<< HEAD

=======
>>>>>>> 466d1694bb91d9ec0afdb05f5d615ddcbc06d9e6
        SurroundingSub()
    End Sub

    Private Sub SurroundingSub()
        Dim app As Application = New Application()
        Dim mail As MailItem = CType(app.CreateItem(OlItemType.olMailItem), MailItem)
        mail.[To] = "contatodocliente@gmail.com"
        mail.Subject = "COTAÇÃO"
        mail.Body = "Olá, segue em anexo sua cotação"
        mail.Importance = OlImportance.olImportanceNormal
        mail.Attachments.Add(Server.MapPath("/Content/CotacaoPDF.pdf"), OlAttachmentType.olByValue, Type.Missing, Type.Missing)
        mail.Display(True)
        Response.Redirect("CotacaoComercial.aspx")
<<<<<<< HEAD
=======

>>>>>>> 466d1694bb91d9ec0afdb05f5d615ddcbc06d9e6
    End Sub
End Class