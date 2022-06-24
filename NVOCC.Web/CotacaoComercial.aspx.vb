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

        Con.Fechar()
    End Sub
    Private Sub dgvCotacao_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCotacao.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        lblmsgErro.Text = ""
        GRID()

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

            'dsHistoricoFrete.SelectCommand = "SELECT * FROM VW_VALOR_FRETE_LOTE where rownum <= " & txtQtd.Text & " and cnpj = '" & txtcnpj.Text & "' "
            'dgvHistoricoFrete.DataBind()


            dsHistoricoStatus.SelectParameters("ID_COTACAO").DefaultValue = ID
            dgvHistoricoStatus.DataBind()
            mpeStatus.Show()

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
                Dim url As String = "/CadastroCotacao.aspx?id={0}"
                url = String.Format(url, txtID.Text)
                Response.Redirect(url)
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
                Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, NR_PROCESSO_GERADO, ID_USUARIO_STATUS,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR,FL_FREE_HAND,ID_PARCEIRO_EXPORTADOR,ID_TIPO_PAGAMENTO,TRANSITTIME_TRUCKING_AEREO,ID_PARCEIRO_IMPORTADOR,ID_STATUS_FRETE_AGENTE,FL_LTL,FL_DTA_HUB,FL_TRANSP_DEDICADO,VL_TOTAL_M3,VL_TOTAL_PESO_BRUTO,FINAL_DESTINATION,VL_TOTAL_FRETE_VENDA_CALCULADO,ID_PARCEIRO_RODOVIARIO,FL_DUPLICATA)    SELECT '" & numero_cotacao & "', GETDATE(), 2, GETDATE(), DT_VALIDADE_COTACAO," & Session("ID_USUARIO") & ", ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, NULL, " & Session("ID_USUARIO") & ", ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR,FL_FREE_HAND,ID_PARCEIRO_EXPORTADOR,ID_TIPO_PAGAMENTO,TRANSITTIME_TRUCKING_AEREO,ID_PARCEIRO_IMPORTADOR,ID_STATUS_FRETE_AGENTE,FL_LTL,FL_DTA_HUB,FL_TRANSP_DEDICADO,VL_TOTAL_M3,VL_TOTAL_PESO_BRUTO,FINAL_DESTINATION,VL_TOTAL_FRETE_VENDA_CALCULADO,ID_PARCEIRO_RODOVIARIO,1  FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_COTACAO;

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
                        dgvCotacao.DataBind()
                    Else
                        divErro.Visible = True
                        lblmsgErro.Text = retorno
                    End If



                End If






                '                If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 9 Then
                '                    divErro.Visible = True
                '                    lblmsgErro.Text = "Não é possivel calcular pois a cotação já esta aprovada!"
                '                Else

                '                    Dim VENDA_MIN As Decimal
                '                    Dim M3 As Decimal
                '                    Dim PESO_BRUTO As Decimal
                '                    Dim QT_CONTAINER As Integer
                '                    Dim LCA As Decimal

                '                    'NUMERO SEQUENCIAL

                '                    ds = Con.ExecutarQuery("Select A.ID_TIPO_ESTUFAGEM,
                'A.ID_SERVICO,
                'isnull(A.VL_TOTAL_M3,0)VL_M3, 
                'isnull(A.VL_TOTAL_PESO_BRUTO,0)VL_PESO_BRUTO,
                'isnull(A.VL_TOTAL_FRETE_VENDA_MIN,0)VL_TOTAL_FRETE_VENDA_MIN,
                'isnull(A.VL_TOTAL_FRETE_VENDA,0)VL_TOTAL_FRETE_VENDA,
                '(select sum(isnull(QT_CONTAINER,0)) FROM TB_COTACAO_MERCADORIA B WHERE B.ID_COTACAO = A.ID_COTACAO )QT_CONTAINER,
                '(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO,
                '(isnull(D.QTD_CAIXA,0) * isnull(D.VL_COMPRIMENTO,0) * isnull(D.VL_ALTURA,0) * isnull(D.VL_LARGURA,0))/6000 AS LCA
                'from TB_COTACAO A 
                'left join TB_COTACAO_MERCADORIA_DIMENSAO D ON D.ID_COTACAO = A.ID_COTACAO
                'Where A.ID_COTACAO = " & txtID.Text)

                '                    M3 = ds.Tables(0).Rows(0).Item("VL_M3")
                '                    PESO_BRUTO = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                '                    VENDA_MIN = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN")
                '                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_CONTAINER")) Then
                '                        QT_CONTAINER = ds.Tables(0).Rows(0).Item("QT_CONTAINER")
                '                    End If



                '                    '        CÁLCULO DO PESO TAXADO
                '                    Dim PESO_TAXADO As Decimal


                '                    'ANTIGO
                '                    ' Dim PV As Decimal = M3
                '                    'If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                '                    '    'AEREO

                '                    '    PV = M3 * 167
                '                    '    ' PESO_BRUTO = PESO_BRUTO / 1000
                '                    '    If PESO_BRUTO >= PV Then
                '                    '        PESO_TAXADO = PESO_BRUTO
                '                    '    Else
                '                    '        PESO_TAXADO = PV
                '                    '    End If
                '                    'End If


                '                    'NOVO
                '                    If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                '                        'AEREO

                '                        'AEREO
                '                        For Each linha As DataRow In ds.Tables(0).Rows
                '                            LCA = LCA + linha.Item("LCA")
                '                        Next
                '                        Dim LCAFinal As String = LCA.ToString
                '                        LCAFinal = LCAFinal.ToString.Replace(".", "")
                '                        LCAFinal = LCAFinal.ToString.Replace(",", ".")
                '                        Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_M3 = " & LCAFinal & " WHERE ID_COTACAO = " & txtID.Text)
                '                        Con.ExecutarQuery("UPDATE TB_COTACAO_MERCADORIA SET VL_M3 = " & LCAFinal & " WHERE ID_COTACAO = " & txtID.Text)

                '                        If LCA >= PESO_BRUTO Then
                '                            PESO_TAXADO = LCA
                '                        Else
                '                            PESO_TAXADO = PESO_BRUTO
                '                        End If



                '                        'If Not IsDBNull(ds.Tables(0).Rows(0).Item("LCA")) Then
                '                        '    LCA = ds.Tables(0).Rows(0).Item("LCA")
                '                        '    Dim LCAFinal As String = LCA.ToString
                '                        '    LCAFinal = LCAFinal.ToString.Replace(".", "")
                '                        '    LCAFinal = LCAFinal.ToString.Replace(",", ".")
                '                        '    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_TOTAL_M3 = " & LCAFinal & " WHERE ID_COTACAO = " & txtID.Text)
                '                        'End If

                '                        'If LCA >= PESO_BRUTO Then
                '                        '    PESO_TAXADO = LCA
                '                        'Else
                '                        '    PESO_TAXADO = PESO_BRUTO
                '                        'End If
                '                    End If


                '                    If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                '                        'MARITIMO
                '                        PESO_BRUTO = PESO_BRUTO / 1000

                '                        If PESO_BRUTO >= M3 Then
                '                            PESO_TAXADO = PESO_BRUTO
                '                        Else
                '                            PESO_TAXADO = M3
                '                        End If
                '                    End If


                '                    Dim FRETE_CALCULADO As Decimal = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA")


                '                    If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                '                        'ID_BASE_CALCULO 34 - POR CNTR
                '                        '  FRETE_CALCULADO = (FRETE_CALCULADO * QT_CONTAINER)
                '                        If FRETE_CALCULADO < VENDA_MIN Then
                '                            FRETE_CALCULADO = VENDA_MIN
                '                        End If
                '                    ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                '                        'ID_BASE_CALCULO 13 - POR TON / M³
                '                        FRETE_CALCULADO = (FRETE_CALCULADO * PESO_TAXADO)
                '                        If FRETE_CALCULADO < VENDA_MIN Then
                '                            FRETE_CALCULADO = VENDA_MIN
                '                        End If

                '                    End If




                '                    Dim Peso_Final As String = PESO_TAXADO.ToString
                '                    Peso_Final = Peso_Final.ToString.Replace(".", "")
                '                    Peso_Final = Peso_Final.ToString.Replace(",", ".")

                '                    Dim frete_Final As String = FRETE_CALCULADO.ToString
                '                    frete_Final = frete_Final.ToString.Replace(".", "")
                '                    frete_Final = frete_Final.ToString.Replace(",", ".")

                '                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_PESO_TAXADO = " & Peso_Final & ",VL_TOTAL_FRETE_VENDA_CALCULADO = " & frete_Final & "  WHERE ID_COTACAO = " & txtID.Text)

                '                    divSuccess.Visible = True
                '                    lblmsgSuccess.Text = "Taxa calculada com sucesso"
                '                    CalcTaxas()
                '                    Con.ExecutarQuery("UPDATE TB_COTACAO SET Dt_Calculo_Cotacao = GETDATE() WHERE ID_COTACAO = " & txtID.Text)


                '                    dgvCotacao.DataBind()
                '                End If



                GRID()

            End If
        End If


    End Sub

    Sub CalcTaxas()
        divPesquisa.Visible = False
        divSuccess.Visible = False
        divErro.Visible = False


        Dim dataatual As Date = Now.Date.ToString("dd/MM/yyyy")

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet = Con.ExecutarQuery("SELECT a.ID_SERVICO,b.ID_COTACAO_TAXA,isnull(A.VL_PESO_TAXADO,0) VL_PESO_TAXADO,a.ID_MOEDA_FRETE,isnull(A.ID_INCOTERM,0)ID_INCOTERM,
isnull(B.VL_TAXA_COMPRA,0)VL_TAXA_COMPRA,
isnull(B.VL_TAXA_VENDA,0)VL_TAXA_VENDA,
B.ID_BASE_CALCULO_TAXA,isnull(A.VL_TOTAL_M3,0)VL_M3, 
isnull(A.VL_TOTAL_PESO_BRUTO,0)VL_PESO_BRUTO, 
(select CASE WHEN A.ID_MOEDA_FRETE = 124 THEN CONVERT(varchar,GETDATE(),103) ELSE (select CONVERT(varchar,MAX(DT_CAMBIO),103) FROM TB_MOEDA_FRETE WHERE ID_MOEDA = A.ID_MOEDA_FRETE) END)DT_CAMBIO,
isnull(B.VL_TAXA_COMPRA_MIN,0)VL_TAXA_COMPRA_MIN,
isnull(B.VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN,
isnull(B.ID_MOEDA_COMPRA,0)ID_MOEDA_COMPRA,
isnull(B.ID_MOEDA_VENDA,0)ID_MOEDA_VENDA,
isnull(B.QTD_BASE_CALCULO,0)QTD_BASE_CALCULO
From TB_COTACAO A 
Left Join TB_COTACAO_TAXA B ON A.ID_COTACAO = B.ID_COTACAO 
Left Join TB_BASE_CALCULO_TAXA C ON B.ID_BASE_CALCULO_TAXA = C.ID_BASE_CALCULO_TAXA 
WHERE A.ID_COTACAO =" & txtID.Text & " ORDER BY NM_BASE_CALCULO_TAXA desc")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Dim COMPRA_MIN As Decimal = linha.Item("VL_TAXA_COMPRA_MIN")
                Dim VENDA_MIN As Decimal = linha.Item("VL_TAXA_VENDA_MIN")
                Dim QTD_BASE_CALCULO As Integer = linha.Item("QTD_BASE_CALCULO")
                Dim x As Double
                Dim y As Double
                Dim z As Double
                Dim CompraCalc As String = "0"
                Dim VendaCalc As String = "0"
                If IsDBNull(linha.Item("ID_COTACAO_TAXA")) Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Não há taxas vinculadas a essa cotação"
                    divSuccess.Visible = False

                    Exit Sub

                ElseIf IsDBNull(linha.Item("ID_BASE_CALCULO_TAXA")) Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Base de Calculo não informada."
                    divSuccess.Visible = False
                    Exit Sub

                ElseIf linha.Item("ID_MOEDA_FRETE") = 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Moeda de frete não informada."
                    divSuccess.Visible = False
                    Exit Sub

                ElseIf linha.Item("ID_MOEDA_COMPRA") = 0 And linha.Item("VL_TAXA_COMPRA") <> 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Moeda não informada."
                    divSuccess.Visible = False
                    Exit Sub
                ElseIf linha.Item("ID_MOEDA_VENDA") = 0 And linha.Item("VL_TAXA_VENDA") <> 0 Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Moeda não informada."
                    divSuccess.Visible = False
                    Exit Sub
                ElseIf IsDBNull(linha.Item("DT_CAMBIO")) Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Não há valor de moeda de câmbio cadastrado com a data atual."
                    divSuccess.Visible = False

                    Exit Sub
                ElseIf linha.Item("DT_CAMBIO") < dataatual Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Não há valor de moeda de câmbio cadastrado com a data atual."
                    divSuccess.Visible = False

                    Exit Sub

                ElseIf linha.Item("DT_CAMBIO") > dataatual Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Não há valor de moeda de câmbio cadastrado com a data atual."
                    divSuccess.Visible = False

                    Exit Sub

                Else
                    If linha.Item("ID_BASE_CALCULO_TAXA") = 1 Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Base de Calculo não informada."
                        divSuccess.Visible = False
                        Exit Sub

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 2 Then
                        '% VR DO FRETE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 as QTD
FROM TB_COTACAO A
WHERE A.ID_COTACAO =  " & txtID.Text)

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 3 Then
                        'VR DO FRETE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 as QTD
FROM TB_COTACAO A
WHERE A.ID_COTACAO =   " & txtID.Text)

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 4 Then
                        '% TOTAL DO HOUSE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT 
   sum(VL_TAXA_VENDA_CALCULADO) + VL_TOTAL_FRETE_VENDA_CALCULADO TOTAL_VENDA, 
   
   sum(VL_TAXA_COMPRA_CALCULADO) + VL_TOTAL_FRETE_COMPRA TOTAL_COMPRA
FROM TB_COTACAO A
INNER JOIN TB_COTACAO_TAXA B ON B.ID_COTACAO = A.ID_COTACAO
WHERE B.FL_DECLARADO = 1
AND A.ID_MOEDA_FRETE = B.ID_MOEDA_VENDA 
AND A.ID_COTACAO = " & txtID.Text & "
GROUP BY A.ID_COTACAO,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_COMPRA")

                        If ds1.Tables(0).Rows.Count > 0 Then
                            If Not IsDBNull(ds1.Tables(0).Rows(0).Item("TOTAL_COMPRA")) Then
                                x = ds1.Tables(0).Rows(0).Item("TOTAL_COMPRA") / 100
                            Else
                                x = 0 / 100
                            End If
                        Else
                            x = 0 / 100
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString


                        If ds1.Tables(0).Rows.Count > 0 Then
                            If Not IsDBNull(ds1.Tables(0).Rows(0).Item("TOTAL_VENDA")) Then
                                x = ds1.Tables(0).Rows(0).Item("TOTAL_VENDA") / 100
                            Else
                                x = 0 / 100
                            End If
                        Else
                            x = 0 / 100
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 5 Then
                        'VALOR FIXO

                        VendaCalc = linha.Item("VL_TAXA_VENDA").ToString()
                        CompraCalc = linha.Item("VL_TAXA_COMPRA").ToString()


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 6 Then
                        'POR M³

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 7 Then
                        'POR TON

                        x = linha.Item("VL_PESO_BRUTO")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x / 1000
                        z = y * z
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString


                        x = linha.Item("VL_PESO_BRUTO")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = x / 1000
                        z = y * z
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString



                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 10 Then
                        'POR MAFI 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (19)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 11 Then
                        'POR CNTR 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (5,6,2,9,10,12,16,18,19)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 12 Then
                        'POR CNTR 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (17,13,14,15,11,3,4,7,8,1)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 13 Then
                        'POR TON / M³
                        If linha.Item("ID_SERVICO") = 1 Or linha.Item("ID_SERVICO") = 4 Then
                            'MARITIMO


                            x = linha.Item("VL_M3")
                            y = linha.Item("VL_PESO_BRUTO") / 1000



                            If x > y Then
                                x = x
                            Else
                                x = y
                            End If



                            y = linha.Item("VL_TAXA_VENDA")
                            z = x * y
                            If VENDA_MIN < 0 Then
                                If z > VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            ElseIf VENDA_MIN > 0 Then
                                If z < VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            End If
                            VendaCalc = z.ToString

                            y = linha.Item("VL_TAXA_COMPRA")
                            z = x * y
                            If COMPRA_MIN < 0 Then
                                If z > COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            ElseIf COMPRA_MIN > 0 Then
                                If z < COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            End If
                            CompraCalc = z.ToString


                        ElseIf linha.Item("ID_SERVICO") = 2 Or linha.Item("ID_SERVICO") = 5 Then
                            'AEREO


                            x = linha.Item("VL_M3")

                            y = linha.Item("VL_TAXA_VENDA")
                            z = x * y
                            If VENDA_MIN < 0 Then
                                If z > VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            ElseIf VENDA_MIN > 0 Then
                                If z < VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            End If
                            VendaCalc = z.ToString

                            y = linha.Item("VL_TAXA_COMPRA")
                            z = x * y
                            If COMPRA_MIN < 0 Then
                                If z > COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            ElseIf COMPRA_MIN > 0 Then
                                If z < COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            End If
                            CompraCalc = z.ToString
                        End If



                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 14 Then
                        'POR KG

                        If linha.Item("ID_SERVICO") = 1 Or linha.Item("ID_SERVICO") = 4 Then
                            'MARITIMO
                            x = linha.Item("VL_PESO_BRUTO")

                        ElseIf linha.Item("ID_SERVICO") = 2 Or linha.Item("ID_SERVICO") = 5 Then
                            'AEREO
                            x = linha.Item("VL_PESO_TAXADO")

                        End If




                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z


                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 15 Then
                        '% VR DA MERCADORIA
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_CARGA),0)) AS VALOR
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " ")

                        x = ds1.Tables(0).Rows(0).Item("VALOR") / 100
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("VALOR") / 100
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 16 Then
                        '% HOUSE COLLECT

                        Dim ds1 As DataSet = Con.ExecutarQuery("   SELECT 
   sum(VL_TAXA_VENDA_CALCULADO) 
   + CASE WHEN A.ID_TIPO_PAGAMENTO = 1 THEN VL_TOTAL_FRETE_VENDA_CALCULADO ELSE 0 END  
   TOTAL_VENDA, 
   
   sum(VL_TAXA_COMPRA_CALCULADO) + 
      + CASE WHEN A.ID_TIPO_PAGAMENTO = 1 THEN VL_TOTAL_FRETE_COMPRA ELSE 0 END 
	  TOTAL_COMPRA
FROM TB_COTACAO A
INNER JOIN TB_COTACAO_TAXA B ON B.ID_COTACAO = A.ID_COTACAO
WHERE B.FL_DECLARADO = 1
AND B.ID_MOEDA_VENDA = B.ID_MOEDA_VENDA 
AND A.ID_COTACAO = " & txtID.Text & "
AND B.ID_TIPO_PAGAMENTO = 1
GROUP BY A.ID_COTACAO,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_COMPRA,A.ID_TIPO_PAGAMENTO")
                        If ds1.Tables(0).Rows.Count = 0 Then
                            CompraCalc = COMPRA_MIN.ToString
                            VendaCalc = VENDA_MIN.ToString
                        Else
                            x = ds1.Tables(0).Rows(0).Item("TOTAL_COMPRA") / 100
                            y = linha.Item("VL_TAXA_COMPRA")
                            z = y * x
                            If COMPRA_MIN < 0 Then
                                If z > COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            ElseIf COMPRA_MIN > 0 Then
                                If z < COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            End If

                            CompraCalc = z.ToString

                            x = ds1.Tables(0).Rows(0).Item("TOTAL_VENDA") / 100
                            y = linha.Item("VL_TAXA_VENDA")
                            z = y * x
                            If VENDA_MIN < 0 Then
                                If z > VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            ElseIf VENDA_MIN > 0 Then
                                If z < VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            End If
                            VendaCalc = z.ToString
                        End If


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 20 Then
                        'POR HC 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER = 10")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 21 Then
                        'POR FLAT RACK 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (15)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 22 Then
                        ' POR OPEN TOP 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (9)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 23 Then
                        'POR OPEN TOP 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (8)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        If x = 0 Then
                            x = 1
                        End If
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 24 Then
                        'POR FLAT RACK 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (16)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 25 Then
                        'POR REEFER 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (5)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 26 Then
                        'POR REEFER 40
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (4)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 28 Then
                        'POR MAFI 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (13)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 29 Then
                        'VALOR POR EMBARQUE- valor fixo digitado

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString



                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 30 Then
                        'POR UNIDADE - quantidade de conteineres do processo

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & "")

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 31 Then
                        'POR HAWB (AEREO)- na cotação é 1 por 1

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 32 Then
                        'POR HBL (MARITIMO) - na cotação é 1 por 1

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 33 Then
                        'POR DOCUMENTO

                        z = linha.Item("VL_TAXA_VENDA").ToString()
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA").ToString()
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf (linha.Item("ID_BASE_CALCULO_TAXA") = 38 Or linha.Item("ID_BASE_CALCULO_TAXA") = 40 Or linha.Item("ID_BASE_CALCULO_TAXA") = 41) Then
                        'POR DOC/SHIPPER   -   POR ENTRADA    -   POR CARGA

                        z = linha.Item("VL_TAXA_VENDA") * QTD_BASE_CALCULO
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = linha.Item("VL_TAXA_COMPRA") * QTD_BASE_CALCULO
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 34 Then
                        'POR CNTR 
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text)

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x

                        If x = 0 Then
                            x = 1
                        End If


                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 35 Then
                        ' POR TEU

                        'Para cada conteiner de 20' corresponde 1 teu
                        Dim ds1 As DataSet = Con.ExecutarQuery(" Select ISNULL(SUM(QT_CONTAINER), 0)QTD
From TB_COTACAO_MERCADORIA A
Where a.ID_COTACAO = " & txtID.Text & " And ID_TIPO_CONTAINER In (5,6,2,9,10,12,16,18)")
                        y = ds1.Tables(0).Rows(0).Item("QTD")


                        'Para cada conteiner de 40' corresponde a 2 teus

                        ds1 = Con.ExecutarQuery("Select ISNULL(SUM(QT_CONTAINER), 0)QTD
From TB_COTACAO_MERCADORIA A
Where a.ID_COTACAO = " & txtID.Text & " And ID_TIPO_CONTAINER In (19,17,13,14,15,11,3,4,7,8,1)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        x = x * 2
                        Dim total As Integer = x + y

                        z = total * linha.Item("VL_TAXA_VENDA")
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                        z = total * linha.Item("VL_TAXA_COMPRA")
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 36 Then
                        'POR REEFER
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =" & txtID.Text & " AND ID_TIPO_CONTAINER IN (4,5)")

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")

                        If x = 0 Then
                            x = 1
                        End If

                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 37 Then
                        'SEGURO

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_CARGA),0)) AS VALOR_CARGA, (ISNULL(SUM(B.VL_TOTAL_FRETE_VENDA_CALCULADO),0)) AS FRETE_VENDA_CALCULADO
FROM TB_COTACAO_MERCADORIA A
INNER JOIN TB_COTACAO B ON A.ID_COTACAO = B.ID_COTACAO
WHERE A.ID_COTACAO = " & txtID.Text & " ")
                        Dim TAXAS_DECLARADAS As Decimal = 0
                        Dim FOB As Decimal = ds1.Tables(0).Rows(0).Item("VALOR_CARGA")
                        Dim FRETE As Decimal = ds1.Tables(0).Rows(0).Item("FRETE_VENDA_CALCULADO")

                        If linha.Item("ID_INCOTERM") = 10 Then
                            ds1 = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_TAXA_VENDA_CALCULADO),0)) AS VALOR_TAXA
