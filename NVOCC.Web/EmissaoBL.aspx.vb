Public Class EmissaoBL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Request.QueryString("id") <> "" And Not Page.IsPostBack Then
            Dim Con As New Conexao_sql
            Dim ID As String = Request.QueryString("id")
            Con.Conectar()

            Dim dsHistorico As DataSet = Con.ExecutarQuery("SELECT ID_BL_EMISSAO,DS_SHIPPER,DS_CONSIGNED,DS_CONSIGNEE,DS_FOR_DELIVERY,DS_NOTIFY_ADDRESS,DS_VOY_NUMBER,DS_OCEAN_VESSEL,DS_NUMBER_ORIGINAL_BL,DS_FREIGHT_PAYABLE,DS_PORT_LOADING,DS_PLACE_RECEIPT,DS_PORT_DISCHARGE,DS_PORT_DELIVERY,DS_MARK_NUMBER,DS_NUMBER_KIND_PACKAGES,DS_DESCRIPTION_GOODS,DS_FREIGHT,DS_CURRENCY,DS_FREIGHT_CHARGES,DS_GROSS_WEIGHT,DS_NET_WEIGHT,DS_MEASUREMENT,DS_AGENT_FOR,DS_SIGNATURE,DS_CPF,DS_IMPRESSAO,DS_PLACE,DS_DATE_ISSUE,DS_COMMODITY,DT_APROVACAO,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL)NR_BL FROM TB_BL_EMISSAO A WHERE ID_BL = " & ID & " ORDER BY ID_BL_EMISSAO DESC")

            If dsHistorico.Tables(0).Rows.Count = 0 Then
                Con.ExecutarQuery("UPDATE TB_BL SET DT_EMISSAO_BL = GETDATE() WHERE ID_BL = " & ID)
                Dim ds As DataSet = Con.ExecutarQuery("SELECT * FROM [dbo].[View_Emissao_BL] WHERE ID_BL = " & ID)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtID.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                    txtIDHistorico.Text = 0
                    txtCliente.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_CLIENTE").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_CLIENTE").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_CLIENTE").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_CLIENTE").ToString()

                    lblNumeroBLImpressao.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                    lblNumHBL.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                    txtPesoLiquido.Text = "0,000"
                    txtCampoEditavel7.Text = Now.Date.ToString("dd-MM-yyyy")

                    txtImportador1.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_IMPORTADOR").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_IMPORTADOR").ToString()

                    txtImportador2.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_IMPORTADOR").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_IMPORTADOR").ToString()



                    txtImportador3.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_IMPORTADOR").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_IMPORTADOR").ToString()

                    txtViagem.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_MASTER").ToString()
                    txtNavio.Text = ds.Tables(0).Rows(0).Item("NAVIO_MASTER").ToString()
                    txtTipoPagamento.Text = ds.Tables(0).Rows(0).Item("TIPO_PAGAMENTO").ToString()
                    txtOrigem.Text = ds.Tables(0).Rows(0).Item("ORIGEM").ToString()
                    txtDestino.Text = ds.Tables(0).Rows(0).Item("DESTINO").ToString()
                    txtQtdVolumes.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString()
                    txtMoeda.Text = ds.Tables(0).Rows(0).Item("MOEDA_FRETE").ToString()
                    txtFrete.Text = ds.Tables(0).Rows(0).Item("VL_FRETE").ToString()
                    txtPesoBruto.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString()
                    txtM3.Text = ds.Tables(0).Rows(0).Item("VL_M3").ToString()
                    txtAgente.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_AGENTE").ToString()
                End If
                ds = Con.ExecutarQuery("SELECT A.ID_BL_MASTER,NR_CNTR,(SELECT NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER WHERE ID_TIPO_CONTAINER = A.ID_TIPO_CNTR)TIPO_CONTAINER,VL_PESO_TARA,NR_LACRE
FROM TB_CNTR_BL A  WHERE A.ID_BL_MASTER = (SELECT  ID_BL_MASTER FROM TB_BL WHERE ID_BL = " & ID & ")")
                If ds.Tables(0).Rows.Count > 0 Then

                    For Each linha As DataRow In ds.Tables(0).Rows
                        txtContainer.Text &= linha("TIPO_CONTAINER").ToString() & ": " & linha("NR_CNTR").ToString() & Environment.NewLine & "Tara: " & linha("VL_PESO_TARA").ToString() & Environment.NewLine & "Lacre: " & linha("NR_LACRE").ToString() & Environment.NewLine & Environment.NewLine
                    Next
                End If

                ds = Con.ExecutarQuery("SELECT ID_USUARIO, NOME,CPF FROM [dbo].[TB_USUARIO] WHERE ID_USUARIO = " & Session("ID_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then
                    txtCampoEditavel6.Text = ds.Tables(0).Rows(0).Item("NOME").ToString()
                    txtCPF.Text = ds.Tables(0).Rows(0).Item("CPF").ToString()
                End If


                ds = Con.ExecutarQuery("SELECT (SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA =  A.ID_ITEM_DESPESA)+': '+ CAST(SUM(ISNULL(A.VL_TAXA_CALCULADO,0))AS varchar) + ' ' + (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA)as  descricao  FROM TB_BL_TAXA A  WHERE ID_BL = " & ID & "  GROUP BY A.ID_MOEDA,ID_ITEM_DESPESA
UNION
SELECT 'FREIGHT: '+ CAST(SUM(ISNULL(A.VL_FRETE,0))AS varchar) + ' ' + (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_FRETE)as  descricao  FROM TB_BL A  WHERE ID_BL = " & ID & " AND VL_FRETE IS NOT NULL GROUP BY A.ID_MOEDA_FRETE")
                If ds.Tables(0).Rows.Count > 0 Then

                    For Each linha As DataRow In ds.Tables(0).Rows
                        txtFreteTaxa.Text &= linha("descricao") & Environment.NewLine
                    Next
                End If

                Con.Fechar()
            Else
                txtIDHistorico.Text = dsHistorico.Tables(0).Rows(0).Item("ID_BL_EMISSAO").ToString()
                txtCliente.Text = dsHistorico.Tables(0).Rows(0).Item("DS_SHIPPER").ToString()
                lblNumeroBLImpressao.Text = dsHistorico.Tables(0).Rows(0).Item("NR_BL").ToString()
                lblNumHBL.Text = dsHistorico.Tables(0).Rows(0).Item("NR_BL").ToString()
                txtPesoLiquido.Text = dsHistorico.Tables(0).Rows(0).Item("DS_NET_WEIGHT").ToString()
                txtImportador1.Text = dsHistorico.Tables(0).Rows(0).Item("DS_CONSIGNED").ToString()
                txtImportador2.Text = dsHistorico.Tables(0).Rows(0).Item("DS_CONSIGNEE").ToString()
                txtCampoEditavel.Text = dsHistorico.Tables(0).Rows(0).Item("DS_FOR_DELIVERY").ToString()
                txtImportador3.Text = dsHistorico.Tables(0).Rows(0).Item("DS_NOTIFY_ADDRESS").ToString()
                txtViagem.Text = dsHistorico.Tables(0).Rows(0).Item("DS_VOY_NUMBER").ToString()
                txtNavio.Text = dsHistorico.Tables(0).Rows(0).Item("DS_OCEAN_VESSEL").ToString()
                txtCampoEditavel1.Text = dsHistorico.Tables(0).Rows(0).Item("DS_NUMBER_ORIGINAL_BL").ToString()
                txtTipoPagamento.Text = dsHistorico.Tables(0).Rows(0).Item("DS_FREIGHT_PAYABLE").ToString()
                txtOrigem.Text = dsHistorico.Tables(0).Rows(0).Item("DS_PORT_LOADING").ToString()
                txtCampoEditavel2.Text = dsHistorico.Tables(0).Rows(0).Item("DS_PLACE_RECEIPT").ToString()
                txtDestino.Text = dsHistorico.Tables(0).Rows(0).Item("DS_PORT_DISCHARGE").ToString()
                txtCampoEditavel3.Text = dsHistorico.Tables(0).Rows(0).Item("DS_PORT_DELIVERY").ToString()
                txtContainer.Text = dsHistorico.Tables(0).Rows(0).Item("DS_MARK_NUMBER").ToString()
                txtQtdVolumes.Text = dsHistorico.Tables(0).Rows(0).Item("DS_NUMBER_KIND_PACKAGES").ToString()
                txtCampoEditavel4.Text = dsHistorico.Tables(0).Rows(0).Item("DS_COMMODITY").ToString()
                txtCampoEditavel5.Text = dsHistorico.Tables(0).Rows(0).Item("DS_DESCRIPTION_GOODS").ToString()
                txtFreteTaxa.Text = dsHistorico.Tables(0).Rows(0).Item("DS_FREIGHT_CHARGES").ToString()
                txtMoeda.Text = dsHistorico.Tables(0).Rows(0).Item("DS_CURRENCY").ToString()
                txtFrete.Text = dsHistorico.Tables(0).Rows(0).Item("DS_FREIGHT").ToString()
                txtPesoBruto.Text = dsHistorico.Tables(0).Rows(0).Item("DS_GROSS_WEIGHT").ToString()
                txtM3.Text = dsHistorico.Tables(0).Rows(0).Item("DS_MEASUREMENT").ToString()
                txtAgente.Text = dsHistorico.Tables(0).Rows(0).Item("DS_AGENT_FOR").ToString()
                txtCampoEditavel6.Text = dsHistorico.Tables(0).Rows(0).Item("DS_SIGNATURE").ToString()
                txtCPF.Text = dsHistorico.Tables(0).Rows(0).Item("DS_CPF").ToString()
                txtImpressao.Text = dsHistorico.Tables(0).Rows(0).Item("DS_IMPRESSAO").ToString()
                txtOrigemPagamento.Text = dsHistorico.Tables(0).Rows(0).Item("DS_PLACE").ToString()
                txtCampoEditavel7.Text = dsHistorico.Tables(0).Rows(0).Item("DS_DATE_ISSUE").ToString()

                If Not IsDBNull(dsHistorico.Tables(0).Rows(0).Item("DT_APROVACAO")) Then
                    ckbAprovar.Checked = True
                    DesabilitaCampos(Me)
                End If
            End If










        End If

    End Sub
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Response.Redirect("ListagemBL.aspx")

    End Sub

    Public Sub DesabilitaCampos(ByVal controlP As Control)
        Dim ctl As Control

        For Each ctl In controlP.Controls

            If TypeOf ctl Is TextBox Then

                DirectCast(ctl, TextBox).Enabled = False

            ElseIf ctl.Controls.Count > 0 Then

                DesabilitaCampos(ctl)

            End If

        Next
        ckbAprovar.Enabled = False
    End Sub
    Private Sub ckbAprovar_CheckedChanged(sender As Object, e As EventArgs) Handles ckbAprovar.CheckedChanged
        Dim Con As New Conexao_sql
        Dim ID As String = Request.QueryString("id")
        Con.Conectar()

        If ckbAprovar.Checked = True Then
            IncluiHistorico()
            Con.ExecutarQuery("UPDATE TB_BL_EMISSAO SET DT_APROVACAO = GETDATE(), ID_USUARIO_APROVACAO = " & Session("ID_USUARIO") & " WHERE ID_BL = " & ID & " AND ID_BL_EMISSAO = " & txtIDHistorico.Text)
            DesabilitaCampos(Me)
        ElseIf ckbAprovar.Checked = False Then
            Con.ExecutarQuery("UPDATE TB_BL_EMISSAO SET DT_APROVACAO = NULL, ID_USUARIO_APROVACAO = NULL WHERE ID_BL = " & ID & " AND ID_BL_EMISSAO = " & txtIDHistorico.Text)

        End If

    End Sub
    Sub IncluiHistorico()
        Dim Con As New Conexao_sql
        Dim ID As String = Request.QueryString("id")
        Con.Conectar()
        Dim dsHistorico As DataSet = Con.ExecutarQuery("INSERT INTO  TB_BL_EMISSAO (ID_BL,DS_SHIPPER,DS_CONSIGNED,DS_CONSIGNEE,DS_FOR_DELIVERY,DS_NOTIFY_ADDRESS,DS_VOY_NUMBER,DS_OCEAN_VESSEL,DS_NUMBER_ORIGINAL_BL,DS_FREIGHT_PAYABLE,DS_PORT_LOADING,DS_PLACE_RECEIPT,DS_PORT_DISCHARGE,DS_PORT_DELIVERY,DS_MARK_NUMBER,DS_NUMBER_KIND_PACKAGES,DS_DESCRIPTION_GOODS,DS_FREIGHT,DS_CURRENCY,DS_FREIGHT_CHARGES,DS_GROSS_WEIGHT,DS_NET_WEIGHT,DS_MEASUREMENT,DS_AGENT_FOR,DS_SIGNATURE,DS_CPF,DS_IMPRESSAO,DS_PLACE,DS_DATE_ISSUE) VALUES (" & ID & ",'" & txtCliente.Text & "','" & txtImportador1.Text & "','" & txtImportador2.Text & "','" & txtCampoEditavel.Text & "','" & txtImportador3.Text & "','" & txtViagem.Text & "','" & txtNavio.Text & "','" & txtCampoEditavel1.Text & "','" & txtTipoPagamento.Text & "','" & txtOrigem.Text & "','" & txtCampoEditavel2.Text & "','" & txtDestino.Text & "','" & txtCampoEditavel3.Text & "','" & txtContainer.Text & "','" & txtQtdVolumes.Text & "','" & txtCampoEditavel5.Text & "','" & txtFrete.Text & "','" & txtMoeda.Text & "','" & txtFreteTaxa.Text & "','" & txtPesoBruto.Text & "','" & txtPesoLiquido.Text & "','" & txtM3.Text & "','" & txtAgente.Text & "','" & txtCampoEditavel6.Text & "','" & txtCPF.Text & "','" & txtImpressao.Text & "','" & txtOrigemPagamento.Text & "','" & txtCampoEditavel7.Text & "')   Select SCOPE_IDENTITY() as ID_BL_EMISSAO ")

        txtIDHistorico.Text = dsHistorico.Tables(0).Rows(0).Item("ID_BL_EMISSAO").ToString()
    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim Con As New Conexao_sql
        Dim ID As String = Request.QueryString("id")
        Con.Conectar()

        If ckbAprovar.Checked = False Then
            IncluiHistorico()
            Con.ExecutarQuery("UPDATE TB_BL_EMISSAO SET DT_APROVACAO = NULL, ID_USUARIO_APROVACAO = NULL WHERE ID_BL = " & ID & " AND ID_BL_EMISSAO = " & txtIDHistorico.Text)

        End If



        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "Imprimir()", True)

    End Sub
End Class