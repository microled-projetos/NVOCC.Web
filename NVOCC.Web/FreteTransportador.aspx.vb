'Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.Drawing
Imports OfficeOpenXml.Drawing
Imports OfficeOpenXml.Style
Imports OfficeOpenXml
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Web



Imports Microsoft.Office.Interop.Excel



Public Class FreteTransportador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        If Not Page.IsPostBack Then
            txtID.Text = ""
        End If

        Page.MaintainScrollPositionOnPostBack = True

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If

        Con.Fechar()
    End Sub

    'Private Sub dgvFreteTranportador_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgvFreteTranportador.SelectedIndexChanged
    '    txtID.Text = dgvFreteTranportador.SelectedValue

    '    If txtlinha.Text <> "" Then
    '        Dim linhavelha As Integer = txtlinha.Text
    '        dgvFreteTranportador.Rows(linhavelha).CssClass = "normal"
    '    End If

    '    Dim linhanova As Integer = dgvFreteTranportador.SelectedIndex
    '    txtlinha.Text = linhanova
    '    dgvFreteTranportador.Rows(linhanova).CssClass = "selected1"

    'End Sub

    'Private Sub dgvFreteTranportador_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvFreteTranportador.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim lb As LinkButton = CType(e.Row.FindControl("lbSelecionar"), LinkButton)
    '        e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""))
    '    End If


    'End Sub


    Private Sub lkInserir_Click(sender As Object, e As EventArgs) Handles lkInserir.Click
        Response.Redirect("CadastrarFreteTransportador.aspx")
    End Sub

    Private Sub lkAlterar_Click(sender As Object, e As EventArgs) Handles lkAlterar.Click

        divSuccess.Visible = False
        divErro.Visible = False

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja editar!"
        Else
            Dim url As String = "/CadastrarFreteTransportador.aspx?id={0}"
            url = String.Format(url, txtID.Text)
            Response.Redirect(url)
        End If

    End Sub

    Private Sub lkSair_Click(sender As Object, e As EventArgs) Handles lkSair.Click
        Response.Redirect("Default.aspx")

    End Sub

    Private Sub lkInativar_Click(sender As Object, e As EventArgs) Handles lkInativar.Click
        divSuccess.Visible = False
        divErro.Visible = False

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja desativar!"
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ATIVO FROM [TB_FRETE_TRANSPORTADOR] WHERE ID_FRETE_TRANSPORTADOR =" & txtID.Text)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("FL_ATIVO") <> True Then

                    Con.ExecutarQuery("UPDATE [dbo].[TB_FRETE_TRANSPORTADOR] SET FL_ATIVO = 1 WHERE ID_FRETE_TRANSPORTADOR =" & txtID.Text)
                    dgvFreteTranportador.DataBind()
                    divSuccess.Visible = True
                    lblmsgSuccess.Text = "Item ativado com sucesso"
                Else
                    Con.ExecutarQuery("UPDATE [dbo].[TB_FRETE_TRANSPORTADOR] SET FL_ATIVO = 0 WHERE ID_FRETE_TRANSPORTADOR =" & txtID.Text)
                    dgvFreteTranportador.DataBind()
                    divSuccess.Visible = True
                    lblmsgSuccess.Text = "Item desativado com sucesso"

                End If
            End If
            Con.Fechar()
        End If




    End Sub

    Private Sub lkDuplicar_Click(sender As Object, e As EventArgs) Handles lkDuplicar.Click
        divSuccess.Visible = False
        divErro.Visible = False

        If txtID.Text = "" Then
            divErro.Visible = True
            lblmsgErro.Text = "Selecione o registro que deseja duplicar!"
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()
            Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR ( ID_TRANSPORTADOR, ID_AGENTE, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PORTO_ESCALA, ID_MOEDA_FRETE, ID_TIPO_CARGA, ID_VIA_ROTA, ID_TIPO_COMEX, QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL, QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, FL_ATIVO, DT_VALIDADE_FINAL)    SELECT ID_TRANSPORTADOR, ID_AGENTE, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PORTO_ESCALA, ID_MOEDA_FRETE, ID_TIPO_CARGA, ID_VIA_ROTA, ID_TIPO_COMEX, QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL, QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, FL_ATIVO, DT_VALIDADE_FINAL FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text & " Select SCOPE_IDENTITY() as ID_FRETE_TRANSPORTADOR;
INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR, ID_TIPO_CONTAINER, ID_TIPO_ESTUFAGEM, DT_VALIDADE_INICIAL, VL_COMPRA, VL_MINIMO, QT_DIAS_FREETIME, FL_IMO, FL_CARGA_ESPECIAL, VL_M3_INICIAL, VL_M3_FINAL, ID_MERCADORIA,SERVICO)    SELECT  (Select SCOPE_IDENTITY() as ID_FRETE_TRANSPORTADOR),ID_TIPO_CONTAINER, ID_TIPO_ESTUFAGEM, DT_VALIDADE_INICIAL, VL_COMPRA, VL_MINIMO, QT_DIAS_FREETIME, FL_IMO, FL_CARGA_ESPECIAL, VL_M3_INICIAL, VL_M3_FINAL, ID_MERCADORIA,SERVICO FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text & " INSERT INTO TB_TABELA_FRETE_TAXA (  ID_FRETE_TRANSPORTADOR, ID_TIPO_ESTUFAGEM, ID_ITEM_DESPESA, ID_ORIGEM_PAGAMENTO, ID_BASE_CALCULO_TAXA, ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA,VL_TAXA_VENDA_MIN)    SELECT(SELECT IDENT_CURRENT('TB_FRETE_TRANSPORTADOR')), ID_TIPO_ESTUFAGEM, ID_ITEM_DESPESA, ID_ORIGEM_PAGAMENTO, ID_BASE_CALCULO_TAXA, ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA,VL_TAXA_VENDA_MIN FROM TB_TABELA_FRETE_TAXA WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
            dgvFreteTranportador.DataBind()
            divSuccess.Visible = True
            lblmsgSuccess.Text = "Item duplicado com sucesso!"
        End If

    End Sub

    Private Sub lkFiltrar_Click(sender As Object, e As EventArgs) Handles lkFiltrar.Click
        Response.Redirect("BuscaFreteTransportador.aspx")
    End Sub
    Protected Sub dgvFreteTranportador_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        divSuccess.Visible = False
        divErro.Visible = False
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvFreteTranportador.DataSource = Session("TaskTable")
            dgvFreteTranportador.DataBind()
            dgvFreteTranportador.HeaderRow.TableSection = TableRowSection.TableHeader
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



    Private Sub dgvFreteTranportador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFreteTranportador.RowCommand
        If e.CommandName = "Edit" Then

            txtID.Text = e.CommandArgument
            Page.MaintainScrollPositionOnPostBack = True
        End If

    End Sub



    Private Sub ddlConsultas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConsultas.SelectedIndexChanged
        If ddlConsultas.SelectedValue = 1 Then
            ocean.Visible = True
            locais.Visible = False
            txtDataInicial.Text = Now.Date.ToString("dd-MM-yyyy")


        ElseIf ddlConsultas.SelectedValue = 2 Then
            locais.Visible = True
            ocean.Visible = False

        Else
            ocean.Visible = False
            locais.Visible = False

        End If
    End Sub

    Private Sub bntPesquisarLocais_Click(sender As Object, e As EventArgs) Handles bntPesquisarLocais.Click

        If ddlOrigemLocais.SelectedValue = 0 Or ddlDestinoLocais.SelectedValue = 0 Or ddlTransportadorLocais.SelectedValue = 0 Then
            lblmsgErro.Text = "Preencha os filtros para pesquisar"
        Else
            Dim sql As String = "SELECT A.ID_FRETE_TRANSPORTADOR as Id ,A.ID_TRANSPORTADOR,G.NM_FANTASIA Transportador, A.ID_AGENTE, F.NM_FANTASIA,A.ID_PORTO_ORIGEM,D.NM_PORTO as PORTO_ORIGEM,A.ID_PORTO_DESTINO,E.NM_PORTO as PORTO_DESTINO,A.QT_DIAS_TRANSITTIME_INICIAL,A.QT_DIAS_TRANSITTIME_FINAL,A.QT_DIAS_TRANSITTIME_MEDIA,A.FL_ATIVO,A.DT_VALIDADE_FINAL, 
