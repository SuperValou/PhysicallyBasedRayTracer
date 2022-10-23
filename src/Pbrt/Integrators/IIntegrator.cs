using Pbrt.Core;

namespace Pbrt.Integrators
{
    /// <summary>
    /// Interface in charge of rendering an image of the scene, 
    /// i.e. solving the Light Transport Equation
    /// </summary>
    public interface IIntegrator
    {
        void Render(Scene scene);
    }
}
