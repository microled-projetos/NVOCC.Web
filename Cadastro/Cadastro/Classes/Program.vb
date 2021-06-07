Public Class Program

    Public Shared Sub Main()
        Dim PatiosArr As New List(Of String)

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Dim Args = Environment.GetCommandLineArgs

        If Environment.GetCommandLineArgs.Count >= 2 Then
            If Environment.GetCommandLineArgs(1).ToString = "CAD" Then
                Banco.UsuarioSistema = Environment.GetCommandLineArgs(2).ToString
                Cod_Usuario = Banco.UsuarioSistema
                Dim Ds As New DataTable
                Ds = Banco.List("SELECT AUTONUM, ISNULL(FLAG_BLOQUEIO_NVOCC,0) FLAG_BLOQUEIO_NVOCC, ISNULL(FLAG_ISENTA_IMPOSTO,0) FLAG_ISENTA_IMPOSTO, ISNULL(FLAG_RETIRADA,0) FLAG_RETIRADA, ISNULL(COD_EMPRESA,0) COD_EMPRESA,ISNULL(GR_GR_DOC,0) GR_GR_DOC, ISNULL(FLAG_SOL_COM,0) FLAG_SOL_COM, USUARIO, ISNULL(FLAG_ALTERA_TABELA,0) FLAG_ALTERA_TABELA FROM " & Banco.BancoSGIPA & "TB_CAD_USUARIOS WHERE AUTONUM = " & Banco.UsuarioSistema)
                If Ds.Rows.Count > 0 Then
                    Usuario_Sistema = Ds.Rows(0)("USUARIO").ToString
                    Cod_Empresa = Environment.GetCommandLineArgs(3).ToString
                    Banco.Empresa = Cod_Empresa
                    Banco.UsuarioSistema = Convert.ToInt32(Ds.Rows(0)("AUTONUM").ToString())
                    Banco.IsentaImpostos = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_ISENTA_IMPOSTO").ToString()))
                    Banco.Retirada = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_RETIRADA").ToString()))
                    Banco.Empresa = Convert.ToInt32(Ds.Rows(0)("COD_EMPRESA").ToString())
                    Banco.GR_GR_DOC = Convert.ToBoolean(Val(Ds.Rows(0)("GR_GR_DOC").ToString()))
                    Banco.FLAG_SOL_COM = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_SOL_COM").ToString()))
                    Banco.FLAG_ALTERA_TABELA = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_ALTERA_TABELA").ToString()))
                    Banco.FLAG_BLOQUEIO_NVOCC = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_BLOQUEIO_NVOCC").ToString()))
                    Cod_Usuario = Banco.UsuarioSistema
                    Ds = Banco.List("SELECT ISNULL(TABELA_PADRAO,0) TABELA_PADRAO FROM " & Banco.BancoSGIPA & "TB_EMPRESAS WHERE (AUTONUM = " & Banco.Empresa & " OR 0 = " & Banco.Empresa & ")")
                    If Ds.Rows.Count > 0 Then
                        Banco.TabelaPadrao = Convert.ToInt32(Ds.Rows(0)("TABELA_PADRAO").ToString())
                    End If



                    Ds = Banco.List("SELECT AUTONUM FROM " & Banco.BancoOPERADOR & "TB_PATIOS WHERE (COD_EMPRESA = " & Banco.Empresa & " OR 0 = " & Banco.Empresa & ") ORDER BY AUTONUM")

                For Each Linha As DataRow In Ds.Rows
                    PatiosArr.Add(Linha("AUTONUM").ToString())
                Next

                Banco.Patios = String.Join(",", PatiosArr)

                    Using oFrm As New FrmPrincipal
                        oFrm.lblUsuario.Text = "MICROLED"
                        oFrm.lblEmpresa.Text = "MICROLED"
                        oFrm.mnPrincipal.Enabled = True
                        Application.Run(oFrm)
                    End Using

                End If


            Else

                Dim Ds As New DataTable
                Ds = Banco.List("SELECT AUTONUM, COD_EMPRESA FROM " & Banco.BancoSGIPA & "TB_CAD_USUARIOS WHERE USUARIO = 'MICROLED'")

                If Ds.Rows.Count > 0 Then

                    Banco.UsuarioSistema = Convert.ToInt32(Ds.Rows(0)("AUTONUM").ToString())
                    Banco.Empresa = Convert.ToInt32(Ds.Rows(0)("COD_EMPRESA").ToString())

                    'Dim oFrm As New FrmSimulador
                    'oFrm.CalcAuto = True

                    'Application.Run(oFrm)

                End If
            End If

        Else
            Application.Run(FrmLogin)
        End If

    End Sub




    Public Shared Sub Login()

        Dim Ds As New DataTable

        Ds = Banco.List("SELECT AUTONUM, ISNULL(FLAG_BLOQUEIO_NVOCC,0) FLAG_BLOQUEIO_NVOCC, ISNULL(FLAG_ISENTA_IMPOSTO,0) FLAG_ISENTA_IMPOSTO, ISNULL(FLAG_RETIRADA,0) FLAG_RETIRADA, ISNULL(COD_EMPRESA,0) COD_EMPRESA,ISNULL(GR_GR_DOC,0) GR_GR_DOC, ISNULL(FLAG_SOL_COM,0) FLAG_SOL_COM, USUARIO, ISNULL(FLAG_ALTERA_TABELA,0) FLAG_ALTERA_TABELA FROM " & Banco.BancoSGIPA & "TB_CAD_USUARIOS WHERE USUARIO = 'MICROLED' AND SENHA = 'TESTE' ")

        If Ds.Rows.Count > 0 Then

            Banco.UsuarioSistema = Convert.ToInt32(Ds.Rows(0)("AUTONUM").ToString())
            Banco.IsentaImpostos = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_ISENTA_IMPOSTO").ToString()))
            Banco.Retirada = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_RETIRADA").ToString()))
            Banco.Empresa = Convert.ToInt32(Ds.Rows(0)("COD_EMPRESA").ToString())
            Banco.GR_GR_DOC = Convert.ToBoolean(Val(Ds.Rows(0)("GR_GR_DOC").ToString()))
            Banco.FLAG_SOL_COM = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_SOL_COM").ToString()))
            Banco.FLAG_ALTERA_TABELA = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_ALTERA_TABELA").ToString()))
            Banco.FLAG_BLOQUEIO_NVOCC = Convert.ToBoolean(Val(Ds.Rows(0)("FLAG_BLOQUEIO_NVOCC").ToString()))

            If Ds.Rows.Count > 0 Then
                Cod_Empresa = Banco.Empresa
                Cod_Usuario = Banco.UsuarioSistema
                Usuario_Sistema = Ds.Rows(0)("USUARIO").ToString
            End If

            Ds = Banco.List("SELECT ISNULL(TABELA_PADRAO,0) TABELA_PADRAO FROM " & Banco.BancoSGIPA & "TB_EMPRESAS WHERE (AUTONUM = " & Banco.Empresa & " OR 0 = " & Banco.Empresa & ")")

            If Ds.Rows.Count > 0 Then
                Banco.TabelaPadrao = Convert.ToInt32(Ds.Rows(0)("TABELA_PADRAO").ToString())
            End If

            Dim PatiosArr As New List(Of String)
            Dim Patios As String = String.Empty
            Dim Linhas As Integer = 0
            Dim Cont As Integer = 0

            Banco.Empresa = Cod_Empresa

            Ds = Banco.List("SELECT AUTONUM FROM " & Banco.BancoOPERADOR & "TB_PATIOS WHERE (COD_EMPRESA = " & Banco.Empresa & " OR 0 = " & Banco.Empresa & ") ORDER BY AUTONUM")

            For Each Linha As DataRow In Ds.Rows
                PatiosArr.Add(Linha("AUTONUM").ToString())
            Next

            Banco.Patios = String.Join(",", PatiosArr)

            Using oFrm As New FrmPrincipal
                oFrm.lblUsuario.Text = "MICROLED"
                oFrm.lblEmpresa.Text = "MICROLED"
                oFrm.mnPrincipal.Enabled = True
                Application.Run(oFrm)
            End Using

        End If


    End Sub

End Class