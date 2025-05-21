namespace Poly2Tri
{
	public abstract class TriangulationDebugContext
	{
		protected global::Poly2Tri.TriangulationContext _tcx;

		public TriangulationDebugContext(global::Poly2Tri.TriangulationContext tcx)
		{
			_tcx = tcx;
		}

		public abstract void Clear();
	}
}
