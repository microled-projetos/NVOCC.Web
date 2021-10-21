Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices
Public Class FrmPortos

    Private Coluna As Integer
    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_PORTO,CD_PORTO,NM_PORTO,isnull(A.ID_CIDADE,0)ID_CIDADE,B.NM_CIDADE,SIGLA_IATA,CD_SIGLA,A.ID_VIATRANSPORTE,C.NM_VIATRANSPORTE,FL_ATIVO FROM TB_PORTO A left join TB_CIDADE B ON B.ID_CIDADE = A.ID_CIDADE left join TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE")
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
        If Not String.IsNullOrEmpty(txtID.Text) AndAlso MessageBox.Show("Deseja realmente excluir o registro selecionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes AndAlso Banco.Execute("DELETE FROM TB_PORTO WHERE ID_PORTO = " + txtID.Text) Then
            Consultar()
            Dim Tela As Control = Me
            Geral.LimparCampos(Tela)
        End If
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Dim Ds As DataTable = New DataTable()
        Dim Tela As Control = Me

        If Not Geral.ValidarCampos(Tela) Then
            Return
        End If
        If Operators.CompareString(txtID.Text, String.Empty, TextCompare:=False) = 0 Then
            Ds = Banco.List("SELECT ID_PORTO FROM [TB_PORTO] where NM_PORTO = '" & txtNome.Text & "' and CD_PORTO ='" & txtID.Text & "'")

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Try


                    If Banco.Execute("INSERT INTO TB_PORTO (CD_PORTO,NM_PORTO,ID_CIDADE,SIGLA_IATA,CD_SIGLA,ID_VIATRANSPORTE,FL_ATIVO) VALUES ('" & txtCodigo.Text & "','" & txtNome.Text & "'," & cbCidade.SelectedValue & ",'" & txtSiglaIATA.Text & "','" & txtCodSigla.Text & "'," & cbTransporte.SelectedValue & ",'" & Conversions.ToString(chkAtivo.Checked) & "')") Then
                        Consultar()
                        Geral.Mensagens(Me, 1)
                    Else
                        Geral.Mensagens(Me, 4)
                    End If

                Catch ex3 As Exception
                    ProjectData.SetProjectError(ex3)
                    Dim ex2 As Exception = ex3
                    Geral.Mensagens(Me, 4)
                    ProjectData.ClearProjectError()
                End Try
            End If
        Else
            Ds = Banco.List("SELECT ID_PORTO FROM [TB_PORTO] where NM_PORTO = '" & txtNome.Text & "' and CD_PORTO ='" & txtCodigo.Text & "' and FL_ATIVO = 1 AND ID_PORTO <> " & txtID.Text)

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Try

                    If Banco.Execute("UPDATE TB_PORTO SET CD_PORTO = '" & txtCodigo.Text & "', NM_PORTO = '" & txtNome.Text & "',SIGLA_IATA = '" & txtSiglaIATA.Text & "', FL_ATIVO = '" & Conversions.ToString(chkAtivo.Checked) & "' ,CD_SIGLA = '" & txtCodSigla.Text & "',ID_VIATRANSPORTE = " & cbTransporte.SelectedValue & ",ID_CIDADE = " & cbCidade.SelectedValue & " WHERE ID_PORTO = " & txtID.Text) Then
                        Consultar()
                        Geral.Mensagens(Me, 2)
                    Else
                        Geral.Mensagens(Me, 5)
                    End If

                Catch ex4 As Exception
                    ProjectData.SetProjectError(ex4)
                    Dim ex As Exception = ex4
                    Geral.Mensagens(Me, 5)
                    ProjectData.ClearProjectError()
                End Try
            End If
        End If

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

    Private Sub FrmPortos_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmPortos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub


End Class