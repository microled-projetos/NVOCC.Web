Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports System.Configuration
Imports Oracle.ManagedDataAccess.Client

Public Class Conexao_oracle1
    Public Property Server As String
    Public Property User As String
    Public Property Password As String
    Private Con As OleDbConnection
    Public Sub Conectar()

        If Con Is Nothing Then
            Con = New OleDbConnection(ConnectionString())

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

    Public Function BeginTransaction(ByVal SQL As String) As Integer
        Conectar()

        If Con.State = 0 Then
            Con.Open()
        End If

        Using Cmd As OleDbCommand = New OleDbCommand(SQL, Con)

            Try
                Return Cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                Desconectar()
            End Try
        End Using
    End Function

    Public Function ExecuteScalar(ByVal SQL As String) As Object
        Conectar()

        Using Cmd As OleDbCommand = New OleDbCommand(SQL, Con)

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

        ' Return String.Format("Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 177.11.210.55)(PORT = 1522)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = TCHRONOS)));User Id=SGIPA;Password=teste;")
        Dim STR As String = "Provider=OraOLEDB.Oracle;" & ConfigurationManager.ConnectionStrings("StringConexaoOracle").ConnectionString
        Return STR
    End Function


    Public Function Consultar(ByVal sSql As String) As DataTable
        Dim ds As DataTable = New DataTable()
        Conectar()

        Using Cmd As OleDbCommand = New OleDbCommand(sSql, Con)

            Try
                Dim dr As OleDbDataReader
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
