Public Class AtualizaBancoEudmarco

    Sub Atualizar(ID_BL As String)

        If ID_BL = "" Then
            Exit Sub
        Else
            Dim Con As New Conexao_sql
            Con.Conectar()


            ''BUSCA INFORMACOES BASICAS
            Dim dsNVOCC As DataSet = Con.ExecutarQuery("SELECT A.ID_BL,A.NR_BL,A.NR_PROCESSO,A.ID_PARCEIRO_CLIENTE,A.ID_PARCEIRO_TRANSPORTADOR,A.ID_PARCEIRO_IMPORTADOR,A.ID_PORTO_ORIGEM,A.ID_PORTO_DESTINO,A.NR_VIAGEM,CONVERT(VARCHAR,A.DT_ABERTURA,103)DT_ABERTURA,ISNULL(A.NR_CE,'')NR_CE,ISNULL(A.VL_M3,0)VL_M3,B.ID_CNTR_BL,B.ID_TIPO_CNTR,B.NR_CNTR,B.NR_LACRE,B.VL_PESO_TARA,C.TAMANHO_CONTAINER,ISNULL(C.ISO,'NULL')ISO
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

                    ConOracle.ExecuteScalar("INSERT INTO TB_BL ( AUTONUM,GRAU,DT_BL,VIAGEM,NUMERO,CE,VOLUME_M3  ) VALUES (SGIPA.SEQ_BL.NEXTVAL,'M', TO_DATE(nvl('','" & dsNVOCC.Tables(0).Rows(0).Item("DT_ABERTURA") & "'),'DD/MM/YYYY HH24:MI:SS'),'" & dsNVOCC.Tables(0).Rows(0).Item("NR_VIAGEM") & "','" & dsNVOCC.Tables(0).Rows(0).Item("NR_BL") & "','" & dsNVOCC.Tables(0).Rows(0).Item("NR_CE") & "'," & dsNVOCC.Tables(0).Rows(0).Item("VL_M3").ToString.Replace(",", ".") & " ) ")
                    Dim dtInsert As DataTable = ConOracle.Consultar("SELECT SEQ_BL.CURRVAL FROM DUAL")
                    Dim AUTONUM_BL As String = dtInsert.Rows(0)("CURRVAL").ToString

                    'INSERE CNTR
                    For Each linha As DataRow In dsNVOCC.Tables(0).Rows

                        If linha.Item("ID_CNTR_BL").ToString <> 0 Then
                            Dim dtEudmarcoCNTR As DataTable = ConOracle.Consultar("SELECT AUTONUM FROM TB_CNTR_BL WHERE ID_CONTEINER = '" & linha.Item("NR_CNTR") & "' ")

                            'VERIFICA SE O CNTR JÁ EXISTE NO BANCO DA EUDMARCO
                            If dtEudmarcoCNTR.Rows.Count = 0 Then

                                ConOracle.ExecuteScalar("INSERT INTO TB_CNTR_BL ( AUTONUM,ID_CONTEINER,TAMANHO,TARA,LACRE_ORIGEM ) VALUES (SGIPA.SEQ_CNTR_BL.NEXTVAL,'" & linha.Item("NR_CNTR") & "'," & linha.Item("TAMANHO_CONTAINER") & "," & linha.Item("VL_PESO_TARA").ToString.Replace(",", ".") & ",'" & linha.Item("NR_LACRE") & "' ) ")
                                dtInsert = ConOracle.Consultar("SELECT SEQ_CNTR_BL.CURRVAL FROM DUAL ")

                                Dim AUTONUM_CNTRL As String = dtInsert.Rows(0)("CURRVAL").ToString

                                'INSERE AMR CNTR BL
                                ConOracle.ExecuteScalar("INSERT INTO TB_AMR_CNTR_BL ( AUTONUM,BL,CNTR ) VALUES (SGIPA.SEQ_AMR_CNTR_BL.NEXTVAL," & AUTONUM_BL & "," & AUTONUM_CNTRL & ")")


                                'INSERE CARGAS DO CNTR EM QUESTAO
                                Dim dsCargaNVOCC As DataSet = Con.ExecutarQuery("SELECT ID_CNTR_BL,VL_M3,VL_PESO_BRUTO,DS_GRUPO_NCM,QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL IN (SELECT ID_BL FROM TB_BL WHERE GRAU='C' AND ID_BL_MASTER = " & ID_BL & ") and ID_CNTR_BL = " & linha.Item("ID_CNTR_BL"))
                                For Each linhaCarga As DataRow In dsCargaNVOCC.Tables(0).Rows

                                    If linhaCarga.Item("ID_CNTR_BL").ToString <> 0 Then

                                        ConOracle.ExecuteScalar("INSERT INTO TB_CARGA_CNTR ( AUTONUM,BL,ID_CONTEINER,QUANTIDADE,PESO_BRUTO,VOLUME_M3,NCM ) VALUES (SGIPA.SEQ_CARGA_CNTR.NEXTVAL," & AUTONUM_BL & ",'" & linha.Item("NR_CNTR") & "'," & linhaCarga.Item("QT_MERCADORIA").ToString & "," & linhaCarga.Item("VL_PESO_BRUTO").ToString.Replace(",", ".") & "," & linhaCarga.Item("VL_M3").ToString.Replace(",", ".") & ",'" & linhaCarga.Item("DS_GRUPO_NCM").ToString & "')")

                                    End If

                                Next


                            Else

                                'VERIFICA SE JÁ EXISTE AMARRAÇÃO DO CNTR EM QUESTAO COM O BL
                                Dim dtEudmarcoAMR As DataTable = ConOracle.Consultar("SELECT AUTONUM FROM TB_CARGA_CNTR WHERE ID_CONTEINER = ''" & linha.Item("NR_CNTR") & "' AND BL = " & AUTONUM_BL)
                                If dtEudmarcoAMR.Rows.Count = 0 Then
                                    'INSERE AMR CNTR BL
                                    ConOracle.ExecuteScalar("INSERT INTO TB_AMR_CNTR_BL ( AUTONUM,BL,CNTR ) VALUES (SGIPA.SEQ_AMR_CNTR_BL.NEXTVAL," & AUTONUM_BL & "," & dtEudmarcoCNTR.Rows(0)("AUTONUM").ToString & ")")
                                End If

                                'VERIFICA SE JÁ EXISTE CARGAS DO CNTR EM QUESTAO
                                Dim dtEudmarcoCarga As DataTable = ConOracle.Consultar("SELECT AUTONUM FROM TB_CARGA_CNTR WHERE ID_CONTEINER = " & linha.Item("NR_CNTR") & " AND BL = " & AUTONUM_BL)
                                If dtEudmarcoCarga.Rows.Count = 0 Then
                                    Dim dsCargaNVOCC As DataSet = Con.ExecutarQuery("SELECT ID_CNTR_BL,VL_M3,VL_PESO_BRUTO,DS_GRUPO_NCM,QT_MERCADORIA FROM TB_CARGA_BL WHERE ID_BL IN (SELECT ID_BL FROM TB_BL WHERE GRAU='C' AND ID_BL_MASTER = " & ID_BL & ") and ID_CNTR_BL = " & linha.Item("ID_CNTR_BL"))
                                    For Each linhaCarga As DataRow In dsCargaNVOCC.Tables(0).Rows

                                        If linhaCarga.Item("ID_CNTR_BL").ToString <> 0 Then
                                            'INSERE CARGAS DO CNTR
                                            ConOracle.ExecuteScalar("INSERT INTO TB_CARGA_CNTR ( AUTONUM,BL,ID_CONTEINER,QUANTIDADE,PESO_BRUTO,VOLUME_M3,NCM ) VALUES (SGIPA.SEQ_CARGA_CNTR.NEXTVAL," & AUTONUM_BL & ",'" & linha.Item("NR_CNTR") & "'," & linhaCarga.Item("QT_MERCADORIA").ToString & "," & linhaCarga.Item("VL_PESO_BRUTO").ToString.Replace(",", ".") & "," & linhaCarga.Item("VL_M3").ToString.Replace(",", ".") & ",'" & linhaCarga.Item("DS_GRUPO_NCM").ToString & "')")

                                        End If

                                    Next

                                End If

                            End If
                        End If

                    Next



                End If

            End If
        End If

    End Sub
End Class
