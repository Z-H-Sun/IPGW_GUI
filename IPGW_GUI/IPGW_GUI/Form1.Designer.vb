<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.FToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.ButtonAdd = New System.Windows.Forms.Button
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.usrBox = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pswBox = New IPGW_GUI.PassWordBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.TxtHotkey = New System.Windows.Forms.TextBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.ScriptButton2 = New System.Windows.Forms.Button
        Me.ScriptButton1 = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtScript2 = New System.Windows.Forms.TextBox
        Me.TxtScript1 = New System.Windows.Forms.TextBox
        Me.ProxyTxt2 = New System.Windows.Forms.TextBox
        Me.ProxyTxt1 = New System.Windows.Forms.TextBox
        Me.ProxyPort = New System.Windows.Forms.NumericUpDown
        Me.ProxyCheckBox = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.ProxyTxt3 = New IPGW_GUI.PassWordBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Button1 = New System.Windows.Forms.Button
        Me.StartButton = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ButtonInfo = New System.Windows.Forms.Button
        Me.ContextMenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.ProxyPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Text = "IPGW_GUI"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.MintCream
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5, Me.FToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(171, 164)
        Me.ContextMenuStrip1.Text = "Tray"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(170, 22)
        Me.ToolStripMenuItem1.Tag = "0"
        Me.ToolStripMenuItem1.Text = "连接网关(&C)"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(170, 22)
        Me.ToolStripMenuItem2.Tag = "1"
        Me.ToolStripMenuItem2.Text = "当前连接(&Q)"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(170, 22)
        Me.ToolStripMenuItem3.Tag = "-1"
        Me.ToolStripMenuItem3.Text = "断开当前(&D)"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(170, 22)
        Me.ToolStripMenuItem4.Tag = "-2"
        Me.ToolStripMenuItem4.Text = "断开全部(&A)"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(170, 22)
        Me.ToolStripMenuItem5.Text = "自定义脚本(&Z)"
        '
        'FToolStripMenuItem
        '
        Me.FToolStripMenuItem.Name = "FToolStripMenuItem"
        Me.FToolStripMenuItem.Size = New System.Drawing.Size(167, 6)
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.SettingsToolStripMenuItem.Text = "设置(&S)"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.QuitToolStripMenuItem.Text = "退出(&Q)"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(254, 164)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ButtonDel)
        Me.TabPage1.Controls.Add(Me.ButtonAdd)
        Me.TabPage1.Controls.Add(Me.DateTimePicker1)
        Me.TabPage1.Controls.Add(Me.CheckBox1)
        Me.TabPage1.Controls.Add(Me.usrBox)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.pswBox)
        Me.TabPage1.Location = New System.Drawing.Point(4, 28)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(246, 132)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "常规"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ButtonDel
        '
        Me.ButtonDel.BackColor = System.Drawing.Color.White
        Me.ButtonDel.Location = New System.Drawing.Point(186, 49)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(49, 25)
        Me.ButtonDel.TabIndex = 5
        Me.ButtonDel.Text = "删除"
        Me.ToolTip1.SetToolTip(Me.ButtonDel, "删除当前网关账户")
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonAdd
        '
        Me.ButtonAdd.BackColor = System.Drawing.Color.White
        Me.ButtonAdd.Location = New System.Drawing.Point(186, 16)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(49, 25)
        Me.ButtonAdd.TabIndex = 2
        Me.ButtonAdd.Text = "新增"
        Me.ToolTip1.SetToolTip(Me.ButtonAdd, "保存当前更改为新的网关账户")
        Me.ButtonAdd.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Checked = False
        Me.DateTimePicker1.CustomFormat = "自 yy/MM/dd"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(14, 89)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.ShowCheckBox = True
        Me.DateTimePicker1.ShowUpDown = True
        Me.DateTimePicker1.Size = New System.Drawing.Size(133, 24)
        Me.DateTimePicker1.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.DateTimePicker1, "设置本程序计划自启任务的起始日期" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "可实现假期结束前不自动启动的功能")
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.CheckBox1.Location = New System.Drawing.Point(157, 90)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(84, 23)
        Me.CheckBox1.TabIndex = 7
        Me.CheckBox1.Text = "计划自启"
        Me.ToolTip1.SetToolTip(Me.CheckBox1, "设置本程序是否随开机启动")
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'usrBox
        '
        Me.usrBox.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usrBox.FormattingEnabled = True
        Me.usrBox.Location = New System.Drawing.Point(71, 16)
        Me.usrBox.Name = "usrBox"
        Me.usrBox.Size = New System.Drawing.Size(109, 25)
        Me.usrBox.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.usrBox, "选中欲操作网关的账户" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "或输入新的网关账户")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "密码(&P)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "账号(&U)"
        '
        'pswBox
        '
        Me.pswBox.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pswBox.Location = New System.Drawing.Point(71, 50)
        Me.pswBox.Name = "pswBox"
        Me.pswBox.Size = New System.Drawing.Size(109, 24)
        Me.pswBox.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.pswBox, "设置当前网关账户的密码" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "对现有账号的密码所作更改将自动保存" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "新账号密码设置结束后请单击 ""新增""")
        Me.pswBox.UseSystemPasswordChar = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.ComboBox1)
        Me.TabPage2.Controls.Add(Me.TxtHotkey)
        Me.TabPage2.Location = New System.Drawing.Point(4, 28)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(246, 132)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "热键"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label8.Location = New System.Drawing.Point(17, 86)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(211, 30)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "选择对应项目后，在文本框内按下欲设" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "的键，按删除键取消。离开选项卡更新"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "键值(&K)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 19)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "项目(&I)"
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.SystemColors.Control
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBox1.Items.AddRange(New Object() {"连接网关", "当前连接", "断开当前", "断开全部", "自定义脚本"})
        Me.ComboBox1.Location = New System.Drawing.Point(72, 16)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(148, 27)
        Me.ComboBox1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ComboBox1, "选择要设置热键的项目")
        '
        'TxtHotkey
        '
        Me.TxtHotkey.BackColor = System.Drawing.SystemColors.Control
        Me.TxtHotkey.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHotkey.Location = New System.Drawing.Point(72, 51)
        Me.TxtHotkey.Name = "TxtHotkey"
        Me.TxtHotkey.ReadOnly = True
        Me.TxtHotkey.Size = New System.Drawing.Size(148, 24)
        Me.TxtHotkey.TabIndex = 3
        Me.TxtHotkey.Text = "(None)"
        Me.ToolTip1.SetToolTip(Me.TxtHotkey, "参照下方说明设置系统全局热键")
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TxtScript2)
        Me.TabPage3.Controls.Add(Me.TxtScript1)
        Me.TabPage3.Controls.Add(Me.ScriptButton2)
        Me.TabPage3.Controls.Add(Me.ScriptButton1)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.ProxyTxt2)
        Me.TabPage3.Controls.Add(Me.ProxyTxt1)
        Me.TabPage3.Controls.Add(Me.ProxyPort)
        Me.TabPage3.Controls.Add(Me.ProxyCheckBox)
        Me.TabPage3.Controls.Add(Me.Label5)
        Me.TabPage3.Controls.Add(Me.ProxyTxt3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 28)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(246, 132)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "高级"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ScriptButton2
        '
        Me.ScriptButton2.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue
        Me.ScriptButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ScriptButton2.Location = New System.Drawing.Point(204, 99)
        Me.ScriptButton2.Name = "ScriptButton2"
        Me.ScriptButton2.Size = New System.Drawing.Size(28, 24)
        Me.ScriptButton2.TabIndex = 11
        Me.ScriptButton2.Text = "···"
        Me.ToolTip1.SetToolTip(Me.ScriptButton2, "选择自定义网关操作的脚本文件位置")
        Me.ScriptButton2.UseVisualStyleBackColor = True
        '
        'ScriptButton1
        '
        Me.ScriptButton1.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue
        Me.ScriptButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ScriptButton1.Location = New System.Drawing.Point(204, 69)
        Me.ScriptButton1.Name = "ScriptButton1"
        Me.ScriptButton1.Size = New System.Drawing.Size(28, 24)
        Me.ScriptButton1.TabIndex = 8
        Me.ScriptButton1.Text = "···"
        Me.ToolTip1.SetToolTip(Me.ScriptButton1, "选择本程序的初始化脚本文件位置")
        Me.ScriptButton1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 19)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "自定脚本(&C)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 19)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "启动脚本(&S)"
        '
        'TxtScript2
        '
        Me.TxtScript2.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtScript2.Location = New System.Drawing.Point(93, 99)
        Me.TxtScript2.Name = "TxtScript2"
        Me.TxtScript2.Size = New System.Drawing.Size(108, 24)
        Me.TxtScript2.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.TxtScript2, "更改自定义网关操作脚本的" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "命令行（重启附加进程生效）")
        '
        'TxtScript1
        '
        Me.TxtScript1.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtScript1.Location = New System.Drawing.Point(93, 69)
        Me.TxtScript1.Name = "TxtScript1"
        Me.TxtScript1.Size = New System.Drawing.Size(108, 24)
        Me.TxtScript1.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.TxtScript1, "更改本程序的初始化脚本的" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "命令行（重启核心进程生效）")
        '
        'ProxyTxt2
        '
        Me.ProxyTxt2.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProxyTxt2.Location = New System.Drawing.Point(93, 36)
        Me.ProxyTxt2.Name = "ProxyTxt2"
        Me.ProxyTxt2.Size = New System.Drawing.Size(78, 24)
        Me.ProxyTxt2.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.ProxyTxt2, "设置代理用户名")
        '
        'ProxyTxt1
        '
        Me.ProxyTxt1.AutoCompleteCustomSource.AddRange(New String() {"127.0.0.1"})
        Me.ProxyTxt1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ProxyTxt1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.ProxyTxt1.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProxyTxt1.Location = New System.Drawing.Point(93, 10)
        Me.ProxyTxt1.Name = "ProxyTxt1"
        Me.ProxyTxt1.Size = New System.Drawing.Size(78, 24)
        Me.ProxyTxt1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ProxyTxt1, "设置代理地址")
        '
        'ProxyPort
        '
        Me.ProxyPort.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProxyPort.Location = New System.Drawing.Point(172, 10)
        Me.ProxyPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ProxyPort.Name = "ProxyPort"
        Me.ProxyPort.Size = New System.Drawing.Size(60, 24)
        Me.ProxyPort.TabIndex = 2
        Me.ProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.ProxyPort, "设置代理端口")
        '
        'ProxyCheckBox
        '
        Me.ProxyCheckBox.AutoCheck = False
        Me.ProxyCheckBox.AutoSize = True
        Me.ProxyCheckBox.Location = New System.Drawing.Point(10, 11)
        Me.ProxyCheckBox.Name = "ProxyCheckBox"
        Me.ProxyCheckBox.Size = New System.Drawing.Size(84, 23)
        Me.ProxyCheckBox.TabIndex = 0
        Me.ProxyCheckBox.Text = "启用代理"
        Me.ToolTip1.SetToolTip(Me.ProxyCheckBox, "为网关操作设置代理（不支持SOCKS）" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "变更代理设置后，复选框变为中间态，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "此时再次点击复选框以使设置立即生效")
        Me.ProxyCheckBox.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 19)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "用户密码"
        '
        'ProxyTxt3
        '
        Me.ProxyTxt3.Font = New System.Drawing.Font("Consolas", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProxyTxt3.Location = New System.Drawing.Point(172, 36)
        Me.ProxyTxt3.Name = "ProxyTxt3"
        Me.ProxyTxt3.Size = New System.Drawing.Size(60, 24)
        Me.ProxyTxt3.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ProxyTxt3, "设置代理密码")
        Me.ProxyTxt3.UseSystemPasswordChar = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "rb"
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "Ruby 文件|*.rb|所有文件|*.*"
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(172, 182)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 36)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "开发(&D)"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.Button1, "开发者工具：监控" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "及执行 Ruby 脚本")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'StartButton
        '
        Me.StartButton.Image = CType(resources.GetObject("StartButton.Image"), System.Drawing.Image)
        Me.StartButton.Location = New System.Drawing.Point(76, 182)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(90, 36)
        Me.StartButton.TabIndex = 0
        Me.StartButton.Text = "启动(&I)"
        Me.StartButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.StartButton, "启动或终止网关操作核心进程")
        Me.StartButton.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.BackColor = System.Drawing.Color.MintCream
        Me.ToolTip1.OwnerDraw = True
        Me.ToolTip1.ToolTipTitle = "IPGW GUI"
        '
        'ButtonInfo
        '
        Me.ButtonInfo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonInfo.FlatAppearance.BorderSize = 0
        Me.ButtonInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue
        Me.ButtonInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonInfo.Image = CType(resources.GetObject("ButtonInfo.Image"), System.Drawing.Image)
        Me.ButtonInfo.Location = New System.Drawing.Point(240, 10)
        Me.ButtonInfo.Name = "ButtonInfo"
        Me.ButtonInfo.Size = New System.Drawing.Size(24, 24)
        Me.ButtonInfo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.ButtonInfo, "显示""关于""对话框")
        Me.ButtonInfo.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.MintCream
        Me.ClientSize = New System.Drawing.Size(279, 226)
        Me.Controls.Add(Me.ButtonInfo)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.StartButton)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IPGW_GUI 设置"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.ProxyPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QuitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents usrBox As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents pswBox As IPGW_GUI.PassWordBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents TxtHotkey As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProxyCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ProxyTxt1 As System.Windows.Forms.TextBox
    Friend WithEvents ProxyPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents ProxyTxt3 As IPGW_GUI.PassWordBox
    Friend WithEvents ProxyTxt2 As System.Windows.Forms.TextBox
    Friend WithEvents TxtScript2 As System.Windows.Forms.TextBox
    Friend WithEvents TxtScript1 As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ScriptButton1 As System.Windows.Forms.Button
    Friend WithEvents ScriptButton2 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ButtonInfo As System.Windows.Forms.Button

End Class
