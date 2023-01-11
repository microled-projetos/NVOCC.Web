Public Class EmissaoNDFaturamento

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2028 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            If Request.QueryString("id") <> "" Then
                Con.Conectar()
                Dim ID As String = Request.QueryString("id")
                ds = Con.ExecutarQuery("SELECT F.NR_NOTA_DEBITO,CONVERT(VARCHAR,F.DT_NOTA_DEBITO,103)DT_NOTA_DEBITO,F.ID_FATURAMENTO,A.ID_CONTA_PAGAR_RECEBER,C.ID_PARCEIRO_EMPRESA,CONVERT(VARCHAR,DT_VENCIMENTO,103)DT_VENCIMENTO,NR_FATURA_FORNECEDOR,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = D.ID_PORTO_ORIGEM)ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = D.ID_PORTO_DESTINO)DESTINO,
CONVERT(VARCHAR,DT_EMBARQUE,103)EMBARQUE,
CONVERT(VARCHAR,DT_CHEGADA,103)CHEGADA,
(SELECT NM_NAVIO FROM TB_NAVIO WHERE ID_NAVIO = D.ID_NAVIO)NAVIO,
NR_BL, VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = D.ID_TIPO_PAGAMENTO)NM_TIPO_PAGAMENTO,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = D.ID_PARCEIRO_EXPORTADOR)PARCEIRO_EXPORTADOR,
(SELECT NR_REFERENCIA_CLIENTE FROM VW_REFERENCIA_CLIENTE WHERE ID_BL= D.ID_BL)REFERENCIA_CLIENTE,
(SELECT NR_REFERENCIA_SHIPPER FROM VW_REFERENCIA_CLIENTE WHERE ID_BL= D.ID_BL)REFERENCIA_SHIPPER,
(SELECT NR_REFERENCIA_AUXILIAR FROM VW_REFERENCIA_CLIENTE WHERE ID_BL= D.ID_BL) REFERENCIA_AUXILIAR ,
(SELECT X.NR_BL FROM TB_BL X WHERE X.ID_BL= D.ID_BL_MASTER)BL_MASTER 
FROM TB_FATURAMENTO F
LEFT JOIN TB_CONTA_PAGAR_RECEBER A ON A.ID_CONTA_PAGAR_RECEBER = F.ID_CONTA_PAGAR_RECEBER
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER
LEFT JOIN TB_BL_TAXA C ON C.ID_BL_TAXA = B.ID_BL_TAXA
LEFT JOIN TB_BL D ON D.ID_BL = C.ID_BL
WHERE F.ID_FATURAMENTO = " & ID & "
GROUP BY A.ID_CONTA_PAGAR_RECEBER,C.ID_PARCEIRO_EMPRESA,DT_VENCIMENTO,NR_FATURA_FORNECEDOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,DT_EMBARQUE,DT_CHEGADA,ID_NAVIO,NR_BL, VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,D.ID_BL,F.ID_FATURAMENTO,F.NR_NOTA_DEBITO,F.DT_NOTA_DEBITO, D.ID_TIPO_PAGAMENTO, D.ID_PARCEIRO_EXPORTADOR,D.ID_BL_MASTER")
                If ds.Tables(0).Rows.Count > 0 Then


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VENCIMENTO")) Then
                        lblVencimento.Text = ds.Tables(0).Rows(0).Item("DT_VENCIMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("BL_MASTER")) Then
                        lblConhecimento.Text = ds.Tables(0).Rows(0).Item("BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_FATURA_FORNECEDOR")) Then
                        lblFatura.Text = ds.Tables(0).Rows(0).Item("NR_FATURA_FORNECEDOR")
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

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("PARCEIRO_EXPORTADOR")) Then
                        lblExportador.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_EXPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                        lblPesoBruto.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                        lblQtdVolumes.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_PAGAMENTO")) Then
                        lblFrete.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("REFERENCIA_CLIENTE")) Then
                        lblReferencias.Text = ds.Tables(0).Rows(0).Item("REFERENCIA_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("REFERENCIA_SHIPPER")) Then
                        lblRefShipper.Text = ds.Tables(0).Rows(0).Item("REFERENCIA_SHIPPER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("REFERENCIA_AUXILIAR")) Then
                        lblRefAux.Text = ds.Tables(0).Rows(0).Item("REFERENCIA_AUXILIAR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        lblHouse.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    Dim dsParceiro As DataSet = Con.ExecutarQuery("SELECT NM_CLIENTE,ENDERECO,NR_ENDERECO,CNPJ,CEP,CIDADE,BAIRRO,ESTADO FROM TB_FATURAMENTO A WHERE ID_FATURAMENTO = " & ID)

                    If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("NM_CLIENTE")) Then
                        lblEmpresa.Text = dsParceiro.Tables(0).Rows(0).Item("NM_CLIENTE")
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
                    Else
                        lblCidade.Text = ""
                    End If

                    If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("CNPJ")) Then
                        lblCNPJ.Text = dsParceiro.Tables(0).Rows(0).Item("CNPJ")
                    End If

                    lblProcesso.Text = Session("ProcessoFaturamento")


                    Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT (SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_DESPESA FROM TB_BL_TAXA WHERE ID_BL_TAXA = A.ID_BL_TAXA))ITEM_DESPESA,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = (SELECT ID_MOEDA FROM TB_BL_TAXA WHERE ID_BL_TAXA = A.ID_BL_TAXA))MOEDA,VL_TAXA_CALCULADO,VL_CAMBIO,ISNULL(VL_LANCAMENTO,0)VALORES,CASE WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_TIPO_ITEM_DESPESA = 1) THEN
