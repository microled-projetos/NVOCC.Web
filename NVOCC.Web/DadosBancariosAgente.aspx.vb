Public Class DadosBancariosAgente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Request.QueryString("id") = "" Then

            Response.Redirect("Default.aspx")
        Else
            txtID.Text = Request.QueryString("id")
            CarregaDados(Request.QueryString("id"))
        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub


    Sub CarregaDados(ID_Parceiro)
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        ds = Con.ExecutarQuery("SELECT NM_FANTASIA,PAYMENT_TO,BANK_NAME,AGREEMENT,IBAN_BR,SWIFT_CODE,ACCOUNT_NUMBER,AGENCY,REFUND,OB_BANCARIA FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO = " & ID_Parceiro)
        If ds.Tables(0).Rows.Count > 0 Then
            lblRazaoSocial.Text = ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
            txtPayment.Text = ds.Tables(0).Rows(0).Item("PAYMENT_TO").ToString()
            txtBank.Text = ds.Tables(0).Rows(0).Item("BANK_NAME").ToString()
            txtSwift.Text = ds.Tables(0).Rows(0).Item("SWIFT_CODE").ToString()
            txtAccount.Text = ds.Tables(0).Rows(0).Item("ACCOUNT_NUMBER").ToString()
            txtAgency.Text = ds.Tables(0).Rows(0).Item("AGENCY").ToString()
            txtRefund.Text = ds.Tables(0).Rows(0).Item("REFUND").ToString()
            txtObs.Text = ds.Tables(0).Rows(0).Item("OB_BANCARIA").ToString()
            txtAgreement.Text = ds.Tables(0).Rows(0).Item("AGREEMENT").ToString()
            txtIban.Text = ds.Tables(0).Rows(0).Item("IBAN_BR").ToString()

        End If

        Con.Fechar()
    End Sub
    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divSuccess.Visible = False
        divErro.Visible = False

        If txtID.Text = "" Then
            msgErro.Text = "Agente nao informado."
            divErro.Visible = True

        ElseIf txtPayment.Text = "" And txtBank.Text = "" And txtSwift.Text = "" And txtAgency.Text = "" And txtAccount.Text = "" And txtRefund.Text = "" And txtObs.Text = "" And txtAgreement.Text = "" And txtIban.Text = "" Then
            msgErro.Text = "Necessário preencher pelo menos um dos campos."
            divErro.Visible = True
        Else

            Dim Con As New Conexao_sql

            'update 

            Dim filtro As String = ""
            If txtPayment.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " PAYMENT_TO ='" & txtPayment.Text & "' "
            End If

            If txtBank.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " BANK_NAME ='" & txtBank.Text & "' "
            End If

            If txtSwift.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " SWIFT_CODE ='" & txtSwift.Text & "' "
            End If

            If txtAccount.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " ACCOUNT_NUMBER ='" & txtAccount.Text & "' "
            End If

            If txtAgency.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " AGENCY ='" & txtAgency.Text & "' "
            End If

            If txtRefund.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " REFUND ='" & txtRefund.Text & "' "
            End If

            If txtObs.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " OB_BANCARIA ='" & txtObs.Text & "' "
            End If

            If txtAgreement.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " AGREEMENT ='" & txtAgreement.Text & "' "
            End If

            If txtIban.Text <> "" Then
                If filtro <> "" Then
                    filtro = ","
                End If
                filtro = filtro & " IBAN_BR = '" & txtIban.Text & "' "
            End If

            Dim SQL As String = "UPDATE [dbo].[TB_PARCEIRO] SET " & filtro & "  WHERE ID_PARCEIRO = " & txtID.Text

            Con.ExecutarQuery(SQL)
            divSuccess.Visible = True

        End If

    End Sub

End Class