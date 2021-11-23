﻿'------------------------------------------------------------------------------
' <auto-generated>
'     O código foi gerado por uma ferramenta.
'     Versão de Tempo de Execução:4.0.30319.42000
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for gerado novamente.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
'
Namespace NotaFiscal
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="WsNvoccSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class WsNvocc
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private IntegraNFePrefeituraOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ConsultaNFePrefeituraOperationCompleted As System.Threading.SendOrPostCallback
        
        Private CancelaNFePrefeituraOperationCompleted As System.Threading.SendOrPostCallback
        
        Private SubstituiNFePrefeituraOperationCompleted As System.Threading.SendOrPostCallback
        
        Private DesBloqueioOperationCompleted As System.Threading.SendOrPostCallback
        
        Private StatusBloqueioOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.NVOCC.Web.My.MySettings.Default.NVOCC_Web_NotaFiscal_WsNvocc
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event IntegraNFePrefeituraCompleted As IntegraNFePrefeituraCompletedEventHandler
        
        '''<remarks/>
        Public Event ConsultaNFePrefeituraCompleted As ConsultaNFePrefeituraCompletedEventHandler
        
        '''<remarks/>
        Public Event CancelaNFePrefeituraCompleted As CancelaNFePrefeituraCompletedEventHandler
        
        '''<remarks/>
        Public Event SubstituiNFePrefeituraCompleted As SubstituiNFePrefeituraCompletedEventHandler
        
        '''<remarks/>
        Public Event DesBloqueioCompleted As DesBloqueioCompletedEventHandler
        
        '''<remarks/>
        Public Event StatusBloqueioCompleted As StatusBloqueioCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IntegraNFePrefeitura", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function IntegraNFePrefeitura(ByVal RPS As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal Reprocessamento As Boolean) As String
            Dim results() As Object = Me.Invoke("IntegraNFePrefeitura", New Object() {RPS, CodEmpresa, BancoDestino, StringConexaoDestino, Reprocessamento})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub IntegraNFePrefeituraAsync(ByVal RPS As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal Reprocessamento As Boolean)
            Me.IntegraNFePrefeituraAsync(RPS, CodEmpresa, BancoDestino, StringConexaoDestino, Reprocessamento, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub IntegraNFePrefeituraAsync(ByVal RPS As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal Reprocessamento As Boolean, ByVal userState As Object)
            If (Me.IntegraNFePrefeituraOperationCompleted Is Nothing) Then
                Me.IntegraNFePrefeituraOperationCompleted = AddressOf Me.OnIntegraNFePrefeituraOperationCompleted
            End If
            Me.InvokeAsync("IntegraNFePrefeitura", New Object() {RPS, CodEmpresa, BancoDestino, StringConexaoDestino, Reprocessamento}, Me.IntegraNFePrefeituraOperationCompleted, userState)
        End Sub
        
        Private Sub OnIntegraNFePrefeituraOperationCompleted(ByVal arg As Object)
            If (Not (Me.IntegraNFePrefeituraCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent IntegraNFePrefeituraCompleted(Me, New IntegraNFePrefeituraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConsultaNFePrefeitura", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ConsultaNFePrefeitura(ByVal ID_Faturamento As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String) As String
            Dim results() As Object = Me.Invoke("ConsultaNFePrefeitura", New Object() {ID_Faturamento, CodEmpresa, BancoDestino, StringConexaoDestino})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ConsultaNFePrefeituraAsync(ByVal ID_Faturamento As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String)
            Me.ConsultaNFePrefeituraAsync(ID_Faturamento, CodEmpresa, BancoDestino, StringConexaoDestino, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ConsultaNFePrefeituraAsync(ByVal ID_Faturamento As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal userState As Object)
            If (Me.ConsultaNFePrefeituraOperationCompleted Is Nothing) Then
                Me.ConsultaNFePrefeituraOperationCompleted = AddressOf Me.OnConsultaNFePrefeituraOperationCompleted
            End If
            Me.InvokeAsync("ConsultaNFePrefeitura", New Object() {ID_Faturamento, CodEmpresa, BancoDestino, StringConexaoDestino}, Me.ConsultaNFePrefeituraOperationCompleted, userState)
        End Sub
        
        Private Sub OnConsultaNFePrefeituraOperationCompleted(ByVal arg As Object)
            If (Not (Me.ConsultaNFePrefeituraCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ConsultaNFePrefeituraCompleted(Me, New ConsultaNFePrefeituraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CancelaNFePrefeitura", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function CancelaNFePrefeitura(ByVal Rps As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String) As String
            Dim results() As Object = Me.Invoke("CancelaNFePrefeitura", New Object() {Rps, CodEmpresa, BancoDestino, StringConexaoDestino})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub CancelaNFePrefeituraAsync(ByVal Rps As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String)
            Me.CancelaNFePrefeituraAsync(Rps, CodEmpresa, BancoDestino, StringConexaoDestino, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub CancelaNFePrefeituraAsync(ByVal Rps As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal userState As Object)
            If (Me.CancelaNFePrefeituraOperationCompleted Is Nothing) Then
                Me.CancelaNFePrefeituraOperationCompleted = AddressOf Me.OnCancelaNFePrefeituraOperationCompleted
            End If
            Me.InvokeAsync("CancelaNFePrefeitura", New Object() {Rps, CodEmpresa, BancoDestino, StringConexaoDestino}, Me.CancelaNFePrefeituraOperationCompleted, userState)
        End Sub
        
        Private Sub OnCancelaNFePrefeituraOperationCompleted(ByVal arg As Object)
            If (Not (Me.CancelaNFePrefeituraCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent CancelaNFePrefeituraCompleted(Me, New CancelaNFePrefeituraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SubstituiNFePrefeitura", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function SubstituiNFePrefeitura(ByVal RpsOld As String, ByVal RpsNew As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal id_faturamento As String) As String
            Dim results() As Object = Me.Invoke("SubstituiNFePrefeitura", New Object() {RpsOld, RpsNew, CodEmpresa, BancoDestino, StringConexaoDestino, id_faturamento})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub SubstituiNFePrefeituraAsync(ByVal RpsOld As String, ByVal RpsNew As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal id_faturamento As String)
            Me.SubstituiNFePrefeituraAsync(RpsOld, RpsNew, CodEmpresa, BancoDestino, StringConexaoDestino, id_faturamento, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub SubstituiNFePrefeituraAsync(ByVal RpsOld As String, ByVal RpsNew As String, ByVal CodEmpresa As String, ByVal BancoDestino As String, ByVal StringConexaoDestino As String, ByVal id_faturamento As String, ByVal userState As Object)
            If (Me.SubstituiNFePrefeituraOperationCompleted Is Nothing) Then
                Me.SubstituiNFePrefeituraOperationCompleted = AddressOf Me.OnSubstituiNFePrefeituraOperationCompleted
            End If
            Me.InvokeAsync("SubstituiNFePrefeitura", New Object() {RpsOld, RpsNew, CodEmpresa, BancoDestino, StringConexaoDestino, id_faturamento}, Me.SubstituiNFePrefeituraOperationCompleted, userState)
        End Sub
        
        Private Sub OnSubstituiNFePrefeituraOperationCompleted(ByVal arg As Object)
            If (Not (Me.SubstituiNFePrefeituraCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SubstituiNFePrefeituraCompleted(Me, New SubstituiNFePrefeituraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DesBloqueio", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function DesBloqueio(ByVal Bl As String, ByVal Acao As String, ByVal MotivoBloqueio As String, ByVal MotivoLiberacao As String, ByVal usuario As String) As String
            Dim results() As Object = Me.Invoke("DesBloqueio", New Object() {Bl, Acao, MotivoBloqueio, MotivoLiberacao, usuario})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub DesBloqueioAsync(ByVal Bl As String, ByVal Acao As String, ByVal MotivoBloqueio As String, ByVal MotivoLiberacao As String, ByVal usuario As String)
            Me.DesBloqueioAsync(Bl, Acao, MotivoBloqueio, MotivoLiberacao, usuario, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub DesBloqueioAsync(ByVal Bl As String, ByVal Acao As String, ByVal MotivoBloqueio As String, ByVal MotivoLiberacao As String, ByVal usuario As String, ByVal userState As Object)
            If (Me.DesBloqueioOperationCompleted Is Nothing) Then
                Me.DesBloqueioOperationCompleted = AddressOf Me.OnDesBloqueioOperationCompleted
            End If
            Me.InvokeAsync("DesBloqueio", New Object() {Bl, Acao, MotivoBloqueio, MotivoLiberacao, usuario}, Me.DesBloqueioOperationCompleted, userState)
        End Sub
        
        Private Sub OnDesBloqueioOperationCompleted(ByVal arg As Object)
            If (Not (Me.DesBloqueioCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent DesBloqueioCompleted(Me, New DesBloqueioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/StatusBloqueio", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub StatusBloqueio(ByVal consulta As String)
            Me.Invoke("StatusBloqueio", New Object() {consulta})
        End Sub
        
        '''<remarks/>
        Public Overloads Sub StatusBloqueioAsync(ByVal consulta As String)
            Me.StatusBloqueioAsync(consulta, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub StatusBloqueioAsync(ByVal consulta As String, ByVal userState As Object)
            If (Me.StatusBloqueioOperationCompleted Is Nothing) Then
                Me.StatusBloqueioOperationCompleted = AddressOf Me.OnStatusBloqueioOperationCompleted
            End If
            Me.InvokeAsync("StatusBloqueio", New Object() {consulta}, Me.StatusBloqueioOperationCompleted, userState)
        End Sub
        
        Private Sub OnStatusBloqueioOperationCompleted(ByVal arg As Object)
            If (Not (Me.StatusBloqueioCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent StatusBloqueioCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub IntegraNFePrefeituraCompletedEventHandler(ByVal sender As Object, ByVal e As IntegraNFePrefeituraCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class IntegraNFePrefeituraCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub ConsultaNFePrefeituraCompletedEventHandler(ByVal sender As Object, ByVal e As ConsultaNFePrefeituraCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ConsultaNFePrefeituraCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub CancelaNFePrefeituraCompletedEventHandler(ByVal sender As Object, ByVal e As CancelaNFePrefeituraCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class CancelaNFePrefeituraCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub SubstituiNFePrefeituraCompletedEventHandler(ByVal sender As Object, ByVal e As SubstituiNFePrefeituraCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class SubstituiNFePrefeituraCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub DesBloqueioCompletedEventHandler(ByVal sender As Object, ByVal e As DesBloqueioCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class DesBloqueioCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")>  _
    Public Delegate Sub StatusBloqueioCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
End Namespace
