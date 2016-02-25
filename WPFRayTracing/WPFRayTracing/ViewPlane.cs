using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFRayTracing
{
    public class ViewPlane
    {
        public int HRes { get; set; }
        public int VRes { get; set; }
        public int NumOfSample { get; set; }
        double PixelSize { get; set; }
        double Gamma { get; set; }
        double Inv_Gamma { get { return 1.0 / Gamma; } }
        bool ShowOutOfGamut { get; set; }

        Image OutputImage;
        WriteableBitmap BackBuffer;

        public ViewPlane(int hres, int vres)
        {
            HRes = hres;
            VRes = vres;
            OutputImage = new Image();
            BackBuffer = new WriteableBitmap(HRes, VRes, 96, 96, PixelFormats.Bgra32, null);
            OutputImage.Source = BackBuffer;
        }
    }
}
