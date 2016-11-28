using MathNet.Spatial.Euclidean;
using System;

namespace WPFRayTracing
{
    public interface Light
    {
        Vector3D GetDirection(ref ShadeRec sr);				
																
		Vector3D L(ref ShadeRec sr);
    }
}
