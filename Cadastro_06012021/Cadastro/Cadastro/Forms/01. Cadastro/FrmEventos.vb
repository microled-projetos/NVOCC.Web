
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
        ConsultarTransporte()
        FundoTextBox(Me)
        CampoNumerico(Me.txtDias)
        CampoNumerico(Me.txtHoras)


        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As New DataTable

        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 16 AND ID_TIPO_USUARIO = " & TipoUsuario)

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
        Me.txtDescricao.Focus()

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
            If Convert.ToInt32(e.ColumnIndex) >= 0 Then Coluna = Me.dgvConsulta.Columns(e.ColumnIndex).Index
        End If

    End Sub

    Private Sub dgvConsulta_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellEnter
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
            If Me.dgvConsulta.CurrentRow.Cells(4).Value.ToString() <> String.Empty Then
                Me.cbTransporte.SelectedValue = Me.dgvConsulta.CurrentRow.Cells(3).Value.ToString()
            End If
            Me.txtHoras.Text = Me.dgvConsulta.CurrentRow.Cells(5).Value.ToString()
            Me.txtDias.Text = Me.dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            Me.chkAtivo.Checked = Me.dgvConsulta.CurrentRow.Cells(7).Value.ToString()

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click
        Dim sql As String

        If ValidarCampos(Me) = False Then
            Exit Sub
        End If

        If txtCodigo.Text = String.Empty Then

            If VerificarDadosRepetidos(Me.dgvConsulta, 1, Me.txtDescricao.Text) Then
                Mensagens(Me, 7)
                Exit Sub
            End If

            Try
                If txtSequencia.Text = "" Then
                    txtSequencia.Text = " NULL "
                Else
                    txtSequencia.Text = "'" & txtSequencia.Text & "'"
                End If

                If txtHoras.Text = "" Then
                    txtHoras.Text = " NULL "
                End If

                If txtDias.Text = "" Then
                    txtDias.Text = " NULL "
                End If


                If Me.cbTransporte.SelectedIndex = -1 Then
                    sql = "INSERT INTO " & Banco.BancoNVOCC & "TB_EVENTO (NM_EVENTO,SEQUENCIA,PRZ_HORAS,PRZ_DIAS,FL_ATIVO ) VALUES ('" & Me.txtDescricao.Text & "'," & Me.txtSequencia.Text & " ," & txtHoras.Text & ", " & txtDias.Text & ", '" & chkAtivo.Checked & "')"
                Else
                    sql = "INSERT INTO " & Banco.BancoNVOCC & "TB_EVENTO (NM_EVENTO,SEQUENCIA,ID_VIATRANSPORTE,PRZ_HORAS,PRZ_DIAS,FL_ATIVO ) VALUES ('" & Me.txtDescricao.Text & "'," & Me.txtSequencia.Text & "," & cbTransporte.SelectedValue & " ," & txtHoras.Text & ", " & txtDias.Text & ", '" & chkAtivo.Checked & "')"
                End If



                If Banco.Execute(sql) Then
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
                If txtSequencia.Text = "" Then
                    txtSequencia.Text = " NULL "
                Else
                    txtSequencia.Text = "'" & txtSequencia.Text & "'"
                End If

                If txtHoras.Text = "" Then
                    txtHoras.Text = " NULL "
                End If

                If txtDias.Text = "" Then
                    txtDias.Text = " NULL "
                End If


                If Me.cbTransporte.SelectedIndex = -1 Then
                    sql = "UPDATE " & Banco.BancoNVOCC & "TB_EVENTO SET NM_EVENTO = '" & Me.txtDescricao.Text & "',SEQUENCIA = " & Me.txtSequencia.Text & ",PRZ_HORAS = " & txtHoras.Text & ", PRZ_DIAS = " & txtDias.Text & ", FL_ATIVO = '" & chkAtivo.Checked & "'  WHERE ID_EVENTO = " & Me.txtCodigo.Text & ""
                Else
                    sql = "UPDATE " & Banco.BancoNVOCC & "TB_EVENTO SET NM_EVENTO = '" & Me.txtDescricao.Text & "',SEQUENCIA = " & Me.txtSequencia.Text & ",ID_VIATRANSPORTE = " & cbTransporte.SelectedValue & ",PRZ_HORAS = " & txtHoras.Text & ", PRZ_DIAS = " & txtDias.Text & ", FL_ATIVO = '" & chkAtivo.Checked & "'  WHERE ID_EVENTO = " & Me.txtCodigo.Text & ""
                End If

                If Banco.Execute(sql) Then
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
                If Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_EVENTO WHERE ID_EVENTO = " & txtCodigo.Text & "") Then
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
        Dim query As String = "SELECT DTE_TB_EVENTOS.CODE, DTE_TB_EVENTOS.DESCR FROM " & Banco.BancoNVOCC & "DTE_TB_EVENTOS DTE_TB_EVENTOS ORDER BY DTE_TB_EVENTOS.CODE"
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
    Private Sub ConsultarTransporte()
        Me.cbTransporte.DataSource = Banco.List("SELECT ID_VIATRANSPORTE, NM_VIATRANSPORTE FROM [dbo].[TB_VIATRANSPORTE]")
        Me.cbTransporte.SelectedIndex = -1
    End Sub
End Class
