using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "your_api_key";

        Console.WriteLine("Lütfen bir konu giriniz:");
        while (true)
        {
            var prompt = Console.ReadLine();
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            client.DefaultRequestHeaders.Add("HTTP-Referer", "https://openrouter.ai/");
            client.DefaultRequestHeaders.Add("X-Title", "OpenAIChat");
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You're great asistant." },
                new { role = "user", content = prompt }
            },
                temperature = 0.7,
                max_tokens = 500,
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            try
            {
                //var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                    var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                    Console.WriteLine("Cevap:");
                    Console.WriteLine(answer);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Response: {responseString}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}