Public Class Program

    Public Shared Sub Main()
        Dim PatiosArr As New List(Of String)

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)


        Application.Run(FrmLogin)


    End Sub




    Public Shared Sub Login()





        Banco.Empresa = Cod_Empresa

            Using oFrm As New FrmPrincipal
                oFrm.lblUsuario.Text = "MICROLED"
                oFrm.lblEmpresa.Text = "MICROLED"
                oFrm.mnPrincipal.Enabled = True
                Application.Run(oFrm)
            End Using




    End Sub

End Class