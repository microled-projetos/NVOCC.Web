Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices
Public Class FrmNavios

    Private Coluna As Integer

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_NAVIO,NM_NAVIO,CD_LOYD,isnull(A.ID_PAIS,0)ID_PAIS,B.NM_PAIS,FL_ATIVO FROM TB_NAVIO A LEFT JOIN TB_PAIS B on B.ID_PAIS = A.ID_PAIS")
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

        Consultar()
        ConsultarPaises()
        FundoTextBox(Me)

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Me.txtID.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtNome.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtCodigoLoyd.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            '  Me.cbPais.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            If dgvConsulta.CurrentRow.Cells(3).Value.ToString() <> 0 Then
                cbPais.SelectedValue = dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            End If

            chkAtivo.Checked = Conversions.ToBoolean(dgvConsulta.CurrentRow.Cells(5).Value.ToString())
        End If

    End Sub

    Private Sub ConsultarPaises()
        cbPais.DataSource = Banco.List("SELECT ID_PAIS, NM_PAIS DESCR FROM TB_PAIS ORDER BY DESCR")
        cbPais.SelectedIndex = -1
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

    Private Sub dgvConsulta_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
        If dgvConsulta.Rows.Count > 0 Then
            MostraDados()

            If Convert.ToInt32(e.ColumnIndex) >= 0 Then
                Coluna = dgvConsulta.Columns(e.ColumnIndex).Index
            End If
        End If
    End Sub

    Private Sub dgvConsulta_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConsulta.CellClick
        If dgvConsulta.Rows.Count > 0 Then
            MostraDados()

            If Convert.ToInt32(e.ColumnIndex) >= 0 Then
                Coluna = dgvConsulta.Columns(e.ColumnIndex).Index
            End If
        End If
    End Sub



    Private Sub FrmNavios_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmNavios_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub



    Private Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtNome.Focus()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        SetaControles()
        LimparCampos(Me)
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Selecione um registro.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtNome.Focus()

    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Not String.IsNullOrEmpty(txtID.Text) AndAlso MessageBox.Show("Deseja realmente excluir o registro selecionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes AndAlso Banco.Execute("DELETE FROM TB_NAVIO WHERE ID_NAVIO = " + txtID.Text) Then
            Consultar()
            Dim Tela As Control = Me
            Geral.LimparCampos(Tela)
        End If
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If
        Dim pais As String = 0


        If txtID.Text = String.Empty Then
            Try
                If cbPais.SelectedValue = 0 Then
                    pais = 0
                Else
                    pais = cbPais.SelectedValue
                End If

                If Banco.Execute("INSERT INTO TB_NAVIO (NM_NAVIO,CD_LOYD,FL_ATIVO,ID_PAIS) VALUES ('" & Me.txtNome.Text & "','" & Me.txtCodigoLoyd.Text & "', '" & Me.chkAtivo.CheckState & "', " & pais & ")") Then
                    Consultar()
                    Mensagens(Me, 1)
                Else
                    Mensagens(Me, 4)
                End If
            Catch ex As Exception
                Mensagens(Me, 4)
            End Try
        Else
            Try
                If cbPais.SelectedValue = 0 Then
                    pais = 0
                Else
                    pais = cbPais.SelectedValue
                End If

                If Banco.Execute("UPDATE TB_NAVIO SET FL_ATIVO = '" & Me.chkAtivo.CheckState & "' , NM_NAVIO = '" & txtNome.Text & "', CD_LOYD = '" & Me.txtCodigoLoyd.Text & "', ID_PAIS = " & pais & " WHERE ID_NAVIO = " & Me.txtID.Text & "") Then
                    Consultar()
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
    End Sub
End Class
