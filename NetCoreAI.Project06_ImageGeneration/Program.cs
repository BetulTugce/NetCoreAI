﻿using Newtonsoft.Json;

class Program
{
    public static async Task Main(string[] args)
    {
        #region Open AI Dall E
        string apiKey = "your_api_key";
        Console.Write("Enter the prompt for image generation: ");
        string prompt = Console.ReadLine();
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };
            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
        #endregion


    }
}
