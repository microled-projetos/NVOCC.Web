Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Web

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

                Next
                Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 30 ")

            End If



            ''ROTINA QUE ATUALIZA DATA DA BAIXA TOTVS NAS COMISSOES NACIONAIS - CHAMADO 3505
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 42 - Proc_Comissoes_Nacional_Totvs ")
            Con.ExecutarQuery("EXEC [dbo].[Proc_Comissoes_Nacional_Totvs]")


            '''ROTINA QUE DELETA ARQUIVOC DE UPLOAD DO GLOBAL SYS - CHAMADO 33531 
            'Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 47 - DeletaArquivos ")
            'DeletaArquivos()


            FlagExecutando = True

        Catch ex As Exception
            WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)

            FlagExecutando = False

        End Try



    End Sub

    Sub DeletaArquivos()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_ARQUIVO,CAMINHO_ARQUIVO FROM TB_UPLOADS A
INNER JOIN TB_TIPO_ARQUIVO B ON A.ID_TIPO_ARQUIVO = B.ID_TIPO_ARQUIVO
WHERE B.FL_EXPIRA = 1 AND DATEDIFF( DAY , DT_UPLOAD,GETDATE()) >= (SELECT QT_DIAS_EXPURGO FROM TB_PARAMETROS)")

        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Con.ExecutarQuery("DELETE FROM TB_UPLOADS WHERE ID_ARQUIVO = " & linha.Item("ID_ARQUIVO"))
                File.Delete(linha.Item("CAMINHO_ARQUIVO"))
            Next

        End If
        Con.Fechar()

        Dim di As DirectoryInfo = New DirectoryInfo(HttpContext.Current.Server.MapPath("/Content/temp"))
        For Each file As FileInfo In di.GetFiles()
            If file.LastAccessTime < DateTime.Now.AddDays(-1) Then
                file.Delete()
            End If
        Next


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
