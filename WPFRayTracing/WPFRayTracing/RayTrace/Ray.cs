using MathNet.Spatial.Euclidean;

namespace WPFRayTracing.RayTrace
{
    class Ray
    {
        public Point3D Origin { get; set; }

        public Vector3D Direction { get; set; };
             
        public Ray()
        {
            Origin = new Point3D(0,0,0);
            Direction = new Vector3D(0,0,0);
        }

        public Ray(ref Point3D o, ref Vector3D d)
        {
            Origin = o;
            Direction = d;
        }

        public Ray(Ray r)
        {
            Origin = r.Origin;
            Direction = r.Direction;
        }

        ~Ray()
        {

        }
        
            
    }
}
