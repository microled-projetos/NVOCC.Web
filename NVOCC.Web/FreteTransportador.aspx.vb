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
    Sub BUSCA()
        Dim v As New VerificaData
        Dim filtro As String = ""
        If txtValidadeFinal.Text <> "" Then
            If v.ValidaData(txtValidadeFinal.Text) = False Then
                divErro.Visible = True
                lblmsgErro.Text = "Data Inválida."
            Else
                If filtro = "" Then
                    filtro &= " WHERE DT_VALIDADE_FINAL = Convert(smalldatetime, '" & txtValidadeFinal.Text & "', 103) "
                Else
                    filtro &= " AND DT_VALIDADE_FINAL = Convert(smalldatetime, '" & txtValidadeFinal.Text & "', 103) "
                End If

            End If
        End If


        If ddlTransportador.SelectedValue <> 0 Then
            If filtro = "" Then
                filtro &= " WHERE ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue
            Else
                filtro &= " AND ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue
            End If
        End If
        If ddlOrigem.SelectedValue <> 0 Then
            If filtro = "" Then
                filtro &= " WHERE ID_PORTO_ORIGEM = " & ddlOrigem.SelectedValue
            Else
                filtro &= "  AND ID_PORTO_ORIGEM = " & ddlOrigem.SelectedValue
            End If
        End If
        If ddlDestino.SelectedValue <> 0 Then
            If filtro = "" Then
                filtro &= " WHERE ID_PORTO_DESTINO = " & ddlDestino.SelectedValue
            Else
                filtro &= "  AND ID_PORTO_DESTINO = " & ddlDestino.SelectedValue
            End If
        End If

        Dim sql As String = "SELECT * FROM [View_FreteTransportador]  " & filtro & " order by ID,VL_COMPRA,QT_DIAS_FREETIME DESC "
        dsFreteTranportador.SelectCommand = sql

        dgvFreteTranportador.DataBind()
    End Sub

    Private Sub btnBusca_Click(sender As Object, e As EventArgs) Handles btnBusca.Click
        BUSCA()
    End Sub
End Class