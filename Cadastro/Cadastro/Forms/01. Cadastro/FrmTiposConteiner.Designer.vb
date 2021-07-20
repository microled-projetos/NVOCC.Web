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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnFiltro = New System.Windows.Forms.Button()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescricao = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAtivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMax40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFlagTemp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFlagExcesso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCodigoDescr = New System.Windows.Forms.TextBox()
        Me.chkExcesso = New System.Windows.Forms.CheckBox()
        Me.chkTemp = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMax40 = New System.Windows.Forms.TextBox()
        Me.txtMin40 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMax20 = New System.Windows.Forms.TextBox()
        Me.txtMin20 = New System.Windows.Forms.TextBox()
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnNovo)
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnSalvar)
        Me.Panel2.Controls.Add(Me.btnEditar)
        Me.Panel2.Controls.Add(Me.btnSair)
        Me.Panel2.Controls.Add(Me.txtCodigo)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.dgvConsulta)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(619, 477)
        Me.Panel2.TabIndex = 13
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = CType(resources.GetObject("btnNovo.Image"), System.Drawing.Image)
        Me.btnNovo.Location = New System.Drawing.Point(15, 430)
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
        Me.btnCancelar.Location = New System.Drawing.Point(251, 430)
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
        Me.btnSalvar.Location = New System.Drawing.Point(172, 430)
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
        Me.btnEditar.Location = New System.Drawing.Point(93, 430)
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
        Me.btnSair.Location = New System.Drawing.Point(497, 430)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(106, 34)
        Me.btnSair.TabIndex = 70
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSair, "Fechar o formulário.")
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'txtCodigo
        '
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Location = New System.Drawing.Point(361, 437)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(78, 27)
        Me.txtCodigo.TabIndex = 71
        Me.txtCodigo.Visible = False
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
        Me.Panel3.Location = New System.Drawing.Point(15, 188)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(588, 22)
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
        Me.btnFiltro.Location = New System.Drawing.Point(470, -2)
        Me.btnFiltro.Name = "btnFiltro"
        Me.btnFiltro.Size = New System.Drawing.Size(29, 25)
        Me.btnFiltro.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnFiltro, "Filtrar os registros da Tabela")
        Me.btnFiltro.UseVisualStyleBackColor = True
        '
        'btnUltimo
        '
        Me.btnUltimo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUltimo.FlatAppearance.BorderSize = 0
        Me.btnUltimo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUltimo.Image = CType(resources.GetObject("btnUltimo.Image"), System.Drawing.Image)
        Me.btnUltimo.Location = New System.Drawing.Point(556, -2)
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
        Me.btnProximo.Location = New System.Drawing.Point(538, -2)
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
        Me.btnAnterior.Location = New System.Drawing.Point(520, -2)
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
        Me.btnPrimeiro.Location = New System.Drawing.Point(500, -2)
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
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCodigo, Me.colCode, Me.colDescr, Me.colDescricao, Me.colAtivo, Me.colStatus, Me.colMax40, Me.colFlagTemp, Me.colFlagExcesso})
        Me.dgvConsulta.Location = New System.Drawing.Point(15, 218)
        Me.dgvConsulta.Name = "dgvConsulta"
        Me.dgvConsulta.ReadOnly = True
        Me.dgvConsulta.RowHeadersWidth = 24
        Me.dgvConsulta.RowTemplate.Height = 18
        Me.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConsulta.Size = New System.Drawing.Size(588, 199)
        Me.dgvConsulta.TabIndex = 1
        '
        'colCodigo
        '
        Me.colCodigo.DataPropertyName = "CODE"
        Me.colCodigo.HeaderText = "Código"
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.ReadOnly = True
        Me.colCodigo.Visible = False
        Me.colCodigo.Width = 60
        '
        'colCode
        '
        Me.colCode.DataPropertyName = "CODIGO"
        Me.colCode.HeaderText = "Código"
        Me.colCode.Name = "colCode"
        Me.colCode.ReadOnly = True
        Me.colCode.Width = 50
        '
        'colDescr
        '
        Me.colDescr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDescr.DataPropertyName = "DESCR"
        Me.colDescr.HeaderText = "Descrição"
        Me.colDescr.Name = "colDescr"
        Me.colDescr.ReadOnly = True
        '
        'colDescricao
        '
        Me.colDescricao.DataPropertyName = "TARA_MIN_20"
        Me.colDescricao.HeaderText = "Min.20'"
        Me.colDescricao.Name = "colDescricao"
        Me.colDescricao.ReadOnly = True
        Me.colDescricao.Width = 55
        '
        'colAtivo
        '
        Me.colAtivo.DataPropertyName = "TARA_MIN_40"
        Me.colAtivo.HeaderText = "Min.40'"
        Me.colAtivo.Name = "colAtivo"
        Me.colAtivo.ReadOnly = True
        Me.colAtivo.Width = 55
        '
        'colStatus
        '
        Me.colStatus.DataPropertyName = "TARA_MAX_20"
        Me.colStatus.HeaderText = "Max.20'"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        Me.colStatus.Width = 55
        '
        'colMax40
        '
        Me.colMax40.DataPropertyName = "TARA_MAX_40"
        Me.colMax40.HeaderText = "Max.40'"
        Me.colMax40.Name = "colMax40"
        Me.colMax40.ReadOnly = True
        Me.colMax40.Width = 55
        '
        'colFlagTemp
        '
        Me.colFlagTemp.DataPropertyName = "FLAG_TEMPERATURA"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colFlagTemp.DefaultCellStyle = DataGridViewCellStyle2
        Me.colFlagTemp.HeaderText = "Temp"
        Me.colFlagTemp.Name = "colFlagTemp"
        Me.colFlagTemp.ReadOnly = True
        Me.colFlagTemp.Width = 50
        '
        'colFlagExcesso
        '
        Me.colFlagExcesso.DataPropertyName = "FLAG_EXCESSO"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colFlagExcesso.DefaultCellStyle = DataGridViewCellStyle3
        Me.colFlagExcesso.HeaderText = "Excesso"
        Me.colFlagExcesso.Name = "colFlagExcesso"
        Me.colFlagExcesso.ReadOnly = True
        Me.colFlagExcesso.Width = 50
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtCodigoDescr)
        Me.GroupBox1.Controls.Add(Me.chkExcesso)
        Me.GroupBox1.Controls.Add(Me.chkTemp)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtDescricao)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(588, 174)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 19)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Código:"
        '
        'txtCodigoDescr
        '
        Me.txtCodigoDescr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigoDescr.Enabled = False
        Me.txtCodigoDescr.Location = New System.Drawing.Point(25, 48)
        Me.txtCodigoDescr.MaxLength = 2
        Me.txtCodigoDescr.Name = "txtCodigoDescr"
        Me.txtCodigoDescr.Size = New System.Drawing.Size(87, 27)
        Me.txtCodigoDescr.TabIndex = 6
        Me.txtCodigoDescr.Tag = "requerido"
        '
        'chkExcesso
        '
        Me.chkExcesso.AutoSize = True
        Me.chkExcesso.Enabled = False
        Me.chkExcesso.Location = New System.Drawing.Point(469, 136)
        Me.chkExcesso.Name = "chkExcesso"
        Me.chkExcesso.Size = New System.Drawing.Size(86, 23)
        Me.chkExcesso.TabIndex = 4
        Me.chkExcesso.Text = "Excesso"
        Me.chkExcesso.UseVisualStyleBackColor = True
        '
        'chkTemp
        '
        Me.chkTemp.AutoSize = True
        Me.chkTemp.Enabled = False
        Me.chkTemp.Location = New System.Drawing.Point(469, 106)
        Me.chkTemp.Name = "chkTemp"
        Me.chkTemp.Size = New System.Drawing.Size(122, 23)
        Me.chkTemp.TabIndex = 3
        Me.chkTemp.Text = "Temperatura"
        Me.chkTemp.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(116, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 19)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Descrição:"
        '
        'txtDescricao
        '
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.Enabled = False
        Me.txtDescricao.Location = New System.Drawing.Point(119, 48)
        Me.txtDescricao.MaxLength = 50
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(448, 27)
        Me.txtDescricao.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.txtMax40)
        Me.GroupBox3.Controls.Add(Me.txtMin40)
        Me.GroupBox3.Location = New System.Drawing.Point(241, 83)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(209, 85)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Tara Contêiner 40'"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 19)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Máximo:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 19)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Mínimo:"
        '
        'txtMax40
        '
        Me.txtMax40.Enabled = False
        Me.txtMax40.Location = New System.Drawing.Point(84, 51)
        Me.txtMax40.MaxLength = 5
        Me.txtMax40.Name = "txtMax40"
        Me.txtMax40.Size = New System.Drawing.Size(112, 27)
        Me.txtMax40.TabIndex = 1
        Me.txtMax40.Tag = "requerido"
        '
        'txtMin40
        '
        Me.txtMin40.Enabled = False
        Me.txtMin40.Location = New System.Drawing.Point(84, 21)
        Me.txtMin40.MaxLength = 5
        Me.txtMin40.Name = "txtMin40"
        Me.txtMin40.Size = New System.Drawing.Size(112, 27)
        Me.txtMin40.TabIndex = 0
        Me.txtMin40.Tag = "requerido"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtMax20)
        Me.GroupBox2.Controls.Add(Me.txtMin20)
        Me.GroupBox2.Location = New System.Drawing.Point(25, 83)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(209, 85)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tara Contêiner 20'"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Máximo:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 19)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Mínimo:"
        '
        'txtMax20
        '
        Me.txtMax20.Enabled = False
        Me.txtMax20.Location = New System.Drawing.Point(84, 51)
        Me.txtMax20.MaxLength = 5
        Me.txtMax20.Name = "txtMax20"
        Me.txtMax20.Size = New System.Drawing.Size(112, 27)
        Me.txtMax20.TabIndex = 1
        Me.txtMax20.Tag = "requerido"
        '
        'txtMin20
        '
        Me.txtMin20.Enabled = False
        Me.txtMin20.Location = New System.Drawing.Point(84, 21)
        Me.txtMin20.MaxLength = 5
        Me.txtMin20.Name = "txtMin20"
        Me.txtMin20.Size = New System.Drawing.Size(112, 27)
        Me.txtMin20.TabIndex = 0
        Me.txtMin20.Tag = "requerido"
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(619, 477)
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
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMax40 As System.Windows.Forms.TextBox
    Friend WithEvents txtMin40 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMax20 As System.Windows.Forms.TextBox
    Friend WithEvents txtMin20 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDescricao As System.Windows.Forms.TextBox
    Friend WithEvents chkExcesso As System.Windows.Forms.CheckBox
    Friend WithEvents chkTemp As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoDescr As System.Windows.Forms.TextBox
    Friend WithEvents colCodigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescricao As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAtivo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMax40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFlagTemp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFlagExcesso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnNovo As Button
End Class
