using System.Net.Sockets;

internal class TcpClientExample
{
    private static void Main(string[] args)
    {   
        
        
        
        while (true)
        {
            TcpClient client = new TcpClient("127.0.0.1",5000);
            NetworkStream stream = client.GetStream();
            System.Console.WriteLine("Napisz zadanie: ");
            string ?message = System.Console.ReadLine();
            if (message == "exit" || message == null)
            {
                break;
            }
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
            System.Console.WriteLine(response);        
            client.Close();
        }
        
    }




}