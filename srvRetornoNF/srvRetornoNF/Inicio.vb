Imports System.Data.SqlClient
Imports System.Configuration

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

            Inicio.WriteToFile($"{DateTime.Now.ToString()} - RetornoNF: linha 34 ")
            ' Con.ExecutarQuery("EXEC [dbo].[Proc_Comissoes_Nacional_Totvs]")

            FlagExecutando = True

        Catch ex As Exception
            WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)

            FlagExecutando = False

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
