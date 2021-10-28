Imports System.Text
Imports DgvFilterPopup
Public Class FrmUsuarios

    Private Coluna As Integer
    Dim Filtro As DgvFilterManager

    Private Sub ConsultarPatios()
        Me.cbPatio.DataSource = Banco.List("SELECT AUTONUM,DESCR_RESUMIDO AS DESCR FROM " & Banco.BancoNVOCC & "TB_PATIOS")
        Me.cbPatio.SelectedIndex = -1
    End Sub

    Private Sub ConsultarGrupos()

        Dim ds As New DataTable
        ds = Banco.List("SELECT CODGRUPO, CONVERT(VARCHAR,CODGRUPO) + ' - ' + DESCRICAO AS DESCRICAO FROM " & Banco.BancoNVOCC & "TB_SYS_GRUPO_USER ORDER BY DESCRICAO")

        Me.lstGrupos.Items.Clear()

        For Each Item As DataRow In ds.Rows
            Me.lstGrupos.Items.Add(Item(1).ToString())
        Next

    End Sub

    Private Sub ConsultarServicosTerminal()

        Dim ds As New DataTable
        ds = Banco.List("SELECT CODE, CONVERT(VARCHAR,CODE) + ' - ' + DESCR AS DESCR FROM " & Banco.BancoNVOCC & "TB_MOTIVO_POSICAO ORDER BY DESCR")

        Me.lstServicos.Items.Clear()

        For Each Item As DataRow In ds.Rows
            Me.lstServicos.Items.Add(Item(1).ToString())
        Next

    End Sub

    Private Sub ConsultarEmpresas()
        Me.cbEmpresa.DataSource = Banco.List("SELECT AUTONUM,NOME_FANTASIA AS DESCRICAO FROM " & Banco.BancoNVOCC & "TB_EMPRESAS")
        Me.cbEmpresa.SelectedIndex = -1
    End Sub

    Private Sub ConsultarVendedores()
        Me.cbVendedor.DataSource = Banco.List("SELECT AUTONUM,FANTASIA FROM " & Banco.BancoNVOCC & "TB_CAD_PARCEIROS WHERE FLAG_VENDEDOR = 1")
        Me.cbVendedor.SelectedIndex = -1
    End Sub

    Private Sub Consultar(ByVal Chave As String)

        Chave = Chave.ToUpper()

        Cursor = Cursors.WaitCursor

        If Me.txtBuscarUsuario.Text <> String.Empty Then
            Me.dgvConsulta.DataSource = Banco.List("SELECT AUTONUM,USUARIO,SENHA,ISNULL(PATIO,0) PATIO,NOME,EMAIL,ISNULL(ID_VENDEDOR,0) ID_VENDEDOR,ISNULL(COD_EMPRESA,0) COD_EMPRESA,CPF, FLAG_ATIVO FROM " & Banco.BancoNVOCC & "TB_CAD_USUARIOS WHERE USUARIO LIKE '%" & Chave & "%' OR NOME LIKE '%" & Chave & "%'")
        Else
            Me.dgvConsulta.DataSource = Banco.List("SELECT AUTONUM,USUARIO,SENHA,ISNULL(PATIO,0) PATIO,NOME,EMAIL,ISNULL(ID_VENDEDOR,0) ID_VENDEDOR,ISNULL(COD_EMPRESA,0) COD_EMPRESA,CPF, FLAG_ATIVO FROM " & Banco.BancoNVOCC & "TB_CAD_USUARIOS")
        End If

        Cursor = Cursors.Arrow

    End Sub

    Private Sub SetaControles()

        btnNovo.Enabled = Not (btnNovo.Enabled)
        btnEditar.Enabled = Not (btnEditar.Enabled)
        btnSalvar.Enabled = Not (btnSalvar.Enabled)
        btnExcluir.Enabled = Not (btnExcluir.Enabled)
        btnCancelar.Enabled = Not (btnCancelar.Enabled)
        dgvConsulta.Enabled = Not (dgvConsulta.Enabled)
        pnControles.Enabled = Not (pnControles.Enabled)

    End Sub

    Private Sub FrmPrincipal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If Not Filtro Is Nothing Then
            Filtro.ActivateAllFilters(False)
        End If

        Consultar(String.Empty)
        ConsultarGrupos()
        ConsultarEmpresas()
        ConsultarVendedores()
        ConsultarServicosTerminal()
        ConsultarPatios()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Filtro = New DgvFilterManager(Me.dgvConsulta)
            LoadFilters(Filtro)
        End If

        FundoTextBox(Me)

    End Sub

    Private Sub btnNovo_Click(sender As System.Object, e As System.EventArgs) Handles btnNovo.Click

        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtNome.Focus()

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then

            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtUsuario.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtSenha.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            Me.cbPatio.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            Me.txtNome.Text = Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString()
            Me.txtEmail.Text = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            Me.cbVendedor.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            Me.cbEmpresa.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString()
            Me.txtCPF.Text = Me.dgvConsulta.CurrentRow.Cells(8).Value.ToString()

            Dim SQL As New StringBuilder

            For Each Controle As Control In TabPage2.Controls
                If TypeOf Controle Is CheckBox Then
                    If Not TryCast(Controle, CheckBox).TabIndex = 52 Then
                        SQL.Append("ISNULL(" & TryCast(Controle, CheckBox).Tag & ",0) " & TryCast(Controle, CheckBox).Tag & ",")
                    Else
                        SQL.Append("ISNULL(" & TryCast(Controle, CheckBox).Tag & ",0) " & TryCast(Controle, CheckBox).Tag)
                    End If
                End If
            Next

            Dim Ds As New DataTable

            Ds = Banco.List("SELECT " & SQL.ToString() & " FROM " & Banco.BancoNVOCC & "TB_CAD_USUARIOS WHERE AUTONUM = " & Me.txtCodigo.Text)

            If Not Ds Is Nothing Then
                For Each Controle As Control In TabPage2.Controls
                    If TypeOf Controle Is CheckBox Then
                        TryCast(Controle, CheckBox).Checked = Ds.Rows(0)(TryCast(Controle, CheckBox).Tag.ToString())
                    End If
                Next
            End If

            Ds.Clear()

            Ds = Banco.List("SELECT CODGRUPO FROM " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS WHERE AUTONUMUSER = " & txtCodigo.Text)

            For i As Integer = 0 To lstGrupos.Items.Count - 1
                lstGrupos.SetItemChecked(i, False)
            Next

            If Not Ds Is Nothing Then
                For i As Integer = 0 To Ds.Rows.Count - 1
                    For j As Integer = 0 To lstGrupos.Items.Count - 1
                        If Convert.ToInt32(lstGrupos.Items(j).ToString().Split("-")(0).ToString()) = Convert.ToInt32(Ds.Rows(i)(0).ToString()) Then
                            lstGrupos.SetItemChecked(j, True)
                        End If
                    Next                    
                Next
            End If
            
            Ds.Clear()

            Ds = Banco.List("SELECT A.AUTONUM_POSICIONAMENTO FROM " & Banco.BancoNVOCC & "TB_AMR_USU_POSICIONAMENTO A LEFT JOIN " & Banco.BancoNVOCC & "TB_MOTIVO_POSICAO B ON A.AUTONUM_POSICIONAMENTO = B.CODE WHERE A.AUTONUM_USUARIO = " & txtCodigo.Text)

            For i As Integer = 0 To lstServicos.Items.Count - 1
                lstServicos.SetItemChecked(i, False)
            Next

            If Not Ds Is Nothing Then
                For i As Integer = 0 To Ds.Rows.Count - 1
                    For j As Integer = 0 To lstServicos.Items.Count - 1
                        If Convert.ToInt32(lstServicos.Items(j).ToString().Split("-")(0).ToString()) = Convert.ToInt32(Ds.Rows(i)(0).ToString()) Then
                            lstServicos.SetItemChecked(j, True)
                        End If
                    Next
                Next
            End If

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        Dim sSql As String = ""

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If Not ValidarEmail(Me.txtEmail.Text) Then
            MessageBox.Show("Informe um Email válido.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then
            Try

                Dim Campos As New StringBuilder
                Dim Valores As New StringBuilder

                For Each Controle As Control In TabPage2.Controls
                    If TypeOf Controle Is CheckBox Then
                        If Not TryCast(Controle, CheckBox).TabIndex = 52 Then
                            Campos.Append(TryCast(Controle, CheckBox).Tag & ",")
                            Valores.Append(TryCast(Controle, CheckBox).CheckState & ",")
                        Else
                            Campos.Append(TryCast(Controle, CheckBox).Tag)
                            Valores.Append(TryCast(Controle, CheckBox).CheckState)
                        End If
                    End If
                Next

                Dim Codigo As String = String.Empty

                sSql = "INSERT INTO " & Banco.BancoNVOCC & "TB_CAD_USUARIOS (USUARIO,SENHA,PATIO,NOME,EMAIL,ID_VENDEDOR,COD_EMPRESA,CPF," & Campos.ToString() & ") VALUES ('" & txtUsuario.Text & "','" & txtSenha.Text & "'," & IIf(cbPatio.SelectedValue IsNot Nothing, cbPatio.SelectedValue, "NULL") & ",'" & txtNome.Text & "','" & txtEmail.Text & "'," & IIf(cbVendedor.SelectedValue IsNot Nothing, cbVendedor.SelectedValue, "NULL") & "," & IIf(cbEmpresa.SelectedValue IsNot Nothing, cbEmpresa.SelectedValue, "0") & ",'" & txtCPF.Text & "'," & Valores.ToString() & ")"

                If Banco.Execute(sSql) Then

                    Codigo = Banco.ExecuteScalar("SELECT IDENT_CURRENT ('SGIPA.dbo.TB_CAD_USUARIOS') AS Current_Identity")

                    For Each Item As String In lstGrupos.CheckedItems
                        Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS (AUTONUMUSER,CODGRUPO) VALUES (" & Codigo & "," & Convert.ToInt32(Item.Split("-")(0).ToString().Trim) & ")")
                    Next

                    For Each Item As String In lstServicos.CheckedItems
                        Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "TB_AMR_USU_POSICIONAMENTO (AUTONUM,AUTONUM_USUARIO,AUTONUM_POSICIONAMENTO) VALUES (" & Banco.BancoNVOCC & "SEQ_AMR_USU_SERVICO.NEXTVAL," & Codigo & ", " & Convert.ToInt32(Item.Split("-")(0).ToString().Trim) & ")")
                    Next

                    Consultar(String.Empty)
                    Mensagens(Me, 1)

                Else
                    Mensagens(Me, 4)
                End If

                Campos.Length = 0
                Valores.Length = 0

            Catch ex As Exception
                Mensagens(Me, 4)
            End Try
        Else
            Try

                Dim Campos As New StringBuilder

                For Each Controle As Control In TabPage2.Controls
                    If TypeOf Controle Is CheckBox Then
                        If Not TryCast(Controle, CheckBox).TabIndex = 52 Then
                            Campos.Append(TryCast(Controle, CheckBox).Tag & "=" & TryCast(Controle, CheckBox).CheckState & ",")
                        Else
                            Campos.Append(TryCast(Controle, CheckBox).Tag & "=" & TryCast(Controle, CheckBox).CheckState)
                        End If
                    End If
                Next

                sSql = "UPDATE " & Banco.BancoNVOCC & "TB_CAD_USUARIOS SET USUARIO = '" & txtUsuario.Text & "',SENHA = '" & txtSenha.Text & "',PATIO = " & IIf(cbPatio.SelectedValue IsNot Nothing, cbPatio.SelectedValue, "NULL") & ",NOME = '" & txtNome.Text & "',EMAIL = '" & txtEmail.Text & "',ID_VENDEDOR = " & IIf(cbVendedor.SelectedValue IsNot Nothing, cbVendedor.SelectedValue, "NULL") & ",COD_EMPRESA = " & IIf(cbEmpresa.SelectedValue IsNot Nothing, cbEmpresa.SelectedValue, "0") & ",CPF = '" & txtCPF.Text & "', " & Campos.ToString() & " WHERE AUTONUM = " & txtCodigo.Text

                If Banco.Execute(sSql) Then

                    Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS WHERE AUTONUMUSER = " & txtCodigo.Text)
                    Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_AMR_USU_POSICIONAMENTO WHERE AUTONUM_USUARIO = " & txtCodigo.Text)

                    For Each Item As String In lstGrupos.CheckedItems
                        Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS (AUTONUMUSER,CODGRUPO) VALUES (" & txtCodigo.Text & "," & Convert.ToInt32(Item.Split("-")(0).ToString().Trim) & ")")
                    Next

                    For Each Item As String In lstServicos.CheckedItems
                        Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "TB_AMR_USU_POSICIONAMENTO (AUTONUM,AUTONUM_USUARIO,AUTONUM_POSICIONAMENTO) VALUES (" & Banco.BancoNVOCC & "SEQ_AMR_USU_SERVICO.NEXTVAL," & txtCodigo.Text & ", " & Convert.ToInt32(Item.Split("-")(0).ToString().Trim) & ")")
                    Next

                    Consultar(String.Empty)
                    Mensagens(Me, 2)

                Else
                    Mensagens(Me, 5)
                End If
            Catch ex As Exception
                Mensagens(Me, 5)
            End Try
        End If

        SetaControles()
        HabilitarCampos(Me, False)
        txtBuscarUsuario.Enabled = True
        TabControl1.SelectedTab = TabPage1

    End Sub

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsnullOrEmpty(txtCodigo.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_CAD_USUARIOS WHERE AUTONUM = " & txtCodigo.Text & "") Then
                    Consultar(String.Empty)
                    LimparCampos(Me)
                End If
            End If
        End If

    End Sub

    Private Sub btnSair_Click(sender As System.Object, e As System.EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub btnPrimeiro_Click(sender As System.Object, e As System.EventArgs) Handles btnPrimeiro.Click

        If dgvConsulta.Rows.Count > 0 Then
            dgvConsulta.CurrentCell = dgvConsulta.Rows(0).Cells(0)
            MostraDados()
        End If

    End Sub

    Private Sub btnProximo_Click(sender As System.Object, e As System.EventArgs) Handles btnProximo.Click

        If dgvConsulta.Rows.Count > 0 Then
            If dgvConsulta.CurrentRow.Index < dgvConsulta.Rows.Count - 1 Then
                dgvConsulta.CurrentCell = dgvConsulta.Rows(dgvConsulta.CurrentRow.Index + 1).Cells(0)
                MostraDados()
            End If
        End If

    End Sub

    Private Sub btnUltimo_Click(sender As System.Object, e As System.EventArgs) Handles btnUltimo.Click

        If dgvConsulta.Rows.Count > 0 Then
            dgvConsulta.CurrentCell = dgvConsulta.Rows(dgvConsulta.Rows.Count - 1).Cells(0)
            MostraDados()
        End If

    End Sub

    Private Sub btnAnterior_Click(sender As System.Object, e As System.EventArgs) Handles btnAnterior.Click

        If dgvConsulta.CurrentRow.Index > 0 Then
            dgvConsulta.CurrentCell = dgvConsulta.Rows(dgvConsulta.CurrentRow.Index - 1).Cells(0)
            MostraDados()
        End If

    End Sub

    Private Sub btnEditar_Click(sender As System.Object, e As System.EventArgs) Handles btnEditar.Click

        If String.IsnullOrEmpty(txtCodigo.Text) Then
            MessageBox.Show("Selecione um registro.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        SetaControles()
        HabilitarCampos(Me, True)
        txtNome.Focus()

    End Sub

    Private Sub btnFiltro_Click(sender As System.Object, e As System.EventArgs) Handles btnFiltro.Click

    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click

        SetaControles()
        LimparCampos(Me)
        HabilitarCampos(Me, False)
        txtBuscarUsuario.Enabled = True

    End Sub

    Private Sub btImprimir_Click(sender As System.Object, e As System.EventArgs)

        Me.cbGrupoRel.DataSource = Banco.List("SELECT DISTINCT TB_SYS_GRUPO_USER.DESCRICAO, TB_SYS_GRUPO_USER.CODGRUPO FROM " & Banco.BancoNVOCC & "TB_SYS_GRUPO_USER JOIN " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS ON TB_SYS_GRUPO_USER.CodGrupo = TB_SYS_USER_GRUPOS.CodGrupo JOIN " & Banco.BancoNVOCC & "TB_CAD_USUARIOS ON TB_SYS_USER_GRUPOS.AUTONUMUSER = TB_CAD_USUARIOS.AUTONUM WHERE TB_CAD_USUARIOS.FLAG_ATIVO=1 ORDER BY DESCRICAO")
        Me.cbUsuarioRel.DataSource = Banco.List("SELECT USUARIO,AUTONUM FROM " & Banco.BancoNVOCC & "TB_CAD_USUARIOS WHERE FLAG_ATIVO = 1")
        Me.cbModuloRel.DataSource = Banco.List("SELECT DISTINCT SISTEMA FROM " & Banco.BancoNVOCC & "TB_SYS_GRUPO_USER JOIN " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS ON TB_SYS_GRUPO_USER.CodGrupo = TB_SYS_USER_GRUPOS.CodGrupo JOIN " & Banco.BancoNVOCC & "TB_SYS_GRP_PERMISSOES TB_SYS_GRP_PERMISSOES ON TB_SYS_USER_GRUPOS.CodGrupo = TB_SYS_GRP_PERMISSOES.CodGrupo JOIN " & Banco.BancoNVOCC & "TB_SYS_FUNCOES ON TB_SYS_GRP_PERMISSOES.CodFunc = TB_SYS_FUNCOES.CodFunc JOIN " & Banco.BancoNVOCC & "TB_CAD_USUARIOS ON TB_SYS_USER_GRUPOS.AUTONUMUSER = TB_CAD_USUARIOS.AUTONUM WHERE TB_CAD_USUARIOS.FLAG_ATIVO=1 AND TB_SYS_FUNCOES.sistema <> 'MICROLED' ORDER BY SISTEMA")

        Me.cbGrupoRel.SelectedIndex = -1
        Me.cbUsuarioRel.SelectedIndex = -1
        Me.cbModuloRel.SelectedIndex = -1

        Panel3.Show()

    End Sub

    Private Sub FrmMotoristas_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmMotoristas_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnImprimirRel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimirRel.Click

        Dim TipoRel As String
        Dim Filtro As String
        Dim descrFiltro As String

        Do
            TipoRel = InputBox("Infome o Relatório Desejado:" & vbCrLf & vbCrLf & "[1] - Relatório de Grupos." & vbCrLf & "[2] - Relatório de Usuários." & vbCrLf & "[3] - Relatório de Grupos por Módulo.", "Relatório de Grupos/Usuários", "1")
            If nNull(TipoRel, 1) = "" Then Exit Sub
        Loop Until TipoRel = "1" Or TipoRel = "2" Or TipoRel = "3"

        Filtro = String.Empty
        descrFiltro = String.Empty

        If nNull(cbGrupoRel.Text, 1) <> "" Then
            If Len(Filtro) > 0 Then Filtro = Filtro & " AND "
            If Len(descrFiltro) > 0 Then descrFiltro = descrFiltro & " | "
            Filtro = Filtro & " TB_SYS_GRUPO_USER.CODGRUPO = " & nNull(cbGrupoRel.SelectedValue, 0)
            descrFiltro = descrFiltro & " Grupo: " & nNull(cbGrupoRel.Text, 1)
        End If

        If TipoRel <> "3" Then
            If nNull(cbUsuarioRel.Text, 1) <> "" Then
                If Len(Filtro) > 0 Then Filtro = Filtro & " AND "
                If Len(descrFiltro) > 0 Then descrFiltro = descrFiltro & " | "
                Filtro = Filtro & " TB_CAD_USUARIOS.AUTONUM = " & nNull(cbUsuarioRel.SelectedValue, 0)
                descrFiltro = descrFiltro & " Usuário: " & nNull(cbUsuarioRel.Text, 1)
            End If
        End If

        If TipoRel <> "2" And TipoRel <> "3" Then
            If nNull(cbModuloRel.Text, 1) <> "" Then
                If Len(Filtro) > 0 Then Filtro = Filtro & " AND "
                If Len(descrFiltro) > 0 Then descrFiltro = descrFiltro & " | "
                Filtro = Filtro & " TB_SYS_FUNCOES.SISTEMA = '" & nNull(cbModuloRel.Text, 0) & "' "
                descrFiltro = descrFiltro & " Módulo: " & nNull(cbModuloRel.Text, 1)
            End If
        End If

        If rbAmbos.Checked = False Then
            If Len(Filtro) > 0 Then Filtro = Filtro & " AND "
            If Len(descrFiltro) > 0 Then descrFiltro = descrFiltro & " | "
            If rbAtivos.Checked = True Then
                Filtro = Filtro & " TB_CAD_USUARIOS.FLAG_ATIVO=1 "
                descrFiltro = descrFiltro & " Usuários: Ativos "
            Else
                Filtro = Filtro & " TB_CAD_USUARIOS.FLAG_ATIVO=0 "
                descrFiltro = descrFiltro & " Usuários: Inativos "
            End If
        End If

        Dim formulas As List(Of String) = New List(Of String)
        Dim valores As List(Of String) = New List(Of String)

        formulas.Add("user")
        valores.Add(FrmPrincipal.lblUsuario.Text)

        formulas.Add("filtro")
        valores.Add("Filtro(s) Utilizado(s): " & descrFiltro)

        Select Case TipoRel
            Case "1" 'Relatório de Grupos.

                formulas.Add("titulo")
                valores.Add("RELATÓRIO DE GRUPOS")

                Dim Sql As String = String.Empty

                Sql = " SELECT DISTINCT TB_SYS_GRUPO_USER.DESCRICAO, TB_SYS_FUNCOES.SISTEMA, TB_SYS_FUNCOES.CAPTIONOBJ, TB_SYS_TIPO_PERM.DESCRICAO "
                Sql = Sql & " FROM   " & Banco.BancoNVOCC & "TB_SYS_GRUPO_USER TB_SYS_GRUPO_USER JOIN  "
                Sql = Sql & " " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS TB_SYS_USER_GRUPOS ON TB_SYS_GRUPO_USER.CodGrupo = TB_SYS_USER_GRUPOS.CodGrupo JOIN "
                Sql = Sql & " " & Banco.BancoNVOCC & "TB_SYS_GRP_PERMISSOES TB_SYS_GRP_PERMISSOES ON TB_SYS_USER_GRUPOS.CodGrupo = TB_SYS_GRP_PERMISSOES.CodGrupo JOIN "
                Sql = Sql & " " & Banco.BancoNVOCC & "TB_SYS_FUNCOES TB_SYS_FUNCOES ON TB_SYS_GRP_PERMISSOES.CodFunc = TB_SYS_FUNCOES.CodFunc JOIN "
                Sql = Sql & " " & Banco.BancoNVOCC & "TB_SYS_TIPO_PERM TB_SYS_TIPO_PERM ON TB_SYS_GRP_PERMISSOES.CODTIPOPERM = TB_SYS_TIPO_PERM.CODTIPOPERM JOIN "
                Sql = Sql & " " & Banco.BancoNVOCC & "TB_CAD_USUARIOS TB_CAD_USUARIOS ON TB_SYS_USER_GRUPOS.AUTONUMUSER = TB_CAD_USUARIOS.AUTONUM "
                Sql = Sql & " WHERE " & Filtro
                Sql = Sql & " ORDER BY TB_SYS_GRUPO_USER.DESCRICAO, TB_SYS_FUNCOES.SISTEMA, TB_SYS_FUNCOES.CAPTIONOBJ, TB_SYS_TIPO_PERM.DESCRICAO "

                Dim rptName As String = "\rpts\RelGrupoUsu.rpt"
                Dim query As String = Sql
            'Banco.TestaSQL(query)
            'ChamarRelatorio(rptName, query, "0", formulas, valores)

            Case "2" 'Relatório de Usuários.                              

                formulas.Add("titulo")
                valores.Add("RELATÓRIO DE USUÁRIOS")

                Dim sql As String = String.Empty

                sql = " SELECT  DISTINCT  TB_SYS_GRUPO_USER.DESCRICAO, TB_CAD_USUARIOS.USUARIO, TB_CAD_USUARIOS.NOME "
                sql = sql & "  FROM   " & Banco.BancoNVOCC & "TB_SYS_GRUPO_USER TB_SYS_GRUPO_USER JOIN  "
                sql = sql & " " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS TB_SYS_USER_GRUPOS ON TB_SYS_GRUPO_USER.CodGrupo = TB_SYS_USER_GRUPOS.CodGrupo JOIN "
                sql = sql & " " & Banco.BancoNVOCC & "TB_CAD_USUARIOS TB_CAD_USUARIOS ON TB_SYS_USER_GRUPOS.AUTONUMUSER = TB_CAD_USUARIOS.Autonum  "
                sql = sql & " WHERE " & Filtro
                sql = sql & " ORDER BY TB_SYS_GRUPO_USER.DESCRICAO, TB_CAD_USUARIOS.USUARIO "

                Dim rptName As String = "\rpts\RelUsuGrupo.rpt"
                Dim query As String = sql
            'Banco.TestaSQL(query)
            'ChamarRelatorio(rptName, query, "0", formulas, valores)


            Case "3" 'Relatório de Grupos por Módulo.

                formulas.Add("titulo")
                valores.Add("RELATÓRIO DE GRUPOS POR MÓDULO")

                Dim sql As String = String.Empty

                sql = " SELECT DISTINCT TB_SYS_FUNCOES.SISTEMA, TB_SYS_GRUPO_USER.DESCRICAO, TB_SYS_FUNCOES.CAPTIONOBJ "
                sql = sql & " FROM   " & Banco.BancoNVOCC & "TB_SYS_GRUPO_USER TB_SYS_GRUPO_USER  "
                sql = sql & "        join  " & Banco.BancoNVOCC & "TB_SYS_USER_GRUPOS TB_SYS_USER_GRUPOS on (TB_SYS_GRUPO_USER.CODGRUPO=TB_SYS_USER_GRUPOS.CODGRUPO) "
                sql = sql & "        join  " & Banco.BancoNVOCC & "TB_SYS_GRP_PERMISSOES TB_SYS_GRP_PERMISSOES on  (TB_SYS_USER_GRUPOS.CODGRUPO=TB_SYS_GRP_PERMISSOES.CODGRUPO) "
                sql = sql & "        join  " & Banco.BancoNVOCC & "TB_SYS_FUNCOES TB_SYS_FUNCOES on (TB_SYS_GRP_PERMISSOES.CODFUNC=TB_SYS_FUNCOES.CODFUNC) "
                If Len(Filtro) > 0 Then sql = sql & "  where " & Filtro

                Dim rptName As String = "\rpts\RelGrupoMod.rpt"
                Dim query As String = sql
                'Banco.TestaSQL(query)
                'ChamarRelatorio(rptName, query, "0", formulas, valores)

        End Select

    End Sub

    Private Sub btnPesquisarUsuario_Click(sender As Object, e As EventArgs) Handles btnPesquisarUsuario.Click
        Consultar(Me.txtBuscarUsuario.Text.Trim())
    End Sub

    Private Sub txtBuscarUsuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBuscarUsuario.KeyPress

        If e.KeyChar = ChrW(Keys.Enter) Then
            Consultar(Me.txtBuscarUsuario.Text.Trim())
        End If

    End Sub

    Private Sub btnFiltro2_Click(sender As Object, e As EventArgs) Handles btnFiltro2.Click
        If dgvConsulta.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvConsulta.CurrentCell.ColumnIndex)
        End If
    End Sub
End Class