VL_ISS
ELSE 0 END VL_ISS
FROM TB_CONTA_PAGAR_RECEBER_ITENS A
WHERE ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE isnull(ID_TIPO_ITEM_DESPESA,0) <> 1) AND ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & ID & " )")

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
                            tabela &= "<tr><td style='padding-left:10px;padding-right:10px'>" & linha("ITEM_DESPESA") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_TAXA_CALCULADO") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_CAMBIO") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VALORES") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_ISS") & "</td></tr>"

                            valores = valores + linha("VALORES")

                            If lblCidade.Text = "SANTOS" Then
                                valores = valores - linha("VL_ISS")
                            End If


                        Next
                        Dim Total As String = FormatCurrency(valores)
                        tabela &= "<tr><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'>Total: " & Total & "</td></tr>"
                        tabela &= "</table>"
                        divConteudoDinamico.InnerHtml = tabela
                        'lbltotal.Text = "Total: " & valores
                    End If

                    If IsDBNull(ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")) And IsDBNull(ds.Tables(0).Rows(0).Item("DT_NOTA_DEBITO")) Then
                        Dim NumeracaoDoc As New NumeracaoDoc
                        Dim numero As String = NumeracaoDoc.Numerar(1)

                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_NOTA_DEBITO = getdate(), NR_NOTA_DEBITO = '" & numero & "' WHERE ID_FATURAMENTO =" & ID)
                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_NOTA_DEBITO = '" & numero & "' WHERE ID_NUMERACAO = 5")

                        lblDataEmissao.Text = Now.Date.ToString("dd/MM/yyyy")
                        lblFatura.Text = numero
                    Else
                        lblDataEmissao.Text = ds.Tables(0).Rows(0).Item("DT_NOTA_DEBITO")
                        lblFatura.Text = ds.Tables(0).Rows(0).Item("NR_NOTA_DEBITO")

                    End If

                    ds = Con.ExecutarQuery("SELECT NOME FROM TB_USUARIO WHERE ID_USUARIO = " & Session("ID_USUARIO"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        lblUsuario.Text = ds.Tables(0).Rows(0).Item("NOME")
                    End If

                    lblDataImpressao.Text = Now.Date.ToString("dd-MM-yyyy")

                    ds = Con.ExecutarQuery(" SELECT convert(varchar,count(A.ID_CNTR_BL)) +' x '+ (SELECT NM_TIPO_CONTAINER FROM TB_TIPO_CONTAINER B WHERE B.ID_TIPO_CONTAINER = A.ID_TIPO_CNTR )CONTAINER from TB_CNTR_BL A INNER JOIN TB_AMR_CNTR_BL C ON A.ID_CNTR_BL = C.ID_CNTR_BL WHERE C.ID_BL IN (  SELECT ID_BL FROM TB_CONTA_PAGAR_RECEBER_ITENS  WHERE ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & ID & " )) GROUP BY A.ID_TIPO_CNTR")
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
                    Dim processo As String = lblProcesso.Text
                    Page.Title = "NOTA DE DEBITO " & processo.Replace("/", "-")

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

                    Con.Fechar()
                End If

            End If
        End If

    End Sub

End Class