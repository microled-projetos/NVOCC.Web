Imports System.String
Public Class GeraRemessa
    Public Function criaHeaderSantander(cod_banco As String, CNPJ_CEDENTE As String, NOME_CEDENTE As String, cod_trans As String, seq_arquivo As String) As String
        '001 - 003 Código do Banco na compensação N 003 353 / 008 / 033
        '004 - 007 Lote de serviço N 004 0000 1
        '008 - 008 Tipo de registro N 001 0 2
        '009 - 016 Reservado (uso Banco) A 008 Brancos
        '017 - 017 Tipo de inscrição da empresa N 001 1 = CPF, 2 = CNPJ
        '018 - 032 Nº de inscrição da empresa N 015
        '033 - 047 Código de Transmissão N 015 3
        '048 - 072 Reservado (uso Banco) A 025 Brancos
        '073 - 102 Nome da empresa A 030
        '103 - 132 Nome do Banco A 030 Banco Santander
        '133 - 142 Reservado (uso Banco) A 010 Brancos
        '143 - 143 Código remessa N 001 1 = Remessa
        '144 - 151 Data de geração do arquivo N 008 DDMMAAAA
        '152 - 157 Reservado (uso Banco) A 006 Brancos
        '158 - 163 Nº seqüencial do arquivo N 006 4
        '164 - 166 Nº da versão do layout do arquivo N 003 040
        '167 - 240 Reservado (uso Banco) A 074 Brancos
        On Error GoTo erro
        Dim strS As String

        criaHeaderSantander = String.Empty

        strS = String.Empty
        strS = Right(Strings.StrDup(3, "0") & NNull(cod_banco, 0), 3)
        strS = strS & "0000"
        strS = strS & "0"
        strS = strS & Strings.StrDup(8, " ")
        strS = strS & "2"
        strS = strS & Right(Strings.StrDup(15, "0") & ObtemNumero(NNull(CNPJ_CEDENTE, 1)), 15)
        strS = strS & Right(Strings.StrDup(15, "0") & NNull(cod_trans, 0), 15)
        strS = strS & Strings.StrDup(25, " ")
        strS = strS & Mid(NNull(NOME_CEDENTE, 1) & Strings.StrDup(30, " "), 1, 30)

        If cod_banco = "033" Then strS = strS & Mid("BANCO SANTANDER" & Strings.StrDup(30, " "), 1, 30)
        If cod_banco = "104" Then strS = strS & Mid("CAIXA" & Strings.StrDup(30, " "), 1, 30)
        If cod_banco = "001" Then strS = strS & Mid("BANCO DO BRASIL" & Strings.StrDup(30, " "), 1, 30)

        strS = strS & Strings.StrDup(10, " ")
        strS = strS & "1"
        strS = strS & Format(Now.Date, "ddmmyyyy").Replace("/", "")
        strS = strS & Strings.StrDup(6, " ")
        strS = strS & Right(Strings.StrDup(6, "0") & NNull(seq_arquivo, 0), 6)
        strS = strS & "040"
        strS = strS & Strings.StrDup(74, " ")

        criaHeaderSantander = strS

        Exit Function

