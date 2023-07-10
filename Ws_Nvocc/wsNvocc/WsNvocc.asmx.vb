Imports System.Xml
Imports System.Net
Imports System.Web.Services
Imports System.ComponentModel
Imports Oracle.ManagedDataAccess.Client
Imports System.Data.OleDb
Imports RestSharp


' Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WsNvocc
    Inherits System.Web.Services.WebService

    Dim erroValor As Boolean
    Dim rsEmpresa As DataSet
    Dim stringDoXml As String = ""
    Dim doc As New XmlDocument
    Dim raiz As XmlElement
    Dim raizC As XmlElement
    Dim No As XmlElement
    Dim noNFE As XmlElement
    Dim noLoteRPS As XmlElement
    Dim noText As XmlText
    Dim att As XmlAttribute
    Dim noListaRPS As XmlElement
    Public noRPS As XmlElement
    Dim noInfRps As XmlElement
    Dim noIdentRPS As XmlElement
    Dim noServicos As XmlElement
    Dim noValServ As XmlElement
    Dim noPrestador As XmlElement
    Dim noTomador As XmlElement
    Dim noIdentTomador As XmlElement
    Dim noCPFCNPJ As XmlElement
    Dim docTomador As String
    Dim noEnderecoTom As XmlElement
    Dim noContato As XmlElement
    Dim Con As New Conexao_sql
    Dim Funcoes As New Funcoes
    Public msgValidacao As String = ""

    'INTEGRACAO
    <WebMethod()>
    Public Function IntegraNFePrefeitura(ByVal RPS As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String, ByVal Reprocessamento As Boolean) As String
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_FATURAMENTO,NR_RPS FROM TB_FATURAMENTO WHERE NR_RPS = '" & RPS & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            For I = 0 To ds.Tables(0).Rows.Count - 1

                If Reprocessamento Then
                    Call montaLoteRPS(Funcoes.NNull(ds.Tables(0).Rows(I)("ID_FATURAMENTO").ToString, 0), 1, 1)

                Else

                    Call montaLoteRPS(Funcoes.NNull(ds.Tables(0).Rows(I)("ID_FATURAMENTO").ToString, 0), , 1)
                End If


            Next

        Else
            Return "00000000RPS NAO ENCONTRADA NO BANCO DE DADOS"

        End If

    End Function
    Public Sub montaLoteRPS(ByVal IDFatura As Long, Optional ByVal Reprocessamento As Boolean = False, Optional Cod_Empresa As Integer = 1)

        Con.Conectar()
        Dim ret As Boolean = False
        Dim nomeArquivo As String
        Try
            doc = Nothing
            doc = New XmlDocument

            Dim rsRPSs As DataSet
            Dim sSql As String
            If Reprocessamento Then
                sSql = "SELECT * FROM VW_FILA_LOTE_RPS WHERE IDFATURA =" & IDFatura

            Else

                sSql = "SELECT * FROM VW_FILA_LOTE_RPS WHERE STATUS_NFE IN (0,4) AND IDFATURA = " & IDFatura
            End If
            rsRPSs = Con.ExecutarQuery(sSql)
            If rsRPSs.Tables(0).Rows.Count <= 0 Then
                Exit Sub
            End If

            Dim NFeNamespacte As String = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd"


            raiz = doc.CreateElement("EnviarLoteRpsEnvio", NFeNamespacte)


            Dim loteNumero As Long

            If Reprocessamento Then
                sSql = "SELECT NR_LOTE FROM VW_FILA_LOTE_RPS WHERE IDFATURA =" & IDFatura
                Dim rsNumero As DataSet = Con.ExecutarQuery(sSql)
                loteNumero = rsNumero.Tables(0).Rows(0)("NR_LOTE").ToString
            Else

                Dim ConOracle As New Conexao_oracle
                ConOracle.Conectar()
                sSql = "SELECT SEQ_LOTE_NFSE.NEXTVAL FROM DUAL "
                Dim rsNumero As DataTable = ConOracle.Consultar(sSql)
                loteNumero = rsNumero.Rows(0)("NEXTVAL").ToString
            End If

            sSql = "UPDATE TB_FATURAMENTO SET NR_LOTE =  " & loteNumero & " WHERE ID_FATURAMENTO = " & IDFatura
            Con.ExecutarQuery(sSql)

            sSql = "INSERT INTO TB_LOTE_NFSE (ID_FATURAMENTO, DT_ENVIO_LOTE, NUMERO_RPS) "
            sSql = sSql & " VALUES (" & IDFatura & ",GETDATE(),'" & Funcoes.NNull(rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString, 0) & "') "
            Con.ExecutarQuery(sSql)


            noLoteRPS = doc.CreateElement("LoteRps", NFeNamespacte)

            att = doc.CreateAttribute("Id")
            att.Value = "_10" & Format(loteNumero, "000000")
            noLoteRPS.Attributes.Append(att)


            rsEmpresa = Con.ExecutarQuery("SELECT CNPJ,NM_RAZAO,IM,NOME_CERTIFICADO,TIPO_RPS,NAT_OPERACAO,SIMPLES,INC_CULTURAL,CIDADE_IBGE,
CD_ATIVIDADE_RPS AS 'COD_SERVICO',
CD_TRIBUTACAO_RPS AS 'COD_TRIB_MUN', 
CD_ATIVIDADE_COMISSAO_RPS AS 'COD_SERVICO_COMISSAO',
CD_TRIBUTACAO_COMISSAO_RPS AS 'COD_TRIB_MUN_COMISSAO', 
FL_INTERMEDIACAO,
FL_PROFIT,
B.ID_SERVICO, 
'10.05' AS 'COD_SERVICO_INTERMEDIACAO' , 
'5250803' AS 'CD_TRIBUTACAO_INTERMEDIACAO' 
FROM TB_EMPRESAS A
CROSS JOIN View_Faturamento B 
LEFT JOIN TB_SERVICO C On C.ID_SERVICO = B.ID_SERVICO
WHERE ID_FATURAMENTO = " & IDFatura)
            GRAVARLOG(IDFatura, "INICIA MONTAGEM DO XML")
            NFeNamespacte = "http://www.ginfes.com.br/tipos_v03.xsd"

            No = doc.CreateElement("NumeroLote", NFeNamespacte)
            noText = doc.CreateTextNode("10" & Format(loteNumero, "000000"))
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            No = doc.CreateElement("Cnpj", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("CNPJ").ToString))
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            No = doc.CreateElement("InscricaoMunicipal", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("IM").ToString))
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            No = doc.CreateElement("QuantidadeRps", NFeNamespacte)
            noText = doc.CreateTextNode("1")
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            noListaRPS = doc.CreateElement("ListaRps", NFeNamespacte)
            noRPS = doc.CreateElement("Rps", NFeNamespacte)

            Dim DADOS As String
            DADOS = "LOTE :" & loteNumero & " | RPS Nº " & rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString


            GRAVARLOG(IDFatura, "CHAMA ROTINA QUE MONTA INFORMACOES DO XML")

            Call montaInfRps(loteNumero, NFeNamespacte, rsRPSs.Tables(0), Cod_Empresa)


            If erroValor Then
                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, CRITICA, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString & ",'ENCONTRADA DIVERGENCIA DE VALORES',GETDATE(),'" & rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString & "','" & loteNumero & "') "
                Con.ExecutarQuery(sSql)


                sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE ID_FATURAMENTO =" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString



                Con.ExecutarQuery(sSql)

                sSql = "UPDATE TB_LOTE_NFSE SET CRITICA ='ENCONTRADA DIVERGENCIA DE VALORES' WHERE ID_FATURAMENTO =" & loteNumero
                Con.ExecutarQuery(sSql)


                Exit Sub
            End If

            noRPS.AppendChild(noInfRps)

            noListaRPS.AppendChild(noRPS)
            noLoteRPS.AppendChild(noListaRPS)

            raiz.AppendChild(noLoteRPS)

            doc.AppendChild(raiz)

            nomeArquivo = Funcoes.diretorioLoteRps & "NFsE_" & Format(loteNumero, "00000000") & ".xml"
            GRAVARLOG(IDFatura, "GRAVAR XML ANTES DE ASSINAR")
            doc.Save(nomeArquivo)
            doc.Load(nomeArquivo)


            Dim docAssinado As New XmlDocument
            docAssinado.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?>" & Funcoes.AssinarXML(Funcoes.tiraCaracEspXML(doc.OuterXml), "InfRps", Cod_Empresa))
            GRAVARLOG(IDFatura, "SALVAR XML ASSINADO")
            docAssinado.Save(nomeArquivo)
            GRAVARLOG(IDFatura, "VALIDAR XML")
            GRAVARLOG(IDFatura, nomeArquivo)
            GRAVARLOG(IDFatura, Funcoes.diretorioXSD & "\servico_enviar_lote_rps_envio_v03.xsd")
            If Not Funcoes.validaXMLXSD(nomeArquivo, Funcoes.diretorioXSD & "\servico_enviar_lote_rps_envio_v03.xsd", "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd") Then

                GRAVARLOG(IDFatura, "ERRO DE VALIDACAO")

                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, CRITICA, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString & ",'" & Mid(Funcoes.tiraCaracEspXML(msgValidacao), 1, 2000) & "',GETDATE(),'" & rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString & "','" & loteNumero & "') "
                Con.ExecutarQuery(sSql)

                sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE ID_FATURAMENTO =" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString


                Con.ExecutarQuery(sSql)



                sSql = "UPDATE TB_LOTE_NFSE SET CRITICA ='" & Funcoes.tiraCaracEspXML(msgValidacao) & "' WHERE ID_FATURAMENTO =" & loteNumero
                Con.ExecutarQuery(sSql)


                Exit Sub
            Else
                GRAVARLOG(IDFatura, "VALIDACAO DEU CERTO")
                sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 1 WHERE ID_FATURAMENTO =" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString
                Con.ExecutarQuery(sSql)

                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, NOME_ARQ_ENVIO, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString & ",'" & Right(nomeArquivo, 100) & "',GETDATE(),'" & rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString & "','" & loteNumero & "') "
                Con.ExecutarQuery(sSql)

            End If

            GRAVARLOG(loteNumero, "CHAMA ROTINA DE ENVIO")
            GRAVARLOG(loteNumero, nomeArquivo)
            Call EnviaXML(nomeArquivo, "LOTE-RPS", loteNumero, Cod_Empresa)


        Catch ex As Exception
            GRAVARLOG(IDFatura, ex.Message)
        End Try
    End Sub

    Public Sub montaInfRps(ByVal numeroLote As Long, Optional ByVal NFeNamespacte As String = "", Optional ByVal rsRPS As DataTable = Nothing, Optional Cod_Empresa As Integer = 1)

        Con.Conectar()
        Try

            erroValor = False

            noInfRps = doc.CreateElement("InfRps", NFeNamespacte)

            att = doc.CreateAttribute("Id")
            att.Value = "_20" & Format(numeroLote, "000000")
            noInfRps.Attributes.Append(att)

            noIdentRPS = doc.CreateElement("IdentificacaoRps", NFeNamespacte)

            No = doc.CreateElement("Numero", NFeNamespacte)
            noText = doc.CreateTextNode("1")
            noText = doc.CreateTextNode(rsRPS.Rows(0)("NUMERO_RPS").ToString)
            No.AppendChild(noText)
            noIdentRPS.AppendChild(No)

            No = doc.CreateElement("Serie", NFeNamespacte)
            noText = doc.CreateTextNode(rsRPS.Rows(0)("SERIE_RPS").ToString)
            No.AppendChild(noText)
            noIdentRPS.AppendChild(No)

            No = doc.CreateElement("Tipo", NFeNamespacte)
            noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("TIPO_RPS").ToString)
            No.AppendChild(noText)
            noIdentRPS.AppendChild(No)

            noInfRps.AppendChild(noIdentRPS)

            No = doc.CreateElement("DataEmissao", NFeNamespacte)

            Dim DataEmi As String = rsRPS.Rows(0)("DATA_EMISSAO").ToString
            noText = doc.CreateTextNode(Format(CDate(DataEmi), "yyyy-MM-dd") & "T" & Format(CDate(DataEmi), "HH:mm:ss"))
            No.AppendChild(noText)
            noInfRps.AppendChild(No)


            No = doc.CreateElement("NaturezaOperacao", NFeNamespacte)
            noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("NAT_OPERACAO").ToString)
            No.AppendChild(noText)
            noInfRps.AppendChild(No)

            No = doc.CreateElement("OptanteSimplesNacional", NFeNamespacte)
            noText = doc.CreateTextNode(IIf(Funcoes.NNull(rsEmpresa.Tables(0).Rows(0)("SIMPLES").ToString, 0) = 0, 2, 1))
            No.AppendChild(noText)
            noInfRps.AppendChild(No)

            No = doc.CreateElement("IncentivadorCultural", NFeNamespacte)
            noText = doc.CreateTextNode(IIf(Funcoes.NNull(rsEmpresa.Tables(0).Rows(0).Item("INC_CULTURAL").ToString, 0) = 0, 2, 1))
            No.AppendChild(noText)
            noInfRps.AppendChild(No)

            No = doc.CreateElement("Status", NFeNamespacte)
            noText = doc.CreateTextNode("1")
            No.AppendChild(noText)
            noInfRps.AppendChild(No)


            GRAVARLOG(rsRPS.Rows(0)("IDFATURA").ToString, "PEGA INFORMAÇOES DO PROCESSO PARA A DISCRIMINACAO")


            Dim SqlInfo As String = "SELECT NR_PROCESSO,NR_BL,ISNULL(NR_BL_MASTER,'')NR_BL_MASTER,CASE WHEN NR_PROCESSO LIKE '%L%' THEN 1 ELSE 0 END NFDELUCRO,GRAU FROM View_BL WHERE NR_PROCESSO = (SELECT NR_PROCESSO FROM  View_Faturamento WHERE ID_FATURAMENTO = " & rsRPS.Rows(0)("IDFATURA").ToString & ")"
            Dim Fatura As String = rsRPS.Rows(0)("NUMERO_RPS").ToString
            Dim Processo As String = ""
            Dim Ref As String = ""
            Dim MASTER As String = ""
            Dim HOUSE As String = ""
            Dim NFDELUCRO As Integer = 0
            Dim dsInfo As DataSet
            dsInfo = Con.ExecutarQuery(SqlInfo)
            If dsInfo.Tables(0).Rows.Count > 0 Then
                If dsInfo.Tables(0).Rows(0).Item("GRAU") = "C" Then
                    Processo = dsInfo.Tables(0).Rows(0)("NR_PROCESSO").ToString
                    MASTER = dsInfo.Tables(0).Rows(0)("NR_BL_MASTER").ToString
                    HOUSE = dsInfo.Tables(0).Rows(0)("NR_BL").ToString
                    NFDELUCRO = dsInfo.Tables(0).Rows(0)("NFDELUCRO").ToString
                Else
                    Processo = dsInfo.Tables(0).Rows(0)("NR_PROCESSO").ToString
                    MASTER = dsInfo.Tables(0).Rows(0)("NR_BL").ToString
                    HOUSE = ""
                    NFDELUCRO = dsInfo.Tables(0).Rows(0)("NFDELUCRO").ToString
                End If
            End If


            Dim noServicos As XmlElement
            Dim noValServ As XmlElement
            noServicos = doc.CreateElement("Servico", NFeNamespacte)

            Dim rsServicos As DataSet
            Dim sSql As String


            sSql = "SELECT ISNULL(OB_RPS,'')OB_RPS, 
