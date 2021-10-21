<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmServico
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmServico))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbTransporte = New System.Windows.Forms.ComboBox()
        Me.ckbAtivo = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSigla = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAtividadeRPS = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTributacaoRPS = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNomeServico = New System.Windows.Forms.TextBox()
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.ID_SERVICO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NM_SERVICO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CD_TRIBUTACAO_RPS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CD_ATIVIDADE_RPS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NM_VIATRANSPORTE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID_VIATRANSPORTE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SIGLA_PROCESSO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FL_ATIVO = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1.SuspendLayout()
        Me.pnControles.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtAtividadeRPS)
        Me.GroupBox1.Controls.Add(Me.txtNomeServico)
        Me.GroupBox1.Controls.Add(Me.cbTransporte)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ckbAtivo)
        Me.GroupBox1.Controls.Add(Me.txtTributacaoRPS)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtSigla)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(848, 140)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Transporte:"
        '
        'cbTransporte
        '
        Me.cbTransporte.FormattingEnabled = True
        Me.cbTransporte.Location = New System.Drawing.Point(17, 103)
        Me.cbTransporte.Name = "cbTransporte"
        Me.cbTransporte.Size = New System.Drawing.Size(401, 21)
        Me.cbTransporte.TabIndex = 11
        Me.cbTransporte.DisplayMember = "NM_VIATRANSPORTE"
        Me.cbTransporte.ValueMember = "ID_VIATRANSPORTE"
        Me.cbTransporte.Enabled = False
        Me.cbTransporte.Tag = "requerido"

        '
        'ckbAtivo
        '
        Me.ckbAtivo.AutoSize = True
        Me.ckbAtivo.Location = New System.Drawing.Point(657, 103)
        Me.ckbAtivo.Name = "ckbAtivo"
        Me.ckbAtivo.Size = New System.Drawing.Size(50, 17)
        Me.ckbAtivo.TabIndex = 10
        Me.ckbAtivo.Text = "Ativo"
        Me.ckbAtivo.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(467, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Sigla Processo:"
        '
        'txtSigla
        '
        Me.txtSigla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSigla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSigla.Enabled = False
        Me.txtSigla.Location = New System.Drawing.Point(470, 101)
        Me.txtSigla.MaxLength = 3
        Me.txtSigla.Name = "txtSigla"
        Me.txtSigla.Size = New System.Drawing.Size(156, 20)
        Me.txtSigla.TabIndex = 8
        Me.txtSigla.Tag = "requerido"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(654, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Cód. Atividade RPS:"
        '
        'txtAtividadeRPS
        '
        Me.txtAtividadeRPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAtividadeRPS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAtividadeRPS.Enabled = False
        Me.txtAtividadeRPS.Location = New System.Drawing.Point(657, 48)
        Me.txtAtividadeRPS.MaxLength = 3
        Me.txtAtividadeRPS.Name = "txtAtividadeRPS"
        Me.txtAtividadeRPS.Size = New System.Drawing.Size(156, 20)
        Me.txtAtividadeRPS.TabIndex = 6
        Me.txtAtividadeRPS.Tag = "requerido"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(467, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Cód. Tributação RPS:"
        '
        'txtTributacaoRPS
        '
        Me.txtTributacaoRPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTributacaoRPS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTributacaoRPS.Enabled = False
        Me.txtTributacaoRPS.Location = New System.Drawing.Point(470, 48)
        Me.txtTributacaoRPS.MaxLength = 3
        Me.txtTributacaoRPS.Name = "txtTributacaoRPS"
        Me.txtTributacaoRPS.Size = New System.Drawing.Size(156, 20)
        Me.txtTributacaoRPS.TabIndex = 1
        Me.txtTributacaoRPS.Tag = "requerido"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Nome do Serviço:"
        '
        'txtNomeServico
        '
        Me.txtNomeServico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNomeServico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNomeServico.Enabled = False
        Me.txtNomeServico.Location = New System.Drawing.Point(17, 48)
        Me.txtNomeServico.MaxLength = 20
        Me.txtNomeServico.Name = "txtNomeServico"
        Me.txtNomeServico.Size = New System.Drawing.Size(401, 20)
        Me.txtNomeServico.TabIndex = 0
        Me.txtNomeServico.Tag = "requerido"
        '
        'pnControles
        '
        Me.pnControles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnControles.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnControles.Controls.Add(Me.btnUltimo)
        Me.pnControles.Controls.Add(Me.btnProximo)
        Me.pnControles.Controls.Add(Me.btnPrimeiro)
        Me.pnControles.Controls.Add(Me.btnAnterior)
        Me.pnControles.Location = New System.Drawing.Point(12, 177)
        Me.pnControles.Name = "pnControles"
        Me.pnControles.Size = New System.Drawing.Size(848, 22)
        Me.pnControles.TabIndex = 115
        '
        'btnUltimo
        '
        Me.btnUltimo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUltimo.FlatAppearance.BorderSize = 0
        Me.btnUltimo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUltimo.Image = CType(resources.GetObject("btnUltimo.Image"), System.Drawing.Image)
        Me.btnUltimo.Location = New System.Drawing.Point(94, -4)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(29, 25)
        Me.btnUltimo.TabIndex = 3
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
        Me.btnProximo.Location = New System.Drawing.Point(76, -4)
        Me.btnProximo.Name = "btnProximo"
        Me.btnProximo.Size = New System.Drawing.Size(29, 25)
        Me.btnProximo.TabIndex = 2
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
        Me.btnAnterior.Location = New System.Drawing.Point(58, -4)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(29, 25)
        Me.btnAnterior.TabIndex = 1
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
        Me.btnPrimeiro.Location = New System.Drawing.Point(38, -4)
        Me.btnPrimeiro.Name = "btnPrimeiro"
        Me.btnPrimeiro.Size = New System.Drawing.Size(29, 25)
        Me.btnPrimeiro.TabIndex = 0
        Me.btnPrimeiro.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(375, 410)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 34)
        Me.btnCancelar.TabIndex = 122
        Me.btnCancelar.Tag = "Cancelar"
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Image = CType(resources.GetObject("btnNovo.Image"), System.Drawing.Image)
        Me.btnNovo.Location = New System.Drawing.Point(60, 410)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(79, 34)
        Me.btnNovo.TabIndex = 121
        Me.btnNovo.Tag = "Adiciona um novo registro"
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'btnExcluir
        '
        Me.btnExcluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcluir.Image = CType(resources.GetObject("btnExcluir.Image"), System.Drawing.Image)
        Me.btnExcluir.Location = New System.Drawing.Point(217, 410)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(79, 34)
        Me.btnExcluir.TabIndex = 120
        Me.btnExcluir.Tag = "Excluir o Registro Selecionado"
        Me.btnExcluir.Text = "Excluir"
        Me.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExcluir.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Enabled = False
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(296, 410)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(79, 34)
        Me.btnSalvar.TabIndex = 119
        Me.btnSalvar.Tag = "Salva todas as mudanças"
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEditar.Image = CType(resources.GetObject("btnEditar.Image"), System.Drawing.Image)
        Me.btnEditar.Location = New System.Drawing.Point(139, 410)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(79, 34)
        Me.btnEditar.TabIndex = 118
        Me.btnEditar.Tag = "Editar Registro Selecionado"
        Me.btnEditar.Text = "Editar"
        Me.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'txtCodigo
        '
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Location = New System.Drawing.Point(465, 415)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(78, 20)
        Me.txtCodigo.TabIndex = 117
        Me.txtCodigo.Visible = False
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.Location = New System.Drawing.Point(747, 410)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(113, 34)
        Me.btnSair.TabIndex = 116
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSair.UseVisualStyleBackColor = True
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
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID_SERVICO, Me.NM_SERVICO, Me.CD_TRIBUTACAO_RPS, Me.CD_ATIVIDADE_RPS, Me.NM_VIATRANSPORTE, Me.ID_VIATRANSPORTE, Me.SIGLA_PROCESSO, Me.FL_ATIVO})
        Me.dgvConsulta.Location = New System.Drawing.Point(12, 205)
        Me.dgvConsulta.Name = "dgvConsulta"
        Me.dgvConsulta.ReadOnly = True
        Me.dgvConsulta.RowHeadersWidth = 24
        Me.dgvConsulta.RowTemplate.Height = 18
        Me.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConsulta.Size = New System.Drawing.Size(848, 199)
        Me.dgvConsulta.TabIndex = 123
        '
        'ID_SERVICO
        '
        Me.ID_SERVICO.DataPropertyName = "ID_SERVICO"
        Me.ID_SERVICO.HeaderText = "ID"
        Me.ID_SERVICO.Name = "ID_SERVICO"
        Me.ID_SERVICO.ReadOnly = True
        '
        'NM_SERVICO
        '
        Me.NM_SERVICO.DataPropertyName = "NM_SERVICO"
        Me.NM_SERVICO.HeaderText = "SERVIÇO"
        Me.NM_SERVICO.Name = "NM_SERVICO"
        Me.NM_SERVICO.ReadOnly = True
        Me.NM_SERVICO.Width = 300
        '
        'CD_TRIBUTACAO_RPS
        '
        Me.CD_TRIBUTACAO_RPS.DataPropertyName = "CD_TRIBUTACAO_RPS"
        Me.CD_TRIBUTACAO_RPS.HeaderText = "TRIBUTACAO RPS"
        Me.CD_TRIBUTACAO_RPS.Name = "CD_TRIBUTACAO_RPS"
        Me.CD_TRIBUTACAO_RPS.ReadOnly = True
        '
        'CD_ATIVIDADE_RPS
        '
        Me.CD_ATIVIDADE_RPS.DataPropertyName = "CD_ATIVIDADE_RPS"
        Me.CD_ATIVIDADE_RPS.HeaderText = "ATIVIDADE RPS"
        Me.CD_ATIVIDADE_RPS.Name = "CD_ATIVIDADE_RPS"
        Me.CD_ATIVIDADE_RPS.ReadOnly = True
        '
        'NM_VIATRANSPORTE
        '
        Me.NM_VIATRANSPORTE.DataPropertyName = "NM_VIATRANSPORTE"
        Me.NM_VIATRANSPORTE.HeaderText = "TRANSPORTE"
        Me.NM_VIATRANSPORTE.Name = "NM_VIATRANSPORTE"
        Me.NM_VIATRANSPORTE.ReadOnly = True
        '
        'ID_VIATRANSPORTE
        '
        Me.ID_VIATRANSPORTE.DataPropertyName = "ID_VIATRANSPORTE"
        Me.ID_VIATRANSPORTE.HeaderText = "ID_VIATRANSPORTE"
        Me.ID_VIATRANSPORTE.Name = "ID_VIATRANSPORTE"
        Me.ID_VIATRANSPORTE.ReadOnly = True
        Me.ID_VIATRANSPORTE.Visible = False
        '
        'SIGLA_PROCESSO
        '
        Me.SIGLA_PROCESSO.DataPropertyName = "SIGLA_PROCESSO"
        Me.SIGLA_PROCESSO.HeaderText = "SIGLA PROCESSO"
        Me.SIGLA_PROCESSO.Name = "SIGLA_PROCESSO"
        Me.SIGLA_PROCESSO.ReadOnly = True
        '
        'FL_ATIVO
        '
        Me.FL_ATIVO.DataPropertyName = "FL_ATIVO"
        Me.FL_ATIVO.HeaderText = "ATIVO"
        Me.FL_ATIVO.Name = "FL_ATIVO"
        Me.FL_ATIVO.ReadOnly = True
        '
        'FrmServico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 460)
        Me.Controls.Add(Me.dgvConsulta)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnNovo)
        Me.Controls.Add(Me.btnExcluir)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.btnEditar)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.btnSair)
        Me.Controls.Add(Me.pnControles)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmServico"
        Me.Text = "FrmServico"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnControles.ResumeLayout(False)
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTributacaoRPS As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtNomeServico As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbTransporte As ComboBox
    Friend WithEvents ckbAtivo As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtSigla As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtAtividadeRPS As TextBox
    Friend WithEvents pnControles As Panel
    Friend WithEvents btnUltimo As Button
    Friend WithEvents btnProximo As Button
    Friend WithEvents btnAnterior As Button
    Friend WithEvents btnPrimeiro As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnNovo As Button
    Friend WithEvents btnExcluir As Button
    Friend WithEvents btnSalvar As Button
    Friend WithEvents btnEditar As Button
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents btnSair As Button
    Friend WithEvents dgvConsulta As DataGridView
    Friend WithEvents ID_SERVICO As DataGridViewTextBoxColumn
    Friend WithEvents NM_SERVICO As DataGridViewTextBoxColumn
    Friend WithEvents CD_TRIBUTACAO_RPS As DataGridViewTextBoxColumn
    Friend WithEvents CD_ATIVIDADE_RPS As DataGridViewTextBoxColumn
    Friend WithEvents NM_VIATRANSPORTE As DataGridViewTextBoxColumn
    Friend WithEvents ID_VIATRANSPORTE As DataGridViewTextBoxColumn
    Friend WithEvents SIGLA_PROCESSO As DataGridViewTextBoxColumn
    Friend WithEvents FL_ATIVO As DataGridViewCheckBoxColumn
End Class
