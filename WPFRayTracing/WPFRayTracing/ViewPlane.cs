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

        public Image OutputImage;
        WriteableBitmap BackBuffer;

        public ViewPlane(int hres, int vres)
        {
            HRes = hres;
            VRes = vres;
            OutputImage = new Image();
            BackBuffer = new WriteableBitmap(HRes, VRes, 96, 96, PixelFormats.Bgra32, null);
            OutputImage.Source = BackBuffer;
        }

        public void SetPixel(int X, int Y, Vector3D Color)
        {
            BackBuffer.Lock();

            unsafe
            {
                int pBackBuffer = (int)BackBuffer.BackBuffer;
                pBackBuffer += X * BackBuffer.BackBufferStride;
                pBackBuffer += Y * 4;

                int ColorData = (int)(Color.X*255) << 16;
                ColorData |= (int)(Color.Y * 255) << 8;
                ColorData |= (int)(Color.Z * 255) << 4;
                *((int*)pBackBuffer) = ColorData;
            }

            BackBuffer.AddDirtyRect(new Int32Rect(X, Y, 1, 1));
            BackBuffer.Unlock();
        }
    }
}
