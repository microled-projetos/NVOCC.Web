Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Outlook
Public Class OUTLOOK
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim Con As New Conexao_sql
        'Con.Conectar()
        'Dim ID_COTACAO As String = Request.QueryString("c")
        'Dim Nome As String = ""
        'Dim Email As String = ""
        'Dim ds As DataSet = Con.ExecutarQuery("select isnull(EMAIL_CONTATO,'')EMAIL_CONTATO,isnull(NM_CONTATO,'')NM_CONTATO from TB_CONTATO where ID_CONTATO = (select ID_CONTATO from TB_COTACAO where ID_COTACAO = " & ID_COTACAO & ")")

        'If ds.Tables(0).Rows.Count > 0 Then
        '    Email = ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
        '    Nome = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
        'End If

        'Dim app As Application = New Application()
        'Dim mail As MailItem = CType(app.CreateItem(OlItemType.olMailItem), MailItem)
        'mail.[To] = Email
        'mail.Subject = "COTAÇÃO"
        'mail.Body = "Prezado(a) " & Nome & ", segue sua proposta visando uma oportunidade de embarque."
        'mail.Importance = OlImportance.olImportanceNormal
        'mail.Attachments.Add(Server.MapPath("/Content/CotacaoPDF.pdf"), OlAttachmentType.olByValue, Type.Missing, Type.Missing)
        'mail.Display(True)
        'Response.Redirect("CotacaoComercial.aspx")


        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else

            If Not Page.IsPostBack Then
                Dim Nome As String = ""
                Dim ID_COTACAO As String = Request.QueryString("c")
                ds = Con.ExecutarQuery("select isnull(EMAIL_CONTATO,'')EMAIL_CONTATO,isnull(NM_CONTATO,'')NM_CONTATO from TB_CONTATO where ID_CONTATO = (select ID_CONTATO from TB_COTACAO where ID_COTACAO = " & ID_COTACAO & ")")

                If ds.Tables(0).Rows.Count > 0 Then
                    txtDestinatario.Text = ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
                    Nome = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
                End If
                txtMsg.Text = "Prezado(a) " & Nome & ", segue sua proposta visando uma oportunidade de embarque."
                txtAssunto.Text = "COTAÇÃO"

                ds = Con.ExecutarQuery("SELECT isnull(EMAIL,'')EMAIL FROM TB_USUARIO where ID_USUARIO = " & Session("ID_USUARIO"))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtCC.Text = ds.Tables(0).Rows(0).Item("EMAIL").ToString()
                End If
            End If
        End If

        Con.Fechar()
    End Sub

    Private Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click
        Response.Redirect("CotacaoComercial.aspx")
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click

        If SeparaEmail(txtCC.Text) = False Then
            lblerro.Text = "Email de CC é invalido!"
            lblerro.Visible = True
        ElseIf SeparaEmail(txtDestinatario.Text) = False Then
            lblerro.Text = "Email de destinatário é invalido!"
            lblerro.Visible = True
        Else
            Dim email As New EmailService
            Dim retorno As String = email.EnviarEmail(txtDestinatario.Text, txtAssunto.Text, txtMsg.Text, txtCC.Text, "Content/CotacaoPDF.pdf")
            If retorno = "" Then
                divErro.Visible = True
                lblerro.Text = "Erro ao enviar e-mail"
            Else
                divSuccess.Visible = True
                lblSuccess.Text = retorno
            End If
        End If
    End Sub

    Private Sub ButtonUpload_Click(sender As Object, e As EventArgs) Handles ButtonUpload.Click
        If (FileUpload1.HasFile) Then
            Dim fileName As String = FileUpload1.FileName
            Dim SaveTo As String = "C \\ Uploads \\ "
            SaveTo += fileName
            FileUpload1.SaveAs(SaveTo)
            '  LabelMessage.Text = "Enviar foi bem sucedida "
        Else
            ' LabelMessage.Text = "Selecione um arquivo para upload "
        End If
    End Sub

    Function SeparaEmail(ByVal email As String) As Boolean
        divSuccess.Visible = False
        divErro.Visible = True

        Dim erro As Boolean
        'quebrar a string
        Dim palavras As String() = email.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

        'exibe o resultado
        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
            If ValidaEmail.Validar(palavras(i)) = False Then
                erro = False
            Else
                erro = True
            End If
        Next
        Return erro
    End Function
End Class