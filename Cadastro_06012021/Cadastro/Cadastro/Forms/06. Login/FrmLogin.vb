Imports System.Security.Cryptography
Imports System.Text
Imports System
Imports System.Text.StringBuilder
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
        Dim Ds As New DataTable
        Dim senha As String = CriptografarSenha(txtSenha.Text)

        Ds = Banco.List("SELECT ID_USUARIO, ISNULL(FL_ATIVO,0), LOGIN,ID_TIPO_USUARIO FROM TB_USUARIO WHERE LOGIN = '" & txtUsuario.Text.ToUpper() & "' AND SENHA = '" & senha & "' ")

        If Ds.Rows.Count > 0 Then

            Banco.UsuarioSistema = Convert.ToInt32(Ds.Rows(0)("ID_USUARIO").ToString())
            Banco.TipoUsuario = Convert.ToInt32(Ds.Rows(0)("ID_TIPO_USUARIO").ToString())


            If Ds.Rows.Count > 0 Then
                Cod_Usuario = Banco.UsuarioSistema
                Usuario_Sistema = Ds.Rows(0)("LOGIN").ToString
            End If

            Me.Hide()
            lblDadosIncorretos.Visible = False
            FrmPrincipal.lblUsuario.Text = Me.txtUsuario.Text

            FrmPrincipal.mnPrincipal.Enabled = True

            If FrmPrincipal.IsHandleCreated Then
                Me.Hide()
            Else
                FrmSplash.Show()
            End If

        Else
            Return False

        End If
        Return True

    End Function

    Public Function CriptografarSenha(pass)
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
    End Class