Public Class Estabelecimento
    Public Property cnpj As String
    Public Property atividades_secundarias As List(Of AtividadesSecundaria)
    Public Property cnpj_raiz As String
    Public Property cnpj_ordem As String
    Public Property cnpj_digito_verificador As String
    Public Property tipo As String
    Public Property nome_fantasia As String
    Public Property situacao_cadastral As String
    Public Property data_situacao_cadastral As String
    Public Property data_inicio_atividade As String
    Public Property nome_cidade_exterior As Object
    Public Property tipo_logradouro As String
    Public Property logradouro As String
    Public Property numero As String
    Public Property complemento As Object
    Public Property bairro As String
    Public Property cep As String
    Public Property ddd1 As String
    Public Property telefone1 As String
    Public Property ddd2 As String
    Public Property telefone2 As String
    Public Property ddd_fax As String
    Public Property fax As String
    Public Property email As Object
    Public Property situacao_especial As Object
    Public Property data_situacao_especial As Object
    Public Property atividade_principal As AtividadePrincipal
    Public Property pais As Pais
    Public Property estado As Estado
    Public Property cidade As Cidade
    Public Property motivo_situacao_cadastral As Object
    Public Property inscricoes_estaduais As List(Of InscricoesEstaduais)

End Class
Public Class AtividadePrincipal
    Public Property id As String
    Public Property secao As String
    Public Property divisao As String
    Public Property grupo As String
    Public Property classe As String
    Public Property subclasse As String
    Public Property descricao As String
End Class

Public Class AtividadesSecundaria
    Public Property id As String
    Public Property secao As String
    Public Property divisao As String
    Public Property grupo As String
    Public Property classe As String
    Public Property subclasse As String
    Public Property descricao As String
End Class

Public Class Cidade
    Public Property id As Integer
    Public Property nome As String
    Public Property ibge_id As Integer
    Public Property siafi_id As String
End Class

'Public Class Estabelecimento
'Public Property cnpj As String
'    Public Property atividades_secundarias As List(Of AtividadesSecundaria)
'    Public Property cnpj_raiz As String
'    Public Property cnpj_ordem As String
'    Public Property cnpj_digito_verificador As String
'    Public Property tipo As String
'    Public Property nome_fantasia As String
'    Public Property situacao_cadastral As String
'    Public Property data_situacao_cadastral As String
'    Public Property data_inicio_atividade As String
'    Public Property nome_cidade_exterior As Object
'    Public Property tipo_logradouro As String
'    Public Property logradouro As String
'    Public Property numero As String
'    Public Property complemento As Object
'    Public Property bairro As String
'    Public Property cep As String
'    Public Property ddd1 As String
'    Public Property telefone1 As String
'    Public Property ddd2 As String
'    Public Property telefone2 As String
'    Public Property ddd_fax As String
'    Public Property fax As String
'    Public Property email As Object
'    Public Property situacao_especial As Object
'    Public Property data_situacao_especial As Object
'    Public Property atividade_principal As AtividadePrincipal
'    Public Property pais As Pais
'    Public Property estado As Estado
'    Public Property cidade As Cidade
'    Public Property motivo_situacao_cadastral As Object
'    Public Property inscricoes_estaduais As List(Of InscricoesEstaduai)
' End Class

Public Class Estado
    Public Property id As Integer
    Public Property nome As String
    Public Property sigla As String
    Public Property ibge_id As Integer
End Class

Public Class InscricoesEstaduais
    Public Property inscricao_estadual As String
    Public Property ativo As Boolean
    Public Property atualizado_em As DateTime
    Public Property estado As Estado
End Class

Public Class NaturezaJuridica
    Public Property id As String
    Public Property descricao As String
End Class

Public Class Pais
    Public Property id As String
    Public Property iso2 As String
    Public Property iso3 As String
    Public Property nome As String
    Public Property comex_id As String
End Class

Public Class Porte
    Public Property id As String
    Public Property descricao As String
End Class

Public Class QualificacaoDoResponsavel
    Public Property id As Integer
    Public Property descricao As String
End Class

Public Class QualificacaoSocio
    Public Property id As Integer
    Public Property descricao As String
End Class

Public Class Root
    Public Property cnpj_raiz As String
    Public Property razao_social As String
    Public Property capital_social As String
    Public Property responsavel_federativo As String
    Public Property atualizado_em As DateTime
    Public Property porte As Porte
    Public Property natureza_juridica As NaturezaJuridica
    Public Property qualificacao_do_responsavel As QualificacaoDoResponsavel
    Public Property socios As List(Of Socio)
    Public Property simples As Object
    Public Property estabelecimento As Estabelecimento
End Class

Public Class Socio
    Public Property cpf_cnpj_socio As String
    Public Property nome As String
    Public Property tipo As String
    Public Property data_entrada As String
    Public Property cpf_representante_legal As String
    Public Property nome_representante As Object
    Public Property faixa_etaria As String
    Public Property atualizado_em As DateTime
    Public Property pais_id As String
    Public Property qualificacao_socio As QualificacaoSocio
    Public Property qualificacao_representante As Object
End Class


