using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HuHelper
{
    internal class NetworkTime
    {
        // 常用NTP服务器列表
        private static readonly string[] DefaultNtpServers =
            {
                 "time.windows.com",    // 微软官方NTP服务器
                 "time.nist.gov",       // 美国国家标准与技术研究院
                 "pool.ntp.org",        // 全球公共NTP池
                 "cn.pool.ntp.org",     // 中国专用NTP池
                 "0.cn.pool.ntp.org"    // 中国专用服务器
             };
        /// <summary>
        /// 获取网络时间（异步）
        /// </summary>
        /// <param name="ntpServer"></param>
        /// <returns></returns>
        public static async Task<DateTime> GetNetworkTimeAsync(string ntpServer = null)
        {
            try
            {
                // 使用默认服务器或指定服务器
                var server = ntpServer ?? DefaultNtpServers[new Random().Next(DefaultNtpServers.Length)];

                // NTP协议数据包 (48 bytes)
                var ntpData = new byte[48];
                ntpData[0] = 0x1B; // LI = 0, VN = 3, Mode = 3 (Client)

                // 创建UDP客户端
                using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                await socket.ConnectAsync(server, 123);
                socket.ReceiveTimeout = 3000;

                // 发送/接收NTP请求
                await socket.SendAsync(ntpData, SocketFlags.None);
                await socket.ReceiveAsync(ntpData, SocketFlags.None);

                // 解析NTP响应
                ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16
                              | (ulong)ntpData[42] << 8 | ntpData[43];
                ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16
                                | (ulong)ntpData[46] << 8 | ntpData[47];

                // NTP时间戳（1900年1月1日至今的秒数）
                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                var networkTime = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddMilliseconds(milliseconds);

                return networkTime.ToLocalTime();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NTP同步失败: {ex.Message}");
                return DateTime.Now; // 失败时返回本地时间
            }
        }
        /// <summary>
        /// 获取网络连接状态
        /// </summary>
        /// <returns></returns>
        public static bool GetNetworkAvailable()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
