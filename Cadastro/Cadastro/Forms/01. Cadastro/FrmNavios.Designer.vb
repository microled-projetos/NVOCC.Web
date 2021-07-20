<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNavios
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNavios))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescricao = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAtivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkRoRo = New System.Windows.Forms.CheckBox()
        Me.txtNacionalidade = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNome = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnFiltro = New System.Windows.Forms.Button()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.pnControles.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnNovo)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnSalvar)
        Me.Panel2.Controls.Add(Me.btnEditar)
        Me.Panel2.Controls.Add(Me.btnSair)
        Me.Panel2.Controls.Add(Me.txtCodigo)
        Me.Panel2.Controls.Add(Me.dgvConsulta)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(626, 433)
        Me.Panel2.TabIndex = 13
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = CType(resources.GetObject("btnNovo.Image"), System.Drawing.Image)
        Me.btnNovo.Location = New System.Drawing.Point(17, 381)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(79, 34)
        Me.btnNovo.TabIndex = 105
        Me.btnNovo.Tag = "Adiciona um novo registro"
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnNovo, "Inclui um novo registro.")
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.Button4)
        Me.Panel3.Controls.Add(Me.Button5)
        Me.Panel3.Location = New System.Drawing.Point(18, 110)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(588, 22)
        Me.Panel3.TabIndex = 104
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(470, -2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 25)
        Me.Button1.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.Button1, "Vai até o primeiro registro.")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(556, -2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 25)
        Me.Button2.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.Button2, "Vai até o último registro.")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(538, -2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(29, 25)
        Me.Button3.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.Button3, "Vai até o próximo registro.")
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(520, -2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(29, 25)
        Me.Button4.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.Button4, "Vai até o registro anterior.")
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.Location = New System.Drawing.Point(500, -2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(29, 25)
        Me.Button5.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.Button5, "Vai até o primeiro registro.")
        Me.Button5.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(253, 381)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 34)
        Me.btnCancelar.TabIndex = 84
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
        Me.btnSalvar.Location = New System.Drawing.Point(174, 381)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(79, 34)
        Me.btnSalvar.TabIndex = 81
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
        Me.btnEditar.Location = New System.Drawing.Point(96, 381)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(79, 34)
        Me.btnEditar.TabIndex = 80
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
        Me.btnSair.Location = New System.Drawing.Point(516, 381)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(90, 34)
        Me.btnSair.TabIndex = 78
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSair, "Fechar o formulário.")
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'txtCodigo
        '
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Location = New System.Drawing.Point(363, 388)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(78, 27)
        Me.txtCodigo.TabIndex = 79
        Me.txtCodigo.Visible = False
        '
        'dgvConsulta
        '
        Me.dgvConsulta.AllowUserToAddRows = False
        Me.dgvConsulta.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvConsulta.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvConsulta.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvConsulta.BackgroundColor = System.Drawing.Color.White
        Me.dgvConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCodigo, Me.colDescricao, Me.colAtivo, Me.colDescr})
        Me.dgvConsulta.Location = New System.Drawing.Point(18, 141)
        Me.dgvConsulta.Name = "dgvConsulta"
        Me.dgvConsulta.ReadOnly = True
        Me.dgvConsulta.RowHeadersWidth = 24
        Me.dgvConsulta.RowTemplate.Height = 18
        Me.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConsulta.Size = New System.Drawing.Size(588, 224)
        Me.dgvConsulta.TabIndex = 1
        '
        'colCodigo
        '
        Me.colCodigo.DataPropertyName = "CODE"
        Me.colCodigo.HeaderText = "Código"
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.ReadOnly = True
        Me.colCodigo.Width = 60
        '
        'colDescricao
        '
        Me.colDescricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDescricao.DataPropertyName = "DESCR"
        Me.colDescricao.HeaderText = "Nome"
        Me.colDescricao.Name = "colDescricao"
        Me.colDescricao.ReadOnly = True
        '
        'colAtivo
        '
        Me.colAtivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colAtivo.DataPropertyName = "NACIONALIDADE"
        Me.colAtivo.HeaderText = "Nacionalidade"
        Me.colAtivo.Name = "colAtivo"
        Me.colAtivo.ReadOnly = True
        '
        'colDescr
        '
        Me.colDescr.DataPropertyName = "RO_RO"
        Me.colDescr.HeaderText = "RoRo"
        Me.colDescr.Name = "colDescr"
        Me.colDescr.ReadOnly = True
        Me.colDescr.Width = 60
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkRoRo)
        Me.GroupBox1.Controls.Add(Me.txtNacionalidade)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtNome)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(588, 93)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'chkRoRo
        '
        Me.chkRoRo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRoRo.AutoSize = True
        Me.chkRoRo.Enabled = False
        Me.chkRoRo.Location = New System.Drawing.Point(464, 50)
        Me.chkRoRo.Name = "chkRoRo"
        Me.chkRoRo.Size = New System.Drawing.Size(141, 23)
        Me.chkRoRo.TabIndex = 2
        Me.chkRoRo.Text = "Operação RoRo"
        Me.chkRoRo.UseVisualStyleBackColor = True
        '
        'txtNacionalidade
        '
        Me.txtNacionalidade.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNacionalidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNacionalidade.Enabled = False
        Me.txtNacionalidade.Location = New System.Drawing.Point(331, 48)
        Me.txtNacionalidade.MaxLength = 30
        Me.txtNacionalidade.Name = "txtNacionalidade"
        Me.txtNacionalidade.Size = New System.Drawing.Size(119, 27)
        Me.txtNacionalidade.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(327, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 19)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Nacionalidade:"
        '
        'txtNome
        '
        Me.txtNome.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNome.Enabled = False
        Me.txtNome.Location = New System.Drawing.Point(20, 48)
        Me.txtNome.MaxLength = 25
        Me.txtNome.Name = "txtNome"
        Me.txtNome.Size = New System.Drawing.Size(303, 27)
        Me.txtNome.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 19)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Nome:"
        '
        'btnFiltro
        '
        Me.btnFiltro.FlatAppearance.BorderSize = 0
        Me.btnFiltro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFiltro.Image = CType(resources.GetObject("btnFiltro.Image"), System.Drawing.Image)
        Me.btnFiltro.Location = New System.Drawing.Point(423, -2)
        Me.btnFiltro.Name = "btnFiltro"
        Me.btnFiltro.Size = New System.Drawing.Size(26, 22)
        Me.btnFiltro.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnFiltro, "Filtrar os registros da Tabela")
        Me.btnFiltro.UseVisualStyleBackColor = True
        '
        'btnUltimo
        '
        Me.btnUltimo.FlatAppearance.BorderSize = 0
        Me.btnUltimo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUltimo.Image = CType(resources.GetObject("btnUltimo.Image"), System.Drawing.Image)
        Me.btnUltimo.Location = New System.Drawing.Point(499, -2)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(26, 22)
        Me.btnUltimo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btnUltimo, "Vai até o último registro.")
        Me.btnUltimo.UseVisualStyleBackColor = True
        '
        'btnProximo
        '
        Me.btnProximo.FlatAppearance.BorderSize = 0
        Me.btnProximo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnProximo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnProximo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProximo.Image = CType(resources.GetObject("btnProximo.Image"), System.Drawing.Image)
        Me.btnProximo.Location = New System.Drawing.Point(483, -2)
        Me.btnProximo.Name = "btnProximo"
        Me.btnProximo.Size = New System.Drawing.Size(26, 22)
        Me.btnProximo.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnProximo, "Vai até o próximo registro.")
        Me.btnProximo.UseVisualStyleBackColor = True
        '
        'btnAnterior
        '
        Me.btnAnterior.FlatAppearance.BorderSize = 0
        Me.btnAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnterior.Image = CType(resources.GetObject("btnAnterior.Image"), System.Drawing.Image)
        Me.btnAnterior.Location = New System.Drawing.Point(467, -2)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(26, 22)
        Me.btnAnterior.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnAnterior, "Vai até o registro anterior.")
        Me.btnAnterior.UseVisualStyleBackColor = True
        '
        'btnPrimeiro
        '
        Me.btnPrimeiro.FlatAppearance.BorderSize = 0
        Me.btnPrimeiro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrimeiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrimeiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrimeiro.Image = CType(resources.GetObject("btnPrimeiro.Image"), System.Drawing.Image)
        Me.btnPrimeiro.Location = New System.Drawing.Point(449, -2)
        Me.btnPrimeiro.Name = "btnPrimeiro"
        Me.btnPrimeiro.Size = New System.Drawing.Size(26, 22)
        Me.btnPrimeiro.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnPrimeiro, "Vai até o primeiro registro.")
        Me.btnPrimeiro.UseVisualStyleBackColor = True
        '
        'pnControles
        '
        Me.pnControles.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles.Controls.Add(Me.btnFiltro)
        Me.pnControles.Controls.Add(Me.btnUltimo)
        Me.pnControles.Controls.Add(Me.btnProximo)
        Me.pnControles.Controls.Add(Me.btnAnterior)
        Me.pnControles.Controls.Add(Me.btnPrimeiro)
        Me.pnControles.Location = New System.Drawing.Point(20, 127)
        Me.pnControles.Name = "pnControles"
        Me.pnControles.Size = New System.Drawing.Size(530, 20)
        Me.pnControles.TabIndex = 11
        '
        'FrmNavios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(626, 433)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "FrmNavios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cadastro de Navios"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnControles.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnUltimo As System.Windows.Forms.Button
    Friend WithEvents btnProximo As System.Windows.Forms.Button
    Friend WithEvents btnAnterior As System.Windows.Forms.Button
    Friend WithEvents btnPrimeiro As System.Windows.Forms.Button
    Friend WithEvents dgvConsulta As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnFiltro As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkRoRo As System.Windows.Forms.CheckBox
    Friend WithEvents txtNacionalidade As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNome As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnSalvar As System.Windows.Forms.Button
    Friend WithEvents btnEditar As System.Windows.Forms.Button
    Friend WithEvents btnSair As System.Windows.Forms.Button
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents colCodigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescricao As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAtivo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnControles As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents btnNovo As Button
End Class
