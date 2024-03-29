﻿Imports System.Data.SqlClient
Imports System.Configuration

Public Class Conexao_sql

    Private ObjCon As SqlClient.SqlConnection
    Public Sub Conectar()
        Try

            ObjCon = New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("NVOCC").ConnectionString)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Fechar()
        Try
            If Not IsNothing(ObjCon) Then
                If ObjCon.State = ConnectionState.Open Then
                    ObjCon.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ExecutarQuery(ByVal Command As String) As DataSet
        Dim ds As New DataSet
        Dim ObjDataAdapter As New SqlClient.SqlDataAdapter
        Dim ObjCommand As New SqlClient.SqlCommand

        Try
            ObjCommand = ObjCon.CreateCommand
            ObjCommand.CommandText = Command

            ObjDataAdapter = New SqlClient.SqlDataAdapter(ObjCommand)
            ObjDataAdapter.Fill(ds)

        Catch ex As Exception
            Throw ex
        End Try

        Return ds

    End Function
    Public Shared Function List(ByVal SQL As String) As DataTable
        Dim Ds As DataSet = New DataSet()

        Using Con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NVOCC").ConnectionString)

            Using Cmd As SqlCommand = New SqlCommand()
                Cmd.CommandTimeout = 120000
                Cmd.Connection = Con
                Cmd.CommandType = CommandType.Text
                Cmd.CommandText = SQL

                Using Adp As SqlDataAdapter = New SqlDataAdapter(Cmd)
                    Adp.Fill(Ds)

                    If Ds.Tables(0).Rows.Count > 0 Then
                        Return Ds.Tables(0)
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
    End Function
End Class
