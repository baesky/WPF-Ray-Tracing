using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class ViewPlane
    {
        public int HRes { get; set; }
        public int VRes { get; set; }
        public int NumOfSample { get; set; }
        public double PixelSize { get; set; }
        public double Gamma { get; set; }
        public double Inv_Gamma { get { return 1.0 / Gamma; } }
        public bool ShowOutOfGamut { get; set; }

        public Sampler SamplerRef { get; set; }
        public int MaxDepth { get; set; }
        public static WriteableBitmap BackBuffer;

        public ViewPlane(int hres, int vres)
        {
            HRes = hres;
            VRes = vres;
            NumOfSample = 1;
            BackBuffer = new WriteableBitmap(HRes, VRes, 96, 96, PixelFormats.Bgra32, null);
            SamplerRef = new MultiJittered(16);
            NumOfSample = SamplerRef.NumSamples; 
            SamplerRef.Generate_Samples();
            MaxDepth = 2;
        }

        public static void SetPixel(int X, int Y, Vector3D Color)
        {

            BackBuffer.Lock();

            unsafe
            {
                int pBackBuffer = (int)BackBuffer.BackBuffer;
                pBackBuffer += Y * BackBuffer.BackBufferStride;
                pBackBuffer += X * 4;

                int ColorData = 255 << 24;
                ColorData |= (int)(Color.X * 255.0) << 16;
                ColorData |= (int)(Color.Y * 255.0) << 8;
                ColorData |= (int)(Color.Z * 255.0);

                * ((int*)pBackBuffer) = ColorData;
            }

            BackBuffer.AddDirtyRect(new Int32Rect(X, Y, 1, 1));
            BackBuffer.Unlock();

        }
    }
}
