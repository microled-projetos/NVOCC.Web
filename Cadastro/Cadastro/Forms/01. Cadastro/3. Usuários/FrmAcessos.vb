Imports System.ComponentModel
Imports System.Text

Public Class FrmAcessos

    Private _acessar As Boolean = False
    Private _editar As Boolean = False
    Private _excluir As Boolean = False
    Private _imprimir As Boolean = False
    Private _incluir As Boolean = False

    Private Sub ConsultarPermissoes(ByVal CodGrupo As String)

        Dim PermissoesFuncoes(5) As String

        PermissoesFuncoes(1) = "ACESSAR"
        PermissoesFuncoes(2) = "EDITAR"
        PermissoesFuncoes(3) = "EXCLUIR"
        PermissoesFuncoes(4) = "IMPRIMIR"
        PermissoesFuncoes(5) = "INCLUIR"

        Dim SQL As New StringBuilder

        SQL.Append("SELECT DISTINCT C.CAPTIONOBJ, ")

        For i = 1 To 5
            SQL.Append(" (SELECT COUNT (*) DESCRICAO FROM TB_SYS_TIPO_PERM A ")
            SQL.Append("   LEFT JOIN TB_SYS_GRP_PERMISSOES B ON B.CODTIPOPERM = A.CODTIPOPERM ")
            SQL.Append("  WHERE B.CODFUNC = C.CODFUNC AND A.CODTIPOPERM = " & i & " AND B.CODGRUPO=CODGRUPO) " & PermissoesFuncoes(i) & ", ")
        Next

        SQL.Append(" B.CODFUNC ")
        SQL.Append("  FROM  ")
        SQL.Append("	" & Banco.BancoSGIPA & "TB_SYS_GRUPO_USER A ")
        SQL.Append("LEFT JOIN  ")
        SQL.Append("	" & Banco.BancoSGIPA & "TB_SYS_GRP_PERMISSOES B ON B.CodGrupo = A.CodGrupo ")
        SQL.Append("LEFT JOIN  ")
        SQL.Append("	" & Banco.BancoSGIPA & "TB_SYS_FUNCOES C ON B.CodFunc = C.CodFunc ")
        SQL.Append("LEFT JOIN  ")
        SQL.Append("	" & Banco.BancoSGIPA & "TB_SYS_TIPO_PERM D ON D.CODTIPOPERM = B.CODTIPOPERM ")

        SQL.Append(" WHERE ")
        SQL.Append("       C.SISTEMA = '" & My.Application.Info.ProductName.ToUpper() & "' AND A.CODGRUPO = " & CodGrupo)

        Me.dgvConsulta.DataSource = Banco.List(SQL.ToString())

    End Sub

    Private Sub ConsultarGrupos()
        Me.cbGrupos.DataSource = Banco.List("SELECT CODGRUPO,DESCRICAO FROM " & Banco.BancoSGIPA & "TB_SYS_GRUPO_USER ORDER BY DESCRICAO")
        Me.cbGrupos.SelectedIndex = -1
    End Sub

    Private Sub SetaControles()

        btnSalvar.Enabled = Not (btnSalvar.Enabled)
        dgvConsulta.Enabled = Not (dgvConsulta.Enabled)
        pnControles.Enabled = Not (pnControles.Enabled)

    End Sub

    Private Sub FrmPrincipal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ConsultarGrupos()
        FundoTextBox(Me)

        Me.cbGrupos.SelectedValue = 1

        ConsultarPermissoes(Val(Me.cbGrupos.SelectedValue))

    End Sub

    Private Sub btnNovo_Click(sender As System.Object, e As System.EventArgs)

        LimparCampos(Me)
        HabilitarCampos(Me, True)
        SetaControles()

    End Sub

    Private Sub dgvConsulta_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvConsulta.CellClick

        If Me.dgvConsulta.Rows.Count > 0 Then
            MostraDados()
        End If

    End Sub

    Private Sub MostraDados()

        If Me.dgvConsulta.Rows.Count > 0 Then

            Me.txtCodigo.Text = Me.dgvConsulta.CurrentRow.Cells(0).Value.ToString()

            _acessar = Me.dgvConsulta.CurrentRow.Cells("colAcessar").Value.ToString()
            _editar = Me.dgvConsulta.CurrentRow.Cells("colEditar").Value.ToString()
            _excluir = Me.dgvConsulta.CurrentRow.Cells("colExcluir").Value.ToString()
            _imprimir = Me.dgvConsulta.CurrentRow.Cells("colImprimir").Value.ToString()
            _incluir = Me.dgvConsulta.CurrentRow.Cells("colIncluir").Value.ToString()

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As System.Object, e As System.EventArgs) Handles btnSalvar.Click

        For i As Integer = 0 To dgvConsulta.SelectedRows.Count - 1
            If Val(dgvConsulta.Rows(i).Cells(6).Value.ToString()) > 0 Then
                Banco.Execute("DELETE FROM " & Banco.BancoSGIPA & "TB_SYS_GRP_PERMISSOES WHERE CODFUNC = " & dgvConsulta.Rows(i).Cells(6).Value.ToString() & " AND CODGRUPO = " & cbGrupos.SelectedValue.ToString())
                For j As Integer = 1 To 5
                    If Val(dgvConsulta.Rows(i).Cells(j).Value.ToString()) > 0 Then
                        Banco.Execute("INSERT INTO " & Banco.BancoSGIPA & "TB_SYS_GRP_PERMISSOES (CODFUNC,CODGRUPO,CODTIPOPERM) VALUES (" & dgvConsulta.Rows(i).Cells(6).Value.ToString() & "," & cbGrupos.SelectedValue.ToString() & "," & j & ")")
                    End If
                Next
            End If
        Next

        MessageBox.Show("Permissões concedidas com sucesso!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs)
        SetaControles()
        LimparCampos(Me)
        HabilitarCampos(Me, False)
    End Sub

    Private Sub FrmAgencias_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub

    Private Sub FrmAgencias_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub cbGrupos_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbGrupos.SelectedIndexChanged

        If cbGrupos.Items.Count > 0 Then
            If cbGrupos.SelectedValue IsNot Nothing Then
                ConsultarPermissoes(cbGrupos.SelectedValue.ToString())
            End If
        End If

    End Sub

    Private Sub dgvConsulta_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConsulta.CellContentClick

        Try

            Dim Linha As Integer = e.RowIndex
            Dim Coluna As Integer = e.ColumnIndex

            If e.ColumnIndex = 1 Then

                If TypeOf dgvConsulta.CurrentCell Is System.Windows.Forms.DataGridViewCheckBoxCell Then

                    Dim Celula As DataGridViewCell = dgvConsulta.CurrentCell

                    Dim CelulaCheck As DataGridViewCheckBoxCell = DirectCast(dgvConsulta.Rows(Linha).Cells(Coluna), DataGridViewCheckBoxCell)

                    Dim NovoValor As Boolean = CBool(CelulaCheck.EditingCellFormattedValue)

                    For i As Integer = 2 To 5
                        Me.dgvConsulta.Rows(Linha).Cells(i).Value = NovoValor
                    Next

                End If

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnNovaPermissao_Click(sender As Object, e As EventArgs) Handles btnNovaPermissao.Click

        Using oFrm As New FrmNovaPermissao
            oFrm.txtCodigoGrupo.Text = Me.cbGrupos.SelectedValue.ToString()
            oFrm.ShowDialog()
            ConsultarPermissoes(Me.cbGrupos.SelectedValue.ToString())
        End Using

    End Sub

End Class
