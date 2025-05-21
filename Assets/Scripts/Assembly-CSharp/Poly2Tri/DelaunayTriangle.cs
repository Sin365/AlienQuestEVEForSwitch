namespace Poly2Tri
{
	public class DelaunayTriangle
	{
		public global::Poly2Tri.FixedArray3<global::Poly2Tri.TriangulationPoint> Points;

		public global::Poly2Tri.FixedArray3<global::Poly2Tri.DelaunayTriangle> Neighbors;

		private global::Poly2Tri.FixedBitArray3 mEdgeIsConstrained;

		public global::Poly2Tri.FixedBitArray3 EdgeIsDelaunay;

		public global::Poly2Tri.FixedBitArray3 EdgeIsConstrained
		{
			get
			{
				return mEdgeIsConstrained;
			}
		}

		public bool IsInterior { get; set; }

		public DelaunayTriangle(global::Poly2Tri.TriangulationPoint p1, global::Poly2Tri.TriangulationPoint p2, global::Poly2Tri.TriangulationPoint p3)
		{
			Points[0] = p1;
			Points[1] = p2;
			Points[2] = p3;
		}

		public int IndexOf(global::Poly2Tri.TriangulationPoint p)
		{
			int num = Points.IndexOf(p);
			if (num == -1)
			{
				throw new global::System.Exception("Calling index with a point that doesn't exist in triangle");
			}
			return num;
		}

		public int IndexCWFrom(global::Poly2Tri.TriangulationPoint p)
		{
			return (IndexOf(p) + 2) % 3;
		}

		public int IndexCCWFrom(global::Poly2Tri.TriangulationPoint p)
		{
			return (IndexOf(p) + 1) % 3;
		}

		public bool Contains(global::Poly2Tri.TriangulationPoint p)
		{
			return Points.Contains(p);
		}

		private void MarkNeighbor(global::Poly2Tri.TriangulationPoint p1, global::Poly2Tri.TriangulationPoint p2, global::Poly2Tri.DelaunayTriangle t)
		{
			int num = EdgeIndex(p1, p2);
			if (num == -1)
			{
				throw new global::System.Exception("Error marking neighbors -- t doesn't contain edge p1-p2!");
			}
			Neighbors[num] = t;
		}

		public void MarkNeighbor(global::Poly2Tri.DelaunayTriangle t)
		{
			bool flag = t.Contains(Points[0]);
			bool flag2 = t.Contains(Points[1]);
			bool flag3 = t.Contains(Points[2]);
			if (flag2 && flag3)
			{
				Neighbors[0] = t;
				t.MarkNeighbor(Points[1], Points[2], this);
			}
			else if (flag && flag3)
			{
				Neighbors[1] = t;
				t.MarkNeighbor(Points[0], Points[2], this);
			}
			else if (flag && flag2)
			{
				Neighbors[2] = t;
				t.MarkNeighbor(Points[0], Points[1], this);
			}
		}

		public bool SharedEdge(global::Poly2Tri.TriangulationPoint p1, global::Poly2Tri.TriangulationPoint p2)
		{
			bool result = false;
			foreach (global::Poly2Tri.DelaunayTriangle neighbor in Neighbors)
			{
				if (neighbor != null && neighbor.Contains(p1) && neighbor.Contains(p2))
				{
					result = true;
				}
			}
			return result;
		}

		public void ClearNeighbors()
		{
			var neighbors = Neighbors;
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = null;
			Neighbors[2] = delaunayTriangle;
			delaunayTriangle = delaunayTriangle;
			Neighbors[1] = delaunayTriangle;
			neighbors[0] = delaunayTriangle;
		}

		public void ClearNeighbor(global::Poly2Tri.DelaunayTriangle triangle)
		{
			if (Neighbors[0] == triangle)
			{
				Neighbors[0] = null;
			}
			else if (Neighbors[1] == triangle)
			{
				Neighbors[1] = null;
			}
			else if (Neighbors[2] == triangle)
			{
				Neighbors[2] = null;
			}
		}

		public void Clear()
		{
			for (int i = 0; i < 3; i++)
			{
				global::Poly2Tri.DelaunayTriangle delaunayTriangle = Neighbors[i];
				if (delaunayTriangle != null)
				{
					delaunayTriangle.ClearNeighbor(this);
				}
			}
			ClearNeighbors();
			var points = Points;
			global::Poly2Tri.TriangulationPoint triangulationPoint = null;
			Points[2] = triangulationPoint;
			triangulationPoint = triangulationPoint;
			Points[1] = triangulationPoint;
			points[0] = triangulationPoint;
		}

		public global::Poly2Tri.TriangulationPoint OppositePoint(global::Poly2Tri.DelaunayTriangle t, global::Poly2Tri.TriangulationPoint p)
		{
			return PointCWFrom(t.PointCWFrom(p));
		}

		public global::Poly2Tri.DelaunayTriangle NeighborCWFrom(global::Poly2Tri.TriangulationPoint point)
		{
			return Neighbors[(Points.IndexOf(point) + 1) % 3];
		}

		public global::Poly2Tri.DelaunayTriangle NeighborCCWFrom(global::Poly2Tri.TriangulationPoint point)
		{
			return Neighbors[(Points.IndexOf(point) + 2) % 3];
		}

		public global::Poly2Tri.DelaunayTriangle NeighborAcrossFrom(global::Poly2Tri.TriangulationPoint point)
		{
			return Neighbors[Points.IndexOf(point)];
		}

		public global::Poly2Tri.TriangulationPoint PointCCWFrom(global::Poly2Tri.TriangulationPoint point)
		{
			return Points[(IndexOf(point) + 1) % 3];
		}

		public global::Poly2Tri.TriangulationPoint PointCWFrom(global::Poly2Tri.TriangulationPoint point)
		{
			return Points[(IndexOf(point) + 2) % 3];
		}

		private void RotateCW()
		{
			global::Poly2Tri.TriangulationPoint value = Points[2];
			Points[2] = Points[1];
			Points[1] = Points[0];
			Points[0] = value;
		}

		public void Legalize(global::Poly2Tri.TriangulationPoint oPoint, global::Poly2Tri.TriangulationPoint nPoint)
		{
			RotateCW();
			Points[IndexCCWFrom(oPoint)] = nPoint;
		}

		public override string ToString()
		{
			return string.Concat(Points[0], ",", Points[1], ",", Points[2]);
		}

		public void MarkNeighborEdges()
		{
			for (int i = 0; i < 3; i++)
			{
				if (EdgeIsConstrained[i] && Neighbors[i] != null)
				{
					Neighbors[i].MarkConstrainedEdge(Points[(i + 1) % 3], Points[(i + 2) % 3]);
				}
			}
		}

		public void MarkEdge(global::Poly2Tri.DelaunayTriangle triangle)
		{
			for (int i = 0; i < 3; i++)
			{
				if (EdgeIsConstrained[i])
				{
					triangle.MarkConstrainedEdge(Points[(i + 1) % 3], Points[(i + 2) % 3]);
				}
			}
		}

		public void MarkEdge(global::System.Collections.Generic.List<global::Poly2Tri.DelaunayTriangle> tList)
		{
			foreach (global::Poly2Tri.DelaunayTriangle t in tList)
			{
				for (int i = 0; i < 3; i++)
				{
					if (t.EdgeIsConstrained[i])
					{
						MarkConstrainedEdge(t.Points[(i + 1) % 3], t.Points[(i + 2) % 3]);
					}
				}
			}
		}

		public void MarkConstrainedEdge(int index)
		{
			mEdgeIsConstrained[index] = true;
		}

		public void MarkConstrainedEdge(global::Poly2Tri.DTSweepConstraint edge)
		{
			MarkConstrainedEdge(edge.P, edge.Q);
		}

		public void MarkConstrainedEdge(global::Poly2Tri.TriangulationPoint p, global::Poly2Tri.TriangulationPoint q)
		{
			int num = EdgeIndex(p, q);
			if (num != -1)
			{
				mEdgeIsConstrained[num] = true;
			}
		}

		public double Area()
		{
			double num = Points[0].X - Points[1].X;
			double num2 = Points[2].Y - Points[1].Y;
			return global::System.Math.Abs(num * num2 * 0.5);
		}

		public global::Poly2Tri.TriangulationPoint Centroid()
		{
			double x = (Points[0].X + Points[1].X + Points[2].X) / 3.0;
			double y = (Points[0].Y + Points[1].Y + Points[2].Y) / 3.0;
			return new global::Poly2Tri.TriangulationPoint(x, y);
		}

		public int EdgeIndex(global::Poly2Tri.TriangulationPoint p1, global::Poly2Tri.TriangulationPoint p2)
		{
			int num = Points.IndexOf(p1);
			int num2 = Points.IndexOf(p2);
			bool flag = num == 0 || num2 == 0;
			bool flag2 = num == 1 || num2 == 1;
			bool flag3 = num == 2 || num2 == 2;
			if (flag2 && flag3)
			{
				return 0;
			}
			if (flag && flag3)
			{
				return 1;
			}
			if (flag && flag2)
			{
				return 2;
			}
			return -1;
		}

		public bool GetConstrainedEdgeCCW(global::Poly2Tri.TriangulationPoint p)
		{
			return EdgeIsConstrained[(IndexOf(p) + 2) % 3];
		}

		public bool GetConstrainedEdgeCW(global::Poly2Tri.TriangulationPoint p)
		{
			return EdgeIsConstrained[(IndexOf(p) + 1) % 3];
		}

		public bool GetConstrainedEdgeAcross(global::Poly2Tri.TriangulationPoint p)
		{
			return EdgeIsConstrained[IndexOf(p)];
		}

		protected void SetConstrainedEdge(int idx, bool ce)
		{
			mEdgeIsConstrained[idx] = ce;
		}

		public void SetConstrainedEdgeCCW(global::Poly2Tri.TriangulationPoint p, bool ce)
		{
			int idx = (IndexOf(p) + 2) % 3;
			SetConstrainedEdge(idx, ce);
		}

		public void SetConstrainedEdgeCW(global::Poly2Tri.TriangulationPoint p, bool ce)
		{
			int idx = (IndexOf(p) + 1) % 3;
			SetConstrainedEdge(idx, ce);
		}

		public void SetConstrainedEdgeAcross(global::Poly2Tri.TriangulationPoint p, bool ce)
		{
			int idx = IndexOf(p);
			SetConstrainedEdge(idx, ce);
		}

		public bool GetDelaunayEdgeCCW(global::Poly2Tri.TriangulationPoint p)
		{
			return EdgeIsDelaunay[(IndexOf(p) + 2) % 3];
		}

		public bool GetDelaunayEdgeCW(global::Poly2Tri.TriangulationPoint p)
		{
			return EdgeIsDelaunay[(IndexOf(p) + 1) % 3];
		}

		public bool GetDelaunayEdgeAcross(global::Poly2Tri.TriangulationPoint p)
		{
			return EdgeIsDelaunay[IndexOf(p)];
		}

		public void SetDelaunayEdgeCCW(global::Poly2Tri.TriangulationPoint p, bool ce)
		{
			EdgeIsDelaunay[(IndexOf(p) + 2) % 3] = ce;
		}

		public void SetDelaunayEdgeCW(global::Poly2Tri.TriangulationPoint p, bool ce)
		{
			EdgeIsDelaunay[(IndexOf(p) + 1) % 3] = ce;
		}

		public void SetDelaunayEdgeAcross(global::Poly2Tri.TriangulationPoint p, bool ce)
		{
			EdgeIsDelaunay[IndexOf(p)] = ce;
		}

		public bool GetEdge(int idx, out global::Poly2Tri.DTSweepConstraint edge)
		{
			edge = null;
			if (idx < 0 || idx > 2)
			{
				return false;
			}
			global::Poly2Tri.TriangulationPoint triangulationPoint = Points[(idx + 1) % 3];
			global::Poly2Tri.TriangulationPoint triangulationPoint2 = Points[(idx + 2) % 3];
			if (triangulationPoint.GetEdge(triangulationPoint2, out edge))
			{
				return true;
			}
			if (triangulationPoint2.GetEdge(triangulationPoint, out edge))
			{
				return true;
			}
			return false;
		}

		public bool GetEdgeCCW(global::Poly2Tri.TriangulationPoint p, out global::Poly2Tri.DTSweepConstraint edge)
		{
			int num = IndexOf(p);
			int idx = (num + 2) % 3;
			return GetEdge(idx, out edge);
		}

		public bool GetEdgeCW(global::Poly2Tri.TriangulationPoint p, out global::Poly2Tri.DTSweepConstraint edge)
		{
			int num = IndexOf(p);
			int idx = (num + 1) % 3;
			return GetEdge(idx, out edge);
		}

		public bool GetEdgeAcross(global::Poly2Tri.TriangulationPoint p, out global::Poly2Tri.DTSweepConstraint edge)
		{
			int num = IndexOf(p);
			int idx = num;
			return GetEdge(idx, out edge);
		}
	}
}
