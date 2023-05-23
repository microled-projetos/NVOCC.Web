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


        If Request.QueryString("id") <> "" Then
            lblIDINVOICE.Text = Request.QueryString("id")
            ds = Con.ExecutarQuery("SELECT A.ID_BL,A.ID_ACCOUNT_TIPO_INVOICE,(SELECT B.ID_BL_MASTER FROM TB_BL B WHERE B.ID_BL = A.ID_BL)ID_BL_MASTER,(SELECT C.NM_RAZAO FROM TB_PARCEIRO C WHERE C.ID_PARCEIRO = A.ID_PARCEIRO_AGENTE)PARCEIRO_AGENTE FROM TB_ACCOUNT_INVOICE A WHERE A.ID_ACCOUNT_INVOICE = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 Then

                lblID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                If ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE") = 1 Then
                    lblGrau.Text = "M"
                    lblID_BL_MASTER.Text = lblID_BL.Text

                    Dim dsDados As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,A.NR_PROCESSO,NR_BL MBL, ''HBL,  ID_BL_MASTER,NR_INVOICE,CONVERT(VARCHAR,DT_VENCIMENTO,103)DT_VENCIMENTO,CONVERT(VARCHAR,DT_INVOICE,103)DT_INVOICE,PARCEIRO_CLIENTE,NM_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO, CONVERT(VARCHAR,DT_PREVISAO_EMBARQUE,103)DT_PREVISAO_EMBARQUE,
CONVERT(VARCHAR,DT_EMBARQUE,103)DT_EMBARQUE,
 CONVERT(VARCHAR,DT_PREVISAO_CHEGADA,103)DT_PREVISAO_CHEGADA,
 CONVERT(VARCHAR,DT_CHEGADA,103)DT_CHEGADA,NM_AGENTE ,
 (SELECT [DBO].[FN_REFERENCIA_CLIENTE] (A.ID_BL) )REFERENCIA_CLIENTE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_M3,VL_PESO_TAXADO
 FROM View_Master A
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
WHERE ID_BL = " & lblID_BL.Text & " AND ID_ACCOUNT_INVOICE  =" & lblIDINVOICE.Text & " GROUP BY A.ID_BL,A.NR_PROCESSO,NR_BL,  ID_BL_MASTER,NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO, DT_PREVISAO_EMBARQUE,DT_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,NM_AGENTE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_PESO_TAXADO,VL_M3,DT_INVOICE,DT_VENCIMENTO")
                    If dsDados.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NM_AGENTE")) Then
                            lblEmpresa.Text = dsDados.Tables(0).Rows(0).Item("NM_AGENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_INVOICE")) Then
                            lblNumeroInvoice.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DT_INVOICE")) Then
                            lblDataInvoice.Text = dsDados.Tables(0).Rows(0).Item("DT_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DT_VENCIMENTO")) Then
                            lblDataVencimento.Text = dsDados.Tables(0).Rows(0).Item("DT_VENCIMENTO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                            lblProcesso.Text = dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")) Then
                            lblCliente.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                            lblReferencias.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("MBL")) Then
                            lblMBL.Text = dsDados.Tables(0).Rows(0).Item("MBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("HBL")) Then
                            lblHBL.Text = dsDados.Tables(0).Rows(0).Item("HBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")) Then
                            lblImportador.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_PESO_TAXADO")) Then
                            lblPesoTaxado.Text = dsDados.Tables(0).Rows(0).Item("VL_PESO_TAXADO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_TRANSPORTADOR")) Then
                            lblTransportador.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_TRANSPORTADOR")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DT_EMBARQUE")) Then
                            lblEmbarque.Text = dsDados.Tables(0).Rows(0).Item("DT_EMBARQUE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")) Then
                            lblViagem.Text = dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("ORIGEM")) Then
                            lblOrigem.Text = dsDados.Tables(0).Rows(0).Item("ORIGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DESTINO")) Then
                            lblDestino.Text = dsDados.Tables(0).Rows(0).Item("DESTINO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_M3")) Then
                            lblQtdVolumes.Text = dsDados.Tables(0).Rows(0).Item("VL_M3")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                            lblPesoBruto.Text = dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                        End If
                    End If

                ElseIf ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE") = 2 Then
                    lblGrau.Text = "C"
                    lblID_BL_MASTER.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER").ToString()

                    Dim dsDados As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,A.NR_PROCESSO,NR_BL HBL,ID_BL_MASTER,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)MBL,NR_INVOICE,CONVERT(VARCHAR,DT_VENCIMENTO,103)DT_VENCIMENTO,CONVERT(VARCHAR,DT_INVOICE,103)DT_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO,
 CONVERT(VARCHAR,DT_PREVISAO_EMBARQUE_MASTER,103)DT_PREVISAO_EMBARQUE_MASTER,
