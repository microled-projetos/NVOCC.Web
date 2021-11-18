Public Class ReciboDemurrage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2034 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            If Request.QueryString("id") <> "" Then
                Con.Conectar()
                Dim CIDADE As String = ""
                Dim ID As String = Request.QueryString("id")
                ds = Con.ExecutarQuery("SELECT (SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = D.ID_PARCEIRO_CLIENTE)NM_CLIENTE,(SELECT ID_CIDADE FROM TB_PARCEIRO WHERE ID_PARCEIRO = D.ID_PARCEIRO_CLIENTE)ID_CIDADE,
SUM(C.VL_DEMURRAGE_VENDA)VL_DEMURRAGE_VENDA, SUM(C.VL_DEMURRAGE_VENDA_BR)VL_DEMURRAGE_VENDA_BR ,
SUM(C.VL_DEMURRAGE_LIQUIDO_VENDA)VL_DEMURRAGE_LIQUIDO_VENDA,
NR_BL,NR_PROCESSO, (SELECT NR_BL FROM TB_BL M WHERE M.ID_BL = D.ID_BL_MASTER)NR_BL_MASTER,(SELECT top 1 NR_REFERENCIA_CLIENTE FROM VW_REFERENCIA_CLIENTE WHERE ID_BL = D.ID_BL_MASTER)NR_REFERENCIA_CLIENTE,A.DT_LANCAMENTO,A.NR_RECIBO
FROM TB_DEMURRAGE_FATURA A
INNER JOIN TB_DEMURRAGE_FATURA_ITENS B ON A.ID_DEMURRAGE_FATURA = B.ID_DEMURRAGE_FATURA
INNER JOIN TB_CNTR_DEMURRAGE C ON B.ID_CNTR_DEMURRAGE = C.ID_CNTR_DEMURRAGE
LEFT  JOIN TB_BL D ON A.ID_BL = D.ID_BL
WHERE A.ID_DEMURRAGE_FATURA = " & ID & "
GROUP BY ID_PARCEIRO_CLIENTE,NR_BL,NR_PROCESSO,ID_BL_MASTER,A.DT_LANCAMENTO,A.NR_RECIBO")
                If ds.Tables(0).Rows.Count > 0 Then


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_LANCAMENTO")) Then
                        lblEmissao.Text = ds.Tables(0).Rows(0).Item("DT_LANCAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_RECIBO")) Then
                        lblNumeroRecibo.Text = ds.Tables(0).Rows(0).Item("NR_RECIBO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CLIENTE")) Then
                        lblEmpresa.Text = ds.Tables(0).Rows(0).Item("NM_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CIDADE")) Then
                        CIDADE = ds.Tables(0).Rows(0).Item("ID_CIDADE")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        lblNRef.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                        lblSRef.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        lblHBL.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL_MASTER")) Then
                        lblMBL.Text = ds.Tables(0).Rows(0).Item("NR_BL_MASTER")
                    End If



                    Dim Total As String
                    Dim valores As Decimal = 0

                    Dim tabela As String = " <br/><table style='font-family:Arial;font-size:10px;'><tr>"
                    tabela &= "<th style='padding-right:10px'>Taxa</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>Valores R$</th>"
                    tabela &= "<th class='valor' style='padding-left:10px;padding-right:10px'>ISS</th></tr>"

                    tabela &= "<tr><td style='padding-right:10px'>DEMURRAGE</td>"
                    tabela &= "<td style='padding-left:10px;padding-right:10px'>" & ds.Tables(0).Rows(0).Item("VL_DEMURRAGE_LIQUIDO_VENDA") & "</td>"
                    tabela &= "<td style='padding-left:10px;padding-right:10px'></td></tr>"
                    valores = valores + ds.Tables(0).Rows(0).Item("VL_DEMURRAGE_LIQUIDO_VENDA")
                    If CIDADE = "78" Then
                        valores = valores '- linha("VL_ISS")
                    End If

                    Total = FormatCurrency(valores)
                    tabela &= "<tr><td style='padding-left:10px;padding-right:10px;float: right;'></td><td style='padding-left:10px;padding-right:10px'>Total: " & Total & "</td></tr>"
                    tabela &= "</table>"
                    divConteudoDinamico.InnerHtml = tabela

                    Dim ValorExtenso As New ValorExtenso
                    lblValorExtenso.Text = ValorExtenso.NumeroToExtenso(valores)
                    lblValor.Text = Total.ToString

                    Con.Fechar()
                End If
            End If

        End If
        Con.Fechar()
    End Sub

End Class