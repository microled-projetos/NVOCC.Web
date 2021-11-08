Imports DgvFilterPopup
Public Class FrmMoedas

    Private Coluna As Integer
    Dim Filtro As DgvFilterManager
    Private Dlg As New OpenFileDialog



    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_MOEDA,CD_MOEDA,NM_MOEDA,SIGLA_MOEDA, FL_ATIVO FROM TB_MOEDA")
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

        Consultar()

        If Me.dgvConsulta.Rows.Count > 0 Then
            'Filtro = New DgvFilterManager(Me.dgvConsulta)
        End If

        FundoTextBox(Me)


        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 18 AND ID_TIPO_USUARIO = " & TipoUsuario)

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

    Private Sub btnNovo_Click(sender As System.Object, e As System.EventArgs) Handles btnNovo.Click

        HabilitarCampos(Me, True)
        LimparCampos(Me)
        SetaControles()
        Me.txtCodigo.Focus()

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If

    End Sub

    Private Sub dgvConsulta_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If
    End Sub
    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then
            txtID.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            txtNome.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            txtSigla.Text = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            chkAtivo.Checked = Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString()

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim Ds As New DataTable

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If


        If txtID.Text = String.Empty Then



            Ds = Banco.List("SELECT ID_MOEDA FROM [TB_MOEDA] where NM_MOEDA = '" & txtNome.Text & "' and CD_MOEDA ='" & txtCodigo.Text & "'")
            If Ds.Rows.Count > 0 Then

                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else
                Try
                    If txtSigla.Text = "" Then
                        txtSigla.Text = " NULL "
                    Else
                        txtSigla.Text = "'" & txtSigla.Text & "'"
                    End If

                    If Banco.Execute("INSERT INTO TB_MOEDA (CD_MOEDA,NM_MOEDA,SIGLA_MOEDA, FL_ATIVO) VALUES ('" & txtCodigo.Text & "','" & txtNome.Text & "'," & txtSigla.Text & ",'" & chkAtivo.Checked & "')") Then
                        Consultar()
                        Mensagens(Me, 1)
                        txtSigla.Text = ""
                    Else
                        Mensagens(Me, 4)
                    End If
                Catch ex As Exception
                    Mensagens(Me, 4)
                End Try
            End If

        Else


            Ds = Banco.List("SELECT ID_MOEDA FROM [TB_MOEDA] where NM_MOEDA = '" & txtNome.Text & "' and CD_MOEDA ='" & txtCodigo.Text & "' and FL_ATIVO = 1 AND ID_MOEDA <> " & txtID.Text)
            If Ds.Rows.Count > 0 Then

                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else
                Try

                    If txtSigla.Text = "" Then
                        txtSigla.Text = " NULL "
                    Else
                        txtSigla.Text = "'" & txtSigla.Text & "'"
                    End If

                    If Banco.Execute("UPDATE TB_MOEDA SET CD_MOEDA = '" & txtCodigo.Text & "', NM_MOEDA = '" & txtNome.Text & "',SIGLA_MOEDA = " & txtSigla.Text & ", FL_ATIVO = '" & chkAtivo.Checked & "' WHERE ID_MOEDA = " & txtID.Text) Then
                        Consultar()
                        Mensagens(Me, 2)
                        txtSigla.Text = ""
                    Else
                        Mensagens(Me, 5)
                    End If
                Catch ex As Exception
                    Mensagens(Me, 5)
                End Try

            End If

        End If

        HabilitarCampos(Me, False)
        SetaControles()

    End Sub

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsNullOrEmpty(txtID.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_MOEDA WHERE ID_MOEDA = " & txtID.Text & "") Then
                    Consultar()
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

        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Selecione um registro.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        HabilitarCampos(Me, True)
        SetaControles()
        txtCodigo.Focus()

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

    Private Sub FrmEmpresas_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub





    Private Sub FrmEmpresas_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub


End Class
