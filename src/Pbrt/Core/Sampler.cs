using System;
using System.Drawing;
using System.Numerics;

namespace Pbrt.Core
{
    /// <summary>
    /// Responsible for choosing the points on the image plane from which rays are traced, 
    /// and supplying these sample positions to integrators
    /// </summary>
    public class Sampler // TODO: probably abstract class
    {
        public Point PixelCoord { get; private set; }

        /// <summary>
        /// Simple way to avoid making this class thread-safe
        /// </summary>        
        public Sampler Clone()
        {            
            throw new NotImplementedException();
        }

        // TODO: move to child-class and become abstract, see https://pbr-book.org/3ed-2018/Sampling_and_Reconstruction/Sampling_Interface#BasicSamplerInterface        
        public void StartPixel(Point pixelCoord)
        {
            PixelCoord = pixelCoord;
        }

        // TODO: naive implementation, see https://pbr-book.org/3ed-2018/Sampling_and_Reconstruction/Sampling_Interface#BasicSamplerInterface
        public Vector2 GetCameraSample(Point pixelCoordinates)
        {            
            return new Vector2(pixelCoordinates.X, pixelCoordinates.Y);
        }
    }
}