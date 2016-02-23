using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class Materials
    {
        public Materials()
        { }

        public Materials(Materials Mat)
        {
        }

        public virtual Vector3D Shading(ShaderRec SR)
        {
            return new Vector3D(0, 0, 0);
        }
    }
}
