using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class GeometryObject
    {
        public GeometryObject()
        {
        }

        public GeometryObject(GeometryObject GeoObj)
        {
            Material = GeoObj.Material;
        }

        ~GeometryObject()
        { }

        public virtual bool Hit(Ray TheRay, ref double Param, ref ShadeRec SR)
        {
            return false;
        }
        public Materials Material { get; set; }

        public Vector3D Color { get; set; } //debug only

        public virtual bool ShadowHit(ref Ray RayDir, ref float HitPos) { return false; }
        public virtual float PDF(ref ShadeRec SR) { return 0.0f; }
        public virtual Vector3D Sample() { return PreDefColor.BlackColor; }
        public virtual Vector3D GetNormal(Vector3D SamplePt) { return PreDefColor.BlackColor; }
    }
}
