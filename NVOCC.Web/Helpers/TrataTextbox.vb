Public Class TrataTextbox
    Public Shared Function tratarDados(ByVal sDado As String) As String
        'Armazena o que foi enviado para a função para ser comparado no final
        Dim strOriginal As String = sDado

        'Variável que será verdadeira quando a função detectar um apóstrofo, uma palavra chave na primeira verificação ou quando achar uma palavra chave na segunda e na terceira verificação (simultâneamente)
        Dim bSqlInjection As Boolean

        'variável utilizada para fazer as comparações
        Dim sPalavraChave As String

        'Array que armazena as palavras chaves utilizadas na primeira verificação
        'São as palavras chave mais importantes do processo e que não devem ser passadas para o banco dedados
        Dim aPalavrasChave() As String = {"DELETE", "UPDATE", "INSERT", "DROP", "XP_", "--", "SP_", "UNION", "TABLES", "GET_HOST_ADDRESS", "CHR(", "CHAR(", "CONVERT"}

        'bPalavraEncontrada será verdadeira quando uma das palavras chaves forem encontradas na segunda verificação
        'bSubstituir será verdadeira quando forem encontradas palavras chaves na segunda e terceira verificação
        Dim bPalavraEncontrada, bSubstituir As Boolean

        'Array que irá armazenar as palavras chaves encontradas
        Dim aPalavrasEncontradas() As String

        'Contador utilizado para redimensionar o array que contém as palavras que foram encontradas
        Dim i As Integer = 0

        'Array que contém as palavras chaves de operadores lógicos
        Dim aPalavrasChave2() As String = {" OR ", " AND "}

        'Primeiro procedimento: substituir os apóstrofos(') por aspas dúplas (")
        If sDado.IndexOf("'") >= 0 Then
            bSqlInjection = True
            sDado = sDado.Replace("'", "''")
        End If

        'Segundo procedimento: substituir os ponto e vírgula(;) por virgula(,)
        If sDado.IndexOf(";") >= 0 Then
            If bSqlInjection = False Then
                bSqlInjection = True
            End If
            sDado = sDado.Replace(";", ",")
        End If

        'Utiliza o for each para fazer uma verificação em cada string contida na array
        For Each sPalavraChave In aPalavrasChave
            'o valor da variável sPalavraChave passa a ser a string atual da array
            'Verifica se a string enviada pelo usuário contém pelo menos uma palavra igual ao valor da sPalavraChave
            If sDado.ToUpper().IndexOf(sPalavraChave) >= 0 Then
                'Substituição da palavra chave contida na string enviada pelo usuário
                'é utilizado Replace da classe Regex por ter a opção de ser ou não case-sensitive
                sDado = Regex.Replace(sDado, sPalavraChave, "", RegexOptions.IgnoreCase)
                If bSqlInjection = False Then
                    bSqlInjection = True
                End If
            End If
        Next

        For Each sPalavraChave In aPalavrasChave2
            If sDado.ToUpper().IndexOf(sPalavraChave) >= 0 Then
                If bPalavraEncontrada = False Then
                    bPalavraEncontrada = True
                End If
                'Redimensiona o tamanho da array conforme for achando mais palavras utilizando o contador i
                ReDim Preserve aPalavrasEncontradas(i)
                aPalavrasEncontradas(i) = sPalavraChave
                i += 1
            End If
        Next

        'Se foi encontrada alguma palavra chave na segunda verificação é executado o bloco abaixo
        If bPalavraEncontrada Then
            Dim aPalavrasChave3() As String = {">", "=", "<", "<>"}
            For Each sPalavraChave In aPalavrasChave3
                If sDado.ToUpper().IndexOf(sPalavraChave) >= 0 Then
                    If bSubstituir = False Then
                        'a variável é marcada como true poi foi encontrada uma palavra chave nesta verificação
                        bSubstituir = True
                    End If
                    If bSqlInjection = False Then
                        bSqlInjection = True
                    End If
                    ReDim Preserve aPalavrasEncontradas(i)
                    aPalavrasEncontradas(i) = sPalavraChave
                    i += 1
                End If
            Next
        End If

        'Se foi encontrada palavras chave na segunda e terceira verificação então é executada a substituição das mesmas
        If bSubstituir Then
            For Each sPalavraChave In aPalavrasEncontradas
                sDado = Regex.Replace(sDado, sPalavraChave, "", RegexOptions.IgnoreCase)
            Next
        End If


        'Retorna a string sDado depois de ter sido tratada
        Return sDado
    End Function
End Class
