

using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    using ColorRGB = Vector3D;
    public class SingleSphere : Tracer
    {
        public SingleSphere() : base()
        { }
        public SingleSphere(World world):base(world)
        { }
        ~SingleSphere()
        {

        }
        public override ColorRGB TraceRay(ref Ray TestRay)
        {
            ShadeRec SR = new ShadeRec(WorldInstance);
            double t = 0;

            if (WorldInstance.TestSphere.Hit(TestRay,ref t,ref SR))
                return new ColorRGB(1, 0, 0);
            else
                return new ColorRGB(0, 0, 0);
        }
    }
}