CONVERT(VARCHAR,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER,
CONVERT(VARCHAR,DT_PREVISAO_CHEGADA_MASTER,103)DT_PREVISAO_CHEGADA_MASTER,
CONVERT(VARCHAR,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER,NM_AGENTE,
(SELECT [DBO].[FN_REFERENCIA_CLIENTE] (A.ID_BL) )REFERENCIA_CLIENTE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_M3
FROM View_House A
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
WHERE ID_BL_MASTER = " & lblID_BL_MASTER.Text & " AND ID_ACCOUNT_INVOICE  =" & lblIDINVOICE.Text & " GROUP BY  A.ID_BL,A.NR_PROCESSO,NR_BL,ID_BL_MASTER, NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO,DT_PREVISAO_EMBARQUE_MASTER,DT_EMBARQUE_MASTER,DT_PREVISAO_CHEGADA_MASTER,DT_CHEGADA_MASTER,NM_AGENTE,DT_INVOICE,PARCEIRO_IMPORTADOR,NR_VIAGEM,VL_PESO_BRUTO,VL_M3,DT_VENCIMENTO")
                    If dsDados.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NM_AGENTE")) Then
                            lblEmpresa.Text = dsDados.Tables(0).Rows(0).Item("NM_AGENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_INVOICE")) Then
                            lblNumeroInvoice.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DT_INVOICE")) Then
                            lblDataInvoice.Text = dsDados.Tables(0).Rows(0).Item("DT_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DT_VENCIMENTO")) Then
                            lblDataVencimento.Text = dsDados.Tables(0).Rows(0).Item("DT_VENCIMENTO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                            lblProcesso.Text = dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")) Then
                            lblCliente.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_CLIENTE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                            lblReferencias.Text = dsDados.Tables(0).Rows(0).Item("NR_INVOICE")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("MBL")) Then
                            lblMBL.Text = dsDados.Tables(0).Rows(0).Item("MBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("HBL")) Then
                            lblHBL.Text = dsDados.Tables(0).Rows(0).Item("HBL")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")) Then
                            lblImportador.Text = dsDados.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")) Then
                            lblViagem.Text = dsDados.Tables(0).Rows(0).Item("NR_VIAGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("ORIGEM")) Then
                            lblOrigem.Text = dsDados.Tables(0).Rows(0).Item("ORIGEM")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("DESTINO")) Then
                            lblDestino.Text = dsDados.Tables(0).Rows(0).Item("DESTINO")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_M3")) Then
                            lblQtdVolumes.Text = dsDados.Tables(0).Rows(0).Item("VL_M3")
                        End If
                        If Not IsDBNull(dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                            lblPesoBruto.Text = dsDados.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                        End If
                    End If

                End If

                Dim dsTaxas As DataSet = Con.ExecutarQuery("SELECT ISNULL(SIGLA_MOEDA,'')SIGLA_MOEDA,ISNULL(NM_ITEM_DESPESA,'')NM_ITEM_DESPESA,ISNULL(SUM(VL_TAXA),0)VL_TAXA FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "') WHERE ID_ACCOUNT_INVOICE  =" & lblIDINVOICE.Text & " GROUP BY  SIGLA_MOEDA,NM_ITEM_DESPESA")

                Dim valores As Decimal = 0
                If dsTaxas.Tables(0).Rows.Count > 0 Then

                    Dim tabela As String = "<br/><table><tr>"
                    tabela &= "<td><strong>DESCRIPTION</strong></td>"
                    tabela &= "<td><strong>INVOICE AMOUNT</strong></td>"
                    tabela &= "<td><strong>CURRENCY</strong></td></tr>"

                    For Each linha As DataRow In dsTaxas.Tables(0).Rows
                        tabela &= "<tr><td>" & linha("NM_ITEM_DESPESA") & "</td>"
                        tabela &= "<td>" & linha("VL_TAXA").ToString.Replace("-", "") & "</td>"
                        tabela &= "<td>" & linha("SIGLA_MOEDA") & "</td></tr>"

                        valores = valores + linha("VL_TAXA")


                    Next
                    tabela &= "<tr><td></td><td></td><td><strong>Total Invoice: " & valores.ToString.Replace("-", "") & "</strong></td></tr>"
                    tabela &= "</table>"
                    divConteudoDinamico.InnerHtml = tabela
                End If


            End If



            Dim dsBanco As DataSet = Con.ExecutarQuery("SELECT NAME,SWIFT,ACCOUNT,AGENCY,IBAN_BR, BANK_ADRESS, BANK_NAME FROM TB_CONTA_BANCARIA WHERE ID_CONTA_BANCARIA = 1")
            If dsBanco.Tables(0).Rows.Count > 0 Then
                lblName.Text = dsBanco.Tables(0).Rows(0).Item("NAME")
                lblSwift.Text = dsBanco.Tables(0).Rows(0).Item("SWIFT")
                'lblAccount.Text = dsBanco.Tables(0).Rows(0).Item("ACCOUNT")
                'lblAgency.Text = dsBanco.Tables(0).Rows(0).Item("AGENCY")
                lblAccount.Text = "AGENCY:" & dsBanco.Tables(0).Rows(0).Item("AGENCY") & " / ACCOUNT NUMBER:" & dsBanco.Tables(0).Rows(0).Item("ACCOUNT")
                lblIban.Text = dsBanco.Tables(0).Rows(0).Item("IBAN_BR")
                lblEnderecoBanco.Text = dsBanco.Tables(0).Rows(0).Item("BANK_ADRESS")
                lblBanco.Text = dsBanco.Tables(0).Rows(0).Item("BANK_NAME")

            End If


            Dim dsParceiro As DataSet = Con.ExecutarQuery("SELECT UPPER(NM_RAZAO),UPPER(ENDERECO)ENDERECO,NR_ENDERECO,CNPJ,CPF,CEP,ID_CIDADE,(SELECT NM_CIDADE FROM TB_CIDADE WHERE ID_CIDADE = A.ID_CIDADE)CIDADE,UPPER(BAIRRO)BAIRRO,TELEFONE,(SELECT NM_PAIS FROM TB_PAIS WHERE ID_PAIS = A.ID_PAIS)PAIS,INSCR_ESTADUAL FROM TB_PARCEIRO A WHERE ID_PARCEIRO = 1 ")

            If dsParceiro.Tables(0).Rows.Count > 0 Then
                lblEnderecoFCA.Text = dsParceiro.Tables(0).Rows(0).Item("ENDERECO") & ", " & dsParceiro.Tables(0).Rows(0).Item("NR_ENDERECO") & " - " & dsParceiro.Tables(0).Rows(0).Item("BAIRRO")
                lblEnderecoFCA2.Text = dsParceiro.Tables(0).Rows(0).Item("CIDADE") & " - " & dsParceiro.Tables(0).Rows(0).Item("PAIS") & " - CEP: " & dsParceiro.Tables(0).Rows(0).Item("CEP")


                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("CNPJ")) Then
                    lblDocFCA.Text = "CNPJ: " & dsParceiro.Tables(0).Rows(0).Item("CNPJ") & " - I.E: " & dsParceiro.Tables(0).Rows(0).Item("INSCR_ESTADUAL")
                End If

                If Not IsDBNull(dsParceiro.Tables(0).Rows(0).Item("TELEFONE")) Then
                    lblContatoFCA.Text = "FONE: " & dsParceiro.Tables(0).Rows(0).Item("TELEFONE")
                End If

            End If


            Dim dsUsuario As DataSet = Con.ExecutarQuery("SELECT REPLACE(LOGIN, '.', ' ') NOME FROM TB_USUARIO WHERE ID_USUARIO = " & Session("ID_USUARIO"))
            If dsUsuario.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(dsUsuario.Tables(0).Rows(0).Item("NOME")) Then
                    lblUsuario.Text = "ISSUED BY: " & dsUsuario.Tables(0).Rows(0).Item("NOME")
                Else
                    lblUsuario.Text = ""
                End If
            End If
        End If


        Con.Fechar()
    End Sub

End Class