Public Class Form1

    Public Shared Temp As Double

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim getcollant As Double = ProgressBar1.Value
        Dim getrod As Double = ProgressBar2.Value

        Dim tempadd As Double = controlrod(getrod)
        Dim showpsi As Double
        Dim showrpm As Double
        Dim showpower As Double

        Dim showcorestatus As String
        Dim showturbine As String

        tempadd = coolant(getcollant, tempadd)

        tempadd = tempadd - 30
        Temp = Temp + tempadd

        TextBox1.Text = Temp
        showpsi = psi(Temp)
        TextBox2.Text = showpsi
        showrpm = rpm(showpsi)
        TextBox3.Text = showrpm
        showpower = power(showrpm)
        TextBox4.Text = showpower

        showcorestatus = corestatus(Temp)
        TextBox5.Text = showcorestatus
        showturbine = turbinestatus(showrpm)
        TextBox6.Text = showturbine

        Label10.Text = getcollant
        Label11.Text = getrod
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        On Error Resume Next
        Me.ProgressBar1.Value = Me.ProgressBar1.Value - 100
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        On Error Resume Next
        Me.ProgressBar1.Value = Me.ProgressBar1.Value + 100
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        On Error Resume Next
        Me.ProgressBar2.Value = Me.ProgressBar2.Value - 10
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        On Error Resume Next
        Me.ProgressBar2.Value = Me.ProgressBar2.Value + 10
    End Sub

    Function coolant(ByVal gpm As Double, ByVal tempadd As Double) As Double

        Dim Tempaddition As Double

        If gpm < 500 Then
            Tempaddition = ((500 - gpm) / 500 + 1) * tempadd
            Return Tempaddition
        ElseIf gpm = 500 Then
            Tempaddition = tempadd
            Return Tempaddition
        ElseIf gpm > 500 Then
            Tempaddition = (1 - ((gpm - 500) / 500)) * tempadd
            Return Tempaddition
        End If
        Return tempadd
    End Function

    Function controlrod(ByVal rodValue As Double) As Double
        Dim Tempaddition As Double
        Tempaddition = (1 - (rodValue / 100)) * 200
        Return Tempaddition
    End Function

    Function psi(ByVal currentTemp As Double) As Double
        Dim currentpsi As Double
        currentpsi = currentTemp / 2
        Return currentpsi
    End Function

    Function rpm(ByVal psi As Double) As Double
        Dim currentrpm As Double
        currentrpm = psi / 10
        Return currentrpm
    End Function

    Function power(ByVal rpm As Double) As Double
        Dim output As Double
        output = rpm * 500
        Return output
    End Function

    Function corestatus(ByVal finaltemp As Double) As String

        Dim feedback As String

        If finaltemp > 5999 Then
            feedback = "Core Meltdown"
            Return feedback
        ElseIf finaltemp <= 5999 And finaltemp > 2501 Then
            feedback = "Core Unstable"
            Return feedback
        ElseIf finaltemp <= 2501 And finaltemp > 801 Then
            feedback = "Highly Reactive"
            Return feedback
        ElseIf finaltemp <= 801 And finaltemp > 500 Then
            feedback = "Core Stable"
            Return feedback
        ElseIf finaltemp <= 500 Then
            feedback = "Low Power"
            Return feedback
        End If

        Return ""
    End Function

    Function turbinestatus(ByVal speed As Double) As String

        Dim feedback As String

        If speed >= 225 Then
            feedback = "Turbine Damage"
            Return feedback
        ElseIf speed < 225 And speed >= 50 Then
            feedback = "Highspeed"
            Return feedback
        ElseIf speed < 50 Then
            feedback = "Turbine OK"
            Return feedback
        End If
        Return ""
    End Function

End Class
