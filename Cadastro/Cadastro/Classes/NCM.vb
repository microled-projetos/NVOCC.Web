Imports System.Data.OleDb

Public Class NCM

    Private _Codigo As String
    Private _CodigoNCM As String
    Private _Descricao As String
    Private _Anvisa As Byte
    Private _Ibama As Byte
    Private _MAPA As Byte
    Private _Exercito As Byte
    Private _PoliciaCivil As Byte
    Private _PoliciaFederal As Byte

    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

    Public Property CodigoNCM() As String
        Get
            Return _CodigoNCM
        End Get
        Set(ByVal value As String)
            _CodigoNCM = value
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Public Property Anvisa() As Byte
        Get
            Return _Anvisa
        End Get
        Set(ByVal value As Byte)
            _Anvisa = value
        End Set
    End Property

    Public Property Ibama() As Byte
        Get
            Return _Ibama
        End Get
        Set(ByVal value As Byte)
            _Ibama = value
        End Set
    End Property

    Public Property MAPA() As Byte
        Get
            Return _MAPA
        End Get
        Set(ByVal value As Byte)
            _MAPA = value
        End Set
    End Property

    Public Property Exercito() As Byte
        Get
            Return _Exercito
        End Get
        Set(ByVal value As Byte)
            _Exercito = value
        End Set
    End Property

    Public Property PoliciaCivil() As Byte
        Get
            Return _PoliciaCivil
        End Get
        Set(ByVal value As Byte)
            _PoliciaCivil = value
        End Set
    End Property

    Public Property PoliciaFederal() As Byte
        Get
            Return _PoliciaFederal
        End Get
        Set(ByVal value As Byte)
            _PoliciaFederal = value
        End Set
    End Property

    Public Function Inserir(ByVal NCM As NCM) As Boolean

        If Banco.Execute(String.Format("INSERT INTO " & Banco.BancoOPERADOR & "TB_CAD_MERCADORIA (CODIGO,DESCR,FLAG_ANVISA,FLAG_IBAMA,FLAG_MAPA,FLAG_EXERCITO,FLAG_POLICIA_CIVIL,FLAG_POLICIA_FEDERAL) VALUES ('{0}','{1}',{2},{3},{4},{5},{6},{7})", NCM.CodigoNCM, NCM.Descricao, NCM.Anvisa, NCM.Ibama, NCM.MAPA, NCM.Exercito, NCM.PoliciaCivil, NCM.PoliciaFederal)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Alterar(ByVal NCM As NCM) As Boolean

        If Banco.Execute(String.Format("UPDATE " & Banco.BancoOPERADOR & "TB_CAD_MERCADORIA SET CODIGO='{0}',DESCR='{1}',FLAG_ANVISA={2},FLAG_IBAMA={3},FLAG_MAPA={4},FLAG_EXERCITO={5},FLAG_POLICIA_CIVIL={6},FLAG_POLICIA_FEDERAL={7} WHERE CODIGO='{8}'", NCM.CodigoNCM, NCM.Descricao, NCM.Anvisa, NCM.Ibama, NCM.MAPA, NCM.Exercito, NCM.PoliciaCivil, NCM.PoliciaFederal, NCM.Codigo)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Excluir(ByVal NCM As NCM) As Boolean

        If Banco.Execute(String.Format("DELETE FROM " & Banco.BancoOPERADOR & "TB_CAD_MERCADORIA WHERE CODIGO='{0}'", NCM.Codigo)) Then
            Return True
        End If

        Return True

    End Function

    Public Function Consultar() As DataTable
        Return Banco.List("SELECT CODIGO,DESCR,ISNULL(FLAG_ANVISA,0) ANVISA, ISNULL(FLAG_IBAMA,0) IBAMA, ISNULL(FLAG_MAPA,0) MAPA, ISNULL(FLAG_EXERCITO,0) EXERCITO, ISNULL(FLAG_POLICIA_CIVIL,0) POLICIA_CIVIL,ISNULL(FLAG_POLICIA_FEDERAL,0) POLICIA_FEDERAL FROM " & Banco.BancoOPERADOR & "TB_CAD_MERCADORIA ORDER BY DESCR ASC")
    End Function

End Class
