
Imports System.ComponentModel

Public Class FrmSplash

    Dim Cont As Integer = 0

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick

        If Cont < 10 Then
            lblCarregando.Visible = Not (lblCarregando.Visible)
            pbCarregando.Value += 1
            Cont += 1
        Else
            Timer1.Enabled = False
            Me.Hide()
            FrmPrincipal.Show()
        End If

    End Sub

End Class
