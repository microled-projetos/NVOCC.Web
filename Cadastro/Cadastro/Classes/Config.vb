Public Class Config

    Private Shared _UsuarioSistema As String
    Private Shared _Patios As String
    Private Shared _Empresa As String

    Public Shared ReadOnly Property Versão() As String
        Get
            Try
                Return Replace(FormatDateTime(FileDateTime(Application.StartupPath & "\" &
                    My.Application.Info.AssemblyName.ToString() & ".exe"), DateFormat.ShortDate).ToString(), "/", ".")
            Catch ex As Exception
                Return My.Application.Info.Version.ToString()
            End Try
        End Get
    End Property

    Public Shared ReadOnly Property Data() As String
        Get
            Return FormatDateTime(Now.Date, DateFormat.ShortDate)
        End Get
    End Property

    Public Shared ReadOnly Property Sistema() As String
        Get
            Return My.Application.Info.ProductName.ToString()
        End Get
    End Property

    Public Shared ReadOnly Property ArquivoConfig() As String
        Get
            Return Application.StartupPath & "\BD.xml"
        End Get
    End Property

    Public Shared Property Patios() As String
        Get
            Return _Patios
        End Get
        Set(ByVal value As String)
            _Patios = value
        End Set
    End Property

    Public Shared Property Empresa() As String
        Get
            Return _Empresa
        End Get
        Set(ByVal value As String)
            _Empresa = value
        End Set
    End Property

    Public Shared Property UsuarioSistema() As String
        Get
            Return _UsuarioSistema
        End Get
        Set(ByVal value As String)
            _UsuarioSistema = value
        End Set
    End Property

    Public Shared Sub SomenteNumeros(ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = Convert.ToChar(Keys.Back) Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub SomenteLetras(ByVal e As KeyPressEventArgs)

        If Char.IsNumber(e.KeyChar) And Not e.KeyChar = Convert.ToChar(Keys.Back) Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub SomenteNumerosDecimais(ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = "," And Not e.KeyChar = Convert.ToChar(Keys.Back) Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub CampoAlphaNumerico(ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not Char.IsLetter(e.KeyChar) And
                Not e.KeyChar = Convert.ToChar(Keys.Back) And Not e.KeyChar = Convert.ToChar(Keys.Space) And Not e.KeyChar = "." Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub CampoAlphaNumericoCGCCPF(ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And
                Not Char.IsLetter(e.KeyChar) And
                Not e.KeyChar = Convert.ToChar(Keys.Back) And
                Not e.KeyChar = Convert.ToChar(Keys.Space) And
                Not e.KeyChar = "." And
                Not e.KeyChar = "/" And
                Not e.KeyChar = "-" Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub CampoLoginOuSenha(ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not Char.IsLetter(e.KeyChar) And Not e.KeyChar = Convert.ToChar(Keys.Back) Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub CampoPath(ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not Char.IsLetter(e.KeyChar) And Not e.KeyChar = Convert.ToChar(Keys.Back) And Not e.KeyChar = ":" And Not e.KeyChar = "\" Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub CampoEmail(ByVal e As KeyPressEventArgs)

        If Not Char.IsNumber(e.KeyChar) And Not Char.IsLetter(e.KeyChar) And
                Not e.KeyChar = Convert.ToChar(Keys.Back) And Not e.KeyChar = Convert.ToChar(Keys.Space) _
                    And Not e.KeyChar = "@" And Not e.KeyChar = "." Then
            e.Handled = True
            Beep()
        End If

    End Sub

    Public Shared Sub EnterTAB(ByVal e As KeyPressEventArgs)

        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If

    End Sub

End Class
