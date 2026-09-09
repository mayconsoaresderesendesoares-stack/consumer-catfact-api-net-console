using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumerCatFactApi
{
    public class CatFact
    {
        [JsonPropertyName("fact")]
        public string Fact { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://catfact.ninja/fact";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseString = await response.Content.ReadAsStringAsync();

                    CatFact catFact = JsonSerializer.Deserialize<CatFact>(responseString);

                    if (catFact != null)
                    {
                        Console.WriteLine("Fato sobre Gatos:");
                        Console.WriteLine(catFact.Fact);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Aconteceu um erro ao consultar a api: " + e.Message);
                }
            }
        }
    }
}