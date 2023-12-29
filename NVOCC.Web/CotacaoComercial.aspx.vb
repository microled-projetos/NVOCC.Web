Imports System.Net
Imports System.Net.Mail
Imports Attachment = System.Net.Mail.Attachment
Public Class CotacaoComercial
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")
        Else
            ds = Con.ExecutarQuery("SELECT count(ID_COTACAO)Aprovadas FROM TB_COTACAO A
LEFT JOIN TB_STATUS_COTACAO B ON B.ID_STATUS_COTACAO = A.ID_STATUS_COTACAO
WHERE FL_COTACAO_APROVADA = 1")
            lblAprovadas.Text = "Total de cotações aprovadas: " & ds.Tables(0).Rows(0).Item("Aprovadas")

            ds = Con.ExecutarQuery("SELECT count(ID_COTACAO)Rejeitadas FROM TB_COTACAO A
WHERE A.ID_STATUS_COTACAO = 8")
            lblRejeitadas.Text = "Total de cotações rejeitadas: " & ds.Tables(0).Rows(0).Item("Rejeitadas")

        End If

        txtData.Text = Now.Date.ToString("dd/MM/yyyy")

        Con.Fechar()
    End Sub
    Private Sub dgvCotacao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCotacao.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        lblmsgErro.Text = ""
        ' GRID()

        If e.CommandName = "Selecionar" Then

            For i As Integer = 0 To dgvCotacao.Rows.Count - 1
                dgvCotacao.Rows(i).CssClass = "Normal"

            Next
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")

            If txtlinha.Text > 100 Then
                txtlinha.Text = txtlinha.Text.Substring(txtlinha.Text.Length() - 2)
            End If
            dgvCotacao.Rows(txtlinha.Text).CssClass = "selected1"

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO,ID_TIPO_ESTUFAGEM,ID_SERVICO,NR_COTACAO FROM TB_COTACAO WHERE ID_COTACAO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 9 Or ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 15 Then
                    lkCalcular.Visible = False
                    lkAprovar.Visible = False
                    lkRejeitar.Visible = False
                    lkCancelar.Visible = True
                    lkUpdate.Visible = True
                ElseIf ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 12 Then
                    lkCalcular.Visible = False
                    lkAprovar.Visible = False
                    lkRejeitar.Visible = False
                    lkCancelar.Visible = False
                    lkUpdate.Visible = True
                ElseIf ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 11 Or ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 8 Or ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 7 Then
                    lkCalcular.Visible = False
                    lkAprovar.Visible = False
                    lkRejeitar.Visible = False
                    lkCancelar.Visible = False
                    lkUpdate.Visible = False
                Else
                    lkCalcular.Visible = True
                    lkAprovar.Visible = True
                    lkRejeitar.Visible = True
                    lkCancelar.Visible = True
                    lkUpdate.Visible = False
                End If
                txtServico.Text = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                txtEstufagem.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                txtNumeroCotacao.Text = ds.Tables(0).Rows(0).Item("NR_COTACAO")
            End If


        ElseIf e.CommandName = "Status" Then

            Dim ID As String = e.CommandArgument.Substring(0, e.CommandArgument.IndexOf("|"))

            dsHistoricoStatus.SelectParameters("ID_COTACAO").DefaultValue = ID
            dgvHistoricoStatus.DataBind()
            mpeStatus.Show()

        ElseIf e.CommandName = "Duplicar" Then

            Dim ID As String = e.CommandArgument

            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblmsgErro.Text = "Usuário não possui permissão."
                dgvCotacao.Columns(0).Visible = False

                Exit Sub
            Else
                If ID = "" Then

                    divErro.Visible = True
                    lblmsgErro.Text = "Selecione o registro que deseja duplicar!"

                Else

                    'Verifica se cotação duplicada tem sequencial de repetição
                    Dim dsVerifica As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_COTACAO WHERE REF_REPETIDAS IS NULL AND ID_COTACAO = " & ID)
                    If dsVerifica.Tables(0).Rows(0).Item("QTD") > 0 Then
                        'Em caso negativo faz update com sequencial na cotação mãe que posteriormente se repetirá na filha
                        ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Cotacao_Repetidas_" & Now.Year.ToString & " REF_REPETIDAS")
                        Dim REF_REPETIDAS As String = "REP_" & ds.Tables(0).Rows(0).Item("REF_REPETIDAS")
                        Con.ExecutarQuery("UPDATE TB_COTACAO SET REF_REPETIDAS = '" & REF_REPETIDAS & "' WHERE ID_COTACAO = " & ID)

                    End If

                    Dim numero_cotacao As String = NumeroCotacao()

                    Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE,   OB_OPERACIONAL,   NR_PROCESSO_GERADO, ID_USUARIO_STATUS,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR,FL_FREE_HAND,ID_PARCEIRO_EXPORTADOR,ID_TIPO_PAGAMENTO,TRANSITTIME_TRUCKING_AEREO,ID_PARCEIRO_IMPORTADOR,ID_STATUS_FRETE_AGENTE,FL_LTL,FL_DTA_HUB,FL_TRANSP_DEDICADO,VL_TOTAL_M3,VL_TOTAL_PESO_BRUTO,FINAL_DESTINATION,VL_TOTAL_FRETE_VENDA_CALCULADO,ID_PARCEIRO_RODOVIARIO,FL_DUPLICATA,ID_COTACAO_MENOR,REF_REPETIDAS )    SELECT '" & numero_cotacao & "', GETDATE(), 16, GETDATE(), DT_VALIDADE_COTACAO," & Session("ID_USUARIO") & ", ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE,  OB_OPERACIONAL, NULL, " & Session("ID_USUARIO") & ", ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR,FL_FREE_HAND,ID_PARCEIRO_EXPORTADOR,ID_TIPO_PAGAMENTO,TRANSITTIME_TRUCKING_AEREO,ID_PARCEIRO_IMPORTADOR,ID_STATUS_FRETE_AGENTE,FL_LTL,FL_DTA_HUB,FL_TRANSP_DEDICADO,VL_TOTAL_M3,VL_TOTAL_PESO_BRUTO,FINAL_DESTINATION,VL_TOTAL_FRETE_VENDA_CALCULADO,ID_PARCEIRO_RODOVIARIO,1,ID_COTACAO, REF_REPETIDAS   FROM TB_COTACAO WHERE ID_COTACAO = " & ID & " Select SCOPE_IDENTITY() as ID_COTACAO;

INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,
ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN,QTD_BASE_CALCULO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA, FL_TAXA_TRANSPORTADOR,DT_IMPORTACAO )    
SELECT  (Select SCOPE_IDENTITY() as ID_COTACAO),
ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN,QTD_BASE_CALCULO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA, FL_TAXA_TRANSPORTADOR,DT_IMPORTACAO  FROM TB_COTACAO_TAXA 
WHERE  ID_COTACAO = " & ID & " 

INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO, ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,OUTRAS_OBS,VL_FRETE_COMPRA_MIN,VL_FRETE_VENDA_MIN,ID_MOEDA_CARGA,QT_MERCADORIA) 
SELECT (SELECT MAX(ID_COTACAO) FROM TB_COTACAO ), ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,OUTRAS_OBS,VL_FRETE_COMPRA_MIN,VL_FRETE_VENDA_MIN ,ID_MOEDA_CARGA,QT_MERCADORIA 
FROM TB_COTACAO_MERCADORIA WHERE  ID_COTACAO = " & ID)

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALCOTACAO =  " & Session("NR_COTACAO") & ", ANOSEQUENCIALCOTACAO = YEAR(GETDATE())")

                    dgvCotacao.DataBind()
                    divSuccess.Visible = True
                    lblmsgSuccess.Text = "Item duplicado com sucesso! <br/><br/><strong> Nova cotação:" & numero_cotacao & "</strong>"

                End If

            End If

        End If


    End Sub

    Private Sub dgvCotacao_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgvCotacao.SelectedIndexChanged
        txtID.Text = dgvCotacao.SelectedValue
        Dim index As Integer = dgvCotacao.SelectedIndex

        If txtlinha.Text <> "" Then
            dgvCotacao.Rows(index).ForeColor = System.Drawing.Color.Black

        End If

        Dim linhanova As Integer = dgvCotacao.SelectedIndex
        txtlinha.Text = linhanova

        For i As Integer = 0 To dgvCotacao.Rows.Count - 1
            dgvCotacao.Rows(index).ForeColor = System.Drawing.Color.Red
        Next
    End Sub

    Private Sub lkInserir_Click(sender As Object, e As EventArgs) Handles lkInserir.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
        Else
            Response.Redirect("CadastroCotacao.aspx")
        End If

    End Sub

    Private Sub lkAlterar_Click(sender As Object, e As EventArgs) Handles lkAlterar.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione o registro que deseja editar!"
            Else

                Response.Redirect("CadastroCotacao.aspx?id=" & txtID.Text)

            End If
        End If

    End Sub

    Function NumeroCotacao() As String

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet


        ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Cotacao_" & Now.Year.ToString & " NRSEQUENCIALCOTACAO")
        Dim NR_COTACAO As String = ds.Tables(0).Rows(0).Item("NRSEQUENCIALCOTACAO")
        Dim ano_atual = Now.Year.ToString.Substring(2)

        Session("NR_COTACAO") = NR_COTACAO
        NR_COTACAO = NR_COTACAO.PadLeft(7, "0") & "/" & ano_atual

        Con.Fechar()


        Return NR_COTACAO
    End Function




    Private Sub lkDuplicar_Click(sender As Object, e As EventArgs) Handles lkDuplicar.Click
        divSuccess.Visible = False
        divErro.Visible = False
        divPesquisa.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione o registro que deseja duplicar!"
            Else
                Dim numero_cotacao As String = NumeroCotacao()
                Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, NR_PROCESSO_GERADO, ID_USUARIO_STATUS,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR,FL_FREE_HAND,ID_PARCEIRO_EXPORTADOR,ID_TIPO_PAGAMENTO,TRANSITTIME_TRUCKING_AEREO,ID_PARCEIRO_IMPORTADOR,ID_STATUS_FRETE_AGENTE,FL_LTL,FL_DTA_HUB,FL_TRANSP_DEDICADO,VL_TOTAL_M3,VL_TOTAL_PESO_BRUTO,FINAL_DESTINATION,VL_TOTAL_FRETE_VENDA_CALCULADO,ID_PARCEIRO_RODOVIARIO,FL_DUPLICATA,ID_COTACAO_MENOR)    SELECT '" & numero_cotacao & "', GETDATE(), 16, GETDATE(), DT_VALIDADE_COTACAO," & Session("ID_USUARIO") & ", ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, NULL, " & Session("ID_USUARIO") & ", ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR,FL_FREE_HAND,ID_PARCEIRO_EXPORTADOR,ID_TIPO_PAGAMENTO,TRANSITTIME_TRUCKING_AEREO,ID_PARCEIRO_IMPORTADOR,ID_STATUS_FRETE_AGENTE,FL_LTL,FL_DTA_HUB,FL_TRANSP_DEDICADO,VL_TOTAL_M3,VL_TOTAL_PESO_BRUTO,FINAL_DESTINATION,VL_TOTAL_FRETE_VENDA_CALCULADO,ID_PARCEIRO_RODOVIARIO,1,ID_COTACAO  FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_COTACAO;

INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,
ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN,QTD_BASE_CALCULO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA, FL_TAXA_TRANSPORTADOR,DT_IMPORTACAO )    
SELECT  (Select SCOPE_IDENTITY() as ID_COTACAO),
ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN,QTD_BASE_CALCULO,ID_FORNECEDOR,FL_IMPORTADO_SISTEMA, FL_TAXA_TRANSPORTADOR,DT_IMPORTACAO  FROM TB_COTACAO_TAXA 
WHERE  ID_COTACAO = " & txtID.Text & " 

INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO, ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,OUTRAS_OBS,VL_FRETE_COMPRA_MIN,VL_FRETE_VENDA_MIN,ID_MOEDA_CARGA,QT_MERCADORIA) 
SELECT (SELECT MAX(ID_COTACAO) FROM TB_COTACAO ), ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME,VL_FRETE_COMPRA_UNITARIO,VL_FRETE_VENDA_UNITARIO,OUTRAS_OBS,VL_FRETE_COMPRA_MIN,VL_FRETE_VENDA_MIN ,ID_MOEDA_CARGA,QT_MERCADORIA 
FROM TB_COTACAO_MERCADORIA WHERE  ID_COTACAO = " & txtID.Text)
                Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALCOTACAO =  " & Session("NR_COTACAO") & ", ANOSEQUENCIALCOTACAO = YEAR(GETDATE())")
                txtPesquisa.Text = ""
                dgvCotacao.DataBind()
                divSuccess.Visible = True
                lblmsgSuccess.Text = "Item duplicado com sucesso! <br/><br/><strong> Nova cotação:" & numero_cotacao & "</strong>"
            End If
        End If

        Con.Fechar()
    End Sub

    Private Sub lkFiltrar_Click(sender As Object, e As EventArgs) Handles lkFiltrar.Click
        divPesquisa.Visible = True
    End Sub
    Protected Sub dgvCotacao_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        divSuccess.Visible = False
        divErro.Visible = False
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvCotacao.DataSource = Session("TaskTable")
            dgvCotacao.DataBind()
            dgvCotacao.HeaderRow.TableSection = TableRowSection.TableHeader
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

    Private Sub dgvCotacao_PreRender(sender As Object, e As EventArgs) Handles dgvCotacao.PreRender
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            dgvCotacao.Columns(0).Visible = False

        End If

        Con.Fechar()

    End Sub

    Private Sub lkCalcular_Click(sender As Object, e As EventArgs) Handles lkCalcular.Click
        divPesquisa.Visible = False
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione o registro que deseja calcular!"
            Else



                ds = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO FROM TB_COTACAO WHERE ID_COTACAO =" & txtID.Text)

                If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 9 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Não é possivel calcular pois a cotação já esta aprovada!"
                Else
                    Dim CalCotacao As New CalculaCotacao
                    Dim retorno As String = CalCotacao.CalculaCotacao(txtID.Text)
                    If retorno = "Calculo realizado com sucesso" Then

                        divSuccess.Visible = True
                        lblmsgSuccess.Text = "Calculo realizado com sucesso"

                    Else
                        divErro.Visible = True
                        lblmsgErro.Text = retorno
                    End If



                End If

                GRID()

            End If
        End If


    End Sub

    Private Sub bntPesquisar_Click(sender As Object, e As EventArgs) Handles bntPesquisar.Click
        GRID()
    End Sub

    Sub GRID()

        If txtData.Text <> "" And ddlEstufagem.SelectedValue <> 0 And ddlIncoterm.SelectedValue <> 0 And ddlOrigem.SelectedValue <> 0 And ddlDestino.SelectedValue <> 0 And txtPeso.Text <> "" And txtCBM.Text <> "" And ddlServico.SelectedValue <> 0 Then
            PesquisaRepetidas()
        Else


            Dim FILTRO As String



            If rdTRansporte.SelectedValue <> 0 Then
                If FILTRO = "" Then
                    FILTRO = " WHERE ID_VIATRANSPORTE = " & rdTRansporte.SelectedValue
                Else
                    FILTRO = FILTRO & " AND ID_VIATRANSPORTE = " & rdTRansporte.SelectedValue
                End If
            End If

            If rdServico.SelectedValue <> 0 Then
                If FILTRO = "" Then
                    If rdServico.SelectedValue = 1 Then
                        FILTRO = " WHERE ID_SERVICO in (1,2) "
                    ElseIf rdServico.SelectedValue = 2 Then
                        FILTRO = " WHERE ID_SERVICO in (4,5) "
                    End If
                Else
                    If rdServico.SelectedValue = 1 Then
                        FILTRO = FILTRO & " AND ID_SERVICO in (1,2) "
                    ElseIf rdServico.SelectedValue = 2 Then
                        FILTRO = FILTRO & " AND ID_SERVICO in (4,5) "
                    End If
                End If
            End If

            If rdEstufagem.SelectedValue <> 0 Then
                If FILTRO = "" Then
                    FILTRO = " WHERE ID_TIPO_ESTUFAGEM = " & rdEstufagem.SelectedValue
                Else
                    FILTRO = FILTRO & " AND ID_TIPO_ESTUFAGEM = " & rdEstufagem.SelectedValue
                End If
            End If

            If txtPesquisa.Text <> "" Then

                If ddlConsultas.SelectedValue = 1 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE NR_COTACAO LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND NR_COTACAO LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 2 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE STATUS LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND STATUS LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 3 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE CLIENTE LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND CLIENTE LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 4 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE ORIGEM LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND ORIGEM LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 5 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE DESTINO LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND DESTINO LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 6 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE AGENTE LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND AGENTE LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 7 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE VENDEDOR LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND VENDEDOR LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 8 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE NR_PROCESSO_GERADO LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND NR_PROCESSO_GERADO LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 10 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE CLIENTE_FINAL LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND CLIENTE_FINAL LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 11 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE SERVICO LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND SERVICO LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 12 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE TIPO_ESTUFAGEM LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND TIPO_ESTUFAGEM LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 13 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE INCOTERM LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND INCOTERM LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 14 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE ARMADOR LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND ARMADOR LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 15 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE ANALISTA_COTACAO_INSIDE LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND ANALISTA_COTACAO_INSIDE LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 16 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE ANALISTA_COTACAO_PRICING LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND ANALISTA_COTACAO_PRICING LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 17 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE SERVICO LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND SERVICO LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                ElseIf ddlConsultas.SelectedValue = 18 Then
                    If FILTRO = "" Then
                        FILTRO = " WHERE EXPORTADOR LIKE '%" & txtPesquisa.Text & "%' "
                    Else
                        FILTRO = FILTRO & " AND EXPORTADOR LIKE '%" & txtPesquisa.Text & "%' "
                    End If
                End If

            End If
            Dim sql As String = "SELECT top 500 *  FROM [dbo].[View_Cotacao]  " & FILTRO & " ORDER BY DT_ABERTURA DESC"

            dsCotacao.SelectCommand = sql
            dgvCotacao.DataBind()

        End If
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefault()", True)

    End Sub

    Private Sub lkImprimir_Click(sender As Object, e As EventArgs) Handles lkImprimir.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja imprimir!"
        Else
            mpeImprimir.Show()
            PanelImprimir.Attributes.CssStyle.Add("display", "block")
        End If

    End Sub

    Private Sub dgvCotacao_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvCotacao.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btnDuplicar As Button = CType(e.Row.FindControl("btnDuplicar"), Button)
            Dim Status As Label = CType(e.Row.FindControl("lblStatus"), Label)

            Dim Cor As Label = CType(e.Row.FindControl("lblCor"), Label)

            Status.Style("background-color") = Cor.Text

            If Cor.Text = "#000000" Or Cor.Text = "#7517c2" Or Cor.Text = "#822830" Then

                Status.Style("color") = "white"

            End If

            If ddlEstufagem.SelectedValue <> 0 And ddlIncoterm.SelectedValue <> 0 And ddlOrigem.SelectedValue <> 0 And ddlDestino.SelectedValue <> 0 And txtPeso.Text <> "" And txtCBM.Text <> "" And ddlServico.SelectedValue <> 0 Then
                btnDuplicar.Visible = True
            Else
                btnDuplicar.Visible = False
            End If

        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja imprimir!"

        Else
            lblmsgErro.Text = ""
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("Select count(*)QTD from TB_COTACAO where DT_CALCULO_COTACAO is not null and id_cotacao = " & txtID.Text)
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                divErro.Visible = True
                lblmsgErro.Text = "Cotação necessita de cálculo!"
            Else

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "ImprimirCotacao()", True)

            End If
            Con.Fechar()
        End If
    End Sub


    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja enviar!"

        Else

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "EnviarCotacao()", True)

        End If
    End Sub
    Private Sub lkRemover_Click(sender As Object, e As EventArgs) Handles lkRemover.Click
        divSuccess.Visible = False
        divErro.Visible = False
        divPesquisa.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            lblmsgErro.Text = "Usuário não tem permissão para realizar exclusões"
            divErro.Visible = True
        Else

            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione o registro que deseja excluir!"
            Else
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_COTACAO WHERE ID_STATUS_COTACAO NOT IN (7,8,9,10,11,12,14,15) AND ID_COTACAO = " & txtID.Text)

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "O status dessa cotação não permite a sua remoção!"
                Else
                    Con.ExecutarQuery("DELETE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)
                    lblmsgSuccess.Text = "Registro deletado!"
                    divSuccess.Visible = True
                    GRID()
                End If
            End If
        End If


        Con.Fechar()

    End Sub

    Private Sub lkAprovar_Click(sender As Object, e As EventArgs) Handles lkAprovar.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro!"
        Else
            lkAprovar.Visible = False
            Dim Con As New Conexao_sql
            Con.Conectar()

            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(C.ID_STATUS_COTACAO,0)ID_STATUS_COTACAO,A.NM_RAZAO,P.NM_PORTO FROM TB_COTACAO C
LEFT JOIN TB_PARCEIRO A ON C.ID_AGENTE_INTERNACIONAL = A.ID_PARCEIRO
LEFT JOIN TB_PORTO P ON C.ID_PORTO_ORIGEM = P.ID_PORTO
WHERE C.ID_COTACAO = " & txtID.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                lblAgenteSI.Text = "Agente Internacional: " & ds.Tables(0).Rows(0).Item("NM_RAZAO")
                lblPortoOrigemSI.Text = "Porto de Origem: " & ds.Tables(0).Rows(0).Item("NM_PORTO")
                mpeEnvioSI.Show()
            End If
        End If
    End Sub

    Sub Aprovar()
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione um registro!"
            Else
                lkAprovar.Visible = False
                'APROVADA
                ds = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_COTACAO WHERE DT_CALCULO_COTACAO IS NULL AND ID_COTACAO = " & txtID.Text)

                If ds.Tables(0).Rows(0).Item("QTD") > 0 Then

                    divErro.Visible = True
                    lblmsgErro.Text = "Necessário calcular cotação!"
                    Exit Sub

                End If

                ds = Con.ExecutarQuery("SELECT ISNULL(ID_STATUS_COTACAO,0)ID_STATUS_COTACAO,ISNULL(ID_AGENTE_INTERNACIONAL,0)ID_AGENTE_INTERNACIONAL,ISNULL(ID_TIPO_PAGAMENTO,0)ID_TIPO_PAGAMENTO, ISNULL(NR_PROCESSO_GERADO,'')NR_PROCESSO_GERADO, DT_VALIDADE_COTACAO, ISNULL(ID_TIPO_DIVISAO_FRETE,0)ID_TIPO_DIVISAO_FRETE, ISNULL(ID_SERVICO,0)ID_SERVICO,ISNULL(ID_TIPO_ESTUFAGEM,0)ID_TIPO_ESTUFAGEM FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)

                If ds.Tables(0).Rows(0).Item("ID_AGENTE_INTERNACIONAL") = 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Apenas cotações com agente preechido podem ser aprovadas!"
                    Exit Sub

                ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO") = 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Apenas cotações com tipo de frete preechido podem ser aprovadas!"
                    Exit Sub
                ElseIf ds.Tables(0).Rows(0).Item("DT_VALIDADE_COTACAO") < Now.Date And ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO") = "" Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Cotação com data de validade inferior a data atual!"
                    Exit Sub
                ElseIf ValorMinimoPendente(txtID.Text) = True Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Cotação contém taxa(s) com valor minimo vazio!"
                    Exit Sub


                Else

                    If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") <> 10 Then

                        If ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO") = "" Then
                            ' NumeroProcesso()
                            Dim AprovaCotacao As New AprovaCotacao
                            AprovaCotacao.AprovaCotacao(txtID.Text, ds.Tables(0).Rows(0).Item("ID_SERVICO"), ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM"), ds.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE"), Session("ID_USUARIO"))
                        End If

                    End If
                End If

                If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") <> 10 Then
                    Con.ExecutarQuery("UPDATE TB_COTACAO SET DT_ENVIO_COTACAO = GETDATE(), ID_STATUS_COTACAO = 9, DT_STATUS_COTACAO = GETDATE(), ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)

                Else
                    Con.ExecutarQuery("UPDATE TB_COTACAO SET DT_ENVIO_COTACAO = GETDATE(), ID_STATUS_COTACAO = 15, DT_STATUS_COTACAO = GETDATE(), ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)

                    Dim RotinaUpdate As New RotinaUpdate
                    RotinaUpdate.UpdateInfoBasicas(txtID.Text, ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO"))
                    RotinaUpdate.UpdateFrete(txtID.Text, ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO"))
                    RotinaUpdate.UpdateFreteTaxa(txtID.Text, ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO"))
                    Dim ds2 As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA FROM TB_COTACAO_TAXA WHERE ID_COTACAO = " & txtID.Text)
                    If ds2.Tables(0).Rows.Count > 0 Then
                        For Each linha As DataRow In ds2.Tables(0).Rows
                            RotinaUpdate.UpdateTaxas(txtID.Text, linha.Item("ID_COTACAO_TAXA"), ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO"))
                        Next
                    End If


                End If


                ds = Con.ExecutarQuery("SELECT EMAIL_FECHAMENTO_COTACAO FROM 
TB_PARAMETROS WHERE EMAIL_FECHAMENTO_COTACAO IS NOT NULL")

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim DESTINATARIO As String = ds.Tables(0).Rows(0).Item("EMAIL_FECHAMENTO_COTACAO").ToString()
                    SeparaEmail(DESTINATARIO)

                End If

                GRID()
                txtID.Text = ""
                lblmsgSuccess.Text = "Registro aprovado!"
                divSuccess.Visible = True
            End If
        End If

    End Sub

    Function ValorMinimoPendente(ID_COTACAO As Integer) As Boolean
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim finaliza As New FinalizaCotacao
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,
VL_TAXA_COMPRA,
ISNULL(VL_TAXA_COMPRA_MIN, 0)VL_TAXA_COMPRA_MIN,
VL_TAXA_VENDA,
ISNULL(VL_TAXA_VENDA_MIN, 0) VL_TAXA_VENDA_MIN
From TB_COTACAO_TAXA
WHERE ID_COTACAO = " & ID_COTACAO & " And ID_BASE_CALCULO_TAXA In (6,7,13,14)")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows
                If finaliza.TaxaBloqueada(linha.Item("ID_COTACAO_TAXA"), "COTACAO") = False Then

                    If linha.Item("VL_TAXA_COMPRA") <> 0 And linha.Item("VL_TAXA_COMPRA_MIN") = 0 Then
                        Return True
                    End If

                    If linha.Item("VL_TAXA_VENDA") <> 0 And linha.Item("VL_TAXA_VENDA_MIN") = 0 Then
                        Return True
                    End If

                End If
            Next
        End If

        ds = Con.ExecutarQuery("SELECT ID_COTACAO_TAXA,
VL_TAXA_COMPRA,
ISNULL(VL_TAXA_COMPRA_MIN, 0)VL_TAXA_COMPRA_MIN,
VL_TAXA_VENDA,
ISNULL(VL_TAXA_VENDA_MIN, 0) VL_TAXA_VENDA_MIN
From TB_COTACAO_TAXA
WHERE ID_COTACAO = " & ID_COTACAO & " And ID_BASE_CALCULO_TAXA = 37 ")
        If ds.Tables(0).Rows.Count > 0 Then

            For Each linha As DataRow In ds.Tables(0).Rows
                If finaliza.TaxaBloqueada(linha.Item("ID_COTACAO_TAXA"), "COTACAO") = False Then

                    If linha.Item("VL_TAXA_VENDA") <> 0 And linha.Item("VL_TAXA_VENDA_MIN") = 0 Then
                        Return True
                    End If

                End If
            Next

        End If


        Return False
    End Function


    Private Sub lkCancelar_Click(sender As Object, e As EventArgs) Handles lkCancelar.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione um registro!"
            Else
                'CANCELAR
                Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = 7, DT_STATUS_COTACAO = GETDATE() , ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)

                Con.ExecutarQuery("UPDATE TB_BL SET DT_CANCELAMENTO = GETDATE() , ID_USUARIO_CANCELAMENTO = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)
                divSuccess.Visible = True
                lblmsgSuccess.Text = "Item cancelado com sucesso!"
                GRID()
            End If
        End If

    End Sub

    Private Sub lkRejeitar_Click(sender As Object, e As EventArgs) Handles lkRejeitar.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione um registro!"
            Else
                'REJEITAR
                Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = 8, DT_STATUS_COTACAO = GETDATE() , ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)
                divSuccess.Visible = True
                lblmsgSuccess.Text = "Item rejeitado com sucesso!"
                GRID()

            End If
        End If

    End Sub


    Sub SeparaEmail(ByVal email As String)
        'quebrar a string
        Dim palavras As String() = email.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

        'exibe o resultado
        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
            Dim EmailService As New EmailService
            Dim Mensagem As String = "Cotação Aprovada! <br/><br> ID:" & txtID.Text & "<br/><br/>NUMERO COTAÇÃO: " & txtNumeroCotacao.Text & "<br/><br/>."

            Dim retorno As String = EmailService.EnviarEmail(palavras(i), "COTAÇÃO APROVADA - NVOCC", Mensagem)
        Next
    End Sub


    Private Sub lkUpdate_Click(sender As Object, e As EventArgs) Handles lkUpdate.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1025 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            divErro.Visible = True
            lblmsgErro.Text = "Usuário não possui permissão."
            dgvCotacao.Columns(0).Visible = False

            Exit Sub
            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione um registro!"
            Else
                Dim dsVerifica As DataSet = Con.ExecutarQuery("SELECT count(*)qtd FROM TB_BL_TAXA where id_item_despesa <> 14 and isnull(CD_ORIGEM_INF,'COTA') ='COTA' and isnull(ID_COTACAO_TAXA,0) = 0 and ID_BL_MASTER IS NULL and ID_BL = (SELECT ID_BL FROM [TB_BL] WHERE GRAU = 'C' AND ID_COTACAO = " & txtID.Text & " AND NR_PROCESSO = (SELECT NR_PROCESSO_GERADO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " ))")
                If dsVerifica.Tables(0).Rows(0).Item("QTD") > 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Necessário comunicar o T.I: Processo sem ID_COTAÇÃO_TAXA "
                Else
                    Con.ExecutarQuery("UPDATE TB_COTACAO Set ID_STATUS_COTACAO = 10, DT_STATUS_COTACAO = GETDATE() , ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)
                    divSuccess.Visible = True
                    lblmsgSuccess.Text = "Item atualizado para 'Em update' com sucesso!"
                    GRID()

                End If



            End If
        End If

    End Sub

    Private Sub lkFollowUp_Click(sender As Object, e As EventArgs) Handles lkFollowUp.Click
        divSuccess.Visible = False
        divErro.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim Msg As String = ""
        Dim Assunto As String = ""
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione um registro!"
        Else
            Dim dsVerifica As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_COTACAO WHERE DT_FOLLOWUP IS NOT NULL AND ID_COTACAO = " & txtID.Text)
            If dsVerifica.Tables(0).Rows(0).Item("QTD") > 0 Then
                divErro.Visible = True
                lblmsgErro.Text = "Follow Up já realizado!"
            Else
                lkFollowUp.Visible = False
                Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(M.NM_CLIENTE_FINAL,'')CLIENTE_FINAL,NR_REFERENCIA_CLIENTE, B.NM_RAZAO AS CLIENTE,C.NM_TIPO_ESTUFAGEM,O.CD_SIGLA AS ORIGEM,D.CD_SIGLA AS DESTINO,I.NM_INCOTERM, NR_COTACAO, J.NM_CONTATO AS CONTATO,isnull(lower(EMAIL_CONTATO),'')EMAIL_CONTATO,K.NM_RAZAO as CNEE
    FROM TB_COTACAO A 
    LEFT JOIN TB_PARCEIRO B ON  B.ID_PARCEIRO = A.ID_CLIENTE
    LEFT JOIN TB_TIPO_ESTUFAGEM C ON  C.ID_TIPO_ESTUFAGEM = A.ID_TIPO_ESTUFAGEM
    LEFT JOIN TB_PORTO O ON O.ID_PORTO = A.ID_PORTO_ORIGEM
    LEFT JOIN TB_PORTO D ON D.ID_PORTO = A.ID_PORTO_DESTINO
    LEFT JOIN TB_INCOTERM I ON I.ID_INCOTERM = A.ID_INCOTERM
    LEFT JOIN TB_CONTATO J ON J.ID_CONTATO = A.ID_CONTATO
    LEFT JOIN TB_PARCEIRO K ON  K.ID_PARCEIRO = A.ID_PARCEIRO_IMPORTADOR
	LEFT JOIN VW_REFERENCIA_CLIENTE L ON L.ID_COTACAO = A.ID_COTACAO
	LEFT JOIN TB_CLIENTE_FINAL M on M.ID_CLIENTE_FINAL = A.ID_CLIENTE_FINAL
    WHERE A.ID_COTACAO = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("EMAIL_CONTATO") = "" Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Contato " & ds.Tables(0).Rows(0).Item("CONTATO") & " não possui email cadastrado! "
                    Else


                        Assunto = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE") & " + " & ds.Tables(0).Rows(0).Item("NM_TIPO_ESTUFAGEM") & " - " & ds.Tables(0).Rows(0).Item("NM_INCOTERM") & " – " & ds.Tables(0).Rows(0).Item("ORIGEM") & "/" & ds.Tables(0).Rows(0).Item("DESTINO") & " / CNEE: " & ds.Tables(0).Rows(0).Item("CLIENTE_FINAL") & " (" & ds.Tables(0).Rows(0).Item("CLIENTE") & ") / " & ds.Tables(0).Rows(0).Item("CONTATO") & " / COT " & ds.Tables(0).Rows(0).Item("NR_COTACAO")

                        Msg = "Ol&aacute; " & ds.Tables(0).Rows(0).Item("CONTATO") & ", Tudo bem?<br/><br/>Estou passando para verificar, se conseguiu analisar nossa cota&ccedil;&atilde;o.<br/>Ainda estamos concorrendo?<br/><br/>Ficamos a disposi&ccedil;&atilde;o, para lhe ajudar a fechar mais este processo.<br/>Contem conosco para o que precisar!"


                        'If processaFila(ds.Tables(0).Rows(0).Item("EMAIL_CONTATO"), Msg, Assunto) = False Then
                        If processaFila("tatiane.amorim@fcalog.com", Msg, Assunto) = False Then
                            divSuccess.Visible = False
                            divErro.Visible = True
                        Else

                            Con.ExecutarQuery("UPDATE TB_COTACAO SET DT_FOLLOWUP = GETDATE(),ID_STATUS_COTACAO = 14, DT_STATUS_COTACAO = GETDATE() , ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)
                            divErro.Visible = False
                            divSuccess.Visible = True
                            lblmsgSuccess.Text = "Follow Up realizado com sucesso!"
                            GRID()
                        End If

                    End If
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Dados necessários não localizados! "
                End If

            End If
        End If

        lkFollowUp.Visible = True
    End Sub

    Function processaFila(destinatario As String, msg As String, assunto As String) As Boolean
        Dim sSql As String
        Dim anexos As Attachment()
        Dim critica As String = ""
        Dim rsParam As DataSet = Nothing
        Dim enderecos As String = ""
        Dim indExc As Long
        Dim nomeArq As String
        Dim validaEnd As String

        Dim ends() As String
        Dim Mail As New MailMessage
        Dim smtp As New SmtpClient()

        Try
            Dim Con As New Conexao_sql
            Con.Conectar()

            sSql = "SELECT EMAIL_REMETENTE, END_SMTP, SENHA_REMETENTE, DOMINIO_REMETENTE, EXIGE_SSL, PORTA_SMTP, DIR_EMAIL_GER AS DIR_EMAIL "
            sSql = sSql & " FROM TB_PARAMETROS "
            rsParam = Con.ExecutarQuery(sSql)
            If rsParam.Tables(0).Rows.Count > 0 Then


                Mail = New MailMessage
                Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Try
                    Mail.From = New MailAddress(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString)
                Catch ex As Exception
                    critica = "Endereço de envio dos e-mails inválido [" & rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString & "] "
                    lblmsgErro.Text = critica
                    divErro.Visible = True
                    Return False
                End Try



                Try
                    smtp = New SmtpClient(rsParam.Tables(0).Rows(0)("END_SMTP").ToString)
                    If rsParam.Tables(0).Rows(0)("EXIGE_SSL").ToString = "1" Then
                        smtp.EnableSsl = True
                    Else
                        smtp.EnableSsl = False
                    End If
                    smtp.Credentials = New System.Net.NetworkCredential(rsParam.Tables(0).Rows(0)("EMAIL_REMETENTE").ToString, rsParam.Tables(0).Rows(0)("SENHA_REMETENTE").ToString, rsParam.Tables(0).Rows(0)("DOMINIO_REMETENTE").ToString)
                    smtp.Port = rsParam.Tables(0).Rows(0)("PORTA_SMTP").ToString
                Catch ex As Exception
                    critica = "Configurações de envio de e-mail inválidas, contate o suporte!" & Err.Description
                    lblmsgErro.Text = critica
                    divErro.Visible = True
                    Return False

                End Try

                'ASSUNTO
                If assunto <> "" Then
                    Mail.Subject = assunto
                Else
                    Mail.Subject = ""
                End If


                'CORPO
                If msg <> "" Then
                    Mail.Body = msg
                Else
                    Mail.Body = ""
                End If

                Mail.IsBodyHtml = True

                enderecos = destinatario
                Dim palavras As String() = enderecos.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

                'exibe o resultado
                For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
                    Mail.To.Add(palavras(i).ToString)

                Next

                Try

                    smtp.Send(Mail)

                    smtp.Dispose()

                Catch ex As Exception
                    critica = "Ocorreu um erro ao enviar o e-mail! Erro:  " & Err.Description
                    lblmsgErro.Text = critica
                    divErro.Visible = True
                    Err.Clear()
                    Return False

                End Try

            Else
                critica = "Não foi possível acessar As configurações para envio de e-mails, contate o suporte!"
                lblmsgErro.Text = critica
                divErro.Visible = True
                Return False

            End If
        Catch ex As Exception
            critica = "Ocorreu um erro ao realizar o envio de e-mails, contate o suporte!" & vbCrLf & "Erro:  " & Err.Description
            lblmsgErro.Text = critica
            divErro.Visible = True
            Return False

        End Try

        Return True

    End Function

    Sub PesquisaRepetidas()

        If txtData.Text = "" Or ddlEstufagem.SelectedValue = 0 Or ddlIncoterm.SelectedValue = 0 Or ddlOrigem.SelectedValue = 0 Or ddlDestino.SelectedValue = 0 Or txtPeso.Text = "" Or txtCBM.Text = "" Or ddlServico.SelectedValue = 0 Then
            lblmsgErro.Text = "Para verificação é necessario preencher todos os campos"
            divErro.Visible = True
        Else

            Dim peso As String = txtPeso.Text
            Dim cbm As String = txtCBM.Text

            Dim sql As String = ""

            If ddlServico.SelectedValue = 1 Or ddlServico.SelectedValue = 4 Then
                'AGENCIAMENTO DE IMPORTACAO MARITIMA (1)
                'AGENCIAMENTO DE EXPORTACAO MARITIMA (4)
                sql = " SELECT top 500 *  FROM [dbo].[View_Cotacao] WHERE ID_COTACAO IN ( SELECT ID_COTACAO FROM [dbo].[View_Cotacao_Repetidas] WHERE CONVERT(DATE,DT_ABERTURA,103) BETWEEN CONVERT(DATE,GETDATE()-6,103) AND CONVERT(DATE,GETDATE(),103) AND ID_PORTO_ORIGEM = " & ddlOrigem.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestino.SelectedValue & " AND ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & " AND ID_INCOTERM = " & ddlIncoterm.SelectedValue & " AND VL_TOTAL_PESO_BRUTO = " & peso.Replace(",", ".") & " AND VL_M3 = " & cbm.Replace(",", ".") & " ) ORDER BY DT_ABERTURA DESC"

            ElseIf ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then

                'AGENCIAMENTO DE IMPORTACAO AEREO (2)
                'AGENCIAMENTO DE EXPORTAÇÃO AEREO (5)
                sql = " SELECT top 500 *  FROM [dbo].[View_Cotacao] WHERE ID_COTACAO IN ( SELECT ID_COTACAO FROM [dbo].[View_Cotacao_Repetidas] WHERE CONVERT(DATE,DT_ABERTURA,103) BETWEEN CONVERT(DATE,GETDATE()-6,103) AND CONVERT(DATE,GETDATE(),103) AND ID_PORTO_ORIGEM = " & ddlOrigem.SelectedValue & " AND ID_PORTO_DESTINO = " & ddlDestino.SelectedValue & " AND ID_TIPO_ESTUFAGEM = " & ddlEstufagem.SelectedValue & " AND ID_INCOTERM = " & ddlIncoterm.SelectedValue & " AND VL_TOTAL_PESO_BRUTO = " & peso.Replace(",", ".") & " AND VL_CBM= " & cbm.Replace(",", ".") & " ) ORDER BY DT_ABERTURA DESC"

            End If


            dsCotacao.SelectCommand = sql
            dgvCotacao.DataBind()

            If dgvCotacao.Rows.Count = 0 Then
                ''NENHUMA COTAÇÃO ENCONTRADA. DESEJA INSERIR UMA COTAÇÃO COM OS PARAMETROS PESQUISADOS?
                mpeRepetida.Show()
            End If

        End If
    End Sub
    Private Sub btnPesquisaRepetidas_Click(sender As Object, e As EventArgs) Handles btnPesquisaRepetidas.Click
        divSuccess.Visible = False
        divErro.Visible = False
        PesquisaRepetidas()
    End Sub

    Private Sub btnInserirRepetida_Click(sender As Object, e As EventArgs) Handles btnInserirRepetida.Click
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim numero_cotacao As String = NumeroCotacao()
        Dim ID_COTACAO As String = ""
        Dim ds As DataSet = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Cotacao_Repetidas_" & Now.Year.ToString & " REF_REPETIDAS")
        Dim REF_REPETIDAS As String = "REP_" & ds.Tables(0).Rows(0).Item("REF_REPETIDAS")

        Dim peso As String = txtPeso.Text
        Dim cbm As String = txtCBM.Text

        Dim sql As String = ""

        If ddlServico.SelectedValue = 1 Or ddlServico.SelectedValue = 4 Then
            'AGENCIAMENTO DE IMPORTACAO MARITIMA (1)
            'AGENCIAMENTO DE EXPORTACAO MARITIMA (4)
            sql = "INSERT INTO TB_COTACAO (NR_COTACAO, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, ID_ANALISTA_COTACAO, ID_USUARIO_STATUS, ID_INCOTERM, ID_TIPO_ESTUFAGEM, ID_PORTO_ORIGEM, ID_PORTO_DESTINO  , VL_TOTAL_PESO_BRUTO, REF_REPETIDAS, ID_SERVICO, VL_TOTAL_M3) VALUES ('" & numero_cotacao & "', GETDATE(), 16, GETDATE(), GETDATE() + 5 , " & Session("ID_USUARIO") & " , " & Session("ID_USUARIO") & " , " & ddlIncoterm.SelectedValue & " , " & ddlEstufagem.SelectedValue & " , " & ddlOrigem.SelectedValue & " ," & ddlDestino.SelectedValue & "  , " & peso.Replace(",", ".") & ",  '" & REF_REPETIDAS & "', (SELECT CASE WHEN (SELECT ISNULL(ID_VIATRANSPORTE,0) FROM TB_PORTO WHERE ID_PORTO = " & ddlOrigem.SelectedValue & " ) = 1 THEN 1 ELSE 2 END ID_SERVICO), " & cbm.Replace(",", ".") & " ) Select SCOPE_IDENTITY() as ID_COTACAO ;"

        ElseIf ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
            'AGENCIAMENTO DE IMPORTACAO AEREO (2)
            'AGENCIAMENTO DE EXPORTAÇÃO AEREO (5)
            sql = "INSERT INTO TB_COTACAO (NR_COTACAO, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, ID_ANALISTA_COTACAO, ID_USUARIO_STATUS, ID_INCOTERM, ID_TIPO_ESTUFAGEM, ID_PORTO_ORIGEM, ID_PORTO_DESTINO  , VL_TOTAL_PESO_BRUTO, REF_REPETIDAS, ID_SERVICO) VALUES ('" & numero_cotacao & "', GETDATE(), 16, GETDATE(), GETDATE() + 5 , " & Session("ID_USUARIO") & " , " & Session("ID_USUARIO") & " , " & ddlIncoterm.SelectedValue & " , " & ddlEstufagem.SelectedValue & " , " & ddlOrigem.SelectedValue & " ," & ddlDestino.SelectedValue & "  , " & peso.Replace(",", ".") & ",  '" & REF_REPETIDAS & "', (SELECT CASE WHEN (SELECT ISNULL(ID_VIATRANSPORTE,0) FROM TB_PORTO WHERE ID_PORTO = " & ddlOrigem.SelectedValue & " ) = 1 THEN 1 ELSE 2 END ID_SERVICO)) Select SCOPE_IDENTITY() as ID_COTACAO ;"
        End If

        ds = Con.ExecutarQuery(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            ID_COTACAO = ds.Tables(0).Rows(0).Item("ID_COTACAO")

            If ddlServico.SelectedValue = 1 Or ddlServico.SelectedValue = 4 Then
                'AGENCIAMENTO DE IMPORTACAO MARITIMA (1)
                'AGENCIAMENTO DE EXPORTACAO MARITIMA (4)
                Con.ExecutarQuery("INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO, VL_PESO_BRUTO, VL_M3) VALUES (" & ID_COTACAO & ", " & peso.Replace(",", ".") & " , " & cbm.Replace(",", ".") & " )")

            ElseIf ddlServico.SelectedValue = 2 Or ddlServico.SelectedValue = 5 Then
                'AGENCIAMENTO DE IMPORTACAO AEREO (2)
                'AGENCIAMENTO DE EXPORTAÇÃO AEREO (5)
                Con.ExecutarQuery("INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO, VL_PESO_BRUTO, VL_CBM) VALUES (" & ID_COTACAO & ", " & peso.Replace(",", ".") & " , " & cbm.Replace(",", ".") & " )")

            End If


            Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALCOTACAO =  " & Session("NR_COTACAO") & ", ANOSEQUENCIALCOTACAO = YEAR(GETDATE())")


            Response.Redirect("CadastroCotacao.aspx?ID=" & ID_COTACAO)

        End If
    End Sub

    Private Sub btnCancelaEnvioSI_Click(sender As Object, e As EventArgs) Handles btnCancelaEnvioSI.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_COTACAO SET FL_ENVIA_SI = 0 WHERE ID_COTACAO = " & txtID.Text)
        Aprovar()
        mpeEnvioSI.Hide()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefaultSI()", True)
    End Sub

    Private Sub btnConfirmaEnviarSI_Click(sender As Object, e As EventArgs) Handles btnConfirmaEnviarSI.Click
        Dim Con As New Conexao_sql
        Con.Conectar()
        Con.ExecutarQuery("UPDATE TB_COTACAO SET FL_ENVIA_SI = 1 WHERE ID_COTACAO = " & txtID.Text)
        Aprovar()
        mpeEnvioSI.Hide()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "MouseDefaultSI()", True)
    End Sub

    Private Sub rdTRansporte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdTRansporte.SelectedIndexChanged
        If rdTRansporte.SelectedValue = 1 Then 'MARITIMO
            rdEstufagem.SelectedValue = 1
            rdServico.SelectedValue = 1
        ElseIf rdTRansporte.SelectedValue = 4 Then 'AEREO
            rdEstufagem.SelectedValue = 2
            rdServico.SelectedValue = 1
        Else
            rdEstufagem.SelectedValue = 0
            rdServico.SelectedValue = 0
        End If
    End Sub
End Class