case when A.FL_Ativo = 1 then 'Sim' else 'Não' end as Ativo,

B.ID_TARIFARIO_FRETE_TRANSPORTADOR,B.DT_VALIDADE_INICIAL,B.VL_COMPRA,B.QT_DIAS_FREETIME,

C.ID_TABELA_FRETE_TAXA

FROM TB_FRETE_TRANSPORTADOR A
                Left Join TB_TARIFARIO_FRETE_TRANSPORTADOR B ON B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR
                Left Join TB_TABELA_FRETE_TAXA C ON B.ID_FRETE_TRANSPORTADOR = a.ID_FRETE_TRANSPORTADOR  
            left Join TB_PORTO D ON D.ID_PORTO = A.ID_PORTO_ORIGEM
             left Join TB_PORTO E ON E.ID_PORTO = A.ID_PORTO_DESTINO
            left join TB_PARCEIRO F ON F.ID_PARCEIRO = A.ID_AGENTE
                        left join TB_PARCEIRO G ON G.ID_PARCEIRO = A.ID_TRANSPORTADOR
WHERE A.ID_TRANSPORTADOR = " & ddlTransportadorLocais.SelectedValue & " AND A.ID_PORTO_ORIGEM = " & ddlOrigemLocais.SelectedValue & " AND A.ID_PORTO_DESTINO = " & ddlDestinoLocais.SelectedValue & "
           order by B.VL_COMPRA, B.QT_DIAS_FREETIME DESC "
            dgvFreteTranportador.DataBind()
            dsFreteTranportador.SelectCommand = sql

        End If


    End Sub

    Private Sub bntPesquisarOcean_Click(sender As Object, e As EventArgs) Handles bntPesquisarOcean.Click
        Dim v As New VerificaData

        If v.ValidaData(txtDataFinal.Text) = False Or v.ValidaData(txtDataInicial.Text) = False Then
            divErro.Visible = True
            lblmsgErro.Text = "Data Inválida."
        ElseIf ddlTransportadorOcean.SelectedValue = 0 Or ddlContainer.SelectedValue = 0 Or ddlOrigemOcean.SelectedValue = 0 Or ddlDestinoOcena.SelectedValue = 0 Then
            lblmsgErro.Text = "Preencha os filtros para pesquisar"
        Else
            Dim sql As String = "SELECT A.ID_FRETE_TRANSPORTADOR as Id ,A.ID_TRANSPORTADOR,G.NM_FANTASIA Transportador, A.ID_AGENTE, F.NM_FANTASIA,A.ID_PORTO_ORIGEM,D.NM_PORTO as PORTO_ORIGEM,A.ID_PORTO_DESTINO,E.NM_PORTO as PORTO_DESTINO,A.QT_DIAS_TRANSITTIME_INICIAL,A.QT_DIAS_TRANSITTIME_FINAL,A.QT_DIAS_TRANSITTIME_MEDIA,A.FL_ATIVO,A.DT_VALIDADE_FINAL, 
case when A.FL_Ativo = 1 then 'Sim' else 'Não' end as Ativo,

B.ID_TARIFARIO_FRETE_TRANSPORTADOR,B.DT_VALIDADE_INICIAL,B.VL_COMPRA,B.QT_DIAS_FREETIME,

C.ID_TABELA_FRETE_TAXA

