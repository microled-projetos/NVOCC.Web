Public Class FollowUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            CarregaCampos()
            CarregaCampos2()
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

            Dim Lista As String = "<div class='row'>
          <div class='panel panel-primary'>
                <div class='panel-heading'>
                    <h3 class='panel-title'>FOLLOWUP
                    </h3>
                </div>
                <div class='panel-body'><ul class='DB_Timeline'><div class='marcador'></div>
"

            For Each linha As DataRow In ds.Tables(0).Rows
                If linha("STATUS_ETAPA") = "ETAPA REALIZADA" Then
                    Lista &= "<li runat='server' ID='" & linha("ID_FOLLOWUP") & "'  style='color:green;list-style:none;font-size:12px;padding-bottom:20px'><strong>" & linha("NM_EVENTO") & "&nbsp;&nbsp</strong><i class='glyphicon glyphicon-ok'></i><br/><div style='font-size:10px;font-color:black !important' class='DB_Timeline__text'>STATUS:" & linha("STATUS_ETAPA") & "<br/>DATA:11/10/2020<br/>USUÁRIO: JULIANE</div>
</li>"

                Else
                    Lista &= "<li runat='server' ID='" & linha("ID_FOLLOWUP") & "' style='color:red;list-style:none;font-size:12px;padding-bottom:20px'><strong>" & linha("NM_EVENTO") & " &nbsp;&nbsp</strong><i class='glyphicon glyphicon-remove'></i><br/><div style='font-size:10px'  class='DB_Timeline__text'> STATUS:" & linha("STATUS_ETAPA") & "</div>
</li>"

                End If
            Next

            Lista &= "</ul>  </div>
              </div>
         </div>"
            divConteudoDinamico.InnerHtml &= Lista


        End If

    End Sub

    Sub CarregaCampos2()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT * FROM View_FollowUp")
        If ds.Tables(0).Rows.Count > 0 Then

            Dim Lista As String = "<ul class='linha-do-tempo'><div class='marcador'></div>
"

            For Each linha As DataRow In ds.Tables(0).Rows
                If linha("STATUS_ETAPA") = "ETAPA REALIZADA" Then
                    Lista &= "<li runat='server' ID='" & linha("ID_FOLLOWUP") & "' class='Evento'><p>" & linha("NM_EVENTO") & "</p></li>
 <li class='item' href='#'><p style='color:green'>STATUS:" & linha("STATUS_ETAPA") & " <i class='glyphicon glyphicon-ok'></i></p></li>
<li class='item' href='#'>
<p>DATA:11/10/2020<br/>
USUÁRIO: JULIANE</p> 
</li>"

                Else
                    Lista &= "<li runat='server' ID='" & linha("ID_FOLLOWUP") & "' class='Evento'><p>" & linha("NM_EVENTO") & "</p></li>
 <li class='item' href='#'><p style='color:red'>STATUS:" & linha("STATUS_ETAPA") & " <i class='glyphicon glyphicon-remove'></i></p></li>
<li class='item' href='#'>
<p>SEM INFORMAÇÃO</p> 
</li>"

                End If
            Next

            Lista &= "</ul>"
            TESTE.InnerHtml &= Lista


        End If

    End Sub



End Class