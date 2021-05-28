Imports DgvFilterPopup
Public Class FrmPaises

    Dim Filtro As DgvFilterManager

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT CODE,DESCR,SIGLA,BIGRAMA,ISNULL(FUMIGACAO,0) AS FUMIGACAO FROM " & Banco.BancoSGIPA & "TB_CAD_PAISES WHERE CODE <> 0")
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

    Private Sub btnNovo_Click(sender As System.Object, e As System.EventArgs) Handles btnNovo.Click

        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()
        Me.txtPais.Focus()

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then

            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtPais.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtSigla.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
            Me.txtCodigoPais.Text = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            Me.chkFumigacao.Checked = Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString()

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then

            If VerificarDadosRepetidos(Me.dgvConsulta, 2, Me.txtSigla.Text) Then
                Mensagens(Me, 7, "Campo: Sigla")
                Exit Sub
            End If

            Try
                Dim Codigo As String = Banco.ExecuteScalar("SELECT MAX(CAST(CODE AS NUMERIC)) + 1 FROM " & Banco.BancoSGIPA & "TB_CAD_PAISES ")
                If Banco.Execute("INSERT INTO " & Banco.BancoSGIPA & "TB_CAD_PAISES (CODE,DESCR,SIGLA,BIGRAMA,FUMIGACAO) VALUES (" & Codigo & ", '" & Me.txtPais.Text & "','" & Me.txtSigla.Text & "','" & Me.txtCodigoPais.Text & "'," & Me.chkFumigacao.CheckState & ")") Then
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
                If Banco.Execute("UPDATE " & Banco.BancoSGIPA & "TB_CAD_PAISES SET DESCR = '" & Me.txtPais.Text & "',SIGLA = '" & Me.txtSigla.Text & "',BIGRAMA = '" & Me.txtCodigoPais.Text & "',FUMIGACAO = " & Me.chkFumigacao.CheckState & " WHERE CODE = " & txtCodigo.Text & " ") Then
                    Consultar()
                    BackgroundWorker1.RunWorkerAsync()
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
                If Banco.Execute("DELETE FROM " & Banco.BancoSGIPA & "TB_CAD_PAISES WHERE CODE = " & txtCodigo.Text & "") Then
                    Consultar()
                    LimparCampos(Me)
                End If
            End If
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

    Private Sub btnEditar_Click(sender As System.Object, e As System.EventArgs) Handles btnEditar.Click

        If String.IsNullOrEmpty(txtCodigo.Text) Then
            MessageBox.Show("Selecione um registro.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        SetaControles()
        HabilitarCampos(Me, True)
        txtPais.Focus()

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

        Dim rptName As String = "\rpts\tbpais.rpt"
        Dim query As String = "SELECT CODE,DESCR,SIGLA,BIGRAMA,FUMIGACAO FROM " & Banco.BancoSGIPA & "TB_CAD_PAISES"
        'Banco.TestaSQL(query)
        'ChamarRelatorio(rptName, query, "0", formulas, valores)

    End Sub

    Private Sub FrmPaises_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub btnPrimeiro_Click(sender As System.Object, e As System.EventArgs) Handles btnPrimeiro.Click

        If dgvConsulta.Rows.Count > 0 Then
            dgvConsulta.CurrentCell = dgvConsulta.Rows(0).Cells(0)
            MostraDados()
        End If

    End Sub

    Private Sub FrmPaises_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If Not Filtro Is Nothing Then
            Filtro.ActivateAllFilters(False)
        End If

        Consultar()

        If Me.dgvConsulta.Rows.Count > 0 Then
            Filtro = New DgvFilterManager(Me.dgvConsulta)
        End If

        FundoTextBox(Me)

    End Sub

    Private Sub FrmPaises_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnFiltro_Click_1(sender As Object, e As EventArgs) Handles btnFiltro.Click
        If dgvConsulta.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvConsulta.CurrentCell.ColumnIndex)
        End If
    End Sub
End Class
