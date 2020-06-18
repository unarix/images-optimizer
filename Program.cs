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
            if (opts.help) 
                Console.WriteLine(opts.ToString() + "" + opts);
            else if (opts.recursive) 
            {
                const int size = 150;
                const int quality = 75;

                // Obtengo todos los archivos que coincidan con: 
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                string [] fileEntries = Directory.GetFiles(".", String.Format("*.{0}", filters));
                
                foreach(string fileName in fileEntries)
                    convertImg(fileName,size,quality);

                // Recurse into subdirectories of this directory.
                /*string [] subdirectoryEntries = Directory.GetDirectories(".");
                foreach(string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory);*/

            }
        }

        static void defaultAction(IEnumerable<Error> errs)
        {
            Console.WriteLine("No option, type -h for help.");
        }

        static bool convertImg(string file, int size, int quality)
        {
            try
            {
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
                    using (var graphics = Graphics.FromImage(resized))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.DrawImage(image, 0, 0, width, height);
                        using (var output = File.Open(file.Replace(".","_rez."), FileMode.Create))
                        {
                            var qualityParamId = Encoder.Quality;
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                            var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codecx => codecx.FormatID == ImageFormat.Jpeg.Guid);
                            resized.Save(output, codec, encoderParameters);
                        }
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
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
    }
}
