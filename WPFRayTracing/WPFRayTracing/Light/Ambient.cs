using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class Ambient : Light
    {
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
            Ray ShadowRay = new Ray();
            ShadowRay.Origin = sr.HitPoint;

            Vector3D DiffL = new Vector3D();

            for(int i = 0; i< SamplerRef.NumSamples; ++i)
            {
                Vector3D HemiVec = GetDirection(ref sr).Normalize().ToVector3D();
                ShadowRay.Direction = u.ScaleBy(HemiVec.X) + v.ScaleBy(HemiVec.Y) + w.ScaleBy(HemiVec.Z);
                if (CheckInShadow(ref ShadowRay, ref sr))
                    DiffL += (MinAmount * ls * Color);
                else
                    DiffL += (ls * Color);
            }

            DiffL /= SamplerRef.NumSamples;

            return DiffL;
        }

        public float ls { get; set; }
        public Vector3D Color { get; set; }
        public float MinAmount { get; set; }
        protected Sampler SamplerRef;
        public bool bCastShadow { get { return false; } }
        public bool EnableCastShadow() { return bCastShadow; }
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
    }
}
