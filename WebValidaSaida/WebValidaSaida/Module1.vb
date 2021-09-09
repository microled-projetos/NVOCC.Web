Module Module1

    Public Function PRSet(ByRef ObjetoTabela As ADODB.Recordset, ByVal Instrução As String, Optional ByVal Modo As Integer = 0, Optional ByVal Carregar As Boolean = False, Optional ByVal LadoCliente As Boolean = True) As Boolean


        Dim Con As New ADODB.Connection


        Con.ConnectionString = BD.StringConexaoOracle
        Con.Open()

        If Not ObjetoTabela Is Nothing Then  'Encerra-se o recordset em caso de erros
            If Not ObjetoTabela.State = 0 Then ObjetoTabela.Close()
            ObjetoTabela = Nothing
        End If

        ObjetoTabela = New ADODB.Recordset
        ObjetoTabela.ActiveConnection = Con

        If LadoCliente = True Then
            ObjetoTabela.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Else
            ObjetoTabela.CursorLocation = ADODB.CursorLocationEnum.adUseServer
        End If

        If Modo = 0 Then
            ObjetoTabela.CursorType = ADODB.CursorTypeEnum.adOpenForwardOnly
            ObjetoTabela.LockType = ADODB.LockTypeEnum.adLockReadOnly
        Else
            ObjetoTabela.CursorType = ADODB.CursorTypeEnum.adOpenDynamic
            ObjetoTabela.LockType = ADODB.LockTypeEnum.adLockBatchOptimistic
        End If

        If ObjetoTabela.State = 1 Then ObjetoTabela.Close()

        If Not Instrução = String.Empty Then
            ObjetoTabela.Open(Instrução)
        End If

        If Carregar = True Then ObjetoTabela.MoveLast()

        If Modo = 0 Then
            If Not ObjetoTabela.CursorLocation = ADODB.CursorLocationEnum.adUseServer Then ObjetoTabela.ActiveConnection = Nothing
        End If

        PRSet = True

    End Function

End Module
