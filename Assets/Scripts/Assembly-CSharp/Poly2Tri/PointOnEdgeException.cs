namespace Poly2Tri
{
	public class PointOnEdgeException : global::System.NotImplementedException
	{
		public readonly global::Poly2Tri.TriangulationPoint A;

		public readonly global::Poly2Tri.TriangulationPoint B;

		public readonly global::Poly2Tri.TriangulationPoint C;

		public PointOnEdgeException(string message, global::Poly2Tri.TriangulationPoint a, global::Poly2Tri.TriangulationPoint b, global::Poly2Tri.TriangulationPoint c)
			: base(message)
		{
			A = a;
			B = b;
			C = c;
		}
	}
}
