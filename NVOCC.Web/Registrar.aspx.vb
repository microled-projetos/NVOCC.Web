Public Class Registrar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'seta valor incial para session
        Session("Logado") = "False"
    End Sub


    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click

        msgErro.Visible = False
        divmsg.Visible = False
        Dim Criptografar As New Criptografia
        Dim Con As New Conexao_sql
        Dim ds As DataSet

        Dim Nome As String = txtNome.Text
        Dim Login As String = txtLogin.Text
        Dim Senha As String = txtSenha.Text
        Dim SenhaConfirmada As String = txtSenhaConfirmada.Text
        Dim CPF As String = txtCPF.Text
        Dim Email As String = txtEmail.Text

        'compara senhas 
        If Senha <> SenhaConfirmada Then
            msgErro.Visible = True
            lblMsg.Text = "Os campos de senha não correspondem"
        Else
            'Valida CPF
            If ValidaCPF.Validar(txtCPF.Text) = False Then
                msgErro.Visible = True
                lblMsg.Text = "CPF inválido"

                'Valida Email
            ElseIf ValidaEmail.Validar(txtEmail.Text) = False Then
                msgErro.Visible = True
                lblMsg.Text = "Email inválido"

            Else
                'Verifica se o login já existe
                Con.Conectar()
                ds = Con.ExecutarQuery("SELECT LOGIN FROM [TB_USUARIO] where LOGIN = '" & Login & "'")
                If ds.Tables(0).Rows.Count > 0 Then
                    msgErro.Visible = True
                    lblMsg.Text = "Já existe usuário cadastrado com esse login"

                Else
                    'Verifica se o email já existe
                    ds = Con.ExecutarQuery("SELECT EMAIL FROM [TB_USUARIO] where EMAIL = '" & Email & "'")
                    If ds.Tables(0).Rows.Count > 0 Then
                        msgErro.Visible = True
                        lblMsg.Text = "Já existe usuário cadastrado com esse email"
                    Else
                        'Verifica se o CPF já existe
                        ds = Con.ExecutarQuery("SELECT CPF FROM [TB_USUARIO] where CPF = '" & CPF & "'")
                        If ds.Tables(0).Rows.Count > 0 Then
                            msgErro.Visible = True
                            lblMsg.Text = "Já existe usuário cadastrado com esse CPF"

                        Else
                            'Criptografa senha e insere dados no banco
                            Senha = Criptografar.CriptografarSenha(txtSenha.Text)
                            Con.ExecutarQuery("INSERT INTO 
                        [dbo].[TB_USUARIO] 
                            (   [Login],
                                [Senha],
                                [Nome],
                                [Cpf],
                                [Email],
                                [ID_TIPO_USUARIO],
                                [DT_CADASTRO],
                                [FL_ATIVO]
                            ) VALUES (
                                '" & Login & "', 
                                '" & Senha & "', 
                                '" & Nome & "', 
                                '" & CPF & "', 
                                '" & Email & "', 
                                0,
                                Getdate(),
                                0
                            ); SELECT CAST(SCOPE_IDENTITY() AS INT)")


                            txtNome.Text = ""
                            txtLogin.Text = ""
                            txtSenha.Text = ""
                            txtSenhaConfirmada.Text = ""
                            txtCPF.Text = ""
                            txtEmail.Text = ""
                            divmsg.Visible = True

                            Con.Fechar()

                        End If
                    End If
                End If

            End If

        End If

    End Sub

End Class