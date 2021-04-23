Namespace Classes

    Public Class Excel
        Public Shared Sub exportaExcel(ByVal sql As String, ByVal sNomeConexao As String, ByVal sNomeArquivo As String, Optional ByVal formatoCelulas As String = "", Optional ByVal CommandTimeOut As Integer = -1, Optional camposTextos As List(Of String) = Nothing)

            Dim oDt As DataTable = Classes.BD.RS(sql, sNomeConexao, CommandTimeOut)
            Dim colunas As New List(Of String)
            Dim linhas As New List(Of String)

            For Each col As DataColumn In oDt.Columns
                colunas.Add(col.ColumnName.Replace("ID", "Id").Replace(";", ","))
            Next
            linhas.Add(String.Join(";", colunas.ToArray()))
            Dim linhaProcessada As List(Of String)
            Dim campo As String
            For Each linha As DataRow In oDt.Rows
                linhaProcessada = New List(Of String)
                For Each col As DataColumn In oDt.Columns
                    campo = linha(col).ToString().Replace(vbCr, " ").Replace(vbCrLf, " ")
                    campo = campo.Replace(vbLf, " ")
                    campo = campo.Replace(";", ",")
                    If camposTextos IsNot Nothing Then
                        If camposTextos.Contains(col.ColumnName) Then
                            campo = "=""" & campo & """"
                        End If
                    End If
                    linhaProcessada.Add(campo)
                Next
                linhas.Add(String.Join(";", linhaProcessada.ToArray))
            Next

            Dim construtor As New StringBuilder
            construtor.Append(String.Join(vbCrLf, linhas.ToArray))

            Dim oResponse As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
            oResponse.Clear()
            oResponse.ContentEncoding = Encoding.GetEncoding(28591)
            oResponse.AddHeader("Content-Disposition", "attachment;filename=" & sNomeArquivo & ".csv")
            oResponse.ContentType = "text/csv"
            oResponse.Write(construtor)
            oResponse.End()

        End Sub

    End Class
    Public Class Conexao
        Public Sub New(ByVal ConnectionString As String)
            strConnectionString = ConnectionString
        End Sub

        'Propriedade que define a string de conexão
        Private strConnectionString As String
        Public Property ConnectionString As String
            Get
                Return strConnectionString
            End Get
            Set(ByVal value As String)
                strConnectionString = value
            End Set
        End Property

        Private CN As SqlClient.SqlConnection

        'Sub-Rotina que abre a conexão com o banco
        Private Sub Conectar()
            Try
                CN = New SqlClient.SqlConnection(strConnectionString)
                CN.Open()
            Catch ex As SqlClient.SqlException
                Throw New Exception(ex.Message)
            End Try
        End Sub

        'Sub-Rotina que fecha a conexão com o banco
        Private Sub Desconectar()
            Try
                CN.Close()
                CN.Dispose()

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Function RS(ByVal String_SQL As String, Optional ByVal CommandTimeOut As Integer = -1) As System.Data.DataTable
            Dim DA As SqlClient.SqlDataAdapter
            Dim CB As SqlClient.SqlCommandBuilder
            RS = Nothing
            Try
                Conectar()
                RS = New System.Data.DataTable
                DA = New SqlClient.SqlDataAdapter(String_SQL, CN)

                If CommandTimeOut >= 0 Then
                    DA.SelectCommand.CommandTimeout = CommandTimeOut
                End If

                CB = New SqlClient.SqlCommandBuilder(DA)
                DA.Fill(RS)

                Desconectar()
            Catch ex As SqlClient.SqlException
                Desconectar()
                Throw New Exception(ex.Message)

            End Try
        End Function


        Public Function RS_Procedure(ByVal NomeProcedure As String, ByVal Parametros As List(Of SqlClient.SqlParameter)) As System.Data.DataTable
            Dim DA As New SqlClient.SqlDataAdapter(NomeProcedure, strConnectionString)

            Dim CMD As New SqlClient.SqlCommand
            Dim Parametro As SqlClient.SqlParameter
            Dim DS As New DataTable
            Try
                Conectar()
                DA.SelectCommand.CommandType = CommandType.StoredProcedure

                For Each Parametro In Parametros
                    DA.SelectCommand.Parameters.Add(Parametro)
                Next

                DA.Fill(DS)
                Desconectar()
                Return DS
            Catch ex As SqlClient.SqlException
                Desconectar()
                Throw New Exception(ex.Message)
            End Try
        End Function
        ''' <summary>
        ''' Função que executa a query enviada por parâmetro no banco de dados. 
        ''' </summary>
        ''' <param name="String_SQL"></param>
        ''' <param name="RetornaUltimoID"></param>
        ''' <param name="CommandTimeOut">Tempo em segundos que o sistema deve esperar antes de retornar erro por timeout</param>
        ''' <returns></returns>
        Public Function Executar(ByVal String_SQL As String, ByVal RetornaUltimoID As Boolean, Optional ByVal CommandTimeOut As Integer = -1) As Long
            Try
                Dim CM As SqlClient.SqlCommand
                Conectar()
                CM = New SqlClient.SqlCommand(String_SQL, CN)
                If CommandTimeOut > 0 Then
                    CM.CommandTimeout = CommandTimeOut
                End If
                If InStr(UCase(String_SQL), "INSERT") = 0 Then
                    Executar = CM.ExecuteNonQuery()
                Else
                    CM.ExecuteNonQuery()
                    If RetornaUltimoID = True Then
                        Executar = GetLastID()
                    Else
                        Executar = 0
                    End If
                End If
                Desconectar()
            Catch ex As SqlClient.SqlException
                Desconectar()
                Throw New Exception(ex.Message)
            End Try
        End Function

        Private Function GetLastID() As Long
            Dim RSGetLastID As System.Data.DataTable
            Dim DA As SqlClient.SqlDataAdapter
            Dim CB As SqlClient.SqlCommandBuilder
            GetLastID = 0
            RSGetLastID = Nothing

            Try
                RSGetLastID = New System.Data.DataTable
                DA = New SqlClient.SqlDataAdapter("SELECT SCOPE_IDENTITY()", CN)
                CB = New SqlClient.SqlCommandBuilder(DA)
                DA.Fill(RSGetLastID)
                GetLastID = IIf(Not IsDBNull(RSGetLastID.Rows(0).Item(0)), CLng(RSGetLastID.Rows(0).Item(0)), 0)
            Catch ex As SqlClient.SqlException
                Desconectar()
                Return 0
            End Try
            Return GetLastID
        End Function


        Public Function retornaDataSet(ByVal NomeProcedure As String, ByVal Parametros As List(Of SqlClient.SqlParameter)) As System.Data.DataSet
            Dim DA As New SqlClient.SqlDataAdapter(NomeProcedure, strConnectionString)
            Dim CMD As New SqlClient.SqlCommand
            Dim Parametro As SqlClient.SqlParameter
            Dim DS As New DataSet
            ' Try
            Conectar()
            DA.SelectCommand.CommandType = CommandType.StoredProcedure

            For Each Parametro In Parametros
                DA.SelectCommand.Parameters.Add(Parametro)
            Next

            DA.Fill(DS)
            Desconectar()
            Return DS
            'Catch ex As SqlClient.SqlException
            'Desconectar()
            'Throw New Exception(ex.Message)
            ' End Try
        End Function
    End Class


    Public Class BD

        Public Shared Function RS(ByVal String_SQL As String, Optional ByVal Base As String = "NVOCC", Optional ByVal CommandTimeOut As Integer = -1) As System.Data.DataTable
            Dim conexao As New Conexao(ConfigurationManager.ConnectionStrings(Base).ConnectionString)
            Return conexao.RS(String_SQL, CommandTimeOut)
        End Function

        Public Shared Function RS_Procedure(ByVal NomeProcedure As String, ByVal Parametros As List(Of SqlClient.SqlParameter), Optional ByVal Base As String = "NVOCC") As System.Data.DataTable
            Dim conexao As New Conexao(ConfigurationManager.ConnectionStrings(Base).ConnectionString)
            Return conexao.RS_Procedure(NomeProcedure, Parametros)
        End Function


        Public Shared Function retornaDataSet(ByVal NomeProcedure As String, ByVal Parametros As List(Of SqlClient.SqlParameter), Optional ByVal Base As String = "NVOCC") As System.Data.DataSet
            Dim conexao As New Conexao(ConfigurationManager.ConnectionStrings(Base).ConnectionString)
            Return conexao.retornaDataSet(NomeProcedure, Parametros)
        End Function

        ''' <summary>
        ''' Função que executa a query enviada por parâmetro no banco de dados. 
        ''' </summary>
        ''' <param name="String_SQL"></param>
        ''' <param name="RetornaUltimoID"></param>
        ''' <param name="CommandTimeOut">Tempo em segundos que o sistema deve esperar antes de retornar erro por timeout</param>
        ''' <returns></returns>
        Public Shared Function Executar(ByVal String_SQL As String, Optional ByVal Base As String = "NVOCC", Optional ByVal RetornaUltimoID As Boolean = True, Optional ByVal CommandTimeOut As Integer = -1) As Long
            Dim conexao As New Conexao(ConfigurationManager.ConnectionStrings(Base).ConnectionString)
            Return conexao.Executar(String_SQL, RetornaUltimoID, CommandTimeOut)
        End Function

    End Class
End Namespace
