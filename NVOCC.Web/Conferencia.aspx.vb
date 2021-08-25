Public Class Conferencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If Request.QueryString("id") <> "" Then
            ds = Con.ExecutarQuery("SELECT ID_BL FROM TB_ACCOUNT_INVOICE WHERE ID_ACCOUNT_INVOICE = " & Request.QueryString("id"))
            lblID_BL.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
            lblIDINVOICE.Text = Request.QueryString("id")

            If ds.Tables(0).Rows.Count > 0 Then
                If Request.QueryString("T") = "C" Then
                    Dim ID_BL_MASTER As String = ""
                    ds = Con.ExecutarQuery("SELECT ID_BL,GRAU,ID_BL_MASTER,NR_BL,NR_PROCESSO,CONVERT(VARCHAR,DT_EMBARQUE,103)DT_EMBARQUE,CONVERT(VARCHAR,DT_CHEGADA,103)DT_CHEGADA,VL_PESO_BRUTO,VL_M3,(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM)ESTUFAGEM,(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_FRETE)SIGLA_MOEDA,(SELECT NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE WHERE ID_STATUS_FRETE_AGENTE = A.ID_STATUS_FRETE_AGENTE) STATUS_FRETE_AGENTE,ISNULL((SELECT NM_TIPO_DIVISAO_PROFIT FROM TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT = A.ID_PROFIT_DIVISAO),'') TIPO_DIVISAO_PROFIT FROM TB_BL A WHERE GRAU = 'C' AND ID_BL = " & lblID_BL.Text)
                    If ds.Tables(0).Rows.Count > 0 Then
                        lblStatusComissoesHBL.Text = ds.Tables(0).Rows(0).Item("TIPO_DIVISAO_PROFIT").ToString()
                        lblHBL_House.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                        lblGrau.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                        ID_BL_MASTER = ds.Tables(0).Rows(0).Item("ID_BL_MASTER").ToString()
                        lblProcesso_House.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO").ToString()
                        lblTipoFrete_House.Text = ds.Tables(0).Rows(0).Item("TIPO_PAGAMENTO").ToString()
                        lblEstufagem_House.Text = ds.Tables(0).Rows(0).Item("ESTUFAGEM").ToString()
                        lblPesoBruto_House.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO").ToString()
                        lblM3_House.Text = ds.Tables(0).Rows(0).Item("VL_M3").ToString()
                    End If

                    ds = Con.ExecutarQuery("SELECT ID_BL,NR_BL,NR_PROCESSO,CONVERT(VARCHAR,DT_EMBARQUE,103)DT_EMBARQUE,CONVERT(VARCHAR,DT_CHEGADA,103)DT_CHEGADA,VL_PESO_BRUTO,VL_M3,(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM)ESTUFAGEM,(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_FRETE)SIGLA_MOEDA,(SELECT NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE WHERE ID_STATUS_FRETE_AGENTE = A.ID_STATUS_FRETE_AGENTE) STATUS_FRETE_AGENTE,ISNULL((SELECT NM_TIPO_DIVISAO_PROFIT FROM TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT = A.ID_PROFIT_DIVISAO),'') TIPO_DIVISAO_PROFIT FROM TB_BL A WHERE GRAU = 'M' AND ID_BL = " & ID_BL_MASTER)
                    If ds.Tables(0).Rows.Count > 0 Then
                        lblStatusComissoesMBL.Text = ds.Tables(0).Rows(0).Item("TIPO_DIVISAO_PROFIT").ToString()
                        lblMBL_Master.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                        lblMoeda_Master.Text = ds.Tables(0).Rows(0).Item("SIGLA_MOEDA").ToString()
                        lblTipoFrete_Master.Text = ds.Tables(0).Rows(0).Item("TIPO_PAGAMENTO").ToString()
                        lblEstufagem_Master.Text = ds.Tables(0).Rows(0).Item("ESTUFAGEM").ToString()
                        lblEmbarque_Master.Text = ds.Tables(0).Rows(0).Item("DT_EMBARQUE").ToString()
                        lblChegada_Master.Text = ds.Tables(0).Rows(0).Item("DT_CHEGADA").ToString()
                    End If

                    'EXIBE HBL
                    divHouse.Visible = True

                    'OCULTA MBL
                    divMaster.Visible = False

                    dsInvoice.DataBind()
                    dgvInvoiceHBL.DataBind()
                    dgvInvoiceHBL.Visible = True
                ElseIf Request.QueryString("T") = "M" Then
                    ds = Con.ExecutarQuery("SELECT ID_BL,GRAU,NR_BL,NR_PROCESSO,CONVERT(VARCHAR,DT_EMBARQUE,103)DT_EMBARQUE,CONVERT(VARCHAR,DT_CHEGADA,103)DT_CHEGADA,VL_PESO_BRUTO,VL_M3,(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM)ESTUFAGEM,(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,(SELECT SIGLA_MOEDA FROM TB_MOEDA WHERE ID_MOEDA = A.ID_MOEDA_FRETE)SIGLA_MOEDA,(SELECT NM_STATUS_FRETE_AGENTE FROM TB_STATUS_FRETE_AGENTE WHERE ID_STATUS_FRETE_AGENTE = A.ID_STATUS_FRETE_AGENTE) STATUS_FRETE_AGENTE,ISNULL((SELECT NM_TIPO_DIVISAO_PROFIT FROM TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT = A.ID_PROFIT_DIVISAO),'') TIPO_DIVISAO_PROFIT FROM TB_BL A WHERE GRAU = 'M' AND ID_BL = " & lblID_BL.Text)
                    If ds.Tables(0).Rows.Count > 0 Then
                        lblStatusComissoesMBL.Text = ds.Tables(0).Rows(0).Item("TIPO_DIVISAO_PROFIT").ToString()
                        lblGrau.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                        lblMBL.Text = ds.Tables(0).Rows(0).Item("NR_BL").ToString()
                        lblMoeda.Text = ds.Tables(0).Rows(0).Item("SIGLA_MOEDA").ToString()
                        lblTipoFrete.Text = ds.Tables(0).Rows(0).Item("TIPO_PAGAMENTO").ToString()
                        lblEstufagem.Text = ds.Tables(0).Rows(0).Item("ESTUFAGEM").ToString()
                        lblEmbarque.Text = ds.Tables(0).Rows(0).Item("DT_EMBARQUE").ToString()
                        lblChegada.Text = ds.Tables(0).Rows(0).Item("DT_CHEGADA").ToString()

                    End If

                    'EXIBE MBL
                    divMaster.Visible = True


                    'OCULTA HBL
                    divHouse.Visible = False

                    dsInvoice.DataBind()
                    dgvInvoiceMBL.DataBind()
                    dgvInvoiceMBL.Visible = True
                End If

            Else
                'ERRO
            End If

        End If

    End Sub

End Class