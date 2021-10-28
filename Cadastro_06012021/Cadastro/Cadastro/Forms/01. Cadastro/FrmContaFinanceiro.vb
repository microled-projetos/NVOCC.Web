Imports DgvFilterPopup
Public Class FrmContaFinanceiro

    Private Coluna As Integer
    Dim Filtro As DgvFilterManager

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_CONTA_BANCARIA,NM_CONTA_BANCARIA, A.ID_TIPO_CONTA_BANCARIA,B.NM_TIPO_CONTA_BANCARIA,NR_BANCO,NR_AGENCIA,DG_AGENCIA, NR_CONTA, FL_ATIVO FROM TB_CONTA_BANCARIA A LEFT JOIN TB_TIPO_CONTA_BANCARIA B on B.ID_TIPO_CONTA_BANCARIA = A.ID_TIPO_CONTA_BANCARIA")
    End Sub

    Private Sub ConsultarTipoConta()
        Me.cbTipoConta.DataSource = Banco.List("SELECT ID_TIPO_CONTA_BANCARIA,NM_TIPO_CONTA_BANCARIA FROM TB_TIPO_CONTA_BANCARIA")
        Me.cbTipoConta.SelectedIndex = -1
    End Sub
    Private Sub SetaControles()

        btnNovo.Enabled = Not (btnNovo.Enabled)
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
        ConsultarTipoConta()
        CampoNumerico(txtBanco)

        If Me.dgvConsulta.Rows.Count > 0 Then
            'Filtro = New DgvFilterManager(Me.dgvConsulta)
        End If

        FundoTextBox(Me)

        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 17 AND ID_TIPO_USUARIO = " & TipoUsuario)

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
    Private Sub dgvConsulta_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If
    End Sub

    Private Sub MostraDados()
        If Me.dgvConsulta.Rows.Count > 0 Then
            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtDescricao.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            If Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString() <> String.Empty Then
                Me.cbTipoConta.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            End If
            Me.txtBanco.Text = Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString()
            Me.txtAgencia.Text = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            Me.txtDigito.Text = Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            Me.txtConta.Text = Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString()
            Me.chkAtivo.Checked = Me.dgvConsulta.CurrentRow.Cells(8).Value.ToString()

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim Ds As New DataTable

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then


            Ds = Banco.List("SELECT ID_CONTA_BANCARIA FROM [TB_CONTA_BANCARIA] where NR_BANCO ='" & txtBanco.Text & "' and NR_AGENCIA ='" & txtAgencia.Text & "' and NR_CONTA = '" & txtConta.Text & "' and FL_ATIVO = 1")
            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Esse registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else


                If txtDigito.Text = "" Then
                    txtDigito.Text = " NULL "
                Else
                    txtDigito.Text = "'" & txtDigito.Text & "'"
                End If

                Try
                    If Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "TB_CONTA_BANCARIA (NM_CONTA_BANCARIA, ID_TIPO_CONTA_BANCARIA,NR_BANCO,NR_AGENCIA,DG_AGENCIA, NR_CONTA, FL_ATIVO) VALUES ('" & txtDescricao.Text & "'," & cbTipoConta.SelectedValue & ",'" & txtBanco.Text & "','" & txtAgencia.Text & "'," & txtDigito.Text & ",'" & txtConta.Text & "','" & chkAtivo.Checked & "')") Then
                        Consultar()
                        Mensagens(Me, 1)
                    Else
                        Mensagens(Me, 4)
                    End If
                Catch ex As Exception
                    Mensagens(Me, 4)
                End Try
            End If





        Else
            If txtDigito.Text = "" Then
                txtDigito.Text = " NULL "
            Else
                txtDigito.Text = "'" & txtDigito.Text & "'"
            End If

            Ds = Banco.List("SELECT ID_CONTA_BANCARIA FROM [TB_CONTA_BANCARIA] where NR_BANCO ='" & txtBanco.Text & "' and NR_AGENCIA ='" & txtAgencia.Text & "' and NR_CONTA = '" & txtConta.Text & "' and FL_ATIVO = 1 and ID_CONTA_BANCARIA <> " & txtCodigo.Text)
            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Esse registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Try
                    If Banco.Execute("UPDATE " & Banco.BancoNVOCC & "TB_CONTA_BANCARIA SET NM_CONTA_BANCARIA = '" & txtDescricao.Text & "', ID_TIPO_CONTA_BANCARIA = " & cbTipoConta.SelectedValue & " ,NR_BANCO = '" & txtBanco.Text & "', NR_AGENCIA = '" & txtAgencia.Text & "',DG_AGENCIA = " & txtDigito.Text & ", NR_CONTA = '" & txtConta.Text & "', FL_ATIVO = '" & chkAtivo.Checked & "' WHERE ID_CONTA_BANCARIA = " & txtCodigo.Text & "") Then
                        Consultar()
                        Mensagens(Me, 2)
                        txtDigito.Text = ""
                    Else
                        Mensagens(Me, 5)
                    End If
                Catch ex As Exception
                    Mensagens(Me, 5)
                End Try
            End If

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

        If String.IsNullOrEmpty(txtCodigo.Text) Then
            MessageBox.Show("Selecione um registro.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        SetaControles()
        HabilitarCampos(Me, True)
        txtDescricao.Focus()

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

    Private Sub FrmEmbalagens_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmEmbalagens_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click

        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtDescricao.Focus()

    End Sub

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsNullOrEmpty(txtCodigo.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_CONTA_BANCARIA WHERE ID_CONTA_BANCARIA = " & txtCodigo.Text & "") Then
                    Consultar()
                    LimparCampos(Me)
                End If
            End If
        End If

    End Sub


End Class
