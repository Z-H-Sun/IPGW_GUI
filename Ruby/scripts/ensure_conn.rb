# ensure_conn: 保持本机网关连接 (强制下线其他连接), 配合 IPGW_GUI 的版本
# 用法: 将自定脚本设为 scripts\ensure_conn.rb <log>
# 其中 <log> 为日志文件位置, 或将其指定为 nul 以阻止生成日志文件

require File.join(File.dirname(__FILE__), 'init') # 请加上此句以初始化
exit if `tasklist`.downcase.scan("ipgw_core").length > 2 # 不能打开多个以避免混乱
INTERVAL = 600 # 每隔十分钟检查一次

# 检查是否联外网, 默认采用百度检验
def checkConn(timeout=5, site='www.baidu.com')
  http = @ipgw.proxy.new(site)
  http.open_timeout = timeout
  http.request_get('/') {|res| return res.class.to_s} # Net::HTTPOK
rescue StandardError, Timeout::Error
  $stderr.puts '网络连接失败: ' + @ipgw.errMsg
end

loop do
  f = open($*[-1], 'ab')
  @uid, proxy = init # 检查是否变化
  if proxy != @proxy
    p_out @ipgw.setProxy(*proxy)
    @proxy = proxy
  end
  err = checkConn
  if err=='Net::HTTPOK' then f.close; sleep(INTERVAL); next end # 能连外网, 什么也不做
  $stderr.puts '状态码响应异常: ' + err unless err.nil?

  f.puts "\nTIME\t#{Time.now}"
  f.puts "ERROR\t#{@ipgw.lastErr}"
  result = p_out(@ipgw.connection(@uid)) # 先尝试连接
  if result
    f.puts "CONN. #\t#{result[0]}"
    f.puts "BALANCE\t#{result[1]}"
    f.puts "IP_ADD\t#{result[2]}"
  else
    result = @ipgw.connection(@uid, 1); puts 'false'
    f.puts "REASON\t#{@ipgw.lastErr}"
    unless @ipgw.lastErr.include?('连接数') then f.close; sleep(INTERVAL); next end # 检查是否由超过连接数限制所致
    if result
      result.each {|i| i[2].insert(0, '*') unless i[1].include?('化学')}
      # 上一句中, 如果 IP 在化学楼外, 则在时间字符串最前面加 *
      # 那么按 ASCII 码排序就会更占先, 即优先断开非化学楼内 IP
      # 按这个思路可以规定其他规则. 如无必要可注释掉这一行
      result = result.sort_by{|i| i[2]}[0] # 按时间顺序排序, 优先断最早的 IP
      # 按这个思路可以规定其他规则. 如无必要则可注释掉这一行
      f.puts "DISCON.\t#{result[0]}"
      f.puts "LOCAT.\t#{result[1]}"
      f.puts "DATETM.\t#{result[2]}"
      
      p_out @ipgw.connection(@uid, 2, result[0])
      sleep(1)
      
      result = p_out(@ipgw.connection(@uid, 0))
      if result
        f.puts "CONN. #\t#{result[0]}"
        f.puts "BALANCE\t#{result[1]}"
        f.puts "IP_ADD\t#{result[2]}"
      else
        f.puts "REASON\t#{@ipgw.lastErr}"
      end
    else
      f.puts "REASON\t#{@ipgw.lastErr}"
    end
  end
  f.close; sleep(INTERVAL)
end
