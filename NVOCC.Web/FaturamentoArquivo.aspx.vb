Imports System.IO
Public Class FaturamentoArquivo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2028 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
        End If

        If Request.QueryString("id") <> "" Then
            Dim ID_FATURAMENTO As String = Request.QueryString("id")
            Dim diretorio_arquivos As String = Server.MapPath("~/Content/Arquivos/Faturamento") & ID_FATURAMENTO

            Dim di As System.IO.DirectoryInfo = New DirectoryInfo(diretorio_arquivos)
            For Each file As FileInfo In di.GetFiles()

                Dim _file As System.IO.FileInfo = New System.IO.FileInfo(Server.MapPath("/Content/Arquivos/Faturamento") & ID_FATURAMENTO & "/" & file.ToString)
                If _file.Exists Then
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & _file.Name)
                    Response.AddHeader("Content-Length", _file.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(Server.UrlDecode(_file.FullName))
                    Response.Flush()
                    Response.Close()
                End If


            Next
        End If

    End Sub

End Class