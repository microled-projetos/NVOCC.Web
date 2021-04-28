Public Class CadastrarEmbarqueHouse
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

                Response.Redirect("Default.aspx")
            Else

                If Request.QueryString("tipo") = "e" Then
                    lblTipoModulo.Text = " EMBARQUE"


                ElseIf Request.QueryString("tipo") = "h" Then
                    lblTipoModulo.Text = " HOUSE"
                    ddlOrigem_BasicoMaritimo.Enabled = False
                    ddlDestino_BasicoMaritimo.Enabled = False
                    ddlOrigem_BasicoAereo.Enabled = False
                    ddlDestino_BasicoAereo.Enabled = False

                End If

                If Not Page.IsPostBack And Request.QueryString("id") <> "" Then
                    CarregaCampos()
                End If

            End If

        Else
            Response.Redirect("Default.aspx")
        End If
        Con.Fechar()


    End Sub

    Sub CarregaCampos()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_BL,ID_SERVICO,ID_BL_MASTER,NR_BL,NR_PROCESSO,ID_PARCEIRO_TRANSPORTADOR,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_IMPORTADOR, ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_PORTO_ORIGEM,ID_PORTO_DESTINO, ID_PARCEIRO_EXPORTADOR,ID_PARCEIRO_COMISSARIA,ID_PARCEIRO_AGENTE,ID_INCOTERM,FL_FREE_HAND,ID_TIPO_PAGAMENTO,ID_TIPO_CARGA,ID_TIPO_ESTUFAGEM,NR_CE,CONVERT(varchar,DT_CE, 103)DT_CE,OB_REFERENCIA_COMERCIAL,OB_REFERENCIA_AUXILIAR,NM_RESUMO_MERCADORIA,OB_CLIENTE,OB_AGENTE_INTERNACIONAL,OB_COMERCIAL,OB_OPERACIONAL_INTERNA,CD_RASTREAMENTO_HBL,CD_RASTREAMENTO_MBL,ID_PARCEIRO_ARMAZEM_DESEMBARACO,ID_PARCEIRO_RODOVIARIO,(SELECT NR_BL FROM TB_BL WHERE ID_BL = A.ID_BL_MASTER)BL_MASTER,(SELECT DT_CHEGADA FROM TB_BL WHERE TB_BL.ID_BL = A.ID_BL_MASTER)DT_CHEGADA_MASTER FROM TB_BL A where ID_BL =" & Request.QueryString("id"))
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then

                If ds.Tables(0).Rows(0).Item("ID_SERVICO") = 1 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 4 Then
                    'AGENCIAMENTO DE EXPORTACAO MARITIMA
                    'AGENCIAMENTO DE IMPORTACAO MARITIMA


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("BL_MASTER")) Then
                        txtMBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("BL_MASTER")
                        Session("ID_BL_MASTER") = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtHBL_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_PROCESSO")) Then
                        txtProcesso_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_PROCESSO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        ddlTransportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")) Then
                        ddlCliente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")) Then
                        ddlExportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")) Then
                        ddlComissaria_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")) Then
                        ddlImportador_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        ddlAgente_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_INCOTERM")) Then
                        ddlIncoterm_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_FREE_HAND")) Then
                        ckbFreeHand.Checked = ds.Tables(0).Rows(0).Item("FL_FREE_HAND")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                        ddlTipoCarga_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                        ddlEstufagem_BasicoMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CE")) Then
                        txtCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) Then
                        txtDataCE_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("DT_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")) Then
                        txtRefComercial_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")) Then
                        txtRefAuxiliar_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")) Then
                        txtResumoMercadoria_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")
                    End If


                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE")) Then
                        txtObsCliente_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")) Then
                        txtObsAgente_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_COMERCIAL")) Then
                        txtObsComercial_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")) Then
                        txtObsoperacional_ObsMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA_MASTER")) Then

                        btnGravar_BasicoMaritimo.Visible = False
                        btnLimpar_BasicoMaritimo.Visible = False
                        btnNovaTaxaMaritimo.Visible = False
                        btnSalvar_TaxaMaritimo.Visible = False
                        btnNovaCargaMaritimo.Visible = False
                        btnSalvar_CargaMaritimo.Visible = False

                        PermissoesEspeciais()

                    End If

                    btnGravar_BasicoAereo.Visible = False
                    btnLimpar_BasicoAereo.Visible = False
                    btnNovaTaxaAereo.Visible = False
                    btnNovaCargaAereo.Visible = False
                    btnGravar_RefAereo.Visible = False
                    btnGravar_ObsAereo.Visible = False
                    btnLimpar_ObsAereo.Visible = False

                ElseIf ds.Tables(0).Rows(0).Item("ID_SERVICO") = 2 Or ds.Tables(0).Rows(0).Item("ID_SERVICO") = 5 Then
                    'AGENCIAMENTO DE EXPORTAÇÃO AEREO
                    'AGENCIAMENTO DE IMPORTACAO AEREO

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL")) Then
                        txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("BL_MASTER")) Then
                        txtMBL_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("BL_MASTER")
                        Session("ID_BL_MASTER") = ds.Tables(0).Rows(0).Item("ID_BL_MASTER")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_BL")) Then
                        txtHBL_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_BL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_SERVICO")) Then
                        ddlServico_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_SERVICO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")) Then
                        ddlTransportador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")) Then
                        ddlTranspRod_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_RODOVIARIO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESEMBARACO")) Then
                        ddlArmazem_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_ARMAZEM_DESEMBARACO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")) Then
                        ddlCliente_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")) Then
                        ddlImportador_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")) Then
                        ddlOrigem_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")) Then
                        ddlDestino_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PORTO_DESTINO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")) Then
                        ddlExportador_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EXPORTADOR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")) Then
                        ddlComissaria_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_COMISSARIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")) Then
                        ddlAgente_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_INCOTERM")) Then
                        ddlIncoterm_BasicoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_INCOTERM")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_FREE_HAND")) Then
                        ckbFreeHand.Checked = ds.Tables(0).Rows(0).Item("FL_FREE_HAND")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                        ddlTipoPagamento_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")) Then
                        ddlTipoCarga_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_CARGA")
                    End If

                    'If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")) Then
                    '    ddlEstufagem_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_TIPO_ESTUFAGEM")
                    'End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_CE")) Then
                        txtNumeroCE_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NR_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CE")) Then
                        txtDataCE_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("DT_CE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")) Then
                        txtRefComercial_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")) Then
                        txtRefAuxiliar_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("OB_REFERENCIA_AUXILIAR")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")) Then
                        txtResumoMercadoria_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("NM_RESUMO_MERCADORIA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_CLIENTE")) Then
                        txtObsCliente_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_CLIENTE")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")) Then
                        txtObsAgente_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_AGENTE_INTERNACIONAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_COMERCIAL")) Then
                        txtObsComercial_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_COMERCIAL")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")) Then
                        txtObsOperacional_ObsAereo.Text = ds.Tables(0).Rows(0).Item("OB_OPERACIONAL_INTERNA")
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("DT_CHEGADA_MASTER")) Then

                        btnGravar_BasicoAereo.Visible = False
                        btnLimpar_BasicoAereo.Visible = False
                        btnNovaTaxaAereo.Visible = False
                        btnSalvar_TaxaAereo.Visible = False
                        btnNovaCargaAereo.Visible = False
                        btnSalvar_CargaAereo.Visible = False

                        PermissoesEspeciais()

                    End If


                    btnGravar_BasicoMaritimo.Visible = False
                    btnLimpar_BasicoMaritimo.Visible = False
                    btnNovaTaxaMaritimo.Visible = False
                    btnNovaCargaMaritimo.Visible = False
                    btnGravar_RefMaritimo.Visible = False
                    btnGravar_ObsMaritimo.Visible = False
                    btnLimpar_ObsMaritimo.Visible = False


                End If

            End If

        End If
    End Sub

    Private Sub dgvCargaMaritimo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCargaMaritimo.RowCommand
        divSuccess_CargaMaritimo1.Visible = False
        divErro_CargaMaritimo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1026 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then
                    Con.ExecutarQuery("DELETE From TB_CARGA_BL Where ID_CARGA_BL = " & ID)
                    lblSuccess_CargaMaritimo1.Text = "Registro deletado!"
                    divSuccess_CargaMaritimo1.Visible = True
                    dgvCargaMaritimo.DataBind()

                Else
                    lblErro_CargaMaritimo1.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_CargaMaritimo1.Visible = True
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select ID_CARGA_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA from TB_CARGA_BL 
WHERE ID_CARGA_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CARGA_BL")) Then
                    txtID_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                    txtQtdVolumes_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MERCADORIA")) Then
                    ddlMercadoria_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MERCADORIA").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NCM")) Then
                    ddlNCM_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_NCM").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                    txtPesoBruto_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3")) Then
                    txtPesoVolumetrico_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_M3")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_EMBALAGEM")) Then
                    ddlEmbalagem_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EMBALAGEM").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_GRUPO_NCM")) Then
                    txtGrupoNCM_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("DS_GRUPO_NCM")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CNTR_BL")) Then
                    Dim Sql As String = "SELECT ID_CNTR_BL, NR_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = " & ds.Tables(0).Rows(0).Item("ID_CNTR_BL") & " OR ID_BL_MASTER = " & Session("ID_BL_MASTER") & "
union Select 0, 'Selecione' FROM [dbo].[TB_CNTR_BL] ORDER BY ID_CNTR_BL"
                    Dim ds1 As DataSet = Con.ExecutarQuery(SQL)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        dsCNTR.SelectCommand = Sql
                        ddlNumeroCNTR_CargaMaritimo.DataBind()
                    End If

                    ddlNumeroCNTR_CargaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CNTR_BL").ToString()

                    ds1 = Con.ExecutarQuery("SELECT VL_PESO_TARA,NR_LACRE,ID_TIPO_CNTR FROM TB_CNTR_BL WHERE ID_CNTR_BL = " & ds.Tables(0).Rows(0).Item("ID_CNTR_BL").ToString())
                    If ds1.Tables(0).Rows.Count > 0 Then
                        txtNumeroLacre_CargaMaritimo.Text = ds1.Tables(0).Rows(0).Item("NR_LACRE")
                        txtValorTara_CargaMaritimo.Text = ds1.Tables(0).Rows(0).Item("VL_PESO_TARA")
                        ddlTipoContainer_CargaMaritimo.SelectedValue = ds1.Tables(0).Rows(0).Item("ID_TIPO_CNTR")
                    End If
                    Con.Fechar()
                End If

                mpeCargaMaritimo.Show()

            End If


        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA )  select ID_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA from TB_CARGA_BL WHERE ID_CARGA_BL = " & ID)
            lblSuccess_CargaMaritimo1.Text = "Registro duplicado!"
            divSuccess_CargaMaritimo1.Visible = True
            dgvCargaMaritimo.DataBind()
        End If
        Con.Fechar()
    End Sub

    Private Sub dgvTaxaMaritimo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxaMaritimo.RowCommand
        divSuccess_TaxaMaritimo1.Visible = False
        divErro_TaxaMaritimo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1026 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then

                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaMaritimo1.Visible = True
                        lblErro_TaxaMaritimo1.Text = "Não foi possível excluir o registro:taxa já enviada para contas a pagar/receber!"
                    Else


                        Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                        lblSuccess_TaxaMaritimo1.Text = "Registro deletado!"
                        divSuccess_TaxaMaritimo1.Visible = True
                        dgvTaxaMaritimo.DataBind()
                    End If

                Else
                    lblErro_TaxaMaritimo1.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_TaxaMaritimo1.Visible = True
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO
from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 

