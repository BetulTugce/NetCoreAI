using Tesseract;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Karakter Okuması Yapılacak Metin Yolu: ");
        string imagePath = Console.ReadLine();
        //string tessDataPath = @"C:\Program Files\Tesseract-OCR\tessdata";
        string tessDataPath = @"C:\Tesseract-OCR\tessdata";
        string language = "eng";
        try
        {
            using (var engine = new TesseractEngine(tessDataPath, language, EngineMode.Default))
            {
                // Pix sinifi, Leptonica (goruntu isleme kutuphanesi) kutuphanesinden gelir ve resimleri Tesseract icin islenebilir hale getiren bir veri yapisidir.
                // TesseractEngine sinifi, OCR islemlerini gerceklestirmek icin kullanilir. OCR islemini yapabilmek icin bir motor olusturur ve bu motor, belirtilen dili ve egitim verisini kullanarak resimden metni cıkarır.
                // Process metodu, goruntuyu isleyerek metni cikarir.
                // Page, OCR isleminin sonucunu temsil eder ve metni almak icin kullanilir.
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img, language))
                    {
                        string text = page.GetText();
                        Console.WriteLine("Okunan Metin:");
                        Console.WriteLine(text);
                        Console.WriteLine($"Güven Skoru: {page.GetMeanConfidence()}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
    }
}