SUM(ISNULL(VL_LIQUIDO,0))VALOR, 
SUM(ISNULL(VL_LIQUIDO,0)) - SUM(ISNULL(A.VL_ISS,0)) - SUM(ISNULL(B.VL_IR_NF,0)) AS VL_LIQUIDO, 
SUM(ISNULL(A.VL_ISS,0)) VL_ISS, 
0 VL_PIS,
0 VL_COFINS,
SUM(ISNULL(B.VL_IR_NF,0)) VL_IR, 
SUM(ISNULL(VL_LIQUIDO,0)) - SUM(ISNULL(B.VL_IR_NF,0)) AS VL_DESCONTANDO_IR, 
SUM(ISNULL(A.VL_ISS,0)) + SUM(ISNULL(VL_PIS,0)) + SUM(ISNULL(VL_COFINS,0)) + SUM(ISNULL(B.VL_IR_NF,0)) AS VL_IMPOSTOS,
CASE WHEN SUM(ISNULL(B.VL_IR_NF,0))> 0 THEN 1 ELSE 0 END FL_IR 
FROM TB_CONTA_PAGAR_RECEBER_ITENS A
LEFT JOIN TB_FATURAMENTO B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_RECEITA = 1 ) AND ID_FATURAMENTO IN (" & rsRPS.Rows(0)("IDFATURA").ToString & ") GROUP BY OB_RPS "

            GRAVARLOG(rsRPS.Rows(0)("IDFATURA").ToString, "INFORMACOES DE VALORES DA NOTA")

            rsServicos = Con.ExecutarQuery(sSql)

            For I = 0 To rsServicos.Tables(0).Rows.Count - 1
                If Val(Funcoes.NNull(rsServicos.Tables(0).Rows(0)(0).ToString, 0)) <> Val(Funcoes.NNull(rsServicos.Tables(0).Rows(0)(1).ToString, 0)) Then
                    sSql = "SELECT COUNT(1) FROM TB_FATURAMENTO WHERE ID_CONTA_PAGAR_RECEBER ='" & rsRPS.Rows(0)("ID_CONTA_PAGAR_RECEBER").ToString & "' "
                    sSql = sSql & " AND FL_RPS = 1 AND DT_CANCELAMENTO IS NULL"
                    If Funcoes.NNull(Con.ExecutarQuery(sSql).Tables(0).Rows(0)(0), 0) = 1 Then
                        erroValor = True
                        Exit Sub
                    End If
                End If
                noServicos = doc.CreateElement("Servico", NFeNamespacte)

                noValServ = doc.CreateElement("Valores", NFeNamespacte)

                No = doc.CreateElement("ValorServicos", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)


                No = doc.CreateElement("ValorPis", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_PIS").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)



                No = doc.CreateElement("ValorCofins", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_COFINS").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("ValorInss", NFeNamespacte)
                Dim valInss As Double = Funcoes.NNull(0, 0)
                noText = doc.CreateTextNode(Format(Double.Parse(valInss), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)


                No = doc.CreateElement("ValorIr", NFeNamespacte)
                If rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    Dim ValorIr As Double = Funcoes.NNull(0, 0)
                    ValorIr = FormatNumber(ValorIr, 2)
                    noText = doc.CreateTextNode(Format(Double.Parse(ValorIr), "0.00").Replace(",", "."))
                Else
                    noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_IR").ToString), "0.00").Replace(",", "."))
                End If
                No.AppendChild(noText)
                noValServ.AppendChild(No)


                Dim valCsll As Double = Funcoes.NNull(0, 0)
                valCsll = FormatNumber(valCsll, 2)
                No = doc.CreateElement("ValorCsll", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(valCsll), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)



                If NFDELUCRO = 1 Then
                    No = doc.CreateElement("IssRetido", NFeNamespacte)
                    noText = doc.CreateTextNode("2")
                    No.AppendChild(noText)
                    noValServ.AppendChild(No)
                Else
                    No = doc.CreateElement("IssRetido", NFeNamespacte)
                    If rsRPS.Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                        noText = doc.CreateTextNode("1")
                    Else
                        noText = doc.CreateTextNode("2")
                    End If
                    No.AppendChild(noText)
                    noValServ.AppendChild(No)
                End If


                No = doc.CreateElement("ValorIss", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_ISS").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                If NFDELUCRO = 1 Then
                    No = doc.CreateElement("ValorIssRetido", NFeNamespacte)
                    noText = doc.CreateTextNode(Format(Double.Parse(valCsll), "0.00").Replace(",", "."))
                    No.AppendChild(noText)
                    noValServ.AppendChild(No)
                Else
                    If rsRPS.Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                        No = doc.CreateElement("ValorIssRetido", NFeNamespacte)
                        noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_ISS").ToString), "0.00").Replace(",", "."))
                        No.AppendChild(noText)
                        noValServ.AppendChild(No)
                    Else
                        No = doc.CreateElement("ValorIssRetido", NFeNamespacte)
                        noText = doc.CreateTextNode(Format(Double.Parse(valCsll), "0.00").Replace(",", "."))
                        No.AppendChild(noText)
                        noValServ.AppendChild(No)
                    End If
                End If


                No = doc.CreateElement("BaseCalculo", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("Aliquota", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.aliquotaISS().ToString.Replace(",", "."))

                No.AppendChild(noText)
                noValServ.AppendChild(No)
                No = doc.CreateElement("ValorLiquidoNfse", NFeNamespacte)
                If NFDELUCRO = 1 Then
                    noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                ElseIf rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                Else
                    If rsRPS.Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                        noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_LIQUIDO").ToString), "0.00").Replace(",", "."))
                    Else
                        If rsServicos.Tables(0).Rows(I)("FL_IR").ToString = 1 Then
                            noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_DESCONTANDO_IR").ToString), "0.00").Replace(",", "."))
                        Else
                            noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                        End If
                    End If
                End If


                No.AppendChild(noText)
                noValServ.AppendChild(No)

                noServicos.AppendChild(noValServ)

                Dim dfinal As String

                Dim dDescr As String = ""


                'Codigo alterado para atender chamados 375/381.
                If rsEmpresa.Tables(0).Rows(0)("FL_INTERMEDIACAO").ToString = 1 Then
                    dDescr = "INTERMEDIAÇÃO "
                    No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                    noText = doc.CreateTextNode("1005")

                ElseIf rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    dDescr = "COMISSÃO "
                    No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                    noText = doc.CreateTextNode("1005")
                Else

                    If NFDELUCRO = 1 Then
                        dDescr = " \n Emitida conforme PA 065598/2021-16 da PMS e Relatórios gerenciais emitidos pelo sistema ERP"
                        No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                        noText = doc.CreateTextNode("1701")
                    Else
                        dDescr = "***Valor que levamos a debito, conforme nossa Fatura n " & Fatura & " | Processo: " & Processo & " | S/Ref: " & Ref & " | MASTER: " & MASTER & " | HOUSE: " & HOUSE & " | "

                        dDescr &= "SENDO: "
                        dDescr &= Funcoes.obtemDescricao(rsRPS.Rows(0)("IDFATURA").ToString,, Cod_Empresa) & Space(20)
                        dfinal = " Valor aproximado dos tributos R$ "
                        dfinal = dfinal & Funcoes.NNull(rsServicos.Tables(0).Rows(I)("VL_IMPOSTOS").ToString, 0)
                        dfinal = dfinal & " (" & Funcoes.aliquotaImpostos() * 100 & "%) conforme LEI 12741/2012"
                        dfinal = dfinal & " \n \n " & rsServicos.Tables(0).Rows(I)("OB_RPS").ToString
                        dfinal = Funcoes.tiraCaracEspXML(dfinal)

                        dDescr = Mid(dDescr, 1, 2000 - dfinal.Length)
                        dDescr = dDescr.Trim & " " & dfinal

                        No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                        noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0)("COD_SERVICO").ToString)

                    End If

                End If



                No.AppendChild(noText)
                noServicos.AppendChild(No)


                No = doc.CreateElement("CodigoTributacaoMunicipio", NFeNamespacte)

                'Codigo alterado para atender chamados 375/381.
                If rsEmpresa.Tables(0).Rows(0)("FL_INTERMEDIACAO").ToString = 1 Then
                    noText = doc.CreateTextNode("5250803")

                ElseIf rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    noText = doc.CreateTextNode("5250803")
                Else



                    If NFDELUCRO = 1 Then
                        noText = doc.CreateTextNode("829979910")

                    Else
                        noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0)("COD_TRIB_MUN").ToString)

                    End If
                End If

                No.AppendChild(noText)
                noServicos.AppendChild(No)


                dDescr = Funcoes.tiraCaracEspXML(dDescr)
                No = doc.CreateElement("Discriminacao", NFeNamespacte)
                noText = doc.CreateTextNode(dDescr)
                No.AppendChild(noText)
                noServicos.AppendChild(No)

                No = doc.CreateElement("CodigoMunicipio", NFeNamespacte)
                noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("CIDADE_IBGE").ToString)
                No.AppendChild(noText)
                noServicos.AppendChild(No)

                noInfRps.AppendChild(noServicos)
            Next
            rsServicos.Dispose()

            Dim noPrestador As XmlElement
            noPrestador = doc.CreateElement("Prestador", NFeNamespacte)
            No = doc.CreateElement("Cnpj", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0).Item("CNPJ").ToString))
            No.AppendChild(noText)
            noPrestador.AppendChild(No)

            No = doc.CreateElement("InscricaoMunicipal", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0).Item("IM").ToString))
            No.AppendChild(noText)
            noPrestador.AppendChild(No)
            noInfRps.AppendChild(noPrestador)

            Dim noTomador As XmlElement
            Dim noIdentTomador As XmlElement
            Dim noCPFCNPJ As XmlElement

            noTomador = doc.CreateElement("Tomador", NFeNamespacte)
            noIdentTomador = doc.CreateElement("IdentificacaoTomador", NFeNamespacte)

            If Funcoes.NNull(Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString), 0) > 0 Then
                noCPFCNPJ = doc.CreateElement("CpfCnpj", NFeNamespacte)
                Dim docTomador As String = Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString)
                If docTomador.Length < 14 Then
                    No = doc.CreateElement("Cpf", NFeNamespacte)
                Else
                    No = doc.CreateElement("Cnpj", NFeNamespacte)
                End If
                noText = doc.CreateTextNode(docTomador)
                No.AppendChild(noText)
                noCPFCNPJ.AppendChild(No)
                noIdentTomador.AppendChild(noCPFCNPJ)
            End If

            If Funcoes.NNull(rsRPS.Rows(0)("IM_CLI").ToString, 1) <> "" Then
                If Funcoes.NNull(rsRPS.Rows(0)("IM_CLI").ToString, 1).Trim.ToUpper <> "ISENTO" Then
                    No = doc.CreateElement("InscricaoMunicipal", NFeNamespacte)
                    noText = doc.CreateTextNode(Mid(Funcoes.obtemNumero(rsRPS.Rows(0)("IM_CLI").ToString), 1, 15))
                    No.AppendChild(noText)
                    noIdentTomador.AppendChild(No)
                End If
            End If

            noTomador.AppendChild(noIdentTomador)

            No = doc.CreateElement("RazaoSocial", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Rows(0)("CLIENTE").ToString))
            No.AppendChild(noText)
            noTomador.AppendChild(No)

            Dim noEnderecoTom As XmlElement


            noEnderecoTom = doc.CreateElement("Endereco", NFeNamespacte)

            If Funcoes.NNull(rsRPS.Rows(0)("END_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Endereco", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Rows(0)("END_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            No = doc.CreateElement("Numero", NFeNamespacte)
            If Funcoes.NNull(rsRPS.Rows(0)("NUM_END_CLI").ToString, 1) <> "" Then
                noText = doc.CreateTextNode(Mid(rsRPS.Rows(0)("NUM_END_CLI").ToString, 1, 10))
            Else
                noText = doc.CreateTextNode(".")
            End If
            No.AppendChild(noText)
            noEnderecoTom.AppendChild(No)


            If Funcoes.NNull(rsRPS.Rows(0)("COMP_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Complemento", NFeNamespacte)

                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Rows(0)("COMP_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(rsRPS.Rows(0)("BAIRRO_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Bairro", NFeNamespacte)

                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Rows(0)("BAIRRO_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(rsRPS.Rows(0)("IBGE_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("CodigoMunicipio", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Rows(0)("IBGE_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(rsRPS.Rows(0)("UF_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Uf", NFeNamespacte)
                noText = doc.CreateTextNode(rsRPS.Rows(0)("UF_CLI").ToString)
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(Funcoes.obtemNumero(rsRPS.Rows(0)("CEP_CLI").ToString), 1) <> "" Then
                No = doc.CreateElement("Cep", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.obtemNumero(rsRPS.Rows(0)("CEP_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If
            If noEnderecoTom.ChildNodes.Count > 0 Then
                noTomador.AppendChild(noEnderecoTom)
            End If


            Dim noContato As XmlElement
            noContato = doc.CreateElement("Contato", NFeNamespacte)


            If rsRPS.Rows(0)("TEL_CLI").ToString <> "" Then
                No = doc.CreateElement("Telefone", NFeNamespacte)
                noText = doc.CreateTextNode(Right(Funcoes.obtemNumero(rsRPS.Rows(0)("TEL_CLI").ToString.Replace(" ", "")), 11))
                No.AppendChild(noText)
                noContato.AppendChild(No)
            End If

            If rsRPS.Rows(0)("EMAIL_CLI").ToString <> "" Then
                No = doc.CreateElement("Email", NFeNamespacte)
                noText = doc.CreateTextNode(rsRPS.Rows(0)("EMAIL_CLI").ToString)
                No.AppendChild(noText)
                noContato.AppendChild(No)
            End If



            noTomador.AppendChild(noContato)

            noInfRps.AppendChild(noTomador)


        Catch ex As Exception
            Err.Clear()
        End Try

    End Sub

    'CONSULTA
    <WebMethod()>
    Public Function ConsultaNFePrefeitura(ByVal ID_Faturamento As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String) As String
        Con.Conectar()
        GRAVARLOG(ID_Faturamento, "CHAMA ROTINA DE CONSULTA NF")

        Dim Sql As String

        Sql = $"SELECT top 1 a.ID_FATURAMENTO,
            b.PROTOCOLO,
            a.nr_lote as LOTE_RPS 
            FROM 
            TB_FATURAMENTO a inner join tb_lote_nfse b
            on a.id_faturamento=b.id_faturamento and a.nr_rps = b.numero_rps WHERE a.id_faturamento = '{ID_Faturamento}'"


        Dim ds As DataSet = Con.ExecutarQuery(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For I = 0 To ds.Tables(0).Rows.Count - 1
                Call montaConsultaLoteRPS(Funcoes.NNull(ds.Tables(0).Rows(I)("PROTOCOLO").ToString, 0), ds.Tables(0).Rows(I)("LOTE_RpS").ToString, CodEmpresa)
            Next

        Else
            Return "00000000ID Faturamento NAO ENCONTRADO NO BANCO DE DADOS"

        End If
    End Function
    Public Sub montaConsultaLoteRPS(ByVal numeroProtocolo As String, ByVal numeroLote As Long, Optional Cod_Empresa As Integer = 1)

        Dim ret As Boolean = False
        Dim nomeArquivo As String
        Try
            Dim raizCo As XmlElement
            Dim docCo As New XmlDocument
            GRAVARLOG(numeroLote, "CHAMA ROTINA DE CONSULTA NF")

            rsEmpresa = Con.ExecutarQuery("SELECT * FROM TB_EMPRESAS where ID_EMPRESA=" & Cod_Empresa)

            Dim NFeNamespacte As String = "http://www.ginfes.com.br/servico_consultar_lote_rps_envio_v03.xsd"

            raizCo = docCo.CreateElement("ConsultarLoteRpsEnvio", NFeNamespacte)

            Dim noPrestador As XmlElement
            noPrestador = docCo.CreateElement("Prestador", NFeNamespacte)

            NFeNamespacte = "http://www.ginfes.com.br/tipos_v03.xsd"
            No = docCo.CreateElement("Cnpj", NFeNamespacte)
            noText = docCo.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("CNPJ").ToString))
            No.AppendChild(noText)
            noPrestador.AppendChild(No)

            No = docCo.CreateElement("InscricaoMunicipal", NFeNamespacte)
            noText = docCo.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("IM").ToString))
            No.AppendChild(noText)
            noPrestador.AppendChild(No)

            raizCo.AppendChild(noPrestador)

            NFeNamespacte = "http://www.ginfes.com.br/servico_consultar_lote_rps_envio_v03.xsd"
            No = docCo.CreateElement("Protocolo", NFeNamespacte)
            noText = docCo.CreateTextNode(numeroProtocolo)
            No.AppendChild(noText)
            raizCo.AppendChild(No)

            docCo.AppendChild(raizCo)

            nomeArquivo = Funcoes.diretorioConultaLoteRps & "NFsE_Consulta_" & Format(numeroLote, "00000000") & ".xml"
            docCo.Save(nomeArquivo)

            docCo.Load(nomeArquivo)
            GRAVARLOG(numeroLote, "1")

            Funcoes.AssinarDocumentoXML(nomeArquivo, "ConsultarLoteRpsEnvio", Cod_Empresa)
            GRAVARLOG(numeroLote, "2")

            docCo.Load(nomeArquivo)

            Dim docAssinado As New XmlDocument
            docAssinado.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?>" & docCo.OuterXml)
            docAssinado.Save(nomeArquivo)

            Dim sSql As String
            If Not Funcoes.validaXMLXSD(nomeArquivo, Funcoes.diretorioXSD & "\servico_consultar_lote_rps_envio_v03.xsd", "http://www.ginfes.com.br/servico_consultar_lote_rps_envio_v03.xsd") Then

                Exit Sub
            Else

            End If
            GRAVARLOG(Cod_Empresa, "3")

            GRAVARLOG(numeroLote, "CHAMA ROTINA DE CONSULTA NF")

            Call EnviaXML(nomeArquivo, "CONSULTA-RPS", numeroLote, Cod_Empresa)
            GRAVARLOG(numeroLote, "4")


        Catch ex As Exception
            GRAVARLOG(Cod_Empresa, ex.Message)
        End Try


    End Sub


    'CANCELAMENTO
    <WebMethod()>
    Public Function CancelaNFePrefeitura(ByVal Rps As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String) As String
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_FATURAMENTO,NR_RPS,NR_NOTA_FISCAL FROM TB_FATURAMENTO WHERE NR_RPS = '" & Rps & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            For I = 0 To ds.Tables(0).Rows.Count - 1
                Call montaCancelamentoNFSEv2(Funcoes.NNull(ds.Tables(0).Rows(I)("NR_NOTA_FISCAL").ToString, 0), ds.Tables(0).Rows(I)("ID_FATURAMENTO").ToString)
            Next
        Else
            Return "00000000RPS NAO ENCONTRADA NO BANCO DE DADOS"

        End If
    End Function
    Public Sub montaCancelamentoNFSEv2(ByVal numeroNota As String, ByVal numeroLote As Long, Optional Cod_Empresa As Integer = 1)
        Dim NoCA As XmlElement
        Dim ret As Boolean = False
        Dim nomeArquivo As String
        Dim xmlNaMao As String = ""
        Try
            Dim raizCa As XmlElement
            Dim docCa As New XmlDocument


            rsEmpresa = Con.ExecutarQuery("SELECT * FROM TB_EMPRESAS where ID_EMPRESA=" & Cod_Empresa)

            xmlNaMao = "<CancelarNfseEnvio xmlns=""http://www.ginfes.com.br/servico_cancelar_nfse_envio"" xmlns:tipos=""http://www.ginfes.com.br/tipos"">"
            xmlNaMao += "<Prestador>"
            xmlNaMao += "<tipos:Cnpj>" & Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("CNPJ").ToString) & "</tipos:Cnpj>"
            xmlNaMao += "<tipos:InscricaoMunicipal>" & Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("IM").ToString) & "</tipos:InscricaoMunicipal>"
            xmlNaMao += "</Prestador>"
            xmlNaMao += "<NumeroNfse>" & numeroNota & "</NumeroNfse>"
            xmlNaMao += "</CancelarNfseEnvio>"

            docCa.LoadXml(xmlNaMao)
            nomeArquivo = Funcoes.diretorioCancEnv & "NFsE_Cancela_" & Format(numeroLote, "00000000") & ".xml"
            docCa.Save(nomeArquivo)

            Funcoes.AssinarDocumentoXML(nomeArquivo, "CancelarNfseEnvio", Cod_Empresa)

            docCa.Load(nomeArquivo)

            Dim docAssinado As New XmlDocument
            docAssinado.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?>" & docCa.OuterXml)
            docAssinado.Save(nomeArquivo)

            Call EnviaXML(nomeArquivo, "CANCELAMENTO", numeroLote, Cod_Empresa)
        Catch ex As Exception
            If Not Funcoes.modoAutomatico Then
                MsgBox("Ocorreu um erro ao gerar o arquivo Lote\RPS, contate o suporte!", vbInformation, "Integração PMS")
            End If
        End Try


    End Sub


    'SUBSTITUICAO
    <WebMethod()>
    Public Function SubstituiNFePrefeitura(ByVal RpsOld As String, ByVal RpsNew As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String, id_faturamento As String) As String
        Call montaLoteRPSSUB(RpsOld, RpsNew, id_faturamento)

    End Function

    Public Sub montaLoteRPSSUB(ByVal RpsOld As String, ByVal RpsNew As String, IDFatura As String)
        Dim sql As String
        Dim rsaux As New DataSet
        Dim rsRPS As DataSet
        Dim Cod_Empresa As String = 1
        Con.Conectar()

        Try
            erroValor = False


            rsRPS = Con.ExecutarQuery("SELECT * FROM VW_FILA_LOTE_RPS WHERE NUMERO_RPS ='" & RpsNew & "'")
            If rsRPS.Tables(0).Rows.Count <= 0 Then
                Exit Sub
            End If

            Dim NFeNamespacte As String = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd"


            raiz = doc.CreateElement("EnviarLoteRpsEnvio", NFeNamespacte)


            Dim loteNumero As Long
            Dim sSql As String

            Dim ConOracle As New Conexao_oracle
            ConOracle.Conectar()
            sSql = "SELECT SEQ_LOTE_NFSE.NEXTVAL FROM DUAL "
            Dim rsNumero As DataTable = ConOracle.Consultar(sSql)
            loteNumero = rsNumero.Rows(0)("NEXTVAL").ToString


            sSql = "UPDATE TB_FATURAMENTO SET NR_LOTE =  " & loteNumero & " WHERE ID_FATURAMENTO = " & IDFatura
            Con.ExecutarQuery(sSql)

            sSql = "UPDATE TB_FATURAMENTO SET NR_NOTA_ORIGINAL = (SELECT NR_NOTA_FISCAL FROM TB_FATURAMENTO WHERE NR_RPS = '" & RpsOld & "') WHERE ID_FATURAMENTO = " & IDFatura
            Con.ExecutarQuery(sSql)

            sSql = "INSERT INTO TB_LOTE_NFSE (ID_FATURAMENTO, DT_ENVIO_LOTE, NUMERO_RPS) "
            sSql = sSql & " VALUES (" & IDFatura & ",GETDATE(),'" & Funcoes.NNull(rsRPS.Tables(0).Rows(0)("NUMERO_RPS").ToString, 0) & "') "
            Con.ExecutarQuery(sSql)

            Dim SqlInfo As String = "SELECT NR_PROCESSO,CASE WHEN NR_PROCESSO LIKE '%L%' THEN 1 ELSE 0 END NFDELUCRO,NR_BL,ISNULL(NR_BL_MASTER,'')NR_BL_MASTER,GRAU FROM View_BL WHERE NR_PROCESSO = (SELECT NR_PROCESSO FROM  View_Faturamento WHERE ID_FATURAMENTO = " & IDFatura & ")"
            Dim Fatura As String = rsRPS.Tables(0).Rows(0)("NUMERO_RPS").ToString
            Dim Processo As String = ""
            Dim Ref As String = ""
            Dim MASTER As String = ""
            Dim HOUSE As String = ""
            Dim NFDELUCRO As Integer = 0
            Dim dsInfo As DataSet
            dsInfo = Con.ExecutarQuery(SqlInfo)
            If dsInfo.Tables(0).Rows.Count > 0 Then
                If dsInfo.Tables(0).Rows(0).Item("GRAU") = "C" Then
                    Processo = dsInfo.Tables(0).Rows(0)("NR_PROCESSO").ToString
                    MASTER = dsInfo.Tables(0).Rows(0)("NR_BL_MASTER").ToString
                    HOUSE = dsInfo.Tables(0).Rows(0)("NR_BL").ToString
                    NFDELUCRO = dsInfo.Tables(0).Rows(0)("NFDELUCRO").ToString
                Else
                    Processo = dsInfo.Tables(0).Rows(0)("NR_PROCESSO").ToString
                    MASTER = dsInfo.Tables(0).Rows(0)("NR_BL").ToString
                    HOUSE = ""
                    NFDELUCRO = dsInfo.Tables(0).Rows(0)("NFDELUCRO").ToString
                End If
            End If




            noLoteRPS = doc.CreateElement("LoteRps", NFeNamespacte)

            att = doc.CreateAttribute("Id")
            att.Value = "_10" & Format(loteNumero, "000000")
            noLoteRPS.Attributes.Append(att)



            rsEmpresa = Con.ExecutarQuery("SELECT CNPJ,NM_RAZAO,IM,NOME_CERTIFICADO,TIPO_RPS,NAT_OPERACAO,SIMPLES,INC_CULTURAL,CIDADE_IBGE,
CD_ATIVIDADE_RPS AS 'COD_SERVICO',
CD_TRIBUTACAO_RPS AS 'COD_TRIB_MUN', 
CD_ATIVIDADE_COMISSAO_RPS AS 'COD_SERVICO_COMISSAO',
CD_TRIBUTACAO_COMISSAO_RPS AS 'COD_TRIB_MUN_COMISSAO', 
FL_INTERMEDIACAO,
FL_PROFIT,
B.ID_SERVICO, 
'10.05' AS 'COD_SERVICO_INTERMEDIACAO' , 
'5250803' AS 'CD_TRIBUTACAO_INTERMEDIACAO' 
FROM TB_EMPRESAS A
CROSS JOIN View_Faturamento B 
LEFT JOIN TB_SERVICO C On C.ID_SERVICO = B.ID_SERVICO
WHERE ID_FATURAMENTO = " & IDFatura)

            NFeNamespacte = "http://www.ginfes.com.br/tipos_v03.xsd"
            No = doc.CreateElement("NumeroLote", NFeNamespacte)
            noText = doc.CreateTextNode("10" & Format(loteNumero, "000000"))
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            No = doc.CreateElement("Cnpj", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("CNPJ").ToString))
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            No = doc.CreateElement("InscricaoMunicipal", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("IM").ToString))
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            No = doc.CreateElement("QuantidadeRps", NFeNamespacte)
            noText = doc.CreateTextNode("1")
            No.AppendChild(noText)
            noLoteRPS.AppendChild(No)

            noListaRPS = doc.CreateElement("ListaRps", NFeNamespacte)
            noRPS = doc.CreateElement("Rps", NFeNamespacte)

            Dim DADOS As String
            DADOS = "LOTE :" & loteNumero & " | RPS Nº " & rsRPS.Tables(0).Rows(0)("NUMERO_RPS").ToString


            noInfRps = doc.CreateElement("InfRps", NFeNamespacte)

            att = doc.CreateAttribute("Id")
            att.Value = "_20" & Format(loteNumero, "000000")
            noInfRps.Attributes.Append(att)

            noIdentRPS = doc.CreateElement("IdentificacaoRps", NFeNamespacte)

            No = doc.CreateElement("Numero", NFeNamespacte)
            noText = doc.CreateTextNode("1")
            noText = doc.CreateTextNode(rsRPS.Tables(0).Rows(0)("NUMERO_RPS").ToString)
            No.AppendChild(noText)
            noIdentRPS.AppendChild(No)

            No = doc.CreateElement("Serie", NFeNamespacte)
            noText = doc.CreateTextNode(rsRPS.Tables(0).Rows(0)("SERIE_RPS").ToString)
            No.AppendChild(noText)
            noIdentRPS.AppendChild(No)

            No = doc.CreateElement("Tipo", NFeNamespacte)
            noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0)("TIPO_RPS").ToString)
            No.AppendChild(noText)
            noIdentRPS.AppendChild(No)

            noInfRps.AppendChild(noIdentRPS)

            No = doc.CreateElement("DataEmissao", NFeNamespacte)

            Dim DataEmi As String = rsRPS.Tables(0).Rows(0)("DATA_EMISSAO").ToString
            If Val(Funcoes.NNull(RpsOld, 0)) > 0 Then
                sql = " Select DT_NOTA_FISCAL From TB_FATURAMENTO WHERE NR_RPS = '" & RpsOld & "'"
                rsaux = Con.ExecutarQuery(sql)
                For ln = 0 To rsaux.Tables(0).Rows.Count - 1
                    DataEmi = rsaux.Tables(0).Rows(0)("DT_NOTA_FISCAL").ToString
                Next
                rsaux.Dispose()
                noText = doc.CreateTextNode(Format(CDate(DataEmi), "yyyy-MM-dd") & "T" & Format(CDate(DataEmi), "HH:mm:ss"))
            Else
                noText = doc.CreateTextNode(Format(CDate(DataEmi), "yyyy-MM-dd") & "T" & Format(CDate(DataEmi), "HH:mm:ss"))
            End If
            noText = doc.CreateTextNode(Format(CDate(DataEmi), "yyyy-MM-dd") & "T" & Format(CDate(DataEmi), "HH:mm:ss"))
            No.AppendChild(noText)
            noInfRps.AppendChild(No)


            No = doc.CreateElement("NaturezaOperacao", NFeNamespacte)
            noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0)("NAT_OPERACAO").ToString)
            No.AppendChild(noText)
            noInfRps.AppendChild(No)

            No = doc.CreateElement("OptanteSimplesNacional", NFeNamespacte)
            noText = doc.CreateTextNode(IIf(Funcoes.NNull(rsEmpresa.Tables(0).Rows(0)("SIMPLES").ToString, 0) = 0, 2, 1))
            No.AppendChild(noText)
            noInfRps.AppendChild(No)

            No = doc.CreateElement("IncentivadorCultural", NFeNamespacte)
            noText = doc.CreateTextNode(IIf(Funcoes.NNull(rsEmpresa.Tables(0).Rows(0)("INC_CULTURAL").ToString, 0) = 0, 2, 1))
            No.AppendChild(noText)
            noInfRps.AppendChild(No)

            No = doc.CreateElement("Status", NFeNamespacte)
            noText = doc.CreateTextNode("1")
            No.AppendChild(noText)
            noInfRps.AppendChild(No)

            If Val(Funcoes.NNull(RpsOld, 0)) > 0 Then
                Dim noSubst As XmlElement
                noSubst = doc.CreateElement("RpsSubstituido", NFeNamespacte)

                No = doc.CreateElement("Numero", NFeNamespacte)
                noText = doc.CreateTextNode("1")
                noText = doc.CreateTextNode(RpsOld)
                No.AppendChild(noText)
                noSubst.AppendChild(No)

                No = doc.CreateElement("Serie", NFeNamespacte)
                noText = doc.CreateTextNode(rsRPS.Tables(0).Rows(0)("SERIE_RPS").ToString)
                No.AppendChild(noText)
                noSubst.AppendChild(No)

                No = doc.CreateElement("Tipo", NFeNamespacte)
                noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0)("TIPO_RPS").ToString)
                No.AppendChild(noText)
                noSubst.AppendChild(No)

                noInfRps.AppendChild(noSubst)
            End If

            Dim noServicos As XmlElement
            Dim noValServ As XmlElement
            noServicos = doc.CreateElement("Servico", NFeNamespacte)

            Dim rsServicos As DataSet
            Dim rsValImp As DataTable
            sSql = "SELECT ISNULL(OB_RPS,'')OB_RPS, 
