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

    Private Sub dgvNCM_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvNCM.CellEnter
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

            Me.txtID.Text = dgvNCM.CurrentRow.Cells("ID_NCM").Value.ToString()
            Me.txtCodigoNCM.Text = dgvNCM.CurrentRow.Cells("CD_NCM").Value.ToString()
            Me.txtDescricao.Text = dgvNCM.CurrentRow.Cells("NM_NCM").Value.ToString()
            Me.txtAPNCM.Text = dgvNCM.CurrentRow.Cells("AP_NCM").Value.ToString()
            Me.ckbAtivo.Checked = dgvNCM.CurrentRow.Cells("FL_ATIVO").Value.ToString()
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

        If txtAPNCM.Text = String.Empty Then
            txtAPNCM.BackColor = Color.MistyRose
            MessageBox.Show("Campo obrigatório em branco.", "Cadastro de Linhas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtDescricao.Focus()
            Return False
        Else
            txtDescricao.BackColor = Color.White
        End If
        Return True

    End Function

    Private Sub btSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Dim Ds As New DataTable

        If ValidarCampos() Then

            NCM.CodigoNCM = Me.txtCodigoNCM.Text
            NCM.Descricao = Me.txtDescricao.Text
            NCM.AP_NCM = Me.txtAPNCM.Text
            NCM.Codigo = Me.txtID.Text
            NCM.FL_ATIVO = Me.ckbAtivo.CheckState


            If txtID.Text = String.Empty Then
                Ds = Banco.List("SELECT ID_NCM FROM TB_NCM WHERE CD_NCM = '" & txtCodigoNCM.Text & "' AND AP_NCM = '" & txtAPNCM.Text & "' AND NM_NCM = '" & txtDescricao.Text & "'")
                If Ds.Rows.Count > 0 Then

                    MessageBox.Show("Este registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                Else
                    If NCM.Inserir(NCM) Then
                        MessageBox.Show("Registro inserido com sucesso.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CarregarGridNCM()
                    Else
                        MessageBox.Show("Erro durante a operação. Tente Novamente", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                End If


            Else

                NCM.Codigo = txtID.Text



                Ds = Banco.List("SELECT ID_NCM FROM TB_NCM WHERE CD_NCM = '" & txtCodigoNCM.Text & "' AND AP_NCM = '" & txtAPNCM.Text & "' AND NM_NCM = '" & txtDescricao.Text & "' AND ID_NCM <> " & txtID.Text)
                If Ds.Rows.Count > 0 Then

                    MessageBox.Show("Este registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else
                    If NCM.Alterar(NCM) Then
                        MessageBox.Show("Registro alterado com sucesso.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CarregarGridNCM()
                    Else
                        MessageBox.Show("Erro durante a operação. Tente Novamente", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If

            End If

            LimparControles()
            SetarControles()
            HabilitarCampos(Me, False)

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


    Private Sub btNovo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click
        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetarControles()
        txtCodigoNCM.Focus()
        txtAPNCM.Text = ""
        txtCodigoNCM.Text = ""
        txtDescricao.Text = ""
        txtID.Text = ""
    End Sub

    Private Sub btEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click

        If txtID.Text = String.Empty Then
            MessageBox.Show("Selecione um Registro.", "Cadastro de NCM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        SetarControles()
        HabilitarCampos(Me, True)
        Me.txtDescricao.Focus()

    End Sub

    Private Sub btCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        HabilitarCampos(Me, False)
        LimparCampos(Me)
        SetarControles()

    End Sub

    Private Sub btExcluir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcluir.Click

        If dgvNCM.Rows.Count > 0 Then

            If Not txtID.Text = String.Empty Then
                If MessageBox.Show("Deseja excluir o NCM: " & dgvNCM.CurrentRow.Cells("NM_NCM").Value & "?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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
            'Filtro = New DgvFilterManager(Me.dgvNCM)
        End If


        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 19 AND ID_TIPO_USUARIO = " & TipoUsuario)

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

    Private Sub btFiltro_Click(sender As Object, e As EventArgs) Handles btFiltro.Click
        If dgvNCM.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvNCM.CurrentCell.ColumnIndex)
        End If
    End Sub

End Class