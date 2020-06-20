using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

/*
    https://github.com/unarix/images-optimizer
    - nhtello 2020 (initial project)
*/
namespace images_optimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args).WithParsed(withoption).WithNotParsed(defaultAction);
        }
            
        static void withoption(Options opts)
        {
            if (opts.help){
                Console.WriteLine("");
                Console.WriteLine(@"                           ¯\_(ツ)_/¯");
                Console.WriteLine("   -h Help");
                Console.WriteLine("   -r Recursive (800px of width and 90% of quality by default)");
                Console.WriteLine("   -w Size width in pixels (and keeps the aspect ratio), example: -w 150");
                Console.WriteLine("   -q Quiality in percentage, example: -q 90");
                Console.WriteLine("");
            }
            else if (opts.recursive) 
            {
                int size = (opts.width>0)? opts.width : 800;
                int quality = (opts.quiality>0)? opts.quiality : 90;

                Console.WriteLine("\U0001F964 · Searching formats in this path: jpg, jpeg, png, gif, tiff, bmp, svg");
                
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                string [] fileEntries = Directory.GetFiles(".", String.Format("*.{0}", filters));
                
                foreach(string fileName in fileEntries)
                    convertImg(fileName,fileName.Replace("./","./rez/"),size,quality);

            }
            else
            {
                Console.WriteLine(" Type -h for usage arguments");
            }
        }

        static void defaultAction(IEnumerable<Error> errs)
        {
            Console.WriteLine("No option, type -h for help.");
        }

        static bool convertImg(string file, string fileGen, int size, int quality)
        {
            try
            {
                Console.Write(" - \U0001F477 Creating image " + fileGen + " with quality: " + quality + " and width size:" + size);

                System.IO.Directory.CreateDirectory("rez");
                Console.Write(" .");
                using (var image = new Bitmap(System.Drawing.Image.FromFile(file)))
                {
                    int width, height;
                    if (image.Width > image.Height)
                    {
                        width = size;
                        height = Convert.ToInt32(image.Height * size / (double)image.Width);
                    }
                    else
                    {
                        width = Convert.ToInt32(image.Width * size / (double)image.Height);
                        height = size;
                    }
                    var resized = new Bitmap(width, height);
                    Console.Write(".");
                    using (var graphics = Graphics.FromImage(resized))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.DrawImage(image, 0, 0, width, height);
                        Console.Write(".");
                        using (var output = File.Open(fileGen, FileMode.CreateNew))
                        {
                            var qualityParamId = Encoder.Quality;
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                            var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codecx => codecx.FormatID == ImageFormat.Jpeg.Guid);
                            resized.Save(fileGen, codec, encoderParameters);
                            Console.Write(".");
                        }
                    }
                    Console.Write(" \U00002714 -> Created OK \n");
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(" \U0000274C \n -> " + ex.Message );
                return false;
            }
        }

    }

    public class Options
    {
        [Option('h', "help", Required = false, HelpText = "This helps.")]
        public bool help { get; set; }

        [Option('r', "recursive", Required = false, HelpText = "Make optimization recursive")]
        public bool recursive { get; set; }

        [Option('q', "quiality", Required = false, HelpText = "Quality")]
        public int quiality { get; set; }
        
        [Option('w', "width", Required = false, HelpText = "Width")]
        public int width { get; set; }
    }
}
