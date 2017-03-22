using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class Materials
    {
        public Materials()
        { ShadowRay = new Ray(); }

        public Materials(Materials Mat):this()
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

        public Ray ShadowRay;
    }
}
