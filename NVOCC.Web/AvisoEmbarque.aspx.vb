Public Class AvisoEmbarque
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
            lblIDINVOICE.text = Request.QueryString("id")
            ds = Con.ExecutarQuery("SELECT A.ID_BL,A.ID_ACCOUNT_TIPO_INVOICE,(SELECT B.ID_BL_MASTER FROM TB_BL B WHERE B.ID_BL = A.ID_BL)ID_BL_MASTER,(SELECT C.NM_RAZAO FROM TB_PARCEIRO C WHERE C.ID_PARCEIRO = A.ID_PARCEIRO_AGENTE)PARCEIRO_AGENTE FROM TB_ACCOUNT_INVOICE A WHERE A.ID_ACCOUNT_INVOICE = " & Request.QueryString("id"))
            If ds.Tables(0).Rows.Count > 0 Then

                lblID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                lblAgente.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_AGENTE").ToString()

                If ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE") = 1 Then
                    lblGrau.Text = "M"
                    lblID_BL_MASTER.Text = lblID_BL.Text

                    dsBLInvoice.SelectCommand = "SELECT A.ID_BL,A.NR_PROCESSO,NR_BL MBL, ''HBL,  ID_BL_MASTER,NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO, CONVERT(VARCHAR,DT_PREVISAO_EMBARQUE,103)DT_PREVISAO_EMBARQUE,
CONVERT(VARCHAR,DT_EMBARQUE,103)DT_EMBARQUE,
 CONVERT(VARCHAR,DT_PREVISAO_CHEGADA,103)DT_PREVISAO_CHEGADA,
 CONVERT(VARCHAR,DT_CHEGADA,103)DT_CHEGADA,NM_AGENTE FROM View_Master A
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
WHERE ID_BL_MASTER = " & lblID_BL_MASTER.Text & " GROUP BY A.ID_BL,A.NR_PROCESSO,NR_BL,  ID_BL_MASTER,NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO, DT_PREVISAO_EMBARQUE,DT_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,NM_AGENTE

"
                    dgvBLInvoiceMBL.DataBind()
                    dgvBLInvoiceMBL.Visible = True
                ElseIf ds.Tables(0).Rows(0).Item("ID_ACCOUNT_TIPO_INVOICE") = 2 Then
                    lblGrau.Text = "C"
                    lblID_BL_MASTER.Text = ds.Tables(0).Rows(0).Item("ID_BL_MASTER").ToString()

                    dsBLInvoice.SelectCommand = "SELECT A.ID_BL,A.NR_PROCESSO,NR_BL HBL,ID_BL_MASTER,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)MBL,NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO,
 CONVERT(VARCHAR,DT_PREVISAO_EMBARQUE_MASTER,103)DT_PREVISAO_EMBARQUE_MASTER,
CONVERT(VARCHAR,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER,
CONVERT(VARCHAR,DT_PREVISAO_CHEGADA_MASTER,103)DT_PREVISAO_CHEGADA_MASTER,
CONVERT(VARCHAR,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER,NM_AGENTE FROM View_House A
INNER JOIN (SELECT * FROM FN_ACCOUNT_INVOICE('" & Session("DataInicial") & "','" & Session("DataFinal") & "')) AS B ON B.ID_BL_INVOICE = A.ID_BL
WHERE ID_BL_MASTER = " & lblID_BL_MASTER.Text & " GROUP BY  A.ID_BL,A.NR_PROCESSO,NR_BL,ID_BL_MASTER, NR_INVOICE,PARCEIRO_CLIENTE,PARCEIRO_AGENTE,PARCEIRO_TRANSPORTADOR,ORIGEM,DESTINO,DT_PREVISAO_EMBARQUE_MASTER,DT_EMBARQUE_MASTER,DT_PREVISAO_CHEGADA_MASTER,DT_CHEGADA_MASTER,NM_AGENTE "
                    dgvBLInvoiceHBL.DataBind()
                    dgvBLInvoicehBL.Visible = True

                End If

            End If
        End If

            Con.Fechar()
    End Sub

End Class