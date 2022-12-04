using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Pbrt.Core
{
    public class Film
    {
        // TODO: replace by a Pixel struct instead, see https://www.pbr-book.org/3ed-2018/Sampling_and_Reconstruction/Film_and_the_Imaging_Pipeline#TheFilmClass
        private Spectrum[] _pixels;

        public Size Resolution { get; }

        public Film(int width, int height) 
            : this(new Size(width, height))
        {
        }

        public Film(Size resolution)
        {
            Resolution = resolution;
            _pixels = new Spectrum[resolution.Width * resolution.Height];
            for (int i = 0; i < _pixels.Length; i++)
            {
                _pixels[i] = Spectrum.Black;
            }
        }

        // TODO: very naive implementation
        public Vector3[] GetPixels()
        {            
            return _pixels.Select(p => p.ToRGB()).ToArray();
        }

        // TODO: simplified signature and implementation, see https://www.pbr-book.org/3ed-2018/Sampling_and_Reconstruction/Film_and_the_Imaging_Pipeline#SupplyingPixelValuestotheFilm
        public void MergeFilmTile(Point pixelCoord, Spectrum result)
        {
            Debug.Assert(pixelCoord.X < Resolution.Width);
            Debug.Assert(pixelCoord.Y < Resolution.Height);

            int index = pixelCoord.X + pixelCoord.Y * Resolution.Width;
            _pixels[index] += result;
        }
    }

}
