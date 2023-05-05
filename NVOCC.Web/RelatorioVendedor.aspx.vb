Public Class RelatorioVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2029 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            Dim COMPETENCIA As String = Request.QueryString("c")

            If Request.QueryString("tipo") = 1 Then

                Dim ID As String = Request.QueryString("id")
                ds = Con.ExecutarQuery("SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE ID_PARCEIRO_VENDEDOR = (SELECT ID_PARCEIRO_VENDEDOR FROM TB_DETALHE_COMISSAO_VENDEDOR WHERE ID_DETALHE_COMISSAO_VENDEDOR = " & ID & ") AND COMPETENCIA = '" & COMPETENCIA & "'")
                Dim valores As Decimal = 0

                If ds.Tables(0).Rows.Count > 0 Then

                    Dim tabela As String = "<div style='padding:20px'><p>OLÁ, " & ds.Tables(0).Rows(0).Item("PARCEIRO_VENDEDOR").ToString() & "</p>
<p>SEGUE SEU RELATÓRIO DE PREMIAÇÕES REFERENTE AO MÊS " & ds.Tables(0).Rows(0).Item("COMPETENCIA").ToString() & "</p></div><br/><table style='font-family:Arial;font-size:10px;'><tr>"
                    tabela &= "<th style='padding-right:10px'>PROCESSO</th>"
                    tabela &= "<th style='padding-left:10px;padding-right:10px'>NOTA FISCAL</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>DATA NF</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>IMP/EXP</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>VIA</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>CLIENTE</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>ESTUFAGEM</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO BASE</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>QTD. BL/CNTR</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>%</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO TOTAL</th></tr>"

                    For Each linha As DataRow In ds.Tables(0).Rows
                        tabela &= "<tr><td style='padding-right:10px'>" & linha("NR_PROCESSO") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("NR_NOTAS_FISCAL") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("DT_NOTA_FISCAL") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_SERVICO") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_VIA") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("PARCEIRO_CLIENTE") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TIPO_ESTUFAGEM") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_BASE") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("QTD. BL/CNTR") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_PERCENTUAL") & "</td>"
                        tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_TOTAL") & "</td></tr>"

                        valores = valores + linha("VL_COMISSAO_TOTAL")


                    Next
                    tabela &= "<tr>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'>Total: " & valores & "</td>
</tr>"
                    tabela &= "</table>"
                    divConteudoDinamico.InnerHtml = tabela
                End If




                Con.Fechar()
            ElseIf Request.QueryString("tipo") = 2 Then
                Dim tabela As String = ""

                Dim dsVendedores As DataSet = Con.ExecutarQuery("SELECT DISTINCT ID_PARCEIRO_VENDEDOR,PARCEIRO_VENDEDOR FROM [dbo].[View_Comissao_Vendedor] WHERE COMPETENCIA = '" & COMPETENCIA & "'")

                If dsVendedores.Tables(0).Rows.Count > 0 Then
                    For Each linhaVendedor As DataRow In dsVendedores.Tables(0).Rows
                        ds = Con.ExecutarQuery("SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE ID_PARCEIRO_VENDEDOR = " & linhaVendedor("ID_PARCEIRO_VENDEDOR").ToString() & " AND COMPETENCIA = '" & COMPETENCIA & "'")
                        Dim valores As Double = 0
                        If ds.Tables(0).Rows.Count > 0 Then

                            tabela &= "<div style='padding-top:20px'><p>OLÁ," & ds.Tables(0).Rows(0).Item("PARCEIRO_VENDEDOR").ToString() & "</p><p>SEGUE SEU RELATÓRIO DE PREMIAÇÕES REFERENTE A " & ds.Tables(0).Rows(0).Item("COMPETENCIA").ToString() & "</p></div><br/><table style='font-family:Arial;font-size:10px;'><tr>"
                            tabela &= "<th style='padding-right:10px'>PROCESSO</th>"
                            tabela &= "<th style='padding-left:10px;padding-right:10px'>NOTA FISCAL</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>DATA NF</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>IMP/EXP</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>VIA</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>CLIENTE</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>ESTUFAGEM</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO BASE</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>QTD. BL/CNTR</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>%</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO TOTAL</th></tr>"

                            For Each linha As DataRow In ds.Tables(0).Rows
                                tabela &= "<tr><td style='padding-right:10px'>" & linha("NR_PROCESSO") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("NR_NOTAS_FISCAL") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("DT_NOTA_FISCAL") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_SERVICO") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_VIA") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("PARCEIRO_CLIENTE") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TIPO_ESTUFAGEM") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_BASE") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("QTD. BL/CNTR") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_PERCENTUAL") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_TOTAL") & "</td></tr>"

                                valores = valores + linha("VL_COMISSAO_TOTAL")


                            Next

                            tabela &= "<tr>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'>Total: " & valores & "</td>
