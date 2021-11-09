Imports DgvFilterPopup
Public Class FrmServico

    Private Coluna As Integer
    Dim Filtro As DgvFilterManager

    Private Sub ConsultarTransporte()
        Me.cbTransporte.DataSource = Banco.List("SELECT ID_VIATRANSPORTE, NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]")
        Me.cbTransporte.SelectedIndex = -1
    End Sub

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_SERVICO, NM_SERVICO,CD_TRIBUTACAO_RPS,CD_ATIVIDADE_RPS,NM_VIATRANSPORTE,A.ID_VIATRANSPORTE,SIGLA_PROCESSO,FL_ATIVO from [dbo].[TB_SERVICO] A LEFT JOIN TB_VIATRANSPORTE B ON A.ID_VIATRANSPORTE = B.ID_VIATRANSPORTE")
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

        ConsultarTransporte()
        Consultar()
        Me.dgvConsulta.Focus()

        If Me.dgvConsulta.Rows.Count > 0 Then
            'Filtro = New DgvFilterManager(Me.dgvConsulta)
            'LoadFilters(Filtro)
        End If

        FundoTextBox(Me)
        CampoNumerico(txtAtividadeRPS)
        CampoNumerico(txtTributacaoRPS)


        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 15 AND ID_TIPO_USUARIO = " & TipoUsuario)

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

        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.cbTransporte.Focus()

        Me.cbTransporte.SelectedIndex = 0

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
            Me.txtNomeServico.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtTributacaoRPS.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            Me.txtAtividadeRPS.Text = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            If Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString() <> String.Empty Then
                Me.cbTransporte.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            End If
            Me.txtSigla.Text = Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            Me.ckbAtivo.Checked = Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString()

        End If

    End Sub
    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim Ds As New DataTable

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If


        If txtCodigo.Text = String.Empty Then
            Ds = Banco.List("SELECT ID_SERVICO FROM [TB_SERVICO] where NM_SERVICO = '" & txtNomeServico.Text & "' and CD_ATIVIDADE_RPS ='" & txtAtividadeRPS.Text & "' and ID_VIATRANSPORTE = " & cbTransporte.SelectedValue & " and SIGLA_PROCESSO ='" & txtSigla.Text & "' and CD_TRIBUTACAO_RPS ='" & txtTributacaoRPS.Text & "' and FL_ATIVO = 1")
            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Esse registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Try
                    If txtAtividadeRPS.Text = "" Then
                        txtAtividadeRPS.Text = " NULL "
                    Else
                        txtAtividadeRPS.Text = "'" & txtAtividadeRPS.Text & "'"
                    End If

                    If txtTributacaoRPS.Text = "" Then
                        txtTributacaoRPS.Text = " NULL "
                    Else
                        txtTributacaoRPS.Text = "'" & txtTributacaoRPS.Text & "'"
                    End If

                    If Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "[TB_SERVICO] (NM_SERVICO,CD_TRIBUTACAO_RPS,CD_ATIVIDADE_RPS,ID_VIATRANSPORTE,SIGLA_PROCESSO,FL_ATIVO) VALUES ('" & txtNomeServico.Text & "'," & txtTributacaoRPS.Text & "," & txtAtividadeRPS.Text & "," & cbTransporte.SelectedValue & ",'" & txtSigla.Text & "'," & IIf(ckbAtivo.Checked, "1", "0") & ")") Then
                        Consultar()
                        Mensagens(Me, 1)
                        txtTributacaoRPS.Text = ""
                        txtAtividadeRPS.Text = ""
                    Else
                        Mensagens(Me, 4)
                    End If
                Catch ex As Exception
                    Mensagens(Me, 4)
                End Try

            End If


        Else

            Ds = Banco.List("SELECT ID_SERVICO FROM [TB_SERVICO] where NM_SERVICO = '" & txtNomeServico.Text & "' and CD_ATIVIDADE_RPS ='" & txtAtividadeRPS.Text & "' and ID_VIATRANSPORTE = " & cbTransporte.SelectedValue & " and SIGLA_PROCESSO ='" & txtSigla.Text & "' and CD_TRIBUTACAO_RPS ='" & txtTributacaoRPS.Text & "' and FL_ATIVO = 1 and ID_SERVICO <> " & txtCodigo.Text)
            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Esse registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Try
                    Dim sql As String = ("UPDATE TB_SERVICO SET NM_SERVICO = '" & txtNomeServico.Text & "',FL_ATIVO = '" & ckbAtivo.Checked & "', CD_ATIVIDADE_RPS = '" & txtAtividadeRPS.Text & "', CD_TRIBUTACAO_RPS = '" & txtTributacaoRPS.Text & "',ID_VIATRANSPORTE = " & cbTransporte.SelectedValue & ", SIGLA_PROCESSO = '" & txtSigla.Text & "' WHERE ID_SERVICO = " & txtCodigo.Text)



                    If txtAtividadeRPS.Text = "" Then
                        sql = sql.Replace(" CD_ATIVIDADE_RPS = ''", " CD_ATIVIDADE_RPS = NULL ")
                    End If

                    If txtTributacaoRPS.Text = "" Then
                        sql = sql.Replace(" CD_TRIBUTACAO_RPS = ''", " CD_TRIBUTACAO_RPS = NULL ")
                    End If


                    Banco.Execute(sql)
                    Dim TESTE1 As String = sql
                    Consultar()
                    Mensagens(Me, 2)
                Catch ex As Exception
                    Mensagens(Me, 5)
                End Try
            End If

        End If

        SetaControles()
        HabilitarCampos(Me, False)

    End Sub

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsNullOrEmpty(txtCodigo.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_SERVICO WHERE ID_SERVICO = " & txtCodigo.Text & "") Then
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

        If String.IsNullOrEmpty(txtCodigo.Text) Then
            MessageBox.Show("Selecione um registro.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        SetaControles()
        HabilitarCampos(Me, True)
        cbTransporte.Focus()

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

    Private Sub FrmAvisoExtrato_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub cbSerieNF_DropDown(sender As System.Object, e As System.EventArgs)
        AjustarLarguraComboBox(sender, e)
    End Sub


End Class
