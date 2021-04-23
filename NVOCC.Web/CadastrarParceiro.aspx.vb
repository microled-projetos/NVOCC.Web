
Public Class CadastrarParceiro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then

            Response.Redirect("Login.aspx")

        End If
        divmsg.Visible = False

        If Not Page.IsPostBack Then
            CarregaCampos()
        End If



        Dim Con As New Conexao_sql
        Con.Conectar()
        Dim ds As DataSet = Con.ExecutarQuery("SELECT FL_ACESSAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND  ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
        If ds.Tables(0).Rows.Count > 0 Then

            If ds.Tables(0).Rows(0).Item("FL_ACESSAR") <> True Then

                Response.Redirect("Default.aspx")


            End If


        Else
            Response.Redirect("Default.aspx")
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
TP_PESSOA,
INSCR_ESTADUAL,
INSCR_MUNICIPAL,
ENDERECO,
NR_ENDERECO,
BAIRRO,
COMPL_ENDERECO,
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
ID_ACORDO_CAMBIO_MARITIMO_IMPO,
ID_ACORDO_CAMBIO_MARITIMO_EXPO,
ID_ACORDO_CAMBIO_AEREO,
QT_DIAS_FATURAMENTO
FROM TB_PARCEIRO 
WHERE ID_PARCEIRO =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtID.Text = ds.Tables(0).Rows(0).Item("ID_PARCEIRO").ToString()
                ckbImportador.Checked = ds.Tables(0).Rows(0).Item("FL_IMPORTADOR")
                ckbExportador.Checked = ds.Tables(0).Rows(0).Item("FL_EXPORTADOR")
                ckbAgente.Checked = ds.Tables(0).Rows(0).Item("FL_AGENTE")
                ckbAgenteInternacional.Checked = ds.Tables(0).Rows(0).Item("FL_AGENTE_INTERNACIONAL")
                ckbTransportador.Checked = ds.Tables(0).Rows(0).Item("FL_TRANSPORTADOR")
                ckbComissaria.Checked = ds.Tables(0).Rows(0).Item("FL_COMISSARIA")
                ckbVendedor.Checked = ds.Tables(0).Rows(0).Item("FL_VENDEDOR")
                ckbArmazemAtracacao.Checked = ds.Tables(0).Rows(0).Item("FL_ARMAZEM_ATRACACAO")
                ckbArmazemDesembaraco.Checked = ds.Tables(0).Rows(0).Item("FL_ARMAZEM_DESEMBARACO")
                ckbArmazemDescarga.Checked = ds.Tables(0).Rows(0).Item("FL_ARMAZEM_DESCARGA")
                ckbPrestador.Checked = ds.Tables(0).Rows(0).Item("FL_PRESTADOR")
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
                ddlCidade.SelectedValue = ds.Tables(0).Rows(0).Item("ID_CIDADE")
                txtTelefone.Text = ds.Tables(0).Rows(0).Item("TELEFONE").ToString()
                txtCEP.Text = ds.Tables(0).Rows(0).Item("CEP").ToString()
                ddlVendedor.SelectedValue = ds.Tables(0).Rows(0).Item("ID_VENDEDOR")
                ckbISS.Checked = ds.Tables(0).Rows(0).Item("FL_ISENTO_ISS")
                ckbPIS.Checked = ds.Tables(0).Rows(0).Item("FL_ISENTO_PIS")
                ckbCOFINS.Checked = ds.Tables(0).Rows(0).Item("FL_ISENTO_COFINS")
                txtMaritimoImpoFCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_FCL")
                txtMaritimoImpoLCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_IMPO_LCL")
                txtMaritimoExpoFCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_FCL")
                txtMaritimoExpoLCL.Text = ds.Tables(0).Rows(0).Item("SPREAD_MARITIMO_EXPO_LCL")
                txtAereoImpo.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_IMPO")
                txtAereoExpo.Text = ds.Tables(0).Rows(0).Item("SPREAD_AEREO_EXPO")
                ddlAcordoCambioAereo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_AEREO")
                ddlAcordoCambioMaritimoImpo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_IMPO")
                ddlAcordoCambioMaritimoExpo.SelectedValue = ds.Tables(0).Rows(0).Item("ID_ACORDO_CAMBIO_MARITIMO_EXPO")
                txtQtdFaturamento.Text = ds.Tables(0).Rows(0).Item("QT_DIAS_FATURAMENTO")


                txtAliquotaISS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_ISS").ToString()

                txtAliquotaPIS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_PIS").ToString()

                txtAliquotaCOFINS.Text = ds.Tables(0).Rows(0).Item("VL_ALIQUOTA_COFINS").ToString()


                txtEmailNF.Text = ds.Tables(0).Rows(0).Item("EMAIL_NF_ELETRONICA").ToString()
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
            ds = Con.ExecutarQuery("SELECT [ID_CONTATO],[ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO] FROM [dbo].[TB_CONTATO] WHERE ID_PARCEIRO =" & ID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtNomeContato.Text = ds.Tables(0).Rows(0).Item("NM_CONTATO").ToString()
                txtDepartamento.Text = ds.Tables(0).Rows(0).Item("NM_DEPARTAMENTO").ToString()
                txtTelContato.Text = ds.Tables(0).Rows(0).Item("TELEFONE_CONTATO").ToString()
                txtEmailContato.Text = ds.Tables(0).Rows(0).Item("EMAIL_CONTATO").ToString()
                txtCelularContato.Text = ds.Tables(0).Rows(0).Item("CELULAR_CONTATO").ToString()

            End If

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
        Dim ds As DataSet

        Dim Con As New Conexao_sql
        If txtRazaoSocial.Text = "" Then
            msgErro.Text = "Preencha todos os campos obrigatórios."
            divmsg1.Visible = True
            msgErro.Visible = True
            'ElseIf ddlTipoPessoa.SelectedValue <> 3 And txtInscEstadual.Text = "" Then
            '    msgErro.Text = "Preencha todos os campos obrigatórios."
            '    divmsg1.Visible = True
            '    msgErro.Visible = True
            'ElseIf ddlTipoPessoa.SelectedValue <> 3 And txtInscMunicipal.Text = "" Then
            '    msgErro.Text = "Preencha todos os campos obrigatórios."
            '    divmsg1.Visible = True
            '    msgErro.Visible = True
        ElseIf ddlTipoPessoa.SelectedValue = 0 Then
            msgErro.Text = "Preencha todos os campos obrigatórios."
            divmsg1.Visible = True
            msgErro.Visible = True
        ElseIf ddlTipoPessoa.SelectedValue <> 3 And (ddlCidade.SelectedValue = 0 Or txtEndereco.Text = "" Or txtBairro.Text = "" Or txtNumero.Text = "" Or txtCEP.Text = "") Then
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

        ElseIf ckbImportador.Checked = False And ckbExportador.Checked = False And ckbAgente.Checked = False And ckbComissaria.Checked = False And ckbArmazemDescarga.Checked = False And ckbArmazemDesembaraco.Checked = False And ckbArmazemAtracacao.Checked = False And ckbAgenteInternacional.Checked = False And ckbTransportador.Checked = False And ckbPrestador.Checked = False And ckbVendedor.Checked = False Then
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
        ElseIf txtEmail.Text <> "" And ValidaEmail.Validar(txtEmail.Text) = False Then
            msgErro.Text = "E-Mail informado na aba Email x Eventos é inválido."
            divmsg1.Visible = True
            msgErro.Visible = True
        Else
            Con.Conectar()

            If txtID.Text = "" Then

                ds = Con.ExecutarQuery("SELECT FL_CADASTRAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4  AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_CADASTRAR") <> True Then
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
ID_ACORDO_CAMBIO_MARITIMO_IMPO,
ID_ACORDO_CAMBIO_MARITIMO_EXPO,
ID_ACORDO_CAMBIO_AEREO,
QT_DIAS_FATURAMENTO,
EMAIL 
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
" & ddlAcordoCambioMaritimoImpo.SelectedValue & ",
" & ddlAcordoCambioMaritimoExpo.SelectedValue & ",
" & ddlAcordoCambioAereo.SelectedValue & ",
" & txtQtdFaturamento.Text & ",
" & txtEmailParceiro.Text & "
) Select SCOPE_IDENTITY() as ID_PARCEIRO ")




                                SQL = SQL.Replace("#filtro", FILTRO)
                                ds = Con.ExecutarQuery(SQL)
                                Dim ID_Parceiro As String = ds.Tables(0).Rows(0).Item("ID_PARCEIRO").ToString()

                                If txtNomeContato.Text = "" And txtTelContato.Text = "" And txtEmailContato.Text = "" And txtDepartamento.Text = "" Then
                                    divInformativa.Visible = True
                                    lblInformacao.Text = "Parceiro cadastrado sem informações de contato"
                                Else
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


                                If txtEmail.Text = "" And ddlPorto.SelectedValue = 0 And ddlEvento.SelectedValue = 0 Then
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
                            End If
                        End If

                    End If
                Else
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para cadastrar."
                End If

            Else

                ds = Con.ExecutarQuery("SELECT FL_ATUALIZAR FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 4 AND ID_TIPO_USUARIO = " & Session("ID_TIPO_USUARIO"))
                If ds.Tables(0).Rows.Count > 0 Then

                    If ds.Tables(0).Rows(0).Item("FL_ATUALIZAR") <> True Then
                        divmsg1.Visible = True
                        msgErro.Visible = True
                        msgErro.Text = "Usuário não possui permissão para alterar."
                    Else
                        ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CNPJ = '" & txtCNPJ.Text & "' AND ID_PARCEIRO <> " & txtID.Text)
                        If ds.Tables(0).Rows.Count > 0 And ddlTipoPessoa.SelectedValue <> 3 Then
                            msgErro.Text = "Já existe Parceiro com este CNPJ"
                            divmsg1.Visible = True
                            msgErro.Visible = True
                        Else
                            ds = Con.ExecutarQuery("SELECT ID_PARCEIRO FROM TB_PARCEIRO WHERE CPF = '" & txtCPF.Text & "' AND ID_PARCEIRO <> " & txtID.Text)
                            If ds.Tables(0).Rows.Count > 0 Then
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
FL_ARMAZEM_ATRACACAO = '" & ckbArmazemDescarga.Checked & "',
FL_ARMAZEM_DESEMBARACO = '" & ckbArmazemDesembaraco.Checked & "',
FL_ARMAZEM_DESCARGA = '" & ckbArmazemAtracacao.Checked & "',
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
ID_ACORDO_CAMBIO_MARITIMO_IMPO = " & ddlAcordoCambioMaritimoImpo.SelectedValue & ",
ID_ACORDO_CAMBIO_MARITIMO_EXPO = " & ddlAcordoCambioMaritimoExpo.SelectedValue & ",
ID_ACORDO_CAMBIO_AEREO = " & ddlAcordoCambioAereo.SelectedValue & ",
QT_DIAS_FATURAMENTO =  " & txtQtdFaturamento.Text & ",
EMAIL =  " & txtEmailParceiro.Text & "
where ID_PARCEIRO = " & ID)

                                SQL = SQL.Replace("#filtro", FILTRO)
                                Con.ExecutarQuery(SQL)

                                If txtNomeContato.Text = "" And txtTelContato.Text = "" And txtEmailContato.Text = "" And txtDepartamento.Text = "" Then

                                    Con.ExecutarQuery("DELETE FROM TB_CONTATO WHERE ID_Parceiro = " & ID)

                                Else

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

                                    Dim dsContatos As DataSet = Con.ExecutarQuery("SELECT count(ID_CONTATO)ID_CONTATO FROM TB_CONTATO WHERE ID_PARCEIRO = " & ID)


                                    If dsContatos.Tables(0).Rows(0).Item("ID_CONTATO") > 0 Then
                                        'update contatos
                                        Con.ExecutarQuery("UPDATE TB_CONTATO SET [NM_CONTATO] = " & txtNomeContato.Text & " ,[TELEFONE_CONTATO] =" & txtTelContato.Text & ",[EMAIL_CONTATO] =  " & txtEmailContato.Text & ",[NM_DEPARTAMENTO] =" & txtDepartamento.Text & ", [CELULAR_CONTATO] =" & txtCelularContato.Text & " where ID_Parceiro = " & ID)
                                    Else
                                        'insere contatos
                                        Con.ExecutarQuery("INSERT INTO TB_CONTATO ([ID_PARCEIRO],[NM_CONTATO],[TELEFONE_CONTATO],[EMAIL_CONTATO],[NM_DEPARTAMENTO],[CELULAR_CONTATO]) VALUES (" & ID & "," & txtNomeContato.Text & "," & txtTelContato.Text & "," & txtEmailContato.Text & "," & txtDepartamento.Text & ", " & txtCelularContato.Text & ")")
                                    End If

                                End If

                                If txtEmail.Text <> "" And ddlPorto.SelectedValue <> 0 And ddlEvento.SelectedValue <> 0 Then

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


                                        'update emails
                                        Con.ExecutarQuery("UPDATE [dbo].[TB_AMR_PESSOA_EVENTO] SET ID_EVENTO = " & ddlEvento.SelectedValue & ", ID_TERMINAL =" & ddlPorto.SelectedValue & ", TIPO = '" & TIPO & "', TIPO_PESSOA ='" & TIPO_PESSOA & "', ENDERECOS= '" & txtEmail.Text & "' where ID_PESSOA = " & ID)
                                    End If

                                End If
                                Call Limpar(Me)
                                Con.Fechar()
                                divmsg.Visible = True

                            End If
                        End If

                    End If
                Else
                    divmsg1.Visible = True
                    msgErro.Visible = True
                    msgErro.Text = "Usuário não possui permissão para alterar."
                End If
            End If
        End If

    End Sub

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
            divCPF.Visible = True
            divCNPJ.Visible = False
            txtCPF.Text = ""
            btnConsultaCNPJ.Visible = False
            RedCidade.Visible = True
            RedBairro.Visible = True
            RedNum.Visible = True
            RedEnd.Visible = True
            RedCEP.Visible = True
        ElseIf ddlTipoPessoa.SelectedValue = 1 Then
            divCPF.Visible = False
            divCNPJ.Visible = True
            txtCNPJ.Text = ""
            btnConsultaCNPJ.Visible = True
            RedCidade.Visible = True
            RedBairro.Visible = True
            RedNum.Visible = True
            RedEnd.Visible = True
            RedCEP.Visible = True
        ElseIf ddlTipoPessoa.SelectedValue = 3 Then

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

        If ddlTipoPessoa.SelectedValue = 3 Then
            Dim FILTRO As String = " where ID_PAIS <> 12 "
            dsCidades.SelectCommand = dsCidades.SelectCommand.Replace("#FILTRO", FILTRO)
            ddlCidade.DataBind()

        Else
            dsCidades.SelectCommand = dsCidades.SelectCommand.Replace("where ID_PAIS <> 12", "#FILTRO")
            ddlCidade.DataBind()
        End If
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
End Class