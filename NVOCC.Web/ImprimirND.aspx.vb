Public Class ImprimirND
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" Then
            Page.Title = "NOTA DE DEBITO"

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ID_CIDADE As Integer = 0
            Dim ID As String = Request.QueryString("id")
            Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_CONTA_PAGAR_RECEBER,B.ID_PARCEIRO_EMPRESA,CONVERT(VARCHAR,DT_VENCIMENTO,103)DT_VENCIMENTO,NR_FATURA_FORNECEDOR,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = D.ID_PORTO_ORIGEM)ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = D.ID_PORTO_DESTINO)DESTINO,
CONVERT(VARCHAR,DT_EMBARQUE,103)EMBARQUE,
CONVERT(VARCHAR,DT_CHEGADA,103)CHEGADA,
(SELECT NM_NAVIO FROM TB_NAVIO WHERE ID_NAVIO = D.ID_NAVIO)NAVIO,
NR_BL, VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,
(SELECT NR_REFERENCIA_CLIENTE FROM VW_REFERENCIA_CLIENTE WHERE ID_BL= D.ID_BL)REFERENCIA_CLIENTE,
(SELECT X.NR_BL FROM TB_BL X WHERE X.ID_BL= D.ID_BL_MASTER)BL_MASTER
FROM TB_CONTA_PAGAR_RECEBER A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER
LEFT JOIN TB_BL_TAXA C ON C.ID_BL_TAXA = B.ID_BL_TAXA
LEFT JOIN TB_BL D ON D.ID_BL = C.ID_BL
WHERE A.ID_CONTA_PAGAR_RECEBER = " & ID & "
GROUP BY A.ID_CONTA_PAGAR_RECEBER,B.ID_PARCEIRO_EMPRESA,DT_VENCIMENTO,NR_FATURA_FORNECEDOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,DT_EMBARQUE,DT_CHEGADA,ID_NAVIO,NR_BL, VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,D.ID_BL,D.ID_BL_MASTER")
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VENCIMENTO")) Then
                    lblVencimento.Text = ds.Tables(0).Rows(0).Item("DT_VENCIMENTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_FATURA_FORNECEDOR")) Then
                    lblFatura.Text = ds.Tables(0).Rows(0).Item("NR_FATURA_FORNECEDOR")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("BL_MASTER")) Then
                    lblConhecimento.Text = ds.Tables(0).Rows(0).Item("BL_MASTER")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                    lblHouse.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ORIGEM")) Then
                    lblOrigem.Text = ds.Tables(0).Rows(0).Item("ORIGEM")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DESTINO")) Then
                    lblDestino.Text = ds.Tables(0).Rows(0).Item("DESTINO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMBARQUE")) Then
                    lblDataEmbarque.Text = ds.Tables(0).Rows(0).Item("EMBARQUE")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CHEGADA")) Then
                    lblDataChegada.Text = ds.Tables(0).Rows(0).Item("CHEGADA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NAVIO")) Then
                    lblNavio.Text = ds.Tables(0).Rows(0).Item("NAVIO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                    lblPesoBruto.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                    lblQtdVolumes.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("REFERENCIA_CLIENTE")) Then
                    lblReferencias.Text = ds.Tables(0).Rows(0).Item("REFERENCIA_CLIENTE")
                End If

                If IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                    DivImpressao.Visible = False
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "script", "<script>alert('Não foi localizado parceiro para esta fatura.');</script>", False)
                    Exit Sub
                End If

                Dim dsParceiro As DataSet = Con.ExecutarQuery("SELECT NM_RAZAO,ENDERECO,NR_ENDERECO,CNPJ,CPF,CEP,ID_CIDADE,(SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)CIDADE,BAIRRO,TELEFONE FROM TB_PARCEIRO A WHERE ID_PARCEIRO = " & ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA"))

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("ID_CIDADE")) Then
                    ID_CIDADE = dsParceiro.Tables(0).Rows(0).Item("ID_CIDADE")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("NM_RAZAO")) Then
                    lblEmpresa.Text = dsParceiro.Tables(0).Rows(0).Item("NM_RAZAO")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("ENDERECO")) Then
                    lblEndereco.Text = dsParceiro.Tables(0).Rows(0).Item("ENDERECO")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("NR_ENDERECO")) Then
                    lblNumero.Text = dsParceiro.Tables(0).Rows(0).Item("NR_ENDERECO")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("BAIRRO")) Then
                    lblBairro.Text = dsParceiro.Tables(0).Rows(0).Item("BAIRRO")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("CEP")) Then
                    lblCEP.Text = dsParceiro.Tables(0).Rows(0).Item("CEP")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("CIDADE")) Then
                    lblCidade.Text = dsParceiro.Tables(0).Rows(0).Item("CIDADE")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("CNPJ")) Then
                    lblCNPJ.Text = dsParceiro.Tables(0).Rows(0).Item("CNPJ")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("TELEFONE")) Then
                    lblTelefone.Text = dsParceiro.Tables(0).Rows(0).Item("TELEFONE")
                End If


                Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT (SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_DESPESA FROM TB_BL_TAXA WHERE ID_BL_TAXA = A.ID_BL_TAXA))ITEM_DESPESA,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = (SELECT ID_MOEDA FROM TB_BL_TAXA WHERE ID_BL_TAXA = A.ID_BL_TAXA))MOEDA,VL_LANCAMENTO,VL_CAMBIO,VL_TAXA_CALCULADO,CASE WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_TIPO_ITEM_DESPESA = 1) THEN