</tr>"
                            tabela &= "</table><div style='break-after:page'></div>"

                        End If


                        divConteudoDinamico.InnerHtml = tabela

                    Next


                    'Dim dsResumo As DataSet = Con.ExecutarQuery(" SELECT PARCEIRO_VENDEDOR as NOME, sum (VL_COMISSAO_TOTAL)VL_COMISSAO_TOTAL  FROM View_Comissao_Vendedor  WHERE COMPETENCIA = '" & COMPETENCIA & "' group BY PARCEIRO_VENDEDOR  UNION  SELECT USUARIO, VL_COMISSAO as 'VALOR'  FROM View_Equipes  WHERE COMPETENCIA = '" & COMPETENCIA & "' ORDER BY PARCEIRO_VENDEDOR ASC ")
                    Dim dsResumo As DataSet = Con.ExecutarQuery(" SELECT PARCEIRO_VENDEDOR as NOME, sum (VL_COMISSAO_TOTAL)VL_COMISSAO_TOTAL  FROM View_Comissao_Vendedor  WHERE COMPETENCIA = '" & COMPETENCIA & "' group BY PARCEIRO_VENDEDOR ORDER BY PARCEIRO_VENDEDOR ASC ")
                    If dsResumo.Tables(0).Rows.Count > 0 Then
                        tabela = "<div style='padding-top:20px'><p>RESUMO DA COMPETENCIA</p></div>"
                        tabela &= "<table><tr><th class='valor' style='padding-left:10px;padding-right:10px'>NOME</th>"
                        tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO TOTAL</th></tr>"
                        For Each linhaResumo As DataRow In dsResumo.Tables(0).Rows
                            tabela &= "<tr><td style='padding-left:10px;padding-right:10px'>" & linhaResumo("NOME") & "</td>"
                            tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linhaResumo("VL_COMISSAO_TOTAL") & "</td></tr>"
                        Next
                        tabela &= "</table>"
                    End If


                    divResumo.InnerHtml = tabela

                End If




            ElseIf Request.QueryString("tipo") = 3 Then

                Dim tabela As String = ""
                Dim dsVendedores As DataSet = Con.ExecutarQuery("SELECT DISTINCT ID_PARCEIRO_VENDEDOR,PARCEIRO_VENDEDOR,(SELECT EMAIL FROM TB_PARCEIRO WHERE ID_PARCEIRO = ID_PARCEIRO_VENDEDOR)EMAIL FROM [dbo].[View_Comissao_Vendedor] A WHERE COMPETENCIA = '" & COMPETENCIA & "'")

                If dsVendedores.Tables(0).Rows.Count > 0 Then
                    For Each linhaVendedor As DataRow In dsVendedores.Tables(0).Rows
                        ds = Con.ExecutarQuery("SELECT * FROM [dbo].[View_Comissao_Vendedor] WHERE ID_PARCEIRO_VENDEDOR = " & linhaVendedor("ID_PARCEIRO_VENDEDOR").ToString() & " AND COMPETENCIA = '" & COMPETENCIA & "'")
                        Dim Mensagem As String = ""
                        Dim valores As Double = 0
                        If ds.Tables(0).Rows.Count > 0 Then

                            'EXIBE NA TELA
                            tabela &= "<div style='padding-top:20px'><p>OLÁ," & ds.Tables(0).Rows(0).Item("PARCEIRO_VENDEDOR").ToString() & "</p>
