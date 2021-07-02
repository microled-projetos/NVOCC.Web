Public Class DebitNote
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then 

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 2032 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If


        If Request.QueryString("id") <> "" then
            lblIDINVOICE.text = Request.QueryString("id")
            ds = Con.ExecutarQuery("SELECT A.ID_BL,A.ID_ACCOUNT_TIPO_INVOICE,(SELECT B.ID_BL_MASTER FROM TB_BL B WHERE B.ID_BL = A.ID_BL)ID_BL_MASTER,(SELECT C.NM_RAZAO FROM TB_PARCEIRO C WHERE C.ID_PARCEIRO = A.ID_PARCEIRO_AGENTE)PARCEIRO_AGENTE FROM TB_ACCOUNT_INVOICE A WHERE A.ID_ACCOUNT_INVOICE = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 then

                lblID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                If ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE") = 1 then
                    lblGrau.Text = "M"
                    lblID_BL_MASTER.Text = lblID_BL.Text

                    Dim dsDados As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,A.NR_PROCESSO,NR_BL MBL, ''HBL,  ID_BL_MASTER,NR_INVOICE,CONVERT(VARCHAR,DT_INVOICE,103)DT_INVOICE,PARCEIRO_CLIENTE,NM_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO, CONVERT(VARCHAR,DT_PREVISAO_EMBARQUE,103)DT_PREVISAO_EMBARQUE,
CONVERT(VARCHAR,DT_EMBARQUE,103)DT_EMBARQUE,
 CONVERT(VARCHAR,DT_PREVISAO_CHEGADA,103)DT_PREVISAO_CHEGADA,
 CONVERT(VARCHAR,DT_CHEGADA,103)DT_CHEGADA,NM_AGENTE ,
 (SELECT [DBO].[FN_REFERENCIA_CLIENTE] (A.ID_BL) )REFERENCIA_CLIENTE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_M3
 FROM View_Master A
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
WHERE ID_BL_MASTER = " & lblID_BL.Text & " GROUP BY A.ID_BL,A.NR_PROCESSO,NR_BL,  ID_BL_MASTER,NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO, DT_PREVISAO_EMBARQUE,DT_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,NM_AGENTE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_M3,DT_INVOICE")
                    If dsDados.Tables(0).Rows.Count > 0 then
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NM_AGENTE")) then
                            lblEmpresa.Text = dsDados.Tables(0).Rows(0).Item("NM_AGENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_INVOICE")) then
                            lblNumeroInvoice.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DT_INVOICE")) then
                            lblDataInvoice.Text = dsDados.Tables(0).Rows(0).Item("DT_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) then
                            lblProcesso.Text = dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")) then
                            lblCliente.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) then
                            lblReferencias.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("MBL")) then
                            lblMBL.Text = dsDados.Tables(0).Rows(0).Item("MBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("HBL")) then
                            lblHBL.Text = dsDados.Tables(0).Rows(0).Item("HBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")) then
                            lblImportador.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")) then
                            lblViagem.Text = dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("ORIGEM")) then
                            lblOrigem.Text = dsDados.Tables(0).Rows(0).Item("ORIGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DESTINO")) then
                            lblDestino.Text = dsDados.Tables(0).Rows(0).Item("DESTINO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_M3")) then
                            lblQtdVolumes.Text = dsDados.Tables(0).Rows(0).Item("VL_M3")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) then
                            lblPesoBruto.Text = dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                        End If
                    End If

                ElseIf ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE") = 2 then
                    lblGrau.Text = "C"
                    lblID_BL_MASTER.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER").ToString()

                    Dim dsDados As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,A.NR_PROCESSO,NR_BL HBL,ID_BL_MASTER,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)MBL,NR_INVOICE,CONVERT(VARCHAR,DT_INVOICE,103)DT_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO,
 CONVERT(VARCHAR,DT_PREVISAO_EMBARQUE_MASTER,103)DT_PREVISAO_EMBARQUE_MASTER,
CONVERT(VARCHAR,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER,
CONVERT(VARCHAR,DT_PREVISAO_CHEGADA_MASTER,103)DT_PREVISAO_CHEGADA_MASTER,
CONVERT(VARCHAR,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER,NM_AGENTE,
(SELECT [DBO].[FN_REFERENCIA_CLIENTE] (A.ID_BL) )REFERENCIA_CLIENTE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_M3
FROM View_House A
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
WHERE ID_BL_MASTER = " & lblID_BL_MASTER.Text & " GROUP BY  A.ID_BL,A.NR_PROCESSO,NR_BL,ID_BL_MASTER, NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO,DT_PREVISAO_EMBARQUE_MASTER,DT_EMBARQUE_MASTER,DT_PREVISAO_CHEGADA_MASTER,DT_CHEGADA_MASTER,NM_AGENTE,DT_INVOICE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_M3")
                    If dsDados.Tables(0).Rows.Count > 0 then
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NM_AGENTE")) then
                            lblEmpresa.Text = dsDados.Tables(0).Rows(0).Item("NM_AGENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_INVOICE")) then
                            lblNumeroInvoice.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DT_INVOICE")) then
                            lblDataInvoice.Text = dsDados.Tables(0).Rows(0).Item("DT_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) then
                            lblProcesso.Text = dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")) then
                            lblCliente.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) then
                            lblReferencias.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("MBL")) then
                            lblMBL.Text = dsDados.Tables(0).Rows(0).Item("MBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("HBL")) then
                            lblHBL.Text = dsDados.Tables(0).Rows(0).Item("HBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")) then
                            lblImportador.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")) then
                            lblViagem.Text = dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("ORIGEM")) then
                            lblOrigem.Text = dsDados.Tables(0).Rows(0).Item("ORIGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DESTINO")) then
                            lblDestino.Text = dsDados.Tables(0).Rows(0).Item("DESTINO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_M3")) then
                            lblQtdVolumes.Text = dsDados.Tables(0).Rows(0).Item("VL_M3")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) then
                            lblPesoBruto.Text = dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                        End If
                    End If

                End If

                Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT SIGLA_MOEDA,NM_ITEM_DESPESA,VL_TAXA FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "') WHERE ID_ACCOUNT_INVOICE  =" & lblIDINVOICE.Text)

                Dim valores As Double = 0
                If dsTaxas.Tables(0).Rows.Count > 0 then

                    Dim tabela As String = "<br/><table><tr>"
                    tabela &= "<td><strong>DESCRIPTION</strong></td>"
                    tabela &= "<td><strong>INVOICE AMOUNT</strong></td>"
                    tabela &= "<td><strong>CURRENCY</strong></td></tr>"

                    For Each linha As DataRow In dsTaxas.Tables(0).Rows
                        tabela &= "<tr><td>" & linha("NM_ITEM_DESPESA") & "</td>"
                        tabela &= "<td>" & linha("VL_TAXA") & "</td>"
                        tabela &= "<td>" & linha("SIGLA_MOEDA") & "</td></tr>"

                        valores = valores + linha("VL_TAXA")


                    Next
                    tabela &= "<tr><td></td><td></td><td><strong>Total Invoice: " & valores & "</strong></td></tr>"
                    tabela &= "</table>"
                    divConteudoDinamico.InnerHtml = tabela
                End If


            End If

        End If

        Con.Fechar()
    End Sub

End Class