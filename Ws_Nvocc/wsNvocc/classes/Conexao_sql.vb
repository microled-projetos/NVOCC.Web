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
End Class
