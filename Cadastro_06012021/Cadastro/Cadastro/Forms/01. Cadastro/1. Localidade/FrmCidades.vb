Imports DgvFilterPopup
Public Class FrmCidades

    Dim Filtro As DgvFilterManager

    Private Sub ConsultarPaises()
        Me.cbPais.DataSource = Banco.List("SELECT ID_PAIS, NM_PAIS DESCR FROM " & Banco.BancoNVOCC & "TB_PAIS ORDER BY DESCR")
        Me.cbPais.SelectedIndex = -1
    End Sub

    Private Sub ConsultarEstados()
        Me.cbUF.DataSource = Banco.List("SELECT ID_ESTADO, NM_ESTADO DESCR FROM " & Banco.BancoNVOCC & "TB_ESTADO ORDER BY DESCR")
        Me.cbUF.SelectedIndex = -1
    End Sub

    Private Sub Consultar()

        Me.dgvConsulta.DataSource = Banco.List("SELECT a.ID_CIDADE ,A.COD_IBGE as IBGE, A.NM_CIDADE as DESCR, B.NM_ESTADO as ESTADO, C.NM_PAIS as PAIS, A.VL_ISS ,A.ID_ESTADO,A.ID_PAIS FROM TB_CIDADE A LEFT JOIN TB_ESTADO B on A.ID_ESTADO = B.ID_ESTADO LEFT JOIN TB_PAIS C on A.ID_PAIS = C.ID_PAIS WHERE NOT A.COD_IBGE IS NULL ORDER BY ESTADO,DESCR")
        Me.pbCarregando.Visible = False

    End Sub
    Private Sub dgvConsulta_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If
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
            'Filtro = New DgvFilterManager(Me.dgvConsulta)
            'LoadFilters(Filtro)
        End If

        FundoTextBox(Me)

        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 12 AND ID_TIPO_USUARIO = " & TipoUsuario)

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

    Private Sub btnNovo_Click(sender As System.Object, e As System.EventArgs) Handles btnNovo.Click

        LimparCampos(Me)
        HabilitarCampos(Me, True)

        SetaControles()

        Me.txtCidade.Focus()

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then

            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            Me.txtIBGE.Text = Me.dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            Me.txtCidade.Text = Me.dgvConsulta.CurrentRow.Cells(2).Value.ToString()

            If Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString() <> String.Empty Then
                Me.cbUF.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            End If

            If Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString() <> String.Empty Then
                Me.cbPais.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString()
            End If

            Me.txtVL_ISS.Text = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        If Not ValidarCampos(Me) Then
            Exit Sub
        End If

        If txtVL_ISS.Text = "" Then
            txtVL_ISS.Text = "0"
        Else
            txtVL_ISS.Text = txtVL_ISS.Text.Replace(",", ".")
        End If

        If txtCodigo.Text = String.Empty Then
            Try

                Dim IBGE As String = Banco.ExecuteScalar("SELECT MAX(COD_IBGE) + 1 FROM " & Banco.BancoNVOCC & "TB_CIDADE ")
                Dim sql As String = "INSERT INTO TB_CIDADE (COD_IBGE,NM_CIDADE,ID_PAIS,ID_ESTADO,VL_ISS) VALUES (" & IBGE & ", '" & txtCidade.Text & "'," & cbPais.SelectedValue & "," & cbUF.SelectedValue & ",'" & txtVL_ISS.Text & "')"
                Banco.Execute(sql)
                Consultar()
                Mensagens(Me, 1)

            Catch ex As Exception
                Mensagens(Me, 4)
            End Try
        Else
            Try

                Dim sql As String = "UPDATE TB_CIDADE Set NM_CIDADE = '" & txtCidade.Text & "',ID_PAIS = " & cbPais.SelectedValue & ",ID_ESTADO = " & cbUF.SelectedValue & ", VL_ISS ='" & txtVL_ISS.Text & "' WHERE COD_IBGE = " & txtIBGE.Text & " AND ID_CIDADE = " & txtCodigo.Text

                Banco.Execute(sql)
                Consultar()
                Mensagens(Me, 2)

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
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_CIDADE WHERE COD_IBGE = " & txtIBGE.Text & " AND ID_CIDADE = " & txtCodigo.Text) Then
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
        txtCidade.Focus()

    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click

        SetaControles()
        LimparCampos(Me)
        HabilitarCampos(Me, False)


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
