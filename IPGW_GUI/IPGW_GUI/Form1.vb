Imports System.IO
Imports System.Text

Public Class Form1

    Public StdIn As StreamWriter, StdErr As New StringBuilder, ico(3) As Icon, thisBitmap As Bitmap, isWorking As Boolean, Dlg As New Dialog1, Dlg2 As New Dialog2, initClass As New initClass
    Public WithEvents coreProc, customProc As Process
    Private WithEvents ToolTip2 As ToolTip = Dlg.ToolTip1
    Private WithEvents ToolTip3 As ToolTip = Dlg2.ToolTip1
    Private Delegate Sub UpdateListviewHandler(ByVal Data As String(), ByVal usr As String)
    Private Declare Function ExtractIconA Lib "shell32" (ByVal hInst As Integer, ByVal lpszExeFileName As String, ByVal nIconIndex As Integer) As IntPtr
    Private Declare Function WritePrivateProfileStringA Lib "kernel32" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Auto Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    Private Declare Auto Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

    Protected Overrides Sub SetVisibleCore(ByVal value As Boolean)
        ' To have an invisible start up form. Ref: https://stackoverflow.com/a/12394096
        On Error Resume Next
        If Not Me.IsHandleCreated Then
            Me.CreateHandle()
            Dim readStream As New FileStream("users.profile", IO.FileMode.Open, IO.FileAccess.Read)
            Do While initClass.UsrPsw(readStream)
            Loop
            If initClass.users.Count > 0 Then
                For Each i In initClass.users
                    usrBox.Items.Add(i(0))
                Next
                usrBox.SelectedIndex = initClass.uid
            End If

            If Date.Compare(initClass.notuntil, Date.Today) > 0 Then
                DateTimePicker1.Checked = True
                DateTimePicker1.Value = initClass.notuntil
            End If
            If My.Application.CommandLineArgs.Contains("/startup") Then
                If DateTimePicker1.Checked Then End
                initProcess(True)
                If initClass.users.Count > 0 Then value = False
            End If

            CheckBox1.CheckState = -CInt(initClass.autorun)
            ComboBox1.SelectedIndex = 0
            ProxyCheckBox.Checked = initClass.useproxy
            ProxyPort.Value = initClass.proxyport
            ProxyTxt1.Text = initClass.proxyaddr
            ProxyTxt2.Text = initClass.proxyusr
            ProxyTxt3.Text = initClass.proxypsw
            TxtScript1.Text = initClass.initscript
            TxtScript2.Text = initClass.customscript

            reRegister()

            For i = 0 To 3
                ico(i) = Icon.FromHandle(ExtractIconA(0, "IPGW_Core.exe", i + 1))
            Next
            NotifyIcon1.Icon = ico(3)
            thisBitmap = New Icon(Me.Icon, 16, 16).ToBitmap
            SettingsToolStripMenuItem.Image = thisBitmap
            ToolStripMenuItem1.Image = ico(0).ToBitmap
            ToolStripMenuItem3.Image = ico(1).ToBitmap
        End If
        MyBase.SetVisibleCore(value)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Dim w As Long = m.WParam
        If m.Msg = 274 Then
            If w = &HF020 Then ' 覆写最小化
                Me.Hide()
                NotifyIcon1.ShowBalloonTip(10000, "IPGW_GUI", "窗口已最小化到托盘", ToolTipIcon.None)
                Threading.Thread.Sleep(2000)
                NotifyIcon1.Visible = False
                NotifyIcon1.Visible = True ' 撤下气泡通知
                Exit Sub
            End If
        End If
        If m.Msg = 786 Then
            Select Case w
                Case 104 : ToolStripMenuItem5_Click(Me, EventArgs.Empty)
                Case 100 : connection(0)
                Case 101 : connection(1)
                Case 102 : connection(-1)
                Case 103 : connection(-2)
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub connection(ByVal opr As Integer)
        If coreProc Is Nothing Then GoTo NInit
        If coreProc.HasExited Then GoTo NInit
        If usrBox.SelectedIndex = -1 Then MsgBox("请先点击 ""新增"" 按钮以保存新的网关账号", MsgBoxStyle.Exclamation) : Exit Sub
        If isWorking Then
            NotifyIcon1.ShowBalloonTip(10000, "IPGW Core 正忙", "核心进程正忙，无法响应本次操作，请稍后再试", ToolTipIcon.Warning)
            Threading.Thread.Sleep(3000)
            NotifyIcon1.Visible = False
            NotifyIcon1.Visible = True ' 撤下气泡通知
            Exit Sub
        End If

        StdIn.WriteLine("p_out @ipgw.connection({0},{1})", usrBox.SelectedIndex, opr)
        StdIn.WriteLine(vbNullChar) ' 写入\0表示本次脚本结束
        Exit Sub
