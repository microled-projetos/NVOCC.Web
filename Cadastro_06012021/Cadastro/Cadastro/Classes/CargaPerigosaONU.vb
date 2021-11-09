Imports System.Data.OleDb

Public Class CargaPerigosaONU

    Private _Codigo As String
    Private _CodigoCarga As String
    Private _Descricao As String

    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

    Public Property CodigoCarga() As String
        Get
            Return _CodigoCarga
        End Get
        Set(ByVal value As String)
            _CodigoCarga = value
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

    Public Function Inserir(ByVal Carga As CargaPerigosaONU) As Boolean

        If Banco.Execute(String.Format("INSERT INTO " & Banco.BancoNVOCC & "TB_CAD_ONU (CODE,DESCR) VALUES ('{0}','{1}')", Carga.CodigoCarga, Carga.Descricao)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Alterar(ByVal Carga As CargaPerigosaONU) As Boolean

        If Banco.Execute(String.Format("UPDATE " & Banco.BancoNVOCC & "TB_CAD_ONU SET CODE='{0}',DESCR='{1}' WHERE CODE='{2}'", Carga.CodigoCarga, Carga.Descricao, Carga.Codigo)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Excluir(ByVal Carga As CargaPerigosaONU) As Boolean

        If Banco.Execute(String.Format("DELETE FROM " & Banco.BancoNVOCC & "TB_CAD_ONU WHERE CODE='{0}'", Carga.Codigo)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Consultar() As DataTable
        Return Banco.List("SELECT * FROM " & Banco.BancoNVOCC & "TB_CAD_ONU ORDER BY DESCR ASC")
    End Function

    Public Function ConsultarCarga(ByVal Carga As CargaPerigosaONU) As Boolean

        If Banco.ExecuteScalar(String.Format("SELECT * FROM " & Banco.BancoNVOCC & "TB_CAD_ONU WHERE CODE='{0}'", Carga.CodigoCarga)) > 0 Then
            Return True
        End If

        Return False

    End Function

End Class
