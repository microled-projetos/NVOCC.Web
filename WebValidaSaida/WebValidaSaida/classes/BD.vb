Public Class BD

    Public Shared ReadOnly Property BancoEmUso() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("Banco")
        End Get
    End Property

    Public Shared ReadOnly Property Servidor() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("Servidor")
        End Get
    End Property

    Public Shared ReadOnly Property Schema() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("Schema")
        End Get
    End Property

    Public Shared ReadOnly Property Usuario() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("Usuario")
        End Get
    End Property

    Public Shared ReadOnly Property Senha() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("Senha")
        End Get
    End Property

    Public Shared ReadOnly Property BancoOperador() As String
        Get
            If BancoEmUso = "ORACLE" Then
                Return "OPERADOR."
            Else
                Return "OPERADOR.."
            End If
        End Get
    End Property

    Public Shared ReadOnly Property BancoSgipa() As String
        Get
            If BancoEmUso = "ORACLE" Then
                Return "SGIPA."
            Else
                Return ""
            End If
        End Get
    End Property

    
    Public Shared ReadOnly Property StringConexaoOracle() As String
        Get
            Return String.Format("Provider=OraOLEDB.Oracle.1;Data Source={0};User ID={1};Password={2}", Servidor, Usuario, Senha)
        End Get
    End Property

    Public Shared ReadOnly Property StringConexaoSQLServer() As String
        Get
            Return String.Format("Provider=SQLOLEDB.1;Data Source={0};Initial Catalog={1};User ID={2};Password={3}", Servidor, Schema, Usuario, Senha)
        End Get
    End Property

End Class
