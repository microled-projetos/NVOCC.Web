﻿Imports System.Text

Public Class ExportacaoCSV

    Public Sub New()
        TextoDelimitador = ","c
        TextoQualificadores = """"c
        ColunaTemHeaders = True
    End Sub

    Public Sub New(ByVal txtDelimitador As String, ByVal txtQualificador As String, ByVal temHeaders As Boolean)
        'TextoDelimitador = txtQualificador
        'TextoQualificadores = txtQualificador
        'ColunaTemHeaders = temHeaders


        TextoDelimitador = ","c
        TextoQualificadores = """"c
        ColunaTemHeaders = True
    End Sub

    Private _TextoDelimitador As Char
    Public Property TextoDelimitador() As Char
        Get
            Return _TextoDelimitador
        End Get
        Set(ByVal value As Char)
            _TextoDelimitador = value
        End Set
    End Property
    Private _TextoQualificadores As Char
    Public Property TextoQualificadores() As Char
        Get
            Return _TextoQualificadores
        End Get
        Set(ByVal value As Char)
            _TextoQualificadores = value
        End Set
    End Property
    Private _ColunaTemHeaders As Boolean
    Public Property ColunaTemHeaders() As Boolean
        Get
            Return _ColunaTemHeaders
        End Get
        Set(ByVal value As Boolean)
            _ColunaTemHeaders = value
        End Set
    End Property
    Public Function CsvDoDataTable(ByVal tabelaEntrada As DataTable) As String
        Try
            Dim CsvBuilder As New StringBuilder()
            If ColunaTemHeaders Then
                CriaHeader(tabelaEntrada, CsvBuilder)
            End If
            CriaLinhas(tabelaEntrada, CsvBuilder)
            Return CsvBuilder.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub CriaLinhas(ByVal tabelaEntrada As DataTable, ByVal CsvBuilder As StringBuilder)
        Try
            For Each ExportarRow As DataRow In tabelaEntrada.Rows
                For Each ExportaColuna As DataColumn In tabelaEntrada.Columns
                    Dim ColunaTexto As String = ExportarRow(ExportaColuna.ColumnName).ToString()
                    ColunaTexto = ColunaTexto.Replace(TextoQualificadores.ToString(), TextoQualificadores.ToString() + TextoQualificadores.ToString())
                    CsvBuilder.Append(TextoQualificadores + ColunaTexto + TextoQualificadores)
                    CsvBuilder.Append(TextoDelimitador)
                Next
                CsvBuilder.AppendLine()
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CriaHeader(ByVal tabelaEntrada As DataTable, ByVal CsvBuilder As StringBuilder)
        Try
            For Each ExportaColuna As DataColumn In tabelaEntrada.Columns
                Dim ColunaTexto As String = ExportaColuna.ColumnName.ToString()
                ColunaTexto = ColunaTexto.Replace(TextoQualificadores.ToString(), TextoQualificadores.ToString() + TextoQualificadores.ToString())
                CsvBuilder.Append(TextoQualificadores + ExportaColuna.ColumnName + TextoQualificadores)
                CsvBuilder.Append(TextoDelimitador)
            Next
            CsvBuilder.AppendLine()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class