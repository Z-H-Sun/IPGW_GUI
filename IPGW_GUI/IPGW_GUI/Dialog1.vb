Imports System.Windows.Forms

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If ListView1.SelectedIndices.Count = 0 Then MsgBox("请选中需要断开的网关连接", MsgBoxStyle.Information) : Exit Sub
        If Me.Tag = -1 Then MsgBox("找不到指定账号", MsgBoxStyle.Critical) : Exit Sub
        If Form1.isWorking Then
            Form1.NotifyIcon1.ShowBalloonTip(10000, "IPGW Core 正忙", "核心进程正忙，无法响应本次操作，请稍后再试", ToolTipIcon.Warning)
            Threading.Thread.Sleep(3000)
            Form1.NotifyIcon1.Visible = False
            Form1.NotifyIcon1.Visible = True ' 撤下气泡通知
            Exit Sub
        End If
        Form1.StdIn.WriteLine("p_out @ipgw.connection({0},2,'{1}')", Me.Tag, ListView1.SelectedItems(0).Text)
        Form1.StdIn.WriteLine(vbNullChar)
        Me.Hide()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Hide()
    End Sub

    Private Sub Dialog1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Icon = Form1.ico(1)

    End Sub

    Private Sub Dialog1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        OK_Button_Click(Me, EventArgs.Empty)
    End Sub
End Class
