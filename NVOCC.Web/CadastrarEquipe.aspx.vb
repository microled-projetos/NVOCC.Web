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
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_INSIDE_EQUIPE WHERE ID_USUARIO_LIDER = " & ddlLider.SelectedValue)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

                    Dim TaxaEquipe_final As String = txtTaxaEquipe.Text.ToString.Replace(".", "")
                    TaxaEquipe_final = TaxaEquipe_final.ToString.Replace(",", ".")

                    Dim TaxaLider_final As String = txtTaxaLider.Text.ToString.Replace(".", "")
                    TaxaLider_final = TaxaLider_final.ToString.Replace(",", ".")


                    ds = Con.ExecutarQuery("INSERT INTO TB_INSIDE_EQUIPE (ID_USUARIO_LIDER,NM_EQUIPE,TAXA_LIDER,TAXA_EQUIPE) VALUES (" & ddlLider.SelectedValue & ",'" & txtNomeEquipe.Text & "'," & TaxaLider_final & "," & TaxaEquipe_final & " ) Select SCOPE_IDENTITY() As ID_EQUIPE ")
                    txtIDEquipe.Text = ds.Tables(0).Rows(0).Item("ID_EQUIPE")
                    divSuccessMontarEquipe.Visible = True
                    lblSuccessMontarEquipe.Text = "Lider cadastrado com sucesso!"
                    gdvMembrosEquipesCadastradas.DataBind()
                    divEquipe.Visible = True
                    txtIDLider.Text = ddlLider.SelectedValue
                    ddlLider.SelectedValue = txtIDLider.Text
                    ddlLider.Enabled = False
                    btnCadastrarLider.Visible = False
                    btnSalvarEdicao.Visible = True
                    mpeMontarEquipe.Show()
                Else
                    divErroMontarEquipe.Visible = True
                    lblErroMontarEquipe.Text = "Este lider já faz possui equipe!"
                    gdvMembrosEquipesCadastradas.DataBind()
                    divEquipe.Visible = False
                    mpeMontarEquipe.Show()
                End If
            End If

        End If

    End Sub

    Private Sub btnFecharMontarEquipe_Click(sender As Object, e As EventArgs) Handles btnFecharMontarEquipe.Click
        txtIDEquipe.Text = ""
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
        txtBuscaMembros.Text = ""
        ddlMembro.SelectedValue = 0
        txtCodMembro.Text = ""
        divSuccessMontarEquipe.Visible = False
        divErroMontarEquipe.Visible = False
        divEquipe.Visible = False
        txtNomeLider.Enabled = True
        btnCadastrarLider.Visible = True
        btnSalvarEdicao.Visible = False
        gdvEquipesCadastradas.DataBind()
    End Sub

    Private Sub txtNomeLider_TextChanged(sender As Object, e As EventArgs) Handles txtNomeLider.TextChanged
        mpeMontarEquipe.Show()
    End Sub

    Private Sub txtBuscaMembros_TextChanged(sender As Object, e As EventArgs) Handles txtBuscaMembros.TextChanged
        mpeMontarEquipe.Show()
    End Sub

    Private Sub gdvEquipesCadastradas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvEquipesCadastradas.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_TIME_MEMBROS] WHERE ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_INSIDE_EQUIPE_MEMBROS] WHERE ID_EQUIPE = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_TIME] WHERE  ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_INSIDE_EQUIPE_MEMBROS] WHERE ID_EQUIPE = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_EQUIPE_MEMBROS] WHERE ID_EQUIPE = " & ID)
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_EQUIPE] WHERE ID_EQUIPE =" & ID)
            Con.Fechar()
            gdvEquipesCadastradas.DataBind()
            lblSuccess.Text = "Registro deletado!"
            divSuccess.Visible = True

        ElseIf e.CommandName = "Editar" Then


            Dim ID As String = e.CommandArgument
            txtIDEquipe.Text = ID
            Dim ds As DataSet = Con.ExecutarQuery("Select ID_USUARIO_LIDER,NM_EQUIPE,TAXA_LIDER,TAXA_EQUIPE FROM TB_INSIDE_EQUIPE WHERE ID_EQUIPE = " & ID)
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
                    dsMembrosEquipesCadastradas.DataBind()
                    gdvMembrosEquipesCadastradas.DataBind()
                    mpeMontarEquipe.Show()
                    divEquipe.Visible = True
                    btnCadastrarLider.Visible = False
                    btnSalvarEdicao.Visible = True
                End If

            End If


        End If
        Con.Fechar()
    End Sub

    Private Sub gdvMembrosEquipesCadastradas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvMembrosEquipesCadastradas.RowCommand
        divErroMontarEquipe.Visible = False
        divSuccessMontarEquipe.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_TIME_MEMBROS] WHERE ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_INSIDE_EQUIPE_MEMBROS] WHERE ID_EQUIPE_MEMBROS = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_TIME] WHERE ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_INSIDE_EQUIPE_MEMBROS] WHERE ID_EQUIPE_MEMBROS = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_EQUIPE_MEMBROS] WHERE ID_EQUIPE_MEMBROS = " & ID)
            Con.Fechar()
            gdvMembrosEquipesCadastradas.DataBind()
            mpeMontarEquipe.Show()
            lblSuccessMontarEquipe.Text = "Registro deletado!"
            divSuccessMontarEquipe.Visible = True

        ElseIf e.CommandName = "Editar" Then
            txtIDTime.Text = e.CommandArgument
            dgvMembrosTime.DataBind()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_EQUIPE, NM_TIME,QTD_MEMBROS FROM TB_INSIDE_EQUIPE_MEMBROS A
