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

        public virtual Vector3D Shading(ShadeRec SR)
        {
            return PreDefColor.BlackColor;
        }

        public virtual Vector3D AreaLightShade(ref ShadeRec SR)
        {
            return PreDefColor.BlackColor;
        }
        public virtual Vector3D Le(ref ShadeRec sr)
        { return PreDefColor.BlackColor; }
    }
}
