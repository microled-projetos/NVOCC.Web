Public Class InativaAtivaTaxa



    Function InativarAtivar(GRAU As String, ID_BL_TAXA As String, DS_MOTIVO_INATIVACAO As String, ID_MOTIVO_INATIVACAO As Integer, ID_USUARIO As Integer) As Boolean

        Dim Con As New Conexao_sql
        Con.Conectar()

        Dim ds As DataSet
        'Verifica status atual da taxa para realizar a mudança para o status oposto
        ds = Con.ExecutarQuery("SELECT ISNULL(FL_TAXA_INATIVA,0)FL_TAXA_INATIVA FROM TB_BL_TAXA WHERE ID_BL_TAXA = " & ID_BL_TAXA)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim Sql As String = ""

            Try
                If ds.Tables(0).Rows(0).Item("FL_TAXA_INATIVA") = False Then
                    'Inclui registro de historico
                    Sql = "INSERT INTO TB_INATIVACAO_TAXAS (ID_BL_TAXA,FL_TAXA_INATIVA,ID_MOTIVO_INATIVACAO,DS_MOTIVO_INATIVACAO,ID_USUARIO_INATIVACAO,DT_INATIVACAO) VALUES (" & ID_BL_TAXA & ",1," & ID_MOTIVO_INATIVACAO

                    'Verifica se tem descrição
                    If DS_MOTIVO_INATIVACAO <> "" Then
                        Sql &= " , '" & DS_MOTIVO_INATIVACAO & "'"
                    Else
                        Sql &= " , NULL "
                    End If

                    Sql &= " , " & ID_USUARIO & ", GETDATE() )"
                    Con.ExecutarQuery(Sql)

                    'Atualiza TB_BL_TAXA
                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_TAXA_INATIVA = 1 WHERE ID_BL_TAXA = " & ID_BL_TAXA)

                    If GRAU = "M" Then
                        'Inclui registro de historico
                        Sql = "INSERT INTO TB_INATIVACAO_TAXAS (ID_BL_TAXA,FL_TAXA_INATIVA,ID_MOTIVO_INATIVACAO,DS_MOTIVO_INATIVACAO,ID_USUARIO_INATIVACAO,DT_INATIVACAO) SELECT ID_BL_TAXA, 1," & ID_MOTIVO_INATIVACAO

                        'Verifica se tem descrição
                        If DS_MOTIVO_INATIVACAO <> "" Then
                            Sql &= " , '" & DS_MOTIVO_INATIVACAO & "'"
                        Else
                            Sql &= " , NULL "
                        End If

                        Sql &= " , " & ID_USUARIO & ", GETDATE() FROM TB_BL_TAXA WHERE ID_BL_TAXA_MASTER = " & ID_BL_TAXA
                        Con.ExecutarQuery(Sql)

                        'Atualiza TB_BL_TAXA
                        Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_TAXA_INATIVA = 1 WHERE ID_BL_TAXA_MASTER = " & ID_BL_TAXA)

                    End If



                    Return True

                Else ds.Tables(0).Rows(0).Item("FL_TAXA_INATIVA") = True

                    'Inclui registro de historico
                    Sql = "INSERT INTO TB_INATIVACAO_TAXAS (ID_BL_TAXA,FL_TAXA_INATIVA,ID_MOTIVO_INATIVACAO,DS_MOTIVO_INATIVACAO,ID_USUARIO_INATIVACAO,DT_INATIVACAO) VALUES (" & ID_BL_TAXA & ",0," & ID_MOTIVO_INATIVACAO

                    'Verifica se tem descrição
                    If DS_MOTIVO_INATIVACAO <> "" Then
                        Sql &= " , '" & DS_MOTIVO_INATIVACAO & "'"
                    Else
                        Sql &= " , NULL "
                    End If
                    Sql &= " , " & ID_USUARIO & ", GETDATE() )"
                    Con.ExecutarQuery(Sql)
                    'Atualiza TB_BL_TAXA

                    Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_TAXA_INATIVA = 0 WHERE ID_BL_TAXA = " & ID_BL_TAXA)

                    If GRAU = "M" Then
                        'Inclui registro de historico
                        Sql = "INSERT INTO TB_INATIVACAO_TAXAS (ID_BL_TAXA,FL_TAXA_INATIVA,ID_MOTIVO_INATIVACAO,DS_MOTIVO_INATIVACAO,ID_USUARIO_INATIVACAO,DT_INATIVACAO) SELECT ID_BL_TAXA, 1," & ID_MOTIVO_INATIVACAO

                        'Verifica se tem descrição
                        If DS_MOTIVO_INATIVACAO <> "" Then
                            Sql &= " , '" & DS_MOTIVO_INATIVACAO & "'"
                        Else
                            Sql &= " , NULL "
                        End If

                        Sql &= " , " & ID_USUARIO & ", GETDATE() FROM TB_BL_TAXA WHERE ID_BL_TAXA_MASTER = " & ID_BL_TAXA
                        Con.ExecutarQuery(Sql)

                        'Atualiza TB_BL_TAXA
                        Con.ExecutarQuery("UPDATE TB_BL_TAXA SET FL_TAXA_INATIVA = 1 WHERE ID_BL_TAXA_MASTER = " & ID_BL_TAXA)

                    End If

                    Return True

                End If


            Catch ex As Exception

                Return False

            End Try

        Else

            Return False

        End If

    End Function
End Class