NInit:
        MsgBox("网关连接核心程序未启动或已退出", MsgBoxStyle.Exclamation)
    End Sub

    Private Sub initProcess(ByVal isCore As Boolean)
        If initClass.users.Count = 0 Then
            MsgBox("尚未设置任何网关账号", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim prc As New Process()
        With prc.StartInfo
            .FileName = "IPGW_Core.exe"
            .Arguments = IIf(isCore, initClass.initscript, initClass.customscript)
            .CreateNoWindow = True
            .UseShellExecute = False
            .RedirectStandardOutput = True
            .RedirectStandardError = True
            .RedirectStandardInput = True
            .StandardOutputEncoding = Encoding.UTF8
            .StandardErrorEncoding = Encoding.Default
        End With
        prc.EnableRaisingEvents = True
        prc.Start()
        prc.BeginErrorReadLine()

        If isCore Then coreProc = prc : StdIn = coreProc.StandardInput Else customProc = prc
        Invoke(New MethodInvoker(AddressOf Dlg2.UpdateStatus))
    End Sub

    Private Function reRegister() As Boolean
        reRegister = True
        For i = 0 To 4
            WritePrivateProfileStringA("Hotkeys", "Mod" & i + 1, initClass.modifiers(i), ".\ipgw_gui.ini")
            WritePrivateProfileStringA("Hotkeys", "Key" & i + 1, initClass.keys(i), ".\ipgw_gui.ini")
            UnregisterHotKey(Me.Handle, 100 + i)
            If initClass.keys(i) = 0 Then Continue For
            If Not RegisterHotKey(Me.Handle, 100 + i, initClass.modifiers(i), initClass.keys(i)) Then
                MsgBox(ComboBox1.Items(i) & " 的关联热键未能成功设置，请重设", MsgBoxStyle.Critical)
                reRegister = False
            End If
        Next
    End Function

    Private Sub ErrorHandler(ByVal sender As Object, ByVal e As DataReceivedEventArgs) Handles coreProc.ErrorDataReceived, customProc.ErrorDataReceived
        If String.IsNullOrEmpty(e.Data) Then Exit Sub
        On Error Resume Next
        If sender Is coreProc Then
            StdErr.AppendLine(Encoding.UTF8.GetString(Encoding.Default.GetBytes(e.Data)))
            Invoke(New MethodInvoker(AddressOf Dlg2.UpdateStdErr))
        End If

        Dim ErrMsg() As String = e.Data.Split(New String() {": "}, 2, StringSplitOptions.RemoveEmptyEntries)
        Select Case ErrMsg(0)
            Case "鎵ц杩炴帴閫夐」" ' 执行连接选项
                Dim user As String = ErrMsg(1).Split("@")(1)
                Dim opration() As String = ErrMsg(1).Split("'")
                Dim Data As String()
                Data = sender.StandardOutput.ReadLine.Split(vbNullChar)
                If Data(0) = "false" Then Exit Sub
                Select Case opration(0) ' 返回True
                    Case "open("
                        NotifyIcon1.Icon = ico(0)
                        NotifyIcon1.ShowBalloonTip(10000, "账号 " & user & ": 连接网关", String.Format("连接数: {1}      余额: {2}{0}本机IP: {3}", vbNewLine, Data(0), Data(1), Data(2)), ToolTipIcon.None)
                    Case "close("
                        NotifyIcon1.Icon = ico(1)
                        NotifyIcon1.ShowBalloonTip(10000, "账号 " & user & ": 断开当前", "成功断开本机网关连接", ToolTipIcon.None)
                    Case "closeall("
                        NotifyIcon1.Icon = ico(1)
                        NotifyIcon1.ShowBalloonTip(10000, "账号 " & user & ": 断开全部", "成功断开全部网关连接", ToolTipIcon.None)
                    Case "disconnect("
                        NotifyIcon1.Icon = ico(1)
                        NotifyIcon1.ShowBalloonTip(10000, "账号 " & user & ": 断开指定", "成功断开指定IP的网关: " & opration(1), ToolTipIcon.None)
                    Case "getconnections("
                        If Data.Count < 3 Then
                            MsgBox("网关账号 " & user & " 当前无连接", MsgBoxStyle.Exclamation)
                        Else
                            Invoke(New UpdateListviewHandler(AddressOf UpdateListView), New Object() {Data, user}) ' 线程间委托操作，否则引发异常
                        End If
                        Exit Sub
                End Select
                Threading.Thread.Sleep(3000)
                NotifyIcon1.Visible = False
                NotifyIcon1.Visible = True ' 撤下气泡通知
                NotifyIcon1.Icon = ico(3)
            Case "缃戠粶杩炴帴澶辫触" ' 网络连接失败
                NotifyIcon1.Icon = ico(2)
                NotifyIcon1.ShowBalloonTip(10000, "网络连接失败", ErrMsg(1), ToolTipIcon.None)
                Threading.Thread.Sleep(3000)
                NotifyIcon1.Icon = ico(3)
            Case "缃戝叧杩斿洖閿欒" ' 网关返回错误
                NotifyIcon1.Icon = ico(2)
                NotifyIcon1.ShowBalloonTip(10000, "网关返回错误", Encoding.UTF8.GetString(Encoding.Default.GetBytes(ErrMsg(1))), ToolTipIcon.None)
                Threading.Thread.Sleep(3000)
                NotifyIcon1.Icon = ico(3)
            Case "鐘舵€佺爜鍝嶅簲寮傚父" ' 状态码响应异常
                NotifyIcon1.Icon = ico(2)
                NotifyIcon1.ShowBalloonTip(10000, "状态码响应异常", ErrMsg(1), ToolTipIcon.Warning)
                Threading.Thread.Sleep(3000)
                NotifyIcon1.Icon = ico(3)
            Case "璁剧疆浠ｇ悊鍦板潃" ' 设置代理地址
                If sender.StandardOutput.ReadLine = "false" Then Exit Sub
                Dim Msg As String = ErrMsg(1)
                Msg = IIf(Msg.Contains("@"), "更改为 " & Msg, "已取消使用代理连接网关")
                NotifyIcon1.ShowBalloonTip(10000, "设置网关代理", Msg, ToolTipIcon.Info)
                Threading.Thread.Sleep(3000)
                NotifyIcon1.Visible = False
                NotifyIcon1.Visible = True ' 撤下气泡通知
            Case "浠ｇ悊璁剧疆澶辫触" ' 代理设置失败
                NotifyIcon1.ShowBalloonTip(10000, "代理设置失败", ErrMsg(1), ToolTipIcon.Warning)
            Case ":IDLE"
                isWorking = False
            Case ":WORKING"
                isWorking = True
            Case Else
                If ErrMsg(0).Trim.Substring(0, 4) = "from" Then Exit Sub ' 省略Traceback
                ' 还是编码问题
                NotifyIcon1.ShowBalloonTip(10000, "发生临界错误", Encoding.UTF8.GetString(Encoding.Default.GetBytes(ErrMsg(1))), ToolTipIcon.Error)
        End Select
    End Sub

    Private Sub UnexpectedExit(ByVal sender As Object, ByVal e As System.EventArgs) Handles coreProc.Exited, customProc.Exited
        On Error Resume Next
        Invoke(New MethodInvoker(AddressOf Dlg2.UpdateStatus))
        If sender Is customProc Then Exit Sub
        If MsgBox("核心进程 IPGW_Core 意外结束，退出代码为 " & coreProc.ExitCode & vbNewLine & "是否重新启动？", MsgBoxStyle.Critical Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            If My.Application.CommandLineArgs.Contains("/startup") Then Invoke(New MethodInvoker(AddressOf Me.Close)) ' 线程间委托操作，否则引发异常
        Else
            initProcess(True)
        End If
    End Sub

    Private Sub UpdateListView(ByVal Data As String(), ByVal usr As String)
        On Error Resume Next
        Dlg.Text = usr & " 的当前连接"
        Dlg.Tag = usrBox.Items.IndexOf(usr.Split("'")(1))
        With Dlg.ListView1
            .Items.Clear()
            For i = 0 To Data.Length \ 3 - 1
                .Items.Add(New ListViewItem(New String() {Data(3 * i), Data(3 * i + 1), Data(3 * i + 2)}, -1))
            Next
        End With
        Dlg.Show()
    End Sub

    Private Sub UpdateUsers()
        Dim writeStream As IO.FileStream
        Try
            writeStream = New IO.FileStream("users.profile", IO.FileMode.Create, IO.FileAccess.Write)
        Catch ex As Exception
            MsgBox("Users.profile 文件被占用或具有写入保护，请重试", MsgBoxStyle.Critical)
            Exit Sub
        End Try
        For i = 1 To initClass.users.Count
            initClass.UsrPsw(writeStream, i)
        Next
        writeStream.Close()
        If coreProc Is Nothing Then Exit Sub
        If coreProc.HasExited Then Exit Sub
        StdIn.WriteLine("@ipgw=IPGW.new; @ipgw.setProxy(*@proxy); puts false")
        ' 不写入\0表示不立即执行，下次一起提交
    End Sub

    Private Sub usrBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles usrBox.SelectedIndexChanged
        Dim i As Integer = usrBox.SelectedIndex
        pswBox.Text = initClass.users(i)(1)
        WritePrivateProfileStringA("General", "UID", i, ".\ipgw_gui.ini")
        initClass.uid = i

        If coreProc Is Nothing Then Exit Sub
        If coreProc.HasExited Then Exit Sub
        StdIn.WriteLine("@uid=" & i)
        ' 不写入\0表示不立即执行，下次一起提交
    End Sub

    Private Sub StartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click
        If StartButton.Text = "启动(&I)" Then initProcess(True) Else coreProc.Kill()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dlg2.Show()
    End Sub

    Private Sub ButtonInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonInfo.Click
        AboutBox1.Show(Me)
    End Sub

    Private Sub TabControl1_Deselecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Deselecting
        If e.TabPageIndex <> 1 Then Exit Sub
        If Not reRegister() Then e.Cancel = True
    End Sub

    Private Sub pswBox_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles pswBox.LostFocus
        If usrBox.SelectedIndex = -1 Then Exit Sub
        initClass.users(usrBox.SelectedIndex)(1) = pswBox.Text
        UpdateUsers()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If usrBox.SelectedIndex = -1 Then Exit Sub
        MsgBox("移除了网关账户 " & usrBox.SelectedItem, MsgBoxStyle.Information)
        initClass.users.RemoveAt(usrBox.SelectedIndex)
        usrBox.Items.RemoveAt(usrBox.SelectedIndex)
        pswBox.Text = ""
        UpdateUsers()
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        If usrBox.SelectedIndex <> -1 Then Exit Sub
        MsgBox("添加了网关账户 " & usrBox.Text, MsgBoxStyle.Information)
        initClass.users.Add(New String() {usrBox.Text, pswBox.Text})
        usrBox.SelectedIndex = usrBox.Items.Add(usrBox.Text)
        UpdateUsers()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.MinDate = Date.Today
        If DateTimePicker1.Focused Then ' 如果是用户点击再设置
            Dim d As Date = IIf(DateTimePicker1.Checked, DateTimePicker1.Value, Date.Today)
            WritePrivateProfileStringA("General", "Notuntil", d.ToShortDateString, ".\ipgw_gui.ini")
            initClass.notuntil = d
        End If
    End Sub

    Private Sub CheckBox1_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckStateChanged
        If Not Me.IsHandleCreated Then Exit Sub
        On Error Resume Next
        DateTimePicker1.Enabled = CheckBox1.Checked
        Dim reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        If reg.GetValue("ipgwhotkeys") <> vbNullString Then
            If MsgBox("您之前曾设置 IPGW Hotkeys 为开机启动，可能与本程序产生冲突。" & vbNewLine & "删除该注册表键？", _
                      MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then reg.DeleteValue("ipgwhotkeys")
        End If
        If CheckBox1.CheckState = CheckState.Checked Then
            reg.SetValue("IPGWGUI", """" & Application.ExecutablePath & """ /startup")
        ElseIf CheckBox1.CheckState = CheckState.Unchecked Then
            reg.DeleteValue("IPGWGUI", False)
        End If
        If Err.Number Then
            MsgBox("设置或取消 IPGW_GUI 自动启动失败，请检查权限" & vbNewLine & "错误代码 " & Err.Number & ": " & Err.Description, MsgBoxStyle.Critical)
            CheckBox1.CheckState = CheckState.Indeterminate
        End If
        If CheckBox1.Focused Then ' 如果是用户点击再设置
            WritePrivateProfileStringA("General", "Autorun", CStr(CheckBox1.CheckState), ".\ipgw_gui.ini")
            initClass.autorun = CheckBox1.Checked
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim m As Integer = initClass.modifiers(ComboBox1.SelectedIndex)
        Dim k As Integer = initClass.keys(ComboBox1.SelectedIndex)
        If k = 0 Then TxtHotkey.Text = "(None)" : Exit Sub
        Dim T As New StringBuilder
        If ((m >> 1) And 1) Then T.Append("Ctrl+")
        If ((m >> 2) And 1) Then T.Append("Shift+")
        ' TODO: .Net 不支持Windows Modifier Key，目前只能手动设置
        If m >> 3 Then T.Append("Win+")
        If (m And 1) Then T.Append("Alt+")
        T.Append(initClass.ToKeyChar(k))
        TxtHotkey.Text = T.ToString
    End Sub

    Private Sub TxtHotkey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtHotkey.KeyDown
        Dim T As New StringBuilder
        If e.Control Then T.Append("Ctrl+")
        If e.Shift Then T.Append("Shift+")
        If e.Alt Then T.Append("Alt+")
        If e.KeyCode <> Keys.ControlKey AndAlso e.KeyCode <> Keys.ShiftKey AndAlso e.KeyCode <> Keys.Menu Then T.Append(initClass.ToKeyChar(e.KeyValue))
        TxtHotkey.Text = T.ToString
    End Sub

    Private Sub TxtHotkey_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtHotkey.KeyUp
        Dim ind As Integer = ComboBox1.SelectedIndex
        If Not TxtHotkey.Text.Contains("+") Then
            If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then ' 只按下退格/删除键
                initClass.keys(ind) = 0
                TxtHotkey.Text = "(None)"
                Exit Sub
            End If
        End If
        If e.KeyCode = Keys.ControlKey OrElse e.KeyCode = Keys.ShiftKey OrElse e.KeyCode = Keys.Menu Then GoTo Rep ' 没有主键，还原

        Dim m As Integer
        If e.Control Then m = 2
        If e.Shift Then m += 4
        If e.Alt Then m += 1

        For i = 0 To 3 ' 检查重复
            If i = ind Then Continue For
            If m = initClass.modifiers(i) AndAlso e.KeyValue = initClass.keys(i) Then
                MsgBox("新设置的热键与 """ & ComboBox1.Items(i) & """ 的热键冲突，请重设", MsgBoxStyle.Exclamation)
                GoTo Rep
            End If
        Next
        initClass.modifiers(ind) = m
        initClass.keys(ind) = e.KeyValue
        Exit Sub
Rep:
        ComboBox1_SelectedIndexChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click, ToolStripMenuItem2.Click, ToolStripMenuItem3.Click, ToolStripMenuItem4.Click
        connection(sender.Tag)
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        If customProc Is Nothing Then GoTo NInit
        If customProc.HasExited Then GoTo NInit
        If MsgBox("附加进程已在运行，是否终止？", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then customProc.Kill()
        Exit Sub
NInit:
        initProcess(False)
    End Sub

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        Me.Show()
    End Sub

    Private Sub TxtScript1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtScript1.LostFocus
        WritePrivateProfileStringA("Advanced", "InitScript", TxtScript1.Text, ".\ipgw_gui.ini")
        initClass.initscript = TxtScript1.Text
    End Sub
    Private Sub TxtScript2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtScript2.LostFocus
        WritePrivateProfileStringA("Advanced", "CustomScript", TxtScript2.Text, ".\ipgw_gui.ini")
        initClass.customscript = TxtScript2.Text
    End Sub
    Private Sub ProxyTxt1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProxyTxt1.LostFocus
        WritePrivateProfileStringA("Advanced", "ProxyAddr", ProxyTxt1.Text, ".\ipgw_gui.ini")
        initClass.proxyaddr = ProxyTxt1.Text
    End Sub
    Private Sub ProxyTxt2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProxyTxt2.LostFocus
        WritePrivateProfileStringA("Advanced", "ProxyUsr", ProxyTxt2.Text, ".\ipgw_gui.ini")
        initClass.proxyusr = ProxyTxt2.Text
    End Sub
    Private Sub ProxyTxt3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProxyTxt3.LostFocus
        WritePrivateProfileStringA("Advanced", "ProxyPsw", ProxyTxt3.Text, ".\ipgw_gui.ini")
        initClass.proxypsw = ProxyTxt3.Text
    End Sub
    Private Sub ProxyPort_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProxyPort.LostFocus
        WritePrivateProfileStringA("Advanced", "ProxyPort", ProxyPort.Value, ".\ipgw_gui.ini")
        initClass.proxyport = ProxyPort.Value
    End Sub

    Private Sub Proxy_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProxyTxt1.TextChanged, ProxyTxt2.TextChanged, ProxyTxt3.TextChanged, ProxyPort.ValueChanged
        If coreProc Is Nothing Then Exit Sub
        If coreProc.HasExited Then Exit Sub

        If sender.Focused Then ProxyCheckBox.CheckState = CheckState.Indeterminate ' 有改动则使复选框变为中间态
    End Sub

    Private Sub ProxyCheckBox_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProxyCheckBox.CheckStateChanged
        ProxyPort.Enabled = ProxyCheckBox.Checked
        ProxyTxt1.Enabled = ProxyCheckBox.Checked
        ProxyTxt2.Enabled = ProxyCheckBox.Checked
        ProxyTxt3.Enabled = ProxyCheckBox.Checked
    End Sub

    Private Sub ProxyCheckBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProxyCheckBox.Click
        ProxyCheckBox.CheckState = 1 - ProxyCheckBox.CheckState Mod 2 ' 0, 2 => 1; 1 => 0
        WritePrivateProfileStringA("Advanced", "UseProxy", CStr(ProxyCheckBox.CheckState), ".\ipgw_gui.ini")
        initClass.useproxy = ProxyCheckBox.Checked
        If coreProc Is Nothing Then Exit Sub
        If coreProc.HasExited Then Exit Sub
        If isWorking Then
            ProxyCheckBox.CheckState = CheckState.Indeterminate
            NotifyIcon1.ShowBalloonTip(10000, "IPGW Core 正忙", "核心进程正忙，无法响应本次操作，请稍后再试", ToolTipIcon.Warning)
            Threading.Thread.Sleep(3000)
            NotifyIcon1.Visible = False
            NotifyIcon1.Visible = True ' 撤下气泡通知
            Exit Sub
        End If

        If ProxyCheckBox.Checked Then
            StdIn.WriteLine("@proxy=['{0}',{1},'{2}','{3}']", ProxyTxt1.Text, ProxyPort.Value, ProxyTxt2.Text, ProxyTxt3.Text)
            StdIn.WriteLine("p_out @ipgw.setProxy(*@proxy)")
            StdIn.WriteLine(vbNullChar)
        Else
            StdIn.WriteLine("@proxy=[nil]; p_out @ipgw.setProxy(nil)")
            StdIn.WriteLine(vbNullChar)
        End If
    End Sub

    Private Sub ScriptButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptButton1.Click, ScriptButton2.Click
        If OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If sender Is ScriptButton1 Then
            TxtScript1.Text = OpenFileDialog1.FileName
            TxtScript1_LostFocus(Me, EventArgs.Empty)
        Else
            TxtScript2.Text = OpenFileDialog1.FileName
            TxtScript2_LostFocus(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        On Error Resume Next
        NotifyIcon1.ShowBalloonTip(10000, "IPGW GUI", "程序正在退出", ToolTipIcon.None)
        Me.Hide()
        Dlg.Close()
        Dlg2.Close()
        AboutBox1.Close()
        Threading.Thread.Sleep(3000)
        RemoveHandler coreProc.Exited, AddressOf UnexpectedExit
        RemoveHandler customProc.Exited, AddressOf UnexpectedExit
        coreProc.Kill()
        customProc.Kill()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
    End Sub

    Private Sub ToolTip1_Draw(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawToolTipEventArgs) Handles ToolTip1.Draw, ToolTip2.Draw, ToolTip3.Draw
        ' 重绘 ToolTip
        Dim rc = e.Bounds.Location
        rc.X += 10 : rc.Y += 10

        e.DrawBackground()
        e.DrawBorder()
        e.Graphics.DrawImage(thisBitmap, rc.X + 2, rc.Y + 2)
        e.Graphics.DrawString(sender.ToolTipTitle, Dlg2.Font, Brushes.Black, rc.X + 24, rc.Y)
        e.Graphics.DrawString(e.ToolTipText, Me.Font, Brushes.Black, rc.X, rc.Y + 24)
    End Sub

    Private Sub ToolTip1_Popup(ByVal sender As Object, ByVal e As System.Windows.Forms.PopupEventArgs) Handles ToolTip1.Popup, ToolTip2.Popup, ToolTip3.Popup
        Dim rc = TextRenderer.MeasureText(sender.GetToolTip(e.AssociatedControl), Me.Font)
        rc.Width += 20 : rc.Height += 44
        e.ToolTipSize = rc
    End Sub
End Class
