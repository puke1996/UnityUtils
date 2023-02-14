using System.IO;
using System.Net;
using System.Threading;

namespace NetwordUtils
{
    public class HTTPServer
    {
        public delegate void RequestHandler(HttpListenerRequest request, HttpListenerResponse response);

        public static void StartServer(string url, int port, RequestHandler requestHandler)
        {
            var serverThread = new Thread(() =>
            {
                var listener = new HttpListener();
                listener.Prefixes.Add(url + port + "/");
                listener.Start();
                while (true)
                {
                    var context = listener.GetContext();
                    var thread = new Thread(() => { requestHandler(context.Request, context.Response); });
                    thread.Start();
                }
            });
            serverThread.Start();
        }

        public static byte[] GetBytesFromRequest(HttpListenerRequest request)
        {
            var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            var memoryStream = new MemoryStream();
            reader.BaseStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}