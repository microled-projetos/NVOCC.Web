﻿'------------------------------------------------------------------------------
' <auto-generated>
'     O código foi gerado por uma ferramenta.
'     Versão de Tempo de Execução:4.0.30319.42000
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for gerado novamente.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace GinfesTeste
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://homologacao.ginfes.com.br", ConfigurationName:="GinfesTeste.ServiceGinfesImpl")>  _
    Public Interface ServiceGinfesImpl
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function CancelarNfse(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function CancelarNfseAsync(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function CancelarNfseV3(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function CancelarNfseV3Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function CancelarNfseV4(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function CancelarNfseV4Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarLoteRps(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarLoteRpsAsync(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarLoteRpsV3(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarLoteRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarLoteRpsV4(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarLoteRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarNfse(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarNfseAsync(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarNfsePorRps(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarNfsePorRpsAsync(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarNfsePorRpsV3(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarNfsePorRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarNfsePorRpsV4(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarNfsePorRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarNfseV3(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarNfseV3Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarNfseV4(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarNfseV4Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarSituacaoLoteRps(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarSituacaoLoteRpsAsync(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarSituacaoLoteRpsV3(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarSituacaoLoteRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function ConsultarSituacaoLoteRpsV4(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function ConsultarSituacaoLoteRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function RecepcionarLoteRps(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function RecepcionarLoteRpsAsync(ByVal arg0 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function RecepcionarLoteRpsV3(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function RecepcionarLoteRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*"),  _
         System.ServiceModel.DataContractFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc)>  _
        Function RecepcionarLoteRpsV4(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="", ReplyAction:="*")>  _
        Function RecepcionarLoteRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> System.Threading.Tasks.Task(Of String)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ServiceGinfesImplChannel
        Inherits GinfesTeste.ServiceGinfesImpl, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class ServiceGinfesImplClient
        Inherits System.ServiceModel.ClientBase(Of GinfesTeste.ServiceGinfesImpl)
        Implements GinfesTeste.ServiceGinfesImpl
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function CancelarNfse(ByVal arg0 As String) As String Implements GinfesTeste.ServiceGinfesImpl.CancelarNfse
            Return MyBase.Channel.CancelarNfse(arg0)
        End Function
        
        Public Function CancelarNfseAsync(ByVal arg0 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.CancelarNfseAsync
            Return MyBase.Channel.CancelarNfseAsync(arg0)
        End Function
        
        Public Function CancelarNfseV3(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.CancelarNfseV3
            Return MyBase.Channel.CancelarNfseV3(arg0, arg1)
        End Function
        
        Public Function CancelarNfseV3Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.CancelarNfseV3Async
            Return MyBase.Channel.CancelarNfseV3Async(arg0, arg1)
        End Function
        
        Public Function CancelarNfseV4(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.CancelarNfseV4
            Return MyBase.Channel.CancelarNfseV4(arg0, arg1)
        End Function
        
        Public Function CancelarNfseV4Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.CancelarNfseV4Async
            Return MyBase.Channel.CancelarNfseV4Async(arg0, arg1)
        End Function
        
        Public Function ConsultarLoteRps(ByVal arg0 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarLoteRps
            Return MyBase.Channel.ConsultarLoteRps(arg0)
        End Function
        
        Public Function ConsultarLoteRpsAsync(ByVal arg0 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarLoteRpsAsync
            Return MyBase.Channel.ConsultarLoteRpsAsync(arg0)
        End Function
        
        Public Function ConsultarLoteRpsV3(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarLoteRpsV3
            Return MyBase.Channel.ConsultarLoteRpsV3(arg0, arg1)
        End Function
        
        Public Function ConsultarLoteRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarLoteRpsV3Async
            Return MyBase.Channel.ConsultarLoteRpsV3Async(arg0, arg1)
        End Function
        
        Public Function ConsultarLoteRpsV4(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarLoteRpsV4
            Return MyBase.Channel.ConsultarLoteRpsV4(arg0, arg1)
        End Function
        
        Public Function ConsultarLoteRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarLoteRpsV4Async
            Return MyBase.Channel.ConsultarLoteRpsV4Async(arg0, arg1)
        End Function
        
        Public Function ConsultarNfse(ByVal arg0 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfse
            Return MyBase.Channel.ConsultarNfse(arg0)
        End Function
        
        Public Function ConsultarNfseAsync(ByVal arg0 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfseAsync
            Return MyBase.Channel.ConsultarNfseAsync(arg0)
        End Function
        
        Public Function ConsultarNfsePorRps(ByVal arg0 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfsePorRps
            Return MyBase.Channel.ConsultarNfsePorRps(arg0)
        End Function
        
        Public Function ConsultarNfsePorRpsAsync(ByVal arg0 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfsePorRpsAsync
            Return MyBase.Channel.ConsultarNfsePorRpsAsync(arg0)
        End Function
        
        Public Function ConsultarNfsePorRpsV3(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfsePorRpsV3
            Return MyBase.Channel.ConsultarNfsePorRpsV3(arg0, arg1)
        End Function
        
        Public Function ConsultarNfsePorRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfsePorRpsV3Async
            Return MyBase.Channel.ConsultarNfsePorRpsV3Async(arg0, arg1)
        End Function
        
        Public Function ConsultarNfsePorRpsV4(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfsePorRpsV4
            Return MyBase.Channel.ConsultarNfsePorRpsV4(arg0, arg1)
        End Function
        
        Public Function ConsultarNfsePorRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfsePorRpsV4Async
            Return MyBase.Channel.ConsultarNfsePorRpsV4Async(arg0, arg1)
        End Function
        
        Public Function ConsultarNfseV3(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfseV3
            Return MyBase.Channel.ConsultarNfseV3(arg0, arg1)
        End Function
        
        Public Function ConsultarNfseV3Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfseV3Async
            Return MyBase.Channel.ConsultarNfseV3Async(arg0, arg1)
        End Function
        
        Public Function ConsultarNfseV4(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfseV4
            Return MyBase.Channel.ConsultarNfseV4(arg0, arg1)
        End Function
        
        Public Function ConsultarNfseV4Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarNfseV4Async
            Return MyBase.Channel.ConsultarNfseV4Async(arg0, arg1)
        End Function
        
        Public Function ConsultarSituacaoLoteRps(ByVal arg0 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarSituacaoLoteRps
            Return MyBase.Channel.ConsultarSituacaoLoteRps(arg0)
        End Function
        
        Public Function ConsultarSituacaoLoteRpsAsync(ByVal arg0 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarSituacaoLoteRpsAsync
            Return MyBase.Channel.ConsultarSituacaoLoteRpsAsync(arg0)
        End Function
        
        Public Function ConsultarSituacaoLoteRpsV3(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarSituacaoLoteRpsV3
            Return MyBase.Channel.ConsultarSituacaoLoteRpsV3(arg0, arg1)
        End Function
        
        Public Function ConsultarSituacaoLoteRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarSituacaoLoteRpsV3Async
            Return MyBase.Channel.ConsultarSituacaoLoteRpsV3Async(arg0, arg1)
        End Function
        
        Public Function ConsultarSituacaoLoteRpsV4(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.ConsultarSituacaoLoteRpsV4
            Return MyBase.Channel.ConsultarSituacaoLoteRpsV4(arg0, arg1)
        End Function
        
        Public Function ConsultarSituacaoLoteRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.ConsultarSituacaoLoteRpsV4Async
            Return MyBase.Channel.ConsultarSituacaoLoteRpsV4Async(arg0, arg1)
        End Function
        
        Public Function RecepcionarLoteRps(ByVal arg0 As String) As String Implements GinfesTeste.ServiceGinfesImpl.RecepcionarLoteRps
            Return MyBase.Channel.RecepcionarLoteRps(arg0)
        End Function
        
        Public Function RecepcionarLoteRpsAsync(ByVal arg0 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.RecepcionarLoteRpsAsync
            Return MyBase.Channel.RecepcionarLoteRpsAsync(arg0)
        End Function
        
        Public Function RecepcionarLoteRpsV3(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.RecepcionarLoteRpsV3
            Return MyBase.Channel.RecepcionarLoteRpsV3(arg0, arg1)
        End Function
        
        Public Function RecepcionarLoteRpsV3Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.RecepcionarLoteRpsV3Async
            Return MyBase.Channel.RecepcionarLoteRpsV3Async(arg0, arg1)
        End Function
        
        Public Function RecepcionarLoteRpsV4(ByVal arg0 As String, ByVal arg1 As String) As String Implements GinfesTeste.ServiceGinfesImpl.RecepcionarLoteRpsV4
            Return MyBase.Channel.RecepcionarLoteRpsV4(arg0, arg1)
        End Function
        
        Public Function RecepcionarLoteRpsV4Async(ByVal arg0 As String, ByVal arg1 As String) As System.Threading.Tasks.Task(Of String) Implements GinfesTeste.ServiceGinfesImpl.RecepcionarLoteRpsV4Async
            Return MyBase.Channel.RecepcionarLoteRpsV4Async(arg0, arg1)
        End Function
    End Class
End Namespace