SUM(ISNULL(VL_LIQUIDO,0))VALOR, 
SUM(ISNULL(VL_LIQUIDO,0)) - SUM(ISNULL(A.VL_ISS,0)) - SUM(ISNULL(B.VL_IR_NF,0)) AS VL_LIQUIDO, 
SUM(ISNULL(A.VL_ISS,0)) VL_ISS, 
0 VL_PIS,
0 VL_COFINS,
SUM(ISNULL(B.VL_IR_NF,0)) VL_IR, 
SUM(ISNULL(VL_LIQUIDO,0)) - SUM(ISNULL(B.VL_IR_NF,0)) AS VL_DESCONTANDO_IR, 
SUM(ISNULL(A.VL_ISS,0)) + SUM(ISNULL(VL_PIS,0)) + SUM(ISNULL(VL_COFINS,0)) + SUM(ISNULL(B.VL_IR_NF,0)) AS VL_IMPOSTOS,
CASE WHEN SUM(ISNULL(B.VL_IR_NF,0))> 0 THEN 1 ELSE 0 END FL_IR 
FROM TB_CONTA_PAGAR_RECEBER_ITENS A
LEFT JOIN TB_FATURAMENTO B ON A.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER
WHERE ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_RECEITA = 1 ) AND ID_FATURAMENTO IN (" & IDFatura & ") GROUP BY OB_RPS "



            rsServicos = Con.ExecutarQuery(sSql)

            For I = 0 To rsServicos.Tables(0).Rows.Count - 1
                If Val(Funcoes.NNull(rsServicos.Tables(0).Rows(0)(0).ToString, 0)) <> Val(Funcoes.NNull(rsServicos.Tables(0).Rows(0)(1).ToString, 0)) Then
                    sSql = "SELECT COUNT(1) FROM TB_FATURAMENTO WHERE ID_CONTA_PAGAR_RECEBER ='" & rsRPS.Tables(0).Rows(0)("ID_CONTA_PAGAR_RECEBER").ToString & "' "
                    sSql = sSql & " AND FL_RPS = 1 AND DT_CANCELAMENTO IS NULL"
                    If Funcoes.NNull(Con.ExecutarQuery(sSql).Tables(0).Rows(0)(0), 0) = 1 Then
                        erroValor = True
                        Exit Sub
                    End If
                End If
                noServicos = doc.CreateElement("Servico", NFeNamespacte)

                noValServ = doc.CreateElement("Valores", NFeNamespacte)

                No = doc.CreateElement("ValorServicos", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)


                No = doc.CreateElement("ValorPis", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_PIS").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)



                No = doc.CreateElement("ValorCofins", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_COFINS").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("ValorInss", NFeNamespacte)
                Dim valInss As Double = Funcoes.NNull(0, 0)
                noText = doc.CreateTextNode(Format(Double.Parse(valInss), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)


                No = doc.CreateElement("ValorIr", NFeNamespacte)
                If rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    Dim ValorIr As Double = Funcoes.NNull(0, 0)
                    ValorIr = FormatNumber(ValorIr, 2)
                    noText = doc.CreateTextNode(Format(Double.Parse(ValorIr), "0.00").Replace(",", "."))
                Else
                    noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_IR").ToString), "0.00").Replace(",", "."))
                End If
                No.AppendChild(noText)
                noValServ.AppendChild(No)


                Dim valCsll As Double = Funcoes.NNull(0, 0)
                valCsll = FormatNumber(valCsll, 2)
                No = doc.CreateElement("ValorCsll", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(valCsll), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)



                If NFDELUCRO = 1 Then
                    No = doc.CreateElement("IssRetido", NFeNamespacte)
                    noText = doc.CreateTextNode("2")
                Else
                    No = doc.CreateElement("IssRetido", NFeNamespacte)
                    If rsRPS.Tables(0).Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                        noText = doc.CreateTextNode("1")
                    Else
                        noText = doc.CreateTextNode("2")
                    End If
                End If

                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("ValorIss", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_ISS").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)



                If NFDELUCRO = 1 Then
                    No = doc.CreateElement("ValorIssRetido", NFeNamespacte)
                    noText = doc.CreateTextNode(Format(Double.Parse(valCsll), "0.00").Replace(",", "."))
                    No.AppendChild(noText)
                    noValServ.AppendChild(No)
                Else
                    If rsRPS.Tables(0).Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                        No = doc.CreateElement("ValorIssRetido", NFeNamespacte)
                        noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_ISS").ToString), "0.00").Replace(",", "."))
                        No.AppendChild(noText)
                        noValServ.AppendChild(No)
                    Else
                        No = doc.CreateElement("ValorIssRetido", NFeNamespacte)
                        noText = doc.CreateTextNode(Format(Double.Parse(valCsll), "0.00").Replace(",", "."))
                        No.AppendChild(noText)
                        noValServ.AppendChild(No)
                    End If
                End If


                No = doc.CreateElement("BaseCalculo", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("Aliquota", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.aliquotaISS().ToString.Replace(",", "."))

                No.AppendChild(noText)
                noValServ.AppendChild(No)
                No = doc.CreateElement("ValorLiquidoNfse", NFeNamespacte)
                If NFDELUCRO = 1 Then
                    noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))

                ElseIf rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))

                Else

                    If rsRPS.Tables(0).Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                        noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_LIQUIDO").ToString), "0.00").Replace(",", "."))
                    Else
                        If rsServicos.Tables(0).Rows(I)("FL_IR").ToString = 1 Then
                            noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VL_DESCONTANDO_IR").ToString), "0.00").Replace(",", "."))
                        Else
                            noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                        End If
                    End If
                End If


                No.AppendChild(noText)
                noValServ.AppendChild(No)

                noServicos.AppendChild(noValServ)

                Dim dfinal As String

                Dim dDescr As String

                'Codigo alterado para atender chamados 375/381.
                If rsEmpresa.Tables(0).Rows(0)("FL_INTERMEDIACAO").ToString = 1 Then
                    dDescr = "INTERMEDIAÇÃO "
                    No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                    noText = doc.CreateTextNode("1005")

                ElseIf rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    dDescr = "COMISSÃO "
                    No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                    noText = doc.CreateTextNode("1005")
                Else

                    If NFDELUCRO = 1 Then
                        dDescr = " \n Emitida conforme PA 065598/2021-16 da PMS e Relatórios gerenciais emitidos pelo sistema ERP"
                        No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                        noText = doc.CreateTextNode("1701")
                    Else
                        dDescr = "***Valor que levamos a debito, conforme nossa Fatura n " & Fatura & " | Processo: " & Processo & " | S/Ref: " & Ref & " | MASTER: " & MASTER & " | HOUSE: " & HOUSE & " | "

                        dDescr &= "SENDO: "
                        dDescr &= Funcoes.obtemDescricao(rsRPS.Tables(0).Rows(0)("IDFATURA").ToString,, Cod_Empresa) & Space(20)
                        dfinal = " Valor aproximado dos tributos R$ "
                        dfinal = dfinal & Funcoes.NNull(rsServicos.Tables(0).Rows(I)("VL_IMPOSTOS").ToString, 0)
                        dfinal = dfinal & " (" & Funcoes.aliquotaImpostos() * 100 & "%) conforme LEI 12741/2012"
                        dfinal = dfinal & " \n \n " & rsServicos.Tables(0).Rows(I)("OB_RPS").ToString
                        dfinal = Funcoes.tiraCaracEspXML(dfinal)

                        dDescr = Mid(dDescr, 1, 2000 - dfinal.Length)
                        dDescr = dDescr.Trim & " " & dfinal

                        No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                        noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0)("COD_SERVICO").ToString)


                    End If

                End If





                No.AppendChild(noText)
                noServicos.AppendChild(No)

                'Codigo alterado para atender chamados 375/381.
                No = doc.CreateElement("CodigoTributacaoMunicipio", NFeNamespacte)
                If rsEmpresa.Tables(0).Rows(0)("FL_INTERMEDIACAO").ToString = 1 Then
                    noText = doc.CreateTextNode("5250803")

                ElseIf rsEmpresa.Tables(0).Rows(0)("FL_PROFIT").ToString = 1 Then
                    noText = doc.CreateTextNode("5250803")

                Else
                    If NFDELUCRO = 1 Then
                        noText = doc.CreateTextNode("829979910")
                    Else
                        noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0)("COD_TRIB_MUN").ToString)
                    End If
                End If

                No.AppendChild(noText)
                noServicos.AppendChild(No)


                dDescr = Funcoes.tiraCaracEspXML(dDescr)
                No = doc.CreateElement("Discriminacao", NFeNamespacte)
                noText = doc.CreateTextNode(dDescr)
                No.AppendChild(noText)
                noServicos.AppendChild(No)

                No = doc.CreateElement("CodigoMunicipio", NFeNamespacte)
                noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("CIDADE_IBGE").ToString)
                No.AppendChild(noText)
                noServicos.AppendChild(No)

                noInfRps.AppendChild(noServicos)
            Next
            rsServicos.Dispose()

            Dim noPrestador As XmlElement
            noPrestador = doc.CreateElement("Prestador", NFeNamespacte)
            No = doc.CreateElement("Cnpj", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("CNPJ").ToString))
            No.AppendChild(noText)
            noPrestador.AppendChild(No)

            No = doc.CreateElement("InscricaoMunicipal", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.obtemNumero(rsEmpresa.Tables(0).Rows(0)("IM").ToString))
            No.AppendChild(noText)
            noPrestador.AppendChild(No)
            noInfRps.AppendChild(noPrestador)

            Dim noTomador As XmlElement
            Dim noIdentTomador As XmlElement
            Dim noCPFCNPJ As XmlElement

            noTomador = doc.CreateElement("Tomador", NFeNamespacte)
            noIdentTomador = doc.CreateElement("IdentificacaoTomador", NFeNamespacte)

            If Funcoes.NNull(Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("CNPJ_CLI").ToString), 0) > 0 Then
                noCPFCNPJ = doc.CreateElement("CpfCnpj", NFeNamespacte)
                Dim docTomador As String = Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("CNPJ_CLI").ToString)
                If docTomador.Length < 14 Then
                    No = doc.CreateElement("Cpf", NFeNamespacte)
                Else
                    No = doc.CreateElement("Cnpj", NFeNamespacte)
                End If
                noText = doc.CreateTextNode(docTomador)
                No.AppendChild(noText)
                noCPFCNPJ.AppendChild(No)
                noIdentTomador.AppendChild(noCPFCNPJ)
            End If

            If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("IM_CLI").ToString, 1) <> "" Then
                If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("IM_CLI").ToString, 1).Trim.ToUpper <> "ISENTO" Then
                    No = doc.CreateElement("InscricaoMunicipal", NFeNamespacte)
                    noText = doc.CreateTextNode(Mid(Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("IM_CLI").ToString), 1, 15))
                    No.AppendChild(noText)
                    noIdentTomador.AppendChild(No)
                End If
            End If

            noTomador.AppendChild(noIdentTomador)

            No = doc.CreateElement("RazaoSocial", NFeNamespacte)
            noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Tables(0).Rows(0)("CLIENTE").ToString))
            No.AppendChild(noText)
            noTomador.AppendChild(No)

            Dim noEnderecoTom As XmlElement


            noEnderecoTom = doc.CreateElement("Endereco", NFeNamespacte)

            If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("END_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Endereco", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Tables(0).Rows(0)("END_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            No = doc.CreateElement("Numero", NFeNamespacte)
            If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("NUM_END_CLI").ToString, 1) <> "" Then
                noText = doc.CreateTextNode(Mid(rsRPS.Tables(0).Rows(0)("NUM_END_CLI").ToString, 1, 10))
            Else
                noText = doc.CreateTextNode(".")
            End If
            No.AppendChild(noText)
            noEnderecoTom.AppendChild(No)


            If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("COMP_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Complemento", NFeNamespacte)

                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Tables(0).Rows(0)("COMP_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("BAIRRO_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Bairro", NFeNamespacte)

                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Tables(0).Rows(0)("BAIRRO_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("IBGE_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("CodigoMunicipio", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.tiraCaracEspXML(rsRPS.Tables(0).Rows(0)("IBGE_CLI").ToString))

                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(rsRPS.Tables(0).Rows(0)("UF_CLI").ToString, 1) <> "" Then
                No = doc.CreateElement("Uf", NFeNamespacte)

                noText = doc.CreateTextNode(rsRPS.Tables(0).Rows(0)("UF_CLI").ToString)
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If

            If Funcoes.NNull(Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("CEP_CLI").ToString), 1) <> "" Then
                No = doc.CreateElement("Cep", NFeNamespacte)
                noText = doc.CreateTextNode(Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("CEP_CLI").ToString))
                No.AppendChild(noText)
                noEnderecoTom.AppendChild(No)
            End If
            If noEnderecoTom.ChildNodes.Count > 0 Then
                noTomador.AppendChild(noEnderecoTom)
            End If


            Dim noContato As XmlElement
            noContato = doc.CreateElement("Contato", NFeNamespacte)


            If rsRPS.Tables(0).Rows(0)("TEL_CLI").ToString <> "" Then
                No = doc.CreateElement("Telefone", NFeNamespacte)
                noText = doc.CreateTextNode(Right(Funcoes.obtemNumero(rsRPS.Tables(0).Rows(0)("TEL_CLI").ToString.Replace(" ", "")), 11))
                No.AppendChild(noText)
                noContato.AppendChild(No)
            End If

            If rsRPS.Tables(0).Rows(0)("EMAIL_CLI").ToString <> "" Then
                No = doc.CreateElement("Email", NFeNamespacte)
                noText = doc.CreateTextNode(rsRPS.Tables(0).Rows(0)("EMAIL_CLI").ToString)
                No.AppendChild(noText)
                noContato.AppendChild(No)
            End If



            noTomador.AppendChild(noContato)

            noInfRps.AppendChild(noTomador)


            noRPS.AppendChild(noInfRps)

            noListaRPS.AppendChild(noRPS)
            noLoteRPS.AppendChild(noListaRPS)

            raiz.AppendChild(noLoteRPS)

            doc.AppendChild(raiz)

            Dim nomeArquivo As String = Funcoes.diretorioLoteRps & "NFsE_" & Format(loteNumero, "00000000") & ".xml"
            doc.Save(nomeArquivo)

            doc.Load(nomeArquivo)


            Dim docAssinado As New XmlDocument
            docAssinado.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?>" & Funcoes.AssinarXML(Funcoes.tiraCaracEspXML(doc.OuterXml), "InfRps", Cod_Empresa))
            docAssinado.Save(nomeArquivo)

            If Not Funcoes.validaXMLXSD(nomeArquivo, Funcoes.diretorioXSD & "\servico_enviar_lote_rps_envio_v03.xsd", "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd") Then


                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, CRITICA, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & IDFatura & ",'" & Mid(Funcoes.tiraCaracEspXML(msgValidacao), 1, 2000) & "',GETDATE(),'" & rsRPS.Tables(0).Rows(0)("NUMERO_RPS").ToString & "','" & loteNumero & "') "
                Con.ExecutarQuery(sSql)
                Con.ExecutarQuery(sSql)


                sSql = "UPDATE TB_FATURAMENTO SET NR_LOTE = " & loteNumero & ", STATUS_NFE = 5 WHERE ID_FATURAMENTO =" & IDFatura


                Con.ExecutarQuery(sSql)

                sSql = "UPDATE TB_LOTE_NFSE SET CRITICA ='" & Funcoes.tiraCaracEspXML(msgValidacao) & "' WHERE ID_FATURAMENTO =" & IDFatura
                Con.ExecutarQuery(sSql)

                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO,CRITICA,DATA_ENVIO) "
                sSql = sSql & " VALUES ( " & IDFatura & " , 'ERRO DE VALIDACAO - " & Funcoes.tiraCaracEspXML(msgValidacao) & "', GETDATE()) "
                Con.ExecutarQuery(sSql)

                Exit Sub
            Else

                sSql = "UPDATE TB_FATURAMENTO SET NR_LOTE = " & loteNumero & ", STATUS_NFE = 1 WHERE ID_FATURAMENTO =" & IDFatura

                Con.ExecutarQuery(sSql)





                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, NOME_ARQ_ENVIO, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & IDFatura & ",'" & Right(nomeArquivo, 100) & "',GETDATE(),'" & rsRPS.Tables(0).Rows(0)("NUMERO_RPS").ToString & "','" & loteNumero & "') "
                Con.ExecutarQuery(sSql)



                Con.ExecutarQuery(sSql)
            End If

            Call EnviaXML(nomeArquivo, "LOTE-RPS", loteNumero, 1)





        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    'ENVIA XML
    Public Sub EnviaXML(ByVal DocXml As String, ByVal tipo As String, ByVal loteNumero As Long, codEmpresa As Long)
        GRAVARLOG(loteNumero, "COMEÇA ROTINA DA PREFEITURA")
        Dim nomeArq As String
        Dim docRetorno As New XmlDocument
        Dim retProtocolo As String
        Dim sSql As String
        Dim retNFSE As String
        Dim uri As XmlNodeList
        Dim retData As String
        Dim retRps As String
        Dim retCompetencia As String
        Dim codVerificacao As String
        Dim retCodErro As String
        Dim ConteudoArquixoXML As String
        Dim objXML As New XmlDocument

        GRAVARLOG(loteNumero, "ANTES DO CLIENT DO GINFES")

        Dim client As New ginfes2.ServiceGinfesImplClient

        'Dim client As New GinfesTeste.ServiceGinfesImplClient

        GRAVARLOG(loteNumero, "PROCURA CERTIFICADO DE NOVO")
        client.ClientCredentials.ClientCertificate.Certificate = Funcoes.ObtemCertificado(codEmpresa)(0)

        GRAVARLOG(loteNumero, "APOS CLIENT DO GINFES")



        Dim docCab As New XmlDocument
        Dim Retorno
        Dim seqGR As String = ""
        Dim rsGR As DataSet


        Try
            GRAVARLOG(loteNumero, "COMEÇA A ENVIAR XML PARA PREFEITURA")


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


            ConteudoArquixoXML = ""

            objXML.Load(DocXml)


            Retorno = Nothing

            docCab.LoadXml("<?xml version=""1.0"" encoding=""UTF-8""?><ns2:cabecalho versao=""3"" xmlns:ns2=""http://www.ginfes.com.br/cabecalho_v03.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><versaoDados>3</versaoDados></ns2:cabecalho>")


            If tipo = "LOTE-RPS" Then
                GRAVARLOG(loteNumero, "ENTROU NO IF DO TIPO LOTE-RPS ")

                Retorno = client.RecepcionarLoteRpsV3(docCab.InnerXml, Funcoes.tiraCaracEspXML(objXML.InnerXml))
                GRAVARLOG(loteNumero, "RETORNO XML")
                nomeArq = Funcoes.diretorioLoteRpsRet & "NFsE_" & Format(loteNumero, "00000000") & "_ret.xml"
                docRetorno.LoadXml(Retorno)
                docRetorno.Save(nomeArq)
                GRAVARLOG(loteNumero, "SALVOU RETORNO XML")


                sSql = "UPDATE TB_LOG_NFSE SET NOME_ARQ_RET ='" & Right(nomeArq, 100) & "' WHERE LOTE_RPS =" & loteNumero
                sSql = sSql & " AND NOME_ARQ_RET IS NULL "
                Con.ExecutarQuery(sSql)

                docRetorno.Load(nomeArq)
                Try
                    GRAVARLOG(loteNumero, "Atualiza protocolo")

                    uri = docRetorno.GetElementsByTagName("ns3:Protocolo")
                    retProtocolo = uri(0).InnerText


                    sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO ='" & retProtocolo & "' WHERE ISNULL(PROTOCOLO,' ') = ' ' AND ID_FATURAMENTO in ( select id_faturamento from tb_log_nfse where lote_rps=" & loteNumero & ")"
                    Con.ExecutarQuery(sSql)


                Catch ex As Exception
                    GRAVARLOG(loteNumero, ex.Message)

                    Err.Clear()
                    Try
                        uri = docRetorno.GetElementsByTagName("ns4:MensagemRetorno")
                        retProtocolo = uri(0)("ns4:Mensagem").InnerText

                        sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO = 'ERRO' , CRITICA ='" & retProtocolo & "' WHERE ISNULL(PROTOCOLO,' ') = ' ' AND ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)

                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE ID_FATURAMENTO =  (SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)

                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE ID_FATURAMENTO =  (SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)

                        GRAVAERRO(0, loteNumero, retProtocolo)

                    Catch ex2 As Exception
                        GRAVARLOG(loteNumero, ex.Message)

                        retProtocolo = "XML Recusado"

                        sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO = 'ERRO' , CRITICA ='" & retProtocolo & "' WHERE ISNULL(PROTOCOLO,' ') = ' ' AND ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)

                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE ID_FATURAMENTO =  (SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)

                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE ID_FATURAMENTO =  (SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)

                        GRAVAERRO(0, loteNumero, retProtocolo)

                    End Try
                End Try

            ElseIf tipo = "CONSULTA-RPS-" Then
                Retorno = client.ConsultarNfsePorRpsV3(docCab.InnerXml, Funcoes.tiraCaracEspXML(objXML.InnerXml))

                nomeArq = Funcoes.diretorioConRPSRet & "NFsE_Consulta_RPS_" & Format(loteNumero, "00000000") & "_ret.xml"
                docRetorno.LoadXml(Retorno)
                docRetorno.Save(nomeArq)

                docRetorno.Load(nomeArq)
                Try

                    uri = docRetorno.GetElementsByTagName("ns4:InfNfse")
                    retNFSE = uri(0)("ns4:Numero").InnerText
                    retData = uri(0)("ns4:DataEmissao").InnerText
                    retCompetencia = Format(CDate(uri(0)("ns4:Competencia").InnerText), "yyyyMM")
                    codVerificacao = uri(0)("ns4:CodigoVerificacao").InnerText

                    uri = docRetorno.GetElementsByTagName("ns4:IdentificacaoRps")
                    retRps = uri(0)("ns4:Numero").InnerText


                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 2"
                    sSql = sSql & " , NR_NOTA_FISCAL ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " , DT_NOTA_FISCAL =CONVERT(DATETIME,'" & Format(CDate(retData), "dd/MM/yyyy hh:mm:ss") & "',103) "
                    sSql = sSql & " , COMPETENCIA ='" & retCompetencia & "' "
                    sSql = sSql & " , COD_VER_NFSE ='" & codVerificacao & "' "
                    sSql = sSql & " WHERE ID_FATURAMENTO = (SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                    Con.ExecutarQuery(sSql)




                    sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_LOTE = GETDATE(), CRITICA = NULL WHERE ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                    sSql = sSql & " AND DT_RETORNO_LOTE IS NULL "
                    Con.ExecutarQuery(sSql)


                Catch ex As Exception
                    Err.Clear()

                    Try
                        uri = docRetorno.GetElementsByTagName("ns4:MensagemRetorno")
                        retNFSE = uri(0)("ns4:Mensagem").InnerText
                        retCodErro = uri(0)("ns4:Codigo").InnerText

                        GRAVAERRO(0, loteNumero, retCodErro & " - " & retNFSE)

                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5"
                        sSql = sSql & " WHERE ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)


                        If retCodErro = "A02" Then
                            GoTo saida
                        End If

                        If retCodErro = "E4" Then
                            sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 4 WHERE ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                            Con.ExecutarQuery(sSql)
                            GoTo saida
                        End If



                        sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO = NULL"
                        sSql = sSql & " , CRITICA ='" & retCodErro & " - " & Mid(retNFSE, 1, 1980) & "' "
                        sSql = sSql & " WHERE ID_FATURAMENTO = (SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                        Con.ExecutarQuery(sSql)


                    Catch ex1 As Exception
                        Err.Clear()
                    End Try

                End Try


            ElseIf tipo = "CONSULTA-RPS" Then

                GRAVARLOG(loteNumero, "ENTROU NO IF DO TIPO CONSULTA-RPS ")
                Retorno = client.ConsultarLoteRpsV3(docCab.InnerXml, Funcoes.tiraCaracEspXML(objXML.InnerXml))

                nomeArq = Funcoes.diretorioLoteRpsConsultaRet & "NFsE_Consulta_" & Format(loteNumero, "00000000") & "_ret.xml"
                docRetorno.LoadXml(Retorno)
                GRAVARLOG(loteNumero, "CONSULTA-RPS: salva arquivo de retorno ")
                docRetorno.Save(nomeArq)
                GRAVARLOG(loteNumero, "CONSULTA-RPS: lê arquivo de retorno")

                docRetorno.Load(nomeArq)
                Try
                    GRAVARLOG(loteNumero, "Entra no primeiro Try da consulta")
                    uri = docRetorno.GetElementsByTagName("ns4:InfNfse")
                    retNFSE = uri(0)("ns4:Numero").InnerText
                    retData = uri(0)("ns4:DataEmissao").InnerText
                    retCompetencia = Format(CDate(uri(0)("ns4:Competencia").InnerText), "yyyyMM")
                    codVerificacao = uri(0)("ns4:CodigoVerificacao").InnerText

                    uri = docRetorno.GetElementsByTagName("ns4:IdentificacaoRps")
                    retRps = uri(0)("ns4:Numero").InnerText


                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 2"
                    sSql = sSql & " , NR_NOTA_FISCAL ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " , DT_NOTA_FISCAL = CONVERT(DATETIME,'" & Format(CDate(retData), "dd/MM/yyyy hh:mm:ss") & "',103) "
                    sSql = sSql & " , COMPETENCIA ='" & retCompetencia & "' "
                    sSql = sSql & " , COD_VER_NFSE ='" & codVerificacao & "' "
                    sSql = sSql & " WHERE ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                    Con.ExecutarQuery(sSql)


                    sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_LOTE = GETDATE(), CRITICA = NULL WHERE ID_FATURAMENTO = (SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                    sSql = sSql & " AND DT_RETORNO_LOTE IS NULL "
                    Con.ExecutarQuery(sSql)
                    GRAVARLOG(loteNumero, "DEU CERTO")

                Catch ex As Exception
                    Err.Clear()
                    GRAVARLOG(loteNumero, "DEU ERRO")
                    Try
                        uri = docRetorno.GetElementsByTagName("ns4:MensagemRetorno")
                        retNFSE = uri(0)("ns4:Mensagem").InnerText

                        retCodErro = uri(0)("ns4:Codigo").InnerText


                        GRAVAERRO(0, loteNumero, retCodErro & " - " & retNFSE)


                        If retCodErro = "A02" Then
                            sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                            Con.ExecutarQuery(sSql)
                            GoTo saida
                        End If

                        If retCodErro = "E4" Then
                            sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 4 WHERE ID_FATURAMENTO =(SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & loteNumero & " ) "
                            Con.ExecutarQuery(sSql)
                            GoTo saida
                        End If

                    Catch ex1 As Exception
                        GRAVARLOG(loteNumero, ex1.Message)
                        Err.Clear()
                    End Try

                End Try

            ElseIf tipo = "CANCELAMENTO" Then

                Retorno = client.CancelarNfse(Funcoes.tiraCaracEspXML(objXML.InnerXml))
                nomeArq = Funcoes.diretorioCancRet & "NFsE_Cancela_" & Format(loteNumero, "00000000") & "_ret.xml"
                docRetorno.LoadXml(Retorno)
                docRetorno.Save(nomeArq)

                docRetorno.Load(nomeArq)
                Try
                    Dim codRetCan As String
                    uri = docRetorno.GetElementsByTagName("ns5:CancelarNfseResposta")
                    retNFSE = uri(0)("ns5:Sucesso").InnerText
                    retData = uri(0)("ns5:DataHora").InnerText

                    If retNFSE.ToUpper = "TRUE" Then
atualizaCancel:
                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 3 "
                        sSql = sSql & " , DT_CANCELAMENTO =CONVERT(DATETIME,'" & Format(CDate(retData), "dd/MM/yyyy hh:mm:ss") & "',103) "
                        sSql = sSql & " , CANCELA_NFE = 1 "
                        sSql = sSql & " WHERE ID_FATURAMENTO = " & loteNumero
                        Con.ExecutarQuery(sSql)

                        sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_CANCEL = GETDATE() WHERE ID_FATURAMENTO = " & loteNumero
                        sSql = sSql & " AND DT_RETORNO_CANCEL IS NULL "
                        Con.ExecutarQuery(sSql)
                    ElseIf retNFSE.ToUpper = "FALSE" Then
                        uri = docRetorno.GetElementsByTagName("ns5:MensagemRetorno")
                        codRetCan = uri(0)("ns3:Codigo").InnerText
                        If codRetCan = "E79" Then
                            GoTo atualizaCancel
                        End If
                    End If
                Catch ex As Exception
                    Err.Clear()
                    uri = docRetorno.GetElementsByTagName("ns4:MensagemRetorno")
                    retNFSE = uri(0)("ns4:Mensagem").InnerText

                    sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_CANCEL = GETDATE() "
                    sSql = sSql & " , CRITICA_CAN ='" & retNFSE & "' "
                    sSql = sSql & " WHERE ID_FATURAMENTO = " & loteNumero
                    sSql = sSql & " AND DT_RETORNO_CANCEL IS NULL "
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5"
                    sSql = sSql & " WHERE ID_FATURAMENTO = " & loteNumero
                    Con.ExecutarQuery(sSql)

                End Try

            End If




        Catch ex As Exception
            GRAVARLOG(loteNumero, ex.Message)
        End Try

saida:
        nomeArq = ""
        docRetorno = Nothing
        sSql = ""
        retProtocolo = ""
        retNFSE = ""
        uri = Nothing
        retData = ""
        retRps = ""
        retCompetencia = ""
        retCodErro = ""
        ConteudoArquixoXML = ""
        objXML = Nothing
        client.Close()

        docCab = Nothing
        Retorno = ""
        seqGR = ""
        rsGR = Nothing
    End Sub

    'LOG
    Sub GRAVARLOG(ID_FATURAMENTO As String, ACAO As String)
        Con.Conectar()

        Dim sSql As String = ""
        If ACAO.Length > 200 Then
            ACAO = ACAO.Substring(1, 200)
        End If
        sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO,ACAO,DATA_ENVIO) "
        sSql = sSql & " VALUES (" & ID_FATURAMENTO & ", '" & ACAO & "', GETDATE()) "
        Con.ExecutarQuery(sSql)

        Con.Fechar()
    End Sub

    'ERRO
    Sub GRAVAERRO(ID_FATURAMENTO As String, LOTE As String, CRITICA As String)
        Con.Conectar()
        Dim sSql As String = ""
        If CRITICA.Length > 200 Then
            CRITICA = CRITICA.Substring(1, 200)
        End If

        If ID_FATURAMENTO <> "0" Then

            sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO,CRITICA,DATA_ENVIO) "
            sSql = sSql & " VALUES (" & ID_FATURAMENTO & ", '" & CRITICA & "', GETDATE()) "

        ElseIf LOTE <> "0" Then
            sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO,CRITICA,DATA_ENVIO) "
            sSql = sSql & " VALUES ((SELECT ID_FATURAMENTO FROM  TB_FATURAMENTO where NR_LOTE = " & LOTE & " ), '" & CRITICA & "', GETDATE()) "

        End If

        Con.ExecutarQuery(sSql)

        Con.Fechar()
    End Sub

    'BLOQUEIO/DESBLOQUEIO
    <WebMethod()> Public Function DesBloqueio(ByVal Bl As String, ByVal Acao As String, ByVal MotivoBloqueio As String, ByVal MotivoLiberacao As String, usuario As String) As String
        Dim Sql As String
        Dim Retorno_Sql As String
        Dim Lote As String
        Dim Comando_Proc As New OracleCommand()
        Dim ConOracle As New Conexao_oracle
        ConOracle.Conectar()

        Sql = "SELECT nvl(max(Autonum),0) lote FROM sgipa.tb_bl Where numero='" & Bl & "' and flag_ativo=1 and ultima_saida is null "
        Dim rsNumero As DataTable = ConOracle.Consultar(Sql)
        Lote = rsNumero.Rows(0)("lote").ToString

        If Lote <> "0" Then
            ConOracle.Conectar()
            Using Comando_Proc

                Comando_Proc.Connection = ConOracle.Con_ORA
                Comando_Proc.CommandType = CommandType.StoredProcedure
                Comando_Proc.CommandText = "PROC_CHRONOS_BLOQUEIO"
                Comando_Proc.Parameters.Add("@ID_LOTE", OracleDbType.Int32).Direction = ParameterDirection.Input
                Comando_Proc.Parameters("@ID_LOTE").Value = Lote
                Comando_Proc.Parameters.Add("@V_Motivo", OracleDbType.Varchar2).Direction = ParameterDirection.Input
                Comando_Proc.Parameters("@V_Motivo").Value = MotivoBloqueio
                Comando_Proc.Parameters.Add("@V_Motivo_lib", OracleDbType.Varchar2).Direction = ParameterDirection.Input
                Comando_Proc.Parameters("@V_Motivo_lib").Value = MotivoLiberacao
                Comando_Proc.Parameters.Add("@ACAO", OracleDbType.Varchar2).Direction = ParameterDirection.Input
                Comando_Proc.Parameters("@ACAO").Value = Acao
                Comando_Proc.Parameters.Add("@V_USUARIO", OracleDbType.Varchar2).Direction = ParameterDirection.Input
                Comando_Proc.Parameters("@V_USUARIO").Value = usuario
                Retorno_Sql = Comando_Proc.Parameters.Add("@Errocode", OracleDbType.Varchar2).Direction = ParameterDirection.Output
                Comando_Proc.Parameters("@Errocode").Size = 32660
                Comando_Proc.ExecuteNonQuery()


            End Using
            Comando_Proc = Nothing
            Return Retorno_Sql
        Else
            Return "BL não localizado!"
        End If
    End Function
    <WebMethod()> Public Sub StatusBloqueio(ByVal consulta As String)
        Dim Sql As String
        Dim Lote As String
        Dim AUTONUM As String
        Dim STATUS As String
        Dim Comando_Proc As New OracleCommand()
        Dim ConOracle As New Conexao_oracle
        ConOracle.Conectar()
        Con.Conectar()
        Dim rsNumero As DataTable
        Dim dsConsulta As DataSet = Con.ExecutarQuery(consulta)
        If dsConsulta.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In dsConsulta.Tables(0).Rows

                'consulta se bl esta bloqueado no sgipa
                Sql = "SELECT nvl(FLAG_BLOQUEIO_MANUAL,0)lote, AUTONUM FROM sgipa.tb_bl Where numero='" & linha.Item("NR_BL").ToString() & "' and flag_ativo=1 and ultima_saida is null "
                rsNumero = ConOracle.Consultar(Sql)
                If rsNumero.Rows.Count > 0 Then
                    Lote = rsNumero.Rows(0)("lote").ToString
                    AUTONUM = rsNumero.Rows(0)("AUTONUM").ToString
                    If Lote = "0" Then
                        'Caso nao gravar bloqueado = 0 pra todos os tipos de bloqueio do NVOCC
                        Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_DOCUMENTAL = 0 ,FL_BLOQUEIO_FINANCEIRO = 0  WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                    Else

                        Con.ExecutarQuery("UPDATE TB_BL SET AUTONUM_SGIPA = " & AUTONUM & " WHERE ISNULL(AUTONUM_SGIPA,0) = 0 AND ID_BL = " & linha.Item("ID_BL").ToString())

                        'Caso sim consultar todos os tipos de bloqueio para atualizar NVOCC

                        'BLOQUEIO FINANCEIRO
                        Sql = "SELECT AUTONUM,STATUS FROM SGIPA.TB_HIST_BLOQUEIO WHERE AUTONUM =(SELECT NVL(MAX(AUTONUM),0)AUTONUM FROM SGIPA.TB_HIST_BLOQUEIO WHERE BL = '" & AUTONUM & "' AND COD_MOTIVO_BLOQUEIO = 40)"
                        rsNumero = ConOracle.Consultar(Sql)
                        If rsNumero.Rows.Count > 0 Then
                            STATUS = rsNumero.Rows(0)("STATUS").ToString

                            If STATUS = "L" Then
                                ' if se lote = 0 gravar na tb_bl como liberado se for = 1 gravar como bloqueado
                                Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_FINANCEIRO = 0 WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                            Else
                                Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_FINANCEIRO = 1 WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                            End If
                        Else
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_FINANCEIRO = 0 WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                        End If


                        'BLOQUEIO DOCUMENTAL
                        Sql = "SELECT AUTONUM,STATUS FROM SGIPA.TB_HIST_BLOQUEIO WHERE AUTONUM =(SELECT NVL(MAX(AUTONUM),0)AUTONUM FROM SGIPA.TB_HIST_BLOQUEIO WHERE BL = '" & AUTONUM & "' AND COD_MOTIVO_BLOQUEIO = 44)"
                        rsNumero = ConOracle.Consultar(Sql)
                        If rsNumero.Rows.Count > 0 Then
                            STATUS = rsNumero.Rows(0)("STATUS").ToString
                            If STATUS = "L" Then
                                ' if se lote = 0 gravar na tb_bl como liberado se for = 1 gravar como bloqueado
                                Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_DOCUMENTAL = 0 WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                            Else
                                Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_DOCUMENTAL = 1 WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                            End If
                        Else
                            Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_DOCUMENTAL = 0 WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                        End If


                    End If

                Else
                    'Caso nao gravar bloqueado = 0 pra todos os tipos de bloqueio do NVOCC
                    Con.ExecutarQuery("UPDATE TB_BL SET FL_BLOQUEIO_DOCUMENTAL = 0 ,FL_BLOQUEIO_FINANCEIRO = 0  WHERE ID_BL = " & linha.Item("ID_BL").ToString())
                End If

            Next
        End If

    End Sub

    'CONSULTA CNPJ

    <WebMethod()>
    Public Function ConsultaCNPJ(ByVal CNPJ As String)

        'https://comercial.cnpj.ws/cnpj/27865757000102?token=SEU_TOKEN

        Dim token As String = System.Configuration.ConfigurationSettings.AppSettings("TokenConsulaCNPJ")

        ' Dim Url = "https://publica.cnpj.ws/cnpj/"
        Dim Url = "https://comercial.cnpj.ws/cnpj/"

        Dim rest As RestClient = New RestClient

        rest.BaseUrl = New Uri(Url + CNPJ + "?token=" & token)
        'rest.BaseUrl = New Uri(Url + CNPJ)

        rest.Timeout = 480000


        Dim request = New RestRequest(Method.GET)

        request.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim response = rest.Execute(request)

        Dim Content = response.Content

        Return Content

    End Function

End Class