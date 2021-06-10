
Public Class FrmEventos

    Private Coluna As Integer

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT AUTONUM,DESCR,SUFIXO FROM " & Banco.BancoSGIPA & "DTE_TB_EVENTOS")
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
            Me.txtSufixo.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then

            If VerificarDadosRepetidos(Me.dgvConsulta, 1, Me.txtDescricao.Text) Then
                Mensagens(Me, 7)
                Exit Sub
            End If

            Try

                If Banco.Execute("INSERT INTO " & Banco.BancoSGIPA & "DTE_TB_EVENTOS (DESCR,SUFIXO) VALUES ('" & Me.txtDescricao.Text & "','" & Me.txtSufixo.Text & "')") Then
                    Consultar()
                    Mensagens(Me, 1)
                Else
                    Mensagens(Me, 4)
                End If
            Catch ex As Exception
                Mensagens(Me, 4)
            End Try
        Else
            Try
                If Banco.Execute("UPDATE " & Banco.BancoSGIPA & "DTE_TB_EVENTOS SET DESCR = '" & Me.txtDescricao.Text & "',SUFIXO='" & Me.txtSufixo.Text & "'  WHERE AUTONUM = " & Me.txtCodigo.Text & "") Then
                    Consultar()
                    Mensagens(Me, 2)
                Else
                    Mensagens(Me, 5)
                End If
            Catch ex As Exception
                Mensagens(Me, 5)
            End Try
        End If

        LimparCampos(Me)
        HabilitarCampos(Me, False)
        SetaControles()

    End Sub

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsNullOrEmpty(txtCodigo.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoSGIPA & "DTE_TB_EVENTOS WHERE AUTONUM = " & txtCodigo.Text & "") Then
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

    Private Sub btnFiltro_Click(sender As System.Object, e As System.EventArgs) Handles btnFiltro.Click

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
        Dim query As String = "SELECT DTE_TB_EVENTOS.CODE, DTE_TB_EVENTOS.DESCR FROM " & Banco.BancoSGIPA & "DTE_TB_EVENTOS DTE_TB_EVENTOS ORDER BY DTE_TB_EVENTOS.CODE"
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

    Private Sub txtSufixo_TextChanged(sender As Object, e As EventArgs) Handles txtSufixo.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub txtDescricao_TextChanged(sender As Object, e As EventArgs) Handles txtDescricao.TextChanged

    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub
End Class
