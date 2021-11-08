<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImpressao
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmImpressao))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rbPaisagem = New System.Windows.Forms.RadioButton()
        Me.rbRetrato = New System.Windows.Forms.RadioButton()
        Me.btnRestaurarPadroes = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkAjustarColunas = New System.Windows.Forms.CheckBox()
        Me.chkMostrarNumCabecalho = New System.Windows.Forms.CheckBox()
        Me.chkMostrarNum = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lstColunas = New System.Windows.Forms.CheckedListBox()
        Me.btnVisualizarImpressao = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pbRodDir = New System.Windows.Forms.PictureBox()
        Me.pbRodCen = New System.Windows.Forms.PictureBox()
        Me.pbRodEsq = New System.Windows.Forms.PictureBox()
        Me.pbStDir = New System.Windows.Forms.PictureBox()
        Me.pbStCen = New System.Windows.Forms.PictureBox()
        Me.pbStEsq = New System.Windows.Forms.PictureBox()
        Me.pbTituloDir = New System.Windows.Forms.PictureBox()
        Me.pnTituloCen = New System.Windows.Forms.PictureBox()
        Me.pbTituloEsq = New System.Windows.Forms.PictureBox()
        Me.pbCorRodape = New System.Windows.Forms.PictureBox()
        Me.cbFonteRodape = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTamRodape = New System.Windows.Forms.NumericUpDown()
        Me.txtRodape = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pbCorSubTitulo = New System.Windows.Forms.PictureBox()
        Me.cbFonteSubTitulo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTamSubTitulo = New System.Windows.Forms.NumericUpDown()
        Me.txtSubTitulo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pbCorTitulo = New System.Windows.Forms.PictureBox()
        Me.cbFonteTitulo = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTamTitulo = New System.Windows.Forms.NumericUpDown()
        Me.txtTitulo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbRodDir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbRodCen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbRodEsq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStDir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStCen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStEsq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTituloDir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnTituloCen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTituloEsq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCorRodape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTamRodape, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCorSubTitulo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTamSubTitulo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCorTitulo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTamTitulo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Controls.Add(Me.btnRestaurarPadroes)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.btnVisualizarImpressao)
        Me.Panel2.Controls.Add(Me.btnSair)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(634, 412)
        Me.Panel2.TabIndex = 13
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rbPaisagem)
        Me.GroupBox4.Controls.Add(Me.rbRetrato)
        Me.GroupBox4.Location = New System.Drawing.Point(401, 295)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(206, 44)
        Me.GroupBox4.TabIndex = 57
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Layout"
        '
        'rbPaisagem
        '
        Me.rbPaisagem.AutoSize = True
        Me.rbPaisagem.Location = New System.Drawing.Point(109, 18)
        Me.rbPaisagem.Name = "rbPaisagem"
        Me.rbPaisagem.Size = New System.Drawing.Size(70, 17)
        Me.rbPaisagem.TabIndex = 1
        Me.rbPaisagem.Text = "Paisagem"
        Me.rbPaisagem.UseVisualStyleBackColor = True
        '
        'rbRetrato
        '
        Me.rbRetrato.AutoSize = True
        Me.rbRetrato.Checked = True
        Me.rbRetrato.Location = New System.Drawing.Point(44, 18)
        Me.rbRetrato.Name = "rbRetrato"
        Me.rbRetrato.Size = New System.Drawing.Size(62, 17)
        Me.rbRetrato.TabIndex = 0
        Me.rbRetrato.TabStop = True
        Me.rbRetrato.Text = "Retrato"
        Me.rbRetrato.UseVisualStyleBackColor = True
        '
        'btnRestaurarPadroes
        '
        Me.btnRestaurarPadroes.Image = CType(resources.GetObject("btnRestaurarPadroes.Image"), System.Drawing.Image)
        Me.btnRestaurarPadroes.Location = New System.Drawing.Point(219, 357)
        Me.btnRestaurarPadroes.Name = "btnRestaurarPadroes"
        Me.btnRestaurarPadroes.Size = New System.Drawing.Size(130, 30)
        Me.btnRestaurarPadroes.TabIndex = 56
        Me.btnRestaurarPadroes.Tag = ""
        Me.btnRestaurarPadroes.Text = "Restaurar Padrões"
        Me.btnRestaurarPadroes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnRestaurarPadroes, "Excluir o Registro Selecionado.")
        Me.btnRestaurarPadroes.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkAjustarColunas)
        Me.GroupBox3.Controls.Add(Me.chkMostrarNumCabecalho)
        Me.GroupBox3.Controls.Add(Me.chkMostrarNum)
        Me.GroupBox3.Location = New System.Drawing.Point(401, 215)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(206, 80)
        Me.GroupBox3.TabIndex = 55
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Configurações"
        '
        'chkAjustarColunas
        '
        Me.chkAjustarColunas.AutoSize = True
        Me.chkAjustarColunas.Checked = True
        Me.chkAjustarColunas.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAjustarColunas.Location = New System.Drawing.Point(17, 54)
        Me.chkAjustarColunas.Name = "chkAjustarColunas"
        Me.chkAjustarColunas.Size = New System.Drawing.Size(162, 17)
        Me.chkAjustarColunas.TabIndex = 2
        Me.chkAjustarColunas.Text = "Ajustar Largura das Colunas"
        Me.chkAjustarColunas.UseVisualStyleBackColor = True
        '
        'chkMostrarNumCabecalho
        '
        Me.chkMostrarNumCabecalho.AutoSize = True
        Me.chkMostrarNumCabecalho.Location = New System.Drawing.Point(17, 37)
        Me.chkMostrarNumCabecalho.Name = "chkMostrarNumCabecalho"
        Me.chkMostrarNumCabecalho.Size = New System.Drawing.Size(181, 17)
        Me.chkMostrarNumCabecalho.TabIndex = 1
        Me.chkMostrarNumCabecalho.Text = "Mostrar Numeração (Cabeçalho)"
        Me.chkMostrarNumCabecalho.UseVisualStyleBackColor = True
        '
        'chkMostrarNum
        '
        Me.chkMostrarNum.AutoSize = True
        Me.chkMostrarNum.Checked = True
        Me.chkMostrarNum.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMostrarNum.Location = New System.Drawing.Point(17, 20)
        Me.chkMostrarNum.Name = "chkMostrarNum"
        Me.chkMostrarNum.Size = New System.Drawing.Size(175, 17)
        Me.chkMostrarNum.TabIndex = 0
        Me.chkMostrarNum.Text = "Mostrar Numeração de Páginas"
        Me.chkMostrarNum.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.lstColunas)
        Me.GroupBox2.Location = New System.Drawing.Point(401, 24)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(206, 191)
        Me.GroupBox2.TabIndex = 54
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Colunas"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(14, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(184, 32)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "As colunas selecionadas serão apresentadas no relatório"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstColunas
        '
        Me.lstColunas.FormattingEnabled = True
        Me.lstColunas.Location = New System.Drawing.Point(17, 58)
        Me.lstColunas.Name = "lstColunas"
        Me.lstColunas.Size = New System.Drawing.Size(172, 116)
        Me.lstColunas.TabIndex = 0
        '
        'btnVisualizarImpressao
        '
        Me.btnVisualizarImpressao.Image = CType(resources.GetObject("btnVisualizarImpressao.Image"), System.Drawing.Image)
        Me.btnVisualizarImpressao.Location = New System.Drawing.Point(24, 357)
        Me.btnVisualizarImpressao.Name = "btnVisualizarImpressao"
        Me.btnVisualizarImpressao.Size = New System.Drawing.Size(190, 30)
        Me.btnVisualizarImpressao.TabIndex = 53
        Me.btnVisualizarImpressao.Tag = ""
        Me.btnVisualizarImpressao.Text = "Visualizar Impressão / Imprimir"
        Me.btnVisualizarImpressao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnVisualizarImpressao, "Excluir o Registro Selecionado.")
        Me.btnVisualizarImpressao.UseVisualStyleBackColor = True
        '
        'btnSair
        '
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.Location = New System.Drawing.Point(527, 357)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(80, 30)
        Me.btnSair.TabIndex = 7
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSair, "Fechar o formulário.")
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pbRodDir)
        Me.GroupBox1.Controls.Add(Me.pbRodCen)
        Me.GroupBox1.Controls.Add(Me.pbRodEsq)
        Me.GroupBox1.Controls.Add(Me.pbStDir)
        Me.GroupBox1.Controls.Add(Me.pbStCen)
        Me.GroupBox1.Controls.Add(Me.pbStEsq)
        Me.GroupBox1.Controls.Add(Me.pbTituloDir)
        Me.GroupBox1.Controls.Add(Me.pnTituloCen)
        Me.GroupBox1.Controls.Add(Me.pbTituloEsq)
        Me.GroupBox1.Controls.Add(Me.pbCorRodape)
        Me.GroupBox1.Controls.Add(Me.cbFonteRodape)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtTamRodape)
        Me.GroupBox1.Controls.Add(Me.txtRodape)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.pbCorSubTitulo)
        Me.GroupBox1.Controls.Add(Me.cbFonteSubTitulo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtTamSubTitulo)
        Me.GroupBox1.Controls.Add(Me.txtSubTitulo)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.pbCorTitulo)
        Me.GroupBox1.Controls.Add(Me.cbFonteTitulo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTamTitulo)
        Me.GroupBox1.Controls.Add(Me.txtTitulo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(373, 315)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'pbRodDir
        '
        Me.pbRodDir.BackColor = System.Drawing.Color.Gray
        Me.pbRodDir.Image = CType(resources.GetObject("pbRodDir.Image"), System.Drawing.Image)
        Me.pbRodDir.Location = New System.Drawing.Point(302, 273)
        Me.pbRodDir.Name = "pbRodDir"
        Me.pbRodDir.Size = New System.Drawing.Size(23, 22)
        Me.pbRodDir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbRodDir.TabIndex = 38
        Me.pbRodDir.TabStop = False
        '
        'pbRodCen
        '
        Me.pbRodCen.BackColor = System.Drawing.Color.Gray
        Me.pbRodCen.Image = CType(resources.GetObject("pbRodCen.Image"), System.Drawing.Image)
        Me.pbRodCen.Location = New System.Drawing.Point(277, 273)
        Me.pbRodCen.Name = "pbRodCen"
        Me.pbRodCen.Size = New System.Drawing.Size(22, 22)
        Me.pbRodCen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbRodCen.TabIndex = 37
        Me.pbRodCen.TabStop = False
        '
        'pbRodEsq
        '
        Me.pbRodEsq.BackColor = System.Drawing.Color.Maroon
        Me.pbRodEsq.Image = CType(resources.GetObject("pbRodEsq.Image"), System.Drawing.Image)
        Me.pbRodEsq.Location = New System.Drawing.Point(252, 273)
        Me.pbRodEsq.Name = "pbRodEsq"
        Me.pbRodEsq.Size = New System.Drawing.Size(22, 22)
        Me.pbRodEsq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbRodEsq.TabIndex = 36
        Me.pbRodEsq.TabStop = False
        '
        'pbStDir
        '
        Me.pbStDir.BackColor = System.Drawing.Color.Gray
        Me.pbStDir.Image = CType(resources.GetObject("pbStDir.Image"), System.Drawing.Image)
        Me.pbStDir.Location = New System.Drawing.Point(302, 182)
        Me.pbStDir.Name = "pbStDir"
        Me.pbStDir.Size = New System.Drawing.Size(23, 22)
        Me.pbStDir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbStDir.TabIndex = 35
        Me.pbStDir.TabStop = False
        '
        'pbStCen
        '
        Me.pbStCen.BackColor = System.Drawing.Color.Gray
        Me.pbStCen.Image = CType(resources.GetObject("pbStCen.Image"), System.Drawing.Image)
        Me.pbStCen.Location = New System.Drawing.Point(277, 182)
        Me.pbStCen.Name = "pbStCen"
        Me.pbStCen.Size = New System.Drawing.Size(22, 22)
        Me.pbStCen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbStCen.TabIndex = 34
        Me.pbStCen.TabStop = False
        '
        'pbStEsq
        '
        Me.pbStEsq.BackColor = System.Drawing.Color.Maroon
        Me.pbStEsq.Image = CType(resources.GetObject("pbStEsq.Image"), System.Drawing.Image)
        Me.pbStEsq.Location = New System.Drawing.Point(252, 182)
        Me.pbStEsq.Name = "pbStEsq"
        Me.pbStEsq.Size = New System.Drawing.Size(22, 22)
        Me.pbStEsq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbStEsq.TabIndex = 33
        Me.pbStEsq.TabStop = False
        '
        'pbTituloDir
        '
        Me.pbTituloDir.BackColor = System.Drawing.Color.Gray
        Me.pbTituloDir.Image = CType(resources.GetObject("pbTituloDir.Image"), System.Drawing.Image)
        Me.pbTituloDir.Location = New System.Drawing.Point(302, 88)
        Me.pbTituloDir.Name = "pbTituloDir"
        Me.pbTituloDir.Size = New System.Drawing.Size(23, 22)
        Me.pbTituloDir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbTituloDir.TabIndex = 32
        Me.pbTituloDir.TabStop = False
        '
        'pnTituloCen
        '
        Me.pnTituloCen.BackColor = System.Drawing.Color.Maroon
        Me.pnTituloCen.Image = CType(resources.GetObject("pnTituloCen.Image"), System.Drawing.Image)
        Me.pnTituloCen.Location = New System.Drawing.Point(277, 88)
        Me.pnTituloCen.Name = "pnTituloCen"
        Me.pnTituloCen.Size = New System.Drawing.Size(22, 22)
        Me.pnTituloCen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pnTituloCen.TabIndex = 31
        Me.pnTituloCen.TabStop = False
        '
        'pbTituloEsq
        '
        Me.pbTituloEsq.BackColor = System.Drawing.Color.Gray
        Me.pbTituloEsq.Image = CType(resources.GetObject("pbTituloEsq.Image"), System.Drawing.Image)
        Me.pbTituloEsq.Location = New System.Drawing.Point(252, 88)
        Me.pbTituloEsq.Name = "pbTituloEsq"
        Me.pbTituloEsq.Size = New System.Drawing.Size(22, 22)
        Me.pbTituloEsq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbTituloEsq.TabIndex = 30
        Me.pbTituloEsq.TabStop = False
        '
        'pbCorRodape
        '
        Me.pbCorRodape.Image = CType(resources.GetObject("pbCorRodape.Image"), System.Drawing.Image)
        Me.pbCorRodape.Location = New System.Drawing.Point(329, 273)
        Me.pbCorRodape.Name = "pbCorRodape"
        Me.pbCorRodape.Size = New System.Drawing.Size(21, 21)
        Me.pbCorRodape.TabIndex = 29
        Me.pbCorRodape.TabStop = False
        '
        'cbFonteRodape
        '
        Me.cbFonteRodape.DisplayMember = "DESCR"
        Me.cbFonteRodape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFonteRodape.FormattingEnabled = True
        Me.cbFonteRodape.Location = New System.Drawing.Point(22, 273)
        Me.cbFonteRodape.Name = "cbFonteRodape"
        Me.cbFonteRodape.Size = New System.Drawing.Size(168, 21)
        Me.cbFonteRodape.TabIndex = 7
        Me.cbFonteRodape.Tag = "requerido"
        Me.cbFonteRodape.ValueMember = "CODE"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 256)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Fonte:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(192, 257)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Tamanho:"
        '
        'txtTamRodape
        '
        Me.txtTamRodape.Location = New System.Drawing.Point(195, 273)
        Me.txtTamRodape.Name = "txtTamRodape"
        Me.txtTamRodape.Size = New System.Drawing.Size(52, 21)
        Me.txtTamRodape.TabIndex = 8
        Me.txtTamRodape.Value = New Decimal(New Integer() {12, 0, 0, 0})
        '
        'txtRodape
        '
        Me.txtRodape.Location = New System.Drawing.Point(22, 227)
        Me.txtRodape.Name = "txtRodape"
        Me.txtRodape.Size = New System.Drawing.Size(328, 21)
        Me.txtRodape.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 211)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Rodapé:"
        '
        'pbCorSubTitulo
        '
        Me.pbCorSubTitulo.Image = CType(resources.GetObject("pbCorSubTitulo.Image"), System.Drawing.Image)
        Me.pbCorSubTitulo.Location = New System.Drawing.Point(329, 182)
        Me.pbCorSubTitulo.Name = "pbCorSubTitulo"
        Me.pbCorSubTitulo.Size = New System.Drawing.Size(21, 21)
        Me.pbCorSubTitulo.TabIndex = 22
        Me.pbCorSubTitulo.TabStop = False
        '
        'cbFonteSubTitulo
        '
        Me.cbFonteSubTitulo.DisplayMember = "DESCR"
        Me.cbFonteSubTitulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFonteSubTitulo.FormattingEnabled = True
        Me.cbFonteSubTitulo.Location = New System.Drawing.Point(22, 182)
        Me.cbFonteSubTitulo.Name = "cbFonteSubTitulo"
        Me.cbFonteSubTitulo.Size = New System.Drawing.Size(168, 21)
        Me.cbFonteSubTitulo.TabIndex = 4
        Me.cbFonteSubTitulo.Tag = "requerido"
        Me.cbFonteSubTitulo.ValueMember = "CODE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 165)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Fonte:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(192, 166)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Tamanho:"
        '
        'txtTamSubTitulo
        '
        Me.txtTamSubTitulo.Location = New System.Drawing.Point(195, 182)
        Me.txtTamSubTitulo.Name = "txtTamSubTitulo"
        Me.txtTamSubTitulo.Size = New System.Drawing.Size(52, 21)
        Me.txtTamSubTitulo.TabIndex = 5
        Me.txtTamSubTitulo.Value = New Decimal(New Integer() {12, 0, 0, 0})
        '
        'txtSubTitulo
        '
        Me.txtSubTitulo.Location = New System.Drawing.Point(22, 136)
        Me.txtSubTitulo.Name = "txtSubTitulo"
        Me.txtSubTitulo.Size = New System.Drawing.Size(328, 21)
        Me.txtSubTitulo.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Sub Título:"
        '
        'pbCorTitulo
        '
        Me.pbCorTitulo.Image = CType(resources.GetObject("pbCorTitulo.Image"), System.Drawing.Image)
        Me.pbCorTitulo.Location = New System.Drawing.Point(329, 88)
        Me.pbCorTitulo.Name = "pbCorTitulo"
        Me.pbCorTitulo.Size = New System.Drawing.Size(21, 21)
        Me.pbCorTitulo.TabIndex = 15
        Me.pbCorTitulo.TabStop = False
        '
        'cbFonteTitulo
        '
        Me.cbFonteTitulo.DisplayMember = "DESCR"
        Me.cbFonteTitulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFonteTitulo.FormattingEnabled = True
        Me.cbFonteTitulo.Location = New System.Drawing.Point(22, 88)
        Me.cbFonteTitulo.Name = "cbFonteTitulo"
        Me.cbFonteTitulo.Size = New System.Drawing.Size(168, 21)
        Me.cbFonteTitulo.TabIndex = 1
        Me.cbFonteTitulo.Tag = "requerido"
        Me.cbFonteTitulo.ValueMember = "CODE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Fonte:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(192, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Tamanho:"
        '
        'txtTamTitulo
        '
        Me.txtTamTitulo.Location = New System.Drawing.Point(195, 89)
        Me.txtTamTitulo.Name = "txtTamTitulo"
        Me.txtTamTitulo.Size = New System.Drawing.Size(52, 21)
        Me.txtTamTitulo.TabIndex = 2
        Me.txtTamTitulo.Value = New Decimal(New Integer() {14, 0, 0, 0})
        '
        'txtTitulo
        '
        Me.txtTitulo.Location = New System.Drawing.Point(22, 42)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(328, 21)
        Me.txtTitulo.TabIndex = 0
        Me.txtTitulo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Título:"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'FrmImpressao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(634, 412)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "FrmImpressao"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gerenciador de Impressão"
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pbRodDir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbRodCen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbRodEsq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStDir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStCen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStEsq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTituloDir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnTituloCen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTituloEsq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCorRodape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTamRodape, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCorSubTitulo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTamSubTitulo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCorTitulo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTamTitulo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSair As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnVisualizarImpressao As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkMostrarNum As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstColunas As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkMostrarNumCabecalho As System.Windows.Forms.CheckBox
    Friend WithEvents chkAjustarColunas As System.Windows.Forms.CheckBox
    Friend WithEvents pbCorRodape As System.Windows.Forms.PictureBox
    Friend WithEvents cbFonteRodape As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTamRodape As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtRodape As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pbCorSubTitulo As System.Windows.Forms.PictureBox
    Friend WithEvents cbFonteSubTitulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTamSubTitulo As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtSubTitulo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pbCorTitulo As System.Windows.Forms.PictureBox
    Friend WithEvents cbFonteTitulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTamTitulo As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTitulo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pbTituloDir As System.Windows.Forms.PictureBox
    Friend WithEvents pnTituloCen As System.Windows.Forms.PictureBox
    Friend WithEvents pbTituloEsq As System.Windows.Forms.PictureBox
    Friend WithEvents pbRodDir As System.Windows.Forms.PictureBox
    Friend WithEvents pbRodCen As System.Windows.Forms.PictureBox
    Friend WithEvents pbRodEsq As System.Windows.Forms.PictureBox
    Friend WithEvents pbStDir As System.Windows.Forms.PictureBox
    Friend WithEvents pbStCen As System.Windows.Forms.PictureBox
    Friend WithEvents pbStEsq As System.Windows.Forms.PictureBox
    Friend WithEvents btnRestaurarPadroes As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rbPaisagem As System.Windows.Forms.RadioButton
    Friend WithEvents rbRetrato As System.Windows.Forms.RadioButton

End Class
