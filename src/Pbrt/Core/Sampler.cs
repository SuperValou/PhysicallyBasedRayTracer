using System;

namespace Pbrt.Core
{
    /// <summary>
    /// Responsible for choosing the points on the image plane from which rays are traced, 
    /// and supplying these sample positions to integrators
    /// </summary>
    public class Sampler // TODO: probably abstract class
    {
        /// <summary>
        /// Simple way to avoid making this class thread-safe
        /// </summary>        
        public Sampler Clone()
        {            
            throw new NotImplementedException();
        }
    }
}