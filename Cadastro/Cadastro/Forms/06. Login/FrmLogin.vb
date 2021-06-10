Public Class FrmLogin

    Private Sub FrmAcordos_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Environment.Exit(0)
        End If

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        If txtUsuario.Text = String.Empty Then
            MessageBox.Show("Informe o Nome de Usuário.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUsuario.Focus()
            Exit Sub
        End If

        If txtSenha.Text = String.Empty Then
            MessageBox.Show("Informe a Senha de Acesso.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSenha.Focus()
            Exit Sub
        End If

        If cbEmpresa.Text = String.Empty Then
            MessageBox.Show("Informe a Empresa.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cbEmpresa.Focus()
            Exit Sub
        End If

        If Not Logou() Then
            MessageBox.Show("Usuário ou senha inválidos!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Function Logou() As Boolean

        Dim Ds As New DataTable

        Ds = Banco.List("SELECT AUTONUM, ISNULL(FLAG_BLOQUEIO_NVOCC,0) FLAG_BLOQUEIO_NVOCC, ISNULL(FLAG_ISENTA_IMPOSTO,0) FLAG_ISENTA_IMPOSTO, ISNULL(FLAG_RETIRADA,0) FLAG_RETIRADA, ISNULL(COD_EMPRESA,0) COD_EMPRESA,ISNULL(GR_GR_DOC,0) GR_GR_DOC, ISNULL(FLAG_SOL_COM,0) FLAG_SOL_COM, USUARIO, ISNULL(FLAG_ALTERA_TABELA,0) FLAG_ALTERA_TABELA FROM " & Banco.BancoSGIPA & "TB_CAD_USUARIOS WHERE USUARIO = '" & txtUsuario.Text.ToUpper() & "' AND SENHA = '" & txtSenha.Text.Trim() & "' ")

        If Ds.Rows.Count > 0 Then

            Banco.UsuarioSistema = Convert.ToInt32(Ds.Rows(0)("AUTONUM").ToString())
            Banco.IsentaImpostos = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_ISENTA_IMPOSTO").ToString()))
            Banco.Retirada = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_RETIRADA").ToString()))
            Banco.Empresa = Convert.ToInt32(Ds.Rows(0)("COD_EMPRESA").ToString())
            Banco.GR_GR_DOC = Convert.ToBoolean(Val(Ds.Rows(0)("GR_GR_DOC").ToString()))
            Banco.FLAG_SOL_COM = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_SOL_COM").ToString()))
            Banco.FLAG_ALTERA_TABELA = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_ALTERA_TABELA").ToString()))
            Banco.FLAG_BLOQUEIO_NVOCC = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_BLOQUEIO_NVOCC").ToString()))

            If Ds.Rows.Count > 0 Then
                Cod_Empresa = Banco.Empresa
                Cod_Usuario = Banco.UsuarioSistema
                Usuario_Sistema = Ds.Rows(0)("USUARIO").ToString
            End If

            Ds = Banco.List("SELECT ISNULL(TABELA_PADRAO,0) TABELA_PADRAO FROM " & Banco.BancoSGIPA & "TB_EMPRESAS WHERE (AUTONUM = " & Banco.Empresa & " OR 0 = " & Banco.Empresa & ")")

            If Ds.Rows.Count > 0 Then
                Banco.TabelaPadrao = Convert.ToInt32(Ds.Rows(0)("TABELA_PADRAO").ToString())
            End If

        Else
            Return False
        End If

        Me.Hide()
        lblDadosIncorretos.Visible = False
        FrmPrincipal.lblUsuario.Text = Me.txtUsuario.Text

        Dim PatiosArr As New List(Of String)
        Dim Patios As String = String.Empty
        Dim Linhas As Integer = 0
        Dim Cont As Integer = 0

        Banco.Empresa = Convert.ToInt32(cbEmpresa.SelectedValue)

        Ds = Banco.List("SELECT AUTONUM FROM " & Banco.BancoOPERADOR & "TB_PATIOS WHERE (COD_EMPRESA = " & Banco.Empresa & " OR 0 = " & Banco.Empresa & ") ORDER BY AUTONUM")

        For Each Linha As DataRow In Ds.Rows
            PatiosArr.Add(Linha("AUTONUM").ToString())
        Next

        Banco.Patios = String.Join(",", PatiosArr)

        FrmPrincipal.lblEmpresa.Text = cbEmpresa.Text
        FrmPrincipal.mnPrincipal.Enabled = True

        If FrmPrincipal.IsHandleCreated Then
            Me.Hide()
        Else
            FrmSplash.Show()
        End If

        Return True

    End Function

    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ActiveControl = txtUsuario

        cbEmpresa.DataSource = Banco.List("SELECT AUTONUM,NOME_FANTASIA FROM " & Banco.BancoSGIPA & "TB_EMPRESAS ORDER BY NOME_FANTASIA")
        cbEmpresa.SelectedIndex = 0

    End Sub

    Private Sub txtUsuario_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsuario.Leave
        txtSenha.Focus()
    End Sub

    Private Sub txtSenha_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSenha.Leave

        If Convert.ToInt32(Banco.ExecuteScalar("SELECT ISNULL(COD_EMPRESA,0) COD_EMPRESA FROM " & Banco.BancoSGIPA & "TB_CAD_USUARIOS WHERE USUARIO = '" & txtUsuario.Text & "'")) = 0 Then
            cbEmpresa.Enabled = True
            cbEmpresa.Focus()
        End If

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class