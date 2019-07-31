# IPGW_GUI
Windows 平台下北京大学网关操作图形用户界面工具，基于.NET 3.5和[IPGW_Core](https://github.com/Z-H-Sun/ipgw)项目。

## 运行环境
* Windows 7 及以上
* 或更低版本的已配置.NET 3.5的Windos x86/x64环境

## 特性
* 以可视化的方式进行连接网关、查询连接、断开指定、断开当前、断开全部等网关操作
* 为上述网关操作提供全局热键（快捷键）支持
* 可设置为开机启动/计划任务以方便使用
* 高度支持定制，提供多种[开发者工具](/README.md#高级功能)，例如执行自定义脚本（默认为内置的[Ensure_Conn](https://github.com/Z-H-Sun/ipgw#ensure_conn)，功能为确保本机网关连接）等
* 其他`IPGW_Core`所具有[特性](https://github.com/Z-H-Sun/ipgw#特性)（如支持存储多个账号、加密链接启用证书验证、支持HTTP(S)代理等）

## 使用方法
### 下载及运行
* 如果已经下载了[IPGW_Core](https://github.com/Z-H-Sun/ipgw)，只需下载[GUI部分](https://github.com/Z-H-Sun/IPGW_GUI/releases/download/v2.01/IPGW_GUI-v2.01_without_core.zip)，并与`IPGW_Core`存放于同一目录下（*注意：勿启用`IPGW_Core`中的Hotkeys小工具，可能与`IPGW_GUI`冲突*）；
* 否则，请下载[合并程序包](https://github.com/Z-H-Sun/IPGW_GUI/releases/download/v2.01/IPGW_GUI-v2.01.zip)，其中只保留了`IPGW_Core`中的必要功能；
* 随后，下载解压运行`IPGW_GUI.exe`即可使用（*注意：程序对解压所在文件夹应具有写入权限*）。

### 基本功能

<p align="center"><img width="30%" height="30%" src="/screenshots/1.png"></p>

* 双击打开应用程序后即显示“设置”窗口，设置好网关账号密码（*输入焦点离开文本框即会更新保存*）；
* **点击`启动(I)`按钮开启核心进程**；程序会**先自动连接网关**；随后，只需按下对应的快捷键即可实现连接网关/断开连接/断开指定/断开全部等操作。默认`Ctrl+F8`=`连接网关`，`Shift+F8`=`断开本机`，`Alt+F8`=查看当前连接（弹出如下所示窗口）/`断开指定`，`Ctrl+Shift+F8`=`断开全部`。<p align="center"><img width="50%" height="50%" src="/screenshots/3.png"></p>
* 若连接成功，则会显示如下所示包含连接基本信息的气泡提示，3秒后消失；若连接失败，则会在气泡提示中显示错误原因（如账号封锁、IP处在校外等）。<p align="center"><img width="50%" height="50%" src="/screenshots/2.png"></p>
* 若设置为开机启动，那么为使用方便起见，开机时本程序会**在后台运行，并自动启动核心进程连接网关**（*即不会弹出“设置”窗口，且无需手动按“启动”按钮*）；全局快捷键将同样有效。
* 在假期离校时可设置计划启动时间，在此之前程序不会开机启动。
* 程序启动后在托盘创建图标，双击或右键菜单可调出“设置”窗口。
* 在“热键”选项卡可以自定义网关操作的快捷键，离开选项卡更新。
* 可执行自定义脚本（默认快捷键=`Ctrl+Shift+Alt+F8`，*程序将会新开一个独立的附加进程*），默认为内置的“保持网关连接”[Ensure_Conn](https://github.com/Z-H-Sun/ipgw#ensure_conn)，功能为确保本机网关连接，每隔十分钟若发现无法连接外网，先尝试连接网关；如果发现连接数太多，则强制下线其他连接（*具体规则为优先断开非化学楼内的、最早连接的IP地址。该规则可以在代码文件中参照注释进行更改*）；同时输出日志文件。
    
### 高级功能
* 如果你不明白这一部分的内容，请略过且不要改动这一节所提到的任何设置
* 点击`开发(D)`按钮打开“开发者工具”窗口（如下图所示），可实现监控和调试功能；<p align="center"><img width="50%" height="50%" src="/screenshots/4.png"></p>
* 在文本框内输入要在核心进程执行的Ruby脚本（程序默认给出查询连接的范例），随后点击`执行(E)`按钮（具体说明详见[脚本说明](/README.md#脚本说明)）；
* 点击`输出(S)`按钮以显示/隐藏核心进程的标准错误流。
* .NET控件的键盘事件不支持接收Windows徽标键作为“修饰键(modifier keys)”，因此若要设置诸如`Win+F8`之类的快捷键，请手动编辑程序同一目录下的`ipgw_gui.ini`中的`Mod{n}`(*n*=1-5)值，在其初始值基础上+8（作为对照，Alt=+1; Ctrl=+2; Shift=+4）。
* 在“高级”选项卡中可设置网关操作的HTTP(S)代理、启动脚本命令行、自定义脚本命令行；
* 例如自定义脚本可替换为除了“保持网关连接”之外的其他.rb文件，或者将`"ipgw.log"`改为`nul`以不输出日志文件；
* 启动脚本指每次“启动核心进程”之前所加载的指令：

    * 其中大部分内容关乎程序稳定性，不建议更改
    * 但可以改动`p_out @ipgw.connection(@uid, 0, '', 20)`这一行以定制进程启动时的行为（默认为：对选定的网关账户，自动执行连接网关操作，设置超时(timeout)为20秒，并且显示气泡提示）
    * 具体详见[脚本说明](/README.md#脚本说明)，这里提供一些可行的更改样例：

        * 删除或注释该行可阻止启动时自动连接网关
        * 改为`@ipgw.connection(@uid, 0, '', 20); puts false`则可不显示气泡提示（但仍然自动连接网关）
        * 改为 `@ipgw.connection(@uid, -2, '', 20); puts false; sleep(0.5); p_out @ipgw.connection(@uid)` 表示启动时先断开所有连接（不弹出气泡提示），然后再连接本机网关（有气泡提示）。

## 脚本说明
* 如果你不明白这一部分的内容，请略过且不要改动这一节所提到的任何设置
* 请先参考 (1) [IPGW_Core](https://github.com/Z-H-Sun/ipgw)的代码及样例脚本；(2) `IPGW_Core`的[内置函数文档](https://github.com/Z-H-Sun/ipgw/blob/master/README.md#内置函数)；(3) 提供的两个脚本文件[init.rb](/Ruby/scripts/init.rb)和[ensure_conn.rb](/Ruby/scripts/ensure_conn.rb)
* 涉及 IPGW 类的 `setProxy` 和 `connection` 操作结束后，主程序会等待接收一行标准输出消息，因此必须 `p_out` 结果或打印一行 `false`， 否则将导致程序故障
    * 如果欲让 IPGW_GUI 响应（显示气泡），请`p_out`结果
    * 否则，在语句后加一句`; puts false`以声明不要显示气泡
* 自定义脚本开头加上一句`require File.join(File.dirname(__FILE__), 'init')`以执行`init.rb`中的相关初始化命令
* 其中包括在 `@uid` `@proxy` 两个变量中读入初始化文件`ipgw_gui.ini`中的值

    * 若核心进程启动后，选定网关账号、代理发生更改，主程序会主动更新这两个变量，因此在“开发者工具”窗口中调用这两个变量完全没有问题
    * 但附加进程启动后再发生的更改一律不会影响这两个变量，因此自定义脚本中若要响应 `IPGW_GUI` 运行中的设置更改，请重新调用 `init`
