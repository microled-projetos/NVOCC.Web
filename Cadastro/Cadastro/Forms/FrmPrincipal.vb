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

    Private Sub FrmPrincipal_Activated(ByVal sender As Object, ByVal e As EventArgs)
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

    Private Sub FrmPrincipal_Load(ByVal sender As Object, ByVal e As EventArgs)
        Text = "FCA - Log :: Cadastro - Versão: " & Application.ProductVersion.ToString()
        Dim enumerator As IEnumerator = Nothing

        Try
            enumerator = mnPrincipal.Items.GetEnumerator()
            Dim enumerator2 As IEnumerator = Nothing
            Dim enumerator3 As IEnumerator = Nothing

            While enumerator.MoveNext()
                Dim MenuPrincipal As ToolStripMenuItem = CType(enumerator.Current, ToolStripMenuItem)

                If Not UsuarioTemAcesso(MenuPrincipal.Name) Then
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
                                (TryCast(enumerator3, IDisposable)).Dispose()
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
                    (TryCast(enumerator2, IDisposable)).Dispose()
                End If
                End Try
            End While

        Finally

            If TypeOf enumerator Is IDisposable Then
            (TryCast(enumerator, IDisposable)).Dispose()
        End If
        End Try
    End Sub
    Private Function UsuarioTemAcesso(ByVal NomeMenu As String) As Boolean
        Dim TipoUsuario As Integer = Banco.TipoUsuario
        Dim sql As String = "SELECT (SELECT FL_Acessar FROM [dbo].[TB_GRUPO_PERMISSAO] WHERE ID_MENU = M.ID_MENUS AND ID_TIPO_USUARIO = " & Conversions.ToString(TipoUsuario) & "   ) As Acessar FROM [dbo].[TB_MENUS] M WHERE  M.NM_OBJETO = '" & NomeMenu & "' ORDER BY M.NM_MENUS"
        Dim Ds As DataTable = New DataTable()
        Ds = Banco.List(sql)

        If Ds.Rows.Count > 0 Then

            If Not Information.IsDBNull(RuntimeHelpers.GetObjectValue(Ds.Rows(0)("Acessar"))) Then

                If Operators.ConditionalCompareObjectEqual(Ds.Rows(0)("Acessar"), True, TextCompare:=False) Then
                    Return True
                End If

                Return False
            End If

            Return False
        End If

        Return False
    End Function

    Private Sub mnCidades_Click_1(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmCidades.Show()
    End Sub

    Private Sub mnPais_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmPaises.Show()
    End Sub

    Private Sub mnNCM_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmNCM.Show()
    End Sub

    Private Sub mnEstados_Click_1(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmUF.Show()
    End Sub

    Private Sub mnCadConteiner_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmTiposConteiner.Show()
    End Sub

    Private Sub mnSevicos_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmServico.Show()
    End Sub

    Private Sub mnEventos_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmEventos.Show()
    End Sub

    Private Sub mnContas_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmContaFinanceiro.Show()
    End Sub

    Private Sub mnMoedas_Click(ByVal sender As Object, ByVal e As EventArgs)
        MyProject.Forms.FrmMoedas.Show()
    End Sub
End Class