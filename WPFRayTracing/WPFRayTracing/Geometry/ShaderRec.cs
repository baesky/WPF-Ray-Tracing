using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class ShaderRec
    {
        public bool HitAnObject { get; set; }
        public Materials Material { get; set; }
        public Vector3D HitPoint { get; set; }
        public Vector3D LocalHitPoint { get; set; }
        public Vector3D Normal { get; set; }
        public Ray Ray { get; set; }
        public int Depth { get; set; }
        public float RayParam { get; set; }
        public World World { get; set; }
        public Vector3D Color { get; set; }
    }
}
