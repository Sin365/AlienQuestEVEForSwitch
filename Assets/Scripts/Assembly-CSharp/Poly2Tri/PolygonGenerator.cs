namespace Poly2Tri
{
	public class PolygonGenerator
	{
		private static readonly global::System.Random RNG = new global::System.Random();

		private static double PI_2 = global::System.Math.PI * 2.0;

		public static global::Poly2Tri.Polygon RandomCircleSweep(double scale, int vertexCount)
		{
			double num = scale / 4.0;
			global::Poly2Tri.PolygonPoint[] array = new global::Poly2Tri.PolygonPoint[vertexCount];
			for (int i = 0; i < vertexCount; i++)
			{
				do
				{
					num = ((i % 250 == 0) ? (num + scale / 2.0 * (0.5 - RNG.NextDouble())) : ((i % 50 != 0) ? (num + 25.0 * scale / (double)vertexCount * (0.5 - RNG.NextDouble())) : (num + scale / 5.0 * (0.5 - RNG.NextDouble()))));
					num = ((!(num > scale / 2.0)) ? num : (scale / 2.0));
					num = ((!(num < scale / 10.0)) ? num : (scale / 10.0));
				}
				while (num < scale / 10.0 || num > scale / 2.0);
				global::Poly2Tri.PolygonPoint polygonPoint = new global::Poly2Tri.PolygonPoint(num * global::System.Math.Cos(PI_2 * (double)i / (double)vertexCount), num * global::System.Math.Sin(PI_2 * (double)i / (double)vertexCount));
				array[i] = polygonPoint;
			}
			return new global::Poly2Tri.Polygon(array);
		}

		public static global::Poly2Tri.Polygon RandomCircleSweep2(double scale, int vertexCount)
		{
			double num = scale / 4.0;
			global::Poly2Tri.PolygonPoint[] array = new global::Poly2Tri.PolygonPoint[vertexCount];
			for (int i = 0; i < vertexCount; i++)
			{
				do
				{
					num += scale / 5.0 * (0.5 - RNG.NextDouble());
					num = ((!(num > scale / 2.0)) ? num : (scale / 2.0));
					num = ((!(num < scale / 10.0)) ? num : (scale / 10.0));
				}
				while (num < scale / 10.0 || num > scale / 2.0);
				global::Poly2Tri.PolygonPoint polygonPoint = new global::Poly2Tri.PolygonPoint(num * global::System.Math.Cos(PI_2 * (double)i / (double)vertexCount), num * global::System.Math.Sin(PI_2 * (double)i / (double)vertexCount));
				array[i] = polygonPoint;
			}
			return new global::Poly2Tri.Polygon(array);
		}
	}
}
