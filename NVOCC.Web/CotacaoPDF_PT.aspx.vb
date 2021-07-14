Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.tool.xml
Public Class CotacaoPDF_PT
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT A.ID_COTACAO,ID_TIPO_ESTUFAGEM,
A.NR_COTACAO,
CONVERT(VARCHAR,A.DT_VALIDADE_COTACAO,103)DT_VALIDADE_COTACAO,
(SELECT NM_TIPO_FREQUENCIA FROM TB_TIPO_FREQUENCIA WHERE ID_TIPO_FREQUENCIA = A.ID_TIPO_FREQUENCIA)NM_TIPO_FREQUENCIA,
VL_FREQUENCIA,
A.ID_ANALISTA_COTACAO,
(SELECT NOME FROM TB_USUARIO WHERE ID_USUARIO = A.ID_ANALISTA_COTACAO )NOME_ANALISTA,
A.ID_INCOTERM,
(SELECT NM_INCOTERM FROM TB_INCOTERM WHERE ID_INCOTERM = A.ID_INCOTERM )NOME_INCOTERM,
A.ID_TIPO_ESTUFAGEM,
(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM )NOME_ESTUFAGEM,
A.ID_DESTINATARIO_COMERCIAL,
A.ID_CLIENTE,
(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE )NOME_CLIENTE,
(SELECT CNPJ FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE )CNPJ_CLIENTE,
(SELECT CPF FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_CLIENTE )CPF_CLIENTE,
A.ID_CONTATO,
A.ID_SERVICO,
(SELECT NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO )NOME_SERVICO,
A.ID_VENDEDOR,
A.VL_PESO_TAXADO,
A.VL_TOTAL_PESO_BRUTO,
A.VL_TOTAL_M3,
ID_PORTO_DESTINO,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_DESTINO )PORTO_DESTINO,
ID_PORTO_ORIGEM,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ORIGEM )PORTO_ORIGEM,
ID_VIA_ROTA,
(SELECT NM_VIA_ROTA FROM TB_VIA_ROTA WHERE ID_VIA_ROTA = A.ID_VIA_ROTA )VIA_ROTA,
QT_TRANSITTIME_MEDIA,
VL_TOTAL_FRETE_VENDA,
VL_TOTAL_FRETE_VENDA_MIN,
VL_TOTAL_FRETE_VENDA_CALCULADO,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_FRETE )MOEDA,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA1 )PORTO_ESCALA1,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA2 )PORTO_ESCALA2,
(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO_ESCALA3 )PORTO_ESCALA3
FROM  TB_COTACAO A
    WHERE A.ID_COTACAO = " & Request.QueryString("c"))
        If ds.Tables(0).Rows.Count > 0 Then
            ' PORTUGUES
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NOME_SERVICO")) Then
                lblTitulo.Text = ds.Tables(0).Rows(0).Item("NOME_SERVICO") & " (" & ds.Tables(0).Rows(0).Item("NOME_ESTUFAGEM") & ")"
            End If

            If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                CONTAINER()
                detalhesCarga.Visible = False
            Else
                detalhesCarga.Visible = True
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NOME_CLIENTE")) Then
                lblCliente.Text = ds.Tables(0).Rows(0).Item("NOME_CLIENTE")
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
                lblPesoBruto.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_PESO_BRUTO").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_TIPO_FREQUENCIA")) Then
                lblFrequencia.Text = ds.Tables(0).Rows(0).Item("NM_TIPO_FREQUENCIA").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_FREQUENCIA")) Then
                lblValorFrequencia.Text = " - " & ds.Tables(0).Rows(0).Item("VL_FREQUENCIA").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_M3")) Then
                lblM3.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_M3").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                lblPesoTaxado.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TAXADO").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ORIGEM")) Then
                lblOrigem.Text = ds.Tables(0).Rows(0).Item("PORTO_ORIGEM").ToString
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_DESTINO")) Then
                lblDestino.Text = ds.Tables(0).Rows(0).Item("PORTO_DESTINO").ToString
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ESCALA1")) Then
                lblEscalas.Text &= " / " & ds.Tables(0).Rows(0).Item("PORTO_ESCALA1").ToString
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ESCALA2")) Then
                lblEscalas.Text &= " / " & ds.Tables(0).Rows(0).Item("PORTO_ESCALA2").ToString
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("PORTO_ESCALA3")) Then
                lblEscalas.Text &= " / " & ds.Tables(0).Rows(0).Item("PORTO_ESCALA3").ToString
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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO")) Then
                Dim tabela As String = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
                tabela &= "<th style='padding-right:10px'>Taxa</th>"
                tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Moeda</th>"
                If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                    tabela &= "<th style='padding-right:10px'>Base de Calc.</th>"
                    tabela &= "<th style='padding-right:10px'>Valor Min.</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Tarifa</th>"
                End If
                tabela &= "<th style='padding-right:10px'>Valor</th>"




                For Each linha As DataRow In ds.Tables(0).Rows
                    tabela &= "</tr><tr><td style='padding-right:10px'>FRETE INTERNACIONAL</td>"
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"

                    If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                        tabela &= "<td style='padding-right:10px'>POR TON / M³</td>"
                        tabela &= "<td style='padding-right:10px'>" & linha("VL_TOTAL_FRETE_VENDA_MIN") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_TOTAL_FRETE_VENDA") & "</td>"
                    End If
                    tabela &= "<td style='padding-right:10px'>" & linha("VL_TOTAL_FRETE_VENDA_CALCULADO") & "</td>"
                    tabela &= "</tr>"

                Next

                tabela = tabela.Replace("³", "<sup>3</sup>")
                tabela = tabela.Replace("ã", "&atilde;")
                tabela = tabela.Replace("Á", "&Aacute;")
                tabela &= "</table>"
                divConteudofrete.InnerHtml = tabela
                lblTotalFinalFrete.Text = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_CALCULADO").ToString & " " & ds.Tables(0).Rows(0).Item("MOEDA").ToString

            End If




            TAXAS()
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
A.VL_TAXA_VENDA,
A.VL_TAXA_VENDA_MIN,
A.VL_TAXA_VENDA_CALCULADO,
case when (A.VL_TAXA_VENDA <> 0 and  A.VL_TAXA_VENDA_CALCULADO = 0) then 'Taxa não calculada' else  cast(a.VL_TAXA_VENDA_CALCULADO as varchar) end as CALCULADO,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA ) TAXA 
FROM TB_COTACAO_TAXA A
WHERE FL_DECLARADO = 1 AND ID_DESTINATARIO_COBRANCA <> 3
    AND A.ID_COTACAO = " & Request.QueryString("c"))
        If ds.Tables(0).Rows.Count > 0 Then

            Dim tabela As String = "<br/><table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
            tabela &= "<th style='padding-right:10px'>Taxa</th>"
            tabela &= "<th style='padding-left:10px;padding-right:10px'>Moeda</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Base de Calc.</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Min</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Calc.</th></tr>"

            For Each linha As DataRow In ds.Tables(0).Rows
                tabela &= "<tr><td style='padding-right:10px'>" & linha("TAXA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_TAXA_VENDA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("BASE_CALCULO") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_TAXA_VENDA_MIN") & "</td>"

                If Not IsDBNull(linha.Item("CALCULADO")) Then
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("CALCULADO") & "</td>"
                End If
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

            ds = Con.ExecutarQuery("SELECT CAST(SUM(A.VL_TAXA_VENDA_CALCULADO)AS varchar) + ' ' + (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA)as  descricao  FROM TB_COTACAO_TAXA A  WHERE ID_COTACAO = " & Request.QueryString("c") & " AND FL_DECLARADO = 1 AND A.ID_DESTINATARIO_COBRANCA <> 3 GROUP BY A.ID_MOEDA_VENDA ")
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    If DescTotalOrigem = "" Then
                        DescTotalOrigem &= linha("descricao")
                    Else
                        DescTotalOrigem &= " + " & linha("descricao")
                    End If
                Next
            End If
            DescTotalOrigem = DescTotalOrigem.Replace("+ -", "-")

            lblTotalTaxasOrigem.Text = DescTotalOrigem

        End If







        '-----------------------------------------------------------------------------------------







        'DESTINO
        ds = Con.ExecutarQuery("SELECT A.ID_COTACAO,
(SELECT ID_TIPO_ITEM_DESPESA FROM TB_TIPO_ITEM_DESPESA WHERE ID_TIPO_ITEM_DESPESA = (SELECT ID_TIPO_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA)) ID_TIPO_ITEM_DESPESA,
A.ID_COTACAO_TAXA,
A.FL_DECLARADO,
(SELECT NM_BASE_CALCULO_TAXA FROM TB_BASE_CALCULO_TAXA WHERE ID_BASE_CALCULO_TAXA = A.ID_BASE_CALCULO_TAXA )BASE_CALCULO,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA )MOEDA,
A.VL_TAXA_VENDA,
A.VL_TAXA_VENDA_MIN,
A.VL_TAXA_VENDA_CALCULADO,
case when (A.VL_TAXA_VENDA <> 0 and  A.VL_TAXA_VENDA_CALCULADO = 0) then 'Taxa não calculada' else  cast(a.VL_TAXA_VENDA_CALCULADO as varchar) end as CALCULADO,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = A.ID_ITEM_DESPESA ) TAXA 
FROM TB_COTACAO_TAXA A
WHERE FL_DECLARADO = 0 AND ID_DESTINATARIO_COBRANCA <> 3
    AND A.ID_COTACAO = " & Request.QueryString("c"))
        If ds.Tables(0).Rows.Count > 0 Then

            'Dim tabela As String = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'>"
            Dim tabela As String = "<br/><table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
            tabela &= "<th style='padding-right:10px'>Taxa</th>"
            tabela &= "<th style='padding-left:10px;padding-right:10px'>Moeda</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Base de Calc.</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Min</th>"
            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Calc.</th></tr>"
            For Each linha As DataRow In ds.Tables(0).Rows
                tabela &= "<tr><td style='padding-right:10px'>" & linha("TAXA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("MOEDA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_TAXA_VENDA") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("BASE_CALCULO") & "</td>"
                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_TAXA_VENDA_MIN") & "</td>"

                If Not IsDBNull(linha.Item("CALCULADO")) Then
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("CALCULADO") & "</td>"
                End If
                tabela &= "</tr>"

            Next
            tabela = tabela.Replace("³", "<sup>3</sup>")
            tabela = tabela.Replace("Ã", "&Atilde;")
            tabela = tabela.Replace("ã", "&atilde;")
            tabela = tabela.Replace("Ç", "&Ccedil;")
            tabela &= "</table>"
            divTaxaDestino.InnerHtml = tabela

            Dim DescTotalDestino As String = ""

            ' lblTaxasDestinoCalc.Text = taxasDestinoCalc


            'total destino
            ds = Con.ExecutarQuery("SELECT CAST(SUM(A.VL_TAXA_VENDA_CALCULADO)AS varchar) + ' ' + (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA)as  descricao  FROM TB_COTACAO_TAXA A  WHERE ID_COTACAO = " & Request.QueryString("c") & " AND FL_DECLARADO = 0  AND ID_DESTINATARIO_COBRANCA <> 3 GROUP BY A.ID_MOEDA_VENDA ")
            If ds.Tables(0).Rows.Count > 0 Then


                For Each linha As DataRow In ds.Tables(0).Rows
                    If DescTotalDestino = "" Then
                        DescTotalDestino &= linha("descricao")
                    Else

                        DescTotalDestino &= " + " & linha("descricao")


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

        ds = Con.ExecutarQuery("SELECT CAST(SUM(ISNULL(A.VL_TAXA_VENDA_CALCULADO,0))AS varchar) + ' ' + (SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_VENDA)as  descricao  FROM TB_COTACAO_TAXA A  WHERE ID_COTACAO = " & Request.QueryString("c") & " AND ID_DESTINATARIO_COBRANCA <> 3 GROUP BY A.ID_MOEDA_VENDA ")
        If ds.Tables(0).Rows.Count > 0 Then


            For Each linha As DataRow In ds.Tables(0).Rows
                If TotalFinalTaxas = "" Then
                    TotalFinalTaxas &= linha("descricao")
                Else
                    TotalFinalTaxas &= " + " & linha("descricao")
                End If
            Next
        End If

        TotalFinalTaxas = TotalFinalTaxas.Replace("+ -", "-")


        lblTotalFinalTaxas.Text = TotalFinalTaxas



















        'total final geral
        ds = Con.ExecutarQuery("select cast(sum(valor)as varchar) + ' '+MOEDA as TOTAL  from [dbo].[View_Total_Cotacao] where ID_COTACAO = " & Request.QueryString("c") & " group by ID_MOEDA,MOEDA")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                If TotalFinal = "" Then
                    TotalFinal &= linha("TOTAL")
                Else
                    TotalFinal &= " + " & linha("TOTAL")
                End If
            Next
        End If
        TotalFinal = TotalFinal.Replace("+ -", "-")

        lblTotalFinal.Text = TotalFinal


    End Sub

    Sub CONTAINER()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT NM_TIPO_CONTAINER, CAST((VL_FRETE_VENDA)AS NUMERIC(13,2))VALOR,SUM(QT_CONTAINER)QTD
FROM TB_COTACAO_MERCADORIA A
LEFT JOIN TB_TIPO_CONTAINER B ON B.ID_TIPO_CONTAINER = A.ID_TIPO_CONTAINER WHERE ID_COTACAO = " & Request.QueryString("c") & " and A.QT_CONTAINER > 0 
GROUP BY B.NM_TIPO_CONTAINER,VL_FRETE_VENDA,QT_CONTAINER
")
        Dim tabela As String = " <div class='linha-colorida2'>CONTAINER</div>
<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
        tabela &= "<th style='padding-right:10px'>Tipo Container</th>"
        tabela &= "<th style='padding-left:10px;padding-right:10px'>Qtd.</th>"
        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valor Unit.</th></tr>"

        For Each linha As DataRow In ds.Tables(0).Rows
            tabela &= "<tr><td style='padding-right:10px'>" & linha("NM_TIPO_CONTAINER") & "</td>"
            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("QTD") & "</td>"
            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VALOR") & "</td></tr>"
        Next

        tabela &= "</table>"
        divConteudoDinamico.InnerHtml = tabela


    End Sub
End Class