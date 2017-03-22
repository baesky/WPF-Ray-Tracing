using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class RectLight : GeometryObject
    {
        public RectLight(Vector3D P0, Vector3D A, Vector3D B, Vector3D Norm):base()
        {
            p0 = P0;
            a = A;
            b = B;
            Normal = Norm;
            InvArea = 1.0f / (float)(a.Length * b.Length);
        } 

        public Vector3D p0;
        public Vector3D a;
        public Vector3D b;
        public Vector3D Normal;
        public override Vector3D GetNormal(Vector3D SamplePt) { return Normal.Normalize().ToVector3D(); }
        public float InvArea = 0.25f;
        public override Vector3D Sample()
        {
            Vector2D SP = SamplerRef.SampleUnitSquare();
            return p0 + a.ScaleBy(SP.X) + b.ScaleBy(SP.Y);
        }
        public override float PDF(ref ShadeRec SR)
        {
            return InvArea;
        }

    }
}
