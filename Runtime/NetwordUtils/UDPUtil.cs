using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace NetwordUtils
{
    public class UDPUtil : MonoBehaviour
    {
        public static void Send(string ipStr, int port, string msg)
        {
            var udpClient = new UdpClient();
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
            var data = Encoding.Default.GetBytes(msg);
            udpClient.Send(data, data.Length, ipEndPoint);
            udpClient.Close();
        }
    }
}