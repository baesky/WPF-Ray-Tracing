
using MathNet.Spatial.Euclidean;
namespace WPFRayTracing
{
    public class Emissive : Materials
    {
        public float ls { get; set; }
        public Vector3D ce { get; set; }

        public virtual Vector3D Shade(ref ShadeRec SR)
        {
            return AreaLightShade(ref SR);
        }

        public override Vector3D AreaLightShade(ref ShadeRec SR)
        {
            if (-SR.Normal.DotProduct(SR.Ray.Direction) > 0.0)
            {
                return ce.ScaleBy(ls);
            }
            else
                return PreDefColor.BlackColor;
        }

        public Vector3D Le()
        {
            return ce.ScaleBy(ls);
        }
    }
}
