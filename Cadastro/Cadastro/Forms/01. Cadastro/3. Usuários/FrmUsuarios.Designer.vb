<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmUsuarios
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUsuarios))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtBuscarUsuario = New System.Windows.Forms.TextBox()
        Me.btnPesquisarUsuario = New System.Windows.Forms.Button()
        Me.pnControles = New System.Windows.Forms.Panel()
        Me.btnFiltro2 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.dgvConsulta = New System.Windows.Forms.DataGridView()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescricao = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSenha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPatio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNome = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEmail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVendedor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEmpresa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCPF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAtivo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnImprimirRel = New System.Windows.Forms.Button()
        Me.btnSairRel = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbInativos = New System.Windows.Forms.RadioButton()
        Me.rbAtivos = New System.Windows.Forms.RadioButton()
        Me.rbAmbos = New System.Windows.Forms.RadioButton()
        Me.cbModuloRel = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbUsuarioRel = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbGrupoRel = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNovoEmail = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lstServicos = New System.Windows.Forms.CheckedListBox()
        Me.lstGrupos = New System.Windows.Forms.CheckedListBox()
        Me.cbPatio = New System.Windows.Forms.ComboBox()
        Me.cbEmpresa = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbVendedor = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCPF = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNome = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSenha = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.chk51 = New System.Windows.Forms.CheckBox()
        Me.chk50 = New System.Windows.Forms.CheckBox()
        Me.chk48 = New System.Windows.Forms.CheckBox()
        Me.chk47 = New System.Windows.Forms.CheckBox()
        Me.chk46 = New System.Windows.Forms.CheckBox()
        Me.chk45 = New System.Windows.Forms.CheckBox()
        Me.chk44 = New System.Windows.Forms.CheckBox()
        Me.chk43 = New System.Windows.Forms.CheckBox()
        Me.chk42 = New System.Windows.Forms.CheckBox()
        Me.chk41 = New System.Windows.Forms.CheckBox()
        Me.chk40 = New System.Windows.Forms.CheckBox()
        Me.chk39 = New System.Windows.Forms.CheckBox()
        Me.chk38 = New System.Windows.Forms.CheckBox()
        Me.chk37 = New System.Windows.Forms.CheckBox()
        Me.chk36 = New System.Windows.Forms.CheckBox()
        Me.chk33 = New System.Windows.Forms.CheckBox()
        Me.chk32 = New System.Windows.Forms.CheckBox()
        Me.chk30 = New System.Windows.Forms.CheckBox()
        Me.chk27 = New System.Windows.Forms.CheckBox()
        Me.chk22 = New System.Windows.Forms.CheckBox()
        Me.chk23 = New System.Windows.Forms.CheckBox()
        Me.chk35 = New System.Windows.Forms.CheckBox()
        Me.chk34 = New System.Windows.Forms.CheckBox()
        Me.chk31 = New System.Windows.Forms.CheckBox()
        Me.chk29 = New System.Windows.Forms.CheckBox()
        Me.chk28 = New System.Windows.Forms.CheckBox()
        Me.chk21 = New System.Windows.Forms.CheckBox()
        Me.chk20 = New System.Windows.Forms.CheckBox()
        Me.chk19 = New System.Windows.Forms.CheckBox()
        Me.chk18 = New System.Windows.Forms.CheckBox()
        Me.chk17 = New System.Windows.Forms.CheckBox()
        Me.chk16 = New System.Windows.Forms.CheckBox()
        Me.chk15 = New System.Windows.Forms.CheckBox()
        Me.chk12 = New System.Windows.Forms.CheckBox()
        Me.chk11 = New System.Windows.Forms.CheckBox()
        Me.chk9 = New System.Windows.Forms.CheckBox()
        Me.chk5 = New System.Windows.Forms.CheckBox()
        Me.chk6 = New System.Windows.Forms.CheckBox()
        Me.chk1 = New System.Windows.Forms.CheckBox()
        Me.chk2 = New System.Windows.Forms.CheckBox()
        Me.chk14 = New System.Windows.Forms.CheckBox()
        Me.chk13 = New System.Windows.Forms.CheckBox()
        Me.chk10 = New System.Windows.Forms.CheckBox()
        Me.chk8 = New System.Windows.Forms.CheckBox()
        Me.chk7 = New System.Windows.Forms.CheckBox()
        Me.chk3 = New System.Windows.Forms.CheckBox()
        Me.chk4 = New System.Windows.Forms.CheckBox()
        Me.chk0 = New System.Windows.Forms.CheckBox()
        Me.chk49 = New System.Windows.Forms.CheckBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.btnFiltro = New System.Windows.Forms.Button()
        Me.btnUltimo = New System.Windows.Forms.Button()
        Me.btnProximo = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.btnPrimeiro = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnControles1 = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        Me.pnControles.SuspendLayout()
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.txtNovoEmail.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.pnControles1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.txtBuscarUsuario)
        Me.Panel2.Controls.Add(Me.btnPesquisarUsuario)
        Me.Panel2.Controls.Add(Me.pnControles)
        Me.Panel2.Controls.Add(Me.dgvConsulta)
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnNovo)
        Me.Panel2.Controls.Add(Me.btnExcluir)
        Me.Panel2.Controls.Add(Me.btnSalvar)
        Me.Panel2.Controls.Add(Me.btnEditar)
        Me.Panel2.Controls.Add(Me.btnSair)
        Me.Panel2.Controls.Add(Me.txtCodigo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(873, 642)
        Me.Panel2.TabIndex = 13
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(478, 580)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(199, 19)
        Me.Label14.TabIndex = 153
        Me.Label14.Text = "Pesquisar Usuário / Nome:"
        '
        'txtBuscarUsuario
        '
        Me.txtBuscarUsuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBuscarUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscarUsuario.Location = New System.Drawing.Point(481, 599)
        Me.txtBuscarUsuario.MaxLength = 25
        Me.txtBuscarUsuario.Name = "txtBuscarUsuario"
        Me.txtBuscarUsuario.Size = New System.Drawing.Size(196, 27)
        Me.txtBuscarUsuario.TabIndex = 154
        Me.txtBuscarUsuario.Tag = ""
        '
        'btnPesquisarUsuario
        '
        Me.btnPesquisarUsuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPesquisarUsuario.Image = CType(resources.GetObject("btnPesquisarUsuario.Image"), System.Drawing.Image)
        Me.btnPesquisarUsuario.Location = New System.Drawing.Point(694, 592)
        Me.btnPesquisarUsuario.Name = "btnPesquisarUsuario"
        Me.btnPesquisarUsuario.Size = New System.Drawing.Size(32, 34)
        Me.btnPesquisarUsuario.TabIndex = 152
        Me.btnPesquisarUsuario.Tag = ""
        Me.btnPesquisarUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnPesquisarUsuario, "Pesquisar Parceiros")
        Me.btnPesquisarUsuario.UseVisualStyleBackColor = True
        '
        'pnControles
        '
        Me.pnControles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnControles.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnControles.Controls.Add(Me.btnFiltro2)
        Me.pnControles.Controls.Add(Me.Button2)
        Me.pnControles.Controls.Add(Me.Button3)
        Me.pnControles.Controls.Add(Me.Button4)
        Me.pnControles.Controls.Add(Me.Button5)
        Me.pnControles.Location = New System.Drawing.Point(14, 411)
        Me.pnControles.Name = "pnControles"
        Me.pnControles.Size = New System.Drawing.Size(845, 22)
        Me.pnControles.TabIndex = 95
        '
        'btnFiltro2
        '
        Me.btnFiltro2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFiltro2.FlatAppearance.BorderSize = 0
        Me.btnFiltro2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFiltro2.Image = CType(resources.GetObject("btnFiltro2.Image"), System.Drawing.Image)
        Me.btnFiltro2.Location = New System.Drawing.Point(727, -2)
        Me.btnFiltro2.Name = "btnFiltro2"
        Me.btnFiltro2.Size = New System.Drawing.Size(29, 25)
        Me.btnFiltro2.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnFiltro2, "Vai até o primeiro registro.")
        Me.btnFiltro2.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(812, -2)
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
        Me.Button3.Location = New System.Drawing.Point(794, -2)
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
        Me.Button4.Location = New System.Drawing.Point(776, -2)
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
        Me.Button5.Location = New System.Drawing.Point(756, -2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(29, 25)
        Me.Button5.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.Button5, "Vai até o primeiro registro.")
        Me.Button5.UseVisualStyleBackColor = True
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
        Me.dgvConsulta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCodigo, Me.colDescricao, Me.colSenha, Me.colPatio, Me.colNome, Me.colEmail, Me.colVendedor, Me.colEmpresa, Me.colCPF, Me.colAtivo})
        Me.dgvConsulta.Location = New System.Drawing.Point(14, 441)
        Me.dgvConsulta.Name = "dgvConsulta"
        Me.dgvConsulta.ReadOnly = True
        Me.dgvConsulta.RowHeadersWidth = 24
        Me.dgvConsulta.RowTemplate.Height = 18
        Me.dgvConsulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvConsulta.Size = New System.Drawing.Size(845, 136)
        Me.dgvConsulta.TabIndex = 92
        '
        'colCodigo
        '
        Me.colCodigo.DataPropertyName = "AUTONUM"
        Me.colCodigo.HeaderText = "Código"
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.ReadOnly = True
        Me.colCodigo.Width = 60
        '
        'colDescricao
        '
        Me.colDescricao.DataPropertyName = "USUARIO"
        Me.colDescricao.HeaderText = "Usuário"
        Me.colDescricao.Name = "colDescricao"
        Me.colDescricao.ReadOnly = True
        Me.colDescricao.Width = 80
        '
        'colSenha
        '
        Me.colSenha.DataPropertyName = "SENHA"
        Me.colSenha.HeaderText = "Senha"
        Me.colSenha.Name = "colSenha"
        Me.colSenha.ReadOnly = True
        Me.colSenha.Visible = False
        '
        'colPatio
        '
        Me.colPatio.DataPropertyName = "PATIO"
        Me.colPatio.HeaderText = "Patio"
        Me.colPatio.Name = "colPatio"
        Me.colPatio.ReadOnly = True
        Me.colPatio.Visible = False
        '
        'colNome
        '
        Me.colNome.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colNome.DataPropertyName = "NOME"
        Me.colNome.HeaderText = "Nome"
        Me.colNome.Name = "colNome"
        Me.colNome.ReadOnly = True
        '
        'colEmail
        '
        Me.colEmail.DataPropertyName = "EMAIL"
        Me.colEmail.HeaderText = "Email"
        Me.colEmail.Name = "colEmail"
        Me.colEmail.ReadOnly = True
        Me.colEmail.Width = 350
        '
        'colVendedor
        '
        Me.colVendedor.DataPropertyName = "ID_VENDEDOR"
        Me.colVendedor.HeaderText = "ID Vendedor"
        Me.colVendedor.Name = "colVendedor"
        Me.colVendedor.ReadOnly = True
        Me.colVendedor.Visible = False
        '
        'colEmpresa
        '
        Me.colEmpresa.DataPropertyName = "COD_EMPRESA"
        Me.colEmpresa.HeaderText = "Empresa"
        Me.colEmpresa.Name = "colEmpresa"
        Me.colEmpresa.ReadOnly = True
        Me.colEmpresa.Visible = False
        '
        'colCPF
        '
        Me.colCPF.DataPropertyName = "CPF"
        Me.colCPF.HeaderText = "CPF"
        Me.colCPF.Name = "colCPF"
        Me.colCPF.ReadOnly = True
        Me.colCPF.Visible = False
        '
        'colAtivo
        '
        Me.colAtivo.DataPropertyName = "FLAG_ATIVO"
        Me.colAtivo.FalseValue = "0"
        Me.colAtivo.HeaderText = "Ativo"
        Me.colAtivo.Name = "colAtivo"
        Me.colAtivo.ReadOnly = True
        Me.colAtivo.TrueValue = "1"
        Me.colAtivo.Width = 60
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(14, 13)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(845, 391)
        Me.TabControl1.TabIndex = 19
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Controls.Add(Me.txtNovoEmail)
        Me.TabPage1.Location = New System.Drawing.Point(4, 28)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(837, 359)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Usuário"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.GroupBox2)
        Me.Panel3.Location = New System.Drawing.Point(72, 354)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(695, 164)
        Me.Panel3.TabIndex = 21
        Me.Panel3.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnImprimirRel)
        Me.GroupBox2.Controls.Add(Me.btnSairRel)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.cbModuloRel)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cbUsuarioRel)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.cbGrupoRel)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(665, 129)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Relatório de Usuário"
        '
        'btnImprimirRel
        '
        Me.btnImprimirRel.Image = CType(resources.GetObject("btnImprimirRel.Image"), System.Drawing.Image)
        Me.btnImprimirRel.Location = New System.Drawing.Point(441, 79)
        Me.btnImprimirRel.Name = "btnImprimirRel"
        Me.btnImprimirRel.Size = New System.Drawing.Size(79, 34)
        Me.btnImprimirRel.TabIndex = 87
        Me.btnImprimirRel.Tag = ""
        Me.btnImprimirRel.Text = "Imprimir"
        Me.btnImprimirRel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnImprimirRel, "Imprimir registros.")
        Me.btnImprimirRel.UseVisualStyleBackColor = True
        '
        'btnSairRel
        '
        Me.btnSairRel.Image = CType(resources.GetObject("btnSairRel.Image"), System.Drawing.Image)
        Me.btnSairRel.Location = New System.Drawing.Point(552, 79)
        Me.btnSairRel.Name = "btnSairRel"
        Me.btnSairRel.Size = New System.Drawing.Size(90, 34)
        Me.btnSairRel.TabIndex = 86
        Me.btnSairRel.Tag = "Sair"
        Me.btnSairRel.Text = "Sair [ESC]"
        Me.btnSairRel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSairRel, "Fechar o formulário.")
        Me.btnSairRel.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbInativos)
        Me.GroupBox3.Controls.Add(Me.rbAtivos)
        Me.GroupBox3.Controls.Add(Me.rbAmbos)
        Me.GroupBox3.Location = New System.Drawing.Point(25, 72)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(225, 41)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Status:"
        '
        'rbInativos
        '
        Me.rbInativos.AutoSize = True
        Me.rbInativos.Location = New System.Drawing.Point(147, 16)
        Me.rbInativos.Name = "rbInativos"
        Me.rbInativos.Size = New System.Drawing.Size(86, 23)
        Me.rbInativos.TabIndex = 2
        Me.rbInativos.TabStop = True
        Me.rbInativos.Text = "Inativos"
        Me.rbInativos.UseVisualStyleBackColor = True
        '
        'rbAtivos
        '
        Me.rbAtivos.AutoSize = True
        Me.rbAtivos.Location = New System.Drawing.Point(81, 16)
        Me.rbAtivos.Name = "rbAtivos"
        Me.rbAtivos.Size = New System.Drawing.Size(74, 23)
        Me.rbAtivos.TabIndex = 1
        Me.rbAtivos.TabStop = True
        Me.rbAtivos.Text = "Ativos"
        Me.rbAtivos.UseVisualStyleBackColor = True
        '
        'rbAmbos
        '
        Me.rbAmbos.AutoSize = True
        Me.rbAmbos.Checked = True
        Me.rbAmbos.Location = New System.Drawing.Point(10, 16)
        Me.rbAmbos.Name = "rbAmbos"
        Me.rbAmbos.Size = New System.Drawing.Size(80, 23)
        Me.rbAmbos.TabIndex = 0
        Me.rbAmbos.TabStop = True
        Me.rbAmbos.Text = "Ambos"
        Me.rbAmbos.UseVisualStyleBackColor = True
        '
        'cbModuloRel
        '
        Me.cbModuloRel.DisplayMember = "SISTEMA"
        Me.cbModuloRel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbModuloRel.FormattingEnabled = True
        Me.cbModuloRel.Location = New System.Drawing.Point(441, 41)
        Me.cbModuloRel.Name = "cbModuloRel"
        Me.cbModuloRel.Size = New System.Drawing.Size(201, 27)
        Me.cbModuloRel.TabIndex = 12
        Me.cbModuloRel.Tag = ""
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(438, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 19)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Módulo:"
        '
        'cbUsuarioRel
        '
        Me.cbUsuarioRel.DisplayMember = "USUARIO"
        Me.cbUsuarioRel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUsuarioRel.FormattingEnabled = True
        Me.cbUsuarioRel.Location = New System.Drawing.Point(233, 41)
        Me.cbUsuarioRel.Name = "cbUsuarioRel"
        Me.cbUsuarioRel.Size = New System.Drawing.Size(201, 27)
        Me.cbUsuarioRel.TabIndex = 10
        Me.cbUsuarioRel.Tag = ""
        Me.cbUsuarioRel.ValueMember = "AUTONUM"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(230, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 19)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Usuário:"
        '
        'cbGrupoRel
        '
        Me.cbGrupoRel.DisplayMember = "DESCRICAO"
        Me.cbGrupoRel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGrupoRel.FormattingEnabled = True
        Me.cbGrupoRel.Location = New System.Drawing.Point(25, 41)
        Me.cbGrupoRel.Name = "cbGrupoRel"
        Me.cbGrupoRel.Size = New System.Drawing.Size(201, 27)
        Me.cbGrupoRel.TabIndex = 8
        Me.cbGrupoRel.Tag = ""
        Me.cbGrupoRel.ValueMember = "CODGRUPO"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(21, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 19)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Grupo:"
        '
        'txtNovoEmail
        '
        Me.txtNovoEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNovoEmail.Controls.Add(Me.Label10)
        Me.txtNovoEmail.Controls.Add(Me.Label9)
        Me.txtNovoEmail.Controls.Add(Me.lstServicos)
        Me.txtNovoEmail.Controls.Add(Me.lstGrupos)
        Me.txtNovoEmail.Controls.Add(Me.cbPatio)
        Me.txtNovoEmail.Controls.Add(Me.cbEmpresa)
        Me.txtNovoEmail.Controls.Add(Me.Label8)
        Me.txtNovoEmail.Controls.Add(Me.cbVendedor)
        Me.txtNovoEmail.Controls.Add(Me.Label7)
        Me.txtNovoEmail.Controls.Add(Me.Label6)
        Me.txtNovoEmail.Controls.Add(Me.txtEmail)
        Me.txtNovoEmail.Controls.Add(Me.Label5)
        Me.txtNovoEmail.Controls.Add(Me.txtCPF)
        Me.txtNovoEmail.Controls.Add(Me.Label4)
        Me.txtNovoEmail.Controls.Add(Me.txtNome)
        Me.txtNovoEmail.Controls.Add(Me.Label2)
        Me.txtNovoEmail.Controls.Add(Me.Label1)
        Me.txtNovoEmail.Controls.Add(Me.txtSenha)
        Me.txtNovoEmail.Controls.Add(Me.Label3)
        Me.txtNovoEmail.Controls.Add(Me.txtUsuario)
        Me.txtNovoEmail.Location = New System.Drawing.Point(14, 8)
        Me.txtNovoEmail.Name = "txtNovoEmail"
        Me.txtNovoEmail.Size = New System.Drawing.Size(810, 342)
        Me.txtNovoEmail.TabIndex = 1
        Me.txtNovoEmail.TabStop = False
        Me.txtNovoEmail.Text = "Detalhes:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(430, 178)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(164, 19)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Serviços do Terminal:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 178)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 19)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Grupos:"
        '
        'lstServicos
        '
        Me.lstServicos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstServicos.Enabled = False
        Me.lstServicos.FormattingEnabled = True
        Me.lstServicos.Location = New System.Drawing.Point(433, 196)
        Me.lstServicos.Name = "lstServicos"
        Me.lstServicos.Size = New System.Drawing.Size(355, 114)
        Me.lstServicos.TabIndex = 0
        '
        'lstGrupos
        '
        Me.lstGrupos.Enabled = False
        Me.lstGrupos.FormattingEnabled = True
        Me.lstGrupos.Location = New System.Drawing.Point(20, 196)
        Me.lstGrupos.Name = "lstGrupos"
        Me.lstGrupos.Size = New System.Drawing.Size(406, 114)
        Me.lstGrupos.TabIndex = 0
        '
        'cbPatio
        '
        Me.cbPatio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPatio.DisplayMember = "DESCR"
        Me.cbPatio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPatio.Enabled = False
        Me.cbPatio.FormattingEnabled = True
        Me.cbPatio.Location = New System.Drawing.Point(690, 96)
        Me.cbPatio.Name = "cbPatio"
        Me.cbPatio.Size = New System.Drawing.Size(98, 27)
        Me.cbPatio.TabIndex = 5
        Me.cbPatio.Tag = ""
        Me.cbPatio.ValueMember = "AUTONUM"
        '
        'cbEmpresa
        '
        Me.cbEmpresa.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbEmpresa.DisplayMember = "DESCRICAO"
        Me.cbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEmpresa.Enabled = False
        Me.cbEmpresa.FormattingEnabled = True
        Me.cbEmpresa.Location = New System.Drawing.Point(433, 145)
        Me.cbEmpresa.Name = "cbEmpresa"
        Me.cbEmpresa.Size = New System.Drawing.Size(355, 27)
        Me.cbEmpresa.TabIndex = 7
        Me.cbEmpresa.Tag = ""
        Me.cbEmpresa.ValueMember = "AUTONUM"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(430, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 19)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Empresa:"
        '
        'cbVendedor
        '
        Me.cbVendedor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbVendedor.DisplayMember = "FANTASIA"
        Me.cbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVendedor.Enabled = False
        Me.cbVendedor.FormattingEnabled = True
        Me.cbVendedor.Location = New System.Drawing.Point(20, 145)
        Me.cbVendedor.Name = "cbVendedor"
        Me.cbVendedor.Size = New System.Drawing.Size(406, 27)
        Me.cbVendedor.TabIndex = 6
        Me.cbVendedor.Tag = ""
        Me.cbVendedor.ValueMember = "AUTONUM"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 19)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Vendedor:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 19)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Email:"
        '
        'txtEmail
        '
        Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEmail.Enabled = False
        Me.txtEmail.Location = New System.Drawing.Point(20, 96)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(406, 27)
        Me.txtEmail.TabIndex = 2
        Me.txtEmail.Tag = ""
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(686, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 19)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "CPF:"
        '
        'txtCPF
        '
        Me.txtCPF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCPF.Enabled = False
        Me.txtCPF.Location = New System.Drawing.Point(690, 48)
        Me.txtCPF.Mask = "999,999,999-99"
        Me.txtCPF.Name = "txtCPF"
        Me.txtCPF.Size = New System.Drawing.Size(98, 27)
        Me.txtCPF.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 19)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Nome Completo:"
        '
        'txtNome
        '
        Me.txtNome.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNome.Enabled = False
        Me.txtNome.Location = New System.Drawing.Point(20, 48)
        Me.txtNome.MaxLength = 40
        Me.txtNome.Name = "txtNome"
        Me.txtNome.Size = New System.Drawing.Size(662, 27)
        Me.txtNome.TabIndex = 0
        Me.txtNome.Tag = "requerido"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(686, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 19)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Pátio:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(539, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 19)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Senha:"
        '
        'txtSenha
        '
        Me.txtSenha.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSenha.Enabled = False
        Me.txtSenha.Location = New System.Drawing.Point(542, 96)
        Me.txtSenha.MaxLength = 8
        Me.txtSenha.Name = "txtSenha"
        Me.txtSenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenha.Size = New System.Drawing.Size(140, 27)
        Me.txtSenha.TabIndex = 4
        Me.txtSenha.Tag = "requerido"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(430, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 19)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Usuário:"
        '
        'txtUsuario
        '
        Me.txtUsuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUsuario.Enabled = False
        Me.txtUsuario.Location = New System.Drawing.Point(433, 96)
        Me.txtUsuario.MaxLength = 25
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(102, 27)
        Me.txtUsuario.TabIndex = 3
        Me.txtUsuario.Tag = "requerido"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.CheckBox5)
        Me.TabPage2.Controls.Add(Me.CheckBox4)
        Me.TabPage2.Controls.Add(Me.CheckBox3)
        Me.TabPage2.Controls.Add(Me.CheckBox2)
        Me.TabPage2.Controls.Add(Me.CheckBox1)
        Me.TabPage2.Controls.Add(Me.chk51)
        Me.TabPage2.Controls.Add(Me.chk50)
        Me.TabPage2.Controls.Add(Me.chk48)
        Me.TabPage2.Controls.Add(Me.chk47)
        Me.TabPage2.Controls.Add(Me.chk46)
        Me.TabPage2.Controls.Add(Me.chk45)
        Me.TabPage2.Controls.Add(Me.chk44)
        Me.TabPage2.Controls.Add(Me.chk43)
        Me.TabPage2.Controls.Add(Me.chk42)
        Me.TabPage2.Controls.Add(Me.chk41)
        Me.TabPage2.Controls.Add(Me.chk40)
        Me.TabPage2.Controls.Add(Me.chk39)
        Me.TabPage2.Controls.Add(Me.chk38)
        Me.TabPage2.Controls.Add(Me.chk37)
        Me.TabPage2.Controls.Add(Me.chk36)
        Me.TabPage2.Controls.Add(Me.chk33)
        Me.TabPage2.Controls.Add(Me.chk32)
        Me.TabPage2.Controls.Add(Me.chk30)
        Me.TabPage2.Controls.Add(Me.chk27)
        Me.TabPage2.Controls.Add(Me.chk22)
        Me.TabPage2.Controls.Add(Me.chk23)
        Me.TabPage2.Controls.Add(Me.chk35)
        Me.TabPage2.Controls.Add(Me.chk34)
        Me.TabPage2.Controls.Add(Me.chk31)
        Me.TabPage2.Controls.Add(Me.chk29)
        Me.TabPage2.Controls.Add(Me.chk28)
        Me.TabPage2.Controls.Add(Me.chk21)
        Me.TabPage2.Controls.Add(Me.chk20)
        Me.TabPage2.Controls.Add(Me.chk19)
        Me.TabPage2.Controls.Add(Me.chk18)
        Me.TabPage2.Controls.Add(Me.chk17)
        Me.TabPage2.Controls.Add(Me.chk16)
        Me.TabPage2.Controls.Add(Me.chk15)
        Me.TabPage2.Controls.Add(Me.chk12)
        Me.TabPage2.Controls.Add(Me.chk11)
        Me.TabPage2.Controls.Add(Me.chk9)
        Me.TabPage2.Controls.Add(Me.chk5)
        Me.TabPage2.Controls.Add(Me.chk6)
        Me.TabPage2.Controls.Add(Me.chk1)
        Me.TabPage2.Controls.Add(Me.chk2)
        Me.TabPage2.Controls.Add(Me.chk14)
        Me.TabPage2.Controls.Add(Me.chk13)
        Me.TabPage2.Controls.Add(Me.chk10)
        Me.TabPage2.Controls.Add(Me.chk8)
        Me.TabPage2.Controls.Add(Me.chk7)
        Me.TabPage2.Controls.Add(Me.chk3)
        Me.TabPage2.Controls.Add(Me.chk4)
        Me.TabPage2.Controls.Add(Me.chk0)
        Me.TabPage2.Controls.Add(Me.chk49)
        Me.TabPage2.Location = New System.Drawing.Point(4, 28)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(837, 359)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Permissões"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(609, 321)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(209, 23)
        Me.CheckBox5.TabIndex = 67
        Me.CheckBox5.Tag = "flag_indicador_operacional"
        Me.CheckBox5.Text = "Indicadores Operacionais"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(292, 321)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(94, 23)
        Me.CheckBox4.TabIndex = 66
        Me.CheckBox4.Tag = "flag_mov_patio_rel_est"
        Me.CheckBox4.Text = "Relatório"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(609, 302)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(165, 23)
        Me.CheckBox3.TabIndex = 65
        Me.CheckBox3.Tag = "flag_mov_patio_parametros"
        Me.CheckBox3.Text = "Acesso Parâmetros"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(17, 321)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(139, 23)
        Me.CheckBox2.TabIndex = 64
        Me.CheckBox2.Tag = "flag_mov_patio_liberacao"
        Me.CheckBox2.Text = "Permite Liberar"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(292, 302)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(217, 23)
        Me.CheckBox1.TabIndex = 63
        Me.CheckBox1.Tag = "flag_mov_patio"
        Me.CheckBox1.Text = "Acesso sistema Mov. Pátio"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'chk51
        '
        Me.chk51.AutoSize = True
        Me.chk51.Location = New System.Drawing.Point(292, 248)
        Me.chk51.Name = "chk51"
        Me.chk51.Size = New System.Drawing.Size(237, 23)
        Me.chk51.TabIndex = 32
        Me.chk51.Tag = "flag_data_carregamento"
        Me.chk51.Text = "Altera Data de Carregamento"
        Me.chk51.UseVisualStyleBackColor = True
        '
        'chk50
        '
        Me.chk50.AutoSize = True
        Me.chk50.Location = New System.Drawing.Point(609, 178)
        Me.chk50.Name = "chk50"
        Me.chk50.Size = New System.Drawing.Size(286, 23)
        Me.chk50.TabIndex = 46
        Me.chk50.Tag = "flag_prophix"
        Me.chk50.Text = "Gestão Orçamentária - Base Prophix"
        Me.chk50.UseVisualStyleBackColor = True
        '
        'chk48
        '
        Me.chk48.AutoSize = True
        Me.chk48.Location = New System.Drawing.Point(17, 266)
        Me.chk48.Name = "chk48"
        Me.chk48.Size = New System.Drawing.Size(305, 23)
        Me.chk48.TabIndex = 16
        Me.chk48.Tag = "Flag_Param_Transit"
        Me.chk48.Text = "Cad. Parametro - Tempo Av. e Calculo"
        Me.chk48.UseVisualStyleBackColor = True
        '
        'chk47
        '
        Me.chk47.AutoSize = True
        Me.chk47.Location = New System.Drawing.Point(609, 213)
        Me.chk47.Name = "chk47"
        Me.chk47.Size = New System.Drawing.Size(226, 23)
        Me.chk47.TabIndex = 48
        Me.chk47.Tag = "Flag_Dt_Chegada_Manual"
        Me.chk47.Text = "Data Recepção Doc. Manual"
        Me.chk47.UseVisualStyleBackColor = True
        '
        'chk46
        '
        Me.chk46.AutoSize = True
        Me.chk46.Location = New System.Drawing.Point(292, 212)
        Me.chk46.Name = "chk46"
        Me.chk46.Size = New System.Drawing.Size(327, 23)
        Me.chk46.TabIndex = 29
        Me.chk46.Tag = "flag_bt_transporte"
        Me.chk46.Text = "Botão Transporte (Consulta Atendimento)"
        Me.chk46.UseVisualStyleBackColor = True
        '
        'chk45
        '
        Me.chk45.AutoSize = True
        Me.chk45.Location = New System.Drawing.Point(292, 230)
        Me.chk45.Name = "chk45"
        Me.chk45.Size = New System.Drawing.Size(308, 23)
        Me.chk45.TabIndex = 31
        Me.chk45.Tag = "flag_bt_anvisa"
        Me.chk45.Text = "Botão ANVISA (Consulta Atendimento)"
        Me.chk45.UseVisualStyleBackColor = True
        '
        'chk44
        '
        Me.chk44.AutoSize = True
        Me.chk44.Location = New System.Drawing.Point(292, 104)
        Me.chk44.Name = "chk44"
        Me.chk44.Size = New System.Drawing.Size(364, 23)
        Me.chk44.TabIndex = 23
        Me.chk44.Tag = "flag_email_desconto"
        Me.chk44.Text = "Recebe E-Mail Informativo de Desc. Comerciais"
        Me.chk44.UseVisualStyleBackColor = True
        '
        'chk43
        '
        Me.chk43.AutoSize = True
        Me.chk43.Location = New System.Drawing.Point(609, 16)
        Me.chk43.Name = "chk43"
        Me.chk43.Size = New System.Drawing.Size(110, 23)
        Me.chk43.TabIndex = 37
        Me.chk43.Tag = "flag_pre_calculo"
        Me.chk43.Text = "Pré Cálculo"
        Me.chk43.UseVisualStyleBackColor = True
        '
        'chk42
        '
        Me.chk42.AutoSize = True
        Me.chk42.Location = New System.Drawing.Point(292, 284)
        Me.chk42.Name = "chk42"
        Me.chk42.Size = New System.Drawing.Size(406, 23)
        Me.chk42.TabIndex = 35
        Me.chk42.Tag = "Flag_Transp_Serv"
        Me.chk42.Text = "Altera Motorista/Transp. Vinculados a P. de Serviços "
        Me.chk42.UseVisualStyleBackColor = True
        '
        'chk41
        '
        Me.chk41.AutoSize = True
        Me.chk41.Location = New System.Drawing.Point(17, 248)
        Me.chk41.Name = "chk41"
        Me.chk41.Size = New System.Drawing.Size(267, 23)
        Me.chk41.TabIndex = 14
        Me.chk41.Tag = "Flag_Estrutura_XML"
        Me.chk41.Text = "Monta Estrutura de Arquivos XML"
        Me.chk41.UseVisualStyleBackColor = True
        '
        'chk40
        '
        Me.chk40.AutoSize = True
        Me.chk40.Location = New System.Drawing.Point(609, 267)
        Me.chk40.Name = "chk40"
        Me.chk40.Size = New System.Drawing.Size(204, 23)
        Me.chk40.TabIndex = 51
        Me.chk40.Tag = "Flag_Alt_Demurrage"
        Me.chk40.Text = "Corrige Data Demurrage"
        Me.chk40.UseVisualStyleBackColor = True
        '
        'chk39
        '
        Me.chk39.AutoSize = True
        Me.chk39.Location = New System.Drawing.Point(609, 51)
        Me.chk39.Name = "chk39"
        Me.chk39.Size = New System.Drawing.Size(157, 23)
        Me.chk39.TabIndex = 39
        Me.chk39.Tag = "Flag_Retirada"
        Me.chk39.Text = "Indica Cliente VIP"
        Me.chk39.UseVisualStyleBackColor = True
        '
        'chk38
        '
        Me.chk38.AutoSize = True
        Me.chk38.Location = New System.Drawing.Point(609, 196)
        Me.chk38.Name = "chk38"
        Me.chk38.Size = New System.Drawing.Size(270, 23)
        Me.chk38.TabIndex = 47
        Me.chk38.Tag = "Flag_Redestinacao"
        Me.chk38.Text = "Executa Redestinação entre Pátios"
        Me.chk38.UseVisualStyleBackColor = True
        '
        'chk37
        '
        Me.chk37.AutoSize = True
        Me.chk37.Location = New System.Drawing.Point(292, 194)
        Me.chk37.Name = "chk37"
        Me.chk37.Size = New System.Drawing.Size(363, 23)
        Me.chk37.TabIndex = 28
        Me.chk37.Tag = "Flag_RM"
        Me.chk37.Text = "Altera status de Recebimento de Reg.Manifesto"
        Me.chk37.UseVisualStyleBackColor = True
        '
        'chk36
        '
        Me.chk36.AutoSize = True
        Me.chk36.Location = New System.Drawing.Point(292, 177)
        Me.chk36.Name = "chk36"
        Me.chk36.Size = New System.Drawing.Size(279, 23)
        Me.chk36.TabIndex = 27
        Me.chk36.Tag = "Flag_Transporte_especial"
        Me.chk36.Text = "Grava Valor de Transporte Especial"
        Me.chk36.UseVisualStyleBackColor = True
        '
        'chk33
        '
        Me.chk33.AutoSize = True
        Me.chk33.Location = New System.Drawing.Point(292, 122)
        Me.chk33.Name = "chk33"
        Me.chk33.Size = New System.Drawing.Size(291, 23)
        Me.chk33.TabIndex = 24
        Me.chk33.Tag = "flag_prest_serv"
        Me.chk33.Text = "Define Prest. Serviço Transportadora"
        Me.chk33.UseVisualStyleBackColor = True
        '
        'chk32
        '
        Me.chk32.AutoSize = True
        Me.chk32.Location = New System.Drawing.Point(609, 69)
        Me.chk32.Name = "chk32"
        Me.chk32.Size = New System.Drawing.Size(187, 23)
        Me.chk32.TabIndex = 40
        Me.chk32.Tag = "flag_altera_transportadora"
        Me.chk32.Text = "Altera Transportadora"
        Me.chk32.UseVisualStyleBackColor = True
        '
        'chk30
        '
        Me.chk30.AutoSize = True
        Me.chk30.Location = New System.Drawing.Point(292, 69)
        Me.chk30.Name = "chk30"
        Me.chk30.Size = New System.Drawing.Size(308, 23)
        Me.chk30.TabIndex = 21
        Me.chk30.Tag = "flag_bloqueio_nvocc"
        Me.chk30.Text = "Bloqueio NVOCC Cadastro de Parceiros"
        Me.chk30.UseVisualStyleBackColor = True
        '
        'chk27
        '
        Me.chk27.AutoSize = True
        Me.chk27.Location = New System.Drawing.Point(292, 16)
        Me.chk27.Name = "chk27"
        Me.chk27.Size = New System.Drawing.Size(179, 23)
        Me.chk27.TabIndex = 18
        Me.chk27.Tag = "flag_altera_tabela"
        Me.chk27.Text = "Altera Tabela Padrão"
        Me.chk27.UseVisualStyleBackColor = True
        '
        'chk22
        '
        Me.chk22.AutoSize = True
        Me.chk22.Location = New System.Drawing.Point(17, 284)
        Me.chk22.Name = "chk22"
        Me.chk22.Size = New System.Drawing.Size(298, 23)
        Me.chk22.TabIndex = 17
        Me.chk22.Tag = "rel_completo_agenda"
        Me.chk22.Text = "Imprime Relatório de Posicionamento"
        Me.chk22.UseVisualStyleBackColor = True
        '
        'chk23
        '
        Me.chk23.AutoSize = True
        Me.chk23.Location = New System.Drawing.Point(609, 123)
        Me.chk23.Name = "chk23"
        Me.chk23.Size = New System.Drawing.Size(222, 23)
        Me.chk23.TabIndex = 43
        Me.chk23.Tag = "flag_indicador_restrito"
        Me.chk23.Text = "Exibe Indicadores Restritos"
        Me.chk23.UseVisualStyleBackColor = True
        '
        'chk35
        '
        Me.chk35.AutoSize = True
        Me.chk35.Location = New System.Drawing.Point(292, 159)
        Me.chk35.Name = "chk35"
        Me.chk35.Size = New System.Drawing.Size(255, 23)
        Me.chk35.TabIndex = 26
        Me.chk35.Tag = "flag_Muda_Forma_Pagamento"
        Me.chk35.Text = "Altera Forma de Pagamento GR"
        Me.chk35.UseVisualStyleBackColor = True
        '
        'chk34
        '
        Me.chk34.AutoSize = True
        Me.chk34.Location = New System.Drawing.Point(292, 141)
        Me.chk34.Name = "chk34"
        Me.chk34.Size = New System.Drawing.Size(276, 23)
        Me.chk34.TabIndex = 25
        Me.chk34.Tag = "flag_liberado_saida"
        Me.chk34.Text = "Libera Saída de Carga do Terminal"
        Me.chk34.UseVisualStyleBackColor = True
        '
        'chk31
        '
        Me.chk31.AutoSize = True
        Me.chk31.Location = New System.Drawing.Point(292, 87)
        Me.chk31.Name = "chk31"
        Me.chk31.Size = New System.Drawing.Size(333, 23)
        Me.chk31.TabIndex = 22
        Me.chk31.Tag = "GR_GR_DOC"
        Me.chk31.Text = "Grava Recebimento de Doc. (Faturamento)"
        Me.chk31.UseVisualStyleBackColor = True
        '
        'chk29
        '
        Me.chk29.AutoSize = True
        Me.chk29.Location = New System.Drawing.Point(292, 51)
        Me.chk29.Name = "chk29"
        Me.chk29.Size = New System.Drawing.Size(289, 23)
        Me.chk29.TabIndex = 20
        Me.chk29.Tag = "flag_ferrovia"
        Me.chk29.Text = "Status de Ferrovia Tela Atendimento"
        Me.chk29.UseVisualStyleBackColor = True
        '
        'chk28
        '
        Me.chk28.AutoSize = True
        Me.chk28.Location = New System.Drawing.Point(292, 34)
        Me.chk28.Name = "chk28"
        Me.chk28.Size = New System.Drawing.Size(269, 23)
        Me.chk28.TabIndex = 19
        Me.chk28.Tag = "FLAG_CANCELA_REG_GR_VENCIDA"
        Me.chk28.Text = "Cancela Registro com GR Vencida"
        Me.chk28.UseVisualStyleBackColor = True
        '
        'chk21
        '
        Me.chk21.AutoSize = True
        Me.chk21.Location = New System.Drawing.Point(609, 160)
        Me.chk21.Name = "chk21"
        Me.chk21.Size = New System.Drawing.Size(195, 23)
        Me.chk21.TabIndex = 45
        Me.chk21.Tag = "gr_tb_cob"
        Me.chk21.Text = "Grava Tabela Cobrança"
        Me.chk21.UseVisualStyleBackColor = True
        '
        'chk20
        '
        Me.chk20.AutoSize = True
        Me.chk20.Location = New System.Drawing.Point(609, 285)
        Me.chk20.Name = "chk20"
        Me.chk20.Size = New System.Drawing.Size(108, 23)
        Me.chk20.TabIndex = 36
        Me.chk20.Tag = "gr_fma"
        Me.chk20.Text = "Grava FMA"
        Me.chk20.UseVisualStyleBackColor = True
        '
        'chk19
        '
        Me.chk19.AutoSize = True
        Me.chk19.Location = New System.Drawing.Point(609, 142)
        Me.chk19.Name = "chk19"
        Me.chk19.Size = New System.Drawing.Size(250, 23)
        Me.chk19.TabIndex = 44
        Me.chk19.Tag = "FLAG_SOL_COM"
        Me.chk19.Text = "Aprova Solicitações Comerciais"
        Me.chk19.UseVisualStyleBackColor = True
        '
        'chk18
        '
        Me.chk18.AutoSize = True
        Me.chk18.Location = New System.Drawing.Point(609, 105)
        Me.chk18.Name = "chk18"
        Me.chk18.Size = New System.Drawing.Size(216, 23)
        Me.chk18.TabIndex = 42
        Me.chk18.Tag = "FLAG_REGULARIZA_EVE25"
        Me.chk18.Text = "Regularizar Saida (EVE25)"
        Me.chk18.UseVisualStyleBackColor = True
        '
        'chk17
        '
        Me.chk17.AutoSize = True
        Me.chk17.Location = New System.Drawing.Point(609, 249)
        Me.chk17.Name = "chk17"
        Me.chk17.Size = New System.Drawing.Size(221, 23)
        Me.chk17.TabIndex = 50
        Me.chk17.Tag = "flag_del_reg_imp"
        Me.chk17.Text = "Exclui Registro Importação"
        Me.chk17.UseVisualStyleBackColor = True
        '
        'chk16
        '
        Me.chk16.AutoSize = True
        Me.chk16.Location = New System.Drawing.Point(292, 266)
        Me.chk16.Name = "chk16"
        Me.chk16.Size = New System.Drawing.Size(269, 23)
        Me.chk16.TabIndex = 34
        Me.chk16.Tag = "flag_email_presenca_carga"
        Me.chk16.Text = "Recebe Email P.Carga não gerada"
        Me.chk16.UseVisualStyleBackColor = True
        '
        'chk15
        '
        Me.chk15.AutoSize = True
        Me.chk15.Location = New System.Drawing.Point(609, 34)
        Me.chk15.Name = "chk15"
        Me.chk15.Size = New System.Drawing.Size(111, 23)
        Me.chk15.TabIndex = 38
        Me.chk15.Tag = "flag_cancela_GR"
        Me.chk15.Text = "Cancela GR"
        Me.chk15.UseVisualStyleBackColor = True
        '
        'chk12
        '
        Me.chk12.AutoSize = True
        Me.chk12.Location = New System.Drawing.Point(17, 230)
        Me.chk12.Name = "chk12"
        Me.chk12.Size = New System.Drawing.Size(220, 23)
        Me.chk12.TabIndex = 12
        Me.chk12.Tag = "Flag_Calculo_Automatico"
        Me.chk12.Text = "Executa Calculo Automatio"
        Me.chk12.UseVisualStyleBackColor = True
        '
        'chk11
        '
        Me.chk11.AutoSize = True
        Me.chk11.Location = New System.Drawing.Point(17, 212)
        Me.chk11.Name = "chk11"
        Me.chk11.Size = New System.Drawing.Size(349, 23)
        Me.chk11.TabIndex = 11
        Me.chk11.Tag = "flag_altera_despachante"
        Me.chk11.Text = "Permite alterar desp. p/ doc. DTAS averbado"
        Me.chk11.UseVisualStyleBackColor = True
        '
        'chk9
        '
        Me.chk9.AutoSize = True
        Me.chk9.Location = New System.Drawing.Point(17, 178)
        Me.chk9.Name = "chk9"
        Me.chk9.Size = New System.Drawing.Size(310, 23)
        Me.chk9.TabIndex = 9
        Me.chk9.Tag = "flag_peso"
        Me.chk9.Text = "Autoriza Alteração de Peso Manifestado"
        Me.chk9.UseVisualStyleBackColor = True
        '
        'chk5
        '
        Me.chk5.AutoSize = True
        Me.chk5.Location = New System.Drawing.Point(17, 105)
        Me.chk5.Name = "chk5"
        Me.chk5.Size = New System.Drawing.Size(226, 23)
        Me.chk5.TabIndex = 5
        Me.chk5.Tag = "transf_patio"
        Me.chk5.Text = "Config. Tranf. Patios (Gate)"
        Me.chk5.UseVisualStyleBackColor = True
        '
        'chk6
        '
        Me.chk6.AutoSize = True
        Me.chk6.Location = New System.Drawing.Point(17, 123)
        Me.chk6.Name = "chk6"
        Me.chk6.Size = New System.Drawing.Size(229, 23)
        Me.chk6.TabIndex = 6
        Me.chk6.Tag = "cancel_patio"
        Me.chk6.Text = "Cancela Tranf. Patios (Gate)"
        Me.chk6.UseVisualStyleBackColor = True
        '
        'chk1
        '
        Me.chk1.AutoSize = True
        Me.chk1.Location = New System.Drawing.Point(17, 34)
        Me.chk1.Name = "chk1"
        Me.chk1.Size = New System.Drawing.Size(126, 23)
        Me.chk1.TabIndex = 1
        Me.chk1.Tag = "versao"
        Me.chk1.Text = "Checa Versão"
        Me.chk1.UseVisualStyleBackColor = True
        '
        'chk2
        '
        Me.chk2.AutoSize = True
        Me.chk2.Location = New System.Drawing.Point(17, 51)
        Me.chk2.Name = "chk2"
        Me.chk2.Size = New System.Drawing.Size(157, 23)
        Me.chk2.TabIndex = 2
        Me.chk2.Tag = "enviar"
        Me.chk2.Text = "Notifica Bloqueios"
        Me.chk2.UseVisualStyleBackColor = True
        '
        'chk14
        '
        Me.chk14.AutoSize = True
        Me.chk14.Location = New System.Drawing.Point(609, 231)
        Me.chk14.Name = "chk14"
        Me.chk14.Size = New System.Drawing.Size(220, 23)
        Me.chk14.TabIndex = 49
        Me.chk14.Tag = "flag_Isenta_imposto"
        Me.chk14.Text = "Isenta Cliente de Impostos"
        Me.chk14.UseVisualStyleBackColor = True
        '
        'chk13
        '
        Me.chk13.AutoSize = True
        Me.chk13.Location = New System.Drawing.Point(609, 87)
        Me.chk13.Name = "chk13"
        Me.chk13.Size = New System.Drawing.Size(149, 23)
        Me.chk13.TabIndex = 41
        Me.chk13.Tag = "flag_libera_Motorista"
        Me.chk13.Text = "Libera Motorista "
        Me.chk13.UseVisualStyleBackColor = True
        '
        'chk10
        '
        Me.chk10.AutoSize = True
        Me.chk10.Location = New System.Drawing.Point(17, 196)
        Me.chk10.Name = "chk10"
        Me.chk10.Size = New System.Drawing.Size(265, 23)
        Me.chk10.TabIndex = 10
        Me.chk10.Tag = "flag_vinculo"
        Me.chk10.Text = "Vincular Lotes (Tabela Cobrança)"
        Me.chk10.UseVisualStyleBackColor = True
        '
        'chk8
        '
        Me.chk8.AutoSize = True
        Me.chk8.Location = New System.Drawing.Point(17, 160)
        Me.chk8.Name = "chk8"
        Me.chk8.Size = New System.Drawing.Size(223, 23)
        Me.chk8.TabIndex = 8
        Me.chk8.Tag = "flag_libera_carga"
        Me.chk8.Text = "Libera Carga Fora do Prazo"
        Me.chk8.UseVisualStyleBackColor = True
        '
        'chk7
        '
        Me.chk7.AutoSize = True
        Me.chk7.Location = New System.Drawing.Point(17, 142)
        Me.chk7.Name = "chk7"
        Me.chk7.Size = New System.Drawing.Size(222, 23)
        Me.chk7.TabIndex = 7
        Me.chk7.Tag = "flag_edit_reg_imp"
        Me.chk7.Text = "Altera Registro Importação"
        Me.chk7.UseVisualStyleBackColor = True
        '
        'chk3
        '
        Me.chk3.AutoSize = True
        Me.chk3.Location = New System.Drawing.Point(17, 69)
        Me.chk3.Name = "chk3"
        Me.chk3.Size = New System.Drawing.Size(232, 23)
        Me.chk3.TabIndex = 3
        Me.chk3.Tag = "le_financeiro"
        Me.chk3.Text = "Lê Financeiro (Atendimento)"
        Me.chk3.UseVisualStyleBackColor = True
        '
        'chk4
        '
        Me.chk4.AutoSize = True
        Me.chk4.Location = New System.Drawing.Point(17, 87)
        Me.chk4.Name = "chk4"
        Me.chk4.Size = New System.Drawing.Size(178, 23)
        Me.chk4.TabIndex = 4
        Me.chk4.Tag = "rel_completo"
        Me.chk4.Text = "Visualiza Importador"
        Me.chk4.UseVisualStyleBackColor = True
        '
        'chk0
        '
        Me.chk0.AutoSize = True
        Me.chk0.Location = New System.Drawing.Point(17, 16)
        Me.chk0.Name = "chk0"
        Me.chk0.Size = New System.Drawing.Size(118, 23)
        Me.chk0.TabIndex = 0
        Me.chk0.Tag = "flag_balanca_manual"
        Me.chk0.Text = "Gate Manual"
        Me.chk0.UseVisualStyleBackColor = True
        '
        'chk49
        '
        Me.chk49.AutoSize = True
        Me.chk49.Location = New System.Drawing.Point(17, 302)
        Me.chk49.Name = "chk49"
        Me.chk49.Size = New System.Drawing.Size(278, 23)
        Me.chk49.TabIndex = 52
        Me.chk49.Tag = "Flag_Transit_Dif"
        Me.chk49.Text = "Param. de Tempo Av. e Calc. Man."
        Me.chk49.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(328, 592)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(84, 34)
        Me.btnCancelar.TabIndex = 84
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
        Me.btnNovo.Location = New System.Drawing.Point(14, 592)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(79, 34)
        Me.btnNovo.TabIndex = 83
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
        Me.btnExcluir.Location = New System.Drawing.Point(171, 592)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(79, 34)
        Me.btnExcluir.TabIndex = 82
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
        Me.btnSalvar.Location = New System.Drawing.Point(250, 592)
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
        Me.btnEditar.Location = New System.Drawing.Point(92, 592)
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
        Me.btnSair.Location = New System.Drawing.Point(742, 592)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(116, 34)
        Me.btnSair.TabIndex = 78
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnSair, "Fechar o formulário.")
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'txtCodigo
        '
        Me.txtCodigo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Location = New System.Drawing.Point(439, 599)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(78, 27)
        Me.txtCodigo.TabIndex = 79
        Me.txtCodigo.Visible = False
        '
        'btnFiltro
        '
        Me.btnFiltro.FlatAppearance.BorderSize = 0
        Me.btnFiltro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFiltro.Image = CType(resources.GetObject("btnFiltro.Image"), System.Drawing.Image)
        Me.btnFiltro.Location = New System.Drawing.Point(597, -2)
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
        Me.btnUltimo.Location = New System.Drawing.Point(673, -2)
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
        Me.btnProximo.Location = New System.Drawing.Point(657, -2)
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
        Me.btnAnterior.Location = New System.Drawing.Point(641, -2)
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
        Me.btnPrimeiro.Location = New System.Drawing.Point(623, -2)
        Me.btnPrimeiro.Name = "btnPrimeiro"
        Me.btnPrimeiro.Size = New System.Drawing.Size(26, 22)
        Me.btnPrimeiro.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnPrimeiro, "Vai até o primeiro registro.")
        Me.btnPrimeiro.UseVisualStyleBackColor = True
        '
        'pnControles1
        '
        Me.pnControles1.BackColor = System.Drawing.SystemColors.Control
        Me.pnControles1.Controls.Add(Me.btnFiltro)
        Me.pnControles1.Controls.Add(Me.btnUltimo)
        Me.pnControles1.Controls.Add(Me.btnProximo)
        Me.pnControles1.Controls.Add(Me.btnAnterior)
        Me.pnControles1.Controls.Add(Me.btnPrimeiro)
        Me.pnControles1.Location = New System.Drawing.Point(20, 521)
        Me.pnControles1.Name = "pnControles1"
        Me.pnControles1.Size = New System.Drawing.Size(710, 20)
        Me.pnControles1.TabIndex = 11
        '
        'FrmUsuarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(873, 642)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "FrmUsuarios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cadastro de Usuários"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnControles.ResumeLayout(False)
        CType(Me.dgvConsulta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.txtNovoEmail.ResumeLayout(False)
        Me.txtNovoEmail.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.pnControles1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnUltimo As System.Windows.Forms.Button
    Friend WithEvents btnProximo As System.Windows.Forms.Button
    Friend WithEvents btnAnterior As System.Windows.Forms.Button
    Friend WithEvents btnPrimeiro As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnFiltro As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnNovo As System.Windows.Forms.Button
    Friend WithEvents btnExcluir As System.Windows.Forms.Button
    Friend WithEvents btnSalvar As System.Windows.Forms.Button
    Friend WithEvents btnEditar As System.Windows.Forms.Button
    Friend WithEvents btnSair As System.Windows.Forms.Button
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents pnControles1 As System.Windows.Forms.Panel
    Friend WithEvents dgvConsulta As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtNovoEmail As System.Windows.Forms.GroupBox
    Friend WithEvents lstServicos As System.Windows.Forms.CheckedListBox
    Friend WithEvents lstGrupos As System.Windows.Forms.CheckedListBox
    Friend WithEvents cbPatio As System.Windows.Forms.ComboBox
    Friend WithEvents cbEmpresa As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbVendedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCPF As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNome As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSenha As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents chk51 As System.Windows.Forms.CheckBox
    Friend WithEvents chk50 As System.Windows.Forms.CheckBox
    Friend WithEvents chk48 As System.Windows.Forms.CheckBox
    Friend WithEvents chk47 As System.Windows.Forms.CheckBox
    Friend WithEvents chk46 As System.Windows.Forms.CheckBox
    Friend WithEvents chk45 As System.Windows.Forms.CheckBox
    Friend WithEvents chk44 As System.Windows.Forms.CheckBox
    Friend WithEvents chk43 As System.Windows.Forms.CheckBox
    Friend WithEvents chk42 As System.Windows.Forms.CheckBox
    Friend WithEvents chk41 As System.Windows.Forms.CheckBox
    Friend WithEvents chk40 As System.Windows.Forms.CheckBox
    Friend WithEvents chk39 As System.Windows.Forms.CheckBox
    Friend WithEvents chk38 As System.Windows.Forms.CheckBox
    Friend WithEvents chk37 As System.Windows.Forms.CheckBox
    Friend WithEvents chk36 As System.Windows.Forms.CheckBox
    Friend WithEvents chk33 As System.Windows.Forms.CheckBox
    Friend WithEvents chk32 As System.Windows.Forms.CheckBox
    Friend WithEvents chk30 As System.Windows.Forms.CheckBox
    Friend WithEvents chk27 As System.Windows.Forms.CheckBox
    Friend WithEvents chk22 As System.Windows.Forms.CheckBox
    Friend WithEvents chk23 As System.Windows.Forms.CheckBox
    Friend WithEvents chk35 As System.Windows.Forms.CheckBox
    Friend WithEvents chk34 As System.Windows.Forms.CheckBox
    Friend WithEvents chk31 As System.Windows.Forms.CheckBox
    Friend WithEvents chk29 As System.Windows.Forms.CheckBox
    Friend WithEvents chk28 As System.Windows.Forms.CheckBox
    Friend WithEvents chk21 As System.Windows.Forms.CheckBox
    Friend WithEvents chk20 As System.Windows.Forms.CheckBox
    Friend WithEvents chk19 As System.Windows.Forms.CheckBox
    Friend WithEvents chk18 As System.Windows.Forms.CheckBox
    Friend WithEvents chk17 As System.Windows.Forms.CheckBox
    Friend WithEvents chk16 As System.Windows.Forms.CheckBox
    Friend WithEvents chk15 As System.Windows.Forms.CheckBox
    Friend WithEvents chk12 As System.Windows.Forms.CheckBox
    Friend WithEvents chk11 As System.Windows.Forms.CheckBox
    Friend WithEvents chk9 As System.Windows.Forms.CheckBox
    Friend WithEvents chk5 As System.Windows.Forms.CheckBox
    Friend WithEvents chk6 As System.Windows.Forms.CheckBox
    Friend WithEvents chk1 As System.Windows.Forms.CheckBox
    Friend WithEvents chk2 As System.Windows.Forms.CheckBox
    Friend WithEvents chk14 As System.Windows.Forms.CheckBox
    Friend WithEvents chk13 As System.Windows.Forms.CheckBox
    Friend WithEvents chk10 As System.Windows.Forms.CheckBox
    Friend WithEvents chk8 As System.Windows.Forms.CheckBox
    Friend WithEvents chk7 As System.Windows.Forms.CheckBox
    Friend WithEvents chk3 As System.Windows.Forms.CheckBox
    Friend WithEvents chk4 As System.Windows.Forms.CheckBox
    Friend WithEvents chk0 As System.Windows.Forms.CheckBox
    Friend WithEvents chk49 As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnControles As System.Windows.Forms.Panel
    Friend WithEvents btnFiltro2 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSairRel As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rbInativos As System.Windows.Forms.RadioButton
    Friend WithEvents rbAtivos As System.Windows.Forms.RadioButton
    Friend WithEvents rbAmbos As System.Windows.Forms.RadioButton
    Friend WithEvents cbModuloRel As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbUsuarioRel As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cbGrupoRel As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnImprimirRel As System.Windows.Forms.Button
    Friend WithEvents Label14 As Label
    Friend WithEvents txtBuscarUsuario As TextBox
    Friend WithEvents btnPesquisarUsuario As Button
    Friend WithEvents colCodigo As DataGridViewTextBoxColumn
    Friend WithEvents colDescricao As DataGridViewTextBoxColumn
    Friend WithEvents colSenha As DataGridViewTextBoxColumn
    Friend WithEvents colPatio As DataGridViewTextBoxColumn
    Friend WithEvents colNome As DataGridViewTextBoxColumn
    Friend WithEvents colEmail As DataGridViewTextBoxColumn
    Friend WithEvents colVendedor As DataGridViewTextBoxColumn
    Friend WithEvents colEmpresa As DataGridViewTextBoxColumn
    Friend WithEvents colCPF As DataGridViewTextBoxColumn
    Friend WithEvents colAtivo As DataGridViewCheckBoxColumn
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
End Class
