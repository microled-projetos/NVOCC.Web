Imports System.IO
Public Class RotinaDeletaDoc

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

End Class
