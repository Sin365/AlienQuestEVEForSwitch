namespace Poly2Tri
{
	public abstract class TriangulationContext
	{
		public readonly global::System.Collections.Generic.List<global::Poly2Tri.DelaunayTriangle> Triangles = new global::System.Collections.Generic.List<global::Poly2Tri.DelaunayTriangle>();

		public readonly global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> Points = new global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint>(200);

		public global::Poly2Tri.TriangulationDebugContext DebugContext { get; protected set; }

		public global::Poly2Tri.TriangulationMode TriangulationMode { get; protected set; }

		public global::Poly2Tri.ITriangulatable Triangulatable { get; private set; }

		public int StepCount { get; private set; }

		public abstract global::Poly2Tri.TriangulationAlgorithm Algorithm { get; }

		public virtual bool IsDebugEnabled { get; protected set; }

		public global::Poly2Tri.DTSweepDebugContext DTDebugContext
		{
			get
			{
				return DebugContext as global::Poly2Tri.DTSweepDebugContext;
			}
		}

		public void Done()
		{
			StepCount++;
		}

		public virtual void PrepareTriangulation(global::Poly2Tri.ITriangulatable t)
		{
			Triangulatable = t;
			TriangulationMode = t.TriangulationMode;
			t.Prepare(this);
		}

		public abstract global::Poly2Tri.TriangulationConstraint NewConstraint(global::Poly2Tri.TriangulationPoint a, global::Poly2Tri.TriangulationPoint b);

		public void Update(string message)
		{
		}

		public virtual void Clear()
		{
			Points.Clear();
			if (DebugContext != null)
			{
				DebugContext.Clear();
			}
			StepCount = 0;
		}
	}
}
