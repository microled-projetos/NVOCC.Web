Public Class FollowUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
            CarregaCampos()
            CarregaCampos3()


            ds = Con.ExecutarQuery("SELECT NR_BL FROM [TB_BL] WHERE ID_BL = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 Then
                NumeroBL.Text = " - " & ds.Tables(0).Rows(0).Item("NR_BL")
            End If

        End If


        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    Sub CarregaCampos()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("EXEC [dbo].[PROC_FOLLOWUP]" & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then

            Dim Lista As String = "<div class='row'>
          <div class='panel panel-primary'>
                <div class='panel-heading'>
                    <h3 class='panel-title'>FOLLOWUP
                    </h3>
                </div>
                <div class='panel-body'><ul class='DB_Timeline'>
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


    Sub CarregaCampos3()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("EXEC [dbo].[PROC_FOLLOWUP]" & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then

            Dim Lista As String = "<div class='page'>
  <div class='timeline'>
    <div class='timeline__group'>"

            For Each linha As DataRow In ds.Tables(0).Rows
                If linha("STATUS_ETAPA") = "ETAPA REALIZADA" Then
                    Lista &= "<span class='timeline__year time' aria-hidden='true'>" & linha("NM_EVENTO") & "</span>
      <div class='timeline__cards'>
        <div class='timeline__card card'>
          <header class='card__header'>
<time class='time' datetime='2008-07-14' style='background-color: #d3e8d1;'>
              <span class='time__day' style='color:green;'><strong>" & linha("STATUS_ETAPA") & "</strong><i class='glyphicon glyphicon-ok'></i></span>
            </time>
          </header>
          <div class='card__content'>
            <p>" & linha("VALOR_CAMPO") & "<br/>
</p>
        </div>
       
        </div>
      </div>"

                Else
                    Lista &= "<span class='timeline__year time' aria-hidden='true'>" & linha("NM_EVENTO") & "</span>
      <div class='timeline__cards'>
        <div class='timeline__card card'>
          <header class='card__header'>
<time class='time' datetime='2008-07-14' style='background-color: #e8d1d1;'>
              <span class='time__day' style='color:red;'><strong>" & linha("STATUS_ETAPA") & "</strong><i class='glyphicon glyphicon-remove'></i></span>
            </time>
          </header>
          <div class='card__content'></div>
       
        </div>
      </div>"

                End If
            Next

            Lista &= "</div></div>
</div>"
            teste2.InnerHtml &= Lista


        End If

    End Sub
End Class