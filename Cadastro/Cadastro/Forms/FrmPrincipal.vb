Imports System.Runtime.InteropServices
Imports System.Text
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports Gerencial
Imports Gerencial.My
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices


Public Class FrmPrincipal
    Private Sub FrmPrincipal_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Try
            Dim ItemProcess As Process() = Process.GetProcessesByName("FontePagadora")

            If ItemProcess IsNot Nothing Then
                Dim array As Process() = ItemProcess

                For Each SubProcess As Process In array
                    SubProcess.Kill()
                Next
            End If

        Catch ex2 As Exception
            ProjectData.SetProjectError(ex2)
            Dim ex As Exception = ex2
            Environment.[Exit](0)
            ProjectData.ClearProjectError()
        End Try

        Environment.[Exit](0)
    End Sub

    Private Sub mnSair_Click(ByVal sender As Object, ByVal e As EventArgs)
        Environment.[Exit](0)
    End Sub

    Private Function UsuarioTemAcesso(ByVal NomeMenu As String) As Boolean
        Dim Usuario As Integer = Banco.UsuarioSistema

        Dim sql As String = "SELECT COUNT(ID_GRUPO_PERMISSAO)QTD FROM [TB_GRUPO_PERMISSAO] where ID_Menu = 21 AND FL_ACESSAR = 1 AND ID_TIPO_USUARIO IN( SELECT distinct ID_TIPO_USUARIO from TB_VINCULO_USUARIO where ID_USUARIO =  " & Conversions.ToString(Usuario) & " )"
        Dim Ds As DataTable = New DataTable()

        Ds = Banco.List(sql)

        If Ds.Rows.Count > 0 Then

            If Ds.Rows(0).Item("QTD") = 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If


    End Function

    Private Sub MnuCadNavios_Click(sender As Object, e As EventArgs) Handles mnNavios.Click
        MyProject.Forms.FrmNavios.Show()
    End Sub

    Private Sub nmNCM_Click(sender As Object, e As EventArgs) Handles mnNCM.Click
        MyProject.Forms.FrmNCM.Show()
    End Sub

    Private Sub MnuCadPortos_Click(sender As Object, e As EventArgs) Handles mnPortos.Click
        MyProject.Forms.FrmPortos.Show()
    End Sub

    Private Sub CidadesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnCidades.Click
        MyProject.Forms.FrmCidades.Show()
    End Sub

    Private Sub PaísesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnPais.Click
        MyProject.Forms.FrmPaises.Show()
    End Sub

    Private Sub EstadosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnEstados.Click
        MyProject.Forms.FrmUF.Show()
    End Sub

    Private Sub ServiçosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnServicos.Click
        MyProject.Forms.FrmServico.Show()
    End Sub

    Private Sub EventosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnEventos.Click
        MyProject.Forms.FrmEventos.Show()
    End Sub

    Private Sub ContasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnContas.Click
        MyProject.Forms.FrmContaFinanceiro.Show()
    End Sub

    Private Sub MoedasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnMoedas.Click
        MyProject.Forms.FrmMoedas.Show()
    End Sub

    Private Sub ContainerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnCadConteiner.Click
        MyProject.Forms.FrmTiposConteiner.Show()
    End Sub

    Private Sub Mnusair_Click(sender As Object, e As EventArgs) Handles Mnusair.Click
        Environment.[Exit](0)
    End Sub

    Private Sub FrmPrincipal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Text = "FCA - Log :: Cadastro - Versão: 23.03.2022.1" '& Application.ProductVersion.ToString()
        Dim enumerator As IEnumerator = Nothing

        Try
            enumerator = mnPrincipal.Items.GetEnumerator()
            Dim enumerator2 As IEnumerator = Nothing
            Dim enumerator3 As IEnumerator = Nothing

            While enumerator.MoveNext()
                Dim MenuPrincipal As ToolStripMenuItem = CType(enumerator.Current, ToolStripMenuItem)

                If Not UsuarioTemAcesso(MenuPrincipal.Name) And MenuPrincipal.Name <> "Mnusair" Then
                    MenuPrincipal.Visible = False
                Else
                    MenuPrincipal.Visible = True
                End If

                Try
                    enumerator2 = MenuPrincipal.DropDownItems.GetEnumerator()

                    While enumerator2.MoveNext()
                        Dim ItemMenu As ToolStripMenuItem = CType(enumerator2.Current, ToolStripMenuItem)

                        If ItemMenu.DropDownItems.Count > 0 Then

                            Try
                                enumerator3 = ItemMenu.DropDownItems.GetEnumerator()

                                While enumerator3.MoveNext()
                                    Dim SubMenu As ToolStripMenuItem = CType(enumerator3.Current, ToolStripMenuItem)

                                    If Not UsuarioTemAcesso(SubMenu.Name) Then
                                        SubMenu.Visible = False
                                    End If

                                    If Not UsuarioTemAcesso(ItemMenu.Name) Then
                                        ItemMenu.Visible = False
                                    End If
                                End While

                            Finally

                                If TypeOf enumerator3 Is IDisposable Then
                                    TryCast(enumerator3, IDisposable).Dispose()
                                End If
                            End Try
                        ElseIf Not UsuarioTemAcesso(ItemMenu.Name) Then
                            ItemMenu.Visible = False
                        Else
                            ItemMenu.Visible = True
                        End If
                    End While

                Finally

                    If TypeOf enumerator2 Is IDisposable Then
                        TryCast(enumerator2, IDisposable).Dispose()
                    End If
                End Try
            End While

        Finally

            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
    End Sub

    Private Sub FrmPrincipal_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            Dim IP As IPAddress() = Dns.GetHostAddresses(Dns.GetHostName())
            lblData.Text = Conversions.ToString(DateAndTime.Now.Date)
            lblEstacao.Text = MyProject.Computer.Name
            lblBase.Text = Banco.Servidor
            lblIP.Text = IP(1).ToString()
        Catch ex2 As Exception
            ProjectData.SetProjectError(ex2)
            Dim ex As Exception = ex2
            ProjectData.ClearProjectError()
        End Try
    End Sub
End Class