using System;
using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class Directional : Light
    {
        public Vector3D GetDirection(ref ShadeRec sr)
        {
            return Dir;
        }

        public Vector3D L(ref ShadeRec sr)
        {
            return Color.ScaleBy(ls);
        }

        public float ls { get; set; }
        public Vector3D Color { get; set; }
        public Vector3D Dir { get; set; }
        public bool bCastShadow {get;set;}
        public bool EnableCastShadow() { return bCastShadow; }
        public bool CheckInShadow(ref Ray ShadowRay, ref ShadeRec SR)
        {
            float t = 0.0f;
            int NumObjects = SR.World.Objects.Count;

            for (int j = 0; j < NumObjects; j++)
            {
                if (SR.World.Objects[j].ShadowHit(ref ShadowRay, ref t))
                    return true;
            }

            return false;
        }
    }
}
