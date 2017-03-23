using System;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class WhittedTracer : Tracer
    {
        public WhittedTracer(World TheWorld) : base(TheWorld)
        {

        }

        public override Vector3D TraceRay(ref Ray TestRay, int depth)
        {
            if (depth > WorldInstance.VP.MaxDepth)
            {
                return PreDefColor.BlackColor;
            }
            else
            {
                ShadeRec SR = WorldInstance.HitBareBoneObjects(ref TestRay);
                if (SR.HitAnObject)
                {
                    SR.Depth = depth;
                    SR.Ray = TestRay;
                    return SR.Material.AreaLightShade(ref SR);
                }
                else
                {
                    return WorldInstance.BackgroundColor;
                }
            }

        }
    }
}
