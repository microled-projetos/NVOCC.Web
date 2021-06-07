Public Class BaixasCancelamentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2027 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        Else
            If Request.QueryString("t") = "p" Then
                lblTipo.Text = "CONTAS A PAGAR"
                'btnBaixarPagamento.Visible = True
                gridPagar.Visible = True
                gridReceber.Visible = False

            ElseIf Request.QueryString("t") = "r" Then
                lblTipo.Text = "CONTAS A RECEBER"
                'btnBaixarRecebimento.Visible = True
                gridPagar.Visible = False
                gridReceber.Visible = True

            End If
        End If
        Con.Fechar()
    End Sub

    'Private Sub btnBaixarPagamento_Click(sender As Object, e As EventArgs) Handles btnBaixarPagamento.Click
    '    divErro.Visible = False
    '    divSuccess.Visible = False

    '    If txtData.Text = "" Then
    '        lblErro.Text = "É necessário informar a data para efetuar a baixa!"
    '        divErro.Visible = True
    '    Else
    '        Dim Con As New Conexao_sql
    '        Con.Conectar()

    '        For Each linha As GridViewRow In dgvTaxasPagar.Rows
    '            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
    '            If check.Checked Then
    '                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
    '                Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
    '            End If
    '        Next
    '        Con.Fechar()
    '        lblSuccess.Text = "Baixa realizada com sucesso!"
    '        divSuccess.Visible = True
    '        txtData.Text = ""
    '        dgvTaxasPagar.DataBind()

    '    End If

    'End Sub

    'Private Sub btnBaixarRecebimento_Click(sender As Object, e As EventArgs) Handles btnBaixarRecebimento.Click
    '    divErro.Visible = False
    '    divSuccess.Visible = False

    '    If txtData.Text = "" Then
    '        lblErro.Text = "É necessário informar a data para efetuar a baixa!"
    '        divErro.Visible = True
    '    Else
    '        Dim Con As New Conexao_sql
    '        Con.Conectar()

    '        For Each linha As GridViewRow In dgvTaxasReceber.Rows
    '            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
    '            If check.Checked Then
    '                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
    '                Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
    '            End If
    '        Next
    '        Con.Fechar()
    '        lblSuccess.Text = "Baixa realizada com sucesso!"
    '        divSuccess.Visible = True
    '        txtData.Text = ""
    '        dgvTaxasReceber.DataBind()

    '    End If

    'End Sub

    Private Sub btnSalvarCancelamento_Click(sender As Object, e As EventArgs) Handles btnSalvarCancelamento.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If Request.QueryString("t") = "p" Then

            For Each linha As GridViewRow In dgvTaxasPagar.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "'  WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                End If
            Next
            dgvTaxasPagar.DataBind()

        ElseIf Request.QueryString("t") = "r" Then

            For Each linha As GridViewRow In dgvTaxasReceber.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "' WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                End If
            Next
            dgvTaxasReceber.DataBind()

        End If


        Con.Fechar()
        lblSuccess.Text = "Cancelamento realizado com sucesso!"
        divSuccess.Visible = True
        txtData.Text = ""
        txtObs.Text = ""
        mpeObs.Hide()


    End Sub

    Private Sub btnSalvarBaixa_Click(sender As Object, e As EventArgs) Handles btnSalvarBaixa.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtData.Text = "" Then
            lblErro.Text = "É necessário informar a data para efetuar a baixa!"
            divErro.Visible = True
            Exit Sub

        Else
            If Request.QueryString("t") = "p" Then

                For Each linha As GridViewRow In dgvTaxasPagar.Rows
                    Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                    If check.Checked Then
                        Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                    End If
                Next

                dgvTaxasPagar.DataBind()

            ElseIf Request.QueryString("t") = "r" Then

                For Each linha As GridViewRow In dgvTaxasReceber.Rows
                    Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                    If check.Checked Then
                        Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & ID)

                        Dim dsFaturamento As DataSet = Con.ExecutarQuery("SELECT ID_FATURAMENTO FROM TB_FATURAMENTO WHERE ID_CONTA_PAGAR_RECEBER = " & ID)
                        If dsFaturamento.Tables(0).Rows.Count > 0 Then
                            Dim NumeracaoDoc As New NumeracaoDoc
                            Dim numero As String = NumeracaoDoc.Numerar(2)

                            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_RECIBO = getdate(), NR_RECIBO = '" & numero & "' WHERE ID_FATURAMENTO =" & dsFaturamento.Tables(0).Rows(0).Item("ID_FATURAMENTO"))
                            Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RECIBO = '" & numero & "' WHERE ID_NUMERACAO = 5")
                        End If
                    End If
                Next

                dgvTaxasReceber.DataBind()

            End If


            Con.Fechar()
            lblSuccess.Text = "Baixa realizada com sucesso!"
            divSuccess.Visible = True
            txtData.Text = ""
            txtObs.Text = ""
            mpeObs.Hide()

        End If
    End Sub


    Private Sub dgvTaxasPagar_Load(sender As Object, e As EventArgs) Handles dgvTaxasPagar.Load
        Dim Con As New Conexao_sql
        lblFaturaCancelamento.Text = ""
        lblProcessoCancelamento.Text = ""
        lblClienteCancelamento.Text = ""

        lblFaturaBaixa.Text = ""
        lblProcessoBaixa.Text = ""
        lblClienteBaixa.Text = ""
        For Each linha As GridViewRow In dgvTaxasPagar.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim fatura As String = CType(linha.FindControl("lblFatura"), Label).Text
            Dim fornecedor As String = CType(linha.FindControl("lblFornecedor"), Label).Text

            If check.Checked Then
                lblFaturaCancelamento.Text &= "Nº Fatura: " & fatura & "<br/>"
                lblClienteCancelamento.Text &= "Fornecedor: " & fornecedor & "<br/>"


                lblFaturaBaixa.Text &= "Nº Fatura: " & fatura & "<br/>"
                lblClienteBaixa.Text &= "Fornecedor: " & fornecedor & "<br/>"
            End If
        Next
    End Sub


    Private Sub dgvTaxasReceber_Load(sender As Object, e As EventArgs) Handles dgvTaxasReceber.Load
        Dim Con As New Conexao_sql
        lblFaturaCancelamento.Text = ""
        lblProcessoCancelamento.Text = ""
        lblClienteCancelamento.Text = ""

        lblFaturaBaixa.Text = ""
        lblProcessoBaixa.Text = ""
        lblClienteBaixa.Text = ""
        For Each linha As GridViewRow In dgvTaxasReceber.Rows
            Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            Dim processo As String = CType(linha.FindControl("lblProcesso"), Label).Text
            Dim fornecedor As String = CType(linha.FindControl("lblFornecedor"), Label).Text

            If check.Checked Then
                lblProcessoCancelamento.Text &= "Nº Processo: " & processo & "<br/>"
                lblClienteCancelamento.Text &= "Fornecedor: " & fornecedor & "<br/>"


                lblProcessoBaixa.Text &= "Nº Processo: " & processo & "<br/>"
                lblClienteBaixa.Text &= "Fornecedor: " & fornecedor & "<br/>"
            End If
        Next
    End Sub
End Class