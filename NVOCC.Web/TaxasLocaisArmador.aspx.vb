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
        ElseIf ddlTransportadorTaxa.SelectedValue = 0 Or ddlPortoTaxa.SelectedValue = 0 Or ddlComexTaxa.SelectedValue = 0 Or ddlViaTransporte.SelectedValue = 0 Or ddlBaseCalculo.SelectedValue = 0 Or ddlMoeda.SelectedValue = 0 Or ddlDespesaTaxa.SelectedValue = 0 Or txtValorTaxaLocal.Text = "" Or txtValidadeInicialTaxa.Text = "" Or ddlOrigemPagamento.SelectedValue = 0 Then
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
                    Con.ExecutarQuery("UPDATE TB_TAXA_LOCAL_TRANSPORTADOR SET ID_TRANSPORTADOR = " & ddlTransportadorTaxa.SelectedValue & ",ID_MOEDA =  " & ddlMoeda.SelectedValue & ",ID_BASE_CALCULO =  " & ddlBaseCalculo.SelectedValue & ",ID_PORTO =  " & ddlPortoTaxa.SelectedValue & ",ID_TIPO_COMEX = " & ddlComexTaxa.SelectedValue & ",ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & ",ID_ITEM_DESPESA = " & ddlDespesaTaxa.SelectedValue & ", VL_TAXA_LOCAL_COMPRA = '" & txtValorTaxaLocal.Text & "', DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicialTaxa.Text & "',103), ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento.SelectedValue & " FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & txtIDTaxa.Text)
                    lblmsgSuccess.Text = "Registro cadastrado/atualizado com sucesso!"

                    txtValorTaxaLocal.Text = txtValorTaxaLocal.Text.Replace(".", ",")

                    divSuccess.Visible = True
                    dgvTaxas.DataBind()
                    'mpe.Show()
                End If


            End If


        End If

        Con.Fechar()
    End Sub

    Sub Pesquisa()
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
a.ID_TRANSPORTADOR,
a.ID_PORTO, b.NM_PORTO,
a.ID_TIPO_COMEX, d.NM_TIPO_COMEX,
a.ID_VIATRANSPORTE, c.NM_VIATRANSPORTE,
a.ID_ITEM_DESPESA, f.NM_ITEM_DESPESA,
a.VL_TAXA_LOCAL_COMPRA,
a.DT_VALIDADE_INICIAL, e.NM_BASE_CALCULO_TAXA, g.SIGLA_MOEDA
FROM
            TB_TAXA_LOCAL_TRANSPORTADOR a 
