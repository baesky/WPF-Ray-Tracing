using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class PathTracer : Tracer
    {
        public PathTracer(World world)
        {
            WorldInstance = world;
        }

        public override Vector3D TraceRay(ref Ray TestRay, int Depth)
        {
            if (Depth > WorldInstance.VP.MaxDepth)
            {
                return PreDefColor.BlackColor;
            }
            else
            {
                ShadeRec SR = WorldInstance.HitBareBoneObjects(ref TestRay);

                if (SR.HitAnObject)
                {
                    SR.Depth = Depth;
                    SR.Ray = TestRay;

                    return (SR.Material.PathShading(ref SR));
                }
                else
                {
                    return (WorldInstance.BackgroundColor);
                }
                    
            }
        }

    }
}
