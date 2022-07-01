Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.tool.xml
Imports System
Imports System.Collections
Imports System.Globalization

Public Class CotacaoPDF_ING
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MOEDA_CNTR") = ""
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("Select A.ID_COTACAO,ID_TIPO_ESTUFAGEM,OB_CLIENTE,
A.NR_COTACAO,
CONVERT(VARCHAR,A.DT_VALIDADE_COTACAO,103)DT_VALIDADE_COTACAO,
(SELECT NM_TIPO_FREQUENCIA FROM TB_TIPO_FREQUENCIA WHERE ID_TIPO_FREQUENCIA = A.ID_TIPO_FREQUENCIA)NM_TIPO_FREQUENCIA,
VL_FREQUENCIA,
A.ID_ANALISTA_COTACAO,
(SELECT NOME FROM TB_USUARIO WHERE ID_USUARIO = A.ID_ANALISTA_COTACAO )NOME_ANALISTA,
(SELECT TELEFONE FROM TB_USUARIO WHERE ID_USUARIO = A.ID_ANALISTA_COTACAO )TELEFONE_ANALISTA,
A.ID_INCOTERM,
(SELECT NM_INCOTERM FROM TB_INCOTERM WHERE ID_INCOTERM = A.ID_INCOTERM )NOME_INCOTERM,
A.ID_TIPO_ESTUFAGEM,
(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM )NOME_ESTUFAGEM,
(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO )TIPO_PAGAMENTO,
A.ID_DESTINATARIO_COMERCIAL,
A.ID_CLIENTE,
CASE WHEN ID_DESTINATARIO_COMERCIAL = 6  
THEN(SELECT NM_CLIENTE_FINAL FROM TB_CLIENTE_FINAL B WHERE B.ID_CLIENTE_FINAL = A.ID_CLIENTE_FINAL ) 
ELSE (SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE ) END NOME_CLIENTE,

CASE WHEN ID_DESTINATARIO_COMERCIAL = 6  
THEN(SELECT NR_CNPJ FROM TB_CLIENTE_FINAL B WHERE B.ID_CLIENTE_FINAL = A.ID_CLIENTE_FINAL ) 
ELSE (SELECT CNPJ FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE ) 
END CNPJ_CLIENTE,

CASE WHEN ID_DESTINATARIO_COMERCIAL = 6  THEN NULL 
ELSE (SELECT CPF FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE ) 
END CPF_CLIENTE, 
(SELECT NM_CONTATO FROM TB_CONTATO WHERE ID_CONTATO = A.ID_CONTATO )CONTATO,
A.ID_CONTATO,
A.ID_SERVICO,
(SELECT NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO )NOME_SERVICO,
A.ID_VENDEDOR,
A.VL_PESO_TAXADO,
A.VL_TOTAL_PESO_BRUTO,
A.VL_TOTAL_M3,

ID_PORTO_ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM )PORTO_ORIGEM,
(SELECT CD_PORTO + ' - ' + NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM )CD_PORTO_ORIGEM,

ID_PORTO_DESTINO,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO )PORTO_DESTINO,
(SELECT CD_PORTO + ' - ' + NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO )CD_PORTO_DESTINO,

ID_VIA_ROTA,
(SELECT NM_VIA_ROTA FROM TB_VIA_ROTA WHERE ID_VIA_ROTA = A.ID_VIA_ROTA )VIA_ROTA,
QT_TRANSITTIME_MEDIA,
TRANSITTIME_TRUCKING_AEREO,

