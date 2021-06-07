Public Class FrmNovaPermissao

    Private Sub CarregarTipoPermissoes()
        Banco.CarregarCheckedListBox(Me.lstTipoPerm, "SELECT CODTIPOPERM AS AUTONUM,DESCRICAO AS DESCR FROM " & Banco.BancoSGIPA & "TB_SYS_TIPO_PERM ORDER BY CODTIPOPERM")
    End Sub

    Private Sub CarregarMenus()
        Banco.CarregarListBox(Me.lstMenus, "SELECT CODFUNC AS AUTONUM,CAPTIONOBJ AS DESCR FROM " & Banco.BancoSGIPA & "TB_SYS_FUNCOES WHERE UPPER(SISTEMA) = '" & My.Application.Info.ProductName.ToUpper() & "' ORDER BY CAPTIONOBJ")
    End Sub

    Private Sub FrmNovaPermissao_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarMenus()
        CarregarTipoPermissoes()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        For Each Item As DataRowView In Me.lstTipoPerm.CheckedItems
            If Convert.ToBoolean(Me.lstTipoPerm.GetItemCheckState(Me.lstTipoPerm.Items.IndexOf(Item))) Then
                If Convert.ToInt32(Banco.ExecuteScalar("SELECT COUNT(*) FROM " & Banco.BancoSGIPA & "TB_SYS_GRP_PERMISSOES WHERE CODGRUPO = " & Me.txtCodigoGrupo.Text & " AND CODFUNC = " & Me.lstMenus.SelectedValue.ToString() & " AND CODTIPOPERM = " & Item("AUTONUM").ToString())) = 0 Then
                    Banco.Execute("INSERT INTO " & Banco.BancoSGIPA & "TB_SYS_GRP_PERMISSOES (CODGRUPO,CODFUNC,CODTIPOPERM) VALUES (" & Me.txtCodigoGrupo.Text & "," & Me.lstMenus.SelectedValue.ToString() & "," & Item("AUTONUM").ToString() & ")")
                End If
            End If
        Next

        MessageBox.Show("Permissões concedidas!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Sub

    Private Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub btnMarcarTodos_Click(sender As Object, e As EventArgs) Handles btnMarcarTodos.Click

        For i As Integer = 0 To Me.lstTipoPerm.Items.Count - 1
            Me.lstTipoPerm.SetItemChecked(i, True)
        Next

    End Sub

End Class
