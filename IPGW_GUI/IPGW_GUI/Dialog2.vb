Imports System.Windows.Forms

Public Class Dialog2

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Form1.StdIn.Write(TextBox1.Text)
        Form1.StdIn.WriteLine(vbNullChar)
    End Sub

    Public Sub UpdateStatus()
        If Form1.coreProc Is Nothing Then
            OK_Button.Visible = False
            Label1.Text = "核心进程未启动"
            Form1.StartButton.Text = "启动(&I)"
        ElseIf Form1.coreProc.HasExited Then
            OK_Button.Visible = False
            Label1.Text = "核心进程已意外退出，ExitCode = " & Form1.coreProc.ExitCode
            Form1.StartButton.Text = "启动(&I)"
        Else
            OK_Button.Visible = Not CheckBox1.Checked
            Label1.Text = "核心进程就绪，PID = " & Form1.coreProc.Id
            Form1.StartButton.Text = "终止(&D)"
        End If
        If Form1.customProc Is Nothing Then
            Label2.Text = "附加进程未启动"
        ElseIf Form1.customProc.HasExited Then
            Label2.Text = "附加进程已退出，ExitCode = " & Form1.customProc.ExitCode
        Else
            Label2.Text = "附加进程已启动，PID = " & Form1.customProc.Id
        End If
    End Sub

    Public Sub UpdateStdErr()
        On Error Resume Next
        Dim stderr As String = Form1.StdErr.ToString
        If CheckBox1.Checked Then TextBox1.Text = stderr
        ' 若工作中禁止执行脚本
        OK_Button.Enabled = (stderr.Split(vbNewLine.ToCharArray, StringSplitOptions.RemoveEmptyEntries).Last = ":IDLE")
        If OK_Button.Enabled Then
            Label1.Text = Label1.Text.Replace("工作中", "就绪")
        Else
            Label1.Text = Label1.Text.Replace("就绪", "工作中")
        End If
    End Sub

    Private Sub Dialog2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Dialog2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdateStatus()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox1.Tag = TextBox1.Text
            TextBox1.ReadOnly = True
        Else
            TextBox1.ReadOnly = False
            TextBox1.Text = TextBox1.Tag
        End If
        UpdateStatus()
        UpdateStdErr()
    End Sub
End Class