FROM TB_COTACAO_TAXA A
WHERE  FL_DECLARADO = 1 AND A.ID_COTACAO = " & txtID.Text & " ")
                            TAXAS_DECLARADAS = ds1.Tables(0).Rows(0).Item("VALOR_TAXA")
                        End If

                        Dim DESPESA As Decimal = FOB + FRETE + TAXAS_DECLARADAS
                        DESPESA = DESPESA / 100
                        DESPESA = DESPESA * 10

                        Dim TOTAL As Decimal = DESPESA + FRETE + TAXAS_DECLARADAS + FOB

                        x = TOTAL / 100
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        If COMPRA_MIN < 0 Then
                            If z > COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        ElseIf COMPRA_MIN > 0 Then
                            If z < COMPRA_MIN Then
                                z = COMPRA_MIN
                            End If
                        End If
                        CompraCalc = z.ToString

                        x = TOTAL / 100
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        If VENDA_MIN < 0 Then
                            If z > VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        ElseIf VENDA_MIN > 0 Then
                            If z < VENDA_MIN Then
                                z = VENDA_MIN
                            End If
                        End If
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 42 Then
                        'POR PESO TAXADO

                        x = linha.Item("VL_PESO_TAXADO")

                            y = linha.Item("VL_TAXA_VENDA")
                            z = x * y
                            If VENDA_MIN < 0 Then
                                If z > VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            ElseIf VENDA_MIN > 0 Then
                                If z < VENDA_MIN Then
                                    z = VENDA_MIN
                                End If
                            End If
                            VendaCalc = z.ToString

                            y = linha.Item("VL_TAXA_COMPRA")
                            z = x * y
                            If COMPRA_MIN < 0 Then
                                If z > COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            ElseIf COMPRA_MIN > 0 Then
                                If z < COMPRA_MIN Then
                                    z = COMPRA_MIN
                                End If
                            End If
                            CompraCalc = z.ToString




                    End If


                    CompraCalc = CompraCalc.Replace(".", String.Empty)
                    CompraCalc = CompraCalc.Replace(",", ".")
                    VendaCalc = VendaCalc.Replace(".", String.Empty)
                    VendaCalc = VendaCalc.Replace(",", ".")


                    Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET VL_TAXA_COMPRA_CALCULADO = '" & CompraCalc & "', VL_TAXA_VENDA_CALCULADO = '" & VendaCalc & "' WHERE ID_COTACAO_TAXA = " & linha.Item("ID_COTACAO_TAXA"))
                    divSuccess.Visible = True

                End If
            Next

        End If


        'Recalcula seguro

    End Sub
    Private Sub bntPesquisar_Click(sender As Object, e As EventArgs) Handles bntPesquisar.Click
        GRID()
    End Sub

    Sub GRID()
        'divSuccess.Visible = False
        'divErro.Visible = False
        'lblmsgErro.Text = ""
        If ddlConsultas.SelectedValue = 0 Or txtPesquisa.Text = "" Then
            dgvCotacao.DataBind()
        Else
            Dim FILTRO As String

            If ddlConsultas.SelectedValue = 1 Then
                FILTRO = " NR_COTACAO LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 2 Then
                FILTRO = " STATUS LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 3 Then
                FILTRO = " CLIENTE LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 4 Then
                FILTRO = " ORIGEM LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 5 Then
                FILTRO = " DESTINO LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 6 Then
                FILTRO = " AGENTE LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 7 Then
                FILTRO = " VENDEDOR LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 8 Then
                FILTRO = " NR_PROCESSO_GERADO LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 9 Then
                FILTRO = " ANALISTA_COTACAO LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 10 Then
                FILTRO = " CLIENTE_FINAL LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 11 Then
                FILTRO = " SERVICO LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 12 Then
                FILTRO = " TIPO_ESTUFAGEM LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 13 Then
                FILTRO = " INCOTERM LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 14 Then
                FILTRO = " ARMADOR LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 15 Then
                FILTRO = " ANALISTA_COTACAO_INSIDE LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 16 Then
                FILTRO = " ANALISTA_COTACAO_PRICING LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 17 Then
                FILTRO = " SERVICO LIKE '%" & txtPesquisa.Text & "%' "
            End If

            Dim sql As String = "select * from [dbo].[View_Filtro_Cotacao] WHERE " & FILTRO
            dsCotacao.SelectCommand = sql
            dgvCotacao.DataBind()

        End If
    End Sub

    Private Sub lkImprimir_Click(sender As Object, e As EventArgs) Handles lkImprimir.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja imprimir!"
        Else
            mpeImprimir.Show()
            Panel1.Attributes.CssStyle.Add("display", "block")
        End If

    End Sub

    Private Sub dgvCotacao_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvCotacao.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Status As Label = CType(e.Row.FindControl("lblStatus"), Label)

            Dim Cor As Label = CType(e.Row.FindControl("lblCor"), Label)

            Status.Style("background-color") = Cor.Text

            If Cor.Text = "#000000" Then

                Status.Style("color") = "white"

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
                'Dim url As String = ""

                'url = "GeraPDF.aspx?c=" & txtID.Text & "&l=" & ddlLinguagem.SelectedValue & "&f=i"

                'Response.Write("<script>")
                'Response.Write("window.open('" & url & "','_blank')")
                'Response.Write("</script>")
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
            'Dim url As String = ""
            'url = "GeraPDF.aspx?c=" & txtID.Text & "&l=" & ddlLinguagem.SelectedValue & "&f=e"
            'Response.Redirect(url)
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
                            AprovaCotacao.AprovaCotacao(txtID.Text, ds.Tables(0).Rows(0).Item("ID_SERVICO"), ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM"), ds.Tables(0).Rows(0).Item("ID_TIPO_DIVISAO_FRETE"))
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

End Class