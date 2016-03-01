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
        public Sphere TestSphere;

        public World()
        {
            VP = new ViewPlane(512, 512);
        }
        public void Build()
        {

            VP.PixelSize = 1.0;
            VP.Gamma = 1.0;
            BackgroundColor = new Vector3D(0, 0, 0.3);

            RayTracer = new MultiObjects(this);

            Objects = new List<GeometryObject>();

            GeometryObject TestSphere1 = new Sphere(new Vector3D(0,-25,0), 80.0);
            TestSphere1.Color = new Vector3D(1, 0, 0);
            AddRenderObjects(ref TestSphere1);

            GeometryObject TestSphere2 = new Sphere(new Vector3D(0, 30, 0), 60.0);
            TestSphere2.Color = new Vector3D(1, 1, 0);
            AddRenderObjects(ref TestSphere2);
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
                                              , VP.PixelSize * (VP.VRes / 2.0 - VPixel  + 0.5)
                                              , 100.0);
                    RGBColor = RayTracer.TraceRay(ref TestRay);
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

        public void AddRenderObjects(ref GeometryObject GeoObj)
        {
            Objects.Add(GeoObj);
        }

        public ShaderRec HitBareBoneObjects(ref Ray TestRay)
        {
            ShaderRec SR = new ShaderRec(this);
            double t = 0.0;
            double tmin = double.MaxValue;
            foreach(GeometryObject Obj in Objects)
            {
                if(Obj.Hit(TestRay,ref t,ref SR) && t < tmin)
                {
                    SR.HitAnObject = true;
                    tmin = t;
                    SR.Color = Obj.Color;
                }
            }

            return SR;
        }
    }
}
