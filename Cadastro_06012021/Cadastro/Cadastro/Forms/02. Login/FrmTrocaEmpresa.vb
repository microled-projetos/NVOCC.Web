Public Class FrmTrocaEmpresa

    Private Sub FrmAcordos_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Environment.Exit(0)
        End If

    End Sub

    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ActiveControl = Me.cbEmpresa

        cbEmpresa.DataSource = Banco.List("SELECT AUTONUM,NOME_FANTASIA FROM " & Banco.BancoNVOCC & "TB_EMPRESAS ORDER BY NOME_FANTASIA")
        cbEmpresa.SelectedIndex = -1

    End Sub

    Private Sub btnAlteraSenha_Click(sender As Object, e As EventArgs) Handles btnAlteraSenha.Click

        Me.lblDadosIncorretos.Visible = False

        If cbEmpresa.Text = String.Empty Then
            Me.lblDadosIncorretos.Visible = True
            cbEmpresa.Focus()
            Exit Sub
        End If

        Banco.Empresa = Me.cbEmpresa.SelectedValue
        Cod_Empresa = Banco.Empresa
        lblDadosIncorretos.Visible = False
        Me.Hide()


        FrmPrincipal.lblEmpresa.Text = cbEmpresa.Text
        FrmPrincipal.mnPrincipal.Enabled = True

        FrmPrincipal.Text = Me.cbEmpresa.Text & " :: Faturamento IPA - Versão: " & Application.ProductVersion.ToString()

    End Sub

End Class