VL_TOTAL_FRETE_VENDA,
VL_TOTAL_FRETE_VENDA_MIN,
VL_TOTAL_FRETE_VENDA_CALCULADO,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_FRETE )MOEDA,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA1 )PORTO_ESCALA1,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA2 )PORTO_ESCALA2,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA3 )PORTO_ESCALA3,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_TRANSPORTADOR)CIA_AEREA
FROM  TB_COTACAO A
    WHERE A.ID_COTACAO = " & Request.QueryString("c"))
        If ds.Tables(0).Rows.Count > 0 Then

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NOME_SERVICO")) Then
                lblTitulo.Text = ds.Tables(0).Rows(0).Item("NOME_SERVICO") & " (" & ds.Tables(0).Rows(0).Item("NOME_ESTUFAGEM") & ")"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("MOEDA")) Then
                Session("MOEDA_CNTR") = ds.Tables(0).Rows(0).Item("MOEDA")
            End If

            CARGA()

            If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                CONTAINER()
                detalhesCarga.Visible = False
                divCargaFCL.Visible = True
                divCargaLCL.Visible = False

            Else
                detalhesCarga.Visible = True
                divCargaFCL.Visible = False
                divCargaLCL.Visible = True
                lblTipoCargaFCL.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NOME_CLIENTE")) Then
                lblCliente.Text = ds.Tables(0).Rows(0).Item("NOME_CLIENTE")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CONTATO")) Then
                lblNome.Text = ds.Tables(0).Rows(0).Item("CONTATO")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CNPJ_CLIENTE")) Then
                lblCnpjCliente.Text = ds.Tables(0).Rows(0).Item("CNPJ_CLIENTE")
            ElseIf Not IsDBNull(ds.Tables(0).Rows(0).Item("CPF_CLIENTE")) Then
                lblCnpjCliente.Text = ds.Tables(0).Rows(0).Item("CPF_CLIENTE")
            End If

            lblDataAtual.Text = Now.Date
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_COTACAO")) Then
                lblNumeroCotacao.Text = ds.Tables(0).Rows(0).Item("NR_COTACAO")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO")) Then
                lblPesoBruto.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO").ToString & " KG"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_FREQUENCIA")) Then
                lblFrequencia.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_FREQUENCIA").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FREQUENCIA")) Then
                lblValorFrequencia.Text = " - " & ds.Tables(0).Rows(0).Item("VL_FREQUENCIA").ToString
            End If



            'If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE")) Then
            '    lblObsCliente.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE").ToString
            'End If



            If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                    lblPesoTaxado.Text = "<br /><strong>&nbsp;Taxed Weight:</strong>" & ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO").ToString & " KG"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_PORTO_ORIGEM")) Then
                    lblOrigem.Text = ds.Tables(0).Rows(0).Item("CD_PORTO_ORIGEM").ToString
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CD_PORTO_DESTINO")) Then
                    lblDestino.Text = ds.Tables(0).Rows(0).Item("CD_PORTO_DESTINO").ToString
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_MEDIA")) Then
                    lblTTime.Text = ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_MEDIA").ToString & " dias"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TRANSITTIME_TRUCKING_AEREO")) Then
                    lblTTimeAereo.Text = "  -  <strong>TransitTime Trucking: </strong>" & ds.Tables(0).Rows(0).Item("TRANSITTIME_TRUCKING_AEREO").ToString & " dias"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CIA_AEREA")) Then
                    lblCiaAerea.Text = "<strong>Cia Aérea: </strong>" & ds.Tables(0).Rows(0).Item("CIA_AEREA").ToString & " <br />"
                End If

                MEDIDASAEREO()

            Else
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ORIGEM")) Then
                    lblOrigem.Text = ds.Tables(0).Rows(0).Item("PORTO_ORIGEM").ToString
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_DESTINO")) Then
                    lblDestino.Text = ds.Tables(0).Rows(0).Item("PORTO_DESTINO").ToString
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_MEDIA")) Then
                    lblTTime.Text = ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_MEDIA").ToString & " dias"
                End If

            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")) Then
                lblM3.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_M3").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VIA_ROTA")) Then
                If ds.Tables(0).Rows(0).Item("ID_VIA_ROTA") = 2 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ESCALA1")) Then
                        lblEscalas.Text &= " / " & ds.Tables(0).Rows(0).Item("PORTO_ESCALA1").ToString
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ESCALA2")) Then
                        lblEscalas.Text &= " / " & ds.Tables(0).Rows(0).Item("PORTO_ESCALA2").ToString
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ESCALA3")) Then
                        lblEscalas.Text &= " / " & ds.Tables(0).Rows(0).Item("PORTO_ESCALA3").ToString
                    End If
                End If
            End If



            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VIA_ROTA")) Then
                lblVia.Text = ds.Tables(0).Rows(0).Item("VIA_ROTA").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_MEDIA")) Then
                lblTTime.Text = ds.Tables(0).Rows(0).Item("QT_TRANSITTIME_MEDIA").ToString & " dias"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_VALIDADE_COTACAO")) Then
                lblValidade.Text = ds.Tables(0).Rows(0).Item("DT_VALIDADE_COTACAO").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NOME_INCOTERM")) Then
                lblINCOTERM.Text = ds.Tables(0).Rows(0).Item("NOME_INCOTERM").ToString
            End If



            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NOME_ANALISTA")) Then
                lblAnalista.Text = ds.Tables(0).Rows(0).Item("NOME_ANALISTA").ToString
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("TELEFONE_ANALISTA")) Then
                lblTelefoneAnalista.Text = ds.Tables(0).Rows(0).Item("TELEFONE_ANALISTA").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO")) Then
                Dim tabela As String = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
                tabela &= "<th style='padding-right:10px'>Taxa</th>"
                tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Moeda</th>"
                If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Tarifa</th>"
                    tabela &= "<th style='padding-right:10px'>Base de Calc.</th>"
                    tabela &= "<th style='padding-right:10px'>Valor Min.</th>"

                End If
                tabela &= "<th style='padding-right:10px'>Valor</th>"
                tabela &= "<th style='padding-right:10px'>Tipo Pag.</th>"


                For Each linha As DataRow In ds.Tables(0).Rows
                    tabela &= "</tr><tr><td style='padding-right:10px'>FRETE INTERNACIONAL</td>"
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"

                    If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then

                        If linha("MOEDA") = "USD" Then
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TOTAL_FRETE_VENDA")).Replace("$", "") & "</td>"
                        Else
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TOTAL_FRETE_VENDA")).Replace("R$", "") & "</td>"
                        End If


                        If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                            tabela &= "<td style='padding-right:10px'>PESO TAXADO</td>"
                        Else
                            tabela &= "<td style='padding-right:10px'>POR TON / M³</td>"
                        End If


                        If linha("MOEDA") = "USD" Then
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TOTAL_FRETE_VENDA_MIN")).Replace("$", "") & "</td>"
                        Else
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TOTAL_FRETE_VENDA_MIN")).Replace("R$", "") & "</td>"
                        End If
                    End If




                    If linha("MOEDA") = "USD" Then
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TOTAL_FRETE_VENDA_CALCULADO")).Replace("$", "") & "</td>"
                    Else
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TOTAL_FRETE_VENDA_CALCULADO")).Replace("R$", "") & "</td>"
                    End If


                    tabela &= "<td style='padding-right:10px'>" & linha("TIPO_PAGAMENTO") & "</td>"
                    tabela &= "</tr>"

                Next

                tabela = tabela.Replace("³", "<sup>3</sup>")
                tabela = tabela.Replace("ã", "&atilde;")
                tabela = tabela.Replace("Á", "&Aacute;")
                tabela = tabela.Replace("é", "&eacute;")
                tabela = tabela.Replace("É", "&Eacute;")
                tabela &= "</table>"
                divConteudofrete.InnerHtml = tabela
                lblTotalFinalFrete.Text = ds.Tables(0).Rows(0).Item("MOEDA").ToString & " " & String.Format("{0:N}", ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO"))
            End If


            lblCliente.Text = SubstituiCaracteresEspeciais(lblCliente.Text)
            lblNome.Text = SubstituiCaracteresEspeciais(lblNome.Text)
            lblAnalista.Text = SubstituiCaracteresEspeciais(lblAnalista.Text)
            lblEscalas.Text = SubstituiCaracteresEspeciais(lblEscalas.Text)
            lblVia.Text = SubstituiCaracteresEspeciais(lblVia.Text)
            lblDestino.Text = SubstituiCaracteresEspeciais(lblDestino.Text)
            lblOrigem.Text = SubstituiCaracteresEspeciais(lblOrigem.Text)
            lblTitulo.Text = SubstituiCaracteresEspeciais(lblTitulo.Text)
            lblINCOTERM.Text = SubstituiCaracteresEspeciais(lblINCOTERM.Text)
            ' lblObsCliente.Text = SubstituiCaracteresEspeciais(lblObsCliente.Text)
            lblFrequencia.Text = SubstituiCaracteresEspeciais(lblFrequencia.Text)
            lblCiaAerea.Text = SubstituiCaracteresEspeciais(lblCiaAerea.Text)



            TAXAS()

        End If



        If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then

            ds = Con.ExecutarQuery("select ISNULL(TEXTO_COTACAO_ING_AER,'')TEXTO_COTACAO from TB_PARAMETROS")
            If ds.Tables(0).Rows.Count > 0 Then
                Dim texto As String = ds.Tables(0).Rows(0).Item("TEXTO_COTACAO").ToString
                texto = SubstituiCaracteresEspeciais(texto)
                divTexto.InnerHtml = "<br/><br/>" & texto
            End If

        Else
            ds = Con.ExecutarQuery("select ISNULL(TEXTO_COTACAO_ING_MAR,'')TEXTO_COTACAO from TB_PARAMETROS")
            If ds.Tables(0).Rows.Count > 0 Then
                Dim texto As String = ds.Tables(0).Rows(0).Item("TEXTO_COTACAO").ToString
                texto = SubstituiCaracteresEspeciais(texto)
                divTexto.InnerHtml = "<br/><br/>" & texto
            End If
        End If



        Con.Fechar()

    End Sub

    Sub TAXAS()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        'ORIGEM
        ds = Con.ExecutarQuery("SELECT A.ID_COTACAO,
A.ID_COTACAO_TAXA,
A.FL_DECLARADO,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA )BASE_CALCULO,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA )MOEDA,
(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,
isnull(A.VL_TAXA_VENDA,0)VL_TAXA_VENDA,
isnull(A.VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN,
isnull(A.VL_TAXA_VENDA_CALCULADO,0)VL_TAXA_VENDA_CALCULADO,
case when (A.VL_TAXA_VENDA <> 0 and  A.VL_TAXA_VENDA_CALCULADO = 0) then 'Taxa não calculada' 
when (A.VL_TAXA_VENDA <> 0 and  A.VL_TAXA_VENDA_CALCULADO is null) then 'Taxa não calculada' 
else  cast(isnull(A.VL_TAXA_VENDA_CALCULADO,0) as varchar) end as CALCULADO,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA ) TAXA 
FROM TB_COTACAO_TAXA A
WHERE FL_DECLARADO = 1 AND ID_DESTINATARIO_COBRANCA <> 3  AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA  AND FL_INTERMEDIACAO = 0 )    AND A.ID_COTACAO = " & Request.QueryString("c"))
        If ds.Tables(0).Rows.Count > 0 Then

            Dim tabela As String = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
            tabela &= "<th style='padding-right:10px'>Taxa</th>"
            tabela &= "<th style='padding-left:10px;padding-right:10px'>Moeda</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Base de Calc.</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Min</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Calc.</th>"
            tabela &= "<th style='padding-left:10px;padding-right:10px'>Tipo Pag.</th>"
            tabela &= "</tr>"
            For Each linha As DataRow In ds.Tables(0).Rows
                tabela &= "<tr><td style='padding-right:10px'>" & linha("TAXA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"



                If linha.Item("MOEDA") = "USD" Then
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TAXA_VENDA")).Replace("$", "") & "</td>"
                Else
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TAXA_VENDA")).Replace("R$", "") & "</td>"
                End If


                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("BASE_CALCULO") & "</td>"



                If linha.Item("MOEDA") = "USD" Then
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TAXA_VENDA_MIN")).Replace("$", "") & "</td>"
                Else
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TAXA_VENDA_MIN")).Replace("R$", "") & "</td>"
                End If

                If Not IsDBNull(linha.Item("CALCULADO")) Then
                    If linha.Item("CALCULADO") <> "Taxa não calculada" Then

                        If linha.Item("MOEDA") = "USD" Then
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TAXA_VENDA_CALCULADO")).Replace("$", "") & "</td>"
                        Else
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TAXA_VENDA_CALCULADO")).Replace("R$", "") & "</td>"
                        End If


                    Else
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("CALCULADO") & "</td>"

                    End If

                End If
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TIPO_PAGAMENTO") & "</td>"
                tabela &= "</tr>"
            Next
            tabela &= "</table>"
            tabela = tabela.Replace("³", "<sup>3</sup>")
            tabela = tabela.Replace("Ã", "&Atilde;")
            tabela = tabela.Replace("ã", "&atilde;")
            tabela = tabela.Replace("Ç", "&Ccedil;")
            divTaxaOrigem.InnerHtml = tabela





            'total origem
            Dim DescTotalOrigem As String = ""

            ds = Con.ExecutarQuery("SELECT (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA) moeda, SUM(A.VL_TAXA_VENDA_CALCULADO) valor  FROM TB_COTACAO_TAXA A  WHERE ID_COTACAO = " & Request.QueryString("c") & " AND FL_DECLARADO = 1 AND A.ID_DESTINATARIO_COBRANCA <> 3 AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA  AND FL_INTERMEDIACAO = 0 ) GROUP BY A.ID_MOEDA_VENDA ")
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    If DescTotalOrigem = "" Then
                        DescTotalOrigem &= linha("moeda") & " " & String.Format("{0:N}", linha("valor"))
                    Else
                        DescTotalOrigem &= " + " & linha("moeda") & " " & String.Format("{0:N}", linha("valor"))
                    End If
                Next
            End If
            DescTotalOrigem = DescTotalOrigem.Replace("+ -", "-")

            lblTotalTaxasOrigem.Text = DescTotalOrigem

        End If







        '-----------------------------------------------------------------------------------------







        'DESTINO
        ds = Con.ExecutarQuery("SELECT A.ID_COTACAO,
A.ID_COTACAO_TAXA,
A.FL_DECLARADO,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA )BASE_CALCULO,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA )MOEDA,
(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,
isnull(A.VL_TAXA_VENDA,0)VL_TAXA_VENDA,
isnull(A.VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN,
isnull(A.VL_TAXA_VENDA_CALCULADO,0)VL_TAXA_VENDA_CALCULADO,
case when (A.VL_TAXA_VENDA <> 0 and  A.VL_TAXA_VENDA_CALCULADO = 0) then 'Taxa não calculada' 
when (A.VL_TAXA_VENDA <> 0 and  A.VL_TAXA_VENDA_CALCULADO is null) then 'Taxa não calculada' 
else  cast(isnull(A.VL_TAXA_VENDA_CALCULADO,0) as varchar) end as CALCULADO,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA ) TAXA 
FROM TB_COTACAO_TAXA A
WHERE FL_DECLARADO = 0 AND ID_DESTINATARIO_COBRANCA <> 3 AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA  AND FL_INTERMEDIACAO = 0 ) 
    AND A.ID_COTACAO = " & Request.QueryString("c"))
        If ds.Tables(0).Rows.Count > 0 Then


            Dim tabela As String = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
            tabela &= "<th style='padding-right:10px'>Taxa</th>"
            tabela &= "<th style='padding-left:10px;padding-right:10px'>Moeda</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Base de Calc.</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Min</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Calc.</th>"
            tabela &= "<th style='padding-left:10px;padding-right:10px'>Tipo Pag.</th>"
            tabela &= "</tr>"

            For Each linha As DataRow In ds.Tables(0).Rows
                tabela &= "<tr><td style='padding-right:10px'>" & linha("TAXA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"


                If linha.Item("MOEDA") = "USD" Then
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TAXA_VENDA")).Replace("$", "") & "</td>"
                Else
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TAXA_VENDA")).Replace("R$", "") & "</td>"
                End If


                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("BASE_CALCULO") & "</td>"


                If linha.Item("MOEDA") = "USD" Then
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TAXA_VENDA_MIN")).Replace("$", "") & "</td>"
                Else
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TAXA_VENDA_MIN")).Replace("R$", "") & "</td>"
                End If


                If Not IsDBNull(linha.Item("CALCULADO")) Then
                    If linha.Item("CALCULADO") <> "Taxa não calculada" Then
                        If linha.Item("MOEDA") = "USD" Then
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_TAXA_VENDA_CALCULADO")).Replace("$", "") & "</td>"
                        Else
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_TAXA_VENDA_CALCULADO")).Replace("R$", "") & "</td>"
                        End If
                    Else
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("CALCULADO") & "</td>"

                    End If

                End If
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TIPO_PAGAMENTO") & "</td>"
                tabela &= "</tr>"

            Next
            tabela = tabela.Replace("³", "<sup>3</sup>")
            tabela = tabela.Replace("Ã", "&Atilde;")
            tabela = tabela.Replace("ã", "&atilde;")
            tabela = tabela.Replace("Ç", "&Ccedil;")
            tabela &= "</table>"
            divTaxaDestino.InnerHtml = tabela

            Dim DescTotalDestino As String = ""




            'total destino
            ds = Con.ExecutarQuery("SELECT (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA) moeda, case  when A.ID_MOEDA_VENDA = 1 then
 FORMAT(SUM(A.VL_TAXA_VENDA_CALCULADO) , 'C', 'en-us') 
 else
 FORMAT(SUM(A.VL_TAXA_VENDA_CALCULADO) , 'C', 'pt-br')
 end
 AS valor   FROM TB_COTACAO_TAXA A  WHERE ID_COTACAO = " & Request.QueryString("c") & " AND FL_DECLARADO = 0  AND ID_DESTINATARIO_COBRANCA <> 3 AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA  AND FL_INTERMEDIACAO = 0 ) GROUP BY A.ID_MOEDA_VENDA ")
            If ds.Tables(0).Rows.Count > 0 Then


                For Each linha As DataRow In ds.Tables(0).Rows
                    If DescTotalDestino = "" Then
                        DescTotalDestino &= linha("moeda") & " " & String.Format("{0:N}", linha("valor").Replace("R$", "").Replace("$", ""))
                    Else

                        DescTotalDestino &= " + " & linha("moeda") & " " & String.Format("{0:N}", linha("valor").Replace("R$", "").Replace("$", ""))


                    End If
                Next
            End If


            DescTotalDestino = DescTotalDestino.Replace("+ -", "-")

            lblTotalTaxasDestino.Text = DescTotalDestino



        End If












        'total final taxas
        Dim TotalFinal As String = ""
        Dim TotalFinalTaxas As String = ""
        Dim TotalFinalFrete As String = ""

        ds = Con.ExecutarQuery("SELECT (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA) moeda, case  when A.ID_MOEDA_VENDA = 1 then
 FORMAT(SUM(A.VL_TAXA_VENDA_CALCULADO) , 'C', 'en-us') 
 else
 FORMAT(SUM(A.VL_TAXA_VENDA_CALCULADO) , 'C', 'pt-br')
 end
 AS valor  FROM TB_COTACAO_TAXA A  WHERE ID_COTACAO = " & Request.QueryString("c") & " AND ID_DESTINATARIO_COBRANCA <> 3 AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA  AND FL_INTERMEDIACAO = 0 ) GROUP BY A.ID_MOEDA_VENDA ")
        If ds.Tables(0).Rows.Count > 0 Then


            For Each linha As DataRow In ds.Tables(0).Rows
                If TotalFinalTaxas = "" Then
                    TotalFinalTaxas &= linha("moeda") & " " & String.Format("{0:N}", linha("valor").Replace("R$", "").Replace("$", ""))
                Else
                    TotalFinalTaxas &= " + " & linha("moeda") & " " & String.Format("{0:N}", linha("valor").Replace("R$", "").Replace("$", ""))
                End If
            Next
        End If

        TotalFinalTaxas = TotalFinalTaxas.Replace("+ -", "-")


        lblTotalFinalTaxas.Text = TotalFinalTaxas

















        'total final geral
        Dim ExtensoEstrangeira As New ExtensoEstrangeira
        Dim Extenso As New ValorExtenso
        Dim ExtensoDolar As String = ""
        Dim ExtensoReal As String = ""
        Dim ExtensoEuro As String = ""

        ds = Con.ExecutarQuery("select ID_MOEDA, moeda, SUM(valor) valor, case  when ID_MOEDA = 1 then
 FORMAT(SUM(valor) , 'C', 'en-us') 
 else
 FORMAT(SUM(valor) , 'C', 'pt-br')
 end  AS ValorFormatado from [dbo].[View_Total_Cotacao] where ID_COTACAO = " & Request.QueryString("c") & " group by ID_MOEDA,MOEDA")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows

                If TotalFinal = "" Then
                    TotalFinal &= linha("moeda") & " " & linha("ValorFormatado") & " "
                Else
                    TotalFinal &= " + " & linha("moeda") & " " & linha("ValorFormatado") & " "
                End If

                If linha("ID_MOEDA") = 1 Then
                    'dolar
                    TotalFinal &= " (" & ExtensoEstrangeira.NumberToDolar(linha("valor")) & ") "

                ElseIf linha("ID_MOEDA") = 124 Then
                    'real
                    TotalFinal &= " (" & Extenso.NumeroToExtenso(linha("valor")) & ") "

                ElseIf linha("ID_MOEDA") = 164 Then
                    'euro
                    TotalFinal &= " (" & ExtensoEstrangeira.NumberToEuro(linha("valor")) & ") "

                End If
            Next
        End If
        TotalFinal = TotalFinal.Replace("+ -", "-").Replace("R$", "").Replace("$", "")

        lblTotalFinal.Text = SubstituiCaracteresEspeciais(TotalFinal)


    End Sub

    Sub MEDIDASAEREO()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        ds = Con.ExecutarQuery("SELECT QTD_CAIXA ,VL_LARGURA ,VL_ALTURA ,VL_COMPRIMENTO FROM TB_COTACAO_MERCADORIA_DIMENSAO  WHERE ID_COTACAO = " & Request.QueryString("c") & " ORDER BY ID DESC ")
        If ds.Tables(0).Rows.Count > 0 Then

            Dim tabela As String = "<table style='font-family:Arial;font-size:10px;'><tr>"
            tabela &= "<th style='padding-right:20px'>Qtd. Embalagem</th>"
            tabela &= "<th style='padding-right:20px'>Comprimento</th>"
            tabela &= "<th style='padding-right:20px'>Largura</th>"
            tabela &= "<th style='padding-right:20px'>Altura</th></tr>"
            For Each linha As DataRow In ds.Tables(0).Rows
                tabela &= "<tr><td style='padding-right:20px'>" & linha("QTD_CAIXA") & "</td>"
                tabela &= "<td style='padding-right:20px'>" & linha("VL_COMPRIMENTO") & "</td>"
                tabela &= "<td style='padding-right:20px'>" & linha("VL_LARGURA") & "</td>"
                tabela &= "<td style='padding-right:20px'>" & linha("VL_ALTURA") & "</td></tr>"


            Next
            tabela &= "</table>"
            divMedidasAereo.InnerHtml = tabela
        End If
    End Sub
    Sub CARGA()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim NM_TIPO_CARGA As String = ""
        ds = Con.ExecutarQuery("SELECT ISNULL(NM_TIPO_CARGA,'')NM_TIPO_CARGA FROM TB_TIPO_CARGA WHERE ID_TIPO_CARGA = (SELECT ID_TIPO_CARGA  FROM TB_COTACAO WHERE ID_COTACAO = " & Request.QueryString("c") & " )")
        If ds.Tables(0).Rows.Count > 0 Then
            NM_TIPO_CARGA = ds.Tables(0).Rows(0).Item("NM_TIPO_CARGA").ToString
            lblTipoCargaFCL.Text = "<br/><strong>Tipo:</strong>" & NM_TIPO_CARGA
            lblTipoCargaLCL.Text = "<br/><strong>&nbsp;Tipo:</strong>" & NM_TIPO_CARGA
            ds = Con.ExecutarQuery("SELECT sum(VL_CARGA)VL_CARGA,ID_MOEDA_CARGA, (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = ID_MOEDA_CARGA)MOEDA_CARGA FROM TB_COTACAO_MERCADORIA  WHERE ID_COTACAO = " & Request.QueryString("c") & " AND ISNULL(ID_MOEDA_CARGA,0) <>  0  group by ID_MOEDA_CARGA")
            If ds.Tables(0).Rows.Count > 0 Then
                Dim tabela As String = "<table style='font-family:Arial;font-size:10px;'><tr>"
                tabela &= "<th style='padding-right:20px'>Moeda</th>"
                tabela &= "<th style='padding-right:20px'>Valor da Mercadoria</th></tr>"

                For Each linha As DataRow In ds.Tables(0).Rows
                    tabela &= "<tr><td style='padding-right:20px'>" & linha("MOEDA_CARGA") & "</td>"
                    If linha("MOEDA_CARGA") = "USD" Then
                        tabela &= "<td style='padding-right:20px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VL_CARGA")).Replace("$", "") & "</td></tr>"
                    Else
                        tabela &= "<td style='padding-right:20px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VL_CARGA")).Replace("R$", "") & "</td></tr>"
                    End If


                Next

                tabela &= "</table>"
                divCargaFCL.InnerHtml = tabela
                divCargaLCL.InnerHtml = tabela
            End If
        End If

    End Sub

    Sub CONTAINER()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT NM_TIPO_CONTAINER, ISNULL(VL_FRETE_VENDA_UNITARIO,0)VALOR,QT_CONTAINER QTD,QT_DIAS_FREETIME
