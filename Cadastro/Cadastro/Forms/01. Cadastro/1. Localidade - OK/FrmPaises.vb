﻿Imports DgvFilterPopup
Imports Microsoft.VisualBasic.CompilerServices
Imports Gerencial.My



Public Class FrmPaises

    Dim Filtro As DgvFilterManager

    Private Sub Consultar()
        dgvConsulta.DataSource = Banco.List("SELECT ID_PAIS, COD_PAIS, NM_PAIS FROM TB_PAIS")
        pbCarregando.Visible = False
    End Sub

    Private Sub SetaControles()
        btnNovo.Enabled = Not btnNovo.Enabled
        btnEditar.Enabled = Not btnEditar.Enabled
        btnSalvar.Enabled = Not btnSalvar.Enabled
        btnExcluir.Enabled = Not btnExcluir.Enabled
        btnCancelar.Enabled = Not btnCancelar.Enabled
        dgvConsulta.Enabled = Not dgvConsulta.Enabled
        pnControles.Enabled = Not pnControles.Enabled
    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If

    End Sub

    Private Sub MostraDados()
        If dgvConsulta.Rows.Count > 0 Then
            txtID.Text = dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            txtCodigoPais.Text = dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            txtPais.Text = dgvConsulta.CurrentRow.Cells(2).Value.ToString()
        End If
    End Sub


    Private Sub btnSair_Click(sender As System.Object, e As System.EventArgs) Handles btnSair.Click
        Me.Close()
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
    Private Sub btnPrimeiro_Click(sender As System.Object, e As System.EventArgs) Handles btnPrimeiro.Click

        If dgvConsulta.Rows.Count > 0 Then
            dgvConsulta.CurrentCell = dgvConsulta.Rows(0).Cells(0)
            MostraDados()
        End If

    End Sub

    Private Sub FrmPaises_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub


    Private Sub FrmPaises_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Filtro IsNot Nothing Then
            Filtro.ActivateAllFilters(Active:=False)
        End If

        Consultar()

        If dgvConsulta.Rows.Count > 0 Then
        End If

        Dim Tela As Control = Me
        Geral.FundoTextBox(Tela)
        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As DataTable = New DataTable()
        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 13 AND ID_TIPO_USUARIO = " & Conversions.ToString(TipoUsuario))

        If Ds.Rows.Count > 0 Then

            If Not Conversions.ToBoolean(Ds.Rows(0)("FL_ATUALIZAR").ToString()) Then
                btnEditar.Visible = False
            Else
                btnEditar.Visible = True
            End If

            If Not Conversions.ToBoolean(Ds.Rows(0)("FL_EXCLUIR").ToString()) Then
                btnExcluir.Visible = False
            Else
                btnExcluir.Visible = True
            End If

            If Not Conversions.ToBoolean(Ds.Rows(0)("FL_CADASTRAR").ToString()) Then
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

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Not String.IsNullOrEmpty(txtID.Text) AndAlso MessageBox.Show("Deseja realmente excluir o registro selecionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes AndAlso Banco.Execute("DELETE FROM TB_PAIS WHERE ID_PAIS = " + txtID.Text) Then
            Consultar()
            Dim Tela As Control = Me
            Geral.LimparCampos(Tela)
        End If
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        SetaControles()
        txtPais.Focus()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        SetaControles()
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
    End Sub

    Private Sub FrmPaises_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub dgvConsulta_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
        If dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Dim Ds As DataTable = New DataTable()
        Dim Tela As Control = Me

        If Not Geral.ValidarCampos(Tela) Then
            Return
        End If

        If Operators.CompareString(txtID.Text, String.Empty, TextCompare:=False) = 0 Then
            Ds = Banco.List("SELECT NM_PAIS FROM [TB_PAIS] where NM_PAIS = '" & txtPais.Text & "'")

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Já existe pais cadastrado com este nome", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Ds = Banco.List("SELECT COD_PAIS FROM [TB_PAIS] where COD_PAIS = '" & txtCodigoPais.Text & "'")

                If Ds.Rows.Count > 0 Then
                    MessageBox.Show("Já existe pais cadastrado com este código", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else

                    Try

                        If Operators.CompareString(txtCodigoPais.Text, String.Empty, TextCompare:=False) = 0 Then
                            txtCodigoPais.Text = "NULL"
                        End If

                        If Banco.Execute("INSERT INTO TB_PAIS (NM_PAIS,COD_PAIS) VALUES ('" + txtPais.Text & "'," + txtCodigoPais.Text & ")") Then
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
            End If
        Else
            Ds = Banco.List("SELECT NM_PAIS FROM [TB_PAIS] where NM_PAIS = '" & txtPais.Text & "' and ID_PAIS <> " + txtID.Text)

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Já existe pais cadastrado com este nome", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Ds = Banco.List("SELECT COD_PAIS FROM [TB_PAIS] where COD_PAIS = '" & txtCodigoPais.Text & "' and ID_PAIS <> " + txtID.Text)

                If Ds.Rows.Count > 0 Then
                    MessageBox.Show("Já existe pais cadastrado com este código", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else

                    Try

                        If Operators.CompareString(txtCodigoPais.Text, String.Empty, TextCompare:=False) = 0 Then
                            txtCodigoPais.Text = "NULL"
                        End If

                        If Banco.Execute("UPDATE TB_PAIS SET NM_PAIS = '" + txtPais.Text & "',COD_PAIS = " + txtCodigoPais.Text & " WHERE ID_PAIS = " + txtID.Text) Then
                            Consultar()
                            BackgroundWorker1.RunWorkerAsync()
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
        End If

        SetaControles()
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Selecione um registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        SetaControles()
        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        txtPais.Focus()
    End Sub
End Class
