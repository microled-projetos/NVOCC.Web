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

        If e.CommandName = "Selecionar" Then

            For i As Integer = 0 To dgvCotacao.Rows.Count - 1
                dgvCotacao.Rows(i).CssClass = "Normal"

            Next
            Dim ID As String = e.CommandArgument


            txtID.Text = ID.Substring(0, ID.IndexOf("|"))

            txtlinha.Text = ID.Substring(ID.IndexOf("|"))
            txtlinha.Text = txtlinha.Text.Replace("|", "")

            If txtlinha.Text > 100 Then
                txtlinha.Text = txtlinha.Text.Substring(1)
            End If
            dgvCotacao.Rows(txtlinha.Text).CssClass = "selected1"




            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO,ID_TIPO_ESTUFAGEM,ID_SERVICO,NR_COTACAO FROM TB_COTACAO WHERE ID_COTACAO =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 9 Then
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
                Con.ExecutarQuery("INSERT INTO TB_COTACAO (NR_COTACAO, DT_ABERTURA, ID_STATUS_COTACAO, DT_STATUS_COTACAO, DT_VALIDADE_COTACAO, DT_ENVIO_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, DT_CALCULO_COTACAO, NR_PROCESSO_GERADO, ID_USUARIO_STATUS,ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR 
)    SELECT '" & numero_cotacao & "', DT_ABERTURA, 2, GETDATE(), DT_VALIDADE_COTACAO, DT_ENVIO_COTACAO, ID_ANALISTA_COTACAO, ID_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_DESTINATARIO_COMERCIAL, ID_CLIENTE, ID_CLIENTE_FINAL, ID_CONTATO, ID_SERVICO, ID_VENDEDOR, OB_CLIENTE, OB_MOTIVO_CANCELAMENTO, OB_OPERACIONAL, ID_MOTIVO_CANCELAMENTO, DT_CALCULO_COTACAO, NULL, " & Session("ID_USUARIO") & ", ID_PORTO_DESTINO,ID_PORTO_ESCALA1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_PORTO_ORIGEM,QT_TRANSITTIME_INICIAL, QT_TRANSITTIME_FINAL,ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, ID_FRETE_TRANSPORTADOR,VL_TIPO_DIVISAO_FRETE, VL_DIVISAO_FRETE, ID_TIPO_DIVISAO_FRETE,VL_PESO_TAXADO, ID_TIPO_BL, ID_TRANSPORTADOR,ID_TIPO_CARGA,ID_VIA_ROTA,ID_TIPO_ESTUFAGEM,ID_PROCESSO,ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_MIN,ID_PARCEIRO_INDICADOR   FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_COTACAO;

INSERT INTO TB_COTACAO_TAXA (ID_COTACAO,
ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN)    
SELECT  (Select SCOPE_IDENTITY() as ID_COTACAO),
ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_DESTINATARIO_COBRANCA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA_CALCULADO,ID_MOEDA_VENDA,VL_TAXA_VENDA_CALCULADO,ID_BASE_CALCULO_TAXA,OB_TAXAS,VL_TAXA_VENDA_MIN,VL_TAXA_COMPRA,VL_TAXA_VENDA,VL_TAXA_COMPRA_MIN FROM TB_COTACAO_TAXA 
WHERE  ID_COTACAO = " & txtID.Text & " 

