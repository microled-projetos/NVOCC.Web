Imports System.Data
Imports System.Data.SqlClient
Public Class EmailService
    Public Function EnviarEmail(ByVal toName As String, ByVal subject As String, ByVal message As String, Optional ByVal ccName As String = "")

        Dim Con As New Conexao_sql
        Con.Conectar()

        If ccName = "" Then
            ccName = "NULL"
        Else
            ccName = "'" & ccName & "'"
        End If

        Dim Comando As String = "EXECUTE [NVOCC].[dbo].[PROC_EMAIL]"
        Comando &= " @from_name = 'FCA-Log' ,"
        Comando &= " @to_names  = '" & toName & "',"
        Comando &= " @subject  = '" & subject & "',"
        Comando &= " @cc_names  = " & ccName & ","
        Comando &= " @bcc_names  = NULL ,"
        Comando &= " @message = NULL ,"
        Comando &= " @html_message = '" & message & "' ,"
        Comando &= " @file_attachments  = NULL ,"
        Comando &= " @ERROCODE = NULL "



        Dim msg As String = ""
        Try
            Con.ExecutarQuery(Comando)
            Con.Fechar()
            msg = "Email enviado com sucesso!"
            Return (msg)
        Catch ex As Exception
            '  Throw ex
            msg = ""
            Return (msg)

        End Try
    End Function
End Class


