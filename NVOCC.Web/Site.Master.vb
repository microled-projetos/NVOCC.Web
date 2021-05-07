﻿Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Logado") = "False" Or Session("Logado") = Nothing Then
            Response.Redirect("Login.aspx")

        Else

            Dim Con As New Conexao_sql

            Con.Conectar()
            Dim ds As DataSet = Con.ExecutarQuery("select Login from TB_USUARIO where ID_USUARIO = " & Session("ID_USUARIO") & "")

            If ds.Tables(0).Rows.Count > 0 Then
                Dim Nome As String = ds.Tables(0).Rows(0).Item("Login").ToString()
                lbllogin.Text = Nome

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

    'Sub SeparaUsuario(ByVal ID_TIPO_USUARIO As String)
    '    Dim ID_TIPO_USUARIO_JUNTO As String = ID_TIPO_USUARIO
    '    'quebrar a string
    '    Dim palavras As String() = ID_TIPO_USUARIO.Split(New String() _
    '      {","}, StringSplitOptions.RemoveEmptyEntries)

    '    'exibe o resultado
    '    For i As Integer = 0 To palavras.GetUpperBound(0) Step 1
    '        Session("ID_TIPO_USUARIO") = palavras(i)
    '        MenusInterno()
    '    Next
    '    Session("ID_TIPO_USUARIO") = ID_TIPO_USUARIO_JUNTO
    'End Sub
    'Sub MenusExterno()
    '    If Session("ID_TIPO_USUARIO") = 0 Then
    '        navbar.Visible = False
    '    Else

    '        Dim Con As New Conexao_sql
    '        Con.Conectar()
    '        Dim ds As DataSet = Con.ExecutarQuery("SELECT 
    '                 M.ID_MENUS as Id,
    '                    M.NM_MENUS As Descricao, 
    '                    M.NM_OBJETO as Objeto,
    '                    (SELECT FL_Acessar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS And ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO") & ") As Acessar, 
    '                 (SELECT FL_Cadastrar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS And ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO") & ") As Cadastrar, 
    '                 (SELECT FL_Atualizar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS And ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO") & ") As Atualizar, 
    '                 (SELECT FL_Excluir FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS And ID_TIPO_USUARIO =" & Session("ID_TIPO_USUARIO") & ") As Excluir 
    '                FROM
    '    [dbo].[TB_MENUS] M
    '                ORDER BY 
    '                    M.ID_MENUS")
    '        If ds.Tables(0).Rows.Count > 0 Then

    '            For Each linha As DataRow In ds.Tables(0).Rows
    '                If linha.Item("ID").ToString() = 1 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnUsuarios.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 2 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnGruposUsuarios.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 3 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnPermissoesUsuarios.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 4 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnParceiros.Visible = False
    '                    mnParceirosConsulta.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 7 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnEmailParceiro.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 9 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnMoedaFrete.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 10 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnMoedaFreteArmador.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 22 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnItemDespesa.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 24 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnFreteTransportador.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 1025 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnCotacaoComercial.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 1026 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnModuloOperacional.Visible = False
    '                ElseIf linha.Item("ID").ToString() = 2026 And linha.Item("Acessar").ToString() <> "True" Then
    '                    mnConsultarWeek.Visible = False
    '                End If
    '            Next
    '        End If
    '        Con.Fechar()
    '        If mnParceiros.Visible = False And mnParceirosConsulta.Visible = False And mnEmailParceiro.Visible = False Then
    '            MenuParceiro.Visible = False
    '        End If

    '        If mnUsuarios.Visible = False And mnGruposUsuarios.Visible = False And mnPermissoesUsuarios.Visible = False Then
    '            MenuUsuario.Visible = False
    '        End If

    '    End If


    'End Sub

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
                        mnModuloOperacional.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2026 And linha.Item("Acessar").ToString() = 0 Then
                        mnConsultarWeek.Visible = False
                    ElseIf linha.Item("ID").ToString() = 2027 And linha.Item("Acessar").ToString() = 0 Then
                        mnFinanceiro.Visible = False
                    End If
                Next
            End If
            Con.Fechar()
            If mnParceiros.Visible = False And mnParceirosConsulta.Visible = False And mnEmailParceiro.Visible = False Then
                MenuParceiro.Visible = False
            End If

            If mnUsuarios.Visible = False And mnGruposUsuarios.Visible = False And mnPermissoesUsuarios.Visible = False Then
                MenuUsuario.Visible = False
            End If

        End If


    End Sub

End Class