erro:
        criaHeaderSantander = "ERRO:" & Err.Description


    End Function



    Public Function criaHeaderLoteSantander(NHeaderLote As Long, cod_banco As String, CNPJ_CEDENTE As String, NOME_CEDENTE As String, cod_trans As String, obs1 As String, obs2 As String) As String
        '001 - 003 Código do Banco na compensação N 003 353 / 008 / 033
        '004 - 007 Numero do lote remessa N 004 1
        '008 - 008 Tipo de registro N 001 1 2
        '009 - 009 Tipo de operação A 001 R (Remessa)
        '010 - 011 Tipo de serviço N 002 01 (Cobrança)
        '012 - 013 Reservado (uso Banco) A 002 Brancos
        '014 - 016 Nº da versão do layout do lote N 003 030
        '017 - 017 Reservado (uso Banco) A 001 Brancos
        '018 - 018 Tipo de inscrição da empresa N 001 1 = CPF, 2 = CNPJ
        '019 - 033 Nº de inscrição da empresa N 015
        '034 - 053 Reservado (uso Banco) A 020 Brancos
        '054 - 068 Código de Transmissão N 015 3
        '069 - 073 Reservado uso Banco A 005 Brancos
        '074 - 103 Nome do Cedente A 030
        '104 - 143 Mensagem 1 A 040 9
        '144 - 183 Mensagem 2 A 040 9
        '184 - 191 Número remessa/retorno N 008 10
        '192 - 199 Data da gravação remessa/retorno N 008 DDMMAAAA
        '200 - 240 Reservado (uso Banco) A 041 Brancos

        On Error GoTo erro
        Dim strS As String

        criaHeaderLoteSantander = String.Empty

        strS = String.Empty
        strS = Microsoft.VisualBasic.Right(Strings.StrDup(3, "0") & NNull(cod_banco, 0), 3)
        strS = strS & Right(Strings.StrDup(4, "0") & NHeaderLote, 4)
        strS = strS & "1"
        strS = strS & "R"
        strS = strS & "01"
        strS = strS & Strings.StrDup(2, " ")
        strS = strS & "030"
        strS = strS & " "
        strS = strS & "2"
        strS = strS & Microsoft.VisualBasic.Right(Strings.StrDup(15, "0") & ObtemNumero(NNull(CNPJ_CEDENTE, 1)), 15)
        strS = strS & Strings.StrDup(20, " ")
        strS = strS & Microsoft.VisualBasic.Right(Strings.StrDup(15, "0") & NNull(cod_trans, 0), 15)
        strS = strS & Strings.StrDup(5, " ")
        strS = strS & Mid(NNull(NOME_CEDENTE, 1) & Strings.StrDup(30, " "), 1, 30)
        strS = strS & Mid(NNull(obs1, 1) & Strings.StrDup(40, " "), 1, 40)
        strS = strS & Mid(NNull(obs2, 1) & Strings.StrDup(40, " "), 1, 40)
        strS = strS & Microsoft.VisualBasic.Right(Strings.StrDup(8, "0") & NHeaderLote, 8)
        strS = strS & Format(Now.Date, "ddmmyyyy").Replace("/", "")
        strS = strS & Strings.StrDup(41, " ")

        criaHeaderLoteSantander = strS

        Exit Function

