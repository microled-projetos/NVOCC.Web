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
        If e.CommandName = "Selecionar" Then
            If txtlinha.Text <> "" Then
                'dgvCotacao.Rows(txtlinha.Text).ForeColor = System.Drawing.Color.Black
                dgvCotacao.Rows(txtlinha.Text).CssClass = "Normal"

            End If
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")


            For i As Integer = 0 To dgvCotacao.Rows.Count - 1
                'dgvCotacao.ForeColor = System.Drawing.Color.Black
                dgvCotacao.Rows(txtlinha.Text).CssClass = "Normal"

            Next

            'dgvCotacao.Rows(txtlinha.Text).ForeColor = System.Drawing.Color.Red
            dgvCotacao.Rows(txtlinha.Text).CssClass = "selected1"

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO FROM TB_COTACAO WHERE ID_COTACAO =" & txtID.Text)
            If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 9 Then
                lkCalcular.Visible = False
            Else
                lkCalcular.Visible = True

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
            dgvCotacao.Columns(11).Visible = False

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
            dgvCotacao.Columns(11).Visible = False

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


        ds = Con.ExecutarQuery("SELECT MAX(NR_COTACAO)NR_COTACAO FROM [TB_COTACAO]")


        Dim numero_antecedente As String = ds.Tables(0).Rows(0).Item("NR_COTACAO").Substring(0, 7)

        Dim ano_antecedente As String = ds.Tables(0).Rows(0).Item("NR_COTACAO").Substring(8)

        Dim ano_atual = Now.Year.ToString.Substring(2)

        Dim var_aux As Integer
        Dim NR_COTACAO As String

        If ano_antecedente = ano_atual Then
            var_aux = numero_antecedente + 1
            NR_COTACAO = var_aux
            NR_COTACAO = NR_COTACAO.PadLeft(7, "0") & "/" & ano_atual
        Else
            NR_COTACAO = 1

            NR_COTACAO = NR_COTACAO.PadLeft(7, "0") & "/" & ano_atual
        End If

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
            dgvCotacao.Columns(11).Visible = False

            Exit Sub
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione o registro que deseja duplicar!"
            Else
                Dim numero_cotacao As String = NumeroCotacao()
                Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, DT_ENVIO_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, DT_CALCULO_COTACAO, NR_PROCESSO_GERADO, ID_USUARIO_STATUS,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN 
)    SELECT '" & numero_cotacao & "', DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, DT_ENVIO_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, DT_CALCULO_COTACAO, NR_PROCESSO_GERADO, ID_USUARIO_STATUS,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN  FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_COTACAO;

INSERT INTO TB_COTACAO_TAXA ( ID_COTACAO,
ID_TIPO_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN)    
SELECT  (Select SCOPE_IDENTITY() as ID_COTACAO),
ID_TIPO_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN FROM TB_COTACAO_TAXA 
WHERE  ID_COTACAO = " & txtID.Text & " 

INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO, ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME) 
SELECT (SELECT MAX(ID_COTACAO) FROM TB_COTACAO ), ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME
FROM TB_COTACAO_MERCADORIA WHERE  ID_COTACAO = " & txtID.Text)
                dgvCotacao.DataBind()
                divSuccess.Visible = True
                lblmsgSuccess.Text = "Item duplicado com sucesso!"
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

            dgvCotacao.Columns(11).Visible = False

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
            dgvCotacao.Columns(11).Visible = False

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


                    Dim M3 As Double
                    Dim PESO_BRUTO As Double
                    'NUMERO SEQUENCIAL


                    ds = Con.ExecutarQuery("Select A.ID_SERVICO,isnull(B.VL_M3,0)VL_M3, isnull(B.VL_PESO_BRUTO,0)VL_PESO_BRUTO,
                                                    (SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO
                                                    from TB_COTACAO A 
                                                    left JOIN TB_COTACAO_MERCADORIA B ON B.ID_COTACAO = A.ID_COTACAO
                                                    Where A.ID_COTACAO = " & txtID.Text)
                    M3 = ds.Tables(0).Rows(0).Item("VL_M3")
                    PESO_BRUTO = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")



                    Con.ExecutarQuery("UPDATE TB_COTACAO SET Dt_Calculo_Cotacao = GETDATE() WHERE ID_COTACAO = " & txtID.Text)

                    '        CÁLCULO DO PESO TAXADO
                    Dim PESO_TAXADO As Double
                    Dim PV As Double

                    If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                        PV = M3 * 0.167
                    End If

                    If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                        PESO_BRUTO = PESO_BRUTO / 1000
                    End If

                    If PESO_BRUTO >= PV Then
                        PESO_TAXADO = PESO_BRUTO
                    Else
                        PESO_TAXADO = M3
                    End If


                    PESO_TAXADO = PESO_TAXADO.ToString.Replace(".", "")
                    PESO_TAXADO = PESO_TAXADO.ToString.Replace(",", ".")

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_PESO_TAXADO = '" & PESO_TAXADO & "' WHERE ID_COTACAO = " & txtID.Text)

                    divSuccess.Visible = True
                    lblmsgSuccess.Text = "Taxa calculada com sucesso"
                    CalcTaxas()


                    dgvCotacao.DataBind()
                End If





            End If
        End If


    End Sub

    Sub CalcTaxas()
        divPesquisa.Visible = False
        divSuccess.Visible = False
        divErro.Visible = False

        Dim CompraCalc As String = ""
        Dim VendaCalc As String = ""
        Dim dataatual As Date = Now.Date.ToString("dd/MM/yyyy")
        Dim x As Double
        Dim y As Double
        Dim z As Double
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT b.ID_COTACAO_TAXA, isnull(B.VL_TAXA_COMPRA,0)VL_TAXA_COMPRA,isnull(B.VL_TAXA_VENDA,0)VL_TAXA_VENDA,B.ID_BASE_CALCULO_TAXA,isnull(C.VL_M3,0)VL_M3, isnull(C.VL_PESO_BRUTO,0)VL_PESO_BRUTO, (select CONVERT(varchar,MAX(DT_CAMBIO),103) FROM TB_MOEDA_FRETE WHERE ID_MOEDA = A.ID_MOEDA_FRETE)DT_CAMBIO From TB_COTACAO A Left Join TB_COTACAO_TAXA B ON A.ID_COTACAO = B.ID_COTACAO Left Join TB_COTACAO_MERCADORIA C ON A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & txtID.Text)
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows

                If IsDBNull(linha.Item("ID_COTACAO_TAXA")) Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Não há taxas vinculadas a essa cotação"
                    divSuccess.Visible = False

                    Exit Sub
                ElseIf IsDBNull(linha.Item("ID_BASE_CALCULO_TAXA")) Then
                    divErro.Visible = True
                    lblmsgErro.Text = "Base de Calculo não informada."
                    divSuccess.Visible = False

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


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 2 Then
                        'VR DO FRETE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 )QTD
FROM TB_COTACAO A
WHERE A.ID_COTACAO =  " & txtID.Text)
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 3 Then
                        'VR DO FRETE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 )QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO =  " & txtID.Text)
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 4 Then
                        'TOTAL DO HOUSE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(COUNT(ID_BL),0)QTD FROM TB_BL A
WHERE A.ID_COTACAO = " & txtID.Text & " AND GRAU = 'C' ")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 5 Then
                        'VALOR FIXO
                        VendaCalc = linha.Item("VL_TAXA_VENDA").ToString()
                        CompraCalc = linha.Item("VL_TAXA_COMPRA").ToString()
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 6 Then

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        VendaCalc = z.ToString

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        CompraCalc = z.ToString
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 7 Then
                        'POR TON
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text)
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_PESO_BRUTO")
                        z = y * x
                        z = z / 1000

                        CompraCalc = z.ToString
                        VendaCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 10 Then
                        'POR MAFI 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (19)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 11 Then
                        'POR CNTR 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (5,6,2,9,10,12,16,18,19)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 12 Then
                        'POR CNTR 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (17,13,14,15,11,3,4,7,8,1)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 13 Then
                        'POR TON / M³

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        VendaCalc = z.ToString

                        x = linha.Item("VL_M3")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        CompraCalc = z.ToString


                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 14 Then
                        'POR KG
                        x = linha.Item("VL_PESO_BRUTO")

                        y = linha.Item("VL_TAXA_COMPRA")
                        z = x * y
                        CompraCalc = z


                        y = linha.Item("VL_TAXA_VENDA")
                        z = x * y
                        VendaCalc = z

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 15 Then
                        '% VR DA MERCADORIA
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT (ISNULL(SUM(VL_CARGA),0) * 1/100 ) AS VALOR
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO " & txtID.Text & " ")
                        x = ds1.Tables(0).Rows(0).Item("VALOR")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("VALOR")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 16 Then
                        '% HOUSE COLLECT

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(COUNT(ID_BL),0)QTD FROM TB_BL A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_PAGAMENTO = 1 AND GRAU = 'C' ")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 20 Then
                        'POR HC 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER = 10")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 21 Then
                        'POR FLAT RACK 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (15)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 22 Then
                        ' POR OPEN TOP 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (9)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 23 Then
                        'POR OPEN TOP 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (8)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 24 Then
                        'POR FLAT RACK 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (16)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 25 Then
                        'POR REEFER 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (5)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 26 Then
                        'POR REEFER 40
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (4)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 28 Then
                        'POR MAFI 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (13)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 29 Then
                        'VALOR POR EMBARQUE- valor fixo digitado
                        VendaCalc = linha.Item("VL_TAXA_VENDA").ToString()
                        CompraCalc = linha.Item("VL_TAXA_COMPRA").ToString()
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 30 Then
                        'POR UNIDADE - quantidade de conteineres do processo

                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & "")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 31 Then
                        'POR HAWB (AEREO)- na cotação é 1 por 1
                        VendaCalc = linha.Item("VL_TAXA_VENDA").ToString()
                        CompraCalc = linha.Item("VL_TAXA_COMPRA").ToString()

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 32 Then
                        'POR HBL (MARITIMO) - na cotação é 1 por 1
                        VendaCalc = linha.Item("VL_TAXA_VENDA").ToString()
                        CompraCalc = linha.Item("VL_TAXA_COMPRA").ToString()

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 34 Then
                        'POR CNTR 
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & "")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_COMPRA")
                        z = y * x
                        CompraCalc = z.ToString

                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        y = linha.Item("VL_TAXA_VENDA")
                        z = y * x
                        VendaCalc = z.ToString
                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 35 Then
                        ' POR TEU

                        'Para cada conteiner de 20' corresponde 1 teu
                        Dim ds1 As DataSet = Con.ExecutarQuery(" Select ISNULL(SUM(QT_CONTAINER), 0)QTD
From TB_COTACAO_MERCADORIA A
Where a.ID_COTACAO = 14 And ID_TIPO_CONTAINER In (5,6,2,9,10,12,16,18)")
                        y = ds1.Tables(0).Rows(0).Item("QTD")


                        'Para cada conteiner de 40' corresponde a 2 teus

                        ds1 = Con.ExecutarQuery("Select ISNULL(SUM(QT_CONTAINER), 0)QTD
From TB_COTACAO_MERCADORIA A
Where a.ID_COTACAO = 14 And ID_TIPO_CONTAINER In (19,17,13,14,15,11,3,4,7,8,1)")
                        x = ds1.Tables(0).Rows(0).Item("QTD")
                        x = x * 2

                        z = x + y
                        VendaCalc = z.ToString
                        CompraCalc = z.ToString
                    End If


                    CompraCalc = CompraCalc.Replace(".", String.Empty).Replace(",", ".")
                    VendaCalc = VendaCalc.Replace(".", String.Empty).Replace(",", ".")


                    Con.ExecutarQuery("UPDATE TB_COTACAO_TAXA SET VL_TAXA_COMPRA_CALCULADO = '" & CompraCalc & "', VL_TAXA_VENDA_CALCULADO = '" & VendaCalc & "' WHERE ID_COTACAO_TAXA = " & linha.Item("ID_COTACAO_TAXA"))
                    divSuccess.Visible = True

                End If
            Next

        End If
    End Sub
    Private Sub bntPesquisar_Click(sender As Object, e As EventArgs) Handles bntPesquisar.Click
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
                FILTRO = " Origem LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 5 Then
                FILTRO = " Destino LIKE '%" & txtPesquisa.Text & "%' "
            Else
                FILTRO = " AGENTE LIKE '%" & txtPesquisa.Text & "%' "
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

            Dim img As Image = CType(e.Row.FindControl("image1"), Image)

            Select Case e.Row.Cells(11).Text

                Case "#000000"

                    img.ImageUrl = "Content/imagens/000000.png"
                    img.Visible = True


                Case "#0672d6"

                    img.ImageUrl = "Content/imagens/0672d6.png"
                    img.Visible = True


                Case "#159e1b"
                    img.ImageUrl = "Content/imagens/159e1b.png"
                    img.Visible = True

                Case "#7517c2"

                    img.ImageUrl = "Content/imagens/7517c2.png"
                    img.Visible = True

                Case "#b2b4eb"

                    img.ImageUrl = "Content/imagens/b2b4eb.png"
                    img.Visible = True

                Case "#e72c17"

                    img.ImageUrl = "Content/imagens/e72c17.png"
                    img.Visible = True

                Case "#e77817"

                    img.ImageUrl = "Content/imagens/e77817.png"
                    img.Visible = True

                Case "#e7d617"

                    img.ImageUrl = "Content/imagens/e7d617.png"
                    img.Visible = True

                Case "#f5d4cb"

                    img.ImageUrl = "Content/imagens/f5d4cb.png"
                    img.Visible = True

            End Select

        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja imprimir!"

        Else
            Dim url As String = ""
            ' Response.Redirect("CotacaoPDF_PT.aspx?c=" & txtID.Text)
            url = "GeraPDF.aspx?c=" & txtID.Text & "&l=" & ddlLinguagem.SelectedValue
            Response.Write("<script>")
            Response.Write("window.open('" & url & "','_blank')")
            Response.Write("</script>")
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
                Con.ExecutarQuery("DELETE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)
                lblmsgSuccess.Text = "Registro deletado!"
                divSuccess.Visible = True
                dgvCotacao.DataBind()
            End If
        End If


        Con.Fechar()

    End Sub


End Class