INSERT INTO TB_COTACAO_MERCADORIA ( ID_COTACAO, ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME) 
SELECT (SELECT MAX(ID_COTACAO) FROM TB_COTACAO ), ID_MERCADORIA, ID_TIPO_CONTAINER, QT_CONTAINER, VL_FRETE_COMPRA,
 VL_FRETE_VENDA, VL_PESO_BRUTO, VL_M3, DS_MERCADORIA, VL_COMPRIMENTO, VL_LARGURA, VL_ALTURA, VL_CARGA, QT_DIAS_FREETIME
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

                    Dim VENDA_MIN As Decimal
                    Dim M3 As Decimal
                    Dim PESO_BRUTO As Decimal
                    Dim QT_CONTAINER As Integer
                    'NUMERO SEQUENCIAL


                    'ds = Con.ExecutarQuery("Select A.ID_TIPO_ESTUFAGEM,A.ID_SERVICO,isnull(B.VL_M3,0)VL_M3, isnull(B.VL_PESO_BRUTO,0)VL_PESO_BRUTO,isnull(A.VL_TOTAL_FRETE_VENDA_MIN,0)VL_TOTAL_FRETE_VENDA_MIN,isnull(A.VL_TOTAL_FRETE_VENDA,0)VL_TOTAL_FRETE_VENDA,
                    '                                (SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO, isnull(B.QT_CONTAINER,0) AS QT_CONTAINER
                    '                                from TB_COTACAO A 
                    '                                left JOIN TB_COTACAO_MERCADORIA B ON B.ID_COTACAO = A.ID_COTACAO
                    '                                Where A.ID_COTACAO = " & txtID.Text)

                    ds = Con.ExecutarQuery("Select A.ID_TIPO_ESTUFAGEM,
A.ID_SERVICO,
isnull(A.VL_TOTAL_M3,0)VL_M3, 
isnull(A.VL_TOTAL_PESO_BRUTO,0)VL_PESO_BRUTO,
isnull(A.VL_TOTAL_FRETE_VENDA_MIN,0)VL_TOTAL_FRETE_VENDA_MIN,
isnull(A.VL_TOTAL_FRETE_VENDA,0)VL_TOTAL_FRETE_VENDA,
(select sum(isnull(QT_CONTAINER,0)) FROM TB_COTACAO_MERCADORIA B WHERE B.ID_COTACAO = A.ID_COTACAO )QT_CONTAINER,
(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO
from TB_COTACAO A 
Where A.ID_COTACAO = " & txtID.Text)

                    M3 = ds.Tables(0).Rows(0).Item("VL_M3")
                    PESO_BRUTO = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                    VENDA_MIN = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA_MIN")
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_CONTAINER")) Then
                        QT_CONTAINER = ds.Tables(0).Rows(0).Item("QT_CONTAINER")
                    End If


                    Con.ExecutarQuery("UPDATE TB_COTACAO SET Dt_Calculo_Cotacao = GETDATE() WHERE ID_COTACAO = " & txtID.Text)

                    '        CÁLCULO DO PESO TAXADO
                    Dim PESO_TAXADO As Decimal
                    Dim PV As Decimal = M3

                    If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                        'AEREO

                        PV = M3 * 167
                        ' PESO_BRUTO = PESO_BRUTO / 1000
                        If PESO_BRUTO >= PV Then
                            PESO_TAXADO = PESO_BRUTO
                        Else
                            PESO_TAXADO = PV
                        End If
                    End If

                    If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                        'MARITIMO
                        PESO_BRUTO = PESO_BRUTO / 1000

                        If PESO_BRUTO >= M3 Then
                            PESO_TAXADO = PESO_BRUTO
                        Else
                            PESO_TAXADO = M3
                        End If
                    End If


                    Dim FRETE_CALCULADO As Decimal = ds.Tables(0).Rows(0).Item("VL_TOTAL_FRETE_VENDA")


                    If ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 1 Then
                        'ID_BASE_CALCULO 34 - POR CNTR
                        '  FRETE_CALCULADO = (FRETE_CALCULADO * QT_CONTAINER)
                        If FRETE_CALCULADO < VENDA_MIN Then
                            FRETE_CALCULADO = VENDA_MIN
                        End If
                    ElseIf ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM") = 2 Then
                        'ID_BASE_CALCULO 13 - POR TON / M³
                        FRETE_CALCULADO = (FRETE_CALCULADO * PESO_TAXADO)
                        If FRETE_CALCULADO < VENDA_MIN Then
                            FRETE_CALCULADO = VENDA_MIN
                        End If

                    End If




                    Dim Peso_Final As String = PESO_TAXADO.ToString
                    Peso_Final = Peso_Final.ToString.Replace(".", "")
                    Peso_Final = Peso_Final.ToString.Replace(",", ".")

                    Dim frete_Final As String = FRETE_CALCULADO.ToString
                    frete_Final = frete_Final.ToString.Replace(".", "")
                    frete_Final = frete_Final.ToString.Replace(",", ".")

                    Con.ExecutarQuery("UPDATE TB_COTACAO SET VL_PESO_TAXADO = " & Peso_Final & ",VL_TOTAL_FRETE_VENDA_CALCULADO = " & frete_Final & "  WHERE ID_COTACAO = " & txtID.Text)

                    divSuccess.Visible = True
                    lblmsgSuccess.Text = "Taxa calculada com sucesso"
                    CalcTaxas()


                    dgvCotacao.DataBind()
                End If



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

        'Dim ds As DataSet = Con.ExecutarQuery("SELECT b.ID_COTACAO_TAXA, isnull(B.VL_TAXA_COMPRA,0)VL_TAXA_COMPRA,isnull(B.VL_TAXA_VENDA,0)VL_TAXA_VENDA,B.ID_BASE_CALCULO_TAXA,isnull(C.VL_M3,0)VL_M3, isnull(C.VL_PESO_BRUTO,0)VL_PESO_BRUTO, (select CONVERT(varchar,MAX(DT_CAMBIO),103) FROM TB_MOEDA_FRETE WHERE ID_MOEDA = A.ID_MOEDA_FRETE)DT_CAMBIO, isnull(B.VL_TAXA_COMPRA_MIN,0)VL_TAXA_COMPRA_MIN, isnull(B.VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN From TB_COTACAO A Left Join TB_COTACAO_TAXA B ON A.ID_COTACAO = B.ID_COTACAO Left Join TB_COTACAO_MERCADORIA C ON A.ID_COTACAO = C.ID_COTACAO WHERE A.ID_COTACAO = " & txtID.Text)


        Dim ds As DataSet = Con.ExecutarQuery("SELECT a.ID_SERVICO,b.ID_COTACAO_TAXA,isnull(A.VL_PESO_TAXADO,0) VL_PESO_TAXADO,a.ID_MOEDA_FRETE,
isnull(B.VL_TAXA_COMPRA,0)VL_TAXA_COMPRA,
isnull(B.VL_TAXA_VENDA,0)VL_TAXA_VENDA,
B.ID_BASE_CALCULO_TAXA,isnull(A.VL_TOTAL_M3,0)VL_M3, 
isnull(A.VL_TOTAL_PESO_BRUTO,0)VL_PESO_BRUTO, 
(select CONVERT(varchar,MAX(DT_CAMBIO),103) FROM TB_MOEDA_FRETE WHERE ID_MOEDA = A.ID_MOEDA_FRETE)DT_CAMBIO,
isnull(B.VL_TAXA_COMPRA_MIN,0)VL_TAXA_COMPRA_MIN,
isnull(B.VL_TAXA_VENDA_MIN,0)VL_TAXA_VENDA_MIN,
isnull(B.ID_MOEDA_COMPRA,0)ID_MOEDA_COMPRA,
isnull(B.ID_MOEDA_VENDA,0)ID_MOEDA_VENDA
From TB_COTACAO A 
Left Join TB_COTACAO_TAXA B ON A.ID_COTACAO = B.ID_COTACAO 
WHERE A.ID_COTACAO =" & txtID.Text)
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Dim COMPRA_MIN As Decimal = linha.Item("VL_TAXA_COMPRA_MIN")
                Dim VENDA_MIN As Decimal = linha.Item("VL_TAXA_VENDA_MIN")
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
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 )QTD
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
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(VL_TOTAL_FRETE_VENDA),0) * 1/100 )QTD
FROM TB_COTACAO_MERCADORIA A
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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 4 Then
                        '% TOTAL DO HOUSE
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT 
   sum(VL_TAXA_VENDA_CALCULADO) + VL_TOTAL_FRETE_VENDA_CALCULADO TOTAL_VENDA, 
   
   sum(VL_TAXA_COMPRA_CALCULADO) + VL_TOTAL_FRETE_COMPRA TOTAL_COMPRA
