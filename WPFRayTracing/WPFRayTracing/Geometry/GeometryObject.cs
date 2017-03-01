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
    }
}
