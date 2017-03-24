﻿
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class Matte : Materials
    {
        public Matte()
        {
            AmbientBRDF = new Lambertian();
            DiffuseBRDF = new Lambertian();
            SpecularBRDF = new PerfactSpecular();
        }
        public Lambertian AmbientBRDF { get; set; }
        public Lambertian DiffuseBRDF { get; set; }
        public PerfactSpecular SpecularBRDF { get; set; }

        public override Vector3D Shading(ref ShadeRec SR)
        {
            Vector3D Wo = -SR.Ray.Direction;
            Vector3D RHO = AmbientBRDF.RHO(ref SR, ref Wo);
            Vector3D LtFac = SR.World.AmbientLight.L(ref SR);
            Vector3D L = new Vector3D(RHO.X * LtFac.X, RHO.Y * LtFac.Y, RHO.Z * LtFac.Z);

            if ((L.X + L.Y + L.Z) <= 0.001f)
                return L;

            foreach (Light Lt in SR.World.Lights)
            {
                Vector3D Wi = Lt.GetDirection(ref SR).Normalize().ToVector3D();
                double NDotWi = SR.Normal.DotProduct(Wi);
                if(NDotWi > 0.0)
                {
                    bool bInShadow = false;
                    if (Lt.EnableCastShadow())
                    {
                        ShadowRay.Origin = SR.HitPoint;
                        ShadowRay.Direction = Wi;
                        bInShadow = Lt.CheckInShadow(ref ShadowRay, ref SR);
                    }

                    if (!bInShadow)
                    {
                        RHO = DiffuseBRDF.Factor(ref SR, ref Wo, ref Wi) + SpecularBRDF.Factor(ref SR, ref Wo, ref Wi);
                        LtFac = Lt.L(ref SR);
                        L += new Vector3D(RHO.X * LtFac.X * NDotWi, RHO.Y * LtFac.Y * NDotWi, RHO.Z * LtFac.Z * NDotWi);
                    }      
                }
            }

            return L;
        }
        public override Vector3D PathShading(ref ShadeRec SR)
        {
            Vector3D wo = -SR.Ray.Direction;
            Vector3D wi;
            double pdf;
            Vector3D f = DiffuseBRDF.SampleF(ref SR, ref wo,out wi,out pdf);
            float NDotWi = (float)(SR.Normal.DotProduct(wi) / pdf);
            ReflectRay.Origin = SR.HitPoint;
            ReflectRay.Direction = wi;
            Vector3D Rslt = SR.World.RayTracer.TraceRay(ref ReflectRay, SR.Depth + 1);
            return new Vector3D(f.X * Rslt.X * NDotWi, f.Y * Rslt.Y * NDotWi, f.Z * Rslt.Z * NDotWi);
        }
        public override Vector3D AreaLightShade(ref ShadeRec SR)
        {
            Vector3D Wo = -SR.Ray.Direction;
            Vector3D RHO = AmbientBRDF.RHO(ref SR, ref Wo);
            Vector3D LtFac = SR.World.AmbientLight.L(ref SR);
            Vector3D L = new Vector3D(RHO.X * LtFac.X, RHO.Y * LtFac.Y, RHO.Z * LtFac.Z);

            if ((L.X + L.Y + L.Z) <= 0.001f)
                return L;

            foreach (Light Lt in SR.World.Lights)
            {
                Vector3D Wi = Lt.GetDirection(ref SR).Normalize().ToVector3D();
                double NDotWi = SR.Normal.DotProduct(Wi);
                if (NDotWi > 0.0)
                {
                    bool bInShadow = false;
                    if (Lt.EnableCastShadow())
                    {
                        ShadowRay.Origin = SR.HitPoint;
                        ShadowRay.Direction = Wi;
                        bInShadow = Lt.CheckInShadow(ref ShadowRay, ref SR);
                    }

                    if (!bInShadow)
                    {
                        RHO = DiffuseBRDF.Factor(ref SR, ref Wo, ref Wi) + SpecularBRDF.Factor(ref SR, ref Wo, ref Wi);
                        LtFac = Lt.L(ref SR);
                        float G = Lt.GeoTerms(ref SR) / Lt.PDF(ref SR);
                        L += new Vector3D(RHO.X * LtFac.X * NDotWi * G, RHO.Y * LtFac.Y * NDotWi * G, RHO.Z * LtFac.Z * NDotWi * G);
                    }
                }
            }

            return L;
        }
    }
}
