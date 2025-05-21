namespace Poly2Tri
{
	public interface ITriangulatable
	{
		global::System.Collections.Generic.IList<global::Poly2Tri.DelaunayTriangle> Triangles { get; }

		global::Poly2Tri.TriangulationMode TriangulationMode { get; }

		string FileName { get; set; }

		bool DisplayFlipX { get; set; }

		bool DisplayFlipY { get; set; }

		float DisplayRotate { get; set; }

		double Precision { get; set; }

		double MinX { get; }

		double MaxX { get; }

		double MinY { get; }

		double MaxY { get; }

		global::Poly2Tri.Rect2D Bounds { get; }

		void Prepare(global::Poly2Tri.TriangulationContext tcx);

		void AddTriangle(global::Poly2Tri.DelaunayTriangle t);

		void AddTriangles(global::System.Collections.Generic.IEnumerable<global::Poly2Tri.DelaunayTriangle> list);

		void ClearTriangles();
	}
}
