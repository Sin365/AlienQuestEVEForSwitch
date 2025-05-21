namespace Poly2Tri
{
	public class PolygonPoint : global::Poly2Tri.TriangulationPoint
	{
		public global::Poly2Tri.PolygonPoint Next { get; set; }

		public global::Poly2Tri.PolygonPoint Previous { get; set; }

		public PolygonPoint(double x, double y)
			: base(x, y)
		{
		}

		public static global::Poly2Tri.Point2D ToBasePoint(global::Poly2Tri.PolygonPoint p)
		{
			return p;
		}

		public static global::Poly2Tri.TriangulationPoint ToTriangulationPoint(global::Poly2Tri.PolygonPoint p)
		{
			return p;
		}
	}
}
