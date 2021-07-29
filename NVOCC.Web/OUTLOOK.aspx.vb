Imports System.Runtime.InteropServices
Public Class OUTLOOK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mapi As SendFileTo.MAPI = New SendFileTo.MAPI()
        mapi.AddAttachment("c:\temp\file1.txt")
        mapi.AddAttachment("c:\temp\file2.txt")
        mapi.AddRecipientTo("person1@somewhere.com")
        mapi.AddRecipientTo("person2@somewhere.com")
        mapi.SendMailPopup("testing", "body text")

    End Sub


End Class