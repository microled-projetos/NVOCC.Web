Public Class CadastrarEquipeVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnFecharMontarEquipe_Click(sender As Object, e As EventArgs) Handles btnFecharMontarEquipe.Click
        txtIDEquipe.Text = ""

        txtNomeEquipe.Text = ""
        txtNomeEquipe.Enabled = True

        txtBuscaMembrosEquipe.Text = ""
        ddlBuscaMembrosEquipe.SelectedValue = 0
        txtCodMembroEquipe.Text = ""
        divSuccessMontarEquipe.Visible = False
        divErroMontarEquipe.Visible = False
        divMembrosEquipe.Visible = False
        txtBuscaMembrosTime.Text = ""
        ddlBuscaMembrosTime.SelectedValue = 0
        txtCodMembrosTime.Text = ""
        btnAddTime.Visible = False
        gdvEquipesCadastradas.DataBind()
    End Sub

    Private Sub txtBuscaMembrosEquipe_TextChanged(sender As Object, e As EventArgs) Handles txtBuscaMembrosEquipe.TextChanged
        mpeMontarEquipe.Show()
    End Sub

    Private Sub gdvEquipesCadastradas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvEquipesCadastradas.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_TIME_MEMBROS] WHERE ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_VENDEDOR_EQUIPE_MEMBROS] WHERE ID_EQUIPE = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_TIME] WHERE  ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_VENDEDOR_EQUIPE_MEMBROS] WHERE ID_EQUIPE = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_EQUIPE_MEMBROS] WHERE ID_EQUIPE = " & ID)
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_EQUIPE] WHERE ID_EQUIPE =" & ID)
            Con.Fechar()
            gdvEquipesCadastradas.DataBind()
            lblSuccess.Text = "Registro deletado!"
            divSuccess.Visible = True
            btnSalvarTime.Visible = True
        ElseIf e.CommandName = "Editar" Then

            Dim ID As String = e.CommandArgument
            txtIDEquipe.Text = ID
            Dim ds As DataSet = Con.ExecutarQuery("Select ID_EQUIPE,NM_EQUIPE FROM TB_VENDEDOR_EQUIPE WHERE ID_EQUIPE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_EQUIPE")) Then
                    txtIDEquipe.Text = ds.Tables(0).Rows(0).Item("ID_EQUIPE")
                    txtNomeEquipe.Text = ds.Tables(0).Rows(0).Item("NM_EQUIPE")
                    dsMembrosEquipesCadastradas.DataBind()
                    gdvMembrosEquipesCadastradas.DataBind()
                    mpeMontarEquipe.Show()
                    divMembrosEquipe.Visible = True
                    btnSalvarEquipe.Visible = True
                    btnAddTime.Visible = True
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
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_TIME_MEMBROS] WHERE ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_VENDEDOR_EQUIPE_MEMBROS] WHERE ID_EQUIPE_MEMBROS = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_TIME] WHERE ID_TIME IN (SELECT ID_TIME FROM [dbo].[TB_VENDEDOR_EQUIPE_MEMBROS] WHERE ID_EQUIPE_MEMBROS = " & ID & " AND ID_TIME IS NOT NULL) ")
            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_EQUIPE_MEMBROS] WHERE ID_EQUIPE_MEMBROS = " & ID)
            Con.Fechar()
            gdvMembrosEquipesCadastradas.DataBind()
            mpeMontarEquipe.Show()
            lblSuccessMontarEquipe.Text = "Registro deletado!"
            divSuccessMontarEquipe.Visible = True

        ElseIf e.CommandName = "Editar" Then
            txtIDTime.Text = e.CommandArgument
            dgvMembrosTime.DataBind()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_EQUIPE, NM_TIME,QTD_MEMBROS FROM TB_VENDEDOR_EQUIPE_MEMBROS A
