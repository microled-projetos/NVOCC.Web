Imports System.Xml
Imports System.Xml.Schema
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Public Class Funcoes
    Dim Con As New Conexao_sql
    Dim ret As Boolean = True
    Public msgValidacao As String = ""

    Public nomeEmpresa As String
    Public codABTRA As String
    Public modoAutomatico As Boolean = True
    Public diretorioLoteRps As String = AppContext.BaseDirectory & "\LoteRpsEnviado\" 'Application.StartupPath 
    Public diretorioLoteRpsRet As String = AppContext.BaseDirectory & "\LoteRpsRet\"
    Public diretorioConultaLoteRps As String = AppContext.BaseDirectory & "\LoteRpsConsulta\"
    Public diretorioLoteRpsConsultaRet As String = AppContext.BaseDirectory & "\LoteRpsConsultaRet\"
    Public diretorioCancEnv As String = AppContext.BaseDirectory & "\CancelamentoEnv\"
    Public diretorioCancRet As String = AppContext.BaseDirectory & "\CancelamentoRet\"
    Public diretorioConRPS As String = AppContext.BaseDirectory & "\RpsConsulta\"
    Public diretorioConRPSRet As String = AppContext.BaseDirectory & "\RpsConsultaRet\"
    Public diretorioAnexoEmail As String = AppContext.BaseDirectory & "\AnexosEmail\"
    Public diretorioXSD As String = AppContext.BaseDirectory & "\XSD\"
    Public nomeCertificado As String = "eudmarco"

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
            'Select Case COD
            '    Case "12"
            '        ret = "AC"
            '    Case "27"
            '        ret = "AL"
            '    Case "13"
            '        ret = "AM"
            '    Case "16"
            '        ret = "AP"
            '    Case "29"
            '        ret = "BA"
            '    Case "23"
            '        ret = "CE"
            '    Case "53"
            '        ret = "DF"
            '    Case "32"
            '        ret = "ES"
            '    Case "52"
            '        ret = "GO"
            '    Case "21"
            '        ret = "MA"
            '    Case "31"
            '        ret = "MG"
            '    Case "50"
            '        ret = "MS"
            '    Case "51"
            '        ret = "MT"
            '    Case "15"
            '        ret = "PA"
            '    Case "25"
            '        ret = "PB"
            '    Case "26"
            '        ret = "PE"
            '    Case "22"
            '        ret = "PI"
            '    Case "41"
            '        ret = "PR"
            '    Case "33"
            '        ret = "RJ"
            '    Case "24"
            '        ret = "RN"
            '    Case "11"
            '        ret = "RO"
            '    Case "14"
            '        ret = "RR"
            '    Case "43"
            '        ret = "RS"
            '    Case "42"
            '        ret = "SC"
            '    Case "28"
            '        ret = "SE"
            '    Case "35"
            '        ret = "SP"
            '    Case "17"
            '        ret = "TO"
            '    Case Else
            '        ret = ""
            'End Select

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
        Try
            If Tipo = "IPA" Then
                If Cod_Empresa = 1 Then
                    rsDescr = Con.ExecutarQuery("Select * FROM FATURA_ITEM WHERE IDFATURA =" & idFatura & " ORDER BY ITEM ")
                Else
                    rsDescr = Con.ExecutarQuery("Select sum(VALOR) As VALOR, 1 As ITEM,'PACOTE LOGISTICO LCL' as DESCRICAO  FROM FATURA_ITEM WHERE IDFATURA =" & idFatura)
                End If
            Else
                rsDescr = Con.ExecutarQuery("SELECT * FROM FATURA_ITEM WHERE IDFATURA =" & idFatura & " ORDER BY ITEM ")
            End If
            For i = 0 To rsDescr.Tables(0).Rows.Count - 1
                If ret <> "" Then ret = ret & " * "
                ret = ret & "ITEM " & rsDescr.Tables(0).Rows(i)("ITEM").ToString & " - " & rsDescr.Tables(0).Rows(i)("DESCRICAO").ToString & " - R$ " & rsDescr.Tables(0).Rows(i)("VALOR").ToString
            Next
        Catch ex As Exception
            Err.Clear()
            ret = ""
        End Try
        Return ret
    End Function
    Public Function obtemDocumento(gr As String) As String
        Dim ret As String = ""
        Dim Sql As String = ""
        Dim rsAux As New DataSet
        Try
            Sql = "SELECT DISTINCT 'DOCUMENTO: ' || "
            Sql = Sql & " D.DESCR || "
            Sql = Sql & " ' - ' || B.NUM_DOCUMENTO AS CONTEUDO "
            Sql = Sql & " FROM TB_GR_BL A JOIN "
            Sql = Sql & "TB_BL B ON A.BL = B.AUTONUM JOIN "
            Sql = Sql & "DTE_TB_VIAGENS C ON B.VIAGEM = C.VIAGEM LEFT JOIN "
            Sql = Sql & "TB_TIPOS_DOCUMENTOS D ON B.TIPO_DOCUMENTO = D.CODE "
            Sql = Sql & " WHERE A.SEQ_GR IN(" & NNull(gr, 1) & ")"
            rsAux = Con.ExecutarQuery(Sql)
            If rsAux.Tables(0).Rows.Count > 0 Then
                ret = rsAux.Tables(0).Rows(0)(0).ToString
            End If

        Catch ex As Exception
            Err.Clear()
        End Try

        Return ret
    End Function

    Public Function obtemOBS(IDFat As Long) As String

        Dim ret As String = ""
        Dim sql As String
        Dim rsAux As New DataSet
        Try
            sql = "SELECT NF.OBS FROM FATURANOTA F INNER JOIN TB_NF_TRANSP NF ON F.NUM_TITULO = NF.TITULO WHERE F.ID = " & IDFat
            rsAux = Con.ExecutarQuery(sql)
            If rsAux.Tables(0).Rows.Count > 0 Then
                ret = rsAux.Tables(0).Rows(0)(0).ToString
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return ret

    End Function

    Public Function aliquotaImpostos() As Double
        Dim sSql As String
        Dim rsAux As New DataSet
        Try
            aliquotaImpostos = 0.1225
            sSql = "SELECT sum(TAXA) / 100 FROM TB_CAD_IMPOSTOS WHERE DESCRICAO in('ISS','COFINS','PIS') "
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
        Dim rsAux As New DataTable
        Try
            aliquotaISS = 0.03
            sSql = "SELECT TAXA/100 FROM TB_CAD_IMPOSTOS WHERE DESCRICAO = 'ISS' "
            rsAux = DAO.Consultar(sSql)
            If Not rsAux.Rows.Count <= 0 Then
                aliquotaISS = NNull(rsAux.Rows(0)(0).ToString, 0)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Function
    Public Function obtemNumeroLote() As Long
        Dim sSql As String
        Dim rsNumero As DataSet
        sSql = "SELECT SEQ_LOTE_NFSE.NEXTVAL FROM DUAL "
        rsNumero = Con.ExecutarQuery(sSql)
        obtemNumeroLote = Long.Parse(NNull(rsNumero.Tables(0).Rows(0)(0).ToString, 0))
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
            getCertificadosX509.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)

            Dim ds As DataSet = Con.ExecutarQuery("SELECT NVL(NOME_CERTIFICADO,'') FROM TB_EMPRESAS WHERE AUTONUM=" & Cod_Empresa)
            nomeCertificado = ds.Tables(0).Rows(0).Item("NOME_CERTIFICADO")

            objColecaoCertificadosX509 = getCertificadosX509.Certificates.Find(X509FindType.FindBySubjectName, nomeCertificado, False)

            '<clientCertificate storeLocation="CurrentUser" storeName="My" x509FindType="FindBySubjectName" findValue="eudmarco"/>

            If objColecaoCertificadosX509.Count = 1 Then
                Dim documento As New XmlDocument()
                documento.PreserveWhitespace = False
                'verificando elemento de referencia
                documento.LoadXml(XML)

                Dim qtdeRefUri As Integer = documento.GetElementsByTagName(pUri).Count
                If qtdeRefUri = 0 Then
                    Resultado = ResultadoAssinatura.TagAssinaturaNaoExiste
                    Mensagem = "A tag de assinatura " + pUri.Trim() + " não existe."
                ElseIf qtdeRefUri > 1 Then
                    Resultado = ResultadoAssinatura.TagAssinaturaNaoUnica
                    Mensagem = "A tag de assinatura " + pUri.Trim() + " não é unica."
                Else
                    Try
                        'selecionando certificado digital baseado no subject
                        X509Certificate = objColecaoCertificadosX509.Item(0)
                        Dim docXML As New SignedXml(documento)
                        'SignedXml docXML = new SignedXml(documento);
                        docXML.SigningKey = X509Certificate.PrivateKey

                        Dim reference As New Reference()
                        Dim uri As XmlAttributeCollection = documento.GetElementsByTagName(pUri).Item(0).Attributes

                        For Each atributo As XmlAttribute In uri
                            If atributo.Name.ToUpper = "ID" Then
                                reference.Uri = "#" + atributo.InnerText
                            End If
                        Next

                        'adicionando EnvelopedSignatureTransform a referencia
                        Dim envelopedSigntature As New XmlDsigEnvelopedSignatureTransform
                        reference.AddTransform(envelopedSigntature)

                        Dim c14Transform As New XmlDsigC14NTransform()
                        reference.AddTransform(c14Transform)

                        docXML.AddReference(reference)

                        'carrega o certificado em KeyInfoX509Data para adicionar a KeyInfo
                        Dim keyInfo = New KeyInfo()
                        keyInfo.AddClause(New KeyInfoX509Data(X509Certificate))

                        docXML.KeyInfo = keyInfo
                        docXML.ComputeSignature()

                        'recuperando a representacao do XML assinado
                        Dim xmlDigitalSignature As XmlElement = docXML.GetXml()

                        documento.DocumentElement.AppendChild(documento.ImportNode(xmlDigitalSignature, True))

                        XMLAssinado = documento.OuterXml

                    Catch ex As Exception
                        Resultado = ResultadoAssinatura.ErroAssinarDocumento
                        Mensagem = "Erro ao assinar o documento - " + ex.Message
                    End Try
                End If
            End If

        Catch ex As Exception
            Resultado = ResultadoAssinatura.ProblemaAcessoCertificadoDigital
            Mensagem = "Problema ao acessar o certificado digital - " + ex.Message

        End Try

        Return XMLAssinado


    End Function

    Public Function validaXMLXSD(ByVal meuXML As String, ByVal meuXSD As String, ByVal urlXSD As String) As Boolean
        Dim sSql As String = ""
        ret = True
        Try
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
                    ret = False
                    msgValidacao = msgValidacao & vbCrLf & ex.Message
                    'lstValida.Items.Add(ex.Message)
                    'Return ret
                End Try
                msgValidacao = msgValidacao & vbCrLf & "Validação concluída -> " & IIf(ret = True, "Arquivo validado com SUCESSO", "Validação FALHOU")
                'lstValida.Items.Add("Validação concluída -> " & IIf(ret = True, "Arquivo validado com SUCESSO", "Validação FALHOU"))
            Else
                MsgBox("Informe o arquivo XML e o arquivo XSD.")
            End If

            'MsgBox(msgValidacao)
        Catch ex As Exception
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
End Class

