using MathNet.Spatial.Euclidean;
using System.Collections.Generic;

namespace WPFRayTracing
{
    public class World
    {
        public ViewPlane            VP;
        public Vector3D             BackgroundColor;
        public Tracer               RayTracer;
        public Light                AmbientLight;
        public Camera               Cam;
        public List<GeometryObject> Objects;
        public List<Light>          Lights;
        public Sphere               TestSphere;

        public World()
        {
            VP = new ViewPlane(500, 500);
        }
        public void Build()
        {

            VP.PixelSize = 1.0;
            VP.Gamma = 1.0;
            BackgroundColor = new Vector3D(0, 0, 0.1);

            RayTracer = new SingleSphere(this);

            TestSphere = new Sphere(new Vector3D(), 85.0);

        }
        public void RenderScene()
        {
            Vector3D RGBColor;
            Ray TestRay = new Ray();
            TestRay.Direction = new Vector3D(0, 0, -1);

            for(int VPixel = 0; VPixel < VP.VRes; ++VPixel)
            {
                for(int HPixel = 0; HPixel < VP.HRes; ++HPixel)
                {
                    TestRay.Origin = new Vector3D(VP.PixelSize * (HPixel - VP.HRes / 2.0 + 0.5)
                                              , VP.PixelSize * (VPixel - VP.VRes / 2.0 + 0.5)
                                              , 100.0);
                    RGBColor = RayTracer.TraceRay(TestRay);
                    DisplayPixel(HPixel, VPixel, ref RGBColor);
                }
            }

        }
        public void OpenWindow()
        { }
        public void DisplayPixel(int X, int Y, ref Vector3D Colors)
        {
            ViewPlane.SetPixel(X, Y, Colors);
        }
    }
}
