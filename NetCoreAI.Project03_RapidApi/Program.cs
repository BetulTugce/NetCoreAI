using NetCoreAI.Project03_RapidApi.ViewModels;
using Newtonsoft.Json;
var client = new HttpClient();

List<ApiSeriesViewModel> seriesList = new List<ApiSeriesViewModel>();

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"),
    Headers =
    {
        { "x-rapidapi-key", "your_api_key" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();

    seriesList = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);

    Console.WriteLine("Top 100 Series Listesi");
    Console.WriteLine("---------------------------");
    
    for (int i = 0; i < seriesList.Count; i++)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(seriesList[i].id);
        Console.WriteLine(seriesList[i].title + " - IMDB: " + seriesList[i].rating + " - Yıl: " + seriesList[i].year);
        Console.Write(seriesList[i].genre.First());
        for (int j = 1; j < seriesList[i].genre.Count(); j++)
        {
            Console.Write(", ");
            Console.Write(seriesList[i].genre[j]);
        }
        Console.WriteLine($"\n{seriesList[i].image}");
        Console.WriteLine(seriesList[i].description);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("---------------------------");
    }
    
   
}

Console.ReadLine();