erro:
        criaHeaderLoteSantander = "ERRO:" & Err.Description


    End Function

    Public Function ObtemNumero(VALOR As String) As String
        Dim i As Integer
        ObtemNumero = ""
        For i = 1 To Len(VALOR)
            If IsNumeric(Mid(VALOR, i, 1)) = True Then
                ObtemNumero = ObtemNumero & Mid(VALOR, i, 1)
            End If
        Next i

    End Function

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

    Public Function criaDetalhePSantander(NHeaderLote As Long, NSeqRegistro As Long, NossoNum As String, NNossoDoc As String, DtVencimento As Date, DtEmissao As Date, VALOR As STRING, cod_banco As String, CNPJ_CEDENTE As String, NOME_CEDENTE As String, cod_trans As String, COD_MOV As String, agencia As String, DIG_agencia As String, Conta As String, dig_conta As String, especie_titulo As String, cod_mora As String, COD_PROTESTO As String, N_DIAS_PROTESTO As String, COD_BAIXA As String, N_DIAS_BAIXA As String, vlr_mora As Double) As String
        On Error GoTo erro
        Dim strS As String

        Dim teste As String = vlr_mora.ToString("#.00000").Replace(",", "").Replace(".", "")

        criaDetalhePSantander = String.Empty

        strS = String.Empty
        strS = Right(Strings.StrDup(3, "0") & NNull(cod_banco, 0), 3)
        strS = strS & Right(Strings.StrDup(4, "0") & NHeaderLote, 4)
        strS = strS & "3"
        strS = strS & Right(Strings.StrDup(5, "0") & NSeqRegistro, 5)
        strS = strS & "P"
        strS = strS & " "
        strS = strS & Right(Strings.StrDup(2, "0") & NNull(Mid(COD_MOV, 1, 2), 0), 2)
        strS = strS & Right(Strings.StrDup(4, "0") & NNull(agencia, 0), 4)
        strS = strS & Right(Strings.StrDup(1, "0") & NNull(DIG_agencia, 0), 1)
        strS = strS & Right(Strings.StrDup(9, "0") & NNull(Conta, 0), 9)
        strS = strS & Right(Strings.StrDup(1, "0") & NNull(dig_conta, 0), 1)
        strS = strS & Right(Strings.StrDup(9, "0") & NNull(Conta, 0), 9)
        strS = strS & Right(Strings.StrDup(1, "0") & NNull(dig_conta, 0), 1)
        strS = strS & Strings.StrDup(2, " ")
        strS = strS & Right(Strings.StrDup(13, "0") & NNull(NossoNum.Replace(" ", ""), 1), 13)
        strS = strS & "5"
        strS = strS & "1"
        strS = strS & "1"
        strS = strS & Strings.StrDup(1, " ")
        strS = strS & Strings.StrDup(1, " ")
        strS = strS & Mid(NNossoDoc & Strings.StrDup(15, " "), 1, 15)
        strS = strS & Format(DtVencimento, "ddmmyyyy").Replace("/", "")
        strS = strS & Right(Strings.StrDup(15, "0") & VALOR.Replace(",", ".").Replace(".", ""), 15) '& Replace(PPonto(Format(VALOR, "#.00")), ".", ""), 15)
        strS = strS & Right(Strings.StrDup(4, "0") & NNull(agencia, 0), 4)
        strS = strS & Microsoft.VisualBasic.Right(Strings.StrDup(1, "0") & NNull(DIG_agencia, 0), 1)
        strS = strS & Strings.StrDup(1, " ")
        Select Case NNull(Mid(especie_titulo, 1, 2), 1)
            Case "DM" : strS = strS & "02"
            Case "DS" : strS = strS & "04"
            Case "LC" : strS = strS & "07"
            Case "NP" : strS = strS & "12"
            Case "NR" : strS = strS & "13"
            Case "RC" : strS = strS & "17"
            Case "AP" : strS = strS & "20"
            Case "CH" : strS = strS & "97"
            Case "ND" : strS = strS & "98"
            Case Else : strS = strS & "00"
        End Select
        strS = strS & "N"
        strS = strS & Format(DtEmissao, "ddmmyyyy").Replace("/", "")
        strS = strS & NNull(Mid(cod_mora, 1, 1), 0)
        strS = strS & Format(DtVencimento, "ddmmyyyy").Replace("/", "")

        Select Case Mid(NNull(cod_mora, 1), 1, 1)
            Case "1"
                strS = strS & Right(Strings.StrDup(15, "0") & vlr_mora.ToString("#.00"), 15)
            Case "2"

                strS = strS & Right(Strings.StrDup(15, "0") & vlr_mora.ToString("#.00000").Replace(",", "").Replace(".", ""), 15)


            Case Else
                strS = strS & Strings.StrDup(15, "0")
        End Select


        strS = strS & "0"
        strS = strS & "00000000"
        strS = strS & Strings.StrDup(15, "0")
        strS = strS & Strings.StrDup(15, "0")
        strS = strS & Strings.StrDup(15, "0")
        strS = strS & Strings.StrDup(25, " ")
        strS = strS & NNull(Mid(COD_PROTESTO, 1, 1), 0)
        strS = strS & Right(Strings.StrDup(2, "0") & NNull(N_DIAS_PROTESTO, 0), 2)
        strS = strS & NNull(Mid(COD_BAIXA, 1, 1), 0)
        strS = strS & "0"
        strS = strS & Right(Strings.StrDup(2, "0") & NNull(N_DIAS_BAIXA, 0), 2)
        strS = strS & "00"
        strS = strS & Strings.StrDup(11, " ")

        criaDetalhePSantander = strS

        Exit Function

