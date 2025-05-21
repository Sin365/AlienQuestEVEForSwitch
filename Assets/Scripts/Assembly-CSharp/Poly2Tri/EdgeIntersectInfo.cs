namespace Poly2Tri
{
	public class EdgeIntersectInfo
	{
		public global::Poly2Tri.Edge EdgeOne { get; private set; }

		public global::Poly2Tri.Edge EdgeTwo { get; private set; }

		public global::Poly2Tri.Point2D IntersectionPoint { get; private set; }

		public EdgeIntersectInfo(global::Poly2Tri.Edge edgeOne, global::Poly2Tri.Edge edgeTwo, global::Poly2Tri.Point2D intersectionPoint)
		{
			EdgeOne = edgeOne;
			EdgeTwo = edgeTwo;
			IntersectionPoint = intersectionPoint;
		}
	}
}
