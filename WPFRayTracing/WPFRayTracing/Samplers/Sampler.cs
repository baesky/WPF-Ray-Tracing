using System;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;
using MathNet.Numerics;

namespace WPFRayTracing
{
    public class Sampler
    {
        public Sampler(int SamplesNum)
        {
            NumSamples = SamplesNum;
            Generate_Samples();
        }

        public virtual void Generate_Samples() { }

        public void SetupShuffledIndex() { }

        public void ShuffleSamples() { }

        public Vector2D SampleUnitSquare()
        {
            if(Count % NumSamples == 0)
            {
                Random rand = new Random();
                RandomJump = (rand.Next() % NumSets) * NumSamples;
            }

            return Samples[RandomJump + ShuffledIndices[RandomJump + Count++ % NumSamples]];
        }

        public void MapSamplesToHemisphere(float P)
        {
            int Size = Samples.Count;
            HemisphereSamples = new List<Vector3D>(NumSamples * NumSets);

            for (int j = 0; j < Size; j++)
            {
                float cos_phi = (float)Trig.Cos(2.0 * Math.PI * Samples[j].X);
                float sin_phi = (float)Trig.Sin(2.0 * Math.PI * Samples[j].X);
                float cos_theta = (float)Math.Pow((1.0 - Samples[j].Y), 1.0 / (P + 1.0));
                float sin_theta = (float)Math.Sqrt(1.0 - cos_theta * cos_theta);
                float pu = sin_theta * cos_phi;
                float pv = sin_theta * sin_phi;
                float pw = cos_theta;
                HemisphereSamples.Add(new Vector3D(pu, pv, pw));
            }
        }

        public Vector3D SampleHemisphere()
        {
            Random rand = new Random();

            if (Count % NumSamples == 0)                                   // start of a new pixel
                RandomJump = (rand.Next() % NumSets) * NumSamples;

            return (HemisphereSamples[RandomJump + ShuffledIndices[RandomJump + Count++ % NumSamples]]);
        }

        /* the number of sample points in a set*/
        protected int NumSamples;
        /* the number of sample sets*/
        protected int NumSets;
        /* sample points on a unit square*/
        protected List<Vector2D> Samples;
        /* shuffled samples array indices*/
        protected List<int> ShuffledIndices;
        /* the current number of sample points used*/
        protected int Count;
        /* random index jump*/
        protected int RandomJump;

        protected List<Vector3D> HemisphereSamples;
    }
}
