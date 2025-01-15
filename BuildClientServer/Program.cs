using System;
using System.Net.Sockets;
using System.Text;

class ClientApp
{
    static void Main(string[] args)
    {
        
        string serverAddress = "localhost";
        int port = 8080;

        try
        {
           
            TcpClient tcpClient = new TcpClient(serverAddress, port);
            Console.WriteLine($"Connected to server at {serverAddress}:{port}");

            
            NetworkStream stream = tcpClient.GetStream();

           
            Console.Write("Enter a message to send to the server: ");
            string message = Console.ReadLine();
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            stream.Write(messageBytes, 0, messageBytes.Length);
            Console.WriteLine($"Sent to server: {message}");

           
            byte[] buffer = new byte[256];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string serverResponse = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received from server: {serverResponse}");

            
            tcpClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