FROM TB_COTACAO A
INNER JOIN TB_COTACAO_TAXA B ON B.ID_COTACAO = A.ID_COTACAO
WHERE B.FL_DECLARADO = 1
AND B.ID_MOEDA_VENDA = B.ID_MOEDA_VENDA 
AND A.ID_COTACAO = " & txtID.Text & "
GROUP BY A.ID_COTACAO,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_COMPRA")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 11 Then
                        'POR CNTR 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (5,6,2,9,10,12,16,18,19)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 12 Then
                        'POR CNTR 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (17,13,14,15,11,3,4,7,8,1)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 21 Then
                        'POR FLAT RACK 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (15)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 22 Then
                        ' POR OPEN TOP 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (9)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 23 Then
                        'POR OPEN TOP 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (8)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 24 Then
                        'POR FLAT RACK 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (16)")
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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 25 Then
                        'POR REEFER 20'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (5)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 26 Then
                        'POR REEFER 40
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER in (4)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 28 Then
                        'POR MAFI 40'
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text & " AND ID_TIPO_CONTAINER IN (13)")

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

                    ElseIf linha.Item("ID_BASE_CALCULO_TAXA") = 34 Then
                        'POR CNTR 
                        Dim ds1 As DataSet = Con.ExecutarQuery("SELECT ISNULL(SUM(QT_CONTAINER),0)QTD
FROM TB_COTACAO_MERCADORIA A
WHERE A.ID_COTACAO = " & txtID.Text)

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
                FILTRO = " Origem LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 5 Then
                FILTRO = " Destino LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 6 Then
                FILTRO = " AGENTE LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 7 Then
                FILTRO = " VENDEDOR LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 8 Then
                FILTRO = " NR_PROCESSO_GERADO LIKE '%" & txtPesquisa.Text & "%' "
            ElseIf ddlConsultas.SelectedValue = 9 Then
                FILTRO = " ANALISTA_COTACAO LIKE '%" & txtPesquisa.Text & "%' "

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
            Dim url As String = ""
            url = "GeraPDF.aspx?c=" & txtID.Text & "&l=" & ddlLinguagem.SelectedValue & "&f=e"
            Response.Redirect(url)

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
                GRID()
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
                Dim reaprovamento As Boolean = False
                ds = Con.ExecutarQuery("SELECT ID_STATUS_COTACAO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("ID_STATUS_COTACAO") = 10 Then
                        reaprovamento = True
                    Else
                        reaprovamento = False
                    End If
                Else
                    reaprovamento = False
                End If



                If NumeroProcesso(reaprovamento) = False Then
                    lkAprovar.Visible = True
                    divErro.Visible = True
                    lblmsgErro.Text = "Já existe processo para esta cotação!"
                    Exit Sub
                End If

                Con.ExecutarQuery("UPDATE TB_COTACAO SET DT_ENVIO_COTACAO = GETDATE(), ID_STATUS_COTACAO = 9, DT_STATUS_COTACAO = GETDATE(), ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)

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


    Function NumeroProcesso(Optional reaprovamento As Boolean = False) As Boolean
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim PROCESSO_FINAL As String = ""
        Dim ID_BL_OLD As String = 0
        Dim ID_BL As String
        Dim OB_CLIENTE As String = ""
        Dim OB_AGENTE_INTERNACIONAL As String = ""
        Dim OB_COMERCIAL As String = ""
        Dim OB_OPERACIONAL_INTERNA As String = ""
        Dim HBL As String = "0"
        If reaprovamento = True Then

            ds = Con.ExecutarQuery("SELECT NR_PROCESSO_GERADO FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)
            PROCESSO_FINAL = ds.Tables(0).Rows(0).Item("NR_PROCESSO_GERADO")
            ds = Con.ExecutarQuery("SELECT ID_BL FROM TB_BL WHERE GRAU='C' AND ID_COTACAO = " & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                ID_BL_OLD = ds.Tables(0).Rows(0).Item("ID_BL")

                'SALVA OBS DO CLIENTE, DO AGENTE, DO COMERCIAL E DO OPERACIONAL CADASTRADO NA BL ANTIGA PARA O NOVO BL
                ds = Con.ExecutarQuery("SELECT ISNULL(NR_BL,'0')HBL,ISNULL(OB_CLIENTE,'')OB_CLIENTE, ISNULL(OB_AGENTE_INTERNACIONAL,'')OB_AGENTE_INTERNACIONAL, ISNULL(OB_COMERCIAL,'')OB_COMERCIAL, ISNULL(OB_OPERACIONAL_INTERNA,'')OB_OPERACIONAL_INTERNA  FROM TB_BL WHERE ID_BL = " & ID_BL_OLD)
                If ds.Tables(0).Rows.Count > 0 Then
                    OB_CLIENTE = ds.Tables(0).Rows(0).Item("OB_CLIENTE")
                    OB_AGENTE_INTERNACIONAL = ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")
                    OB_COMERCIAL = ds.Tables(0).Rows(0).Item("OB_COMERCIAL")
                    OB_OPERACIONAL_INTERNA = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")
                    HBL = ds.Tables(0).Rows(0).Item("HBL")
                End If


                'DELETA BL ANTIGO
                Con.ExecutarQuery("DELETE FROM TB_BL WHERE GRAU = 'C' AND ID_BL = " & ID_BL_OLD)
            End If
        ElseIf reaprovamento = False Then

                ds = Con.ExecutarQuery("SELECT NEXT VALUE FOR Seq_Processo_" & Now.Year.ToString & " NRSEQUENCIALPROCESSO")
            Dim NRSEQUENCIALPROCESSO As Integer = ds.Tables(0).Rows(0).Item("NRSEQUENCIALPROCESSO")
            Dim ano_atual = Now.Year.ToString.Substring(2)
            Dim SIGLA_PROCESSO As String
            Dim mes_atual As String

            If Now.Month < 10 Then
                mes_atual = "0" & Now.Month.ToString
            Else
                mes_atual = Now.Month.ToString
            End If


            ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO
                            from TB_COTACAO A Where A.ID_COTACAO = " & txtID.Text)

            SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

            PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

            Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "', ANOSEQUENCIALPROCESSO = year(getdate()) ")

            Con.ExecutarQuery("UPDATE TB_COTACAO SET NR_PROCESSO_GERADO = '" & PROCESSO_FINAL & "' WHERE ID_COTACAO = " & txtID.Text)

        End If

        ds = Con.ExecutarQuery("SELECT COUNT(ID_COTACAO)QTD FROM TB_BL WHERE GRAU='C' AND DT_CANCELAMENTO IS NULL AND ID_COTACAO = " & txtID.Text)

        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Dim dsBL As DataSet = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,GRAU,ID_SERVICO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_PARCEIRO_TRANSPORTADOR,ID_COTACAO,DT_ABERTURA,VL_PROFIT_DIVISAO,ID_PROFIT_DIVISAO,VL_FRETE,ID_MOEDA_FRETE,ID_PARCEIRO_VENDEDOR,ID_TIPO_PAGAMENTO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_IMPORTADOR ) 
SELECT '" & PROCESSO_FINAL & "','C', " & txtServico.Text & ",ID_CLIENTE,ID_AGENTE_INTERNACIONAL,ID_INCOTERM,ID_TIPO_ESTUFAGEM,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_TIPO_CARGA,ID_TRANSPORTADOR,ID_COTACAO,GETDATE(),VL_DIVISAO_FRETE,ID_TIPO_DIVISAO_FRETE,VL_TOTAL_FRETE_VENDA,ID_MOEDA_FRETE,ID_VENDEDOR,ID_TIPO_PAGAMENTO,FL_FREE_HAND,ID_STATUS_FRETE_AGENTE,ID_PARCEIRO_INDICADOR,ID_PARCEIRO_EXPORTADOR,CASE WHEN ID_PARCEIRO_IMPORTADOR IS NULL THEN ID_CLIENTE WHEN ID_PARCEIRO_IMPORTADOR = 0 THEN ID_CLIENTE ELSE ID_PARCEIRO_IMPORTADOR END ID_PARCEIRO_IMPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_BL ")
            ID_BL = dsBL.Tables(0).Rows(0).Item("ID_BL").ToString()


            'TAXAS COMPRAS
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,CD_ORIGEM_INF) 
SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA_COMPRA,VL_TAXA_COMPRA,VL_TAXA_COMPRA_CALCULADO,VL_TAXA_COMPRA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'P',ID_FORNECEDOR,'COTA' FROM TB_COTACAO_TAXA
 WHERE VL_TAXA_COMPRA IS NOT NULL AND VL_TAXA_COMPRA <> 0 AND ID_COTACAO = " & txtID.Text)

            'TAXAS VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,OB_TAXAS,ID_BL,FL_TAXA_TRANSPORTADOR,CD_PR,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF) 
 SELECT ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_BASE_CALCULO_TAXA,ID_MOEDA_VENDA,VL_TAXA_VENDA,VL_TAXA_VENDA_CALCULADO,VL_TAXA_VENDA_MIN,OB_TAXAS," & ID_BL & ",FL_TAXA_TRANSPORTADOR,'R',
 
 CASE 
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") 
 
 WHEN ID_DESTINATARIO_COBRANCA = 2
 THEN (SELECT ID_AGENTE_INTERNACIONAL FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ")
 
 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") = 0
 THEN (SELECT ID_CLIENTE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ")
 
 WHEN ID_DESTINATARIO_COBRANCA = 4 and (SELECT isnull(ID_PARCEIRO_IMPORTADOR,0) FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") <> 0 then
 (SELECT ID_PARCEIRO_IMPORTADOR FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & ") 

 ELSE NULL
 END ID_PARCEIRO_EMPRESA,
 

CASE
 WHEN isnull(ID_DESTINATARIO_COBRANCA,0) <= 1 
 THEN 1 
 ELSE ID_DESTINATARIO_COBRANCA
 END ID_DESTINATARIO_COBRANCA,'COTA'

from TB_COTACAO_TAXA WHERE VL_TAXA_VENDA IS NOT NULL AND VL_TAXA_VENDA <> 0 AND  ID_COTACAO = " & txtID.Text)

            Dim FL_PROFIT_FRETE As Integer = 0
            Dim dsProfit As DataSet = Con.ExecutarQuery("SELECT isnull(FL_PROFIT_FRETE,0)FL_PROFIT_FRETE FROM [dbo].TB_TIPO_DIVISAO_PROFIT WHERE ID_TIPO_DIVISAO_PROFIT IN (SELECT ID_TIPO_DIVISAO_FRETE FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text & " ) ")
            If dsProfit.Tables(0).Rows.Count > 0 Then
                FL_PROFIT_FRETE = dsProfit.Tables(0).Rows(0).Item("FL_PROFIT_FRETE")
            End If


            Dim ID_BASE_CALCULO As Integer
            If txtEstufagem.Text = 1 Then
                ID_BASE_CALCULO = 5
                'FRETE COMPRA
                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,FL_TAXA_TRANSPORTADOR,CD_ORIGEM_INF)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA,VL_TOTAL_FRETE_COMPRA_MIN," & ID_BL & ",'P',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & ",  
 
 ID_TRANSPORTADOR AS ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA,
 1,'COTA'

 FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)
            ElseIf txtEstufagem.Text = 2 Then
                ID_BASE_CALCULO = 13
            Else
                ID_BASE_CALCULO = 0
            End If



            'FRETE VENDA
            Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_ITEM_DESPESA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN,ID_BL,CD_PR,ID_TIPO_PAGAMENTO,FL_DIVISAO_PROFIT,ID_PARCEIRO_EMPRESA,ID_DESTINATARIO_COBRANCA,CD_ORIGEM_INF)
 SELECT (SELECT ID_ITEM_FRETE_MASTER FROM TB_PARAMETROS)," & ID_BASE_CALCULO & ",ID_MOEDA_FRETE,VL_TOTAL_FRETE_VENDA,VL_TOTAL_FRETE_VENDA_CALCULADO,VL_TOTAL_FRETE_VENDA_MIN," & ID_BL & ",'R',ID_TIPO_PAGAMENTO, " & FL_PROFIT_FRETE & " ,
 
CASE WHEN (ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6) AND ISNULL(ID_PARCEIRO_IMPORTADOR,0) <> 0
 THEN ID_PARCEIRO_IMPORTADOR
 ELSE ID_CLIENTE
 END ID_PARCEIRO_EMPRESA, 
 
 CASE WHEN ID_DESTINATARIO_COMERCIAL = 1 OR  ID_DESTINATARIO_COMERCIAL = 6
 THEN 4
 ELSE 1
 END ID_DESTINATARIO_COBRANCA,'COTA' 

 FROM TB_COTACAO WHERE ID_COTACAO = " & txtID.Text)


            Dim dsCarga As DataSet


            If txtEstufagem.Text = 1 Then

                dsCarga = Con.ExecutarQuery("SELECT ID_COTACAO_MERCADORIA,QT_CONTAINER FROM TB_COTACAO_MERCADORIA
 WHERE QT_CONTAINER is not null and QT_CONTAINER <> 0 and ID_COTACAO = " & txtID.Text)
                If dsCarga.Tables(0).Rows.Count > 0 Then
                    Dim QT_CONTAINER As Integer
                    For Each linha As DataRow In dsCarga.Tables(0).Rows
                        QT_CONTAINER = linha.Item("QT_CONTAINER")

                        For i As Integer = 1 To QT_CONTAINER Step 1
                            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL,ID_TIPO_CNTR) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,isnull(VL_PESO_BRUTO,0)/isnull(QT_CONTAINER,0)VL_PESO_BRUTO,isnull(VL_M3,0)/isnull(QT_CONTAINER,0)VL_M3," & ID_BL & ",ID_TIPO_CONTAINER  FROM TB_COTACAO_MERCADORIA
        WHERE ID_COTACAO_MERCADORIA =  " & linha.Item("ID_COTACAO_MERCADORIA"))
                        Next
                    Next
                Else
                    Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_MERCADORIA,ID_EMBALAGEM,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,ID_BL) SELECT ID_MERCADORIA,ID_MERCADORIA,QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO," & ID_BL & " FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)
                End If


            ElseIf txtEstufagem.Text = 2 Then

                Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (QT_MERCADORIA,VL_PESO_BRUTO,VL_M3,ID_BL) 
SELECT SUM(QT_MERCADORIA)QT_MERCADORIA,SUM(VL_PESO_BRUTO)VL_PESO_BRUTO,SUM(VL_M3)VL_M3," & ID_BL & " FROM TB_COTACAO_MERCADORIA
 WHERE ID_COTACAO =  " & txtID.Text)

            End If

        Else

            Return False


        End If


        If reaprovamento = True Then



            'PASSA OBS DO CLIENTE, DO AGENTE, DO COMERCIAL E DO OPERACIONAL CADASTRADO NA BL ANTIGA PARA O NOVO BL
            If OB_CLIENTE <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = '" & OB_CLIENTE & "' WHERE  ID_BL = " & ID_BL)
            End If
            If OB_AGENTE_INTERNACIONAL <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_AGENTE_INTERNACIONAL = '" & OB_AGENTE_INTERNACIONAL & "' WHERE  ID_BL = " & ID_BL)
            End If
            If OB_COMERCIAL <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_COMERCIAL = '" & OB_COMERCIAL & "' WHERE ID_BL = " & ID_BL)
            End If
            If OB_OPERACIONAL_INTERNA <> "" Then
                Con.ExecutarQuery("UPDATE TB_BL SET OB_OPERACIONAL_INTERNA = '" & OB_OPERACIONAL_INTERNA & "' WHERE ID_BL = " & ID_BL)
            End If

            'INSERE NR_BL ANTIGO NO REGISTRO NOVO
            Con.ExecutarQuery("UPDATE TB_BL SET NR_BL = '" & HBL & "' WHERE ID_BL = " & ID_BL)

            'DELETA BL ANTIGO
            Con.ExecutarQuery("DELETE FROM TB_BL WHERE GRAU = 'C' AND ID_BL = " & ID_BL_OLD)

            'DELETA TAXAS ANTIGAS QUE VIERAM DA COTAÇÃO
            Con.ExecutarQuery("DELETE FROM TB_BL_TAXA WHERE ID_BL = " & ID_BL_OLD & " AND CD_ORIGEM_INF = 'COTA' ")

            'ALTERA ID_BL DAS TAXAS ANTIGAS QUE NAO VIERAM DA COTACAO PARA O NOVO O ID_BL
            Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL = " & ID_BL & " WHERE ID_BL = " & ID_BL_OLD & " AND CD_ORIGEM_INF = 'OPER' ")

            'ALTERA ID_BL DA REFERENCIA DO CLIENTE PARA O NOVO BL
            Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET ID_BL = " & ID_BL & " WHERE ID_BL = " & ID_BL_OLD)

            If txtEstufagem.Text = 1 Then

                'DELETA CARGAS ANTIGAS QUE VIERAM DA COTAÇÃO
                Con.ExecutarQuery("DELETE FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL_OLD & " AND ID_MERCADORIA in (SELECT OLD.ID_MERCADORIA FROM TB_CARGA_BL OLD
INNER JOIN TB_CARGA_BL NEW ON NEW.ID_MERCADORIA = OLD.ID_MERCADORIA 
WHERE OLD.ID_BL = " & ID_BL_OLD & " AND NEW.ID_BL = " & ID_BL & ")")

                'ALTERA CARGAS ANTIGAS QUE NAO VIERAM DA COTACAO PARA O NOVO O ID_BL
                Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & ID_BL & " WHERE ID_BL = " & ID_BL_OLD)

            ElseIf txtEstufagem.Text = 2 Then

                'DELETA CARGAS 
                Con.ExecutarQuery("DELETE FROM TB_CARGA_BL WHERE ID_BL = " & ID_BL_OLD)

            End If




        End If

        Return True

    End Function


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
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione um registro!"
            Else
                Dim dsVerifica As DataSet = Con.ExecutarQuery("SELECT ID_BL_MASTER FROM [TB_BL] WHERE ID_COTACAO = " & txtID.Text)
                If dsVerifica.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(dsVerifica.Tables(0).Rows(0).Item("ID_BL_MASTER")) Then
                        divErro.Visible = True
                        lblmsgErro.Text = "Não foi possivel completar a ação: O house desta cotação já possui um Master!"
                    Else
                        Con.ExecutarQuery("UPDATE TB_COTACAO SET ID_STATUS_COTACAO = 10, DT_STATUS_COTACAO = GETDATE() , ID_USUARIO_STATUS = " & Session("ID_USUARIO") & "  WHERE ID_COTACAO = " & txtID.Text)
                        divSuccess.Visible = True
                        lblmsgSuccess.Text = "Item atualizado para 'Em update' com sucesso!"
                        GRID()
                    End If

                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Instrução de embarque não encontrada para cotação selecionada!"
                End If

            End If
        End If

    End Sub
End Class