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
    }
}
