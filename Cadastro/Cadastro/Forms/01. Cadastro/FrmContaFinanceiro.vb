Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports DgvFilterPopup
Imports Gerencial
Imports Microsoft.VisualBasic.CompilerServices


Public Class FrmContaFinanceiro
    Private Coluna As Integer

    Private Sub FrmContaFinanceiro_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Consultar()
        ConsultarTipoConta()
        Geral.CampoNumerico(txtBanco)

        If dgvConsulta.Rows.Count > 0 Then
        End If

        Dim Tela As Control = Me
        Geral.FundoTextBox(Tela)
        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As DataTable = New DataTable()
        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 17 AND ID_TIPO_USUARIO = " & Conversions.ToString(TipoUsuario))

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

    Private Sub Consultar()
        dgvConsulta.DataSource = Banco.List("SELECT ID_CONTA_BANCARIA,NM_CONTA_BANCARIA, A.ID_TIPO_CONTA_BANCARIA,B.NM_TIPO_CONTA_BANCARIA,NR_BANCO,NR_AGENCIA,DG_AGENCIA, NR_CONTA, FL_ATIVO FROM TB_CONTA_BANCARIA A LEFT JOIN TB_TIPO_CONTA_BANCARIA B on B.ID_TIPO_CONTA_BANCARIA = A.ID_TIPO_CONTA_BANCARIA")
    End Sub

    Private Sub ConsultarTipoConta()
        cbTipoConta.DataSource = Banco.List("SELECT ID_TIPO_CONTA_BANCARIA,NM_TIPO_CONTA_BANCARIA FROM TB_TIPO_CONTA_BANCARIA")
        cbTipoConta.SelectedIndex = -1
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
    Private Sub MostraDados()
        If dgvConsulta.Rows.Count > 0 Then
            txtCodigo.Text = dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            txtDescricao.Text = dgvConsulta.CurrentRow.Cells(1).Value.ToString()

            If Operators.CompareString(dgvConsulta.CurrentRow.Cells(3).Value.ToString(), String.Empty, TextCompare:=False) <> 0 Then
                cbTipoConta.SelectedValue = dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            End If

            txtBanco.Text = dgvConsulta.CurrentRow.Cells(4).Value.ToString()
            txtAgencia.Text = dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            txtDigito.Text = dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            txtConta.Text = dgvConsulta.CurrentRow.Cells(7).Value.ToString()
            chkAtivo.Checked = Conversions.ToBoolean(dgvConsulta.CurrentRow.Cells(8).Value.ToString())
        End If
    End Sub

    Private Sub SetaControles()
        btnNovo.Enabled = Not btnNovo.Enabled
        btnEditar.Enabled = Not btnEditar.Enabled
        btnSalvar.Enabled = Not btnSalvar.Enabled
        btnCancelar.Enabled = Not btnCancelar.Enabled
        dgvConsulta.Enabled = Not dgvConsulta.Enabled
        pnControles.Enabled = Not pnControles.Enabled
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

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        SetaControles()
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If String.IsNullOrEmpty(txtCodigo.Text) Then
            MessageBox.Show("Selecione um registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        SetaControles()
        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        txtDescricao.Focus()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        SetaControles()
        txtDescricao.Focus()
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Not String.IsNullOrEmpty(txtCodigo.Text) AndAlso MessageBox.Show("Deseja realmente excluir o registro selecionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes AndAlso Banco.Execute(If(("DELETE FROM TB_CONTA_BANCARIA WHERE ID_CONTA_BANCARIA = " + txtCodigo.Text), "")) Then
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

        If Operators.CompareString(txtCodigo.Text, String.Empty, TextCompare:=False) = 0 Then
            Ds = Banco.List("SELECT ID_CONTA_BANCARIA FROM [TB_CONTA_BANCARIA] where NR_BANCO ='" & txtBanco.Text & "' and NR_AGENCIA ='" + txtAgencia.Text & "' and NR_CONTA = '" + txtConta.Text & "' and FL_ATIVO = 1")

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Esse registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                If Operators.CompareString(txtDigito.Text, "", TextCompare:=False) = 0 Then
                    txtDigito.Text = " NULL "
                Else
                    txtDigito.Text = "'" & txtDigito.Text & "'"
                End If

                Try

                    If Banco.Execute(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("INSERT INTO TB_CONTA_BANCARIA (NM_CONTA_BANCARIA, ID_TIPO_CONTA_BANCARIA,NR_BANCO,NR_AGENCIA,DG_AGENCIA, NR_CONTA, FL_ATIVO) VALUES ('" + txtDescricao.Text & "',", cbTipoConta.SelectedValue), ",'"), txtBanco.Text), "','"), txtAgencia.Text), "',"), txtDigito.Text), ",'"), txtConta.Text), "','"), chkAtivo.Checked), "')"))) Then
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

            If Operators.CompareString(txtDigito.Text, "", TextCompare:=False) = 0 Then
                txtDigito.Text = " NULL "
            Else
                txtDigito.Text = "'" & txtDigito.Text & "'"
            End If

            Ds = Banco.List("SELECT ID_CONTA_BANCARIA FROM [TB_CONTA_BANCARIA] where NR_BANCO ='" & txtBanco.Text & "' and NR_AGENCIA ='" + txtAgencia.Text & "' and NR_CONTA = '" + txtConta.Text & "' and FL_ATIVO = 1 and ID_CONTA_BANCARIA <> " + txtCodigo.Text)

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Esse registro já existe", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Try

                    If Banco.Execute(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("UPDATE TB_CONTA_BANCARIA SET NM_CONTA_BANCARIA = '" + txtDescricao.Text & "', ID_TIPO_CONTA_BANCARIA = ", cbTipoConta.SelectedValue), " ,NR_BANCO = '"), txtBanco.Text), "', NR_AGENCIA = '"), txtAgencia.Text), "',DG_AGENCIA = "), txtDigito.Text), ", NR_CONTA = '"), txtConta.Text), "', FL_ATIVO = '"), chkAtivo.Checked), "' WHERE ID_CONTA_BANCARIA = "), txtCodigo.Text), ""))) Then
                        Consultar()
                        Geral.Mensagens(Me, 2)
                        txtDigito.Text = ""
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

        SetaControles()
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
    End Sub


End Class