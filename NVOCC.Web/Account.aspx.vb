Public Class Account
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    Private Sub lkAlterarInvoice_Click(sender As Object, e As EventArgs) Handles lkAlterarInvoice.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja editar!"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else

                'realiza select e preenche os campos do painel de inserção
                ModalPopupExtender2.Show()
            End If

        End If
    End Sub

    'Private Sub btnSalvarNovaInvoice_Click(sender As Object, e As EventArgs) Handles btnSalvarNovaInvoice.Click
    '    divErro.Visible = False
    '    divSuccess.Visible = False
    '    divSuccessInvoice.Visible = False
    '    divErroInvoice.Visible = False

    '    Dim Con As New Conexao_sql
    '    Con.Conectar()
    '    Dim ds As DataSet
    '    If txtIDInvoice.Text = "" Then
    '        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

    '        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
    '            divErro.Visible = True
    '            lblErro.Text = "Usuário não possui permissão."
    '        Else

    '            'insert
    '            Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE (ID_PARCEIRO_AGENTE,ID_ACCOUNT_TIPO_INVOICE,ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA,ID_BL,ID_MOEDA,NR_INVOICE,DT_INVOICE,DT_VENCIMENTO,FL_CONFERIDO,DS_OBSERVACAO,ID_USUARIO_LANCAMENTO) VALUES ()")
    '            ' Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE_ITENS (ID_ACCOUN,ID_ACCOUNT_TIPO_INVOICE,ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA,ID_BL,ID_MOEDA,NR_INVOICE,DT_INVOICE,DT_VENCIMENTO,FL_CONFERIDO,DS_OBSERVACAO,ID_USUARIO_LANCAMENTO) VALUES ()")
    '            lblSuccess.Text = "Registro cadastrado com sucesso!"
    '            divSuccess.Visible = True
    '            ModalPopupExtender2.Show()
    '        End If
    '    Else
    '        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

    '        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
    '            divErro.Visible = True
    '            lblErro.Text = "Usuário não possui permissão"
    '        Else

    '            'update 
    '            Con.ExecutarQuery("UPDATE TB_ACCOUNT_INVOICE SET ID_PARCEIRO_AGENTE,ID_ACCOUNT_TIPO_INVOICE,ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA,ID_BL,ID_MOEDA,NR_INVOICE,DT_INVOICE,DT_VENCIMENTO,FL_CONFERIDO,DS_OBSERVACAO,ID_USUARIO_ALTERACAO")
    '            '  Con.ExecutarQuery("UPDATE TB_ACCOUNT_INVOICE_ITENS SET   WHERE ID_ACCOUNT_INVOICE " & txtID.Text)
    '            lblSuccess.Text = "Registro alterado com sucesso!"
    '            divSuccess.Visible = True
    '            ModalPopupExtender2.Show()
    '        End If

    '    End If
    'End Sub
    Private Sub btnGravarCabeçalho_Click(sender As Object, e As EventArgs) Handles btnGravarCabeçalho.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divSuccessInvoice.Visible = False
        divErroInvoice.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        If txtIDInvoice.Text = "" Then
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão."
            Else

                'insert
                Con.ExecutarQuery("INSERT INTO TB_ACCOUNT_INVOICE (ID_PARCEIRO_AGENTE,ID_ACCOUNT_TIPO_INVOICE,ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA,ID_BL,ID_MOEDA,NR_INVOICE,DT_INVOICE,DT_VENCIMENTO,FL_CONFERIDO,DS_OBSERVACAO,ID_USUARIO_LANCAMENTO) VALUES ()")
                lblSuccess.Text = "Registro cadastrado com sucesso!"
                divSuccess.Visible = True
                ModalPopupExtender2.Show()
            End If
        Else
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else

                'update 
                Con.ExecutarQuery("UPDATE TB_ACCOUNT_INVOICE SET ID_PARCEIRO_AGENTE,ID_ACCOUNT_TIPO_INVOICE,ID_ACCOUNT_TIPO_EMISSOR,ID_ACCOUNT_TIPO_FATURA,ID_BL,ID_MOEDA,NR_INVOICE,DT_INVOICE,DT_VENCIMENTO,FL_CONFERIDO,DS_OBSERVACAO,ID_USUARIO_ALTERACAO")
                lblSuccess.Text = "Registro alterado com sucesso!"
                divSuccess.Visible = True
                ModalPopupExtender2.Show()
            End If

        End If
    End Sub

    Private Sub lkExcluirInvoice_Click(sender As Object, e As EventArgs) Handles lkExcluirInvoice.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        If txtID.Text = "" Then
            divErro.Visible = True
            lblErro.Text = "Selecione o registro que deseja excluir!"
        Else
            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblErro.Text = "Usuário não possui permissão"
            Else

                'delete
                Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE " & txtID.Text)
                Con.ExecutarQuery("DELETE FROM TB_ACCOUNT_INVOICE_ITENS WHERE ID_ACCOUNT_INVOICE " & txtID.Text)
                lblSuccess.Text = "Registro deletado com sucesso!"
                divSuccess.Visible = True
            End If
        End If
    End Sub
End Class