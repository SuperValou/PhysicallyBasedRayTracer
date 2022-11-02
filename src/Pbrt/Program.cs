using System;

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
                rayTracer.Initialize();
                rayTracer.RenderScene();
            }
        }
    }
}
