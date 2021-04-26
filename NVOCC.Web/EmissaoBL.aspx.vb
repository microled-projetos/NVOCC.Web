Public Class EmissaoBL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Request.QueryString("id") <> "" Then
            Dim Con As New Conexao_sql
            Dim ID As String = Request.QueryString("id")
            Con.Conectar()
            Con.ExecutarQuery("UPDATE TB_BL SET DT_EMISSAO_BL = GETDATE() WHERE ID_BL = " & ID)
            Dim ds As DataSet = Con.ExecutarQuery("SELECT * FROM [dbo].[View_Emissao_BL] WHERE ID_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtID.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                txtCliente.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_CLIENTE").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_CLIENTE").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_CLIENTE").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_CLIENTE").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_CLIENTE").ToString()

                lblNumeroBLImpressao.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                lblNumHBL.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                txtPesoLiquido.Text = "0,000"
                txtCampoEditavel7.Text = Now.Date.ToString("dd-MM-yyyy")

                txtImportador1.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_IMPORTADOR").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_IMPORTADOR").ToString()

                txtImportador2.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_IMPORTADOR").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_IMPORTADOR").ToString()

                'txtCampoEditavel.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()

                txtImportador3.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("ENDERECO_PARCEIRO_IMPORTADOR").ToString() & " " & ds.Tables(0).Rows(0).Item("NR_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("COMPL_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("BAIRRO_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("CEP_PARCEIRO_IMPORTADOR").ToString() & " - " & ds.Tables(0).Rows(0).Item("CIDADE_PARCEIRO_IMPORTADOR").ToString() & Environment.NewLine & ds.Tables(0).Rows(0).Item("TELEFONE_PARCEIRO_IMPORTADOR").ToString()

                txtViagem.Text = ds.Tables(0).Rows(0).Item("NR_VIAGEM_MASTER").ToString()
                txtNavio.Text = ds.Tables(0).Rows(0).Item("NAVIO_MASTER").ToString()
                ' txtCampoEditavel1.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
                txtTipoPagamento.Text = ds.Tables(0).Rows(0).Item("TIPO_PAGAMENTO").ToString()
                txtOrigem.Text = ds.Tables(0).Rows(0).Item("ORIGEM").ToString()
                ' txtCampoEditavel2.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
                txtDestino.Text = ds.Tables(0).Rows(0).Item("DESTINO").ToString()
                ' txtCampoEditavel3.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
                txtQtdVolumes.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString()
                '  txtCampoEditavel4.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
                '  txtCampoEditavel5.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
                txtMoeda.Text = ds.Tables(0).Rows(0).Item("MOEDA_FRETE").ToString()
                txtFrete.Text = ds.Tables(0).Rows(0).Item("VL_FRETE").ToString()
                'txtOrigemPagamento.Text = ds.Tables(0).Rows(0).Item("ORIGEM_PAGAMENTO").ToString()
                txtPesoBruto.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString()
                txtM3.Text = ds.Tables(0).Rows(0).Item("VL_M3").ToString()
                txtAgente.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_AGENTE").ToString()
                'txtCampoEditavel7.Text = ds.Tables(0).Rows(0).Item("ID_USUARIO").ToString()
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
        End If

    End Sub
    Private Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Response.Redirect("ModuloOperacional.aspx")

    End Sub
End Class