erro:
        criaDetalhePSantander = "ERRO:" & Err.Description


    End Function

    Public Function criaDetalheQSantander(NHeaderLote As Long, NSeqRegistro As Long, DocSacado As String, NomeSacado As String, EndSacado As String, BaiSacado As String, CEPSacado As String, CidadeSacado As String, UFSacado As String, cod_banco As String, COD_MOV As String) As String
        On Error GoTo erro
        Dim strS As String
        criaDetalheQSantander = String.Empty

        strS = String.Empty
        strS = Right(Strings.StrDup(3, "0") & NNull(cod_banco, 0), 3)
        strS = strS & Right(Strings.StrDup(4, "0") & NHeaderLote, 4)
        strS = strS & "3"
        strS = strS & Right(Strings.StrDup(5, "0") & NSeqRegistro, 5)
        strS = strS & "Q"
        strS = strS & " "
        strS = strS & Right(Strings.StrDup(2, "0") & NNull(Mid(COD_MOV, 1, 2), 0), 2)
        strS = strS & IIf(InStr(1, DocSacado, "/"), "2", "1")
        strS = strS & Right(Strings.StrDup(15, "0") & ObtemNumero(NNull(DocSacado, 1)), 15)
        strS = strS & Mid(NomeSacado & Strings.StrDup(40, " "), 1, 40)
        strS = strS & Mid(EndSacado & Strings.StrDup(40, " "), 1, 40)
        strS = strS & Mid(BaiSacado & Strings.StrDup(15, " "), 1, 15)
        strS = strS & Right(Strings.StrDup(8, "0") & NNull(ObtemNumero(CEPSacado), 0), 8)
        strS = strS & Mid(CidadeSacado & Strings.StrDup(15, " "), 1, 15)
        strS = strS & Mid(UFSacado & Strings.StrDup(2, " "), 1, 2)
        strS = strS & "0"
        strS = strS & Strings.StrDup(15, "0")
        strS = strS & Strings.StrDup(40, " ")
        strS = strS & Strings.StrDup(3, "0")
        strS = strS & Strings.StrDup(3, "0")
        strS = strS & Strings.StrDup(3, "0")
        strS = strS & Strings.StrDup(3, "0")
        strS = strS & Strings.StrDup(19, " ")


        criaDetalheQSantander = strS

        Exit Function

erro:
        criaDetalheQSantander = "ERRO:" & Err.Description


    End Function

    Public Function criaDetalheRSantander(NHeaderLote As Long, NSeqRegistro As Long, cod_banco As String, COD_MOV As String, COD_MULTA As String, vlr_multa As Decimal, ND As String) As String
        On Error GoTo erro
        Dim strS As String

        criaDetalheRSantander = String.Empty

        strS = String.Empty
        strS = Right(Strings.StrDup(3, "0") & NNull(cod_banco, 0), 3)
        strS = strS & Right(Strings.StrDup(4, "0") & NHeaderLote, 4)
        strS = strS & "3"
        strS = strS & Right(Strings.StrDup(5, "0") & NSeqRegistro, 5)
        strS = strS & "R"
        strS = strS & " "
        strS = strS & Right(Strings.StrDup(2, "0") & NNull(Mid(COD_MOV, 1, 2), 0), 2)
        strS = strS & "0"
        strS = strS & "00000000"
        strS = strS & Strings.StrDup(15, "0")
        strS = strS & Strings.StrDup(24, " ")
        strS = strS & NNull(Mid(COD_MULTA, 1, 1), 0)
        strS = strS & "00000000"
        strS = strS & Right(Strings.StrDup(15, "0") & vlr_multa.ToString("#.00").Replace(",", "").Replace(".", ""), 15)
        strS = strS & Strings.StrDup(10, " ")
        ''strS = strS & Strings.StrDup(40, " ")
        strS = strS & ND.PadRight(40, " ")
        strS = strS & Strings.StrDup(40, " ")
        strS = strS & Strings.StrDup(61, " ")
        criaDetalheRSantander = strS

        Exit Function

