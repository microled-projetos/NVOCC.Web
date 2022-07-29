Imports Newtonsoft.Json
Public Class CadastrarParceiro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        divmsg.Visible = False

        If Not Page.IsPostBack Then
            ddlTipoFaturamento.SelectedValue = 1
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
SPREAD_MARITIMO_IMPO_FCL,
SPREAD_MARITIMO_IMPO_LCL,
SPREAD_MARITIMO_EXPO_FCL,
SPREAD_MARITIMO_EXPO_LCL,
SPREAD_AEREO_IMPO,
SPREAD_AEREO_EXPO,
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL,0)ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL,
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL,0)ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL,
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL,0)ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL,
ISNULL(ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL,0)ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL,
ISNULL(ID_ACORDO_CAMBIO_AEREO_IMPO,0)ID_ACORDO_CAMBIO_AEREO_IMPO,
ISNULL(ID_ACORDO_CAMBIO_AEREO_EXPO,0)ID_ACORDO_CAMBIO_AEREO_EXPO,
ISNULL(QT_DIAS_FATURAMENTO,0)QT_DIAS_FATURAMENTO,
ISNULL(ID_TIPO_FATURAMENTO,0)ID_TIPO_FATURAMENTO,
FL_VENDEDOR_DIRETO,
FL_EQUIPE_INSIDE_SALES,
FL_SHIPPER,
FL_CNEE,
FL_RODOVIARIO,
OB_COMPLEMENTARES,
ISNULL(REGRA_ATUALIZACAO,0)REGRA_ATUALIZACAO 
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
                txtMaritimoImpoFCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_FCL")
                txtMaritimoImpoLCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_LCL")
                txtMaritimoExpoFCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_FCL")
                txtMaritimoExpoLCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_LCL")
                txtAereoImpo.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_IMPO")
                txtAereoExpo.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_EXPO")
                ddlAcordoCambioAereoIMPO.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO_IMPO")
                ddlAcordoCambioAereoEXPO.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO_EXPO")
                ddlAcordoCambioMaritimoImpoFCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL")
                ddlAcordoCambioMaritimoImpoLCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL")
                ddlAcordoCambioMaritimoExpoFCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL")
                ddlAcordoCambioMaritimoExpoLCL.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL")
                ddlRegraAtualizacao.SelectedValue = ds.Tables(0).Rows(0).Item("REGRA_ATUALIZACAO")
                txtQtdFaturamento.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO")
                ddlTipoFaturamento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TIPO_FATURAMENTO")

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

                VerificaChecks()

                If ddlTipoPessoa.SelectedValue = 2 Then
                    txtCPF.Enabled = True
                    txtCNPJ.Enabled = False
                Else
                    txtCPF.Enabled = False
                    txtCNPJ.Enabled = True

                End If

            End If

            'PREENCHE CAMPOS DE CONTATO
            'ds = Con.ExecutarQuery("SELECT [ID_CONTATO],[ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO] FROM [dbo].[TB_CONTATO] WHERE ID_PARCEIRO =" & ID)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    txtNomeContato.Text = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
            '    txtDepartamento.Text = ds.Tables(0).Rows(0).Item("NM_DEPARTAMENTO").ToString()
            '    txtTelContato.Text = ds.Tables(0).Rows(0).Item("TELEFONE_CONTATO").ToString()
            '    txtEmailContato.Text = ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
            '    txtCelularContato.Text = ds.Tables(0).Rows(0).Item("CELULAR_CONTATO").ToString()

            'End If

            'PREENCHE CAMPOS DE EMAIL
            ds = Con.ExecutarQuery("SELECT ID, ENDERECOS,ID_EVENTO, ID_TERMINAL FROM [TB_AMR_PESSOA_EVENTO] WHERE ID_PESSOA =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtEmail.Text = ds.Tables(0).Rows(0).Item("ENDERECOS").ToString()
                ddlPorto.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TERMINAL")
                ddlEvento.SelectedValue = ds.Tables(0).Rows(0).Item("ID_EVENTO")
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
        divSuccesgrid.Visible = False
        divErrogrid.Visible = False
        btnSalvarContato.Visible = False
        Dim ds As DataSet

        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim UF As String = ""
        Dim dsUf As DataSet = Con.ExecutarQuery("SELECT ISNULL(E.SIGLA_ESTADO,0)UF FROM TB_ESTADO E INNER JOIN TB_CIDADE C ON C.ID_ESTADO = E.ID_ESTADO WHERE C.ID_CIDADE = " & ddlCidade.SelectedValue)
        If dsUf.Tables(0).Rows.Count > 0 Then
            UF = dsUf.Tables(0).Rows(0).Item("UF")
        End If

        If txtQtdFaturamento.Text = "" Then
            txtQtdFaturamento.Text = 0
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

        ElseIf (ckbImportador.Checked = True Or ckbExportador.Checked = True Or ckbAgente.Checked = True Or ckbComissaria.Checked = True Or ckbAgenteInternacional.Checked = True Or ckbIndicador.Checked = True Or ckbShipper.Checked = True Or ckbCNEE.Checked = True Or ckbTranspRodoviario.Checked = True) And ddlTipoFaturamento.SelectedValue = 0 Then
            msgErro.Text = "Informe o tipo de faturamento na aba de Inf. Adicionais!"
            divmsg1.Visible = True
            msgErro.Visible = True

        ElseIf ddlTipoFaturamento.SelectedValue = 2 And txtQtdFaturamento.Text = 0 Then
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



            txtInscEstadual.Text = txtInscEstadual.Text.Replace(".", String.Empty)
            txtInscEstadual.Text = txtInscEstadual.Text.Replace(" ", String.Empty)
            txtInscEstadual.Text = txtInscEstadual.Text.Replace("-", String.Empty)
            txtInscEstadual.Text = txtInscEstadual.Text.Replace("/", String.Empty)

            If txtID.Text = "" Then

                ds = Con.ExecutarQuery("SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND FL_CADASTRAR = 1 AND ID_TIPO_USUARIO IN(" & Session("ID_TIPO_USUARIO") & " )")

                If ds.Tables(0).Rows(0).Item("QTD") = 0 Then
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para cadastrar."
                Else
                    ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "'")
                    If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                        msgErro.Text = "Já existe Parceiro com este CNPJ"
                        divmsg1.Visible = True
                        msgErro.Visible = True
                    Else
                        ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "'")
                        If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                            msgErro.Text = "Já existe Parceiro com este CPF"
                            divmsg1.Visible = True
                            msgErro.Visible = True
                        Else
                            Dim FILTRO As String
                            If ddlTipoPessoa.SelectedValue = 1 Or ddlTipoPessoa.SelectedValue = 3 Then

                                FILTRO = "'" & txtCNPJ.Text & "', NULL ,"
                            Else

                                FILTRO = " NULL, '" & txtCPF.Text & "',"

                            End If



                            If txtInscEstadual.Text = "" Then
                                txtInscEstadual.Text = "NULL"
                            Else
                                txtInscEstadual.Text = "'" & txtInscEstadual.Text & "'"
                            End If

                            If txtInscMunicipal.Text = "" Then
                                txtInscMunicipal.Text = "NULL"
                            Else
                                txtInscMunicipal.Text = "'" & txtInscMunicipal.Text & "'"
                            End If



                            If txtAliquotaISS.Text = "" Then
                                txtAliquotaISS.Text = "0"
                            Else

                                txtAliquotaISS.Text = txtAliquotaISS.Text.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAliquotaPIS.Text = "" Then
                                txtAliquotaPIS.Text = "0"
                            Else

                                txtAliquotaPIS.Text = txtAliquotaPIS.Text.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAliquotaCOFINS.Text = "" Then
                                txtAliquotaCOFINS.Text = "0"
                            Else
                                txtAliquotaCOFINS.Text = txtAliquotaCOFINS.Text.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
                            End If





                            If txtMaritimoImpoFCL.Text = "" Then
                                txtMaritimoImpoFCL.Text = "0"
                            Else
                                txtMaritimoImpoFCL.Text = txtMaritimoImpoFCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtMaritimoImpoLCL.Text = "" Then
                                txtMaritimoImpoLCL.Text = "0"
                            Else
                                txtMaritimoImpoLCL.Text = txtMaritimoImpoLCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtMaritimoExpoFCL.Text = "" Then
                                txtMaritimoExpoFCL.Text = "0"
                            Else
                                txtMaritimoExpoFCL.Text = txtMaritimoExpoFCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtMaritimoExpoLCL.Text = "" Then
                                txtMaritimoExpoLCL.Text = "0"
                            Else
                                txtMaritimoExpoLCL.Text = txtMaritimoExpoLCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAereoImpo.Text = "" Then
                                txtAereoImpo.Text = "0"
                            Else
                                txtAereoImpo.Text = txtAereoImpo.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAereoExpo.Text = "" Then
                                txtAereoExpo.Text = "0"
                            Else
                                txtAereoExpo.Text = txtAereoExpo.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If


                            If txtCEP.Text = "" Then
                                txtCEP.Text = "NULL"
                            Else
                                txtCEP.Text = "'" & txtCEP.Text & "'"
                            End If

                            If txtBairro.Text = "" Then
                                txtBairro.Text = "NULL"
                            Else
                                txtBairro.Text = "'" & txtBairro.Text & "'"
                            End If


                            If txtEndereco.Text = "" Then
                                txtEndereco.Text = "NULL"
                            Else
                                txtEndereco.Text = "'" & txtEndereco.Text & "'"
                            End If


                            If txtNumero.Text = "" Then
                                txtNumero.Text = "NULL"
                            Else
                                txtNumero.Text = "'" & txtNumero.Text & "'"
                            End If


                            If txtCDIATA.Text = "" Then
                                txtCDIATA.Text = "NULL"
                            Else
                                txtCDIATA.Text = "'" & txtCDIATA.Text & "'"
                            End If


                            If txtNomeFantasia.Text = "" Then
                                txtNomeFantasia.Text = "NULL"
                            Else
                                txtNomeFantasia.Text = "'" & txtNomeFantasia.Text & "'"
                            End If

                            If txtTelefone.Text = "" Then
                                txtTelefone.Text = "NULL"
                            Else
                                txtTelefone.Text = "'" & txtTelefone.Text & "'"
                            End If

                            If txtComplemento.Text = "" Then
                                txtComplemento.Text = "NULL"
                            Else
                                txtComplemento.Text = "'" & txtComplemento.Text & "'"
                            End If

                            If txtOBSComplementares.Text = "" Then
                                txtOBSComplementares.Text = "NULL"
                            Else
                                txtOBSComplementares.Text = "'" & txtOBSComplementares.Text & "'"
                            End If

                            If txtEmailParceiro.Text = "" Then
                                txtEmailParceiro.Text = "NULL "
                            Else
                                txtEmailParceiro.Text = "'" & txtEmailParceiro.Text & "'"
                            End If

                            If txtEmailNF.Text = "" Then
                                txtEmailNF.Text = "NULL "
                            Else
                                txtEmailNF.Text = "'" & txtEmailNF.Text & "'"
                            End If

                            If txtQtdFaturamento.Text = "" Then
                                txtQtdFaturamento.Text = 0
                            End If

                            Con.Conectar()

                            Dim SQL As String = ("INSERT INTO [dbo].[TB_PARCEIRO] (
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
            SPREAD_MARITIMO_IMPO_FCL,
            SPREAD_MARITIMO_IMPO_LCL,
            SPREAD_MARITIMO_EXPO_FCL,
            SPREAD_MARITIMO_EXPO_LCL,
            SPREAD_AEREO_IMPO,
            SPREAD_AEREO_EXPO,
            ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL,
            ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL,
            ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL,
            ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL,
            ID_ACORDO_CAMBIO_AEREO_IMPO,
            ID_ACORDO_CAMBIO_AEREO_EXPO,
            QT_DIAS_FATURAMENTO,
            ID_TIPO_FATURAMENTO,
            EMAIL,
            FL_VENDEDOR_DIRETO,
            FL_EQUIPE_INSIDE_SALES,
            FL_SHIPPER,
            FL_CNEE,
            FL_RODOVIARIO,
            REGRA_ATUALIZACAO,
            ID_PAIS,
            ID_USUARIO,
            CREATED_AT
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
            '" & txtRazaoSocial.Text & "',
            " & txtNomeFantasia.Text & ",
            #filtro 
            " & ddlTipoPessoa.SelectedValue & ",
            " & txtInscEstadual.Text & ",
            " & txtInscMunicipal.Text & ",
            " & txtEndereco.Text & ",
            " & txtNumero.Text & ",
            " & txtBairro.Text & ",
            " & txtComplemento.Text & ",
            " & txtOBSComplementares.Text & ",
            " & ddlCidade.SelectedValue & ",
            " & txtTelefone.Text & ",
            " & txtCEP.Text & ",
            " & ddlVendedor.SelectedValue & ",
            '" & ckbISS.Checked & "',
            '" & ckbPIS.Checked & "',
            '" & ckbCOFINS.Checked & "',
            '" & txtAliquotaISS.Text & "',
            '" & txtAliquotaPIS.Text & "',
            '" & txtAliquotaCOFINS.Text & "',
            " & txtEmailNF.Text & ",
            " & txtCDIATA.Text & ",
            '" & ckbSimplesNacional.Checked & "',
            '" & ckbAtivo.Checked & "',
            '" & ckbIndicador.Checked & "',
            '" & txtMaritimoImpoFCL.Text & "',
            '" & txtMaritimoImpoLCL.Text & "',
            '" & txtMaritimoExpoFCL.Text & "',
            '" & txtMaritimoExpoLCL.Text & "',
            '" & txtAereoImpo.Text & "',
            '" & txtAereoExpo.Text & "',
            " & ddlAcordoCambioMaritimoImpoFCL.SelectedValue & ",
            " & ddlAcordoCambioMaritimoImpoLCL.SelectedValue & ",
            " & ddlAcordoCambioMaritimoExpoFCL.SelectedValue & ",
            " & ddlAcordoCambioMaritimoExpoLCL.SelectedValue & ",
            " & ddlAcordoCambioAereoIMPO.SelectedValue & ",
            " & ddlAcordoCambioAereoEXPO.SelectedValue & ",
            " & txtQtdFaturamento.Text & ",
            " & ddlTipoFaturamento.SelectedValue & ",
            " & txtEmailParceiro.Text & ",
            '" & ckbVendedorDireto.Checked & "',
            '" & ckbEquipeInsideSales.Checked & "',
            '" & ckbShipper.Checked & "',
            '" & ckbCNEE.Checked & "',
            '" & ckbTranspRodoviario.Checked & "',
            " & ddlRegraAtualizacao.SelectedValue & ",
            " & ddlPais.SelectedValue & " ,
            " & Session("ID_USUARIO") & ",
             getdate() 
            ) Select SCOPE_IDENTITY() as ID_PARCEIRO ")




                            SQL = SQL.Replace("#filtro", FILTRO)
                            ds = Con.ExecutarQuery(SQL)
                            Dim ID_Parceiro As String = ds.Tables(0).Rows(0).Item("ID_PARCEIRO").ToString()
                            Session("ID_Parceiro") = ID_Parceiro

                            If txtNomeContato.Text = "" And txtTelContato.Text = "" And txtEmailContato.Text = "" And txtDepartamento.Text = "" Then
                                divInformativa.Visible = True
                                lblInformacao.Text = "Parceiro cadastrado sem informações de contato"

                            ElseIf txtIDContato.Text = "" Then
                                If txtNomeContato.Text = "" Then
                                    msgErro.Text = "Preencha o campo de nome na aba contatos."
                                    divmsg1.Visible = True
                                    msgErro.Visible = True
                                Else

                                    If txtTelContato.Text = "" Then
                                        txtTelContato.Text = "NULL "
                                    Else
                                        txtTelContato.Text = "'" & txtTelContato.Text & "'"
                                    End If

                                    If txtEmailContato.Text = "" Then
                                        txtEmailContato.Text = "NULL "
                                    Else
                                        txtEmailContato.Text = "'" & txtEmailContato.Text & "'"
                                    End If

                                    If txtDepartamento.Text = "" Then
                                        txtDepartamento.Text = "NULL "
                                    Else
                                        txtDepartamento.Text = "'" & txtDepartamento.Text & "'"
                                    End If

                                    If txtCelularContato.Text = "" Then
                                        txtCelularContato.Text = "NULL "
                                    Else
                                        txtCelularContato.Text = "'" & txtCelularContato.Text & "'"
                                    End If

                                    'insere contatos
                                    Dim sqlContatos As String = "INSERT INTO TB_CONTATO ([ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO]) VALUES (" & ID_Parceiro & ",'" & txtNomeContato.Text & "'," & txtTelContato.Text & "," & txtEmailContato.Text & "," & txtDepartamento.Text & ", " & txtCelularContato.Text & ")"
                                    Con.ExecutarQuery(sqlContatos)
                                End If
                            End If


                            If txtEmail.Text = "" And ddlEvento.SelectedValue = 0 Then
                                divInformativa.Visible = True
                                lblInformacao.Text &= " <br/> Parceiro cadastrado sem informações de email e evento"
                            Else
                                If txtEmail.Text = "" Then
                                    msgErro.Text = "Preencha o campo de Endereços de Email na aba Email x Eventos."
                                    divmsg1.Visible = True
                                    msgErro.Visible = True
                                Else

                                    Dim TIPO As String = "E"
                                    Dim TIPO_PESSOA As String = ""

                                    'Verifica qual o tipo de pessoa
                                    If ckbArmazemAtracacao.Checked = True Then
                                        TIPO_PESSOA = "T"
                                    ElseIf ckbArmazemDesembaraco.Checked = True Then
                                        TIPO_PESSOA = "T"
                                    ElseIf ckbArmazemDescarga.Checked = True Then
                                        TIPO_PESSOA = "T"
                                    ElseIf ckbPrestador.Checked = True Then
                                        TIPO_PESSOA = "P"
                                    Else
                                        TIPO_PESSOA = "C"
                                    End If


                                    'insere emails
                                    Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ddlEvento.SelectedValue & "," & ddlPorto.SelectedValue & "," & ID_Parceiro & ",'" & TIPO & "','" & TIPO_PESSOA & "', '" & txtEmail.Text & "')")



                                    'REPLICA EMAILS
                                    If ckbReplica.Checked = True Then

                                        ds = Con.ExecutarQuery("select IDTIPOAVISO FROM TB_TIPOAVISO WHERE TPPROCESSO = 'P'")


                                        If ds.Tables(0).Rows.Count > 0 Then

                                            For Each linha As DataRow In ds.Tables(0).Rows
                                                Dim ID_AVISO As Integer = linha.Item("IDTIPOAVISO").ToString()
                                                Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ID_AVISO & "," & ddlPorto.SelectedValue & "," & ID_Parceiro & ",'" & TIPO & "','" & TIPO_PESSOA & "', '" & txtEmail.Text & "')")
                                            Next

                                        End If
                                    End If



                                End If

                            End If


                            Con.Fechar()
                            Call Limpar(Me)
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
                    ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "' AND ID_PARCEIRO <> " & txtID.Text)
                    If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue = 1 Then
                        msgErro.Text = "Já existe Parceiro com este CNPJ"
                        divmsg1.Visible = True
                        msgErro.Visible = True
                    Else
                        ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "' AND ID_PARCEIRO <> " & txtID.Text)
                        If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue = 2 Then
                            msgErro.Text = "Já existe Parceiro com este CPF"
                            divmsg1.Visible = True
                            msgErro.Visible = True
                        Else

                            Dim FILTRO As String
                            If ddlTipoPessoa.SelectedValue = 1 Or ddlTipoPessoa.SelectedValue = 3 Then

                                FILTRO = "CNPJ ='" & txtCNPJ.Text & "', CPF = NULL ,"
                            Else

                                FILTRO = "CNPJ= NULL, CPF ='" & txtCPF.Text & "',"

                            End If

                            If txtCDIATA.Text = "" Then
                                txtCDIATA.Text = "NULL"
                            Else
                                txtCDIATA.Text = "'" & txtCDIATA.Text & "'"
                            End If

                            If txtNomeFantasia.Text = "" Then
                                txtNomeFantasia.Text = "NULL"
                            Else
                                txtNomeFantasia.Text = "'" & txtNomeFantasia.Text & "'"
                            End If



                            If txtCEP.Text = "" Then
                                txtCEP.Text = "NULL"
                            Else
                                txtCEP.Text = "'" & txtCEP.Text & "'"
                            End If

                            If txtBairro.Text = "" Then
                                txtBairro.Text = "NULL"
                            Else
                                txtBairro.Text = "'" & txtBairro.Text & "'"
                            End If


                            If txtEndereco.Text = "" Then
                                txtEndereco.Text = "NULL"
                            Else
                                txtEndereco.Text = "'" & txtEndereco.Text & "'"
                            End If


                            If txtNumero.Text = "" Then
                                txtNumero.Text = "NULL"
                            Else
                                txtNumero.Text = "'" & txtNumero.Text & "'"
                            End If

                            If txtEmailParceiro.Text = "" Then
                                txtEmailParceiro.Text = "NULL "
                            Else
                                txtEmailParceiro.Text = "'" & txtEmailParceiro.Text & "'"
                            End If

                            If txtTelefone.Text = "" Then
                                txtTelefone.Text = "NULL"
                            Else
                                txtTelefone.Text = "'" & txtTelefone.Text & "'"
                            End If

                            If txtComplemento.Text = "" Then
                                txtComplemento.Text = "NULL "
                            Else
                                txtComplemento.Text = "'" & txtComplemento.Text & "'"
                            End If

                            If txtOBSComplementares.Text = "" Then
                                txtOBSComplementares.Text = "NULL "
                            Else
                                txtOBSComplementares.Text = "'" & txtOBSComplementares.Text & "'"
                            End If

                            If txtEmailNF.Text = "" Then
                                txtEmailNF.Text = "NULL "
                            Else
                                txtEmailNF.Text = "'" & txtEmailNF.Text & "'"
                            End If

                            If txtAliquotaISS.Text = "" Then
                                txtAliquotaISS.Text = "0"
                            Else

                                txtAliquotaISS.Text = txtAliquotaISS.Text.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAliquotaPIS.Text = "" Then
                                txtAliquotaPIS.Text = "0"
                            Else

                                txtAliquotaPIS.Text = txtAliquotaPIS.Text.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAliquotaCOFINS.Text = "" Then
                                txtAliquotaCOFINS.Text = "0"
                            Else
                                txtAliquotaCOFINS.Text = txtAliquotaCOFINS.Text.Replace("R", String.Empty).Replace("$", String.Empty).Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtMaritimoImpoFCL.Text = "" Then
                                txtMaritimoImpoFCL.Text = "0"
                            Else
                                txtMaritimoImpoFCL.Text = txtMaritimoImpoFCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtMaritimoImpoLCL.Text = "" Then
                                txtMaritimoImpoLCL.Text = "0"
                            Else
                                txtMaritimoImpoLCL.Text = txtMaritimoImpoLCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtMaritimoExpoFCL.Text = "" Then
                                txtMaritimoExpoFCL.Text = "0"
                            Else
                                txtMaritimoExpoFCL.Text = txtMaritimoExpoFCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtMaritimoExpoLCL.Text = "" Then
                                txtMaritimoExpoLCL.Text = "0"
                            Else
                                txtMaritimoExpoLCL.Text = txtMaritimoExpoLCL.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAereoImpo.Text = "" Then
                                txtAereoImpo.Text = "0"
                            Else
                                txtAereoImpo.Text = txtAereoImpo.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtAereoExpo.Text = "" Then
                                txtAereoExpo.Text = "0"
                            Else
                                txtAereoExpo.Text = txtAereoExpo.Text.Replace(".", String.Empty).Replace(",", ".")
                            End If

                            If txtQtdFaturamento.Text = "" Then
                                txtQtdFaturamento.Text = 0
                            End If



                            If txtInscEstadual.Text = "" Then
                                txtInscEstadual.Text = "NULL"
                            Else
                                txtInscEstadual.Text = "'" & txtInscEstadual.Text & "'"
                            End If

                            If txtInscMunicipal.Text = "" Then
                                txtInscMunicipal.Text = "NULL"
                            Else
                                txtInscMunicipal.Text = "'" & txtInscMunicipal.Text & "'"
                            End If

                            Dim ID As String = Request.QueryString("id")
                            Con.Conectar()

                            Dim SQL As String = ("UPDATE [dbo].[TB_PARCEIRO] SET FL_IMPORTADOR = '" & ckbImportador.Checked & "',
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
            NM_RAZAO= '" & txtRazaoSocial.Text & "',
            NM_FANTASIA = " & txtNomeFantasia.Text & ",
            #filtro 
            TP_PESSOA= '" & ddlTipoPessoa.SelectedValue & "',
            INSCR_ESTADUAL=" & txtInscEstadual.Text & ",
            INSCR_MUNICIPAL= " & txtInscMunicipal.Text & ",
            ENDERECO=" & txtEndereco.Text & ",
            NR_ENDERECO=" & txtNumero.Text & ",
            BAIRRO=" & txtBairro.Text & ",
            COMPL_ENDERECO=" & txtComplemento.Text & ",
            OB_COMPLEMENTARES=" & txtOBSComplementares.Text & ",
            ID_CIDADE=" & ddlCidade.SelectedValue & ",
            TELEFONE=" & txtTelefone.Text & ",
            CEP=" & txtCEP.Text & ",
            ID_VENDEDOR=" & ddlVendedor.SelectedValue & ",
            FL_ISENTO_ISS='" & ckbISS.Checked & "',
            FL_ISENTO_PIS='" & ckbPIS.Checked & "',
            FL_ISENTO_COFINS='" & ckbCOFINS.Checked & "',
            VL_ALIQUOTA_ISS='" & txtAliquotaISS.Text & "',
            VL_ALIQUOTA_PIS= '" & txtAliquotaPIS.Text & "',
            VL_ALIQUOTA_COFINS='" & txtAliquotaCOFINS.Text & "',
            EMAIL_NF_ELETRONICA=" & txtEmailNF.Text & ",
            CD_IATA=" & txtCDIATA.Text & ",
            FL_SIMPLES_NACIONAL='" & ckbSimplesNacional.Checked & "',
            FL_ATIVO = '" & ckbAtivo.Checked & "', 
            FL_INDICADOR = '" & ckbIndicador.Checked & "',
            SPREAD_MARITIMO_IMPO_FCL = '" & txtMaritimoImpoFCL.Text & "',
            SPREAD_MARITIMO_IMPO_LCL = '" & txtMaritimoImpoLCL.Text & "',
            SPREAD_MARITIMO_EXPO_FCL = '" & txtMaritimoExpoFCL.Text & "',
            SPREAD_MARITIMO_EXPO_LCL = '" & txtMaritimoExpoLCL.Text & "',
            SPREAD_AEREO_IMPO = '" & txtAereoImpo.Text & "',
            SPREAD_AEREO_EXPO = '" & txtAereoExpo.Text & "',
            ID_ACORDO_CAMBIO_MARITIMO_IMPO_FCL = " & ddlAcordoCambioMaritimoImpoFCL.SelectedValue & ",
            ID_ACORDO_CAMBIO_MARITIMO_IMPO_LCL = " & ddlAcordoCambioMaritimoImpoLCL.SelectedValue & ",
            ID_ACORDO_CAMBIO_MARITIMO_EXPO_FCL = " & ddlAcordoCambioMaritimoExpoFCL.SelectedValue & ",
            ID_ACORDO_CAMBIO_MARITIMO_EXPO_LCL = " & ddlAcordoCambioMaritimoExpoLCL.SelectedValue & ",
            ID_ACORDO_CAMBIO_AEREO_IMPO = " & ddlAcordoCambioAereoIMPO.SelectedValue & ",
            ID_ACORDO_CAMBIO_AEREO_EXPO = " & ddlAcordoCambioAereoEXPO.SelectedValue & ",
            QT_DIAS_FATURAMENTO =  " & txtQtdFaturamento.Text & ",
            ID_TIPO_FATURAMENTO = " & ddlTipoFaturamento.SelectedValue & ",
            EMAIL =  " & txtEmailParceiro.Text & ",
            FL_EQUIPE_INSIDE_SALES ='" & ckbEquipeInsideSales.Checked & "',
            FL_VENDEDOR_DIRETO ='" & ckbVendedorDireto.Checked & "',
            FL_SHIPPER = '" & ckbShipper.Checked & "',
            FL_CNEE = '" & ckbCNEE.Checked & "',
            FL_RODOVIARIO = '" & ckbTranspRodoviario.Checked & "',
            REGRA_ATUALIZACAO = " & ddlRegraAtualizacao.SelectedValue & " ,
            ID_PAIS=" & ddlPais.SelectedValue & ",
            ID_USUARIO = " & Session("ID_USUARIO") & ",
            UPDATED_AT = GETDATE() 
            where ID_PARCEIRO = " & ID)
                            Session("ID_Parceiro") = ID

                            SQL = SQL.Replace("#filtro", FILTRO)
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

                            If txtNomeContato.Text <> "" And txtIDContato.Text = "" Then

                                If txtNomeContato.Text = "" Then
                                    txtNomeContato.Text = "NULL"
                                Else
                                    txtNomeContato.Text = "'" & txtNomeContato.Text & "'"
                                End If

                                If txtTelContato.Text = "" Then
                                    txtTelContato.Text = "NULL"
                                Else
                                    txtTelContato.Text = "'" & txtTelContato.Text & "'"
                                End If

                                If txtEmailContato.Text = "" Then
                                    txtEmailContato.Text = "NULL"
                                Else
                                    txtEmailContato.Text = "'" & txtEmailContato.Text & "'"
                                End If

                                If txtDepartamento.Text = "" Then
                                    txtDepartamento.Text = "NULL"
                                Else
                                    txtDepartamento.Text = "'" & txtDepartamento.Text & "'"
                                End If

                                If txtCelularContato.Text = "" Then
                                    txtCelularContato.Text = "NULL"
                                Else
                                    txtCelularContato.Text = "'" & txtCelularContato.Text & "'"
                                End If

                                '   Dim dsContatos As DataSet = Con.ExecutarQuery("SELECT count(ID_CONTATO)ID_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ID)


                                'If dsContatos.Tables(0).Rows(0).Item("ID_CONTATO") > 0 Then
                                '    'update contatos
                                '    Con.ExecutarQuery("UPDATE TB_CONTATO SET [NM_CONTATO] = " & txtNomeContato.Text & " ,[TELEFONE_CONTATO] =" & txtTelContato.Text & ",[EMAIL_CONTATO] =  " & txtEmailContato.Text & ",[NM_DEPARTAMENTO] =" & txtDepartamento.Text & ", [CELULAR_CONTATO] =" & txtCelularContato.Text & " where ID_Parceiro = " & ID)
                                'Else
                                'insere contatos
                                Con.ExecutarQuery("INSERT INTO TB_CONTATO ([ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO]) VALUES (" & ID & "," & txtNomeContato.Text & "," & txtTelContato.Text & "," & txtEmailContato.Text & "," & txtDepartamento.Text & ", " & txtCelularContato.Text & ")")
                                ' End If

                            End If

                            If txtEmail.Text <> "" And ddlEvento.SelectedValue <> 0 Then

                                If txtEmail.Text = "" Then
                                    msgErro.Text = "Preencha o campo de Endereços de Email na aba Email x Eventos."
                                    divmsg1.Visible = True
                                    msgErro.Visible = True
                                Else
                                    Dim TIPO As String = "E"
                                    Dim TIPO_PESSOA As String = ""

                                    'Verifica qual o tipo de pessoa
                                    If ckbArmazemAtracacao.Checked = True Then
                                        TIPO_PESSOA = "T"
                                    ElseIf ckbArmazemDesembaraco.Checked = True Then
                                        TIPO_PESSOA = "T"
                                    ElseIf ckbArmazemDescarga.Checked = True Then
                                        TIPO_PESSOA = "T"
                                    ElseIf ckbPrestador.Checked = True Then
                                        TIPO_PESSOA = "P"
                                    Else
                                        TIPO_PESSOA = "C"
                                    End If

                                    Dim dsEmail As DataSet = Con.ExecutarQuery("SELECT ID FROM TB_AMR_PESSOA_EVENTO WHERE ID_PESSOA  = " & ID & " AND ID_EVENTO = " & ddlEvento.SelectedValue)
                                    If dsEmail.Tables(0).Rows.Count = 0 Then
                                        'insere emails
                                        Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ddlEvento.SelectedValue & "," & ddlPorto.SelectedValue & "," & ID & ",'" & TIPO & "','" & TIPO_PESSOA & "', '" & txtEmail.Text & "')")

                                    Else

                                        For Each linha As DataRow In dsEmail.Tables(0).Rows
                                            'update emails
                                            Con.ExecutarQuery("UPDATE [dbo].[TB_AMR_PESSOA_EVENTO] SET ID_EVENTO = " & ddlEvento.SelectedValue & ", ID_TERMINAL =" & ddlPorto.SelectedValue & ", TIPO = '" & TIPO & "', TIPO_PESSOA ='" & TIPO_PESSOA & "', ENDERECOS= '" & txtEmail.Text & "' where ID = " & linha.Item("ID").ToString())
                                        Next

                                    End If


                                    'REPLICA EMAILS
                                    If ckbReplica.Checked = True Then

                                        ds = Con.ExecutarQuery("select IDTIPOAVISO FROM TB_TIPOAVISO WHERE TPPROCESSO = 'P'")


                                        If ds.Tables(0).Rows.Count > 0 Then

                                            For Each linha As DataRow In ds.Tables(0).Rows
                                                Dim ID_AVISO As Integer = linha.Item("IDTIPOAVISO").ToString()


                                                dsEmail = Con.ExecutarQuery("SELECT ID FROM TB_AMR_PESSOA_EVENTO WHERE ID_PESSOA  = " & ID & " AND ID_EVENTO = " & ID_AVISO)
                                                If dsEmail.Tables(0).Rows.Count = 0 Then
                                                    'insere emails
                                                    Con.ExecutarQuery("INSERT INTO TB_AMR_PESSOA_EVENTO (ID_EVENTO, ID_TERMINAL, ID_PESSOA, TIPO, TIPO_PESSOA, ENDERECOS) values(" & ID_AVISO & "," & ddlPorto.SelectedValue & "," & ID & ",'" & TIPO & "','" & TIPO_PESSOA & "', '" & txtEmail.Text & "')")

                                                Else

                                                    For Each linhaEmail As DataRow In dsEmail.Tables(0).Rows
                                                        'update emails
                                                        Con.ExecutarQuery("UPDATE [dbo].[TB_AMR_PESSOA_EVENTO] SET ENDERECOS= '" & txtEmail.Text & "' where ID = " & linhaEmail.Item("ID").ToString())
                                                    Next

                                                End If
                                            Next

                                        End If
                                    End If

                                End If

                            End If
                            Call Limpar(Me)
                            dgvContato.DataBind()
                            dgvEmailvento.DataBind()
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
        If ddlTipoPessoa.SelectedValue = 2 Then
            divIATA.Attributes.CssStyle.Add("display", "none")
            divCPF.Visible = True
            divCNPJ.Visible = False
            txtCPF.Text = ""
            txtCDIATA.Text = ""
            btnConsultaCNPJ.Visible = False
            RedCidade.Visible = True
            RedBairro.Visible = True
            RedNum.Visible = True
            RedEnd.Visible = True
            RedCEP.Visible = True
        ElseIf ddlTipoPessoa.SelectedValue = 1 Then
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
        ElseIf ddlTipoPessoa.SelectedValue = 3 Then
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
        Else
            divCPF.Visible = False
            divCNPJ.Visible = False
            btnConsultaCNPJ.Visible = False
        End If

        'If ddlTipoPessoa.SelectedValue = 3 Then
        '    Dim FILTRO As String = " where ID_PAIS <> 12 "
        '    dsCidades.SelectCommand = dsCidades.SelectCommand.Replace("#FILTRO", FILTRO)
        '    ddlCidade.DataBind()

        'Else
        '    dsCidades.SelectCommand = dsCidades.SelectCommand.Replace("where ID_PAIS <> 12", "#FILTRO")
        '    ddlCidade.DataBind()
        'End If
    End Sub

    Private Sub ddlTipoPessoa_PreRender(sender As Object, e As EventArgs) Handles ddlTipoPessoa.PreRender
        If ddlTipoPessoa.SelectedValue = 2 Then
            divCPF.Visible = True
            divCNPJ.Visible = False
            btnConsultaCNPJ.Visible = False

        ElseIf ddlTipoPessoa.SelectedValue = 1 Then
            divCPF.Visible = False
            divCNPJ.Visible = True
            btnConsultaCNPJ.Visible = True

        ElseIf ddlTipoPessoa.SelectedValue = 3 Then

            divCPF.Visible = False
            divCNPJ.Visible = True
            btnConsultaCNPJ.Visible = False

        Else
            divCPF.Visible = False
            divCNPJ.Visible = False
            btnConsultaCNPJ.Visible = False
        End If


    End Sub

    Private Sub ddlEvento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEvento.SelectedIndexChanged
        Dim ID_EVENTO As Integer = ddlEvento.SelectedValue
        Dim filtro As String
        If txtID.Text <> "" Then
            filtro = " and ID_Pessoa = " & Request.QueryString("id")
            Dim Con As New Conexao_sql

            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID, ENDERECOS, ID_TERMINAL FROM [TB_AMR_PESSOA_EVENTO] WHERE ID_EVENTO =" & ID_EVENTO & " " & filtro)
            If ds.Tables(0).Rows.Count > 0 Then
                txtEmail.Text = ds.Tables(0).Rows(0).Item("ENDERECOS").ToString()
                ddlPorto.SelectedValue = ds.Tables(0).Rows(0).Item("ID_TERMINAL").ToString()
            Else
                txtEmail.Text = ""
                ddlPorto.SelectedValue = 0
            End If
            Con.Fechar()

        End If

    End Sub
    Protected Sub dgvEmailvento_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim dt As DataTable = TryCast(Session("TaskTable"), DataTable)

        If dt IsNot Nothing Then
            dt.DefaultView.Sort = e.SortExpression & " " + GetSortDirection(e.SortExpression)
            Session("TaskTable") = dt
            dgvEmailvento.DataSource = Session("TaskTable")
            dgvEmailvento.DataBind()
            dgvEmailvento.HeaderRow.TableSection = TableRowSection.TableHeader
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
        divSuccesgrid.Visible = False
        divErrogrid.Visible = False
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
        divSuccesgrid.Visible = False
        divErrogrid.Visible = False
        Dim Con As New Conexao_sql
        Con.Conectar()
        If txtNomeContato.Text = "" Then
            'ERRO
            divErrogrid.Visible = True
            lblErrogrid.Text = "Obrigatorio o preenchimento de nome do contato!"
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
            divSuccesgrid.Visible = True
            lblSuccesgrid.Text = "Contato alterado com sucesso!"
            btnSalvarContato.Visible = False
            txtIDContato.Text = ""
            txtNomeContato.Text = ""
            txtTelContato.Text = ""
            txtCelularContato.Text = ""
            txtEmailContato.Text = ""
            txtDepartamento.Text = ""
            dgvContato.DataBind()
        End If
    End Sub

    Private Sub txtCNPJ_TextChanged(sender As Object, e As EventArgs) Handles txtCNPJ.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        divmsg1.Visible = False

        'Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "'")
        'If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
        '    msgErro.Text = "Já existe Parceiro com este CNPJ"
        '    divmsg1.Visible = True
        '    msgErro.Visible = True
        'Else
        If ddlTipoPessoa.SelectedValue = 1 And ValidaCNPJ.Validar(txtCNPJ.Text) = False Then
            msgErro.Text = "CNPJ Inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "' and TP_PESSOA <> 3")
            If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                msgErro.Text = "Já existe Parceiro com este CNPJ"
                divmsg1.Visible = True
                msgErro.Visible = True
            End If
        End If
        ' End If

    End Sub

    Private Sub txtCPF_TextChanged(sender As Object, e As EventArgs) Handles txtCPF.TextChanged
        Dim Con As New Conexao_sql
        Con.Conectar()
        divmsg1.Visible = False

        'Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "'")
        'If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
        '    msgErro.Text = "Já existe Parceiro com este CPF"
        '    divmsg1.Visible = True
        '    msgErro.Visible = True
        'Else
        If ddlTipoPessoa.SelectedValue = 2 And ValidaCPF.Validar(txtCPF.Text) = False Then
            msgErro.Text = "CPF Inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        Else
            Dim ds As DataSet = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "'")
            If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                msgErro.Text = "Já existe Parceiro com este CPF"
                divmsg1.Visible = True
                msgErro.Visible = True
            End If
        End If
        ''  End If
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
                    txtRazaoSocial.Text = dados.razao_social
                    txtNomeFantasia.Text = dados.estabelecimento.nome_fantasia
                    If txtNomeFantasia.Text = "" Then
                        txtNomeFantasia.Text = dados.razao_social
                    End If
                    txtEmailParceiro.Text = dados.estabelecimento.email
                    txtEmailNF.Text = dados.estabelecimento.email
                    txtTelefone.Text = dados.estabelecimento.ddd1 & dados.estabelecimento.telefone1
                    txtCEP.Text = dados.estabelecimento.cep
                    txtEndereco.Text = dados.estabelecimento.tipo_logradouro & " " & dados.estabelecimento.logradouro
                    txtNumero.Text = dados.estabelecimento.numero
                    txtComplemento.Text = dados.estabelecimento.complemento
                    txtBairro.Text = dados.estabelecimento.bairro

                    Dim listaInsc = dados.estabelecimento.inscricoes_estaduais
                    For Each Insc In listaInsc
                        If Insc.ativo = "true" Then
                            txtInscEstadual.Text = Insc.inscricao_estadual
                        End If
                    Next

                    If txtInscEstadual.Text = "" Then
                        txtInscEstadual.Text = "ISENTO"
                    End If

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

                End Using


            Catch ex As Exception

                divmsg1.Visible = True
                msgErro.Text = "Não foi possivel completar a ação: " & ex.Message
                Exit Sub

            End Try


        End If

    End Sub
End Class