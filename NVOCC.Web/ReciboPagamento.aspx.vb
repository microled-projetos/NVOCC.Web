﻿Public Class ReciboPagamento
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
                Dim CIDADE As String = ""
                Dim ID As String = Request.QueryString("id")
                ds = Con.ExecutarQuery("SELECT A.ID_FATURAMENTO,A.NR_RECIBO,CONVERT(VARCHAR,A.DT_RECIBO,103)DT_RECIBO,A.NM_CLIENTE,A.CNPJ,A.INSCR_ESTADUAL,A.INSCR_MUNICIPAL,A.ENDERECO,A.NR_ENDERECO,A.BAIRRO,A.CIDADE,A.ESTADO,A.CEP,ISNULL(VL_ISS,0)VL_ISS
FROM TB_FATURAMENTO A WHERE ID_FATURAMENTO =" & ID)
                If ds.Tables(0).Rows.Count > 0 Then

                    If IsDBNull(ds.Tables(0).Rows(0).Item("NR_RECIBO")) And IsDBNull(ds.Tables(0).Rows(0).Item("DT_RECIBO")) Then
                        Dim NumeracaoDoc As New NumeracaoDoc
                        Dim numero As String = NumeracaoDoc.Numerar(2)

                        Con.ExecutarQuery("UPDATE [dbo].[TB_FATURAMENTO] SET DT_RECIBO = getdate(), NR_RECIBO = '" & numero & "' WHERE ID_FATURAMENTO =" & ID)
                        Con.ExecutarQuery("UPDATE [dbo].[TB_NUMERACAO] SET NR_RECIBO = '" & numero & "'")

                        lblEmissao.Text = Now.Date.ToString("dd/MM/yyyy")
                        lblNumeroRecibo.Text = numero
                    Else
                        lblEmissao.Text = ds.Tables(0).Rows(0).Item("DT_RECIBO")
                        lblNumeroRecibo.Text = ds.Tables(0).Rows(0).Item("NR_RECIBO")

                    End If

                    'If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_RECIBO")) Then
                    '    lblEmissao.Text = ds.Tables(0).Rows(0).Item("DT_RECIBO")
                    'End If
                    'If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_RECIBO")) Then
                    '    lblNumeroRecibo.Text = ds.Tables(0).Rows(0).Item("NR_RECIBO")
                    'End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CLIENTE")) Then
                        lblEmpresa.Text = ds.Tables(0).Rows(0).Item("NM_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("CIDADE")) Then
                        CIDADE = ds.Tables(0).Rows(0).Item("CIDADE")
                    End If

                    Dim dsProcesso As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,A.NR_PROCESSO,A.NR_BL,DT_CHEGADA,(SELECT NM_SERVICO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SERVICO,GRAU,ID_BL_MASTER,
 (SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)  NR_BL_MASTER,
  (SELECT DT_CHEGADA FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)  DT_CHEGADA_MASTER
  ,(SELECT top 1 NR_REFERENCIA_CLIENTE FROM VW_REFERENCIA_CLIENTE WHERE ID_BL = A.ID_BL_MASTER)NR_REFERENCIA_CLIENTE
FROM TB_BL A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL = A.ID_BL
WHERE GRAU = 'C' AND ID_CONTA_PAGAR_RECEBER =  (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & ID & " )")
                    If dsProcesso.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(dsProcesso.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                            lblNRef.Text = dsProcesso.Tables(0).Rows(0).Item("NR_PROCESSO")
                        End If

                        If Not IsDBNull(dsProcesso.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                            lblSRef.Text = dsProcesso.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
                        End If

                        If Not IsDBNull(dsProcesso.Tables(0).Rows(0).Item("NR_BL")) Then
                            lblHBL.Text = dsProcesso.Tables(0).Rows(0).Item("NR_BL")
                        End If

                        If Not IsDBNull(dsProcesso.Tables(0).Rows(0).Item("NR_BL_MASTER")) Then
                            lblMBL.Text = dsProcesso.Tables(0).Rows(0).Item("NR_BL_MASTER")
                        End If
                    End If




                    Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT DISTINCT ID_BL,ID_BL_TAXA,(SELECT CD_PR FROM TB_CONTA_PAGAR_RECEBER WHERE ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER)CD_PR,
(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = (SELECT ID_ITEM_DESPESA FROM TB_BL_TAXA WHERE ID_BL_TAXA = A.ID_BL_TAXA))ITEM_DESPESA,
(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = (SELECT ID_MOEDA FROM TB_BL_TAXA WHERE ID_BL_TAXA = A.ID_BL_TAXA))MOEDA,

VL_LANCAMENTO,VL_CAMBIO,ISNULL(VL_LANCAMENTO,0)VALORES,CASE WHEN ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_TIPO_ITEM_DESPESA = 1) THEN
VL_ISS
ELSE 0 END VL_ISS
FROM TB_CONTA_PAGAR_RECEBER_ITENS A
WHERE ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO =" & ID & " )")
                    Dim Total As String
                    Dim valores As Decimal = 0
                    If dsTaxas.Tables(0).Rows.Count > 0 Then

                        Dim tabela As String = " <br/><table style='font-family:Arial;font-size:10px;'><tr>"
                        tabela &= "<th style='padding-right:10px'>Taxa</th>"
                        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valores R$</th>"
                        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>ISS</th></tr>"

                        For Each linha As DataRow In dsTaxas.Tables(0).Rows
                            tabela &= "<tr><td style='padding-right:10px'>" & linha("ITEM_DESPESA") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VALORES") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_ISS") & "</td></tr>"
                            valores = valores + linha("VALORES")
                            If CIDADE = "SANTOS" Then
                                valores = valores - linha("VL_ISS")
                            End If

                        Next
                        Total = FormatCurrency(valores)
                        tabela &= "<tr><td style='padding-left:10px;padding-right:10px;float: right;'></td><td style='padding-left:10px;padding-right:10px'>Total: " & Total & "</td></tr>"
                        tabela &= "</table>"
                        divConteudoDinamico.InnerHtml = tabela

                    End If
                    Dim ValorExtenso As New ValorExtenso
                    lblValorExtenso.Text = ValorExtenso.NumeroToExtenso(valores)
                    lblValor.Text = Total.ToString

                    Con.Fechar()
                End If
            End If

        End If
        Con.Fechar()
    End Sub

End Class