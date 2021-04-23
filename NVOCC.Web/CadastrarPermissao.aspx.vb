Public Class CadastrarPermissao
    Inherits System.Web.UI.Page

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        msgSucesso.Visible = False
        msgerro.Visible = False
        Dim Con As New Conexao_sql
        Dim grupo As Integer = cbGrupo.SelectedValue
        Con.Conectar()

        If Session("ID_TIPO_USUARIO") = 1 Then

            For Each linha As GridViewRow In dgvMenus.Rows
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(*)Qtd FROM [TB_GRUPO_PERMISSAO] where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)

                If ds.Tables(0).Rows(0).Item("Qtd") = 0 Then

                    ID = CType(linha.FindControl("lblID"), Label).Text

                    Con.ExecutarQuery("INSERT INTO [dbo].[TB_GRUPO_PERMISSAO](ID_TIPO_USUARIO, ID_MENU, DT_CADASTRO) VALUES (" & grupo & "," & ID & ",GetDate())")
                End If


                Dim check As CheckBox = linha.FindControl("Acessar")
                If check.Checked Then

                    ID = CType(linha.FindControl("lblID"), Label).Text

                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Acessar] = 1  where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)

                Else
                    ID = CType(linha.FindControl("lblID"), Label).Text

                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Acessar] = 0 where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)


                End If

                check = linha.FindControl("Cadastrar")
                If check.Checked Then

                    ID = CType(linha.FindControl("lblID"), Label).Text

                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Cadastrar] = 1  , [FL_Acessar] = 1 where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)

                Else
                    ID = CType(linha.FindControl("lblID"), Label).Text

                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Cadastrar] = 0 where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)

                End If

                check = linha.FindControl("Atualizar")
                If check.Checked Then

                    ID = CType(linha.FindControl("lblID"), Label).Text

                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Atualizar] = 1  , [FL_Acessar] = 1 where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)
                Else
                    ID = CType(linha.FindControl("lblID"), Label).Text
                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Atualizar] = 0 where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)

                End If

                check = linha.FindControl("Excluir")
                If check.Checked Then

                    ID = CType(linha.FindControl("lblID"), Label).Text

                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Excluir] = 1 , [FL_Acessar] = 1 where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)
                Else
                    ID = CType(linha.FindControl("lblID"), Label).Text
                    Con.ExecutarQuery("UPDATE [dbo].[TB_GRUPO_PERMISSAO] SET  [FL_Excluir] = 0 where ID_Menu =" & ID & " AND ID_TIPO_USUARIO  = " & grupo)

                End If
            Next
            Con.Fechar()
            msgSucesso.Visible = True
            dgvMenus.DataBind()
        Else
            msgerro.Visible = True

        End If
    End Sub

    Private Sub cbGrupo_Load(sender As Object, e As EventArgs) Handles cbGrupo.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        msgSucesso.Visible = False

        If Session("ID_TIPO_USUARIO") <> 1 Then
            btnGravar.Enabled = False
            btnSelecionarTodos.Enabled = False
        End If
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 3 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

                Response.Redirect("Default.aspx")

            End If


        Else
            Response.Redirect("Default.aspx")
        End If
        Con.Fechar()
    End Sub

    Private Sub btnSelecionarTodos_Click(sender As Object, e As EventArgs) Handles btnSelecionarTodos.Click
        If btnSelecionarTodos.Text = "Selecionar Todos" Then
            For i As Integer = 0 To Me.dgvMenus.Rows.Count - 1
                Dim chkAcessar = CType(Me.dgvMenus.Rows(i).FindControl("Acessar"), CheckBox)
                Dim chkCadastrar = CType(Me.dgvMenus.Rows(i).FindControl("Cadastrar"), CheckBox)
                Dim chkAtualizar = CType(Me.dgvMenus.Rows(i).FindControl("Atualizar"), CheckBox)
                Dim chkExcluir = CType(Me.dgvMenus.Rows(i).FindControl("Excluir"), CheckBox)
                chkAcessar.Checked = True
                chkCadastrar.Checked = True
                chkAtualizar.Checked = True
                chkExcluir.Checked = True
            Next
            btnSelecionarTodos.Text = "Retirar Seleções"
        Else
            For i As Integer = 0 To Me.dgvMenus.Rows.Count - 1
                Dim chkAcessar = CType(Me.dgvMenus.Rows(i).FindControl("Acessar"), CheckBox)
                Dim chkCadastrar = CType(Me.dgvMenus.Rows(i).FindControl("Cadastrar"), CheckBox)
                Dim chkAtualizar = CType(Me.dgvMenus.Rows(i).FindControl("Atualizar"), CheckBox)
                Dim chkExcluir = CType(Me.dgvMenus.Rows(i).FindControl("Excluir"), CheckBox)
                chkAcessar.Checked = False
                chkCadastrar.Checked = False
                chkAtualizar.Checked = False
                chkExcluir.Checked = False
            Next
            btnSelecionarTodos.Text = "Selecionar Todos"

        End If


    End Sub

    Private Sub dgvMenus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgvMenus.SelectedIndexChanged
        For i As Integer = 0 To Me.dgvMenus.Rows.Count - 1
            Dim chkAcessar = CType(Me.dgvMenus.Rows(i).FindControl("Acessar"), CheckBox)
            Dim chkCadastrar = CType(Me.dgvMenus.Rows(i).FindControl("Cadastrar"), CheckBox)
            Dim chkAtualizar = CType(Me.dgvMenus.Rows(i).FindControl("Atualizar"), CheckBox)
            Dim chkExcluir = CType(Me.dgvMenus.Rows(i).FindControl("Excluir"), CheckBox)
            If chkCadastrar.Checked = True Then
                chkAcessar.Checked = True
            End If
            If chkCadastrar.Checked = True Then
                chkAcessar.Checked = True
            End If
            If chkCadastrar.Checked = True Then
                chkAcessar.Checked = True
            End If
        Next
    End Sub

    Private Sub dgvMenus_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dgvMenus.SelectedIndexChanging
        For i As Integer = 0 To Me.dgvMenus.Rows.Count - 1
            Dim chkAcessar = CType(Me.dgvMenus.Rows(i).FindControl("Acessar"), CheckBox)
            Dim chkCadastrar = CType(Me.dgvMenus.Rows(i).FindControl("Cadastrar"), CheckBox)
            Dim chkAtualizar = CType(Me.dgvMenus.Rows(i).FindControl("Atualizar"), CheckBox)
            Dim chkExcluir = CType(Me.dgvMenus.Rows(i).FindControl("Excluir"), CheckBox)
            If chkCadastrar.Checked = True Then
                chkAcessar.Checked = True
            End If
            If chkCadastrar.Checked = True Then
                chkAcessar.Checked = True
            End If
            If chkCadastrar.Checked = True Then
                chkAcessar.Checked = True
            End If
        Next
    End Sub
End Class