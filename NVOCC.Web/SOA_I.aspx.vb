Public Class SOA_I
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""


        If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
            FILTRO = " AND ID_PARCEIRO_AGENTE = " & Request.QueryString("ag")
        Else
            FILTRO = ""
        End If
        Dim tabela As String = ""
        Dim DadosBancarios As String = ""

        Dim ds As DataSet = Con.ExecutarQuery("SELECT DISTINCT ID_PARCEIRO_AGENTE,ID_MOEDA,NM_AGENTE,SIGLA_MOEDA FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')  WHERE FL_CONFERIDO = 1 " & FILTRO)

        For Each linhaTitulo As DataRow In ds.Tables(0).Rows
            If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
                Dim dsDadosBancarios As DataSet = Con.ExecutarQuery("SELECT NM_RAZAO,TAX_ID,BANK_NAME,BANK_ADDRESS,ACCOUNT_NUMBER, AGENCY, SWIFT_CODE, IBAN_BR, UPPER(ENDERECO)ENDERECO, UPPER(COMPL_ENDERECO)COMPL_ENDERECO, UPPER(BAIRRO)BAIRRO, NR_ENDERECO FROM TB_PARCEIRO WHERE ID_PARCEIRO= " & Request.QueryString("ag"))

                DadosBancarios = "<strong>Agente Name: </strong> " & dsDadosBancarios.Tables(0).Rows(0).Item("NM_RAZAO").ToString()
                DadosBancarios &= "<br/><strong>Address: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("ENDERECO").ToString() & ", " & dsDadosBancarios.Tables(0).Rows(0).Item("NR_ENDERECO").ToString() & " - " & dsDadosBancarios.Tables(0).Rows(0).Item("BAIRRO").ToString() & " - " & dsDadosBancarios.Tables(0).Rows(0).Item("COMPL_ENDERECO").ToString()
                DadosBancarios &= "<br/><strong>Tax ID: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("TAX_ID").ToString()
                DadosBancarios &= "<br/><strong>Bank Name: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("BANK_NAME").ToString()
                DadosBancarios &= "<br/><strong>Bank Address: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("BANK_ADDRESS").ToString()
                DadosBancarios &= "<br/><strong>Account Number: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("ACCOUNT_NUMBER").ToString() & " - " & dsDadosBancarios.Tables(0).Rows(0).Item("AGENCY").ToString()
                DadosBancarios &= "<br/><strong>Swift Cod: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("SWIFT_CODE").ToString()
                DadosBancarios &= "<br/><strong>Iban BR: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("IBAN_BR").ToString()

                divDadosBancarios.InnerHtml = DadosBancarios
            End If

            Dim PARCEIRO As String = linhaTitulo("ID_PARCEIRO_AGENTE")
            Dim MOEDA As String = linhaTitulo("ID_MOEDA")
            Dim SIGLA_MOEDA As String = linhaTitulo("SIGLA_MOEDA")
          
             If Request.QueryString("ag") = 0  Then
                Dim dsDadosBancarios As DataSet = Con.ExecutarQuery("SELECT NM_RAZAO,TAX_ID,BANK_NAME,BANK_ADDRESS,ACCOUNT_NUMBER, AGENCY, SWIFT_CODE, IBAN_BR, UPPER(ENDERECO)ENDERECO, UPPER(COMPL_ENDERECO)COMPL_ENDERECO, UPPER(BAIRRO)BAIRRO, NR_ENDERECO FROM TB_PARCEIRO WHERE ID_PARCEIRO= " & PARCEIRO)

                DadosBancarios = "<br/><br/><div style='float:right'><strong>Agente Name: </strong> " & dsDadosBancarios.Tables(0).Rows(0).Item("NM_RAZAO").ToString()
                DadosBancarios &= "<br/><strong>Address: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("ENDERECO").ToString() & ", " & dsDadosBancarios.Tables(0).Rows(0).Item("NR_ENDERECO").ToString() & " - " & dsDadosBancarios.Tables(0).Rows(0).Item("BAIRRO").ToString() & " - " & dsDadosBancarios.Tables(0).Rows(0).Item("COMPL_ENDERECO").ToString()
                DadosBancarios &= "<br/><strong>Tax ID: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("TAX_ID").ToString()
                DadosBancarios &= "<br/><strong>Bank Name: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("BANK_NAME").ToString()
                DadosBancarios &= "<br/><strong>Bank Address: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("BANK_ADDRESS").ToString()
                DadosBancarios &= "<br/><strong>Account Number: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("ACCOUNT_NUMBER").ToString() & " - " & dsDadosBancarios.Tables(0).Rows(0).Item("AGENCY").ToString()
                DadosBancarios &= "<br/><strong>Swift Cod: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("SWIFT_CODE").ToString()
                DadosBancarios &= "<br/><strong>Iban BR: </strong>" & dsDadosBancarios.Tables(0).Rows(0).Item("IBAN_BR").ToString()
                DadosBancarios &="<br/><br/></div>"
                tabela &=  DadosBancarios
            End If



            tabela &= "<h5>AGENTE: " & linhaTitulo("NM_AGENTE") & "<br/>CURRENCY: " & linhaTitulo("SIGLA_MOEDA") & "</h5> Periodo de Vencimento:" & Session("DataInicial") & " - " & Session("DataFinal")

            tabela &= "<table class='tabelaDinamica' border='1' style='font-size:10px;'>"
            tabela &= "<tr><td><strong>OUR REFERENCE</strong></td>"
            tabela &= "<td><strong>CUSTOMER</strong></td>"
            tabela &= "<td><strong>DEPARTURE</strong></td>"
            tabela &= "<td><strong>ARRIVAL</strong></td>"
            tabela &= "<td><strong>AIR/SEA</strong></td>"
            tabela &= "<td><strong>IMP/EXP</strong></td>"
            tabela &= "<td><strong>ORIGIN</strong></td>"
            tabela &= "<td><strong>DESTIN</strong></td>"
            tabela &= "<td><strong>Nº MASTER</strong></td>"
            tabela &= "<td><strong>P/C</strong></td>"
            tabela &= "<td><strong>Nº HOUSE</strong></td>"
            tabela &= "<td><strong>P/C</strong></td>"
            tabela &= "<td><strong>INVOICE</strong></td>"
            tabela &= "<td><strong>DATE</strong></td>"
            tabela &= "<td><strong>CN/DN</strong></td>"'''EXCLUIR??
            tabela &= "<td><strong>CURR</strong></td>"
          ''  tabela &= "<td><strong>TOTAL</strong></td>" '''EXCLUIR
            tabela &= "<td><strong>DEBIT</strong></td>"
            tabela &= "<td><strong>CREDIT</strong></td>"
            tabela &= "<td><strong>SALDO</strong></td></tr>"


            Dim dsdados As DataSet = Con.ExecutarQuery("SELECT DISTINCT GRAU,NR_PROCESSO,PARCEIRO_CLIENTE,DT_EMBARQUE,DT_CHEGADA,TP_SERVICO,TP_VIA_INGLES,ORIGEM,DESTINO,(SELECT CD_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO_MASTER)TIPO_PAGAMENTO_MASTER,NR_BL_MASTER,(SELECT CD_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,NR_BL,B.NR_INVOICE,DT_INVOICE,SIGLA_MOEDA,CD_ACCOUNT_TIPO_FATURA,SUM(ISNULL(VL_TAXA,0))VL_TAXA,SUM(ISNULL(VL_TAXA_BR,0))VL_TAXA_BR
FROM [dbo].[View_BL]  A 
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
 WHERE FL_CONFERIDO = 1 AND B.ID_PARCEIRO_AGENTE = " & PARCEIRO & " AND ID_MOEDA = " & MOEDA & "
GROUP BY B.ID_ACCOUNT_INVOICE,B.NR_INVOICE,ORIGEM,DESTINO,NR_BL,GRAU,DT_EMBARQUE,DT_CHEGADA,PARCEIRO_CLIENTE,DT_INVOICE,SIGLA_MOEDA,CD_ACCOUNT_TIPO_FATURA,NR_PROCESSO,TIPO_PAGAMENTO,NR_BL_MASTER,TP_SERVICO,TP_VIA_INGLES,ID_TIPO_PAGAMENTO_MASTER,ID_TIPO_PAGAMENTO")

             Dim credit As Decimal = 0
             Dim debit As Decimal = 0
             Dim saldo As Decimal = 0

            For Each linhadados As DataRow In dsdados.Tables(0).Rows


                If linhadados("GRAU") = "M" Then
                    tabela &= "<tr><td>MBL</td>"
                ElseIf linhadados("GRAU") = "C" Then
                    tabela &= "<tr><td>" & linhadados("NR_PROCESSO") & "</td>"
                End If


                tabela &= "<td>" & linhadados("PARCEIRO_CLIENTE") & "</td>"
                tabela &= "<td>" & linhadados("DT_EMBARQUE") & "</td>"
                tabela &= "<td>" & linhadados("DT_CHEGADA") & "</td>"
                tabela &= "<td>" & linhadados("TP_VIA_INGLES") & "</td>"
                tabela &= "<td>" & linhadados("TP_SERVICO") & "</td>"
                tabela &= "<td>" & linhadados("ORIGEM") & "</td>"
                tabela &= "<td>" & linhadados("DESTINO") & "</td>"

                If linhadados("GRAU") = "M" Then
                    tabela &= "<td>" & linhadados("NR_BL") & "</td>"
                    tabela &= "<td>" & linhadados("TIPO_PAGAMENTO") & "</td>"
                    tabela &= "<td></td>"
                    tabela &= "<td></td>"

                ElseIf linhadados("GRAU") = "C" Then
                    tabela &= "<td>" & linhadados("NR_BL_MASTER") & "</td>"
                    tabela &= "<td>" & linhadados("TIPO_PAGAMENTO_MASTER") & "</td>"
                    tabela &= "<td>" & linhadados("NR_BL") & "</td>"
                    tabela &= "<td>" & linhadados("TIPO_PAGAMENTO") & "</td>"
                End If

                tabela &= "<td>" & linhadados("NR_INVOICE") & "</td>"
                tabela &= "<td>" & linhadados("DT_INVOICE") & "</td>"
                tabela &= "<td>" & linhadados("CD_ACCOUNT_TIPO_FATURA") & "</td>" 
                tabela &= "<td>" & linhadados("SIGLA_MOEDA") & "</td>"

                If linhadados("CD_ACCOUNT_TIPO_FATURA") =  "DN" THEN
               
                    tabela &= "<td><strong>" & linhadados("VL_TAXA") & "</strong></td>"
                    tabela &= "<td><strong></strong></td>"
                
                    debit = debit + linhadados("VL_TAXA")
                ElseIf linhadados("CD_ACCOUNT_TIPO_FATURA") =  "CN" THEN
                  
                    tabela &= "<td><strong></strong></td>"
                    tabela &= "<td><strong>" & linhadados("VL_TAXA") & "</strong></td>"
                    credit = credit + linhadados("VL_TAXA")

                End If
               
                saldo = saldo + linhadados("VL_TAXA")
               
                tabela &= "<td><strong>" & saldo  & "</td></tr>"




            Next
            Dim Total As String = saldo
            tabela &= "<tr style='border:none;font-weight:bold'><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>TOTAL</td><td>" & SIGLA_MOEDA & "</td><td>" & debit & "</td><td>" & credit & "</td><td>" & Total & "</td></tr>"
            tabela &= "</table>"

        Next

        divConteudoDinamico.InnerHtml &= tabela

        Con.Fechar()
    End Sub

    Private Sub btnCSV_Click(sender As Object, e As EventArgs) Handles btnCSV.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim FILTRO As String = ""

        If Request.QueryString("ag") <> "" And Request.QueryString("ag") <> 0 Then
            FILTRO = " AND B.ID_PARCEIRO_AGENTE = " & Request.QueryString("ag")
        Else
            FILTRO = ""
        End If

        Dim sql As String = "SELECT 
        CASE WHEN GRAU ='M' THEN 'MBL' ELSE NR_PROCESSO END 'OUR REFERENCE' ,
        PARCEIRO_CLIENTE AS CUSTOMER,
        DT_EMBARQUE AS DEPARTURE ,
        DT_CHEGADA AS ARRIVAL,
        TP_VIA_INGLES AS 'AIR/SEA',
        TP_SERVICO AS 'IMP/EXP',
        ORIGEM AS ORIGIN,
        DESTINO AS DESTIN,
        NR_BL_MASTER AS 'Nº MASTER',
        (SELECT CD_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO_MASTER)'P/C MBL'	,
        NR_BL AS 'Nº HOUSE',
        (SELECT CD_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)'P/C HBL',
        B.NR_INVOICE INVOICE,
        DT_INVOICE DATE,
        CD_ACCOUNT_TIPO_FATURA 'CN/DN',
        SIGLA_MOEDA CURR,
        SUM(ISNULL(VL_TAXA,0))TOTAL
FROM [dbo].[View_BL]  A 
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
 WHERE FL_CONFERIDO = 1 " & FILTRO & " 
GROUP BY B.ID_ACCOUNT_INVOICE,B.NR_INVOICE,ORIGEM,DESTINO,NR_BL,GRAU,DT_EMBARQUE,DT_CHEGADA,PARCEIRO_CLIENTE,DT_INVOICE,SIGLA_MOEDA,CD_ACCOUNT_TIPO_FATURA,NR_PROCESSO,TIPO_PAGAMENTO,NR_BL_MASTER,TP_SERVICO,TP_VIA_INGLES,ID_TIPO_PAGAMENTO_MASTER,ID_TIPO_PAGAMENTO"


        Classes.Excel.exportaExcel(sql, "NVOCC", "SOA_I")

    End Sub
End Class