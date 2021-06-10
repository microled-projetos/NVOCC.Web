Imports DgvFilterPopup
Public Class FrmNavios

    Private Coluna As Integer
    Dim Filtro As DgvFilterManager

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT CODE,DESCR,NACIONALIDADE,CASE WHEN FLAG_RORO IS NULL THEN 'NÃO' WHEN FLAG_RORO = 0 THEN 'NÃO' ELSE 'SIM'END AS RO_RO FROM " & Banco.BancoSGIPA & "DTE_TB_NAVIOS")
    End Sub

    Private Sub SetaControles()

        btnNovo.Enabled = Not (btnNovo.Enabled)
        btnEditar.Enabled = Not (btnEditar.Enabled)
        btnSalvar.Enabled = Not (btnSalvar.Enabled)
        btnCancelar.Enabled = Not (btnCancelar.Enabled)
        dgvConsulta.Enabled = Not (dgvConsulta.Enabled)
        pnControles.Enabled = Not (pnControles.Enabled)

    End Sub

    Private Sub FrmPrincipal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If Not Filtro Is Nothing Then
            Filtro.ActivateAllFilters(False)
        End If

        Consultar()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Filtro = New DgvFilterManager(Me.dgvConsulta)
        End If

        FundoTextBox(Me)

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
            Me.txtNome.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtNacionalidade.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            Me.chkRoRo.Checked = IIf(Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString().Equals("SIM"), True, False)
        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then
            Try
                If Banco.Execute("INSERT INTO " & Banco.BancoSGIPA & "DTE_TB_NAVIOS (DESCR, NACIONALIDADE, FLAG_RORO) VALUES ('" & Me.txtNome.Text & "','" & Me.txtNacionalidade.Text & "', '" & Me.chkRoRo.CheckState & "')") Then
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
                If Banco.Execute("UPDATE " & Banco.BancoSGIPA & "DTE_TB_NAVIOS SET FLAG_RORO = '" & Me.chkRoRo.CheckState & "' WHERE CODE = " & Me.txtCodigo.Text & "") Then
                    Consultar()
                    Mensagens(Me, 2)
                Else
                    Mensagens(Me, 5)
                End If
            Catch ex As Exception
                Mensagens(Me, 5)
            End Try
        End If

        SetaControles()
        HabilitarCampos(Me, False)

    End Sub

    Private Sub btnSair_Click(sender As System.Object, e As System.EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub btnPrimeiro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        If dgvConsulta.Rows.Count > 0 Then
            dgvConsulta.CurrentCell = dgvConsulta.Rows(0).Cells(0)
            MostraDados()
        End If

    End Sub

    Private Sub btnProximo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If dgvConsulta.Rows.Count > 0 Then
            If dgvConsulta.CurrentRow.Index < dgvConsulta.Rows.Count - 1 Then
                dgvConsulta.CurrentCell = dgvConsulta.Rows(dgvConsulta.CurrentRow.Index + 1).Cells(0)
                MostraDados()
            End If
        End If

    End Sub

    Private Sub btnUltimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If dgvConsulta.Rows.Count > 0 Then
            dgvConsulta.CurrentCell = dgvConsulta.Rows(dgvConsulta.Rows.Count - 1).Cells(0)
            MostraDados()
        End If

    End Sub

    Private Sub btnAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

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

        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtNome.Focus()

    End Sub

    Private Sub btnFiltro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If dgvConsulta.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvConsulta.CurrentCell.ColumnIndex)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click
        SetaControles()
        LimparCampos(Me)
    End Sub

    Private Sub FrmNavios_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmNavios_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtNome.Focus()
    End Sub

End Class
