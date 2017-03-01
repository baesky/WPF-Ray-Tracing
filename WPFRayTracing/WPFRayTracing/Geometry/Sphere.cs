using MathNet.Spatial.Euclidean;
using System;

namespace WPFRayTracing
{
    public class Sphere : GeometryObject
    {
        public Sphere()
        {
            Center = new Vector3D ( 0, 0, 0);
            Radius = 1.0;
        }

        public Sphere(Vector3D Center, double Radius)
        {
            this.Center = Center;
            this.Radius = Radius;
        }

        Vector3D Center { get; set; }
        double Radius { get; set; }
        static double Epsilon = 0.01;

        public override bool Hit(Ray TheRay, ref double Param, ref ShadeRec SR)
        {
            Vector3D Temp = TheRay.Origin - Center;
            double a = TheRay.Direction.DotProduct(TheRay.Direction);
            double b = 2.0 * Temp.DotProduct(TheRay.Direction);
            double c = Temp.DotProduct(Temp) - Radius * Radius;
            double disc = b * b - 4.0 * a * c;

            if (disc < 0.0)
                return false;
            else
            {
                double e = Math.Sqrt(disc);
                double denom = 2.0 * a;

                double t = (-b - e) / denom;

                if(t > Epsilon)
                {
                    Param = t;
                    SR.Normal = (Temp + t * TheRay.Direction) / Radius;
                    SR.LocalHitPoint = TheRay.Origin + t * TheRay.Direction;
                    return true;
                }

                t = (-b + e) / denom;

                if(t > Epsilon)
                {
                    Param = t;
                    SR.Normal = (Temp + t * TheRay.Direction) / Radius;
                    SR.LocalHitPoint = TheRay.Origin + t * TheRay.Direction;
                    return true;
                }

                return false;
            }
        }

        public override bool ShadowHit(ref Ray RayDir, ref float HitPos)
        {
            Vector3D Temp = RayDir.Origin - Center;
            double a = RayDir.Direction.DotProduct(RayDir.Direction);
            double b = 2.0 * Temp.DotProduct(RayDir.Direction);
            double c = Temp.DotProduct(Temp) - Radius * Radius;
            double disc = b * b - 4.0 * a * c;

            if (disc < 0.0)
                return false;
            else
            {
                double e = Math.Sqrt(disc);
                double denom = 2.0 * a;

                double t = (-b - e) / denom;

                if (t > Epsilon)
                {
                    HitPos = (float)t;
                    return true;
                }

                t = (-b + e) / denom;

                if (t > Epsilon)
                {
                    HitPos = (float)t;
                    return true;
                }

                return false;
            }
        }
    }
}
