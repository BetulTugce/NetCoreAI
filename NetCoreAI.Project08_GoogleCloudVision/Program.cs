/* 
 Google Cloud Vision, Google Cloud tarafindan sunulan bir goruntu analiz hizmetidir.
Bu hizmet, goruntuleri analiz ederek nesne tanima, metin okuma (OCR), yuz tanima ve daha fazlasini yapabilen bir API sunar. Google Cloud Vision API, makine ogrenimi ve derin ogrenme tekniklerini kullanarak goruntuleri anlamaya calisir. Bu sayede, goruntulerdeki nesneleri, metinleri ve yuzleri tanimak, analiz etmek icin kullanilabilir.

OCR (Optical Character Recognition): Goruntulerdeki metinleri tanimak, analiz etmek ve dijital metne donusturmek icin kullanilir.

Etiketleme (Labeling): Goruntulerdeki nesneleri, sahneleri tanimak ve etiketlemek icin kullanilir.

Yuz Algilama (Face Detection): Goruntulerdeki yuzleri tanimak ve analiz etmek icin kullanilir. Ayni zamanda goruntulerdeki yuzlerin duygusal durumlarini tanimak icin kullanilan bir tekniktir.

Logo Algilama (Logo Detection): Goruntulerdeki logolari tanimak ve analiz etmek icin kullanilir.

Web Algisi (Web Detection): Goruntunun internette benzerlerinin olup olmadigini kontrol eder. Bu ozellik, goruntunun internetteki benzerlerini bulmak icin kullanilir.

Nesne Algilama (Object Detection): Goruntulerdeki nesneleri tanimak ve analiz etmek icin kullanilir.

Guvenlik Algilari (Safety Detection): Goruntulerdeki guvenlik tehditlerini, uygunsuz icerikleri tanimak ve analiz etmek icin kullanilir.

Kullanim Alanlari
- Belge Tarama ve Metin Cikarma
- Yuz Tanima ve Duygu Analizi
- Guvenlik ve Icerik Denetimi (Uygunsuz Icerik Algilama)
- E-ticaret (Urun Tanima, Logo Algilama)
- Goruntu Arama ve Benzerlik Analizi

Cloud Vision API, hem RESTful HTTP istekleriyle hem de gRPC protokolu uzerinden calisabilir. Bu API, goruntuleri analiz etmek icin HTTP istekleri kullanarak goruntuleri gonderir ve analiz sonucunu aliriz. Google Cloud Vision API, Python, Java, Node.js, Go ve C# gibi bircok programlama dili icin resmi istemci kitapliklari sunar.
 */

using Google.Cloud.Vision.V1;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Resim dosyasının yolunu girin: ");
        string imagePath = Console.ReadLine();
        Console.WriteLine();

        string credentialsPath = "path_to_your_google_cloud_credentials.json"; // Google Cloud kimlik bilgileri dosyasinin yolu

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

        try
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);
            Console.WriteLine("Görüntüden okunan metin:");
            foreach (var annotation in response)
            {
                Console.WriteLine(annotation.Description);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("İşlem tamamlandı.");
            Console.ReadLine();
        }
    }
}