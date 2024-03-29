﻿Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices
Public Class FrmMoedas
    Private Coluna As Integer

    Private Sub Consultar()
        dgvConsulta.DataSource = Banco.List("SELECT ID_MOEDA,CD_MOEDA,NM_MOEDA,SIGLA_MOEDA, FL_ATIVO FROM TB_MOEDA")
    End Sub

    Private Sub FrmMoedas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Consultar()

        If dgvConsulta.Rows.Count > 0 Then
        End If

        Dim Tela As Control = Me
        Geral.FundoTextBox(Tela)
        Dim UsuarioSistema As Integer = Banco.UsuarioSistema
        Dim Ds As DataTable = New DataTable()
        ' Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 18 AND ID_TIPO_USUARIO = " & Conversions.ToString(TipoUsuario))
        Ds = Banco.List("SELECT 
MAX(CONVERT(VARCHAR, FL_EXCLUIR))FL_EXCLUIR ,
MAX(CONVERT(VARCHAR,FL_ATUALIZAR))FL_ATUALIZAR,
MAX(CONVERT(VARCHAR,FL_CADASTRAR))FL_CADASTRAR
FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 18 AND ID_TIPO_USUARIO IN( SELECT distinct ID_TIPO_USUARIO from TB_VINCULO_USUARIO where ID_USUARIO = " & Conversions.ToString(UsuarioSistema) & ")  ")

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

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        Tela = Me
        Geral.LimparCampos(Tela)
        SetaControles()
        txtCodigo.Focus()
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Selecione um registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        SetaControles()
        txtID.Focus()
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Not String.IsNullOrEmpty(txtID.Text) AndAlso MessageBox.Show("Deseja realmente excluir o registro selecionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes AndAlso Banco.Execute(If(("DELETE FROM TB_MOEDA WHERE ID_MOEDA = " + txtID.Text), "")) Then
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
            Ds = Banco.List("SELECT ID_MOEDA FROM [TB_MOEDA] where NM_MOEDA = '" & txtNome.Text & "' and CD_MOEDA ='" + txtID.Text & "'")

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Try

                    If Operators.CompareString(txtSigla.Text, "", TextCompare:=False) = 0 Then
                        txtSigla.Text = " NULL "
                    Else
                        txtSigla.Text = "'" & txtSigla.Text & "'"
                    End If

                    If Banco.Execute("INSERT INTO TB_MOEDA (CD_MOEDA,NM_MOEDA,SIGLA_MOEDA, FL_ATIVO) VALUES ('" & txtCodigo.Text & "','" + txtNome.Text & "'," + txtSigla.Text & ",'" + Conversions.ToString(chkAtivo.Checked) & "')") Then
                        Consultar()
                        Geral.Mensagens(Me, 1)
                        txtSigla.Text = ""
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
            Ds = Banco.List("SELECT ID_MOEDA FROM [TB_MOEDA] where NM_MOEDA = '" & txtNome.Text & "' and CD_MOEDA ='" + txtCodigo.Text & "' and FL_ATIVO = 1 AND ID_MOEDA <> " + txtID.Text)

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Try

                    If Operators.CompareString(txtSigla.Text, "", TextCompare:=False) = 0 Then
                        txtSigla.Text = " NULL "
                    Else
                        txtSigla.Text = "'" & txtSigla.Text & "'"
                    End If

                    If Banco.Execute("UPDATE TB_MOEDA SET CD_MOEDA = '" & txtCodigo.Text & "', NM_MOEDA = '" + txtNome.Text & "',SIGLA_MOEDA = " + txtSigla.Text & ", FL_ATIVO = '" + Conversions.ToString(chkAtivo.Checked) & "' WHERE ID_MOEDA = " + txtID.Text) Then
                        Consultar()
                        Geral.Mensagens(Me, 2)
                        txtSigla.Text = ""
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

        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
        SetaControles()
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

    Private Sub MostraDados()
        If dgvConsulta.Rows.Count > 0 Then
            txtID.Text = dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            txtCodigo.Text = dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            txtNome.Text = dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            txtSigla.Text = dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            chkAtivo.Checked = Conversions.ToBoolean(dgvConsulta.CurrentRow.Cells(4).Value.ToString())
        End If
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
End Class