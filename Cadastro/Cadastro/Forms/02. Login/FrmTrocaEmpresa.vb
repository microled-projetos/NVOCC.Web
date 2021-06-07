Public Class FrmTrocaEmpresa

    Private Sub FrmAcordos_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Environment.Exit(0)
        End If

    End Sub

    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ActiveControl = Me.cbEmpresa

        cbEmpresa.DataSource = Banco.List("SELECT AUTONUM,NOME_FANTASIA FROM " & Banco.BancoSGIPA & "TB_EMPRESAS ORDER BY NOME_FANTASIA")
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

        Dim Patios As String = String.Empty
        Dim Linhas As Integer = 0
        Dim Cont As Integer = 0

        Dim Ds As New DataTable
        Ds = Banco.List("SELECT AUTONUM FROM " & Banco.BancoOPERADOR & "TB_PATIOS WHERE recinto<>'000' and (COD_EMPRESA = " & Banco.Empresa & " OR 0 = " & Banco.Empresa & ")")
        Linhas = Ds.Rows.Count

        For i As Integer = 0 To Ds.Rows.Count - 1
            If i <> Linhas Then
                Patios = Patios & Ds.Rows(i)("AUTONUM").ToString() & ","
            Else
                Patios = Patios & Ds.Rows(i)("AUTONUM").ToString()
            End If
        Next

        If Patios.Substring(Patios.Length - 1, 1) = "," Then
            Patios = Patios.Remove(Patios.Length - 1, 1)
        End If

        Banco.Patios = Patios
        FrmPrincipal.lblEmpresa.Text = cbEmpresa.Text
        FrmPrincipal.mnPrincipal.Enabled = True

        FrmPrincipal.Text = Me.cbEmpresa.Text & " :: Faturamento IPA - Versão: " & Application.ProductVersion.ToString()

    End Sub

End Class
