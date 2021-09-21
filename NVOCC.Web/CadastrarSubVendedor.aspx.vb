Public Class CadastrarSubVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        Dim v As New VerificaData
        divErro.Visible = False
        divErroNovo.Visible = False
        divSuccess.Visible = False

        If txtValidade.Text = "" And txtTaxaFixa.Text = "" And txtPercentual.Text = "" And ddlVendedor.SelectedValue = 0 And ddlSubVendedor.SelectedValue = 0 Then
            lblmsgErroNovo.Text = "Preencha os campos obrigatórios."
            divErroNovo.Visible = True
            ModalPopupExtender3.Show()

        ElseIf v.ValidaData(txtValidade.Text) = False Then
            divErroNovo.Visible = True
            lblmsgErroNovo.Text = "A data de validade é inválida."
            ModalPopupExtender3.Show()

        ElseIf txtTaxaFixa.Text <> "" And txtPercentual.Text = "" And ddlBaseCalculoTaxa.SelectedValue = 0 Then
            lblmsgErroNovo.Text = "Preencha os campos obrigatórios."
            divErroNovo.Visible = True
            ModalPopupExtender3.Show()

        Else
            Dim base_calculo As String = "NULL"
            txtPercentual.Text = txtPercentual.Text.Replace(".", "")
            txtPercentual.Text = txtPercentual.Text.Replace(",", ".")

            txtTaxaFixa.Text = txtTaxaFixa.Text.Replace(".", "")
            txtTaxaFixa.Text = txtTaxaFixa.Text.Replace(",", ".")

            If txtTaxaFixa.Text = "" Then
                txtTaxaFixa.Text = "NULL"
            Else
                txtTaxaFixa.Text = "'" & txtTaxaFixa.Text & "'"
            End If

            If txtPercentual.Text = "" Then
                txtPercentual.Text = "NULL"
            Else
                txtPercentual.Text = "'" & txtPercentual.Text & "'"
            End If

            If ddlBaseCalculoTaxa.SelectedValue = 0 Then
                base_calculo = "NULL"
            Else
                base_calculo = ddlBaseCalculoTaxa.SelectedValue
            End If

            Dim Con As New Conexao_sql
            Con.Conectar()
            Con.ExecutarQuery("INSERT INTO TB_SUB_VENDEDOR (ID_PARCEIRO_SUB_VENDEDOR,ID_PARCEIRO_VENDEDOR,VL_PERCENTUAL,VL_TAXA_FIXA,ID_BASE_CALCULO,DT_VALIDADE_INICIAL) VALUES (" & ddlSubVendedor.SelectedValue & "," & ddlVendedor.SelectedValue & "," & txtPercentual.Text & "," & txtTaxaFixa.Text & "," & base_calculo & ",CONVERT(DATE,'" & txtValidade.Text & "',103))")
            dgvSubVendedor.DataBind()
            lblmsgSuccess.Text = "Cadastrado com sucesso!"
            divSuccess.Visible = True
        End If


    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click

    End Sub


    Private Sub txtTaxaFixa_TextChanged(sender As Object, e As EventArgs) Handles txtTaxaFixa.TextChanged
        If txtTaxaFixa.Text <> "" Then
            txtPercentual.Enabled = False

        Else
            txtPercentual.Enabled = True
        End If
        ModalPopupExtender3.Show()
    End Sub

    Private Sub txtPercentual_TextChanged(sender As Object, e As EventArgs) Handles txtPercentual.TextChanged
        If txtPercentual.Text <> "" Then
            txtTaxaFixa.Enabled = False
            txtTaxaFixa.Text = ""

            ddlBaseCalculoTaxa.SelectedValue = 0
            ddlBaseCalculoTaxa.Enabled = False
        Else
            txtTaxaFixa.Enabled = True
            ddlBaseCalculoTaxa.Enabled = True
        End If
        ModalPopupExtender3.Show()
    End Sub

    Private Sub dgvSubVendedor_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvSubVendedor.RowCommand
        divErro.Visible = False
        divSuccess.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ds As DataSet

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblmsgErro.Text = "Usuário não tem permissão para realizar exclusões"
                divErro.Visible = True
            Else
                Dim id As String = e.CommandArgument
                Con.ExecutarQuery("DELETE FROM TB_SUB_VENDEDOR WHERE ID_SUB_VENDEDOR =" & id)
                lblmsgSuccess.Text = "Deletado com sucesso!"
                divSuccess.Visible = True
                dgvSubVendedor.DataBind()
            End If


        End If
    End Sub
End Class