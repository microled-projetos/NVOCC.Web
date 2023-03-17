Public Class ConsultaParceiro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub
    Private Sub dgvParceiros_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvParceiros.RowCommand
        diverro.Visible = False
        divInfo.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Excluir" Then
            Dim ds As DataSet = Con.ExecutarQuery("select count(ID_BL)ID_BL from TB_BL where ID_PARCEIRO_AGENCIA = " & ID & " OR ID_PARCEIRO_AGENTE = " & ID & " OR ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ID & " OR ID_PARCEIRO_ARMAZEM_ATRACACAO = " & ID & " OR ID_PARCEIRO_ARMAZEM_DESCARGA = " & ID & " OR ID_PARCEIRO_ARMAZEM_DESEMBARACO = " & ID & " OR ID_PARCEIRO_CLIENTE = " & ID & " OR ID_PARCEIRO_COLOADER = " & ID & " OR ID_PARCEIRO_COMISSARIA = " & ID & " OR ID_PARCEIRO_EXPORTADOR = " & ID & " OR ID_PARCEIRO_OPERADOR = " & ID & " OR ID_PARCEIRO_RODOVIARIO = " & ID & " OR ID_PARCEIRO_TRANSPORTADOR = " & ID & " OR ID_PARCEIRO_VENDEDOR = " & ID)


            If ds.Tables(0).Rows(0).Item("ID_BL") > 0 Then
                diverro.Visible = True
                lblErroExcluir.Text = "Não foi possível excluir o registro: existe uma ou mais BL's vinculadas a este parceiro"
            Else
                Con.ExecutarQuery("DELETE FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO =" & ID & " ; DELETE FROM [dbo].[TB_CONTATO] WHERE ID_PARCEIRO =" & ID & " ; DELETE FROM [dbo].[TB_AMR_PESSOA_EVENTO] WHERE ID_PESSOA =" & ID)
                dgvParceiros.DataBind()
                divInfo.Visible = True
                lblInfo.Text = "Parceiro excluído com sucesso"
            End If

            AtualizaGrid()
        ElseIf e.CommandName = "Taxas" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(FL_PRESTADOR,0)FL_PRESTADOR, ISNULL(FL_AGENTE_INTERNACIONAL,0)FL_AGENTE_INTERNACIONAL FROM [TB_PARCEIRO] WHERE ID_PARCEIRO = " & ID)

            If ds.Tables(0).Rows.Count > 0 Then
                txtID.text = ID
                If ds.Tables(0).Rows(0).Item("FL_PRESTADOR") = True Then

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "TaxaPrestador()", True)

                ElseIf ds.Tables(0).Rows(0).Item("FL_AGENTE_INTERNACIONAL") = True Then

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "TaxaAgenteInternacional()", True)

                Else

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "<script>alert('Parceiro selecionado não é AGENTE INTERNACIONAL e nem PRESTADOR!');</script>", False)
                End If

            End If

        ElseIf e.CommandName = "TaxasArmador" Then

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(FL_TRANSPORTADOR,0)FL_TRANSPORTADOR, ISNULL(FL_CIA_AEREA,0)FL_CIA_AEREA FROM [TB_PARCEIRO] WHERE ID_PARCEIRO = " & ID)

            If ds.Tables(0).Rows.Count > 0 Then
                txtID.text = ID
                If ds.Tables(0).Rows(0).Item("FL_TRANSPORTADOR") = True Then

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "TaxaTransportador()", True)

                ElseIf ds.Tables(0).Rows(0).Item("FL_CIA_AEREA") = True Then

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "TaxaTransportador()", True)

                Else

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "<script>alert('Parceiro selecionado não é TRANSPORTADOR e nem CIA AÉREA!');</script>", False)

                End If

            End If

        ElseIf e.CommandName = "Duplicar" Then
            txtID.Text = ID
            Con.ExecutarQuery("INSERT INTO [dbo].[TB_PARCEIRO] (FL_IMPORTADOR, FL_EXPORTADOR, FL_AGENTE, FL_AGENTE_INTERNACIONAL, FL_TRANSPORTADOR,  FL_COMISSARIA,  FL_VENDEDOR,  FL_ARMAZEM_ATRACACAO, FL_ARMAZEM_DESEMBARACO, FL_ARMAZEM_DESCARGA,  FL_PRESTADOR,  NM_RAZAO,  NM_FANTASIA, CNPJ,  CPF,  TP_PESSOA,  INSCR_ESTADUAL,  INSCR_MUNICIPAL,  ENDERECO, NR_ENDERECO, BAIRRO, COMPL_ENDERECO, OB_COMPLEMENTARES, ID_CIDADE, TELEFONE, CEP, ID_VENDEDOR, FL_ISENTO_ISS, FL_ISENTO_PIS, FL_ISENTO_COFINS, VL_ALIQUOTA_ISS, VL_ALIQUOTA_PIS, VL_ALIQUOTA_COFINS, EMAIL_NF_ELETRONICA, CD_IATA, FL_SIMPLES_NACIONAL, FL_ATIVO, FL_INDICADOR, SPREAD_MARITIMO_IMPO_FCL, SPREAD_MARITIMO_IMPO_LCL, SPREAD_MARITIMO_EXPO_FCL, SPREAD_MARITIMO_EXPO_LCL, SPREAD_AEREO_IMPO, SPREAD_AEREO_EXPO, ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL, ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL, ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL, ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL, ID_ACORDO_CAMBIO_AEREO_IMPO, ID_ACORDO_CAMBIO_AEREO_EXPO, QT_DIAS_FATURAMENTO, ID_TIPO_FATURAMENTO, EMAIL, FL_VENDEDOR_DIRETO, FL_EQUIPE_INSIDE_SALES, FL_SHIPPER, REGRA_ATUALIZACAO)			SELECT FL_IMPORTADOR,  FL_EXPORTADOR,  FL_AGENTE,  FL_AGENTE_INTERNACIONAL,  FL_TRANSPORTADOR,  FL_COMISSARIA,  FL_VENDEDOR,  FL_ARMAZEM_ATRACACAO, FL_ARMAZEM_DESEMBARACO, FL_ARMAZEM_DESCARGA,  FL_PRESTADOR,  NM_RAZAO,  NM_FANTASIA, CNPJ,  CPF,  TP_PESSOA,  INSCR_ESTADUAL,  INSCR_MUNICIPAL,  ENDERECO, NR_ENDERECO, BAIRRO, COMPL_ENDERECO, OB_COMPLEMENTARES, ID_CIDADE, TELEFONE, CEP, ID_VENDEDOR, FL_ISENTO_ISS, FL_ISENTO_PIS, FL_ISENTO_COFINS, VL_ALIQUOTA_ISS, VL_ALIQUOTA_PIS, VL_ALIQUOTA_COFINS, EMAIL_NF_ELETRONICA, CD_IATA, FL_SIMPLES_NACIONAL, FL_ATIVO, FL_INDICADOR, SPREAD_MARITIMO_IMPO_FCL, SPREAD_MARITIMO_IMPO_LCL, SPREAD_MARITIMO_EXPO_FCL, SPREAD_MARITIMO_EXPO_LCL, SPREAD_AEREO_IMPO, SPREAD_AEREO_EXPO, ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL, ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL, ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL, ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL, ID_ACORDO_CAMBIO_AEREO_IMPO, ID_ACORDO_CAMBIO_AEREO_EXPO, QT_DIAS_FATURAMENTO, ID_TIPO_FATURAMENTO, EMAIL, FL_VENDEDOR_DIRETO, FL_EQUIPE_INSIDE_SALES, FL_SHIPPER, REGRA_ATUALIZACAO FROM TB_PARCEIRO WHERE ID_PARCEIRO =" & txtID.Text)
            dgvParceiros.DataBind()
            divInfo.Visible = True
            lblInfo.Text = "Parceiro duplicado com sucesso!"

        End If
        Con.Fechar()
    End Sub

    Private Sub dgvParceiros_PreRender(sender As Object, e As EventArgs) Handles dgvParceiros.PreRender

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        'exibe botão de excluir apenas para administradores
        ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_VINCULO_USUARIO A 
