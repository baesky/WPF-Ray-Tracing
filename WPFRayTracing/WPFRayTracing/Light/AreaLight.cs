using System;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class AreaLight : Light
    {
        public bool CheckInShadow(ref Ray ShadowRay, ref ShadeRec SR)
        {
            float t = .0f;
            float ts = (float)(SamplePoint - ShadowRay.Origin).DotProduct(ShadowRay.Direction);

            foreach ( GeometryObject Obj in SR.World.Objects)
            {
                if (Obj.ShadowHit(ref ShadowRay, ref t) && (t < ts))
                    return true;
            }

            return false;
        }

        public bool EnableCastShadow()
        {
            return true;
        }

        public Vector3D GetDirection(ref ShadeRec sr)
        {
            SamplePoint = GeoObj.Sample();
            SampleNormal = GeoObj.GetNormal(SamplePoint);
            Wi = SamplePoint - sr.HitPoint;

            return Wi;
        }

        public Vector3D L(ref ShadeRec sr)
        {
            float NDotD = (float)-SampleNormal.DotProduct(Wi);

            if(NDotD > 0.0f)
            {
                return GeoObj.Material.Le(ref sr);
            }
            else
            {
                return PreDefColor.BlackColor;
            }
        }

        public float GeoTerms(ref ShadeRec sr)
        {
            float NDotD = (float)-SampleNormal.DotProduct(Wi);
            float d2 = (float)Math.Pow((SamplePoint - sr.HitPoint).Length,2);
            return NDotD / d2;
        }

        public float PDF(ref ShadeRec sr)
        {
            return GeoObj.PDF(ref sr);
        }

        public GeometryObject GeoObj { get; set; }
        public Vector3D SamplePoint { get; set; }
        private Vector3D SampleNormal { get; set; }
        public Vector3D Wi { get; set; }
    }
}