WHERE A.ID_BL_TAXA = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                    txtID_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlDespesa_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then
                    ckbDeclarado_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")) Then
                    ckbProfit_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                    ddlDestinatarioCob_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoedaCompra_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtValorCompra_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                    txtMinCompra_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")) Then
                    ' = ds.Tables(0).Rows(0).Item("VL_TAXA_CALCULADO")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")) Then
                    ddlStatusPagamento_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_STATUS_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                    txtObs_TaxaMaritimo.Text = ds.Tables(0).Rows(0).Item("OB_TAXAS")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                    ddlEmpresa_TaxaMaritimo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        btnSalvar_TaxaMaritimo.Visible = False
                    Else
                        btnSalvar_TaxaMaritimo.Visible = True
                    End If
                Else
                    btnSalvar_TaxaMaritimo.Visible = True
                End If

                mpeTaxaMaritimo.Show()

            End If

        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument
            Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


            If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                divErro_TaxaMaritimo1.Visible = True
                lblErro_TaxaMaritimo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
            Else

                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO)  select ID_BL,ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxaMaritimo1.Text = "Registro duplicado!"
                divSuccess_TaxaMaritimo1.Visible = True
                dgvTaxaMaritimo.DataBind()
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub dgvTaxaAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvTaxaAereo.RowCommand
        divSuccess_TaxaAereo1.Visible = False
        divErro_TaxaAereo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1026 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then

                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaAereo1.Visible = True
                        lblErro_TaxaAereo1.Text = "Não foi possível excluir o registro: taxa já enviada para contas a pagar/receber!"
                    Else


                        Con.ExecutarQuery("DELETE From TB_BL_TAXA Where ID_BL_TAXA = " & ID)
                        lblSuccess_TaxaAereo1.Text = "Registro deletado!"
                        divSuccess_TaxaAereo1.Visible = True
                        dgvTaxaAereo.DataBind()
                    End If

                Else
                    lblErro_TaxaAereo1.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_TaxaAereo1.Visible = True
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select A.ID_BL_TAXA,A.ID_ITEM_DESPESA,A.FL_DECLARADO,A.FL_DIVISAO_PROFIT,A.ID_TIPO_PAGAMENTO,A.ID_ORIGEM_PAGAMENTO,A.ID_DESTINATARIO_COBRANCA,A.ID_BASE_CALCULO_TAXA,A.ID_MOEDA,A.VL_TAXA,A.VL_TAXA_CALCULADO,A.VL_TAXA_MIN,A. ID_STATUS_PAGAMENTO,A.OB_TAXAS,A.ID_PARCEIRO_EMPRESA, B.ID_CONTA_PAGAR_RECEBER_ITENS,C.DT_CANCELAMENTO,FL_PREMIACAO
from TB_BL_TAXA A
LEFT JOIN TB_CONTA_PAGAR_RECEBER_ITENS B ON B.ID_BL_TAXA = A.ID_BL_TAXA  
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = B.ID_CONTA_PAGAR_RECEBER 

