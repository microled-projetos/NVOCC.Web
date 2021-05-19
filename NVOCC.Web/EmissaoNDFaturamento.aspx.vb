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
                ds = Con.ExecutarQuery("SELECT F.ID_FATURAMENTO,A.ID_CONTA_PAGAR_RECEBER,C.ID_PARCEIRO_EMPRESA,CONVERT(VARCHAR,DT_VENCIMENTO,103)DT_VENCIMENTO,NR_FATURA_FORNECEDOR,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = D.ID_PORTO_ORIGEM)ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = D.ID_PORTO_DESTINO)DESTINO,
CONVERT(VARCHAR,DT_EMBARQUE,103)EMBARQUE,
CONVERT(VARCHAR,DT_CHEGADA,103)CHEGADA,
(SELECT NM_NAVIO FROM TB_NAVIO WHERE ID_NAVIO = D.ID_NAVIO)NAVIO,
NR_BL, VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,
(SELECT NR_REFERENCIA_CLIENTE FROM VW_REFERENCIA_CLIENTE WHERE ID_BL= D.ID_BL)REFERENCIA_CLIENTE
FROM TB_FATURAMENTO F
LEFT JOIN TB_CONTA_PAGAR_RECEBER A ON A.ID_CONTA_PAGAR_RECEBER = F.ID_CONTA_PAGAR_RECEBER
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER
LEFT JOIN TB_BL_TAXA C ON C.ID_BL_TAXA = B.ID_BL_TAXA
LEFT JOIN TB_BL D ON D.ID_BL = C.ID_BL
WHERE F.ID_FATURAMENTO = " & ID & "
GROUP BY A.ID_CONTA_PAGAR_RECEBER,C.ID_PARCEIRO_EMPRESA,DT_VENCIMENTO,NR_FATURA_FORNECEDOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,DT_EMBARQUE,DT_CHEGADA,ID_NAVIO,NR_BL, VL_PESO_BRUTO,VL_M3,QT_MERCADORIA,D.ID_BL,F.ID_FATURAMENTO")
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VENCIMENTO")) Then
                        lblVencimento.Text = ds.Tables(0).Rows(0).Item("DT_VENCIMENTO")
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

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                        lblPesoBruto.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                        lblQtdVolumes.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("REFERENCIA_CLIENTE")) Then
                        lblReferencias.Text = ds.Tables(0).Rows(0).Item("REFERENCIA_CLIENTE")
                    End If


                    Dim dsParceiro As DataSet = Con.ExecutarQuery("SELECT NM_RAZAO,ENDERECO,NR_ENDERECO,CNPJ,CPF,CEP,(SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)CIDADE,BAIRRO,TELEFONE FROM TB_PARCEIRO A WHERE ID_PARCEIRO = " & ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA"))

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
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = (SELECT ID_MOEDA FROM TB_BL_TAXA WHERE ID_BL_TAXA = A.ID_BL_TAXA))MOEDA,VL_LANCAMENTO,VL_CAMBIO,CAST((ISNULL(VL_LANCAMENTO,0) * ISNULL(VL_CAMBIO,1)) AS decimal(13,2))VALORES
FROM TB_CONTA_PAGAR_RECEBER_ITENS A
WHERE ID_CONTA_PAGAR_RECEBER = " & ID)

                    Dim valores As Double = 0
                    If dsTaxas.Tables(0).Rows.Count > 0 Then

                        Dim tabela As String = "<br/><table style='font-family:Arial;font-size:10px;'><tr>"
                        tabela &= "<th style='padding-right:10px'>Taxa</th>"
                        tabela &= "<th style='padding-left:10px;padding-right:10px'>Moeda</th>"
                        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor</th>"
                        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Taxa de Câmbio</th>"
                        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valores R$</th></tr>"

                        For Each linha As DataRow In dsTaxas.Tables(0).Rows
                            tabela &= "<tr><td style='padding-right:10px'>" & linha("ITEM_DESPESA") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_LANCAMENTO") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_CAMBIO") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VALORES") & "</td></tr>"

                            valores = valores + linha("VALORES")


                        Next
                        tabela &= "<tr><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'></td><td style='padding-left:10px;padding-right:10px'>Total: " & valores & "</td></tr>"
                        tabela &= "</table>"
                        divConteudoDinamico.InnerHtml = tabela
                        'lbltotal.Text = "Total: " & valores
                    End If

                    ds = Con.ExecutarQuery("SELECT NOME FROM TB_USUARIO WHERE ID_USUARIO = " & Session("ID_USUARIO"))
                    If ds.Tables(0).Rows.Count > 0 Then
                        lblUsuario.Text = ds.Tables(0).Rows(0).Item("NOME")
                    End If

                    lblDataImpressao.Text = Now.Date.ToString("dd-MM-yyyy")
                    Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET [DT_NOTA_DEBITO] = getdate() WHERE ID_FATURAMENTO =" & ID)

                    Con.Fechar()
                End If
            End If
        End If

    End Sub

End Class