erro:
        criaDetalheRSantander = "ERRO:" & Err.Description

    End Function

    Public Function criaTrailerLoteSantander(NHeaderLote As Long, QtdRegLote As Long, cod_banco As String) As String
        On Error GoTo erro
        Dim strS As String
        'QtdRegLote = QtdRegLote + 1
        criaTrailerLoteSantander = String.Empty

        strS = String.Empty
        strS = Right(Strings.StrDup(3, "0") & NNull(cod_banco, 0), 3)
        strS = strS & Right(Strings.StrDup(4, "0") & NHeaderLote, 4)
        strS = strS & "5"
        strS = strS & Strings.StrDup(9, " ")
        strS = strS & Right(Strings.StrDup(6, "0") & QtdRegLote, 6)
        strS = strS & Strings.StrDup(217, " ")
        criaTrailerLoteSantander = strS

        Exit Function

erro:
        criaTrailerLoteSantander = "ERRO:" & Err.Description


    End Function


    Public Function criaTrailerSantander(NHeaderLote As Long, QtdRegArq As Long, cod_banco As String) As String
        On Error GoTo erro
        Dim strS As String
        'QtdRegArq = QtdRegArq + 2
        criaTrailerSantander = String.Empty

        strS = String.Empty
        strS = Right(Strings.StrDup(3, "0") & NNull(cod_banco, 0), 3)
        'strS = strS & Right(String(4, "0") & NHeaderLote, 4)
        strS = strS & Strings.StrDup(4, "9")
        strS = strS & "9"
        strS = strS & Strings.StrDup(9, " ")
        strS = strS & "000001"
        strS = strS & Right(Strings.StrDup(6, "0") & QtdRegArq, 6)
        strS = strS & Strings.StrDup(211, " ")

        criaTrailerSantander = strS

        Exit Function

erro:
        criaTrailerSantander = "ERRO:" & Err.Description


    End Function







    Public Function obtemProximoNossoNum(SEQ As String) As String
        On Error GoTo erro

        obtemProximoNossoNum = Format("0", "0###########")
        Dim ConOracle As New Conexao_oracle
        ConOracle.Conectar()
        Dim Sql As String = "SELECT SGIPA." & SEQ & ".NEXTVAL AS SEQ FROM DUAL "
        Dim rsNoss As DataTable = ConOracle.Consultar(Sql)
        If rsNoss.Rows.Count > 0 Then
            obtemProximoNossoNum = Format(NNull(rsNoss.Rows(0)("SEQ").ToString, 1), "0###########")
        End If

        Exit Function

erro:

        Err.Description = Empty
        Exit Function

    End Function
    Public Function Calculo_NossoNumero(Sequencia As String) As String
        'montamos o nosso numero com o numero do convenio ( 6 posicoes)
        Dim dv As Integer

        dv = Calculo_DV_NN_Santander(Sequencia)
        Calculo_NossoNumero = Format(Sequencia, "0###########") & dv

    End Function
    Public Function Calculo_DV_NN_Santander(strNumero As String) As String
        'declara as variáveis
        Dim intcontador, intnumero, intTotalNumero, intMultiplicador, intResto As Integer

        strNumero = strNumero.Replace(".", "")

        ' se nao for um valor numerico sai da função
        If Not IsNumeric(strNumero) Then
            Calculo_DV_NN_Santander = ""
            Exit Function
        End If

        'inicia o multiplicador
        intMultiplicador = 2

        'pega cada caracter do numero a partir da direita
        For intcontador = Len(strNumero) To 1 Step -1

            'extrai o caracter e multiplica prlo multiplicador
            intnumero = Val(Mid(strNumero, intcontador, 1)) * intMultiplicador

            'soma o resultado para totalização
            intTotalNumero = intTotalNumero + intnumero

            'se o multiplicador for maior que 2 decrementa-o caso contrario atribuir valor padrao original
            intMultiplicador = IIf(intMultiplicador = 9, 2, intMultiplicador + 1)

        Next

        'calcula o resto da divisao do total por 11
        intResto = intTotalNumero Mod 11

        'verifica as exceções ( 0 -> DV=0    10 -> DV=X (para o BB) e retorna o DV
        Select Case intResto
            Case 0, 1
                Calculo_DV_NN_Santander = "0"
            Case 10
                Calculo_DV_NN_Santander = "1"
            Case Else
                Calculo_DV_NN_Santander = Str(11 - intResto)
        End Select

    End Function




End Class

