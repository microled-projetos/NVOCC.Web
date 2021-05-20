Public Class Faturamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        ' ClientScript.RegisterOnSubmitStatement(Me.GetType(), "confirm", "return confirm('teste?');")

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2028 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click

        Dim filtro As String = ""

        If ddlFiltro.SelectedValue = 1 Then
            filtro &= " WHERE DT_VENCIMENTO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 2 Then
            filtro &= " WHERE NR_PROCESSO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 3 Then
            filtro &= " WHERE PARCEIRO_EMPRESA LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 4 Then
            filtro &= " WHERE REFERENCIA_CLIENTE LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 5 Then
            filtro &= " WHERE NR_NOTA_DEBITO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 6 Then
            filtro &= " WHERE NR_RPS LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 7 Then
            filtro &= " WHERE NR_NOTA_FISCAL LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 8 Then
            filtro &= " WHERE NR_RECIBO LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlFiltro.SelectedValue = 9 Then
            filtro &= " WHERE DT_LIQUIDACAO >= '%" & txtPesquisa.Text & "%'"

        End If

        If ckStatus.Items.FindByValue(1).Selected Then
            If filtro = "" Then
                filtro &= " WHERE DT_LIQUIDACAO IS NULL"

            Else
                filtro &= " OR DT_LIQUIDACAO IS NULL"

            End If


        End If
        If ckStatus.Items.FindByValue(2).Selected Then

            If filtro = "" Then
                filtro &= " WHERE DT_LIQUIDACAO IS NOT NULL"

            Else
                filtro &= " OR DT_LIQUIDACAO IS NOT NULL"

            End If

        End If
        If ckStatus.Items.FindByValue(3).Selected Then
            If filtro = "" Then
                filtro &= " WHERE DT_CANCELAMENTO IS NOT NULL"

            Else
                filtro &= " OR DT_CANCELAMENTO IS NOT NULL"

            End If
        End If

        dsFaturamento.SelectCommand = "SELECT * FROM [dbo].[View_Faturamento] " & filtro
        dgvFaturamento.DataBind()

        ddlFiltro.SelectedValue = 0
        txtPesquisa.Text = ""
    End Sub

    Private Sub ddlFiltro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltro.SelectedIndexChanged
        If ddlFiltro.SelectedValue = 1 Or ddlFiltro.SelectedValue = 9 Then
            txtPesquisa.CssClass = "form-control data"
            txtPesquisa.Text = Now.Date.AddDays(-1)
            txtPesquisa.Text = FinalSemana(txtPesquisa.Text)

        Else
            txtPesquisa.CssClass = "form-control"
            txtPesquisa.Text = ""
        End If
    End Sub

    Private Sub btnBaixarFatura_Click(sender As Object, e As EventArgs) Handles btnBaixarFatura.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            If txtData.Text = "" Then
                lblmsgErro.Text = "É necessário informar a data para efetuar a baixa!"
                divErro.Visible = True
                Exit Sub
            Else
                Dim NumeracaoDoc As New NumeracaoDoc
                Dim numero As String = NumeracaoDoc.Numerar(2)

                Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_CANCELAMENTO,DT_LIQUIDACAO FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura cancelada!"
                        divErro.Visible = True
                        Exit Sub

                    ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LIQUIDACAO")) Then
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura já liquidada!"
                        divErro.Visible = True
                        Exit Sub

                    Else
                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_RECIBO = getdate(), NR_RECIBO = '" & numero & "' WHERE ID_FATURAMENTO =" & txtID.Text)
                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RECIBO = '" & numero & "' WHERE ID_NUMERACAO = 5")

                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text & ")")
                        Con.Fechar()
                        lblmsgSuccess.Text = "Baixa realizada com sucesso!"
                        divSuccess.Visible = True
                        txtData.Text = ""
                        txtObs.Text = ""
                        dgvFaturamento.DataBind()
                    End If
                End If


            End If
        End If

    End Sub

    Private Sub btnSalvarCancelamento_Click(sender As Object, e As EventArgs) Handles btnSalvarCancelamento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divInfo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            If txtObs.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "É necessario preencher o campo de observações!"
            Else
                Dim ds As DataSet = Con.ExecutarQuery("SELECT B.DT_LIQUIDACAO,NR_NOTA_FISCAL FROM [TB_FATURAMENTO] A
LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE DT_LIQUIDACAO IS NULL AND ID_FATURAMENTO =" & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                        If lblContador.Text = "" Then
                            divInfo.Visible = True
                            lblmsgInfo.Text = "A NOTA FISCAL JÁ FOI GERADA.<br/>CONFIRMA O CANCELAMENTO DA FATURA ASSIM MESMO?"
                            lblContador.Text = 1
                            ModalPopupExtender3.Show()
                            btnSalvarCancelamento.Text = "Confirmar Cancelamento"
                            Exit Sub
                        Else
                            lblContador.Text = ""
                            btnSalvarCancelamento.Text = "Salvar"
                            divInfo.Visible = False
                            divSuccess.Visible = True
                            lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                        End If

                    Else
                        ' Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "' WHERE ID_FATURAMENTO =" & txtID.Text)
                        Con.Fechar()
                        lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                        divSuccess.Visible = True
                        txtData.Text = ""
                        txtObs.Text = ""
                        dgvFaturamento.DataBind()
                        divInfo.Visible = False
                    End If
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura já liquidada!"
                End If
            End If
        End If

    End Sub

    Private Sub dgvFaturamento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFaturamento.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        divErroSubstituir.Visible = False
        divInfo.Visible = False

        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                dgvFaturamento.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            For i As Integer = 0 To dgvFaturamento.Rows.Count - 1
                dgvFaturamento.Rows(txtlinha.Text).CssClass = "Normal"

            Next

            dgvFaturamento.Rows(txtlinha.Text).CssClass = "selected1"

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_PROCESSO,PARCEIRO_EMPRESA,CONVERT(VARCHAR,DT_NOTA_FISCAL,103)DT_NOTA_FISCAL,NR_NOTA_FISCAL FROM View_Faturamento WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                    lblProcessoCancelamento.Text = "PROCESSO: " & ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    lblProcessoBaixa.Text = "PROCESSO: " & ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    lblProcessoSubs.Text = "PROCESSO: " & ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")) Then
                    lblClienteCancelamento.Text = "CLIENTE: " & ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")
                    lblClienteBaixa.Text = "CLIENTE: " & ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")
                    lblClienteSubs.Text = "CLIENTE: " & ds.Tables(0).Rows(0).Item("PARCEIRO_EMPRESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then
                    lblNumeroNota.Text = ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")
                    lkNotasFiscais.Visible = True
                Else
                    lkNotasFiscais.Visible = False
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")) Then
                    lblDataEmissao.Text = ds.Tables(0).Rows(0).Item("DT_NOTA_FISCAL")
                End If
            End If
            Con.Fechar()

        End If
    End Sub

    Private Sub lkNotaDebito_Click(sender As Object, e As EventArgs) Handles lkNotaDebito.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Response.Redirect("EmissaoNDFaturamento.aspx?id=" & txtID.Text)
        End If
    End Sub

    Private Sub lkReciboPagamento_Click(sender As Object, e As EventArgs) Handles lkReciboPagamento.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT B.DT_LIQUIDACAO FROM [TB_FATURAMENTO] A
LEFT JOIN TB_CONTA_PAGAR_RECEBER B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE DT_LIQUIDACAO IS NOT NULL AND ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                Response.Redirect("ReciboPagamento.aspx?id=" & txtID.Text)
            Else
                divErro.Visible = True
                lblmsgErro.Text = "Nota sem liquidação!"
            End If
            Con.Fechar()
        End If


    End Sub

    Private Sub lkReciboServico_Click(sender As Object, e As EventArgs) Handles lkReciboServico.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT DT_RPS FROM [TB_FATURAMENTO] WHERE DT_RPS IS NOT NULL AND ID_FATURAMENTO = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                Response.Redirect("ReciboProvisorioServico.aspx?id=" & txtID.Text)
            Else
                divErro.Visible = True
                lblmsgErro.Text = "Nota sem RPS!"
            End If
        End If
    End Sub

    Private Sub lkGerarRPS_Click(sender As Object, e As EventArgs) Handles lkGerarRPS.Click
        divSuccess.Visible = False
        divErro.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_DEBITO,NR_RPS,DT_RPS FROM [TB_FATURAMENTO]
WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("NR_RPS")) And IsDBNull(ds.Tables(0).Rows(0).Item("DT_RPS")) Then
                        Dim NumeracaoDoc As New NumeracaoDoc
                        Dim numero As String = NumeracaoDoc.Numerar(3)
                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_RPS = getdate(), NR_RPS = '" & numero & "' WHERE ID_FATURAMENTO =" & txtID.Text)
                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RPS = '" & numero & "' WHERE ID_NUMERACAO = 5")
                        divSuccess.Visible = True
                        lblmsgSuccess.Text = "RPS gerada com sucesso!"
                    Else
                        lblmsgErro.Text = "Não foi possivel completar a ação: fatura já possui RPS!"
                        divErro.Visible = True
                    End If

                Else
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota de débito!"
                    divErro.Visible = True
                End If

            End If

        End If
    End Sub

    Private Sub lkCancelarNota_Click(sender As Object, e As EventArgs) Handles lkCancelarNota.Click
        divErro.Visible = False
        divSuccess.Visible = False
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_FISCAL FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then

                    Dim NumeracaoDoc As New NumeracaoDoc
                    Dim numero As String = NumeracaoDoc.Numerar(3)
                    Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RPS = '" & numero & "' WHERE ID_NUMERACAO = 5")

                    Con.ExecutarQuery("INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,NR_RPS,NR_NOTA_FISCAL,NR_RECIBO,DT_NOTA_DEBITO,DT_RPS,DT_NOTA_FISCAL,DT_RECIBO,FL_RPS,FL_NOTA_SUBSTITUTA,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,STATUS_NFE) SELECT ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,NR_RPS,NR_NOTA_FISCAL,NR_RECIBO,DT_NOTA_DEBITO,'" & numero & "',Getdate(),DT_RECIBO,FL_RPS,FL_NOTA_SUBSTITUTA,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,3 FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text)

                    Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_CANCELAMENTO = getdate(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = 'CANCELAMENTO DA NOTA FISCAL' WHERE ID_FATURAMENTO =" & txtID.Text)
                    lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
                    divSuccess.Visible = True
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
                End If

            Else
                divErro.Visible = True
                lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
            End If
            Con.Fechar()
        End If

    End Sub

    Private Sub btnSubstituir_Click(sender As Object, e As EventArgs) Handles btnSubstituir.Click
        divErroSubstituir.Visible = False
        divSuccess.Visible = False
        Dim validar As New VerificaData

        If txtID.Text = "" Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "Selecione um registro"

        ElseIf txtNovoNumeroNota.Text = "" Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "É necessário informar um novo número de nota para concluir a substituição!"
            ModalPopupExtender5.Show()
            Exit Sub
        ElseIf txtNovaEmissaoNota.Text = "" Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "É necessário informar uma nova data de emissao de nota para concluir a substituição!"
            ModalPopupExtender5.Show()
            Exit Sub
        ElseIf validar.ValidaData(txtNovaEmissaoNota.Text) = False Then
            divErroSubstituir.Visible = True
            lblErroSubstituir.Text = "Nova data de emissão inválida!"
            ModalPopupExtender5.Show()
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_NOTA_FISCAL FROM [TB_FATURAMENTO] WHERE ID_FATURAMENTO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_FISCAL")) Then

                    Con.ExecutarQuery("INSERT INTO TB_FATURAMENTO (ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,NR_RPS,NR_NOTA_FISCAL,NR_RECIBO,DT_NOTA_DEBITO,DT_RPS,DT_NOTA_FISCAL,DT_RECIBO,FL_RPS,FL_NOTA_SUBSTITUTA,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,STATUS_NFE) SELECT ID_CONTA_PAGAR_RECEBER,DT_CANCELAMENTO,ID_USUARIO_CANCELAMENTO,DS_MOTIVO_CANCELAMENTO,NR_NOTA_DEBITO,NR_RPS,'" & txtNovoNumeroNota.Text & "',NR_RECIBO,DT_NOTA_DEBITO,DT_RPS,CONVERT(DATE,'" & txtNovaEmissaoNota.Text & "',103),DT_RECIBO,FL_RPS,1,VL_NOTA,VL_NOTA_EXTENSO,NM_CLIENTE,CNPJ,INSCR_ESTADUAL,INSCR_MUNICIPAL,ENDERECO,NR_ENDERECO,COMPL_ENDERECO,BAIRRO,CIDADE,ESTADO,CEP,VL_ISS,2 FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & txtID.Text)

                    Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_CANCELAMENTO = getdate(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = 'SUBSTITUIÇÃO DA NOTA FISCAL' WHERE ID_FATURAMENTO =" & txtID.Text)
                    lblmsgSuccess.Text = "Substituição realizada com sucesso!"
                    divSuccess.Visible = True
                    txtNovoNumeroNota.Text = ""
                    txtNovaEmissaoNota.Text = ""
                    divErroSubstituir.Visible = False
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
                End If

            Else
                divErro.Visible = True
                lblmsgErro.Text = "Não foi possivel completar a ação: fatura sem nota fiscal!"
            End If
            Con.Fechar()
        End If

    End Sub

    Public Function FinalSemana(ByVal data As Date)
        If data.DayOfWeek = DayOfWeek.Saturday Then
            data = DateAdd(DateInterval.Day, 2, data)
        ElseIf data.DayOfWeek = DayOfWeek.Sunday Then
            data = DateAdd(DateInterval.Day, 1, data)
        End If
        Return data.ToString("dd/MM/yyyy")
    End Function


End Class