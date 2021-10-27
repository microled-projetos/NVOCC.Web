<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNCM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNCM))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btAnt = New System.Windows.Forms.Button()
        Me.txtCodigoNCM = New System.Windows.Forms.TextBox()
        Me.BtProx = New System.Windows.Forms.Button()
        Me.lblRazao = New System.Windows.Forms.Label()
        Me.btPrim = New System.Windows.Forms.Button()
        Me.barraNavegacao = New System.Windows.Forms.Panel()
        Me.btFiltro = New System.Windows.Forms.Button()
        Me.btUlt = New System.Windows.Forms.Button()
        Me.PnGeral = New System.Windows.Forms.Panel()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ckbAtivo = New System.Windows.Forms.CheckBox()
        Me.txtAPNCM = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.dgvNCM = New System.Windows.Forms.DataGridView()
        Me.ID_NCM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CD_NCM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NM_NCM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AP_NCM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FL_ATIVO = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.barraNavegacao.SuspendLayout()
        Me.PnGeral.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvNCM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btAnt
        '
        Me.btAnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAnt.FlatAppearance.BorderSize = 0
        Me.btAnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btAnt.Image = CType(resources.GetObject("btAnt.Image"), System.Drawing.Image)
        Me.btAnt.Location = New System.Drawing.Point(878, 4)
        Me.btAnt.Margin = New System.Windows.Forms.Padding(4)
        Me.btAnt.Name = "btAnt"
        Me.btAnt.Size = New System.Drawing.Size(24, 23)
        Me.btAnt.TabIndex = 58
        Me.btAnt.UseVisualStyleBackColor = True
        '
        'txtCodigoNCM
        '
        Me.txtCodigoNCM.BackColor = System.Drawing.Color.White
        Me.txtCodigoNCM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigoNCM.Enabled = False
        Me.txtCodigoNCM.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoNCM.Location = New System.Drawing.Point(8, 53)
        Me.txtCodigoNCM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoNCM.MaxLength = 8
        Me.txtCodigoNCM.Name = "txtCodigoNCM"
        Me.txtCodigoNCM.Size = New System.Drawing.Size(89, 21)
        Me.txtCodigoNCM.TabIndex = 0
        Me.txtCodigoNCM.Tag = "requerido"
        '
        'BtProx
        '
        Me.BtProx.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtProx.FlatAppearance.BorderSize = 0
        Me.BtProx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtProx.Image = CType(resources.GetObject("BtProx.Image"), System.Drawing.Image)
        Me.BtProx.Location = New System.Drawing.Point(897, 4)
        Me.BtProx.Margin = New System.Windows.Forms.Padding(4)
        Me.BtProx.Name = "BtProx"
        Me.BtProx.Size = New System.Drawing.Size(24, 23)
        Me.BtProx.TabIndex = 59
        Me.BtProx.UseVisualStyleBackColor = True
        '
        'lblRazao
        '
        Me.lblRazao.AutoSize = True
        Me.lblRazao.BackColor = System.Drawing.Color.Transparent
        Me.lblRazao.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRazao.Location = New System.Drawing.Point(8, 36)
        Me.lblRazao.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRazao.Name = "lblRazao"
        Me.lblRazao.Size = New System.Drawing.Size(44, 13)
        Me.lblRazao.TabIndex = 109
        Me.lblRazao.Text = "Código:"
        '
        'btPrim
        '
        Me.btPrim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btPrim.FlatAppearance.BorderSize = 0
        Me.btPrim.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btPrim.Image = CType(resources.GetObject("btPrim.Image"), System.Drawing.Image)
        Me.btPrim.Location = New System.Drawing.Point(856, 4)
        Me.btPrim.Margin = New System.Windows.Forms.Padding(4)
        Me.btPrim.Name = "btPrim"
        Me.btPrim.Size = New System.Drawing.Size(24, 23)
        Me.btPrim.TabIndex = 61
        Me.btPrim.UseVisualStyleBackColor = True
        '
        'barraNavegacao
        '
        Me.barraNavegacao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.barraNavegacao.BackColor = System.Drawing.Color.WhiteSmoke
        Me.barraNavegacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.barraNavegacao.Controls.Add(Me.btFiltro)
        Me.barraNavegacao.Controls.Add(Me.btUlt)
        Me.barraNavegacao.Controls.Add(Me.btPrim)
        Me.barraNavegacao.Controls.Add(Me.BtProx)
        Me.barraNavegacao.Controls.Add(Me.btAnt)
        Me.barraNavegacao.Location = New System.Drawing.Point(21, 163)
        Me.barraNavegacao.Margin = New System.Windows.Forms.Padding(4)
        Me.barraNavegacao.Name = "barraNavegacao"
        Me.barraNavegacao.Size = New System.Drawing.Size(979, 28)
        Me.barraNavegacao.TabIndex = 110
        '
        'btFiltro
        '
        Me.btFiltro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btFiltro.FlatAppearance.BorderSize = 0
        Me.btFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFiltro.Image = CType(resources.GetObject("btFiltro.Image"), System.Drawing.Image)
        Me.btFiltro.Location = New System.Drawing.Point(821, 4)
        Me.btFiltro.Margin = New System.Windows.Forms.Padding(4)
        Me.btFiltro.Name = "btFiltro"
        Me.btFiltro.Size = New System.Drawing.Size(24, 23)
        Me.btFiltro.TabIndex = 63
        Me.btFiltro.UseVisualStyleBackColor = True
        Me.btFiltro.Visible = False
        '
        'btUlt
        '
        Me.btUlt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btUlt.FlatAppearance.BorderSize = 0
        Me.btUlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btUlt.Image = CType(resources.GetObject("btUlt.Image"), System.Drawing.Image)
        Me.btUlt.Location = New System.Drawing.Point(919, 4)
        Me.btUlt.Margin = New System.Windows.Forms.Padding(4)
        Me.btUlt.Name = "btUlt"
        Me.btUlt.Size = New System.Drawing.Size(25, 23)
        Me.btUlt.TabIndex = 62
        Me.btUlt.UseVisualStyleBackColor = True
        '
        'PnGeral
        '
        Me.PnGeral.BackColor = System.Drawing.Color.Transparent
        Me.PnGeral.Controls.Add(Me.btnCancelar)
        Me.PnGeral.Controls.Add(Me.txtID)
        Me.PnGeral.Controls.Add(Me.btnNovo)
        Me.PnGeral.Controls.Add(Me.btnExcluir)
        Me.PnGeral.Controls.Add(Me.btnSalvar)
        Me.PnGeral.Controls.Add(Me.btnEditar)
        Me.PnGeral.Controls.Add(Me.btnSair)
        Me.PnGeral.Controls.Add(Me.GroupBox1)
        Me.PnGeral.Controls.Add(Me.dgvNCM)
        Me.PnGeral.Controls.Add(Me.barraNavegacao)
        Me.PnGeral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnGeral.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnGeral.Location = New System.Drawing.Point(0, 0)
        Me.PnGeral.Margin = New System.Windows.Forms.Padding(4)
        Me.PnGeral.Name = "PnGeral"
        Me.PnGeral.Size = New System.Drawing.Size(1023, 535)
        Me.PnGeral.TabIndex = 126
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(443, 478)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(112, 44)
        Me.btnCancelar.TabIndex = 128
        Me.btnCancelar.Tag = "Cancelar"
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnNovo
        '
        Me.btnNovo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNovo.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.btnNovo.Image = CType(resources.GetObject("btnNovo.Image"), System.Drawing.Image)
        Me.btnNovo.Location = New System.Drawing.Point(22, 478)
        Me.btnNovo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(105, 44)
        Me.btnNovo.TabIndex = 127
        Me.btnNovo.Tag = "Adiciona um novo registro"
        Me.btnNovo.Text = "Novo"
        Me.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'btnExcluir
        '
        Me.btnExcluir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcluir.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.btnExcluir.Image = CType(resources.GetObject("btnExcluir.Image"), System.Drawing.Image)
        Me.btnExcluir.Location = New System.Drawing.Point(233, 478)
        Me.btnExcluir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(105, 44)
        Me.btnExcluir.TabIndex = 126
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
        Me.btnSalvar.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.btnSalvar.Image = CType(resources.GetObject("btnSalvar.Image"), System.Drawing.Image)
        Me.btnSalvar.Location = New System.Drawing.Point(337, 478)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(105, 44)
        Me.btnSalvar.TabIndex = 125
        Me.btnSalvar.Tag = "Salva todas as mudanças"
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEditar.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.btnEditar.Image = CType(resources.GetObject("btnEditar.Image"), System.Drawing.Image)
        Me.btnEditar.Location = New System.Drawing.Point(128, 478)
        Me.btnEditar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(105, 44)
        Me.btnEditar.TabIndex = 124
        Me.btnEditar.Tag = "Editar Registro Selecionado"
        Me.btnEditar.Text = "Editar"
        Me.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'btnSair
        '
        Me.btnSair.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSair.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.btnSair.Image = CType(resources.GetObject("btnSair.Image"), System.Drawing.Image)
        Me.btnSair.Location = New System.Drawing.Point(883, 475)
        Me.btnSair.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSair.Name = "btnSair"
        Me.btnSair.Size = New System.Drawing.Size(120, 44)
        Me.btnSair.TabIndex = 122
        Me.btnSair.Tag = "Sair"
        Me.btnSair.Text = "Sair [ESC]"
        Me.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSair.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ckbAtivo)
        Me.GroupBox1.Controls.Add(Me.txtAPNCM)
        Me.GroupBox1.Controls.Add(Me.txtCodigoNCM)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtDescricao)
        Me.GroupBox1.Controls.Add(Me.lblRazao)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 31)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(979, 82)
        Me.GroupBox1.TabIndex = 121
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes"
        '
        'txtID
        '
        Me.txtID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtID.BackColor = System.Drawing.Color.White
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.txtID.Location = New System.Drawing.Point(635, 496)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(50, 23)
        Me.txtID.TabIndex = 129
        Me.txtID.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(554, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 16)
        Me.Label3.TabIndex = 136
        Me.Label3.Text = "*"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(46, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 16)
        Me.Label1.TabIndex = 135
        Me.Label1.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(165, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 16)
        Me.Label6.TabIndex = 130
        Me.Label6.Text = "*"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(509, 35)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 134
        Me.Label4.Text = "AP NCM:"
        '
        'ckbAtivo
        '
        Me.ckbAtivo.AutoSize = True
        Me.ckbAtivo.Enabled = False
        Me.ckbAtivo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbAtivo.Location = New System.Drawing.Point(920, 57)
        Me.ckbAtivo.Margin = New System.Windows.Forms.Padding(4)
        Me.ckbAtivo.Name = "ckbAtivo"
        Me.ckbAtivo.Size = New System.Drawing.Size(51, 17)
        Me.ckbAtivo.TabIndex = 131
        Me.ckbAtivo.Text = "Ativo"
        Me.ckbAtivo.UseVisualStyleBackColor = True
        '
        'txtAPNCM
        '
        Me.txtAPNCM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAPNCM.BackColor = System.Drawing.Color.White
        Me.txtAPNCM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAPNCM.Enabled = False
        Me.txtAPNCM.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPNCM.Location = New System.Drawing.Point(512, 53)
        Me.txtAPNCM.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAPNCM.MaxLength = 60
        Me.txtAPNCM.Name = "txtAPNCM"
        Me.txtAPNCM.Size = New System.Drawing.Size(391, 21)
        Me.txtAPNCM.TabIndex = 129
        Me.txtAPNCM.Tag = "requerido"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(113, 31)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "Descrição:"
        '
        'txtDescricao
        '
        Me.txtDescricao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescricao.BackColor = System.Drawing.Color.White
        Me.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDescricao.Enabled = False
        Me.txtDescricao.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescricao.Location = New System.Drawing.Point(116, 53)
        Me.txtDescricao.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescricao.MaxLength = 60
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(375, 21)
        Me.txtDescricao.TabIndex = 1
        Me.txtDescricao.Tag = "requerido"
        '
        'dgvNCM
        '
        Me.dgvNCM.AllowUserToAddRows = False
        Me.dgvNCM.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvNCM.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvNCM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNCM.BackgroundColor = System.Drawing.Color.White
        Me.dgvNCM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNCM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID_NCM, Me.CD_NCM, Me.NM_NCM, Me.AP_NCM, Me.FL_ATIVO})
        Me.dgvNCM.Location = New System.Drawing.Point(21, 201)
        Me.dgvNCM.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvNCM.Name = "dgvNCM"
        Me.dgvNCM.ReadOnly = True
        Me.dgvNCM.RowHeadersWidth = 18
        Me.dgvNCM.RowTemplate.Height = 18
        Me.dgvNCM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNCM.Size = New System.Drawing.Size(979, 266)
        Me.dgvNCM.TabIndex = 107
        '
        'ID_NCM
        '
        Me.ID_NCM.DataPropertyName = "ID_NCM"
        Me.ID_NCM.HeaderText = "ID"
        Me.ID_NCM.Name = "ID_NCM"
        Me.ID_NCM.ReadOnly = True
        Me.ID_NCM.Width = 50
        '
        'CD_NCM
        '
        Me.CD_NCM.DataPropertyName = "CD_NCM"
        Me.CD_NCM.HeaderText = "CODIGO_NCM"
        Me.CD_NCM.Name = "CD_NCM"
        Me.CD_NCM.ReadOnly = True
        '
        'NM_NCM
        '
        Me.NM_NCM.DataPropertyName = "NM_NCM"
        Me.NM_NCM.HeaderText = "NM_NCM"
        Me.NM_NCM.Name = "NM_NCM"
        Me.NM_NCM.ReadOnly = True
        Me.NM_NCM.Width = 350
        '
        'AP_NCM
        '
        Me.AP_NCM.DataPropertyName = "AP_NCM"
        Me.AP_NCM.HeaderText = "AP_NCM"
        Me.AP_NCM.Name = "AP_NCM"
        Me.AP_NCM.ReadOnly = True
        Me.AP_NCM.Width = 350
        '
        'FL_ATIVO
        '
        Me.FL_ATIVO.DataPropertyName = "FL_ATIVO"
        Me.FL_ATIVO.FalseValue = "0"
        Me.FL_ATIVO.HeaderText = "Ativo"
        Me.FL_ATIVO.Name = "FL_ATIVO"
        Me.FL_ATIVO.ReadOnly = True
        Me.FL_ATIVO.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FL_ATIVO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.FL_ATIVO.TrueValue = "1"
        Me.FL_ATIVO.Width = 50
        '
        'FrmNCM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1023, 535)
        Me.Controls.Add(Me.PnGeral)
        Me.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmNCM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "SISCAD_MNU_NCM"
        Me.Text = "Cadastro de NCM"
        Me.barraNavegacao.ResumeLayout(False)
        Me.PnGeral.ResumeLayout(False)
        Me.PnGeral.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvNCM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btAnt As System.Windows.Forms.Button
    Friend WithEvents txtCodigoNCM As System.Windows.Forms.TextBox
    Friend WithEvents BtProx As System.Windows.Forms.Button
    Friend WithEvents lblRazao As System.Windows.Forms.Label
    Friend WithEvents btPrim As System.Windows.Forms.Button
    Friend WithEvents barraNavegacao As System.Windows.Forms.Panel
    Friend WithEvents btFiltro As System.Windows.Forms.Button
    Friend WithEvents btUlt As System.Windows.Forms.Button
    Friend WithEvents PnGeral As System.Windows.Forms.Panel
    Friend WithEvents txtDescricao As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents dgvNCM As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnNovo As Button
    Friend WithEvents btnExcluir As Button
    Friend WithEvents btnSalvar As Button
    Friend WithEvents btnEditar As Button
    Friend WithEvents btnSair As Button
    Friend WithEvents txtID As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ckbAtivo As CheckBox
    Friend WithEvents txtAPNCM As TextBox
    Friend WithEvents ID_NCM As DataGridViewTextBoxColumn
    Friend WithEvents CD_NCM As DataGridViewTextBoxColumn
    Friend WithEvents NM_NCM As DataGridViewTextBoxColumn
    Friend WithEvents AP_NCM As DataGridViewTextBoxColumn
    Friend WithEvents FL_ATIVO As DataGridViewCheckBoxColumn
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
End Class
