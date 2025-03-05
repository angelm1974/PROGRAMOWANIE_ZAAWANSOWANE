using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


internal class TcpServer
{
    private static void Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any,5000);
        server.Start();
        Console.WriteLine("Server started on port 5000");
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Task.Run(() => HandleClient(client));
        }
    }

    static void HandleClient(TcpClient client)
    {
        NetworkStream stream =client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead=stream.Read(buffer,0,buffer.Length);
        string reccievedMessage = Encoding.UTF8.GetString(buffer,0,bytesRead);
        Console.WriteLine($"Received: {reccievedMessage}");

        string responseMessage = processMathEquation(reccievedMessage);

        byte[] response = Encoding.UTF8.GetBytes($"Wiadomość odebrana\n Wynik: {responseMessage}");
        stream.Write(response,0,response.Length);

        Console.WriteLine("Response wysłany");
        client.Close();
    }

    static string processMathEquation(string equation)
    {
        string[] parts = equation.Split(' ');// 4 + 5
        double a = double.Parse(parts[0]);
        double b = double.Parse(parts[2]);
        double result = 0;
        switch (parts[1])
        {
            case "+":
                result = a + b;
                break;
            case "-":
                result = a - b;
                break;
            case "*":
                result = a * b;
                break;
            case "/":
                result = a / b;
                break;
            default:
                return "Nieznane działanie";
        }
        return result.ToString();
    }

}