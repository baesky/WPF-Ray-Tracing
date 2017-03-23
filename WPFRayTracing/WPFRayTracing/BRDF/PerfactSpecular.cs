using System;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class PerfactSpecular : BRDF
    {
        public override Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo, ref Vector3D wi,out double pdf)
        {
            float NDotWo = (float)sr.Normal.DotProduct(wo);
            wi = -wo + 2.0 * sr.Normal.ScaleBy(NDotWo);
            pdf = Math.Abs(sr.Normal * wi);
            return Cr.ScaleBy(Kr);
        }

        public override Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo, out Vector3D wi)
        {
            float NDotWo = (float)sr.Normal.DotProduct(wo);
            wi = -wo + 2.0 * sr.Normal.ScaleBy(NDotWo);
            return (Kr * Cr / Math.Abs(sr.Normal.DotProduct(wi)));
        }

        public float Kr { get; set; }
        public Vector3D Cr { get; set; }
    }
}
