using AssemblyAI;
using AssemblyAI.Transcripts;
using System.Reflection;

class Program
{
    static async Task Main(string[] args)
    {

        string apiKey = "your_api_key";

        #region OpenAI

        string audioPath = "ses.mp3";

        //using (var client = new HttpClient())
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        //    var requestContent = new MultipartFormDataContent();
        //    requestContent.Add(new StreamContent(File.OpenRead(audioPath)), "file", Path.GetFileName(audioPath));
        //    requestContent.Add(new StringContent("whisper-1"), "model");
        //    try
        //    {
        //        var response = await client.PostAsync("https://api.openai.com/v1/audio/transcriptions", requestContent);
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Console.WriteLine("Transcription:");
        //            Console.WriteLine(responseString);
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Error: {response.StatusCode}");
        //            Console.WriteLine($"Response: {responseString}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}

        //using (var client = new HttpClient())
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        //    var form = new MultipartFormDataContent();
        //    var audioContent = new ByteArrayContent(File.ReadAllBytes(audioPath));
        //    audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse("audio/mpeg");
        //    form.Add(audioContent, "file", Path.GetFileName(audioPath));
        //    form.Add(new StringContent("whisper-1"), "model");
        //    Console.WriteLine("Transcribing audio...");
        //    var response = await client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine("Transcription:");
        //        Console.WriteLine(result);
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Error: {response.StatusCode}");
        //        Console.WriteLine(await response.Content.ReadAsStringAsync());
        //    }
        //}


        #endregion


        #region AssemblyAI -> https://www.assemblyai.com/
        string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        string audioFilePath = Path.Combine(exePath, "ses.mp3");

        Console.WriteLine($"Looking for audio file at: {audioFilePath}");

        if (!File.Exists(audioFilePath))
        {
            Console.WriteLine("Audio file not found! Please ensure the mp3 file is in the same directory as the executable.");
            return;
        }

        try
        {
            Console.WriteLine("Initializing AssemblyAI client...");
            var client = new AssemblyAIClient(apiKey);

            Console.WriteLine("Transcribing audio...");

            var transcript = await client.Transcripts.TranscribeAsync(
                new FileInfo(audioFilePath)
            );

            Console.WriteLine($"{transcript.Status}");

            if (transcript.Status == TranscriptStatus.Error)
            {
                Console.WriteLine("Error: " + transcript.Error);
                return;
            }

            Console.WriteLine("Transcription: ");
            if (string.IsNullOrEmpty(transcript.Text))
            {
                Console.WriteLine("Sorry, we were unable to convert the audio file to text.");
            }
            else
            {
                Console.WriteLine(transcript.Text);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        #endregion
    }
}