STDOUT.sync = true # ��Ҫ, ���������򲻻���յ�STDOUT��Ϣ
RdINIInt = Win32API.new('kernel32','GetPrivateProfileInt','pplp','i')
RdINIStr = Win32API.new('kernel32','GetPrivateProfileString','pppplp','i')
# ��������, �� \0 ���Ӵ���������
def p_out(obj)
  if obj.is_a?(Array) then puts obj.join("\0") else puts obj end
  return obj
end
@ipgw = IPGW.new

def init # ���ض�ȡINI���õ�uid, proxy
  uid = RdINIInt.call('General', 'UID', 0, '.\ipgw_gui.ini') # ��ע����ȷ���õ�ǰĿ¼, һ��Ϊipgw_core����Ŀ¼
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
# �� @uid @proxy �ж����ʼ���ļ��е�ֵ �ɹ����� .rb �ļ�����
# �����Ľ���������������, �������������������������
# �����ӽ����������ٷ����ĸ���һ�ɲ���Ӱ������������, �����µ���init
@uid, @proxy = init

# �漰 IPGW ��� setProxy �� connection ����������, �������ȴ�����һ�б�׼�����Ϣ
# ��˱��� p_out ������ӡһ�� false (�����), ���򽫵��³������
@ipgw.setProxy(*@proxy); puts 'false' # ���������IPGW_GUI��Ӧ, �������һ������

if __FILE__ == $FILENAME # �ų� require ���ļ������

  # ��Ҫ�Զ�������������ʱ����Ϊ, ���޸���һ��
  p_out @ipgw.connection(@uid, 0, '', 20) # �������IPGW_GUI��Ӧ, ��p_out���
  # ���ǵ�����������Ӧ����, �ѳ�ʱ������΢��һЩ
  # ����������������Ķ�

  # ��ʼ�ȴ�������ָ��
  @binding = binding()
  loop do
    $stderr.puts ':IDLE' # ��ʱ���Խ���ָ�� (����)
    scr = STDIN.gets(sep="\0")
    $stderr.puts ':WORKING' # ��ʱ�޷�����ָ�� (���ڹ���)
    eval(scr, @binding)
  end
end