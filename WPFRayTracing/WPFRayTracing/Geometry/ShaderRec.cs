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
        public double RayParam { get; set; }
        public World World { get; set; }
        public Vector3D Color { get; set; }

        public ShaderRec(World world)
        {
            HitAnObject = false;
            Material = null;
            HitPoint = new Vector3D();
            LocalHitPoint = new Vector3D();
            Normal = new Vector3D();
            Ray = new Ray();
            Depth = 0;
            RayParam = 0.0;
            this.World = world;
        }
        public ShaderRec(ShaderRec SR)
        {
            HitAnObject = false;
            Material = null;
            HitPoint = new Vector3D();
            LocalHitPoint = new Vector3D();
            Normal = new Vector3D();
            Ray = new Ray();
            Depth = 0;
            RayParam = SR.RayParam;
            this.World = SR.World;
        }
    }
}
