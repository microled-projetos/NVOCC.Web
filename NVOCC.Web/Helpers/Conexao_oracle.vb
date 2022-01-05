Imports Oracle.ManagedDataAccess.Client
Public Class Conexao_oracle

    Public Property Server As String
    Public Property User As String
    Public Property Password As String
    Private Con As OracleConnection
    Public Sub Conectar()

        If Con Is Nothing Then
            Con = New OracleConnection(ConnectionString())
        End If

        If Con IsNot Nothing Then

            If Con.State = ConnectionState.Closed Then

                Try
                    Con.Open()
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        End If
    End Sub

    Public Sub Desconectar()
        If Con IsNot Nothing Then

            If Con.State = ConnectionState.Open Then

                Try
                    Con.Close()
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        End If
    End Sub

    Public Function ExecuteScalar(ByVal SQL As String) As Object
        Conectar()

        Using Cmd As OracleCommand = New OracleCommand(SQL, Con)
            Try
                Return Cmd.ExecuteScalar()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                Desconectar()
            End Try
        End Using
    End Function

    Public Function ConnectionString() As String
        Dim STR As String = ConfigurationManager.ConnectionStrings("StringConexaoOracle").ConnectionString
        Return STR
    End Function


    Public Function Consultar(ByVal sSql As String) As DataTable
        Dim ds As DataTable = New DataTable()
        Conectar()

        Using Cmd As OracleCommand = New OracleCommand(sSql, Con)
            Try
                Dim dr As OracleDataReader
                dr = Cmd.ExecuteReader()

                If dr.HasRows Then
                    ds.Load(dr)
                End If

                Return ds
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                Desconectar()
            End Try
        End Using
    End Function
End Class