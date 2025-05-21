using System.Speech.Synthesis;

class Program
{
    static void Main(string[] args)
    {
        using (var synthesizer = new SpeechSynthesizer())
        {
            synthesizer.Volume = 100; // Ses seviyesi (0-100)
            synthesizer.Rate = 0; // Konusma hizi (-10 - 10)

            synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            //synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SpeakAsync("Hello, this is a text-to-speech program. Please, enter the text you want to convert to speech.");

            Console.WriteLine("Enter the text you want to convert to speech:");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No text entered. Exiting.");
                return;
            }

            synthesizer.SpeakAsync(input);

            Console.ReadLine();
        }
    }
}