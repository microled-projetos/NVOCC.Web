Imports System.Xml
Imports System.Xml.Schema
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.IO

Public Class Funcoes
    Dim Con As New Conexao_sql
    Dim ret As Boolean = True
    Public msgValidacao As String = ""

    Public nomeEmpresa As String
    Public codABTRA As String
    Public modoAutomatico As Boolean = True
    Public diretorioLoteRps As String = AppContext.BaseDirectory & "LoteRpsEnviado\" 'Application.StartupPath 
    Public diretorioLoteRpsRet As String = AppContext.BaseDirectory & "LoteRpsRet\"
    Public diretorioConultaLoteRps As String = AppContext.BaseDirectory & "\LoteRpsConsulta\"
    Public diretorioLoteRpsConsultaRet As String = AppContext.BaseDirectory & "\LoteRpsConsultaRet\"
    Public diretorioCancEnv As String = AppContext.BaseDirectory & "CancelamentoEnv\"
    Public diretorioCancRet As String = AppContext.BaseDirectory & "\CancelamentoRet\"
    Public diretorioConRPS As String = AppContext.BaseDirectory & "RpsConsulta\"
    Public diretorioConRPSRet As String = AppContext.BaseDirectory & "\RpsConsultaRet\"
    Public diretorioAnexoEmail As String = AppContext.BaseDirectory & "\AnexosEmail\"
    Public diretorioXSD As String = AppContext.BaseDirectory & "XSD_WS"
    Public nomeCertificado As String = "FCA"

    Public Function NNull(ByVal Valor As String, ByVal Tipo As Integer) As String
        If Valor <> Nothing Then
            If Tipo = 0 Then
                If Valor.ToString() = "" Then Valor = "0"
            ElseIf Tipo = 1 Then
                If Valor.ToString() = "" Then Valor = ""
            ElseIf Tipo = 2 Then
                If Not IsDate(Valor.ToString) Then Valor = ""
            End If
        Else
            If Tipo = 0 Then Valor = 0
            If Tipo = 1 Or Tipo = 2 Then Valor = ""

        End If
        Return Valor
    End Function

    Public Function obtemNumero(ByVal texto As String) As String
        Try

            Dim I As Integer
            obtemNumero = ""
            For I = 1 To Len(texto)
                If IsNumeric(Mid(texto, I, 1)) = True Then
                    obtemNumero = obtemNumero & Mid(texto, I, 1)
                End If
            Next I

        Catch ex As Exception
            obtemNumero = vbEmpty
            Err.Description = vbEmpty
        End Try


    End Function

    Public Function tiraCaracEspXML(ByVal Txt As String) As String

        Txt = NNull(Txt, 1)

        Txt = Replace(Txt, "&", "E")

        Txt = Replace(Txt, "ƒ", "C")
        Txt = Replace(Txt, "¨", "''")

        Txt = Replace(Txt, "ã", "a")
        Txt = Replace(Txt, "â", "a")
        Txt = Replace(Txt, "á", "a")
        Txt = Replace(Txt, "à", "a")
        Txt = Replace(Txt, "é", "e")
        Txt = Replace(Txt, "è", "e")
        Txt = Replace(Txt, "ê", "e")
        Txt = Replace(Txt, "í", "i")
        Txt = Replace(Txt, "ì", "i")
        Txt = Replace(Txt, "î", "i")
        Txt = Replace(Txt, "õ", "o")
        Txt = Replace(Txt, "ó", "o")
        Txt = Replace(Txt, "ò", "o")
        Txt = Replace(Txt, "ô", "o")
        Txt = Replace(Txt, "ú", "u")
        Txt = Replace(Txt, "ù", "u")
        Txt = Replace(Txt, "û", "u")
        Txt = Replace(Txt, "ç", "c")

        Txt = Replace(Txt, "Ã", "A")
        Txt = Replace(Txt, "Â", "A")
        Txt = Replace(Txt, "Á", "A")
        Txt = Replace(Txt, "À", "A")
        Txt = Replace(Txt, "É", "E")
        Txt = Replace(Txt, "È", "E")
        Txt = Replace(Txt, "Ê", "E")
        Txt = Replace(Txt, "Í", "I")
        Txt = Replace(Txt, "Ì", "I")
        Txt = Replace(Txt, "Î", "I")
        Txt = Replace(Txt, "Õ", "O")
        Txt = Replace(Txt, "Ó", "O")
        Txt = Replace(Txt, "Ò", "O")
        Txt = Replace(Txt, "Ô", "O")
        Txt = Replace(Txt, "Ú", "U")
        Txt = Replace(Txt, "Ù", "U")
        Txt = Replace(Txt, "Û", "U")
        Txt = Replace(Txt, "Ç", "C")

        Txt = Replace(Txt, "°", ".")
        Txt = Replace(Txt, "ª", ".")

        Txt = Replace(Txt, "'", " ")

        tiraCaracEspXML = Txt
        Return tiraCaracEspXML
    End Function
    Public Function obtemCodIBGEUF(Optional ByVal UF As String = "") As String
        Dim ret As String
        Try
            Select Case UF.ToUpper
                Case "AC"
                    ret = "12"
                Case "AL"
                    ret = "27"
                Case "AM"
                    ret = "13"
                Case "AP"
                    ret = "16"
                Case "BA"
                    ret = "29"
                Case "CE"
                    ret = "23"
                Case "DF"
                    ret = "53"
                Case "ES"
                    ret = "32"
                Case "GO"
                    ret = "52"
                Case "MA"
                    ret = "21"
                Case "MG"
                    ret = "31"
                Case "MS"
                    ret = "50"
                Case "MT"
                    ret = "51"
                Case "PA"
                    ret = "15"
                Case "PB"
                    ret = "25"
                Case "PE"
                    ret = "26"
                Case "PI"
                    ret = "22"
                Case "PR"
                    ret = "41"
                Case "RJ"
                    ret = "33"
                Case "RN"
                    ret = "24"
                Case "RO"
                    ret = "11"
                Case "RR"
                    ret = "14"
                Case "RS"
                    ret = "43"
                Case "SC"
                    ret = "42"
                Case "SE"
                    ret = "28"
                Case "SP"
                    ret = "35"
                Case "TO"
                    ret = "17"
                Case Else
                    ret = ""
            End Select


        Catch ex As Exception
            Err.Clear()
            ret = ""
        End Try
        Return ret
    End Function

    Public Function obtemUF(Optional ByVal COD As Integer = 0) As String
        Dim ret As String
        Try

            Select Case COD
                Case 12
                    ret = "AC"
                Case 27
                    ret = "AL"
                Case 13
                    ret = "AM"
                Case 16
                    ret = "AP"
                Case 29
                    ret = "BA"
                Case 23
                    ret = "CE"
                Case 53
                    ret = "DF"
                Case 32
                    ret = "ES"
                Case 52
                    ret = "GO"
                Case 21
                    ret = "MA"
                Case 31
                    ret = "MG"
                Case 50
                    ret = "MS"
                Case 51
                    ret = "MT"
                Case 15
                    ret = "PA"
                Case 25
                    ret = "PB"
                Case 26
                    ret = "PE"
                Case 22
                    ret = "PI"
                Case 41
                    ret = "PR"
                Case 33
                    ret = "RJ"
                Case 24
                    ret = "RN"
                Case 11
                    ret = "RO"
                Case 14
                    ret = "RR"
                Case 43
                    ret = "RS"
                Case 42
                    ret = "SC"
                Case 28
                    ret = "SE"
                Case 35
                    ret = "SP"
                Case 17
                    ret = "To"
                Case Else
                    ret = ""
            End Select

        Catch ex As Exception
            Err.Clear()
            ret = ""
        End Try
        Return ret
    End Function
    Public Function obtemDescricao(ByVal idFatura As Long, Optional ByVal Tipo As String = "IPA", Optional Cod_Empresa As Integer = 1) As String
        Dim ret As String = ""
        Dim sSql As String
        Dim rsDescr As DataSet
        Con.Conectar()

        Try

            ' rsDescr = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA AS ITEM,(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA =  A.ID_ITEM_DESPESA)AS DESCRICAO, VL_TAXA_CALCULADO AS VALOR FROM TB_CONTA_PAGAR_RECEBER_ITENS A WHERE ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & idFatura & ") ORDER BY ITEM ")

            rsDescr = Con.ExecutarQuery("SELECT ID_ITEM_DESPESA AS ITEM,(SELECT NM_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA =  A.ID_ITEM_DESPESA)AS DESCRICAO, VL_LIQUIDO AS VALOR 
FROM TB_CONTA_PAGAR_RECEBER_ITENS A 
WHERE ID_CONTA_PAGAR_RECEBER = (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_FATURAMENTO WHERE ID_FATURAMENTO = " & idFatura & ") AND ID_ITEM_DESPESA IN (SELECT ID_ITEM_DESPESA FROM TB_ITEM_DESPESA WHERE FL_RECEITA = 1 ) ORDER BY ITEM ")
            For i = 0 To rsDescr.Tables(0).Rows.Count - 1
                If ret <> "" Then ret = ret & " * "
                ret = ret & " " & rsDescr.Tables(0).Rows(i)("DESCRICAO").ToString & " - R$ " & rsDescr.Tables(0).Rows(i)("VALOR").ToString
            Next
        Catch ex As Exception
            Err.Clear()
            ret = ""
        End Try
        Return ret
    End Function

    Public Function aliquotaImpostos() As Double
        Dim sSql As String
        Dim rsAux As New DataSet
        Try
            aliquotaImpostos = 0.1225
            sSql = "SELECT (ISNULL(VL_ALIQUOTA_ISS,0) + ISNULL(VL_ALIQUOTA_PIS,0) + ISNULL(VL_ALIQUOTA_COFINS,0)) / 100 FROM TB_PARAMETROS "
            rsAux = Con.ExecutarQuery(sSql)
            For ln = 0 To rsAux.Tables(0).Rows.Count - 1
                aliquotaImpostos = NNull(rsAux.Tables(0).Rows(0)(0).ToString, 0)
            Next
        Catch ex As Exception
            Err.Clear()
        End Try

    End Function

    Public Function aliquotaISS() As Double
        Dim sSql As String
        Dim rsAux As New DataSet
        Try
            Con.Conectar()

            aliquotaISS = 0.03
            sSql = "SELECT (VL_ALIQUOTA_ISS)/100 FROM TB_PARAMETROS "
            rsAux = Con.ExecutarQuery(sSql)
            If Not rsAux.Tables(0).Rows.Count <= 0 Then
                aliquotaISS = NNull(rsAux.Tables(0).Rows(0)(0).ToString, 0)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Function

    Enum ResultadoAssinatura As Integer
        XMLAssinadoSucesso
        CertificadoDigitalInexistente
        TagAssinaturaNaoExiste
        TagAssinaturaNaoUnica
        ErroAssinarDocumento
        XMLMalFormado
        ProblemaAcessoCertificadoDigital
    End Enum

    Private Resultado As ResultadoAssinatura
    Private Mensagem As String
    Public Function AssinarXML(ByVal pArquivoXML As String, ByVal pUri As String, Optional Cod_Empresa As Integer = 1) As String
        Dim XML As String = pArquivoXML
        Dim XMLAssinado As String = String.Empty

        Resultado = ResultadoAssinatura.XMLAssinadoSucesso
        Mensagem = "Assinatura realizada com sucesso."

        Try
            'verificando existencia de certificado utilizado na assinatura
            Dim subject As String = String.Empty
            Dim objColecaoCertificadosX509 As X509Certificate2Collection = Nothing
            Dim X509Certificate As New X509Certificate2
            Dim getCertificadosX509 As New X509Store("MY", StoreLocation.CurrentUser)
            'Dim getCertificadosX509 As New X509Store("MY", StoreLocation.LocalMachine)

            getCertificadosX509.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(NOME_CERTIFICADO,'')NOME_CERTIFICADO FROM TB_EMPRESAS WHERE ID_EMPRESA = " & Cod_Empresa)
            nomeCertificado = ds.Tables(0).Rows(0).Item("NOME_CERTIFICADO")
            GRAVARLOG(Cod_Empresa, "PROCURA CERTIFICADO")
            objColecaoCertificadosX509 = getCertificadosX509.Certificates.Find(X509FindType.FindBySubjectName, nomeCertificado, False)


            If objColecaoCertificadosX509.Count = 1 Then
                Dim documento As New XmlDocument()
                documento.PreserveWhitespace = False
                'verificando elemento de referencia
                documento.LoadXml(XML)
                GRAVARLOG(Cod_Empresa, "ASSINANDO")
                Dim qtdeRefUri As Integer = documento.GetElementsByTagName(pUri).Count
                If qtdeRefUri = 0 Then
                    GRAVARLOG(Cod_Empresa, "ASSINANDO 1")
                    Resultado = ResultadoAssinatura.TagAssinaturaNaoExiste
                    Mensagem = "A tag de assinatura " + pUri.Trim() + " não existe."
                    GRAVARLOG(Cod_Empresa, "ASSINANDO 2")
                ElseIf qtdeRefUri > 1 Then
                    GRAVARLOG(Cod_Empresa, "ASSINANDO 3")
                    Resultado = ResultadoAssinatura.TagAssinaturaNaoUnica
                    Mensagem = "A tag de assinatura " + pUri.Trim() + " não é unica."
                    GRAVARLOG(Cod_Empresa, "ASSINANDO 4")
                Else
                    Try
                        GRAVARLOG(Cod_Empresa, "ASSINANDO 5")
                        'selecionando certificado digital baseado no subject
                        X509Certificate = objColecaoCertificadosX509.Item(0)
                        GRAVARLOG(Cod_Empresa, documento.ToString)
                        Dim docXML As New SignedXml(documento)
                        GRAVARLOG(Cod_Empresa, docXML.ToString)

                        'SignedXml docXML = new SignedXml(documento);
                        docXML.SigningKey = X509Certificate.PrivateKey
                        GRAVARLOG(Cod_Empresa, "ASSINANDO POS docXML 1")
                        Dim reference As New Reference()
                        GRAVARLOG(Cod_Empresa, "ASSINANDO POS docXML 2")
                        Dim uri As XmlAttributeCollection = documento.GetElementsByTagName(pUri).Item(0).Attributes
                        GRAVARLOG(Cod_Empresa, "ASSINANDO 6 ")
                        For Each atributo As XmlAttribute In uri
                            If atributo.Name.ToUpper = "ID" Then
                                reference.Uri = "#" + atributo.InnerText
                            End If
                        Next
                        GRAVARLOG(Cod_Empresa, "ASSINANDO 7")
                        'adicionando EnvelopedSignatureTransform a referencia
                        Dim envelopedSigntature As New XmlDsigEnvelopedSignatureTransform
                        reference.AddTransform(envelopedSigntature)

                        Dim c14Transform As New XmlDsigC14NTransform()
                        reference.AddTransform(c14Transform)

                        docXML.AddReference(reference)
                        GRAVARLOG(Cod_Empresa, "ASSINANDO 8")
                        'carrega o certificado em KeyInfoX509Data para adicionar a KeyInfo
                        Dim keyInfo = New KeyInfo()
                        keyInfo.AddClause(New KeyInfoX509Data(X509Certificate))

                        docXML.KeyInfo = keyInfo
                        docXML.ComputeSignature()
                        GRAVARLOG(Cod_Empresa, "ASSINANDO 9")
                        'recuperando a representacao do XML assinado
                        Dim xmlDigitalSignature As XmlElement = docXML.GetXml()

                        documento.DocumentElement.AppendChild(documento.ImportNode(xmlDigitalSignature, True))

                        XMLAssinado = documento.OuterXml
                        GRAVARLOG(Cod_Empresa, "ASSINANDO 10")
                    Catch ex As Exception
                        GRAVARLOG(Cod_Empresa, ex.Message)
                        Resultado = ResultadoAssinatura.ErroAssinarDocumento
                        Mensagem = "Erro ao assinar o documento - " + ex.Message
                    End Try
                End If
            End If

        Catch ex As Exception
            Resultado = ResultadoAssinatura.ProblemaAcessoCertificadoDigital
            Mensagem = "Problema ao acessar o certificado digital - " + ex.Message
            GRAVARLOG(Cod_Empresa, ex.Message)
        End Try

        Return XMLAssinado


    End Function

    Public Function validaXMLXSD(ByVal meuXML As String, ByVal meuXSD As String, ByVal urlXSD As String) As Boolean
        Dim sSql As String = ""
        ret = True
        Try
            GRAVARLOG(1, "COMEÇA A VALIDAR")

            If Not meuXML = String.Empty And Not meuXSD = String.Empty Then
                'resultado = True
                Dim settings As New XmlReaderSettings()
                AddHandler settings.ValidationEventHandler, AddressOf ValidationEventHandler

                'Valida o arquivo XML com o seu Schema XSD
                msgValidacao = "Validando o arquivo XML " & meuXML & " com o arquivo de Schema : " & meuXSD
                Try
                    settings.ValidationType = ValidationType.Schema
                    settings.Schemas.Add(urlXSD, XmlReader.Create(meuXSD))

                    Using XmlValidatingReader As XmlReader = XmlReader.Create(meuXML, settings)
                        While XmlValidatingReader.Read()
                        End While
                    End Using
                Catch ex As Exception
                    GRAVARLOG(1, ex.Message)

                    ret = False
                    msgValidacao = msgValidacao & vbCrLf & ex.Message
                    'lstValida.Items.Add(ex.Message)
                    'Return ret
                End Try
                msgValidacao = msgValidacao & vbCrLf & "Validação concluída -> " & IIf(ret = True, "Arquivo validado com SUCESSO", "Validação FALHOU")
                'lstValida.Items.Add("Validação concluída -> " & IIf(ret = True, "Arquivo validado com SUCESSO", "Validação FALHOU"))
            Else
                GRAVARLOG(1, "Informe o arquivo XML e o arquivo XSD.")
                MsgBox("Informe o arquivo XML e o arquivo XSD.")
            End If

            'MsgBox(msgValidacao)
        Catch ex As Exception
            GRAVARLOG(1, ex.Message)
            ret = False
        End Try

        Return ret

    End Function

    Public Sub ValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
        Try
            ret = False
            msgValidacao = msgValidacao & vbCrLf & "Erro de Validação : " & args.Message

            If args.Severity = XmlSeverityType.Warning Then
                msgValidacao = msgValidacao & vbCrLf & "Nenhum arquivo de Schema foi encontrado para efetuar a validação..."
            ElseIf args.Severity = XmlSeverityType.Error Then
                msgValidacao = msgValidacao & vbCrLf & "Ocorreu um erro durante a validação...."
            End If
            If Not (args.Exception Is Nothing) Then ' Erro na validação do schema XSD
                msgValidacao = msgValidacao & vbCrLf & args.Exception.SourceUri + "," & args.Exception.LinePosition & "," & args.Exception.LineNumber
            End If
        Catch ex As Exception

        End Try
    End Sub

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
    Public Function ObtemCertificado(codEmpresa As Long) As X509Certificate2Collection
        Dim rsEmpresa As New DataSet
        Try
            Con.Conectar()
            rsEmpresa = Con.ExecutarQuery("SELECT NOME_CERTIFICADO FROM TB_EMPRESAS WHERE ID_EMPRESA =" & codEmpresa)

            GRAVARLOG(codEmpresa, "ENTROU NA ROTINA ObtemCertificado")

            If rsEmpresa.Tables(0).Rows(0).Item("NOME_CERTIFICADO").ToString <> "" Then
                nomeCertificado = rsEmpresa.Tables(0).Rows(0).Item("NOME_CERTIFICADO").ToString
            End If
            GRAVARLOG(codEmpresa, "Achou o nome do Certificado no banco")

            Dim subject As String = String.Empty
            Dim objColecaoCertificadosX509 As X509Certificate2Collection = Nothing
            Dim X509Certificate As New X509Certificate2
            Dim getCertificadosX509 As New X509Store("MY", StoreLocation.CurrentUser)
            'Dim getCertificadosX509 As New X509Store("MY", StoreLocation.LocalMachine)
            getCertificadosX509.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)

            objColecaoCertificadosX509 = getCertificadosX509.Certificates.Find(X509FindType.FindBySubjectName, nomeCertificado, False)

            Con.Fechar()
            Return objColecaoCertificadosX509
        Catch ex As Exception
            GRAVARLOG(codEmpresa, ex.Message)
            Err.Clear()
            Return Nothing
        End Try

    End Function


    Public Sub AssinarDocumentoXML(ByVal ArqXMLAssinar As String, ByVal TagXML As String, Optional Cod_Empresa As Integer = 1)
        GRAVARLOG(Cod_Empresa, "INICIA ROTINA DE ASSINATURA")
        'XML que sera assinado
        Dim srdDocXML As StreamReader
        Dim strXML As String
        Dim strTagXML As String = TagXML


        srdDocXML = File.OpenText(ArqXMLAssinar)
        strXML = srdDocXML.ReadToEnd()
        srdDocXML.Close()

        Dim objColecaoCertificadosX509 As X509Certificate2Collection = Nothing
        Dim objCertificadoX509 As New X509Certificate2
        Dim getCertificadosX509 As New X509Store("MY", StoreLocation.CurrentUser)
        'Dim getCertificadosX509 As New X509Store("MY", StoreLocation.LocalMachine)
        getCertificadosX509.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(NOME_CERTIFICADO,'')NOME_CERTIFICADO FROM TB_EMPRESAS WHERE ID_EMPRESA=" & Cod_Empresa)
        nomeCertificado = ds.Tables(0).Rows(0).Item("NOME_CERTIFICADO")

        objColecaoCertificadosX509 = getCertificadosX509.Certificates.Find(X509FindType.FindBySubjectName, nomeCertificado, False)

        If objColecaoCertificadosX509.Count = 1 Then

            objCertificadoX509 = objColecaoCertificadosX509.Item(0)

            Dim docXML = New XmlDocument()
            docXML.PreserveWhitespace = False
            docXML.Load(ArqXMLAssinar)

            If docXML.GetElementsByTagName(strTagXML).Count = 1 Then

                Dim signedXml As New System.Security.Cryptography.Xml.SignedXml(docXML)
                signedXml.SigningKey = objCertificadoX509.PrivateKey


                Dim Referencia As New System.Security.Cryptography.Xml.Reference()
                Referencia.Uri = ""

                Dim env As New XmlDsigEnvelopedSignatureTransform
                Referencia.AddTransform(env)

                Dim c14 As New XmlDsigC14NTransform
                Referencia.AddTransform(c14)

                signedXml.AddReference(Referencia)

                Dim keyInfo As New KeyInfo
                keyInfo.AddClause(New KeyInfoX509Data(objCertificadoX509))
                signedXml.KeyInfo = keyInfo

                signedXml.ComputeSignature()


                Dim XmlDigitalSignature As XmlElement
                XmlDigitalSignature = signedXml.GetXml()



                docXML.DocumentElement.AppendChild(docXML.ImportNode(XmlDigitalSignature, True))

                Dim EscreverXML As New StreamWriter(ArqXMLAssinar)
                EscreverXML.Write(docXML.OuterXml)
                EscreverXML.Close()
            End If

        End If


    End Sub

End Class

