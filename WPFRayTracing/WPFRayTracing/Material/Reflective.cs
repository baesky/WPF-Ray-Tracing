using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class Reflective : Matte
    {

        public Reflective():base()
        {
            PefSpecularBRDF = new PerfactSpecular();
        }

        public PerfactSpecular PefSpecularBRDF { get; set; }

        public override Vector3D Shading(ShadeRec SR)
        {
            Vector3D L = base.Shading(SR);

            Vector3D Wo = -SR.Ray.Direction;
            Vector3D Wi;
            Vector3D fr = PefSpecularBRDF.SampleF(ref SR, ref Wo,out Wi);
            Ray ReflectedRay = new Ray(ref SR.HitPoint, ref Wi);

            ReflectedRay.Depth = SR.Depth + 1;
            Vector3D FrTemp = fr.ScaleBy(SR.Normal.DotProduct(Wi));
            Vector3D Rslt = SR.World.RayTracer.TraceRay(ref ReflectedRay, SR.Depth + 1);
            L +=  new Vector3D(Rslt.X * FrTemp.X, Rslt.Y * FrTemp.Y, Rslt.Z * FrTemp.Z);

            return L;
        }

       
    }
}
