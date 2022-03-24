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

        If txtQtdBaseCalculo.Text = "" Then
            txtQtdBaseCalculo.Text = 0
        End If

        If v.ValidaData(txtValidadeInicialTaxa.Text) = False Then
            divErro.Visible = True
            lblmsgErro.Text = "Data Inválida."
        ElseIf ddlTransportadorTaxa.SelectedValue = 0 Or ddlPortoTaxa.SelectedValue = 0 Or ddlComexTaxa.SelectedValue = 0 Or ddlViaTransporte.SelectedValue = 0 Or ddlBaseCalculo.SelectedValue = 0 Or ddlMoeda.SelectedValue = 0 Or ddlDespesaTaxa.SelectedValue = 0 Or txtValorTaxaLocal.Text = "" Or txtValidadeInicialTaxa.Text = "" Or ddlOrigemPagamento.SelectedValue = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Preencha os campos obrigatórios."

        ElseIf (ddlBaseCalculo.SelectedValue = 38 Or ddlBaseCalculo.SelectedValue = 40 Or ddlBaseCalculo.SelectedValue = 41) And txtQtdBaseCalculo.Text = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Necessário informar quantidade para base de cálculo."

        Else



            If txtIDTaxa.Text <> "" Then
                lblValorNovo.Text = txtValorTaxaLocal.Text
                lblValorNovo.Text = lblValorNovo.Text.Replace(".", "")
                lblValorNovo.Text = lblValorNovo.Text.Replace(",", ".")
                lblValorNovo.Text = lblValorNovo.Text.Replace(".", ",")

                ds = Con.ExecutarQuery("SELECT ISNULL(VL_TAXA_LOCAL_COMPRA,0)VL_TAXA_LOCAL_COMPRA FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & txtIDTaxa.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    lblValorAntigo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA")
                    lblValorAntigo.Text = lblValorAntigo.Text.Replace(".", "")
                    lblValorAntigo.Text = lblValorAntigo.Text.Replace(",", ".")
                    lblValorAntigo.Text = lblValorAntigo.Text.Replace(".", ",")

                    divTaxaAntiga.Attributes.CssStyle.Add("display", "block")
                End If


                txtValorTaxaLocal.Text = txtValorTaxaLocal.Text.Replace(".", "")
                txtValorTaxaLocal.Text = txtValorTaxaLocal.Text.Replace(",", ".")


                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else
                    Con.ExecutarQuery("UPDATE TB_TAXA_LOCAL_TRANSPORTADOR SET ID_TRANSPORTADOR = " & ddlTransportadorTaxa.SelectedValue & ",ID_MOEDA =  " & ddlMoeda.SelectedValue & ",ID_BASE_CALCULO =  " & ddlBaseCalculo.SelectedValue & ",ID_PORTO =  " & ddlPortoTaxa.SelectedValue & ",ID_TIPO_COMEX = " & ddlComexTaxa.SelectedValue & ",ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & ",ID_ITEM_DESPESA = " & ddlDespesaTaxa.SelectedValue & ", VL_TAXA_LOCAL_COMPRA = " & txtValorTaxaLocal.Text & ", DT_VALIDADE_INICIAL = convert(date,'" & txtValidadeInicialTaxa.Text & "',103), ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento.SelectedValue & ", QTD_BASE_CALCULO = " & txtQtdBaseCalculo.Text & " FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & txtIDTaxa.Text)
                    lblmsgSuccess.Text = "Registro cadastrado/atualizado com sucesso!"
                    divSuccess.Visible = True

                    ds = Con.ExecutarQuery("SELECT ID_TABELA_FRETE_TAXA FROM TB_TABELA_FRETE_TAXA A
INNER JOIN TB_FRETE_TRANSPORTADOR B ON A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR
WHERE B.ID_TRANSPORTADOR = " & Request.QueryString("id") & " AND VL_TAXA_COMPRA < " & txtValorTaxaLocal.Text & "
AND (CASE 
WHEN B.ID_TIPO_COMEX = 1 THEN 
ID_PORTO_DESTINO
WHEN  B.ID_TIPO_COMEX = 2 THEN 
ID_PORTO_ORIGEM 
END) = " & ddlPortoTaxa.SelectedValue & "
  AND ID_TIPO_COMEX = " & ddlComexTaxa.SelectedValue & " AND ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue & " AND ID_ITEM_DESPESA  = " & ddlDespesaTaxa.SelectedValue)

                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each linha As DataRow In ds.Tables(0).Rows
                            Con.ExecutarQuery("UPDATE TB_TABELA_FRETE_TAXA SET VL_TAXA_COMPRA = " & txtValorTaxaLocal.Text & ",VL_TAXA_VENDA =  CASE WHEN ISNULL(VL_TAXA_VENDA,0) = 0 THEN " & txtValorTaxaLocal.Text & " WHEN VL_TAXA_VENDA < " & txtValorTaxaLocal.Text & " THEN " & txtValorTaxaLocal.Text & " ELSE VL_TAXA_VENDA END WHERE ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA"))
                        Next
                    End If

                    DeletaTaxaIgualTarifario(Request.QueryString("id"), ddlComexTaxa.SelectedValue, ddlDespesaTaxa.SelectedValue, ddlViaTransporte.SelectedValue, ddlPortoTaxa.SelectedValue)

                    txtValorTaxaLocal.Text = txtValorTaxaLocal.Text.Replace(".", ",")

                    dgvTaxas.DataBind()

                    If lblValorNovo.Text > lblValorAntigo.Text Then
                        lblPorto.Text = ddlPortoTaxa.SelectedValue
                        lblComex.Text = ddlComexTaxa.SelectedValue
                        lblVia.Text = ddlViaTransporte.SelectedValue
                        lblItemDespesa.Text = ddlDespesaTaxa.SelectedValue
                        AtualizaGridAjuste()

                        'mpeAjusta.Show()
                        '  mpe.Hide()
                    End If

                End If


            End If


        End If

        Con.Fechar()
    End Sub

    Sub DeletaTaxaIgualTarifario(ID_TRANSPORTADOR As String, ID_TIPO_COMEX As String, ID_ITEM_DESPESA As String, ID_VIATRANSPORTE As String, ID_PORTO As String)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds1 As DataSet
        Dim ds2 As DataSet

        Dim ds0 As DataSet = Con.ExecutarQuery("SELECT DISTINCT B.ID_FRETE_TRANSPORTADOR FROM TB_TABELA_FRETE_TAXA A 
INNER JOIN TB_FRETE_TRANSPORTADOR B ON A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR  WHERE  B.ID_TRANSPORTADOR = " & ID_TRANSPORTADOR & " AND (CASE  WHEN B.ID_TIPO_COMEX = 1 THEN  B.ID_PORTO_DESTINO WHEN  B.ID_TIPO_COMEX = 2 THEN  B.ID_PORTO_ORIGEM  END) = " & ID_PORTO & " AND B.ID_TIPO_COMEX =  " & ID_TIPO_COMEX & " AND B.ID_VIATRANSPORTE = " & ID_VIATRANSPORTE & " AND A.ID_ITEM_DESPESA  =  " & ID_ITEM_DESPESA & " ")
        If ds0.Tables(0).Rows.Count > 0 Then
            For Each linha0 As DataRow In ds0.Tables(0).Rows
                ds1 = Con.ExecutarQuery("SELECT a.ID_FRETE_TRANSPORTADOR,A.ID_ITEM_DESPESA, COUNT(*)QTD FROM TB_TABELA_FRETE_TAXA A INNER JOIN TB_FRETE_TRANSPORTADOR B On A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR WHERE  b.ID_TRANSPORTADOR = " & ID_TRANSPORTADOR & " And (CASE  WHEN B.ID_TIPO_COMEX = 1 THEN  ID_PORTO_DESTINO WHEN  B.ID_TIPO_COMEX = 2 THEN  ID_PORTO_ORIGEM  END) = " & ID_PORTO & "   And ID_TIPO_COMEX = " & ID_TIPO_COMEX & " And ID_VIATRANSPORTE = " & ID_VIATRANSPORTE & " And ID_ITEM_DESPESA  = " & ID_ITEM_DESPESA & " AND B.ID_FRETE_TRANSPORTADOR = " & linha0.Item("ID_FRETE_TRANSPORTADOR") & " Group BY A.ID_FRETE_TRANSPORTADOR, a.ID_ITEM_DESPESA, a.ID_MOEDA_COMPRA, a.ID_MOEDA_VENDA, a.ID_BASE_CALCULO_TAXA ORDER BY QTD DESC")
                If ds1.Tables(0).Rows.Count > 0 Then
                    For Each linha1 As DataRow In ds1.Tables(0).Rows
                        If linha1.Item("QTD") > 1 Then

                            ds2 = Con.ExecutarQuery("SELECT  ID_TABELA_FRETE_TAXA, VL_TAXA_VENDA FROM TB_TABELA_FRETE_TAXA A INNER JOIN TB_FRETE_TRANSPORTADOR B ON A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR WHERE  B.ID_TRANSPORTADOR = " & ID_TRANSPORTADOR & " AND (CASE  WHEN B.ID_TIPO_COMEX = 1 THEN  ID_PORTO_DESTINO WHEN  B.ID_TIPO_COMEX = 2 THEN  ID_PORTO_ORIGEM  END) = " & ID_PORTO & " AND ID_TIPO_COMEX =  " & ID_TIPO_COMEX & " AND ID_VIATRANSPORTE = " & ID_VIATRANSPORTE & " AND ID_ITEM_DESPESA  = " & ID_ITEM_DESPESA & " AND B.ID_FRETE_TRANSPORTADOR = " & linha0.Item("ID_FRETE_TRANSPORTADOR") & " ORDER BY VL_TAXA_VENDA DESC")

                            If ds2.Tables(0).Rows.Count > 0 Then
                                For contador As Integer = 1 To ds2.Tables(0).Rows.Count - 1
                                    Con.ExecutarQuery("DELETE FROM TB_TABELA_FRETE_TAXA WHERE ID_TABELA_FRETE_TAXA = " & ds2.Tables(0).Rows(contador).Item("ID_TABELA_FRETE_TAXA"))
                                Next
                            End If
                        End If
                    Next
                End If

            Next
        End If

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
        WHERE ID_TRANSPORTADOR =  " & Request.QueryString("id") & "  " & FILTRO & " ORDER BY B.NM_PORTO"
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

        If txtQtdBaseCalculoNovo.Text = "" Then
            txtQtdBaseCalculoNovo.Text = 0
        End If

        If v.ValidaData(txtValidadeInicialTaxaNovo.Text) = False Then
            divErroNovo.Visible = True
            lblmsgErroNovo.Text = "Data Inválida."
        ElseIf ddlTransportadorTaxaNovo.SelectedValue = 0 Or ddlPortoTaxaNovo.SelectedValue = 0 Or ddlComexTaxaNovo.SelectedValue = 0 Or ddlViaTransporteNovo.SelectedValue = 0 Or ddlBaseCalculoNovo.SelectedValue = 0 Or ddlMoedaNovo.SelectedValue = 0 Or ddlDespesaTaxaNovo.SelectedValue = 0 Or txtValorTaxaLocalNovo.Text = "" Or txtValidadeInicialTaxaNovo.Text = "" Or ddlOrigemPagamentoNovo.SelectedValue = 0 Then
            divErroNovo.Visible = True
            lblmsgErroNovo.Text = "Preencha os campos obrigatórios."

        ElseIf (ddlBaseCalculoNovo.SelectedValue = 38 Or ddlBaseCalculoNovo.SelectedValue = 40 Or ddlBaseCalculoNovo.SelectedValue = 41) And txtQtdBaseCalculoNovo.Text = 0 Then
            divErroNovo.Visible = True
            lblmsgErroNovo.Text = "Necessário informar quantidade para base de cálculo."

        Else


            If txtIDTaxaNovo.Text = "" Then
                lblValorNovo.Text = txtValorTaxaLocalNovo.Text

                txtValorTaxaLocalNovo.Text = txtValorTaxaLocalNovo.Text.Replace(".", "")
                txtValorTaxaLocalNovo.Text = txtValorTaxaLocalNovo.Text.Replace(",", ".")

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1024 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroNovo.Visible = True
                    lblmsgErroNovo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub
                Else


                    ds = Con.ExecutarQuery("SELECT ISNULL(A.VL_TAXA_LOCAL_COMPRA,0)VL_TAXA_LOCAL_COMPRA
FROM TB_TAXA_LOCAL_TRANSPORTADOR A
INNER JOIN (
SELECT ID_ITEM_DESPESA,ID_BASE_CALCULO, MAX(DT_VALIDADE_INICIAL) AS DT_VALIDADE_INICIAL
FROM TB_TAXA_LOCAL_TRANSPORTADOR
WHERE ID_PORTO =  " & ddlPortoTaxaNovo.SelectedValue & " AND ID_TRANSPORTADOR = " & Request.QueryString("id") & " AND ID_ITEM_DESPESA = " & ddlDespesaTaxaNovo.SelectedValue & " AND ID_TIPO_COMEX = " & ddlComexTaxaNovo.SelectedValue & " AND ID_VIATRANSPORTE = " & ddlViaTransporteNovo.SelectedValue & " AND CONVERT(DATE,DT_VALIDADE_INICIAL,103) <= GETDATE()
GROUP BY  ID_ITEM_DESPESA,ID_BASE_CALCULO) B
ON  A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA
AND A.DT_VALIDADE_INICIAL = B.DT_VALIDADE_INICIAL
WHERE ID_PORTO = " & ddlPortoTaxaNovo.SelectedValue & " AND ID_TRANSPORTADOR = " & Request.QueryString("id") & " AND A.ID_ITEM_DESPESA = " & ddlDespesaTaxaNovo.SelectedValue & " AND ID_TIPO_COMEX = " & ddlComexTaxaNovo.SelectedValue & " AND ID_VIATRANSPORTE = " & ddlViaTransporteNovo.SelectedValue & " AND CONVERT(DATE,A.DT_VALIDADE_INICIAL,103) <= GETDATE() ")
                    If ds.Tables(0).Rows.Count > 0 Then
                        lblValorAntigo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA")
                        divTaxaAntiga.Attributes.CssStyle.Add("display", "block")
                    End If


                    Con.ExecutarQuery("INSERT INTO TB_TAXA_LOCAL_TRANSPORTADOR (ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,ID_ORIGEM_PAGAMENTO,QTD_BASE_CALCULO) VALUES (" & ddlTransportadorTaxaNovo.SelectedValue & " , " & ddlPortoTaxaNovo.SelectedValue & "," & ddlComexTaxaNovo.SelectedValue & " , " & ddlViaTransporteNovo.SelectedValue & " , " & ddlDespesaTaxaNovo.SelectedValue & ", '" & txtValorTaxaLocalNovo.Text & "', convert(date,'" & txtValidadeInicialTaxaNovo.Text & "',103)," & ddlMoedaNovo.SelectedValue & "," & ddlBaseCalculoNovo.SelectedValue & ", " & ddlOrigemPagamentoNovo.SelectedValue & "," & txtQtdBaseCalculoNovo.Text & ") ")
                    lblmsgSuccessNovo.Text = "Registro cadastrado/atualizado com sucesso!"
                    divSuccessNovo.Visible = True


                    ds = Con.ExecutarQuery("SELECT ID_TABELA_FRETE_TAXA FROM TB_TABELA_FRETE_TAXA A
INNER JOIN TB_FRETE_TRANSPORTADOR B ON A.ID_FRETE_TRANSPORTADOR = B.ID_FRETE_TRANSPORTADOR
WHERE B.ID_TRANSPORTADOR = " & Request.QueryString("id") & "  AND VL_TAXA_COMPRA < " & txtValorTaxaLocalNovo.Text & "
AND (CASE 
WHEN B.ID_TIPO_COMEX = 1 THEN 
ID_PORTO_DESTINO
WHEN  B.ID_TIPO_COMEX = 2 THEN 
ID_PORTO_ORIGEM 
END) = " & ddlPortoTaxaNovo.SelectedValue & "
  AND ID_TIPO_COMEX = " & ddlComexTaxaNovo.SelectedValue & " AND ID_VIATRANSPORTE = " & ddlViaTransporteNovo.SelectedValue & " AND ID_ITEM_DESPESA  = " & ddlDespesaTaxaNovo.SelectedValue)

                    If ds.Tables(0).Rows.Count > 0 Then
                        For Each linha As DataRow In ds.Tables(0).Rows
                            ' Con.ExecutarQuery("UPDATE TB_TABELA_FRETE_TAXA SET VL_TAXA_COMPRA = " & txtValorTaxaLocalNovo.Text & " WHERE ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA"))
                            Con.ExecutarQuery("UPDATE TB_TABELA_FRETE_TAXA SET VL_TAXA_COMPRA = " & txtValorTaxaLocalNovo.Text & ",VL_TAXA_VENDA =  CASE WHEN ISNULL(VL_TAXA_VENDA,0) = 0 THEN " & txtValorTaxaLocalNovo.Text & " WHEN VL_TAXA_VENDA < " & txtValorTaxaLocalNovo.Text & " THEN " & txtValorTaxaLocalNovo.Text & " ELSE VL_TAXA_VENDA END WHERE ID_TABELA_FRETE_TAXA = " & linha.Item("ID_TABELA_FRETE_TAXA"))
                        Next
                    End If

                    DeletaTaxaIgualTarifario(Request.QueryString("id"), ddlComexTaxaNovo.SelectedValue, ddlDespesaTaxaNovo.SelectedValue, ddlViaTransporteNovo.SelectedValue, ddlPortoTaxaNovo.SelectedValue)


                    txtValorTaxaLocalNovo.Text = txtValorTaxaLocalNovo.Text.Replace(".", ",")




                    ddlTransportadorTaxaNovo.SelectedValue = Request.QueryString("id")



                    lblPorto.Text = ddlPortoTaxaNovo.SelectedValue
                    lblComex.Text = ddlComexTaxaNovo.SelectedValue
                    lblVia.Text = ddlViaTransporteNovo.SelectedValue
                    lblItemDespesa.Text = ddlDespesaTaxaNovo.SelectedValue
                    AtualizaGridAjuste()

                    Call Limpar(Me)

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
        txtQtdBaseCalculo.Text = ""


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
        txtQtdBaseCalculoNovo.Text = ""

    End Sub
    Private Sub dgvTaxas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxas.RowCommand
        divErro.Visible = False
        divSuccess.Visible = False
        divExcluir_Success.Visible = False
        divExcluir_Erro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Pesquisa()
        If e.CommandName = "visualizar" Then

            Dim id As String = e.CommandArgument

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & id)
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
                txtQtdBaseCalculo.Text = ds.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString()

                If ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO") = 38 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO") = 40 Or ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO") = 41 Then
                    txtQtdBaseCalculo.Enabled = True
                Else
                    txtQtdBaseCalculo.Enabled = False
                End If

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

            Con.ExecutarQuery("INSERT INTO TB_TAXA_LOCAL_TRANSPORTADOR (ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,ID_ORIGEM_PAGAMENTO,QTD_BASE_CALCULO ) SELECT ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,ID_ORIGEM_PAGAMENTO,QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & id)
            lblmsgSuccess.Text = "Registro duplicado com sucesso!"
            divSuccess.Visible = True
            Pesquisa()

        ElseIf e.CommandName = "Atualiza" Then
            Dim id As String = e.CommandArgument
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_PORTO,VL_TAXA_LOCAL_COMPRA FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & id)
            If ds.Tables(0).Rows.Count > 0 Then

                lblItemDespesa.Text = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA").ToString()
                lblComex.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_COMEX").ToString()
                lblVia.Text = ds.Tables(0).Rows(0).Item("ID_VIATRANSPORTE").ToString()
                lblPorto.Text = ds.Tables(0).Rows(0).Item("ID_PORTO").ToString()
                ' lblValorAntigo.Text = "Sem Informação"
                divTaxaAntiga.Attributes.CssStyle.Add("display", "none")
                lblValorNovo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_LOCAL_COMPRA").ToString()

                AtualizaGridAjuste()

            End If

        End If

    End Sub

    Protected Sub dgvTaxas_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        mpe.Hide()
        Pesquisa()
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



        Dim ds As DataSet = Con.ExecutarQuery("SELECT ROW_NUMBER() OVER(ORDER BY NM_PORTO,ID_TAXA_LOCAL_TRANSPORTADOR) AS num,ID_TAXA_LOCAL_TRANSPORTADOR FROM TB_TAXA_LOCAL_TRANSPORTADOR A
Left Join TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
Left Join TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
Left Join TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX WHERE ID_TRANSPORTADOR =  " & Request.QueryString("id") & filtro & " order by NM_PORTO,ID_TAXA_LOCAL_TRANSPORTADOR ")
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiraTaxa = ds.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") = txtIDTaxa.Text Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
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
                        txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString()
                    End If

                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & PrimeiraTaxa)
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
                        txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString()
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



        Dim ds As DataSet = Con.ExecutarQuery("SELECT ROW_NUMBER() OVER(ORDER BY NM_PORTO,ID_TAXA_LOCAL_TRANSPORTADOR desc) AS num,ID_TAXA_LOCAL_TRANSPORTADOR FROM TB_TAXA_LOCAL_TRANSPORTADOR A
Left Join TB_PORTO B ON B.ID_PORTO = A.ID_PORTO
Left Join TB_VIATRANSPORTE C ON C.ID_VIATRANSPORTE = A.ID_VIATRANSPORTE
Left Join TB_TIPO_COMEX D ON D.ID_TIPO_COMEX = A.ID_TIPO_COMEX WHERE ID_TRANSPORTADOR =  " & Request.QueryString("id") & filtro & " ORDER BY NM_PORTO,ID_TAXA_LOCAL_TRANSPORTADOR desc ")
        If ds.Tables(0).Rows.Count > 0 Then
            PrimeiraTaxa = ds.Tables(0).Rows(0).Item("ID_TAXA_LOCAL_TRANSPORTADOR")
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR") = txtIDTaxa.Text Then
                    LinhaAtual = linha.Item("num")
                    ProximaLinha = linha.Item("num") + 1
                End If

                If ProximaLinha = linha.Item("num") Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & linha.Item("ID_TAXA_LOCAL_TRANSPORTADOR"))
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
                        txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString()

                    End If

                ElseIf ProximaLinha > ds.Tables(0).Rows.Count Then

                    Dim dsTaxa As DataSet = Con.ExecutarQuery("SELECT ID_TAXA_LOCAL_TRANSPORTADOR, ID_ORIGEM_PAGAMENTO,ID_TRANSPORTADOR,ID_PORTO,ID_TIPO_COMEX,ID_VIATRANSPORTE,ID_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA,DT_VALIDADE_INICIAL,ID_MOEDA,ID_BASE_CALCULO,QTD_BASE_CALCULO FROM TB_TAXA_LOCAL_TRANSPORTADOR WHERE ID_TAXA_LOCAL_TRANSPORTADOR = " & PrimeiraTaxa)
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
                        txtQtdBaseCalculo.Text = dsTaxa.Tables(0).Rows(0).Item("QTD_BASE_CALCULO").ToString()

                    End If
                End If

            Next

            'Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
            'url = String.Format(url, ds.Tables(0).Rows(0).Item("ID_BL"))
            'Response.Redirect(url)
        End If
    End Sub

    Private Sub btnDesmarcar_Click(sender As Object, e As EventArgs) Handles btnDesmarcar.Click
        AtualizaGridAjuste()
        For i As Integer = 0 To Me.dgvAjustaTaxa.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvAjustaTaxa.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = False
        Next
    End Sub

    Private Sub btnMarcar_Click(sender As Object, e As EventArgs) Handles btnMarcar.Click
        AtualizaGridAjuste()
        For i As Integer = 0 To Me.dgvAjustaTaxa.Rows.Count - 1
            Dim ckbSelecionar = CType(Me.dgvAjustaTaxa.Rows(i).FindControl("ckbSelecionar"), CheckBox)
            ckbSelecionar.Checked = True
        Next

    End Sub

    Private Sub btnFecharAjustaTaxa_Click(sender As Object, e As EventArgs) Handles btnFecharAjustaTaxa.Click
        mpeAjusta.Hide()
        Pesquisa()
    End Sub

    Private Sub btnAjustar_Click(sender As Object, e As EventArgs) Handles btnAjustar.Click
        divErro.Visible = False
        divSuccess.Visible = False
        divErroAjusta.Visible = False
        divSuccessAjusta.Visible = False
        lblErroAjusta.Text = ""
        Dim Con As New Conexao_sql
        Con.Conectar()
        For Each linha As GridViewRow In dgvAjustaTaxa.Rows
            Dim check As CheckBox = linha.FindControl("ckbSelecionar")
            If check.Checked Then
                Dim ID As String = CType(linha.FindControl("lblid_cotacao_taxa"), Label).Text
                Dim VL_TAXA_COMPRA As String = CType(linha.FindControl("lblVL_TAXA_COMPRA"), Label).Text
                Dim VL_TAXA_VENDA As String = CType(linha.FindControl("lblVL_TAXA_VENDA"), Label).Text
                Dim ITEM_DESPESA As String = CType(linha.FindControl("lblITEM_DESPESA"), Label).Text
                Dim ARMADOR As String = CType(linha.FindControl("lblARMADOR"), Label).Text
                Dim ID_TRANSPORTADOR As String = CType(linha.FindControl("lblID_TRANSPORTADOR"), Label).Text
                Dim ID_BL As String = CType(linha.FindControl("lblID_BL"), Label).Text

                Dim Profit As Decimal = VL_TAXA_VENDA - VL_TAXA_COMPRA
                Dim ValorNovoVenda As Decimal = lblValorNovo.Text + Profit
                ValorNovoVenda = FormatNumber(ValorNovoVenda, 2)

                Dim ds As DataSet = Con.ExecutarQuery("Select NR_COTACAO,ID_STATUS_COTACAO,ID_COTACAO,NR_PROCESSO_GERADO,ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = (SELECT ID_COTACAO FROM TB_COTACAO_TAXA WHERE ID_COTACAO_TAXA = " & ID & " )")
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") <> 12 Or ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") <> 9 Or ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") <> 15 Then

                        txtMsg.Text = "Prezado(a),<br><br>Informamos que houve alteração de valores na taxa <strong>" & ITEM_DESPESA & "</strong> do armador <strong>" & ARMADOR & "</strong>:<br/><br/>Cotação " & ds.Tables(0).Rows(0).Item("NR_COTACAO") & " -  Processo " & ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO") & " <br/><br/>Valor compra antigo: " & VL_TAXA_COMPRA & " <br/>Valor compra novo: " & lblValorNovo.Text & " <br/><br/>Valor venda antigo: " & VL_TAXA_VENDA & " <br/>Valor venda novo: " & ValorNovoVenda & " <br/>"

                        Con.ExecutarQuery("UPDATE [dbo].[TB_COTACAO_TAXA]  SET VL_TAXA_COMPRA = " & lblValorNovo.Text.ToString.Replace(",", ".") & ", VL_TAXA_VENDA = " & ValorNovoVenda.ToString.Replace(",", ".") & " WHERE ID_COTACAO_TAXA =" & ID)

                        ''EMAIL INTERNO
                        Con.ExecutarQuery("INSERT INTO [dbo].[TB_GER_EMAIL]  (ASSUNTO,CORPO,DT_GERACAO,DT_START,IDTIPOAVISO,IDPROCESSO,IDCLIENTE,TPORIGEM) VALUES ('ALTERAÇÃO DE TAXAS DO ARMADOR','" & txtMsg.Text & "',GETDATE(),GETDATE(),13," & ID_BL & ",0,'OP')")

                        ''EMAIL CLIENTE
                        'Con.ExecutarQuery("INSERT INTO [dbo].[TB_GER_EMAIL] (ASSUNTO,CORPO,DT_GERACAO,DT_START,IDTIPOAVISO,IDPROCESSO,IDCLIENTE,TPORIGEM) VALUES ('ALTERAÇÃO DE TAXAS DO ARMADOR','" & txtMsg.Text & "',GETDATE(),GETDATE(),13," & ID_BL & "," & ds.Tables(0).Rows(0).Item("ID_CLIENTE") & ",'OP')")

                        Dim RotinaUpdate As New RotinaUpdate
                        RotinaUpdate.UpdateTaxas(ds.Tables(0).Rows(0).Item("ID_COTACAO"), ID, ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO"))

                    Else
                        divErroAjusta.Visible = True
                        lblErroAjusta.Text &= "O status da cotação " & ds.Tables(0).Rows(0).Item("NR_COTACAO") & " não permite ajustes! <br/>"
                    End If

                End If
            End If

        Next

        AtualizaGridAjuste()
        Con.Fechar()
    End Sub
    Sub AtualizaGridAjuste()
        divinfo.Visible = False

        Dim ValorAntigo As String = lblValorAntigo.Text
        ValorAntigo = ValorAntigo.ToString.Replace(".", "")
        ValorAntigo = ValorAntigo.ToString.Replace(",", ".")

        Dim ValorNovo As String = lblValorNovo.Text
        ValorNovo = ValorNovo.ToString.Replace(".", "")
        ValorNovo = ValorNovo.ToString.Replace(",", ".")

        Dim Servico As String = ""

        If lblComex.Text = 1 And lblVia.Text = 1 Then
            'AGENCIAMENTO DE IMPORTACAO MARITIMA
            Servico = 1

            'comex = 1
            'ID_PORTO = ddlDestinoFrete.SelectedValue
            'via = 1
        ElseIf lblComex.Text = 2 And lblVia.Text = 1 Then
            'AGENCIAMENTO DE EXPORTACAO MARITIMA
            Servico = 4
            'comex = 2
            'ID_PORTO = ddlOrigemFrete.SelectedValue
            'via = 1
        ElseIf lblComex.Text = 2 And lblVia.Text = 4 Then
            'AGENCIAMENTO DE EXPORTAÇÃO AEREO
            Servico = 5

            'comex = 2
            'ID_PORTO = ddlOrigemFrete.SelectedValue
            'via = 4
        ElseIf lblComex.Text = 1 And lblVia.Text = 4 Then
            'AGENCIAMENTO DE IMPORTACAO AEREO
            Servico = 2

            'comex = 1
            'ID_PORTO = ddlDestinoFrete.SelectedValue
            'via = 4
        End If




        '        dsAjustaTaxa.SelectCommand = "select id_cotacao_taxa,ID_BL,A.ID_TRANSPORTADOR,  (select nr_cotacao from tb_cotacao where nr_processo_gerado = NR_PROCESSO)NR_COTACAO,ARMADOR,NR_PROCESSO,NM_ITEM_DESPESA,PORTO,DT_EMBARQUE,DT_CHEGADA,REGRA,VL_TAXA_COMPRA,VL_TAXA_VENDA,vl_taxa_local_compra,DT_VALIDADE_INICIAL from VW_AJUSTA_TAXA A 
        'INNER JOIN TB_COTACAO B ON A.ID_COTACAO = B.ID_COTACAO WHERE B.ID_STATUS_COTACAO <> 12 AND A.ID_TRANSPORTADOR = " & Request.QueryString("id") & " AND A.VL_TAXA_COMPRA > " & ValorAntigo & " AND A.ID_PORTO = " & lblPorto.Text & " AND A.ID_SERVICO = " & Servico & " AND A.ID_ITEM_DESPESA = " & lblItemDespesa.Text

        dsAjustaTaxa.SelectCommand = "select id_cotacao_taxa,ID_BL,A.ID_TRANSPORTADOR,  (select nr_cotacao from tb_cotacao where nr_processo_gerado = NR_PROCESSO)NR_COTACAO,ARMADOR,NR_PROCESSO,NM_ITEM_DESPESA,PORTO,DT_EMBARQUE,DT_CHEGADA,REGRA,VL_TAXA_COMPRA,VL_TAXA_VENDA,vl_taxa_local_compra,DT_VALIDADE_INICIAL from VW_AJUSTA_TAXA A 
INNER JOIN TB_COTACAO B ON A.ID_COTACAO = B.ID_COTACAO WHERE B.ID_STATUS_COTACAO <> 12 AND A.ID_TRANSPORTADOR = " & Request.QueryString("id") & " AND A.VL_TAXA_COMPRA < " & ValorNovo & " AND A.ID_PORTO = " & lblPorto.Text & " AND A.ID_SERVICO = " & Servico & " AND A.ID_ITEM_DESPESA = " & lblItemDespesa.Text & " and id_cotacao_taxa not in (select isnull(id_cotacao_taxa,0) from View_Taxa_Bloqueada)"

        dgvAjustaTaxa.DataBind()
        If dgvAjustaTaxa.Rows.Count > 0 Then
            mpeAjusta.Show()
            mpe.Hide()
            mpeNovo.Hide()
        Else
            divinfo.Visible = True
            lblinfo.Text = "Não há registros para atualizar!"
        End If

    End Sub

    Private Sub dgvTaxas_Load(sender As Object, e As EventArgs) Handles dgvTaxas.Load
        Pesquisa()
    End Sub

    Private Sub ddlBaseCalculoNovo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBaseCalculoNovo.SelectedIndexChanged
        If ddlBaseCalculoNovo.SelectedValue = 38 Or ddlBaseCalculoNovo.SelectedValue = 40 Or ddlBaseCalculoNovo.SelectedValue = 41 Then
            txtQtdBaseCalculoNovo.Enabled = True
        Else
            txtQtdBaseCalculoNovo.Enabled = False
            txtQtdBaseCalculoNovo.Text = ""
        End If
    End Sub

    Private Sub ddlBaseCalculo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBaseCalculo.SelectedIndexChanged
        If ddlBaseCalculo.SelectedValue = 38 Or ddlBaseCalculo.SelectedValue = 40 Or ddlBaseCalculo.SelectedValue = 41 Then
            txtQtdBaseCalculo.Enabled = True
        Else
            txtQtdBaseCalculo.Enabled = False
            txtQtdBaseCalculo.Text = ""
        End If
    End Sub
End Class