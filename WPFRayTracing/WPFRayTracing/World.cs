using MathNet.Numerics.LinearAlgebra;
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
            DirLt.bCastShadow = true;
            DirLt.Dir = new Vector3D(150, 100, 100);
            DirLt.ls = 3.0f;
            DirLt.Color = new Vector3D(1.0, 1.0, 1.0);
            Lights.Add(DirLt);

            RayTracer = new MultiObjects(this);

            Objects = new List<GeometryObject>();

            GeometryObject TestSphere1 = new Sphere(new Vector3D(-60, -30, -30), 80.0);
            TestSphere1.Color = new Vector3D(1, 0, 0);
            Matte SphereMat = new Matte();
            SphereMat.AmbientBRDF.Kd = 1.0f;
            SphereMat.AmbientBRDF.Cd = new Vector3D(0.03, 0.03, 0.03);
            SphereMat.DiffuseBRDF.Kd = 0.75f;
            SphereMat.DiffuseBRDF.Cd = TestSphere1.Color;
            SphereMat.SpecularBRDF.Ks = 0.1f;
            SphereMat.SpecularBRDF.Exp = 0.9f;
            TestSphere1.Material = SphereMat;
            AddRenderObjects(ref TestSphere1);

            GeometryObject TestSphere2 = new Sphere(new Vector3D(75, 15, 0 ), 60.0);
            TestSphere2.Color = new Vector3D(1, 1, 0);
            Matte SphereMat2 = new Matte();
            SphereMat2.AmbientBRDF.Kd = 1.0f;
            SphereMat2.AmbientBRDF.Cd = new Vector3D(0.03, 0.03, 0.03);
            SphereMat2.DiffuseBRDF.Kd = 0.75f;
            SphereMat2.DiffuseBRDF.Cd = TestSphere2.Color;
            SphereMat2.SpecularBRDF.Ks = 0.25f;
            SphereMat2.SpecularBRDF.Exp = 2.0f;
            TestSphere2.Material = SphereMat2;
            AddRenderObjects(ref TestSphere2);

            GeometryObject TestPlane1 = new Plane(new Vector3D(0,-170,0), new Vector3D(0,0.5,1));
            TestPlane1.Color = new Vector3D(1, 1, 1);
            Matte PlaneMat1 = new Matte();
            PlaneMat1.AmbientBRDF.Kd = 1.0f;
            PlaneMat1.AmbientBRDF.Cd = new Vector3D(0.01, 0.01, 0.01);
            PlaneMat1.DiffuseBRDF.Kd = 0.75f;
            PlaneMat1.DiffuseBRDF.Cd = TestPlane1.Color;
            PlaneMat1.SpecularBRDF.Ks = 0.25f;
            PlaneMat1.SpecularBRDF.Exp = 2.0f;
            TestPlane1.Material = PlaneMat1;
            AddRenderObjects(ref TestPlane1);
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
            Vector<double> Coords = Colors.ToVector();
            if (Coords[0] > 1.0f)
                Coords[0] /= Coords[0];
            if (Coords[1] > 1.0f)
                Coords[1] /= Coords[1];
            if (Coords[2] > 1.0f)
                Coords[2] /= Coords[2];

            ViewPlane.SetPixel(X, Y, Vector3D.OfVector(Coords));
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
