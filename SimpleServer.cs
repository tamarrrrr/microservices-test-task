using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace helloworldserver
{
    public class SimpleServer
    {
        public static HttpListener serverListener;
        public static string url = "http://localhost:8080/";        

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                // wait for request
                HttpListenerContext context = await serverListener.GetContextAsync();

                // request and response objects
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                // If `shutdown` url requested then shutdown
                if (request?.Url?.AbsolutePath == "/shutdown")
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }

                // response
                string? greetingName = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("GREETING_NAME")) ? "Anonymous" : Environment.GetEnvironmentVariable("GREETING_NAME");
                byte[] output = Encoding.UTF8.GetBytes($"Hello, {greetingName}");
                response.ContentType = "text/html";
                response.ContentEncoding = Encoding.UTF8;
                response.ContentLength64 = output.LongLength;
                await response.OutputStream.WriteAsync(output, 0, output.Length);
                response.Close();
            }
        }
    }
}
