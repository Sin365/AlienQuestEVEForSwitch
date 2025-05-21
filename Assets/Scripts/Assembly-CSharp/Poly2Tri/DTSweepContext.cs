namespace Poly2Tri
{
	public class DTSweepContext : global::Poly2Tri.TriangulationContext
	{
		private readonly float ALPHA = 0.3f;

		public global::Poly2Tri.AdvancingFront Front;

		public global::Poly2Tri.DTSweepBasin Basin = new global::Poly2Tri.DTSweepBasin();

		public global::Poly2Tri.DTSweepEdgeEvent EdgeEvent = new global::Poly2Tri.DTSweepEdgeEvent();

		private global::Poly2Tri.DTSweepPointComparator _comparator = new global::Poly2Tri.DTSweepPointComparator();

		public global::Poly2Tri.TriangulationPoint Head { get; set; }

		public global::Poly2Tri.TriangulationPoint Tail { get; set; }

		public override global::Poly2Tri.TriangulationAlgorithm Algorithm
		{
			get
			{
				return global::Poly2Tri.TriangulationAlgorithm.DTSweep;
			}
		}

		public override bool IsDebugEnabled
		{
			get
			{
				return base.IsDebugEnabled;
			}
			protected set
			{
				if (value && base.DebugContext == null)
				{
					base.DebugContext = new global::Poly2Tri.DTSweepDebugContext(this);
				}
				base.IsDebugEnabled = value;
			}
		}

		public DTSweepContext()
		{
			Clear();
		}

		public void RemoveFromList(global::Poly2Tri.DelaunayTriangle triangle)
		{
			Triangles.Remove(triangle);
		}

		public void MeshClean(global::Poly2Tri.DelaunayTriangle triangle)
		{
			MeshCleanReq(triangle);
		}

		private void MeshCleanReq(global::Poly2Tri.DelaunayTriangle triangle)
		{
			if (triangle == null || triangle.IsInterior)
			{
				return;
			}
			triangle.IsInterior = true;
			base.Triangulatable.AddTriangle(triangle);
			for (int i = 0; i < 3; i++)
			{
				if (!triangle.EdgeIsConstrained[i])
				{
					MeshCleanReq(triangle.Neighbors[i]);
				}
			}
		}

		public override void Clear()
		{
			base.Clear();
			Triangles.Clear();
		}

		public void AddNode(global::Poly2Tri.AdvancingFrontNode node)
		{
			Front.AddNode(node);
		}

		public void RemoveNode(global::Poly2Tri.AdvancingFrontNode node)
		{
			Front.RemoveNode(node);
		}

		public global::Poly2Tri.AdvancingFrontNode LocateNode(global::Poly2Tri.TriangulationPoint point)
		{
			return Front.LocateNode(point);
		}

		public void CreateAdvancingFront()
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = new global::Poly2Tri.DelaunayTriangle(Points[0], Tail, Head);
			Triangles.Add(delaunayTriangle);
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode = new global::Poly2Tri.AdvancingFrontNode(delaunayTriangle.Points[1]);
			advancingFrontNode.Triangle = delaunayTriangle;
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode2 = new global::Poly2Tri.AdvancingFrontNode(delaunayTriangle.Points[0]);
			advancingFrontNode2.Triangle = delaunayTriangle;
			global::Poly2Tri.AdvancingFrontNode tail = new global::Poly2Tri.AdvancingFrontNode(delaunayTriangle.Points[2]);
			Front = new global::Poly2Tri.AdvancingFront(advancingFrontNode, tail);
			Front.AddNode(advancingFrontNode2);
			Front.Head.Next = advancingFrontNode2;
			advancingFrontNode2.Next = Front.Tail;
			advancingFrontNode2.Prev = Front.Head;
			Front.Tail.Prev = advancingFrontNode2;
		}

		public void MapTriangleToNodes(global::Poly2Tri.DelaunayTriangle t)
		{
			for (int i = 0; i < 3; i++)
			{
				if (t.Neighbors[i] == null)
				{
					global::Poly2Tri.AdvancingFrontNode advancingFrontNode = Front.LocatePoint(t.PointCWFrom(t.Points[i]));
					if (advancingFrontNode != null)
					{
						advancingFrontNode.Triangle = t;
					}
				}
			}
		}

		public override void PrepareTriangulation(global::Poly2Tri.ITriangulatable t)
		{
			base.PrepareTriangulation(t);
			double x;
			double num = (x = Points[0].X);
			double y;
			double num2 = (y = Points[0].Y);
			foreach (global::Poly2Tri.TriangulationPoint point in Points)
			{
				if (point.X > num)
				{
					num = point.X;
				}
				if (point.X < x)
				{
					x = point.X;
				}
				if (point.Y > num2)
				{
					num2 = point.Y;
				}
				if (point.Y < y)
				{
					y = point.Y;
				}
			}
			double num3 = (double)ALPHA * (num - x);
			double num4 = (double)ALPHA * (num2 - y);
			global::Poly2Tri.TriangulationPoint head = new global::Poly2Tri.TriangulationPoint(num + num3, y - num4);
			global::Poly2Tri.TriangulationPoint tail = new global::Poly2Tri.TriangulationPoint(x - num3, y - num4);
			Head = head;
			Tail = tail;
			Points.Sort(_comparator);
		}

		public void FinalizeTriangulation()
		{
			base.Triangulatable.AddTriangles(Triangles);
			Triangles.Clear();
		}

		public override global::Poly2Tri.TriangulationConstraint NewConstraint(global::Poly2Tri.TriangulationPoint a, global::Poly2Tri.TriangulationPoint b)
		{
			return new global::Poly2Tri.DTSweepConstraint(a, b);
		}
	}
}
