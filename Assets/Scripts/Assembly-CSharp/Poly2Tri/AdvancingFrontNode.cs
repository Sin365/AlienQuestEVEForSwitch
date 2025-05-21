namespace Poly2Tri
{
	public class AdvancingFrontNode
	{
		public global::Poly2Tri.AdvancingFrontNode Next;

		public global::Poly2Tri.AdvancingFrontNode Prev;

		public double Value;

		public global::Poly2Tri.TriangulationPoint Point;

		public global::Poly2Tri.DelaunayTriangle Triangle;

		public bool HasNext
		{
			get
			{
				return Next != null;
			}
		}

		public bool HasPrev
		{
			get
			{
				return Prev != null;
			}
		}

		public AdvancingFrontNode(global::Poly2Tri.TriangulationPoint point)
		{
			Point = point;
			Value = point.X;
		}
	}
}
