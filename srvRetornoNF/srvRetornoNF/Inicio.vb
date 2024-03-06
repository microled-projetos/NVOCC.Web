Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Net.Mail
Imports System.Net

Module Inicio

    Public FlagExecutando As Boolean

    Public Sub Principal()
        Try

            ''ROTINA RECUPERA NUMERO DA NF, CHAMA ROBO DO PATRICK E ENVIA EMAIL PARA O CLIENTE  
            WriteToFile($"{DateTime.Now.ToString()} - Principal: Chama TesteRetornoNF ")
            ' TesteRetornoNF()


            ''ROTINA RECUPERA NUMERO DA NF, CHAMA ROBO DO PATRICK E ENVIA EMAIL PARA O CLIENTE  
            WriteToFile($"{DateTime.Now.ToString()} - Principal: Chama RetornoNF ")
            RetornoNF()


            ''ROTINA QUE ATUALIZA DATA DA BAIXA TOTVS NAS COMISSOES NACIONAIS - CHAMADO 3505
            WriteToFile($"{DateTime.Now.ToString()} - Principal: Chama TotvsComissoes ")
            TotvsComissoes()


            ''ROTINA QUE DELETA ARQUIVOC DE UPLOAD DO GLOBAL SYS - CHAMADO 33531  
            WriteToFile($"{DateTime.Now.ToString()} - Principal: Chama DeletaArquivos ")
            DeletaArquivos()


            FlagExecutando = True

        Catch ex As Exception
            WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)

            FlagExecutando = False

        End Try

    End Sub

    Public Sub TesteRetornoNF()

        WriteToFile($"{DateTime.Now.ToString()} - TesteRetornoNF: Inicio ")

        Try

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim args As String = ""

            Dim ds As DataSet = Con.ExecutarQuery("SELECT A.ID_FATURAMENTO,A.NR_NOTA_FISCAL,A.COD_VER_NFSE, B.CNPJ FROM TB_FATURAMENTO A CROSS JOIN TB_EMPRESAS B WHERE A.NR_RPS IS NOT NULL AND A.ID_STATUS_DOWNLOAD_NFE = 0 ORDER BY A.ID_FATURAMENTO DESC ")
            If ds.Tables(0).Rows.Count > 0 Then
                For Each linhads As DataRow In ds.Tables(0).Rows
                    args = args & " " & linhads.Item("NR_NOTA_FISCAL") & " " & linhads.Item("COD_VER_NFSE") & " " & linhads.Item("CNPJ") & " " & linhads.Item("ID_FATURAMENTO")
                    WriteToFile($"{DateTime.Now.ToString()} - TesteRetornoNF: args > " & args)
                Next
            End If

            If args <> "" Then
                Process.Start("E:\PDF_NFe\EXEC\BaixaNF.exe ", args)
            End If

            EnviaEmail()

        Catch ex As Exception

            WriteToFile($"{DateTime.Now.ToString()} - TesteRetornoNF Erro: " & ex.ToString)
            FlagExecutando = False

        End Try

        WriteToFile($"{DateTime.Now.ToString()} - TesteRetornoNF: Fim ")

    End Sub

    Public Sub RetornoNF()

        WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: Inicio ")

        Try

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim args As String = ""

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_FATURAMENTO FROM TB_FATURAMENTO WHERE ISNULL(NR_RPS,0) <> 0 AND ISNULL(NR_LOTE,0) <> 0 AND ISNULL(CANCELA_NFE,0) = 0 AND NR_NOTA_FISCAL IS NULL AND DT_CANCELAMENTO IS NULL ")

            If ds.Tables(0).Rows.Count > 0 Then

                For Each linhads As DataRow In ds.Tables(0).Rows

                    WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: For Each linhads As DataRow In ds.Tables(0).Rows  ")

                    Using ConsultaNF = New WsNVOCC.WsNvocc
                        WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: Using ConsultaNF = New WsNVOCC.WsNvocc  ")

                        Dim consulta = ConsultaNF.ConsultaNFePrefeitura(linhads.Item("ID_FATURAMENTO").ToString(), 1, "SQL", "NVOCC")

                        WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: ID_FATURAMENTO >> " & linhads.Item("ID_FATURAMENTO").ToString())

                    End Using

                    Con.ExecutarQuery("UPDATE TB_FATURAMENTO SET FL_SRV_RETORNO_NF =  1 WHERE ID_FATURAMENTO =  " & linhads.Item("ID_FATURAMENTO").ToString())
                    Dim dsDados As DataSet = Con.ExecutarQuery("SELECT A.ID_FATURAMENTO,A.NR_NOTA_FISCAL,A.COD_VER_NFSE, B.CNPJ FROM TB_FATURAMENTO A CROSS JOIN TB_EMPRESAS B WHERE A.ID_STATUS_DOWNLOAD_NFE = 0 AND A.ID_FATURAMENTO = " & linhads.Item("ID_FATURAMENTO").ToString())
                    WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: If dsDados.Tables(0).Rows.Count > 0 Then")
                    If dsDados.Tables(0).Rows.Count > 0 Then
                        WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: dentro do if ")
                        args = args & " " & dsDados.Tables(0).Rows(0).Item("NR_NOTA_FISCAL") & " " & dsDados.Tables(0).Rows(0).Item("COD_VER_NFSE") & " " & dsDados.Tables(0).Rows(0).Item("CNPJ") & " " & dsDados.Tables(0).Rows(0).Item("ID_FATURAMENTO")
                        WriteToFile($"{DateTime.Now.ToString()} - args " & args.ToString())
                    End If


                Next


            End If

            ''CHAMA ROBÔ DO PATRICK PASSANDO A VARIAVEL ARGS COM OS DADOS DAS NOTAS CONSULTADAS NO GINFES E ENVIA EMAIL
            WriteToFile($"{DateTime.Now.ToString()} - antes do If args <>  Then")
            If args <> "" Then
                WriteToFile($"{DateTime.Now.ToString()} - dentro do If args <>  Then  " & args.ToString())
                Process.Start("E:\PDF_NFe\EXEC\BaixaNF.exe", args)
            End If
            WriteToFile($"{DateTime.Now.ToString()} - depois do If args <>  Then")

            EnviaEmail()

        Catch ex As Exception
            WriteToFile($"{DateTime.Now.ToString()} - RetornoNF Erro: " & ex.ToString)

            FlagExecutando = False


        End Try

        WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: Fim ")

    End Sub
    Sub TotvsComissoes()

        WriteToFile($"{DateTime.Now.ToString()} - TotvsComissoes: Inicio ")

        Try
            Dim cn As String = ConfigurationManager.ConnectionStrings("NVOCC").ConnectionString
            Dim dsProc As DataSet = New DataSet()

            Using myConnection As SqlConnection = New SqlConnection(cn)
                WriteToFile($"{DateTime.Now.ToString()} - TotvsComissoes: Proc_Comissoes_Nacional_Totvs ")
                Dim dataadapter As SqlDataAdapter = New SqlDataAdapter("[dbo].[Proc_Comissoes_Nacional_Totvs]", myConnection)
                dataadapter.SelectCommand.CommandTimeout = 1800
                myConnection.Open()
                dataadapter.Fill(dsProc, "Authors_table")
            End Using
        Catch ex As Exception
            WriteToFile($"{DateTime.Now.ToString()} - TotvsComissoes Erro: " & ex.ToString)

            FlagExecutando = False

        End Try

        WriteToFile($"{DateTime.Now.ToString()} - TotvsComissoes: Fim ")

    End Sub
    Sub DeletaArquivos()

        WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos: Inicio ")

        Try

            ''DELETA ARQUIVOS TB_UPLOADS
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_ARQUIVO,CAMINHO_ARQUIVO FROM TB_UPLOADS A
INNER JOIN TB_TIPO_ARQUIVO B ON A.ID_TIPO_ARQUIVO = B.ID_TIPO_ARQUIVO
WHERE B.FL_EXPIRA = 1 AND DATEDIFF( DAY , DT_UPLOAD,GETDATE()) >= (SELECT QT_DIAS_EXPURGO FROM TB_PARAMETROS)")
            If ds.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    Con.ExecutarQuery("DELETE FROM TB_UPLOADS WHERE ID_ARQUIVO = " & linha.Item("ID_ARQUIVO"))
                    If Directory.Exists(linha.Item("CAMINHO_ARQUIVO")) Then
                        File.Delete(linha.Item("CAMINHO_ARQUIVO"))
                    End If
                Next

            End If
            Con.Fechar()

            ''DELETA ARQUIVOS TEMPORARIOS
            Dim CaminhoTemp = ConfigurationManager.AppSettings("CaminhoTemp").ToString()
            Dim di As DirectoryInfo = New DirectoryInfo(CaminhoTemp)


            For Each pastas As DirectoryInfo In di.GetDirectories
                If pastas.LastAccessTime < DateTime.Now.AddDays(-1) Then
                    For Each file As FileInfo In pastas.GetFiles()
                        file.Delete()
                    Next
                    pastas.Delete()
                End If
            Next


            ''DELETA NFs BAIXADAS PELO ROBÔ DO PATRICK
            Dim Mes As Integer = Now.Date.Month
            Dim Ano As Integer = Now.Date.Year
            Dim QtdDias As Integer = 0
            Dim DataAtual As Date = Now.Date
            Dim DataParametro As Date = Now.Date

            ds = Con.ExecutarQuery(" SELECT QT_DIAS_EXPURGO FROM TB_PARAMETROS ")
            If ds.Tables(0).Rows.Count > 0 Then
                QtdDias = ds.Tables(0).Rows(0)("QT_DIAS_EXPURGO").ToString
            End If


            Dim diretorio As String = "E:\PDF_NFe\PDF"
            DataParametro = DataAtual.AddDays(-QtdDias)

            If Directory.Exists(diretorio) Then
                di = New DirectoryInfo(diretorio)
                For Each PastaAno As DirectoryInfo In di.GetDirectories
                    For Each PastaMes As DirectoryInfo In PastaAno.GetDirectories()

                        For Each PastaDia As DirectoryInfo In PastaMes.GetDirectories()
                            For Each file As FileInfo In PastaDia.GetFiles()
                                If file.CreationTime < DataParametro Then
                                    file.Delete()
                                End If
                            Next

                            If PastaDia.GetFiles.Count = 0 And PastaDia.GetDirectories.Count = 0 Then
                                PastaDia.Delete()
                            End If
                        Next
                        If PastaMes.GetFiles.Count = 0 And PastaMes.GetDirectories.Count = 0 Then
                            PastaMes.Delete()
                        End If
                    Next
                    If PastaAno.GetFiles.Count = 0 And PastaAno.GetDirectories.Count = 0 Then
                        PastaAno.Delete()
                    End If
                Next

            End If

            diretorio = "E:\PDF_NFe\LOGS"
            If Directory.Exists(diretorio) Then
                di = New DirectoryInfo(diretorio)
                For Each PastaAno As DirectoryInfo In di.GetDirectories
                    For Each PastaMes As DirectoryInfo In PastaAno.GetDirectories()

                        For Each PastaDia As DirectoryInfo In PastaMes.GetDirectories()
                            For Each file As FileInfo In PastaDia.GetFiles()
                                If file.CreationTime < DataParametro Then
                                    file.Delete()
                                End If
                            Next

                            If PastaDia.GetFiles.Count = 0 And PastaDia.GetDirectories.Count = 0 Then
                                PastaDia.Delete()
                            End If
                        Next
                        If PastaMes.GetFiles.Count = 0 And PastaMes.GetDirectories.Count = 0 Then
                            PastaMes.Delete()
                        End If
                    Next
                    If PastaAno.GetFiles.Count = 0 And PastaAno.GetDirectories.Count = 0 Then
                        PastaAno.Delete()
                    End If
                Next

            End If
            Con.Fechar()



        Catch ex As Exception

            WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos Erro: " & ex.ToString)

            FlagExecutando = False

        End Try

        WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos: Fim ")

    End Sub

    Sub EnviaEmail()

        WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail: Inicio ")

        Try
            Dim anexos As Attachment()
            Dim enderecos As String = ""
            Dim rsParam As DataSet = Nothing
            Dim rsDados As DataSet = Nothing
            Dim nomeArq As String
            Dim Mail As New MailMessage
            Dim smtp As New SmtpClient()


            Dim Con As New Conexao_sql
            Con.Conectar()

            rsParam = Con.ExecutarQuery("SELECT MSG_EMAIL_NFE, EMAIL_COPIA_NFE, EMAIL_REMETENTE, END_SMTP, SENHA_REMETENTE, DOMINIO_REMETENTE, EXIGE_SSL, PORTA_SMTP, DIR_EMAIL_GER AS DIR_EMAIL   FROM TB_PARAMETROS ")

            If rsParam.Tables(0).Rows.Count > 0 Then

                rsDados = Con.ExecutarQuery(" SELECT A.ID_FATURAMENTO,A.NR_NOTA_FISCAL,A.VL_NOTA,A.COD_VER_NFSE,A.DT_NOTA_FISCAL,A.NM_CLIENTE,A.CNPJ,B.EMAIL_NF_ELETRONICA 
FROM TB_FATURAMENTO A
INNER JOIN TB_PARCEIRO B ON A.ID_PARCEIRO_CLIENTE = B.ID_PARCEIRO
WHERE A.ID_STATUS_DOWNLOAD_NFE <> 0 AND A.FL_STATUS_EMAIL_NFE = 0 AND B.EMAIL_NF_ELETRONICA IS NOT NULL")

                If rsParam.Tables(0).Rows.Count > 0 Then

                    For Each linhads As DataRow In rsDados.Tables(0).Rows

                        Dim msg As String = rsParam.Tables(0).Rows(0)("MSG_EMAIL_NFE").ToString

                        msg = msg.Replace("@razao", linhads.Item("NM_CLIENTE").ToString)
                        msg = msg.Replace("@nf", linhads.Item("NR_NOTA_FISCAL").ToString)
                        msg = msg.Replace("@codigo", linhads.Item("COD_VER_NFSE").ToString)
                        msg = msg.Replace("@valor", linhads.Item("VL_NOTA").ToString)
                        msg = msg.Replace("@emissao", linhads.Item("DT_NOTA_FISCAL").ToString)
                        msg = msg.Replace("@cnpj", linhads.Item("CNPJ").ToString)

                        Mail = New MailMessage
                        Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                        Try
                            Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                        Catch ex As Exception
                            WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail Erro: Endereço de envio dos e-mails inválido [" & rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString & "]")
                            WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail Erro: " & ex.ToString)
                        End Try


                        Try
                            smtp = New SmtpClient(rsParam.Tables(0).Rows(0)("END_SMTP").ToString)
                            If rsParam.Tables(0).Rows(0)("EXIGE_SSL").ToString = "1" Then
                                smtp.EnableSsl = True
                            Else
                                smtp.EnableSsl = False
                            End If
                            smtp.Credentials = New System.Net.NetworkCredential(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString, rsParam.Tables(0).Rows(0)("SENHA_REMETENTE").ToString, rsParam.Tables(0).Rows(0)("DOMINIO_REMETENTE").ToString)
                            smtp.Port = rsParam.Tables(0).Rows(0)("PORTA_SMTP").ToString
                        Catch ex As Exception
                            WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail Erro: Configurações de envio de e-mail inválidas, contate o suporte!")
                            WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail Erro: " & ex.ToString)

                        End Try


                        'ASSUNTO
                        Mail.Subject = "Email NFS-e - GlobalSys"


                        'CORPO
                        Mail.Body = msg
                        Mail.IsBodyHtml = True


                        'DESTINATARIO
                        enderecos = linhads.Item("EMAIL_NF_ELETRONICA").ToString
                        Dim palavras As String() = enderecos.Split(New String() _
                  {";"}, StringSplitOptions.RemoveEmptyEntries)

                        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
                            Mail.To.Add(palavras(i).ToString)
                        Next


                        If rsParam.Tables(0).Rows(0)("EMAIL_COPIA_NFE").ToString <> "" Then
                            enderecos = rsParam.Tables(0).Rows(0)("EMAIL_COPIA_NFE").ToString
                            palavras = enderecos.Split(New String() _
              {";"}, StringSplitOptions.RemoveEmptyEntries)

                            'exibe o resultado
                            For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
                                Mail.Bcc.Add(palavras(i).ToString)
                            Next
                        End If


                        Dim Ano As String = Now.Year
                        Dim Mes As String = Now.Month
                        Dim Dia As String = Now.Day

                        If Dia.Length = 1 Then
                            Dia = 0 & Dia
                        End If

                        If Mes.Length = 1 Then
                            Mes = 0 & Mes
                        End If


                        nomeArq = "E:\PDF_NFe\PDF\" & Ano & "\" & Mes & "\" & Dia & "\Nota_Fiscal_" & linhads.Item("NR_NOTA_FISCAL").ToString & "_" & linhads.Item("COD_VER_NFSE").ToString & ".pdf"

                        If File.Exists(nomeArq) Then
                            Dim anexo As New Attachment(nomeArq)
                            Mail.Attachments.Add(anexo)


                        End If

                        Try


                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                            smtp.Send(Mail)

                            smtp.Dispose()

                            Con.ExecutarQuery("UPDATE TB_FATURAMENTO SET FL_STATUS_EMAIL_NFE = 1 WHERE ID_FATURAMENTO = " & linhads.Item("ID_FATURAMENTO").ToString)

                        Catch ex As Exception
                            Err.Clear()
                            WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail Erro" & ex.ToString)

                        End Try
                    Next

                End If

            Else
                WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail Erro: Não foi possível acessar As configurações para envio de e-mails, contate o suporte!")
            End If


        Catch ex As Exception

            WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail EnviaEmail Erro: " & ex.ToString)

        End Try

        WriteToFile($"{DateTime.Now.ToString()} - EnviaEmail: Fim ")

    End Sub


    Public Sub WriteToFile(strToWrite As String)
        Try
            Dim Stream As IO.StreamWriter = Nothing

            Dim NomeStream As String
            NomeStream = Microsoft.VisualBasic.Format(Now, "yyyyMMdd_hh_") & "SrvRetornoNF.log"
            Stream = New IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory & "\log\" & NomeStream, True)
            Stream.WriteLine(strToWrite)
            Stream.Flush()
            Stream.Close()

            'limpa diretorio
            Dim di As System.IO.DirectoryInfo = New DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory & "\log")
            For Each file As FileInfo In di.GetFiles()
                If file.CreationTime < DateTime.Now.AddDays(-7) Then
                    file.Delete()
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
End Module