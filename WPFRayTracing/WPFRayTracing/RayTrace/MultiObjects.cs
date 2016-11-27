using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class MultiObjects : Tracer
    {
        public MultiObjects() : base()
        {

        }

        public MultiObjects(World TheWorld) : base(TheWorld)
        {

        }

        ~MultiObjects() { }

        public override Vector3D TraceRay(ref Ray TestRay)
        {
            ShadeRec SR = WorldInstance.HitBareBoneObjects(ref TestRay);

            if (SR.HitAnObject)
                return SR.Color;
            else
                return WorldInstance.BackgroundColor;
        }
    }
}
