Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class FrmCrystal

    Protected _RptName As String
    Protected _RptPaperOrientation As Integer
    Protected _RptFormulas As List(Of String)
    Protected _RptFormulasName As List(Of String)
    Protected _RptSqlQuery As String
    Private _isSetUp As Boolean = False

    Private rptDoc As ReportDocument

    Public QualEnd As String
    Public DiretoImpressora As Boolean = False
    Public GerarPDF As Byte = 0

    Public Property RptName As String
        Get
            Return _RptName
        End Get
        Set(ByVal value As String)
            _RptName = value
        End Set
    End Property
    Public Property RptPaperOrientation As Integer
        Get
            Return _RptPaperOrientation
        End Get
        Set(ByVal value As Integer)
            _RptPaperOrientation = value
        End Set
    End Property
    Public ReadOnly Property RptFormulas As List(Of String)
        Get
            Return _RptFormulas
        End Get
    End Property
    Public ReadOnly Property RptFormulasName As List(Of String)
        Get
            Return _RptFormulasName
        End Get
    End Property
    Public Property RptSqlQuery As String
        Get
            Return _RptSqlQuery
        End Get
        Set(ByVal value As String)
            _RptSqlQuery = value
        End Set
    End Property

    Protected Overridable Sub init()
        _RptName = String.Empty
        _RptPaperOrientation = 0
        _RptFormulas = New List(Of String)
        _RptFormulasName = New List(Of String)
        _RptSqlQuery = String.Empty

        rptDoc = New ReportDocument()
    End Sub

    Public Overridable Sub Clear()
        _RptName = String.Empty
        _RptPaperOrientation = 0
        _RptFormulas.Clear()
        _RptFormulasName.Clear()
        _RptSqlQuery = String.Empty

        _isSetUp = False

        rptDoc.Dispose()
        rptDoc = Nothing
        GC.Collect()
        rptDoc = New ReportDocument()

    End Sub

    Private Function tudoPreenchido(Optional ByVal verificaParametros As Boolean = False) As Boolean
        Dim ret As Boolean =
            (Not String.IsNullOrEmpty(_RptName)) And
            IIf(verificaParametros, (_RptFormulas.Count > 0), True) And
            IIf(verificaParametros, (_RptFormulasName.Count > 0), True) And
            (Not String.IsNullOrEmpty(_RptSqlQuery))
        Return ret
    End Function

    Private Function setupDoc()
        Dim ret As Boolean = True
        Try
            _isSetUp = False
            If tudoPreenchido() Then
                rptDoc.FileName = _RptName

                Dim crTableLogonInfos As TableLogOnInfos = New TableLogOnInfos()
                Dim crTableLogonInfo As TableLogOnInfo = New TableLogOnInfo()
                Dim crTables As Tables = rptDoc.Database.Tables
                Dim crConnectionInfo As ConnectionInfo = New ConnectionInfo() With {
                    .ServerName = Banco.Servidor,
                    .DatabaseName = Banco.Usuario,
                    .UserID = Banco.Usuario,
                    .Password = Banco.Senha,
                    .Type = ConnectionInfoType.Query
                }

                For Each crTable As Table In crTables
                    crTableLogonInfo = crTable.LogOnInfo
                    crTableLogonInfo.ConnectionInfo = crConnectionInfo
                    crTable.ApplyLogOnInfo(crTableLogonInfo)
                Next

                For Each subrep As ReportDocument In rptDoc.Subreports
                    For Each table As Table In subrep.Database.Tables
                        table.LogOnInfo.ConnectionInfo = crConnectionInfo
                        table.ApplyLogOnInfo(table.LogOnInfo)
                    Next
                Next

                If rptDoc.DataDefinition.FormulaFields.Count > 0 Then
                    If _RptFormulasName.Count > 0 And _RptFormulasName.Count > 0 Then
                        Dim minItem = _RptFormulasName.Count
                        If _RptFormulasName.Count <> _RptFormulasName.Count Then
                            If _RptFormulas.Count < minItem Then minItem = _RptFormulas.Count
                        End If

                        For i = 0 To minItem - 1
                            rptDoc.DataDefinition.FormulaFields(_RptFormulasName(i)).Text = _RptFormulas(i)
                        Next i
                    End If
                End If

                rptDoc.SetDataSource(Banco.List(_RptSqlQuery))

                rptDoc.Refresh()
                CrystalReportViewer1.ReportSource = rptDoc

                _isSetUp = True
            Else

                ret = False
                MessageBox.Show("Existem propriedades não preenchidas, ")
            End If

        Catch ex As Exception
            MessageBox.Show("ERRO CONFIGURANDO RELATÓRIO:" & vbCrLf & vbCrLf & ex.Message & vbCrLf & vbCrLf & ex.StackTrace)
        End Try

        Return ret
    End Function

    Sub New()
        InitializeComponent()

        Me.init()
    End Sub

    Sub New(ByVal nomeRelatorio As String,
            ByVal orientacao As Integer,
            ByVal sqlQuery As String,
            Optional ByVal formulas As List(Of String) = Nothing,
            Optional ByVal valores As List(Of String) = Nothing)
        InitializeComponent()
        Me.init()

        _RptName = nomeRelatorio
        _RptPaperOrientation = orientacao
        If Not IsNothing(formulas) Then _RptFormulasName = formulas
        If Not IsNothing(valores) Then _RptFormulas = valores
        _RptSqlQuery = sqlQuery

        setupDoc()
    End Sub

    Public Overridable Sub Imprimir()
        Dim mandaVer As Boolean = True
        If Not Me._isSetUp Then mandaVer = Me.setupDoc()

        If mandaVer Then CrystalReportViewer1.PrintReport()

    End Sub

    Public Sub exportar(ByVal caminhoRPT As String, ByVal caminhoPDF As String)
        Dim cryRPT As New ReportDocument
        cryRPT = CrystalReportViewer1.ReportSource
        cryRPT.ExportToDisk(ExportFormatType.PortableDocFormat, caminhoPDF)
        Me.Close()
    End Sub

    Private Sub FormCrystalReporter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim PdfName As String

        If GerarPDF = 1 Then
            PdfName = "c:\microledtemp\" & QualEnd & ".PDF"
            Dim cryRPT As New ReportDocument
            cryRPT = CrystalReportViewer1.ReportSource
            cryRPT.ExportToDisk(ExportFormatType.PortableDocFormat, PdfName)
            Me.Close()
        End If

        If DiretoImpressora Then
            CrystalReportViewer1.PrintReport()
            Me.Close()
        End If

    End Sub

End Class