<p>SEGUE SEU RELATÓRIO DE PREMIAÇÕES REFERENTE AO MÊS " & ds.Tables(0).Rows(0).Item("COMPETENCIA").ToString() & "</p></div><br/><table style='font-family:Arial;font-size:10px;'><tr>"
                            tabela &= "<th style='padding-right:10px'>PROCESSO</th>"
                            tabela &= "<th style='padding-left:10px;padding-right:10px'>NOTA FISCAL</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>DATA NF</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>IMP/EXP</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>VIA</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>CLIENTE</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>ESTUFAGEM</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO BASE</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>QTD. BL/CNTR</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>%</th>"
                            tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO TOTAL</th></tr>"

                            For Each linha As DataRow In ds.Tables(0).Rows
                                tabela &= "<tr><td style='padding-right:10px'>" & linha("NR_PROCESSO") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("NR_NOTAS_FISCAL") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("DT_NOTA_FISCAL") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_SERVICO") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_VIA") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("PARCEIRO_CLIENTE") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TIPO_ESTUFAGEM") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_BASE") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("QTD. BL/CNTR") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_PERCENTUAL") & "</td>"
                                tabela &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_TOTAL") & "</td></tr>"

                                valores = valores + linha("VL_COMISSAO_TOTAL")


                            Next
                            tabela &= "<tr>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'>Total: " & valores & "</td>
</tr>"
                            tabela &= "</table><div style='break-after:page'></div>"

                            valores = 0

                            'ENVIA EMAIL
                            Mensagem &= "<div style='padding-top:20px'><p>OLÁ," & ds.Tables(0).Rows(0).Item("PARCEIRO_VENDEDOR").ToString() & "</p>
<p>SEGUE SEU RELATÓRIO DE PREMIAÇÕES REFERENTE AO MÊS " & ds.Tables(0).Rows(0).Item("COMPETENCIA").ToString() & "</p></div><br/><table style='font-family:Arial;font-size:10px;'><tr>"
                            Mensagem &= "<th style='padding-right:10px'>PROCESSO</th>"
                            Mensagem &= "<th style='padding-left:10px;padding-right:10px'>NOTA FISCAL</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>DATA NF</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>IMP/EXP</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>VIA</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>CLIENTE</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>ESTUFAGEM</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO BASE</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>QTD. BL/CNTR</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>%</th>"
                            Mensagem &= "<th class='valor' style='padding-left:10px;padding-right:10px'>COMISSAO TOTAL</th></tr>"

                            For Each linha As DataRow In ds.Tables(0).Rows
                                Mensagem &= "<tr><td style='padding-right:10px'>" & linha("NR_PROCESSO") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("NR_NOTAS_FISCAL") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("DT_NOTA_FISCAL") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_SERVICO") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TP_VIA") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("PARCEIRO_CLIENTE") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("TIPO_ESTUFAGEM") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_BASE") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("QTD. BL/CNTR") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_PERCENTUAL") & "</td>"
                                Mensagem &= "<td style='padding-left:10px;padding-right:10px'>" & linha("VL_COMISSAO_TOTAL") & "</td></tr>"

                                valores = valores + linha("VL_COMISSAO_TOTAL")


                            Next
                            Mensagem &= "<tr>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'></td>
<td style='padding-left:10px;padding-right:10px'>Total: " & valores & "</td>
</tr>"





                            tabela &= "</table><div style='break-after:page'></div>"
                            If Not IsDBNull(linhaVendedor("EMAIL")) Then
                                Dim email As New EmailService
                                Mensagem = Mensagem.Replace("'", """")
                                Dim retorno As String = email.EnviarEmail(linhaVendedor("EMAIL").ToString(), "RELATÓRIO DE PREMIAÇÕES - NVOCC", Mensagem)
                                If retorno = "" Then
                                    divErro.Visible = True
                                Else
                                    divSuccess.Visible = True
                                    lblmsgSuccess.Text = retorno
                                End If
                            End If
                        End If

                        divConteudoDinamico.InnerHtml = tabela


                    Next
                End If



            End If

        End If
        Con.Fechar()

    End Sub

End Class