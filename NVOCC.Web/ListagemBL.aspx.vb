Imports System.Configuration
Public Class ListagemBL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If
        Con.Fechar()
    End Sub

    Private Sub lkInserirMaster_Click(sender As Object, e As EventArgs) Handles lkInserirMaster.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            Exit Sub
        Else
            Response.Redirect("CadastrarMaster.aspx")
        End If

    End Sub

    Private Sub lkInserirEmbarque_Click(sender As Object, e As EventArgs) Handles lkInserirEmbarque.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            Exit Sub
        Else
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=e")
        End If

    End Sub

    Private Sub lkInserirHouse_Click(sender As Object, e As EventArgs) Handles lkInserirHouse.Click
        divSuccessHouse.Visible = False
        divErroHouse.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroHouse.Visible = True
            lblErroHouse.Text = "Usuário não possui permissão."
            Exit Sub
        Else
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=h")
        End If

    End Sub

    Private Sub lkBLHouse_Click(sender As Object, e As EventArgs) Handles lkBLHouse.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            Exit Sub
        Else
            If txtIDHouse.Text = "" Then
                divErroHouse.Visible = True
                lblErroHouse.Text = "Selecione um registro!"
            Else
                Response.Redirect("EmissaoBL.aspx?id=" & txtIDHouse.Text)
            End If

        End If

    End Sub

    Private Sub rdServicoHouse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdServicoHouse.SelectedIndexChanged

        GridHouse()

        '        If rdServicoHouse.SelectedValue = 2 Then
        '            dsHouse.SelectCommand = "SELECT ID_BL,NR_PROCESSO,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,
        'ID_TIPO_PAGAMENTO,
        '(SELECT NM_TIPO_PAGAMENTO FROM TB_TIPO_PAGAMENTO WHERE ID_TIPO_PAGAMENTO = A.ID_TIPO_PAGAMENTO)TIPO_PAGAMENTO,
        'ID_TIPO_ESTUFAGEM,
        '(SELECT NM_TIPO_ESTUFAGEM FROM TB_TIPO_ESTUFAGEM WHERE ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM)TIPO_ESTUFAGEM,
        'ID_PARCEIRO_AGENTE,
        '(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_PARCEIRO_AGENTE)PARCEIRO_AGENTE,
        '(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_PARCEIRO_CLIENTE)PARCEIRO_CLIENTE,
        'DT_PREVISAO_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,
        'ID_NAVIO,
        '(SELECT NM_NAVIO FROM TB_NAVIO WHERE ID_NAVIO = A.ID_NAVIO)NAVIO,
        'ID_PARCEIRO_TRANSPORTADOR,
        '(SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = A.ID_PARCEIRO_TRANSPORTADOR)PARCEIRO_TRANSPORTADOR,
        'ID_BL_MASTER,
        'DT_REDESTINACAO,DT_DESCONSOLIDACAO,ID_WEEK
        'from TB_BL A
        'WHERE ID_SERVICO IN (4,5)"
        '            dgvHouse.DataBind()
        '        Else
        '            dgvHouse.DataBind()

        'End If
    End Sub
    Sub DUPLICAR(ID As String, TIPO As String)
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL (ID_BL_MASTER,NR_BL,GRAU,NR_PROCESSO,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,NR_CE,DT_CE,ID_TIPO_PAGAMENTO,ID_TIPO_ESTUFAGEM,ID_PARCEIRO_AGENTE,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_PARCEIRO_AGENCIA,ID_PARCEIRO_COLOADER,ID_PARCEIRO_COMISSARIA,ID_SERVICO,ID_USUARIO_ABERTURA,DT_ABERTURA,ID_USUARIO_CANCELAMENTO,DT_CANCELAMENTO,ID_MOEDA_FRETE,DT_CALCULO_FRETE,VL_CAMBIO_FRETE,VL_FRETE,VL_FRETE_CONVERT,VL_CARGA,ID_PARCEIRO_OPERADOR,ID_PARCEIRO_TRANSPORTADOR,DT_PREVISAO_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,DT_EMBARQUE,ID_NAVIO,NR_VIAGEM,ID_COTACAO,ID_TIPO_CARGA,ID_INCOTERM,VL_M3,QT_MERCADORIA,VL_PESO_BRUTO,VL_PESO_TAXADO,OB_OPERACIONAL_INTERNA,OB_COMERCIAL,OB_AGENTE_INTERNACIONAL,OB_CLIENTE,NR_FATURA_COURRIER,VL_PROFIT_DIVISAO,FL_DIVISAO_INFORMADA,DT_REDESTINACAO,DT_DESCONSOLIDACAO,ID_WEEK,DT_EMISSAO_BL,DT_EMISSAO_CONHECIMENTO,OB_REFERENCIA_AUXILIAR,OB_REFERENCIA_COMERCIAL,NM_RESUMO_MERCADORIA,ID_PARCEIRO_RODOVIARIO,DT_FLWP_LCL,ID_PARCEIRO_EXPORTADOR,VL_PESO_BRUTO_AGENTE,VL_M3_AGENTE,QT_MERCADORIA_AGENTE,DT_READY_DATE,DT_FORECAST_WH,DT_DRAFT_CUTOFF,DT_ARRIVE_WH,ID_MERCADORIA,ID_PARCEIRO_VENDEDOR,FL_FREE_HAND,DT_RECEBIMENTO_HBL,DT_RECEBIMENTO_MBL,CD_RASTREAMENTO_HBL,CD_RASTREAMENTO_MBL,DT_RETIRADA_COURRIER,NM_RETIRADO_POR_COURRIER,ID_STATUS_BL,ID_WEEK_CONTAINER,DT_ULTIMO_CALCULO_PROFIT,DT_ULTIMO_CALCULO_TAXAS,ID_PORTO_1T,ID_PORTO_2T,ID_PORTO_3T,ID_NAVIO_1T,ID_NAVIO_2T,ID_NAVIO_3T,NR_VIAGEM_1T,NR_VIAGEM_2T,NR_VIAGEM_3T,DT_1T,DT_2T,DT_3T,VL_TARIFA_MASTER,VL_TARIFA_MASTER_MINIMA, ID_PARCEIRO_ARMAZEM_ATRACACAO,ID_PARCEIRO_ARMAZEM_DESCARGA,ID_PARCEIRO_ARMAZEM_DESEMBARACO) SELECT ID_BL_MASTER,NR_BL,GRAU,NR_PROCESSO,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,NR_CE,DT_CE,ID_TIPO_PAGAMENTO,ID_TIPO_ESTUFAGEM,ID_PARCEIRO_AGENTE,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_PARCEIRO_AGENCIA,ID_PARCEIRO_COLOADER,ID_PARCEIRO_COMISSARIA,ID_SERVICO,ID_USUARIO_ABERTURA,DT_ABERTURA,ID_USUARIO_CANCELAMENTO,DT_CANCELAMENTO,ID_MOEDA_FRETE,DT_CALCULO_FRETE,VL_CAMBIO_FRETE,VL_FRETE,VL_FRETE_CONVERT,VL_CARGA,ID_PARCEIRO_OPERADOR,ID_PARCEIRO_TRANSPORTADOR,DT_PREVISAO_EMBARQUE,DT_PREVISAO_CHEGADA,DT_CHEGADA,DT_EMBARQUE,ID_NAVIO,NR_VIAGEM,ID_COTACAO,ID_TIPO_CARGA,ID_INCOTERM,VL_M3,QT_MERCADORIA,VL_PESO_BRUTO,VL_PESO_TAXADO,OB_OPERACIONAL_INTERNA,OB_COMERCIAL,OB_AGENTE_INTERNACIONAL,OB_CLIENTE,NR_FATURA_COURRIER,VL_PROFIT_DIVISAO,FL_DIVISAO_INFORMADA,DT_REDESTINACAO,DT_DESCONSOLIDACAO,ID_WEEK,DT_EMISSAO_BL,DT_EMISSAO_CONHECIMENTO,OB_REFERENCIA_AUXILIAR,OB_REFERENCIA_COMERCIAL,NM_RESUMO_MERCADORIA,ID_PARCEIRO_RODOVIARIO,DT_FLWP_LCL,ID_PARCEIRO_EXPORTADOR,VL_PESO_BRUTO_AGENTE,VL_M3_AGENTE,QT_MERCADORIA_AGENTE,DT_READY_DATE,DT_FORECAST_WH,DT_DRAFT_CUTOFF,DT_ARRIVE_WH,ID_MERCADORIA,ID_PARCEIRO_VENDEDOR,FL_FREE_HAND,DT_RECEBIMENTO_HBL,DT_RECEBIMENTO_MBL,CD_RASTREAMENTO_HBL,CD_RASTREAMENTO_MBL,DT_RETIRADA_COURRIER,NM_RETIRADO_POR_COURRIER,ID_STATUS_BL,ID_WEEK_CONTAINER,DT_ULTIMO_CALCULO_PROFIT,DT_ULTIMO_CALCULO_TAXAS,ID_PORTO_1T,ID_PORTO_2T,ID_PORTO_3T,ID_NAVIO_1T,ID_NAVIO_2T,ID_NAVIO_3T,NR_VIAGEM_1T,NR_VIAGEM_2T,NR_VIAGEM_3T,DT_1T,DT_2T,DT_3T,VL_TARIFA_MASTER,VL_TARIFA_MASTER_MINIMA, ID_PARCEIRO_ARMAZEM_ATRACACAO,ID_PARCEIRO_ARMAZEM_DESCARGA,ID_PARCEIRO_ARMAZEM_DESEMBARACO from TB_BL WHERE ID_BL =" & ID & " ; Select SCOPE_IDENTITY() as ID_BL ")
        Dim NOVA_BL As String = ds.Tables(0).Rows(0).Item("ID_BL")

        If TIPO = "HOUSE" Or TIPO = "EMBARQUE" Then
            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA )  select " & NOVA_BL & ",ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA from TB_CARGA_BL WHERE ID_BL = " & ID & " ; INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA)  select " & NOVA_BL & ",ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA from TB_BL_TAXA WHERE ID_BL = " & ID & " ; INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE)  select " & NOVA_BL & ",NR_REFERENCIA_CLIENTE from TB_REFERENCIA_CLIENTE WHERE ID_BL = " & ID)

        ElseIf TIPO = "MASTER" Then

            ds = Con.ExecutarQuery("INSERT INTO TB_CNTR_BL (ID_BL_MASTER,NR_CNTR,NR_LACRE,VL_PESO_TARA,ID_TIPO_CNTR,QT_DIAS_FREETIME,DT_INICIAL_FREETIME,DT_FINAL_FREETIME,DT_DEVOLUCAO_CNTR,QT_DIAS_DEMURRAGE,DS_STATUS_TERC,DT_STATUS_TERC,DT_VENCIMENTO_FATURA_TERC,DT_PAGAMENTO_FATURA_TERC,FL_DEMURRAGE_FINALIZADA,DS_OBSERVACAO_DEMUR,DT_EXPORTACAO_TERCEIRIZADA,VL_FATURA_TERC)  select " & NOVA_BL & ",NR_CNTR,NR_LACRE,VL_PESO_TARA,ID_TIPO_CNTR,QT_DIAS_FREETIME,DT_INICIAL_FREETIME,DT_FINAL_FREETIME,DT_DEVOLUCAO_CNTR,QT_DIAS_DEMURRAGE,DS_STATUS_TERC,DT_STATUS_TERC,DT_VENCIMENTO_FATURA_TERC,DT_PAGAMENTO_FATURA_TERC,FL_DEMURRAGE_FINALIZADA,DS_OBSERVACAO_DEMUR,DT_EXPORTACAO_TERCEIRIZADA,VL_FATURA_TERC from TB_CNTR_BL
WHERE ID_BL_MASTER =  " & ID & " ; INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA)  select " & NOVA_BL & ",ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA from TB_BL_TAXA WHERE ID_BL = " & ID)

        End If

    End Sub
    Sub GridHouse()

        Dim sql As String
        'AGENCIAMENTO DE IMPORTACAO AEREO
        If rdTransporteHouse.SelectedValue = 2 And rdServicoHouse.SelectedValue = 1 Then
            sql = "SELECT * FROM [dbo].[View_House] WHERE ID_SERVICO = 2"


            'AGENCIAMENTO DE IMPORTACAO MARITIMA
        ElseIf rdTransporteHouse.SelectedValue = 1 And rdServicoHouse.SelectedValue = 1 Then
            sql = "SELECT * FROM [dbo].[View_House] WHERE ID_SERVICO = 1"


            'AGENCIAMENTO DE EXPORTACAO MARITIMA
        ElseIf rdTransporteHouse.SelectedValue = 1 And rdServicoHouse.SelectedValue = 2 Then
            sql = "SELECT * FROM [dbo].[View_House] WHERE ID_SERVICO = 4"


            'AGENCIAMENTO DE EXPORTAÇÃO AEREO
        ElseIf rdTransporteHouse.SelectedValue = 2 And rdServicoHouse.SelectedValue = 2 Then
            sql = "SELECT * FROM [dbo].[View_House] WHERE ID_SERVICO = 5"
        End If

        dsHouse.SelectCommand = sql
        dgvHouse.DataBind()

    End Sub

    Sub GridEmbarque()

        Dim sql As String
        'AGENCIAMENTO DE IMPORTACAO AEREO
        If rdTRansporteEmbarque.SelectedValue = 2 And rdServicoEmbarque.SelectedValue = 1 Then
            sql = "SELECT * FROM [dbo].[View_Embarque] WHERE ID_SERVICO = 2"


            'AGENCIAMENTO DE IMPORTACAO MARITIMA
        ElseIf rdTRansporteEmbarque.SelectedValue = 1 And rdServicoEmbarque.SelectedValue = 1 Then
            sql = "SELECT * FROM [dbo].[View_Embarque] WHERE ID_SERVICO = 1"


            'AGENCIAMENTO DE EXPORTACAO MARITIMA
        ElseIf rdTRansporteEmbarque.SelectedValue = 1 And rdServicoEmbarque.SelectedValue = 2 Then
            sql = "SELECT * FROM [dbo].[View_Embarque] WHERE ID_SERVICO = 4"


            'AGENCIAMENTO DE EXPORTAÇÃO AEREO
        ElseIf rdTRansporteEmbarque.SelectedValue = 2 And rdServicoEmbarque.SelectedValue = 2 Then
            sql = "SELECT * FROM [dbo].[View_Embarque] WHERE ID_SERVICO = 5"
        End If

        dsEmbarque.SelectCommand = sql
        dgvEmbarque.DataBind()

    End Sub

    Sub GridMaster()

        Dim sql As String
        'AGENCIAMENTO DE IMPORTACAO AEREO
        If rdTransporteMaster.SelectedValue = 2 And rdServicoMaster.SelectedValue = 1 Then
            sql = "SELECT * FROM [dbo].[View_Master] WHERE ID_SERVICO = 2"


            'AGENCIAMENTO DE IMPORTACAO MARITIMA
        ElseIf rdTransporteMaster.SelectedValue = 1 And rdServicoMaster.SelectedValue = 1 Then
            sql = "SELECT * FROM [dbo].[View_Master] WHERE ID_SERVICO = 1"


            'AGENCIAMENTO DE EXPORTACAO MARITIMA
        ElseIf rdTransporteMaster.SelectedValue = 1 And rdServicoMaster.SelectedValue = 2 Then
            sql = "SELECT * FROM [dbo].[View_Master] WHERE ID_SERVICO = 4"


            'AGENCIAMENTO DE EXPORTAÇÃO AEREO
        ElseIf rdTransporteMaster.SelectedValue = 2 And rdServicoMaster.SelectedValue = 2 Then
            sql = "SELECT * FROM [dbo].[View_Master] WHERE ID_SERVICO = 5"
        End If

        dsMaster.SelectCommand = sql
        dgvMaster.DataBind()

    End Sub
    Private Sub rdTransporteHouse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdTransporteHouse.SelectedIndexChanged
        GridHouse()

    End Sub

    Private Sub btnPesquisaHouse_Click(sender As Object, e As EventArgs) Handles btnPesquisaHouse.Click
        divSuccessHouse.Visible = False
        divErroHouse.Visible = False

        If ddlFiltroHouse.SelectedValue = 0 Then
            dgvHouse.DataBind()
        ElseIf ddlFiltroHouse.SelectedValue = 1 Then
            dsHouse.SelectCommand = "select * from [dbo].[View_House] WHERE NR_PROCESSO LIKE '%" & txtPesquisaHouse.Text & "%'"
        ElseIf ddlFiltroHouse.SelectedValue = 2 Then
            dsHouse.SelectCommand = "select * from [dbo].[View_House] WHERE TIPO_ESTUFAGEM LIKE '%" & txtPesquisaHouse.Text & "%' "
        ElseIf ddlFiltroHouse.SelectedValue = 3 Then
            dsHouse.SelectCommand = "select * from [dbo].[View_House] WHERE PARCEIRO_CLIENTE LIKE '%" & txtPesquisaHouse.Text & "%' "
        ElseIf ddlFiltroHouse.SelectedValue = 4 Then
            dsHouse.SelectCommand = "select * from [dbo].[View_House] WHERE BL_MASTER LIKE '%" & txtPesquisaHouse.Text & "%'"
        End If
        dgvHouse.DataBind()

    End Sub

    Protected Sub dgvHouse_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvHouse.DataSource = Session("TaskTable")
            dgvHouse.DataBind()
            dgvHouse.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub dgvEmbarque_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvEmbarque.DataSource = Session("TaskTable")
            dgvEmbarque.DataBind()
            dgvEmbarque.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub dgvMaster_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvMaster.DataSource = Session("TaskTable")
            dgvMaster.DataBind()
            dgvMaster.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Private Function GetSortDirection(ByVal column As String) As String
        Dim sortDirection As String = "ASC"
        Dim sortExpression As String = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then

            If sortExpression = column Then
                Dim lastDirection As String = TryCast(ViewState("SortDirection"), String)

                If (lastDirection IsNot Nothing) AndAlso (lastDirection = "ASC") Then
                    sortDirection = "DESC"
                End If
            End If
        End If

        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column
        Return sortDirection
    End Function

    Private Sub dgvHouse_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvHouse.RowCommand
        divSuccessHouse.Visible = False
        divErroHouse.Visible = False
        If e.CommandName = "Selecionar" Then
            If txtlinhaHouse.Text <> "" Then
                dgvHouse.Rows(txtlinhaHouse.Text).CssClass = "Normal"
            End If
            Dim ID As String = e.CommandArgument


            txtIDHouse.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinhaHouse.Text = ID.Substring(ID.IndexOf("|"))
            txtlinhaHouse.Text = txtlinhaHouse.Text.Replace("|", "")


            For i As Integer = 0 To dgvHouse.Rows.Count - 1
                dgvHouse.Rows(txtlinhaHouse.Text).CssClass = "Normal"

            Next

            dgvHouse.Rows(txtlinhaHouse.Text).CssClass = "selected1"

        End If
    End Sub

    Private Sub lkAlterarHouse_Click(sender As Object, e As EventArgs) Handles lkAlterarHouse.Click
        divSuccessHouse.Visible = False
        divErroHouse.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroHouse.Visible = True
            lblErroHouse.Text = "Usuário não possui permissão."

            Exit Sub
        Else
            If txtIDHouse.Text = "" Then
                divErroHouse.Visible = True
                lblErroHouse.Text = "Selecione o registro que deseja editar!"
            Else
                Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=h&id={0}"
                url = String.Format(url, txtIDHouse.Text)
                Response.Redirect(url)
            End If
        End If

    End Sub

    'ROTINA A SER DEFINIDA
    Private Sub lkRemoverHouse_Click(sender As Object, e As EventArgs) Handles lkRemoverHouse.Click
        divSuccessHouse.Visible = False
        divErroHouse.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            lblErroHouse.Text = "Usuário não tem permissão para realizar exclusões"
            divErroHouse.Visible = True
        Else

            If txtIDHouse.Text = "" Then
                divErroHouse.Visible = True
                lblErroHouse.Text = "Selecione o registro que deseja remover!"
            Else
                Con.ExecutarQuery("DELETE FROM TB_BL WHERE ID_BL = " & txtIDHouse.Text)
                lblSuccessHouse.Text = "Registro deletado!"
                divSuccessHouse.Visible = True
                dgvHouse.DataBind()
            End If

        End If
        Con.Fechar()

    End Sub

    'ROTINA A SER DEFINIDA
    Private Sub lkCancelaHouse_Click(sender As Object, e As EventArgs) Handles lkCancelaHouse.Click

    End Sub

    Private Sub lkDuplicarHouse_Click(sender As Object, e As EventArgs) Handles lkDuplicarHouse.Click
        divSuccessHouse.Visible = False
        divErroHouse.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroHouse.Visible = True
            lblErroHouse.Text = "Usuário não possui permissão."

            Exit Sub
        Else
            If txtIDHouse.Text = "" Then
                divErroHouse.Visible = True
                lblErroHouse.Text = "Selecione o registro que deseja duplicar!"
            Else
                DUPLICAR(txtIDHouse.Text, "HOUSE")
                dgvHouse.DataBind()
                divSuccessHouse.Visible = True
                lblSuccessHouse.Text = "Item duplicado com sucesso!"
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub lkCalcularHouse_Click(sender As Object, e As EventArgs) Handles lkCalcularHouse.Click
        divSuccessHouse.Visible = False
        divErroHouse.Visible = False
        If txtIDHouse.Text = "" Then
            divErroHouse.Visible = True
            lblErroHouse.Text = "Selecione um registro para calcular!"
        Else
            Dim dataatual As Date = Now.Date.ToString("dd/MM/yyyy")

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(DT_CAMBIO)QTD FROM [FN_TAXAS_BL](2131) 
WHERE DT_CAMBIO <> Convert(VARCHAR, GETDATE(), 103)")
            If ds.Tables(0).Rows(0).Item("QTD") > 0 Then
                divErroHouse.Visible = True
                lblErroHouse.Text = "Não há valor de moeda de câmbio cadastrado com a data atual."
                Exit Sub
            Else

                ds = Con.ExecutarQuery("Select ID_BL_TAXA FROM [FN_TAXAS_BL](" & txtIDHouse.Text & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    For Each linha As DataRow In ds.Tables(0).Rows

                        Dim Calcula As New CalculaBL
                        Dim retorno As String = Calcula.Calcular(linha.Item("ID_BL_TAXA"))

                        If retorno = "BL calculada com sucesso!" Then
                            lblSuccessHouse.Text = retorno
                            divSuccessHouse.Visible = True
                        End If
                    Next

                End If

            End If
        End If

    End Sub

    Private Sub dgvMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvMaster.RowCommand
        divSuccessMaster.Visible = False
        divErroMaster.Visible = False
        If e.CommandName = "Selecionar" Then
            If txtLinhaMaster.Text <> "" Then
                dgvMaster.Rows(txtLinhaMaster.Text).CssClass = "Normal"
            End If
            Dim ID As String = e.CommandArgument


            txtID_Master.Text = ID.Substring(0, ID.IndexOf("|"))

            txtLinhaMaster.Text = ID.Substring(ID.IndexOf("|"))
            txtLinhaMaster.Text = txtLinhaMaster.Text.Replace("|", "")


            For i As Integer = 0 To dgvMaster.Rows.Count - 1
                dgvMaster.Rows(txtLinhaMaster.Text).CssClass = "Normal"

            Next

            dgvMaster.Rows(txtLinhaMaster.Text).CssClass = "selected1"

        End If
    End Sub

    Private Sub dgvEmbarque_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvEmbarque.RowCommand
        divSuccess.Visible = False
        divErroEmbarque.Visible = False
        If e.CommandName = "Selecionar" Then
            If txtLinhaEmbarque.Text <> "" Then
                dgvEmbarque.Rows(txtLinhaEmbarque.Text).CssClass = "Normal"
            End If
            Dim ID As String = e.CommandArgument


            txtID_Embarque.Text = ID.Substring(0, ID.IndexOf("|"))

            txtLinhaEmbarque.Text = ID.Substring(ID.IndexOf("|"))
            txtLinhaEmbarque.Text = txtLinhaEmbarque.Text.Replace("|", "")


            For i As Integer = 0 To dgvEmbarque.Rows.Count - 1
                dgvEmbarque.Rows(txtLinhaEmbarque.Text).CssClass = "Normal"

            Next

            dgvEmbarque.Rows(txtLinhaEmbarque.Text).CssClass = "selected1"

        End If
    End Sub

    Private Sub lkAlterarEmbarque_Click(sender As Object, e As EventArgs) Handles lkAlterarEmbarque.Click
        divSuccessEmbarque.Visible = False
        divErroEmbarque.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 And FL_ATUALIZAR = 1 And ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroEmbarque.Visible = True
            lblErroEmbarque.Text = "Usuário não possui permissão."

            Exit Sub
        Else
            If txtID_Embarque.Text = "" Then
                divErroEmbarque.Visible = True
                lblErroEmbarque.Text = "Selecione o registro que deseja editar!"
            Else
                Dim url As String = "CadastrarEmbarqueHouse.aspx?tipo=e&id={0}"
                url = String.Format(url, txtID_Embarque.Text)
                Response.Redirect(url)
            End If
        End If

    End Sub

    Private Sub lkAlterarMaster_Click(sender As Object, e As EventArgs) Handles lkAlterarMaster.Click
        divSuccessMaster.Visible = False
        divErroMaster.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 And FL_ATUALIZAR = 1 And ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroMaster.Visible = True
            lblErroMaster.Text = "Usuário não possui permissão."

            Exit Sub
        Else
            If txtID_Master.Text = "" Then
                divErroMaster.Visible = True
                lblErroMaster.Text = "Selecione o registro que deseja editar!"
            Else
                Dim url As String = "CadastrarMaster.aspx?id={0}"
                url = String.Format(url, txtID_Master.Text)
                Response.Redirect(url)
            End If
        End If

    End Sub

    Private Sub rdTRansporteEmbarque_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdTRansporteEmbarque.SelectedIndexChanged
        GridEmbarque()
    End Sub

    Private Sub rdServicoEmbarque_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdServicoEmbarque.SelectedIndexChanged
        GridEmbarque()
    End Sub

    Private Sub rdServicoMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdServicoMaster.SelectedIndexChanged
        GridMaster()
    End Sub

    Private Sub rdTransporteMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdTransporteMaster.SelectedIndexChanged
        GridMaster()
    End Sub

    Private Sub btnPesquisaEmbarque_Click(sender As Object, e As EventArgs) Handles btnPesquisaEmbarque.Click
        If ddlFiltroEmbarque.SelectedValue = 0 Or txtPesquisaEmbarque.Text = "" Then
            dgvEmbarque.DataBind()
        Else
            Dim FILTRO As String


            If ddlFiltroEmbarque.SelectedValue = 1 Then
                FILTRO = " NR_PROCESSO Like '%" & txtPesquisaEmbarque.Text & "%' "
            ElseIf ddlFiltroEmbarque.SelectedValue = 2 Then
                FILTRO = " TIPO_ESTUFAGEM LIKE '%" & txtPesquisaEmbarque.Text & "%' "
            ElseIf ddlFiltroEmbarque.SelectedValue = 3 Then
                FILTRO = " PARCEIRO_CLIENTE LIKE '%" & txtPesquisaEmbarque.Text & "%' "
                'ElseIf ddlFiltroEmbarque.SelectedValue = 4 Then
                '    FILTRO = " ID_BL_MASTER LIKE '%" & txtPesquisaEmbarque.Text & "%' "
            End If

            Dim sql As String = "select * from [dbo].[View_Embarque] WHERE " & FILTRO
            dsEmbarque.SelectCommand = sql
            dgvEmbarque.DataBind()

        End If
    End Sub

    Private Sub btnPesquisaMaster_Click(sender As Object, e As EventArgs) Handles btnPesquisaMaster.Click
        If ddFiltroMaster.SelectedValue = 0 Or txtPesquisaMaster.Text = "" Then
            dgvMaster.DataBind()
        Else
            Dim FILTRO As String


            If ddFiltroMaster.SelectedValue = 1 Then
                FILTRO = " NR_BL LIKE '%" & txtPesquisaMaster.Text & "%' "
            ElseIf ddFiltroMaster.SelectedValue = 2 Then
                FILTRO = " TIPO_ESTUFAGEM LIKE '%" & txtPesquisaMaster.Text & "%' "
            ElseIf ddFiltroMaster.SelectedValue = 3 Then
                FILTRO = " Origem LIKE '%" & txtPesquisaMaster.Text & "%' "
            ElseIf ddFiltroMaster.SelectedValue = 4 Then
                FILTRO = " Destino LIKE '%" & txtPesquisaMaster.Text & "%' "
            End If

            Dim sql As String = "select * from [dbo].[View_Master] WHERE " & FILTRO
            dsMaster.SelectCommand = sql
            dgvMaster.DataBind()

        End If
    End Sub

    Private Sub btnFiltrar_Embarque_Click(sender As Object, e As EventArgs) Handles btnFiltrar_Embarque.Click
        Dim sql As String = "select * from [dbo].[View_BL] WHERE ID_BL_MASTER IS NOT NULL AND"

        If txtAgente_Embarque.Text = "" And
            txtExportador_Embarque.Text = "" And
            txtTransportador_Embarque.Text = "" And
            txtCliente_Embarque.Text = "" And
            txtRefCliente_Embarque.Text = "" And
            txtEstufagem_Embarque.Text = "" And
            txtOrigem_Embarque.Text = "" And
            txtDestino_Embarque.Text = "" And
            txtTipoFrete_Embarque.Text = "" And
            txtNavio_Embarque.Text = "" And
            txtNavioTransb_Embarque.Text = "" And
            txtPrevInicialEmbarque_Embarque.Text = "" And
            txtPrevFimEmbarque_Embarque.Text = "" And
            txtInicialEmbarque_Embarque.Text = "" And
            txtFimEmbarque_Embarque.Text = "" And
            txtPrevInicialChegada_Embarque.Text = "" And
            txtPrevFimChegada_Embarque.Text = "" And
            txtInicialChegada_Embarque.Text = "" And
            txtFimChegada_Embarque.Text = "" Then

            dgvEmbarque.DataBind()

        Else

            If txtAgente_Embarque.Text <> "" Then
                sql &= " PARCEIRO_AGENTE LIKE '%" & txtAgente_Embarque.Text & "%' "

            ElseIf txtExportador_Embarque.Text <> "" Then
                sql &= " PARCEIRO_EXPORTADOR LIKE '%" & txtExportador_Embarque.Text & "%' "

            ElseIf txtTransportador_Embarque.Text <> "" Then
                sql &= " PARCEIRO_TRANSPORTADOR LIKE '%" & txtTransportador_Embarque.Text & "%' "

            ElseIf txtCliente_Embarque.Text <> "" Then
                sql &= " PARCEIRO_CLIENTE LIKE '%" & txtCliente_Embarque.Text & "%' "

            ElseIf txtRefCliente_Embarque.Text <> "" Then
                sql &= " NR_REFERENCIA_CLIENTE LIKE '%" & txtRefCliente_Embarque.Text & "%' "

            ElseIf txtEstufagem_Embarque.Text <> "" Then
                sql &= " TIPO_ESTUFAGEM LIKE '%" & txtEstufagem_Embarque.Text & "%' "

            ElseIf txtOrigem_Embarque.Text <> "" Then
                sql &= " ORIGEM LIKE '%" & txtOrigem_Embarque.Text & "%' "

            ElseIf txtDestino_Embarque.Text <> "" Then
                sql &= " DESTINO LIKE '%" & txtDestino_Embarque.Text & "%' "

            ElseIf txtTipoFrete_Embarque.Text <> "" Then
                sql &= " TIPO_FRETE LIKE '%" & txtTipoFrete_Embarque.Text & "%' "

            ElseIf txtNavio_Embarque.Text <> "" Then
                sql &= " NAVIO LIKE '%" & txtNavio_Embarque.Text & "%' "

            ElseIf txtNavioTransb_Embarque.Text <> "" Then
                sql &= " NAVIO_1T LIKE '%" & txtNavioTransb_Embarque.Text & "%' OR  NAVIO_2T LIKE '%" & txtNavioTransb_Embarque.Text & "%' O NAVIO_3T LIKE '%" & txtNavioTransb_Embarque.Text & "%'  "

            ElseIf txtPrevInicialEmbarque_Embarque.Text <> "" And txtPrevFimEmbarque_Embarque.Text <> "" Then
                sql &= "DT_PREVISAO_EMBARQUE BETWEEN CONVERT(date,'" & txtPrevInicialEmbarque_Embarque.Text & "',103) AND CONVERT(date,'" & txtPrevFimEmbarque_Embarque.Text & "',103) "

            ElseIf txtInicialEmbarque_Embarque.Text <> "" And txtFimEmbarque_Embarque.Text <> "" Then
                sql &= "DT_EMBARQUE BETWEEN CONVERT(date,'" & txtInicialEmbarque_Embarque.Text & "',103) AND CONVERT(date,'" & txtFimEmbarque_Embarque.Text & "',103) "

            ElseIf txtPrevInicialChegada_Embarque.Text <> "" And txtPrevFimChegada_Embarque.Text <> "" Then
                sql &= "DT_PREVISAO_CHEGADA BETWEEN CONVERT(date,'" & txtPrevInicialChegada_Embarque.Text & "',103) AND CONVERT(date,'" & txtPrevFimChegada_Embarque.Text & "',103) "

            ElseIf txtInicialChegada_Embarque.Text <> "" And txtFimChegada_Embarque.Text <> "" Then
                sql &= "DT_CHEGADA BETWEEN CONVERT(date,'" & txtInicialChegada_Embarque.Text & "',103) AND CONVERT(date,'" & txtFimChegada_Embarque.Text & "',103) "

            End If

            dsEmbarque.SelectCommand = sql
            dgvEmbarque.DataBind()

        End If

        FecharFiltros()

    End Sub

    Private Sub btnFiltrar_House_Click(sender As Object, e As EventArgs) Handles btnFiltrar_House.Click

        Dim sql As String = "select * from [dbo].[View_BL] WHERE ID_BL_MASTER IS NOT NULL "

        If txtAgente_House.Text = "" And
            txtExportador_House.Text = "" And
            txtTransportador_House.Text = "" And
        txtCliente_House.Text = "" And
        txtRefCliente_House.Text = "" And
        txtEstufagem_House.Text = "" And
        txtOrigem_House.Text = "" And
        txtDestino_House.Text = "" And
        txtTipoFrete_House.Text = "" And
        txtNavio_House.Text = "" And
        txtNavioTransb_House.Text = "" And
        txtInicioPrevEmbarque_House.Text = "" And
        txtFimPrevEmbarque_House.Text = "" And
        txtInicioEmbarque_House.Text = "" And
        txtFimEmbarque_House.Text = "" And
        txtInicioPrevChegada_House.Text = "" And
        txtFimPrevChegada_House.Text = "" And
        txtInicioChegada_House.Text = "" And
        txtFimChegada_House.Text = "" Then
            dgvHouse.DataBind()
        Else
            If txtAgente_House.Text <> "" Then
                sql &= "AND PARCEIRO_AGENTE LIKE '%" & txtAgente_House.Text & "%' "

            ElseIf txtExportador_House.Text <> "" Then
                sql &= "AND PARCEIRO_EXPORTADOR LIKE '%" & txtExportador_House.Text & "%' "

            ElseIf txtTransportador_House.Text <> "" Then
                sql &= "AND PARCEIRO_TRANSPORTADOR LIKE '%" & txtTransportador_House.Text & "%' "

            ElseIf txtCliente_House.Text <> "" Then
                sql &= "AND PARCEIRO_CLIENTE LIKE '%" & txtCliente_House.Text & "%' "

            ElseIf txtRefCliente_House.Text <> "" Then
                sql &= "AND NR_REFERENCIA_CLIENTE LIKE '%" & txtRefCliente_House.Text & "%' "

            ElseIf txtEstufagem_House.Text <> "" Then
                sql &= "AND TIPO_ESTUFAGEM LIKE '%" & txtEstufagem_House.Text & "%' "

            ElseIf txtOrigem_House.Text <> "" Then
                sql &= "AND ORIGEM LIKE '%" & txtOrigem_House.Text & "%' "

            ElseIf txtDestino_House.Text <> "" Then
                sql &= "AND DESTINO LIKE '%" & txtDestino_House.Text & "%' "

            ElseIf txtTipoFrete_House.Text <> "" Then
                sql &= "AND TIPO_PAGAMENTO LIKE '%" & txtTipoFrete_House.Text & "%' "

            ElseIf txtNavio_House.Text <> "" Then
                sql &= "AND NAVIO LIKE '%" & txtNavio_House.Text & "%' "

            ElseIf txtNavioTransb_House.Text <> "" Then
                sql &= "AND NAVIO_1T LIKE '%" & txtNavioTransb_House.Text & "%' OR  NAVIO_2T LIKE '%" & txtNavioTransb_House.Text & "%' O NAVIO_3T LIKE '%" & txtNavioTransb_House.Text & "%'  "

            ElseIf txtInicioPrevEmbarque_House.Text <> "" And txtFimPrevEmbarque_House.Text <> "" Then
                sql &= "AND DT_PREVISAO_EMBARQUE BETWEEN CONVERT(date,'" & txtInicioPrevEmbarque_House.Text & "',103) AND CONVERT(date,'" & txtFimPrevEmbarque_House.Text & "',103) "

            ElseIf txtInicioEmbarque_House.Text <> "" And txtFimEmbarque_House.Text <> "" Then
                sql &= "AND DT_EMBARQUE BETWEEN CONVERT(date,'" & txtInicioEmbarque_House.Text & "',103) AND CONVERT(date,'" & txtFimEmbarque_House.Text & "',103) "

            ElseIf txtInicioPrevChegada_House.Text <> "" And txtFimPrevChegada_House.Text <> "" Then
                sql &= "AND DT_PREVISAO_CHEGADA BETWEEN CONVERT(date,'" & txtInicioPrevChegada_House.Text & "',103) AND CONVERT(date,'" & txtFimPrevChegada_House.Text & "',103) "

            ElseIf txtInicioChegada_House.Text <> "" And txtFimChegada_House.Text <> "" Then
                sql &= "AND DT_CHEGADA BETWEEN CONVERT(date,'" & txtInicioChegada_House.Text & "',103) AND CONVERT(date,'" & txtFimChegada_House.Text & "',103) "

            End If

            dsHouse.SelectCommand = sql
            dgvHouse.DataBind()

        End If

        FecharFiltros()

    End Sub

    Private Sub btnFechar_House_Click(sender As Object, e As EventArgs) Handles btnFechar_House.Click
        FecharFiltros()


    End Sub
    Sub FecharFiltros()
        txtAgente_House.Text = ""
        txtExportador_House.Text = ""
        txtTransportador_House.Text = ""
        txtCliente_House.Text = ""
        txtRefCliente_House.Text = ""
        txtEstufagem_House.Text = ""
        txtOrigem_House.Text = ""
        txtDestino_House.Text = ""
        txtTipoFrete_House.Text = ""
        txtNavio_House.Text = ""
        txtNavioTransb_House.Text = ""
        txtInicioPrevEmbarque_House.Text = ""
        txtFimPrevEmbarque_House.Text = ""
        txtInicioEmbarque_House.Text = ""
        txtFimEmbarque_House.Text = ""
        txtInicioPrevChegada_House.Text = ""
        txtFimPrevChegada_House.Text = ""
        txtInicioChegada_House.Text = ""
        txtFimChegada_House.Text = ""

        mpe_House.Hide()


        txtAgente_Embarque.Text = ""
        txtExportador_Embarque.Text = ""
        txtTransportador_Embarque.Text = ""
        txtCliente_Embarque.Text = ""
        txtRefCliente_Embarque.Text = ""
        txtEstufagem_Embarque.Text = ""
        txtOrigem_Embarque.Text = ""
        txtDestino_Embarque.Text = ""
        txtTipoFrete_Embarque.Text = ""
        txtNavio_Embarque.Text = ""
        txtNavioTransb_Embarque.Text = ""
        txtPrevInicialEmbarque_Embarque.Text = ""
        txtPrevFimEmbarque_Embarque.Text = ""
        txtInicialEmbarque_Embarque.Text = ""
        txtFimEmbarque_Embarque.Text = ""
        txtPrevInicialChegada_Embarque.Text = ""
        txtPrevFimChegada_Embarque.Text = ""
        txtInicialChegada_Embarque.Text = ""
        txtFimChegada_Embarque.Text = ""


        mpe_Embarque.Hide()
    End Sub
    Private Sub btnFechar_Embarque_Click(sender As Object, e As EventArgs) Handles btnFechar_Embarque.Click
        FecharFiltros()

    End Sub

    Private Sub lkDuplicarMaster_Click(sender As Object, e As EventArgs) Handles lkDuplicarMaster.Click
        divSuccessMaster.Visible = False
        divErroMaster.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroMaster.Visible = True
            lblErroMaster.Text = "Usuário não possui permissão."

            Exit Sub
        Else
            If txtID_Master.Text = "" Then
                divErroMaster.Visible = True
                lblErroMaster.Text = "Selecione o registro que deseja duplicar!"
            Else
                DUPLICAR(txtID_Master.Text, "MASTER")
                dgvMaster.DataBind()
                divSuccessMaster.Visible = True
                lblSuccessMaster.Text = "Item duplicado com sucesso!"
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub lkDuplicarEmbarque_Click(sender As Object, e As EventArgs) Handles lkDuplicarEmbarque.Click
        divSuccessEmbarque.Visible = False
        divErroEmbarque.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroEmbarque.Visible = True
            lblErroEmbarque.Text = "Usuário não possui permissão."

            Exit Sub
        Else
            If txtID_Embarque.Text = "" Then
                divErroEmbarque.Visible = True
                lblErroEmbarque.Text = "Selecione o registro que deseja duplicar!"
            Else
                DUPLICAR(txtID_Embarque.Text, "EMBARQUE")
                dgvEmbarque.DataBind()
                divSuccessEmbarque.Visible = True
                lblSuccessEmbarque.Text = "Item duplicado com sucesso!"
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub lkCourrierHouse_Click(sender As Object, e As EventArgs) Handles lkCourrierHouse.Click
        Response.Redirect("Courrier.aspx")
    End Sub

    Private Sub lkTracking_Click(sender As Object, e As EventArgs) Handles lkTracking.Click
        'divSuccessHouse.Visible = False
        'divErroHouse.Visible = False
        'Dim SiteApiTracking As String = ConfigurationSettings.AppSettings("SiteApiTracking")
        'Dim Con As New Conexao_sql
        'Con.Conectar()
        'Dim ds As DataSet = Con.ExecutarQuery("SELECT BL_TOKEN FROM [TB_BL] WHERE BL_TOKEN IS NOT NULL AND ID_BL = " & txtID_Master.Text)

        'If ds.Tables(0).Rows.Count > 0 Then
        '    ' Response.Redirect("https://localhost:8081/" & ds.Tables(0).Rows(0).Item("BL_TOKEN"))
        '    Response.Redirect(SiteApiTracking & ds.Tables(0).Rows(0).Item("BL_TOKEN"))

        'Else
        '    Response.Redirect(SiteApiTracking & 0)

        '    'divErroHouse.Visible = True
        '    'lblErroHouse.Text = "BL não cadastrada no Logcomex."
        '    'Exit Sub
        'End If
        divSuccessMaster.Visible = False
        divErroMaster.Visible = False

        Session("NR_BL") = 0
        Session("TRAKING_BL") = 0

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_BL,TRAKING_BL FROM [TB_BL] WHERE NR_BL IS NOT NULL AND ID_BL = " & txtID_Master.Text)
        'Dim ds As DataSet = Con.ExecutarQuery("SELECT NR_BL FROM [TB_BL] WHERE NR_BL IS NOT NULL AND GRAU = 'M'")
        If Not IsDBNull(ds.Tables(0).Rows(0).Item("TRAKING_BL")) Then
            Session("NR_BL") = ds.Tables(0).Rows(0).Item("NR_BL")
            Session("TRAKING_BL") = ds.Tables(0).Rows(0).Item("TRAKING_BL")

            Response.Redirect("RastreioBL.aspx")

        Else
            divErroMaster.Visible = True
            lblErroMaster.Text = "BL não cadastrada no Logcomex."
        End If


    End Sub

    Private Sub lkFollowUpMaster_Click(sender As Object, e As EventArgs) Handles lkFollowUpMaster.Click
        If txtID_Master.Text = "" Then
            divErroMaster.Visible = True
            lblErroMaster.Text = "Selecione o registro que deseja consultar!"
        Else
            Response.Redirect("FollowUp.aspx?id=" & txtID_Master.Text)
        End If
    End Sub

    Private Sub lkFollowUpHouse_Click(sender As Object, e As EventArgs) Handles lkFollowUpHouse.Click
        If txtIDHouse.Text = "" Then
            divErroHouse.Visible = True
            lblErroHouse.Text = "Selecione o registro que deseja consultar!"
        Else
            Response.Redirect("FollowUp.aspx?id=" & txtIDHouse.Text)
        End If
    End Sub

    Private Sub lkFollowUpEmbarque_Click(sender As Object, e As EventArgs) Handles lkFollowUpEmbarque.Click
        If txtID_Embarque.Text = "" Then
            divErroEmbarque.Visible = True
            lblErroEmbarque.Text = "Selecione o registro que deseja consultar!"
        Else
            Response.Redirect("FollowUp.aspx?id=" & txtID_Embarque.Text)
        End If
    End Sub

    Private Sub lkRemoverMaster_Click(sender As Object, e As EventArgs) Handles lkRemoverMaster.Click
        divSuccessMaster.Visible = False
        divErroMaster.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErroMaster.Visible = True
            lblErroMaster.Text = "Usuário não possui permissão."

            Exit Sub
        Else
            If txtID_Master.Text = "" Then
                divErroMaster.Visible = True
                lblErroMaster.Text = "Selecione o registro que deseja excluir!"
            Else
                Con.ExecutarQuery("UPDATE TB_BL SET DT_CANCELAMENTO = GETDATE(), ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & " WHERE ID_BL = " & txtID_Master.Text)
                dgvMaster.DataBind()
                divSuccessMaster.Visible = True
                lblSuccessMaster.Text = "Item deletado com sucesso!"
            End If
        End If
        Con.Fechar()
    End Sub
End Class