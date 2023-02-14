using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnityNetworkUtils
{
    /// <summary>
    /// TCP/UDP协议
    /// </summary>
    public class TCPUtil
    {
        private static List<Socket> sockets = new List<Socket>();

        public void Example()
        {
            // 获取主机IPHostEntry
            Dns.GetHostEntry("localhost");
            // 获取主机ip
            IPAddress[] hostIPs = Dns.GetHostEntry("localhost").AddressList;
            // ip地址转字符串
            string str = hostIPs[0].ToString();
            // 字符串转ip地址
            IPAddress.Parse(str);
        }

        private static Socket Connect(string ipAddressString, int port)
        {
            // 创建ip地址
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);
            // 创建终端
            IPEndPoint 终端 = new IPEndPoint(ipAddress, port);
            // 创建TCP/IP套接字
            Socket 套接字 = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            // 连接
            套接字.Connect(终端);
            sockets.Add(套接字);
            return 套接字;
        }

        public static string Connect(string ipAddressString, int port, string msgString)
        {
            Debug.Log("尝试建立连接并请求数据");
            Socket socket = GetSocket(ipAddressString, port);
            if (socket == null)
            {
                Debug.Log("Connect");
                socket = Connect(ipAddressString, port);
            }

            Byte[] bytes = new Byte[1024];
            // 字符串转字节数组
            byte[] msg = Encoding.ASCII.GetBytes(msgString);
            // 通过套接字发送字节数组
            socket.Send(msg);
            Debug.Log("发送成功");
            // 接收返回的数据
            int 长度 = socket.Receive(bytes);
            return (Encoding.ASCII.GetString(bytes, 0, 长度));
        }

        public static void CloseSocket(string ipAddressString, int port)
        {
            var socket = GetSocket(ipAddressString, port);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private static Socket GetSocket(string ipStr, int port)
        {
            foreach (Socket socket in sockets)
            {
                IPEndPoint ip = socket.RemoteEndPoint as IPEndPoint;
                if (ip.Address.ToString() == ipStr && ip.Port == port)
                {
                    return socket;
                }
            }

            return null;
        }
    }
}