using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFRayTracing
{
    /// <summary>
    /// 显示RenderTarget的控件
    /// </summary>
    class PresentationControl : Canvas
    {
        protected override void OnRender(DrawingContext dc)
        {
            //BitmapImage img = new BitmapImage(new Uri("c:\\demo.jpg"));
            //dc.DrawImage(img, new Rect(0, 0, img.PixelWidth, img.PixelHeight));
        }
    }
}
