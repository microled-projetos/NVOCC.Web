Imports System.ComponentModel
Imports System.Web.Services
Imports System.Xml

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

    <WebMethod()>
    Public Function IntegraNFePrefeitura(ByVal RPS As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String, ByVal Reprocessamento As Boolean) As String
        Return "00000000RSP NAO ENCONTRADA NO BANCO DE DADOS"

        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_FATURAMENTO,NR_RPS FROM TB_FATURAMENTO WHERE STATUS_NFE = 0 AND NR_RPS = '" & RPS & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            For I = 0 To ds.Tables(0).Rows.Count - 1
                Call montaLoteRPS2(Funcoes.NNull(ds.Tables(0).Rows(I)("ID_FATURAMENTO").ToString, 0), , 1)
            Next
        End If

    End Function
    Public Function ConsultaNFePrefeitura(ByVal LoteRps As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String) As String
        Return "00000000RSP NAO ENCONTRADA NO BANCO DE DADOS"
    End Function
    Public Function CancelaNFePrefeitura(ByVal Rps As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String) As String
        Return "00000000RSP NAO ENCONTRADA NO BANCO DE DADOS"
    End Function

    Public Function SubstituiNFePrefeitura(ByVal RpsOld As String, ByVal RpsNew As String, CodEmpresa As String, BancoDestino As String, StringConexaoDestino As String) As String
        Return "00000000RSP NAO ENCONTRADA NO BANCO DE DADOS"
    End Function

    Public Sub montaLoteRPS2(ByVal IDFatura As Long, Optional ByVal Reprocessamento As Boolean = False, Optional Cod_Empresa As Integer = 1)

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

                sSql = "SELECT * FROM VW_FILA_LOTE_RPS WHERE STATUS_NFE = 0 AND IDFATURA = " & IDFatura
            End If
            rsRPSs = Con.ExecutarQuery(sSql)
            If rsRPSs.Tables(0).Rows.Count <= 0 Then
                Exit Sub
            End If

            Dim NFeNamespacte As String = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd"


            raiz = doc.CreateElement("EnviarLoteRpsEnvio", NFeNamespacte)


            Dim loteNumero As Long
            loteNumero = Funcoes.obtemNumeroLote()


            sSql = "INSERT INTO TB_LOTE_NFSE (ID_LOTE, DT_ENVIO_LOTE, NUMERO_RPS) "
            sSql = sSql & " VALUES (" & loteNumero & ",GETDATE()," & Funcoes.NNull(rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString, 0) & ") "
            Con.ExecutarQuery(sSql)


            noLoteRPS = doc.CreateElement("LoteRps", NFeNamespacte)

            att = doc.CreateAttribute("Id")
            att.Value = "_10" & Format(loteNumero, "000000")
            noLoteRPS.Attributes.Append(att)


            rsEmpresa = Con.ExecutarQuery("SELECT * FROM TB_EMPRESAS WHERE ID_EMPRESA =" & Cod_Empresa)


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



            Call montaInfRps2_IPA(loteNumero, NFeNamespacte, rsRPSs.Tables(0), Cod_Empresa)


            If erroValor Then
                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, CRITICA, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString & ",'ENCONTRADA DIVERGENCIA DE VALORES',SYSDATE," & rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString & "," & loteNumero & ") "
                Con.ExecutarQuery(sSql)


                sSql = "UPDATE TB_FATURAMENTO SET LOTE_RPS = " & loteNumero & ", STATUS_NFE = 5 WHERE ID =" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString



                Con.ExecutarQuery(sSql)

                sSql = "UPDATE TB_LOTE_NFSE SET CRITICA ='ENCONTRADA DIVERGENCIA DE VALORES' WHERE ID_LOTE =" & loteNumero
                Con.ExecutarQuery(sSql)


                Exit Sub
            End If

            noRPS.AppendChild(noInfRps)

            noListaRPS.AppendChild(noRPS)
            noLoteRPS.AppendChild(noListaRPS)

            raiz.AppendChild(noLoteRPS)

            doc.AppendChild(raiz)

            nomeArquivo = Funcoes.diretorioLoteRps & "NFsE_" & Format(loteNumero, "00000000") & ".xml"
            doc.Save(nomeArquivo)

            doc.Load(nomeArquivo)


            Dim docAssinado As New XmlDocument
            docAssinado.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?>" & Funcoes.AssinarXML(Funcoes.tiraCaracEspXML(doc.OuterXml), "InfRps", Cod_Empresa))
            docAssinado.Save(nomeArquivo)

            If Not Funcoes.validaXMLXSD(nomeArquivo, Funcoes.diretorioXSD & "\servico_enviar_lote_rps_envio_v03.xsd", "http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd") Then


                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, CRITICA, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString & ",'" & Mid(Funcoes.tiraCaracEspXML(msgValidacao), 1, 2000) & "',SYSDATE," & rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString & "," & loteNumero & ") "
                Con.ExecutarQuery(sSql)


                sSql = "UPDATE TB_FATURAMENTO SET LOTE_RPS = " & loteNumero & ", STATUS_NFE = 5 WHERE ID =" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString


                Con.ExecutarQuery(sSql)

                sSql = "UPDATE TB_LOTE_NFSE SET CRITICA ='" & Funcoes.tiraCaracEspXML(msgValidacao) & "' WHERE ID_LOTE =" & loteNumero
                Con.ExecutarQuery(sSql)

                Exit Sub
            Else

                sSql = "UPDATE TB_FATURAMENTO SET LOTE_RPS = " & loteNumero & ", STATUS_NFE = 1 WHERE ID =" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString
                Con.ExecutarQuery(sSql)

                sSql = "INSERT INTO TB_LOG_NFSE (ID_FATURAMENTO, NOME_ARQ_ENVIO, DATA_ENVIO, NUMERO_RPS, LOTE_RPS) "
                sSql = sSql & " VALUES (" & rsRPSs.Tables(0).Rows(0)("IDFATURA").ToString & ",'" & Right(nomeArquivo, 100) & "',SYSDATE," & rsRPSs.Tables(0).Rows(0)("NUMERO_RPS").ToString & "," & loteNumero & ") "
                Con.ExecutarQuery(sSql)
            End If


            Call EnviaXML2(nomeArquivo, "LOTE-RPS", loteNumero)


        Catch ex As Exception
            If Not Funcoes.modoAutomatico Then
                MsgBox("Ocorreu um erro ao gerar o arquivo Lote\RPS, contate o suporte!", vbInformation, "Integração PMS")
            End If
        End Try
    End Sub
    Public Sub montaInfRps2_IPA(ByVal numeroLote As Long, Optional ByVal NFeNamespacte As String = "", Optional ByVal rsRPS As DataTable = Nothing, Optional Cod_Empresa As Integer = 1)
        Dim sP As New ServicoEspecial()
        Con.Conectar()
        Try

            erroValor = False


            sP.carrega(Long.Parse(Funcoes.NNull(rsRPS.Rows(0)("IDFATURA").ToString, 0)), 0)
            If sP.TemDivergencia Then
                erroValor = True
                Exit Sub
            End If

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
            noText = doc.CreateTextNode(IIf(Funcoes.NNull(rsEmpresa.Tables(0).Rows(0).Item("SIMPLES").ToString, 0) = 0, 2, 1))
            noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("NAT_OPERACAO").ToString)

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

            If Val(Funcoes.NNull(rsRPS.Rows(0)("NUMERO_SUB").ToString, 0)) > 0 Then
                Dim noSubst As XmlElement
                noSubst = doc.CreateElement("RpsSubstituido", NFeNamespacte)

                No = doc.CreateElement("Numero", NFeNamespacte)
                noText = doc.CreateTextNode("1")
                noText = doc.CreateTextNode(rsRPS.Rows(0)("NUMERO_SUB").ToString)
                No.AppendChild(noText)
                noSubst.AppendChild(No)

                No = doc.CreateElement("Serie", NFeNamespacte)
                noText = doc.CreateTextNode(rsRPS.Rows(0)("SERIE_RPS").ToString)
                No.AppendChild(noText)
                noSubst.AppendChild(No)

                No = doc.CreateElement("Tipo", NFeNamespacte)
                noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("TIPO_RPS").ToString)
                No.AppendChild(noText)
                noSubst.AppendChild(No)

                noInfRps.AppendChild(noSubst)
            End If

            Dim noServicos As XmlElement
            Dim noValServ As XmlElement
            noServicos = doc.CreateElement("Servico", NFeNamespacte)

            Dim rsServicos As DataSet
            Dim rsValImp As DataSet
            Dim sSql As String

            If InStr(rsRPS.Rows(0)("SEQ_GR").ToString, ",") > 0 Then
                sSql = " SELECT ROUND(SUM(ISNULL(SERV.VALOR,0) + ISNULL(SERV.ADICIONAL,0) + ISNULL(SERV.DESCONTO,0) + ISNULL(IMP.VAL,0)),2) AS VALOR, "
                sSql = sSql & " ROUND(SUM(ISNULL(SERV.VALOR,0) + ISNULL(SERV.ADICIONAL,0) + ISNULL(SERV.DESCONTO,0) + ISNULL(IMP.VAL,0)),2) AS VALOR_GR, "
                If sP.Aliq > 0 Then
                    sSql = sSql & " ROUND(SUM((ISNULL(SERV.VALOR,0) + ISNULL(SERV.ADICIONAL,0) + ISNULL(SERV.DESCONTO,0) + ISNULL(IMP.VAL,0)) * " & sP.Aliq.ToString.Replace(",", ".") & "),2) AS VALOR_ISS "
                Else
                    sSql = sSql & " ROUND(SUM((ISNULL(SERV.VALOR,0) + ISNULL(SERV.ADICIONAL,0) + ISNULL(SERV.DESCONTO,0) + ISNULL(IMP.VAL,0)) * " & Funcoes.aliquotaISS().ToString.Replace(",", ".") & "),2) AS VALOR_ISS "
                End If
                sSql = sSql & "  FROM TB_SERVICOS_FATURADOS SERV LEFT JOIN "
                sSql = sSql & "  (SELECT SUM(VALOR_IMPOSTO) VAL, AUTONUM_SERVICO_FATURADO "
                sSql = sSql & " FROM TB_SERVICOS_FATURADOS_IMPOSTOS GROUP BY AUTONUM_SERVICO_FATURADO) IMP "
                sSql = sSql & " ON SERV.AUTONUM = IMP.AUTONUM_SERVICO_FATURADO   "
                sSql = sSql & " LEFT JOIN TB_GR_BL GR ON SERV.SEQ_GR = GR.SEQ_GR "
                sSql = sSql & " WHERE SERV.SEQ_GR IN(" & rsRPS.Rows(0)("SEQ_GR").ToString & ") "
                sSql = sSql & " AND SERV.SERVICO IN(SELECT SERVICO FROM FATURA_ITEM WHERE IDFATURA = " & rsRPS.Rows(0)("IDFATURA").ToString & ") "

            Else

                If Funcoes.NNull(rsRPS.Rows(0)("SEQ_GR").ToString, 0) > 0 Then

                    sSql = " SELECT GR.VALOR_GR AS VALOR_GR, ROUND(SUM(ISNULL(SERV.VALOR,0) + ISNULL(SERV.ADICIONAL,0) + ISNULL(SERV.DESCONTO,0) + ISNULL(IMP.VAL,0)),2) AS VALOR, "
                    If sP.Aliq > 0 Then
                        sSql = sSql & " ROUND(SUM((ISNULL(SERV.VALOR,0) + ISNULL(SERV.ADICIONAL,0) + ISNULL(SERV.DESCONTO,0) + ISNULL(IMP.VAL,0)) * " & sP.Aliq.ToString.Replace(",", ".") & "),2) AS VALOR_ISS "
                    Else
                        sSql = sSql & " ROUND(SUM((ISNULL(SERV.VALOR,0) + ISNULL(SERV.ADICIONAL,0) + ISNULL(SERV.DESCONTO,0) + ISNULL(IMP.VAL,0)) * " & Funcoes.aliquotaISS().ToString.Replace(",", ".") & "),2) AS VALOR_ISS "
                    End If
                    sSql = sSql & "  FROM TB_SERVICOS_FATURADOS SERV LEFT JOIN "
                    sSql = sSql & "  (SELECT SUM(VALOR_IMPOSTO) VAL, AUTONUM_SERVICO_FATURADO "
                    sSql = sSql & " FROM TB_SERVICOS_FATURADOS_IMPOSTOS GROUP BY AUTONUM_SERVICO_FATURADO) IMP "
                    sSql = sSql & " ON SERV.AUTONUM = IMP.AUTONUM_SERVICO_FATURADO   "
                    sSql = sSql & " LEFT JOIN TB_GR_BL GR ON SERV.SEQ_GR = GR.SEQ_GR "
                    sSql = sSql & " WHERE SERV.SEQ_GR IN(" & rsRPS.Rows(0)("SEQ_GR").ToString & ") "
                    sSql = sSql & " AND SERV.SERVICO IN(SELECT SERVICO FROM FATURA_ITEM WHERE IDFATURA = " & rsRPS.Rows(0)("IDFATURA").ToString & ") "
                    sSql = sSql & " GROUP BY GR.VALOR_GR "
                Else
                    sSql = "SELECT FAT.VALOR AS VALOR_GR, FAT.VALOR,  ROUND(FAT.VALOR * 0.05 ,2) AS VALOR_ISS  "
                    sSql = sSql & " FROM TB_FATURAMENTO FAT"
                    sSql = sSql & " WHERE ID =" & rsRPS.Rows(0)("IDFATURA").ToString

                End If
            End If
            rsServicos = Con.ExecutarQuery(sSql)

            For I = 0 To rsServicos.Tables(0).Rows.Count - 1
                If Val(Funcoes.NNull(rsServicos.Tables(0).Rows(0)(0).ToString, 0)) <> Val(Funcoes.NNull(rsServicos.Tables(0).Rows(0)(1).ToString, 0)) Then
                    sSql = "SELECT COUNT(1) FROM TB_FATURAMENTO WHERE TIPO = 'GR' AND SEQ_GR ='" & rsRPS.Rows(0)("SEQ_GR").ToString & "' "
                    sSql = sSql & " AND FLAG_RPS = 1 AND CANCELADA = 0 "
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

                sSql = "SELECT ISNULL(SUM(VALOR_IMPOSTO),0) VAL FROM TB_SERVICOS_FATURADOS_IMPOSTOS "
                sSql = sSql & "  WHERE AUTONUM_IMPOSTO IN(SELECT AUTONUM FROM TB_CAD_IMPOSTOS WHERE DESCRICAO ='PIS') "
                sSql = sSql & "  AND AUTONUM_SERVICO_FATURADO IN(SELECT AUTONUM FROM TB_SERVICOS_FATURADOS "
                sSql = sSql & " WHERE SEQ_GR IN(" & rsRPS.Rows(0)("SEQ_GR").ToString & ") "
                sSql = sSql & " AND SERVICO IN(SELECT SERVICO FROM FATURA_ITEM WHERE IDFATURA = " & rsRPS.Rows(0)("IDFATURA").ToString & ") "
                sSql = sSql & " )"
                rsValImp = Con.ExecutarQuery(sSql)


                Dim valPis As Double = Funcoes.NNull(0, 0)
                If sP.Pis > 0 Then
                    valPis = Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString) * sP.Pis
                End If
                valPis = FormatNumber(valPis, 2)

                No = doc.CreateElement("ValorPis", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(valPis), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                sSql = "SELECT ISNULL(SUM(VALOR_IMPOSTO),0) VAL FROM TB_SERVICOS_FATURADOS_IMPOSTOS "
                sSql = sSql & "  WHERE AUTONUM_IMPOSTO IN(SELECT AUTONUM FROM TB_CAD_IMPOSTOS WHERE DESCRICAO ='COFINS') "
                sSql = sSql & "  AND AUTONUM_SERVICO_FATURADO IN(SELECT AUTONUM FROM TB_SERVICOS_FATURADOS "
                sSql = sSql & " WHERE SEQ_GR IN(" & rsRPS.Rows(0)("SEQ_GR").ToString & ")"
                sSql = sSql & " AND SERVICO IN(SELECT SERVICO FROM FATURA_ITEM WHERE IDFATURA = " & rsRPS.Rows(0)("IDFATURA").ToString & ") "
                sSql = sSql & " ) "

                rsValImp = Con.ExecutarQuery(sSql)

                No = doc.CreateElement("ValorCofins", NFeNamespacte)

                Dim valCofins As Double = Funcoes.NNull(0, 0)
                If sP.Cofins > 0 Then
                    valCofins = Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString) * sP.Cofins
                End If
                valCofins = FormatNumber(valCofins, 2)

                noText = doc.CreateTextNode(Format(Double.Parse(valCofins), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                sSql = "SELECT ISNULL(SUM(VALOR_IMPOSTO),0) VAL FROM TB_SERVICOS_FATURADOS_IMPOSTOS "
                sSql = sSql & "  WHERE AUTONUM_IMPOSTO IN(SELECT AUTONUM FROM TB_CAD_IMPOSTOS WHERE DESCRICAO ='INSS') "
                sSql = sSql & "  AND AUTONUM_SERVICO_FATURADO IN(SELECT AUTONUM FROM TB_SERVICOS_FATURADOS "
                sSql = sSql & " WHERE SEQ_GR IN(" & rsRPS.Rows(0)("SEQ_GR").ToString & ") "
                sSql = sSql & " AND SERVICO IN(SELECT SERVICO FROM FATURA_ITEM WHERE IDFATURA = " & rsRPS.Rows(0)("IDFATURA").ToString & ") "
                sSql = sSql & " ) "
                rsValImp = Con.ExecutarQuery(sSql)
                No = doc.CreateElement("ValorInss", NFeNamespacte)

                Dim valInss As Double = Funcoes.NNull(0, 0)
                noText = doc.CreateTextNode(Format(Double.Parse(valInss), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)


                sSql = "SELECT ISNULL(SUM(VALOR_IMPOSTO),0) VAL FROM TB_SERVICOS_FATURADOS_IMPOSTOS "
                sSql = sSql & "  WHERE AUTONUM_IMPOSTO IN(SELECT AUTONUM FROM TB_CAD_IMPOSTOS WHERE DESCRICAO ='IRPJ') "
                sSql = sSql & "  AND AUTONUM_SERVICO_FATURADO IN(SELECT AUTONUM FROM TB_SERVICOS_FATURADOS "
                sSql = sSql & " WHERE SEQ_GR IN(" & rsRPS.Rows(0)("SEQ_GR").ToString & ") "
                sSql = sSql & " AND SERVICO IN(SELECT SERVICO FROM FATURA_ITEM WHERE IDFATURA = " & rsRPS.Rows(0)("IDFATURA").ToString & ") "
                sSql = sSql & " ) "
                rsValImp = Con.ExecutarQuery(sSql)
                No = doc.CreateElement("ValorIr", NFeNamespacte)

                Dim valIr As Double = Funcoes.NNull(0, 0)
                If sP.Ir > 0 Then
                    valIr = Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString) * sP.Ir
                End If
                valIr = FormatNumber(valIr, 2)

                noText = doc.CreateTextNode(Format(Double.Parse(valIr), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                sSql = "SELECT ISNULL(SUM(VALOR_IMPOSTO),0) VAL FROM TB_SERVICOS_FATURADOS_IMPOSTOS "
                sSql = sSql & "  WHERE AUTONUM_IMPOSTO IN(SELECT AUTONUM FROM TB_CAD_IMPOSTOS WHERE DESCRICAO ='CSLL') "
                sSql = sSql & "  AND AUTONUM_SERVICO_FATURADO IN(SELECT AUTONUM FROM TB_SERVICOS_FATURADOS "
                sSql = sSql & " WHERE SEQ_GR IN(" & rsRPS.Rows(0)("SEQ_GR").ToString & ") "
                sSql = sSql & " AND SERVICO IN(SELECT SERVICO FROM FATURA_ITEM WHERE IDFATURA = " & rsRPS.Rows(0)("IDFATURA").ToString & ") "
                sSql = sSql & " ) "
                rsValImp = Con.ExecutarQuery(sSql)

                Dim valCsll As Double = Funcoes.NNull(0, 0)
                If sP.Csll > 0 Then
                    valCsll = Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString) * sP.Csll
                End If
                valCsll = FormatNumber(valCsll, 2)


                No = doc.CreateElement("ValorCsll", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(valCsll), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("IssRetido", NFeNamespacte)
                If rsRPS.Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                    noText = doc.CreateTextNode("1")
                Else
                    noText = doc.CreateTextNode("2")
                End If
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("ValorIss", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR_ISS").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                If rsRPS.Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                    No = doc.CreateElement("ValorIssRetido", NFeNamespacte)
                    noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR_ISS").ToString), "0.00").Replace(",", "."))
                    No.AppendChild(noText)
                    noValServ.AppendChild(No)
                End If

                No = doc.CreateElement("BaseCalculo", NFeNamespacte)
                noText = doc.CreateTextNode(Format(Double.Parse(rsServicos.Tables(0).Rows(I)("VALOR").ToString), "0.00").Replace(",", "."))
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                No = doc.CreateElement("Aliquota", NFeNamespacte)
                If sP.Aliq > 0 Then
                    noText = doc.CreateTextNode(sP.Aliq.ToString.Replace(",", "."))
                Else
                    If rsRPS.Rows(0)("TIPO_NF").ToString = "TRANSP" Then
                        noText = doc.CreateTextNode("0.03")
                    Else
                        noText = doc.CreateTextNode(Funcoes.aliquotaISS().ToString.Replace(",", "."))
                    End If
                End If

                No.AppendChild(noText)
                noValServ.AppendChild(No)

                valPis = FormatNumber(valPis, 2)
                valCofins = FormatNumber(valCofins, 2)
                valInss = FormatNumber(valInss, 2)
                valIr = FormatNumber(valIr, 2)
                valCsll = FormatNumber(valCsll, 2)

                Dim valorLiquido As Double
                No = doc.CreateElement("ValorLiquidoNfse", NFeNamespacte)

                If rsRPS.Rows(0)("CIDADE").ToString.ToUpper.Trim = "SANTOS" And Funcoes.obtemNumero(rsRPS.Rows(0)("CNPJ_CLI").ToString).Length > 11 Then
                    valorLiquido = Funcoes.NNull(rsServicos.Tables(0).Rows(I)("VALOR").ToString, 0) - Funcoes.NNull(rsServicos.Tables(0).Rows(I)("VALOR_ISS").ToString, 0)
                    valorLiquido = valorLiquido - valPis - valCofins - valInss - valIr - valCsll
                    noText = doc.CreateTextNode(Format(Double.Parse(valorLiquido.ToString), "0.00").Replace(",", "."))
                Else
                    valorLiquido = Funcoes.NNull(rsServicos.Tables(0).Rows(I)("VALOR").ToString, 0)
                    valorLiquido = valorLiquido - valPis - valCofins - valInss - valIr - valCsll
                    noText = doc.CreateTextNode(Format(Double.Parse(valorLiquido), "0.00").Replace(",", "."))
                End If
                No.AppendChild(noText)
                noValServ.AppendChild(No)

                noServicos.AppendChild(noValServ)

                No = doc.CreateElement("ItemListaServico", NFeNamespacte)
                If sP.CodServ <> "" Then
                    noText = doc.CreateTextNode(sP.CodServ)
                Else
                    If rsRPS.Rows(0)("TIPO_NF").ToString = "TRANSP" Or rsRPS.Rows(0)("TIPO_NF").ToString = "SCANNER" Then
                        noText = doc.CreateTextNode(rsRPS.Rows(0)("CODISS").ToString)
                    Else
                        noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("COD_SERVICO").ToString)
                    End If
                End If

                No.AppendChild(noText)
                noServicos.AppendChild(No)

                No = doc.CreateElement("CodigoTributacaoMunicipio", NFeNamespacte)
                If sP.CodTrib <> "" Then
                    noText = doc.CreateTextNode(sP.CodTrib)
                Else
                    If rsRPS.Rows(0)("TIPO_NF").ToString = "TRANSP" Or rsRPS.Rows(0)("TIPO_NF").ToString = "SCANNER" Then
                        noText = doc.CreateTextNode(rsRPS.Rows(0)("COD_CNAE").ToString)
                    Else
                        noText = doc.CreateTextNode(rsEmpresa.Tables(0).Rows(0).Item("COD_TRIB_MUN").ToString)
                    End If
                End If

                No.AppendChild(noText)
                noServicos.AppendChild(No)

                Dim dfinal As String

                Dim dDescr As String
                valorLiquido = Funcoes.NNull(rsServicos.Tables(0).Rows(I)("VALOR").ToString, 0)
                If rsRPS.Rows(0)("TIPO_NF").ToString = "TRANSP" Then
                    valorLiquido = valorLiquido * 0.1225
                    dDescr = Funcoes.obtemDescricao(rsRPS.Rows(0)("IDFATURA").ToString,, Cod_Empresa) & Space(20)
                    dDescr = dDescr & " " & Funcoes.obtemDocumento(rsRPS.Rows(0)("SEQ_GR").ToString)

                    dfinal = "Valor aproximado dos tributos R$ "
                    dfinal = dfinal & Format(Double.Parse(valorLiquido.ToString), "0.00")
                    dfinal = dfinal & " (12,25%) conforme LEI 12741/2012"
                    dfinal = Funcoes.tiraCaracEspXML(dfinal)

                    dDescr = Mid(dDescr, 1, 2000 - dfinal.Length)
                    dDescr = dDescr.Trim & " " & dfinal


                ElseIf rsRPS.Rows(0)("TIPO_NF").ToString = "SCANNER" Then


                    valorLiquido = valorLiquido * Funcoes.aliquotaImpostos()
                    dDescr = Funcoes.obtemDescricao(rsRPS.Rows(0)("IDFATURA").ToString,, Cod_Empresa) & Space(20)
                    dDescr = dDescr & " " & Funcoes.obtemOBS(rsRPS.Rows(0)("IDFATURA").ToString)

                    dfinal = "Valor aproximado dos tributos R$ "
                    dfinal = dfinal & Format(Double.Parse(valorLiquido.ToString), "0.00")
                    dfinal = dfinal & " (" & Funcoes.aliquotaImpostos() * 100 & "%) conforme LEI 12741/2012"
                    dfinal = Funcoes.tiraCaracEspXML(dfinal)

                    dDescr = Mid(dDescr, 1, 2000 - dfinal.Length)
                    dDescr = dDescr.Trim & " " & dfinal


                Else
                    If sP.Aliq > 0 Then
                        valorLiquido = valorLiquido * sP.Aliq
                        dDescr = Funcoes.obtemDescricao(rsRPS.Rows(0)("IDFATURA").ToString,, Cod_Empresa) & Space(20)
                        dDescr = dDescr & " " & Funcoes.obtemDocumento(rsRPS.Rows(0)("SEQ_GR").ToString)

                        dfinal = "Valor aproximado dos tributos R$ "
                        dfinal = dfinal & Format(Double.Parse(valorLiquido.ToString), "0.00")
                        dfinal = dfinal & " (" & sP.Aliq * 100 & "%) conforme LEI 12741/2012"
                        dfinal = Funcoes.tiraCaracEspXML(dfinal)

                        dDescr = Mid(dDescr, 1, 2000 - dfinal.Length)
                        dDescr = dDescr.Trim & " " & dfinal
                    Else
                        valorLiquido = valorLiquido * Funcoes.aliquotaImpostos()
                        dDescr = Funcoes.obtemDescricao(rsRPS.Rows(0)("IDFATURA").ToString,, Cod_Empresa) & Space(20)
                        dDescr = dDescr & " " & Funcoes.obtemDocumento(rsRPS.Rows(0)("SEQ_GR").ToString)

                        dfinal = "Valor aproximado dos tributos R$ "
                        dfinal = dfinal & Format(Double.Parse(valorLiquido.ToString), "0.00")
                        dfinal = dfinal & " (" & Funcoes.aliquotaImpostos() * 100 & "%) conforme LEI 12741/2012"
                        dfinal = Funcoes.tiraCaracEspXML(dfinal)

                        dDescr = Mid(dDescr, 1, 2000 - dfinal.Length)
                        dDescr = dDescr.Trim & " " & dfinal

                    End If
                End If

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
                noText = doc.CreateTextNode(Format(Long.Parse(Funcoes.obtemCodIBGEUF(rsRPS.Rows(0)("UF_CLI").ToString) & Format(Long.Parse(Funcoes.NNull(rsRPS.Rows(0)("IBGE_CLI").ToString, 0)), "00000")), "0000000"))
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
    Public Sub EnviaXML2(ByVal DocXml As String, ByVal tipo As String, ByVal loteNumero As Long)
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
        Dim client As New NFsE_Santos.ServiceGinfesImplClient
        Dim docCab As New XmlDocument
        Dim Retorno
        Dim seqGR As String = ""
        Dim rsGR As DataSet

        If tipo = "LOTE-RPS" Then
            Retorno = client.RecepcionarLoteRpsV3(docCab.InnerXml, Funcoes.tiraCaracEspXML(objXML.InnerXml))
            nomeArq = Funcoes.diretorioLoteRpsRet & "NFsE_" & Format(loteNumero, "00000000") & "_ret.xml"
            docRetorno.LoadXml(Retorno)
            docRetorno.Save(nomeArq)

            sSql = "UPDATE TB_LOG_NFSE SET NOME_ARQ_RET ='" & Right(nomeArq, 100) & "' WHERE LOTE_RPS =" & loteNumero
            sSql = sSql & " AND NOME_ARQ_RET IS NULL "
            Con.ExecutarQuery(sSql)

            'frmProcessamento.lstValida.Items.Add("ARQUIVO DE RETORNO : " & nomeArq)

            docRetorno.Load(nomeArq)
            Try

                uri = docRetorno.GetElementsByTagName("ns3:Protocolo")
                retProtocolo = uri(0).InnerText


                sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO ='" & retProtocolo & "' WHERE ISNULL(PROTOCOLO,' ') = ' ' AND ID_LOTE =" & loteNumero

                'frmProcessamento.lstValida.Items.Add("Numero do Protocolo:" & retProtocolo)

            Catch ex As Exception

                Err.Clear()
                Try
                    uri = docRetorno.GetElementsByTagName("ns4:MensagemRetorno")
                    retProtocolo = uri(0)("ns4:Mensagem").InnerText

                    sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO = 'ERRO' , CRITICA ='" & retProtocolo & "' WHERE ISNULL(PROTOCOLO,' ') = ' ' AND ID_LOTE =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE LOTE_RPS =  " & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE LOTE_RPS =  " & loteNumero
                    Con.ExecutarQuery(sSql)

                    'frmProcessamento.lstValida.Items.Add("Retorno Prefeitura:" & retProtocolo)
                Catch ex2 As Exception
                    retProtocolo = "XML Recusado"

                    sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO = 'ERRO' , CRITICA ='" & retProtocolo & "' WHERE ISNULL(PROTOCOLO,' ') = ' ' AND ID_LOTE =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE LOTE_RPS =  " & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5 WHERE LOTE_RPS =  " & loteNumero
                    Con.ExecutarQuery(sSql)

                    'frmProcessamento.lstValida.Items.Add("Retorno Prefeitura:" & retProtocolo)

                End Try
            End Try

        ElseIf tipo = "CONSULTA-RPS-" Then
            Retorno = client.ConsultarNfsePorRpsV3(docCab.InnerXml, Funcoes.tiraCaracEspXML(objXML.InnerXml))

            nomeArq = Funcoes.diretorioConRPSRet & "NFsE_Consulta_RPS_" & Format(loteNumero, "00000000") & "_ret.xml"
            docRetorno.LoadXml(Retorno)
            docRetorno.Save(nomeArq)

            'frmProcessamento.lstValida.Items.Add("ARQUIVO DE RETORNO : " & nomeArq)

            docRetorno.Load(nomeArq)
            Try

                uri = docRetorno.GetElementsByTagName("ns4:InfNfse")
                retNFSE = uri(0)("ns4:Numero").InnerText
                retData = uri(0)("ns4:DataEmissao").InnerText
                retCompetencia = Format(CDate(uri(0)("ns4:Competencia").InnerText), "yyyyMM")
                codVerificacao = uri(0)("ns4:CodigoVerificacao").InnerText

                uri = docRetorno.GetElementsByTagName("ns4:IdentificacaoRps")
                retRps = uri(0)("ns4:Numero").InnerText

                rsGR = Con.ExecutarQuery("SELECT SEQ_GR FROM TB_FATURAMENTO WHERE LOTE_RPS = " & loteNumero)
                If rsGR.Tables(0).Rows.Count > 0 Then
                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 2"
                    sSql = sSql & " , NR_NOTA_FISCAL ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " , DT_NOTA_FISCAL =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy HH:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , COMPETENCIA ='" & retCompetencia & "' "
                    sSql = sSql & " , COD_VER_NFSE ='" & codVerificacao & "' "
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    If Funcoes.NNull(rsGR.Tables(0).Rows(0)(0).ToString, 1) <> "" Then
                        sSql = "UPDATE TB_GR_BL SET OBS_GR ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                        sSql = sSql & " WHERE SEQ_GR IN(" & rsGR.Tables(0).Rows(0)(0).ToString & ") "
                        Con.ExecutarQuery(sSql)
                    End If
                End If
                rsGR.Dispose()

                rsGR = Con.ExecutarQuery("SELECT SEQ_GR FROM TB_FATURAMENTO WHERE LOTE_RPS = " & loteNumero)
                If rsGR.Tables(0).Rows.Count > 0 Then
                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 2"
                    sSql = sSql & " , NR_NOTA_FISCAL ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " , DT_NOTA_FISCAL =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy HH:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , COMPETENCIA ='" & retCompetencia & "' "
                    sSql = sSql & " , COD_VER_NFSE ='" & codVerificacao & "' "
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_GR_BOOKING SET OBS_GR ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " WHERE SEQ_GR IN(" & rsGR.Tables(0).Rows(0)(0).ToString & ") "
                    Con.ExecutarQuery(sSql)
                End If
                rsGR.Dispose()

                sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_LOTE = SYSDATE, CRITICA = NULL WHERE ID_LOTE =" & loteNumero
                sSql = sSql & " AND DT_RETORNO_LOTE IS NULL "
                Con.ExecutarQuery(sSql)

                'frmProcessamento.lstValida.Items.Add("Nota Fiscal N°:" & retNFSE)

            Catch ex As Exception
                Err.Clear()

                Try
                    uri = docRetorno.GetElementsByTagName("ns4:MensagemRetorno")
                    retNFSE = uri(0)("ns4:Mensagem").InnerText
                    retCodErro = uri(0)("ns4:Codigo").InnerText

                    If retCodErro = "A02" Then
                        GoTo saida
                    End If

                    If retCodErro = "E4" Then
                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 4 WHERE LOTE_RPS =" & loteNumero
                        Con.ExecutarQuery(sSql)

                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 4 WHERE LOTE_RPS =" & loteNumero
                        Con.ExecutarQuery(sSql)

                        GoTo saida
                    End If

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5"
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5"
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_LOTE_NFSE SET PROTOCOLO = NULL"
                    sSql = sSql & " , CRITICA ='" & retCodErro & " - " & Mid(retNFSE, 1, 1980) & "' "
                    sSql = sSql & " WHERE ID_LOTE = " & loteNumero
                    Con.ExecutarQuery(sSql)

                    'frmProcessamento.lstValida.Items.Add("Retorno Prefeitura:" & retNFSE)

                Catch ex1 As Exception
                    Err.Clear()
                End Try

            End Try


        ElseIf tipo = "CONSULTA-RPS" Then
            Retorno = client.ConsultarLoteRpsV3(docCab.InnerXml, Funcoes.tiraCaracEspXML(objXML.InnerXml))

            nomeArq = Funcoes.diretorioLoteRpsConsultaRet & "NFsE_Consulta_" & Format(loteNumero, "00000000") & "_ret.xml"
            docRetorno.LoadXml(Retorno)
            docRetorno.Save(nomeArq)


            docRetorno.Load(nomeArq)
            Try

                uri = docRetorno.GetElementsByTagName("ns4:InfNfse")
                retNFSE = uri(0)("ns4:Numero").InnerText
                retData = uri(0)("ns4:DataEmissao").InnerText
                'retCompetencia = uri(0)("ns4:Competencia").InnerText
                retCompetencia = Format(CDate(uri(0)("ns4:Competencia").InnerText), "yyyyMM")
                codVerificacao = uri(0)("ns4:CodigoVerificacao").InnerText

                uri = docRetorno.GetElementsByTagName("ns4:IdentificacaoRps")
                retRps = uri(0)("ns4:Numero").InnerText

                rsGR = Con.ExecutarQuery("SELECT SEQ_GR FROM TB_FATURAMENTO WHERE LOTE_RPS = " & loteNumero)
                If rsGR.Tables(0).Rows.Count > 0 Then
                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 2"
                    sSql = sSql & " , NR_NOTA_FISCAL ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " , DT_NOTA_FISCAL =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy HH:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , COMPETENCIA ='" & retCompetencia & "' "
                    sSql = sSql & " , COD_VER_NFSE ='" & codVerificacao & "' "
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    If Funcoes.NNull(rsGR.Tables(0).Rows(0)(0).ToString, 1) <> "" Then
                        sSql = "UPDATE TB_GR_BL SET OBS_GR ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                        sSql = sSql & " WHERE SEQ_GR IN(" & rsGR.Tables(0).Rows(0)(0).ToString & ") "
                        Con.ExecutarQuery(sSql)
                    End If
                End If
                rsGR.Dispose()

                rsGR = Con.ExecutarQuery("SELECT SEQ_GR FROM TB_FATURAMENTO WHERE LOTE_RPS = " & loteNumero)
                If rsGR.Tables(0).Rows.Count > 0 Then
                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 2"
                    sSql = sSql & " , NR_NOTA_FISCAL ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " , DT_NOTA_FISCAL =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy HH:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , COMPETENCIA ='" & retCompetencia & "' "
                    sSql = sSql & " , COD_VER_NFSE ='" & codVerificacao & "' "
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_GR_BOOKING SET OBS_GR ='" & Format(Long.Parse(retNFSE), "00000000") & "' "
                    sSql = sSql & " WHERE SEQ_GR IN(" & rsGR.Tables(0).Rows(0)(0).ToString & ") "
                    Con.ExecutarQuery(sSql)
                End If
                rsGR.Dispose()

                sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_LOTE = SYSDATE, CRITICA = NULL WHERE ID_LOTE =" & loteNumero
                sSql = sSql & " AND DT_RETORNO_LOTE IS NULL "
                Con.ExecutarQuery(sSql)


            Catch ex As Exception
                Err.Clear()

                Try
                    uri = docRetorno.GetElementsByTagName("ns4:MensagemRetorno")
                    retNFSE = uri(0)("ns4:Mensagem").InnerText

                    retCodErro = uri(0)("ns4:Codigo").InnerText
                    If retCodErro = "A02" Then
                        GoTo saida
                    End If

                    If retCodErro = "E4" Then
                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 4 WHERE LOTE_RPS =" & loteNumero
                        Con.ExecutarQuery(sSql)

                        sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 4 WHERE LOTE_RPS =" & loteNumero
                        Con.ExecutarQuery(sSql)

                        GoTo saida
                    End If

                Catch ex1 As Exception
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
                    sSql = sSql & " , DT_SOL_CANCELA =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy hh:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , DT_CANCELAMENTO =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy hh:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , CANCELA_NFE = 1 "
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 3 "
                    sSql = sSql & " , DT_SOL_CANCELA =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy hh:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , DT_CANCELAMENTO =TO_DATE('" & Format(CDate(retData), "dd/MM/yyyy hh:mm:ss") & "','dd/mm/yyyy hh24:mi:ss') "
                    sSql = sSql & " , CANCELA_NFE = 1 "
                    sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                    Con.ExecutarQuery(sSql)

                    sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_CANCEL = SYSDATE WHERE ID_LOTE =" & loteNumero
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

                sSql = "UPDATE TB_LOTE_NFSE SET DT_RETORNO_CANCEL = SYSDATE "
                sSql = sSql & " , CRITICA_CAN ='" & retNFSE & "' "
                sSql = sSql & " WHERE ID_LOTE = " & loteNumero
                sSql = sSql & " AND DT_RETORNO_CANCEL IS NULL "
                Con.ExecutarQuery(sSql)

                sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5"
                sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                Con.ExecutarQuery(sSql)

                sSql = "UPDATE TB_FATURAMENTO SET STATUS_NFE = 5"
                sSql = sSql & " WHERE LOTE_RPS =" & loteNumero
                Con.ExecutarQuery(sSql)
            End Try

        End If
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



End Class