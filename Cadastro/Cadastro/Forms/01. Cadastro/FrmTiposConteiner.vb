Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices

Public Class FrmTiposConteiner

    Private Coluna As Integer

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_TIPO_CONTAINER, NM_TIPO_CONTAINER ,ISO,MAXGROSS,TEU,TAMANHO_CONTAINER,FL_ATIVO FROM TB_TIPO_CONTAINER")
    End Sub

    Private Sub SetaControles()

        btnEditar.Enabled = Not (btnEditar.Enabled)
        btnSalvar.Enabled = Not (btnSalvar.Enabled)
        btnCancelar.Enabled = Not (btnCancelar.Enabled)
        dgvConsulta.Enabled = Not (dgvConsulta.Enabled)
        pnControles.Enabled = Not (pnControles.Enabled)

    End Sub

    Private Sub dgvConsulta_CellEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If dgvConsulta.Rows.Count > 0 Then
            MostraDados()

            If Convert.ToInt32(e.ColumnIndex) >= 0 Then
                Coluna = dgvConsulta.Columns(e.ColumnIndex).Index
            End If
        End If
    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If

    End Sub

    Private Sub MostraDados()
        If dgvConsulta.Rows.Count > 0 Then
            txtID.Text = dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            txtDescricao.Text = dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            txtISO.Text = dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            txtMaxGross.Text = dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            txtTEU.Text = dgvConsulta.CurrentRow.Cells(4).Value.ToString()
            txtTamanho.Text = dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            chkAtivo.Checked = Conversions.ToBoolean(dgvConsulta.CurrentRow.Cells(6).Value.ToString())
        End If
    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim Ds As DataTable = New DataTable()
        Dim Tela As Control = Me

        If Not Geral.ValidarCampos(Tela) Then
            Return
        End If

        If Operators.CompareString(txtID.Text, String.Empty, TextCompare:=False) = 0 Then
            Ds = Banco.List("SELECT ID_TIPO_CONTAINER FROM [TB_TIPO_CONTAINER] where NM_TIPO_CONTAINER = '" & txtDescricao.Text & "' and TEU = '" + txtTEU.Text & "' and TAMANHO_CONTAINER = " + txtTamanho.Text)

            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                Try
                    txtMaxGross.Text = txtMaxGross.Text.Replace(",", ".")
                    txtTEU.Text = txtTEU.Text.Replace(",", ".")

                    If Operators.CompareString(txtMaxGross.Text, "", TextCompare:=False) = 0 Then
                        txtMaxGross.Text = " NULL "
                    Else
                        txtMaxGross.Text = "'" & txtMaxGross.Text & "'"
                    End If

                    If Operators.CompareString(txtISO.Text, "", TextCompare:=False) = 0 Then
                        txtISO.Text = " NULL "
                    Else
                        txtISO.Text = "'" & txtISO.Text & "'"
                    End If

                    If Banco.Execute("INSERT INTO TB_TIPO_CONTAINER (NM_TIPO_CONTAINER,ISO,MAXGROSS,TEU,TAMANHO_CONTAINER,FL_ATIVO) VALUES ('" + txtDescricao.Text & "'," + txtISO.Text & "," + txtMaxGross.Text & ",'" + txtTEU.Text & "'," + txtTamanho.Text & "," + Conversions.ToString(CInt(chkAtivo.CheckState)) & ")") Then
                        Consultar()
                        Geral.Mensagens(Me, 1)
                    Else
                        Geral.Mensagens(Me, 4)
                    End If

                Catch ex3 As Exception
                    ProjectData.SetProjectError(ex3)
                    Dim ex2 As Exception = ex3
                    Geral.Mensagens(Me, 5)
                    ProjectData.ClearProjectError()
                End Try
            End If
        Else

            Try
                Ds = Banco.List("SELECT ID_TIPO_CONTAINER FROM [TB_TIPO_CONTAINER] where NM_TIPO_CONTAINER = '" & txtDescricao.Text & "' and TEU = '" + txtTEU.Text & "' and TAMANHO_CONTAINER = " + txtTamanho.Text & " and FL_ATIVO = 1 AND ID_TIPO_CONTAINER <> " + txtID.Text)

                If Ds.Rows.Count > 0 Then
                    MessageBox.Show("Este registro já existe!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    txtMaxGross.Text = txtMaxGross.Text.Replace(",", ".")
                    txtTEU.Text = txtTEU.Text.Replace(",", ".")
                    If Operators.CompareString(txtMaxGross.Text, "", TextCompare:=False) = 0 Then
                        txtMaxGross.Text = " NULL "
                    Else
                        txtMaxGross.Text = "'" & txtMaxGross.Text & "'"
                    End If

                    If Operators.CompareString(txtISO.Text, "", TextCompare:=False) = 0 Then
                        txtISO.Text = " NULL "
                    Else
                        txtISO.Text = "'" & txtISO.Text & "'"
                    End If

                    If Banco.Execute(If(("UPDATE TB_TIPO_CONTAINER SET NM_TIPO_CONTAINER = '" + txtDescricao.Text & "', ISO = " + txtISO.Text & ", MAXGROSS = " + txtMaxGross.Text & ", TEU = '" + txtTEU.Text & "',TAMANHO_CONTAINER=" + txtTamanho.Text & ",FL_ATIVO=" + Conversions.ToString(CInt(chkAtivo.CheckState)) & " WHERE ID_TIPO_CONTAINER = " + txtID.Text), "")) Then
                        Consultar()
                        Geral.Mensagens(Me, 2)
                    Else
                        Geral.Mensagens(Me, 5)
                    End If
                End If

            Catch ex4 As Exception
                ProjectData.SetProjectError(ex4)
                Dim ex As Exception = ex4
                Geral.Mensagens(Me, 5)
                ProjectData.ClearProjectError()
            End Try
        End If
        LimparCampos(Me)
        SetaControles()
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
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
        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Selecione um registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        SetaControles()
        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        txtID.Enabled = False
    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click
        SetaControles()
        LimparCampos(Me)
        HabilitarCampos(Me, False)
    End Sub
    Private Sub FrmTiposConteiner_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmTiposConteiner_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtDescricao.Focus()
    End Sub

    Private Sub FrmTiposConteiner_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Consultar()

        If dgvConsulta.Rows.Count > 0 Then
        End If

        Dim Tela As Control = Me
        Geral.FundoTextBox(Tela)
        Geral.CampoNumerico(txtTamanho)
        Geral.CampoNumerico(txtID)
        Dim UsuarioSistema As Integer = Banco.UsuarioSistema
        Dim Ds As DataTable = New DataTable()
        '  Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 20 AND ID_TIPO_USUARIO = " & Conversions.ToString(TipoUsuario))
        Ds = Banco.List("SELECT 
MAX(CONVERT(VARCHAR, FL_EXCLUIR))FL_EXCLUIR ,
MAX(CONVERT(VARCHAR,FL_ATUALIZAR))FL_ATUALIZAR,
MAX(CONVERT(VARCHAR,FL_CADASTRAR))FL_CADASTRAR
FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 20 AND ID_TIPO_USUARIO IN( SELECT distinct ID_TIPO_USUARIO from TB_VINCULO_USUARIO where ID_USUARIO = " & Conversions.ToString(UsuarioSistema) & ")  ")

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
        If Not String.IsNullOrEmpty(txtID.Text) AndAlso MessageBox.Show("Deseja realmente excluir o registro selecionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes AndAlso Banco.Execute(If(("DELETE FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = " + txtID.Text), "")) Then
            Consultar()
            Dim Tela As Control = Me
            Geral.LimparCampos(Tela)
        End If
    End Sub
End Class
