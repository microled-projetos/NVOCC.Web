Public Class MoedaFreteArmador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Verifica se o usuario está logado
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        divmsg.Visible = False

        'Carrega campos do formulario usando o ID passado via QueryString
        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            CarregaCampos()

        End If

        'Verifica se usuario possui permissao de acesso a tela
        Dim Con As New Conexao_sql
        Con.Conectar()
        'Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        'If ds.Tables(0).Rows.Count > 0 Then

        '    If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

        '        Response.Redirect("Default.aspx")

        '    End If

        'Else
        '    Response.Redirect("Default.aspx")
        'End If

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Public Sub CarregaCampos()
        'Carrega campos do formulario usando o ID passado via QueryString
        divInfo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        ds = Con.ExecutarQuery("SELECT A.ID_MOEDA_FRETE_ARMADOR, A.ID_ARMADOR, A.ID_MOEDA, B.NM_MOEDA, CONVERT(varchar,A.DT_CAMBIO,103)DT_CAMBIO,A.VL_TXOFICIAL 
FROM TB_MOEDA_FRETE_ARMADOR A
LEFT JOIN TB_MOEDA B ON B.ID_MOEDA = A.ID_MOEDA
WHERE ID_MOEDA_FRETE_ARMADOR = " & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then
            txtIDMoedaFreteArmador.Text = ds.Tables(0).Rows(0).Item("ID_MOEDA_FRETE_ARMADOR").ToString()
            txtDataCambio.Text = ds.Tables(0).Rows(0).Item("DT_CAMBIO").ToString()
            'Dim DATA As Date = ds.Tables(0).Rows(0).Item("DT_CAMBIO")
            'Data = DATA.ToString("dd-MM-yyyy")
            'txtDataCambio.Text = DATA
            txtTxOficial.Text = ds.Tables(0).Rows(0).Item("VL_TXOFICIAL").ToString()
            ddlMoeda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA").ToString()
            ddlArmador.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ARMADOR").ToString()

        End If
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("MoedaFreteArmador.aspx")
    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divErro.Visible = False
        divmsg.Visible = False
        divInfo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        'Verifica campos obrigatorios
        If txtTxOficial.Text = "" Then
            lblErro.Text = "Preencha o campo de Valor de TX Oficial."
            divErro.Visible = True
        ElseIf txtDataCambio.Text = "" Then
            lblErro.Text = "Preencha o campo de Data Cambio."
            divErro.Visible = True
        ElseIf ddlMoeda.SelectedValue = 0 Then
            lblErro.Text = "Selecione o tipo de moeda."
            divErro.Visible = True
        ElseIf ddlArmador.SelectedValue = 0 Then
            lblErro.Text = "Selecione o Armador"
            divErro.Visible = True
        ElseIf v.ValidaData(txtDataCambio.Text) = False Then
            divErro.Visible = True
            lblErro.Text = "Data Inválida."
        Else
            Dim ocorrencias As Integer = QuantidadeAssociada(txtTxOficial.Text, ",")
            If ocorrencias = 0 Then
                txtTxOficial.Text = FormatNumber(txtTxOficial.Text, 5)
            End If
            txtTxOficial.Text = txtTxOficial.Text.Replace(",", ".")

            If txtIDMoedaFreteArmador.Text = "" Then
                'Verifica permissão de usuario para inserir
                'ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                ' If ds.Tables(0).Rows.Count > 0 Then

                '    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                '        divErro.Visible = True
                '        lblErro.Text = "Usuário não possui permissão para cadastrar."
                '    Else
                '        'Verifica se o Registro já existe
                '        ds = Con.ExecutarQuery("SELECT ID_MOEDA_FRETE_ARMADOR FROM [TB_MOEDA_FRETE_ARMADOR] WHERE ID_ARMADOR = " & ddlArmador.SelectedValue & " AND ID_MOEDA = " & ddlMoeda.SelectedValue & " AND Convert(date, DT_CAMBIO, 103)  = Convert(date, '" & txtDataCambio.Text & "', 103) ")
                '        If ds.Tables(0).Rows.Count > 0 Then

                '            lblErro.Text = "Este registro já existe."
                '            divErro.Visible = True

                '        Else
                '            'Insere informaçoes no banco
                '            Con.ExecutarQuery("INSERT INTO [dbo].[TB_MOEDA_FRETE_ARMADOR] (ID_ARMADOR,ID_MOEDA,DT_CAMBIO,VL_TXOFICIAL ) VALUES (" & ddlArmador.SelectedValue & " , " & ddlMoeda.SelectedValue & " , Convert(date, '" & txtDataCambio.Text & "', 103) , '" & txtTxOficial.Text & "'); SELECT CAST(SCOPE_IDENTITY() AS INT)")
                '            Con.Fechar()

                '            divmsg.Visible = True
                '            dgvMoedaFrete.DataBind()
                '            txtIDMoedaFreteArmador.Text = ""
                '            txtDataCambio.Text = ""
                '            txtTxOficial.Text = ""
                '            ddlMoeda.SelectedValue = 0
                '            ddlArmador.SelectedValue = 0

                '        End If

                '    End If
                'Else
                '    divErro.Visible = True
                '    lblErro.Text = "Usuário não possui permissão para cadastrar."
                'End If
                'Verifica se o Registro já existe

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblErro.Text = "Usuário não possui permissão para cadastrar."
                Else
                    ds = Con.ExecutarQuery("SELECT ID_MOEDA_FRETE_ARMADOR FROM [TB_MOEDA_FRETE_ARMADOR] WHERE ID_ARMADOR = " & ddlArmador.SelectedValue & " AND ID_MOEDA = " & ddlMoeda.SelectedValue & " AND Convert(date, DT_CAMBIO, 103)  = Convert(date, '" & txtDataCambio.Text & "', 103) ")
                    If ds.Tables(0).Rows.Count > 0 Then

                        lblErro.Text = "Este registro já existe."
                        divErro.Visible = True

                    Else
                        'Insere informaçoes no banco
                        Con.ExecutarQuery("INSERT INTO [dbo].[TB_MOEDA_FRETE_ARMADOR] (ID_ARMADOR,ID_MOEDA,DT_CAMBIO,VL_TXOFICIAL ) VALUES (" & ddlArmador.SelectedValue & " , " & ddlMoeda.SelectedValue & " , Convert(date, '" & txtDataCambio.Text & "', 103) , '" & txtTxOficial.Text & "'); SELECT CAST(SCOPE_IDENTITY() AS INT)")
                        Con.Fechar()

                        divmsg.Visible = True
                        dgvMoedaFrete.DataBind()
                        txtIDMoedaFreteArmador.Text = ""
                        txtDataCambio.Text = ""
                        txtTxOficial.Text = ""
                        ddlMoeda.SelectedValue = 0
                        ddlArmador.SelectedValue = 0

                    End If
                End If


            Else
                'Verifica se usuario tem permissao para alterar
                'ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                'If ds.Tables(0).Rows.Count > 0 Then

                '    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                '        divErro.Visible = True
                '        lblErro.Text = "Usuário não possui permissão para alterar."
                '    Else
                '        'Verifica se o Registro já existe

                '        ds = Con.ExecutarQuery("SELECT ID_MOEDA_FRETE_ARMADOR FROM [TB_MOEDA_FRETE_ARMADOR] WHERE ID_ARMADOR = " & ddlArmador.SelectedValue & " AND ID_MOEDA = " & ddlMoeda.SelectedValue & " AND Convert(date, DT_CAMBIO, 103)  = Convert(date, '" & txtDataCambio.Text & "', 103)  AND ID_MOEDA_FRETE_ARMADOR <> " & txtIDMoedaFreteArmador.Text)
                '        If ds.Tables(0).Rows.Count > 0 Then

                '            lblErro.Text = "Este registro já existe."
                '            divErro.Visible = True

                '        Else
                '            'Realiza alterações
                '            Con.ExecutarQuery("UPDATE [dbo].[TB_MOEDA_FRETE_ARMADOR] SET ID_ARMADOR = " & ddlArmador.SelectedValue & " ,ID_MOEDA = " & ddlMoeda.SelectedValue & " , DT_CAMBIO = Convert(date, '" & txtDataCambio.Text & "', 103)  , VL_TXOFICIAL = '" & txtTxOficial.Text & "' WHERE ID_MOEDA_FRETE_ARMADOR = " & txtIDMoedaFreteArmador.Text)
                '            Con.Fechar()
                '            txtIDMoedaFreteArmador.Text = ""
                '            txtDataCambio.Text = ""
                '            txtTxOficial.Text = ""
                '            ddlMoeda.SelectedValue = 0
                '            ddlArmador.SelectedValue = 0

                '            divmsg.Visible = True
                '            dgvMoedaFrete.DataBind()
                '        End If

                '    End If
                'Else
                '    divErro.Visible = True
                '    lblErro.Text = "Usuário não possui permissão para alterar."
                'End If


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblErro.Text = "Usuário não possui permissão."
                Else

                    'Verifica se o Registro já existe

                    ds = Con.ExecutarQuery("SELECT ID_MOEDA_FRETE_ARMADOR FROM [TB_MOEDA_FRETE_ARMADOR] WHERE ID_ARMADOR = " & ddlArmador.SelectedValue & " AND ID_MOEDA = " & ddlMoeda.SelectedValue & " AND Convert(date, DT_CAMBIO, 103)  = Convert(date, '" & txtDataCambio.Text & "', 103)  AND ID_MOEDA_FRETE_ARMADOR <> " & txtIDMoedaFreteArmador.Text)
                    If ds.Tables(0).Rows.Count > 0 Then

                        lblErro.Text = "Este registro já existe."
                        divErro.Visible = True

                    Else
                        'Realiza alterações
                        Con.ExecutarQuery("UPDATE [dbo].[TB_MOEDA_FRETE_ARMADOR] SET ID_ARMADOR = " & ddlArmador.SelectedValue & " ,ID_MOEDA = " & ddlMoeda.SelectedValue & " , DT_CAMBIO = Convert(date, '" & txtDataCambio.Text & "', 103)  , VL_TXOFICIAL = '" & txtTxOficial.Text & "' WHERE ID_MOEDA_FRETE_ARMADOR = " & txtIDMoedaFreteArmador.Text)
                        Con.Fechar()
                        txtIDMoedaFreteArmador.Text = ""
                        txtDataCambio.Text = ""
                        txtTxOficial.Text = ""
                        ddlMoeda.SelectedValue = 0
                        ddlArmador.SelectedValue = 0

                        divmsg.Visible = True
                        dgvMoedaFrete.DataBind()
                    End If

                End If

            End If


        End If

    End Sub

    Private Sub dgvMoedaFrete_PreRender(sender As Object, e As EventArgs) Handles dgvMoedaFrete.PreRender

        Dim Con As New Conexao_sql
        Con.Conectar()
        'Verfica permissoes de alteração e exclusão de usuários
        Dim ds As DataSet
        '= Con.ExecutarQuery("SELECT FL_EXCLUIR,FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            dgvMoedaFrete.Columns(5).Visible = False

        End If

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 10 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvMoedaFrete.Columns(6).Visible = False

        End If


        Con.Fechar()
    End Sub


    Protected Sub dgvMoedaFrete_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        'Ordena grid pelo click no cabeçario
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvMoedaFrete.DataSource = Session("TaskTable")
            dgvMoedaFrete.DataBind()
            dgvMoedaFrete.HeaderRow.TableSection = TableRowSection.TableHeader
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

    Private Sub dgvMoedaFrete_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dgvMoedaFrete.RowDeleted
        'Exibe mensagem de exclusão e atualiza grid
        dgvMoedaFrete.DataBind()
        divInfo.Visible = True
        lblInfo.Text = "Registro excluído com sucesso"
    End Sub

    Private Function QuantidadeAssociada(ByVal stringParaConsulta As String, ByVal procurarPor As String) As Integer

        Dim Posicao As Integer = 0
        Dim Ocorrencias As Integer = 0

        Do

            Posicao = stringParaConsulta.IndexOf(procurarPor, Posicao)

            If Posicao <> -1 Then

                ' Uma ocorrencia encontrada.

                ' Incrementa a quantidade de ocorrencias.

                Ocorrencias += 1



                ' Move para frente na string a quantidade

                ' igual a quantidade da termo procurado.

                Posicao += procurarPor.Length

            End If

        Loop Until Posicao = -1

        Return Ocorrencias

    End Function

    Private Sub ddlConsultas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConsultas.SelectedIndexChanged
        If ddlConsultas.SelectedValue = 2 Then
            txtPesquisa.Text = ""
            txtPesquisa.CssClass = "form-control data"
        Else
            txtPesquisa.Text = ""
            txtPesquisa.CssClass = "form-control"
        End If

    End Sub

    Private Sub btnPesquisa_Click(sender As Object, e As EventArgs) Handles btnPesquisa.Click

        Dim sql As String = ""
        If ddlConsultas.SelectedValue = 1 And txtPesquisa.Text <> "" Then
            sql = "SELECT A.ID_MOEDA_FRETE_ARMADOR as Id, C.NM_RAZAO, A.ID_ARMADOR,C.NM_RAZAO, A.ID_MOEDA, B.NM_MOEDA, A.DT_CAMBIO,A.VL_TXOFICIAL 
FROM TB_MOEDA_FRETE_ARMADOR A
LEFT JOIN TB_MOEDA B ON B.ID_MOEDA = A.ID_MOEDA
LEFT JOIN TB_PARCEIRO C ON C.ID_PARCEIRO = A.ID_ARMADOR WHERE  B.NM_MOEDA LIKE '%" & txtPesquisa.Text & "%'"

        ElseIf ddlConsultas.SelectedValue = 2 And txtPesquisa.Text <> "" Then
            sql = "SELECT A.ID_MOEDA_FRETE_ARMADOR as Id, C.NM_RAZAO, A.ID_ARMADOR,C.NM_RAZAO, A.ID_MOEDA, B.NM_MOEDA, A.DT_CAMBIO,A.VL_TXOFICIAL 
FROM TB_MOEDA_FRETE_ARMADOR A
LEFT JOIN TB_MOEDA B ON B.ID_MOEDA = A.ID_MOEDA
LEFT JOIN TB_PARCEIRO C ON C.ID_PARCEIRO = A.ID_ARMADOR WHERE  Convert(date, DT_CAMBIO,103) = Convert(date, '" & txtPesquisa.Text & "',103)"
        Else
            sql = "SELECT A.ID_MOEDA_FRETE_ARMADOR as Id, C.NM_RAZAO, A.ID_ARMADOR,C.NM_RAZAO, A.ID_MOEDA, B.NM_MOEDA, A.DT_CAMBIO,A.VL_TXOFICIAL 
FROM TB_MOEDA_FRETE_ARMADOR A
LEFT JOIN TB_MOEDA B ON B.ID_MOEDA = A.ID_MOEDA
LEFT JOIN TB_PARCEIRO C ON C.ID_PARCEIRO = A.ID_ARMADOR"
        End If
        dsMoedaFrete.SelectCommand = sql
        dgvMoedaFrete.DataBind()
    End Sub

End Class