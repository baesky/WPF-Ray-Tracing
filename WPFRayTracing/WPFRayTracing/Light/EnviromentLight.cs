using System;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class EnviromentLight : Light
    {
        public Materials Mat;
        public Sampler SamplerRef;

        public bool CheckInShadow(ref Ray ShadowRay, ref ShadeRec SR)
        {
            float t = 0.0f;
            foreach (GeometryObject GeoObj in SR.World.Objects)
            {
                if (GeoObj.ShadowHit(ref ShadowRay, ref t))
                    return true;
            }
            return false;
        }

        public bool EnableCastShadow()
        {
            return true;
        }

        public float GeoTerms(ref ShadeRec sr)
        {
            return 1.0f;
        }

        public Vector3D GetDirection(ref ShadeRec sr)
        {
            return SamplerRef.SampleHemisphere();
        }

        public Vector3D L(ref ShadeRec sr)
        {
            return Mat.Le(ref sr);
        }

        public float PDF(ref ShadeRec sr)
        {
            return 1.0f/ (float)Math.PI;
        }
    }
}
