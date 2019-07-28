Public Class PassWordBox

    Inherits TextBox
    '防止密码框中的文本被S/GetText,禁止更改PasswordChar(以变更为明文),因而屏蔽了WM_SETTEXT,
    'WM_GETTEXT,EM_SETPASSWORDCHAR这三个消息,除非程序本身验证IsSecure,且发出消息后立即关闭

    Const WM_SETTEXT As Integer = &HC
    Const WM_GETTEXT As Integer = &HD
    Const EM_SETPASSWORDCHAR = &HCC
    Private components As System.ComponentModel.IContainer
    Private IsSecure As Boolean

    Sub New()
        Me.Multiline = False
        Me.UseSystemPasswordChar = True
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_SETTEXT OrElse m.Msg = WM_GETTEXT OrElse m.Msg = EM_SETPASSWORDCHAR Then
            If Not IsSecure Then Return
        End If
        MyBase.WndProc(m)
    End Sub

    Public Overrides Property Text() As String
        Get
            IsSecure = True
            Return MyBase.Text
            IsSecure = False
        End Get
        Set(ByVal value As String)
            IsSecure = True
            MyBase.Text = value
            IsSecure = False
        End Set
    End Property

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)
    End Sub
End Class
