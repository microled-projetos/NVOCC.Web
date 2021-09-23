Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualBasic.CompilerServices
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

        If Not Logou() Then
            MessageBox.Show("Usuário ou senha inválidos!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Function Logou() As Boolean

        Dim Ds As DataTable = New DataTable()
        Dim senha As String = Conversions.ToString(CriptografarSenha(txtSenha.Text))
        Ds = Banco.List("SELECT ID_USUARIO, ISNULL(FL_ATIVO,0), LOGIN,ID_TIPO_USUARIO FROM TB_USUARIO WHERE LOGIN = '" & txtUsuario.Text.ToUpper() & "' AND SENHA = '" & senha & "' ")
        If Ds.Rows.Count > 0 Then

            Dim x As String = Ds.Rows(0).Item("ID_USUARIO").ToString()
            Banco.UsuarioSistema = Convert.ToInt32(Ds.Rows(0).Item("ID_USUARIO").ToString())
            Banco.TipoUsuario = Convert.ToInt32(Ds.Rows(0).Item("ID_TIPO_USUARIO").ToString())



            Geral.Cod_Usuario = Banco.UsuarioSistema
            Geral.Usuario_Sistema = Ds.Rows(0).Item("LOGIN").ToString()
            lblDadosIncorretos.Visible = False
            FrmPrincipal.lblUsuario.Text = txtUsuario.Text
            FrmPrincipal.mnPrincipal.Enabled = True
            If (FrmPrincipal.IsHandleCreated) Then
                Me.Hide()

            Else
                Me.Hide()
                FrmSplash.Show()
            End If
            Return True
        End If
        Return False

    End Function

    Public Function CriptografarSenha(ByVal pass As Object) As Object
        If pass = "" Then
            Return ""
        Else
            Using hasher As MD5 = MD5.Create()
                Dim dbytes As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(pass))

                Dim sBuilder As New StringBuilder()

                For n As Integer = 0 To dbytes.Length - 1
                    sBuilder.Append(dbytes(n).ToString("X2"))
                Next n
                Return sBuilder.ToString()
            End Using
        End If
    End Function


    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.ActiveControl = txtUsuario

        'cbEmpresa.DataSource = Banco.List("SELECT AUTONUM,NOME_FANTASIA FROM " & Banco.BancoSGIPA & "TB_EMPRESAS ORDER BY NOME_FANTASIA")
        'cbEmpresa.SelectedIndex = 0

    End Sub

    Private Sub txtUsuario_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsuario.Leave
        txtSenha.Focus()
    End Sub


    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class