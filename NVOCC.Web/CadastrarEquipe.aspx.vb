Public Class CadastrarEquipe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnCadastrarLider_Click(sender As Object, e As EventArgs) Handles btnCadastrarLider.Click
        divErroMontarEquipe.Visible = False
        divSuccessMontarEquipe.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If ddlLider.SelectedValue = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um usuário para lider!"

        Else
            If txtIDLider.Text = "" Then
                'INSERT
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_EQUIPE_LIDER WHERE ID_USUARIO_LIDER = " & ddlLider.SelectedValue)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

                    Dim TaxaEquipe_final As String = txtTaxaEquipe.Text.ToString.Replace(".", "")
                    TaxaEquipe_final = TaxaEquipe_final.ToString.Replace(",", ".")

                    Dim TaxaLider_final As String = txtTaxaLider.Text.ToString.Replace(".", "")
                    TaxaLider_final = TaxaLider_final.ToString.Replace(",", ".")


                    Con.ExecutarQuery("INSERT INTO TB_EQUIPE_LIDER (ID_USUARIO_LIDER,NM_EQUIPE,TAXA_LIDER,TAXA_EQUIPE) VALUES (" & ddlLider.SelectedValue & ",'" & txtNomeEquipe.Text & "'," & TaxaLider_final & "," & TaxaEquipe_final & ")")
                    divSuccessMontarEquipe.Visible = True
                    lblSuccessMontarEquipe.Text = "Lider cadastrado com sucesso!"
                    gdvEquipeLider.DataBind()
                    divEquipe.Visible = True
                    txtIDLider.Text = ddlLider.SelectedValue
                    ddlLider.SelectedValue = txtIDLider.Text
                    ddlLider.Enabled = False
                    btnCadastrarLider.Visible = False
                    btnSalvarEdicao.Visible = True
                    ModalPopupExtender1.Show()
                Else
                    divErroMontarEquipe.Visible = True
                    lblErroMontarEquipe.Text = "Este lider já faz possui equipe!"
                    gdvEquipeLider.DataBind()
                    divEquipe.Visible = False
                    ModalPopupExtender1.Show()
                End If
            End If

        End If

    End Sub

    Private Sub btnFecharMontarEquipe_Click(sender As Object, e As EventArgs) Handles btnFecharMontarEquipe.Click
        txtIDEdicao.Text = ""
        txtIDLider.Text = ""
        ddlLider.SelectedValue = 0
        ddlLider.Enabled = True
        txtNomeEquipe.Text = ""
        txtNomeEquipe.Enabled = True
        txtTaxaLider.Text = ""
        txtTaxaLider.Enabled = True
        txtTaxaEquipe.Text = ""
        txtTaxaEquipe.Enabled = True
        txtNomeLider.Text = ""
        txtNomeMembro.Text = ""
        ddlMembro.SelectedValue = 0
        txtCodMembro.Text = ""
        divSuccessMontarEquipe.Visible = False
        divErroMontarEquipe.Visible = False
        divEquipe.Visible = False
        txtNomeLider.Enabled = True
        btnCadastrarLider.Visible = True
        btnSalvarEdicao.Visible = False
        gdvLideres.DataBind()
    End Sub

    Private Sub txtNomeLider_TextChanged(sender As Object, e As EventArgs) Handles txtNomeLider.TextChanged
        ModalPopupExtender1.Show()
    End Sub

    Private Sub txtNomeMembro_TextChanged(sender As Object, e As EventArgs) Handles txtNomeMembro.TextChanged
        ModalPopupExtender1.Show()
    End Sub

    Private Sub gdvLideres_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvLideres.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("DELETE FROM [dbo].[TB_EQUIPE_MEMBROS] WHERE ID_USUARIO_LIDER in ( SELECT ID_USUARIO_LIDER FROM [dbo].[TB_EQUIPE_LIDER] WHERE ID =" & ID & ")")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_EQUIPE_LIDER] WHERE ID =" & ID)
            Con.Fechar()
            gdvLideres.DataBind()
            lblSuccess.Text = "Registro deletado!"
            divSuccess.Visible = True

        ElseIf e.CommandName = "Editar" Then


            Dim ID As String = e.CommandArgument
            txtIDEdicao.Text = ID
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_USUARIO_LIDER,NM_EQUIPE,TAXA_LIDER,TAXA_EQUIPE FROM TB_EQUIPE_LIDER WHERE ID = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_USUARIO_LIDER")) Then
                    txtIDLider.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO_LIDER")
                    ddlLider.DataBind()
                    ddlLider.SelectedValue = ds.Tables(0).Rows(0).Item("ID_USUARIO_LIDER")
                    ddlLider.Enabled = False
                    txtNomeLider.Enabled = False
                    txtNomeEquipe.Text = ds.Tables(0).Rows(0).Item("NM_EQUIPE")
                    txtTaxaLider.Text = ds.Tables(0).Rows(0).Item("TAXA_LIDER")
                    txtTaxaEquipe.Text = ds.Tables(0).Rows(0).Item("TAXA_EQUIPE")
                    dsEquipeLider.DataBind()
                    gdvEquipeLider.DataBind()
                    ModalPopupExtender1.Show()
                    divEquipe.Visible = True
                    btnCadastrarLider.Visible = False
                    btnSalvarEdicao.Visible = True
                End If

            End If


        End If
        Con.Fechar()
    End Sub

    Private Sub gdvEquipeLider_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvEquipeLider.RowCommand
        divErroMontarEquipe.Visible = False
        divSuccessMontarEquipe.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("DELETE FROM [dbo].[TB_EQUIPE_MEMBROS] WHERE ID =" & ID)
            Con.Fechar()
            gdvEquipeLider.DataBind()
            ModalPopupExtender1.Show()
            lblSuccessMontarEquipe.Text = "Registro deletado!"
            divSuccessMontarEquipe.Visible = True
        End If
        Con.Fechar()
    End Sub

    Private Sub btnAdicionarMembro_Click(sender As Object, e As EventArgs) Handles btnAdicionarMembro.Click
        divErroMontarEquipe.Visible = False
        divSuccessMontarEquipe.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtIDLider.Text = "" Then
            divErroMontarEquipe.Visible = True
            lblErroMontarEquipe.Text = "Selecione um usuário para lider!"

        ElseIf ddlMembro.SelectedValue = 0 Then
            divErroMontarEquipe.Visible = True
            lblErroMontarEquipe.Text = "Selecione um usuário para adicionar à equipe!"
        Else

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_EQUIPE_MEMBROS WHERE ID_USUARIO_LIDER = " & ddlLider.SelectedValue & " AND ID_USUARIO_MEMBRO_EQUIPE = " & ddlMembro.SelectedValue)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                'INSERT
                Con.ExecutarQuery("INSERT INTO TB_EQUIPE_MEMBROS (ID_USUARIO_LIDER,ID_USUARIO_MEMBRO_EQUIPE) VALUES (" & ddlLider.SelectedValue & "," & ddlMembro.SelectedValue & ")")
                divSuccessMontarEquipe.Visible = True
                lblSuccessMontarEquipe.Text = "Membro da equipe cadastrado com sucesso!"
                gdvEquipeLider.DataBind()
                divEquipe.Visible = True
                ModalPopupExtender1.Show()
            Else
                divErroMontarEquipe.Visible = True
                lblErroMontarEquipe.Text = "Este usuario já faz parte desta equipe!"
                gdvEquipeLider.DataBind()
                divEquipe.Visible = True
                ModalPopupExtender1.Show()
            End If

        End If
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divErroMontarEquipe.Visible = False
        divSuccessMontarEquipe.Visible = False
        Dim sql As String = "select ID,ID_USUARIO,NOME,NM_EQUIPE from TB_USUARIO A INNER JOIN TB_EQUIPE_LIDER B ON A.ID_USUARIO = B.ID_USUARIO_LIDER WHERE NOME LIKE '%" & txtPesquisa.Text & "%' ORDER BY NOME"
        dsLideres.SelectCommand = sql
        gdvLideres.DataBind()
    End Sub

    Private Sub btnSalvarEdicao_Click(sender As Object, e As EventArgs) Handles btnSalvarEdicao.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        If txtIDEdicao.Text <> "" Then
            Dim TaxaEquipe_final As String = txtTaxaEquipe.Text.ToString.Replace(".", "")
            TaxaEquipe_final = TaxaEquipe_final.ToString.Replace(",", ".")

            Dim TaxaLider_final As String = txtTaxaLider.Text.ToString.Replace(".", "")
            TaxaLider_final = TaxaLider_final.ToString.Replace(",", ".")
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("UPDATE TB_EQUIPE_LIDER SET NM_EQUIPE = '" & txtNomeEquipe.Text & "',TAXA_LIDER = " & TaxaLider_final & ",TAXA_EQUIPE =" & TaxaEquipe_final & " WHERE ID = " & txtIDEdicao.Text)
            divSuccessMontarEquipe.Visible = True
            lblSuccessMontarEquipe.Text = "Atualizado com sucesso!"
            gdvEquipeLider.DataBind()
            divEquipe.Visible = True
            txtIDLider.Text = ddlLider.SelectedValue
            ddlLider.SelectedValue = txtIDLider.Text
            ddlLider.Enabled = False
            btnCadastrarLider.Visible = False
            btnSalvarEdicao.Visible = True
            ModalPopupExtender1.Show()
        End If
        Con.Fechar()
    End Sub
End Class