Imports System.Data.OleDb

Public Class NCM

    Private _Codigo As String
    Private _CodigoNCM As String
    Private _Descricao As String
    Private _AP_NCM As String
    Private _FL_ATIVO As Byte


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

    Public Property AP_NCM() As String
        Get
            Return _AP_NCM
        End Get
        Set(ByVal value As String)
            _AP_NCM = value
        End Set
    End Property
    Public Property FL_ATIVO() As String
        Get
            Return _FL_ATIVO
        End Get
        Set(ByVal value As String)
            _FL_ATIVO = value
        End Set
    End Property

    Public Function Inserir(ByVal NCM As NCM) As Boolean

        If Banco.Execute(String.Format("INSERT INTO " & Banco.BancoNVOCC & " TB_NCM (CD_NCM, NM_NCM, AP_NCM, FL_ATIVO) VALUES ('{0}','{1}','{2}',{3})", NCM.CodigoNCM, NCM.Descricao, NCM.AP_NCM, NCM.FL_ATIVO)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Alterar(ByVal NCM As NCM) As Boolean

        If Banco.Execute(String.Format("UPDATE " & Banco.BancoNVOCC & "TB_NCM SET CD_NCM = '{0}', NM_NCM='{1}',AP_NCM='{2}',FL_ATIVO={3} WHERE ID_NCM={4}", NCM.CodigoNCM, NCM.Descricao, NCM.AP_NCM, NCM.FL_ATIVO, NCM.Codigo)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Excluir(ByVal NCM As NCM) As Boolean

        If Banco.Execute(String.Format("DELETE FROM " & Banco.BancoNVOCC & "TB_NCM WHERE ID_NCM={0}", NCM.Codigo)) Then
            Return True
        End If

        Return True

    End Function

    Public Function Consultar() As DataTable
        Return Banco.List("SELECT ID_NCM, CD_NCM, AP_NCM, NM_NCM, FL_ATIVO FROM [dbo].[TB_NCM] ORDER BY ID_NCM ASC")
    End Function

End Class
