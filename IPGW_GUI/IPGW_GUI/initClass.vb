Imports System.Text
Imports System.Management
Imports System.Security.Cryptography

Public Class initClass
    Public autorun, useproxy As Boolean, notuntil As Date, uuid, initscript, customscript, proxyaddr, proxyusr, proxypsw As String, uid, proxyport As Integer, users As New ArrayList
    Public modifiers() As Integer = {2, 1, 4, 6, 7}
    Public keys() As Integer = {119, 119, 119, 119, 119}
    Public Declare Function GetPrivateProfileIntA Lib "kernel32" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal nDefault As Integer, ByVal lpFileName As String) As Integer
    Public Declare Function GetPrivateProfileStringA Lib "kernel32" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Public Declare Function ToUnicode Lib "user32" (ByVal wVirtKey As UInteger, ByVal wScanCode As UInteger, ByVal lpKeyState As Byte(), ByVal pwszBuff As StringBuilder, ByVal cchBuff As Integer, ByVal wFlags As UInteger) As Integer

    Public Sub New()
        ' 强制设定当前目录
        IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory)

        Try ' Win32 UUID
            Dim search = New ManagementObjectSearcher("select * from Win32_ComputerSystemProduct")
            uuid = search.Get()(0)("UUID").Replace("-", "").Substring(0, 32)
        Catch ex As Exception
            MsgBox(String.Format("读取加/解密所需唯一标识符失败", Err.Number, Err.Description), MsgBoxStyle.Critical)
            End
        End Try
        ' 读取INI
        Dim buf As New StringBuilder(256)
        autorun = (GetPrivateProfileIntA("General", "Autorun", 1, ".\ipgw_gui.ini") <> 0)
        GetPrivateProfileStringA("General", "Notuntil", "", buf, 256, ".\ipgw_gui.ini")
        Try
            notuntil = CDate(buf.ToString)
        Catch
            notuntil = Date.Today
        End Try
        uid = GetPrivateProfileIntA("General", "UID", 0, ".\ipgw_gui.ini")
        useproxy = (GetPrivateProfileIntA("Advanced", "UseProxy", 0, ".\ipgw_gui.ini") <> 0)
        proxyport = GetPrivateProfileIntA("Advanced", "ProxyPort", 80, ".\ipgw_gui.ini")
        buf = New StringBuilder(256)
        GetPrivateProfileStringA("Advanced", "InitScript", "scripts\init.rb", buf, 256, ".\ipgw_gui.ini")
        initscript = buf.ToString
        buf = New StringBuilder(256)
        GetPrivateProfileStringA("Advanced", "CustomScript", "scripts\ensure_conn.rb ""ipgw.log""", buf, 256, ".\ipgw_gui.ini")
        customscript = buf.ToString
        buf = New StringBuilder(256)
        GetPrivateProfileStringA("Advanced", "ProxyAddr", "", buf, 256, ".\ipgw_gui.ini")
        proxyaddr = buf.ToString
        buf = New StringBuilder(256)
        GetPrivateProfileStringA("Advanced", "ProxyUsr", "", buf, 256, ".\ipgw_gui.ini")
        proxyusr = buf.ToString
        buf = New StringBuilder(256)
        GetPrivateProfileStringA("Advanced", "ProxyPsw", "", buf, 256, ".\ipgw_gui.ini")
        proxypsw = buf.ToString
        For i = 0 To 4
            modifiers(i) = GetPrivateProfileIntA("Hotkeys", "Mod" & i + 1, modifiers(i), ".\ipgw_gui.ini")
            keys(i) = GetPrivateProfileIntA("Hotkeys", "Key" & i + 1, keys(i), ".\ipgw_gui.ini")
        Next
    End Sub

    Public Function HexStringToBytes(ByVal str As String) As Byte() ' 十六进制 -> 字节组
        Dim hexDigits As String = "0123456789ABCDEF"
        Dim bytes((str.Length >> 1) - 1) As Byte
        For i As Integer = 0 To str.Length - 1 Step 2
            Dim highDigit As Integer = hexDigits.IndexOf(Char.ToUpperInvariant(str.Chars(i)))
            Dim lowDigit As Integer = hexDigits.IndexOf(Char.ToUpperInvariant(str.Chars(i + 1)))
            If highDigit = -1 OrElse lowDigit = -1 Then
                Throw New ArgumentException("字符串包含无效位", "s")
            End If
            bytes(i >> 1) = CByte((highDigit << 4) Or lowDigit)
        Next i
        Return bytes
    End Function

    Public Function UsrPsw(ByVal File As IO.FileStream, Optional ByVal SaveCount As Integer = 0) As Boolean
        ' SaveCount=0：读取账号；否则：存储第n个账号
        ' 返回false说明已读完
        On Error Resume Next
        If File Is Nothing Then Return False
        Dim encripter As Aes = Aes.Create("AES")
        encripter.Mode = CipherMode.CBC
        ' 初始向量，用户名(\0补齐)，密钥，密码块长度
        Dim iv(15) As Byte, usernm(15) As Byte, pKey(31) As Byte, pswLen As Short
        HexStringToBytes(uuid).CopyTo(pKey, 0) ' 密钥首16位填充uuid

        If SaveCount Then
            Encoding.Default.GetBytes(users(SaveCount - 1)(0)).CopyTo(usernm, 0)
            usernm.CopyTo(pKey, 16) ' 密钥后16位填充usernm
            encripter.GenerateIV()
            File.Write(encripter.IV, 0, 16)
            For i = 0 To 15
                usernm(i) = usernm(i) Xor encripter.IV(i) ' usernm与初始向量异或
            Next
            File.Write(usernm, 0, 16)
            encripter.Key = pKey
            Dim cripter As ICryptoTransform = encripter.CreateEncryptor()
            Dim inBuff As Byte() = Encoding.Default.GetBytes(users(SaveCount - 1)(1))
            Dim passwd As Byte() = cripter.TransformFinalBlock(inBuff, 0, inBuff.Length)
            pswLen = passwd.Length
            File.WriteByte(pswLen And &HFF) ' 密码块长度低8位
            File.WriteByte(pswLen >> 8) ' 高8位
            File.Write(passwd, 0, pswLen)
        Else
            If File.Read(iv, 0, 16) < 16 Then File.Close() : Return False
            File.Read(usernm, 0, 16)
            pswLen = File.ReadByte Or (File.ReadByte << 8)
            Dim passwd(pswLen - 1) As Byte
            File.Read(passwd, 0, pswLen)

            For i = 0 To 15
                usernm(i) = usernm(i) Xor iv(i)
            Next
            usernm.CopyTo(pKey, 16)

            encripter.Key = pKey
            encripter.IV = iv
            Dim cripter As ICryptoTransform = encripter.CreateDecryptor()
            users.Add(New String() {Encoding.Default.GetString(usernm).TrimEnd(vbNullChar), _
                                    Encoding.Default.GetString(cripter.TransformFinalBlock(passwd, 0, pswLen))})
        End If

        If Err.Number Then
            MsgBox(String.Format("用户数据读/写失败，错误代码为 {0} ({1})", Err.Number, Err.Description), MsgBoxStyle.Critical)
            Return False
        End If
        Return True
    End Function

    Public Function ToKeyChar(ByVal KeyVal As Integer) As String
        Dim k As System.Windows.Forms.Keys = KeyVal
        If KeyVal < 48 OrElse (KeyVal > 90 And KeyVal < 184) OrElse KeyVal > 226 Then Return k.ToString
        ' 若为可见字符，返回键盘对应的字符
        Dim buf As New StringBuilder(256), b(256) As Byte
        ToUnicode(KeyVal, 0, b, buf, 256, 0)
        Return buf.ToString.ToUpper
    End Function
End Class
