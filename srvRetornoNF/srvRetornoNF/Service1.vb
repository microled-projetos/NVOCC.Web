Imports System.Configuration

Public Class Service1
    Dim tmr As Timers.Timer

    Protected Overrides Sub OnStart(ByVal args() As String)
        Try
            ' Adicione código aqui para iniciar seu serviço. Este método deve ajustar
            ' o que é necessário para que seu serviço possa executar seu trabalho.
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - Inicio")
            FlagExecutando = False
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - OnStart: linha 12")
            tmr = New Timers.Timer()
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - OnStart: linha 14")
            Inicio.Principal()
            tmr.Interval = 1000 * 60 * Val(ConfigurationManager.AppSettings("Minutos").ToString())
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - OnStart: linha 16")
            AddHandler tmr.Elapsed, AddressOf TimerTick
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - OnStart: linha 18")
            tmr.Enabled = True
            Inicio.WriteToFile($"{DateTime.Now.ToString()} - OnStart: linha 20")

        Catch ex As Exception
            WriteToFile($"{DateTime.Now.ToString()} - Erro: " & ex.ToString)
            FlagExecutando = False
        End Try
    End Sub

    Private Sub TimerTick(obj As Object, e As EventArgs)
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - TimerTick: Antes do IF")
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - TimerTick: Chama Rotina Retorno NF")
        Inicio.Principal()
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - TimerTick: Após o IF")
    End Sub
    Protected Overrides Sub OnStop()
        Inicio.WriteToFile($"{DateTime.Now.ToString()} - Fim")
        ' Adicione código aqui para realizar qualquer limpeza necessária para parar seu serviço.
    End Sub

End Class
