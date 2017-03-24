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

        public override Vector3D Shading(ref ShadeRec SR)
        {
            Vector3D L = base.Shading(ref SR);

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

        public override Vector3D PathShading(ref ShadeRec SR)
        {
            Vector3D wo = -SR.Ray.Direction;
            Vector3D wi;
            double pdf;
            Vector3D fr = PefSpecularBRDF.SampleF(ref SR, ref wo, out wi,out pdf);
            ReflectRay.Origin = SR.HitPoint;
            ReflectRay.Direction = wi;
            Vector3D Rslt = SR.World.RayTracer.TraceRay(ref ReflectRay, SR.Depth + 1);
            double p = (SR.Normal.DotProduct(wi) / pdf);
            return new Vector3D(fr.X * Rslt.X * p, fr.Y * Rslt.Y * p, fr.Z * Rslt.Z * p);
        }

    }
}
