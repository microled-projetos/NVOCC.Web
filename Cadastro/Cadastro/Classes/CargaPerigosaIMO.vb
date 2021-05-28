Imports System.Data.OleDb

Public Class CargaPerigosaIMO

    Private _Codigo As String
    Private _CodigoIMDG As String
    Private _Descricao As String
    Private _Imagem As String

    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(ByVal value As String)
            _Codigo = value
        End Set
    End Property

    Public Property CodigoIMDG() As String
        Get
            Return _CodigoIMDG
        End Get
        Set(ByVal value As String)
            _CodigoIMDG = value
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

    Public Property Imagem() As String
        Get
            Return _Imagem
        End Get
        Set(ByVal value As String)
            _Imagem = value
        End Set
    End Property

    Public Function Inserir(ByVal Carga As CargaPerigosaIMO) As Boolean

        If Banco.Execute(String.Format("INSERT INTO " & Banco.BancoOPERADOR & "TB_CAD_CARGA_PERIGOSA (CODE,DESCR,PLACA) VALUES ('{0}','{1}','{2}')", Carga.CodigoIMDG, Carga.Descricao, Carga.Imagem)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Alterar(ByVal Carga As CargaPerigosaIMO) As Boolean

        If Banco.Execute(String.Format("UPDATE " & Banco.BancoOPERADOR & "TB_CAD_CARGA_PERIGOSA SET CODE='{0}',DESCR='{1}',PLACA='{2}' WHERE CODE='{3}'", Carga.CodigoIMDG, Carga.Descricao, Carga.Imagem, Carga.Codigo)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Excluir(ByVal Carga As CargaPerigosaIMO) As Boolean

        If Banco.Execute(String.Format("DELETE FROM " & Banco.BancoOPERADOR & "TB_CAD_CARGA_PERIGOSA WHERE CODE='{0}'", Carga.Codigo)) Then
            Return True
        End If

        Return False

    End Function

    Public Function Consultar() As DataTable
        Return Banco.List("SELECT * FROM " & Banco.BancoOPERADOR & "TB_CAD_CARGA_PERIGOSA ORDER BY DESCR ASC")
    End Function

    Public Function ConsultarCarga(ByVal Carga As CargaPerigosaIMO) As Boolean

        If Banco.ExecuteScalar(String.Format("SELECT COUNT(*) FROM " & Banco.BancoOPERADOR & "TB_CAD_CARGA_PERIGOSA WHERE CODE='{0}'", Carga.CodigoIMDG)) > 0 Then
            Return True
        End If

        Return False

    End Function

End Class
