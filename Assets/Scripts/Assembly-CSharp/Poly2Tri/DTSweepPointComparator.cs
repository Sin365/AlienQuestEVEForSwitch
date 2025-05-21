namespace Poly2Tri
{
	public class DTSweepPointComparator : global::System.Collections.Generic.IComparer<global::Poly2Tri.TriangulationPoint>
	{
		public int Compare(global::Poly2Tri.TriangulationPoint p1, global::Poly2Tri.TriangulationPoint p2)
		{
			if (p1.Y < p2.Y)
			{
				return -1;
			}
			if (p1.Y > p2.Y)
			{
				return 1;
			}
			if (p1.X < p2.X)
			{
				return -1;
			}
			if (p1.X > p2.X)
			{
				return 1;
			}
			return 0;
		}
	}
}
