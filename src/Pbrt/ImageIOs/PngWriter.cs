using Pbrt.Core;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;

namespace Pbrt.ImageIOs
{
    public static class PngWriter
    {
        public static void WriteImage(string outputFilePath, Vector3[] pixels, Size resolution)
        {
            using (MemoryStream pngStream = new MemoryStream())            
            using (Bitmap bitmap = new Bitmap(resolution.Width, resolution.Height))
            {
                for (int i = 0; i < pixels.Length; i++)
                {
                    var pixel = pixels[i];
                    int red = (int) GammaCorrect(pixel.X);
                    int green = (int) GammaCorrect(pixel.Y);
                    int blue = (int) GammaCorrect(pixel.Z);
                    Color c = Color.FromArgb(red, green, blue);

                    int x = i % resolution.Width;
                    int y = i / resolution.Width;
                    bitmap.SetPixel(x, y, c);
                }

                bitmap.Save(pngStream, ImageFormat.Png);
                File.WriteAllBytes(outputFilePath, pngStream.ToArray());
            }
        }

        private static float GammaCorrect(float value)
        {
            float gammaCorrectedValue;
            if (value <= 0.0031308)
            {
                gammaCorrectedValue = 12.92f * value;
            }
            else
            {
                gammaCorrectedValue = 1.055f * MathF.Pow(value, 1f / 2.4f) - 0.055f;
            }

            gammaCorrectedValue = 255f * gammaCorrectedValue + 0.5f;
            gammaCorrectedValue = Math.Clamp(gammaCorrectedValue, 0, 255);
            return gammaCorrectedValue;
        }
    }
}
