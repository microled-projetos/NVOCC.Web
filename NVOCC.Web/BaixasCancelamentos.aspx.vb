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

            gridPagar.Visible = True

        End If
        Con.Fechar()
    End Sub

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

                    Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM TB_FATURAMENTO WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER =" & ID)
                    If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Não foi possivel conclir a ação: Registro já faturado!"
                        divErro.Visible = True
                        Exit Sub
                    Else

                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "'  WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                        Filtro()
                        lblSuccess.Text = "Cancelamento realizado com sucesso!"
                        divSuccess.Visible = True
                    End If

                End If
            Next


        End If

        Con.Fechar()

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

                Filtro()

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
        lblFaturaCambio.Text = ""
        lblProcessoCambio.Text = ""
        lblClienteCambio.Text = ""

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

                lblFaturaCambio.Text &= "Nº Fatura: " & fatura & "<br/>"
                lblClienteCambio.Text &= "Fornecedor: " & fornecedor & "<br/>"
            End If


        Next


    End Sub

    Sub Filtro()
        divErro.Visible = False
        divSuccess.Visible = False
        Dim FILTRO As String = ""
        If rdStatus.SelectedValue = 2 Then

            If txtVencimentoInicial.Text = "" Or txtVencimentoFinal.Text = "" Then
                lblErro.Text = "É necessário informar a data de vencimento inicial e final para busca de faturas fechadas!"
                divErro.Visible = True
                Exit Sub
            Else
                FILTRO &= " AND DT_LIQUIDACAO IS NOT NULL and CONVERT(DATE,DT_VENCIMENTO,103) Between CONVERT(DATE,'" & txtVencimentoInicial.Text & "',103) and CONVERT(DATE,'" & txtVencimentoFinal.Text & "',103)"

            End If

            btnAtualizaCambio.Visible = False

        ElseIf rdStatus.SelectedValue = 1 Then


            If txtVencimentoInicial.Text = "" Or txtVencimentoFinal.Text = "" Then
                FILTRO &= " AND DT_LIQUIDACAO IS NULL"

            Else
                FILTRO &= " AND DT_LIQUIDACAO IS NULL and CONVERT(DATE,DT_VENCIMENTO,103) Between CONVERT(DATE,'" & txtVencimentoInicial.Text & "',103) and CONVERT(DATE,'" & txtVencimentoFinal.Text & "',103)"

            End If

            btnAtualizaCambio.Visible = True
        End If

        Dim sql As String = "SELECT * FROM [View_Baixas_Cancelamentos_P]  WHERE CD_PR =  'P' " & FILTRO & " ORDER BY DT_VENCIMENTO DESC"
        dsPagar.SelectCommand = sql
        Session("CSVPagar") = sql

        dgvTaxasPagar.DataBind()
    End Sub
    Private Sub btnpesquisar_Click(sender As Object, e As EventArgs) Handles btnpesquisar.Click
        Filtro()
    End Sub


    Private Sub btnAtualizaCambio_Click(sender As Object, e As EventArgs) Handles btnAtualizaCambio.Click
        lblErro.Text = ""
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtDataCambio.Text = "" Then
            lblErro.Text = "É necessário informar a data para prosseguir!"
            divErro.Visible = True
            Exit Sub
        ElseIf txtValorCambio.Text = "" Then
            lblErro.Text = "É necessário informar o valor para prosseguir!"
            divErro.Visible = True
            Exit Sub
        Else

            Con.Fechar()

            txtDataCambio.Text = ""
            txtValorCambio.Text = ""
            ddlMoeda.SelectedValue = 0
            ModalPopupExtender2.Hide()

        End If
    End Sub

    Private Sub btnCSV_Click(sender As Object, e As EventArgs) Handles btnCSV.Click
        Dim sql As String = ""
        If Request.QueryString("t") = "p" Then

            If Session("CSVPagar") <> "" Then
                sql = Session("CSVPagar")

            End If

        End If

        If sql <> "" Then
            Classes.Excel.exportaExcel(sql, "BaixasCancelamentos")
        End If
    End Sub


    Private Sub btnCancelarBaixa_Click(sender As Object, e As EventArgs) Handles btnCancelarBaixa.Click
        Dim Con As New Conexao_sql
        Con.Conectar()

        If Request.QueryString("t") = "p" Then

            For Each linha As GridViewRow In dgvTaxasPagar.Rows
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")
                If check.Checked Then
                    Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                    Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_CONTA_PAGAR_RECEBER)QTD FROM TB_FATURAMENTO WHERE DT_CANCELAMENTO IS NULL AND ID_CONTA_PAGAR_RECEBER =" & ID)
                    If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                        lblErro.Text = "Não foi possivel conclir a ação: Registro já faturado!"
                        divErro.Visible = True
                        Exit Sub
                    Else
                        Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = NULL, ID_USUARIO_LIQUIDACAO = NULL WHERE ID_CONTA_PAGAR_RECEBER =" & ID)
                    End If

                End If
            Next

            Filtro()


        End If


        Con.Fechar()
        lblSuccess.Text = "Baixa cancelada com sucesso!"
        divSuccess.Visible = True
        txtData.Text = ""
        txtObs.Text = ""

    End Sub
End Class