Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports DgvFilterPopup
Imports Gerencial
Imports Microsoft.VisualBasic.CompilerServices


Public Class FrmNCM

    Dim NCM As New NCM



    Private Sub FrmNCM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub btPrim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If dgvNCM.Rows.Count > 0 Then
            dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, 0)
            ExibeInformacoes()
        End If

    End Sub

    Private Sub btAnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If dgvNCM.Rows.Count > 0 Then
            If dgvNCM.CurrentRow.Index > 0 Then
                dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, dgvNCM.CurrentCell.RowIndex - 1)
                ExibeInformacoes()
            End If
        End If

    End Sub

    Private Sub BtProx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If dgvNCM.Rows.Count > 0 Then
            If dgvNCM.CurrentRow.Index < dgvNCM.Rows.Count - 1 Then
                dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, dgvNCM.CurrentCell.RowIndex + 1)
                ExibeInformacoes()
            End If
        End If

    End Sub

    Private Sub btUlt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If dgvNCM.Rows.Count > 0 Then
            dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, dgvNCM.Rows.Count - 1)
            ExibeInformacoes()
        End If

    End Sub

    Private Sub ExibeInformacoes()

        If dgvNCM.Rows.Count > 0 Then
            txtID.Text = dgvNCM.CurrentRow.Cells("ID_NCM").Value.ToString()
            txtCodigoNCM.Text = dgvNCM.CurrentRow.Cells("CD_NCM").Value.ToString()
            txtDescricao.Text = dgvNCM.CurrentRow.Cells("NM_NCM").Value.ToString()
            txtAPNCM.Text = dgvNCM.CurrentRow.Cells("AP_NCM").Value.ToString()
            ckbAtivo.Checked = Conversions.ToBoolean(dgvNCM.CurrentRow.Cells("FL_ATIVO").Value.ToString())
        End If

    End Sub

    Private Sub CarregarGridNCM()

        Me.dgvNCM.AutoGenerateColumns = False
        Me.dgvNCM.DataSource = NCM.Consultar()

    End Sub

    Private Sub btSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Function ValidarCampos() As Boolean

        If txtCodigoNCM.Text = String.Empty Then
            txtCodigoNCM.BackColor = Color.MistyRose
            MessageBox.Show("O campo Código é Obrigatório.", "Cadastro de Linhas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCodigoNCM.Focus()
            Return False
        Else
            txtCodigoNCM.BackColor = Color.White
        End If

        If txtDescricao.Text = String.Empty Then
            txtDescricao.BackColor = Color.MistyRose
            MessageBox.Show("O campo Descrição é Obrigatório.", "Cadastro de Linhas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtDescricao.Focus()
            Return False
        Else
            txtDescricao.BackColor = Color.White
        End If

        Return True

    End Function

    Private Sub btSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Ds As DataTable = New DataTable()

        If Not ValidarCampos() Then
            Return
        End If

        NCM.CodigoNCM = txtCodigoNCM.Text
        NCM.Descricao = txtDescricao.Text
        NCM.AP_NCM = txtAPNCM.Text
        NCM.Codigo = txtID.Text
        NCM.FL_ATIVO = Conversions.ToString(CInt(ckbAtivo.CheckState))

        If Operators.CompareString(txtID.Text, String.Empty, TextCompare:=False) = 0 Then
            Ds = Banco.List("SELECT ID_NCM FROM TB_NCM WHERE CD_NCM = '" & txtCodigoNCM.Text & "' AND AP_NCM = '" + txtAPNCM.Text & "' AND NM_NCM = '" + txtDescricao.Text & "'")

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Este registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf NCM.Inserir(NCM) Then
                MessageBox.Show("Registro inserido com sucesso.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                CarregarGridNCM()
            Else
                MessageBox.Show("Erro durante a operação. Tente Novamente", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
        Else
            NCM.Codigo = txtID.Text
            Ds = Banco.List("SELECT ID_NCM FROM TB_NCM WHERE CD_NCM = '" & txtCodigoNCM.Text & "' AND AP_NCM = '" + txtAPNCM.Text & "' AND NM_NCM = '" + txtDescricao.Text & "' AND ID_NCM <> " + txtID.Text)

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Este registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf NCM.Alterar(NCM) Then
                MessageBox.Show("Registro alterado com sucesso.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                CarregarGridNCM()
            Else
                MessageBox.Show("Erro durante a operação. Tente Novamente", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
        End If
        SetarControles()
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
    End Sub


    Private Sub btNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
        SetarControles()
        txtCodigoNCM.Focus()

    End Sub

    Private Sub btEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If txtID.Text = String.Empty Then
            MessageBox.Show("Selecione um Registro.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        SetarControles()
        HabilitarCampos(Me, True)
        Me.txtDescricao.Focus()

    End Sub

    Private Sub btCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
        Tela = Me
        Geral.LimparCampos(Tela)
        SetarControles()
    End Sub
    Private Sub SetarControles()
        btnNovo.Enabled = Not btnNovo.Enabled
        btnEditar.Enabled = Not btnEditar.Enabled
        btnExcluir.Enabled = Not btnExcluir.Enabled
        btnSalvar.Enabled = Not btnSalvar.Enabled
        btnCancelar.Enabled = Not btnCancelar.Enabled
        '   barraNavegacao.Enabled = Not barraNavegacao.Enabled
        dgvNCM.Enabled = Not dgvNCM.Enabled
    End Sub
    Private Sub btExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If dgvNCM.Rows.Count > 0 Then

            If Not txtID.Text = String.Empty Then
                If MessageBox.Show("Deseja excluir o NCM: " & dgvNCM.CurrentRow.Cells("DESCRICAO").Value & "?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    NCM.Codigo = txtID.Text
                    If NCM.Excluir(NCM) Then
                        MessageBox.Show("Registro excluído com sucesso.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CarregarGridNCM()
                    Else
                        MessageBox.Show("Erro durante a operação. Tente Novamente", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Else
                MessageBox.Show("Selecione um Registro.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            '  LimparControles()

        End If

    End Sub

    Private Sub btFechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FrmNCM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Config.EnterTAB(e)
    End Sub

    Private Sub txtCodigoNCM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Config.CampoAlphaNumerico(e)
    End Sub

    Private Sub txtDescricao_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Config.CampoAlphaNumerico(e)
    End Sub

    Private Sub FrmNCM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CarregarGridNCM()

    End Sub


End Class