Public Class EmailParceiro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Not Page.IsPostBack Then
            If Request.QueryString("p") <> "" And Request.QueryString("id") <> "" Then
                Dim ID_Parceiro As String = Request.QueryString("p")
                Dim ID As String = Request.QueryString("id")
                CarregaDados(ID_Parceiro, ID)
                ddlEmpresa.Enabled = False

            ElseIf Request.QueryString("p") <> "" Then
                Dim ID_Parceiro As String = Request.QueryString("p")
                CarregaDados(ID_Parceiro, 0)
                ddlEmpresa.Enabled = False

            ElseIf Request.QueryString("id") <> "" Then
                Dim ID As String = Request.QueryString("id")
                CarregaDados(0, ID)
            End If

        Else
            Session("filtro") = ""
        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub


    Sub CarregaDados(ID_Parceiro, ID)
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If ID_Parceiro <> 0 And ID <> 0 Then
            ds = Con.ExecutarQuery("SELECT NM_RAZAO,NM_FANTASIA,CNPJ, FL_EXPORTADOR FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO = " & ID_Parceiro)
            If ds.Tables(0).Rows.Count > 0 Then
                lblRazaoSocial.Text = "<strong>Razão Social:</strong>" & ds.Tables(0).Rows(0).Item("NM_RAZAO").ToString()
                lblNomeFantasia.Text = "<strong>Nome Fantasia: </strong>" & ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
                lblCNPJ.Text = "<strong>CNPJ: </strong>" & ds.Tables(0).Rows(0).Item("CNPJ").ToString()
                ddlEmpresa.SelectedValue = ID_Parceiro
            End If
            ds = Con.ExecutarQuery("
SELECT ID_PARCEIRO,NM_RAZAO,NM_FANTASIA,CNPJ,B.ID_TERMINAL,B.ID_EVENTO,B.ENDERECOS FROM [dbo].[TB_PARCEIRO] A
LEFT JOIN TB_AMR_PESSOA_EVENTO B ON B.ID_PESSOA = A.ID_PARCEIRO
WHERE B.ID = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                lblRazaoSocial.Text = "<strong>Razão Social:</strong>" & ds.Tables(0).Rows(0).Item("NM_RAZAO").ToString()
                lblNomeFantasia.Text = "<strong>Nome Fantasia: </strong>" & ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
                lblCNPJ.Text = "<strong>CNPJ: </strong>" & ds.Tables(0).Rows(0).Item("CNPJ").ToString()
                ddlEmpresa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO")
                txtEmail.Text = ds.Tables(0).Rows(0).Item("ENDERECOS").ToString()
                ddlPorto.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TERMINAL").ToString()
                ddlEvento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EVENTO").ToString()
                txtID.Text = ID
            End If

        ElseIf ID_Parceiro <> 0 Then
            ds = Con.ExecutarQuery("SELECT NM_RAZAO,NM_FANTASIA,CNPJ, FL_EXPORTADOR FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO = " & ID_Parceiro)
            If ds.Tables(0).Rows.Count > 0 Then
                lblRazaoSocial.Text = "<strong>Razão Social:</strong>" & ds.Tables(0).Rows(0).Item("NM_RAZAO").ToString()
                lblNomeFantasia.Text = "<strong>Nome Fantasia: </strong>" & ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
                lblCNPJ.Text = "<strong>CNPJ: </strong>" & ds.Tables(0).Rows(0).Item("CNPJ").ToString()
                ddlEmpresa.SelectedValue = ID_Parceiro
            End If


        ElseIf ID <> 0 Then
            ds = Con.ExecutarQuery("
SELECT ID_PARCEIRO,NM_RAZAO,NM_FANTASIA,CNPJ,B.ID_TERMINAL,B.ID_EVENTO,B.ENDERECOS FROM [dbo].[TB_PARCEIRO] A
LEFT JOIN TB_AMR_PESSOA_EVENTO B ON B.ID_PESSOA = A.ID_PARCEIRO
WHERE B.ID = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                lblRazaoSocial.Text = "<strong>Razão Social:</strong>" & ds.Tables(0).Rows(0).Item("NM_RAZAO").ToString()
                lblNomeFantasia.Text = "<strong>Nome Fantasia: </strong>" & ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
                lblCNPJ.Text = "<strong>CNPJ: </strong>" & ds.Tables(0).Rows(0).Item("CNPJ").ToString()
                ddlEmpresa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO")
                txtEmail.Text = ds.Tables(0).Rows(0).Item("ENDERECOS").ToString()
                ddlPorto.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TERMINAL").ToString()
                ddlEvento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EVENTO").ToString()
                txtID.Text = ID
            End If
        End If
        Con.Fechar()
    End Sub
    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divmsg2.Visible = False
        divmsg.Visible = False
        divMsgExcluir.Visible = False
        divMsgErro.Visible = False
        If ddlEmpresa.SelectedValue = 0 Then
            lblerro.Text = "Selecione a empresa."
            divmsg2.Visible = True
        ElseIf ddlEvento.SelectedValue = 0 Then
            lblerro.Text = "Selecione o tipo de evento."
            divmsg2.Visible = True
        ElseIf txtEmail.Text = "" Then
            lblerro.Text = "Informe os endereços de eMail."
            divmsg2.Visible = True
            lblerro.Visible = True
        ElseIf SeparaEmail(txtEmail.Text) = False Then
            lblerro.Text = "Email Invalido."
            divmsg2.Visible = True
            lblerro.Visible = True
        Else

            Dim Con As New Conexao_sql
            Dim ID_EVENTO As Integer = ddlEvento.SelectedValue
            Dim ID_PESSOA As Integer = ddlEmpresa.SelectedValue
            Dim ID_TERMINAL As String = ""
            Dim Email As String = txtEmail.Text

            If ddlPorto.SelectedValue = 0 Then
                ID_TERMINAL = " IS NULL"
            Else
                ID_TERMINAL = " = " & ddlPorto.SelectedValue
            End If



            Con.Conectar()

            'Verifica se há resgistro 
            Dim ds As DataSet
            If txtID.Text <> "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblerro.Text = "Usuário não tem permissão para realizar alterações"
                    divmsg2.Visible = True

                Else

                    Con.ExecutarQuery("UPDATE [dbo].[TB_AMR_PESSOA_EVENTO] SET ENDERECOS = LOWER('" & Email & "') WHERE ID = " & txtID.Text)
                    lblmsg.Text = "Email atualizado com sucesso!"
                    divmsg.Visible = True
                End If

                Dim TIPO As String = "E"
                Dim TIPO_PESSOA As String = ""

                'Verifica qual o tipo de pessoa
                ds = Con.ExecutarQuery("SELECT fl_prestador,  fl_armazem_atracacao, fl_armazem_descarga, fl_armazem_desembaraco, fl_agente_internacional FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO =" & ddlEmpresa.SelectedValue)
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("fl_armazem_atracacao").ToString = True Then
                        TIPO_PESSOA = "T"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_descarga").ToString = True Then
                        TIPO_PESSOA = "T"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_desembaraco").ToString = True Then
                        TIPO_PESSOA = "T"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_prestador").ToString = True Then
                        TIPO_PESSOA = "P"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_agente_internacional").ToString = True Then
                        TIPO_PESSOA = "P"
                    Else
                        TIPO_PESSOA = "C"
                    End If
                End If

                'REPLICA EMAILS
                If ckbReplica.Checked = True Then

                    ds = Con.ExecutarQuery("select IDTIPOAVISO FROM TB_TIPOAVISO WHERE TPPROCESSO = 'P'")


                    If ds.Tables(0).Rows.Count > 0 Then

                        For Each linha As DataRow In ds.Tables(0).Rows
                            Dim ID_AVISO As Integer = linha.Item("IDTIPOAVISO").ToString()


                            Dim dsEmail As DataSet = Con.ExecutarQuery("SELECT ID FROM TB_AMR_PESSOA_EVENTO WHERE ID_PESSOA  = " & ddlEmpresa.SelectedValue & " AND ID_EVENTO = " & ID_AVISO)
                            If dsEmail.Tables(0).Rows.Count = 0 Then

                                'insere emails
                                Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ID_AVISO & "," & ddlPorto.SelectedValue & "," & ddlEmpresa.SelectedValue & ",'" & TIPO & "','" & TIPO_PESSOA & "', '" & txtEmail.Text & "')")

                            Else

                                For Each linhaEmail As DataRow In dsEmail.Tables(0).Rows
                                    'update emails
                                    Con.ExecutarQuery("UPDATE [dbo].[TB_AMR_PESSOA_EVENTO] SET ENDERECOS= '" & txtEmail.Text & "' where ID = " & linhaEmail.Item("ID").ToString())
                                Next

                            End If
                        Next

                    End If
                End If
            Else
                Dim TIPO As String = "E"
                Dim TIPO_PESSOA As String = ""

                'Verifica qual o tipo de pessoa
                ds = Con.ExecutarQuery("SELECT fl_prestador,  fl_armazem_atracacao, fl_armazem_descarga, fl_armazem_desembaraco, fl_agente_internacional FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO =" & ID_PESSOA)
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("fl_armazem_atracacao").ToString = True Then
                        TIPO_PESSOA = "T"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_descarga").ToString = True Then
                        TIPO_PESSOA = "T"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_desembaraco").ToString = True Then
                        TIPO_PESSOA = "T"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_prestador").ToString = True Then
                        TIPO_PESSOA = "P"
                    ElseIf ds.Tables(0).Rows(0).Item("fl_agente_internacional").ToString = True Then
                        TIPO_PESSOA = "P"
                    Else
                        TIPO_PESSOA = "C"
                    End If
                End If


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblerro.Text = "Usuário não tem permissão para realizar novos cadastros"
                    divmsg2.Visible = True
                Else
                    'Insere informaçoes no banco
                    Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ddlEvento.SelectedValue & "," & ddlPorto.SelectedValue & "," & ddlEmpresa.SelectedValue & ",'" & TIPO & "','" & TIPO_PESSOA & "', LOWER('" & Email & "'))")

                    ddlPorto.SelectedValue = 0
                    ddlEvento.SelectedValue = 0
                    lblmsg.Text = "Email cadastrado com sucesso!"
                    divmsg.Visible = True
                    Con.Fechar()
                    Call Limpar(Me)
                End If

                'REPLICA EMAILS
                If ckbReplica.Checked = True Then

                    ds = Con.ExecutarQuery("select IDTIPOAVISO FROM TB_TIPOAVISO WHERE TPPROCESSO = 'P'")


                    If ds.Tables(0).Rows.Count > 0 Then

                        For Each linha As DataRow In ds.Tables(0).Rows
                            Dim ID_AVISO As Integer = linha.Item("IDTIPOAVISO").ToString()

                            'insere emails
                            Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ID_AVISO & "," & ddlPorto.SelectedValue & "," & ddlEmpresa.SelectedValue & ",'" & TIPO & "','" & TIPO_PESSOA & "',  LOWER('" & Email & "'))")

                        Next

                    End If
                End If
            End If

        End If
        dgvEmail.DataBind()

    End Sub
    Public Sub Limpar(ByVal controlP As Control)

        Dim ctl As Control

        For Each ctl In controlP.Controls

            If TypeOf ctl Is TextBox Then

                DirectCast(ctl, TextBox).Text = String.Empty

            ElseIf ctl.Controls.Count > 0 Then

                Limpar(ctl)

            End If

        Next


    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("EmailParceiro.aspx")
    End Sub

    Private Sub ddlEmpresa_TextChanged(sender As Object, e As EventArgs) Handles ddlEmpresa.TextChanged
        txtID.Text = ""
        txtEmail.Text = ""
        ddlPorto.SelectedValue = 0
        ddlEvento.SelectedValue = 0
        'dsListaEmail.SelectParameters("ID_PARCEIRO").DefaultValue = ddlEmpresa.SelectedValue
        dgvEmail.DataBind()



        Dim ID As Integer = ddlEmpresa.SelectedValue
        CarregaDados(ID, 0)
    End Sub

    Private Sub ddlEvento_TextChanged(sender As Object, e As EventArgs) Handles ddlEvento.TextChanged
        Dim ID_EVENTO As Integer = ddlEvento.SelectedValue
        Dim ID_PESSOA As Integer = ddlEmpresa.SelectedValue

        Dim Con As New Conexao_sql

        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID, ENDERECOS, ID_TERMINAL FROM [TB_AMR_PESSOA_EVENTO] WHERE ID_EVENTO =" & ID_EVENTO & " and ID_Pessoa =" & ID_PESSOA)
        If ds.Tables(0).Rows.Count > 0 Then
            txtEmail.Text = ds.Tables(0).Rows(0).Item("ENDERECOS").ToString()
            ddlPorto.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TERMINAL").ToString()
            txtID.Text = ds.Tables(0).Rows(0).Item("ID").ToString()
        Else
            txtID.Text = ""
            txtEmail.Text = ""
            ddlPorto.SelectedValue = 0
        End If
        Con.Fechar()
    End Sub

    Sub Empresas()
        If ckbClientes.Checked = False Then
            If Session("filtro") = "" Then
                Session("filtro") &= " where FL_Importador = 0 AND FL_Exportador = 0 AND FL_Agente = 0 AND  FL_Comissaria = 0 "
            Else
                Session("filtro") &= " and FL_Importador = 0 AND FL_Exportador = 0 AND FL_Agente = 0 AND  FL_Comissaria = 0 "
            End If

        End If
        If ckbParceiros.Checked = False Then
            If Session("filtro") = "" Then
                Session("filtro") &= " where FL_Prestador = 0 "
            Else
                Session("filtro") &= " and FL_Prestador = 0 "
            End If


        End If
        If ckbTerminais.Checked = False Then
            If Session("filtro") = "" Then
                Session("filtro") &= " where FL_Armazem = 0 "
            Else
                Session("filtro") &= " and FL_Armazem = 0 "

            End If


        End If
        dsEmpresas.SelectCommand = dsEmpresas.SelectCommand.Replace("#FILTRO", Session("filtro"))
        dsEmpresas.DataBind()
    End Sub


    'Private Sub dsacordos_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles dsacordos.Selected
    '    Response.Write(Classes.debug.imprimeValoresParametros(e.Command, True))
    '    Response.End()
    'End Sub

    Private Sub ddlPorto_TextChanged(sender As Object, e As EventArgs) Handles ddlPorto.TextChanged
        Dim ID_EVENTO As Integer = ddlEvento.SelectedValue
        Dim ID_PESSOA As Integer = ddlEmpresa.SelectedValue

        Dim Con As New Conexao_sql

        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID, ENDERECOS FROM [TB_AMR_PESSOA_EVENTO] WHERE ID_EVENTO =" & ID_EVENTO & " and ID_Pessoa =" & ID_PESSOA & " AND ID_TERMINAL = " & ddlPorto.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            txtEmail.Text = ds.Tables(0).Rows(0).Item("ENDERECOS").ToString()

            txtID.Text = ds.Tables(0).Rows(0).Item("ID").ToString()
        Else
            txtID.Text = ""
            txtEmail.Text = ""
        End If
        Con.Fechar()
    End Sub

    Private Sub ckbClientes_CheckedChanged(sender As Object, e As EventArgs) Handles ckbClientes.CheckedChanged
        ddlFiltro()
    End Sub

    Sub ddlFiltro()
        Dim filtro As String = ""
        If ckbClientes.Checked = False Then
            filtro &= " AND FL_IMPORTADOR = 0 AND FL_EXPORTADOR = 0 AND FL_AGENTE =  0 AND FL_COMISSARIA  = 0 "

        ElseIf ckbParceiros.Checked = False Then
            filtro &= " AND FL_PRESTADOR = 0 "

        ElseIf ckbTerminais.Checked = False Then
            filtro &= " AND FL_ARMAZEM = 0  "

        End If
        dsEmpresas.SelectCommand = dsEmpresas.SelectCommand.Replace("/*FILTRO*/", filtro)
    End Sub

    Private Sub ckbParceiros_CheckedChanged(sender As Object, e As EventArgs) Handles ckbParceiros.CheckedChanged
        ddlFiltro()

    End Sub

    Private Sub ckbTerminais_CheckedChanged(sender As Object, e As EventArgs) Handles ckbTerminais.CheckedChanged
        ddlFiltro()

    End Sub


    Function SeparaEmail(ByVal email As String) As Boolean
        divmsg2.Visible = False
        lblerro.Text = ""
        btnGravar.Enabled = True
        Dim erro As Boolean
        'quebrar a string
        Dim palavras As String() = email.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

        'exibe o resultado
        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
            If ValidaEmail.Validar(palavras(i)) = False Then
                'lblerro.Text &= "O e-Mail <b>" & palavras(i) & "</b> é inválido. <br/>"
                'divmsg2.Visible = True
                'btnGravar.Enabled = False
                erro = False
            Else
                erro = True

            End If

        Next
        Return erro
    End Function

    'Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
    '    SeparaEmail(txtEmail.Text)
    'End Sub

    Protected Sub dsListaEmail_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvEmail.DataSource = Session("TaskTable")
            dgvEmail.DataBind()
            dgvEmail.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String
        Dim sortDirection As String = "ASC"
        Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then

            If sortExpression = column Then
                Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

                If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column
        Return sortDirection
    End Function

    Private Sub dgvEmail_PreRender(sender As Object, e As EventArgs) Handles dgvEmail.PreRender
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvEmail.Columns(7).Visible = False

        End If

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvEmail.Columns(8).Visible = False
        End If

        Con.Fechar()

    End Sub
    Private Sub dgvEmail_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvEmail.RowCommand
        divMsgExcluir.Visible = False
        divMsgErro.Visible = False
        If e.CommandName = "Excluir" Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ID As String = e.CommandArgument

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

                lblMsgErro.Text = "Usuário não tem permissão para realizar exclusões"
                divMsgErro.Visible = True

            Else

                Con.ExecutarQuery("DELETE FROM [TB_AMR_PESSOA_EVENTO] WHERE ID =" & ID)
                lblMsgExcluir.Text = "Registro deletado!"
                divMsgExcluir.Visible = True
                dgvEmail.DataBind()
            End If
            Con.Fechar()

        End If
    End Sub
End Class