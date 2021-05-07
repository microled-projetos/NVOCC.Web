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
            txtID_BL.Text = Request.QueryString("id")
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
    Private Sub gdvFollowUp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdvFollowUp.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim img As Image = CType(e.Row.FindControl("image1"), Image)

            Select Case e.Row.Cells(5).Text

                Case "ETAPA REALIZADA"
                    img.ImageUrl = "Content/imagens/159e1b.png"
                    img.Visible = True

                Case "ETAPA PENDENTE"

                    img.ImageUrl = "Content/imagens/e72c17.png"
                    img.Visible = True

                Case "ETAPA INEXISTENTE PARA O TIPO DE BL"
                    img.ImageUrl = "Content/imagens/e7d617.png"
                    img.Visible = True
                    e.Row.Visible = False

            End Select


        End If


        'Dim dataAnterior As Date '= 'Now.Date.ToString("dd-MM-yyyy")

        'For Each linha As GridViewRow In gdvFollowUp.Rows

        '    If CType(linha.FindControl("lbldata"), Label).Text <> "" Then
        '        Dim dataAtual As Date = CType(linha.FindControl("lbldata"), Label).Text

        '        If dataAnterior <> dataAtual Then
        '            CType(linha.FindControl("lblteste"), Label).Text = DateDiff("d", dataAnterior, dataAtual)
        '            dataAnterior = dataAtual
        '        End If
        '    End If

        'Next

    End Sub

    Private Sub gdvFollowUp_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvFollowUp.RowCommand
        DetalhesBL.InnerHtml = ""
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        Dim tabela As String = ""
        If e.CommandName = "Detalhes" Then
            Dim ID As String = e.CommandArgument
            ds = Con.ExecutarQuery("SELECT TOP 1 GRAU,ID_BL_MASTER FROM TB_BL A LEFT JOIN LOG_FOLLOWUP B ON B.CHAVE = A.ID_BL 
WHERE GRAU IS NOT NULL AND ID_BL =" & txtID_BL.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("GRAU") = "C" Then
                    lblTituloDetalhe.Text = "<center><h4>Master de vinculo do House</h4></center>"
                    Dim ID_BL_MASTER As Integer = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    ds = Con.ExecutarQuery("
SELECT NR_BL, NR_PROCESSO,CONVERT(VARCHAR,DT_ABERTURA,103)DT_ABERTURA FROM TB_BL WHERE ID_BL = " & ID_BL_MASTER)
                    tabela = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
                    tabela &= "<th style='padding-right:10px'>MBL</th>"
                    tabela &= "<th style='padding-right:10px'>PROCESSO</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>ABERTURA</th></tr>"

                    For Each linha As DataRow In ds.Tables(0).Rows
                        tabela &= "<tr><td style='padding-right:10px'>" & linha("NR_BL") & "</td>"
                        tabela &= "<td style='padding-right:10px'>" & linha("NR_PROCESSO") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("DT_ABERTURA") & "</td></tr>"
                    Next

                    tabela &= "</table>"
                    DetalhesBL.InnerHtml &= tabela
                    mpeDetalhes.Show()

                Else
                    lblTituloDetalhe.Text = "<center><h4>Houses vinculados ao Master</h4></center>"

                    Dim ID_BL_MASTER As Integer = txtID_BL.Text
                    ds = Con.ExecutarQuery("
SELECT NR_BL, NR_PROCESSO,CONVERT(VARCHAR,DT_ABERTURA,103)DT_ABERTURA FROM TB_BL WHERE ID_BL_MASTER = " & ID_BL_MASTER)
                    tabela = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px;'><tr>"
                    tabela &= "<th style='padding-right:10px'>NUMERO HBL</th>"
                    tabela &= "<th style='padding-right:10px'>NUMERO DO PROCESSO</th>"
                    tabela &= "<th style='padding-right:10px'>DATA DE ABERTURA</th></tr>"

                    For Each linha As DataRow In ds.Tables(0).Rows
                        tabela &= "<tr><td style='padding-right:10px'>" & linha("NR_BL") & "</td>"
                        tabela &= "<td style='padding-right:10px'>" & linha("NR_PROCESSO") & "</td>"
                        tabela &= "<td style='padding-right:10px'>" & linha("DT_ABERTURA") & "</td></tr>"
                    Next

                    tabela &= "</table>"
                    DetalhesBL.InnerHtml &= tabela
                    mpeDetalhes.Show()
                End If
            End If
        End If

    End Sub





End Class