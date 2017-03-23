using System;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class Lambertian : BRDF
    {
        public Lambertian()
        {
            Kd = 0;
            Cd = new Vector3D(0,0,0);
        }
        public Lambertian(ref Lambertian OtherLambertian)
        {
            Kd = OtherLambertian.Kd;
            Cd = OtherLambertian.Cd;
            SamplerRef = OtherLambertian.SamplerRef;
        }

        public override Vector3D Factor(ref ShadeRec sr, ref Vector3D wo, ref Vector3D wi)
        {
            return Cd.ScaleBy(Kd * (1.0 / Math.PI));
        }

        public override Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo, ref Vector3D wi, out double pdf)
        {
            Vector3D w = sr.Normal;
            Vector3D v = new Vector3D(0.0034, 1, 0.0071);
            v = v.CrossProduct(w);
            v = v.Normalize().ToVector3D();
            Vector3D u = v.CrossProduct(w);

            Vector3D sp = SamplerRef.SampleHemisphere();
            wi = sp.X * u + sp.Y * v + sp.Z * w;
            wi = wi.Normalize().ToVector3D();

            pdf = (float)(sr.Normal * wi * (1.0/Math.PI));

            return Cd.ScaleBy(Kd * (1.0 / Math.PI));
        }

        public override Vector3D RHO(ref ShadeRec sr, ref Vector3D wo)
        {
            return Cd.ScaleBy(Kd);
        }

        public float Kd { get; set; }
        public Vector3D Cd { get; set; }
    }
}
