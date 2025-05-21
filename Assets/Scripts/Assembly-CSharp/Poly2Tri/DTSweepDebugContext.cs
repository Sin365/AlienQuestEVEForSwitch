namespace Poly2Tri
{
	public class DTSweepDebugContext : global::Poly2Tri.TriangulationDebugContext
	{
		private global::Poly2Tri.DelaunayTriangle _primaryTriangle;

		private global::Poly2Tri.DelaunayTriangle _secondaryTriangle;

		private global::Poly2Tri.TriangulationPoint _activePoint;

		private global::Poly2Tri.AdvancingFrontNode _activeNode;

		private global::Poly2Tri.DTSweepConstraint _activeConstraint;

		public global::Poly2Tri.DelaunayTriangle PrimaryTriangle
		{
			get
			{
				return _primaryTriangle;
			}
			set
			{
				_primaryTriangle = value;
				_tcx.Update("set PrimaryTriangle");
			}
		}

		public global::Poly2Tri.DelaunayTriangle SecondaryTriangle
		{
			get
			{
				return _secondaryTriangle;
			}
			set
			{
				_secondaryTriangle = value;
				_tcx.Update("set SecondaryTriangle");
			}
		}

		public global::Poly2Tri.TriangulationPoint ActivePoint
		{
			get
			{
				return _activePoint;
			}
			set
			{
				_activePoint = value;
				_tcx.Update("set ActivePoint");
			}
		}

		public global::Poly2Tri.AdvancingFrontNode ActiveNode
		{
			get
			{
				return _activeNode;
			}
			set
			{
				_activeNode = value;
				_tcx.Update("set ActiveNode");
			}
		}

		public global::Poly2Tri.DTSweepConstraint ActiveConstraint
		{
			get
			{
				return _activeConstraint;
			}
			set
			{
				_activeConstraint = value;
				_tcx.Update("set ActiveConstraint");
			}
		}

		public bool IsDebugContext
		{
			get
			{
				return true;
			}
		}

		public DTSweepDebugContext(global::Poly2Tri.DTSweepContext tcx)
			: base(tcx)
		{
		}

		public override void Clear()
		{
			PrimaryTriangle = null;
			SecondaryTriangle = null;
			ActivePoint = null;
			ActiveNode = null;
			ActiveConstraint = null;
		}
	}
}
