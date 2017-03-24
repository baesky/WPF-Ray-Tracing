using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class GlossyReflective : Matte
    {
        public GlossyReflective():base()
        {
            GlossySpecularBRDF = new GlossySpecular();
        }

        public override Vector3D AreaLightShade(ref ShadeRec SR)
        {
            Vector3D L = base.Shading(ref SR);
            Vector3D Wo = -SR.Ray.Direction;
            Vector3D Wi;
            double pdf;
            Vector3D fr = GlossySpecularBRDF.SampleF(ref SR, ref Wo, out Wi, out pdf);
            Ray ReflectedRay = new Ray(ref SR.HitPoint, ref Wi);
           // ReflectedRay.Depth = SR.Depth + 1;
            Vector3D FrTemp = fr.ScaleBy(SR.Normal.DotProduct(Wi)) / pdf;
            Vector3D Rslt = SR.World.RayTracer.TraceRay(ref ReflectedRay, SR.Depth + 1);

            L += new Vector3D(Rslt.X * FrTemp.X, Rslt.Y * FrTemp.Y, Rslt.Z * FrTemp.Z);
            return L;
        }

        public GlossySpecular GlossySpecularBRDF { get; set; }
    }
}
