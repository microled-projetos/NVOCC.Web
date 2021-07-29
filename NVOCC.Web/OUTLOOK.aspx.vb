Imports System.Runtime.InteropServices
Public Class OUTLOOK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mapi As SendFileTo.MAPI = New SendFileTo.MAPI()
        mapi.AddAttachment("C:\Users\Grace\Desktop\Desenvolvimento\NVOCC.Web\NVOCC.Web\Content\CotacaoPDF.pdf")
        mapi.AddRecipientTo("juliane@microled.com.br")
        mapi.SendMailPopup("testing", "body text")

    End Sub


End Class