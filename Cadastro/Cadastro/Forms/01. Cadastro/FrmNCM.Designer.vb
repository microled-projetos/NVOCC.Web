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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btAnt = New System.Windows.Forms.Button()
        Me.txtCodigoNCM = New System.Windows.Forms.TextBox()
        Me.BtProx = New System.Windows.Forms.Button()
        Me.lblRazao = New System.Windows.Forms.Label()
        Me.btPrim = New System.Windows.Forms.Button()
        Me.barraNavegacao = New System.Windows.Forms.Panel()
        Me.btFiltro = New System.Windows.Forms.Button()
        Me.btUlt = New System.Windows.Forms.Button()
        Me.PnGeral = New System.Windows.Forms.Panel()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnExcluir = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnSair = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkIbama = New System.Windows.Forms.CheckBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.chkAnvisa = New System.Windows.Forms.CheckBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.chkPoliciaCivil = New System.Windows.Forms.CheckBox()
        Me.chkMapa = New System.Windows.Forms.CheckBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescricao = New System.Windows.Forms.TextBox()
        Me.chkPoliciaFederal = New System.Windows.Forms.CheckBox()
        Me.chkExercito = New System.Windows.Forms.CheckBox()
        Me.dgvNCM = New System.Windows.Forms.DataGridView()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescricao = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPoliciaFederal = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colExercito = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colMapa = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colAnvisa = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colPoliciaCivil = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colIbama = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.barraNavegacao.SuspendLayout()
        Me.PnGeral.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNCM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btAnt
        '
        Me.btAnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btAnt.FlatAppearance.BorderSize = 0
        Me.btAnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btAnt.Image = CType(resources.GetObject("btAnt.Image"), System.Drawing.Image)
        Me.btAnt.Location = New System.Drawing.Point(1206, 0)
        Me.btAnt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.txtCodigoNCM.Location = New System.Drawing.Point(30, 59)
        Me.txtCodigoNCM.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCodigoNCM.MaxLength = 8
        Me.txtCodigoNCM.Name = "txtCodigoNCM"
        Me.txtCodigoNCM.Size = New System.Drawing.Size(148, 24)
        Me.txtCodigoNCM.TabIndex = 0
        '
        'BtProx
        '
        Me.BtProx.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtProx.FlatAppearance.BorderSize = 0
        Me.BtProx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtProx.Image = CType(resources.GetObject("BtProx.Image"), System.Drawing.Image)
        Me.BtProx.Location = New System.Drawing.Point(1225, 0)
        Me.BtProx.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.lblRazao.Location = New System.Drawing.Point(26, 37)
        Me.lblRazao.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRazao.Name = "lblRazao"
        Me.lblRazao.Size = New System.Drawing.Size(56, 17)
        Me.lblRazao.TabIndex = 109
        Me.lblRazao.Text = "Código:"
        '
        'btPrim
        '
        Me.btPrim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btPrim.FlatAppearance.BorderSize = 0
        Me.btPrim.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btPrim.Image = CType(resources.GetObject("btPrim.Image"), System.Drawing.Image)
        Me.btPrim.Location = New System.Drawing.Point(1184, 0)
        Me.btPrim.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.barraNavegacao.Location = New System.Drawing.Point(21, 292)
        Me.barraNavegacao.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.barraNavegacao.Name = "barraNavegacao"
        Me.barraNavegacao.Size = New System.Drawing.Size(1283, 28)
        Me.barraNavegacao.TabIndex = 110
        '
        'btFiltro
        '
        Me.btFiltro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btFiltro.FlatAppearance.BorderSize = 0
        Me.btFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFiltro.Image = CType(resources.GetObject("btFiltro.Image"), System.Drawing.Image)
        Me.btFiltro.Location = New System.Drawing.Point(1149, 0)
        Me.btFiltro.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btFiltro.Name = "btFiltro"
        Me.btFiltro.Size = New System.Drawing.Size(24, 23)
        Me.btFiltro.TabIndex = 63
        Me.btFiltro.UseVisualStyleBackColor = True
        '
        'btUlt
        '
        Me.btUlt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btUlt.FlatAppearance.BorderSize = 0
        Me.btUlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btUlt.Image = CType(resources.GetObject("btUlt.Image"), System.Drawing.Image)
        Me.btUlt.Location = New System.Drawing.Point(1246, 0)
        Me.btUlt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btUlt.Name = "btUlt"
        Me.btUlt.Size = New System.Drawing.Size(24, 23)
        Me.btUlt.TabIndex = 62
        Me.btUlt.UseVisualStyleBackColor = True
        '
        'PnGeral
        '
        Me.PnGeral.BackColor = System.Drawing.Color.Transparent
        Me.PnGeral.Controls.Add(Me.txtCodigo)
        Me.PnGeral.Controls.Add(Me.btnCancelar)
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
        Me.PnGeral.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PnGeral.Name = "PnGeral"
        Me.PnGeral.Size = New System.Drawing.Size(1326, 819)
        Me.PnGeral.TabIndex = 126
        '
        'txtCodigo
        '
        Me.txtCodigo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCodigo.BackColor = System.Drawing.Color.White
        Me.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.txtCodigo.Location = New System.Drawing.Point(557, 767)
        Me.txtCodigo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCodigo.MaxLength = 8
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(57, 27)
        Me.txtCodigo.TabIndex = 129
        Me.txtCodigo.Visible = False
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.Location = New System.Drawing.Point(440, 759)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.btnNovo.Location = New System.Drawing.Point(19, 759)
        Me.btnNovo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.btnExcluir.Location = New System.Drawing.Point(230, 759)
        Me.btnExcluir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.btnSalvar.Location = New System.Drawing.Point(334, 759)
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.btnEditar.Location = New System.Drawing.Point(125, 759)
        Me.btnEditar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.btnSair.Location = New System.Drawing.Point(1184, 759)
        Me.btnSair.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
        Me.GroupBox1.Controls.Add(Me.chkIbama)
        Me.GroupBox1.Controls.Add(Me.PictureBox6)
        Me.GroupBox1.Controls.Add(Me.chkAnvisa)
        Me.GroupBox1.Controls.Add(Me.PictureBox5)
        Me.GroupBox1.Controls.Add(Me.chkPoliciaCivil)
        Me.GroupBox1.Controls.Add(Me.chkMapa)
        Me.GroupBox1.Controls.Add(Me.PictureBox4)
        Me.GroupBox1.Controls.Add(Me.PictureBox3)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.txtCodigoNCM)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtDescricao)
        Me.GroupBox1.Controls.Add(Me.chkPoliciaFederal)
        Me.GroupBox1.Controls.Add(Me.lblRazao)
        Me.GroupBox1.Controls.Add(Me.chkExercito)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 11)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(1284, 273)
        Me.GroupBox1.TabIndex = 121
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalhes"
        '
        'chkIbama
        '
        Me.chkIbama.AutoSize = True
        Me.chkIbama.Enabled = False
        Me.chkIbama.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIbama.Location = New System.Drawing.Point(1047, 213)
        Me.chkIbama.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkIbama.Name = "chkIbama"
        Me.chkIbama.Size = New System.Drawing.Size(124, 21)
        Me.chkIbama.TabIndex = 128
        Me.chkIbama.Text = "Controle IBAMA"
        Me.chkIbama.UseVisualStyleBackColor = True
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(1071, 114)
        Me.PictureBox6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(89, 86)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 127
        Me.PictureBox6.TabStop = False
        '
        'chkAnvisa
        '
        Me.chkAnvisa.AutoSize = True
        Me.chkAnvisa.Enabled = False
        Me.chkAnvisa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnvisa.Location = New System.Drawing.Point(711, 213)
        Me.chkAnvisa.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkAnvisa.Name = "chkAnvisa"
        Me.chkAnvisa.Size = New System.Drawing.Size(86, 38)
        Me.chkAnvisa.TabIndex = 126
        Me.chkAnvisa.Text = "Controle " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ANVISA"
        Me.chkAnvisa.UseVisualStyleBackColor = True
        '
        'PictureBox5
        '
        Me.PictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(712, 114)
        Me.PictureBox5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(89, 86)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 125
        Me.PictureBox5.TabStop = False
        '
        'chkPoliciaCivil
        '
        Me.chkPoliciaCivil.AutoSize = True
        Me.chkPoliciaCivil.Enabled = False
        Me.chkPoliciaCivil.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPoliciaCivil.Location = New System.Drawing.Point(865, 213)
        Me.chkPoliciaCivil.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkPoliciaCivil.Name = "chkPoliciaCivil"
        Me.chkPoliciaCivil.Size = New System.Drawing.Size(126, 38)
        Me.chkPoliciaCivil.TabIndex = 124
        Me.chkPoliciaCivil.Text = "Controle Polícia " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Civil"
        Me.chkPoliciaCivil.UseVisualStyleBackColor = True
        '
        'chkMapa
        '
        Me.chkMapa.AutoSize = True
        Me.chkMapa.Enabled = False
        Me.chkMapa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMapa.Location = New System.Drawing.Point(541, 213)
        Me.chkMapa.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkMapa.Name = "chkMapa"
        Me.chkMapa.Size = New System.Drawing.Size(86, 38)
        Me.chkMapa.TabIndex = 123
        Me.chkMapa.Text = "Controle " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "MAPA"
        Me.chkMapa.UseVisualStyleBackColor = True
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(890, 114)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(89, 86)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 122
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(543, 114)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(89, 86)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 121
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(370, 114)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(89, 86)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 120
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(192, 114)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(89, 86)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 119
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(188, 37)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 17)
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
        Me.txtDescricao.Location = New System.Drawing.Point(192, 59)
        Me.txtDescricao.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDescricao.MaxLength = 60
        Me.txtDescricao.Name = "txtDescricao"
        Me.txtDescricao.Size = New System.Drawing.Size(1063, 24)
        Me.txtDescricao.TabIndex = 1
        '
        'chkPoliciaFederal
        '
        Me.chkPoliciaFederal.AutoSize = True
        Me.chkPoliciaFederal.Enabled = False
        Me.chkPoliciaFederal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPoliciaFederal.Location = New System.Drawing.Point(170, 213)
        Me.chkPoliciaFederal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkPoliciaFederal.Name = "chkPoliciaFederal"
        Me.chkPoliciaFederal.Size = New System.Drawing.Size(126, 38)
        Me.chkPoliciaFederal.TabIndex = 118
        Me.chkPoliciaFederal.Text = "Controle Polícia " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Federal"
        Me.chkPoliciaFederal.UseVisualStyleBackColor = True
        '
        'chkExercito
        '
        Me.chkExercito.AutoSize = True
        Me.chkExercito.Enabled = False
        Me.chkExercito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExercito.Location = New System.Drawing.Point(364, 213)
        Me.chkExercito.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkExercito.Name = "chkExercito"
        Me.chkExercito.Size = New System.Drawing.Size(86, 38)
        Me.chkExercito.TabIndex = 117
        Me.chkExercito.Text = "Controle " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Exército"
        Me.chkExercito.UseVisualStyleBackColor = True
        '
        'dgvNCM
        '
        Me.dgvNCM.AllowUserToAddRows = False
        Me.dgvNCM.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        Me.dgvNCM.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvNCM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNCM.BackgroundColor = System.Drawing.Color.White
        Me.dgvNCM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNCM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCodigo, Me.colDescricao, Me.colPoliciaFederal, Me.colExercito, Me.colMapa, Me.colAnvisa, Me.colPoliciaCivil, Me.colIbama})
        Me.dgvNCM.Location = New System.Drawing.Point(21, 330)
        Me.dgvNCM.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgvNCM.Name = "dgvNCM"
        Me.dgvNCM.ReadOnly = True
        Me.dgvNCM.RowHeadersWidth = 18
        Me.dgvNCM.RowTemplate.Height = 18
        Me.dgvNCM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNCM.Size = New System.Drawing.Size(1284, 414)
        Me.dgvNCM.TabIndex = 107
        '
        'colCodigo
        '
        Me.colCodigo.DataPropertyName = "CODIGO"
        Me.colCodigo.HeaderText = "Código"
        Me.colCodigo.Name = "colCodigo"
        Me.colCodigo.ReadOnly = True
        Me.colCodigo.Width = 68
        '
        'colDescricao
        '
        Me.colDescricao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDescricao.DataPropertyName = "DESCR"
        Me.colDescricao.HeaderText = "Descrição"
        Me.colDescricao.Name = "colDescricao"
        Me.colDescricao.ReadOnly = True
        '
        'colPoliciaFederal
        '
        Me.colPoliciaFederal.DataPropertyName = "POLICIA_FEDERAL"
        Me.colPoliciaFederal.FalseValue = "0"
        Me.colPoliciaFederal.HeaderText = "Pol.Federal"
        Me.colPoliciaFederal.Name = "colPoliciaFederal"
        Me.colPoliciaFederal.ReadOnly = True
        Me.colPoliciaFederal.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colPoliciaFederal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colPoliciaFederal.TrueValue = "1"
        Me.colPoliciaFederal.Width = 76
        '
        'colExercito
        '
        Me.colExercito.DataPropertyName = "EXERCITO"
        Me.colExercito.FalseValue = "0"
        Me.colExercito.HeaderText = "Exército"
        Me.colExercito.Name = "colExercito"
        Me.colExercito.ReadOnly = True
        Me.colExercito.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colExercito.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colExercito.TrueValue = "1"
        Me.colExercito.Width = 76
        '
        'colMapa
        '
        Me.colMapa.DataPropertyName = "MAPA"
        Me.colMapa.FalseValue = "0"
        Me.colMapa.HeaderText = "MAPA"
        Me.colMapa.Name = "colMapa"
        Me.colMapa.ReadOnly = True
        Me.colMapa.TrueValue = "1"
        Me.colMapa.Width = 76
        '
        'colAnvisa
        '
        Me.colAnvisa.DataPropertyName = "ANVISA"
        Me.colAnvisa.FalseValue = "0"
        Me.colAnvisa.HeaderText = "ANVISA"
        Me.colAnvisa.Name = "colAnvisa"
        Me.colAnvisa.ReadOnly = True
        Me.colAnvisa.TrueValue = "1"
        Me.colAnvisa.Width = 76
        '
        'colPoliciaCivil
        '
        Me.colPoliciaCivil.DataPropertyName = "POLICIA_CIVIL"
        Me.colPoliciaCivil.FalseValue = "0"
        Me.colPoliciaCivil.HeaderText = "Pol.Civil"
        Me.colPoliciaCivil.Name = "colPoliciaCivil"
        Me.colPoliciaCivil.ReadOnly = True
        Me.colPoliciaCivil.TrueValue = "1"
        Me.colPoliciaCivil.Width = 76
        '
        'colIbama
        '
        Me.colIbama.DataPropertyName = "IBAMA"
        Me.colIbama.FalseValue = "0"
        Me.colIbama.HeaderText = "IBAMA"
        Me.colIbama.Name = "colIbama"
        Me.colIbama.ReadOnly = True
        Me.colIbama.TrueValue = "1"
        Me.colIbama.Width = 76
        '
        'FrmNCM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1326, 819)
        Me.Controls.Add(Me.PnGeral)
        Me.Font = New System.Drawing.Font("Tahoma", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmNCM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "SISCAD_MNU_NCM"
        Me.Text = "Cadastro de NCM"
        Me.barraNavegacao.ResumeLayout(False)
        Me.PnGeral.ResumeLayout(False)
        Me.PnGeral.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents chkPoliciaFederal As System.Windows.Forms.CheckBox
    Friend WithEvents chkExercito As System.Windows.Forms.CheckBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents dgvNCM As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnNovo As Button
    Friend WithEvents btnExcluir As Button
    Friend WithEvents btnSalvar As Button
    Friend WithEvents btnEditar As Button
    Friend WithEvents btnSair As Button
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents chkPoliciaCivil As CheckBox
    Friend WithEvents chkMapa As CheckBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents chkAnvisa As CheckBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents chkIbama As CheckBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents colCodigo As DataGridViewTextBoxColumn
    Friend WithEvents colDescricao As DataGridViewTextBoxColumn
    Friend WithEvents colPoliciaFederal As DataGridViewCheckBoxColumn
    Friend WithEvents colExercito As DataGridViewCheckBoxColumn
    Friend WithEvents colMapa As DataGridViewCheckBoxColumn
    Friend WithEvents colAnvisa As DataGridViewCheckBoxColumn
    Friend WithEvents colPoliciaCivil As DataGridViewCheckBoxColumn
    Friend WithEvents colIbama As DataGridViewCheckBoxColumn
End Class
