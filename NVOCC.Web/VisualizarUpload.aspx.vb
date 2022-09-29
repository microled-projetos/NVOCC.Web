Imports System.IO

Public Class VisualizarUpload
    Inherits System.Web.UI.Page

    Public imagemBase64Retorno As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" Then
            Dim CAMINHO_ARQUIVO As String = ""
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT CAMINHO_ARQUIVO FROM TB_UPLOADS WHERE ID_ARQUIVO = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CAMINHO_ARQUIVO")) Then
                    CAMINHO_ARQUIVO = ds.Tables(0).Rows(0).Item("CAMINHO_ARQUIVO")
                End If
            End If

            If File.Exists(CAMINHO_ARQUIVO) = True Then
                Dim imagemEmBytes = File.ReadAllBytes(CAMINHO_ARQUIVO)
                imagemBase64Retorno = Convert.ToBase64String(imagemEmBytes)
            Else
                imagemBase64Retorno = ""
            End If
        End If
    End Sub

End Class