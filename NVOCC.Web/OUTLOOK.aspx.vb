Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Outlook
Public Class OUTLOOK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ID_COTACAO As String = Request.QueryString("c")
        Dim Nome As String = ""
        Dim Email As String = ""
        Dim ds As DataSet = Con.ExecutarQuery("select isnull(EMAIL_CONTATO,'')EMAIL_CONTATO,isnull(NM_CONTATO,'')NM_CONTATO from TB_CONTATO where ID_CONTATO = (select ID_CONTATO from TB_COTACAO where ID_COTACAO = " & ID_COTACAO & ")")

        If ds.Tables(0).Rows.Count > 0 Then
            Email = ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
            Nome = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
        End If

        Dim app As Application = New Application()
        Dim mail As MailItem = CType(app.CreateItem(OlItemType.olMailItem), MailItem)
        mail.[To] = Email '"contatodocliente@gmail.com"
        mail.Subject = "COTAÇÃO"
        mail.Body = "Prezado(a) " & Nome & ", segue sua proposta visando uma oportunidade de embarque."
        mail.Importance = OlImportance.olImportanceNormal
        mail.Attachments.Add(Server.MapPath("/Content/CotacaoPDF.pdf"), OlAttachmentType.olByValue, Type.Missing, Type.Missing)
        mail.Display(True)
        Response.Redirect("CotacaoComercial.aspx")
    End Sub

End Class