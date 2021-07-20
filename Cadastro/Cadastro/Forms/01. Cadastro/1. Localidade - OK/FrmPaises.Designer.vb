<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPaises
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPaises))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.pbCarregando = New System.Windows.Forms.PictureBox()
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.btnFiltro = New System.Windows.Forms.Button()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescricao = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSigla = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFumi = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCodigoPais = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPais = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel2.SuspendLayout()
        CType(Me.pbCarregando, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnControles.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnNovo)
        Me.Panel2.Controls.Add(Me.btnExcluir)
        Me.Panel2.Controls.Add(Me.btnSalvar)
        Me.Panel2.Controls.Add(Me.btnEditar)
        Me.Panel2.Controls.Add(Me.txtID)
        Me.Panel2.Controls.Add(Me.btnSair)
        Me.Panel2.Controls.Add(Me.pbCarregando)
        Me.Panel2.Controls.Add(Me.pnControles)
        Me.Panel2.Controls.Add(Me.dgvConsulta)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(623, 436)
        Me.Panel2.TabIndex = 13
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(332, 383)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 34)
        Me.btnCancelar.TabIndex = 113
        Me.btnCancelar.Tag = "Cancelar"
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnCancelar, "Cancela as alterações")
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = CType(resources.GetObject("btnNovo.Image"), System.Drawing.Image)
        Me.btnNovo.Location = New System.Drawing.Point(17, 383)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(79, 34)
        Me.btnNovo.TabIndex = 112
        Me.btnNovo.Tag = "Adiciona um novo registro"
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnNovo, "Inclui um novo registro.")
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'btnExcluir
        '
        Me.btnExcluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcluir.Image = CType(resources.GetObject("btnExcluir.Image"), System.Drawing.Image)
        Me.btnExcluir.Location = New System.Drawing.Point(174, 383)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(79, 34)
        Me.btnExcluir.TabIndex = 111
        Me.btnExcluir.Tag = "Excluir o Registro Selecionado"
        Me.btnExcluir.Text = "Excluir"
        Me.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnExcluir, "Excluir o Registro Selecionado.")
        Me.btnExcluir.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Enabled = False
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(253, 383)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(79, 34)
        Me.btnSalvar.TabIndex = 110
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
        Me.btnEditar.Location = New System.Drawing.Point(96, 383)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(79, 34)
        Me.btnEditar.TabIndex = 109
        Me.btnEditar.Tag = "Editar Registro Selecionado"
        Me.btnEditar.Text = "Editar"
        Me.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'txtID
        '
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(422, 388)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(78, 23)
        Me.txtID.TabIndex = 108
        Me.txtID.Visible = False
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.Location = New System.Drawing.Point(492, 383)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(113, 34)
        Me.btnSair.TabIndex = 107
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSair, "Fechar o formulário.")
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'pbCarregando
        '
        Me.pbCarregando.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbCarregando.BackColor = System.Drawing.Color.White
        Me.pbCarregando.Image = CType(resources.GetObject("pbCarregando.Image"), System.Drawing.Image)
        Me.pbCarregando.Location = New System.Drawing.Point(18, 169)
        Me.pbCarregando.Name = "pbCarregando"
        Me.pbCarregando.Size = New System.Drawing.Size(586, 197)
        Me.pbCarregando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbCarregando.TabIndex = 59
        Me.pbCarregando.TabStop = False
        '
        'pnControles
        '
        Me.pnControles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnControles.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnControles.Controls.Add(Me.btnFiltro)
        Me.pnControles.Controls.Add(Me.btnUltimo)
        Me.pnControles.Controls.Add(Me.btnProximo)
        Me.pnControles.Controls.Add(Me.btnAnterior)
        Me.pnControles.Controls.Add(Me.btnPrimeiro)
        Me.pnControles.Location = New System.Drawing.Point(17, 112)
        Me.pnControles.Name = "pnControles"
        Me.pnControles.Size = New System.Drawing.Size(588, 22)
        Me.pnControles.TabIndex = 11
        '
        'btnFiltro
        '
        Me.btnFiltro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltro.FlatAppearance.BorderSize = 0
        Me.btnFiltro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFiltro.Image = CType(resources.GetObject("btnFiltro.Image"), System.Drawing.Image)
        Me.btnFiltro.Location = New System.Drawing.Point(474, -2)
        Me.btnFiltro.Name = "btnFiltro"
        Me.btnFiltro.Size = New System.Drawing.Size(29, 25)
        Me.btnFiltro.TabIndex = 6
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
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvConsulta.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvConsulta.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvConsulta.BackgroundColor = System.Drawing.Color.White
        Me.dgvConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCodigo, Me.colDescricao, Me.colSigla, Me.colCod, Me.colFumi})
        Me.dgvConsulta.Location = New System.Drawing.Point(17, 143)
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
        Me.colDescricao.HeaderText = "País"
        Me.colDescricao.Name = "colDescricao"
        Me.colDescricao.ReadOnly = True
        '
        'colSigla
        '
        Me.colSigla.DataPropertyName = "SIGLA"
        Me.colSigla.HeaderText = "Sigla"
        Me.colSigla.Name = "colSigla"
        Me.colSigla.ReadOnly = True
        Me.colSigla.Width = 80
        '
        'colCod
        '
        Me.colCod.DataPropertyName = "BIGRAMA"
        Me.colCod.HeaderText = "Bigrama"
        Me.colCod.Name = "colCod"
        Me.colCod.ReadOnly = True
        Me.colCod.Width = 170
        '
        'colFumi
        '
        Me.colFumi.DataPropertyName = "FUMIGACAO"
        Me.colFumi.FalseValue = "0"
        Me.colFumi.HeaderText = "Fumigação"
        Me.colFumi.Name = "colFumi"
        Me.colFumi.ReadOnly = True
        Me.colFumi.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colFumi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colFumi.TrueValue = "1"
        Me.colFumi.Width = 70
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCodigoPais)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPais)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(588, 93)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(233, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Código:"
        '
        'txtCodigoPais
        '
        Me.txtCodigoPais.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCodigoPais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigoPais.Enabled = False
        Me.txtCodigoPais.Location = New System.Drawing.Point(236, 47)
        Me.txtCodigoPais.MaxLength = 3
        Me.txtCodigoPais.Name = "txtCodigoPais"
        Me.txtCodigoPais.Size = New System.Drawing.Size(92, 23)
        Me.txtCodigoPais.TabIndex = 1
        Me.txtCodigoPais.Tag = "requerido"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "País:"
        '
        'txtPais
        '
        Me.txtPais.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPais.Enabled = False
        Me.txtPais.Location = New System.Drawing.Point(20, 47)
        Me.txtPais.MaxLength = 20
        Me.txtPais.Name = "txtPais"
        Me.txtPais.Size = New System.Drawing.Size(209, 23)
        Me.txtPais.TabIndex = 0
        Me.txtPais.Tag = "requerido"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'FrmPaises
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(623, 436)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "FrmPaises"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cadastro de Países"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.pbCarregando, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnControles.ResumeLayout(False)
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPais As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents pbCarregando As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoPais As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnNovo As System.Windows.Forms.Button
    Friend WithEvents btnExcluir As System.Windows.Forms.Button
    Friend WithEvents btnSalvar As System.Windows.Forms.Button
    Friend WithEvents btnEditar As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnSair As System.Windows.Forms.Button
    Friend WithEvents colCodigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescricao As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSigla As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFumi As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents btnFiltro As Button
End Class
