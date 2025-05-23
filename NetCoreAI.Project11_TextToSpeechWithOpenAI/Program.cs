﻿using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

class Program
{
    private static readonly string apiKey = "your_api_key";
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the text you want to convert to speech:");
        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("No text entered. Exiting.");
            return;
        }
        Console.WriteLine("Converting text to speech...");
        await GenerateSpeech(apiKey, input);
        //bin dosyasinin icinde olusturacak..
        Console.WriteLine("Audio file saved as output.mp3");
        Process.Start("explorer.exe", "output.mp3");
        Console.WriteLine("Press any key to exit.");

        static async Task GenerateSpeech(string apiKey, string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
                var requestBody = new
                {
                    //model = "text-to-speech-001",
                    model = "tts-1",
                    input = text,
                    voice = "alloy"
                };
                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/audio/speech", content);
                if (response.IsSuccessStatusCode)
                {
                    byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync("output.mp3", audioBytes);
                }
                else
                {
                    Console.WriteLine("An error occurred: " + response.StatusCode);
                    return;
                }
            }
        }
    }
}