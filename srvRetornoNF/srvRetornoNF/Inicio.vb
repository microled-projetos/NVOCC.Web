Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Net.Mail

Module Inicio

    Public FlagExecutando As Boolean



    Public Sub RetornoNF()

        Try
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 10 ")

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_FATURAMENTO FROM TB_FATURAMENTO WHERE ISNULL(NR_RPS,0) <> 0 AND ISNULL(NR_LOTE,0) <> 0 AND ISNULL(STATUS_NFE,0) = 4 AND ISNULL(CANCELA_NFE,0) = 0 AND NR_NOTA_FISCAL IS NULL ")
            If ds.Tables(0).Rows.Count > 0 Then

                Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 17 ")

                For Each linhads As DataRow In ds.Tables(0).Rows

                    Using ConsultaNF = New WsNVOCC.WsNvocc

                        Dim consulta = ConsultaNF.ConsultaNFePrefeitura(linhads.Item("ID_FATURAMENTO").ToString(), 1, "SQL", "NVOCC")

                        WriteToFile($"{DateTime.Now.ToString()} - ID_FATURAMENTO: " & linhads.Item("ID_FATURAMENTO").ToString())

                    End Using

                    Con.ExecutarQuery("UPDATE TB_FATURAMENTO SET FL_SRV_RETORNO_NF =  1 WHERE ID_FATURAMENTO =  " & linhads.Item("ID_FATURAMENTO").ToString())
                Next
                Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 30 ")

            End If



            ''ROTINA QUE ATUALIZA DATA DA BAIXA TOTVS NAS COMISSOES NACIONAIS - CHAMADO 3505
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 42 - Proc_Comissoes_Nacional_Totvs ")
            Con.ExecutarQuery("EXEC [dbo].[Proc_Comissoes_Nacional_Totvs]")


            ''ROTINA QUE DELETA ARQUIVOC DE UPLOAD DO GLOBAL SYS - CHAMADO 33531  
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 47 - DeletaArquivos ")
            DeletaArquivos()


            FlagExecutando = True

        Catch ex As Exception
            WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)

            FlagExecutando = False

            ' processaFila(ConfigurationManager.AppSettings("Email").ToString(), "Erro no srvRetornoNF - NVOCC", ex.ToString)

        End Try



    End Sub

    Sub DeletaArquivos()
        Dim Con As New Conexao_sql
        Con.Conectar()

        WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos: linha 71 ")
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_ARQUIVO,CAMINHO_ARQUIVO FROM TB_UPLOADS A
INNER JOIN TB_TIPO_ARQUIVO B ON A.ID_TIPO_ARQUIVO = B.ID_TIPO_ARQUIVO
WHERE B.FL_EXPIRA = 1 AND DATEDIFF( DAY , DT_UPLOAD,GETDATE()) >= (SELECT QT_DIAS_EXPURGO FROM TB_PARAMETROS)")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Con.ExecutarQuery("DELETE FROM TB_UPLOADS WHERE ID_ARQUIVO = " & linha.Item("ID_ARQUIVO"))
                File.Delete(linha.Item("CAMINHO_ARQUIVO"))
            Next
            WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos: linha 80 ")

        End If
        Con.Fechar()

        WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos: linha 85 ")
        Dim CaminhoTemp = ConfigurationManager.AppSettings("CaminhoTemp").ToString()
        Dim di As DirectoryInfo = New DirectoryInfo(CaminhoTemp)

        WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos: linha 89 ")

        For Each pastas As DirectoryInfo In di.GetDirectories
            If pastas.LastAccessTime < DateTime.Now.AddDays(-1) Then
                For Each file As FileInfo In pastas.GetFiles()
                    file.Delete()
                Next
                pastas.Delete()
            End If
            WriteToFile($"{DateTime.Now.ToString()} - DeletaArquivos: linha 98 ")
        Next


    End Sub



    Sub processaFila(email As String, assunto As String, msg As String)
        Dim sSql As String
        Dim anexos As Attachment()
        Dim critica As String = ""
        Dim enderecos As String = ""
        Dim rsParam As DataSet = Nothing
        Dim indExc As Long
        Dim nomeArq As String
        Dim validaEnd As String
        Dim ends() As String
        Dim Mail As New MailMessage
        Dim smtp As New SmtpClient()

        Try
            Dim Con As New Conexao_sql
            Con.Conectar()

            sSql = "SELECT EMAIL_REMETENTE, END_SMTP, SENHA_REMETENTE, DOMINIO_REMETENTE, EXIGE_SSL, PORTA_SMTP, DIR_EMAIL_GER AS DIR_EMAIL "
            sSql = sSql & " FROM TB_PARAMETROS "
            rsParam = Con.ExecutarQuery(sSql)

            If rsParam.Tables(0).Rows.Count > 0 Then


                Mail = New MailMessage
                Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Try
                    Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Catch ex As Exception
                    critica = "Endereço de envio dos e-mails inválido [" & rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString & "] "
                    WriteToFile($"{DateTime.Now.ToString()} - Erro: " & critica.ToString)
                    WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)
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
                    critica = "Configurações de envio de e-mail inválidas, contate o suporte!" & Err.Description
                    WriteToFile($"{DateTime.Now.ToString()} - Erro: " & critica.ToString)
                    WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)

                End Try


                'ASSUNTO
                Mail.Subject = assunto


                'CORPO
                Mail.Body = msg
                Mail.IsBodyHtml = True


                'DESTINATARIO
                enderecos = email
                Dim palavras As String() = enderecos.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

                For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
                    Mail.To.Add(palavras(i).ToString)

                Next

                Try

                    smtp.Send(Mail)

                    smtp.Dispose()

                Catch ex As Exception
                    critica = "Ocorreu um erro ao enviar o e-mail! Erro:  " & Err.Description
                    Err.Clear()
                    WriteToFile($"{DateTime.Now.ToString()} - Erro: " & critica.ToString)
                    WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)

                End Try

            Else
                critica = "Não foi possível acessar As configurações para envio de e-mails, contate o suporte!"
                WriteToFile($"{DateTime.Now.ToString()} - Erro: " & critica.ToString)

            End If

        Catch ex As Exception

            critica = "Ocorreu um erro ao realizar o envio de e-mails, contate o suporte!" & vbCrLf & "Erro:  " & Err.Description
            WriteToFile($"{DateTime.Now.ToString()} - Erro: " & critica.ToString)
            WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)

        End Try



    End Sub



    Public Sub WriteToFile(strToWrite As String)
        Dim Stream As IO.StreamWriter = Nothing
        Try
            Dim NomeStream As String
            NomeStream = Microsoft.VisualBasic.Format(Now, "yyyyMMdd_hh_") & "SrvRetornoNF.log"
            Stream = New IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory & "\log\" & NomeStream, True)
            Stream.WriteLine(strToWrite)
            Stream.Flush()
            Stream.Close()
        Catch ex As Exception

        End Try
    End Sub
End Module