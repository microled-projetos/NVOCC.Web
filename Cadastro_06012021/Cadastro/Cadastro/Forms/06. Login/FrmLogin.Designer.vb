<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLogin))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.txtSenha = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblDadosIncorretos = New System.Windows.Forms.Label()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(118, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Usuário:"
        '
        'txtUsuario
        '
        Me.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUsuario.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuario.Location = New System.Drawing.Point(120, 129)
        Me.txtUsuario.MaxLength = 30
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(246, 23)
        Me.txtUsuario.TabIndex = 0
        '
        'txtSenha
        '
        Me.txtSenha.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSenha.Location = New System.Drawing.Point(120, 171)
        Me.txtSenha.MaxLength = 100
        Me.txtSenha.Name = "txtSenha"
        Me.txtSenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenha.Size = New System.Drawing.Size(246, 23)
        Me.txtSenha.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(117, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 16)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Senha:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSenha)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblDadosIncorretos)
        Me.Panel1.Controls.Add(Me.btnLogin)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtUsuario)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(488, 292)
        Me.Panel1.TabIndex = 4
        '
        'lblDadosIncorretos
        '
        Me.lblDadosIncorretos.AutoSize = True
        Me.lblDadosIncorretos.ForeColor = System.Drawing.Color.LightSalmon
        Me.lblDadosIncorretos.Location = New System.Drawing.Point(139, 241)
        Me.lblDadosIncorretos.Name = "lblDadosIncorretos"
        Me.lblDadosIncorretos.Size = New System.Drawing.Size(185, 13)
        Me.lblDadosIncorretos.TabIndex = 41
        Me.lblDadosIncorretos.Text = "Dados incorretos. Tente Novamente."
        Me.lblDadosIncorretos.Visible = False
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(197, 200)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(75, 26)
        Me.btnLogin.TabIndex = 3
        Me.btnLogin.Text = "Entrar"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(404, 272)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Versão: 1.0.0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 272)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Desenvolvido por: MicroLED"
        '
        'FrmLogin
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(488, 292)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "FrmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents txtSenha As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDadosIncorretos As System.Windows.Forms.Label
End Class
