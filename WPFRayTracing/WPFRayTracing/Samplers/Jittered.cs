using System;
using System.Collections.Generic;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class Jittered : Sampler
    {
        public Jittered(int NumSamples) : base(NumSamples) 
        {
            
        }

        public override void Generate_Samples()
        {
            Random rand = new Random();

            int n = (int)Math.Sqrt(NumSamples);
            for(int i = 0; i<NumSets; ++i)
            {
                for(int j = 0; j < n; ++j)
                {
                    for(int k = 0; k < n; ++k)
                    {
                        Samples.Add(new Vector2D((k + rand.NextDouble()) / n, (j + rand.NextDouble()) / n));
                    }
                }
            }
        }
    }
}
