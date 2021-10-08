Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices
Public Class FrmPortos

    Private Coluna As Integer
    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_PORTO,CD_PORTO,NM_PORTO,A.ID_CIDADE,B.NM_CIDADE,SIGLA_IATA,CD_SIGLA,A.ID_VIATRANSPORTE,C.NM_VIATRANSPORTE,FL_ATIVO FROM TB_PORTO A left join TB_CIDADE B ON B.ID_CIDADE = A.ID_CIDADE left join TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE")
    End Sub
    Private Sub ConsultarCidade()
        cbCidade.DataSource = Banco.List("SELECT ID_CIDADE,NM_CIDADE FROM TB_CIDADE")
        cbCidade.SelectedIndex = -1
    End Sub

    Private Sub ConsultarTransporte()
        cbTransporte.DataSource = Banco.List("SELECT ID_VIATRANSPORTE, NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]")
        cbTransporte.SelectedIndex = -1
    End Sub
    Private Sub SetaControles()

        btnNovo.Enabled = Not (btnNovo.Enabled)
        btnEditar.Enabled = Not (btnEditar.Enabled)
        btnSalvar.Enabled = Not (btnSalvar.Enabled)
        btnCancelar.Enabled = Not (btnCancelar.Enabled)
        dgvConsulta.Enabled = Not (dgvConsulta.Enabled)
        pnControles.Enabled = Not (pnControles.Enabled)

    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtCodigo.Focus()
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Selecione um registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        SetaControles()
        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        txtCodigo.Focus()
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click

    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        SetaControles()
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
    End Sub

    Private Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub FrmPortos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Consultar()
        ConsultarTransporte()
        ConsultarCidade()

        FundoTextBox(Me)
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

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Me.txtID.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtNome.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            If Operators.CompareString(dgvConsulta.CurrentRow.Cells(3).Value.ToString(), String.Empty, TextCompare:=False) <> 0 Then
                Me.cbCidade.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            End If
            Me.txtSiglaIATA.Text = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            Me.txtCodSigla.Text = Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            Me.cbTransporte.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString()
            chkAtivo.Checked = Conversions.ToBoolean(dgvConsulta.CurrentRow.Cells(9).Value.ToString())
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

End Class