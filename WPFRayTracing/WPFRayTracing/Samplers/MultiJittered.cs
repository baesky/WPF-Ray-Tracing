using System;
using System.Collections.Generic;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class MultiJittered : Sampler
    {
        public MultiJittered(int NumSamples) : base(NumSamples) {}

        public override void Generate_Samples()
        {
            Random rand = new Random();
            // num_samples needs to be a perfect square

            int n = (int)Math.Sqrt(NumSamples);
            float subcell_width = 1.0f / ((float)NumSamples);

            // fill the samples array with dummy points to allow us to use the [ ] notation when we set the 
            // initial patterns

           // Vector2D fill_point = new Vector2D();
            for (int j = 0; j < NumSamples * NumSets; j++)
                Samples.Add(new Vector2D(rand.NextDouble(), rand.NextDouble()));

            // distribute points in the initial patterns

            for (int p = 0; p < NumSets; p++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Samples[i * n + j + p * NumSamples] = new Vector2D((i * n + j) * subcell_width + rand.NextDouble() * subcell_width,
                           (j * n + i) * subcell_width + rand.NextDouble() * subcell_width);
                    }
                }
                    
            }
              

            // shuffle x coordinates

            for (int p = 0; p < NumSets; p++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int k = rand.Next(j, n - 1);
                        double t = Samples[i * n + j + p * NumSamples].X;
                        Samples[i * n + j + p * NumSamples] = new Vector2D(Samples[i * n + k + p * NumSamples].X, Samples[i * n + j + p * NumSamples].Y);
                        Samples[i * n + k + p * NumSamples] = new Vector2D(t, Samples[i * n + k + p * NumSamples].Y);
                    }
                }
                    
            }
                

            // shuffle y coordinates

            for (int p = 0; p < NumSets; p++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int k = rand.Next(j, n - 1);
                        double t = Samples[j * n + i + p * NumSamples].Y;
                        Samples[j * n + i + p * NumSamples] = new Vector2D(Samples[j * n + i + p * NumSamples].X, Samples[k * n + i + p * NumSamples].Y);
                        Samples[k * n + i + p * NumSamples] = new Vector2D(Samples[k * n + i + p * NumSamples].X, t);
                    }
                } 
            }
                
        }
    }
}
