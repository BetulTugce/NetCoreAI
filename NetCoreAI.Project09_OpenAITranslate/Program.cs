using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to the OpenAI Translate App!");
        Console.WriteLine("Please enter the text you want to translate:");
        string textToTranslate = Console.ReadLine();
        Console.WriteLine("Please enter the target language (e.g., 'es' for Spanish):");
        string targetLanguage = Console.ReadLine();

        string apiKey = "your_api_key";

        string translation = await TranslateText(textToTranslate, targetLanguage, apiKey);
        if (!string.IsNullOrEmpty(translation))
        {
            Console.WriteLine($"Translated text: {translation}");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }


    private static async Task<string> TranslateText(string text, string targetLanguage, string apiKey)
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                        new { role = "system", content = "You are a helpful translator." },
                        new { role = "user", content = $"Translate the following text to {targetLanguage}: {text}" }
                    },
            max_tokens = 1000
        };
        //var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        //response.EnsureSuccessStatusCode();
        //var responseBody = await response.Content.ReadAsStringAsync();
        //dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
        //return jsonResponse.choices[0].message.content.ToString();

        string jsonBody = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
                return jsonResponse.choices[0].message.content.ToString();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {responseString}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

}
