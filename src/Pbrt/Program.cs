using System;
using System.IO;

namespace Pbrt
{
    public class Program
    {
        // Book: https://www.pbr-book.org/3ed-2018/contents
        // C++ sources: https://github.com/mmp/pbrt-v3
        public static void Main(string[] args)
        {            
            using (PhysicallyBasedRayTracer rayTracer = new PhysicallyBasedRayTracer())
            {
                string outputFolder = args[0];
                string outputFile = Path.Combine(outputFolder, "render.png");
                rayTracer.Initialize();
                rayTracer.RenderScene(outputFile);                
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
