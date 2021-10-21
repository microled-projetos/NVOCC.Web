<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrincipal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPrincipal))
        Me.mnPrincipal = New System.Windows.Forms.MenuStrip()
        Me.MnuCadastros = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnNavios = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnNCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnPortos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnCidades = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnPais = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnEstados = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnServicos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnEventos = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnContas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnMoedas = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnCadConteiner = New System.Windows.Forms.ToolStripMenuItem()
        Me.Mnusair = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblUsuario = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblEmpresa = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblBase = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblIP = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblData = New System.Windows.Forms.Label()
        Me.lblEstacao = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Printer = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.mnPrincipal.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnPrincipal
        '
        Me.mnPrincipal.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.mnPrincipal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnPrincipal.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuCadastros, Me.Mnusair})
        Me.mnPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.mnPrincipal.Name = "mnPrincipal"
        Me.mnPrincipal.Size = New System.Drawing.Size(987, 24)
        Me.mnPrincipal.TabIndex = 0
        Me.mnPrincipal.Text = "MenuStrip1"
        '
        'MnuCadastros
        '
        Me.MnuCadastros.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnNavios, Me.mnNCM, Me.mnPortos, Me.mnCidades, Me.mnPais, Me.mnEstados, Me.mnServicos, Me.mnEventos, Me.mnContas, Me.mnMoedas, Me.mnCadConteiner})
        Me.MnuCadastros.Name = "MnuCadastros"
        Me.MnuCadastros.Size = New System.Drawing.Size(71, 20)
        Me.MnuCadastros.Text = "&Cadastros"
        '
        'mnNavios
        '
        Me.mnNavios.Name = "mnNavios"
        Me.mnNavios.Size = New System.Drawing.Size(180, 22)
        Me.mnNavios.Text = "&Navios"
        '
        'mnNCM
        '
        Me.mnNCM.Name = "mnNCM"
        Me.mnNCM.Size = New System.Drawing.Size(180, 22)
        Me.mnNCM.Text = "NCM"
        '
        'mnPortos
        '
        Me.mnPortos.Name = "mnPortos"
        Me.mnPortos.Size = New System.Drawing.Size(180, 22)
        Me.mnPortos.Text = "Portos"
        '
        'mnCidades
        '
        Me.mnCidades.Name = "mnCidades"
        Me.mnCidades.Size = New System.Drawing.Size(180, 22)
        Me.mnCidades.Text = "Cidades"
        '
        'mnPais
        '
        Me.mnPais.Name = "mnPais"
        Me.mnPais.Size = New System.Drawing.Size(180, 22)
        Me.mnPais.Text = "Países"
        '
        'mnEstados
        '
        Me.mnEstados.Name = "mnEstados"
        Me.mnEstados.Size = New System.Drawing.Size(180, 22)
        Me.mnEstados.Text = "Estados"
        '
        'mnServicos
        '
        Me.mnServicos.Name = "mnServicos"
        Me.mnServicos.Size = New System.Drawing.Size(180, 22)
        Me.mnServicos.Text = "Serviços"
        '
        'mnEventos
        '
        Me.mnEventos.Name = "mnEventos"
        Me.mnEventos.Size = New System.Drawing.Size(180, 22)
        Me.mnEventos.Text = "Eventos"
        '
        'mnContas
        '
        Me.mnContas.Name = "mnContas"
        Me.mnContas.Size = New System.Drawing.Size(180, 22)
        Me.mnContas.Text = "Contas"
        '
        'mnMoedas
        '
        Me.mnMoedas.Name = "mnMoedas"
        Me.mnMoedas.Size = New System.Drawing.Size(180, 22)
        Me.mnMoedas.Text = "Moedas"
        '
        'mnCadConteiner
        '
        Me.mnCadConteiner.Name = "mnCadConteiner"
        Me.mnCadConteiner.Size = New System.Drawing.Size(180, 22)
        Me.mnCadConteiner.Text = "Container"
        '
        'Mnusair
        '
        Me.Mnusair.Name = "Mnusair"
        Me.Mnusair.Size = New System.Drawing.Size(38, 20)
        Me.Mnusair.Text = "&Sair"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.lblUsuario, Me.ToolStripStatusLabel2, Me.lblEmpresa, Me.ToolStripStatusLabel3, Me.lblBase})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 528)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(987, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(50, 17)
        Me.ToolStripStatusLabel1.Text = "Usuário:"
        '
        'lblUsuario
        '
        Me.lblUsuario.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(57, 17)
        Me.ToolStripStatusLabel2.Text = "Empresa:"
        '
        'lblEmpresa
        '
        Me.lblEmpresa.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(36, 17)
        Me.ToolStripStatusLabel3.Text = "Base:"
        '
        'lblBase
        '
        Me.lblBase.BackColor = System.Drawing.SystemColors.Control
        Me.lblBase.Name = "lblBase"
        Me.lblBase.Size = New System.Drawing.Size(0, 17)
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(869, 533)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "IP:"
        '
        'lblIP
        '
        Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIP.AutoSize = True
        Me.lblIP.BackColor = System.Drawing.SystemColors.Control
        Me.lblIP.Location = New System.Drawing.Point(886, 533)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(91, 13)
        Me.lblIP.TabIndex = 4
        Me.lblIP.Text = "192.168.200.111"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(592, 533)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Data:"
        '
        'lblData
        '
        Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblData.AutoSize = True
        Me.lblData.BackColor = System.Drawing.SystemColors.Control
        Me.lblData.Location = New System.Drawing.Point(622, 533)
        Me.lblData.Name = "lblData"
        Me.lblData.Size = New System.Drawing.Size(63, 13)
        Me.lblData.TabIndex = 6
        Me.lblData.Text = "00/00/0000"
        '
        'lblEstacao
        '
        Me.lblEstacao.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEstacao.AutoSize = True
        Me.lblEstacao.BackColor = System.Drawing.SystemColors.Control
        Me.lblEstacao.Location = New System.Drawing.Point(769, 533)
        Me.lblEstacao.Name = "lblEstacao"
        Me.lblEstacao.Size = New System.Drawing.Size(62, 13)
        Me.lblEstacao.TabIndex = 8
        Me.lblEstacao.Text = "COMPUTER"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(721, 533)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Estação:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.Document = Me.Printer
        Me.PrintDialog1.UseEXDialog = True
        '
        'pbLogo
        '
        Me.pbLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), System.Drawing.Image)
        Me.pbLogo.Location = New System.Drawing.Point(632, 13)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(355, 537)
        Me.pbLogo.TabIndex = 9
        Me.pbLogo.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pbLogo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(987, 550)
        Me.Panel1.TabIndex = 10
        '
        'FrmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(987, 550)
        Me.Controls.Add(Me.lblEstacao)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblData)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblIP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.mnPrincipal)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnPrincipal
        Me.Name = "FrmPrincipal"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.mnPrincipal.ResumeLayout(False)
        Me.mnPrincipal.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnPrincipal As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuCadastros As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUsuario As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblIP As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblData As System.Windows.Forms.Label
    Friend WithEvents lblEstacao As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Printer As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents mnNavios As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnPortos As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Mnusair As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblEmpresa As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblBase As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents mnNCM As ToolStripMenuItem
    Friend WithEvents mnCidades As ToolStripMenuItem
    Friend WithEvents mnPais As ToolStripMenuItem
    Friend WithEvents mnEstados As ToolStripMenuItem
    Friend WithEvents mnServicos As ToolStripMenuItem
    Friend WithEvents mnEventos As ToolStripMenuItem
    Friend WithEvents mnContas As ToolStripMenuItem
    Friend WithEvents mnMoedas As ToolStripMenuItem
    Friend WithEvents mnCadConteiner As ToolStripMenuItem
End Class
