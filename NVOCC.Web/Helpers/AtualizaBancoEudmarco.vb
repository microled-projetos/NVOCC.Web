Public Class AtualizaBancoEudmarco

    Sub Atualizar(ID_BL As String)

        If ID_BL = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()


            ''BUSCA INFORMACOES BASICAS
            Dim dsNVOCC As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,A.NR_BL,A.NR_PROCESSO,A.ID_PARCEIRO_CLIENTE,A.ID_PARCEIRO_TRANSPORTADOR,A.ID_PARCEIRO_IMPORTADOR,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.NR_VIAGEM,CONVERT(VARCHAR,A.DT_ABERTURA,103)DT_ABERTURA,A.NR_CE,A.VL_M3,B.ID_CNTR_BL,B.ID_TIPO_CNTR,B.NR_CNTR,B.NR_LACRE,B.VL_PESO_TARA,C.TAMANHO_CONTAINER,ISNULL(C.ISO,'NULL')ISO
FROM TB_BL A 
INNER JOIN TB_CNTR_BL B ON B.ID_BL_MASTER = A.ID_BL
INNER JOIN TB_TIPO_CONTAINER C ON C.ID_TIPO_CONTAINER = B.ID_TIPO_CNTR
WHERE A.GRAU ='M' AND DT_EMBARQUE IS NOT NULL AND ID_PARCEIRO_ARMAZEM_DESCARGA = 74 AND A.ID_BL = " & ID_BL)
            If dsNVOCC.Tables(0).Rows.Count > 0 Then

                'VERIFICA SE O BL JÁ EXISTE NO BANCO DA EUDMARCO
                Dim ConOracle As New Conexao_oracle
                ConOracle.Conectar()
                Dim dtEudmarco As DataTable = ConOracle.Consultar("SELECT AUTONUM FROM TB_BL WHERE NUMERO = '" & dsNVOCC.Tables(0).Rows(0).Item("NR_BL") & "' ")

                If dtEudmarco.Rows.Count = 0 Then

                    'INSERE BL
                    'ConOracle.ExecuteScalar("INSERT INTO TB_BL ( AUTONUM,DT_BL,VIAGEM,NUMERO,PORTO_ORIGEM,PORTO_DESTINO,ARMADOR,IMPORTADOR ) VALUES (SGIPA.SEQ_BL.NEXTVAL," & dsNVOCC.Tables(0).Rows(0).Item("DT_ABERTURA") & "," & dsNVOCC.Tables(0).Rows(0).Item("NR_VIAGEM") & "," & dsNVOCC.Tables(0).Rows(0).Item("NR_BL") & "," & dsNVOCC.Tables(0).Rows(0).Item("ID_PORTO_ORIGEM") & "," & dsNVOCC.Tables(0).Rows(0).Item("ID_PORTO_DESTINO") & "," & dsNVOCC.Tables(0).Rows(0).Item("ID_PARCEIRO_TRANSPORTADOR") & "," & dsNVOCC.Tables(0).Rows(0).Item("ID_PARCEIRO_IMPORTADOR") & "," & dsNVOCC.Tables(0).Rows(0).Item("ID_PARCEIRO_CLIENTE") & ")")

                    ConOracle.ExecuteScalar("INSERT INTO TB_BL ( AUTONUM,GRAU,DT_BL,VIAGEM,NUMERO,CE,VOLUME_M3  ) VALUES (SGIPA.SEQ_BL.NEXTVAL,'M', TO_DATE(nvl('','" & dsNVOCC.Tables(0).Rows(0).Item("DT_ABERTURA") & "'),'DD/MM/YYYY HH24:MI:SS'),'" & dsNVOCC.Tables(0).Rows(0).Item("NR_VIAGEM") & "','" & dsNVOCC.Tables(0).Rows(0).Item("NR_BL") & "','" & dsNVOCC.Tables(0).Rows(0).Item("NR_CE") & "'," & dsNVOCC.Tables(0).Rows(0).Item("VL_M3").ToString.Replace(",", ".") & " ) ")
                    Dim dtInsert As DataTable = ConOracle.Consultar("SELECT SEQ_BL.CURRVAL FROM DUAL")
                    Dim AUTONUM_BL As String = dtInsert.Rows(0)("CURRVAL").ToString

                    'INSERE CNTR
                    For Each linha As DataRow In dsNVOCC.Tables(0).Rows

                        If linha.Item("ID_CNTR_BL").ToString <> 0 Then
                            'ConOracle.ExecuteScalar("INSERT INTO TB_CNTR_BL ( AUTONUM,ID_CONTEINER,TIPO,ISO,TAMANHO,TARA,LACRE_ORIGEM ) VALUES (SGIPA.SEQ_CNTR_BL.NEXTVAL," & dsNVOCC.Tables(0).Rows(0).Item("NR_CNTR") & "," & dsNVOCC.Tables(0).Rows(0).Item("ID_TIPO_CNTR") & "," & dsNVOCC.Tables(0).Rows(0).Item("ISO") & "," & dsNVOCC.Tables(0).Rows(0).Item("TAMANHO_CONTAINER") & "," & dsNVOCC.Tables(0).Rows(0).Item("VL_PESO_TARA") & "," & dsNVOCC.Tables(0).Rows(0).Item("NR_LACRE") & " )")
                            ConOracle.ExecuteScalar("INSERT INTO TB_CNTR_BL ( AUTONUM,ID_CONTEINER,TAMANHO,TARA,LACRE_ORIGEM ) VALUES (SGIPA.SEQ_CNTR_BL.NEXTVAL,'" & dsNVOCC.Tables(0).Rows(0).Item("NR_CNTR") & "'," & dsNVOCC.Tables(0).Rows(0).Item("TAMANHO_CONTAINER") & "," & dsNVOCC.Tables(0).Rows(0).Item("VL_PESO_TARA").ToString.Replace(",", ".") & ",'" & dsNVOCC.Tables(0).Rows(0).Item("NR_LACRE") & "' ) ")
                            dtInsert = ConOracle.Consultar("SELECT SEQ_CNTR_BL.CURRVAL FROM DUAL ")

                            Dim AUTONUM_CNTRL As String = dtInsert.Rows(0)("CURRVAL").ToString

                            'INSERE AMR CNTR BL
                            dtInsert = ConOracle.ExecuteScalar("INSERT INTO TB_AMR_CNTR_BL ( AUTONUM,CNTR,BL ) VALUES (SGIPA.SEQ_AMR_CNTR_BL.NEXTVAL," & AUTONUM_BL & "," & AUTONUM_CNTRL & ")")
                        End If

                    Next



                End If

            End If
        End If

    End Sub
End Class
