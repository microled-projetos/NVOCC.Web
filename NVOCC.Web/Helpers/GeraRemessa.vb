Public Class GeraRemessa
    '    Public Function criaHeaderSantander() As String
    '        '001 - 003 Código do Banco na compensação N 003 353 / 008 / 033
    '        '004 - 007 Lote de serviço N 004 0000 1
    '        '008 - 008 Tipo de registro N 001 0 2
    '        '009 - 016 Reservado (uso Banco) A 008 Brancos
    '        '017 - 017 Tipo de inscrição da empresa N 001 1 = CPF, 2 = CNPJ
    '        '018 - 032 Nº de inscrição da empresa N 015
    '        '033 - 047 Código de Transmissão N 015 3
    '        '048 - 072 Reservado (uso Banco) A 025 Brancos
    '        '073 - 102 Nome da empresa A 030
    '        '103 - 132 Nome do Banco A 030 Banco Santander
    '        '133 - 142 Reservado (uso Banco) A 010 Brancos
    '        '143 - 143 Código remessa N 001 1 = Remessa
    '        '144 - 151 Data de geração do arquivo N 008 DDMMAAAA
    '        '152 - 157 Reservado (uso Banco) A 006 Brancos
    '        '158 - 163 Nº seqüencial do arquivo N 006 4
    '        '164 - 166 Nº da versão do layout do arquivo N 003 040
    '        '167 - 240 Reservado (uso Banco) A 074 Brancos
    '        On Error GoTo erro
    '        Dim strS As String

    '        criaHeaderSantander = String.Empty

    '        strS = String.Empty
    '        strS = Right(String(3, "0") & IsDBNull(rsBanco!cod_banco, 0), 3)
    '        strS = strS & "0000"
    '        strS = strS & "0"
    '        strS = strS & String(8, " ")
    '        strS = strS & "2"
    '        strS = strS & Right(String(15, "0") & ObtemNumero(IsDBNull(rsBanco!CNPJ_CEDENTE, 1)), 15)
    '        strS = strS & Right(String(15, "0") & IsDBNull(rsBanco!cod_trans, 0), 15)
    '        strS = strS & String(25, " ")
    '        strS = strS & Mid(IsDBNull(rsBanco!NOME_CEDENTE, 1) & String(30, " "), 1, 30)

    '        If rsBanco!cod_banco = "033" Then strS = strS & Mid("BANCO SANTANDER" & String(30, " "), 1, 30)
    '        If rsBanco!cod_banco = "104" Then strS = strS & Mid("CAIXA" & String(30, " "), 1, 30)
    '        If rsBanco!cod_banco = "001" Then strS = strS & Mid("BANCO DO BRASIL" & String(30, " "), 1, 30)

    '        strS = strS & String(10, " ")
    '        strS = strS & "1"
    '        strS = strS & Format(Now, "ddmmyyyy")
    '        strS = strS & String(6, " ")
    '        strS = strS & Right(String(6, "0") & IsDBNull(rsBanco!seq_arquivo, 0), 6)
    '        strS = strS & "040"
    '        strS = strS & String(74, " ")

    '        criaHeaderSantander = strS

    '        Exit Function

    'erro:
    '        criaHeaderSantander = "ERRO:" & Err.Description


    '    End Function

    '    Public Function criaHeaderLoteSantander(NHeaderLote As Long) As String
    '        '001 - 003 Código do Banco na compensação N 003 353 / 008 / 033
    '        '004 - 007 Numero do lote remessa N 004 1
    '        '008 - 008 Tipo de registro N 001 1 2
    '        '009 - 009 Tipo de operação A 001 R (Remessa)
    '        '010 - 011 Tipo de serviço N 002 01 (Cobrança)
    '        '012 - 013 Reservado (uso Banco) A 002 Brancos
    '        '014 - 016 Nº da versão do layout do lote N 003 030
    '        '017 - 017 Reservado (uso Banco) A 001 Brancos
    '        '018 - 018 Tipo de inscrição da empresa N 001 1 = CPF, 2 = CNPJ
    '        '019 - 033 Nº de inscrição da empresa N 015
    '        '034 - 053 Reservado (uso Banco) A 020 Brancos
    '        '054 - 068 Código de Transmissão N 015 3
    '        '069 - 073 Reservado uso Banco A 005 Brancos
    '        '074 - 103 Nome do Cedente A 030
    '        '104 - 143 Mensagem 1 A 040 9
    '        '144 - 183 Mensagem 2 A 040 9
    '        '184 - 191 Número remessa/retorno N 008 10
    '        '192 - 199 Data da gravação remessa/retorno N 008 DDMMAAAA
    '        '200 - 240 Reservado (uso Banco) A 041 Brancos

    '        On Error GoTo erro
    '        Dim strS As String

    '        criaHeaderLoteSantander = String.Empty

    '        strS = String.Empty
    '        strS = Right(String(3, "0") & IsDBNull(rsBanco!cod_banco, 0), 3)
    '        strS = strS & Right(String(4, "0") & NHeaderLote, 4)
    '        strS = strS & "1"
    '        strS = strS & "R"
    '        strS = strS & "01"
    '        strS = strS & String(2, " ")
    '        strS = strS & "030"
    '        strS = strS & " "
    '        strS = strS & "2"
    '        strS = strS & Right(String(15, "0") & ObtemNumero(IsDBNull(rsBanco!CNPJ_CEDENTE, 1)), 15)
    '        strS = strS & String(20, " ")
    '        strS = strS & Right(String(15, "0") & IsDBNull(rsBanco!cod_trans, 0), 15)
    '        strS = strS & String(5, " ")
    '        strS = strS & Mid(IsDBNull(rsBanco!NOME_CEDENTE, 1) & String(30, " "), 1, 30)
    '        strS = strS & Mid(IsDBNull(rsBanco!obs1, 1) & String(40, " "), 1, 40)
    '        strS = strS & Mid(IsDBNull(rsBanco!obs2, 1) & String(40, " "), 1, 40)
    '        strS = strS & Right(String(8, "0") & NHeaderLote, 8)
    '        strS = strS & Format(Now, "ddmmyyyy")
    '        strS = strS & String(41, " ")

    '        criaHeaderLoteSantander = strS

    '        Exit Function

    'erro:
    '        criaHeaderLoteSantander = "ERRO:" & Err.Description


    '    End Function



    '    Public Function criaDetalhePSantander(NHeaderLote As Long, NSeqRegistro As Long, NossoNum As String, NNossoDoc As String, DtVencimento As Date, DtEmissao As Date, VALOR As Double) As String
    '        On Error GoTo erro
    '        Dim strS As String

    '        criaDetalhePSantander = String.Empty

    '        strS = String.Empty
    '        strS = Right(String(3, "0") & IsDBNull(rsBanco!cod_banco, 0), 3)
    '        strS = strS & Right(String(4, "0") & NHeaderLote, 4)
    '        strS = strS & "3"
    '        strS = strS & Right(String(5, "0") & NSeqRegistro, 5)
    '        strS = strS & "P"
    '        strS = strS & " "
    '        strS = strS & Right(String(2, "0") & IsDBNull(Mid(rsBanco!COD_MOV, 1, 2), 0), 2)
    '        strS = strS & Right(String(4, "0") & IsDBNull(rsBanco!agencia, 0), 4)
    '        strS = strS & Right(String(1, "0") & IsDBNull(rsBanco!DIG_agencia, 0), 1)
    '        strS = strS & Right(String(9, "0") & IsDBNull(rsBanco!Conta, 0), 9)
    '        strS = strS & Right(String(1, "0") & IsDBNull(rsBanco!dig_conta, 0), 1)
    '        strS = strS & Right(String(9, "0") & IsDBNull(rsBanco!Conta, 0), 9)
    '        strS = strS & Right(String(1, "0") & IsDBNull(rsBanco!dig_conta, 0), 1)
    '        strS = strS & String(2, " ")
    '        strS = strS & Right(String(13, "0") & IsDBNull(Replace(NossoNum, " ", ""), 1), 13)
    '        strS = strS & "5"
    '        strS = strS & "1"
    '        strS = strS & "1"
    '        strS = strS & String(1, " ")
    '        strS = strS & String(1, " ")
    '        strS = strS & Mid(NNossoDoc & String(15, " "), 1, 15)
    '        strS = strS & Format(DtVencimento, "ddmmyyyy")
    '        strS = strS & Right(String(15, "0") & Replace(PPonto(Format(VALOR, "#.00")), ".", ""), 15)
    '        strS = strS & Right(String(4, "0") & IsDBNull(rsBanco!agencia, 0), 4)
    '        strS = strS & Right(String(1, "0") & IsDBNull(rsBanco!DIG_agencia, 0), 1)
    '        strS = strS & String(1, " ")
    '        Select Case IsDBNull(Mid(rsBanco!especie_titulo, 1, 2), 1)
    '            Case "DM" : strS = strS & "02"
    '            Case "DS" : strS = strS & "04"
    '            Case "LC" : strS = strS & "07"
    '            Case "NP" : strS = strS & "12"
    '            Case "NR" : strS = strS & "13"
    '            Case "RC" : strS = strS & "17"
    '            Case "AP" : strS = strS & "20"
    '            Case "CH" : strS = strS & "97"
    '            Case "ND" : strS = strS & "98"
    '            Case Else : strS = strS & "00"
    '        End Select
    '        strS = strS & "N"
    '        strS = strS & Format(DtEmissao, "ddmmyyyy")
    '        strS = strS & IsDBNull(Mid(rsBanco!cod_mora, 1, 1), 0)
    '        strS = strS & Format(DtVencimento, "ddmmyyyy")

    '        Select Case Mid(IsDBNull(rsBanco!cod_mora, 1), 1, 1)
    '            Case "1"
    '                strS = strS & Right(String(15, "0") & Replace(PPonto(Format(rsBanco!vlr_mora, "#.00")), ".", ""), 15)
    '            Case "2"
    '                strS = strS & Right(String(15, "0") & Replace(PPonto(Format(rsBanco!vlr_mora, "#.00000")), ".", ""), 15)
    '            Case Else
    '                strS = strS & String(15, "0")
    '        End Select


    '        strS = strS & "0"
    '        strS = strS & "00000000"
    '        strS = strS & String(15, "0")
    '        strS = strS & String(15, "0")
    '        strS = strS & String(15, "0")
    '        strS = strS & String(25, " ")
    '        strS = strS & IsDBNull(Mid(rsBanco!COD_PROTESTO, 1, 1), 0)
    '        strS = strS & Right(String(2, "0") & IsDBNull(rsBanco!N_DIAS_PROTESTO, 0), 2)
    '        strS = strS & IsDBNull(Mid(rsBanco!COD_BAIXA, 1, 1), 0)
    '        strS = strS & "0"
    '        strS = strS & Right(String(2, "0") & IsDBNull(rsBanco!N_DIAS_BAIXA, 0), 2)
    '        strS = strS & "00"
    '        strS = strS & String(11, " ")

    '        criaDetalhePSantander = strS

    '        Exit Function

    'erro:
    '        criaDetalhePSantander = "ERRO:" & Err.Description


    '    End Function

    '    Public Function criaDetalheQSantander(NHeaderLote As Long, NSeqRegistro As Long, DocSacado As String, NomeSacado As String, EndSacado As String, BaiSacado As String, CEPSacado As String, CidadeSacado As String, UFSacado As String) As String
    '        On Error GoTo erro
    '        Dim strS As String

    '        criaDetalheQSantander = String.Empty

    '        strS = String.Empty
    '        strS = Right(String(3, "0") & IsDBNull(rsBanco!cod_banco, 0), 3)
    '        strS = strS & Right(String(4, "0") & NHeaderLote, 4)
    '        strS = strS & "3"
    '        strS = strS & Right(String(5, "0") & NSeqRegistro, 5)
    '        strS = strS & "Q"
    '        strS = strS & " "
    '        strS = strS & Right(String(2, "0") & IsDBNull(Mid(rsBanco!COD_MOV, 1, 2), 0), 2)
    '        strS = strS & IIf(InStr(1, DocSacado, "/"), "2", "1")
    '        strS = strS & Right(String(15, "0") & ObtemNumero(IsDBNull(DocSacado, 1)), 15)
    '        strS = strS & Mid(NomeSacado & String(40, " "), 1, 40)
    '        strS = strS & Mid(EndSacado & String(40, " "), 1, 40)
    '        strS = strS & Mid(BaiSacado & String(15, " "), 1, 15)
    '        strS = strS & Right(String(8, "0") & IsDBNull(ObtemNumero(CEPSacado), 0), 8)
    '        strS = strS & Mid(CidadeSacado & String(15, " "), 1, 15)
    '        strS = strS & Mid(UFSacado & String(2, " "), 1, 2)
    '        strS = strS & "0"
    '        strS = strS & String(15, "0")
    '        strS = strS & String(40, " ")
    '        strS = strS & String(3, "0")
    '        strS = strS & String(3, "0")
    '        strS = strS & String(3, "0")
    '        strS = strS & String(3, "0")
    '        strS = strS & String(19, " ")


    '        criaDetalheQSantander = strS

    '        Exit Function

    'erro:
    '        criaDetalheQSantander = "ERRO:" & Err.Description


    '    End Function

    '    Public Function criaDetalheRSantander(NHeaderLote As Long, NSeqRegistro As Long) As String
    '        On Error GoTo erro
    '        Dim strS As String

    '        criaDetalheRSantander = String.Empty

    '        strS = String.Empty
    '        strS = Right(String(3, "0") & IsDBNull(rsBanco!cod_banco, 0), 3)
    '        strS = strS & Right(String(4, "0") & NHeaderLote, 4)
    '        strS = strS & "3"
    '        strS = strS & Right(String(5, "0") & NSeqRegistro, 5)
    '        strS = strS & "R"
    '        strS = strS & " "
    '        strS = strS & Right(String(2, "0") & IsDBNull(Mid(rsBanco!COD_MOV, 1, 2), 0), 2)
    '        strS = strS & "0"
    '        strS = strS & "00000000"
    '        strS = strS & String(15, "0")
    '        strS = strS & String(24, " ")
    '        strS = strS & IsDBNull(Mid(rsBanco!COD_MULTA, 1, 1), 0)
    '        strS = strS & "00000000"
    '        strS = strS & Right(String(15, "0") & Replace(PPonto(Format(rsBanco!vlr_multa, "#.00")), ".", ""), 15)
    '        strS = strS & String(10, " ")
    '        strS = strS & String(40, " ")
    '        strS = strS & String(40, " ")
    '        strS = strS & String(61, " ")
    '        criaDetalheRSantander = strS

    '        Exit Function

    'erro:
    '        criaDetalheRSantander = "ERRO:" & Err.Description

    '    End Function

    '    Public Function criaTrailerLoteSantander(NHeaderLote As Long, QtdRegLote As Long) As String
    '        On Error GoTo erro
    '        Dim strS As String
    '        'QtdRegLote = QtdRegLote + 1
    '        criaTrailerLoteSantander = String.Empty

    '        strS = String.Empty
    '        strS = Right(String(3, "0") & IsDBNull(rsBanco!cod_banco, 0), 3)
    '        strS = strS & Right(String(4, "0") & NHeaderLote, 4)
    '        strS = strS & "5"
    '        strS = strS & String(9, " ")
    '        strS = strS & Right(String(6, "0") & QtdRegLote, 6)
    '        strS = strS & String(217, " ")
    '        criaTrailerLoteSantander = strS

    '        Exit Function

    'erro:
    '        criaTrailerLoteSantander = "ERRO:" & Err.Description


    '    End Function


    '    Public Function criaTrailerSantander(NHeaderLote As Long, QtdRegArq As Long) As String
    '        On Error GoTo erro
    '        Dim strS As String
    '        'QtdRegArq = QtdRegArq + 2
    '        criaTrailerSantander = String.Empty

    '        strS = String.Empty
    '        strS = Right(String(3, "0") & IsDBNull(rsBanco!cod_banco, 0), 3)
    '        'strS = strS & Right(String(4, "0") & NHeaderLote, 4)
    '        strS = strS & String(4, "9")
    '        strS = strS & "9"
    '        strS = strS & String(9, " ")
    '        strS = strS & "000001"
    '        strS = strS & Right(String(6, "0") & QtdRegArq, 6)
    '        strS = strS & String(211, " ")

    '        criaTrailerSantander = strS

    '        Exit Function

    'erro:
    '        criaTrailerSantander = "ERRO:" & Err.Description


    '    End Function


    '    Sub naosei()
    '        If rsBanco!cod_banco = "033" Or rsBanco!cod_banco = "104" Or rsBanco!cod_banco = "001" Then
    '            Print #1, criaHeaderSantander
    '    seqRem = 1
    '            Print #1, criaHeaderLoteSantander(1)
    '    seqRem = seqRem + 1
    '            seqLote = 1
    '            For i = 1 To gridAberto.Rows - 1
    '                DoEvents
    '                gridAberto.Row = i
    '                gridAberto.col = 1
    '                If gridAberto.CellPicture = ImageSIM.Picture Then
    '                    Print #1, criaDetalhePSantander(1, seqLote, IsDBNull(gridAberto.TextMatrix(i, 5), 1), IsDBNull(gridAberto.TextMatrix(i, 4), 1), IsDBNull(gridAberto.TextMatrix(i, 9), 1), IsDBNull(gridAberto.TextMatrix(i, 8), 1), IsDBNull(gridAberto.TextMatrix(i, 6), 0))
    '            seqLote = seqLote + 1
    '                    seqRem = seqRem + 1
    '                    Print #1, criaDetalheQSantander(1, seqLote, IsDBNull(gridAberto.TextMatrix(i, 13), 1), IsDBNull(gridAberto.TextMatrix(i, 14), 1), IsDBNull(gridAberto.TextMatrix(i, 15), 1), IsDBNull(gridAberto.TextMatrix(i, 16), 1), IsDBNull(gridAberto.TextMatrix(i, 17), 1), IsDBNull(gridAberto.TextMatrix(i, 18), 1), IsDBNull(gridAberto.TextMatrix(i, 19), 1))
    '            seqLote = seqLote + 1
    '                    seqRem = seqRem + 1
    '                    If IsDBNull(rsBanco!COD_MULTA, 1) <> "" Then
    '                        Print #1, criaDetalheRSantander(1, seqLote)
    '                seqLote = seqLote + 1
    '                        seqRem = seqRem + 1
    '                    End If
    '                    If IsDBNull(gridAberto.TextMatrix(i, 2), 1) = "IPA" Then
    '                        Executa "UPDATE " & Banco_Sgipa & ".FATURANOTA SET ENVIADO_REM = 1, DT_ENVIO_REM = SYSDATE, ARQ_REM ='" & IsDBNull(Me.CommonDialog1.FileTitle, 1) & "', USUARIO_REM ='" & IsDBNull(Usuario_Sistema, 1) & "' WHERE NOSSONUMERO ='" & IsDBNull(gridAberto.TextMatrix(i, 5), 0) & "' "
    '            Else
    '                        Executa "UPDATE REDEX.FATURANOTA SET ENVIADO_REM = 1, DT_ENVIO_REM = SYSDATE, ARQ_REM ='" & IsDBNull(Me.CommonDialog1.FileTitle, 1) & "', USUARIO_REM ='" & IsDBNull(Usuario_Sistema, 1) & "' WHERE NOSSONUMERO ='" & IsDBNull(gridAberto.TextMatrix(i, 5), 0) & "' "
    '            End If
    '                End If
    '            Next i
    '            seqLote = seqLote + 1
    '            seqRem = seqRem + 1
    '            Print #1, criaTrailerLoteSantander(1, seqLote)
    '    seqRem = seqRem + 1
    '            Print #1, criaTrailerSantander(1, seqRem)
    '    Close #1
    ' End If
    '    End Sub

End Class
