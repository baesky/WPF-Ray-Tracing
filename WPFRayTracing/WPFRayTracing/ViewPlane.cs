

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
    }
}