FROM TB_FRETE_TRANSPORTADOR A
                Left Join TB_TARIFARIO_FRETE_TRANSPORTADOR B ON B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR
                Left Join TB_TABELA_FRETE_TAXA C ON B.ID_FRETE_TRANSPORTADOR = a.ID_FRETE_TRANSPORTADOR  
            left Join TB_PORTO D ON D.ID_PORTO = A.ID_PORTO_ORIGEM
             left Join TB_PORTO E ON E.ID_PORTO = A.ID_PORTO_DESTINO
            left join TB_PARCEIRO F ON F.ID_PARCEIRO = A.ID_AGENTE
                        left join TB_PARCEIRO G ON G.ID_PARCEIRO = A.ID_TRANSPORTADOR
WHERE A.ID_TRANSPORTADOR = " & ddlTransportadorOcean.SelectedValue & " AND A.ID_PORTO_ORIGEM = " & ddlOrigemOcean.SelectedValue & " AND A.ID_PORTO_DESTINO = " & ddlDestinoOcena.SelectedValue & " AND ID_TIPO_CONTAINER = " & ddlContainer.SelectedValue & " AND A.DT_VALIDADE_FINAL BETWEEN Convert(smalldatetime, '" & txtDataInicial.Text & "', 103) AND Convert(smalldatetime, '" & txtDataFinal.Text & "', 103)  order by B.VL_COMPRA, B.QT_DIAS_FREETIME DESC "
            dsFreteTranportador.SelectCommand = sql

            dgvFreteTranportador.DataBind()
        End If


    End Sub



    Private Sub dgvFreteTranportador_PreRender(sender As Object, e As EventArgs) Handles dgvFreteTranportador.PreRender
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvFreteTranportador.Columns(10).Visible = False
        End If
        Con.Fechar()

    End Sub





    Private Sub lkRemover_Click(sender As Object, e As EventArgs) Handles lkRemover.Click
        divSuccess.Visible = False
        divErro.Visible = False
        divPesquisa.Visible = False


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            lblmsgErro.Text = "Usuário não tem permissão para realizar exclusões"
            divErro.Visible = True
        Else
            If txtID.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Selecione o registro que deseja excluir!"
            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_COTACAO)QTD FROM TB_COTACAO WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

                    Con.ExecutarQuery("DELETE FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                    lblmsgSuccess.Text = "Registro deletado!"
                    divSuccess.Visible = True
                    dgvFreteTranportador.DataBind()
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Há uma ou mais cotações utilizando essa tabela de frete!"
                End If

            End If
        End If
        Con.Fechar()

    End Sub








    'testes de csv e xls
    Private Sub lkExportar_Click(sender As Object, e As EventArgs) Handles lkExportar.Click
        Dim SQL As String = "SELECT 
    ORIGEM.NM_PORTO AS POL,
    DESTINO.NM_PORTO As POD, 
    ROTA.NM_VIA_ROTA AS VIA,
    A.QT_DIAS_TRANSITTIME_MEDIA As TRANSITTIME,
    TRANSPORTADOR.NM_RAZAO AS CARRIER /*ARMADOR*/,
    A.NM_TAXAS_INCLUDED As INCLUDED,

    (select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And C.TAMANHO_CONTAINER = 20 ) 'FREIGHT - 20 CNTR',

    (select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And C.TAMANHO_CONTAINER = 40 ) 'FREIGHT - 40 CNTR',

    (select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And FL_NOR = 1 ) 'FREIGHT - NOR', 

    A.ORIGIN_CHARGES,

    (select MAX( B.QT_DIAS_FREETIME)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR )'FREE TIME - DRY/HC',

    (select MAX(QT_DIAS_FREETIME)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And FL_NOR = 1  ) 'FREE TIME - NOR',

    (select MAX(B.DT_VALIDADE_INICIAL )AS DT_VALIDADE_INICIAL from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) VALIDITY

    From TB_FRETE_TRANSPORTADOR A
    Left Join TB_PORTO ORIGEM ON ORIGEM.ID_PORTO = A.ID_PORTO_ORIGEM
    Left Join TB_PORTO DESTINO ON DESTINO.ID_PORTO = A.ID_PORTO_DESTINO
    Left Join TB_VIA_ROTA ROTA ON ROTA.ID_VIA_ROTA = A.ID_VIA_ROTA
    Left Join TB_PARCEIRO TRANSPORTADOR ON TRANSPORTADOR.ID_PARCEIRO = A.ID_TRANSPORTADOR"

        Classes.Excel.exportaExcel(SQL, "NVOCC", "Tarifario Importacao")

        SQL = ""


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PORTO from TB_TAXA_LOCAL_TRANSPORTADOR ORDER BY ID_PORTO")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Dim ID_PORTO As Integer = linha.Item("ID_PORTO").ToString()
                SQL &= "SELECT ID_PORTO, NM_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA from TB_TAXA_LOCAL_TRANSPORTADOR A
LEFT JOIN TB_ITEM_DESPESA B ON A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA  WHERE ID_PORTO = " & ID_PORTO & " and AND ID_TIPO_COMEX = 1 ;"
            Next
        End If

        Classes.Excel.exportaExcel(SQL, "NVOCC", "Taxas Locais FCL - Impo")



        SQL = ""


        ds = Con.ExecutarQuery("SELECT ID_PORTO from TB_TAXA_LOCAL_TRANSPORTADOR ORDER BY ID_PORTO")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Dim ID_PORTO As Integer = linha.Item("ID_PORTO").ToString()
                SQL &= "SELECT ID_PORTO, NM_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA from TB_TAXA_LOCAL_TRANSPORTADOR A
LEFT JOIN TB_ITEM_DESPESA B ON A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA  WHERE ID_PORTO = " & ID_PORTO & " and AND ID_TIPO_COMEX = 2 ;"
            Next
        End If


        Classes.Excel.exportaExcel(SQL, "NVOCC", "Taxas Locais FCL - Expo")
        Con.Fechar()
    End Sub









    Private Function PegaDados1() As DataTable

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT 
ORIGEM.NM_PORTO AS POL, 
DESTINO.NM_PORTO AS POD, 
ROTA.NM_VIA_ROTA AS VIA,
A.QT_DIAS_TRANSITTIME_MEDIA AS TRANSITTIME,
TRANSPORTADOR.NM_RAZAO AS CARRIER /*ARMADOR*/, 
A.NM_TAXAS_INCLUDED AS INCLUDED,

(select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
LEFT JOIN TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR AND C.TAMANHO_CONTAINER = 20 ) 'FREIGHT - 20 CNTR',

(select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
LEFT JOIN TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR AND C.TAMANHO_CONTAINER = 40 ) 'FREIGHT - 40 CNTR',

(select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
LEFT JOIN TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR AND FL_NOR = 1 ) 'FREIGHT - NOR', 

A.ORIGIN_CHARGES,

(select MAX( B.QT_DIAS_FREETIME)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) 'FREE TIME - DRY/HC',

(select MAX(QT_DIAS_FREETIME)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
LEFT JOIN TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR AND FL_NOR = 1  ) 'FREE TIME - NOR',

(select MAX(B.DT_VALIDADE_INICIAL )AS DT_VALIDADE_INICIAL from TB_TARIFARIO_FRETE_TRANSPORTADOR B
WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) VALIDITY

FROM TB_FRETE_TRANSPORTADOR A
LEFT JOIN TB_PORTO ORIGEM ON ORIGEM.ID_PORTO = A.ID_PORTO_ORIGEM
LEFT JOIN TB_PORTO DESTINO ON DESTINO.ID_PORTO = A.ID_PORTO_DESTINO
LEFT JOIN TB_VIA_ROTA ROTA ON ROTA.ID_VIA_ROTA = A.ID_VIA_ROTA
LEFT JOIN TB_PARCEIRO TRANSPORTADOR ON TRANSPORTADOR.ID_PARCEIRO = A.ID_TRANSPORTADOR")
        Con.Fechar()
        Return ds.Tables(0)
    End Function

    Private Function PegaDados2() As DataTable
        Dim sql_final As String = ""

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PORTO from TB_TAXA_LOCAL_TRANSPORTADOR ORDER BY ID_PORTO")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each linha As DataRow In ds.Tables(0).Rows
                Dim ID_PORTO As Integer = linha.Item("ID_PORTO").ToString()
                sql_final &= "SELECT ID_PORTO, NM_ITEM_DESPESA,VL_TAXA_LOCAL_COMPRA from TB_TAXA_LOCAL_TRANSPORTADOR A
LEFT JOIN TB_ITEM_DESPESA B ON A.ID_ITEM_DESPESA = B.ID_ITEM_DESPESA  WHERE ID_PORTO = " & ID_PORTO & "  ;"
            Next
        End If
        ds = Con.ExecutarQuery(sql_final)
        Con.Fechar()
        Return ds.Tables(0)
    End Function
    Sub DataTabletoXLS(ByVal DT As DataTable, ByVal fileName As String)
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.Charset = "utf-16"
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250")
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}.xls", fileName))
            HttpContext.Current.Response.ContentType = "application/ms-excel"
            Dim tab As String = ""

            For Each dc As DataColumn In DT.Columns
                HttpContext.Current.Response.Write(tab & dc.ColumnName.Replace(vbLf, "").Replace(vbTab, ""))
                tab = vbTab
            Next

            HttpContext.Current.Response.Write(vbLf)
            Dim i As Integer

            For Each dr As DataRow In DT.Rows
                tab = ""

                For i = 0 To DT.Columns.Count - 1
                    HttpContext.Current.Response.Write(tab & dr(i).ToString().Replace(vbLf, "").Replace(vbTab, ""))
                    tab = vbTab
                Next

                HttpContext.Current.Response.Write(vbLf)
            Next

            HttpContext.Current.Response.[End]()
        Catch ex As Exception
            Throw ex

        End Try

    End Sub

    Private Sub lkExportaTarifario_Click(sender As Object, e As EventArgs) Handles lkExportaTarifario.Click
        Dim SQL As String = "SELECT 
    ORIGEM.NM_PORTO AS POL,
    DESTINO.NM_PORTO As POD, 
    ROTA.NM_VIA_ROTA AS VIA,
    A.QT_DIAS_TRANSITTIME_MEDIA As TRANSITTIME,
    TRANSPORTADOR.NM_RAZAO AS CARRIER /*ARMADOR*/,
    A.NM_TAXAS_INCLUDED As INCLUDED,

    (select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And C.TAMANHO_CONTAINER = 20 ) 'FREIGHT - 20 CNTR',

    (select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And C.TAMANHO_CONTAINER = 40 ) 'FREIGHT - 40 CNTR',

    (select MAX(VL_COMPRA)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And FL_NOR = 1 ) 'FREIGHT - NOR', 

    A.ORIGIN_CHARGES,

    (select MAX( B.QT_DIAS_FREETIME)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR )'FREE TIME - DRY/HC',

    (select MAX(QT_DIAS_FREETIME)AS QT_DIAS_FREETIME from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    Left Join TB_TIPO_CONTAINER C  on C.ID_TIPO_CONTAINER = B.ID_TIPO_CONTAINER
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR And FL_NOR = 1  ) 'FREE TIME - NOR',

    (select MAX(B.DT_VALIDADE_INICIAL )AS DT_VALIDADE_INICIAL from TB_TARIFARIO_FRETE_TRANSPORTADOR B
    WHERE B.ID_FRETE_TRANSPORTADOR = A.ID_FRETE_TRANSPORTADOR ) VALIDITY

    From TB_FRETE_TRANSPORTADOR A
    Left Join TB_PORTO ORIGEM ON ORIGEM.ID_PORTO = A.ID_PORTO_ORIGEM
    Left Join TB_PORTO DESTINO ON DESTINO.ID_PORTO = A.ID_PORTO_DESTINO
    Left Join TB_VIA_ROTA ROTA ON ROTA.ID_VIA_ROTA = A.ID_VIA_ROTA
    Left Join TB_PARCEIRO TRANSPORTADOR ON TRANSPORTADOR.ID_PARCEIRO = A.ID_TRANSPORTADOR"

        Classes.Excel.exportaExcel(SQL, "NVOCC", "Tarifario Importacao")
    End Sub

End Class