using MathNet.Numerics.LinearAlgebra;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;

namespace WPFRayTracing
{
    public class TestData
    {
        public static readonly int TestVPSampleCount = 64;
        public static readonly int TestSampleCount = 64;
    }
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

        ShadeRec SR = null;
        
        public World()
        {
            VP = new ViewPlane(768, 768);
            Lights = new List<Light>();
        }
        public void Build()
        {

            VP.PixelSize = 1.0;
            VP.Gamma = 1.0;
            BackgroundColor = new Vector3D(0, 0, 0);

            RayTracer = new WhittedTracer(this);//new AreaLighting(this);//new MultiObjects(this);

            Sampler AmbientSampler = new MultiJittered(TestData.TestSampleCount);
            Ambient AmbLt = new Ambient();
            AmbLt.ls = 0.15f;
            AmbLt.MinAmount = 0.0f;
            AmbLt.Color = new Vector3D(1,1,1);
            AmbLt.SetSampler(ref AmbientSampler);
            AmbientLight = AmbLt;

            Directional DirLt = new Directional();
            DirLt.bCastShadow = true;
            DirLt.Dir = new Vector3D(90, 100, 0.0);
            DirLt.ls = 3.0f;
            DirLt.Color = new Vector3D(1.0, 1.0, 1.0);
            Lights.Add(DirLt);

            Objects = new List<GeometryObject>();

            GeometryObject TestSphere1 = new Sphere(new Vector3D(-60, 100, -230), 90.0);
            TestSphere1.Color = new Vector3D(1, 0, 0);
            //Matte SphereMat = new Matte();
            //SphereMat.AmbientBRDF.Kd = 0.8f;
            //SphereMat.AmbientBRDF.Cd = new Vector3D(1.00, 1.00, 1.00);
            //SphereMat.DiffuseBRDF.Kd = 0.75f;
            //SphereMat.DiffuseBRDF.Cd = TestSphere1.Color;
            //SphereMat.SpecularBRDF.Ks = 0.1f;
            //SphereMat.SpecularBRDF.Exp = 0.9f;
            //TestSphere1.Material = SphereMat;
            GlossyReflective RMat = new GlossyReflective();
            RMat.AmbientBRDF.Kd = 0.0f;
            RMat.DiffuseBRDF.Kd = 0.0f;
            RMat.DiffuseBRDF.Cd = PreDefColor.WhiteColor;
            //RMat.SpecularBRDF.Ks = 0.0f;
            //.SpecularBRDF.Exp = 1000;
            RMat.GlossySpecularBRDF.Ks = 0.9f;
            RMat.GlossySpecularBRDF.Cs = PreDefColor.WhiteColor;
            RMat.GlossySpecularBRDF.Exp = 1;
            Sampler GlossySampler = new MultiJittered(TestData.TestSampleCount);
            RMat.GlossySpecularBRDF.SetSampler(ref GlossySampler);
            GlossySampler.MapSamplesToHemisphere(RMat.GlossySpecularBRDF.Exp);
            TestSphere1.Material = RMat;
            AddRenderObjects(ref TestSphere1);

            GeometryObject TestSphere2 = new Sphere(new Vector3D(75, 15, -130), 60.0);
            TestSphere2.Color = new Vector3D(1, 1, 0);
            Matte SphereMat2 = new Matte();
            SphereMat2.AmbientBRDF.Kd = 1.0f;
            SphereMat2.AmbientBRDF.Cd = PreDefColor.WhiteColor;
            SphereMat2.DiffuseBRDF.Kd = 0.75f;
            SphereMat2.DiffuseBRDF.Cd = TestSphere2.Color;
            SphereMat2.SpecularBRDF.Ks = 0.25f;
            SphereMat2.SpecularBRDF.Exp = 2.0f;
            TestSphere2.Material = SphereMat2;
            AddRenderObjects(ref TestSphere2);

            GeometryObject TestPlane1 = new Plane(new Vector3D(0, 0, -200), new Vector3D(0.0, 1, 0.5));
            TestPlane1.Color = new Vector3D(1, 1, 1);
            Matte PlaneMat1 = new Matte();
            PlaneMat1.AmbientBRDF.Kd = 5.0f;
            PlaneMat1.AmbientBRDF.Cd = PreDefColor.WhiteColor;
            PlaneMat1.DiffuseBRDF.Kd = 0.8f;
            PlaneMat1.DiffuseBRDF.Cd = TestPlane1.Color;
            PlaneMat1.SpecularBRDF.Ks = 0.25f;
            PlaneMat1.SpecularBRDF.Exp = 2.0f;
            TestPlane1.Material = PlaneMat1;
            AddRenderObjects(ref TestPlane1);

            //emissive light
            //Emissive ELight = new Emissive();
            //ELight.ls = 40;
            //ELight.ce = PreDefColor.WhiteColor;

            //GeometryObject RectLight = new RectLight(new Vector3D(100, 0, 250), new Vector3D(10,10,10), new Vector3D(15,15,15), new Vector3D(0, 1, 0));
            //RectLight.Material = ELight;
            //RectLight.SamplerRef = AmbientSampler;
            //AddRenderObjects(ref RectLight);
            //AreaLight AreaLt = new AreaLight();
            //AreaLt.GeoObj = RectLight;
            //Lights.Add(AreaLt);

            //Emissive EnvLight = new Emissive();
            //EnvLight.ls = 1.5f;
            //EnvLight.ce = new Vector3D(1.0, 1.0,0.5);
            //GeometryObject SphereEnv = new Sphere(PreDefColor.BlackColor, 10000);
            //SphereEnv.Material = EnvLight;
            //((Sphere)SphereEnv).bInside = true;
            //AddRenderObjects(ref SphereEnv);

            //EnviromentLight EvnLt = new EnviromentLight();
            //EvnLt.Mat = EnvLight;
            //EvnLt.SamplerRef = AmbientSampler;
            //Lights.Add(EvnLt);
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
                    RGBColor = new Vector3D();

                    for(int SampleIdx = 0; SampleIdx < VP.NumOfSample; ++SampleIdx)
                    {
                        Vector2D SamplePoint = VP.SamplerRef.SampleUnitSquare();
                        TestRay.Origin = new Vector3D(VP.PixelSize * (HPixel - VP.HRes / 2.0 + SamplePoint.X)
                                             , VP.PixelSize * (VP.VRes / 2.0 - VPixel + SamplePoint.Y)
                                             , 100.0);
                        RGBColor += RayTracer.TraceRay(ref TestRay, 0);
                    }

                    RGBColor /= VP.NumOfSample;
                    DisplayPixel(HPixel, VPixel, ref RGBColor);
                }
            }

        }
        public void OpenWindow()
        { }
        public void DisplayPixel(int X, int Y, ref Vector3D Colors)
        {
            Vector<double> Coords = Colors.ToVector();
            if (Coords[0] > 1.0)
                Coords[0] /= Coords[0];
            if (Coords[1] > 1.0)
                Coords[1] /= Coords[1];
            if (Coords[2] > 1.0)
                Coords[2] /= Coords[2];
            Vector3D ClampedColor = Vector3D.OfVector(Coords);
            ViewPlane.SetPixel(X, Y, ref ClampedColor);
        }

        public void AddRenderObjects(ref GeometryObject GeoObj)
        {
            Objects.Add(GeoObj);
        }

        public ShadeRec HitBareBoneObjects(ref Ray TestRay)
        {
            if(SR == null)
                SR = new ShadeRec(this);
            double t = 0.0;
            double tmin = double.MaxValue;
            Vector3D Normal = new Vector3D(1.0,1.0,1.0);
            Vector3D LocalHitPoint = new Vector3D();

            foreach(GeometryObject Obj in Objects)
            {
                if(Obj.Hit(TestRay,ref t,ref SR) && (t < tmin))
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
