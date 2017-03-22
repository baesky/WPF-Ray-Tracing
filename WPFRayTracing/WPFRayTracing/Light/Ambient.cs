using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class Ambient : Light
    {
        public Ambient()
        {
            ShadowRay = new Ray();
        }
        public Vector3D GetDirection(ref ShadeRec sr)
        {
            return SamplerRef.SampleHemisphere();
        }
        public void SetSampler(ref Sampler TheSampler)
        {
            SamplerRef = TheSampler;
            SamplerRef.MapSamplesToHemisphere(1.0f);
        }
        
        public Vector3D L(ref ShadeRec sr)
        {
            Vector3D w = sr.Normal;
            Vector3D v = w.CrossProduct(new Vector3D(0.0072, 1.0, 0.0034)) ; // jitter the up vector in case normal is vertical
            v = v.Normalize().ToVector3D();
            Vector3D u = v.CrossProduct(w);
            u = u.Normalize().ToVector3D();
           
            ShadowRay.Origin = sr.HitPoint;


            Vector3D HemiVec = GetDirection(ref sr).Normalize().ToVector3D();
            ShadowRay.Direction = u.ScaleBy(HemiVec.X) + v.ScaleBy(HemiVec.Y) + w.ScaleBy(HemiVec.Z);
            if (CheckInShadow(ref ShadowRay, ref sr))
                return (MinAmount * ls * Color);
            else
                return (ls * Color);

        }

        public float ls { get; set; }
        public Vector3D Color { get; set; }
        public float MinAmount { get; set; }
        protected Sampler SamplerRef;
        public bool bCastShadow { get { return true; } }
        public bool EnableCastShadow() { return bCastShadow; }
        private Ray ShadowRay;
        public bool CheckInShadow(ref Ray ShadowRay, ref ShadeRec SR)
        {
            float t = 0.0f;
            foreach(GeometryObject GeoObj in SR.World.Objects)
            {
                if (GeoObj.ShadowHit(ref ShadowRay, ref t))
                    return true;
            }
            return false;
        }

        public float GeoTerms(ref ShadeRec SR)
        { return 1.0f; }
        public float PDF(ref ShadeRec SR)
        { return 1.0f; }
    }
}
