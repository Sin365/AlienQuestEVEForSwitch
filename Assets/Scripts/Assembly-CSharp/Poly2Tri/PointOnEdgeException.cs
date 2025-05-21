using System;

namespace Poly2Tri
{
	public class PointOnEdgeException : NotImplementedException
	{
		public PointOnEdgeException(string message, TriangulationPoint a, TriangulationPoint b, TriangulationPoint c)
		{
		}

	}
}
