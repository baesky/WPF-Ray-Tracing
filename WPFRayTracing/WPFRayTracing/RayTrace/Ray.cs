using MathNet.Spatial.Euclidean;

namespace WPFRayTracing
{
    public class Ray
    {
        public Vector3D Origin { get; set; }

        public Vector3D Direction { get; set; }
             
        public Ray()
        {
            Origin = new Vector3D(0,0,0);
            Direction = new Vector3D(0,0,0);
        }

        public Ray(ref Vector3D o, ref Vector3D d)
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
