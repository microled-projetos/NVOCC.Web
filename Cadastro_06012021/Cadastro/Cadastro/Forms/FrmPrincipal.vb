Imports System.Runtime.InteropServices
Imports System.Text

Public Class FrmPrincipal



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


    Private Sub mnSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Mnusair.Click
        Environment.Exit(0)
    End Sub

    Private Sub FrmPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "FCA - Log :: Cadastro - Versão: " & Application.ProductVersion.ToString()

        For Each MenuPrincipal As ToolStripMenuItem In mnPrincipal.Items
            If Not UsuarioTemAcesso(MenuPrincipal.Name) Then
                MenuPrincipal.Visible = False
            Else
                MenuPrincipal.Visible = True
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
                    Else
                        ItemMenu.Visible = True
                    End If
                End If
            Next
        Next

    End Sub

    Private Function UsuarioTemAcesso(ByVal NomeMenu As String) As Boolean

        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim sql As String = "SELECT (SELECT FL_Acessar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = " & TipoUsuario & "   ) As Acessar FROM [dbo].[TB_MENUS] M WHERE  M.NM_OBJETO = '" & NomeMenu & "' ORDER BY M.NM_MENUS"

        Dim Ds As New DataTable
        Ds = Banco.List(sql)
        If Ds.Rows.Count > 0 Then

            If Not IsDBNull(Ds.Rows(0).Item("Acessar")) Then

                If Ds.Rows(0).Item("Acessar") = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False

            End If
        Else
            Return False

        End If


    End Function

    Private Sub mnCidades_Click_1(sender As Object, e As EventArgs) Handles mnCidades.Click
        FrmCidades.Show()
    End Sub

    Private Sub mnPais_Click(sender As Object, e As EventArgs) Handles mnPais.Click
        FrmPaises.Show()
    End Sub

    Private Sub mnNCM_Click(sender As Object, e As EventArgs) Handles mnNCM.Click
        FrmNCM.Show()
    End Sub

    Private Sub mnEstados_Click_1(sender As Object, e As EventArgs) Handles mnEstados.Click
        FrmUF.Show()
    End Sub

    Private Sub mnCadConteiner_Click(sender As Object, e As EventArgs) Handles mnCadConteiner.Click
        FrmTiposConteiner.Show()
    End Sub

    Private Sub mnSevicos_Click(sender As Object, e As EventArgs) Handles mnSevicos.Click
        FrmServico.Show()
    End Sub

    Private Sub mnEventos_Click(sender As Object, e As EventArgs) Handles mnEventos.Click
        FrmEventos.Show()
    End Sub

    Private Sub mnContas_Click(sender As Object, e As EventArgs) Handles mnContas.Click
        FrmContaFinanceiro.Show()
    End Sub

    Private Sub mnMoedas_Click(sender As Object, e As EventArgs) Handles mnMoedas.Click
        FrmMoedas.Show()
    End Sub
End Class
