using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    class Plane : GeometryObject
    {
        public Plane()
        {
            PassthoughPoint = new Vector3D(0, 0, 0);
            Normal = new Vector3D(0, 1, 0);
        }

        public Plane(Vector3D PassPoint, Vector3D Normal)
        {
            PassthoughPoint = PassPoint;
            this.Normal = Normal;
        }

        public Vector3D PassthoughPoint { get; set; }
        public Vector3D Normal { get; set; }
        static readonly double Epsilon = 0.001;
        public override bool Hit(Ray TheRay, ref double Param, ref ShadeRec SR) 
        {
            double t = (PassthoughPoint - TheRay.Origin).DotProduct(Normal) / TheRay.Direction.DotProduct(Normal);

            if (t > Epsilon)
            {
                Param = t;
                SR.Normal = Normal;
                SR.LocalHitPoint = TheRay.Origin + t * TheRay.Direction;
                return true;
            }
            else
                return false;
        }

        public override bool ShadowHit(ref Ray RayDir, ref float HitPos)
        {
            double t = (PassthoughPoint - RayDir.Origin).DotProduct(Normal) / RayDir.Direction.DotProduct(Normal);

            if (t > Epsilon)
            {
                HitPos = (float)t;
                return true;
            }
            else
                return false;
        
        }
    }
}