WHERE A.ID_BL_TAXA =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BL_TAXA")) Then
                    txtID_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")) Then
                    ddlDespesa_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ITEM_DESPESA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DECLARADO")) Then
                    ckbDeclarado_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DECLARADO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")) Then
                    ckbProfit_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_DIVISAO_PROFIT")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")) Then
                    ddlTipoPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")) Then
                    ddlOrigemPagamento_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ORIGEM_PAGAMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")) Then
                    ddlDestinatarioCob_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_DESTINATARIO_COBRANCA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")) Then
                    ddlBaseCalculo_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_BASE_CALCULO_TAXA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MOEDA")) Then
                    ddlMoedaCompra_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MOEDA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA")) Then
                    txtValorCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")) Then
                    txtMinCompra_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("VL_TAXA_MIN")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("OB_TAXAS")) Then
                    txtObs_TaxaAereo.Text = ds.Tables(0).Rows(0).Item("OB_TAXAS")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")) Then
                    ddlEmpresa_TaxaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_EMPRESA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CONTA_PAGAR_RECEBER_ITENS")) Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("DT_CANCELAMENTO")) Then
                        btnSalvar_TaxaAereo.Visible = False
                    Else
                        btnSalvar_TaxaAereo.Visible = True
                    End If
                Else
                    btnSalvar_TaxaAereo.Visible = True
                End If

                mpeTaxaAereo.Show()

            End If
        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & ID & " and DT_CANCELAMENTO is null ")


            If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                divErro_TaxaAereo1.Visible = True
                lblErro_TaxaAereo1.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
            Else

                Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO)  select ID_BL, ID_ITEM_DESPESA,FL_DECLARADO,FL_DIVISAO_PROFIT,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_CALCULADO,VL_TAXA_MIN, ID_STATUS_PAGAMENTO,OB_TAXAS,ID_PARCEIRO_EMPRESA,FL_PREMIACAO from TB_BL_TAXA WHERE ID_BL_TAXA = " & ID)
                lblSuccess_TaxaAereo1.Text = "Registro duplicado!"
                divSuccess_TaxaAereo1.Visible = True
                dgvTaxaAereo.DataBind()
            End If
        End If
        Con.Fechar()
    End Sub

    Private Sub dgvCargaAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvCargaAereo.RowCommand
        divSuccess_CargaAereo1.Visible = False
        divErro_CargaAereo1.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1026 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then
                    Con.ExecutarQuery("DELETE From TB_CARGA_BL Where ID_CARGA_BL = " & ID)
                    lblSuccess_CargaAereo1.Text = "Registro deletado!"
                    divSuccess_CargaAereo1.Visible = True
                    dgvCargaAereo.DataBind()

                Else
                    lblErro_CargaAereo1.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_CargaAereo1.Visible = True
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select ID_CARGA_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,DS_MERCADORIA,QT_MERCADORIA from TB_CARGA_BL 
WHERE ID_CARGA_BL = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                'Taxas
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CARGA_BL")) Then
                    txtID_CargaAereo.Text = ds.Tables(0).Rows(0).Item("ID_CARGA_BL")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("QT_MERCADORIA")) Then
                    txtQtdVolume_CargaAereo.Text = ds.Tables(0).Rows(0).Item("QT_MERCADORIA")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_MERCADORIA")) Then
                    ddlMercadoria_CargaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_MERCADORIA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_NCM")) Then
                    ddlNCM_CargaAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_NCM")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")) Then
                    txtPesoBruto_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_BRUTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_M3")) Then
                    txtPesoVolumetrico_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_M3")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_COMPRIMENTO")) Then
                    txtComprimento_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_COMPRIMENTO")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_LARGURA")) Then
                    txtLargura_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_LARGURA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("VL_ALTURA")) Then
                    txtAltura_CargaAereo.Text = ds.Tables(0).Rows(0).Item("VL_ALTURA")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DS_MERCADORIA")) Then
                    txtDescMercadoria_CargaAereo.Text = ds.Tables(0).Rows(0).Item("DS_MERCADORIA")
                End If
                mpeCargaAereo.Show()

            End If

        ElseIf e.CommandName = "Duplicar" Then
            Dim ID As String = e.CommandArgument

            Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA )  select ID_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,ID_EMBALAGEM,DS_GRUPO_NCM,ID_CNTR_BL,QT_MERCADORIA,DS_MERCADORIA,VL_COMPRIMENTO,VL_ALTURA,VL_LARGURA from TB_CARGA_BL WHERE ID_CARGA_BL = " & ID)
            lblSuccess_CargaAereo1.Text = "Registro duplicado!"
            divSuccess_CargaAereo1.Visible = True
            dgvCargaAereo.DataBind()
        End If
        Con.Fechar()
    End Sub

    Private Sub btnFechar_CargaAereo_Click(sender As Object, e As EventArgs) Handles btnFechar_CargaAereo.Click
        divErro_CargaAereo2.Visible = False
        divSuccess_CargaAereo2.Visible = False
        ddlMercadoria_CargaAereo.SelectedValue = 0
        ddlNCM_CargaAereo.SelectedValue = 0
        txtLargura_CargaAereo.Text = ""
        txtAltura_CargaAereo.Text = ""
        txtComprimento_CargaAereo.Text = ""
        txtDescMercadoria_CargaAereo.Text = ""
        txtPesoBruto_CargaAereo.Text = ""
        txtPesoVolumetrico_CargaAereo.Text = ""
        txtID_CargaAereo.Text = ""

        mpeCargaAereo.Hide()
    End Sub

    Private Sub btnFechar_CargaMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_CargaMaritimo.Click
        divErro_CargaMaritimo2.Visible = False
        divSuccess_CargaMaritimo2.Visible = False
        ddlMercadoria_CargaMaritimo.SelectedValue = 0
        ddlNCM_CargaMaritimo.SelectedValue = 0
        txtNumeroLacre_CargaMaritimo.Text = ""
        txtValorTara_CargaMaritimo.Text = ""
        txtNumeroContainer_CargaMaritimo.Text = ""
        ddlTipoContainer_CargaMaritimo.SelectedValue = 0
        txtPesoBruto_CargaMaritimo.Text = ""
        txtPesoVolumetrico_CargaMaritimo.Text = ""
        txtID_CargaMaritimo.Text = ""
        txtGrupoNCM_CargaMaritimo.Text = ""
        ddlEmbalagem_CargaMaritimo.SelectedValue = 0
        ddlNumeroCNTR_CargaMaritimo.SelectedValue = 0
        txtQtdVolumes_CargaMaritimo.Text = ""

        dgvCargaMaritimo.DataBind()
        mpeCargaMaritimo.Hide()
    End Sub

    Private Sub btnFechar_TaxaAereo_Click(sender As Object, e As EventArgs) Handles btnFechar_TaxaAereo.Click
        divErro_TaxaAereo2.Visible = False
        divSuccess_TaxaAereo2.Visible = False
        ddlDespesa_TaxaAereo.SelectedValue = 0
        ddlTipoPagamento_TaxaAereo.SelectedValue = 0
        ddlOrigemPagamento_TaxaAereo.SelectedValue = 0
        ddlDestinatarioCob_TaxaAereo.SelectedValue = 0
        ddlBaseCalculo_TaxaAereo.SelectedValue = 0
        ddlMoedaCompra_TaxaAereo.SelectedValue = 0
        ddlEmpresa_TaxaAereo.SelectedValue = 0
        txtValorCompra_TaxaAereo.Text = ""
        txtBaseCompra_TaxaAereo.Text = ""
        txtObs_TaxaAereo.Text = ""
        txtMinCompra_TaxaAereo.Text = ""
        txtID_TaxaAereo.Text = ""

        mpeTaxaAereo.Hide()
    End Sub

    Private Sub btnFechar_TaxaMaritimo_Click(sender As Object, e As EventArgs) Handles btnFechar_TaxaMaritimo.Click
        divErro_TaxaMaritimo2.Visible = False
        divSuccess_TaxaMaritimo2.Visible = False
        txtID_TaxaMaritimo.Text = ""
        ddlStatusPagamento_TaxaMaritimo.SelectedValue = 0
        ddlDespesa_TaxaMaritimo.SelectedValue = 0
        ddlTipoPagamento_TaxaMaritimo.SelectedValue = 0
        ddlOrigemPagamento_TaxaMaritimo.SelectedValue = 0
        ddlDestinatarioCob_TaxaMaritimo.SelectedValue = 0
        ddlBaseCalculo_TaxaMaritimo.SelectedValue = 0
        ddlMoedaCompra_TaxaMaritimo.SelectedValue = 0
        ddlEmpresa_TaxaMaritimo.SelectedValue = 0
        txtValorCompra_TaxaMaritimo.Text = ""
        txtBaseCompra_TaxaMaritimo.Text = ""
        txtObs_TaxaMaritimo.Text = ""
        txtMinCompra_TaxaMaritimo.Text = ""

        mpeTaxaMaritimo.Hide()
    End Sub

    Private Sub btnGravar_ObsAereo_Click(sender As Object, e As EventArgs) Handles btnGravar_ObsAereo.Click
        divSuccess_ObsAereo.Visible = False
        divErro_ObsAereo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtID_BasicoAereo.Text = "" Then
            lblErro_ObsAereo.Text = "Antes de inserir observações é necessário cadastrar as Informações Basicas"
            divErro_ObsAereo.Visible = True

        Else
            ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                    divErro_ObsAereo.Visible = True
                    lblErro_ObsAereo.Text = "Usuário não possui permissão."

                Else
                    If txtObsAgente_ObsAereo.Text = "" Then
                        txtObsAgente_ObsAereo.Text = "NULL"
                    Else
                        txtObsAgente_ObsAereo.Text = "'" & txtObsAgente_ObsAereo.Text & "'"
                    End If

                    If txtObsCliente_ObsAereo.Text = "" Then
                        txtObsCliente_ObsAereo.Text = "NULL"
                    Else
                        txtObsCliente_ObsAereo.Text = "'" & txtObsCliente_ObsAereo.Text & "'"
                    End If

                    If txtObsOperacional_ObsAereo.Text = "" Then
                        txtObsOperacional_ObsAereo.Text = "NULL"
                    Else
                        txtObsOperacional_ObsAereo.Text = "'" & txtObsOperacional_ObsAereo.Text & "'"
                    End If

                    If txtObsComercial_ObsAereo.Text = "" Then
                        txtObsComercial_ObsAereo.Text = "NULL"
                    Else
                        txtObsComercial_ObsAereo.Text = "'" & txtObsComercial_ObsAereo.Text & "'"
                    End If

                    Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = " & txtObsCliente_ObsAereo.Text & ", OB_AGENTE_INTERNACIONAL = " & txtObsAgente_ObsAereo.Text & " , OB_COMERCIAL = " & txtObsComercial_ObsAereo.Text & ",OB_OPERACIONAL_INTERNA = " & txtObsOperacional_ObsAereo.Text & " WHERE ID_BL = " & txtID_BasicoAereo.Text)
                    divSuccess_ObsAereo.Visible = True

                    txtObsCliente_ObsAereo.Text = txtObsCliente_ObsAereo.Text.Replace("NULL", "")
                    txtObsCliente_ObsAereo.Text = txtObsCliente_ObsAereo.Text.Replace("'", "")

                    txtObsAgente_ObsAereo.Text = txtObsAgente_ObsAereo.Text.Replace("NULL", "")
                    txtObsAgente_ObsAereo.Text = txtObsAgente_ObsAereo.Text.Replace("'", "")

                    txtObsComercial_ObsAereo.Text = txtObsComercial_ObsAereo.Text.Replace("NULL", "")
                    txtObsComercial_ObsAereo.Text = txtObsComercial_ObsAereo.Text.Replace("'", "")

                    txtObsOperacional_ObsAereo.Text = txtObsOperacional_ObsAereo.Text.Replace("NULL", "")
                    txtObsOperacional_ObsAereo.Text = txtObsOperacional_ObsAereo.Text.Replace("'", "")

                End If
            End If

        End If
    End Sub

    Private Sub btnGravar_ObsMaritimo_Click(sender As Object, e As EventArgs) Handles btnGravar_ObsMaritimo.Click
        divSuccess_ObsMaritimo.Visible = False
        divErro_ObsMaritimo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtID_BasicoMaritimo.Text = "" Then
            lblErro_ObsMaritimo.Text = "Antes de inserir observações é necessário cadastrar as Informações Basicas"
            divErro_ObsMaritimo.Visible = True

        Else
            ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                    divErro_ObsMaritimo.Visible = True
                    lblErro_ObsMaritimo.Text = "Usuário não possui permissão."

                Else
                    If txtObsAgente_ObsMaritimo.Text = "" Then
                        txtObsAgente_ObsMaritimo.Text = "NULL"
                    Else
                        txtObsAgente_ObsMaritimo.Text = "'" & txtObsAgente_ObsMaritimo.Text & "'"
                    End If

                    If txtObsCliente_ObsMaritimo.Text = "" Then
                        txtObsCliente_ObsMaritimo.Text = "NULL"
                    Else
                        txtObsCliente_ObsMaritimo.Text = "'" & txtObsCliente_ObsMaritimo.Text & "'"
                    End If

                    If txtObsoperacional_ObsMaritimo.Text = "" Then
                        txtObsoperacional_ObsMaritimo.Text = "NULL"
                    Else
                        txtObsoperacional_ObsMaritimo.Text = "'" & txtObsoperacional_ObsMaritimo.Text & "'"
                    End If

                    If txtObsComercial_ObsMaritimo.Text = "" Then
                        txtObsComercial_ObsMaritimo.Text = "NULL"
                    Else
                        txtObsComercial_ObsMaritimo.Text = "'" & txtObsComercial_ObsMaritimo.Text & "'"
                    End If

                    Con.ExecutarQuery("UPDATE TB_BL SET OB_CLIENTE = " & txtObsCliente_ObsMaritimo.Text & ", OB_AGENTE_INTERNACIONAL = " & txtObsAgente_ObsMaritimo.Text & " , OB_COMERCIAL = " & txtObsComercial_ObsMaritimo.Text & ",OB_OPERACIONAL_INTERNA = " & txtObsoperacional_ObsMaritimo.Text & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                    divSuccess_ObsMaritimo.Visible = True

                    txtObsCliente_ObsMaritimo.Text = txtObsCliente_ObsMaritimo.Text.Replace("NULL", "")
                    txtObsCliente_ObsMaritimo.Text = txtObsCliente_ObsMaritimo.Text.Replace("'", "")

                    txtObsAgente_ObsMaritimo.Text = txtObsAgente_ObsMaritimo.Text.Replace("NULL", "")
                    txtObsAgente_ObsMaritimo.Text = txtObsAgente_ObsMaritimo.Text.Replace("'", "")

                    txtObsComercial_ObsMaritimo.Text = txtObsComercial_ObsMaritimo.Text.Replace("NULL", "")
                    txtObsComercial_ObsMaritimo.Text = txtObsComercial_ObsMaritimo.Text.Replace("'", "")

                    txtObsoperacional_ObsMaritimo.Text = txtObsoperacional_ObsMaritimo.Text.Replace("NULL", "")
                    txtObsoperacional_ObsMaritimo.Text = txtObsoperacional_ObsMaritimo.Text.Replace("'", "")
                End If
            End If

        End If
    End Sub

    Private Sub btnLimpar_ObsAereo_Click(sender As Object, e As EventArgs) Handles btnLimpar_ObsAereo.Click
        txtObsCliente_ObsAereo.Text = ""
        txtObsAgente_ObsAereo.Text = ""
        txtObsComercial_ObsAereo.Text = ""
        txtObsOperacional_ObsAereo.Text = ""
    End Sub

    Private Sub btnLimpar_ObsMaritimo_Click(sender As Object, e As EventArgs) Handles btnLimpar_ObsMaritimo.Click
        txtObsCliente_ObsMaritimo.Text = ""
        txtObsAgente_ObsMaritimo.Text = ""
        txtObsComercial_ObsMaritimo.Text = ""
        txtObsoperacional_ObsMaritimo.Text = ""
    End Sub

    Private Sub btnLimpar_BasicoAereo_Click(sender As Object, e As EventArgs) Handles btnLimpar_BasicoAereo.Click
        If Request.QueryString("tipo") = "e" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=e")
        ElseIf Request.QueryString("tipo") = "h" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=h")
        End If
    End Sub

    Private Sub btnLimpar_BasicoMaritimo_Click(sender As Object, e As EventArgs) Handles btnLimpar_BasicoMaritimo.Click
        If Request.QueryString("tipo") = "e" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=e")
        ElseIf Request.QueryString("tipo") = "h" Then
            Response.Redirect("CadastrarEmbarqueHouse.aspx?tipo=h")
        End If
    End Sub

    Private Sub btnGravar_RefAereo_Click(sender As Object, e As EventArgs) Handles btnGravar_RefAereo.Click
        divSuccess_RefAereo.Visible = False
        divErro_RefAereo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtRefAereo.Text = "" Then
            lblErro_RefAereo.Text = "Preencha o campo de referencia!"
            divErro_RefAereo.Visible = True

        ElseIf txtID_BasicoAereo.Text = "" Then
            lblErro_RefAereo.Text = "Antes de inserir referencia é necessário cadastrar as Informações Basicas"
            divErro_RefAereo.Visible = True

        Else

            If txtID_RefAereo.Text = "" Then
                ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                        divErro_RefAereo.Visible = True
                        lblErro_RefAereo.Text = "Usuário não possui permissão."

                    Else

                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE) VALUES (" & txtID_BasicoAereo.Text & ", '" & txtRefAereo.Text & "')")
                        divSuccess_RefAereo.Visible = True

                        txtRefAereo.Text = ""

                    End If
                End If
            Else

                ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                        divErro_RefAereo.Visible = True
                        lblErro_RefAereo.Text = "Usuário não possui permissão."

                    Else

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefAereo.Text & "' WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefAereo.Text)
                        divSuccess_RefAereo.Visible = True

                        txtRefAereo.Text = ""
                        dgvRefAereo.DataBind()

                    End If
                End If
            End If

        End If

    End Sub

    Private Sub btnGravar_RefMaritimo_Click(sender As Object, e As EventArgs) Handles btnGravar_RefMaritimo.Click
        divSuccess_RefMaritimo.Visible = False
        divErro_RefMaritimo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        If txtRefMaritimo.Text = "" Then
            lblErro_RefMaritimo.Text = "Preencha o campo de referencia!"
            divErro_RefMaritimo.Visible = True

        ElseIf txtID_BasicoMaritimo.Text = "" Then
            lblErro_RefMaritimo.Text = "Antes de inserir referencia é necessário cadastrar as Informações Basicas"
            divErro_RefMaritimo.Visible = True

        Else

            If txtID_RefMaritimo.Text = "" Then
                ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                        divErro_RefMaritimo.Visible = True
                        lblErro_RefMaritimo.Text = "Usuário não possui permissão."

                    Else

                        Con.ExecutarQuery("INSERT INTO TB_REFERENCIA_CLIENTE (ID_BL,NR_REFERENCIA_CLIENTE) VALUES (" & txtID_BasicoMaritimo.Text & ", '" & txtRefMaritimo.Text & "')")
                        divSuccess_RefMaritimo.Visible = True

                        txtRefMaritimo.Text = ""
                        dgvRefMaritimo.DataBind()
                    End If
                End If
            Else

                ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                        divErro_RefMaritimo.Visible = True
                        lblErro_RefMaritimo.Text = "Usuário não possui permissão."

                    Else

                        Con.ExecutarQuery("UPDATE TB_REFERENCIA_CLIENTE SET NR_REFERENCIA_CLIENTE = '" & txtRefMaritimo.Text & "' WHERE ID_REFERENCIA_CLIENTE = " & txtID_RefMaritimo.Text)
                        divSuccess_RefMaritimo.Visible = True

                        txtRefMaritimo.Text = ""
                        dgvRefMaritimo.DataBind()
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub btnCancelar_RefMaritimo_Click(sender As Object, e As EventArgs) Handles btnCancelar_RefMaritimo.Click
        divSuccess_RefMaritimo.Visible = False
        divErro_RefMaritimo.Visible = False
        txtRefMaritimo.Text = ""
        txtID_RefMaritimo.Text = ""

    End Sub

    Private Sub btnCancelar_RefAereo_Click(sender As Object, e As EventArgs) Handles btnCancelar_RefAereo.Click
        divSuccess_RefAereo.Visible = False
        divErro_RefAereo.Visible = False
        txtRefAereo.Text = ""
        txtID_RefAereo.Text = ""

    End Sub

    Private Sub btnGravar_BasicoMaritimo_Click(sender As Object, e As EventArgs) Handles btnGravar_BasicoMaritimo.Click

        divSuccess_BasicoMaritimo.Visible = False
        divErro_BasicoMaritimo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If txtDataCE_BasicoMaritimo.Text <> "" And v.ValidaData(txtDataCE_BasicoMaritimo.Text) = False Then
            divErro_BasicoMaritimo.Visible = True
            lblErro_BasicoMaritimo.Text = "O valor informado no campo Data CE é inválido."

        Else

            If txtResumoMercadoria_BasicoMaritimo.Text = "" Then
                txtResumoMercadoria_BasicoMaritimo.Text = "NULL"
            Else
                txtResumoMercadoria_BasicoMaritimo.Text = "'" & txtResumoMercadoria_BasicoMaritimo.Text & "'"
            End If

            If txtRefAuxiliar_BasicoMaritimo.Text = "" Then
                txtRefAuxiliar_BasicoMaritimo.Text = "NULL"
            Else
                txtRefAuxiliar_BasicoMaritimo.Text = "'" & txtRefAuxiliar_BasicoMaritimo.Text & "'"
            End If

            If txtRefComercial_BasicoMaritimo.Text = "" Then
                txtRefComercial_BasicoMaritimo.Text = "NULL"
            Else
                txtRefComercial_BasicoMaritimo.Text = "'" & txtRefComercial_BasicoMaritimo.Text & "'"
            End If

            If txtProcesso_BasicoMaritimo.Text = "" Then
                txtProcesso_BasicoMaritimo.Text = "NULL"
            Else
                txtProcesso_BasicoMaritimo.Text = "'" & txtProcesso_BasicoMaritimo.Text & "'"
            End If

            'If txtMBL_BasicoMaritimo.Text = "" Then
            '    txtMBL_BasicoMaritimo.Text = "NULL"
            'Else
            '    txtMBL_BasicoMaritimo.Text = "'" & txtMBL_BasicoMaritimo.Text & "'"
            'End If

            If txtHBL_BasicoMaritimo.Text = "" Then
                txtHBL_BasicoMaritimo.Text = "NULL"
            Else
                txtHBL_BasicoMaritimo.Text = "'" & txtHBL_BasicoMaritimo.Text & "'"
            End If


            If txtCE_BasicoMaritimo.Text = "" Then
                txtCE_BasicoMaritimo.Text = "NULL"
            Else
                txtCE_BasicoMaritimo.Text = "'" & txtCE_BasicoMaritimo.Text & "'"
            End If

            If txtDataCE_BasicoMaritimo.Text = "" Then
                txtDataCE_BasicoMaritimo.Text = "NULL"
            Else
                If v.ValidaData(txtDataCE_BasicoMaritimo.Text) = False Then
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "O valor informado no campo Data de CE é inválido."
                Else
                    txtDataCE_BasicoMaritimo.Text = "CONVERT(date,'" & txtDataCE_BasicoMaritimo.Text & "',103)"

                End If
            End If


            If txtID_BasicoMaritimo.Text = "" Then

                ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                        divErro_BasicoMaritimo.Visible = True
                        lblErro_BasicoMaritimo.Text = "Usuário não possui permissão para cadastrar."
                        Exit Sub

                    Else


                        If Request.QueryString("tipo") = "e" And ddlServico_BasicoMaritimo.SelectedValue = 0 Then
                            divErro_BasicoMaritimo.Visible = True
                            lblErro_BasicoMaritimo.Text = "É necessário preencher o tipo de serviço."
                            LimpaNulo()
                            Exit Sub
                        Else

                            'INSERE 
                            ds = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,NR_BL,ID_PARCEIRO_TRANSPORTADOR,ID_PORTO_ORIGEM,ID_PORTO_DESTINO,ID_PARCEIRO_CLIENTE,ID_PARCEIRO_EXPORTADOR, ID_PARCEIRO_COMISSARIA,ID_PARCEIRO_AGENTE_INTERNACIONAL,ID_INCOTERM,FL_FREE_HAND,ID_TIPO_ESTUFAGEM,ID_TIPO_PAGAMENTO,ID_TIPO_CARGA,NR_CE,DT_CE,OB_REFERENCIA_AUXILIAR,OB_REFERENCIA_COMERCIAL,NM_RESUMO_MERCADORIA, ID_SERVICO,GRAU,ID_PARCEIRO_IMPORTADOR) VALUES (" & txtProcesso_BasicoMaritimo.Text & "," & txtHBL_BasicoMaritimo.Text & "," & ddlTransportador_BasicoMaritimo.SelectedValue & ", " & ddlOrigem_BasicoMaritimo.SelectedValue & ", " & ddlDestino_BasicoMaritimo.SelectedValue & "," & ddlCliente_BasicoMaritimo.SelectedValue & ", " & ddlExportador_BasicoMaritimo.SelectedValue & "," & ddlComissaria_BasicoMaritimo.SelectedValue & "," & ddlAgente_BasicoMaritimo.SelectedValue & "," & ddlIncoterm_BasicoMaritimo.SelectedValue & ",'" & ckbFreeHand.Checked & "'," & ddlEstufagem_BasicoMaritimo.SelectedValue & "," & ddlTipoPagamento_BasicoMaritimo.SelectedValue & "," & ddlTipoCarga_BasicoMaritimo.SelectedValue & "," & txtCE_BasicoMaritimo.Text & "," & txtDataCE_BasicoMaritimo.Text & "," & txtRefAuxiliar_BasicoMaritimo.Text & "," & txtRefComercial_BasicoMaritimo.Text & ", " & txtResumoMercadoria_BasicoMaritimo.Text & ", " & ddlServico_BasicoMaritimo.SelectedValue & ", 'C'," & ddlImportador_BasicoMaritimo.SelectedValue & ") Select SCOPE_IDENTITY() as ID_BL ")

                            'PREENCHE SESSÃO E CAMPO DE ID
                            Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                            txtID_BasicoMaritimo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                            NumeroProcesso()


                            LimpaNulo()


                            Con.Fechar()
                            divSuccess_BasicoMaritimo.Visible = True

                        End If








                    End If
                Else
                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                End If


            Else

                ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                        divErro_BasicoMaritimo.Visible = True
                        lblErro_BasicoMaritimo.Text = "Usuário não possui permissão para alterar."
                        Exit Sub

                    Else


                        'REALIZA UPDATE 
                        Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = " & txtProcesso_BasicoMaritimo.Text & " , NR_BL = " & txtHBL_BasicoMaritimo.Text & ", ID_PARCEIRO_TRANSPORTADOR = " & ddlTransportador_BasicoMaritimo.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem_BasicoMaritimo.SelectedValue & ", ID_PORTO_DESTINO = " & ddlDestino_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_CLIENTE = " & ddlCliente_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_EXPORTADOR = " & ddlExportador_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_COMISSARIA = " & ddlComissaria_BasicoMaritimo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoMaritimo.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm_BasicoMaritimo.SelectedValue & ", ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoMaritimo.SelectedValue & ", ID_TIPO_CARGA = " & ddlTipoCarga_BasicoMaritimo.SelectedValue & ", NR_CE = " & txtCE_BasicoMaritimo.Text & ", DT_CE = " & txtDataCE_BasicoMaritimo.Text & ",  OB_REFERENCIA_AUXILIAR =" & txtRefAuxiliar_BasicoMaritimo.Text & ", OB_REFERENCIA_COMERCIAL = " & txtRefComercial_BasicoMaritimo.Text & ", NM_RESUMO_MERCADORIA = " & txtResumoMercadoria_BasicoMaritimo.Text & ",FL_FREE_HAND = '" & ckbFreeHand.Checked & "', ID_TIPO_ESTUFAGEM = " & ddlEstufagem_BasicoMaritimo.SelectedValue & ",ID_SERVICO =" & ddlServico_BasicoMaritimo.SelectedValue & ", GRAU = 'C', ID_PARCEIRO_IMPORTADOR = " & ddlImportador_BasicoMaritimo.SelectedValue & " WHERE ID_BL = " & txtID_BasicoMaritimo.Text)


                        LimpaNulo()



                        divSuccess_BasicoMaritimo.Visible = True
                        Con.Fechar()


                    End If
                Else

                    divErro_BasicoMaritimo.Visible = True
                    lblErro_BasicoMaritimo.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                End If





            End If


        End If
    End Sub

    Sub LimpaNulo()
        txtResumoMercadoria_BasicoMaritimo.Text = txtResumoMercadoria_BasicoMaritimo.Text.Replace("'", "")
        txtResumoMercadoria_BasicoMaritimo.Text = txtResumoMercadoria_BasicoMaritimo.Text.Replace("NULL", "")

        txtRefComercial_BasicoMaritimo.Text = txtRefComercial_BasicoMaritimo.Text.Replace("'", "")
        txtRefComercial_BasicoMaritimo.Text = txtRefComercial_BasicoMaritimo.Text.Replace("NULL", "")

        txtRefAuxiliar_BasicoMaritimo.Text = txtRefAuxiliar_BasicoMaritimo.Text.Replace("'", "")
        txtRefAuxiliar_BasicoMaritimo.Text = txtRefAuxiliar_BasicoMaritimo.Text.Replace("NULL", "")

        txtProcesso_BasicoMaritimo.Text = txtProcesso_BasicoMaritimo.Text.Replace("'", "")
        txtProcesso_BasicoMaritimo.Text = txtProcesso_BasicoMaritimo.Text.Replace("NULL", "")

        'txtMBL_BasicoMaritimo.Text = txtMBL_BasicoMaritimo.Text.Replace("'", "")
        'txtMBL_BasicoMaritimo.Text = txtMBL_BasicoMaritimo.Text.Replace("NULL", "")

        txtHBL_BasicoMaritimo.Text = txtHBL_BasicoMaritimo.Text.Replace("'", "")
        txtHBL_BasicoMaritimo.Text = txtHBL_BasicoMaritimo.Text.Replace("NULL", "")

        txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("'", "")
        txtCE_BasicoMaritimo.Text = txtCE_BasicoMaritimo.Text.Replace("NULL", "")

        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("NULL", "")
        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("CONVERT(varchar,'", "")
        txtDataCE_BasicoMaritimo.Text = txtDataCE_BasicoMaritimo.Text.Replace("',103)", "")


        txtResumoMercadoria_BasicoAereo.Text = txtResumoMercadoria_BasicoAereo.Text.Replace("'", "")
        txtResumoMercadoria_BasicoAereo.Text = txtResumoMercadoria_BasicoAereo.Text.Replace("NULL", "")

        txtRefComercial_BasicoAereo.Text = txtRefComercial_BasicoAereo.Text.Replace("'", "")
        txtRefComercial_BasicoAereo.Text = txtRefComercial_BasicoAereo.Text.Replace("NULL", "")

        txtRefAuxiliar_BasicoAereo.Text = txtRefAuxiliar_BasicoAereo.Text.Replace("'", "")
        txtRefAuxiliar_BasicoAereo.Text = txtRefAuxiliar_BasicoAereo.Text.Replace("NULL", "")

        txtProcesso_BasicoAereo.Text = txtProcesso_BasicoAereo.Text.Replace("'", "")
        txtProcesso_BasicoAereo.Text = txtProcesso_BasicoAereo.Text.Replace("NULL", "")

        'txtMBL_BasicoAereo.Text = txtMBL_BasicoAereo.Text.Replace("'", "")
        'txtMBL_BasicoAereo.Text = txtMBL_BasicoAereo.Text.Replace("NULL", "")

        txtHBL_BasicoAereo.Text = txtHBL_BasicoAereo.Text.Replace("'", "")
        txtHBL_BasicoAereo.Text = txtHBL_BasicoAereo.Text.Replace("NULL", "")


        txtDataCE_BasicoAereo.Text = txtDataCE_BasicoAereo.Text.Replace("NULL", "")
        txtDataCE_BasicoAereo.Text = txtDataCE_BasicoAereo.Text.Replace("CONVERT(varchar,'", "")
        txtDataCE_BasicoAereo.Text = txtDataCE_BasicoAereo.Text.Replace("',103)", "")

        txtNumeroCE_BasicoAereo.Text = txtNumeroCE_BasicoAereo.Text.Replace("'", "")
        txtNumeroCE_BasicoAereo.Text = txtNumeroCE_BasicoAereo.Text.Replace("NULL", "")
    End Sub
    Private Sub btnGravar_BasicoAereo_Click(sender As Object, e As EventArgs) Handles btnGravar_BasicoAereo.Click

        divSuccess_BasicoAereo.Visible = False
        divErro_BasicoAereo.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If txtDataCE_BasicoAereo.Text <> "" And v.ValidaData(txtDataCE_BasicoAereo.Text) = False Then
            divErro_BasicoAereo.Visible = True
            lblErro_BasicoAereo.Text = "O valor informado no campo Data CE é inválido."

        Else

            If txtResumoMercadoria_BasicoAereo.Text = "" Then
                txtResumoMercadoria_BasicoAereo.Text = "NULL"
            Else
                txtResumoMercadoria_BasicoAereo.Text = "'" & txtResumoMercadoria_BasicoAereo.Text & "'"
            End If

            'If txtMBL_BasicoAereo.Text = "" Then
            '    txtMBL_BasicoAereo.Text = "NULL"
            'Else
            '    txtMBL_BasicoAereo.Text = "'" & txtMBL_BasicoAereo.Text & "'"
            'End If

            If txtProcesso_BasicoAereo.Text = "" Then
                txtProcesso_BasicoAereo.Text = "NULL"
            Else
                txtProcesso_BasicoAereo.Text = "'" & txtProcesso_BasicoAereo.Text & "'"
            End If

            If txtRefAuxiliar_BasicoAereo.Text = "" Then
                txtRefAuxiliar_BasicoAereo.Text = "NULL"
            Else
                txtRefAuxiliar_BasicoAereo.Text = "'" & txtRefAuxiliar_BasicoAereo.Text & "'"
            End If

            If txtRefComercial_BasicoAereo.Text = "" Then
                txtRefComercial_BasicoAereo.Text = "NULL"
            Else
                txtRefComercial_BasicoAereo.Text = "'" & txtRefComercial_BasicoAereo.Text & "'"
            End If

            If txtHBL_BasicoAereo.Text = "" Then
                txtHBL_BasicoAereo.Text = "NULL"
            Else
                txtHBL_BasicoAereo.Text = "'" & txtHBL_BasicoAereo.Text & "'"
            End If

            If txtNumeroCE_BasicoAereo.Text = "" Then
                txtNumeroCE_BasicoAereo.Text = "NULL"
            Else
                txtNumeroCE_BasicoAereo.Text = "'" & txtNumeroCE_BasicoAereo.Text & "'"
            End If


            If txtDataCE_BasicoAereo.Text = "" Then
                txtDataCE_BasicoAereo.Text = "NULL"
            Else
                If v.ValidaData(txtDataCE_BasicoAereo.Text) = False Then
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "O valor informado no campo Data de CE é inválido."
                Else
                    txtDataCE_BasicoAereo.Text = "CONVERT(date,'" & txtDataCE_BasicoAereo.Text & "',103)"

                End If
            End If


            If txtID_BasicoAereo.Text = "" Then


                ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                        divErro_BasicoAereo.Visible = True
                        lblErro_BasicoAereo.Text = "Usuário não possui permissão para cadastrar."
                        Exit Sub

                    Else

                        If Request.QueryString("tipo") = "e" And ddlServico_BasicoAereo.SelectedValue = 0 Then
                            divErro_BasicoAereo.Visible = True
                            lblErro_BasicoAereo.Text = "É necessário preencher o tipo de serviço."
                            LimpaNulo()

                            Exit Sub
                        Else

                            'INSERE 
                            ds = Con.ExecutarQuery("INSERT INTO TB_BL (NR_PROCESSO,NR_BL, ID_PARCEIRO_TRANSPORTADOR, ID_PORTO_ORIGEM, ID_PORTO_DESTINO, ID_PARCEIRO_CLIENTE, ID_PARCEIRO_EXPORTADOR, ID_PARCEIRO_COMISSARIA, ID_PARCEIRO_AGENTE_INTERNACIONAL, ID_INCOTERM, ID_PARCEIRO_ARMAZEM_DESEMBARACO, ID_TIPO_PAGAMENTO, ID_TIPO_CARGA, NR_CE, DT_CE, OB_REFERENCIA_AUXILIAR, OB_REFERENCIA_COMERCIAL, NM_RESUMO_MERCADORIA,ID_PARCEIRO_RODOVIARIO,ID_SERVICO,GRAU,ID_PARCEIRO_IMPORTADOR) VALUES (" & txtProcesso_BasicoAereo.Text & ", " & txtHBL_BasicoAereo.Text & "," & ddlTransportador_BasicoAereo.SelectedValue & ", " & ddlOrigem_BasicoAereo.SelectedValue & ", " & ddlDestino_BasicoAereo.SelectedValue & "," & ddlCliente_BasicoAereo.SelectedValue & ", " & ddlExportador_BasicoAereo.SelectedValue & "," & ddlComissaria_BasicoAereo.SelectedValue & "," & ddlAgente_BasicoAereo.SelectedValue & "," & ddlIncoterm_BasicoAereo.SelectedValue & "," & ddlArmazem_BasicoAereo.SelectedValue & "," & ddlTipoPagamento_BasicoAereo.SelectedValue & "," & ddlTipoCarga_BasicoAereo.SelectedValue & "," & txtNumeroCE_BasicoAereo.Text & ", " & txtDataCE_BasicoAereo.Text & "," & txtRefAuxiliar_BasicoAereo.Text & "," & txtRefComercial_BasicoAereo.Text & ", " & txtResumoMercadoria_BasicoAereo.Text & ", " & ddlTranspRod_BasicoAereo.SelectedValue & "," & ddlServico_BasicoAereo.SelectedValue & ",'C'," & ddlImportador_BasicoAereo.SelectedValue & ") Select SCOPE_IDENTITY() as ID_BL ")


                            'PREENCHE SESSÃO E CAMPO DE ID
                            Session("ID_BL") = ds.Tables(0).Rows(0).Item("ID_BL").ToString()
                            txtID_BasicoAereo.Text = ds.Tables(0).Rows(0).Item("ID_BL").ToString()

                            NumeroProcesso()

                            LimpaNulo()



                            Con.Fechar()
                            divSuccess_BasicoAereo.Visible = True

                        End If

                    End If
                Else
                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                End If


            Else

                ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                        divErro_BasicoAereo.Visible = True
                        lblErro_BasicoAereo.Text = "Usuário não possui permissão para alterar."
                        Exit Sub

                    Else


                        'REALIZA UPDATE 
                        Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = " & txtProcesso_BasicoAereo.Text & " , NR_BL = " & txtHBL_BasicoAereo.Text & ",ID_PARCEIRO_TRANSPORTADOR = " & ddlTransportador_BasicoAereo.SelectedValue & ", ID_PORTO_ORIGEM = " & ddlOrigem_BasicoAereo.SelectedValue & ", ID_PORTO_DESTINO = " & ddlDestino_BasicoAereo.SelectedValue & ", ID_PARCEIRO_CLIENTE = " & ddlCliente_BasicoAereo.SelectedValue & ", ID_PARCEIRO_EXPORTADOR = " & ddlExportador_BasicoAereo.SelectedValue & ", ID_PARCEIRO_COMISSARIA = " & ddlComissaria_BasicoAereo.SelectedValue & ", ID_PARCEIRO_AGENTE_INTERNACIONAL = " & ddlAgente_BasicoAereo.SelectedValue & ", ID_INCOTERM = " & ddlIncoterm_BasicoAereo.SelectedValue & ", ID_PARCEIRO_ARMAZEM_DESEMBARACO = " & ddlArmazem_BasicoAereo.SelectedValue & ", ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ", ID_TIPO_CARGA = " & ddlTipoCarga_BasicoAereo.SelectedValue & ", NR_CE = " & txtNumeroCE_BasicoAereo.Text & ", DT_CE = " & txtDataCE_BasicoAereo.Text & ",  OB_REFERENCIA_AUXILIAR =" & txtRefAuxiliar_BasicoAereo.Text & ", OB_REFERENCIA_COMERCIAL = " & txtRefComercial_BasicoAereo.Text & ", NM_RESUMO_MERCADORIA = " & txtResumoMercadoria_BasicoAereo.Text & ",ID_PARCEIRO_RODOVIARIO = " & ddlTranspRod_BasicoAereo.SelectedValue & ",ID_SERVICO = " & ddlServico_BasicoAereo.SelectedValue & ", ID_PARCEIRO_IMPORTADOR = " & ddlImportador_BasicoAereo.SelectedValue & "  WHERE ID_BL = " & txtID_BasicoAereo.Text)

                        LimpaNulo()

                        divSuccess_BasicoAereo.Visible = True
                        Con.Fechar()


                    End If
                Else

                    divErro_BasicoAereo.Visible = True
                    lblErro_BasicoAereo.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                End If





            End If


        End If
    End Sub

    Private Sub btnSalvar_CargaAereo_Click(sender As Object, e As EventArgs) Handles btnSalvar_CargaAereo.Click

        divSuccess_CargaAereo2.Visible = False
        divErro_CargaAereo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData

        If txtPesoBruto_CargaAereo.Text = "" Then
            txtPesoBruto_CargaAereo.Text = 0
        End If

        If txtQtdVolume_CargaAereo.Text = "" Then
            txtQtdVolume_CargaAereo.Text = 0
        End If

        If txtPesoVolumetrico_CargaAereo.Text = "" Then
            txtPesoVolumetrico_CargaAereo.Text = 0
        End If

        If txtAltura_CargaAereo.Text = "" Then
            txtAltura_CargaAereo.Text = 0
        End If

        If txtLargura_CargaAereo.Text = "" Then
            txtLargura_CargaAereo.Text = 0
        End If

        If txtComprimento_CargaAereo.Text = "" Then
            txtComprimento_CargaAereo.Text = 0
        End If

        If txtDescMercadoria_CargaAereo.Text = "" Then
            txtDescMercadoria_CargaAereo.Text = "NULL"
        Else
            txtDescMercadoria_CargaAereo.Text = "'" & txtDescMercadoria_CargaAereo.Text & "'"
        End If

        txtPesoBruto_CargaAereo.Text = txtPesoBruto_CargaAereo.Text.Replace(".", "")
        txtPesoBruto_CargaAereo.Text = txtPesoBruto_CargaAereo.Text.Replace(",", ".")

        txtPesoVolumetrico_CargaAereo.Text = txtPesoVolumetrico_CargaAereo.Text.Replace(".", "")
        txtPesoVolumetrico_CargaAereo.Text = txtPesoVolumetrico_CargaAereo.Text.Replace(",", ".")

        txtComprimento_CargaAereo.Text = txtComprimento_CargaAereo.Text.Replace(".", "")
        txtComprimento_CargaAereo.Text = txtComprimento_CargaAereo.Text.Replace(",", ".")

        txtLargura_CargaAereo.Text = txtLargura_CargaAereo.Text.Replace(".", "")
        txtLargura_CargaAereo.Text = txtLargura_CargaAereo.Text.Replace(",", ".")

        txtAltura_CargaAereo.Text = txtAltura_CargaAereo.Text.Replace(".", "")
        txtAltura_CargaAereo.Text = txtAltura_CargaAereo.Text.Replace(",", ".")


        If txtID_CargaAereo.Text = "" Then


            ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                    divErro_CargaAereo2.Visible = True
                    lblErro_CargaAereo2.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else



                    'INSERE 
                    ds = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,VL_ALTURA,VL_LARGURA,VL_COMPRIMENTO,DS_MERCADORIA,QT_MERCADORIA) VALUES (" & txtID_BasicoAereo.Text & "," & ddlMercadoria_CargaAereo.SelectedValue & ", " & ddlNCM_CargaAereo.SelectedValue & ", " & txtPesoBruto_CargaAereo.Text & "," & txtPesoVolumetrico_CargaAereo.Text & ", " & txtAltura_CargaAereo.Text & "," & txtLargura_CargaAereo.Text & "," & txtComprimento_CargaAereo.Text & "," & txtDescMercadoria_CargaAereo.Text & "," & txtQtdVolume_CargaAereo.Text & ") Select SCOPE_IDENTITY() as ID_BL ")


                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)


                    txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("'", "")
                    txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("NULL", "")

                    Con.Fechar()
                    divSuccess_CargaAereo2.Visible = True
                    dgvCargaAereo.DataBind()
                End If
            Else
                divErro_CargaAereo2.Visible = True
                lblErro_CargaAereo2.Text = "Usuário não possui permissão para cadastrar."
                Exit Sub

            End If


        Else

            ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                    divErro_CargaAereo2.Visible = True
                    lblErro_CargaAereo2.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else


                    'REALIZA UPDATE 
                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & txtID_BasicoAereo.Text & ",ID_MERCADORIA = " & ddlMercadoria_CargaAereo.SelectedValue & ",ID_NCM = " & ddlNCM_CargaAereo.SelectedValue & ",VL_PESO_BRUTO = " & txtPesoBruto_CargaAereo.Text & ",VL_M3 = " & txtPesoVolumetrico_CargaAereo.Text & ",VL_ALTURA =" & txtAltura_CargaAereo.Text & ",VL_LARGURA = " & txtLargura_CargaAereo.Text & ",VL_COMPRIMENTO = " & txtComprimento_CargaAereo.Text & ",DS_MERCADORIA = " & txtDescMercadoria_CargaAereo.Text & ", QT_MERCADORIA = " & txtQtdVolume_CargaAereo.Text & " WHERE ID_CARGA_BL = " & txtID_CargaAereo.Text)



                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoAereo.Text & ") WHERE ID_BL =  " & txtID_BasicoAereo.Text)

                    divSuccess_CargaAereo2.Visible = True
                    Con.Fechar()
                    dgvCargaAereo.DataBind()

                    txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("'", "")
                    txtDescMercadoria_CargaAereo.Text = txtDescMercadoria_CargaAereo.Text.Replace("NULL", "")

                End If
            Else

                divErro_CargaAereo2.Visible = True
                lblErro_CargaAereo2.Text = "Usuário não possui permissão para alterar."
                Exit Sub

            End If





        End If


    End Sub

    Private Sub btnSalvar_CargaMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvar_CargaMaritimo.Click
        divSuccess_CargaMaritimo2.Visible = False
        divErro_CargaMaritimo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData


        If txtQtdVolumes_CargaMaritimo.Text = "" Then
            txtQtdVolumes_CargaMaritimo.Text = 0
        End If

        If txtGrupoNCM_CargaMaritimo.Text = "" Then
            txtGrupoNCM_CargaMaritimo.Text = "NULL"
        Else
            txtGrupoNCM_CargaMaritimo.Text = "'" & txtGrupoNCM_CargaMaritimo.Text & "'"
        End If

        If txtPesoVolumetrico_CargaMaritimo.Text = "" Then
            txtPesoVolumetrico_CargaMaritimo.Text = 0
        End If

        If txtPesoBruto_CargaMaritimo.Text = "" Then
            txtPesoBruto_CargaMaritimo.Text = 0
        End If


        txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(".", "")
        txtPesoBruto_CargaMaritimo.Text = txtPesoBruto_CargaMaritimo.Text.Replace(",", ".")

        txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(".", "")
        txtPesoVolumetrico_CargaMaritimo.Text = txtPesoVolumetrico_CargaMaritimo.Text.Replace(",", ".")

        If txtID_CargaMaritimo.Text = "" Then


            ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                    divErro_CargaMaritimo2.Visible = True
                    lblErro_CargaMaritimo2.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else



                    'INSERE 
                    ds = Con.ExecutarQuery("INSERT INTO TB_CARGA_BL (ID_CNTR_BL, ID_EMBALAGEM, DS_GRUPO_NCM,ID_BL,ID_MERCADORIA,ID_NCM,VL_PESO_BRUTO,VL_M3,QT_MERCADORIA) VALUES (" & ddlNumeroCNTR_CargaMaritimo.SelectedValue & "," & ddlEmbalagem_CargaMaritimo.SelectedValue & "," & txtGrupoNCM_CargaMaritimo.Text & "," & txtID_BasicoMaritimo.Text & "," & ddlMercadoria_CargaMaritimo.SelectedValue & ", " & ddlNCM_CargaMaritimo.SelectedValue & ", " & txtPesoBruto_CargaMaritimo.Text & "," & txtPesoVolumetrico_CargaMaritimo.Text & "," & txtQtdVolumes_CargaMaritimo.Text & ") Select SCOPE_IDENTITY() as ID_BL ")


                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)



                    dgvCargaMaritimo.DataBind()
                    Con.Fechar()
                    divSuccess_CargaMaritimo2.Visible = True
                End If
            Else
                divErro_CargaMaritimo2.Visible = True
                lblErro_CargaMaritimo2.Text = "Usuário não possui permissão para cadastrar."
                Exit Sub

            End If


        Else

            ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                    divErro_CargaMaritimo2.Visible = True
                    lblErro_CargaMaritimo2.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else


                    'REALIZA UPDATE 
                    Con.ExecutarQuery("UPDATE TB_CARGA_BL SET ID_BL = " & txtID_BasicoMaritimo.Text & ",ID_MERCADORIA = " & ddlMercadoria_CargaMaritimo.SelectedValue & ",ID_NCM = " & ddlNCM_CargaMaritimo.SelectedValue & ",VL_PESO_BRUTO = " & txtPesoBruto_CargaMaritimo.Text & ",VL_M3 = " & txtPesoVolumetrico_CargaMaritimo.Text & ",ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue & ", ID_EMBALAGEM = " & ddlEmbalagem_CargaMaritimo.SelectedValue & ", DS_GRUPO_NCM = " & txtGrupoNCM_CargaMaritimo.Text & ", QT_MERCADORIA = " & txtQtdVolumes_CargaMaritimo.Text & " WHERE ID_CARGA_BL = " & txtID_CargaMaritimo.Text)

                    Con.ExecutarQuery("UPDATE TB_BL SET VL_M3 =
(SELECT SUM(ISNULL(VL_M3,0))VL_M3 FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET VL_PESO_BRUTO =
(SELECT SUM(ISNULL(VL_PESO_BRUTO,0))VL_PESO_BRUTO FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & " ; UPDATE TB_BL SET QT_MERCADORIA =
(SELECT SUM(ISNULL(QT_MERCADORIA,0))QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL =  " & txtID_BasicoMaritimo.Text & ") WHERE ID_BL =  " & txtID_BasicoMaritimo.Text)

                    txtGrupoNCM_CargaMaritimo.Text = txtGrupoNCM_CargaMaritimo.Text.Replace("'", "")
                    txtGrupoNCM_CargaMaritimo.Text = txtGrupoNCM_CargaMaritimo.Text.Replace("NULL", "")

                    divSuccess_CargaMaritimo2.Visible = True
                    Con.Fechar()


                End If
            Else

                divErro_CargaMaritimo2.Visible = True
                lblErro_CargaMaritimo2.Text = "Usuário não possui permissão para alterar."
                Exit Sub

            End If


        End If


        mpeCargaMaritimo.Show()

    End Sub

    Private Sub btnSalvar_TaxaAereo_Click(sender As Object, e As EventArgs) Handles btnSalvar_TaxaAereo.Click

        divSuccess_TaxaAereo2.Visible = False
        divErro_TaxaAereo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData



        If txtObs_TaxaAereo.Text = "" Then
            txtObs_TaxaAereo.Text = "NULL"
        Else
            txtObs_TaxaAereo.Text = "'" & txtObs_TaxaAereo.Text & "'"
        End If

        If txtBaseCompra_TaxaAereo.Text = "" Then
            txtBaseCompra_TaxaAereo.Text = 0
        End If

        If txtMinCompra_TaxaAereo.Text = "" Then
            txtMinCompra_TaxaAereo.Text = 0
        End If

        If txtValorCompra_TaxaAereo.Text = "" Then
            txtValorCompra_TaxaAereo.Text = 0
        End If

        txtBaseCompra_TaxaAereo.Text = txtBaseCompra_TaxaAereo.Text.Replace(".", "")
        txtBaseCompra_TaxaAereo.Text = txtBaseCompra_TaxaAereo.Text.Replace(",", ".")

        txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(".", "")
        txtMinCompra_TaxaAereo.Text = txtMinCompra_TaxaAereo.Text.Replace(",", ".")

        txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(".", "")
        txtValorCompra_TaxaAereo.Text = txtValorCompra_TaxaAereo.Text.Replace(",", ".")

        If txtID_TaxaAereo.Text = "" Then


            ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                    divErro_TaxaAereo2.Visible = True
                    lblErro_TaxaAereo2.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    'INSERE 
                    ds = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,FL_PREMIACAO) VALUES (" & txtID_BasicoAereo.Text & "," & ddlDespesa_TaxaAereo.SelectedValue & "," & ddlTipoPagamento_TaxaAereo.SelectedValue & "," & ddlOrigemPagamento_TaxaAereo.SelectedValue & "," & ddlDestinatarioCob_TaxaAereo.SelectedValue & "," & ddlBaseCalculo_TaxaAereo.SelectedValue & "," & ddlMoedaCompra_TaxaAereo.SelectedValue & "," & txtValorCompra_TaxaAereo.Text & "," & txtMinCompra_TaxaAereo.Text & "," & txtObs_TaxaAereo.Text & ",'" & ckbDeclarado_TaxaAereo.Checked & "','" & ckbProfit_TaxaAereo.Checked & "'," & ddlEmpresa_TaxaAereo.SelectedValue & ",'" & ckbPremiacao_TaxaAereo.Checked & "') Select SCOPE_IDENTITY() as ID_BL_TAXA ")


                    txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("'", "")
                    txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("NULL", "")

                    dgvTaxaAereo.DataBind()
                    Con.Fechar()
                    divSuccess_TaxaAereo2.Visible = True

                    Con.ExecutarQuery("UPDATE TB_BL SET FL_CALCULADO = 0 WHERE ID_BL = " & txtID_BasicoAereo.Text)
                End If
            Else
                divErro_TaxaAereo2.Visible = True
                lblErro_TaxaAereo2.Text = "Usuário não possui permissão para cadastrar."
                Exit Sub

            End If


        Else

            ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                    divErro_TaxaAereo2.Visible = True
                    lblErro_TaxaAereo2.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else
                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & txtID_TaxaAereo.Text & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaAereo2.Visible = True
                        lblErro_TaxaAereo2.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
                    Else

                        'REALIZA UPDATE 
                        Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoAereo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_BasicoAereo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaAereo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaAereo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaAereo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxaAereo.SelectedValue & ",VL_TAXA = " & txtValorCompra_TaxaAereo.Text & ",VL_TAXA_MIN = " & txtMinCompra_TaxaAereo.Text & ",OB_TAXAS = " & txtObs_TaxaAereo.Text & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaAereo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaAereo.Checked & "', ID_PARCEIRO_EMPRESA =  " & ddlEmpresa_TaxaAereo.SelectedValue & ",FL_CALCULADO = 0,FL_PREMIACAO ='" & ckbPremiacao_TaxaAereo.Checked & "' WHERE ID_BL_TAXA = " & txtID_TaxaAereo.Text)


                        txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("'", "")
                        txtObs_TaxaAereo.Text = txtObs_TaxaAereo.Text.Replace("NULL", "")

                        divSuccess_TaxaAereo2.Visible = True
                        Con.Fechar()
                        dgvTaxaAereo.DataBind()


                        Con.ExecutarQuery("UPDATE TB_BL SET FL_CALCULADO = 0 WHERE ID_BL = " & txtID_BasicoAereo.Text)
                    End If
                End If
            Else

                divErro_TaxaAereo2.Visible = True
                lblErro_TaxaAereo2.Text = "Usuário não possui permissão para alterar."
                Exit Sub

            End If


        End If






    End Sub

    Private Sub btnSalvar_TaxaMaritimo_Click(sender As Object, e As EventArgs) Handles btnSalvar_TaxaMaritimo.Click
        divSuccess_TaxaMaritimo2.Visible = False
        divErro_TaxaMaritimo2.Visible = False

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet
        Dim v As New VerificaData



        If txtBaseCompra_TaxaMaritimo.Text = "" Then
            txtBaseCompra_TaxaMaritimo.Text = 0
        End If

        If txtMinCompra_TaxaMaritimo.Text = "" Then
            txtMinCompra_TaxaMaritimo.Text = 0
        End If

        If txtValorCompra_TaxaMaritimo.Text = "" Then
            txtValorCompra_TaxaMaritimo.Text = 0
        End If

        If txtObs_TaxaMaritimo.Text = "" Then
            txtObs_TaxaMaritimo.Text = "NULL"
        Else
            txtObs_TaxaMaritimo.Text = "'" & txtObs_TaxaMaritimo.Text & "'"
        End If

        txtBaseCompra_TaxaMaritimo.Text = txtBaseCompra_TaxaMaritimo.Text.Replace(".", "")
        txtBaseCompra_TaxaMaritimo.Text = txtBaseCompra_TaxaMaritimo.Text.Replace(",", ".")

        txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(".", "")
        txtMinCompra_TaxaMaritimo.Text = txtMinCompra_TaxaMaritimo.Text.Replace(",", ".")

        txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(".", "")
        txtValorCompra_TaxaMaritimo.Text = txtValorCompra_TaxaMaritimo.Text.Replace(",", ".")

        If txtID_TaxaMaritimo.Text = "" Then


            ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
                    divErro_TaxaMaritimo2.Visible = True
                    lblErro_TaxaMaritimo2.Text = "Usuário não possui permissão para cadastrar."
                    Exit Sub

                Else

                    'INSERE 
                    ds = Con.ExecutarQuery("INSERT INTO TB_BL_TAXA (ID_BL,ID_ITEM_DESPESA,ID_TIPO_PAGAMENTO,ID_ORIGEM_PAGAMENTO,ID_STATUS_PAGAMENTO,ID_DESTINATARIO_COBRANCA,ID_BASE_CALCULO_TAXA,ID_MOEDA,VL_TAXA,VL_TAXA_MIN,OB_TAXAS,FL_DIVISAO_PROFIT,FL_DECLARADO,ID_PARCEIRO_EMPRESA,FL_PREMIACAO) VALUES (" & txtID_BasicoMaritimo.Text & "," & ddlDespesa_TaxaMaritimo.SelectedValue & "," & ddlTipoPagamento_TaxaMaritimo.SelectedValue & "," & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & "," & ddlStatusPagamento_TaxaMaritimo.SelectedValue & "," & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & "," & ddlBaseCalculo_TaxaMaritimo.SelectedValue & "," & ddlMoedaCompra_TaxaMaritimo.SelectedValue & "," & txtValorCompra_TaxaMaritimo.Text & "," & txtMinCompra_TaxaMaritimo.Text & "," & txtObs_TaxaMaritimo.Text & ",'" & ckbDeclarado_TaxaMaritimo.Checked & "','" & ckbProfit_TaxaMaritimo.Checked & "'," & ddlEmpresa_TaxaMaritimo.SelectedValue & ",'" & ckbPremiacao_TaxaMaritimo.Checked & "') Select SCOPE_IDENTITY() as ID_BL_TAXA ")

                    dgvTaxaMaritimo.DataBind()


                    txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("'", "")
                    txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("NULL", "")
                    divSuccess_TaxaMaritimo2.Visible = True


                    Con.ExecutarQuery("UPDATE TB_BL SET FL_CALCULADO = 0 WHERE ID_BL = " & txtID_BasicoAereo.Text)
                    Con.Fechar()
                End If
            Else
                divErro_TaxaMaritimo2.Visible = True
                lblErro_TaxaMaritimo2.Text = "Usuário não possui permissão para cadastrar."
                Exit Sub

            End If


        Else

            ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 1026 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                    divErro_TaxaMaritimo2.Visible = True
                    lblErro_TaxaMaritimo2.Text = "Usuário não possui permissão para alterar."
                    Exit Sub

                Else
                    Dim ds1 As DataSet = Con.ExecutarQuery("select COUNT(A.ID_BL_TAXA)ID_BL_TAXA
from TB_CONTA_PAGAR_RECEBER_ITENS A 
LEFT JOIN TB_CONTA_PAGAR_RECEBER C ON C.ID_CONTA_PAGAR_RECEBER = A.ID_CONTA_PAGAR_RECEBER 
WHERE A.ID_BL_TAXA =" & txtID_TaxaMaritimo.Text & " and DT_CANCELAMENTO is null ")


                    If ds1.Tables(0).Rows(0).Item("ID_BL_TAXA") > 0 Then
                        divErro_TaxaMaritimo2.Visible = True
                        lblErro_TaxaMaritimo2.Text = "Não foi possível completar ação: taxa já enviada para contas a pagar/receber!"
                    Else


                        'REALIZA UPDATE 
                        Con.ExecutarQuery("UPDATE TB_BL_TAXA SET ID_BL=" & txtID_BasicoMaritimo.Text & ",ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue & ",ID_TIPO_PAGAMENTO = " & ddlTipoPagamento_TaxaMaritimo.SelectedValue & ",ID_ORIGEM_PAGAMENTO = " & ddlOrigemPagamento_TaxaMaritimo.SelectedValue & ",ID_STATUS_PAGAMENTO = " & ddlStatusPagamento_TaxaMaritimo.SelectedValue & ",ID_DESTINATARIO_COBRANCA = " & ddlDestinatarioCob_TaxaMaritimo.SelectedValue & ",ID_BASE_CALCULO_TAXA = " & ddlBaseCalculo_TaxaMaritimo.SelectedValue & ",ID_MOEDA =" & ddlMoedaCompra_TaxaMaritimo.SelectedValue & ",VL_TAXA = " & txtValorCompra_TaxaMaritimo.Text & ",VL_TAXA_MIN = " & txtMinCompra_TaxaMaritimo.Text & ",OB_TAXAS = " & txtObs_TaxaMaritimo.Text & ",FL_DIVISAO_PROFIT = '" & ckbProfit_TaxaMaritimo.Checked & "',FL_DECLARADO  = '" & ckbDeclarado_TaxaMaritimo.Checked & "',ID_PARCEIRO_EMPRESA = " & ddlEmpresa_TaxaMaritimo.SelectedValue & ",FL_CALCULADO = 0, FL_PREMIACAO  = '" & ckbPremiacao_TaxaMaritimo.Checked & "' WHERE ID_BL_TAXA = " & txtID_TaxaMaritimo.Text)

                        dgvTaxaMaritimo.DataBind()
                        divSuccess_TaxaMaritimo2.Visible = True


                        txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("'", "")
                        txtObs_TaxaMaritimo.Text = txtObs_TaxaMaritimo.Text.Replace("NULL", "")
                        Con.ExecutarQuery("UPDATE TB_BL SET FL_CALCULADO = 0 WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                        Con.Fechar()
                    End If
                End If
            Else

                divErro_TaxaMaritimo2.Visible = True
                lblErro_TaxaMaritimo2.Text = "Usuário não possui permissão para alterar."
                Exit Sub

            End If


        End If


        mpeTaxaMaritimo.Show()

    End Sub

    Private Sub dgvRefMaritimo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvRefMaritimo.RowCommand
        divSuccess_RefMaritimo.Visible = False
        divErro_RefMaritimo.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1026 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then
                    Con.ExecutarQuery("DELETE From TB_REFERENCIA_CLIENTE Where ID_REFERENCIA_CLIENTE = " & ID)
                    lblSuccess_RefMaritimo.Text = "Registro deletado!"
                    divSuccess_RefMaritimo.Visible = True
                    dgvRefMaritimo.DataBind()

                Else
                    lblErro_RefMaritimo.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_RefMaritimo.Visible = True
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select NR_REFERENCIA_CLIENTE from TB_REFERENCIA_CLIENTE
WHERE ID_REFERENCIA_CLIENTE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                txtID_RefMaritimo.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                    txtRefMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
                End If

            End If
        End If
        Con.Fechar()
    End Sub
    Private Sub dgvRefAereo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvRefAereo.RowCommand
        divSuccess_RefAereo.Visible = False
        divErro_RefAereo.Visible = False
        Dim Con As New Conexao_sql
        Dim ds As DataSet
        Con.Conectar()
        If e.CommandName = "Excluir" Then


            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("SELECT FL_EXCLUIR FROM [TB_GRUPO_PERMISSAO] WHERE ID_MENU = 1026 AND ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO"))
            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FL_EXCLUIR").ToString() = True Then
                    Con.ExecutarQuery("DELETE From TB_REFERENCIA_CLIENTE Where ID_REFERENCIA_CLIENTE = " & ID)
                    lblSuccess_RefAereo.Text = "Registro deletado!"
                    divSuccess_RefAereo.Visible = True
                    dgvRefAereo.DataBind()

                Else
                    lblErro_RefAereo.Text = "Usuário não tem permissão para realizar exclusões"
                    divErro_RefAereo.Visible = True
                End If
            End If

        ElseIf e.CommandName = "visualizar" Then
            Dim ID As String = e.CommandArgument

            ds = Con.ExecutarQuery("select NR_REFERENCIA_CLIENTE from TB_REFERENCIA_CLIENTE
WHERE ID_REFERENCIA_CLIENTE = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then

                txtID_RefAereo.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")) Then
                    txtRefAereo.Text = ds.Tables(0).Rows(0).Item("NR_REFERENCIA_CLIENTE")
                End If

            End If
        End If
        Con.Fechar()
    End Sub

    Sub NumeroProcesso()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT NRSEQUENCIALPROCESSO, AnoSequencialProcesso FROM TB_PARAMETROS")

        Dim PROCESSO_FINAL As String

        Dim NRSEQUENCIALPROCESSO As Integer = ds.Tables(0).Rows(0).Item("NRSEQUENCIALPROCESSO")
        Dim AnoSequencialProcesso = ds.Tables(0).Rows(0).Item("AnoSequencialProcesso")
        Dim ano_atual = Now.Year.ToString.Substring(2)
        Dim SIGLA_PROCESSO As String
        Dim mes_atual As String
        If Now.Month < 10 Then
            mes_atual = "0" & Now.Month.ToString
        Else
            mes_atual = Now.Month.ToString
        End If


        If txtID_BasicoMaritimo.Text <> "" Then
            ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO from TB_BL A Where ID_SERVICO <> 0 AND A.ID_BL = " & txtID_BasicoMaritimo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

                If AnoSequencialProcesso = ano_atual Then

                    NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1
                    PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

                    Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
                    txtProcesso_BasicoMaritimo.Text = PROCESSO_FINAL

                Else

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET AnoSequencialProcesso = '" & ano_atual & "'")

                    NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1

                    PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

                End If

                Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtID_BasicoMaritimo.Text)
            End If


        ElseIf txtID_BasicoAereo.Text <> "" Then

            ds = Con.ExecutarQuery("Select A.ID_SERVICO,(SELECT SIGLA_PROCESSO FROM TB_SERVICO WHERE ID_SERVICO = A.ID_SERVICO)SIGLA_PROCESSO from TB_BL A Where ID_SERVICO <> 0 AND A.ID_BL = " & txtID_BasicoAereo.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                SIGLA_PROCESSO = ds.Tables(0).Rows(0).Item("SIGLA_PROCESSO")

                If AnoSequencialProcesso = ano_atual Then

                    NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1
                    PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

                    Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtID_BasicoAereo.Text)
                    txtProcesso_BasicoAereo.Text = PROCESSO_FINAL
                Else

                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET AnoSequencialProcesso = '" & ano_atual & "'")

                    NRSEQUENCIALPROCESSO = NRSEQUENCIALPROCESSO + 1

                    PROCESSO_FINAL = SIGLA_PROCESSO & NRSEQUENCIALPROCESSO.ToString.PadLeft(4, "0") & "-" & mes_atual & "/" & ano_atual
                    Con.ExecutarQuery("UPDATE TB_PARAMETROS SET NRSEQUENCIALPROCESSO = '" & NRSEQUENCIALPROCESSO & "'")

                End If

                Con.ExecutarQuery("UPDATE TB_BL SET NR_PROCESSO = '" & PROCESSO_FINAL & "' WHERE ID_BL = " & txtID_BasicoAereo.Text)
            End If



        End If
    End Sub

    Sub PermissoesEspeciais()
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_GRAVAR_MASTER_BASICO,FL_GRAVAR_MASTER_CONTAINER,FL_GRAVAR_MASTER_TAXAS,FL_GRAVAR_MASTER_VINCULAR,FL_GRAVAR_HOUSE_BASICO,FL_GRAVAR_HOUSE_CARGA,FL_GRAVAR_HOUSE_TAXAS
FROM TB_USUARIO where ID_USUARIO =" & Session("ID_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_BASICO") = True Then
                btnGravar_BasicoMaritimo.Visible = True
                btnLimpar_BasicoMaritimo.Visible = True
                btnGravar_BasicoAereo.Visible = True
                btnLimpar_BasicoAereo.Visible = True
            End If

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_CARGA") = True Then

                btnNovaCargaMaritimo.Visible = True
                btnSalvar_CargaMaritimo.Visible = True
                btnNovaCargaAereo.Visible = True
                btnSalvar_CargaAereo.Visible = True
            End If

            If ds.Tables(0).Rows(0).Item("FL_GRAVAR_HOUSE_TAXAS") = True Then
                btnNovaTaxaMaritimo.Visible = True
                btnSalvar_TaxaMaritimo.Visible = True
                btnNovaTaxaAereo.Visible = True
                btnSalvar_TaxaAereo.Visible = True
            End If

        End If



    End Sub

    Private Sub ddlDespesa_TaxaMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDespesa_TaxaMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT FL_PREMIACAO FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxaMaritimo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            ckbPremiacao_TaxaMaritimo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
        End If

    End Sub

    Private Sub ddlDespesa_TaxaAereo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDespesa_TaxaAereo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet

        ds = Con.ExecutarQuery("SELECT FL_PREMIACAO FROM TB_ITEM_DESPESA WHERE ID_ITEM_DESPESA = " & ddlDespesa_TaxaAereo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            ckbPremiacao_TaxaAereo.Checked = ds.Tables(0).Rows(0).Item("FL_PREMIACAO")
        End If

    End Sub

    Private Sub ddlTipoContainer_CargaMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoContainer_CargaMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim sql As String = "SELECT ID_CNTR_BL, NR_CNTR FROM TB_CNTR_BL WHERE ID_TIPO_CNTR = " & ddlTipoContainer_CargaMaritimo.SelectedValue & " AND ID_BL_MASTER = " & Session("ID_BL_MASTER") & "
union SELECT 0, 'Selecione' FROM [dbo].[TB_CNTR_BL] ORDER BY ID_CNTR_BL"
        Dim ds As DataSet = Con.ExecutarQuery(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dsCNTR.SelectCommand = sql
            ddlNumeroCNTR_CargaMaritimo.DataBind()
        End If
    End Sub

    Private Sub ddlNumeroCNTR_CargaMaritimo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNumeroCNTR_CargaMaritimo.SelectedIndexChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT VL_PESO_TARA,NR_LACRE FROM TB_CNTR_BL WHERE ID_CNTR_BL = " & ddlNumeroCNTR_CargaMaritimo.SelectedValue)
        If ds.Tables(0).Rows.Count > 0 Then
            txtNumeroLacre_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("NR_LACRE")
            txtValorTara_CargaMaritimo.Text = ds.Tables(0).Rows(0).Item("VL_PESO_TARA")
        End If
    End Sub
End Class