using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Net.Http.Headers;

namespace ImageToBase64Converter
{
    internal class Program
    {
        static string ImageToBase64String(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, new JpegEncoder { Quality = 100 });
                var array = ms.ToArray();

                var base64String = Convert.ToBase64String(array);
                return base64String;
            }
        }

        static void Main(string[] args)
        {
            var command = args.Length == 0 ? "help" : args[0].ToLower();
            if (args.Length < 2 || command == "help")
            {
                Console.WriteLine($"{"i2b",-8}: Image to Base64String, e.g. i2b <image_file_path> <export_file_path (option)>");
                Console.WriteLine($"{"b2i",-8}: Base64String to Image");
            }
            else if (command == "i2b")
            {
                var file = args[1];
                using var image = Image.Load(file);
                var base64String = ImageToBase64String(image);

                if (args.Length > 2)
                {
                    var path = args[2];
                    Console.WriteLine("Path: " + path);
                    var ext = Path.GetExtension(path);
                    Console.WriteLine("Ext: " + ext);
                    if (ext == ".html")
                    {
                        path = Path.ChangeExtension(path, ".html");
                        File.WriteAllLines(path, new string[]
                        {
                            "<div>",
                            $" <img src=\"data:image/jpg;base64,{base64String}\" height=\"1080px\" alt=\"\" />",
                            "</div>"
                        });
                    }
                    else
                    {
                        path = Path.ChangeExtension(path, ".txt");
                        File.WriteAllText(path, base64String);
                    }
                    Console.WriteLine($"Base64String is export to: \"{path}\"");
                }
                else
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(base64String);
                    Console.ForegroundColor = color;
                }
            }
            else if(command == "b2i")
            {
                throw new NotImplementedException();                
            }
        }
    }
}