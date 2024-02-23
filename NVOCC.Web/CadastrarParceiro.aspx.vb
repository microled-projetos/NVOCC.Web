Imports Newtonsoft.Json
Public Class CadastrarParceiro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        divmsg.Visible = False

        If Not Page.IsPostBack Then
            ddlTipoFaturamentoImpoLCL.SelectedValue = 1
            CarregaCampos()
        End If



        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

            Response.Redirect("Default.aspx")

        End If


        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 And Request.QueryString("id") = "" Then
            btnGravar.Visible = False
            btnLimpar.Visible = False
        End If

        ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
        If ds.Tables(0).Rows(0).Item("QTD") = 0 And Request.QueryString("id") <> "" Then
            btnGravar.Visible = False
            btnLimpar.Visible = False
        End If

        Con.Fechar()
    End Sub

    Sub CarregaCampos()
        If Request.QueryString("id") <> "" Then
            Dim Con As New Conexao_sql
            Dim ID As String = Request.QueryString("id")
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO,
FL_IMPORTADOR,
FL_EXPORTADOR,
FL_AGENTE,
FL_AGENTE_INTERNACIONAL,
FL_TRANSPORTADOR,
FL_COMISSARIA,
FL_VENDEDOR,
FL_ARMAZEM_ATRACACAO,
FL_ARMAZEM_DESEMBARACO,
FL_ARMAZEM_DESCARGA,
FL_PRESTADOR, 
NM_RAZAO,
NM_FANTASIA,
CNPJ,
CPF,
ISNULL(TP_PESSOA,0)TP_PESSOA,
INSCR_ESTADUAL,
INSCR_MUNICIPAL,
EMAIL,
ENDERECO,
NR_ENDERECO,
BAIRRO,
COMPL_ENDERECO,
ISNULL(ID_CIDADE,0)ID_CIDADE,
ISNULL(ID_PAIS,0)ID_PAIS,
TELEFONE,
CEP,
ISNULL(ID_VENDEDOR,0)ID_VENDEDOR,
FL_ISENTO_ISS,
FL_ISENTO_PIS,
FL_ISENTO_COFINS,
VL_ALIQUOTA_ISS,
VL_ALIQUOTA_PIS,
VL_ALIQUOTA_COFINS,
EMAIL_NF_ELETRONICA,
CD_IATA,FL_SIMPLES_NACIONAL,
FL_ATIVO,
FL_INDICADOR,
PC_CAMBIAL_AEREO,
PC_CAMBIAL_MARITIMO,
ISNULL(ID_TIPO_FATURAMENTO_EXPO_AEREO,0)ID_TIPO_FATURAMENTO_EXPO_AEREO ,
ISNULL(ID_TIPO_FATURAMENTO_EXPO_LCL,0)ID_TIPO_FATURAMENTO_EXPO_LCL ,
ISNULL(ID_TIPO_FATURAMENTO_EXPO_FREE,0)ID_TIPO_FATURAMENTO_EXPO_FREE ,
ISNULL(ID_TIPO_FATURAMENTO_EXPO_FCL,0)ID_TIPO_FATURAMENTO_EXPO_FCL ,
ISNULL(ID_TIPO_FATURAMENTO_IMPO_FREE,0)ID_TIPO_FATURAMENTO_IMPO_FREE ,
ISNULL(ID_TIPO_FATURAMENTO_IMPO_AEREO,0)ID_TIPO_FATURAMENTO_IMPO_AEREO ,
ISNULL(ID_TIPO_FATURAMENTO_IMPO_LCL,0)ID_TIPO_FATURAMENTO_IMPO_LCL ,
ISNULL(ID_TIPO_FATURAMENTO_IMPO_FCL,0)ID_TIPO_FATURAMENTO_IMPO_FCL ,
ISNULL(QT_DIAS_FATURAMENTO_EXPO_FREE,0)QT_DIAS_FATURAMENTO_EXPO_FREE ,
ISNULL(QT_DIAS_FATURAMENTO_EXPO_AEREO,0)QT_DIAS_FATURAMENTO_EXPO_AEREO ,
ISNULL(QT_DIAS_FATURAMENTO_EXPO_LCL,0)QT_DIAS_FATURAMENTO_EXPO_LCL ,
ISNULL(QT_DIAS_FATURAMENTO_EXPO_FCL,0)QT_DIAS_FATURAMENTO_EXPO_FCL ,
ISNULL(QT_DIAS_FATURAMENTO_IMPO_FREE,0)QT_DIAS_FATURAMENTO_IMPO_FREE ,
ISNULL(QT_DIAS_FATURAMENTO_IMPO_AEREO,0)QT_DIAS_FATURAMENTO_IMPO_AEREO ,
ISNULL(QT_DIAS_FATURAMENTO_IMPO_LCL,0)QT_DIAS_FATURAMENTO_IMPO_LCL ,
ISNULL(QT_DIAS_FATURAMENTO_IMPO_FCL,0)QT_DIAS_FATURAMENTO_IMPO_FCL ,
ISNULL(ID_ACORDO_CAMBIO_EXPO_FREE,0)ID_ACORDO_CAMBIO_EXPO_FREE ,
ISNULL(ID_ACORDO_CAMBIO_EXPO_AEREO,0)ID_ACORDO_CAMBIO_EXPO_AEREO ,
ISNULL(ID_ACORDO_CAMBIO_EXPO_LCL,0)ID_ACORDO_CAMBIO_EXPO_LCL ,
ISNULL(ID_ACORDO_CAMBIO_EXPO_FCL,0)ID_ACORDO_CAMBIO_EXPO_FCL ,
ISNULL(ID_ACORDO_CAMBIO_IMPO_FREE,0)ID_ACORDO_CAMBIO_IMPO_FREE ,
ISNULL(ID_ACORDO_CAMBIO_IMPO_AEREO,0)ID_ACORDO_CAMBIO_IMPO_AEREO ,
ISNULL(ID_ACORDO_CAMBIO_IMPO_LCL,0)ID_ACORDO_CAMBIO_IMPO_LCL ,
ISNULL(ID_ACORDO_CAMBIO_IMPO_FCL,0)ID_ACORDO_CAMBIO_IMPO_FCL ,
ISNULL(SPREAD_EXPO_FREEHAND,0)SPREAD_EXPO_FREEHAND ,
ISNULL(SPREAD_EXPO_AEREO,0)SPREAD_EXPO_AEREO ,
ISNULL(SPREAD_EXPO_LCL,0)SPREAD_EXPO_LCL ,
ISNULL(SPREAD_EXPO_FCL,0)SPREAD_EXPO_FCL ,
ISNULL(SPREAD_IMPO_FREEHAND,0)SPREAD_IMPO_FREEHAND ,
ISNULL(SPREAD_IMPO_AEREO,0)SPREAD_IMPO_AEREO ,
ISNULL(SPREAD_IMPO_LCL,0)SPREAD_IMPO_LCL ,
ISNULL(SPREAD_IMPO_FCL,0)SPREAD_IMPO_FCL, 
FL_VENDEDOR_DIRETO,
FL_EQUIPE_INSIDE_SALES,
FL_SHIPPER,
FL_CNEE,
FL_RODOVIARIO,
OB_COMPLEMENTARES,
ISNULL(REGRA_ATUALIZACAO,0)REGRA_ATUALIZACAO,
ISNULL(FL_PROSPECCAO,0)FL_PROSPECCAO,
ISNULL(ID_VENDEDOR_PROSPECCAO,0)ID_VENDEDOR_PROSPECCAO,
ISNULL(FL_INDICACAO_INTERNA,0)FL_INDICACAO_INTERNA,
ISNULL(ID_PARCEIRO_INDICACAO_INTERNA ,0)ID_PARCEIRO_INDICACAO_INTERNA 
FROM TB_PARCEIRO
WHERE ID_PARCEIRO =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtID.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO").ToString()
                ckbImportador.Checked = ds.Tables(0).Rows(0).Item("FL_IMPORTADOR")
                ckbExportador.Checked = ds.Tables(0).Rows(0).Item("FL_EXPORTADOR")
                ckbAgente.Checked = ds.Tables(0).Rows(0).Item("FL_AGENTE")
                ckbAgenteInternacional.Checked = ds.Tables(0).Rows(0).Item("FL_AGENTE_INTERNACIONAL")

                If ds.Tables(0).Rows(0).Item("FL_AGENTE_INTERNACIONAL") = "True" Then
                    divDadosBancarios.Attributes.CssStyle.Add("display", "block")
                Else
                    divDadosBancarios.Attributes.CssStyle.Add("display", "none")
                End If
                ckbTransportador.Checked = ds.Tables(0).Rows(0).Item("FL_TRANSPORTADOR")
                ckbComissaria.Checked = ds.Tables(0).Rows(0).Item("FL_COMISSARIA")
                ckbVendedor.Checked = ds.Tables(0).Rows(0).Item("FL_VENDEDOR")
                ckbArmazemAtracacao.Checked = ds.Tables(0).Rows(0).Item("FL_ARMAZEM_ATRACACAO")
                ckbArmazemDesembaraco.Checked = ds.Tables(0).Rows(0).Item("FL_ARMAZEM_DESEMBARACO")
                ckbArmazemDescarga.Checked = ds.Tables(0).Rows(0).Item("FL_ARMAZEM_DESCARGA")
                ckbPrestador.Checked = ds.Tables(0).Rows(0).Item("FL_PRESTADOR")
                ckbShipper.Checked = ds.Tables(0).Rows(0).Item("FL_SHIPPER")
                ckbCNEE.Checked = ds.Tables(0).Rows(0).Item("FL_CNEE")
                ckbTranspRodoviario.Checked = ds.Tables(0).Rows(0).Item("FL_RODOVIARIO")
                txtRazaoSocial.Text = ds.Tables(0).Rows(0).Item("NM_RAZAO").ToString()
                txtNomeFantasia.Text = ds.Tables(0).Rows(0).Item("NM_FANTASIA").ToString()
                ddlTipoPessoa.SelectedValue = ds.Tables(0).Rows(0).Item("TP_PESSOA").ToString()
                txtCNPJ.Text = ds.Tables(0).Rows(0).Item("CNPJ").ToString()
                txtCPF.Text = ds.Tables(0).Rows(0).Item("CPF").ToString()
                txtInscEstadual.Text = ds.Tables(0).Rows(0).Item("INSCR_ESTADUAL").ToString()
                txtInscMunicipal.Text = ds.Tables(0).Rows(0).Item("INSCR_MUNICIPAL").ToString()
                txtEndereco.Text = ds.Tables(0).Rows(0).Item("ENDERECO").ToString()
                txtNumero.Text = ds.Tables(0).Rows(0).Item("NR_ENDERECO").ToString()
                txtBairro.Text = ds.Tables(0).Rows(0).Item("BAIRRO").ToString()
                txtComplemento.Text = ds.Tables(0).Rows(0).Item("COMPL_ENDERECO").ToString()
                txtOBSComplementares.Text = ds.Tables(0).Rows(0).Item("OB_COMPLEMENTARES").ToString()
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMAIL")) Then
                    txtEmailParceiro.Text = ds.Tables(0).Rows(0).Item("EMAIL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_CIDADE")) Then

                    Dim dsCidade As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_CIDADE WHERE ID_CIDADE =" & ds.Tables(0).Rows(0).Item("ID_CIDADE"))
                    If dsCidade.Tables(0).Rows(0).Item("QTD") = 0 Then
                        If ddlTipoPessoa.SelectedValue <> 3 Then
                            msgErro.Text = "Atualização Cadastral Pendente: Cidade selecionada inexistente!"
                            divmsg1.Visible = True
                        End If


                    Else
                        divmsg1.Visible = False
                        ddlCidade.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CIDADE")

                    End If
                End If

                txtTelefone.Text = ds.Tables(0).Rows(0).Item("TELEFONE").ToString()
                txtCEP.Text = ds.Tables(0).Rows(0).Item("CEP").ToString()

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_VENDEDOR")) Then

                    If ds.Tables(0).Rows(0).Item("ID_VENDEDOR") <> 0 Then


                        Dim dssqlVendedor As DataSet = Con.ExecutarQuery("SELECT COUNT(*)QTD FROM TB_PARCEIRO WHERE FL_VENDEDOR =1 AND FL_ATIVO = 1 AND ID_PARCEIRO =" & ds.Tables(0).Rows(0).Item("ID_VENDEDOR"))
                        If dssqlVendedor.Tables(0).Rows(0).Item("QTD") = 0 Then
                            msgErro.Text = "Atualização Cadastral Pendente: Necessário cadastrar um vendedor valido para este parceiro!"
                            divmsg1.Visible = True
                            dsVendedor.SelectCommand = "SELECT ID_PARCEIRO, NM_RAZAO  FROM TB_PARCEIRO WHERE (FL_VENDEDOR = 1  AND FL_ATIVO = 1) OR ID_PARCEIRO = " & ds.Tables(0).Rows(0).Item("ID_VENDEDOR") & " union SELECT  0, '  Selecione' ORDER BY NM_RAZAO"
                            txtID_Vendedor.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR")
                            ddlVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR")
                        Else
                            txtID_Vendedor.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR")
                            txtID_Vendedor.DataBind()
                            ddlVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR")
                            divmsg1.Visible = False
                        End If
                    Else
                        txtID_Vendedor.Text = ds.Tables(0).Rows(0).Item("ID_VENDEDOR")
                        txtID_Vendedor.DataBind()
                        ddlVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR")
                        divmsg1.Visible = False
                    End If
                End If


                ckbISS.Checked = ds.Tables(0).Rows(0).Item("FL_ISENTO_ISS")
                ckbPIS.Checked = ds.Tables(0).Rows(0).Item("FL_ISENTO_PIS")
                ckbCOFINS.Checked = ds.Tables(0).Rows(0).Item("FL_ISENTO_COFINS")

                '-------------------------------------------------------IMPO----------------------------------------------------------
                txtSpreadImpoFCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_IMPO_FCL")
                txtSpreadImpoLCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_IMPO_LCL")
                txtSpreadImpoAereo.Text = ds.Tables(0).Rows(0).Item("SPREAD_IMPO_AEREO")
                txtSpreadImpoFree.Text = ds.Tables(0).Rows(0).Item("SPREAD_IMPO_FREEHAND")

                ddlAcordoCambioImpoFCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_IMPO_FCL")
                ddlAcordoCambioImpoLCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_IMPO_LCL")
                ddlAcordoCambioImpoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_IMPO_AEREO")
                ddlAcordoCambioImpoFree.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_IMPO_FREE")

                txtQtdFaturamentoImpoFCL.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_IMPO_FCL")
                txtQtdFaturamentoImpoLCL.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_IMPO_LCL")
                txtQtdFaturamentoImpoAereo.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_IMPO_AEREO")
                txtQtdFaturamentoImpoFree.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_IMPO_FREE")

                ddlTipoFaturamentoImpoFCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_IMPO_FCL")
                ddlTipoFaturamentoImpoLCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_IMPO_LCL")
                ddlTipoFaturamentoImpoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_IMPO_AEREO")
                ddlTipoFaturamentoImpoFree.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_IMPO_FREE")

                '------------------------------------------------------EXPO----------------------------------------------------------
                txtSpreadExpoFCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_EXPO_FCL")
                txtSpreadExpoLCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_EXPO_LCL")
                txtSpreadExpoAereo.Text = ds.Tables(0).Rows(0).Item("SPREAD_EXPO_AEREO")
                txtSpreadExpoFree.Text = ds.Tables(0).Rows(0).Item("SPREAD_EXPO_FREEHAND")


                ddlAcordoCambioExpoFCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_EXPO_FCL")
                ddlAcordoCambioExpoLCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_EXPO_LCL")
                ddlAcordoCambioExpoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_EXPO_AEREO")
                ddlAcordoCambioExpoFree.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_EXPO_FREE")

                txtQtdFaturamentoExpoFCL.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_EXPO_FCL")
                txtQtdFaturamentoExpoLCL.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_EXPO_LCL")
                txtQtdFaturamentoExpoAereo.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_EXPO_AEREO")
                txtQtdFaturamentoExpoFree.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO_EXPO_FREE")


                ddlTipoFaturamentoExpoFCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_EXPO_FCL")
                ddlTipoFaturamentoExpoLCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_EXPO_LCL")
                ddlTipoFaturamentoExpoAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_EXPO_AEREO")
                ddlTipoFaturamentoExpoFree.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO_EXPO_FREE")

                '-------------------------



                ddlRegraAtualizacao.SelectedValue = ds.Tables(0).Rows(0).Item("REGRA_ATUALIZACAO")


                ckbVendedorDireto.Checked = ds.Tables(0).Rows(0).Item("FL_VENDEDOR_DIRETO")
                ckbEquipeInsideSales.Checked = ds.Tables(0).Rows(0).Item("FL_EQUIPE_INSIDE_SALES")

                txtAliquotaISS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS").ToString()

                txtAliquotaPIS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS").ToString()

                txtAliquotaCOFINS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS").ToString()

                ddlPais.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PAIS")
                txtEmailNF.Text = ds.Tables(0).Rows(0).Item("EMAIL_NF_ELETRONICA").ToString()
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMAIL_NF_ELETRONICA")) Then
                    txtEmailNF.Text = ds.Tables(0).Rows(0).Item("EMAIL_NF_ELETRONICA").ToString()

                End If
                txtCDIATA.Text = ds.Tables(0).Rows(0).Item("CD_IATA").ToString()
                ckbSimplesNacional.Checked = ds.Tables(0).Rows(0).Item("FL_SIMPLES_NACIONAL")
                ckbAtivo.Checked = ds.Tables(0).Rows(0).Item("FL_ATIVO")
                ckbIndicador.Checked = ds.Tables(0).Rows(0).Item("FL_INDICADOR")

                ckbProspeccao.Checked = ds.Tables(0).Rows(0).Item("FL_PROSPECCAO")
                ckbIndicacaoInterna.Checked = ds.Tables(0).Rows(0).Item("FL_INDICACAO_INTERNA")
                ddlTimeProspeccao.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR_PROSPECCAO")
                ddlIndicacaoInterna.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PARCEIRO_INDICACAO_INTERNA")

                VerificaChecks()

                If ddlTipoPessoa.SelectedValue = 2 Then
                    txtCPF.Enabled = True
                    txtCNPJ.Enabled = False
                Else
                    txtCPF.Enabled = False
                    txtCNPJ.Enabled = True

                End If

            End If

            Con.Fechar()
        Else
            ckbAtivo.Checked = True

        End If
    End Sub
    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Response.Redirect("CadastrarParceiro.aspx")

    End Sub

    Private Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        divmsg.Visible = False
        divmsg1.Visible = False
        divInformativa.Visible = False
        divSuccessContato.Visible = False
        divErroContato.Visible = False
        divSuccessEvento.Visible = False
        divErroEvento.Visible = False

        Dim ds As DataSet

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim UF As String = ""
        Dim dsUf As DataSet = Con.ExecutarQuery("SELECT ISNULL(E.SIGLA_ESTADO,0)UF FROM TB_ESTADO E INNER JOIN TB_CIDADE C ON C.ID_ESTADO = E.ID_ESTADO WHERE C.ID_CIDADE = " & ddlCidade.SelectedValue)
        If dsUf.Tables(0).Rows.Count > 0 Then
            UF = dsUf.Tables(0).Rows(0).Item("UF")
        End If

        If txtQtdFaturamentoImpoFCL.Text = "" Then
            txtQtdFaturamentoImpoFCL.Text = 0
        End If

        If txtRazaoSocial.Text = "" Then
            msgErro.Text = "Preencha todos os campos obrigatórios."
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf ddlTipoPessoa.SelectedValue = 0 Then
            msgErro.Text = "Preencha todos os campos obrigatórios."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf (ddlTipoPessoa.SelectedValue <> 3 And ckbVendedor.Checked = False And ckbVendedorDireto.Checked = False) And (ddlCidade.SelectedValue = 0 Or txtEndereco.Text = "" Or txtBairro.Text = "" Or txtNumero.Text = "" Or txtCEP.Text = "") Then
            msgErro.Text = "Preencha todos os campos obrigatórios."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmailParceiro.Text <> "" And ValidaEmail.Validar(txtEmailParceiro.Text) = False Then
            msgErro.Text = "E-Mail informado é inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmailNF.Text = "" And ckbImportador.Checked = True Then
            msgErro.Text = "Campo E-Mail NF é obrigatório para Parceiros do tipo Importador."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmailNF.Text = "" And ckbExportador.Checked = True Then
            msgErro.Text = "Campo E-Mail NF é obrigatório para Parceiros do tipo Exportador."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmailNF.Text = "" And ckbAgente.Checked = True And ckbAgenteInternacional.Checked = False Then
            msgErro.Text = "Campo E-Mail NF é obrigatório para Parceiros do tipo NVOCC."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmailNF.Text = "" And ckbComissaria.Checked = True Then
            msgErro.Text = "Campo E-Mail NF é obrigatório para Parceiros do tipo Comissária."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtCNPJ.Text = "" And txtCPF.Text = "" Then
            msgErro.Text = "Preencha todos os campos obrigatórios."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ckbImportador.Checked = True And ddlVendedor.SelectedValue = 0 Then
            msgErro.Text = "O campo de vendedor é obrigatório para Parceiros do tipo Importador."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ddlVendedor.SelectedValue = 0 And ckbExportador.Checked = True Then
            msgErro.Text = "O campo de vendedor é obrigatório para Parceiros do tipo Exportador."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ddlVendedor.SelectedValue = 0 And ckbAgente.Checked = True And ckbAgenteInternacional.Checked = False Then
            msgErro.Text = "O campo de vendedor é obrigatório para Parceiros do tipo NVOCC."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ddlVendedor.SelectedValue = 0 And ckbComissaria.Checked = True Then
            msgErro.Text = "O campo de vendedor é obrigatório para Parceiros do tipo Comissária."
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf ckbImportador.Checked = False And ckbExportador.Checked = False And ckbAgente.Checked = False And ckbComissaria.Checked = False And ckbArmazemDescarga.Checked = False And ckbArmazemDesembaraco.Checked = False And ckbArmazemAtracacao.Checked = False And ckbAgenteInternacional.Checked = False And ckbTransportador.Checked = False And ckbPrestador.Checked = False And ckbVendedor.Checked = False And ckbVendedorDireto.Checked = False And ckbEquipeInsideSales.Checked = False And ckbIndicador.Checked = False And ckbShipper.Checked = False And ckbCNEE.Checked = False And ckbTranspRodoviario.Checked = False Then
            msgErro.Text = "Marque o tipo de parceiro"
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ddlTipoPessoa.SelectedValue = 1 And ValidaCNPJ.Validar(txtCNPJ.Text) = False Then
            msgErro.Text = "CNPJ Inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ddlTipoPessoa.SelectedValue = 2 And ValidaCPF.Validar(txtCPF.Text) = False Then
            msgErro.Text = "CPF Inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmailNF.Text <> "" And ValidaEmail.Validar(txtEmailNF.Text) = False Then
            msgErro.Text = "E-Mail NF informado é inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmailContato.Text <> "" And ValidaEmail.Validar(txtEmailContato.Text) = False Then
            msgErro.Text = "E-Mail informado na aba Contatos é inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf txtEmail.Text <> "" And SeparaEmail(txtEmail.Text) = False Then
            msgErro.Text = "E-Mail informado na aba Email x Eventos é inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ckbTransportador.Checked = True And ddlRegraAtualizacao.SelectedValue = 0 And ckbAtivo.Checked = True Then
            msgErro.Text = "Necessário informar a <strong>regra de atualização</strong> na Aba de Inf. Adicionais!"
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf (ckbImportador.Checked = True Or ckbExportador.Checked = True Or ckbAgente.Checked = True Or ckbComissaria.Checked = True Or ckbAgenteInternacional.Checked = True Or ckbIndicador.Checked = True Or ckbShipper.Checked = True Or ckbCNEE.Checked = True Or ckbTranspRodoviario.Checked = True) And ddlTipoFaturamentoImpoLCL.SelectedValue = 0 Then
            msgErro.Text = "Informe o tipo de faturamento na aba de Inf. Adicionais!"
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf (ddlTipoFaturamentoImpoFCL.SelectedValue = 2 And txtQtdFaturamentoImpoFCL.Text = 0) Or (ddlTipoFaturamentoImpoLCL.SelectedValue = 2 And txtQtdFaturamentoImpoLCL.Text = 0) Or (ddlTipoFaturamentoImpoAereo.SelectedValue = 2 And txtQtdFaturamentoImpoAereo.Text = 0) Or (ddlTipoFaturamentoImpoFree.SelectedValue = 2 And txtQtdFaturamentoImpoFree.Text = 0) Then
            msgErro.Text = "Informe a quantidade de dias de faturamento na aba de Inf. Adicionais!"
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf (ddlTipoFaturamentoExpoFCL.SelectedValue = 2 And txtQtdFaturamentoExpoFCL.Text = 0) Or (ddlTipoFaturamentoExpoLCL.SelectedValue = 2 And txtQtdFaturamentoExpoLCL.Text = 0) Or (ddlTipoFaturamentoExpoAereo.SelectedValue = 2 And txtQtdFaturamentoExpoAereo.Text = 0) Or (ddlTipoFaturamentoExpoFree.SelectedValue = 2 And txtQtdFaturamentoExpoFree.Text = 0) Then
            msgErro.Text = "Informe a quantidade de dias de faturamento na aba de Inf. Adicionais!"
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf txtInscEstadual.Text = "" And ddlTipoPessoa.SelectedValue = 1 Then
            msgErro.Text = "Preencha todos os campos obrigatórios."
            divmsg1.Visible = True
            msgErro.Visible = True



        Else

            If txtCDIATA.Text <> "" And ddlTipoPessoa.SelectedValue = 3 And ddlPais.SelectedValue <> 0 Then
                Dim dspais As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_PAIS,0)ID_PAIS FROM TB_PAIS WHERE CD_IATA_PAIS='" & txtCDIATA.Text & "'")
                If dspais.Tables(0).Rows.Count > 0 Then
                    If ddlPais.SelectedValue <> dspais.Tables(0).Rows(0).Item("ID_PAIS").ToString() Then
                        msgErro.Text = "Codigo IATA não corresponde ao do pais selecionado"
                        divmsg1.Visible = True
                        msgErro.Visible = True
                        Exit Sub
                    End If
                End If
            End If

            If ddlTipoPessoa.SelectedValue <> 3 And ddlPais.SelectedValue <> 0 And ddlCidade.SelectedValue <> 0 Then
                Dim dspais As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_PAIS,0)ID_PAIS FROM TB_PAIS WHERE ID_PAIS=(SELECT ISNULL(ID_PAIS,0)ID_PAIS FROM TB_CIDADE WHERE ID_CIDADE = " & ddlCidade.SelectedValue & ")")
                If dspais.Tables(0).Rows.Count > 0 Then
                    If ddlPais.SelectedValue <> dspais.Tables(0).Rows(0).Item("ID_PAIS").ToString() Then
                        msgErro.Text = "Cidade não corresponde ao do pais selecionado"
                        divmsg1.Visible = True
                        msgErro.Visible = True
                        Exit Sub
                    End If
                End If
            End If

            If ddlTipoPessoa.SelectedValue = 1 And ValidaInscricao(UF, txtInscEstadual.Text) = False Then
                lblInformacao.Text = "Averigue a Inscrição Estadual!"
                divInformativa.Visible = True
            End If
            Dim AliquotaISS = txtAliquotaISS.Text
            If AliquotaISS = "" Then
                AliquotaISS = "0"
            Else
                AliquotaISS = AliquotaISS.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim AliquotaPIS = txtAliquotaPIS.Text
            If AliquotaPIS = "" Then
                AliquotaPIS = "0"
            Else
                AliquotaPIS = AliquotaPIS.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim AliquotaCOFINS = txtAliquotaCOFINS.Text
            If AliquotaCOFINS = "" Then
                AliquotaCOFINS = "0"
            Else
                AliquotaCOFINS = AliquotaCOFINS.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
            End If



            Dim SpreadImpoFCL = txtSpreadImpoFCL.Text
            If SpreadImpoFCL = "" Then
                SpreadImpoFCL = "0"
            Else
                SpreadImpoFCL = SpreadImpoFCL.Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim SpreadImpoLCL = txtSpreadImpoLCL.Text
            If SpreadImpoLCL = "" Then
                SpreadImpoLCL = "0"
            Else
                SpreadImpoLCL = SpreadImpoLCL.Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim SpreadImpoAereo = txtSpreadImpoAereo.Text
            If SpreadImpoAereo = "" Then
                SpreadImpoAereo = "0"
            Else
                SpreadImpoAereo = SpreadImpoAereo.Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim SpreadImpoFree = txtSpreadImpoFree.Text
            If SpreadImpoFree = "" Then
                SpreadImpoFree = "0"
            Else
                SpreadImpoFree = SpreadImpoFree.Replace(".", String.Empty).Replace(",", ".")
            End If




            Dim SpreadExpoFCL = txtSpreadExpoFCL.Text
            If SpreadExpoFCL = "" Then
                SpreadExpoFCL = "0"
            Else
                SpreadExpoFCL = SpreadExpoFCL.Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim SpreadExpoLCL = txtSpreadExpoLCL.Text
            If SpreadExpoLCL = "" Then
                SpreadExpoLCL = "0"
            Else
                SpreadExpoLCL = SpreadExpoLCL.Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim SpreadExpoAereo = txtSpreadExpoAereo.Text
            If SpreadExpoAereo = "" Then
                SpreadExpoAereo = "0"
            Else
                SpreadExpoAereo = SpreadExpoAereo.Replace(".", String.Empty).Replace(",", ".")
            End If

            Dim SpreadExpoFree = txtSpreadExpoFree.Text
            If SpreadExpoFree = "" Then
                SpreadExpoFree = "0"
            Else
                SpreadExpoFree = SpreadExpoFree.Replace(".", String.Empty).Replace(",", ".")
            End If




            If txtQtdFaturamentoImpoFCL.Text = "" Then
                txtQtdFaturamentoImpoFCL.Text = 0
            End If
            If txtQtdFaturamentoImpoLCL.Text = "" Then
                txtQtdFaturamentoImpoLCL.Text = 0
            End If
            If txtQtdFaturamentoImpoAereo.Text = "" Then
                txtQtdFaturamentoImpoAereo.Text = 0
            End If
            If txtQtdFaturamentoImpoFree.Text = "" Then
                txtQtdFaturamentoImpoFree.Text = 0
            End If


            If txtQtdFaturamentoExpoFCL.Text = "" Then
                txtQtdFaturamentoExpoFCL.Text = 0
            End If
            If txtQtdFaturamentoExpoLCL.Text = "" Then
                txtQtdFaturamentoExpoLCL.Text = 0
            End If
            If txtQtdFaturamentoExpoAereo.Text = "" Then
                txtQtdFaturamentoExpoAereo.Text = 0
            End If
            If txtQtdFaturamentoExpoFree.Text = "" Then
                txtQtdFaturamentoExpoFree.Text = 0
            End If


            Dim Razao = txtRazaoSocial.Text
            Razao = Razao.Replace("'", "''")

            Dim Fantasia = txtNomeFantasia.Text
            If Fantasia = "" Then
                Fantasia = "NULL"
            Else
                Fantasia = Fantasia.Replace("'", "''")
                Fantasia = "'" & Fantasia & "'"
            End If

            Dim InscEstadual = txtInscEstadual.Text
            If InscEstadual = "" Then
                InscEstadual = "NULL"
            Else
                InscEstadual = InscEstadual.Replace(".", String.Empty)
                InscEstadual = InscEstadual.Replace(" ", String.Empty)
                InscEstadual = InscEstadual.Replace("-", String.Empty)
                InscEstadual = InscEstadual.Replace("/", String.Empty)
                InscEstadual = "'" & InscEstadual & "'"
            End If

            Dim InscMunicipal = txtInscMunicipal.Text
            If InscMunicipal = "" Then
                InscMunicipal = "NULL"
            Else
                InscMunicipal = "'" & InscMunicipal & "'"
            End If

            Dim CEP = txtCEP.Text
            If CEP = "" Then
                CEP = "NULL"
            Else
                CEP = "'" & CEP & "'"
            End If

            Dim Bairro = txtBairro.Text
            If Bairro = "" Then
                Bairro = "NULL"
            Else
                Bairro = "'" & Bairro & "'"
            End If

            Dim Endereco = txtEndereco.Text
            If Endereco = "" Then
                Endereco = "NULL"
            Else
                Endereco = "'" & Endereco & "'"
            End If

            Dim Numero = txtNumero.Text
            If Numero = "" Then
                Numero = "NULL"
            Else
                Numero = "'" & Numero & "'"
            End If

            Dim CDIATA = txtCDIATA.Text
            If CDIATA = "" Then
                CDIATA = "NULL"
            Else
                CDIATA = "'" & CDIATA & "'"
            End If

            Dim Telefone = txtTelefone.Text
            If Telefone = "" Then
                Telefone = "NULL"
            Else
                Telefone = "'" & Telefone & "'"
            End If

            Dim Complemento = txtComplemento.Text
            If Complemento = "" Then
                Complemento = "NULL"
            Else
                Complemento = "'" & Complemento & "'"
            End If

            Dim OBSComplementares = txtOBSComplementares.Text
            If OBSComplementares = "" Then
                OBSComplementares = "NULL"
            Else
                OBSComplementares = "'" & OBSComplementares & "'"
            End If

            Dim EmailParceiro = txtEmailParceiro.Text
            If EmailParceiro = "" Then
                EmailParceiro = "NULL "
            Else
                EmailParceiro = "'" & EmailParceiro & "'"
            End If

            Dim EmailNF = txtEmailNF.Text
            If EmailNF = "" Then
                EmailNF = "NULL "
            Else
                EmailNF = "'" & EmailNF & "'"
            End If

            If txtID.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para cadastrar."
                Else
                    ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "' AND FL_ATIVO = 1")
                    If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                        msgErro.Text = "Já existe Parceiro com este CNPJ"
                        divmsg1.Visible = True
                        msgErro.Visible = True
                    Else
                        ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "' AND FL_ATIVO = 1")
                        If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                            msgErro.Text = "Já existe Parceiro com este CPF"
                            divmsg1.Visible = True
                            msgErro.Visible = True
                        Else

                            Con.Conectar()

                            Dim SQL As String = "INSERT INTO [dbo].[TB_PARCEIRO] (
            FL_IMPORTADOR, 
            FL_EXPORTADOR, 
            FL_AGENTE, 
            FL_AGENTE_INTERNACIONAL, 
            FL_TRANSPORTADOR, 
            FL_COMISSARIA, 
            FL_VENDEDOR, 
            FL_ARMAZEM_ATRACACAO,
            FL_ARMAZEM_DESEMBARACO,
            FL_ARMAZEM_DESCARGA, 
            FL_PRESTADOR, 
            NM_RAZAO, 
            NM_FANTASIA,
            CNPJ, 
            CPF, 
            TP_PESSOA, 
            INSCR_ESTADUAL, 
            INSCR_MUNICIPAL, 
            ENDERECO,
            NR_ENDERECO,
            BAIRRO,
            COMPL_ENDERECO,
            OB_COMPLEMENTARES,
            ID_CIDADE,
            TELEFONE,
            CEP,
            ID_VENDEDOR,
            FL_ISENTO_ISS,
            FL_ISENTO_PIS,
            FL_ISENTO_COFINS,
            VL_ALIQUOTA_ISS,
            VL_ALIQUOTA_PIS,
            VL_ALIQUOTA_COFINS,
            EMAIL_NF_ELETRONICA,
            CD_IATA,
            FL_SIMPLES_NACIONAL,
            FL_ATIVO,
            FL_INDICADOR,
            

            ID_TIPO_FATURAMENTO_EXPO_FREE,
            ID_TIPO_FATURAMENTO_EXPO_AEREO,
            ID_TIPO_FATURAMENTO_EXPO_LCL,
            ID_TIPO_FATURAMENTO_EXPO_FCL,
         
            ID_TIPO_FATURAMENTO_IMPO_FREE,
            ID_TIPO_FATURAMENTO_IMPO_AEREO,
            ID_TIPO_FATURAMENTO_IMPO_LCL,
            ID_TIPO_FATURAMENTO_IMPO_FCL,
          
            QT_DIAS_FATURAMENTO_EXPO_FREE,
            QT_DIAS_FATURAMENTO_EXPO_AEREO,
            QT_DIAS_FATURAMENTO_EXPO_LCL,
            QT_DIAS_FATURAMENTO_EXPO_FCL,
           
            QT_DIAS_FATURAMENTO_IMPO_FREE,
            QT_DIAS_FATURAMENTO_IMPO_AEREO,
            QT_DIAS_FATURAMENTO_IMPO_LCL,
            QT_DIAS_FATURAMENTO_IMPO_FCL,
           
            ID_ACORDO_CAMBIO_EXPO_FREE,
            ID_ACORDO_CAMBIO_EXPO_AEREO,
            ID_ACORDO_CAMBIO_EXPO_LCL,
            ID_ACORDO_CAMBIO_EXPO_FCL,
           
            ID_ACORDO_CAMBIO_IMPO_FREE,
            ID_ACORDO_CAMBIO_IMPO_AEREO,
            ID_ACORDO_CAMBIO_IMPO_LCL,
            ID_ACORDO_CAMBIO_IMPO_FCL,
           
            SPREAD_EXPO_FREEHAND,
            SPREAD_EXPO_AEREO,
            SPREAD_EXPO_LCL,
            SPREAD_EXPO_FCL,
           
            SPREAD_IMPO_FREEHAND,
            SPREAD_IMPO_AEREO,
            SPREAD_IMPO_LCL,
            SPREAD_IMPO_FCL,


            EMAIL,
            FL_VENDEDOR_DIRETO,
            FL_EQUIPE_INSIDE_SALES,
            FL_SHIPPER,
            FL_CNEE,
            FL_RODOVIARIO,
            REGRA_ATUALIZACAO,
            ID_PAIS,
            ID_USUARIO,
            CREATED_AT,
            FL_PROSPECCAO,
            ID_VENDEDOR_PROSPECCAO,
            FL_INDICACAO_INTERNA,
            ID_PARCEIRO_INDICACAO_INTERNA
            ) 
            VALUES ( 
            '" & ckbImportador.Checked & "',
            '" & ckbExportador.Checked & "',
            '" & ckbAgente.Checked & "', 
            '" & ckbAgenteInternacional.Checked & "',
            '" & ckbTransportador.Checked & "',
            '" & ckbComissaria.Checked & "',
            '" & ckbVendedor.Checked & "',
            '" & ckbArmazemAtracacao.Checked & "',
            '" & ckbArmazemDesembaraco.Checked & "',
            '" & ckbArmazemDescarga.Checked & "',
            '" & ckbPrestador.Checked & "', 
            '" & Razao & "',
            " & Fantasia & ", "

                            ''CNPJ E CPF
                            If ddlTipoPessoa.SelectedValue = 1 Or ddlTipoPessoa.SelectedValue = 3 Then
                                SQL = SQL & "'" & txtCNPJ.Text & "', NULL ,"
                            Else

                                SQL = SQL & " NULL, '" & txtCPF.Text & "',"
                            End If

                            SQL = SQL & ddlTipoPessoa.SelectedValue & ",
            " & InscEstadual & ",
            " & InscMunicipal & ",
            " & Endereco & ",
            " & Numero & ",
            " & Bairro & ",
            " & Complemento & ",
            " & OBSComplementares & ",
            " & ddlCidade.SelectedValue & ",
            " & Telefone & ",
            " & CEP & ",
            " & ddlVendedor.SelectedValue & ",
            '" & ckbISS.Checked & "',
            '" & ckbPIS.Checked & "',
            '" & ckbCOFINS.Checked & "',
            '" & AliquotaISS & "',
            '" & AliquotaPIS & "',
            '" & AliquotaCOFINS & "',
            " & EmailNF & ",
            " & CDIATA & ",
            '" & ckbSimplesNacional.Checked & "',
            '" & ckbAtivo.Checked & "',
            '" & ckbIndicador.Checked & "',
           

            " & ddlTipoFaturamentoExpoFree.SelectedValue & ",
            " & ddlTipoFaturamentoExpoAereo.SelectedValue & ",
            " & ddlTipoFaturamentoExpoLCL.SelectedValue & ",
            " & ddlTipoFaturamentoExpoFCL.SelectedValue & ",
         
            " & ddlTipoFaturamentoImpoFree.SelectedValue & ",
            " & ddlTipoFaturamentoImpoAereo.SelectedValue & ",
            " & ddlTipoFaturamentoImpoLCL.SelectedValue & ",
            " & ddlTipoFaturamentoImpoFCL.SelectedValue & ",
          
            " & txtQtdFaturamentoExpoFree.Text & ",
            " & txtQtdFaturamentoExpoAereo.Text & ",
            " & txtQtdFaturamentoExpoLCL.Text & ",
            " & txtQtdFaturamentoExpoFCL.Text & ",
           
            " & txtQtdFaturamentoImpoFree.Text & ",
            " & txtQtdFaturamentoImpoAereo.Text & ",
            " & txtQtdFaturamentoImpoLCL.Text & ",
            " & txtQtdFaturamentoImpoFCL.Text & ",
           
            " & ddlAcordoCambioExpoFree.SelectedValue & ",
            " & ddlAcordoCambioExpoAereo.SelectedValue & ",
            " & ddlAcordoCambioExpoLCL.SelectedValue & ",
            " & ddlAcordoCambioExpoFCL.SelectedValue & ",
           
            " & ddlAcordoCambioImpoFree.SelectedValue & ",
            " & ddlAcordoCambioImpoAereo.SelectedValue & ",
            " & ddlAcordoCambioImpoLCL.SelectedValue & ",
            " & ddlAcordoCambioImpoFCL.SelectedValue & ",
           
            '" & SpreadExpoFree & "',
            '" & SpreadExpoAereo & "',
            '" & SpreadExpoLCL & "',
            '" & SpreadExpoFCL & "',
           
            '" & SpreadImpoFree & "',
            '" & SpreadImpoAereo & "',
            '" & SpreadImpoLCL & "',
            '" & SpreadImpoFCL & "',


            " & EmailParceiro & ",
            '" & ckbVendedorDireto.Checked & "',
            '" & ckbEquipeInsideSales.Checked & "',
            '" & ckbShipper.Checked & "',
            '" & ckbCNEE.Checked & "',
            '" & ckbTranspRodoviario.Checked & "',
            " & ddlRegraAtualizacao.SelectedValue & ",
            " & ddlPais.SelectedValue & " ,
            " & Session("ID_USUARIO") & ",
             getdate() ,
            '" & ckbProspeccao.Checked & "',
            " & ddlTimeProspeccao.SelectedValue & ",
            '" & ckbIndicacaoInterna.Checked & "',
            " & ddlIndicacaoInterna.SelectedValue & " 
            ) Select SCOPE_IDENTITY() as ID_PARCEIRO "


                            ds = Con.ExecutarQuery(SQL)
                            Dim ID_Parceiro As String = ds.Tables(0).Rows(0).Item("ID_PARCEIRO").ToString()
                            Session("ID_Parceiro") = ID_Parceiro

                            txtID.Text = ID_Parceiro


                            divmsg.Visible = True

                            If ckbAgenteInternacional.Checked = True Then
                                mpeDadosBancarios.Show()
                            Else
                                mpeTaxas.Show()
                            End If


                        End If
                    End If

                End If


            Else

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para alterar."
                Else
                    ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "' AND FL_ATIVO = 1 AND ID_PARCEIRO <> " & txtID.Text)
                    If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue = 1 Then
                        msgErro.Text = "Já existe Parceiro com este CNPJ"
                        divmsg1.Visible = True
                        msgErro.Visible = True
                    Else
                        ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "' AND FL_ATIVO = 1 AND ID_PARCEIRO <> " & txtID.Text)
                        If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue = 2 Then
                            msgErro.Text = "Já existe Parceiro com este CPF"
                            divmsg1.Visible = True
                            msgErro.Visible = True
                        Else


                            Dim ID As String = txtID.Text
                            Con.Conectar()

                            Dim SQL As String = "UPDATE [dbo].[TB_PARCEIRO] SET FL_IMPORTADOR = '" & ckbImportador.Checked & "',
            FL_EXPORTADOR = '" & ckbExportador.Checked & "',
            FL_AGENTE = '" & ckbAgente.Checked & "' ,
            FL_AGENTE_INTERNACIONAL= '" & ckbAgenteInternacional.Checked & "',
            FL_TRANSPORTADOR= '" & ckbTransportador.Checked & "',
            FL_COMISSARIA ='" & ckbComissaria.Checked & "',
            FL_VENDEDOR='" & ckbVendedor.Checked & "',
            FL_ARMAZEM_DESCARGA = '" & ckbArmazemDescarga.Checked & "',
            FL_ARMAZEM_DESEMBARACO = '" & ckbArmazemDesembaraco.Checked & "',
            FL_ARMAZEM_ATRACACAO= '" & ckbArmazemAtracacao.Checked & "',
            FL_PRESTADOR= '" & ckbPrestador.Checked & "',
            NM_RAZAO= '" & Razao & "',
            NM_FANTASIA = " & Fantasia & ", 
            TP_PESSOA= '" & ddlTipoPessoa.SelectedValue & "',"
                            ''CNPJ E CPF
                            If ddlTipoPessoa.SelectedValue = 1 Or ddlTipoPessoa.SelectedValue = 3 Then
                                SQL = SQL & " CNPJ = '" & txtCNPJ.Text & "',CPF = NULL ,"
                            Else

                                SQL = SQL & " CNPJ = NULL, CPF = '" & txtCPF.Text & "',"
                            End If

                            SQL = SQL & "INSCR_ESTADUAL=" & InscEstadual & ",
            INSCR_MUNICIPAL= " & InscMunicipal & ",
            ENDERECO=" & Endereco & ",
            NR_ENDERECO=" & Numero & ",
            BAIRRO=" & Bairro & ",
            COMPL_ENDERECO=" & Complemento & ",
            OB_COMPLEMENTARES=" & OBSComplementares & ",
            ID_CIDADE=" & ddlCidade.SelectedValue & ",
            TELEFONE=" & Telefone & ",
            CEP=" & CEP & ",
            ID_VENDEDOR=" & ddlVendedor.SelectedValue & ",
            FL_ISENTO_ISS='" & ckbISS.Checked & "',
            FL_ISENTO_PIS='" & ckbPIS.Checked & "',
            FL_ISENTO_COFINS='" & ckbCOFINS.Checked & "',
            VL_ALIQUOTA_ISS='" & AliquotaISS & "',
            VL_ALIQUOTA_PIS= '" & AliquotaPIS & "',
            VL_ALIQUOTA_COFINS='" & AliquotaCOFINS & "',
            EMAIL_NF_ELETRONICA=" & EmailNF & ",
            CD_IATA=" & CDIATA & ",
            FL_SIMPLES_NACIONAL='" & ckbSimplesNacional.Checked & "',
            FL_ATIVO = '" & ckbAtivo.Checked & "', 
            FL_INDICADOR = '" & ckbIndicador.Checked & "',
          

            ID_TIPO_FATURAMENTO_EXPO_FREE = " & ddlTipoFaturamentoExpoFree.SelectedValue & ",
            ID_TIPO_FATURAMENTO_EXPO_AEREO = " & ddlTipoFaturamentoExpoAereo.SelectedValue & ",
            ID_TIPO_FATURAMENTO_EXPO_LCL = " & ddlTipoFaturamentoExpoLCL.SelectedValue & ",
            ID_TIPO_FATURAMENTO_EXPO_FCL = " & ddlTipoFaturamentoExpoFCL.SelectedValue & ",
         
            ID_TIPO_FATURAMENTO_IMPO_FREE = " & ddlTipoFaturamentoImpoFree.SelectedValue & ",
            ID_TIPO_FATURAMENTO_IMPO_AEREO = " & ddlTipoFaturamentoImpoAereo.SelectedValue & ",
            ID_TIPO_FATURAMENTO_IMPO_LCL = " & ddlTipoFaturamentoImpoLCL.SelectedValue & ",
            ID_TIPO_FATURAMENTO_IMPO_FCL = " & ddlTipoFaturamentoImpoFCL.SelectedValue & ",
          
            QT_DIAS_FATURAMENTO_EXPO_FREE = " & txtQtdFaturamentoExpoFree.Text & ",
            QT_DIAS_FATURAMENTO_EXPO_AEREO = " & txtQtdFaturamentoExpoAereo.Text & ",
            QT_DIAS_FATURAMENTO_EXPO_LCL = " & txtQtdFaturamentoExpoLCL.Text & ",
            QT_DIAS_FATURAMENTO_EXPO_FCL = " & txtQtdFaturamentoExpoFCL.Text & ",
           
            QT_DIAS_FATURAMENTO_IMPO_FREE = " & txtQtdFaturamentoImpoFree.Text & ",
            QT_DIAS_FATURAMENTO_IMPO_AEREO = " & txtQtdFaturamentoImpoAereo.Text & ",
            QT_DIAS_FATURAMENTO_IMPO_LCL = " & txtQtdFaturamentoImpoLCL.Text & ",
            QT_DIAS_FATURAMENTO_IMPO_FCL = " & txtQtdFaturamentoImpoFCL.Text & ",
           
            ID_ACORDO_CAMBIO_EXPO_FREE = " & ddlAcordoCambioExpoFree.SelectedValue & ",
            ID_ACORDO_CAMBIO_EXPO_AEREO = " & ddlAcordoCambioExpoAereo.SelectedValue & ",
            ID_ACORDO_CAMBIO_EXPO_LCL = " & ddlAcordoCambioExpoLCL.SelectedValue & ",
            ID_ACORDO_CAMBIO_EXPO_FCL = " & ddlAcordoCambioExpoFCL.SelectedValue & ",
           
            ID_ACORDO_CAMBIO_IMPO_FREE = " & ddlAcordoCambioImpoFree.SelectedValue & ",
            ID_ACORDO_CAMBIO_IMPO_AEREO = " & ddlAcordoCambioImpoAereo.SelectedValue & ",
            ID_ACORDO_CAMBIO_IMPO_LCL = " & ddlAcordoCambioImpoLCL.SelectedValue & ",
            ID_ACORDO_CAMBIO_IMPO_FCL = " & ddlAcordoCambioImpoFCL.SelectedValue & ",
           
            SPREAD_EXPO_FREEHAND = '" & SpreadExpoFree & "',
            SPREAD_EXPO_AEREO = '" & SpreadExpoAereo & "',
            SPREAD_EXPO_LCL = '" & SpreadExpoLCL & "',
            SPREAD_EXPO_FCL = '" & SpreadExpoFCL & "',
           
            SPREAD_IMPO_FREEHAND = '" & SpreadImpoFree & "',
            SPREAD_IMPO_AEREO = '" & SpreadImpoAereo & "',
            SPREAD_IMPO_LCL = '" & SpreadImpoLCL & "',
            SPREAD_IMPO_FCL	= '" & SpreadImpoFCL & "',

            EMAIL =  " & EmailParceiro & ",
            FL_EQUIPE_INSIDE_SALES ='" & ckbEquipeInsideSales.Checked & "',
            FL_VENDEDOR_DIRETO ='" & ckbVendedorDireto.Checked & "',
            FL_SHIPPER = '" & ckbShipper.Checked & "',
            FL_CNEE = '" & ckbCNEE.Checked & "',
            FL_RODOVIARIO = '" & ckbTranspRodoviario.Checked & "',
            REGRA_ATUALIZACAO = " & ddlRegraAtualizacao.SelectedValue & " ,
            ID_PAIS=" & ddlPais.SelectedValue & ",
            ID_USUARIO = " & Session("ID_USUARIO") & ",
            UPDATED_AT = GETDATE() , 
            FL_PROSPECCAO =  '" & ckbProspeccao.Checked & "',
            ID_VENDEDOR_PROSPECCAO =  " & ddlTimeProspeccao.SelectedValue & ",
            FL_INDICACAO_INTERNA=  '" & ckbIndicacaoInterna.Checked & "',
            ID_PARCEIRO_INDICACAO_INTERNA =  " & ddlIndicacaoInterna.SelectedValue & " 
            WHERE ID_PARCEIRO = " & ID

                            Session("ID_Parceiro") = ID

                            Con.ExecutarQuery(SQL)

                            Con.ExecutarQuery("update F 
                            set F.COMPL_ENDERECO = P.COMPL_ENDERECO,
                            F.ENDERECO = P.ENDERECO,
                            F.BAIRRO = P.BAIRRO,
                            F.NR_ENDERECO = P.NR_ENDERECO,
                            F.CEP = P.CEP
                            FROM TB_FATURAMENTO F
                            INNER JOIN TB_PARCEIRO P on P.ID_PARCEIRO = F.ID_PARCEIRO_CLIENTE
                            WHERE F.NR_RPS IS NULL AND F.NR_NOTA_FISCAL IS NULL AND P.ID_PARCEIRO = " & ID)

                            dgvContato.DataBind()
                            dgvEmailEvento.DataBind()
                            Con.Fechar()
                            divmsg.Visible = True
                            If ckbAgenteInternacional.Checked = True Then
                                mpeDadosBancarios.Show()
                            Else
                                mpeTaxas.Show()
                            End If
                        End If
                    End If

                End If

            End If

        End If

    End Sub

    Function ValidaInscricao(UF As String, Inscr As String)
        Dim Con As New Conexao_sql

        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT [dbo].[fncValida_Inscricao_Estadual_Geral]('" & UF & "', '" & Inscr & "')Inscricao_Valida")
        If ds.Tables(0).Rows(0).Item("Inscricao_Valida") = True Then

            Return True
        Else
            Return False

        End If
        Con.Fechar()

    End Function
    Public Sub Limpar(ByVal controlP As Control)
        Dim ctl As Control

        For Each ctl In controlP.Controls

            If TypeOf ctl Is TextBox Then

                DirectCast(ctl, TextBox).Text = String.Empty

            ElseIf ctl.Controls.Count > 0 Then

                Limpar(ctl)

            End If

        Next

    End Sub
    Private Sub ddlTipoPessoa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoPessoa.SelectedIndexChanged
        If ddlTipoPessoa.SelectedValue = 2 Then 'Fisica
            divIATA.Attributes.CssStyle.Add("display", "none")
            divCPF.Visible = True
            divCNPJ.Visible = False
            txtCPF.Enabled = True
            txtCPF.Text = ""
            txtCDIATA.Text = ""
            btnConsultaCNPJ.Visible = False
            RedCidade.Visible = True
            RedBairro.Visible = True
            RedNum.Visible = True
            RedEnd.Visible = True
            RedCEP.Visible = True
            txtTelefone.CssClass = "form-control telefone"
            txtTelContato.CssClass = "form-control telefone"
            txtCelularContato.CssClass = "form-control celular"

        ElseIf ddlTipoPessoa.SelectedValue = 1 Then 'Juridico
            divIATA.Attributes.CssStyle.Add("display", "none")
            divCPF.Visible = False
            divCNPJ.Visible = True
            txtCNPJ.Text = ""
            txtCDIATA.Text = ""
            btnConsultaCNPJ.Visible = True
            RedCidade.Visible = True
            RedBairro.Visible = True
            RedNum.Visible = True
            RedEnd.Visible = True
            RedCEP.Visible = True
            txtTelefone.CssClass = "form-control telefone"
            txtTelContato.CssClass = "form-control telefone"
            txtCelularContato.CssClass = "form-control celular"

        ElseIf ddlTipoPessoa.SelectedValue = 3 Then 'Estrangeira
            divIATA.Attributes.CssStyle.Add("display", "block")
            divCPF.Visible = False
            divCNPJ.Visible = True
            txtCNPJ.Text = "00000000000000"
            btnConsultaCNPJ.Visible = False
            RedCidade.Visible = False
            RedBairro.Visible = False
            RedNum.Visible = False
            RedEnd.Visible = False
            RedCEP.Visible = False
            txtTelefone.CssClass = "form-control"
            txtTelContato.CssClass = "form-control"
            txtCelularContato.CssClass = "form-control"

        Else
            divCPF.Visible = False
            divCNPJ.Visible = False
            btnConsultaCNPJ.Visible = False
            txtTelefone.CssClass = "form-control telefone"
            txtTelContato.CssClass = "form-control telefone"
            txtCelularContato.CssClass = "form-control celular"
        End If
    End Sub

    Private Sub ddlTipoPessoa_PreRender(sender As Object, e As EventArgs) Handles ddlTipoPessoa.PreRender
        If ddlTipoPessoa.SelectedValue = 2 Then 'Fisica
            divCPF.Visible = True
            divCNPJ.Visible = False
            btnConsultaCNPJ.Visible = False
            txtTelefone.CssClass = "form-control telefone"
            txtTelContato.CssClass = "form-control telefone"
            txtCelularContato.CssClass = "form-control celular"

        ElseIf ddlTipoPessoa.SelectedValue = 1 Then 'Juridico
            divCPF.Visible = False
            divCNPJ.Visible = True
            btnConsultaCNPJ.Visible = True
            txtTelefone.CssClass = "form-control telefone"
            txtTelContato.CssClass = "form-control telefone"
            txtCelularContato.CssClass = "form-control celular"

        ElseIf ddlTipoPessoa.SelectedValue = 3 Then 'Estrangeira
            divCPF.Visible = False
            divCNPJ.Visible = True
            btnConsultaCNPJ.Visible = False
            txtTelefone.CssClass = "form-control"
            txtTelContato.CssClass = "form-control"
            txtCelularContato.CssClass = "form-control"

        Else
            divCPF.Visible = False
            divCNPJ.Visible = False
            btnConsultaCNPJ.Visible = False
            txtTelefone.CssClass = "form-control telefone"
            txtTelContato.CssClass = "form-control telefone"
            txtCelularContato.CssClass = "form-control celular"
        End If

    End Sub

    Protected Sub dgvEmailEvento_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvEmailEvento.DataSource = Session("TaskTable")
            dgvEmailEvento.DataBind()
            dgvEmailEvento.HeaderRow.TableSection = TableRowSection.TableHeader
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

    Private Sub ckbComissaria_CheckedChanged(sender As Object, e As EventArgs) Handles ckbComissaria.CheckedChanged
        VerificaChecks()
    End Sub

    Private Sub ckbExportador_CheckedChanged(sender As Object, e As EventArgs) Handles ckbExportador.CheckedChanged
        VerificaChecks()
    End Sub

    Private Sub ckbImportador_CheckedChanged(sender As Object, e As EventArgs) Handles ckbImportador.CheckedChanged
        VerificaChecks()
    End Sub

    Private Sub ckbAgente_CheckedChanged(sender As Object, e As EventArgs) Handles ckbAgente.CheckedChanged
        VerificaChecks()
    End Sub
    Sub VerificaChecks()
        If ckbAgente.Checked = True Or ckbImportador.Checked = True Or ckbExportador.Checked = True Or ckbComissaria.Checked = True Then
            'divVendedor.Visible = True
            divVendedor.Attributes.CssStyle.Add("display", "block")
            lblRed.Visible = True
            lblRed2.Visible = True

        Else
            'divVendedor.Visible = False
            divVendedor.Attributes.CssStyle.Add("display", "none")
            lblRed.Visible = False
            lblRed2.Visible = False
        End If
    End Sub

    Private Sub btnSim_Click(sender As Object, e As EventArgs) Handles btnSim.Click
        If ckbTransportador.Checked = True Then
            Response.Redirect("TaxasLocaisArmador.aspx?id=" & Session("ID_Parceiro"))
        Else
            Response.Redirect("TaxaParceiro.aspx?id=" & Session("ID_Parceiro"))

        End If

    End Sub


    Function SeparaEmail(ByVal email As String) As Boolean
        Dim erro As Boolean
        'quebrar a string
        Dim palavras As String() = email.Split(New String() _
          {";"}, StringSplitOptions.RemoveEmptyEntries)

        'exibe o resultado
        For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
            If ValidaEmail.Validar(palavras(i)) = False Then
                erro = False
                Return erro

                Exit Function

            Else
                erro = True

            End If

        Next
        Return erro
    End Function

    Private Sub btnSimDadosBancarios_Click(sender As Object, e As EventArgs) Handles btnSimDadosBancarios.Click
        Response.Redirect("DadosBancariosAgente.aspx?id=" & Session("ID_Parceiro"))
    End Sub

    Private Sub dgvContato_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvContato.RowCommand
        divSuccessContato.Visible = False
        divErroContato.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Editar" Then
            Dim ds As DataSet = Con.ExecutarQuery("SELECT  A.ID_CONTATO as Id, A.NM_CONTATO,A.TELEFONE_CONTATO,A.CELULAR_CONTATO,A.NM_DEPARTAMENTO,A.EMAIL_CONTATO FROM [dbo].[TB_CONTATO] A WHERE A.ID_CONTATO = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtIDContato.Text = ID
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_CONTATO")) Then
                    txtNomeContato.Text = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TELEFONE_CONTATO")) Then
                    txtTelContato.Text = ds.Tables(0).Rows(0).Item("TELEFONE_CONTATO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CELULAR_CONTATO")) Then
                    txtCelularContato.Text = ds.Tables(0).Rows(0).Item("CELULAR_CONTATO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMAIL_CONTATO")) Then
                    txtEmailContato.Text = ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NM_DEPARTAMENTO")) Then
                    txtDepartamento.Text = ds.Tables(0).Rows(0).Item("NM_DEPARTAMENTO").ToString()
                End If
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "EditaContato()", True)
                btnSalvarContato.Visible = True
            End If
        End If

    End Sub

    Private Sub btnSalvarContato_Click(sender As Object, e As EventArgs) Handles btnSalvarContato.Click
        divSuccessContato.Visible = False
        divErroContato.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtID.Text = "" Then
            'ERRO
            divErroContato.Visible = True
            lblErroContato.Text = "Necessário cadastrar dados basicos e informações adicionais antes de inserir um contato!"

        ElseIf txtNomeContato.Text = "" Then
            'ERRO
            divErroContato.Visible = True
            lblErroContato.Text = "Obrigatorio o preenchimento de nome do contato!"

        Else

            If txtIDContato.Text = "" Then
                Dim sqlContatos As String = "INSERT INTO TB_CONTATO ([ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO]) VALUES (" & txtID.Text & ",'" & txtNomeContato.Text & "'"
                If txtTelContato.Text = "" Then
                    sqlContatos = sqlContatos & ", NULL"
                Else
                    sqlContatos = sqlContatos & ", '" & txtTelContato.Text & "'"
                End If

                If txtEmailContato.Text = "" Then
                    sqlContatos = sqlContatos & ", NULL"
                Else
                    sqlContatos = sqlContatos & ", '" & txtEmailContato.Text & "'"
                End If

                If txtDepartamento.Text = "" Then
                    sqlContatos = sqlContatos & ", NULL"
                Else
                    sqlContatos = sqlContatos & ", '" & txtDepartamento.Text & "'"
                End If

                If txtCelularContato.Text = "" Then
                    sqlContatos = sqlContatos & ", NULL"
                Else
                    sqlContatos = sqlContatos & ", '" & txtCelularContato.Text & "'"
                End If

                sqlContatos = sqlContatos & ")"

                Con.ExecutarQuery(sqlContatos)
                divSuccessContato.Visible = True
                lblSuccessContato.Text = "Contato cadastrado com sucesso!"
                txtIDContato.Text = ""
                txtNomeContato.Text = ""
                txtTelContato.Text = ""
                txtCelularContato.Text = ""
                txtEmailContato.Text = ""
                txtDepartamento.Text = ""
                dgvContato.DataBind()
            Else

                Con.ExecutarQuery("UPDATE TB_CONTATO SET [NM_CONTATO] = '" & txtNomeContato.Text & "' WHERE ID_CONTATO = " & txtIDContato.Text)

                If txtTelContato.Text <> "" Then
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [TELEFONE_CONTATO] = '" & txtTelContato.Text & "'  WHERE ID_CONTATO = " & txtIDContato.Text)
                Else
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [TELEFONE_CONTATO] = NULL WHERE ID_CONTATO = " & txtIDContato.Text)
                End If

                If txtEmailContato.Text <> "" Then
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [EMAIL_CONTATO] = '" & txtEmailContato.Text & "'  where ID_CONTATO = " & txtIDContato.Text)
                Else
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [EMAIL_CONTATO] = NULL WHERE ID_CONTATO = " & txtIDContato.Text)
                End If

                If txtCelularContato.Text <> "" Then
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [CELULAR_CONTATO] = '" & txtCelularContato.Text & "'  where ID_CONTATO = " & txtIDContato.Text)
                Else
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [CELULAR_CONTATO] = NULL WHERE ID_CONTATO = " & txtIDContato.Text)
                End If

                If txtDepartamento.Text <> "" Then
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [NM_DEPARTAMENTO] = '" & txtDepartamento.Text & "'  where ID_CONTATO = " & txtIDContato.Text)
                Else
                    Con.ExecutarQuery("UPDATE TB_CONTATO SET [NM_DEPARTAMENTO] = NULL WHERE ID_CONTATO = " & txtIDContato.Text)
                End If
                divSuccessContato.Visible = True
                lblSuccessContato.Text = "Contato alterado com sucesso!"
                txtIDContato.Text = ""
                txtNomeContato.Text = ""
                txtTelContato.Text = ""
                txtCelularContato.Text = ""
                txtEmailContato.Text = ""
                txtDepartamento.Text = ""
                dgvContato.DataBind()

            End If
        End If
    End Sub

    Private Sub txtCNPJ_TextChanged(sender As Object, e As EventArgs) Handles txtCNPJ.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        divmsg1.Visible = False


        If ddlTipoPessoa.SelectedValue = 1 And ValidaCNPJ.Validar(txtCNPJ.Text) = False Then
            msgErro.Text = "CNPJ Inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "' AND FL_ATIVO = 1 AND TP_PESSOA <> 3")
            If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                msgErro.Text = "Já existe Parceiro com este CNPJ"
                divmsg1.Visible = True
                msgErro.Visible = True
            End If
        End If

    End Sub

    Private Sub txtCPF_TextChanged(sender As Object, e As EventArgs) Handles txtCPF.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        divmsg1.Visible = False

        If ddlTipoPessoa.SelectedValue = 2 And ValidaCPF.Validar(txtCPF.Text) = False Then
            msgErro.Text = "CPF Inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "' AND FL_ATIVO = 1")
            If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                msgErro.Text = "Já existe Parceiro com este CPF"
                divmsg1.Visible = True
                msgErro.Visible = True
            End If
        End If
    End Sub

    Private Sub txtCDIATA_TextChanged(sender As Object, e As EventArgs) Handles txtCDIATA.TextChanged
        If txtCDIATA.Text <> "" Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_PAIS,0)ID_PAIS FROM TB_PAIS WHERE CD_IATA_PAIS='" & txtCDIATA.Text & "'")
            If ds.Tables(0).Rows.Count > 0 Then
                ddlPais.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PAIS").ToString()
            End If
        End If
    End Sub

    Private Sub ddlCidade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCidade.SelectedIndexChanged
        If ddlCidade.SelectedValue > 0 Then
            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_PAIS,0)ID_PAIS FROM TB_PAIS WHERE ID_PAIS=(SELECT ISNULL(ID_PAIS,0)ID_PAIS FROM TB_CIDADE WHERE ID_CIDADE = " & ddlCidade.SelectedValue & ")")
            If ds.Tables(0).Rows.Count > 0 Then
                ddlPais.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PAIS").ToString()
            End If
        End If
    End Sub

    Private Sub btnConsultaCNPJ_Click(sender As Object, e As EventArgs) Handles btnConsultaCNPJ.Click
        If txtCNPJ.Text <> "" Then

            Try
                Using Buscar = New WsNVOCC.WsNvocc

                    Dim Resultado = Buscar.ConsultaCNPJ(txtCNPJ.Text.Replace("-", "").Replace(".", "").Replace("/", ""))
                    Dim dados As Root = JsonConvert.DeserializeObject(Of Root)(Resultado)
                    ' txtOBSComplementares.Text = Resultado

                    If dados.estabelecimento.situacao_cadastral = "Baixada" Then
                        divmsg1.Visible = True
                        msgErro.Text = "CNPJ INATIVO!"
                        Exit Sub

                    Else

                        If Not dados.razao_social Is Nothing Then
                            txtRazaoSocial.Text = dados.razao_social
                        Else
                            txtRazaoSocial.Text = ""
                        End If

                        If Not dados.estabelecimento.nome_fantasia Is Nothing Then
                            txtNomeFantasia.Text = dados.estabelecimento.nome_fantasia
                        ElseIf Not dados.razao_social Is Nothing Then
                            txtNomeFantasia.Text = dados.razao_social
                        Else
                            txtNomeFantasia.Text = ""
                        End If

                        If Not dados.estabelecimento.email Is Nothing Then
                            txtEmailParceiro.Text = dados.estabelecimento.email
                            txtEmailNF.Text = dados.estabelecimento.email
                        Else
                            txtEmailParceiro.Text = ""
                            txtEmailNF.Text = ""
                        End If

                        If Not dados.estabelecimento.telefone1 Is Nothing Then
                            txtTelefone.Text = dados.estabelecimento.ddd1 & dados.estabelecimento.telefone1
                        Else
                            txtTelefone.Text = ""
                        End If

                        If Not dados.estabelecimento.cep Is Nothing Then
                            txtCEP.Text = dados.estabelecimento.cep
                        Else
                            txtCEP.Text = ""
                        End If

                        If Not dados.estabelecimento.logradouro Is Nothing Then
                            txtEndereco.Text = dados.estabelecimento.tipo_logradouro & " " & dados.estabelecimento.logradouro
                        Else
                            txtEndereco.Text = ""
                        End If

                        If Not dados.estabelecimento.numero Is Nothing Then
                            txtNumero.Text = dados.estabelecimento.numero
                        Else
                            txtNumero.Text = ""
                        End If

                        If Not dados.estabelecimento.complemento Is Nothing Then
                            txtComplemento.Text = dados.estabelecimento.complemento
                        Else
                            txtComplemento.Text = ""
                        End If

                        If Not dados.estabelecimento.bairro Is Nothing Then
                            txtBairro.Text = dados.estabelecimento.bairro
                        Else
                            txtBairro.Text = ""
                        End If

                        If Not dados.estabelecimento.inscricoes_estaduais Is Nothing Then
                            txtInscEstadual.Text = ""
                            Dim listaInsc = dados.estabelecimento.inscricoes_estaduais
                            For Each Insc In listaInsc
                                If Insc.ativo = "true" Then
                                    txtInscEstadual.Text = Insc.inscricao_estadual
                                End If
                            Next
                            If txtInscEstadual.Text = "" Then
                                txtInscEstadual.Text = "ISENTO"
                            End If
                        Else
                            txtInscEstadual.Text = "ISENTO"
                        End If

                        If Not dados.estabelecimento.cidade.nome Is Nothing Then
                            Dim Con As New Conexao_sql
                            Con.Conectar()
                            Dim ds As DataSet = Con.ExecutarQuery("SELECT ISNULL(ID_PAIS,0)ID_PAIS, ISNULL(ID_CIDADE,0)ID_CIDADE FROM TB_CIDADE WHERE UPPER(NM_CIDADE) = UPPER([dbo].[fnTiraAcento]('" & dados.estabelecimento.cidade.nome & "'))")
                            If ds.Tables(0).Rows.Count > 0 Then
                                ddlCidade.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CIDADE").ToString()
                                ddlPais.SelectedValue = ds.Tables(0).Rows(0).Item("ID_PAIS").ToString()
                            Else
                                ddlCidade.SelectedValue = 0
                                ddlPais.SelectedValue = 0
                            End If
                        Else
                            ddlCidade.SelectedValue = 0
                            ddlPais.SelectedValue = 0
                        End If

                    End If
                End Using


            Catch ex As Exception

                divmsg1.Visible = True
                msgErro.Text = "Não foi possivel completar a ação: " & ex.Message
                Exit Sub

            End Try


        End If

    End Sub

    Private Sub dgvEmailEvento_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvEmailEvento.RowCommand
        divSuccessEvento.Visible = False
        divErroEvento.Visible = False
        Dim ID As String = e.CommandArgument
        Dim Con As New Conexao_sql
        Con.Conectar()
        If e.CommandName = "Editar" Then
            Dim ds As DataSet = Con.ExecutarQuery("
SELECT ID_PARCEIRO,NM_RAZAO,NM_FANTASIA,CNPJ,B.ID_TERMINAL,B.ID_EVENTO,B.ENDERECOS FROM [dbo].[TB_PARCEIRO] A
LEFT JOIN TB_AMR_PESSOA_EVENTO B ON B.ID_PESSOA = A.ID_PARCEIRO
WHERE B.ID = " & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtIdEvento.Text = ID

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ENDERECOS")) Then
                    txtEmail.Text = ds.Tables(0).Rows(0).Item("ENDERECOS").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_TERMINAL")) Then
                    ddlPorto.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TERMINAL").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ID_EVENTO")) Then
                    ddlEvento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EVENTO").ToString()
                End If

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "EditaEvento()", True)
                btnSalvarEvento.Visible = True
            End If
        End If

    End Sub

    Private Sub btnSalvarEvento_Click(sender As Object, e As EventArgs) Handles btnSalvarEvento.Click
        divSuccessEvento.Visible = False
        divErroEvento.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtID.Text = "" Then
            'ERRO
            divErroEvento.Visible = True
            lblErroEvento.Text = "Necessário cadastrar dados basicos e informações adicionais antes de inserir um evento!"

        ElseIf txtIdEvento.Text = "" And txtEmail.Text = "" Or ddlEvento.SelectedValue = 0 Then
            'ERRO
            divErroEvento.Visible = True
            lblErroEvento.Text = "Campos obrigatórios!!"
        Else

            If txtIdEvento.Text = "" Then

                'VERIFICA PERMISSAO
                Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")
                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divErroEvento.Visible = True
                    lblErroEvento.Text = "Usuário não tem permissão para realizar novos cadastros"
                Else
                    ds = Con.ExecutarQuery("SELECT COUNT(ID)QTD FROM [TB_AMR_PESSOA_EVENTO] WHERE ID_EVENTO = " & ddlEvento.SelectedValue & " AND ID_TERMINAL = " & ddlPorto.SelectedValue & " AND ID_PESSOA = " & txtID.Text & " ")
                    If ds.Tables(0).Rows(0).Item("QTD") = 0 Then

                        Dim Email As String = txtEmail.Text
                        Dim TIPO As String = "E"
                        Dim TIPO_PESSOA As String = ""

                        'VERIFICA QUAL O TIPO DE PESSOA
                        ds = Con.ExecutarQuery("SELECT fl_prestador,  fl_armazem_atracacao, fl_armazem_descarga, fl_armazem_desembaraco, fl_agente_internacional FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO =" & txtID.Text)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("fl_armazem_atracacao").ToString = True Then
                                TIPO_PESSOA = "T"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_descarga").ToString = True Then
                                TIPO_PESSOA = "T"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_desembaraco").ToString = True Then
                                TIPO_PESSOA = "T"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_prestador").ToString = True Then
                                TIPO_PESSOA = "P"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_agente_internacional").ToString = True Then
                                TIPO_PESSOA = "P"
                            Else
                                TIPO_PESSOA = "C"
                            End If
                        End If



                        'INSERE INFORMAÇOES NO BANCO
                        Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ddlEvento.SelectedValue & "," & ddlPorto.SelectedValue & "," & txtID.Text & ",'" & TIPO & "','" & TIPO_PESSOA & "', LOWER('" & Email & "'))")


                        'REPLICA EMAILS
                        If ckbReplica.Checked = True Then

                            ds = Con.ExecutarQuery("select IDTIPOAVISO FROM TB_TIPOAVISO WHERE TPPROCESSO = 'P'")
                            If ds.Tables(0).Rows.Count > 0 Then
                                For Each linha As DataRow In ds.Tables(0).Rows
                                    Dim ID_AVISO As Integer = linha.Item("IDTIPOAVISO").ToString()

                                    'insere emails
                                    Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ID_AVISO & "," & ddlPorto.SelectedValue & "," & txtID.Text & ",'" & TIPO & "','" & TIPO_PESSOA & "',  LOWER('" & Email & "'))")

                                Next

                            End If
                        End If


                        ddlPorto.SelectedValue = 0
                        ddlEvento.SelectedValue = 0
                        txtEmail.Text = ""
                        dgvEmailEvento.DataBind()
                        divSuccessEvento.Visible = True
                        lblSuccessEvento.Text = "Email cadastrado com sucesso!"
                        Con.Fechar()
                    Else
                        divErroEvento.Visible = True
                        lblErroEvento.Text = "Já existe email cadastrado para esse evento, selecione a opção de editar!"
                        dgvEmailEvento.DataBind()
                    End If
                End If



            Else

                'VERIFICA PERMISSAO
                Dim ds As DataSet = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 7 AND FL_ATUALIZAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    lblErroEvento.Text = "Usuário não tem permissão para realizar alterações"
                    divErroEvento.Visible = True

                Else

                    'ALTERA INFORMAÇOES NO BANCO
                    Con.ExecutarQuery("UPDATE [dbo].[TB_AMR_PESSOA_EVENTO] SET ENDERECOS = LOWER('" & txtEmail.Text & "'), ID_EVENTO = " & ddlEvento.SelectedValue & ", ID_TERMINAL = " & ddlPorto.SelectedValue & " WHERE ID = " & txtIdEvento.Text)

                    If ckbReplica.Checked = True Then
                        Dim TIPO As String = "E"
                        Dim TIPO_PESSOA As String = ""

                        'VERIFICA QUAL O TIPO DE PESSOA
                        ds = Con.ExecutarQuery("SELECT fl_prestador,  fl_armazem_atracacao, fl_armazem_descarga, fl_armazem_desembaraco, fl_agente_internacional FROM [dbo].[TB_PARCEIRO] WHERE ID_PARCEIRO =" & txtID.Text)
                        If ds.Tables(0).Rows.Count > 0 Then
                            If ds.Tables(0).Rows(0).Item("fl_armazem_atracacao").ToString = True Then
                                TIPO_PESSOA = "T"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_descarga").ToString = True Then
                                TIPO_PESSOA = "T"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_armazem_desembaraco").ToString = True Then
                                TIPO_PESSOA = "T"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_prestador").ToString = True Then
                                TIPO_PESSOA = "P"
                            ElseIf ds.Tables(0).Rows(0).Item("fl_agente_internacional").ToString = True Then
                                TIPO_PESSOA = "P"
                            Else
                                TIPO_PESSOA = "C"
                            End If
                        End If


                        'REPLICA EMAILS
                        ds = Con.ExecutarQuery("select IDTIPOAVISO FROM TB_TIPOAVISO WHERE TPPROCESSO = 'P'")
                        If ds.Tables(0).Rows.Count > 0 Then

                            For Each linha As DataRow In ds.Tables(0).Rows
                                Dim ID_AVISO As Integer = linha.Item("IDTIPOAVISO").ToString()


                                Dim dsEmail As DataSet = Con.ExecutarQuery("SELECT ID FROM TB_AMR_PESSOA_EVENTO WHERE ID_PESSOA  = " & txtID.Text & " AND ID_EVENTO = " & ID_AVISO)
                                If dsEmail.Tables(0).Rows.Count = 0 Then

                                    'INSERE EMAILS
                                    Con.ExecutarQuery("INSERT INTO [dbo].[TB_AMR_PESSOA_EVENTO] (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ID_AVISO & "," & ddlPorto.SelectedValue & "," & txtID.Text & ",'" & TIPO & "','" & TIPO_PESSOA & "', LOWER('" & txtEmail.Text & "'))")

                                Else

                                    For Each linhaEmail As DataRow In dsEmail.Tables(0).Rows
                                        'UPDATE EMAILS
                                        Con.ExecutarQuery("UPDATE [dbo].[TB_AMR_PESSOA_EVENTO] SET ENDERECOS= LOWER('" & txtEmail.Text & "') , ID_EVENTO = " & ddlEvento.SelectedValue & ", ID_TERMINAL = " & ddlPorto.SelectedValue & " WHERE ID = " & linhaEmail.Item("ID").ToString())
                                    Next

                                End If
                            Next

                        End If



                    End If

                    dgvEmailEvento.DataBind()
                    txtEmail.Text = ""
                    txtIdEvento.Text = ""
                    ddlPorto.SelectedValue = 0
                    ddlEvento.SelectedValue = 0
                    divSuccessEvento.Visible = True
                    lblSuccessEvento.Text = "Email atualizado com sucesso!"

                End If



            End If

        End If

    End Sub

    Private Sub dgvContato_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dgvContato.RowDeleted
        If e.Exception IsNot Nothing Then
            lblErroContato.Text = "Erro ao realizar exclusão!"
            divErroContato.Visible = True
        Else
            divSuccessContato.Visible = True
            lblSuccessContato.Text = "Contato deletado com sucesso!"
        End If
    End Sub

    Private Sub dgvEmailEvento_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dgvEmailEvento.RowDeleted
        If e.Exception IsNot Nothing Then
            lblErroEvento.Text = "Erro ao realizar exclusão!"
            divErroEvento.Visible = True
        Else
            divSuccessEvento.Visible = True
            lblSuccessEvento.Text = "Evento deletado com sucesso!"
        End If
    End Sub
End Class