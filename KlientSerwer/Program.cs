using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
internal class Program
{
    private readonly HttpClient _httpClient;
    private static void Main(string[] args)
    {
        Program program = new Program();
        string url ="https://prod24.pl/";
        string dane = program.PobierzDaneAsync(url).Result;
        Console.WriteLine(dane);
    }

    public Program()
    {
        Console.WriteLine("Inicjalizacja klienta http");
        _httpClient = new HttpClient(); 
    }

    public async Task<string> PobierzDaneAsync(string url)
    {
        Console.WriteLine("Pobieranie danych z serwera");
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}