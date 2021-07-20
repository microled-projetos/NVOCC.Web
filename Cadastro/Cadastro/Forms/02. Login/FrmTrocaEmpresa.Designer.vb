<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTrocaEmpresa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTrocaEmpresa))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.btnAlteraSenha = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbEmpresa = New System.Windows.Forms.ComboBox()
        Me.lblDadosIncorretos = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.lblEmpresa)
        Me.Panel1.Controls.Add(Me.btnAlteraSenha)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cbEmpresa)
        Me.Panel1.Controls.Add(Me.lblDadosIncorretos)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(526, 332)
        Me.Panel1.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Location = New System.Drawing.Point(305, 32)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(153, 50)
        Me.PictureBox1.TabIndex = 48
        Me.PictureBox1.TabStop = False
        '
        'lblEmpresa
        '
        Me.lblEmpresa.AutoSize = True
        Me.lblEmpresa.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpresa.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresa.ForeColor = System.Drawing.Color.White
        Me.lblEmpresa.Location = New System.Drawing.Point(129, 169)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(58, 16)
        Me.lblEmpresa.TabIndex = 47
        Me.lblEmpresa.Text = "Empresa"
        Me.lblEmpresa.Visible = False
        '
        'btnAlteraSenha
        '
        Me.btnAlteraSenha.Location = New System.Drawing.Point(305, 221)
        Me.btnAlteraSenha.Name = "btnAlteraSenha"
        Me.btnAlteraSenha.Size = New System.Drawing.Size(94, 23)
        Me.btnAlteraSenha.TabIndex = 45
        Me.btnAlteraSenha.Text = "Alterar Empresa"
        Me.btnAlteraSenha.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(129, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 16)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Empresa:"
        '
        'cbEmpresa
        '
        Me.cbEmpresa.DisplayMember = "NOME_FANTASIA"
        Me.cbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEmpresa.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEmpresa.FormattingEnabled = True
        Me.cbEmpresa.Location = New System.Drawing.Point(132, 187)
        Me.cbEmpresa.Name = "cbEmpresa"
        Me.cbEmpresa.Size = New System.Drawing.Size(267, 24)
        Me.cbEmpresa.TabIndex = 42
        Me.cbEmpresa.Tag = "requerido"
        Me.cbEmpresa.ValueMember = "AUTONUM"
        '
        'lblDadosIncorretos
        '
        Me.lblDadosIncorretos.AutoSize = True
        Me.lblDadosIncorretos.ForeColor = System.Drawing.Color.LightSalmon
        Me.lblDadosIncorretos.Location = New System.Drawing.Point(130, 221)
        Me.lblDadosIncorretos.Name = "lblDadosIncorretos"
        Me.lblDadosIncorretos.Size = New System.Drawing.Size(119, 13)
        Me.lblDadosIncorretos.TabIndex = 41
        Me.lblDadosIncorretos.Text = "Selecione uma Empresa"
        Me.lblDadosIncorretos.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(449, 315)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Versão: 1.0.0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(2, 315)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Desenvolvido por: MicroLED"
        '
        'FrmTrocaEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(526, 332)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "FrmTrocaEmpresa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDadosIncorretos As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbEmpresa As System.Windows.Forms.ComboBox
    Friend WithEvents lblEmpresa As Label
    Friend WithEvents btnAlteraSenha As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
