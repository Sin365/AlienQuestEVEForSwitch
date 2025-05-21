namespace Poly2Tri
{
	public static class P2T
	{
		private static global::Poly2Tri.TriangulationAlgorithm _defaultAlgorithm;

		public static void Triangulate(global::Poly2Tri.PolygonSet ps)
		{
			foreach (global::Poly2Tri.Polygon polygon in ps.Polygons)
			{
				Triangulate(polygon);
			}
		}

		public static void Triangulate(global::Poly2Tri.Polygon p)
		{
			Triangulate(_defaultAlgorithm, p);
		}

		public static void Triangulate(global::Poly2Tri.ConstrainedPointSet cps)
		{
			Triangulate(_defaultAlgorithm, cps);
		}

		public static void Triangulate(global::Poly2Tri.PointSet ps)
		{
			Triangulate(_defaultAlgorithm, ps);
		}

		public static global::Poly2Tri.TriangulationContext CreateContext(global::Poly2Tri.TriangulationAlgorithm algorithm)
		{
			if (algorithm != global::Poly2Tri.TriangulationAlgorithm.DTSweep)
			{
			}
			return new global::Poly2Tri.DTSweepContext();
		}

		public static void Triangulate(global::Poly2Tri.TriangulationAlgorithm algorithm, global::Poly2Tri.ITriangulatable t)
		{
			global::System.Console.WriteLine("Triangulating " + t.FileName);
			global::Poly2Tri.TriangulationContext triangulationContext = CreateContext(algorithm);
			triangulationContext.PrepareTriangulation(t);
			Triangulate(triangulationContext);
		}

		public static void Triangulate(global::Poly2Tri.TriangulationContext tcx)
		{
			if (tcx.Algorithm != global::Poly2Tri.TriangulationAlgorithm.DTSweep)
			{
			}
			global::Poly2Tri.DTSweep.Triangulate((global::Poly2Tri.DTSweepContext)tcx);
		}

		public static void Warmup()
		{
		}
	}
}