VL_ISS
ELSE 0 END VL_ISS
FROM TB_CONTA_PAGAR_RECEBER_ITENS A
WHERE ID_CONTA_PAGAR_RECEBER = " & ID)

                Dim valores As Double = 0
                If dsTaxas.Tables(0).Rows.Count > 0 Then

                    Dim tabela As String = "<br/><table style='font-family:Arial;font-size:10px;'><tr>"
                    tabela &= "<th style='padding-left:10px;padding-right:10px'>Taxa</th>"
                    tabela &= "<th style='padding-left:10px;padding-right:10px'>Moeda</th>"
                    tabela &= "<th style='padding-left:10px;padding-right:10px'>Valor</th>"
                    tabela &= "<th style='padding-left:10px;padding-right:10px'>Taxa de Câmbio</th>"
                    tabela &= "<th style='padding-left:10px;padding-right:10px'>Valores R$</th>"
                    tabela &= "<th style='padding-left:10px;padding-right:10px'>ISS</th></tr>"


                    For Each linha As DataRow In dsTaxas.Tables(0).Rows
                        tabela &= "<tr><td style='padding-left:10px;padding-right:10px''>" & linha("ITEM_DESPESA") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_TAXA_CALCULADO") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_CAMBIO") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_LANCAMENTO") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_ISS") & "</td></tr>"

                        valores = valores + linha("VL_LANCAMENTO")

                        If ID_CIDADE = 78 Then
                            valores = valores - linha("VL_ISS")
                        End If


                    Next
                    valores = FormatCurrency(valores)
                    Dim Total As String = FormatCurrency(valores)
                    tabela &= "<tr><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'>Total: " & Total & "</td></tr>"
                    tabela &= "</table>"
                    divConteudoDinamico.InnerHtml = tabela
                End If

                ds = Con.ExecutarQuery("SELECT NOME FROM TB_USUARIO WHERE ID_USUARIO = " & Session("ID_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then
                    lblUsuario.Text = ds.Tables(0).Rows(0).Item("NOME")
                End If

                lblDataImpressao.Text = Now.Date.ToString("dd-MM-yyyy")
                lblDataEmissao.Text = Now.Date.ToString("dd/MM/yyyy")


                ds = Con.ExecutarQuery("SELECT NR_PROCESSO FROM View_Contas_Receber WHERE ID_CONTA_PAGAR_RECEBER = " & ID)
                If ds.Tables(0).Rows.Count > 0 Then
                    lblProcesso.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    Dim processo As String = lblProcesso.Text
                    Page.Title = "NOTA DE DEBITO " & processo.Replace("/", "-")
                End If

                ds = Con.ExecutarQuery("SELECT convert(varchar,count(A.ID_CNTR_BL)) +' x '+ (SELECT NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER B WHERE B.ID_TIPO_CONTAINER = A.ID_TIPO_CNTR )CONTAINER from TB_CNTR_BL A INNER JOIN TB_AMR_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL WHERE C.ID_BL IN (  SELECT TOP 1 ID_BL FROM TB_CONTA_PAGAR_RECEBER_ITENS
 WHERE ID_CONTA_PAGAR_RECEBER = " & ID & ") GROUP BY A.ID_TIPO_CNTR")
                If ds.Tables(0).Rows.Count > 0 Then
                    lblContainer.Text = ""
                    For Each linha As DataRow In ds.Tables(0).Rows
                        If lblContainer.Text = "" Then
                            lblContainer.Text = linha("CONTAINER")
                        Else
                            lblContainer.Text &= " + " & linha("CONTAINER")
                        End If
                    Next
                End If

                dsParceiro = Con.ExecutarQuery("SELECT UPPER(NM_RAZAO),UPPER(ENDERECO)ENDERECO,NR_ENDERECO,CNPJ,CPF,CEP,ID_CIDADE,(SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)CIDADE,UPPER(BAIRRO)BAIRRO,TELEFONE,(SELECT NM_PAIS FROM TB_PAIS WHERE ID_PAIS = A.ID_PAIS)PAIS,INSCR_ESTADUAL FROM TB_PARCEIRO A WHERE ID_PARCEIRO = 1 ")

                If ds.Tables(0).Rows.Count > 0 Then
                    lblEnderecoFCA.Text = dsParceiro.Tables(0).Rows(0).Item("ENDERECO") & ", " & dsParceiro.Tables(0).Rows(0).Item("NR_ENDERECO") & " - " & dsParceiro.Tables(0).Rows(0).Item("BAIRRO")
                    lblEnderecoFCA2.Text = dsParceiro.Tables(0).Rows(0).Item("CIDADE") & " - " & dsParceiro.Tables(0).Rows(0).Item("PAIS") & " - CEP: " & dsParceiro.Tables(0).Rows(0).Item("CEP")


                    If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("CNPJ")) Then
                        lblDocFCA.Text = "CNPJ: " & dsParceiro.Tables(0).Rows(0).Item("CNPJ") & " - I.E: " & dsParceiro.Tables(0).Rows(0).Item("INSCR_ESTADUAL")
                    End If

                    If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("TELEFONE")) Then
                        lblContatoFCA.Text = "FONE: " & dsParceiro.Tables(0).Rows(0).Item("TELEFONE")
                    End If

                End If

                Dim dsBanco As DataSet = Con.ExecutarQuery("SELECT NR_AGENCIA,DG_AGENCIA,NR_CONTA,DG_CONTA,NM_CEDENTE,CNPJ,CHAVE_PIX FROM TB_CONTA_BANCARIA WHERE ID_CONTA_BANCARIA = 1")
                If dsBanco.Tables(0).Rows.Count > 0 Then
                    lblRazaoContaBancaria.Text = dsBanco.Tables(0).Rows(0).Item("NM_CEDENTE")
                    lblAgenciaBancaria.Text = "Banco Santander - AG " & dsBanco.Tables(0).Rows(0).Item("NR_AGENCIA")
                    lblContaBancaria.Text = " C/C " & dsBanco.Tables(0).Rows(0).Item("NR_CONTA") & "-" & dsBanco.Tables(0).Rows(0).Item("DG_CONTA")
                    lblCnpjContaBancaria.Text = "CNPJ: " & dsBanco.Tables(0).Rows(0).Item("CNPJ")
                    lblPix.Text = "Chave PIX: " & dsBanco.Tables(0).Rows(0).Item("CHAVE_PIX")

                End If

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ImprimirND()", True)

            End If

            Con.Fechar()
        End If
    End Sub

End Class