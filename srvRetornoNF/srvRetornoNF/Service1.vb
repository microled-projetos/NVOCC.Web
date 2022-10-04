Imports System.Configuration

Public Class Service1
    Dim tmr As Timers.Timer

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Adicione código aqui para iniciar seu serviço. Este método deve ajustar
        ' o que é necessário para que seu serviço possa executar seu trabalho.
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - Inicio")
        FlagExecutando = False
        tmr = New Timers.Timer()
        tmr.Interval = 1000 * 60 * Val(ConfigurationManager.AppSettings("Minutos").ToString())
        AddHandler tmr.Elapsed, AddressOf TimerTick
        tmr.Enabled = True

    End Sub

    Private Sub TimerTick(obj As Object, e As EventArgs)
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - Antes do IF")
        If Not FlagExecutando Then
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - Chama Rotina Retorno NF")
            Inicio.RetornoNF()
        End If
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - Após o IF")
    End Sub
    Protected Overrides Sub OnStop()
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - Fim")
        ' Adicione código aqui para realizar qualquer limpeza necessária para parar seu serviço.
    End Sub

End Class
