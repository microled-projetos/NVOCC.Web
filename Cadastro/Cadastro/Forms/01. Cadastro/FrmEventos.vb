Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices
Public Class FrmEventos

    Private Coluna As Integer

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_EVENTO,NM_EVENTO,SEQUENCIA,A.ID_VIATRANSPORTE,B.NM_VIATRANSPORTE,PRZ_HORAS,PRZ_DIAS,FL_ATIVO FROM TB_EVENTO A left join TB_VIATRANSPORTE B ON B.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE")
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

        Consultar()
        FundoTextBox(Me)

    End Sub

    Private Sub btnNovo_Click(sender As System.Object, e As System.EventArgs) Handles btnNovo.Click

        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtDescricao.Focus()

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtDescricao.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtSequencia.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim Tela As Control = Me

        If Not Geral.ValidarCampos(Tela) Then
            Return
        End If

        If Operators.CompareString(txtCodigo.Text, String.Empty, TextCompare:=False) = 0 Then

            If Geral.VerificarDadosRepetidos(dgvConsulta, 1, txtDescricao.Text) Then
                Geral.Mensagens(Me, 7)
                Return
            End If

            Try

                If Operators.CompareString(txtSequencia.Text, "", TextCompare:=False) = 0 Then
                    txtSequencia.Text = " NULL "
                Else
                    txtSequencia.Text = "'" & txtSequencia.Text & "'"
                End If

                If Operators.CompareString(txtHoras.Text, "", TextCompare:=False) = 0 Then
                    txtHoras.Text = " NULL "
                End If

                If Operators.CompareString(txtDias.Text, "", TextCompare:=False) = 0 Then
                    txtDias.Text = " NULL "
                End If

                Dim sql As String = (If((cbTransporte.SelectedIndex <> -1), Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("INSERT INTO " & Banco.BancoNVOCC & "TB_EVENTO (NM_EVENTO,SEQUENCIA,ID_VIATRANSPORTE,PRZ_HORAS,PRZ_DIAS,FL_ATIVO ) VALUES ('" + txtDescricao.Text & "'," + txtSequencia.Text & ",", cbTransporte.SelectedValue), " ,"), txtHoras.Text), ", "), txtDias.Text), ", '"), chkAtivo.Checked), "')")), ("INSERT INTO " & Banco.BancoNVOCC & "TB_EVENTO (NM_EVENTO,SEQUENCIA,PRZ_HORAS,PRZ_DIAS,FL_ATIVO ) VALUES ('" + txtDescricao.Text & "'," + txtSequencia.Text & " ," + txtHoras.Text & ", " + txtDias.Text & ", '" + Conversions.ToString(chkAtivo.Checked) & "')")))

                If Banco.Execute(sql) Then
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
        Else

            Try

                If Operators.CompareString(txtSequencia.Text, "", TextCompare:=False) = 0 Then
                    txtSequencia.Text = " NULL "
                Else
                    txtSequencia.Text = "'" & txtSequencia.Text & "'"
                End If

                If Operators.CompareString(txtHoras.Text, "", TextCompare:=False) = 0 Then
                    txtHoras.Text = " NULL "
                End If

                If Operators.CompareString(txtDias.Text, "", TextCompare:=False) = 0 Then
                    txtDias.Text = " NULL "
                End If

                Dim sql As String = (If((cbTransporte.SelectedIndex <> -1), Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("UPDATE " & Banco.BancoNVOCC & "TB_EVENTO SET NM_EVENTO = '" + txtDescricao.Text & "',SEQUENCIA = " + txtSequencia.Text & ",ID_VIATRANSPORTE = ", cbTransporte.SelectedValue), ",PRZ_HORAS = "), txtHoras.Text), ", PRZ_DIAS = "), txtDias.Text), ", FL_ATIVO = '"), chkAtivo.Checked), "'  WHERE ID_EVENTO = "), txtCodigo.Text), "")), (If(("UPDATE " & Banco.BancoNVOCC & "TB_EVENTO SET NM_EVENTO = '" + txtDescricao.Text & "',SEQUENCIA = " + txtSequencia.Text & ",PRZ_HORAS = " + txtHoras.Text & ", PRZ_DIAS = " + txtDias.Text & ", FL_ATIVO = '" + Conversions.ToString(chkAtivo.Checked) & "'  WHERE ID_EVENTO = " + txtCodigo.Text), ""))))

                If Banco.Execute(sql) Then
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

        Tela = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
        SetaControles()
    End Sub

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsNullOrEmpty(txtCodigo.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM TB_EVENTO WHERE ID_EVENTO = " & txtCodigo.Text & "") Then
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
        txtDescricao.Focus()

    End Sub

    Private Sub btnFiltro_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click
        SetaControles()
        LimparCampos(Me)
        HabilitarCampos(Me, False)
    End Sub

    Private Sub btImprimir_Click(sender As System.Object, e As System.EventArgs)


        Dim formulas As List(Of String) = New List(Of String)
        Dim valores As List(Of String) = New List(Of String)

        formulas.Add("usuario")
        valores.Add(FrmPrincipal.lblUsuario.Text)

        Dim rptName As String = "\rpts\DteEventos.rpt"
        Dim query As String = "SELECT DTE_TB_EVENTO.CODE, DTE_TB_EVENTO.DESCR FROM TB_EVENTO DTE_TB_EVENTO ORDER BY DTE_TB_EVENTO.CODE"
        'Banco.TestaSQL(query)
        'ChamarRelatorio(rptName, query, "0", formulas, valores)

    End Sub

    Private Sub FrmAgencias_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmAgencias_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged

    End Sub

    Private Sub pbCarregando_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub pnControles_Paint(sender As Object, e As PaintEventArgs) Handles pnControles.Paint

    End Sub

    Private Sub dgvConsulta_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConsulta.CellContentClick

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub txtSufixo_TextChanged(sender As Object, e As EventArgs) Handles txtSequencia.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub txtDescricao_TextChanged(sender As Object, e As EventArgs) Handles txtDescricao.TextChanged

    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub
End Class