Public Class ServicoEspecial

    Dim _especial As Boolean
    Dim _temDivergencia As Boolean
    Dim _codServ As String
    Dim _codTrib As String
    Dim _aliq As Double
    Dim _pis As Double
    Dim _cofins As Double
    Dim _csll As Double
    Dim _ir As Double




    Public Sub carrega(idFat As Long, tipo As Integer)
        'Tipo : 0 - IPA \ 1 - RED
        Dim sSql As String
        Dim rsAux As New DataTable
        Try
            Especial = False
            CodServ = ""
            CodTrib = ""
            Aliq = 0
            Pis = 0
            Cofins = 0
            Csll = 0
            Ir = 0

            If tipo = 0 Then
                sSql = "SELECT S.COD_SER, S.COD_TRIB, S.ISS, S.PIS, S.COFINS, S.CSLL, S.IR FROM FATURA_ITEM F INNER JOIN TB_SERVICOS_IPA S ON F.SERVICO = S.AUTONUM WHERE F.IDFATURA IN(" & idFat & ")"
                sSql = sSql & " AND NVL(S.COD_SER,' ') <> ' ' "
                rsAux = DAO.Consultar(sSql)
                If rsAux.Rows.Count > 0 Then
                    Especial = True
                    CodServ = rsAux.Rows(0)("COD_SER").ToString
                    CodTrib = rsAux.Rows(0)("COD_TRIB").ToString
                    Aliq = Double.Parse(NNull(rsAux.Rows(0)("ISS").ToString, 0)) / 100
                    Pis = Double.Parse(NNull(rsAux.Rows(0)("PIS").ToString, 0)) / 100
                    Cofins = Double.Parse(NNull(rsAux.Rows(0)("COFINS").ToString, 0)) / 100
                    Csll = Double.Parse(NNull(rsAux.Rows(0)("CSLL").ToString, 0)) / 100
                    Ir = Double.Parse(NNull(rsAux.Rows(0)("IR").ToString, 0)) / 100
                Else
                    Exit Sub
                End If

                TemDivergencia = False
                sSql = "SELECT S.COD_SER, S.COD_TRIB, S.ISS FROM FATURA_ITEM F INNER JOIN TB_SERVICOS_IPA S ON F.SERVICO = S.AUTONUM WHERE F.IDFATURA IN(" & idFat & ")"
                sSql = sSql & " AND NVL(S.COD_SER,' ') = ' ' "
                rsAux = DAO.Consultar(sSql)
                If rsAux.Rows.Count > 0 Then
                    TemDivergencia = True
                End If
            ElseIf tipo = 1 Then
                sSql = "SELECT S.COD_SER, S.COD_TRIB, S.ISS, S.PIS, S.COFINS, S.CSLL, S.IR FROM FATURA_ITEM F INNER JOIN REDEX.TB_SERVICOS_REDEX S ON F.SERVICO = S.AUTONUM WHERE F.IDFATURA IN(" & idFat & ")"
                sSql = sSql & " AND NVL(S.COD_SER,' ') <> ' ' "
                rsAux = DAO.Consultar(sSql)
                If rsAux.Rows.Count > 0 Then
                    Especial = True
                    CodServ = rsAux.Rows(0)("COD_SER").ToString
                    CodTrib = rsAux.Rows(0)("COD_TRIB").ToString
                    Aliq = Double.Parse(NNull(rsAux.Rows(0)("ISS").ToString, 0)) / 100
                    Pis = Double.Parse(NNull(rsAux.Rows(0)("PIS").ToString, 0)) / 100
                    Cofins = Double.Parse(NNull(rsAux.Rows(0)("COFINS").ToString, 0)) / 100
                    Csll = Double.Parse(NNull(rsAux.Rows(0)("CSLL").ToString, 0)) / 100
                    Ir = Double.Parse(NNull(rsAux.Rows(0)("IR").ToString, 0)) / 100
                Else
                    Exit Sub
                End If

                TemDivergencia = False
                sSql = "SELECT S.COD_SER, S.COD_TRIB, S.ISS FROM FATURA_ITEM F INNER JOIN REDEX.TB_SERVICOS_REDEX S ON F.SERVICO = S.AUTONUM WHERE F.IDFATURA IN(" & idFat & ")"
                sSql = sSql & " AND NVL(S.COD_SER,' ') = ' ' "
                rsAux = DAO.Consultar(sSql)
                If rsAux.Rows.Count > 0 Then
                    TemDivergencia = True
                End If

            End If
        Catch ex As Exception
            Err.Clear()
        End Try

    End Sub

    Public Property Especial As Boolean
        Get
            Return _especial
        End Get
        Set(value As Boolean)
            _especial = value
        End Set
    End Property

    Public Property CodServ As String
        Get
            Return _codServ
        End Get
        Set(value As String)
            _codServ = value
        End Set
    End Property

    Public Property CodTrib As String
        Get
            Return _codTrib
        End Get
        Set(value As String)
            _codTrib = value
        End Set
    End Property

    Public Property Aliq As Double
        Get
            Return _aliq
        End Get
        Set(value As Double)
            _aliq = value
        End Set
    End Property

    Public Property TemDivergencia As Boolean
        Get
            Return _temDivergencia
        End Get
        Set(value As Boolean)
            _temDivergencia = value
        End Set
    End Property

    Public Property Pis As Double
        Get
            Return _pis
        End Get
        Set(value As Double)
            _pis = value
        End Set
    End Property

    Public Property Cofins As Double
        Get
            Return _cofins
        End Get
        Set(value As Double)
            _cofins = value
        End Set
    End Property

    Public Property Csll As Double
        Get
            Return _csll
        End Get
        Set(value As Double)
            _csll = value
        End Set
    End Property

    Public Property Ir As Double
        Get
            Return _ir
        End Get
        Set(value As Double)
            _ir = value
        End Set
    End Property
End Class

