using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Net.Http.Headers;

namespace ImageToBase64Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var command = args.Length == 0 ? "help" : args[0].ToLower();
            if (args.Length < 2 || command == "help")
            {
                Console.WriteLine($"{"i2b",-8}: Image to Base64String");
                Console.WriteLine($"{"b2i",-8}: Base64String to Image");
                Console.WriteLine($"{"test",-8}: unit test function");
            }
            else if (command == "i2b")
            {
                var file = args[1];
                using(var image = Image.Load(file))
                {
                    using(var ms = new MemoryStream())
                    {
                        image.Save(ms, new JpegEncoder { Quality = 85 });
                        var array = ms.ToArray();

                        var base64String = Convert.ToBase64String(array);

                        var color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine(base64String);
                        Console.ForegroundColor = color;
                    }
                }
            }
            else if(command == "b2i")
            {
                throw new NotImplementedException();                
            }
        }
    }
}