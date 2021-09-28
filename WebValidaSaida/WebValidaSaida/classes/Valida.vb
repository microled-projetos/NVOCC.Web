Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Valida

    Private wPatio As String = ""
    Private wlote As String = ""
    Private WValida As Integer = 0
    Private Wdtfim As String = ""
    Private WAutonum As String = ""


    Public Function ValidaSaida_cntr(ByVal wID_Conteiner As String) As String
        Dim SQL As New StringBuilder
        Dim XML As New StringBuilder
        Dim RSAux As New ADODB.Recordset
        Dim TemErro As Byte
        Dim Tipodoc As Byte
        Dim CargaPatio As Byte
        Dim VarFp As Byte

        
        TemErro = 0
        If wID_Conteiner = String.Empty Then
            XML.Append("Conteiner não informado")
            TemErro = 1
        End If


        'Teste1 - Conteiner no estoque

        SQL = New StringBuilder
        SQL.Append("SELECT ISNULL(AUTONUM,0) AUTONUM,ID_CONTEINER,ISNULL(PATIO,0) PATIO , ISNULL(CONVERT(CHAR(23),DT_FIM_PERIODO,103),'01/01/2010') DT_FIM_PERIODO   ,ISNULL(FLAG_BLOQUEIO,0) AS BLOQUEIO_A , ISNULL(FLAG_BLOQUEIO_MANUAL,0) AS BLOQUEIO_T  FROM " & BD.BancoSgipa & "TB_CNTR_BL WHERE FLAG_TERMINAL=1 AND FLAG_HISTORICO=0 AND ID_CONTEINER = '" & wID_Conteiner & "'")
        PRSet(RSAux, SQL.ToString())
        If Not RSAux.EOF Then
            While Not RSAux.EOF
                WAutonum = RSAux.Fields("AUTONUM").Value
                wPatio = RSAux.Fields("PATIO").Value
                Wdtfim = RSAux.Fields("DT_FIM_PERIODO").Value
                If RSAux.Fields("BLOQUEIO_A").Value = 1 Then
                    XML.Append("Conteiner com bloqueio Alfandega")
                    TemErro = 1
                End If
                If RSAux.Fields("BLOQUEIO_T").Value = 1 Then
                    XML.Append("Conteiner com bloqueio Terminal")
                    TemErro = 1
                End If
                RSAux.MoveNext()
            End While
            RSAux.Close()
        Else
            XML.Append("Conteiner não ENCONTRADO no ESTOQUE")
            RSAux.Close()
            Return XML.ToString
        End If


        SQL = New StringBuilder
        SQL.Append(" SELECT AUTONUM, ISNULL(AUDIT_AV,0) Audit_AV, ISNULL(FLAG_CARGA_PATIO,0) CARGAPATIO, ISNULL(AUDIT_DESEMBARACO,0) AS DESEMBARACO,ISNULL(TIPO_DOCUMENTO,0) TIPO_DOCUMENTO ,ISNULL(FLAG_BLOQUEIO,0) AS BLOQUEIO_A , ISNULL(FLAG_BLOQUEIO_MANUAL,0) AS BLOQUEIO_T ")
        SQL.Append(" FROM " & BD.BancoSgipa & "TB_BL WHERE FLAG_ATIVO=1 AND AUTONUM IN(SELECT BL FROM " & BD.BancoSgipa & "TB_AMR_CNTR_BL WHERE CNTR=" & WAutonum & ")")

        PRSet(RSAux, SQL.ToString())
        If RSAux.EOF Then
            XML.Append("Conteiner não ENCONTRADO no ESTOQUE")
            RSAux.Close()
            TemErro = 1
            Return XML.ToString
        End If
        Tipodoc = RSAux.Fields("TIPO_DOCUMENTO").Value

        CargaPatio = RSAux.Fields("CargaPatio").Value

        While Not RSAux.EOF
            If RSAux.Fields("Audit_Av").Value = 0 Then
                XML.Append("Lote " + RSAux.Fields("AUTONUM").Value.ToString + " Não Averbado")
                TemErro = 1
            End If
            If RSAux.Fields("BLOQUEIO_A").Value = 1 Then
                XML.Append("Lote " + RSAux.Fields("AUTONUM").Value.ToString + " com bloqueio Alfandega")
                TemErro = 1
            End If
            If RSAux.Fields("BLOQUEIO_T").Value = 1 Then
                XML.Append("Lote " + RSAux.Fields("AUTONUM").Value.ToString + " com bloqueio Terminal")
                TemErro = 1
            End If
            If CargaPatio <> RSAux.Fields("CARGAPATIO").Value Then
                XML.Append("Nem Todos os B/Ls Envolvidos Tem o mesmo Status em Carga Pátio.")
                TemErro = 1
            End If
            Tipodoc = RSAux.Fields("TIPO_DOCUMENTO").Value
            CargaPatio = RSAux.Fields("CargaPatio").Value
            RSAux.MoveNext()
        End While
        RSAux.Close()
        VarFp = 0
        SQL = New StringBuilder
        SQL.Append("Select ISNULL(Min(ISNULL(forma_pagamento,0)),0) as FP  from tb_listas_precos where autonum in (select ISNULL(autonum_lista,0) from " & BD.BancoSgipa & "tb_bl ")
        SQL.Append("where flag_ativo=1 AND  AUTONUM IN(SELECT BL FROM " & BD.BancoSgipa & "TB_AMR_CNTR_BL WHERE CNTR=" & WAutonum & "))")
        PRSet(RSAux, SQL.ToString())
        If RSAux.Fields("FP").Value = 0 Then
            XML.Append("Lote Não Averbado")
            TemErro = 1
        End If
        VarFp = RSAux.Fields("FP").Value
        If VarFp = 2 Then
            SQL = New StringBuilder
            SQL.Append("SELECT ISNULL(min(ISNULL(forma_Pagamento,0)),0) FP ")
            SQL.Append("FROM " & BD.BancoSgipa & "tb_gr_bl  Where  bl IN(SELECT BL FROM " & BD.BancoSgipa & "TB_AMR_CNTR_BL WHERE CNTR=" & WAutonum & ")")
            PRSet(RSAux, SQL.ToString())
            If Not RSAux.EOF Then
                If RSAux.Fields("FP").Value = 0 Then
                    XML.Append("Lote sem calculo de GR")
                    TemErro = 1
                End If
            End If
            RSAux.Close()
        End If

        If TemErro = 0 Then
            If CDate(Wdtfim) < CDate(Now) Then
                XML.Append("Cálculo Vencido")
                TemErro = 1
            End If
        End If
        Return XML.ToString

    End Function


    Public Function ValidaSaida_Cs(ByVal wLote As String) As String
        Dim SQL As New StringBuilder
        Dim XML As New StringBuilder
        Dim RSAux As New ADODB.Recordset
        Dim TemErro As Byte
        Dim Tipodoc As Byte
        Dim CargaPatio As Byte
        Dim VarFp As Byte


        TemErro = 0
        If wLote = String.Empty Then
            XML.Append("Lote não informado")
            TemErro = 1
        End If


        'Teste1 - Lote no estoque

        SQL = New StringBuilder
        SQL.Append("SELECT ISNULL(AUTONUM,0) AUTONUM,BL,ISNULL(PATIO,0) PATIO ,  ISNULL(CONVERT(CHAR(23),DT_FIM_PERIODO,103),'01/01/2010') DT_FIM_PERIODO    FROM " & BD.BancoSgipa & "TB_CARGA_SOLTA WHERE FLAG_TERMINAL=1 AND FLAG_HISTORICO=0 AND BL = '" & wLote & "'")
        PRSet(RSAux, SQL.ToString())
        If Not RSAux.EOF Then
            While Not RSAux.EOF
                WAutonum = RSAux.Fields("AUTONUM").Value
                wPatio = RSAux.Fields("PATIO").Value
                Wdtfim = RSAux.Fields("DT_FIM_PERIODO").Value
                RSAux.MoveNext()
            End While
            RSAux.Close()
        Else
            XML.Append("lOTE não ENCONTRADO no ESTOQUE")
            RSAux.Close()
            Return XML.ToString
        End If


        SQL = New StringBuilder
        SQL.Append(" SELECT AUTONUM, ISNULL(AUDIT_AV,0) Audit_AV, ISNULL(AUDIT_DESEMBARACO,0) AS DESEMBARACO,ISNULL(TIPO_DOCUMENTO,0) TIPO_DOCUMENTO ,ISNULL(FLAG_BLOQUEIO,0) AS BLOQUEIO_A , ISNULL(FLAG_BLOQUEIO_MANUAL,0) AS BLOQUEIO_T ")
        SQL.Append(" FROM " & BD.BancoSgipa & "TB_BL WHERE FLAG_ATIVO=1 AND AUTONUM =" & wLote & "")

        PRSet(RSAux, SQL.ToString())
        If RSAux.EOF Then
            XML.Append("Lote não ENCONTRADO no ESTOQUE")
            RSAux.Close()
            TemErro = 1
            Return XML.ToString
        End If
        Tipodoc = RSAux.Fields("TIPO_DOCUMENTO").Value


        While Not RSAux.EOF
            If RSAux.Fields("Audit_Av").Value = 0 Then
                XML.Append("Lote " + RSAux.Fields("AUTONUM").Value.ToString + " Não Averbado")
                TemErro = 1
            End If
            If RSAux.Fields("BLOQUEIO_A").Value = 1 Then
                XML.Append("Lote " + RSAux.Fields("AUTONUM").Value.ToString + " com bloqueio Alfandega")
                TemErro = 1
            End If
            If RSAux.Fields("BLOQUEIO_T").Value = 1 Then
                XML.Append("Lote " + RSAux.Fields("AUTONUM").Value.ToString + " com bloqueio Terminal")
                TemErro = 1
            End If
            Tipodoc = RSAux.Fields("TIPO_DOCUMENTO").Value
            RSAux.MoveNext()
        End While
        RSAux.Close()
        VarFp = 0
        SQL = New StringBuilder
        SQL.Append("Select ISNULL(Min(ISNULL(forma_pagamento,0)),0) as FP  from tb_listas_precos where autonum in (select ISNULL(autonum_lista,0) from " & BD.BancoSgipa & "tb_bl ")
        SQL.Append("where flag_ativo=1 AND  AUTONUM =" & wLote & ")")
        PRSet(RSAux, SQL.ToString())
        If RSAux.Fields("FP").Value = 0 Then
            XML.Append("Lote Não Averbado")
            TemErro = 1
        End If
        VarFp = RSAux.Fields("FP").Value
        If VarFp = 2 Then
            SQL = New StringBuilder
            SQL.Append("SELECT ISNULL(min(ISNULL(forma_Pagamento,0)),0) FP ")
            SQL.Append("FROM " & BD.BancoSgipa & "tb_gr_bl  Where  bl =" & wLote & "")
            PRSet(RSAux, SQL.ToString())
            If Not RSAux.EOF Then
                If RSAux.Fields("FP").Value = 0 Then
                    XML.Append("Lote sem calculo de GR")
                    TemErro = 1
                End If
            End If
            RSAux.Close()
        End If

        If TemErro = 0 Then
            If CDate(Wdtfim) < CDate(Now) Then
                XML.Append("Cálculo Vencido")
                TemErro = 1
            End If
        End If
        Return XML.ToString
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