LEFT JOIN TB_TIPO_USUARIO C ON C.ID_TIPO_USUARIO = A.ID_TIPO_USUARIO
WHERE A.ID_TIPO_USUARIO = 1 AND A.ID_USUARIO  =" & Session("ID_USUARIO"))
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvParceiros.Columns(11).Visible = False
        End If


        'verifica se o usuario tem permissão de alterações de parceiro
        'ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        'If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
        '    dgvParceiros.Columns(6).Visible = False
        'End If



        'verifica se o usuario tem permissão de acesso ao cadastro de Email x Eventos
        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvParceiros.Columns(8).Visible = False
        End If


        'verifica se o usuario tem permissão de acesso ao cadastro de Cliente Final
        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 23 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvParceiros.Columns(9).Visible = False
        End If


        'verifica se o usuario tem permissão de cadastrar novos parceiros para usar a opção de duplicar
        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvParceiros.Columns(10).Visible = False
        End If

        Con.Fechar()

    End Sub

    Private Sub ddlConsulta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConsulta.SelectedIndexChanged

        If ddlConsulta.SelectedValue = 1 Then
            txtConsulta.Text = ""
            divPesquisa.Visible = True
            txtConsulta.CssClass = "form-control cnpj"
        ElseIf ddlConsulta.SelectedValue = 2 Or ddlConsulta.SelectedValue = 4 Then
            txtConsulta.Text = ""
            divPesquisa.Visible = True
            txtConsulta.CssClass = "form-control"
        ElseIf ddlConsulta.SelectedValue = 3 Then
            txtConsulta.Text = ""
            divPesquisa.Visible = True
            txtConsulta.CssClass = "form-control cpf"
        Else
            divPesquisa.Visible = False
        End If
    End Sub


    Sub AtualizaGrid()
        msgerro.Text = ""
        Dim FILTRO As String = ""

        If txtConsulta.Text = "" Then
            dsParceiros.SelectCommand = "SELECT ID_PARCEIRO as Id, CNPJ , UPPER(NM_RAZAO) RazaoSocial, CPF, CASE WHEN ISNULL(FL_ATIVO,0) = 0 THEN 'Não' WHEN ISNULL(FL_ATIVO,0) = 1 THEN 'Sim' end Ativo  FROM TB_PARCEIRO #FILTRO ORDER BY NM_RAZAO"
            dgvParceiros.DataBind()
        Else
            If ddlConsulta.SelectedValue = 1 Then
                If Len(txtConsulta.Text) = 18 Then
                    FILTRO = " WHERE CNPJ = '" & txtConsulta.Text & "'"
                    dsParceiros.SelectCommand = dsParceiros.SelectCommand.Replace("#FILTRO", FILTRO)
                    dgvParceiros.DataBind()
                Else
                    msgerro.Text = "CNPJ é composto de 14 caracteres."
                End If

            ElseIf ddlConsulta.SelectedValue = 3 Then
                If Len(txtConsulta.Text) = 14 Then
                    FILTRO = " WHERE CPF = '" & txtConsulta.Text & "'"
                    dsParceiros.SelectCommand = dsParceiros.SelectCommand.Replace("#FILTRO", FILTRO)
                    dgvParceiros.DataBind()
                Else
                    msgerro.Text = "CPF é composto de 11 caracteres."
                End If
            ElseIf ddlConsulta.SelectedValue = 2 Then
                FILTRO = " WHERE NM_RAZAO LIKE '%" & txtConsulta.Text & "%' "
                dsParceiros.SelectCommand = dsParceiros.SelectCommand.Replace("#FILTRO", FILTRO)
                dgvParceiros.DataBind()

            ElseIf ddlConsulta.SelectedValue = 4 Then
                FILTRO = " WHERE NM_FANTASIA LIKE '%" & txtConsulta.Text & "%' "
                dsParceiros.SelectCommand = dsParceiros.SelectCommand.Replace("#FILTRO", FILTRO)
                dgvParceiros.DataBind()
            End If

            dsParceiros.SelectCommand = "SELECT ID_PARCEIRO as Id, CNPJ , UPPER(NM_RAZAO) RazaoSocial, CPF, CASE WHEN ISNULL(FL_ATIVO,0) = 0 THEN 'Não' WHEN ISNULL(FL_ATIVO,0) = 1 THEN 'Sim' end Ativo  FROM TB_PARCEIRO " & FILTRO & " ORDER BY NM_RAZAO"
        End If
    End Sub
    Private Sub txtConsulta_TextChanged(sender As Object, e As EventArgs) Handles txtConsulta.TextChanged

        AtualizaGrid()


    End Sub

    Protected Sub dgvParceiros_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)
        AtualizaGrid()

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvParceiros.DataSource = Session("TaskTable")
            dgvParceiros.DataBind()

            dgvParceiros.HeaderRow.TableSection = TableRowSection.TableHeader
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

    Private Sub dgvParceiros_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvParceiros.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Ativo As Label = CType(e.Row.FindControl("lblAtivo"), Label)

            If Ativo.Text = "Não" Then

                Ativo.Style("color") = "red"
            ElseIf Ativo.Text = "Sim" Then

                Ativo.Style("color") = "green"

            End If

        End If
    End Sub
End Class