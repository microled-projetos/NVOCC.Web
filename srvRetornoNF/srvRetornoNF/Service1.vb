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
        If Not FlagExecutando Then
            Inicio.RetornoNF()
        End If
    End Sub
    Protected Overrides Sub OnStop()
        ' Adicione código aqui para realizar qualquer limpeza necessária para parar seu serviço.
    End Sub

End Class
