using MathNet.Spatial.Euclidean;
using System;

namespace WPFRayTracing
{
    public class GlossySpecular : BRDF
    {
        public GlossySpecular()
        {
            Ks = 0;
            Cs = new Vector3D(0, 0, 0);
        }

        public override Vector3D Factor(ref ShadeRec sr, ref Vector3D wo, ref Vector3D wi)
        {
            Vector3D L = new Vector3D();
            double NDotWi = sr.Normal.DotProduct(wi);
            Vector3D r = -wi + sr.Normal.ScaleBy(NDotWi).ScaleBy(2.0);
            double RDotWo = r.DotProduct(wo);

            if (RDotWo > 0.0)
                L = new Vector3D(1,1,1).ScaleBy(Math.Pow(RDotWo, Exp)).ScaleBy(Ks);

            return L;
        }

        public override Vector3D RHO(ref ShadeRec sr, ref Vector3D wo)
        {
            return new Vector3D(0,0,0);
        }

        public override Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo, ref Vector3D wi,out double pdf)
        {
            double NDotWo = sr.Normal.DotProduct(wo);
            Vector3D r = -wo + 2.0 * sr.Normal.ScaleBy(NDotWo);   

            Vector3D w = r;
            Vector3D u = new Vector3D(0.00424, 1, 0.00764).CrossProduct(w).Normalize().ToVector3D();
      
            Vector3D v = u.CrossProduct(w);

            Vector3D sp = SamplerRef.SampleHemisphere();
            wi = sp.X * u + sp.Y * v + sp.Z * w;            

            if (sr.Normal.DotProduct(wi) < 0.0)                       
                wi = -sp.X * u - sp.Y * v + sp.Z * w;

            double phong_lobe =  Math.Pow(r * wi, Exp);
            pdf = phong_lobe * (sr.Normal.DotProduct(wi));

            return Cs.ScaleBy(phong_lobe).ScaleBy(Ks);
        }

        public float Ks { get; set; }
        public Vector3D Cs { get; set; }
        public float Exp { get; set; }
    
    }
}
