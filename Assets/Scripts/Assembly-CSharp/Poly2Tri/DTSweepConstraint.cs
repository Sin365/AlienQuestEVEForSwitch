namespace Poly2Tri
{
	public class DTSweepConstraint : global::Poly2Tri.TriangulationConstraint
	{
		public DTSweepConstraint(global::Poly2Tri.TriangulationPoint p1, global::Poly2Tri.TriangulationPoint p2)
			: base(p1, p2)
		{
			base.Q.AddEdge(this);
		}
	}
}
