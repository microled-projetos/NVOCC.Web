Imports System.Web.Services

Public Class TaxasLocaisArmador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

                Response.Redirect("Default.aspx")
            Else

                dsTaxas.SelectParameters("ID").DefaultValue = Request.QueryString("id")
                dgvTaxas.DataBind()
                ddlTransportadorTaxaNovo.SelectedValue = Request.QueryString("id")
            End If

        Else
            Response.Redirect("Default.aspx")
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


                ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Usuário não possui permissão para alterar."
                        Exit Sub
                    Else
                        ds = Con.ExecutarQuery("UPDATE TB_TAXA_LOCAL_TRANSPORTADOR SET ID_TRANSPORTADOR = " & ddlTransportadorTaxa.SelectedValue & ",ID_MOEDA =  " & ddlMoeda.SelectedValue & ",ID_BASE_CALCULO =  " & ddlBaseCalculo.SelectedValue & ",ID_PORTO =  " & ddlPortoTaxa.SelectedValue & ",ID_TIPO_COMEX = " & ddlComexTaxa.SelectedValue & ",ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & ",ID_ITEM_DESPESA = " & ddlDespesaTaxa.SelectedValue & ", VL_TAXA_LOCAL_COMPRA = '" & txtValorTaxaLocal.Text & "', DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicialTaxa.Text & "',103) FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & txtIDTaxa.Text)
                        divSuccess.Visible = True
                        dgvTaxas.DataBind()
                        'mpe.Show()
                    End If
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                End If


            End If


        End If

        Con.Fechar()
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

                ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                        divErroNovo.Visible = True
                        lblmsgErroNovo.Text = "Usuário não possui permissão para cadastrar."
                        Exit Sub
                    Else

                        ds = Con.ExecutarQuery("INSERT INTO TB_TAXA_LOCAL_TRANSPORTADOR (ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO ) VALUES (" & ddlTransportadorTaxaNovo.SelectedValue & " , " & ddlPortoTaxaNovo.SelectedValue & "," & ddlComexTaxaNovo.SelectedValue & " , " & ddlViaTransporteNovo.SelectedValue & " , " & ddlDespesaTaxaNovo.SelectedValue & ", '" & txtValorTaxaLocalNovo.Text & "', convert(date,'" & txtValidadeInicialTaxaNovo.Text & "',103)," & ddlMoedaNovo.SelectedValue & "," & ddlBaseCalculoNovo.SelectedValue & ")")
                        divSuccessNovo.Visible = True
                        Call Limpar(Me)
                        dgvTaxas.DataBind()
                        ddlTransportadorTaxaNovo.SelectedValue = Request.QueryString("id")

                    End If
                Else

                    divErroNovo.Visible = True
                    lblmsgErroNovo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub
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

            ds = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1024 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then
                    Dim id As String = e.CommandArgument


                    Con.ExecutarQuery("DELETE FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR =" & id)

                    dgvTaxas.DataBind()
                    divExcluir_Success.Visible = True


                Else
                    lblExcluir_Erro.Text = "Usuário não tem permissão para realizar exclusões"
                    divExcluir_Erro.Visible = True
                End If
            End If


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