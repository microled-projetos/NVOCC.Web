Public Class TaxasLocaisExpo_PDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        Dim Con As New Conexao_sql
        Con.Conectar()
        '<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
        Dim titulo As String = ""
        Dim dsTitulo As DataSet = Con.ExecutarQuery("SELECT DISTINCT ID_TRANSPORTADOR,(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_TRANSPORTADOR)NM_RAZAO
from TB_TAXA_LOCAL_TRANSPORTADOR A WHERE ID_TIPO_COMEX = 2 ORDER BY ID_TRANSPORTADOR")

        For Each linhaTitulo As DataRow In dsTitulo.Tables(0).Rows
            If titulo <> "" Then
                titulo = "<div style='break-after:page'></div><div style='text-align:center;position:center'><h1>" & linhaTitulo.Item("NM_RAZAO").ToString() & "</h1></div><br/><br/>"
            Else
                titulo = "<div style='text-align:center'><h1>" & linhaTitulo.Item("NM_RAZAO").ToString() & "</h1></div><br/><br/>"
            End If
            divConteudoDinamico.InnerHtml &= titulo



            Dim tabela As String = ""
            Dim ds1 As DataSet = Con.ExecutarQuery("SELECT DISTINCT ID_PORTO,(SELECT NM_PORTO FROM TB_PORTO WHERE ID_PORTO = A.ID_PORTO)NM_PORTO
from TB_TAXA_LOCAL_TRANSPORTADOR A WHERE ID_TIPO_COMEX = 2 and ID_TRANSPORTADOR = " & linhaTitulo.Item("ID_TRANSPORTADOR") & " ORDER BY ID_PORTO")
            For Each linha1 As DataRow In ds1.Tables(0).Rows
                tabela = "<div Class='porto panel panel-default'><div class='panel-heading'><div class='titulo panel-title' style='text-align:center;background-color:#bddea0'>" & linha1.Item("NM_PORTO").ToString() & "</div></div><table border='1' class='subtotal table table-bordered'>"
                Dim ID_PORTO As Integer = linha1.Item("ID_PORTO").ToString()

                Dim ds2 As DataSet = Con.ExecutarQuery("SELECT ID_PORTO, NM_ITEM_DESPESA, VL_TAXA_LOCAL_COMPRA from TB_TAXA_LOCAL_TRANSPORTADOR A
Left Join TB_ITEM_DESPESA B ON A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA  WHERE ID_PORTO = " & ID_PORTO & " AND ID_TRANSPORTADOR = " & linhaTitulo.Item("ID_TRANSPORTADOR") & " and ID_TIPO_COMEX = 2  ")

                For Each linha2 As DataRow In ds2.Tables(0).Rows
                    tabela &= "<tr><td><span class='qtd'>" & linha2("NM_ITEM_DESPESA") & "</span></td>"
                    tabela &= "<td><span class='valor'>" & linha2("VL_TAXA_LOCAL_COMPRA") & "</span></td></tr>"
                Next

                tabela &= "</table></div>"
                divConteudoDinamico.InnerHtml &= tabela

            Next



        Next
        Con.Fechar()
    End Sub

End Class