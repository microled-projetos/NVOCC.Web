Imports System.Runtime.InteropServices
Module Timeout

    <StructLayout(LayoutKind.Sequential)>
    Structure LASTINPUTINFO
        <MarshalAs(UnmanagedType.U4)>
        Public cbSize As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dwTime As Integer
    End Structure

    <DllImport("user32.dll")>
    Function GetLastInputInfo(ByRef plii As LASTINPUTINFO) As Boolean
    End Function

    Dim Tempo As Integer = 0
    Dim Atividade As New LASTINPUTINFO()

    Public Function ObterUltimaAtividade() As Integer

        Tempo = 0
        Atividade.cbSize = Marshal.SizeOf(Atividade)
        Atividade.dwTime = 0

        If GetLastInputInfo(Atividade) Then
            Tempo = Environment.TickCount - Atividade.dwTime
        End If

        If Tempo > 0 Then
            Return Tempo / 1000
        Else
            Return 0
        End If

    End Function

End Module