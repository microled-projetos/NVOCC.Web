<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAcessos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAcessos))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnNovaPermissao = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.colNome = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAcessar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colEditar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colExcluir = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colImprimir = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colIncluir = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbGrupos = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.pnControles.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnNovaPermissao)
        Me.Panel2.Controls.Add(Me.btnSalvar)
        Me.Panel2.Controls.Add(Me.btnSair)
        Me.Panel2.Controls.Add(Me.txtCodigo)
        Me.Panel2.Controls.Add(Me.pnControles)
        Me.Panel2.Controls.Add(Me.dgvConsulta)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(626, 437)
        Me.Panel2.TabIndex = 13
        '
        'btnNovaPermissao
        '
        Me.btnNovaPermissao.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNovaPermissao.Image = CType(resources.GetObject("btnNovaPermissao.Image"), System.Drawing.Image)
        Me.btnNovaPermissao.Location = New System.Drawing.Point(18, 388)
        Me.btnNovaPermissao.Name = "btnNovaPermissao"
        Me.btnNovaPermissao.Size = New System.Drawing.Size(135, 34)
        Me.btnNovaPermissao.TabIndex = 70
        Me.btnNovaPermissao.Tag = "Editar Registro Selecionado"
        Me.btnNovaPermissao.Text = "Nova Permissão"
        Me.btnNovaPermissao.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovaPermissao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNovaPermissao.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(153, 388)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(79, 34)
        Me.btnSalvar.TabIndex = 65
        Me.btnSalvar.Tag = "Salva todas as mudanças"
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSalvar, "Salva todas as modificações.")
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.Location = New System.Drawing.Point(503, 388)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(106, 34)
        Me.btnSair.TabIndex = 62
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSair, "Fechar o formulário.")
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'txtCodigo
        '
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Location = New System.Drawing.Point(251, 395)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(78, 27)
        Me.txtCodigo.TabIndex = 63
        Me.txtCodigo.Visible = False
        '
        'pnControles
        '
        Me.pnControles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnControles.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnControles.Controls.Add(Me.btnUltimo)
        Me.pnControles.Controls.Add(Me.btnProximo)
        Me.pnControles.Controls.Add(Me.btnAnterior)
        Me.pnControles.Controls.Add(Me.btnPrimeiro)
        Me.pnControles.Location = New System.Drawing.Point(19, 113)
        Me.pnControles.Name = "pnControles"
        Me.pnControles.Size = New System.Drawing.Size(588, 22)
        Me.pnControles.TabIndex = 11
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
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNome, Me.colAcessar, Me.colEditar, Me.colExcluir, Me.colImprimir, Me.colIncluir, Me.colCodigo})
        Me.dgvConsulta.Location = New System.Drawing.Point(19, 144)
        Me.dgvConsulta.Name = "dgvConsulta"
        Me.dgvConsulta.RowHeadersWidth = 24
        Me.dgvConsulta.RowTemplate.Height = 18
        Me.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConsulta.Size = New System.Drawing.Size(588, 224)
        Me.dgvConsulta.TabIndex = 1
        '
        'colNome
        '
        Me.colNome.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colNome.DataPropertyName = "captionobj"
        Me.colNome.HeaderText = "Nome"
        Me.colNome.Name = "colNome"
        '
        'colAcessar
        '
        Me.colAcessar.DataPropertyName = "ACESSAR"
        Me.colAcessar.FalseValue = "0"
        Me.colAcessar.HeaderText = "Acessar"
        Me.colAcessar.Name = "colAcessar"
        Me.colAcessar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colAcessar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colAcessar.TrueValue = "1"
        Me.colAcessar.Width = 50
        '
        'colEditar
        '
        Me.colEditar.DataPropertyName = "EDITAR"
        Me.colEditar.FalseValue = "0"
        Me.colEditar.HeaderText = "Editar"
        Me.colEditar.Name = "colEditar"
        Me.colEditar.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colEditar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colEditar.TrueValue = "1"
        Me.colEditar.Width = 50
        '
        'colExcluir
        '
        Me.colExcluir.DataPropertyName = "EXCLUIR"
        Me.colExcluir.FalseValue = "0"
        Me.colExcluir.HeaderText = "Excluir"
        Me.colExcluir.Name = "colExcluir"
        Me.colExcluir.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colExcluir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colExcluir.TrueValue = "1"
        Me.colExcluir.Width = 50
        '
        'colImprimir
        '
        Me.colImprimir.DataPropertyName = "IMPRIMIR"
        Me.colImprimir.FalseValue = "0"
        Me.colImprimir.HeaderText = "Imprimir"
        Me.colImprimir.Name = "colImprimir"
        Me.colImprimir.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colImprimir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colImprimir.TrueValue = "1"
        Me.colImprimir.Width = 50
        '
        'colIncluir
        '
        Me.colIncluir.DataPropertyName = "INCLUIR"
        Me.colIncluir.FalseValue = "0"
        Me.colIncluir.HeaderText = "Incluir"
        Me.colIncluir.Name = "colIncluir"
        Me.colIncluir.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colIncluir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colIncluir.TrueValue = "1"
        Me.colIncluir.Width = 50
        '
        'colCodigo
        '
        Me.colCodigo.DataPropertyName = "codfunc"
        Me.colCodigo.HeaderText = "Codigo"
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cbGrupos)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(588, 93)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes:"
        '
        'cbGrupos
        '
        Me.cbGrupos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbGrupos.DisplayMember = "DESCRICAO"
        Me.cbGrupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGrupos.FormattingEnabled = True
        Me.cbGrupos.Location = New System.Drawing.Point(21, 48)
        Me.cbGrupos.Name = "cbGrupos"
        Me.cbGrupos.Size = New System.Drawing.Size(546, 27)
        Me.cbGrupos.TabIndex = 0
        Me.cbGrupos.Tag = ""
        Me.cbGrupos.ValueMember = "CODGRUPO"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 19)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Selecione o Grupo:"
        '
        'FrmAcessos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(626, 437)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmAcessos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cadastro de Permissões de Acesso"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSalvar As System.Windows.Forms.Button
    Friend WithEvents btnSair As System.Windows.Forms.Button
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents cbGrupos As System.Windows.Forms.ComboBox
    Friend WithEvents btnNovaPermissao As Button
    Friend WithEvents colNome As DataGridViewTextBoxColumn
    Friend WithEvents colAcessar As DataGridViewCheckBoxColumn
    Friend WithEvents colEditar As DataGridViewCheckBoxColumn
    Friend WithEvents colExcluir As DataGridViewCheckBoxColumn
    Friend WithEvents colImprimir As DataGridViewCheckBoxColumn
    Friend WithEvents colIncluir As DataGridViewCheckBoxColumn
    Friend WithEvents colCodigo As DataGridViewTextBoxColumn
End Class