FROM TB_COTACAO_MERCADORIA A
LEFT JOIN TB_TIPO_CONTAINER B ON B.ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER WHERE ID_COTACAO =" & Request.QueryString("c") & "  and A.QT_CONTAINER > 0 ")
        Dim tabela As String = " <div class='linha-colorida2'>CONTAINER</div>
<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
        tabela &= "<th style='padding-right:10px'>Tipo Container</th>"
        tabela &= "<th style='padding-left:10px;padding-right:10px'>Qtd.</th>"
        tabela &= "<th style='padding-left:10px;padding-right:10px'>FreeTime</th>"
        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Unit.</th>"
        tabela &= "<th style='padding-left:10px;padding-right:10px'>Moeda</th></tr>"

        For Each linha As DataRow In ds.Tables(0).Rows
            tabela &= "<tr><td style='padding-right:10px'>" & linha("NM_TIPO_CONTAINER") & "</td>"
            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("QTD") & "</td>"
            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("QT_DIAS_FREETIME") & "</td>"
            If Session("MOEDA_CNTR") = "USD" Then
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", linha("VALOR")).Replace("$", "") & "</td>"
            Else
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", linha("VALOR")).Replace("R$", "") & "</td>"
            End If
            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & Session("MOEDA_CNTR") & "</td></tr>"
        Next

        tabela &= "</table>"
        divConteudoDinamico.InnerHtml = tabela


    End Sub

    Public Shared Function SubstituiCaracteresEspeciais(ByVal strline As String) As String

        strline = strline.Replace("ã", "a")
        strline = strline.Replace("Ã"c, "A"c)
        strline = strline.Replace("â"c, "a"c)
        strline = strline.Replace("Â"c, "A"c)
        strline = strline.Replace("á"c, "a"c)
        strline = strline.Replace("Á"c, "A"c)
        strline = strline.Replace("à"c, "a"c)
        strline = strline.Replace("À"c, "A"c)
        strline = strline.Replace("ç"c, "c"c)
        strline = strline.Replace("Ç"c, "C"c)
        strline = strline.Replace("é"c, "e"c)
        strline = strline.Replace("É"c, "E"c)
        strline = strline.Replace("Ê"c, "E"c)
        strline = strline.Replace("ê"c, "e"c)
        strline = strline.Replace("õ"c, "o"c)
        strline = strline.Replace("Õ"c, "O"c)
        strline = strline.Replace("ó"c, "o"c)
        strline = strline.Replace("Ó"c, "O"c)
        strline = strline.Replace("ô"c, "o"c)
        strline = strline.Replace("Ô"c, "O"c)
        strline = strline.Replace("ú"c, "u"c)
        strline = strline.Replace("Ú"c, "U"c)
        strline = strline.Replace("ü"c, "u"c)
        strline = strline.Replace("Ü"c, "U"c)
        strline = strline.Replace("í"c, "i"c)
        strline = strline.Replace("Í"c, "I"c)
        strline = strline.Replace("ª"c, "a"c)
        strline = strline.Replace("º"c, "o"c)
        strline = strline.Replace("°"c, "o"c)
        strline = strline.Replace("&"c, "e"c)
        strline = strline.Replace("–", "&mdash;")

        Return strline

    End Function
End Class