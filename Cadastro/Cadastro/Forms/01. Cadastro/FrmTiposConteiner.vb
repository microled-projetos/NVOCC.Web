Imports DgvFilterPopup
Public Class FrmTiposConteiner

    Private Coluna As Integer
    Dim Filtro As DgvFilterManager

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT CODE,DESCR + ' (' + CODIGO + ')' DESCR,TARA_MIN_20,TARA_MIN_40,TARA_MAX_20,TARA_MAX_40,CASE WHEN FLAG_TEMPERATURA = 1 THEN 'SIM' ELSE 'NÃO' END FLAG_TEMPERATURA,CASE WHEN FLAG_EXCESSO = 1 THEN 'SIM' ELSE 'NÃO' END FLAG_EXCESSO,CODIGO FROM " & Banco.BancoSGIPA & "DTE_TB_TIPOS_CONTEINER")
    End Sub

    Private Sub SetaControles()

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
            LoadFilters(Filtro)
        End If

        FundoTextBox(Me)

        CampoNumerico(Me.txtMin20)
        CampoNumerico(Me.txtMin40)
        CampoNumerico(Me.txtMax20)
        CampoNumerico(Me.txtMax40)

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
            Me.txtMin20.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            Me.txtMin40.Text = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            Me.txtMax20.Text = Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString()
            Me.txtMax40.Text = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            Me.chkTemp.Checked = IIf(Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString() = "SIM", True, False)
            Me.chkExcesso.Checked = IIf(Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString() = "SIM", True, False)
            Me.txtCodigoDescr.Text = Me.dgvConsulta.CurrentRow.Cells(8).Value.ToString()
        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then
            Try
                Dim Codigo As String = Banco.ExecuteScalar("SELECT MAX(CAST(CODE AS NUMERIC))+1 FROM " & Banco.BancoSGIPA & "DTE_TB_TIPOS_CONTEINER")
                If Banco.Execute("INSERT INTO " & Banco.BancoSGIPA & "DTE_TB_TIPOS_CONTEINER (CODE, DESCR, CODIGO, FLAG_TEMPERATURA, FLAG_EXCESSO, TARA_MIN_20, TARA_MIN_40, TARA_MAX_20, TARA_MAX_40) VALUES (" & Codigo & ",'" & Me.txtDescricao.Text & "','" & Me.txtCodigoDescr.Text & "'," & Me.chkTemp.CheckState & "," & chkExcesso.CheckState & "," & Val(Me.txtMin20.Text) & "," & Val(Me.txtMin40.Text) & "," & Val(Me.txtMax20.Text) & "," & Val(Me.txtMax40.Text) & ")") Then
                    Consultar()
                    Mensagens(Me, 1)
                Else
                    Mensagens(Me, 4)
                End If
            Catch ex As Exception
                Mensagens(Me, 5)
            End Try
        Else
            Try
                If Banco.Execute("UPDATE " & Banco.BancoSGIPA & "DTE_TB_TIPOS_CONTEINER SET TARA_MIN_20 = " & txtMin20.Text & ", TARA_MIN_40 = " & txtMin40.Text & ", TARA_MAX_20 = '" & txtMax20.Text & "', TARA_MAX_40 = '" & txtMax40.Text & "',flag_excesso=" & chkExcesso.CheckState & ",flag_temperatura=" & chkTemp.CheckState & " WHERE CODE = " & txtCodigo.Text & "") Then
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
        txtDescricao.Enabled = False
        txtCodigoDescr.Enabled = False

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

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtDescricao.Focus()
    End Sub

End Class
