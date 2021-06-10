Imports DgvFilterPopup
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.CompilerServices


Public Class FrmCidades

    Dim Filtro As DgvFilterManager

    Private Sub ConsultarPaises()
        cbPais.DataSource = Banco.List("SELECT ID_PAIS, NM_PAIS DESCR FROM " & Banco.BancoNVOCC & "TB_PAIS ORDER BY DESCR")
        cbPais.SelectedIndex = -1
    End Sub

    Private Sub ConsultarEstados()
        cbUF.DataSource = Banco.List("SELECT ID_ESTADO, NM_ESTADO DESCR FROM " & Banco.BancoNVOCC & "TB_ESTADO ORDER BY DESCR")
        cbUF.SelectedIndex = -1
    End Sub

    Private Sub Consultar()
        dgvConsulta.DataSource = Banco.List("SELECT a.ID_CIDADE ,A.COD_IBGE as IBGE, A.NM_CIDADE as DESCR, B.NM_ESTADO as ESTADO, C.NM_PAIS as PAIS, A.VL_ISS ,A.ID_ESTADO,A.ID_PAIS FROM TB_CIDADE A LEFT JOIN TB_ESTADO B on A.ID_ESTADO = B.ID_ESTADO LEFT JOIN TB_PAIS C on A.ID_PAIS = C.ID_PAIS WHERE NOT A.COD_IBGE IS NULL ORDER BY ESTADO,DESCR")
        pbCarregando.Visible = False
    End Sub

    Private Sub SetaControles()
        btnNovo.Enabled = Not btnNovo.Enabled
        btnEditar.Enabled = Not btnEditar.Enabled
        btnSalvar.Enabled = Not btnSalvar.Enabled
        btnExcluir.Enabled = Not btnExcluir.Enabled
        btnCancelar.Enabled = Not btnCancelar.Enabled
        dgvConsulta.Enabled = Not dgvConsulta.Enabled
        pnControles.Enabled = Not pnControles.Enabled
    End Sub

    Private Sub FrmPrincipal_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Filtro IsNot Nothing Then
            Filtro.ActivateAllFilters(Active:=False)
        End If

        ConsultarEstados()
        ConsultarPaises()
        Consultar()

        If dgvConsulta.Rows.Count > 0 Then
        End If

        Dim Tela As Control = Me
        Geral.FundoTextBox(Tela)
        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim Ds As DataTable = New DataTable()
        Ds = Banco.List("SELECT FL_EXCLUIR,FL_ATUALIZAR,FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 12 AND ID_TIPO_USUARIO = " & Conversions.ToString(TipoUsuario))

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

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        SetaControles()
        txtCidade.Focus()
    End Sub

    Private Sub dgvConsulta_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If
    End Sub

    Private Sub MostraDados()
        If dgvConsulta.Rows.Count > 0 Then
            txtCodigo.Text = dgvConsulta.CurrentRow.Cells(0).Value.ToString()
            txtIBGE.Text = dgvConsulta.CurrentRow.Cells(1).Value.ToString()
            txtCidade.Text = dgvConsulta.CurrentRow.Cells(2).Value.ToString()

            If Operators.CompareString(dgvConsulta.CurrentRow.Cells(6).Value.ToString(), String.Empty, TextCompare:=False) <> 0 Then
                cbUF.SelectedValue = dgvConsulta.CurrentRow.Cells(6).Value.ToString()
            End If

            If Operators.CompareString(dgvConsulta.CurrentRow.Cells(7).Value.ToString(), String.Empty, TextCompare:=False) <> 0 Then
                cbPais.SelectedValue = dgvConsulta.CurrentRow.Cells(7).Value.ToString()
            End If

            txtVL_ISS.Text = dgvConsulta.CurrentRow.Cells(5).Value.ToString()
        End If
    End Sub
    Private Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Tela As Control = Me

        If Not Geral.ValidarCampos(Tela) Then
            Return
        End If

        If Operators.CompareString(txtVL_ISS.Text, "", TextCompare:=False) = 0 Then
            txtVL_ISS.Text = "0"
        Else
            txtVL_ISS.Text = txtVL_ISS.Text.Replace(",", ".")
        End If

        If Operators.CompareString(txtCodigo.Text, String.Empty, TextCompare:=False) = 0 Then

            Try
                Dim IBGE As String = obtemCodIBGEUF(cbUF.SelectedText)
                txtIBGE.Text = IBGE
                Dim sql2 As String = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("INSERT INTO TB_CIDADE (COD_IBGE,NM_CIDADE,ID_PAIS,ID_ESTADO,VL_ISS) VALUES (" & IBGE & ", '" & txtCidade.Text & "',", cbPais.SelectedValue), ","), cbUF.SelectedValue), ",'"), txtVL_ISS.Text), "')"))
                Banco.Execute(sql2)
                Consultar()
                Geral.Mensagens(Me, 1)
            Catch ex3 As Exception
                ProjectData.SetProjectError(ex3)
                Dim ex2 As Exception = ex3
                Geral.Mensagens(Me, 4)
                ProjectData.ClearProjectError()
            End Try
        Else

            Try
                Dim IBGE As String = obtemCodIBGEUF(cbUF.SelectedText)
                txtIBGE.Text = IBGE
                Dim sql As String = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("UPDATE TB_CIDADE Set NM_CIDADE = '" & txtCidade.Text & "',ID_PAIS = ", cbPais.SelectedValue), ",ID_ESTADO = "), cbUF.SelectedValue), ", VL_ISS ='"), txtVL_ISS.Text), "' WHERE COD_IBGE = "), txtIBGE.Text), " AND ID_CIDADE = "), txtCodigo.Text))
                Banco.Execute(sql)
                Consultar()
                Geral.Mensagens(Me, 2)
            Catch ex4 As Exception
                ProjectData.SetProjectError(ex4)
                Dim ex As Exception = ex4
                Geral.Mensagens(Me, 5)
                ProjectData.ClearProjectError()
            End Try
        End If

        SetaControles()
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
    End Sub
    Private Sub btnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not String.IsNullOrEmpty(txtCodigo.Text) AndAlso MessageBox.Show("Deseja realmente excluir o registro selecionado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes AndAlso Banco.Execute("DELETE FROM " & Banco.BancoNVOCC & "TB_CIDADE WHERE COD_IBGE = " + txtIBGE.Text & " AND ID_CIDADE = " + txtCodigo.Text) Then
            Consultar()
            Dim Tela As Control = Me
            Geral.LimparCampos(Tela)
        End If
    End Sub

    Private Sub btnSair_Click(sender As System.Object, e As System.EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub btnPrimeiro_Click(ByVal sender As Object, ByVal e As EventArgs)
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

    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs)
        If String.IsNullOrEmpty(txtCodigo.Text) Then
            MessageBox.Show("Selecione um registro.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        SetaControles()
        Dim Tela As Control = Me
        Geral.HabilitarCampos(Tela, Habilita:=True)
        txtCidade.Focus()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetaControles()
        Dim Tela As Control = Me
        Geral.LimparCampos(Tela)
        Tela = Me
        Geral.HabilitarCampos(Tela, Habilita:=False)
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
    Public Function obtemCodIBGEUF(Optional ByVal UF As String = "") As String
        Dim ret As String
        Try
            Select Case UF.ToUpper
                Case "AC"
                    ret = "12"
                Case "AL"
                    ret = "27"
                Case "AM"
                    ret = "13"
                Case "AP"
                    ret = "16"
                Case "BA"
                    ret = "29"
                Case "CE"
                    ret = "23"
                Case "DF"
                    ret = "53"
                Case "ES"
                    ret = "32"
                Case "GO"
                    ret = "52"
                Case "MA"
                    ret = "21"
                Case "MG"
                    ret = "31"
                Case "MS"
                    ret = "50"
                Case "MT"
                    ret = "51"
                Case "PA"
                    ret = "15"
                Case "PB"
                    ret = "25"
                Case "PE"
                    ret = "26"
                Case "PI"
                    ret = "22"
                Case "PR"
                    ret = "41"
                Case "RJ"
                    ret = "33"
                Case "RN"
                    ret = "24"
                Case "RO"
                    ret = "11"
                Case "RR"
                    ret = "14"
                Case "RS"
                    ret = "43"
                Case "SC"
                    ret = "42"
                Case "SE"
                    ret = "28"
                Case "SP"
                    ret = "35"
                Case "TO"
                    ret = "17"
                Case Else
                    ret = ""
            End Select


        Catch ex As Exception
            Err.Clear()
            ret = ""
        End Try
        Return ret
    End Function


End Class
