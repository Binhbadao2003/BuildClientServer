using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ServerApp
{
    static void Main(string[] args)
    {
       
        int port = 8080;
        TcpListener tcpListener = new TcpListener(IPAddress.Any, port);

        try
        {
            
            tcpListener.Start();
            Console.WriteLine($"Server is listening on port {port}...");

            while (true)
            {
                
                TcpClient client = tcpListener.AcceptTcpClient();
                Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

              
                NetworkStream stream = client.GetStream();

              
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received from client: {clientMessage}");

                string response = clientMessage.ToUpper();

              
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine($"Sent to client: {response}");

               
                client.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
