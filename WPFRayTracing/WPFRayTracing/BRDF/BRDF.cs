using System;
using System.Collections.Generic;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class BRDF
    {
        public BRDF()
        { }

        public BRDF(ref BRDF OtherBRDF)
        {
            SamplerRef = OtherBRDF.SamplerRef;
        }

        ~BRDF()
        {
            SamplerRef = null;
        }
     
        public void SetSampler(ref Sampler TheSampler)
        {
            SamplerRef = TheSampler;
            SamplerRef.MapSamplesToHemisphere(1);
        } 
        
        public virtual Vector3D Factor(ref ShadeRec sr, ref Vector3D wo, ref Vector3D wi)
        {
            return PreDefColor.BlackColor;
        }

        public virtual Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo, out Vector3D wi)
        {
            wi = PreDefColor.BlackColor;
            return PreDefColor.BlackColor;
        }

        public virtual Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo,out Vector3D wi,out double pdf)
        {
            wi = PreDefColor.BlackColor;
            pdf = 1.0;
            return PreDefColor.BlackColor;
        }

        public virtual Vector3D RHO(ref ShadeRec sr, ref Vector3D wo)
        {
            return PreDefColor.BlackColor;
        }

        protected Sampler SamplerRef;
    }
}
