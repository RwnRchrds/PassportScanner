using IronOcr;
using PassportScanner.OCR;

namespace PassportScanner;

public class Program
{
    public static void Main(string[] args)
    {
        var images = new[]
            { "British-Passport-1.png", 
                "British-Passport-2.jpg", 
                "British-Passport-3.png", 
                "Korean-Passport-1.jpg" };

        var ocr = new PassportOcrService();

        foreach (var image in images)
        {
            var path = Path.Combine("Images", image);
            var ironText = ocr.GetData(path, OcrLibrary.IronOcr);
            var tesseractText = ocr.GetData(path, OcrLibrary.Tesseract);
            
            Console.WriteLine($"-----{image}-----");
            Console.WriteLine("---IronOCR---");
            Console.WriteLine(ironText);
            Console.WriteLine("---Tesseract---");
            Console.WriteLine(tesseractText);
        }
    }
}