INNER JOIN TB_INSIDE_TIME B ON A.ID_TIME = B.ID_TIME
WHERE A.ID_TIME = " & txtIDTime.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                txtIDEquipe.Text = ds.Tables(0).Rows(0).Item("ID_EQUIPE")
                txtNomeTime.Text = ds.Tables(0).Rows(0).Item("NM_TIME")
                txtQtdMembrosTime.Text = ds.Tables(0).Rows(0).Item("QTD_MEMBROS")
                txtCodMembrosTime.Text = ""
                txtBuscaMembrosTime.Text = ""
                divMembroTime.Visible = True
            End If
            mpeMontarEquipe.Show()
            mpeMontarTime.Show()
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

            ds = Con.ExecutarQuery("Select COUNT(*)QTD FROM TB_INSIDE_EQUIPE_MEMBROS WHERE ID_EQUIPE = " & txtIDEquipe.Text & " AND ID_USUARIO_MEMBRO_EQUIPE = " & ddlMembro.SelectedValue)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                'INSERT
                Con.ExecutarQuery("INSERT INTO TB_INSIDE_EQUIPE_MEMBROS (ID_EQUIPE,ID_USUARIO_MEMBRO_EQUIPE) VALUES (" & txtIDEquipe.Text & "," & ddlMembro.SelectedValue & ")")
                divSuccessMontarEquipe.Visible = True
                lblSuccessMontarEquipe.Text = "Membro da equipe cadastrado com sucesso!"
                gdvMembrosEquipesCadastradas.DataBind()
                divEquipe.Visible = True
                mpeMontarEquipe.Show()
            Else
                divErroMontarEquipe.Visible = True
                lblErroMontarEquipe.Text = "Este usuario já faz parte desta equipe!"
                gdvMembrosEquipesCadastradas.DataBind()
                divEquipe.Visible = True
                mpeMontarEquipe.Show()
            End If

            txtCodMembro.Text = ""
            txtBuscaMembros.Text = ""
            ddlMembro.SelectedValue = 0
        End If
    End Sub

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divErroMontarEquipe.Visible = False
        divSuccessMontarEquipe.Visible = False
        Dim sql As String = "SELECT ID_EQUIPE,ID_USUARIO,NOME,NM_EQUIPE,TAXA_LIDER,TAXA_EQUIPE  FROM TB_USUARIO A INNER JOIN TB_INSIDE_EQUIPE B ON A.ID_USUARIO = B.ID_USUARIO_LIDER WHERE NOME LIKE '%" & txtPesquisa.Text & "%' ORDER BY NOME"
        dsEquipesCadastradas.SelectCommand = sql
        gdvEquipesCadastradas.DataBind()
    End Sub

    Private Sub btnSalvarEdicao_Click(sender As Object, e As EventArgs) Handles btnSalvarEdicao.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        If txtIDEquipe.Text <> "" Then
            Dim TaxaEquipe_final As String = txtTaxaEquipe.Text.ToString.Replace(".", "")
            TaxaEquipe_final = TaxaEquipe_final.ToString.Replace(",", ".")

            Dim TaxaLider_final As String = txtTaxaLider.Text.ToString.Replace(".", "")
            TaxaLider_final = TaxaLider_final.ToString.Replace(",", ".")
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("UPDATE TB_INSIDE_EQUIPE SET NM_EQUIPE = '" & txtNomeEquipe.Text & "',TAXA_LIDER = " & TaxaLider_final & ",TAXA_EQUIPE =" & TaxaEquipe_final & " WHERE ID_EQUIPE = " & txtIDEquipe.Text)
            divSuccessMontarEquipe.Visible = True
            lblSuccessMontarEquipe.Text = "Atualizado com sucesso!"
            gdvMembrosEquipesCadastradas.DataBind()
            divEquipe.Visible = True
            txtIDLider.Text = ddlLider.SelectedValue
            ddlLider.SelectedValue = txtIDLider.Text
            ddlLider.Enabled = False
            btnCadastrarLider.Visible = False
            btnSalvarEdicao.Visible = True
            mpeMontarEquipe.Show()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnAdicionarTime_Click(sender As Object, e As EventArgs) Handles btnAdicionarTime.Click
        mpeMontarEquipe.Show()
        mpeMontarTime.Show()
    End Sub

    Private Sub btnSalvarTime_Click(sender As Object, e As EventArgs) Handles btnSalvarTime.Click
        divTimeErro.Visible = False
        divTimeSuccess.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If ddlLider.SelectedValue = 0 Then
            divTimeErro.Visible = True
            lblErroTime.Text = "Selecione um usuário para lider para equipe!"

        ElseIf txtNomeTime.Text = "" Then
            divTimeErro.Visible = True
            lblErroTime.Text = "Informe o nome do time!"

        ElseIf txtQtdMembrosTime.Text = "" Or txtQtdMembrosTime.Text = 0 Then
            divTimeErro.Visible = True
            lblErroTime.Text = "A quantidade de membros deve ser maior que zero"

        ElseIf txtIDEquipe.Text = "" Then

            divTimeErro.Visible = True
            lblErroTime.Text = "Necessário cadastrar equipe antes do time!"

        Else


            If txtIDTime.Text <> "" Then
                'UPDATE
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_INSIDE_TIME WHERE NM_TIME = '" & txtNomeTime.Text & "' AND QTD_MEMBROS = " & txtQtdMembrosTime.Text & " AND ID_TIME <> " & txtIDTime.Text)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    Con.ExecutarQuery("UPDATE TB_INSIDE_TIME SET NM_TIME = '" & txtNomeTime.Text & "' , QTD_MEMBROS = " & txtQtdMembrosTime.Text & " WHERE ID_TIME = " & txtIDTime.Text)
                    divTimeSuccess.Visible = True
                    lblSuccessTime.Text = "Time atualizado com sucesso!"
                    divMembroTime.Visible = True
                Else
                    divTimeErro.Visible = True
                    lblErroTime.Text = "Este time já existe!"
                    divMembroTime.Visible = False
                End If

            Else
                'INSERT
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_INSIDE_TIME WHERE NM_TIME = '" & txtNomeTime.Text & "' AND QTD_MEMBROS = " & txtQtdMembrosTime.Text)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    ds = Con.ExecutarQuery("INSERT INTO TB_INSIDE_TIME (NM_TIME,QTD_MEMBROS) VALUES ( '" & txtNomeTime.Text & "' , " & txtQtdMembrosTime.Text & " ) Select SCOPE_IDENTITY() as ID_TIME ")
                    txtIDTime.Text = ds.Tables(0).Rows(0).Item("ID_TIME")
                    Con.ExecutarQuery("INSERT INTO TB_INSIDE_EQUIPE_MEMBROS (ID_EQUIPE,ID_TIME) VALUES (" & txtIDEquipe.Text & " , " & txtIDTime.Text & " ) ")
                    divTimeSuccess.Visible = True
                    lblSuccessTime.Text = "Time cadastrado com sucesso!"
                    divMembroTime.Visible = True
                Else
                    divTimeErro.Visible = True
                    lblErroTime.Text = "Este time já existe!"
                    divMembroTime.Visible = False
                End If
            End If

        End If
        mpeMontarEquipe.Show()
        mpeMontarTime.Show()
    End Sub

    Private Sub btnAdicionarMembroTime_Click(sender As Object, e As EventArgs) Handles btnAdicionarMembroTime.Click
        divTimeErro.Visible = False
        divTimeSuccess.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtIDLider.Text = "" Then
            divMembroTime.Visible = True
            lblErroTime.Text = "Selecione um usuário para lider!"

        ElseIf txtIDTime.Text = "" Then
            divMembroTime.Visible = True
            lblErroTime.Text = "Necessario informar dados basicos do time antes de adicionar membros!"

        ElseIf ddlMembroTime.SelectedValue = 0 Then
            divMembroTime.Visible = True
            lblErroTime.Text = "Selecione um usuário para adicionar à equipe!"
        Else

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_INSIDE_TIME_MEMBROS WHERE ID_TIME = " & txtIDTime.Text & " AND ID_USUARIO_MEMBRO_TIME = " & ddlMembroTime.SelectedValue)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                'INSERT
                Con.ExecutarQuery("INSERT INTO TB_INSIDE_TIME_MEMBROS (ID_TIME,ID_USUARIO_MEMBRO_TIME) VALUES (" & txtIDTime.Text & "," & ddlMembroTime.SelectedValue & ")")
                divTimeSuccess.Visible = True
                lblSuccessTime.Text = "Membro do time cadastrado com sucesso!"
                dgvMembrosTime.DataBind()
            Else
                divTimeErro.Visible = True
                lblErroTime.Text = "Este usuario já faz parte desta time!"
                dgvMembrosTime.DataBind()
            End If

            txtCodMembrosTime.Text = ""
            txtBuscaMembrosTime.Text = ""
            ddlMembroTime.SelectedValue = 0
        End If
        mpeMontarEquipe.Show()
        mpeMontarTime.Show()
    End Sub

    Private Sub txtBuscaMembrosTime_TextChanged(sender As Object, e As EventArgs) Handles txtBuscaMembrosTime.TextChanged
        mpeMontarEquipe.Show()
        mpeMontarTime.Show()
    End Sub

    Private Sub btnFecharMontaTime_Click(sender As Object, e As EventArgs) Handles btnFecharMontaTime.Click
        txtIDTime.Text = ""
        txtNomeTime.Text = ""
        txtQtdMembrosTime.Text = ""
        txtCodMembrosTime.Text = ""
        txtBuscaMembrosTime.Text = ""
        ddlMembroTime.SelectedValue = 0
        divTimeErro.Visible = False
        divTimeSuccess.Visible = False
        divMembroTime.Visible = False
        dgvMembrosTime.DataBind()
        gdvMembrosEquipesCadastradas.DataBind()
        mpeMontarTime.Hide()
        mpeMontarEquipe.Show()
    End Sub

    Private Sub dgvMembrosTime_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvMembrosTime.RowCommand
        divTimeSuccess.Visible = False
        divTimeErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("DELETE FROM [dbo].[TB_INSIDE_TIME_MEMBROS] WHERE ID_TIME_MEMBROS  = " & ID)
            Con.Fechar()
            dgvMembrosTime.DataBind()
            lblSuccessTime.Text = "Registro deletado!"
            divTimeSuccess.Visible = True
        End If
        Con.Fechar()
        mpeMontarEquipe.Show()
        mpeMontarTime.Show()
    End Sub
End Class