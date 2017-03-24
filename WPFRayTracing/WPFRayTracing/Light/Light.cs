using MathNet.Spatial.Euclidean;
using System;

namespace WPFRayTracing
{
    public class PreDefColor
    {
        public static readonly Vector3D BlackColor = new Vector3D(0, 0, 0);
        public static readonly Vector3D WhiteColor = new Vector3D(1, 1, 1);
        public static readonly Vector3D RedColor = new Vector3D(1, 0, 0);
        public static readonly Vector3D YellowColor = new Vector3D(1, 1, 0);
    }
   

    public interface Light
    {
        Vector3D GetDirection(ref ShadeRec sr);

        Vector3D L(ref ShadeRec sr);
        bool EnableCastShadow();
        bool CheckInShadow(ref Ray ShadowRay, ref ShadeRec SR);
        float GeoTerms(ref ShadeRec sr);
        float PDF(ref ShadeRec sr);
    }
}
