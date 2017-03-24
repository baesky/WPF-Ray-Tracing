
using MathNet.Spatial.Euclidean;
namespace WPFRayTracing
{
    public class Emissive : Materials
    {
        public float ls { get; set; }
        public Vector3D ce { get; set; }

        public override Vector3D Shading(ref ShadeRec SR)
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

        public override Vector3D Le(ref ShadeRec sr)
        {
            return ce.ScaleBy(ls);
        }
 
    }
}