INNER JOIN TB_VENDEDOR_TIME B ON A.ID_TIME = B.ID_TIME
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

    Private Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divErroMontarEquipe.Visible = False
        divSuccessMontarEquipe.Visible = False
        Dim sql As String = "SELECT ID_EQUIPE,NM_EQUIPE FROM TB_INSIDE_EQUIPE WHERE NM_EQUIPE LIKE '%" & txtPesquisa.Text & "%' ORDER BY NM_EQUIPE"
        dsEquipesCadastradas.SelectCommand = sql
        gdvEquipesCadastradas.DataBind()
    End Sub

    Private Sub btnSalvarEquipe_Click(sender As Object, e As EventArgs) Handles btnSalvarEquipe.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        If txtIDEquipe.Text <> "" Then
            'ALTERA

            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("UPDATE TB_VENDEDOR_EQUIPE SET NM_EQUIPE = '" & txtNomeEquipe.Text & "' WHERE ID_EQUIPE = " & txtIDEquipe.Text)
            divSuccessMontarEquipe.Visible = True
            Con.Fechar()
            lblSuccessMontarEquipe.Text = "Atualizado com sucesso!"
            gdvMembrosEquipesCadastradas.DataBind()

            divMembrosEquipe.Visible = True
            btnSalvarEquipe.Visible = True
            btnAddTime.Visible = True
            mpeMontarEquipe.Show()
        Else
            'INCLUI
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_VENDEDOR_EQUIPE (NM_EQUIPE) VALUES ( '" & txtNomeEquipe.Text & "') Select SCOPE_IDENTITY() As ID_EQUIPE ")
            txtIDEquipe.Text = ds.Tables(0).Rows(0).Item("ID_EQUIPE")
            lblSuccessMontarEquipe.Text = "Equipe cadastrada com sucesso!"
            Con.Fechar()
            gdvMembrosEquipesCadastradas.DataBind()

            divMembrosEquipe.Visible = True
            btnSalvarEquipe.Visible = True
            btnAddTime.Visible = True
            mpeMontarEquipe.Show()
        End If
    End Sub
    Private Sub btnAddTime_Click(sender As Object, e As EventArgs) Handles btnAddTime.Click
        mpeMontarTime.Show()
    End Sub

    Private Sub btnAddMembroEquipe_Click(sender As Object, e As EventArgs) Handles btnAddMembroEquipe.Click
        divSuccessMontarEquipe.Visible = False
        divErroMontarEquipe.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtIDEquipe.Text = "" Then
            divErroMontarEquipe.Visible = True
            lblErroMontarEquipe.Text = "Necessario informar dados basicos da equipe antes de adicionar membros!"

        ElseIf ddlBuscaMembrosEquipe.SelectedValue = 0 Then
            divErroMontarEquipe.Visible = True
            lblErroMontarEquipe.Text = "Selecione um usuário para adicionar à equipe!"
        Else

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_VENDEDOR_EQUIPE_MEMBROS WHERE ID_EQUIPE = " & txtIDEquipe.Text & " AND ID_USUARIO_MEMBRO_EQUIPE = " & ddlBuscaMembrosEquipe.SelectedValue)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                'INSERT
                Con.ExecutarQuery("INSERT INTO TB_VENDEDOR_EQUIPE_MEMBROS (ID_EQUIPE,ID_USUARIO_MEMBRO_EQUIPE) VALUES (" & txtIDEquipe.Text & "," & ddlBuscaMembrosEquipe.SelectedValue & ")")
                divSuccessMontarEquipe.Visible = True
                lblSuccessMontarEquipe.Text = "Membro da equipe cadastrado com sucesso!"
                gdvMembrosEquipesCadastradas.DataBind()
            Else
                divErroMontarEquipe.Visible = True
                lblErroMontarEquipe.Text = "Este usuario já faz parte desta equipe!"
                gdvMembrosEquipesCadastradas.DataBind()
            End If

            txtBuscaMembrosEquipe.Text = ""
            ddlBuscaMembrosEquipe.SelectedValue = 0
        End If
        mpeMontarEquipe.Show()
    End Sub
    Private Sub btnSalvarTime_Click(sender As Object, e As EventArgs) Handles btnSalvarTime.Click
        divTimeErro.Visible = False
        divTimeSuccess.Visible = False
        Dim ds As DataSet
        Dim Con As New Conexao_sql
        Con.Conectar()

        If txtNomeTime.Text = "" Then
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
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_VENDEDOR_TIME WHERE NM_TIME = '" & txtNomeTime.Text & "' AND QTD_MEMBROS = " & txtQtdMembrosTime.Text & " AND ID_TIME <> " & txtIDTime.Text)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    Con.ExecutarQuery("UPDATE TB_VENDEDOR_TIME SET NM_TIME = '" & txtNomeTime.Text & "' , QTD_MEMBROS = " & txtQtdMembrosTime.Text & " WHERE ID_TIME = " & txtIDTime.Text)
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
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_VENDEDOR_TIME WHERE NM_TIME = '" & txtNomeTime.Text & "' AND QTD_MEMBROS = " & txtQtdMembrosTime.Text)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    ds = Con.ExecutarQuery("INSERT INTO TB_VENDEDOR_TIME (NM_TIME,QTD_MEMBROS) VALUES ( '" & txtNomeTime.Text & "' , " & txtQtdMembrosTime.Text & " ) Select SCOPE_IDENTITY() as ID_TIME ")
                    txtIDTime.Text = ds.Tables(0).Rows(0).Item("ID_TIME")
                    Con.ExecutarQuery("INSERT INTO TB_VENDEDOR_EQUIPE_MEMBROS (ID_EQUIPE,ID_TIME) VALUES (" & txtIDEquipe.Text & " , " & txtIDTime.Text & " ) ")
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

        If txtIDTime.Text = "" Then
            divMembroTime.Visible = True
            lblErroTime.Text = "Necessario informar dados basicos do time antes de adicionar membros!"

        ElseIf ddlBuscaMembrosTime.SelectedValue = 0 Then
            divMembroTime.Visible = True
            lblErroTime.Text = "Selecione um usuário para adicionar à equipe!"
        Else

            ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_VENDEDOR_TIME_MEMBROS WHERE ID_TIME = " & txtIDTime.Text & " AND ID_USUARIO_MEMBRO_TIME = " & ddlBuscaMembrosTime.SelectedValue)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                'INSERT
                Con.ExecutarQuery("INSERT INTO TB_VENDEDOR_TIME_MEMBROS (ID_TIME,ID_USUARIO_MEMBRO_TIME) VALUES (" & txtIDTime.Text & "," & ddlBuscaMembrosTime.SelectedValue & ")")
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
            ddlBuscaMembrosTime.SelectedValue = 0
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
        ddlBuscaMembrosTime.SelectedValue = 0
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

            Con.ExecutarQuery("DELETE FROM [dbo].[TB_VENDEDOR_TIME_MEMBROS] WHERE ID_TIME_MEMBROS  = " & ID)
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