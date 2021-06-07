Imports DgvFilterPopup
Public Class FrmCidades

    Dim Filtro As DgvFilterManager

    Private Sub ConsultarPaises()
        Me.cbPais.DataSource = Banco.List("SELECT SIGLA,DESCR FROM " & Banco.BancoSGIPA & "TB_CAD_PAISES ORDER BY DESCR")
        Me.cbPais.SelectedIndex = -1
    End Sub

    Private Sub ConsultarEstados()
        Me.cbUF.DataSource = Banco.List("SELECT SIGLA,DESCR FROM " & Banco.BancoSGIPA & "TB_CAD_ESTADOS ORDER BY DESCR")
        Me.cbUF.SelectedIndex = -1
    End Sub

    Private Sub Consultar()

        Me.dgvConsulta.DataSource = Banco.List("SELECT A.IBGE, A.DESCR, A.ESTADO,A.PAIS,CASE WHEN A.MARGEM = 0 THEN 'DIREITA' WHEN A.MARGEM = 1 THEN 'ENTRE MARGENS' WHEN A.MARGEM = 2 THEN 'ESQUERDA' END MARGEM FROM " & Banco.BancoSGIPA & "TB_CAD_CIDADES A WHERE NOT A.IBGE IS NULL ORDER BY ESTADO,DESCR")
        Me.pbCarregando.Visible = False

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

        ConsultarEstados()
        ConsultarPaises()
        Consultar()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Filtro = New DgvFilterManager(Me.dgvConsulta)
            LoadFilters(Filtro)
        End If

        FundoTextBox(Me)

    End Sub

    Private Sub btnNovo_Click(sender As System.Object, e As System.EventArgs) Handles btnNovo.Click

        LimparCampos(Me)
        HabilitarCampos(Me, True)

        SetaControles()

        Me.txtCidade.Focus()
        Me.rbDireita.Checked = True

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then

            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtCidade.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()

            If Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString() <> String.Empty Then
                Me.cbUF.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            End If

            If Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString() <> String.Empty Then
                Me.cbPais.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            End If

            If Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString().Equals("DIREITA") Then
                rbDireita.Checked = True
            ElseIf Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString().Equals("ENTRE MARGENS") Then
                rbEntreMargens.Checked = True
            Else
                rbEsquerda.Checked = True
            End If

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        If Not ValidarCampos(Me) Then
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then
            Try
                Dim IBGE As String = Banco.ExecuteScalar("SELECT MAX(IBGE) + 1 FROM " & Banco.BancoSGIPA & "TB_CAD_CIDADES ")
                If Banco.Execute("INSERT INTO " & Banco.BancoSGIPA & "TB_CAD_CIDADES (IBGE,DESCR,PAIS,ESTADO,MARGEM,SIGLA_PAIS,SIGLA_ESTADO) VALUES (" & IBGE & ", '" & txtCidade.Text & "','" & cbPais.SelectedValue & "','" & cbUF.Text & "'," & IIf(rbDireita.Checked, "0", "") & IIf(rbEntreMargens.Checked, "1", "") & IIf(rbEsquerda.Checked, "2", "") & ",'" & Mid(cbPais.Text, 1, 5) & "','" & Mid(cbUF.Text, 1, 5) & "')") Then
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
                If Banco.Execute("UPDATE " & Banco.BancoSGIPA & "TB_CAD_CIDADES SET DESCR = '" & txtCidade.Text & "',PAIS = '" & cbPais.SelectedValue & "',ESTADO = '" & cbUF.Text & "',MARGEM = " & IIf(rbDireita.Checked, "0", "") & IIf(rbEntreMargens.Checked, "1", "") & IIf(rbEsquerda.Checked, "2", "") & " ,SIGLA_PAIS = '" & Mid(cbPais.Text, 1, 5) & "',SIGLA_ESTADO = '" & Mid(cbUF.Text, 1, 5) & "' WHERE IBGE = " & txtCodigo.Text & "") Then
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

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsNullOrEmpty(txtCodigo.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoSGIPA & "TB_CAD_CIDADES WHERE IBGE = " & txtCodigo.Text & "") Then
                    Consultar()
                    LimparCampos(Me)
                End If
            End If
        End If

        Me.rbDireita.Checked = True

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
        txtCidade.Focus()

    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click

        SetaControles()
        LimparCampos(Me)
        HabilitarCampos(Me, False)

        Me.rbDireita.Checked = True

    End Sub

    Private Sub FrmCidades_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmCidades_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnFiltro_Click(sender As Object, e As EventArgs) Handles btnFiltro.Click
        If dgvConsulta.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvConsulta.CurrentCell.ColumnIndex)
        End If
    End Sub
End Class
