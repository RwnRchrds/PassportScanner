using System.Text.RegularExpressions;
using IronOcr;
using PassportScanner.Models;
using Tesseract;

namespace PassportScanner.OCR;

public class PassportOcrService
{
    public string GetData(string imagePath, OcrLibrary library)
    {
        var imageText = ExtractText(imagePath, library);

        return imageText;
    }
    
    private string ExtractText(string imagePath, OcrLibrary library)
    {
        switch (library)
        {
            case OcrLibrary.IronOcr:
            {
                var ocrTesseract = new IronTesseract()
                {
                    Language = OcrLanguage.EnglishBest,
                    Configuration = new TesseractConfiguration()
                    {
                        ReadBarCodes = false,
                        BlackListCharacters = "`ë|^",
                        PageSegmentationMode = TesseractPageSegmentationMode.AutoOsd,
                    }
                };
        
                using (var inputPassport = new OcrInput())
                {
                    inputPassport.LoadImage(imagePath);
                    var result = ocrTesseract.Read(inputPassport);
                    return result.Text;
                }
            }
            case OcrLibrary.Tesseract:
            default:
            {
                using var engine = new TesseractEngine(@".\tessdata", "mrz", EngineMode.Default);
                using var img = Pix.LoadFromFile(imagePath);
                var page = engine.Process(img);
                return page.GetText();
            }
        }
    }
}