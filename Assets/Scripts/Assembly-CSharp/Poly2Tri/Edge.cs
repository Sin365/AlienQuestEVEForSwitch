namespace Poly2Tri
{
	public class Edge
	{
		protected global::Poly2Tri.Point2D mP;

		protected global::Poly2Tri.Point2D mQ;

		public global::Poly2Tri.Point2D EdgeStart
		{
			get
			{
				return mP;
			}
			set
			{
				mP = value;
			}
		}

		public global::Poly2Tri.Point2D EdgeEnd
		{
			get
			{
				return mQ;
			}
			set
			{
				mQ = value;
			}
		}

		public Edge()
		{
			mP = null;
			mQ = null;
		}

		public Edge(global::Poly2Tri.Point2D edgeStart, global::Poly2Tri.Point2D edgeEnd)
		{
			mP = edgeStart;
			mQ = edgeEnd;
		}
	}
}
