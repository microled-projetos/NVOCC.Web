Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then
            Response.Redirect("Login.aspx")

        Else

            Dim Con As New Conexao_sql

            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT (SELECT NM_RAZAO FROM TB_PARCEIRO WHERE ID_PARCEIRO = C.ID_PARCEIRO)EMPRESA,(SELECT LOGIN FROM TB_USUARIO WHERE ID_USUARIO = C.ID_USUARIO)LOGIN FROM  TB_VINCULO_USUARIO C 
WHERE C.ID_USUARIO = " & Session("ID_USUARIO") & " AND C.ID_PARCEIRO = " & Session("ID_EMPRESA"))

            If ds.Tables(0).Rows.Count > 0 Then
                Dim Nome As String = ds.Tables(0).Rows(0).Item("LOGIN").ToString()
                lbllogin.Text = Nome
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EMPRESA")) Then
                    lblEmpresa.Text = ds.Tables(0).Rows(0).Item("EMPRESA").ToString()
                End If
            End If

            Con.Fechar()
            Menus()

            'Caso seja usuario interno oculta opção de troca de perfil do menu
            If Session("Externo") = True Then
                mnTrocaPerfil.Visible = True
            ElseIf Session("Externo") = False Then
                mnTrocaPerfil.Visible = False
            End If

        End If

            lblVersion.Text = "ver " & Me.GetType.Assembly.GetName.Version.ToString

    End Sub

    Sub Menus()
        If Session("ID_TIPO_USUARIO") = "0" Then
            navbar.Visible = False
        Else

            Dim Con As New Conexao_sql
            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("SELECT 
                     M.ID_MENUS as Id,
                        M.NM_MENUS As Descricao, 
                        M.NM_OBJETO as Objeto,
                        (SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu =  M.ID_MENUS AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN (" & Session("ID_TIPO_USUARIO") & ")) As Acessar
                    FROM
        [dbo].[TB_MENUS] M
                    ORDER BY 
                        M.ID_MENUS")
            If ds.Tables(0).Rows.Count > 0 Then

                For Each linha As DataRow In ds.Tables(0).Rows
                    If linha.Item("ID").ToString() = 1 And linha.Item("Acessar").ToString() = 0 Then
                        mnUsuarios.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2 And linha.Item("Acessar").ToString() = 0 Then
                        mnGruposUsuarios.Visible = False
                    ElseIf linha.Item("ID").ToString() = 3 And linha.Item("Acessar").ToString() = 0 Then
                        mnPermissoesUsuarios.Visible = False
                    ElseIf linha.Item("ID").ToString() = 4 And linha.Item("Acessar").ToString() = 0 Then
                        mnParceiros.Visible = False
                        mnParceirosConsulta.Visible = False
                    ElseIf linha.Item("ID").ToString() = 7 And linha.Item("Acessar").ToString() = 0 Then
                        mnEmailParceiro.Visible = False
                    ElseIf linha.Item("ID").ToString() = 9 And linha.Item("Acessar").ToString() = 0 Then
                        mnMoedaFrete.Visible = False
                    ElseIf linha.Item("ID").ToString() = 10 And linha.Item("Acessar").ToString() = 0 Then
                        mnMoedaFreteArmador.Visible = False
                    ElseIf linha.Item("ID").ToString() = 22 And linha.Item("Acessar").ToString() = 0 Then
                        mnItemDespesa.Visible = False
                    ElseIf linha.Item("ID").ToString() = 24 And linha.Item("Acessar").ToString() = 0 Then
                        mnFreteTransportador.Visible = False
                    ElseIf linha.Item("ID").ToString() = 1025 And linha.Item("Acessar").ToString() = 0 Then
                        mnCotacaoComercial.Visible = False
                    ElseIf linha.Item("ID").ToString() = 1026 And linha.Item("Acessar").ToString() = 0 Then
                        mnOperacionalListagemBL.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2026 And linha.Item("Acessar").ToString() = 0 Then
                        mnConsultarWeek.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2027 And linha.Item("Acessar").ToString() = 0 Then
                        mnFinanceiro.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2029 And linha.Item("Acessar").ToString() = 0 Then
                        mnComissaoVendedor.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2030 And linha.Item("Acessar").ToString() = 0 Then
                        mnComissaoIndicadorNacional.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2031 And linha.Item("Acessar").ToString() = 0 Then
                        mnComissaoIndicadorInternacional.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2032 And linha.Item("Acessar").ToString() = 0 Then
                        mnAccount.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2033 And linha.Item("Acessar").ToString() = 0 Then
                        mnCourrier.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2034 And linha.Item("Acessar").ToString() = 0 Then
                        mnModuloDemurrage.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2035 And linha.Item("Acessar").ToString() = 0 Then
                        mnFechamentoCambio.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2028 And linha.Item("Acessar").ToString() = 0 Then
                        mnFaturamento.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2036 And linha.Item("Acessar").ToString() = 0 Then
                        mnModuloGerencial.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2037 And linha.Item("Acessar").ToString() = 0 Then
                        mnTOTVSDespesa.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2038 And linha.Item("Acessar").ToString() = 0 Then
                        mnTOTVSPA.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2039 And linha.Item("Acessar").ToString() = 0 Then
                        mnTOTVSDebit.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2040 And linha.Item("Acessar").ToString() = 0 Then
                        mnTOTVSCredit.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2041 And linha.Item("Acessar").ToString() = 0 Then
                        mnTOTVSServico.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2042 And linha.Item("Acessar").ToString() = 0 Then
                        mnDashBoard.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2043 And linha.Item("Acessar").ToString() = 0 Then
                        mnGerencialMaster.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2045 And linha.Item("Acessar").ToString() = 0 Then
                        mnModuloOperacional.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2046 And linha.Item("Acessar").ToString() = 0 Then
                        mnRelacaoCotacao.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2047 And linha.Item("Acessar").ToString() = 0 Then
                        mnContaPagarReceber.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2048 And linha.Item("Acessar").ToString() = 0 Then
                        mnEstimativaContaPagarReceber.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2049 And linha.Item("Acessar").ToString() = 0 Then
                        mnAtendimentoNegado.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2050 And linha.Item("Acessar").ToString() = 0 Then
                        mnCaixaSaida.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2051 And linha.Item("Acessar").ToString() = 0 Then
                        mnRelatorioInvoice.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2052 And linha.Item("Acessar").ToString() = 0 Then
                        mnInvoiceQuitada.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2053 And linha.Item("Acessar").ToString() = 0 Then
                        mnDemonstrativoRateio.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2054 And linha.Item("Acessar").ToString() = 0 Then
                        mnPremiacao.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2055 And linha.Item("Acessar").ToString() = 0 Then
                        mnTOTVSDemurrageRA.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2056 And linha.Item("Acessar").ToString() = 0 Then
                        mnTOTVSDemurragePA.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2057 And linha.Item("Acessar").ToString() = 0 Then
                        mnComissaoTransportadora.Visible = False
                    End If



                Next
            End If
            Con.Fechar()


            If mnUsuarios.Visible = False And mnGruposUsuarios.Visible = False And mnPermissoesUsuarios.Visible = False Then
                MenuUsuario.Visible = False
            End If

            If mnParceiros.Visible = False And mnParceirosConsulta.Visible = False And mnEmailParceiro.Visible = False Then
                MenuParceiro.Visible = False
            End If

            If MenuUsuario.Visible = False And MenuParceiro.Visible = False And mnMoedaFrete.Visible = False And mnMoedaFreteArmador.Visible = False And mnItemDespesa.Visible = False Then
                MenuCadastros.Visible = False
            End If


            If mnComissaoVendedor.Visible = False Then
                MenuComissoes2.Visible = False
            End If

            If mnRelacaoCotacao.Visible = False Then
                MenuRelatoriosComercial.Visible = False
            End If

            If mnFreteTransportador.Visible = False And mnCotacaoComercial.Visible = False And mnComissaoVendedor.Visible = False And MenuRelatoriosComercial.Visible = False And mnAtendimentoNegado.Visible = False And MenuComissoes2.Visible = False Then
                MenuComercial.Visible = False
            End If

            If mnOperacionalListagemBL.Visible = False And mnCourrier.Visible = False And mnConsultarWeek.Visible = False Then
                MenuOperacao.Visible = False
            End If


            If mnTOTVSServico.Visible = False Then
                MenuTotvs2.Visible = False
            End If

            If MenuTotvs2.Visible = False And mnFaturamento.Visible = False Then
                MenuFaturamento.Visible = False
            End If

            If mnDashBoard.Visible = False And mnGerencialMaster.Visible = False And mnModuloGerencial.Visible = False And mnModuloOperacional.Visible = False Then
                MenuGerencial.Visible = False
            End If


            If mnEstimativaContaPagarReceber.Visible = False And mnContaPagarReceber.Visible = False And mnRelatorioInvoice.Visible = False And mnInvoiceQuitada.Visible = False And mnDemonstrativoRateio.Visible = False And mnPremiacao.Visible = False Then
                MenuRelatoriosFinanceiros.Visible = False
            End If

            If mnAccount.Visible = False And mnFechamentoCambio.Visible = False Then
                MenuAccount.Visible = False
            End If

            If mnComissaoIndicadorNacional.Visible = False And mnComissaoIndicadorInternacional.Visible = False And mnComissaoTransportadora.Visible = False Then
                MenuComissoes.Visible = False
            End If

            If mnTOTVSCredit.Visible = False And mnTOTVSDebit.Visible = False And mnTOTVSDespesa.Visible = False And mnTOTVSPA.Visible = False And mnTOTVSDemurrageRA.Visible = False And mnTOTVSDemurragePA.Visible = False Then
                MenuTotvs.Visible = False
            End If

            If mnFinanceiro.Visible = False And mnModuloDemurrage.Visible = False And MenuAccount.Visible = False And MenuComissoes.Visible = False And MenuTotvs.Visible = False And MenuRelatoriosFinanceiros.Visible = False And mnCaixaSaida.Visible = False Then
                MenuFinanceiro.Visible = False
            End If
        End If


    End Sub

    'Protected Sub Unnamed_AsyncPostBackError(sender As Object, e As AsyncPostBackErrorEventArgs)
    '    Response.Redirect("Default.aspx")
    'End Sub
End Class