STDOUT.sync = true # 必要, 否则主程序不会接收到STDOUT消息
RdINIInt = Win32API.new('kernel32','GetPrivateProfileInt','pplp','i')
RdINIStr = Win32API.new('kernel32','GetPrivateProfileString','pppplp','i')
# 对于数组, 用 \0 连接传入主程序
def p_out(obj)
  if obj.is_a?(Array) then puts obj.join("\0") else puts obj end
  return obj
end
@ipgw = IPGW.new

def init # 返回读取INI所得的uid, proxy
  uid = RdINIInt.call('General', 'UID', 0, '.\ipgw_gui.ini') # 请注意正确设置当前目录, 一般为ipgw_core所在目录
  proxy = []
  if RdINIInt.call('Advanced', 'UseProxy', 0, '.\ipgw_gui.ini').zero?
    return [uid, [nil]]
  else
    items = ['ProxyAddr', 'ProxyUsr', 'ProxyPsw']
    for i in 0..2
      buf = "\0" * 256
      len = RdINIStr.call('Advanced', items[i], '', buf, 256, '.\ipgw_gui.ini')
      proxy << buf[0, len]
    end
    proxy.insert(1, RdINIInt.call('Advanced', 'ProxyPort', 80, '.\ipgw_gui.ini'))
    return [uid, proxy]
  end
end
# 在 @uid @proxy 中读入初始化文件中的值 可供其他 .rb 文件调用
# 若核心进程启动后发生更改, 主程序会主动更新这两个变量
# 但附加进程启动后再发生的更改一律不会影响这两个变量, 请重新调用init
@uid, @proxy = init

# 涉及 IPGW 类的 setProxy 和 connection 操作结束后, 主程序会等待接收一行标准输出消息
# 因此必须 p_out 结果或打印一行 false (详见下), 否则将导致程序故障
@ipgw.setProxy(*@proxy); puts 'false' # 如果不欲让IPGW_GUI响应, 请加上这一句声明

if __FILE__ == $FILENAME # 排除 require 本文件的情况

  # 若要自定义主程序启动时的行为, 可修改下一行
  p_out @ipgw.connection(@uid, 0, '', 20) # 如果欲让IPGW_GUI响应, 请p_out结果
  # 考虑到开机启动响应较慢, 把超时调得稍微长一些
  # 其他代码请勿随意改动

  # 开始等待主程序指令
  @binding = binding()
  loop do
    $stderr.puts ':IDLE' # 此时可以接收指令 (就绪)
    scr = STDIN.gets(sep="\0")
    $stderr.puts ':WORKING' # 此时无法接收指令 (正在工作)
    eval(scr, @binding)
  end
end