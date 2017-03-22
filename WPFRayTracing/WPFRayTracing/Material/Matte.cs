
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class Matte : Materials
    {
        public Matte()
        {
            AmbientBRDF = new Lambertian();
            DiffuseBRDF = new Lambertian();
            SpecularBRDF = new GlossySpecular();
        }
        public Lambertian AmbientBRDF { get; set; }
        public Lambertian DiffuseBRDF { get; set; }
        public GlossySpecular SpecularBRDF { get; set; }

        public override Vector3D Shading(ShadeRec SR)
        {
            Vector3D Wo = -SR.Ray.Direction;
            Vector3D RHO = AmbientBRDF.RHO(ref SR, ref Wo);
            Vector3D LtFac = SR.World.AmbientLight.L(ref SR);
            Vector3D L = new Vector3D(RHO.X * LtFac.X, RHO.Y * LtFac.Y, RHO.Z * LtFac.Z);

            if ((L.X + L.Y + L.Z) <= 0.001f)
                return L;

            Ray ShadowRay = new Ray();

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
    }
}
