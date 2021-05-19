Public Class Faturamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

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
            filtro &= " WHERE DT_LIQUIDACAO LIKE '%" & txtPesquisa.Text & "%'"

        End If

        If ckStatus.Items.FindByValue(1).Selected Then
            If filtro = "" Then
                filtro &= "WHERE DT_LIQUIDACAO IS NULL"

            Else
                filtro &= "OR DT_LIQUIDACAO IS NULL"

            End If


        End If
        If ckStatus.Items.FindByValue(2).Selected Then

            If filtro = "" Then
                filtro &= "WHERE DT_LIQUIDACAO IS NOT NULL"

            Else
                filtro &= "OR DT_LIQUIDACAO IS NOT NULL"

            End If

        End If
        If ckStatus.Items.FindByValue(3).Selected Then
            If filtro = "" Then
                filtro &= "WHERE DT_CANCELAMENTO IS NOT NULL"

            Else
                filtro &= "OR DT_CANCELAMENTO IS NOT NULL"

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

        Else
            txtPesquisa.CssClass = "form-control"

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
                Con.ExecutarQuery("UPDATE [dbo].[TB_CONTA_PAGAR_RECEBER] SET [DT_LIQUIDACAO] = CONVERT(DATE,'" & txtData.Text & "',103), ID_USUARIO_LIQUIDACAO = " & Session("ID_USUARIO") & " WHERE ID_CONTA_PAGAR_RECEBER =" & txtID.Text)
                Con.Fechar()
                lblmsgSuccess.Text = "Baixa realizada com sucesso!"
                divSuccess.Visible = True
                txtData.Text = ""
                txtObs.Text = ""
                dgvFaturamento.DataBind()
            End If
        End If

    End Sub

    Private Sub btnSalvarCancelamento_Click(sender As Object, e As EventArgs) Handles btnSalvarCancelamento.Click
        divErro.Visible = False
        divSuccess.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro"
        Else
            Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET [DT_CANCELAMENTO] = getdate() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & ",DS_MOTIVO_CANCELAMENTO = '" & txtObs.Text & "' WHERE ID_FATURAMENTO =" & txtID.Text)
            Con.Fechar()
            lblmsgSuccess.Text = "Cancelamento realizado com sucesso!"
            divSuccess.Visible = True
            txtData.Text = ""
            txtObs.Text = ""
            dgvFaturamento.DataBind()
        End If
    End Sub


    Private Sub dgvFaturamento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFaturamento.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
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


        End If
    End Sub
End Class