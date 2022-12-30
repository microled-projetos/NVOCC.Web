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

Public Class FreteTransportador_New
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If


        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If


        Con.Fechar()
        CarregaPortos()
    End Sub
    Sub CarregaPortos()
        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim tabela As String = "<label style='font-size:12px'><strong>Porto Origem:<label></strong><select data-live-search='True' ID='comboOrigem' data-live-search-style='startsWith' class='form-control selectpicker'style='width:270px !important;' multiple='multiple' onchange='IDOrigem()'  title='Selecione'>"
        Dim dsdados As DataSet
        Dim sql As String = "SELECT ID_PORTO, NM_PORTO + ' - ' + CONVERT(VARCHAR,CD_PORTO) AS NM_PORTO FROM [dbo].[TB_PORTO] WHERE NM_PORTO IS NOT NULL AND FL_ATIVO = 1"
        If ddlViaTransporte.SelectedValue <> "" Then
            If ddlViaTransporte.SelectedValue <> 0 Then

                sql &= " AND ID_VIATRANSPORTE = " & ddlViaTransporte.SelectedValue
            End If
        End If
        sql &= " ORDER BY NM_PORTO "

        'ORIGEM
        dsdados = Con.ExecutarQuery(sql)
        If dsdados.Tables(0).Rows.Count > 0 Then

            For Each linhadados As DataRow In dsdados.Tables(0).Rows
                tabela &= "<option value='" & linhadados("ID_PORTO") & "'>" & linhadados("NM_PORTO") & "</option>"
            Next

        End If
        tabela &= "</select>"
        divOrigem.InnerHtml = tabela

        'DESTINO
        tabela = "<label style='font-size:12px'><strong>Porto Destino:<label></strong><select data-live-search='True' ID='comboDestino' data-live-search-style='startsWith' class='form-control selectpicker' style='width:270px !important;' multiple='multiple' onchange='IDDestino()'  title='Selecione'>"
        dsdados = Con.ExecutarQuery(sql)
        If dsdados.Tables(0).Rows.Count > 0 Then

            For Each linhadados As DataRow In dsdados.Tables(0).Rows
                tabela &= "<option value='" & linhadados("ID_PORTO") & "'>" & linhadados("NM_PORTO") & "</option>"
            Next

        End If
        tabela &= "</select>"
        divDestino.InnerHtml = tabela
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

    Sub CheckUncheckAll()
        Dim chk1 As CheckBox
        chk1 = DirectCast(dgvFreteTranportador.HeaderRow.Cells(0).FindControl("ckbSelecionarTodos"), CheckBox)
        For Each row As GridViewRow In dgvFreteTranportador.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("ckbSelecionar"), CheckBox)
            If chk.Enabled = True Then
                chk.Checked = chk1.Checked
            End If
        Next
    End Sub

    Private Sub dgvFreteTranportador_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvFreteTranportador.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        divSuccessCntr.Visible = False
        divErroCntr.Visible = False

        If e.CommandName = "Edit" Then

            txtID.Text = e.CommandArgument

        ElseIf e.CommandName = "Excluir" Then

            txtID.Text = e.CommandArgument

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblmsgErro.Text = "Usuário não tem permissão para realizar exclusões"
                divErro.Visible = True
            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_COTACAO)QTD FROM TB_COTACAO WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR_HIST (ID_FRETE_TRANSPORTADOR,ACAO,ID_USUARIO,DATA) VALUES (" & txtID.Text & ",'EXCLUSÃO'," & Session("ID_USUARIO") & ", GETDATE()) ")

                    Con.ExecutarQuery("DELETE FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                    Con.ExecutarQuery("DELETE FROM TB_TABELA_FRETE_TAXA WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                    Con.ExecutarQuery("DELETE FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                    lblmsgSuccess.Text = "Registro deletado!"
                    divSuccess.Visible = True
                    dgvFreteTranportador.DataBind()
                Else
                    divErro.Visible = True
                    lblmsgErro.Text = "Há uma ou mais cotações utilizando essa tabela de frete!"
                End If

            End If
            Con.Fechar()
            BUSCA()
        ElseIf e.CommandName = "Duplicar" Then

            Dim ID As String = e.CommandArgument
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR ( ID_TRANSPORTADOR, ID_AGENTE, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PORTO_ESCALA, ID_MOEDA_FRETE, ID_TIPO_CARGA, ID_VIA_ROTA, ID_TIPO_COMEX, QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL, QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, FL_ATIVO, DT_VALIDADE_FINAL,FL_DUPLICATA,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_VIATRANSPORTE,ID_ORIGEM_PAGAMENTO)    SELECT ID_TRANSPORTADOR, ID_AGENTE, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PORTO_ESCALA, ID_MOEDA_FRETE, ID_TIPO_CARGA, ID_VIA_ROTA, ID_TIPO_COMEX, QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL, QT_DIAS_TRANSITTIME_MEDIA, ID_TIPO_FREQUENCIA, VL_FREQUENCIA, NM_TAXAS_INCLUDED, FL_ATIVO, DT_VALIDADE_FINAL,1,ID_PORTO_ESCALA2,ID_PORTO_ESCALA3,ID_VIATRANSPORTE,ID_ORIGEM_PAGAMENTO FROM TB_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & ID & " Select SCOPE_IDENTITY() as ID_FRETE_TRANSPORTADOR;
            INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR, ID_TIPO_CONTAINER, ID_TIPO_ESTUFAGEM, DT_VALIDADE_INICIAL, VL_COMPRA, VL_MINIMO, QT_DIAS_FREETIME, FL_IMO, FL_CARGA_ESPECIAL, VL_M3_INICIAL, VL_M3_FINAL, ID_MERCADORIA,SERVICO,DT_VALIDADE_FINAL)    SELECT  (Select SCOPE_IDENTITY() as ID_FRETE_TRANSPORTADOR),ID_TIPO_CONTAINER, ID_TIPO_ESTUFAGEM, DT_VALIDADE_INICIAL, VL_COMPRA, VL_MINIMO, QT_DIAS_FREETIME, FL_IMO, FL_CARGA_ESPECIAL, VL_M3_INICIAL, VL_M3_FINAL, ID_MERCADORIA,SERVICO,DT_VALIDADE_FINAL FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_FRETE_TRANSPORTADOR = " & ID & " INSERT INTO TB_TABELA_FRETE_TAXA (  ID_FRETE_TRANSPORTADOR, ID_TIPO_ESTUFAGEM, ID_ITEM_DESPESA, ID_ORIGEM_PAGAMENTO, ID_BASE_CALCULO_TAXA, ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,QTD_BASE_CALCULO)    SELECT(SELECT IDENT_CURRENT('TB_FRETE_TRANSPORTADOR')), ID_TIPO_ESTUFAGEM, ID_ITEM_DESPESA, ID_ORIGEM_PAGAMENTO, ID_BASE_CALCULO_TAXA, ID_MOEDA_COMPRA, VL_TAXA_COMPRA, ID_MOEDA_VENDA, VL_TAXA_VENDA,VL_TAXA_VENDA_MIN,QTD_BASE_CALCULO FROM TB_TABELA_FRETE_TAXA WHERE ID_FRETE_TRANSPORTADOR = " & ID)
            Con.Fechar()

            dgvFreteTranportador.DataBind()
            divSuccess.Visible = True
            lblmsgSuccess.Text = "Item duplicado com sucesso!"
            BUSCA()
        ElseIf e.CommandName = "Cntr" Then

            txtID.Text = e.CommandArgument
            dsCntr.SelectParameters("ID_FRETE_TRANSPORTADOR").DefaultValue = txtID.Text
            dgvCntr.DataBind()
            Dim row As GridViewRow = CType(((CType(e.CommandSource, Control)).NamingContainer), GridViewRow)
            If row.RowState = 5 Then
                dgvCntr.Columns(0).Visible = True


            Else
                dgvCntr.Columns(0).Visible = False
            End If


            mpeCntr.Show()

        ElseIf e.CommandName = "Historico" Then

            dsHistorico.SelectParameters("ID_FRETE_TRANSPORTADOR").DefaultValue = e.CommandArgument
            dgvHistorico.DataBind()
            mpeHistorico.Show()

        ElseIf e.CommandName = "Cliente" Then

            TextBox2.Text = e.CommandArgument

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "copiarTexto()", True)

        ElseIf e.CommandName = "Interna" Then

            TextBox2.Text = e.CommandArgument

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "copiarTexto()", True)


        ElseIf e.CommandName = "Atualizar" Then

            txtID.Text = e.CommandArgument

            For Each linha As GridViewRow In dgvFreteTranportador.Rows
                Dim ID_FRETE_TRANSPORTADOR As String = CType(linha.FindControl("lblID"), Label).Text
                Dim ID_PORTO_ORIGEM As DropDownList = CType(linha.FindControl("ddlOrigem"), DropDownList)
                Dim ID_PORTO_DESTINO As DropDownList = CType(linha.FindControl("ddlDestino"), DropDownList)
                Dim ID_TIPO_CARGA As DropDownList = CType(linha.FindControl("ddlTipoCarga"), DropDownList)
                Dim ID_TRANSPORTADOR As DropDownList = CType(linha.FindControl("ddlTransportador"), DropDownList)
                Dim ID_AGENTE As DropDownList = CType(linha.FindControl("ddlAgente"), DropDownList)
                Dim QT_DIAS_TRANSITTIME_INICIAL As TextBox = CType(linha.FindControl("txtTTInicial"), TextBox)
                Dim QT_DIAS_TRANSITTIME_FINAL As TextBox = CType(linha.FindControl("txtTTFinal"), TextBox)
                Dim FL_ATIVO As CheckBox = CType(linha.FindControl("ckAtivo"), CheckBox)
                Dim DT_VALIDADE_FINAL As TextBox = CType(linha.FindControl("txtValidadeFinal"), TextBox)
                Dim OBS_CLIENTE As TextBox = CType(linha.FindControl("txtCliente"), TextBox)
                Dim OBS_INTERNA As TextBox = CType(linha.FindControl("txtInterna"), TextBox)
                Dim ID_TIPO_FREQUENCIA As DropDownList = CType(linha.FindControl("ddlFrequencia"), DropDownList)
                Dim ID_VIA_ROTA As DropDownList = CType(linha.FindControl("ddlRota"), DropDownList)
                Dim FL_CONSOLIDADA As CheckBox = CType(linha.FindControl("ckConsolidada"), CheckBox)


                If ID_FRETE_TRANSPORTADOR = txtID.Text Then
                    Dim Con As New Conexao_sql
                    Con.Conectar()

                    'REALIZA UPDATE DO FRETE TRANSPORTADOR
                    Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR_HIST (ID_FRETE_TRANSPORTADOR,ACAO,ID_USUARIO,DATA) VALUES (" & txtID.Text & ",'EDIÇÃO'," & Session("ID_USUARIO") & ", GETDATE()) ")

                    Con.ExecutarQuery("UPDATE TB_FRETE_TRANSPORTADOR  SET  ID_TRANSPORTADOR = " & ID_TRANSPORTADOR.SelectedValue & ", ID_AGENTE = " & ID_AGENTE.SelectedValue & ", ID_PORTO_ORIGEM = " & ID_PORTO_ORIGEM.SelectedValue & " , ID_PORTO_DESTINO = " & ID_PORTO_DESTINO.SelectedValue & ", ID_TIPO_CARGA = " & ID_TIPO_CARGA.SelectedValue & ", ID_VIA_ROTA =  " & ID_VIA_ROTA.SelectedValue & " , QT_DIAS_TRANSITTIME_INICIAL =  " & QT_DIAS_TRANSITTIME_INICIAL.Text & ", QT_DIAS_TRANSITTIME_FINAL = " & QT_DIAS_TRANSITTIME_FINAL.Text & ", ID_TIPO_FREQUENCIA = " & ID_TIPO_FREQUENCIA.SelectedValue & ",  FL_ATIVO = '" & FL_ATIVO.Checked & "', OBS_INTERNA =  '" & OBS_INTERNA.Text & "', OBS_CLIENTE =  '" & OBS_CLIENTE.Text & "', DT_VALIDADE_FINAL =  CONVERT(DATETIME,'" & DT_VALIDADE_FINAL.Text & "',103), FL_CONSOLIDADA = '" & FL_CONSOLIDADA.Checked & "'  WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                    txtID.Text = ""
                    Con.Fechar()


                    Exit For

                End If

            Next

            divSuccess.Visible = True
            lblmsgSuccess.Text = "Registro atualizado com sucesso!"
            dgvFreteTranportador.SetEditRow(-1)
            BUSCA()


        ElseIf e.CommandName = "Incluir" Then

            Dim ID_PORTO_ORIGEM As DropDownList = dgvFreteTranportador.FooterRow.FindControl("ddlOrigem")
            Dim ID_PORTO_DESTINO As DropDownList = dgvFreteTranportador.FooterRow.FindControl("ddlDestino")
            Dim ID_TIPO_CARGA As DropDownList = dgvFreteTranportador.FooterRow.FindControl("ddlTipoCarga")
            Dim ID_TRANSPORTADOR As DropDownList = dgvFreteTranportador.FooterRow.FindControl("ddlTransportador")
            Dim ID_AGENTE As DropDownList = dgvFreteTranportador.FooterRow.FindControl("ddlAgente")
            Dim QT_DIAS_TRANSITTIME_INICIAL As TextBox = dgvFreteTranportador.FooterRow.FindControl("txtTTInicial")
            Dim QT_DIAS_TRANSITTIME_FINAL As TextBox = dgvFreteTranportador.FooterRow.FindControl("txtTTFinal")
            Dim FL_ATIVO As CheckBox = dgvFreteTranportador.FooterRow.FindControl("ckAtivo")
            Dim DT_VALIDADE_FINAL As TextBox = dgvFreteTranportador.FooterRow.FindControl("txtValidadeFinal")
            Dim OBS_CLIENTE As TextBox = dgvFreteTranportador.FooterRow.FindControl("txtCliente")
            Dim OBS_INTERNA As TextBox = dgvFreteTranportador.FooterRow.FindControl("txtInterna")
            Dim ID_TIPO_FREQUENCIA As DropDownList = dgvFreteTranportador.FooterRow.FindControl("ddlFrequencia")
            Dim ID_VIA_ROTA As DropDownList = dgvFreteTranportador.FooterRow.FindControl("ddlRota")
            Dim FL_CONSOLIDADA As CheckBox = dgvFreteTranportador.FooterRow.FindControl("ckConsolidada")

            If DT_VALIDADE_FINAL.Text = "" Then
                divErro.Visible = True
                lblmsgErro.Text = "Validade final é obrigatoria!!"
                Exit Sub
            End If

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR ( ID_TRANSPORTADOR, ID_AGENTE, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_TIPO_CARGA, ID_VIA_ROTA, QT_DIAS_TRANSITTIME_INICIAL, QT_DIAS_TRANSITTIME_FINAL, ID_TIPO_FREQUENCIA, FL_ATIVO,DT_VALIDADE_FINAL,OBS_CLIENTE,OBS_INTERNA,FL_CONSOLIDADA) VALUES (" & ID_TRANSPORTADOR.SelectedValue & "," & ID_AGENTE.SelectedValue & "," & ID_PORTO_ORIGEM.SelectedValue & " ," & ID_PORTO_DESTINO.SelectedValue & ", " & ID_TIPO_CARGA.SelectedValue & ", " & ID_VIA_ROTA.SelectedValue & ",  '" & QT_DIAS_TRANSITTIME_INICIAL.Text & "','" & QT_DIAS_TRANSITTIME_FINAL.Text & "'," & ID_TIPO_FREQUENCIA.SelectedValue & ",'" & FL_ATIVO.Checked & "', CONVERT(DATETIME,'" & DT_VALIDADE_FINAL.Text & "',103),'" & OBS_CLIENTE.Text & "','" & OBS_INTERNA.Text & "','" & FL_CONSOLIDADA.Checked & "' ) Select SCOPE_IDENTITY() as ID_FRETE_TRANSPORTADOR ")

            Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR_HIST (ID_FRETE_TRANSPORTADOR,ACAO,ID_USUARIO,DATA) VALUES (" & ds.Tables(0).Rows(0).Item("ID_FRETE_TRANSPORTADOR").ToString() & ",'INCLUSÃO'," & Session("ID_USUARIO") & ", GETDATE()) ")

            Con.Fechar()

            BUSCA()

            divSuccess.Visible = True
            lblmsgSuccess.Text = "Registro cadastrado com sucesso!"
            Exit Sub


        ElseIf e.CommandName = "ExpandirRecolher" Then

            For Each linha As GridViewRow In dgvFreteTranportador.Rows
                Dim ID As String = CType(linha.FindControl("lblID"), Label).Text
                Dim check As CheckBox = linha.FindControl("ckbSelecionar")

                If check.Checked Then

                    txtID.Text = ID

                    Exit For

                End If
            Next


            dsCntr.SelectParameters("ID_FRETE_TRANSPORTADOR").DefaultValue = txtID.Text
            dgvCntr.DataBind()

            Dim row As GridViewRow = CType(((CType(e.CommandSource, Control)).NamingContainer), GridViewRow)
            If row.RowState = 5 Then
                dgvCntr.Columns(0).Visible = True

            Else
                dgvCntr.Columns(0).Visible = False
            End If

            mpeCntr.Show()


        End If


    End Sub

    Private Sub dgvFreteTranportador_PreRender(sender As Object, e As EventArgs) Handles dgvFreteTranportador.PreRender
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
            dgvFreteTranportador.Columns(11).Visible = False
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

        If txtFiltroID.Text <> "" Then
            If filtro = "" Then
                filtro &= " WHERE ID_FRETE_TRANSPORTADOR = " & txtFiltroID.Text
            Else
                filtro &= " AND ID_FRETE_TRANSPORTADOR = " & txtFiltroID.Text
            End If
        End If

        If txtValidadeInicial.Text <> "" Then
            If v.ValidaData(txtValidadeInicial.Text) = False Then
                divErro.Visible = True
                lblmsgErro.Text = "Data Inválida."
            Else
                If filtro = "" Then
                    filtro &= " WHERE Convert(date,DT_VALIDADE_FINAL, 103) = Convert(date, '" & txtValidadeInicial.Text & "', 103) "
                Else
                    filtro &= " AND Convert(date,DT_VALIDADE_FINAL, 103) = Convert(date, '" & txtValidadeInicial.Text & "', 103) "
                End If

            End If
        End If

        If txtValidadeFinal.Text <> "" Then
            If v.ValidaData(txtValidadeFinal.Text) = False Then
                divErro.Visible = True
                lblmsgErro.Text = "Data Inválida."
            Else
                If filtro = "" Then
                    filtro &= " WHERE Convert(date,DT_VALIDADE_FINAL, 103) = Convert(date, '" & txtValidadeFinal.Text & "', 103) "
                Else
                    filtro &= " AND Convert(date,DT_VALIDADE_FINAL, 103) = Convert(date, '" & txtValidadeFinal.Text & "', 103) "
                End If

            End If
        End If

        'Transportador
        If ddlTransportador.SelectedValue <> 0 Then
            If filtro = "" Then
                filtro &= " WHERE ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue
            Else
                filtro &= " AND ID_TRANSPORTADOR = " & ddlTransportador.SelectedValue
            End If
        End If

        'Agente
        If ddlAgente.SelectedValue <> 0 Then
            If filtro = "" Then
                filtro &= " WHERE ID_AGENTE = " & ddlAgente.SelectedValue
            Else
                filtro &= " AND ID_AGENTE = " & ddlAgente.SelectedValue
            End If
        End If

        'Origem
        If txtOrigem.Text <> "" Then
            If filtro = "" Then
                filtro &= " WHERE ID_PORTO_ORIGEM IN ( " & txtOrigem.Text & " )"
            Else
                filtro &= "  AND ID_PORTO_ORIGEM IN ( " & txtOrigem.Text & " )"
            End If
        End If

        'Destino
        If txtDestino.Text <> "" Then
            If filtro = "" Then
                filtro &= " WHERE ID_PORTO_DESTINO IN ( " & txtDestino.Text & " )"
            Else
                filtro &= "  AND ID_PORTO_DESTINO IN ( " & txtDestino.Text & " )"
            End If
        End If

        'Inativo
        If ckInativo.Checked = True Then
            If filtro = "" Then
                filtro &= " WHERE DT_VALIDADE_FINAL < GETDATE() "
            Else
                filtro &= "  AND DT_VALIDADE_FINAL < GETDATE() "
            End If
        Else
            If filtro = "" Then
                filtro &= " WHERE DT_VALIDADE_FINAL >= GETDATE() "
            Else
                filtro &= "  AND DT_VALIDADE_FINAL >= GETDATE() "
            End If
        End If

        'Consolidada
        If ckConsolidada.Checked = True Then
            If filtro = "" Then
                filtro &= " WHERE FL_CONSOLIDADA = 1 "
            Else
                filtro &= "  AND FL_CONSOLIDADA = 1  "
            End If
        Else
            If filtro = "" Then
                filtro &= " WHERE FL_CONSOLIDADA = 0 "
            Else
                filtro &= "  AND FL_CONSOLIDADA = 0 "
            End If
        End If

        Dim sql As String = "SELECT TOP 50 * FROM [View_FreteTransportador_new]  " & filtro & " order by ID_FRETE_TRANSPORTADOR DESC"
        dsFreteTranportador.SelectCommand = sql

        dgvFreteTranportador.DataBind()
    End Sub

    Private Sub btnBusca_Click(sender As Object, e As EventArgs) Handles btnBusca.Click
        divSuccess.Visible = False
        divErro.Visible = False
        divSuccessCntr.Visible = False
        divErroCntr.Visible = False
        BUSCA()
    End Sub

    Private Sub ddlViaTransporte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViaTransporte.SelectedIndexChanged
        If ddlViaTransporte.SelectedValue = 4 Then
            txtViaTransporte.Text = 4
        Else
            txtViaTransporte.Text = 1

        End If
    End Sub

    Private Sub dgvFreteTranportador_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgvFreteTranportador.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ID As Label = CType(e.Row.FindControl("lblID"), Label)
            Dim QTD_Cntr As Label = CType(e.Row.FindControl("lblQTDCNTR"), Label)
            Dim btnCntr As LinkButton = CType(e.Row.FindControl("btnCntr"), LinkButton)

            Dim Interna As Label = CType(e.Row.FindControl("lblInterna"), Label)
            Dim btnCopiarInterna As LinkButton = CType(e.Row.FindControl("btnCopiarInterna"), LinkButton)

            Dim Cliente As Label = CType(e.Row.FindControl("lblCliente"), Label)
            Dim btnCopiarCliente As LinkButton = CType(e.Row.FindControl("btnCopiarCliente"), LinkButton)

            Dim QTD_Historico As Label = CType(e.Row.FindControl("lblQTD_HISTORICO"), Label)
            Dim btnHistorico As LinkButton = CType(e.Row.FindControl("btnHistorico"), LinkButton)

            If Not Interna Is Nothing Then
                If Interna.Text = "" Then

                    btnCopiarInterna.Visible = False
                Else
                    btnCopiarInterna.Visible = True

                End If
            End If

            If Not Cliente Is Nothing Then
                If Cliente.Text = "" Then

                    btnCopiarCliente.Visible = False
                Else
                    btnCopiarCliente.Visible = True

                End If
            End If

            If Not QTD_Cntr Is Nothing Then

                If QTD_Cntr.Text = "0" Then

                    btnCntr.Visible = False
                Else
                    btnCntr.Visible = True
                End If
            End If

            If Not QTD_Historico Is Nothing Then

                If QTD_Historico.Text = "0" Then

                    btnHistorico.Visible = False

                Else
                    btnHistorico.Visible = True
                End If
            End If

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_VINCULO_USUARIO WHERE ID_TIPO_USUARIO in (1,11) AND ID_USUARIO = " & Session("ID_USUARIO"))
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                dgvFreteTranportador.Columns(17).Visible = False

            Else
                dgvFreteTranportador.Columns(17).Visible = True

            End If

            If e.Row.RowState = DataControlRowState.Edit Then

                ds = Con.ExecutarQuery("SELECT ID_TRANSPORTADOR,ID_AGENTE,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_VIA_ROTA,ID_TIPO_FREQUENCIA,ID_TIPO_CARGA FROM View_FreteTransportador_new WHERE ID_FRETE_TRANSPORTADOR = " & txtID.Text)
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim ID_FRETE_TRANSPORTADOR As String = CType(e.Row.FindControl("lblID"), Label).Text
                    Dim ID_PORTO_ORIGEM As DropDownList = CType(e.Row.FindControl("ddlOrigem"), DropDownList)
                    Dim ID_PORTO_DESTINO As DropDownList = CType(e.Row.FindControl("ddlDestino"), DropDownList)
                    Dim ID_TIPO_CARGA As DropDownList = CType(e.Row.FindControl("ddlTipoCarga"), DropDownList)
                    Dim ID_TRANSPORTADOR As DropDownList = CType(e.Row.FindControl("ddlTransportador"), DropDownList)
                    Dim ID_AGENTE As DropDownList = CType(e.Row.FindControl("ddlAgente"), DropDownList)
                    Dim ID_TIPO_FREQUENCIA As DropDownList = CType(e.Row.FindControl("ddlFrequencia"), DropDownList)
                    Dim ID_VIA_ROTA As DropDownList = CType(e.Row.FindControl("ddlRota"), DropDownList)
                    Dim btnCntrLinha As LinkButton = CType(e.Row.FindControl("btnCntr2"), LinkButton)

                    If ID_FRETE_TRANSPORTADOR = txtID.Text Then
                        ID_PORTO_ORIGEM.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM").ToString()
                        ID_PORTO_DESTINO.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO").ToString()
                        ID_TIPO_CARGA.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA").ToString()
                        ID_TRANSPORTADOR.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TRANSPORTADOR").ToString()
                        ID_AGENTE.SelectedValue = ds.Tables(0).Rows(0).Item("ID_AGENTE").ToString()
                        ID_TIPO_FREQUENCIA.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FREQUENCIA").ToString()
                        ID_VIA_ROTA.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VIA_ROTA").ToString()
                    End If

                    btnCntrLinha.Visible = True

                End If
            End If

        End If
    End Sub

    Private Sub dgvCntr_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCntr.RowCommand
        divSuccess.Visible = False
        divErro.Visible = False
        divSuccessCntr.Visible = False
        divErroCntr.Visible = False

        If e.CommandName = "Excluir" Then

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet

            ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 24 AND FL_EXCLUIR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
            If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                lblErroCntr.Text = "Usuário não tem permissão para realizar exclusões"
                divErroCntr.Visible = True
            Else

                Con.ExecutarQuery("DELETE FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & e.CommandArgument)
                lblmsgSuccessCntr.Text = "Registro deletado!"
                divSuccessCntr.Visible = True
                dsCntr.SelectParameters("ID_FRETE_TRANSPORTADOR").DefaultValue = txtID.Text
                dgvCntr.DataBind()
                mpeCntr.Show()

            End If
            Con.Fechar()

        ElseIf e.CommandName = "Duplicar" Then

            Dim ID As String = e.CommandArgument
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR,ID_TIPO_CONTAINER,VL_COMPRA,QT_DIAS_FREETIME,ID_MOEDA)   SELECT ID_FRETE_TRANSPORTADOR,ID_TIPO_CONTAINER,VL_COMPRA,QT_DIAS_FREETIME,ID_MOEDA FROM TB_TARIFARIO_FRETE_TRANSPORTADOR WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & ID)
            Con.Fechar()

            dsCntr.SelectParameters("ID_FRETE_TRANSPORTADOR").DefaultValue = txtID.Text
            dgvCntr.DataBind()
            divSuccessCntr.Visible = True
            lblmsgSuccessCntr.Text = "Item duplicado com sucesso!"
            mpeCntr.Show()
            Exit Sub

        ElseIf e.CommandName = "Atualizar" Then

            txtID.Text = e.CommandArgument

            For Each linha As GridViewRow In dgvCntr.Rows
                Dim ID_TARIFARIO_FRETE_TRANSPORTADOR As String = CType(linha.FindControl("lblID"), Label).Text
                Dim ID_FRETE_TRANSPORTADOR As String = CType(linha.FindControl("lblID_FRETE_TRANSPORTADOR"), Label).Text
                Dim ID_TIPO_CONTAINER As DropDownList = CType(linha.FindControl("ddlCntr"), DropDownList)
                Dim ID_MOEDA As DropDownList = CType(linha.FindControl("ddlMoeda"), DropDownList)
                Dim QT_DIAS_FREETIME As TextBox = CType(linha.FindControl("txtFreeTime"), TextBox)
                Dim VL_COMPRA As TextBox = CType(linha.FindControl("txtCompra"), TextBox)



                If ID_TARIFARIO_FRETE_TRANSPORTADOR = txtID.Text Then
                    Dim Con As New Conexao_sql
                    Con.Conectar()

                    'REALIZA UPDATE DO FRETE TRANSPORTADOR
                    Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR_HIST (ID_FRETE_TRANSPORTADOR,ACAO,ID_USUARIO,DATA) VALUES (" & ID_FRETE_TRANSPORTADOR & ",'EDIÇÃO'," & Session("ID_USUARIO") & ", GETDATE()) ")

                    Con.ExecutarQuery("UPDATE TB_TARIFARIO_FRETE_TRANSPORTADOR  SET  ID_TIPO_CONTAINER = " & ID_TIPO_CONTAINER.SelectedValue & ", VL_COMPRA = " & VL_COMPRA.Text.Replace(".", "").Replace(",", ".") & ", QT_DIAS_FREETIME = " & QT_DIAS_FREETIME.Text & ", ID_MOEDA = " & ID_MOEDA.SelectedValue & "  WHERE ID_TARIFARIO_FRETE_TRANSPORTADOR = " & ID_TARIFARIO_FRETE_TRANSPORTADOR)

                    txtID.Text = ID_FRETE_TRANSPORTADOR

                    Con.Fechar()


                    Exit For

                End If

            Next

            divSuccessCntr.Visible = True
            lblmsgSuccessCntr.Text = "Registro atualizado com sucesso!"
            dgvCntr.SetEditRow(-1)
            txtIDTafifario.Text = ""
            dsCntr.SelectParameters("ID_FRETE_TRANSPORTADOR").DefaultValue = txtID.Text
            dgvCntr.DataBind()
            mpeCntr.Show()

        ElseIf e.CommandName = "Incluir" Then


            Dim row As GridViewRow = CType(((CType(e.CommandSource, Control)).NamingContainer), GridViewRow)

            Dim ID_MOEDA As DropDownList = TryCast(row.FindControl("ddlMoeda"), DropDownList)
            Dim ID_TIPO_CONTAINER As DropDownList = TryCast(row.FindControl("ddlCntr"), DropDownList)
            Dim QT_DIAS_FREETIME As TextBox = TryCast(row.FindControl("txtFreeTime"), TextBox)
            Dim VL_COMPRA As TextBox = TryCast(row.FindControl("txtCompra"), TextBox)


            Dim Con As New Conexao_sql
            Con.Conectar()
            Con.ExecutarQuery("INSERT INTO TB_TARIFARIO_FRETE_TRANSPORTADOR ( ID_FRETE_TRANSPORTADOR, ID_TIPO_CONTAINER, VL_COMPRA, QT_DIAS_FREETIME, ID_MOEDA ) VALUES (" & txtID.Text & "," & ID_TIPO_CONTAINER.SelectedValue & "," & VL_COMPRA.Text.Replace(".", "").Replace(",", ".") & " ,'" & QT_DIAS_FREETIME.Text & "', " & ID_MOEDA.SelectedValue & " ) ")


            Con.ExecutarQuery("INSERT INTO TB_FRETE_TRANSPORTADOR_HIST (ID_FRETE_TRANSPORTADOR,ACAO,ID_USUARIO,DATA) VALUES (" & txtID.Text & ",'INCLUSÃO'," & Session("ID_USUARIO") & ", GETDATE()) ")

            Con.Fechar()

            dsCntr.SelectParameters("ID_FRETE_TRANSPORTADOR").DefaultValue = txtID.Text
            dgvCntr.DataBind()
            mpeCntr.Show()

            divSuccessCntr.Visible = True
            lblmsgSuccessCntr.Text = "Registro cadastrado com sucesso!"
            Exit Sub


        ElseIf e.CommandName = "Edit" Then

            mpeCntr.Show()

        End If

    End Sub

    Private Sub btnFecharCntr_Click(sender As Object, e As EventArgs) Handles btnFecharCntr.Click
        CarregaPortos()
        mpeCntr.Hide()
        divSuccessCntr.Visible = False
        divErroCntr.Visible = False
    End Sub

    Private Sub dgvCntr_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles dgvCntr.RowCancelingEdit
        mpeCntr.Show()
    End Sub


End Class