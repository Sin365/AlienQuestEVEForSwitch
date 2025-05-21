namespace Poly2Tri
{
	public class PointGenerator
	{
		private static readonly global::System.Random RNG = new global::System.Random();

		public static global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> UniformDistribution(int n, double scale)
		{
			global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> list = new global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint>();
			for (int i = 0; i < n; i++)
			{
				list.Add(new global::Poly2Tri.TriangulationPoint(scale * (0.5 - RNG.NextDouble()), scale * (0.5 - RNG.NextDouble())));
			}
			return list;
		}

		public static global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> UniformGrid(int n, double scale)
		{
			double num = 0.0;
			double num2 = scale / (double)n;
			double num3 = 0.5 * scale;
			global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> list = new global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint>();
			for (int i = 0; i < n + 1; i++)
			{
				num = num3 - (double)i * num2;
				for (int j = 0; j < n + 1; j++)
				{
					list.Add(new global::Poly2Tri.TriangulationPoint(num, num3 - (double)j * num2));
				}
			}
			return list;
		}
	}
}
