Imports System.Web.Services

Public Class TaxasLocaisArmador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            dsTaxas.SelectParameters("ID").DefaultValue = Request.QueryString("id")
            dgvTaxas.DataBind()
            ddlTransportadorTaxaNovo.SelectedValue = Request.QueryString("id")

        End If

        Con.Fechar()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If v.ValidaData(txtValidadeInicialTaxa.Text) = False Then
            divErro.Visible = True
            lblmsgErro.Text = "Data Inválida."
        ElseIf ddlTransportadorTaxa.SelectedValue = 0 Or ddlPortoTaxa.SelectedValue = 0 Or ddlComexTaxa.SelectedValue = 0 Or ddlViaTransporte.SelectedValue = 0 Or ddlBaseCalculo.SelectedValue = 0 Or ddlMoeda.SelectedValue = 0 Or ddlDespesaTaxa.SelectedValue = 0 Or txtValorTaxaLocal.Text = "" Or txtValidadeInicialTaxa.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Preencha os campos obrigatórios."

        Else

            If txtIDTaxa.Text <> "" Then


                txtValorTaxaLocal.Text = txtValorTaxaLocal.Text.Replace(".", "")
                txtValorTaxaLocal.Text = txtValorTaxaLocal.Text.Replace(",", ".")


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else
                    ds = Con.ExecutarQuery("UPDATE TB_TAXA_LOCAL_TRANSPORTADOR SET ID_TRANSPORTADOR = " & ddlTransportadorTaxa.SelectedValue & ",ID_MOEDA =  " & ddlMoeda.SelectedValue & ",ID_BASE_CALCULO =  " & ddlBaseCalculo.SelectedValue & ",ID_PORTO =  " & ddlPortoTaxa.SelectedValue & ",ID_TIPO_COMEX = " & ddlComexTaxa.SelectedValue & ",ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & ",ID_ITEM_DESPESA = " & ddlDespesaTaxa.SelectedValue & ", VL_TAXA_LOCAL_COMPRA = '" & txtValorTaxaLocal.Text & "', DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicialTaxa.Text & "',103) FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & txtIDTaxa.Text)
                    lblmsgSuccess.Text = "Registro cadastrado/atualizado com sucesso!"
                    divSuccess.Visible = True
                    dgvTaxas.DataBind()
                    'mpe.Show()
                End If


            End If


        End If

        Con.Fechar()
    End Sub

    Private Sub txtConsulta_TextChanged(sender As Object, e As EventArgs) Handles txtConsulta.TextChanged
        msgerro.Text = ""
        Dim FILTRO As String = ""

        If txtConsulta.Text = "" Then
            dsTaxas.SelectParameters("ID").DefaultValue = Request.QueryString("id")
            dgvTaxas.DataBind()
        Else
            If ddlConsulta.SelectedValue = 1 Then
                FILTRO = " AND NM_PORTO LIKE '%" & txtConsulta.Text & "%' "
            ElseIf ddlConsulta.SelectedValue = 3 Then
                FILTRO = " AND NM_TIPO_COMEX LIKE '%" & txtConsulta.Text & "%' "
            Else
                FILTRO = " AND NM_VIATRANSPORTE LIKE '%" & txtConsulta.Text & "%' "

            End If
            dsTaxas.SelectCommand = "SELECT A.ID_TAXA_LOCAL_TRANSPORTADOR,
A.ID_TRANSPORTADOR,
A.ID_PORTO,B.NM_PORTO,
A.ID_TIPO_COMEX,D.NM_TIPO_COMEX,
A.ID_VIATRANSPORTE,C.NM_VIATRANSPORTE,
A.ID_ITEM_DESPESA,F.NM_ITEM_DESPESA,
A.VL_TAXA_LOCAL_COMPRA,
A.DT_VALIDADE_INICIAL 
FROM 
TB_TAXA_LOCAL_TRANSPORTADOR A 
LEFT JOIN TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
LEFT JOIN TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
LEFT JOIN TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX
LEFT JOIN TB_ITEM_DESPESA F ON F.ID_ITEM_DESPESA = A.ID_ITEM_DESPESA
        WHERE ID_TRANSPORTADOR =  " & Request.QueryString("id") & "  " & FILTRO
            dgvTaxas.DataBind()
        End If

    End Sub


    Private Sub btnSalvarNovo_Click(sender As Object, e As EventArgs) Handles btnSalvarNovo.Click
        divErroNovo.Visible = False
        divSuccessNovo.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If v.ValidaData(txtValidadeInicialTaxaNovo.Text) = False Then
            divErroNovo.Visible = True
            lblmsgErroNovo.Text = "Data Inválida."
        ElseIf ddlTransportadorTaxaNovo.SelectedValue = 0 Or ddlPortoTaxaNovo.SelectedValue = 0 Or ddlComexTaxaNovo.SelectedValue = 0 Or ddlViaTransporteNovo.SelectedValue = 0 Or ddlBaseCalculoNovo.SelectedValue = 0 Or ddlMoedaNovo.SelectedValue = 0 Or ddlDespesaTaxaNovo.SelectedValue = 0 Or txtValorTaxaLocalNovo.Text = "" Or txtValidadeInicialTaxaNovo.Text = "" Then
            divErroNovo.Visible = True
            lblmsgErroNovo.Text = "Preencha os campos obrigatórios."

        Else


            If txtIDTaxaNovo.Text = "" Then

                txtValorTaxaLocalNovo.Text = txtValorTaxaLocalNovo.Text.Replace(".", "")
                txtValorTaxaLocalNovo.Text = txtValorTaxaLocalNovo.Text.Replace(",", ".")

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroNovo.Visible = True
                    lblmsgErroNovo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub
                Else

                    ds = Con.ExecutarQuery("INSERT INTO TB_TAXA_LOCAL_TRANSPORTADOR (ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO ) VALUES (" & ddlTransportadorTaxaNovo.SelectedValue & " , " & ddlPortoTaxaNovo.SelectedValue & "," & ddlComexTaxaNovo.SelectedValue & " , " & ddlViaTransporteNovo.SelectedValue & " , " & ddlDespesaTaxaNovo.SelectedValue & ", '" & txtValorTaxaLocalNovo.Text & "', convert(date,'" & txtValidadeInicialTaxaNovo.Text & "',103)," & ddlMoedaNovo.SelectedValue & "," & ddlBaseCalculoNovo.SelectedValue & ")")
                    lblmsgSuccess.Text = "Registro cadastrado/atualizado com sucesso!"
                    divSuccess.Visible = True
                    Call Limpar(Me)
                    dgvTaxas.DataBind()
                    ddlTransportadorTaxaNovo.SelectedValue = Request.QueryString("id")

                End If

            End If


        End If

        Con.Fechar()
    End Sub
    Public Sub Limpar(ByVal controlP As Control)

        txtIDTaxa.Text = ""
        ddlTransportadorTaxa.SelectedValue = 0
        ddlPortoTaxa.SelectedValue = 0
        ddlComexTaxa.SelectedValue = 0
        ddlViaTransporte.SelectedValue = 0
        ddlDespesaTaxa.SelectedValue = 0
        txtValidadeInicialTaxa.Text = ""
        txtValorTaxaLocal.Text = ""
        ddlMoeda.SelectedValue = 0
        ddlBaseCalculo.SelectedValue = 0

        txtIDTaxaNovo.Text = ""
        ddlTransportadorTaxaNovo.SelectedValue = 0
        ddlPortoTaxaNovo.SelectedValue = 0
        ddlComexTaxaNovo.SelectedValue = 0
        ddlViaTransporteNovo.SelectedValue = 0
        ddlDespesaTaxaNovo.SelectedValue = 0
        txtValidadeInicialTaxaNovo.Text = ""
        txtValorTaxaLocalNovo.Text = ""
        ddlMoedaNovo.SelectedValue = 0
        ddlBaseCalculoNovo.SelectedValue = 0

    End Sub
    Private Sub dgvTaxas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxas.RowCommand
        divErro.Visible = False
        divSuccess.Visible = False
        divExcluir_Success.Visible = False
        divExcluir_Erro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "visualizar" Then

            Dim id As String = e.CommandArgument

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & id)
            If ds.Tables(0).Rows.Count > 0 Then

                txtIDTaxa.Text = ds.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR").ToString()
                ddlTransportadorTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR")
                ddlPortoTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO")
                ddlComexTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_COMEX")
                ddlViaTransporte.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                ddlDespesaTaxa.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                Dim data As Date = ds.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")
                data = data.ToString("dd-MM-yyyy")
                txtValidadeInicialTaxa.Text = data
                txtValorTaxaLocal.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA").ToString()
                ddlMoeda.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                ddlBaseCalculo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO")
            End If
            Con.Fechar()

            mpe.Show()

        ElseIf e.CommandName = "Excluir" Then

            Dim ds As DataSet

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblExcluir_Erro.Text = "Usuário não tem permissão para realizar exclusões"
                divExcluir_Erro.Visible = True
            Else
                Dim id As String = e.CommandArgument


                Con.ExecutarQuery("DELETE FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR =" & id)

                dgvTaxas.DataBind()
                divExcluir_Success.Visible = True

            End If
        ElseIf e.CommandName = "Duplicar" Then


            Dim id As String = e.CommandArgument

            Con.ExecutarQuery("INSERT INTO TB_TAXA_LOCAL_TRANSPORTADOR (ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO ) SELECT ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & id)
            lblmsgSuccess.Text = "Registro duplicado com sucesso!"
            divSuccess.Visible = True
            dgvTaxas.DataBind()
        End If

    End Sub

    Protected Sub dgvTaxas_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        mpe.Hide()

        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvTaxas.DataSource = Session("TaskTable")
            dgvTaxas.DataBind()
            dgvTaxas.HeaderRow.TableSection = TableRowSection.TableHeader
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

    Private Sub btnFecharNovo_Click(sender As Object, e As EventArgs) Handles btnFecharNovo.Click
        divErroNovo.Visible = False
        divSuccessNovo.Visible = False
        Call Limpar(Me)
        dgvTaxas.DataBind()
        mpeNovo.Hide()
    End Sub

    'Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
    '    Call Limpar(Me)
    '    dgvTaxas.DataBind()
    '    mpe.Hide()
    'End Sub
End Class