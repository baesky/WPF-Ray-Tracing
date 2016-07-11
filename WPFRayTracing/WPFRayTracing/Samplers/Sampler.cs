using System;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;

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
    }
}
