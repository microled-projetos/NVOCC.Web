Imports System.IO
Imports System.Data.SqlClient

Public Class Banco

    Private Shared _Servidor As String
    Private Shared _Usuario As String
    Private Shared _Senha As String
    Private Shared _Base As String
    Private Shared _BancoSGIPA As String
    Private Shared _BancoOPERADOR As String
    Private Shared _BancoFATURA As String
    Private Shared _BancoSAPIENS As String
    Private Shared _BancoNVOCC As String
    Private Shared _Empresa As Integer = 1
    Private Shared _Patios As String
    Private Shared _UsuarioSistema As Integer = 0
    Private Shared _IsentaImpostos As Boolean
    Private Shared _Retirada As Boolean
    Private Shared _TabelaPadrao As String
    Private Shared _UsuarioRede As String
    Private Shared _MaquinaRede As String
    Private Shared _GR_GR_DOC As Boolean
    Private Shared _FLAG_SOL_COM As Boolean
    Private Shared _FLAG_ALTERA_TABELA As Boolean
    Private Shared _FLAG_BLOQUEIO_NVOCC As Boolean

    Private Shared Connect As New SqlConnection()

    Public Shared Property Servidor() As String
        Get
            Return _Servidor
        End Get
        Set(ByVal value As String)
            _Servidor = value
        End Set
    End Property

    Public Shared Property Usuario() As String
        Get
            Return _Usuario
        End Get
        Set(ByVal value As String)
            _Usuario = value
        End Set
    End Property

    Public Shared Property Senha() As String
        Get
            Return _Senha
        End Get
        Set(ByVal value As String)
            _Senha = value
        End Set
    End Property

    Public Shared Property Base() As String
        Get
            Return _Base
        End Get
        Set(ByVal value As String)
            _Base = value
        End Set
    End Property

    Public Shared Property BancoSGIPA() As String
        Get
            Return _BancoSGIPA
        End Get
        Set(ByVal value As String)
            _BancoSGIPA = value
        End Set
    End Property

    Public Shared Property BancoOPERADOR() As String
        Get
            Return _BancoOPERADOR
        End Get
        Set(ByVal value As String)
            _BancoOPERADOR = value
        End Set
    End Property

    Public Shared Property BancoFATURA() As String
        Get
            Return _BancoFATURA
        End Get
        Set(ByVal value As String)
            _BancoFATURA = value
        End Set
    End Property

    Public Shared Property BancoSAPIENS() As String
        Get
            Return _BancoSAPIENS
        End Get
        Set(ByVal value As String)
            _BancoSAPIENS = value
        End Set
    End Property

    Public Shared Property BancoNVOCC As String
        Get
            Return _BancoNVOCC
        End Get
        Set(ByVal value As String)
            _BancoNVOCC = value
        End Set
    End Property
    Public Shared Property TipoUsuario As Integer
        Get
            Return _TipoUsuario
        End Get
        Set(ByVal value As Integer)
            _TipoUsuario = value
        End Set
    End Property
    Public Shared Property Empresa() As Integer
        Get
            Return _Empresa
        End Get
        Set(ByVal value As Integer)
            _Empresa = value
        End Set
    End Property

    Public Shared Property Patios() As String
        Get
            Return _Patios
        End Get
        Set(ByVal value As String)
            _Patios = value
        End Set
    End Property

    Public Shared Property UsuarioSistema() As Integer
        Get
            Return _UsuarioSistema
        End Get
        Set(ByVal value As Integer)
            _UsuarioSistema = value
        End Set
    End Property

    Public Shared Property IsentaImpostos() As Boolean
        Get
            Return _IsentaImpostos
        End Get
        Set(ByVal value As Boolean)
            _IsentaImpostos = value
        End Set
    End Property

    Public Shared Property Retirada() As Boolean
        Get
            Return _Retirada
        End Get
        Set(ByVal value As Boolean)
            _Retirada = value
        End Set
    End Property

    Public Shared Property TabelaPadrao() As String
        Get
            Return _TabelaPadrao
        End Get
        Set(ByVal value As String)
            _TabelaPadrao = value
        End Set
    End Property

    Public Shared Property UsuarioRede() As String
        Get
            Return _UsuarioRede
        End Get
        Set(ByVal value As String)
            _UsuarioRede = value
        End Set
    End Property

    Public Shared Property MaquinaRede() As String
        Get
            Return My.Computer.Name
        End Get
        Set(ByVal value As String)
            _MaquinaRede = value
        End Set
    End Property

    Public Shared Property GR_GR_DOC() As Boolean
        Get
            Return _GR_GR_DOC
        End Get
        Set(ByVal value As Boolean)
            _GR_GR_DOC = value
        End Set
    End Property

    Public Shared Property FLAG_SOL_COM() As Boolean
        Get
            Return _FLAG_SOL_COM
        End Get
        Set(ByVal value As Boolean)
            _FLAG_SOL_COM = value
        End Set
    End Property

    Public Shared Property FLAG_ALTERA_TABELA() As Boolean
        Get
            Return _FLAG_ALTERA_TABELA
        End Get
        Set(ByVal value As Boolean)
            _FLAG_ALTERA_TABELA = value
        End Set
    End Property

    Public Shared Property FLAG_BLOQUEIO_NVOCC() As Boolean
        Get
            Return _FLAG_BLOQUEIO_NVOCC
        End Get
        Set(ByVal value As Boolean)
            _FLAG_BLOQUEIO_NVOCC = value
        End Set
    End Property

    Shared Sub New()

        Try

            Dim ConfigurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()

            Servidor = My.Settings.Servidor
            Usuario = My.Settings.Usuario
            Senha = My.Settings.Senha
            Base = My.Settings.Banco
            BancoSGIPA = My.Settings.Banco_SGIPA
            BancoOPERADOR = My.Settings.Banco_OPERADOR
            BancoFATURA = My.Settings.Banco_FATURA
            BancoSAPIENS = My.Settings.Banco_SAPIENS

        Catch ex As Exception
            MessageBox.Show("O Arquivo de configuração não foi encontrado ou contém erros. O sistema será encerrado." & ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(0)
        End Try

        ConexaoBD()

    End Sub

    Private Shared Sub ConexaoBD()

        If Connect.State = ConnectionState.Closed Then

            Try
                Connect.ConnectionString = ConnectionString()
                Connect.Open()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If

    End Sub

    Public Shared Function ExecuteScalar(ByVal SQL As String) As String

        Dim Result As Object = Nothing

        Using Cmd As New SqlCommand()

            ConexaoBD()

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = SQL
            Cmd.Connection = Connect

            Try
                Result = Cmd.ExecuteScalar()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Using

        Return If(Result Is Nothing, Nothing, Result.ToString())

    End Function

    Public Shared Function Execute(ByVal SQL As String) As Boolean

        Dim Success As Boolean = False

        Using Cmd As New SqlCommand()

            ConexaoBD()

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = SQL
            Cmd.Connection = Connect

            Try
                Cmd.ExecuteNonQuery()
                Success = True
            Catch ex As SqlException
                MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Using

        Return Success

    End Function

    Public Shared Function List(ByVal SQL As String) As DataTable

        Using Adp As New SqlDataAdapter()
            Using Cmd As New SqlCommand()

                ConexaoBD()

                Dim Ds As New DataSet()

                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = SQL
                Cmd.Connection = Connect

                Try
                    Adp.SelectCommand = Cmd
                    Adp.Fill(Ds)
                    Return Ds.Tables(0)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End Using
        End Using

        Return Nothing

    End Function

    Public Shared Sub CarregarCombo(ByRef Combo As ComboBox, ByVal SQL As String, Optional ByVal DisplayValue As String = "AUTONUM", Optional ByVal DisplayMember As String = "DESCR")

        Using Cmd As New SqlCommand()

            ConexaoBD()

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = SQL
            Cmd.Connection = Connect

            Try

                Dim Lista As New List(Of KeyValuePair(Of String, String))

                Dim Dr As SqlDataReader

                If Combo.Items.Count = 0 Then

                    Dr = Cmd.ExecuteReader()

                    While Dr.Read()
                        Lista.Add(New KeyValuePair(Of String, String)(Dr(DisplayValue).ToString(), Dr(DisplayMember).ToString()))
                    End While

                    Combo.DisplayMember = "value"
                    Combo.ValueMember = "key"

                    Combo.Items.Clear()
                    Dr.Close()
                    Combo.DataSource = New BindingSource(Lista, Nothing)
                    Lista = Nothing

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Using

    End Sub

    Public Shared Sub CarregarListBox(ByRef List As ListBox, ByVal SQL As String, Optional ByVal DisplayValue As String = "AUTONUM", Optional ByVal DisplayMember As String = "DESCR")

        Using Cmd As New SqlCommand()

            ConexaoBD()

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = SQL
            Cmd.Connection = Connect

            Try

                List.DataSource = Nothing

                Dim Lista As New List(Of KeyValuePair(Of String, String))
                Dim Dr As SqlDataReader = Cmd.ExecuteReader()

                While Dr.Read()
                    Lista.Add(New KeyValuePair(Of String, String)(Dr(DisplayValue).ToString(), Dr(DisplayMember).ToString()))
                End While

                List.DisplayMember = "value"
                List.ValueMember = "key"

                List.Items.Clear()
                List.DataSource = New BindingSource(Lista, Nothing)
                Lista = Nothing

                Dr.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Using

    End Sub

    Public Shared Sub CarregarCheckedListBox(ByRef List As CheckedListBox, ByVal SQL As String, Optional ByVal DisplayValue As String = "AUTONUM", Optional ByVal DisplayMember As String = "DESCR")

        Using Cmd As New SqlCommand()

            ConexaoBD()

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = SQL
            Cmd.Connection = Connect

            Try

                List.DataSource = Nothing

                Dim Dr As SqlDataReader = Cmd.ExecuteReader()

                Dim dt As New DataTable()
                dt.Load(Dr)

                List.DataSource = dt

                List.DisplayMember = DisplayMember
                List.ValueMember = DisplayValue

                List.ClearSelected()

                Dr.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Using

    End Sub

    Public Shared Sub CarregarGrid(ByRef Grid As DataGridView, ByVal SQL As String, Optional ByVal AutoGenerateColumns As Boolean = False)

        Dim Colunas As Integer = 0

        Using Cmd As New SqlCommand()

            ConexaoBD()

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = SQL
            Cmd.Connection = Connect

            Try

                Dim Dr As SqlDataReader = Cmd.ExecuteReader()

                Grid.Rows.Clear()
                Grid.AutoGenerateColumns = AutoGenerateColumns

                If Dr.HasRows Then

                    Colunas = Grid.Columns.Count

                    Dim Dados(Colunas) As String

                    While Dr.Read()

                        For i As Integer = 0 To Colunas - 1
                            Dados(i) = Dr(i).ToString()
                        Next

                        Grid.Rows.Add(Dados)

                    End While

                    Grid.PerformLayout()
                    Grid.ClearSelection()

                End If

                Dr.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Using

    End Sub

    Public Shared Function LerCampoBLOB(ByVal SQL As String) As Image

        Dim Result As Object = Nothing

        Using Cmd As New SqlCommand()

            Try

                ConexaoBD()

                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = SQL
                Cmd.Connection = Connect

                Dim imagemEmBytes As Byte() = DirectCast(Cmd.ExecuteScalar(), Byte())
                Dim ms As New MemoryStream()

                ms.Write(imagemEmBytes, 0, imagemEmBytes.Length)
                Result = Image.FromStream(ms)

            Catch ex As Exception
                Result = Nothing
            End Try

        End Using

        Return Result

    End Function

    Public Shared Function GravarCampoBLOB(ByVal SQL As String, ByVal Imagem As PictureBox, ByVal Codigo As Integer) As Boolean

        Dim Result As Object = Nothing

        Dim ms As New System.IO.MemoryStream()
        Imagem.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)

        Using Cmd As New SqlCommand()

            Try

                ConexaoBD()

                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = SQL
                Cmd.Connection = Connect

                Cmd.Parameters.Add("@IMAGEM", SqlDbType.Binary).Value = ms.ToArray()
                Cmd.Parameters.Add("@AUTONUM", SqlDbType.Int).Value = Codigo

                Cmd.ExecuteNonQuery()

            Catch ex As Exception
                Result = Nothing
            End Try

        End Using

        Return Result

    End Function

    Public Shared Function ObterImagemEmpresa() As Object

        Try
            Dim Result As Object = Nothing

            Result = LerCampoBLOB("SELECT IMAGEM FROM " & Banco.BancoSGIPA & "TB_EMPRESAS WHERE AUTONUM = " & Banco.Empresa)

            If Result IsNot Nothing Then
                Return Result
            End If

        Catch ex As Exception
            Return Nothing
        End Try

        Return Nothing

    End Function

    Public Shared Function ConnectionString() As String
        Return String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", Servidor, Usuario, Usuario, Senha)
    End Function

End Class
