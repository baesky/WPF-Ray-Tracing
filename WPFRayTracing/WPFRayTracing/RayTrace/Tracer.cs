using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    using ColorRGB = Vector3D;

    public class Tracer
    {
        public Tracer() { WorldInstance = null; }
        public Tracer(World world)
        {
            WorldInstance = world;
        }
        ~Tracer() { }
        public virtual ColorRGB TraceRay(ref Ray TestRay)
        {
            return new ColorRGB(0, 0, 0);
        }
        public virtual ColorRGB TraceRay(ref Ray TestRay, int Depth)
        {
            return new ColorRGB(0, 0, 0);
        }

        public World WorldInstance { get; set; }


    }
}
