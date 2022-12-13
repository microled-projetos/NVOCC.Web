Imports System.IO

Public Class VisualizarUpload
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" Then
            Dim CAMINHO_ARQUIVO As String = ""
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT CAMINHO_ARQUIVO, NM_ARQUIVO FROM TB_UPLOADS WHERE ID_ARQUIVO = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CAMINHO_ARQUIVO")) Then
                    CAMINHO_ARQUIVO = ds.Tables(0).Rows(0).Item("CAMINHO_ARQUIVO")
                End If
            End If

            Dim diretorio_arquivos As String = Server.MapPath("/Content/temp/" & Request.QueryString("id") & "/")

            If Not Directory.Exists(diretorio_arquivos) Then
                System.IO.Directory.CreateDirectory(diretorio_arquivos)
            End If

            Dim di As System.IO.DirectoryInfo = New DirectoryInfo(Server.MapPath("/Content/temp/" & Request.QueryString("id") & "/"))

            For Each file As FileInfo In di.GetFiles()
                If file.LastAccessTime < DateTime.Now.AddDays(-1) Then
                    file.Delete()
                End If
            Next

            If File.Exists(CAMINHO_ARQUIVO) = True Then
                If File.Exists(Server.MapPath("~/Content/temp/" & ds.Tables(0).Rows(0).Item("NM_ARQUIVO"))) = False Then
                    File.Copy(CAMINHO_ARQUIVO, Server.MapPath("~/Content/temp/" & Request.QueryString("id") & "/" & ds.Tables(0).Rows(0).Item("NM_ARQUIVO")))
                End If
                txtArquivoSelecionado.Text = "/Content/temp/" & Request.QueryString("id") & "/" & ds.Tables(0).Rows(0).Item("NM_ARQUIVO")
                If ds.Tables(0).Rows(0).Item("NM_ARQUIVO").IndexOf(".msg") > 1 Then

                    Response.ContentType = ContentType
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(CAMINHO_ARQUIVO))
                    Response.WriteFile(CAMINHO_ARQUIVO)
                    Response.Flush()
                    Response.Close()

                Else
                    Response.Redirect(txtArquivoSelecionado.Text)

                End If
            End If
        End If
    End Sub

End Class