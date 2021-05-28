Imports System.Runtime.InteropServices
Imports System.Text

Public Class FrmPrincipal

    Private Sub mnAgencias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadAgencias.Click
        FrmAgencias.Show()
    End Sub

    Private Sub FrmPrincipal_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Try

            Dim IP() As System.Net.IPAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())
            Me.lblData.Text = Now.Date
            Me.lblEstacao.Text = My.Computer.Name
            Me.lblBase.Text = Banco.Servidor
            Me.lblIP.Text = IP(1).ToString()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FrmPrincipal_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        Try
            Dim ItemProcess() As Process = Process.GetProcessesByName("FontePagadora")
            If Not ItemProcess Is Nothing Then
                For Each SubProcess As Process In ItemProcess
                    SubProcess.Kill()
                Next
            End If
        Catch ex As Exception
            Environment.Exit(0)
        End Try

        Environment.Exit(0)

    End Sub

    Private Sub mnArmadores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadArmadores.Click
        FrmArmadores.Show()
    End Sub

    Private Sub mnAvarias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MuCadAvarias.Click
        FrmAvarias.Show()
    End Sub

    Private Sub mnGuias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGuias.Click
        FrmGuias.Show()
    End Sub

    Private Sub mnCidades_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCadCidades.Click
        FrmCidades.Show()
    End Sub

    Private Sub mnComunicadosExtratos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadComunicados.Click
        FrmAvisoExtrato.Show()
    End Sub

    Private Sub mnDelegacias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCadDelegacias.Click
        FrmDelegacias.Show()
    End Sub

    Private Sub mnEmailAlteraçãoIndicador_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadEmailVinculo.Click
        FrmEmailIndicador.Show()
    End Sub

    Private Sub mnEmailCálculo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuemailcalc.Click
        FrmEmailCalculo.Show()
    End Sub

    Private Sub mnEmailEscolta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadEmailEscolta.Click
        FrmEmailEscolta.Show()
    End Sub

    Private Sub mnEmailDesova_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuEmailDesova.Click
        FrmEmailDesova.Show()
    End Sub

    Private Sub mnEmbalagens_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadEmbalagens.Click
        FrmEmbalagens.Show()
    End Sub

    Private Sub mnEmpresas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuEmpresa.Click
        FrmEmpresas.Show()
    End Sub

    Private Sub mnFaixaLimiteINSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuLimINSS.Click
        FrmLimiteINSS.Show()
    End Sub

    Private Sub mnImportoDeRenda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuIR.Click
        FrmIR.Show()
    End Sub

    Private Sub mnMotoristas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCadMotoristas.Click
        FrmMotoristas.Show()
    End Sub

    Private Sub mnNavios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadNavios.Click
        FrmNavios.Show()
    End Sub

    Private Sub mnPaíses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FrmPaises.Show()
    End Sub

    Private Sub mnParâmetrosEDIDespachantes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadEdi.Click
        FrmParametrosEDI.Show()
    End Sub

    Private Sub mnPortos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadPortos.Click
        FrmPortos.Show()
    End Sub

    Private Sub MotivosDeAberturaDeContêinerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadMotivos.Click
        FrmMotivos.Show()
    End Sub

    Private Sub MotivosDeAgendamentoDePosicionamentoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadMotivosPosicao.Click
        FrmMotivosPos.Show()
    End Sub

    Private Sub MotivosDeBloqueioManualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCadMotivoBloqueio.Click
        FrmMotivoBloqueio.Show()
    End Sub

    Private Sub MotivosDeDescontoComercialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuMotivoDesc.Click
        FrmMotivoDesconto.Show()
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuParceiros.Click
        FrmParceiros.Show()
    End Sub

    Private Sub mnCalculadora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnCalculadora.Click
        System.Diagnostics.Process.Start("calc.exe")
    End Sub

    Private Sub mnAnotacoes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnAnotacoes.Click
        System.Diagnostics.Process.Start("notepad.exe")
    End Sub

    Private Sub mnDataHora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnNavegador.Click
        System.Diagnostics.Process.Start("http://www.ecoportosantos.com.br/")
    End Sub

    Private Sub mnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Mnusair.Click
        Environment.Exit(0)
    End Sub

    Private Sub mnEventos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadEventos.Click
        FrmEventos.Show()
    End Sub

    Private Sub mnProdutos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadProdutos.Click
        FrmProdutos.Show()
    End Sub

    Private Sub mnProvidencias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadProvidências.Click
        FrmProvidencias.Show()
    End Sub

    Private Sub mnRecintos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadRecintos.Click
        FrmRecintos.Show()
    End Sub

    Private Sub mnRepresentantes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadRepresentantes.Click
        FrmRepresentantes.Show()
    End Sub

    Private Sub mnSeriesDeNF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCadSerieNF.Click
        FrmSeriesNF.Show()
    End Sub

    Private Sub mnStatusDeMadeira_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCadStatusMadeira.Click
        FrmStatusMadeira.Show()
    End Sub

    Private Sub mnTabelaDeComissoesDeVendedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuTabComVend.Click
        FrmComissao.Show()
    End Sub

    Private Sub mnTiposDeDocumentos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadTiposDocumentos.Click
        FrmTiposDocumentos.Show()
    End Sub

    Private Sub MnTiposDeConteineres_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadTiposDeConteiner.Click
        FrmTiposConteiner.Show()
    End Sub

    Private Sub mnEstados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCadUFs.Click
        FrmUF.Show()
    End Sub

    Private Sub mnTransportadoras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadTransportadoras.Click
        FrmTransportadoras.Show()
    End Sub

    Private Sub mnGrupos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FrmGrupos.Show()
    End Sub

    Private Sub mnUsuarios1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadUsuarios.Click
        FrmUsuarios.Show()
    End Sub

    Private Sub mnPermissoes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCadPermissoes.Click
        FrmAcessos.Show()
    End Sub

    Private Sub mnEmpresa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuEscolha_Empresa.Click

        'For i As Integer = My.Application.OpenForms.Count - 1 To 0 Step -1
        '    If My.Application.OpenForms.Item(i).Name <> "FrmPrincipal" And My.Application.OpenForms.Item(i).Name <> "FrmLogin" And My.Application.OpenForms.Item(i).Name <> "FrmSplash" Then
        '        My.Application.OpenForms.Item(i).Close()
        '    End If
        'Next i

        Me.mnPrincipal.Enabled = False
        FrmTrocaEmpresa.Show()

    End Sub

    Private Sub FrmPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = Me.lblEmpresa.Text & " :: Cadastro - Versão: " & Application.ProductVersion.ToString()

        For Each MenuPrincipal As ToolStripMenuItem In mnPrincipal.Items
            If Not UsuarioTemAcesso(MenuPrincipal.Name) Then
                MenuPrincipal.Visible = False
            End If
            For Each ItemMenu As ToolStripMenuItem In MenuPrincipal.DropDownItems
                If ItemMenu.DropDownItems.Count > 0 Then
                    For Each SubMenu As ToolStripMenuItem In ItemMenu.DropDownItems
                        If Not UsuarioTemAcesso(SubMenu.Name) Then
                            SubMenu.Visible = False
                        End If
                        If Not UsuarioTemAcesso(ItemMenu.Name) Then
                            ItemMenu.Visible = False
                        End If
                    Next
                Else
                    If Not UsuarioTemAcesso(ItemMenu.Name) Then
                        ItemMenu.Visible = False
                    End If
                End If
            Next
        Next

    End Sub

    Private Function UsuarioTemAcesso(ByVal NomeMenu As String) As Boolean

        Dim SQL As New StringBuilder

        SQL.Append("Select ")
        SQL.Append("  COUNT(1) CONTAR ")
        SQL.Append("FROM ")
        SQL.Append(" TB_SYS_FUNCOES, ")
        SQL.Append(" TB_SYS_GRP_PERMISSOES, ")
        SQL.Append(" TB_SYS_USER_GRUPOS, ")
        SQL.Append(" TB_CAD_USUARIOS ")
        SQL.Append("WHERE ")
        SQL.Append("  ( ")
        SQL.Append("    (TB_CAD_USUARIOS.AUTONUM = TB_SYS_USER_GRUPOS.AUTONUMUSER) ")
        SQL.Append("  And ")
        SQL.Append("    (TB_SYS_FUNCOES.CODFUNC = TB_SYS_GRP_PERMISSOES.CODFUNC) ")
        SQL.Append(" And ")
        SQL.Append("    (TB_SYS_GRP_PERMISSOES.CODGRUPO = TB_SYS_USER_GRUPOS.CODGRUPO) ")
        SQL.Append(" And ")
        SQL.Append("    (TB_CAD_USUARIOS.USUARIO = '" & Me.lblUsuario.Text.ToUpper() & "') ")
        SQL.Append(" AND ")
        SQL.Append("    (UPPER(TB_SYS_FUNCOES.SISTEMA) = '" & My.Application.Info.ProductName.ToUpper() & "') ")
        SQL.Append(" AND ")
        SQL.Append("    (UPPER(TB_SYS_FUNCOES.NOMEOBJ) = '" & NomeMenu.ToUpper() & "') ")
        SQL.Append("  ) ")

        If Convert.ToInt32(Banco.ExecuteScalar(SQL.ToString())) > 0 Then
            Return True
        End If
        Dim NUMFUNC As Long
        Dim AUX As Byte
        NUMFUNC = Convert.ToInt32(Banco.ExecuteScalar("select count(codfunc)+ 1 as CodFunc from TB_SYS_FUNCOES"))

        Banco.ExecuteScalar("INSERT into TB_SYS_FUNCOES (codfunc, nomeobj, captionobj,sistema) values (" & NUMFUNC & ", '" & NomeMenu.ToUpper() & "', '" & NomeMenu.ToUpper() & "', '" & My.Application.Info.ProductName.ToUpper() & "')")
        For Aux = 1 To 5
            Banco.ExecuteScalar("INSERT INTO  tb_sys_grp_permissoes (codfunc, codgrupo, codtipoperm) VALUES (" & NUMFUNC & ", 1," & AUX & ")")
        Next

        Return True

    End Function

    Private Sub mnPaises_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MNUCADPAISES.Click
        FrmPaises.Show()
    End Sub

    Private Sub mnRelCalculoAutomatico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCalcAutomatico.Click
        FrmRelCalculoAutomatico.Show()
    End Sub

    Private Sub mnConsultaAGRs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCGR.Click
        FrmRelConsultaGrs.Show()
    End Sub

    Private Sub mnFechamentoDeCaixa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFCGR.Click
        FrmRelFechamentoCaixa.Show()
    End Sub

    Private Sub mnListagemDasGrsVencidas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuGrVencida.Click
        FrmRelGrVencida.Show()
    End Sub

    Private Sub mnListagemDeGrs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFechamentoDeCaixa.Click
        FrmRelListagemGr.Show()
    End Sub

    Private Sub ListagemDeGRsSemCobrançaDeServiçoEspecificoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRelSemServico.Click
        FrmRelGrSemServicos.Show()
    End Sub

    Private Sub PorTipoDeServiçoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuTipoServico.Click
        FrmRelPorTipoServico.Show()
    End Sub

    Private Sub ReceitaTotalPlanilhaExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuReceitaTot.Click
        FrmRelReceitaTotal.Show()
    End Sub

    Private Sub ViagemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuViagens.Click
        FrmViagens.Show()
    End Sub

    Private Sub RelatórioDeGRsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGRAFR.Click
        FrmRelRelacaoGR.Show()
    End Sub

    Private Sub ConfigurarEmailDeEnvioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuEmailEnvio.Click

        Dim Email(3) As String
        Email(1) = ""
        Email(2) = ""
        Email(3) = ""

        Dim Sql As String

        Email(1) = InputBox("Informe o 1o. E-Mail para atualizacao")
        If Email(1) = "" Then Exit Sub

        If Email(1) <> "" Then
            Email(2) = InputBox("Informe o 2o. E-Mail para atualizacao")
            If Email(2) <> "" Then
                Email(3) = InputBox("Informe o 3o. E-Mail para atualizacao")
            End If
        End If

        Sql = "update " & Banco.BancoSGIPA & "tb_parametros set"
        Sql = Sql & " email='" & Email(1) & "'"
        If Email(2) = "" Then
            Sql = Sql & " ,email2=null"
        Else
            Sql = Sql & " ,email2='" & Email(2) & "'"
        End If
        If Email(3) = "" Then
            Sql = Sql & " ,email3=null"
        Else
            Sql = Sql & " ,email3='" & Email(3) & "'"
        End If

        Banco.Execute(Sql)  'z4

        MsgBox("Atualizacao Realizada")

    End Sub

    Private Sub RelatórioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_saidas_sem_gr.Click

        Dim wordem2 As String
        Dim wordem As String
        Dim wvalidade As String
        Dim NomeRelatorio As String
        Dim Rs As New DataTable
        Dim Sql As String

        Do
            wordem = InputBox("Selecione a 1º Ordem do Relatório" & Chr(13) & Chr(13) & "1-Ordem de Data de Saída" & Chr(13) & "2-Ordem de Gr" & Chr(13) & "3-Ordem de Importador", , 1)
            If wordem = "" Then Exit Sub
        Loop Until wordem = "1" Or wordem = "2" Or wordem = "3"
        Do
            wordem2 = InputBox("Selecione a 2º Ordem do Relatório" & Chr(13) & Chr(13) & "1-Contêiner" & Chr(13) & "2-Carga Solta", , 1)
            If wordem2 = "" Then Exit Sub
        Loop Until wordem2 = "1" Or wordem2 = "2"

        Banco.Execute("delete from " & Banco.BancoSGIPA & "temp_grs_faturadas where maquina_rede='" & Banco.MaquinaRede & "'")
        'seleciona os registros sem GR

        Sql = "insert into " & Banco.BancoSGIPA & "temp_grs_faturadas"
        Sql = Sql & " SELECT"
        Sql = Sql & "     TB_SAIDAS_GRS_PENDENTES.BL, TB_SAIDAS_GRS_PENDENTES.CNTR, TB_SAIDAS_GRS_PENDENTES.CS,"
        Sql = Sql & "     TB_ORDEM_CARREGAMENTO.SEQ, TB_ORDEM_CARREGAMENTO.ANO,"
        Sql = Sql & "     TB_BL.NUM_DOCUMENTO,"
        Sql = Sql & "     TB_CNTR_BL.ID_CONTEINER,"
        Sql = Sql & "     0,"
        Sql = Sql & "     CAD_DESPACHANTE.RAZAO,"
        Sql = Sql & "     TB_CAD_PARCEIROS.RAZAO,"
        Sql = Sql & "     TB_TIPOS_DOCUMENTOS.DESCR,"
        Sql = Sql & "     tb_ordem_carregamento.DT_SAIDA"
        Sql = Sql & "     ,'" & Banco.MaquinaRede & "',TB_BL.FlAG_NEGOCIACAO"

        'CODIGO NOVO
        Sql = Sql & "  From"
        Sql = Sql & "     " & Banco.BancoSGIPA & "TB_SAIDAS_GRS_PENDENTES TB_SAIDAS_GRS_PENDENTES"
        Sql = Sql & "     INNER JOIN " & Banco.BancoSGIPA & "TB_ORDEM_CARREGAMENTO TB_ORDEM_CARREGAMENTO "
        Sql = Sql & "             ON TB_SAIDAS_GRS_PENDENTES.ORDEM = TB_ORDEM_CARREGAMENTO.AUTONUM "
        Sql = Sql & "     INNER JOIN " & Banco.BancoSGIPA & "TB_BL TB_BL "
        Sql = Sql & "             ON  TB_SAIDAS_GRS_PENDENTES.BL = TB_BL.AUTONUM "
        Sql = Sql & "     LEFT JOIN " & Banco.BancoSGIPA & "TB_CNTR_BL TB_CNTR_BL "
        Sql = Sql & "            ON  TB_SAIDAS_GRS_PENDENTES.CNTR = TB_CNTR_BL.AUTONUM"
        Sql = Sql & "     LEFT JOIN "
        Sql = Sql & "           (Select bl from " & Banco.BancoSGIPA & "TB_GR_BL "
        Sql = Sql & "              where TB_GR_BL.STATUS_GR = 'IM' or TB_GR_BL.STATUS_GR = 'GE') TB_GR_BL "
        Sql = Sql & "              ON TB_SAIDAS_GRS_PENDENTES.BL = TB_GR_BL.BL"

        Sql = Sql & "     INNER JOIN " & Banco.BancoSGIPA & "TB_CAD_PARCEIROS CAD_DESPACHANTE "
        Sql = Sql & "             ON  TB_BL.DESPACHANTE = CAD_DESPACHANTE.AUTONUM"
        Sql = Sql & "     INNER JOIN " & Banco.BancoSGIPA & "TB_CAD_PARCEIROS TB_CAD_PARCEIROS "
        Sql = Sql & "             ON TB_BL.IMPORTADOR = TB_CAD_PARCEIROS.AUTONUM "
        Sql = Sql & "     INNER JOIN " & Banco.BancoSGIPA & "TB_TIPOS_DOCUMENTOS TB_TIPOS_DOCUMENTOS "
        Sql = Sql & "             ON TB_BL.TIPO_DOCUMENTO = TB_TIPOS_DOCUMENTOS.CODE "
        Sql = Sql & "  Where"
        Sql = Sql & "     TB_ORDEM_CARREGAMENTO.DT_SAIDA is not null and "
        Sql = Sql & "     ISNULL(tb_gr_bl.bl,0)=0"
        Sql = Sql & " AND TB_BL.patio in (1,2,3,4,5) "
        'FIM CODIGO NOVO

        Banco.Execute(Sql)  'z4

        Sql = ""
        Sql = Sql & "SELECT tb_saidas_grs_pendentes.bl, tb_saidas_grs_pendentes.cntr, "
        Sql = Sql & "       tb_saidas_grs_pendentes.cs, tb_ordem_carregamento.seq, "
        Sql = Sql & "       tb_ordem_carregamento.ano, tb_bl.num_documento, "
        Sql = Sql & "       tb_cntr_bl.id_conteiner, cad_despachante.razao despachante, "
        Sql = Sql & "       tb_cad_parceiros.razao importador, "
        Sql = Sql & "       tb_tipos_documentos.descr tipo_documento, "
        Sql = Sql & "       tb_ordem_carregamento.dt_saida, tb_bl.flag_negociacao, "
        Sql = Sql & "       TB_GR.SEQ_GR, "
        Sql = Sql & "       TB_GR.VALIDADE, "
        Sql = Sql & "       ISNULL(TB_GR.VALIDADE_REEFER,GETDATE()) VALIDADE_REEFER, "
        Sql = Sql & "       TB_GATE.SAIDA_NEW "

        Sql = Sql & "        "

        'CODIGO NOVO
        Sql = Sql & "  FROM " & Banco.BancoSGIPA & "tb_saidas_grs_pendentes "
        Sql = Sql & "       INNER JOIN " & Banco.BancoSGIPA & "tb_ordem_carregamento "
        Sql = Sql & "               ON  tb_saidas_grs_pendentes.ordem = tb_ordem_carregamento.autonum "
        Sql = Sql & "       INNER JOIN " & Banco.BancoSGIPA & "tb_bl "
        Sql = Sql & "               ON tb_saidas_grs_pendentes.bl = tb_bl.autonum  "
        Sql = Sql & "        LEFT JOIN " & Banco.BancoSGIPA & "tb_cntr_bl "
        Sql = Sql & "               ON tb_saidas_grs_pendentes.cntr = tb_cntr_bl.autonum "
        Sql = Sql & "       INNER JOIN " & Banco.BancoSGIPA & "tb_cad_parceiros cad_despachante "
        Sql = Sql & "               ON  tb_bl.despachante = cad_despachante.autonum  "
        Sql = Sql & "       INNER JOIN " & Banco.BancoSGIPA & "tb_cad_parceiros tb_cad_parceiros "
        Sql = Sql & "               ON tb_bl.importador = tb_cad_parceiros.autonum  "
        Sql = Sql & "       INNER JOIN " & Banco.BancoSGIPA & "tb_tipos_documentos "
        Sql = Sql & "               ON   tb_bl.tipo_documento = tb_tipos_documentos.code "

        Sql = Sql & "       INNER JOIN "
        Sql = Sql & "             (SELECT BL, MAX (seq_gr) seq_gr,MAX (validade_gr) as Validade ,"
        Sql = Sql & "              MAX (dt_base_calculo_reefer) Validade_reefer "
        Sql = Sql & "              FROM " & Banco.BancoSGIPA & "tb_gr_bl "
        Sql = Sql & "              WHERE  (tb_gr_bl.status_gr = 'IM' OR tb_gr_bl.status_gr = 'GE')"
        Sql = Sql & "              GROUP BY BL )tb_gr "
        Sql = Sql & "          ON tb_bl.autonum=tB_gr.bl   "

        Sql = Sql & "       INNER JOIN (SELECT  b.id_oc,MAX(a.dt_gate_out) AS Saida_New "
        Sql = Sql & "                   FROM " & Banco.BancoOPERADOR & "tb_gate_new a "
        Sql = Sql & "                   INNER JOIN " & Banco.BancoOPERADOR & "tb_amr_gate b ON  A.AUTONUM=B.GATE  "
        Sql = Sql & "                   WHERE  ISNULL(b.historico,0)=0   and b.id_oc>0 "
        Sql = Sql & "                   GROUP BY  b.id_oc ) TB_GATE "
        Sql = Sql & "            ON tb_ordem_carregamento.Autonum=Tb_Gate.Id_Oc   "
        Sql = Sql & " WHERE "
        Sql = Sql & "   dbo.TO_CHAR(tb_ordem_carregamento.dt_saida,'YYYYMMDD HH24MISS') >dbo.TO_CHAR(TB_GR.VALIDADE+1,'YYYYMMDD') "
        Sql = Sql & " AND TB_BL.patio in (1,2,3,4,5) "

        'FIM CODIGO NOVO

        Dim wSaida


        Rs = Banco.List(Sql)

        For Each LinhaRs As DataRow In Rs.Rows
            '08/01/2006
            ' ===
            If Not String.IsNullOrEmpty(LinhaRs("VALIDADE_REEFER").ToString()) Then
                wvalidade = LinhaRs("VALIDADE").ToString()
            Else
                wvalidade = LinhaRs("VALIDADE_REEFER").ToString()
            End If
            '        wSaida = Nnull(LinhaRs("Saida, 1)
            '       If string.IsnullOrEmpty(LinhaRs("Saida) Then
            wSaida = NNull(LinhaRs("SAIDA_NEW").ToString(), 1)
            '      End If
            If NNull(wSaida, 1) <> "" Then
                If Convert.ToDateTime(wSaida) > Convert.ToDateTime(LinhaRs("VALIDADE").ToString()) Then
                    If Convert.ToDateTime(wSaida) <= "06:59:59" Then
                        wvalidade = Convert.ToDateTime(LinhaRs("VALIDADE").ToString()).AddDays(1) & "  06:59:59"
                    End If
                End If
            End If
            If NNull(wSaida, 1) <> "" Then
                If Convert.ToDateTime(wSaida) > Convert.ToDateTime(wvalidade) Then
                    Sql = "insert into " & Banco.BancoSGIPA & "temp_grs_faturadas"
                    Sql = Sql & "  (BL,CNTR,CS,SEQ,ANO,NUM_DOCUMENTO,ID_CONTEINER,SEQ_GR,DESPACHANTE,"
                    Sql = Sql & "  Importador,TIPO_DOCUMENTO,DT_SAIDA,Maquina_Rede,Flag_Negociacao)"
                    Sql = Sql & "  values ("
                    Sql = Sql & "'" & LinhaRs("BL").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("cntr").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("cs").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("SEQ").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("Ano").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("Num_Documento").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("Id_Conteiner").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("seq_gr").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("Despachante").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("Importador").ToString() & "'"
                    Sql = Sql & ",'" & LinhaRs("Tipo_Documento").ToString() & "'"
                    Sql = Sql & ",dbo.TO_DATE('" & wSaida & "','dd/mm/yyyy hh24:mi:ss')"
                    Sql = Sql & ",'" & Banco.MaquinaRede & "'," & NNull(LinhaRs("flag_negociacao").ToString(), 0)
                    Sql = Sql & ")"
                    Banco.Execute(Sql)  'z4
                End If
            End If
        Next

        NomeRelatorio = "\RPTS\registro_gr_pendente.RPT"
        Sql = String.Empty
        Sql = " SELECT TEMP_GRS_FATURADAS.BL, TEMP_GRS_FATURADAS.NUM_DOCUMENTO, TEMP_GRS_FATURADAS.TIPO_DOCUMENTO, TEMP_GRS_FATURADAS.ID_CONTEINER, TEMP_GRS_FATURADAS.SEQ_GR, TEMP_GRS_FATURADAS.DT_SAIDA, TEMP_GRS_FATURADAS.IMPORTADOR, TEMP_GRS_FATURADAS.DESPACHANTE, TEMP_GRS_FATURADAS.CNTR, TEMP_GRS_FATURADAS.CS, TEMP_GRS_FATURADAS.FLAG_NEGOCIACAO"
        Sql = Sql & "  FROM   " & Banco.BancoSGIPA & "TEMP_GRS_FATURADAS TEMP_GRS_FATURADAS"
        Sql = Sql & " WHERE temp_grs_faturadas.maquina_rede='" & Banco.MaquinaRede & "'"

        Select Case wordem
            Case 1
                Sql = Sql & " order by temp_grs_faturadas.dt_saida "
            Case 2
                Sql = Sql & " order by temp_grs_faturadas.seq_gr "
            Case 3
                Sql = Sql & " order by temp_grs_faturadas.importador "
        End Select

        Dim formulas As List(Of String) = New List(Of String)
        Dim valores As List(Of String) = New List(Of String)

        Select Case wordem2
            Case 1
                formulas.Add("tipo_carga_formula")
                valores.Add("1")
            Case 2
                formulas.Add("tipo_carga_formula")
                valores.Add("2")
        End Select

        Dim rptName As String = ""
        Dim query As String = ""

        rptName = NomeRelatorio
        query = Sql

        Using frmRep As FrmCrystal = New FrmCrystal(Application.StartupPath & "\" & rptName, 0, query, formulas, valores)
            frmRep.ShowDialog()
        End Using

    End Sub

    Private Sub SolicitaçõesDepComercialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuRelSolCom.Click
        FrmRelSolicitacoesComerciais.Show()
    End Sub

    Private Sub TempoDeAverbaçãoECálculoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuTempoAverbacaoCalculo.Click
        FrmRelTempAverCalc.Show()
    End Sub

    Private Sub RelatórioDeCálculoPosicionamentoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuPosiCalc.Click
        FrmRelRecalculoPosicionamento.Show()
    End Sub

    Private Sub ValoresEmAbertoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuValoresAberto.Click
        FrmRelValoresEmAberto.Show()
    End Sub

    Private Sub TempoDePresençaDeCargaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuTempoPresencaCarga.Click
        FrmRelTempoPresencaDeCarga.Show()
    End Sub

    Private Sub TempoDeDesconsolidaçãoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuTempoDesconsolidacao.Click
        FrmRelTempoDesconsolidacao.Show()
    End Sub

    Private Sub ComissõesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCInd.Click
        FrmRelComissoesIndicador.Show()
    End Sub

    Private Sub ComissõesPorVendedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuVendedorComissao.Click
        FrmRelComissoesVendedor.Show()
    End Sub

    Private Sub AlteraçãoFreeTimeFimDePeríodoDaGRToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuadmingr.Click
        FrmGrNaoImpressas.Show()
    End Sub

    Private Sub CancelamentoDeGRsImpressasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCancelaGRImpressa.Click
        FrmCancelamentoGrImpressa.Show()
    End Sub

    Private Sub HistóricoDeAlteraçõesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuVinculo.Click
        FrmRelHistoricoAlteracoes.Show()
    End Sub

    Private Sub RelatórioDeGruposToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuRelGrupos.Click
        FrmRelGrupos.Show()
    End Sub

    Private Sub TabelasDeCobrançaNãoUtilizadasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTCNaoUlilizada.Click
        FrmRelTabelasCobrancaNaoUtilizadas.Show()
    End Sub

    Private Sub mnProcessosJuridicos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuProcJur.Click
        FrmRelProcessosJuridicos.Show()
    End Sub

    Private Sub mnRelatorioDeGRsPagas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGRsPagas.Click
        FrmRelGrsPagas.Show()
    End Sub

    Private Sub RelatórioDeTabelasDeCobrançaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuTabelaCobr.Click
        FrmRelTabelasCobranca.Show()
    End Sub

    Private Sub mnRelatorioDeVerificacaoDeTabelas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuVerificaTab.Click
        FrmRelVerificacaoTabelas.Show()
    End Sub

    Private Sub MnuHistVinculo_Click(sender As Object, e As EventArgs) Handles MnuHistVinculo.Click
        FrmRelHistoricoVinculos.Show()
    End Sub

    Private Sub nmNCM_Click(sender As Object, e As EventArgs) Handles nmNCM.Click
        FrmNCM.Show()
    End Sub

    Private Sub mnGrupos_Click_1(sender As Object, e As EventArgs) Handles mnGrupos.Click
        FrmGrupos.Show()
    End Sub

    Private Sub CargasPerigosasIMOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CargasPerigosasIMOToolStripMenuItem.Click
        FrmCargasPerigosasIMO.Show()
    End Sub

    Private Sub CargasPerigosasONUToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CargasPerigosasONUToolStripMenuItem.Click
        FrmCargasPerigosasONU.Show()
    End Sub

    Private Sub mnCondicoesPagamento_Click(sender As Object, e As EventArgs) Handles mnCondicoesPagamento.Click
        FrmTiposPgto.Show()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
