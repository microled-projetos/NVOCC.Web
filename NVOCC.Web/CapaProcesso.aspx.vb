Public Class CapaProcesso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        If Request.QueryString("id") <> "" Then
            Dim Con As New Conexao_sql
            Dim ID As String = Request.QueryString("id")
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_BL,NAVIO_MASTER,NR_VIAGEM_MASTER,NR_PROCESSO,PARCEIRO_CLIENTE,PARCEIRO_IMPORTADOR,PARCEIRO_AGENTE,ORIGEM,DESTINO,TIPO_ESTUFAGEM,(SELECT NR_REFERENCIA_CLIENTE FROM VW_REFERENCIA_CLIENTE WHERE ID_BL = A.ID_BL)NR_REFERENCIA_CLIENTE,CONVERT(varchar,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER,
CONVERT(varchar,DT_CHEGADA_MASTER,103)DT_CHEGADA_MASTER,
CONVERT(varchar,DT_EMBARQUE_MASTER,103)DT_EMBARQUE_MASTER, PORTO_1T_MASTER,NAVIO_1T_MASTER,
CONVERT(varchar,DT_1T_MASTER,103)DT_1T_MASTER,NR_BL_MASTER,NM_WEEK,PARCEIRO_TRANSPORTADOR,ID_BL_MASTER,QT_MERCADORIA,NM_RESUMO_MERCADORIA,VL_PESO_BRUTO
	FROM [dbo].[View_Emissao_BL] A WHERE ID_BL =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                lblProcesso_FCL.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO").ToString()
                lblImportador_FCL.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_IMPORTADOR").ToString()
                lblRefCliente_FCL.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE").ToString()
                lblNavioViagem_FCL.Text = ds.Tables(0).Rows(0).Item("NAVIO_MASTER").ToString() & "/" & ds.Tables(0).Rows(0).Item("NR_VIAGEM_MASTER").ToString()
                lblNavioTransb_FCL.Text = ds.Tables(0).Rows(0).Item("NAVIO_1T_MASTER").ToString()
                lblPorto_FCL.Text = ds.Tables(0).Rows(0).Item("PORTO_1T_MASTER").ToString()
                lblDataEmbarque_FCL.Text = ds.Tables(0).Rows(0).Item("DT_EMBARQUE_MASTER").ToString()
                lblDataTransb_FCL.Text = ds.Tables(0).Rows(0).Item("DT_1T_MASTER").ToString()
                lblDataChegada_FCL.Text = ds.Tables(0).Rows(0).Item("DT_CHEGADA_MASTER").ToString()
                lblAgente_FCL.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_AGENTE").ToString()
                lblEstufagem_FCL.Text = ds.Tables(0).Rows(0).Item("TIPO_ESTUFAGEM").ToString()
                lblOrigem_FCL.Text = ds.Tables(0).Rows(0).Item("ORIGEM").ToString()
                lblDestino_FCL.Text = ds.Tables(0).Rows(0).Item("DESTINO").ToString()
                lblMaster_FCL.Text = ds.Tables(0).Rows(0).Item("NR_BL_MASTER").ToString()
                lblHouse_FCL.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                lblPesoBruto_FCL.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString()
                lblDescMercadoria_FCL.Text = ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA").ToString()
                lblQtdVolume_FCL.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA").ToString()



                lblWEEK_LCL.Text = ds.Tables(0).Rows(0).Item("NM_WEEK").ToString()
                lblNavioViagem_LCL.Text = ds.Tables(0).Rows(0).Item("NAVIO_MASTER").ToString() & "/" & ds.Tables(0).Rows(0).Item("NR_VIAGEM_MASTER").ToString()
                lblDataEmbarque_LCL.Text = ds.Tables(0).Rows(0).Item("DT_EMBARQUE_MASTER").ToString()
                lblDataChegada_LCL.Text = ds.Tables(0).Rows(0).Item("DT_CHEGADA_MASTER").ToString()
                lblAgente_LCL.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_AGENTE").ToString()
                lblEstufagem_LCL.Text = ds.Tables(0).Rows(0).Item("TIPO_ESTUFAGEM").ToString()
                lblOrigem_LCL.Text = ds.Tables(0).Rows(0).Item("ORIGEM").ToString()
                lblDestino_LCL.Text = ds.Tables(0).Rows(0).Item("DESTINO").ToString()
                lblMaster_LCL.Text = ds.Tables(0).Rows(0).Item("NR_BL_MASTER").ToString()
                lblArmador_LCL.Text = ds.Tables(0).Rows(0).Item("PARCEIRO_TRANSPORTADOR").ToString()
            End If

            Dim ds1 As DataSet = Con.ExecutarQuery("SELECT PARCEIRO_IMPORTADOR,NR_BL,NR_PROCESSO,INCOTERM, (SELECT MIN(NR_NOTA_DEBITO) NR_NOTA_DEBITO FROM TB_FATURAMENTO 
WHERE ID_CONTA_PAGAR_RECEBER IN (SELECT ID_CONTA_PAGAR_RECEBER FROM TB_CONTA_PAGAR_RECEBER_ITENS WHERE ID_BL = A.ID_BL))NR_NOTA_DEBITO
	FROM [dbo].[View_Emissao_BL] A WHERE ID_BL_MASTER =" & ds.Tables(0).Rows(0).Item("ID_BL_MASTER").ToString())
            If ds1.Tables(0).Rows.Count > 0 Then
                Dim tabela As String = "<table class='subtotal table table-bordered' style='font-family:Arial;font-size:10px !important;'><tr>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>REFERENCIA</strong></td>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>HOUSE</strong></td>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>CNEE</strong></td>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>EMISSÃO ORIGINAIS</strong></td>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>INCOTERM</strong></td>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>BL RETIRADO/TROCADO</strong></td>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>NOTA DE DÉBITO</strong></td>"
                tabela &= "<td style='padding-right:10px;font-size:10px !important;'><strong>LIBERAÇÃO TERMINAL</strong></td></tr>"

                For Each linha As DataRow In ds1.Tables(0).Rows
                    tabela &= "<tr><td style='padding-right:10px;font-size:10px !important;'>" & linha("NR_PROCESSO") & "</td>"
                    tabela &= "<td style='padding-right:10px;font-size:10px !important;'>" & linha("NR_BL") & "</td>"
                    tabela &= "<td style='padding-right:10px;font-size:10px !important;'>" & linha("PARCEIRO_IMPORTADOR") & "</td>"
                    tabela &= "<td style='padding-right:10px;font-size:10px !important;'></td>"
                    tabela &= "<td style='padding-right:10px;font-size:10px !important;'>" & linha("INCOTERM") & "</td>"
                    tabela &= "<td style='padding-right:10px;font-size:10px !important;'></td>"
                    tabela &= "<td style='padding-right:10px;font-size:10px !important;'>" & linha("NR_NOTA_DEBITO") & "</td>"
                    tabela &= "<td style='padding-right:10px;font-size:10px !important;'></td></tr>"
                Next

                tabela &= "</table>"
                divConteudoDinamico.InnerHtml = tabela

                If ds.Tables(0).Rows(0).Item("TIPO_ESTUFAGEM").ToString() = "FCL" Then
                    divFCL.Visible = True
                ElseIf ds.Tables(0).Rows(0).Item("TIPO_ESTUFAGEM").ToString() = "LCL" Then
                    divLCL.Visible = True
                End If
            End If

            Con.Fechar()
        End If

    End Sub

End Class