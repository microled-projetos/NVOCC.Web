Imports DgvFilterPopup
Public Class FrmNCM

    Dim NCM As New NCM
    Dim Filtro As DgvFilterManager

    Private Sub txtCodigo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigoNCM.Enter
        txtCodigoNCM.BackColor = Color.Cornsilk
    End Sub

    Private Sub txtCodigo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoNCM.Leave
        txtCodigoNCM.BackColor = Color.White
    End Sub

    Private Sub txtDescricao_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescricao.Enter
        txtDescricao.BackColor = Color.Cornsilk
    End Sub

    Private Sub txtDescricao_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescricao.Leave
        txtDescricao.BackColor = Color.White
    End Sub

    Private Sub dgNCM_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNCM.CellClick

        If dgvNCM.Rows.Count > 0 Then
            ExibeInformacoes()
        End If

    End Sub

    Private Sub dgvNCM_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvNCM.KeyUp

        If dgvNCM.Rows.Count > 0 Then
            ExibeInformacoes()
        End If

    End Sub

    Private Sub FrmNCM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub btPrim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPrim.Click

        If dgvNCM.Rows.Count > 0 Then
            dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, 0)
            ExibeInformacoes()
        End If

    End Sub

    Private Sub btAnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAnt.Click

        If dgvNCM.Rows.Count > 0 Then
            If dgvNCM.CurrentRow.Index > 0 Then
                dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, dgvNCM.CurrentCell.RowIndex - 1)
                ExibeInformacoes()
            End If
        End If

    End Sub

    Private Sub BtProx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtProx.Click

        If dgvNCM.Rows.Count > 0 Then
            If dgvNCM.CurrentRow.Index < dgvNCM.Rows.Count - 1 Then
                dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, dgvNCM.CurrentCell.RowIndex + 1)
                ExibeInformacoes()
            End If
        End If

    End Sub

    Private Sub btUlt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btUlt.Click

        If dgvNCM.Rows.Count > 0 Then
            dgvNCM.CurrentCell = dgvNCM(dgvNCM.CurrentCell.ColumnIndex, dgvNCM.Rows.Count - 1)
            ExibeInformacoes()
        End If

    End Sub

    Private Sub ExibeInformacoes()

        If dgvNCM.Rows.Count > 0 Then

            Me.txtCodigo.Text = dgvNCM.CurrentRow.Cells("colCodigo").Value.ToString()
            Me.txtCodigoNCM.Text = dgvNCM.CurrentRow.Cells("colCodigo").Value.ToString()
            Me.txtDescricao.Text = dgvNCM.CurrentRow.Cells("colDescricao").Value.ToString()

            Me.chkPoliciaFederal.Checked = dgvNCM.CurrentRow.Cells("colPoliciaFederal").Value.ToString()
            Me.chkExercito.Checked = dgvNCM.CurrentRow.Cells("colExercito").Value.ToString()
            Me.chkMapa.Checked = dgvNCM.CurrentRow.Cells("colMapa").Value.ToString()
            Me.chkAnvisa.Checked = dgvNCM.CurrentRow.Cells("colAnvisa").Value.ToString()
            Me.chkPoliciaCivil.Checked = dgvNCM.CurrentRow.Cells("colPoliciaCivil").Value.ToString()
            Me.chkIbama.Checked = dgvNCM.CurrentRow.Cells("colIbama").Value.ToString()

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

    Private Sub btSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

        If ValidarCampos() Then

            NCM.CodigoNCM = Me.txtCodigoNCM.Text
            NCM.Descricao = Me.txtDescricao.Text
            NCM.Anvisa = Me.chkAnvisa.CheckState
            NCM.Ibama = Me.chkIbama.CheckState
            NCM.MAPA = Me.chkMapa.Checked
            NCM.Exercito = Me.chkExercito.CheckState
            NCM.PoliciaCivil = Me.chkPoliciaCivil.CheckState
            NCM.PoliciaFederal = Me.chkPoliciaFederal.CheckState

            If txtCodigo.Text = String.Empty Then

                If NCM.Inserir(NCM) Then
                    MessageBox.Show("Registro inserido com sucesso.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CarregarGridNCM()
                Else
                    MessageBox.Show("Erro durante a operação. Tente Novamente", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else

                NCM.Codigo = txtCodigo.Text

                If NCM.Alterar(NCM) Then
                    MessageBox.Show("Registro alterado com sucesso.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CarregarGridNCM()
                Else
                    MessageBox.Show("Erro durante a operação. Tente Novamente", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If

            LimparControles()
            SetarControles()
            DesabilitarControles()

        End If

    End Sub

    Private Sub SetarControles()

        btnNovo.Enabled = Not (btnNovo.Enabled)
        btnEditar.Enabled = Not (btnEditar.Enabled)
        btnExcluir.Enabled = Not (btnExcluir.Enabled)
        btnSalvar.Enabled = Not (btnSalvar.Enabled)
        btnCancelar.Enabled = Not (btnCancelar.Enabled)
        barraNavegacao.Enabled = Not (barraNavegacao.Enabled)
        dgvNCM.Enabled = Not (dgvNCM.Enabled)

    End Sub

    Private Sub LimparControles()

        For Each Controle In Me.PnGeral.Controls
            Select Case Controle.GetType().Name
                Case "TextBox"
                    DirectCast(Controle, TextBox).Clear()
                    DirectCast(Controle, TextBox).BackColor = Color.White
                Case "CheckBox"
                    DirectCast(Controle, CheckBox).Checked = False
            End Select
        Next

    End Sub

    Private Sub DesabilitarControles()

        For Each Controle In Me.PnGeral.Controls
            Select Case Controle.GetType().Name
                Case "TextBox"
                    DirectCast(Controle, TextBox).Enabled = False
                Case "CheckBox"
                    DirectCast(Controle, CheckBox).Enabled = False
            End Select
        Next

    End Sub

    Private Sub HabilitarControles()

        For Each Controle In Me.PnGeral.Controls
            Select Case Controle.GetType().Name
                Case "TextBox"
                    DirectCast(Controle, TextBox).Enabled = True
                Case "CheckBox"
                    DirectCast(Controle, CheckBox).Enabled = True
            End Select
        Next

    End Sub

    Private Sub btNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click

        HabilitarControles()
        LimparControles()
        SetarControles()
        txtCodigoNCM.Focus()

    End Sub

    Private Sub btEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click

        If txtCodigo.Text = String.Empty Then
            MessageBox.Show("Selecione um Registro.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        SetarControles()
        HabilitarCampos(Me, True)
        Me.txtDescricao.Focus()

    End Sub

    Private Sub btCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        DesabilitarControles()
        LimparControles()
        SetarControles()

    End Sub

    Private Sub btExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click

        If dgvNCM.Rows.Count > 0 Then

            If Not txtCodigo.Text = String.Empty Then
                If MessageBox.Show("Deseja excluir o NCM: " & dgvNCM.CurrentRow.Cells("DESCRICAO").Value & "?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    NCM.Codigo = txtCodigo.Text
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

            LimparControles()

        End If

    End Sub

    Private Sub btFechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub FrmNCM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Config.EnterTAB(e)
    End Sub

    Private Sub txtCodigoNCM_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigoNCM.KeyPress
        Config.CampoAlphaNumerico(e)
    End Sub

    Private Sub txtDescricao_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescricao.KeyPress
        Config.CampoAlphaNumerico(e)
    End Sub

    Private Sub FrmNCM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Filtro Is Nothing Then
            Filtro.ActivateAllFilters(False)
        End If

        CarregarGridNCM()

        If Me.dgvNCM.Rows.Count > 0 Then
            Filtro = New DgvFilterManager(Me.dgvNCM)
        End If

    End Sub

    Private Sub btFiltro_Click(sender As Object, e As EventArgs) Handles btFiltro.Click
        If dgvNCM.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvNCM.CurrentCell.ColumnIndex)
        End If
    End Sub
End Class