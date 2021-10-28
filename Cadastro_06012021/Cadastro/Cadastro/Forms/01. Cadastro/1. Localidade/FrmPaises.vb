Imports DgvFilterPopup
Public Class FrmPaises

    Dim Filtro As DgvFilterManager

    Private Sub Consultar()
        Me.dgvConsulta.DataSource = Banco.List("SELECT ID_PAIS, COD_PAIS CODE, NM_PAIS DESCR FROM " & Banco.BancoNVOCC & "TB_PAIS W")
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

    Private Sub dgvConsulta_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If
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

            Me.txtID.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtCodigoPais.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtPais.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()
        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim Ds As New DataTable

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtID.Text = String.Empty Then

            Ds = Banco.List("SELECT NM_PAIS FROM [TB_PAIS] where NM_PAIS = '" & txtPais.Text & "'")
            If Ds.Rows.Count > 0 Then
                MessageBox.Show("Já existe pais cadastrado com este nome", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Ds = Banco.List("SELECT COD_PAIS FROM [TB_PAIS] where COD_PAIS = '" & txtCodigoPais.Text & "'")
                If Ds.Rows.Count > 0 Then
                    MessageBox.Show("Já existe pais cadastrado com este código", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else

                    Try

                        If txtCodigoPais.Text = String.Empty Then
                            txtCodigoPais.Text = "NULL"
                        End If

                        If Banco.Execute("INSERT INTO " & Banco.BancoNVOCC & "TB_PAIS (NM_PAIS,COD_PAIS) VALUES ('" & Me.txtPais.Text & "'," & Me.txtCodigoPais.Text & ")") Then
                            Consultar()
                            Mensagens(Me, 1)
                        Else
                            Mensagens(Me, 4)
                        End If
                    Catch ex As Exception
                        Mensagens(Me, 4)
                    End Try

                End If
            End If

        Else



            Ds = Banco.List("SELECT NM_PAIS FROM [TB_PAIS] where NM_PAIS = '" & txtPais.Text & "' and ID_PAIS <> " & txtID.Text)
            If Ds.Rows.Count > 0 Then

                MessageBox.Show("Já existe pais cadastrado com este nome", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


            Else
                Ds = Banco.List("SELECT COD_PAIS FROM [TB_PAIS] where COD_PAIS = '" & txtCodigoPais.Text & "' and ID_PAIS <> " & txtID.Text)
                If Ds.Rows.Count > 0 Then
                    MessageBox.Show("Já existe pais cadastrado com este código", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else

                    Try
                        If txtCodigoPais.Text = String.Empty Then
                            txtCodigoPais.Text = "NULL"
                        End If

                        If Banco.Execute("UPDATE " & Banco.BancoNVOCC & "TB_PAIS SET NM_PAIS = '" & Me.txtPais.Text & "',COD_PAIS = " & Me.txtCodigoPais.Text & " WHERE ID_PAIS = " & txtID.Text) Then
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
            End If

        End If

        SetaControles()
        HabilitarCampos(Me, False)

    End Sub

    Private Sub btnExcluir_Click(sender As System.Object, e As System.EventArgs) Handles btnExcluir.Click

        If Not String.IsNullOrEmpty(txtID.Text) Then
            If MessageBox.Show("Deseja realmente excluir o registro selecionado?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_PAIS WHERE ID_PAIS = " & txtID.Text) Then
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

        If String.IsNullOrEmpty(txtID.Text) Then
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
        Dim query As String = "SELECT COD_PAIS CODE,NM_PAIS DESCR,ID_PAIS FROM " & Banco.BancoNVOCC & "TB_PAIS"
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
            'Filtro = New DgvFilterManager(Me.dgvConsulta)
        End If

        FundoTextBox(Me)


        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 13 AND ID_TIPO_USUARIO = " & TipoUsuario)

        If Ds.Rows.Count > 0 Then
            If Ds.Rows(0)("FL_ATUALIZAR").ToString <> True Then
                btnEditar.Visible = False
            Else
                btnEditar.Visible = True
            End If

            If Ds.Rows(0)("FL_EXCLUIR").ToString <> True Then
                btnExcluir.Visible = False
            Else
                btnExcluir.Visible = True
            End If

            If Ds.Rows(0)("FL_CADASTRAR").ToString <> True Then
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

    Private Sub FrmPaises_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnFiltro_Click_1(sender As Object, e As EventArgs) Handles btnFiltro.Click
        If dgvConsulta.Rows.Count > 0 Then
            Filtro.ShowPopup(Me.dgvConsulta.CurrentCell.ColumnIndex)
        End If
    End Sub
End Class
