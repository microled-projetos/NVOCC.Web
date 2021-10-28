Imports DgvFilterPopup
Public Class FrmTiposConteiner

    Private Coluna As Integer
    Dim Filtro As DgvFilterManager

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER ,ISO,MAXGROSS,TEU,TAMANHO_CONTAINER,FL_ATIVO FROM TB_TIPO_CONTAINER")
    End Sub

    Private Sub SetaControles()

        btnEditar.Enabled = Not (btnEditar.Enabled)
        btnSalvar.Enabled = Not (btnSalvar.Enabled)
        btnCancelar.Enabled = Not (btnCancelar.Enabled)
        dgvConsulta.Enabled = Not (dgvConsulta.Enabled)
        pnControles.Enabled = Not (pnControles.Enabled)

    End Sub

    Private Sub FrmPrincipal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If Not Filtro Is Nothing Then
            Filtro.ActivateAllFilters(False)
        End If

        Consultar()

        If Me.dgvConsulta.Rows.Count > 0 Then
            'Filtro = New DgvFilterManager(Me.dgvConsulta)
            'LoadFilters(Filtro)
        End If

        FundoTextBox(Me)

        CampoNumerico(Me.txtTamanho)
        CampoNumerico(Me.txtID)


        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 20 AND ID_TIPO_USUARIO = " & TipoUsuario)

        If Ds.Rows.Count > 0 Then
            If Ds.Rows(0)("FL_ATUALIZAR").ToString <> True Then
                btnEditar.Visible = False
            Else
                btnEditar.Visible = True
            End If

            If Ds.Rows(0)("FL_EXCLUIR").ToString <> True Then
                btnExcluir.Visible = False
            Else
                btnExcluir.Visible = True
            End If

            If Ds.Rows(0)("FL_CADASTRAR").ToString <> True Then
                btnNovo.Visible = False
            Else
                btnNovo.Visible = True
            End If
        Else
            btnNovo.Visible = False
            btnExcluir.Visible = False
            btnEditar.Visible = False
        End If

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Me.txtID.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtDescricao.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtISO.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            Me.txtMAXGROSS.Text = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            Me.txtTEU.Text = Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString()
            Me.txtTamanho.Text = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            Me.chkAtivo.Checked = Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString()
        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim Ds As New DataTable

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtID.Text = String.Empty Then

            Ds = Banco.List("SELECT ID_TIPO_CONTAINER FROM [TB_TIPO_CONTAINER] where NM_TIPO_CONTAINER = '" & txtDescricao.Text & "' and TEU = '" & txtTEU.Text & "' and TAMANHO_CONTAINER = " & txtTamanho.Text)
            If Ds.Rows.Count > 0 Then

                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else
                Try
                    txtMAXGROSS.Text = txtMAXGROSS.Text.Replace(",", ".")
                    If txtMAXGROSS.Text = "" Then
                        txtMAXGROSS.Text = " NULL "
                    Else
                        txtMAXGROSS.Text = "'" & txtMAXGROSS.Text & "'"
                    End If

                    If txtISO.Text = "" Then
                        txtISO.Text = " NULL "
                    Else
                        txtISO.Text = "'" & txtISO.Text & "'"
                    End If

                    If Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "TB_TIPO_CONTAINER (NM_TIPO_CONTAINER,ISO,MAXGROSS,TEU,TAMANHO_CONTAINER,FL_ATIVO) VALUES ('" & Me.txtDescricao.Text & "'," & Me.txtISO.Text & "," & txtMAXGROSS.Text & ",'" & txtTEU.Text & "'," & txtTamanho.Text & "," & Me.chkAtivo.CheckState & ")") Then
                        Consultar()
                        Mensagens(Me, 1)
                    Else
                        Mensagens(Me, 4)
                    End If
                Catch ex As Exception
                    Mensagens(Me, 5)
                End Try
            End If

        Else
            Try
                Ds = Banco.List("SELECT ID_TIPO_CONTAINER FROM [TB_TIPO_CONTAINER] where NM_TIPO_CONTAINER = '" & txtDescricao.Text & "' and TEU = '" & txtTEU.Text & "' and TAMANHO_CONTAINER = " & txtTamanho.Text & " and FL_ATIVO = 1 AND ID_TIPO_CONTAINER <> " & txtID.Text)
                If Ds.Rows.Count > 0 Then

                    MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else
                    txtMAXGROSS.Text = txtMAXGROSS.Text.Replace(",", ".")
                    If txtMAXGROSS.Text = "" Then
                        txtMAXGROSS.Text = " NULL "
                    Else
                        txtMAXGROSS.Text = "'" & txtMAXGROSS.Text & "'"
                    End If

                    If txtISO.Text = "" Then
                        txtISO.Text = " NULL "
                    Else
                        txtISO.Text = "'" & txtISO.Text & "'"
                    End If

                    If Banco.Execute("UPDATE " & Banco.BancoNVOCC & "TB_TIPO_CONTAINER SET NM_TIPO_CONTAINER = '" & txtDescricao.Text & "', ISO = " & txtISO.Text & ", MAXGROSS = " & txtMAXGROSS.Text & ", TEU = '" & txtTEU.Text & "',TAMANHO_CONTAINER=" & txtTamanho.Text & ",FL_ATIVO=" & chkAtivo.CheckState & " WHERE ID_TIPO_CONTAINER = " & txtID.Text & "") Then
                        Consultar()
                        Mensagens(Me, 2)
                    Else
                        Mensagens(Me, 5)
                    End If
                End If


            Catch ex As Exception
                Mensagens(Me, 5)
            End Try
        End If

        SetaControles()
        HabilitarCampos(Me, False)

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

        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Selecione um registro.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        SetaControles()
        HabilitarCampos(Me, True)
        txtID.Enabled = False

    End Sub

    Private Sub btnFiltro_Click(sender As System.Object, e As System.EventArgs) Handles btnFiltro.Click
        If dgvConsulta.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvConsulta.CurrentCell.ColumnIndex)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click
        SetaControles()
        LimparCampos(Me)
        HabilitarCampos(Me, False)
    End Sub

    Private Sub btImprimir_Click(sender As System.Object, e As System.EventArgs)

        If dgvConsulta.Rows.Count = 0 Then
            MessageBox.Show("Não existem registros a serem impressos.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim FrmImprimir As New FrmImpressao(Me.Text, Me.dgvConsulta, Me)
        FrmImprimir.ShowDialog()

    End Sub

    Private Sub FrmAvisoExtrato_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub
    Private Sub dgvConsulta_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If
    End Sub
    Private Sub FrmAvisoExtrato_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtDescricao.Focus()
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Not String.IsNullOrEmpty(txtID.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = " & txtID.Text & "") Then
                    Consultar()
                    LimparCampos(Me)
                End If
            End If
        End If
    End Sub


End Class
