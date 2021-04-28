Public Class FollowUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            CarregaCampos()
        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    Sub CarregaCampos()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT * FROM View_FollowUp")
        If ds.Tables(0).Rows.Count > 0 Then

            Dim Lista As String = "<ul class='DB_Timeline'>"

            '<i class='glyphicon glyphicon-ok'></i> <i class='glyphicon glyphicon-remove'></i>
            For Each linha As DataRow In ds.Tables(0).Rows
                If linha("STATUS_ETAPA") = "ETAPA REALIZADA" Then
                    Lista &= "<li style='color:green;list-style:none;font-size:15px;padding-bottom:20px'><i class='glyphicon glyphicon-ok'></i>" & linha("NM_EVENTO") & "<br/><div style='font-size:12px' class='DB_Timeline__text'>" & linha("STATUS_ETAPA") & "</div>
</li>"

                Else
                    Lista &= "<li style='color:red;list-style:none;font-size:15px;padding-bottom:20px'><i class='glyphicon glyphicon-remove'></i>" & linha("NM_EVENTO") & "<br/><div style='font-size:12px'  class='DB_Timeline__text'>" & linha("STATUS_ETAPA") & "</div>
</li>"

                End If
            Next

            Lista &= "</ul>"
            divConteudoDinamico.InnerHtml &= Lista


        End If

    End Sub


End Class