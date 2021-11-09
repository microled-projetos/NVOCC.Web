<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMoedas
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
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

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMoedas))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.ID_MOEDA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CD_MOEDA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NM_MOEDA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SIGLA_MOEDA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FL_ATIVO = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNome = New System.Windows.Forms.TextBox()
        Me.txtSigla = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkAtivo = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.pnControles.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(333, 413)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 34)
        Me.btnCancelar.TabIndex = 88
        Me.btnCancelar.Tag = "Cancelar"
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnNovo
        '
        Me.btnNovo.Image = CType(resources.GetObject("btnNovo.Image"), System.Drawing.Image)
        Me.btnNovo.Location = New System.Drawing.Point(19, 413)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(79, 34)
        Me.btnNovo.TabIndex = 87
        Me.btnNovo.Tag = "Adiciona um novo registro"
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'btnExcluir
        '
        Me.btnExcluir.Image = CType(resources.GetObject("btnExcluir.Image"), System.Drawing.Image)
        Me.btnExcluir.Location = New System.Drawing.Point(176, 413)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(79, 34)
        Me.btnExcluir.TabIndex = 86
        Me.btnExcluir.Tag = "Excluir o Registro Selecionado"
        Me.btnExcluir.Text = "Excluir"
        Me.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExcluir.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Enabled = False
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(255, 413)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(79, 34)
        Me.btnSalvar.TabIndex = 85
        Me.btnSalvar.Tag = "Salva todas as mudanças"
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.Image = CType(resources.GetObject("btnEditar.Image"), System.Drawing.Image)
        Me.btnEditar.Location = New System.Drawing.Point(97, 413)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(79, 34)
        Me.btnEditar.TabIndex = 84
        Me.btnEditar.Tag = "Editar Registro Selecionado"
        Me.btnEditar.Text = "Editar"
        Me.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'btnSair
        '
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.Location = New System.Drawing.Point(543, 412)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(99, 34)
        Me.btnSair.TabIndex = 82
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'txtID
        '
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(435, 420)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(78, 20)
        Me.txtID.TabIndex = 83
        Me.txtID.Visible = False
        '
        'pnControles
        '
        Me.pnControles.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnControles.Controls.Add(Me.btnUltimo)
        Me.pnControles.Controls.Add(Me.btnProximo)
        Me.pnControles.Controls.Add(Me.btnAnterior)
        Me.pnControles.Controls.Add(Me.btnPrimeiro)
        Me.pnControles.Location = New System.Drawing.Point(19, 150)
        Me.pnControles.Name = "pnControles"
        Me.pnControles.Size = New System.Drawing.Size(623, 22)
        Me.pnControles.TabIndex = 81
        '
        'btnUltimo
        '
        Me.btnUltimo.FlatAppearance.BorderSize = 0
        Me.btnUltimo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUltimo.Image = CType(resources.GetObject("btnUltimo.Image"), System.Drawing.Image)
        Me.btnUltimo.Location = New System.Drawing.Point(67, -4)
        Me.btnUltimo.Name = "btnUltimo"
        Me.btnUltimo.Size = New System.Drawing.Size(10, 25)
        Me.btnUltimo.TabIndex = 3
        Me.btnUltimo.UseVisualStyleBackColor = True
        '
        'btnProximo
        '
        Me.btnProximo.FlatAppearance.BorderSize = 0
        Me.btnProximo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnProximo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnProximo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProximo.Image = CType(resources.GetObject("btnProximo.Image"), System.Drawing.Image)
        Me.btnProximo.Location = New System.Drawing.Point(49, -4)
        Me.btnProximo.Name = "btnProximo"
        Me.btnProximo.Size = New System.Drawing.Size(10, 25)
        Me.btnProximo.TabIndex = 2
        Me.btnProximo.UseVisualStyleBackColor = True
        '
        'btnAnterior
        '
        Me.btnAnterior.FlatAppearance.BorderSize = 0
        Me.btnAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnterior.Image = CType(resources.GetObject("btnAnterior.Image"), System.Drawing.Image)
        Me.btnAnterior.Location = New System.Drawing.Point(31, -4)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(10, 25)
        Me.btnAnterior.TabIndex = 1
        Me.btnAnterior.UseVisualStyleBackColor = True
        '
        'btnPrimeiro
        '
        Me.btnPrimeiro.FlatAppearance.BorderSize = 0
        Me.btnPrimeiro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrimeiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrimeiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrimeiro.Image = CType(resources.GetObject("btnPrimeiro.Image"), System.Drawing.Image)
        Me.btnPrimeiro.Location = New System.Drawing.Point(11, -4)
        Me.btnPrimeiro.Name = "btnPrimeiro"
        Me.btnPrimeiro.Size = New System.Drawing.Size(10, 25)
        Me.btnPrimeiro.TabIndex = 0
        Me.btnPrimeiro.UseVisualStyleBackColor = True
        '
        'dgvConsulta
        '
        Me.dgvConsulta.AllowUserToAddRows = False
        Me.dgvConsulta.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvConsulta.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvConsulta.BackgroundColor = System.Drawing.Color.White
        Me.dgvConsulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID_MOEDA, Me.CD_MOEDA, Me.NM_MOEDA, Me.SIGLA_MOEDA, Me.FL_ATIVO})
        Me.dgvConsulta.Location = New System.Drawing.Point(19, 178)
        Me.dgvConsulta.Name = "dgvConsulta"
        Me.dgvConsulta.ReadOnly = True
        Me.dgvConsulta.RowHeadersWidth = 24
        Me.dgvConsulta.RowTemplate.Height = 18
        Me.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConsulta.Size = New System.Drawing.Size(623, 228)
        Me.dgvConsulta.TabIndex = 80
        '
        'ID_MOEDA
        '
        Me.ID_MOEDA.DataPropertyName = "ID_MOEDA"
        Me.ID_MOEDA.HeaderText = "ID"
        Me.ID_MOEDA.Name = "ID_MOEDA"
        Me.ID_MOEDA.ReadOnly = True
        '
        'CD_MOEDA
        '
        Me.CD_MOEDA.DataPropertyName = "CD_MOEDA"
        Me.CD_MOEDA.HeaderText = "Código"
        Me.CD_MOEDA.Name = "CD_MOEDA"
        Me.CD_MOEDA.ReadOnly = True
        '
        'NM_MOEDA
        '
        Me.NM_MOEDA.DataPropertyName = "NM_MOEDA"
        Me.NM_MOEDA.HeaderText = "Descrição"
        Me.NM_MOEDA.Name = "NM_MOEDA"
        Me.NM_MOEDA.ReadOnly = True
        Me.NM_MOEDA.Width = 200
        '
        'SIGLA_MOEDA
        '
        Me.SIGLA_MOEDA.DataPropertyName = "SIGLA_MOEDA"
        Me.SIGLA_MOEDA.HeaderText = "Sigla"
        Me.SIGLA_MOEDA.Name = "SIGLA_MOEDA"
        Me.SIGLA_MOEDA.ReadOnly = True
        '
        'FL_ATIVO
        '
        Me.FL_ATIVO.DataPropertyName = "FL_ATIVO"
        Me.FL_ATIVO.HeaderText = "Ativo"
        Me.FL_ATIVO.Name = "FL_ATIVO"
        Me.FL_ATIVO.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtNome)
        Me.GroupBox1.Controls.Add(Me.txtSigla)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.chkAtivo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtCodigo)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(623, 132)
        Me.GroupBox1.TabIndex = 79
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(122, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Descrição:"
        '
        'txtNome
        '
        Me.txtNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNome.Enabled = False
        Me.txtNome.Location = New System.Drawing.Point(125, 45)
        Me.txtNome.MaxLength = 50
        Me.txtNome.Name = "txtNome"
        Me.txtNome.Size = New System.Drawing.Size(355, 20)
        Me.txtNome.TabIndex = 26
        Me.txtNome.Tag = "requerido"
        '
        'txtSigla
        '
        Me.txtSigla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSigla.Enabled = False
        Me.txtSigla.Location = New System.Drawing.Point(17, 92)
        Me.txtSigla.MaxLength = 50
        Me.txtSigla.Name = "txtSigla"
        Me.txtSigla.Size = New System.Drawing.Size(85, 20)
        Me.txtSigla.TabIndex = 25
        Me.txtSigla.Tag = "requerido"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(-126, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Agência:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Sogla:"
        '
        'chkAtivo
        '
        Me.chkAtivo.AutoSize = True
        Me.chkAtivo.Enabled = False
        Me.chkAtivo.Location = New System.Drawing.Point(130, 95)
        Me.chkAtivo.Name = "chkAtivo"
        Me.chkAtivo.Size = New System.Drawing.Size(50, 17)
        Me.chkAtivo.TabIndex = 15
        Me.chkAtivo.Text = "Ativo"
        Me.chkAtivo.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(-126, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Nº Banco:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Cód. Moeda:"
        '
        'txtCodigo
        '
        Me.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Location = New System.Drawing.Point(17, 45)
        Me.txtCodigo.MaxLength = 50
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(85, 20)
        Me.txtCodigo.TabIndex = 0
        Me.txtCodigo.Tag = "requerido"
        '
        'FrmMoedas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 459)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnNovo)
        Me.Controls.Add(Me.btnExcluir)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.btnEditar)
        Me.Controls.Add(Me.btnSair)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.pnControles)
        Me.Controls.Add(Me.dgvConsulta)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmMoedas"
        Me.Text = "Moedas"
        Me.pnControles.ResumeLayout(False)
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnNovo As Button
    Friend WithEvents btnExcluir As Button
    Friend WithEvents btnSalvar As Button
    Friend WithEvents btnEditar As Button
    Friend WithEvents btnSair As Button
    Friend WithEvents txtID As TextBox
    Friend WithEvents pnControles As Panel
    Friend WithEvents btnUltimo As Button
    Friend WithEvents btnProximo As Button
    Friend WithEvents btnAnterior As Button
    Friend WithEvents btnPrimeiro As Button
    Friend WithEvents dgvConsulta As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents chkAtivo As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents ID_MOEDA As DataGridViewTextBoxColumn
    Friend WithEvents CD_MOEDA As DataGridViewTextBoxColumn
    Friend WithEvents NM_MOEDA As DataGridViewTextBoxColumn
    Friend WithEvents SIGLA_MOEDA As DataGridViewTextBoxColumn
    Friend WithEvents FL_ATIVO As DataGridViewCheckBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNome As TextBox
    Friend WithEvents txtSigla As TextBox
End Class
