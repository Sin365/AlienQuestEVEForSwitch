namespace Poly2Tri
{
	public class PolygonSet
	{
		protected global::System.Collections.Generic.List<global::Poly2Tri.Polygon> _polygons = new global::System.Collections.Generic.List<global::Poly2Tri.Polygon>();

		public global::System.Collections.Generic.IEnumerable<global::Poly2Tri.Polygon> Polygons
		{
			get
			{
				return _polygons;
			}
		}

		public PolygonSet()
		{
		}

		public PolygonSet(global::Poly2Tri.Polygon poly)
		{
			_polygons.Add(poly);
		}

		public void Add(global::Poly2Tri.Polygon p)
		{
			_polygons.Add(p);
		}
	}
}
