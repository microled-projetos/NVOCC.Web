<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTiposConteiner
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTiposConteiner))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnFiltro = New System.Windows.Forms.Button()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NM_TIPO_CONTAINER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ISO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MAXGROSS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TEU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TAMANHO_CONTAINER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ativo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMAXGROSS = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTamanho = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTEU = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtISO = New System.Windows.Forms.TextBox()
        Me.chkAtivo = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnExcluir)
        Me.Panel2.Controls.Add(Me.btnNovo)
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnSalvar)
        Me.Panel2.Controls.Add(Me.btnEditar)
        Me.Panel2.Controls.Add(Me.btnSair)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.dgvConsulta)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.txtID)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(804, 556)
        Me.Panel2.TabIndex = 13
        '
        'btnExcluir
        '
        Me.btnExcluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcluir.Image = CType(resources.GetObject("btnExcluir.Image"), System.Drawing.Image)
        Me.btnExcluir.Location = New System.Drawing.Point(268, 490)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(79, 34)
        Me.btnExcluir.TabIndex = 78
        Me.btnExcluir.Tag = "Excluir o Registro Selecionado"
        Me.btnExcluir.Text = "Excluir"
        Me.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnExcluir, "Excluir o Registro Selecionado.")
        Me.btnExcluir.UseVisualStyleBackColor = True
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = CType(resources.GetObject("btnNovo.Image"), System.Drawing.Image)
        Me.btnNovo.Location = New System.Drawing.Point(13, 490)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(79, 34)
        Me.btnNovo.TabIndex = 77
        Me.btnNovo.Tag = "Adiciona um novo registro"
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnNovo, "Inclui um novo registro.")
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(353, 490)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 34)
        Me.btnCancelar.TabIndex = 76
        Me.btnCancelar.Tag = "Cancelar"
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnCancelar, "Cancela as alterações")
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Enabled = False
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(183, 490)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(79, 34)
        Me.btnSalvar.TabIndex = 73
        Me.btnSalvar.Tag = "Salva todas as mudanças"
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSalvar, "Salva todas as modificações.")
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEditar.Image = CType(resources.GetObject("btnEditar.Image"), System.Drawing.Image)
        Me.btnEditar.Location = New System.Drawing.Point(98, 490)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(79, 34)
        Me.btnEditar.TabIndex = 72
        Me.btnEditar.Tag = "Editar Registro Selecionado"
        Me.btnEditar.Text = "Editar"
        Me.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.Location = New System.Drawing.Point(674, 490)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(106, 34)
        Me.btnSair.TabIndex = 70
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSair, "Fechar o formulário.")
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnFiltro)
        Me.Panel3.Controls.Add(Me.btnUltimo)
        Me.Panel3.Controls.Add(Me.btnProximo)
        Me.Panel3.Controls.Add(Me.btnAnterior)
        Me.Panel3.Controls.Add(Me.btnPrimeiro)
        Me.Panel3.Location = New System.Drawing.Point(15, 206)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(765, 22)
        Me.Panel3.TabIndex = 11
        '
        'btnFiltro
        '
        Me.btnFiltro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltro.FlatAppearance.BorderSize = 0
        Me.btnFiltro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFiltro.Image = CType(resources.GetObject("btnFiltro.Image"), System.Drawing.Image)
        Me.btnFiltro.Location = New System.Drawing.Point(647, -2)
        Me.btnFiltro.Name = "btnFiltro"
        Me.btnFiltro.Size = New System.Drawing.Size(29, 25)
        Me.btnFiltro.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnFiltro, "Filtrar os registros da Tabela")
        Me.btnFiltro.UseVisualStyleBackColor = True
        Me.btnFiltro.Visible = False
        '
        'btnUltimo
        '
        Me.btnUltimo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUltimo.FlatAppearance.BorderSize = 0
        Me.btnUltimo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUltimo.Image = CType(resources.GetObject("btnUltimo.Image"), System.Drawing.Image)
        Me.btnUltimo.Location = New System.Drawing.Point(733, -2)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(29, 25)
        Me.btnUltimo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btnUltimo, "Vai até o último registro.")
        Me.btnUltimo.UseVisualStyleBackColor = True
        '
        'btnProximo
        '
        Me.btnProximo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnProximo.FlatAppearance.BorderSize = 0
        Me.btnProximo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnProximo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnProximo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProximo.Image = CType(resources.GetObject("btnProximo.Image"), System.Drawing.Image)
        Me.btnProximo.Location = New System.Drawing.Point(715, -2)
        Me.btnProximo.Name = "btnProximo"
        Me.btnProximo.Size = New System.Drawing.Size(29, 25)
        Me.btnProximo.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnProximo, "Vai até o próximo registro.")
        Me.btnProximo.UseVisualStyleBackColor = True
        '
        'btnAnterior
        '
        Me.btnAnterior.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnterior.FlatAppearance.BorderSize = 0
        Me.btnAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnterior.Image = CType(resources.GetObject("btnAnterior.Image"), System.Drawing.Image)
        Me.btnAnterior.Location = New System.Drawing.Point(697, -2)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(29, 25)
        Me.btnAnterior.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnAnterior, "Vai até o registro anterior.")
        Me.btnAnterior.UseVisualStyleBackColor = True
        '
        'btnPrimeiro
        '
        Me.btnPrimeiro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrimeiro.FlatAppearance.BorderSize = 0
        Me.btnPrimeiro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrimeiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrimeiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrimeiro.Image = CType(resources.GetObject("btnPrimeiro.Image"), System.Drawing.Image)
        Me.btnPrimeiro.Location = New System.Drawing.Point(677, -2)
        Me.btnPrimeiro.Name = "btnPrimeiro"
        Me.btnPrimeiro.Size = New System.Drawing.Size(29, 25)
        Me.btnPrimeiro.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnPrimeiro, "Vai até o primeiro registro.")
        Me.btnPrimeiro.UseVisualStyleBackColor = True
        '
        'dgvConsulta
        '
        Me.dgvConsulta.AllowUserToAddRows = False
        Me.dgvConsulta.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvConsulta.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvConsulta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvConsulta.BackgroundColor = System.Drawing.Color.White
        Me.dgvConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCodigo, Me.NM_TIPO_CONTAINER, Me.ISO, Me.MAXGROSS, Me.TEU, Me.TAMANHO_CONTAINER, Me.Ativo})
        Me.dgvConsulta.Location = New System.Drawing.Point(15, 236)
        Me.dgvConsulta.Name = "dgvConsulta"
        Me.dgvConsulta.ReadOnly = True
        Me.dgvConsulta.RowHeadersWidth = 24
        Me.dgvConsulta.RowTemplate.Height = 18
        Me.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConsulta.Size = New System.Drawing.Size(765, 248)
        Me.dgvConsulta.TabIndex = 1
        '
        'colCodigo
        '
        Me.colCodigo.DataPropertyName = "ID_TIPO_CONTAINER"
        Me.colCodigo.HeaderText = "Código"
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.ReadOnly = True
        Me.colCodigo.Width = 60
        '
        'NM_TIPO_CONTAINER
        '
        Me.NM_TIPO_CONTAINER.DataPropertyName = "NM_TIPO_CONTAINER"
        Me.NM_TIPO_CONTAINER.HeaderText = "Descrição"
        Me.NM_TIPO_CONTAINER.Name = "NM_TIPO_CONTAINER"
        Me.NM_TIPO_CONTAINER.ReadOnly = True
        Me.NM_TIPO_CONTAINER.Width = 200
        '
        'ISO
        '
        Me.ISO.DataPropertyName = "ISO"
        Me.ISO.HeaderText = "ISO"
        Me.ISO.Name = "ISO"
        Me.ISO.ReadOnly = True
        '
        'MAXGROSS
        '
        Me.MAXGROSS.DataPropertyName = "MAXGROSS"
        Me.MAXGROSS.HeaderText = "MAXGROSS"
        Me.MAXGROSS.Name = "MAXGROSS"
        Me.MAXGROSS.ReadOnly = True
        '
        'TEU
        '
        Me.TEU.DataPropertyName = "TEU"
        Me.TEU.HeaderText = "TEU"
        Me.TEU.Name = "TEU"
        Me.TEU.ReadOnly = True
        Me.TEU.Width = 50
        '
        'TAMANHO_CONTAINER
        '
        Me.TAMANHO_CONTAINER.DataPropertyName = "TAMANHO_CONTAINER"
        Me.TAMANHO_CONTAINER.HeaderText = "Tamanho"
        Me.TAMANHO_CONTAINER.Name = "TAMANHO_CONTAINER"
        Me.TAMANHO_CONTAINER.ReadOnly = True
        '
        'Ativo
        '
        Me.Ativo.DataPropertyName = "FL_ATIVO"
        Me.Ativo.HeaderText = "Ativo"
        Me.Ativo.Name = "Ativo"
        Me.Ativo.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtMAXGROSS)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTamanho)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtTEU)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtISO)
        Me.GroupBox1.Controls.Add(Me.chkAtivo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtDescricao)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(763, 182)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(61, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(16, 16)
        Me.Label8.TabIndex = 112
        Me.Label8.Text = "*"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(543, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(16, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(94, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 16)
        Me.Label6.TabIndex = 110
        Me.Label6.Text = "*"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(483, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "MAXGROSS"
        '
        'txtMAXGROSS
        '
        Me.txtMAXGROSS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMAXGROSS.Enabled = False
        Me.txtMAXGROSS.Location = New System.Drawing.Point(487, 111)
        Me.txtMAXGROSS.MaxLength = 50
        Me.txtMAXGROSS.Name = "txtMAXGROSS"
        Me.txtMAXGROSS.Size = New System.Drawing.Size(87, 23)
        Me.txtMAXGROSS.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(483, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Tamanho:"
        '
        'txtTamanho
        '
        Me.txtTamanho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTamanho.Enabled = False
        Me.txtTamanho.Location = New System.Drawing.Point(487, 58)
        Me.txtTamanho.MaxLength = 50
        Me.txtTamanho.Name = "txtTamanho"
        Me.txtTamanho.Size = New System.Drawing.Size(200, 23)
        Me.txtTamanho.TabIndex = 12
        Me.txtTamanho.Tag = "requerido"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 16)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "TEU:"
        '
        'txtTEU
        '
        Me.txtTEU.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTEU.Enabled = False
        Me.txtTEU.Location = New System.Drawing.Point(36, 111)
        Me.txtTEU.MaxLength = 1
        Me.txtTEU.Name = "txtTEU"
        Me.txtTEU.Size = New System.Drawing.Size(87, 23)
        Me.txtTEU.TabIndex = 10
        Me.txtTEU.Tag = "requerido"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(139, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "ISO:"
        '
        'txtISO
        '
        Me.txtISO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtISO.Enabled = False
        Me.txtISO.Location = New System.Drawing.Point(142, 111)
        Me.txtISO.MaxLength = 50
        Me.txtISO.Name = "txtISO"
        Me.txtISO.Size = New System.Drawing.Size(326, 23)
        Me.txtISO.TabIndex = 8
        '
        'chkAtivo
        '
        Me.chkAtivo.AutoSize = True
        Me.chkAtivo.Enabled = False
        Me.chkAtivo.Location = New System.Drawing.Point(633, 113)
        Me.chkAtivo.Name = "chkAtivo"
        Me.chkAtivo.Size = New System.Drawing.Size(55, 20)
        Me.chkAtivo.TabIndex = 4
        Me.chkAtivo.Text = "Ativo"
        Me.chkAtivo.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 16)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Descrição:"
        '
        'txtDescricao
        '
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.Enabled = False
        Me.txtDescricao.Location = New System.Drawing.Point(35, 58)
        Me.txtDescricao.MaxLength = 50
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(433, 23)
        Me.txtDescricao.TabIndex = 0
        Me.txtDescricao.Tag = "requerido"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(521, 501)
        Me.txtID.MaxLength = 2
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(87, 23)
        Me.txtID.TabIndex = 6
        Me.txtID.Tag = ""
        Me.txtID.Visible = False
        '
        'pnControles
        '
        Me.pnControles.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles.Location = New System.Drawing.Point(20, 176)
        Me.pnControles.Name = "pnControles"
        Me.pnControles.Size = New System.Drawing.Size(530, 20)
        Me.pnControles.TabIndex = 11
        '
        'FrmTiposConteiner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(804, 556)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "FrmTiposConteiner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cadastro de Tipos de Contêineres"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnControles As System.Windows.Forms.Panel
    Friend WithEvents btnUltimo As System.Windows.Forms.Button
    Friend WithEvents btnProximo As System.Windows.Forms.Button
    Friend WithEvents btnAnterior As System.Windows.Forms.Button
    Friend WithEvents btnPrimeiro As System.Windows.Forms.Button
    Friend WithEvents dgvConsulta As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnFiltro As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnSalvar As System.Windows.Forms.Button
    Friend WithEvents btnEditar As System.Windows.Forms.Button
    Friend WithEvents btnSair As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDescricao As System.Windows.Forms.TextBox
    Friend WithEvents chkAtivo As System.Windows.Forms.CheckBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnNovo As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMAXGROSS As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtTamanho As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTEU As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtISO As TextBox
    Friend WithEvents colCodigo As DataGridViewTextBoxColumn
    Friend WithEvents NM_TIPO_CONTAINER As DataGridViewTextBoxColumn
    Friend WithEvents ISO As DataGridViewTextBoxColumn
    Friend WithEvents MAXGROSS As DataGridViewTextBoxColumn
    Friend WithEvents TEU As DataGridViewTextBoxColumn
    Friend WithEvents TAMANHO_CONTAINER As DataGridViewTextBoxColumn
    Friend WithEvents Ativo As DataGridViewCheckBoxColumn
    Friend WithEvents btnExcluir As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
End Class