Left Join TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
Left Join TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
Left Join TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX
Left Join TB_ITEM_DESPESA F ON F.ID_ITEM_DESPESA = A.ID_ITEM_DESPESA
Left Join TB_BASE_CALCULO_TAXA E ON E.ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO
Left Join TB_MOEDA G ON G.ID_MOEDA = A.ID_MOEDA     
        WHERE ID_TRANSPORTADOR =  " & Request.QueryString("id") & "  " & FILTRO
            dgvTaxas.DataBind()
        End If
    End Sub
    Private Sub txtConsulta_TextChanged(sender As Object, e As EventArgs) Handles txtConsulta.TextChanged

        Pesquisa()
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
        ElseIf ddlTransportadorTaxaNovo.SelectedValue = 0 Or ddlPortoTaxaNovo.SelectedValue = 0 Or ddlComexTaxaNovo.SelectedValue = 0 Or ddlViaTransporteNovo.SelectedValue = 0 Or ddlBaseCalculoNovo.SelectedValue = 0 Or ddlMoedaNovo.SelectedValue = 0 Or ddlDespesaTaxaNovo.SelectedValue = 0 Or txtValorTaxaLocalNovo.Text = "" Or txtValidadeInicialTaxaNovo.Text = "" Or ddlOrigemPagamentoNovo.SelectedValue = 0 Then
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

                    ds = Con.ExecutarQuery("INSERT INTO TB_TAXA_LOCAL_TRANSPORTADOR (ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,ID_ORIGEM_PAGAMENTO) VALUES (" & ddlTransportadorTaxaNovo.SelectedValue & " , " & ddlPortoTaxaNovo.SelectedValue & "," & ddlComexTaxaNovo.SelectedValue & " , " & ddlViaTransporteNovo.SelectedValue & " , " & ddlDespesaTaxaNovo.SelectedValue & ", '" & txtValorTaxaLocalNovo.Text & "', convert(date,'" & txtValidadeInicialTaxaNovo.Text & "',103)," & ddlMoedaNovo.SelectedValue & "," & ddlBaseCalculoNovo.SelectedValue & ", " & ddlOrigemPagamentoNovo.SelectedValue & ")")
                    lblmsgSuccess.Text = "Registro cadastrado/atualizado com sucesso!"
                    divSuccess.Visible = True
                    txtValorTaxaLocalNovo.Text = txtValorTaxaLocalNovo.Text.Replace(".", ",")

                    Call Limpar(Me)
                    Pesquisa()
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
        ddlOrigemPagamento.SelectedValue = 0

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
        ddlOrigemPagamentoNovo.SelectedValue = 0
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

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & id)
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
                ddlOrigemPagamento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
            End If
            Con.Fechar()

            mpe.Show()
            Pesquisa()
        ElseIf e.CommandName = "Excluir" Then

            Dim ds As DataSet

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblExcluir_Erro.Text = "Usuário não tem permissão para realizar exclusões"
                divExcluir_Erro.Visible = True
            Else
                Dim id As String = e.CommandArgument


                Con.ExecutarQuery("DELETE FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR =" & id)

                Pesquisa()
                divExcluir_Success.Visible = True

            End If
        ElseIf e.CommandName = "Duplicar" Then


            Dim id As String = e.CommandArgument

            Con.ExecutarQuery("INSERT INTO TB_TAXA_LOCAL_TRANSPORTADOR (ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,ID_ORIGEM_PAGAMENTO ) SELECT ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,ID_ORIGEM_PAGAMENTO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & id)
            lblmsgSuccess.Text = "Registro duplicado com sucesso!"
            divSuccess.Visible = True
            Pesquisa()
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
        Pesquisa()
        mpeNovo.Hide()
    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Call Limpar(Me)
        Pesquisa()
        mpeNovo.Hide()
        mpe.Hide()
    End Sub

    Private Sub lkProximo_Click(sender As Object, e As EventArgs) Handles lkProximo.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim LinhaAtual As Integer = 0
        Dim ProximaLinha As Integer = 0
        Dim PrimeiraTaxa As String = 0


        Dim filtro As String = ""
        If ddlConsulta.SelectedValue = 1 Then
            filtro = " AND NM_PORTO LIKE '%" & txtConsulta.Text & "%' "
        ElseIf ddlConsulta.SelectedValue = 3 Then
            filtro = " AND NM_TIPO_COMEX LIKE '%" & txtConsulta.Text & "%' "
        Else
            filtro = " AND NM_VIATRANSPORTE LIKE '%" & txtConsulta.Text & "%' "

        End If



        Dim ds As DataSet = Con.ExecutarQuery("SELECT ROW_NUMBER() OVER(ORDER BY ID_TAXA_LOCAL_TRANSPORTADOR) AS num,ID_TAXA_LOCAL_TRANSPORTADOR FROM TB_TAXA_LOCAL_TRANSPORTADOR A
Left Join TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
Left Join TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
Left Join TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX WHERE ID_TRANSPORTADOR =  " & Request.QueryString("id") & filtro)
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiraTaxa = ds.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") = txtIDTaxa.Text Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
                    If dsTaxa.Tables(0).Rows.Count > 0 Then

                        txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR").ToString()
                        ddlTransportadorTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TRANSPORTADOR")
                        ddlPortoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_PORTO")
                        ddlComexTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_COMEX")
                        ddlViaTransporte.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                        ddlDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        Dim data As Date = dsTaxa.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")
                        data = data.ToString("dd-MM-yyyy")
                        txtValidadeInicialTaxa.Text = data
                        txtValorTaxaLocal.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA").ToString()
                        ddlMoeda.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA")
                        ddlBaseCalculo.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO")
                        ddlOrigemPagamento.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                    End If

                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & PrimeiraTaxa)
                    If dsTaxa.Tables(0).Rows.Count > 0 Then

                        txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR").ToString()
                        ddlTransportadorTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TRANSPORTADOR")
                        ddlPortoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_PORTO")
                        ddlComexTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_COMEX")
                        ddlViaTransporte.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                        ddlDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        Dim data As Date = dsTaxa.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")
                        data = data.ToString("dd-MM-yyyy")
                        txtValidadeInicialTaxa.Text = data
                        txtValorTaxaLocal.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA").ToString()
                        ddlMoeda.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA")
                        ddlBaseCalculo.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO")
                        ddlOrigemPagamento.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")

                    End If
                End If

            Next

            'Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
            'url = String.Format(url, ds.Tables(0).Rows(0).Item("ID_BL"))
            'Response.Redirect(url)
        End If
    End Sub
    Private Sub lkAnterior_Click(sender As Object, e As EventArgs) Handles lkAnterior.Click
        divErro.Visible = False
        divSuccess.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim LinhaAtual As Integer = 0
        Dim ProximaLinha As Integer = 0
        Dim PrimeiraTaxa As String = 0

        Dim filtro As String = ""
        If ddlConsulta.SelectedValue = 1 Then
            filtro = " AND NM_PORTO LIKE '%" & txtConsulta.Text & "%' "
        ElseIf ddlConsulta.SelectedValue = 3 Then
            filtro = " AND NM_TIPO_COMEX LIKE '%" & txtConsulta.Text & "%' "
        Else
            filtro = " AND NM_VIATRANSPORTE LIKE '%" & txtConsulta.Text & "%' "

        End If



        Dim ds As DataSet = Con.ExecutarQuery("SELECT ROW_NUMBER() OVER(ORDER BY ID_TAXA_LOCAL_TRANSPORTADOR desc) AS num,ID_TAXA_LOCAL_TRANSPORTADOR FROM TB_TAXA_LOCAL_TRANSPORTADOR A
Left Join TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
Left Join TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
Left Join TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX WHERE ID_TRANSPORTADOR =  " & Request.QueryString("id") & filtro)
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiraTaxa = ds.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") = txtIDTaxa.Text Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
                    If dsTaxa.Tables(0).Rows.Count > 0 Then

                        txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR").ToString()
                        ddlTransportadorTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TRANSPORTADOR")
                        ddlPortoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_PORTO")
                        ddlComexTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_COMEX")
                        ddlViaTransporte.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                        ddlDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        Dim data As Date = dsTaxa.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")
                        data = data.ToString("dd-MM-yyyy")
                        txtValidadeInicialTaxa.Text = data
                        txtValorTaxaLocal.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA").ToString()
                        ddlMoeda.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA")
                        ddlBaseCalculo.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO")
                        ddlOrigemPagamento.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                    End If

                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & PrimeiraTaxa)
                    If dsTaxa.Tables(0).Rows.Count > 0 Then

                        txtIDTaxa.Text = dsTaxa.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR").ToString()
                        ddlTransportadorTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TRANSPORTADOR")
                        ddlPortoTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_PORTO")
                        ddlComexTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_TIPO_COMEX")
                        ddlViaTransporte.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_VIATRANSPORTE")
                        ddlDespesaTaxa.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                        Dim data As Date = dsTaxa.Tables(0).Rows(0).Item("DT_VALIDADE_INICIAL")
                        data = data.ToString("dd-MM-yyyy")
                        txtValidadeInicialTaxa.Text = data
                        txtValorTaxaLocal.Text = dsTaxa.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA").ToString()
                        ddlMoeda.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_MOEDA")
                        ddlBaseCalculo.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_BASE_CALCULO")
                        ddlOrigemPagamento.SelectedValue = dsTaxa.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")

                    End If
                End If

            Next

            'Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
            'url = String.Format(url, ds.Tables(0).Rows(0).Item("ID_BL"))
            'Response.Redirect(url)
        End If
    End Sub

End Class