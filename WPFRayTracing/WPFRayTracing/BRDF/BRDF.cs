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
            return new Vector3D(0, 0, 0);
        }

        public virtual Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo, ref Vector3D wi)
        {
            return new Vector3D(0, 0, 0);
        }

        public virtual Vector3D SampleF(ref ShadeRec sr, ref Vector3D wo,ref Vector3D wi, float pdf)
        {
            return new Vector3D(0, 0, 0);
        }

        public virtual Vector3D RHO(ref ShadeRec sr, ref Vector3D wo)
        {
            return new Vector3D(0, 0, 0);
        }

        protected Sampler SamplerRef;
    }
}
