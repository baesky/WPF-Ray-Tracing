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

        public World(){ }
        public void Build()
        { }
        public void RenderScene()
        { }
        public void OpenWindow()
        { }
        public void DisplayPixel()
        { }
    }
}
