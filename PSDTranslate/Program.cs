#region

using System.Text.RegularExpressions;
using Aspose.PSD;
using Aspose.PSD.FileFormats.Psd;
using Aspose.PSD.FileFormats.Psd.Layers;
using GoogleTranslate;

#endregion

#pragma warning disable CS0219


string jp = @"[\u3000-\u303f\u3040-\u309f\u30a0-\u30ff\uff00-\uff9f\u3400-\u4dbf]+";

string cn =
    @"[\u3400-\u4DB5\u6300-\u77FF\u7800-\u8CFF\u8D00-\u9FCC\u2e80-\u2fd5\u3190-\u319f\u3400-\u4DBF\u4E00-\u9FCC\uF900-\uFAAD]+";

string cnExt =
    @"[\u20000-\u215FF\u21600-\u230FF\u23100-\u245FF\u24600-\u260FF\u26100-\u275FF\u27600-\u290FF\u29100-\u2A6DF\u2A700-\u2B734\u2B740-\u2B81D]+";

string kr = @"[\uac00-\ud7a3]+";

string regex =
    @"[\u3000-\u303f\u3040-\u309f\u30a0-\u30ff\uff00-\uff9f\u3400-\u4dbf\u3400-\u4DB5\u6300-\u77FF\u7800-\u8CFF\u8D00-\u9FCC\u2e80-\u2fd5\u3190-\u319f\u3400-\u4DBF\u4E00-\u9FCC\uF900-\uFAAD\uac00-\ud7a3]+";
// string regex = @"[\u3000-\u303f\u3040-\u309f\u30a0-\u30ff\uff00-\uff9f\u3400-\u4dbf\u3400-\u4DB5\u6300-\u77FF\u7800-\u8CFF\u8D00-\u9FCC\u2e80-\u2fd5\u3190-\u319f\u3400-\u4DBF\u4E00-\u9FCC\uF900-\uFAAD\u20000-\u215FF\u21600-\u230FF\u23100-\u245FF\u24600-\u260FF\u26100-\u275FF\u27600-\u290FF\u29100-\u2A6DF\u2A700-\u2B734\u2B740-\u2B81D\uac00-\ud7a3]+";


GoogleTranslator translator = new();


void TranslatePSD(string path)
{
    using PsdImage im = (PsdImage)Image.Load(path);
    foreach (Layer layer in im.Layers)
    {
        string text = Regex.Match(layer.DisplayName, regex).Value;
        if (text.Length > 0)
        {
            Console.WriteLine("translating " + text);
            List<string> translated = translator.TranslateFromCacheOrNew(text);
            if (translated.Count > 0)
            {
                layer.DisplayName = translated[0];
            }

            Console.WriteLine("result " + layer.DisplayName);
            Console.WriteLine();
        }
    }

    im.Save();
}

DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);

foreach (FileInfo file in dir.GetFiles())
{
    if (file.Name.ToLower().EndsWith(".psd"))
    {
        TranslatePSD(file.FullName);
    }
}

foreach (string arg in Environment.GetCommandLineArgs())
{
    if (arg.ToLower().EndsWith(".dll")) continue;
    if (arg.ToLower().EndsWith(".psd"))
    {
        TranslatePSD(arg);
    }
    else
    {
        try
        {
            dir = new DirectoryInfo(arg);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.ToLower().EndsWith(".psd"))
                {
                    TranslatePSD(file.FullName);
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Error: " + arg);
        }
    }
}

Console.WriteLine("press any key to exit");
Console.ReadKey();
Environment.Exit(0);