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
            VP = new ViewPlane(768, 768);
            Lights = new List<Light>(5);
        }
        public void Build()
        {

            VP.PixelSize = 1.0;
            VP.Gamma = 1.0;
            BackgroundColor = new Vector3D(0, 0, 0);

            Ambient AmbLt = new Ambient();
            AmbLt.ls = 1.0f;
            AmbLt.Color = new Vector3D(1.0,1.0,1.0);
            AmbientLight = AmbLt;
            
            Directional DirLt = new Directional();
            DirLt.Dir = new Vector3D(100, 100, 200);
            DirLt.ls = 3.0f;
            DirLt.Color = new Vector3D(1.0, 1.0, 1.0);
            Lights.Add(DirLt);

            RayTracer = new MultiObjects(this);

            Objects = new List<GeometryObject>();

            GeometryObject TestSphere1 = new Sphere(new Vector3D(0,-25,0), 80.0);
            TestSphere1.Color = new Vector3D(1, 0, 0);
            Matte SphereMat = new Matte();
            SphereMat.AmbientBRDF.Kd = 0.25f;
            SphereMat.DiffuseBRDF.Kd = 0.75f;
            SphereMat.DiffuseBRDF.Cd = TestSphere1.Color;
            TestSphere1.Material = SphereMat;
            AddRenderObjects(ref TestSphere1);

            GeometryObject TestSphere2 = new Sphere(new Vector3D(0, 30, 0), 60.0);
            TestSphere2.Color = new Vector3D(1, 1, 0);
            Matte SphereMat2 = new Matte();
            SphereMat2.AmbientBRDF.Kd = 0.25f;
            SphereMat2.DiffuseBRDF.Kd = 0.75f;
            SphereMat2.DiffuseBRDF.Cd = TestSphere2.Color;
            TestSphere2.Material = SphereMat2;
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

        public ShadeRec HitBareBoneObjects(ref Ray TestRay)
        {
            ShadeRec SR = new ShadeRec(this);
            double t = 0.0;
            double tmin = double.MaxValue;
            Vector3D Normal = new Vector3D(1.0,1.0,1.0);
            Vector3D LocalHitPoint = new Vector3D();

            foreach(GeometryObject Obj in Objects)
            {
                if(Obj.Hit(TestRay,ref t,ref SR) && t < tmin)
                {
                    SR.HitPoint = TestRay.Origin + t * TestRay.Direction;
                    SR.HitAnObject = true;
                    SR.Material = Obj.Material;
                    LocalHitPoint = SR.LocalHitPoint;
                    Normal = SR.Normal.Normalize().ToVector3D();
                    tmin = t;
                    
                }
            }

            if(SR.HitAnObject)
            {
                SR.RayParam = tmin;
                SR.Normal = Normal;
                SR.LocalHitPoint = LocalHitPoint;
            }

            return SR;
        }
    }
}
