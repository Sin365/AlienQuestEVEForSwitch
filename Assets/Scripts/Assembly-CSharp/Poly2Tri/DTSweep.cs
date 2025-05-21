namespace Poly2Tri
{
	public static class DTSweep
	{
		private const double PI_div2 = global::System.Math.PI / 2.0;

		private const double PI_3div4 = global::System.Math.PI * 3.0 / 4.0;

		public static void Triangulate(global::Poly2Tri.DTSweepContext tcx)
		{
			tcx.CreateAdvancingFront();
			Sweep(tcx);
			FixupConstrainedEdges(tcx);
			if (tcx.TriangulationMode == global::Poly2Tri.TriangulationMode.Polygon)
			{
				FinalizationPolygon(tcx);
			}
			else
			{
				FinalizationConvexHull(tcx);
				if (tcx.TriangulationMode == global::Poly2Tri.TriangulationMode.Constrained)
				{
					tcx.FinalizeTriangulation();
				}
				else
				{
					tcx.FinalizeTriangulation();
				}
			}
			tcx.Done();
		}

		private static void Sweep(global::Poly2Tri.DTSweepContext tcx)
		{
			global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> points = tcx.Points;
			for (int i = 1; i < points.Count; i++)
			{
				global::Poly2Tri.TriangulationPoint triangulationPoint = points[i];
				global::Poly2Tri.AdvancingFrontNode advancingFrontNode = PointEvent(tcx, triangulationPoint);
				if (advancingFrontNode != null && triangulationPoint.HasEdges)
				{
					foreach (global::Poly2Tri.DTSweepConstraint edge in triangulationPoint.Edges)
					{
						if (tcx.IsDebugEnabled)
						{
							tcx.DTDebugContext.ActiveConstraint = edge;
						}
						EdgeEvent(tcx, edge, advancingFrontNode);
					}
				}
				tcx.Update(null);
			}
		}

		private static void FixupConstrainedEdges(global::Poly2Tri.DTSweepContext tcx)
		{
			foreach (global::Poly2Tri.DelaunayTriangle triangle in tcx.Triangles)
			{
				for (int i = 0; i < 3; i++)
				{
					if (!triangle.GetConstrainedEdgeCCW(triangle.Points[i]))
					{
						global::Poly2Tri.DTSweepConstraint edge = null;
						if (triangle.GetEdgeCCW(triangle.Points[i], out edge))
						{
							triangle.MarkConstrainedEdge((i + 2) % 3);
						}
					}
				}
			}
		}

		private static void FinalizationConvexHull(global::Poly2Tri.DTSweepContext tcx)
		{
			global::Poly2Tri.AdvancingFrontNode next = tcx.Front.Head.Next;
			global::Poly2Tri.AdvancingFrontNode next2 = next.Next;
			global::Poly2Tri.TriangulationPoint point = next.Point;
			TurnAdvancingFrontConvex(tcx, next, next2);
			next = tcx.Front.Tail.Prev;
			global::Poly2Tri.DelaunayTriangle delaunayTriangle;
			if (next.Triangle.Contains(next.Next.Point) && next.Triangle.Contains(next.Prev.Point))
			{
				delaunayTriangle = next.Triangle.NeighborAcrossFrom(next.Point);
				RotateTrianglePair(next.Triangle, next.Point, delaunayTriangle, delaunayTriangle.OppositePoint(next.Triangle, next.Point));
				tcx.MapTriangleToNodes(next.Triangle);
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
			next = tcx.Front.Head.Next;
			if (next.Triangle.Contains(next.Prev.Point) && next.Triangle.Contains(next.Next.Point))
			{
				delaunayTriangle = next.Triangle.NeighborAcrossFrom(next.Point);
				RotateTrianglePair(next.Triangle, next.Point, delaunayTriangle, delaunayTriangle.OppositePoint(next.Triangle, next.Point));
				tcx.MapTriangleToNodes(next.Triangle);
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
			point = tcx.Front.Head.Point;
			next2 = tcx.Front.Tail.Prev;
			delaunayTriangle = next2.Triangle;
			global::Poly2Tri.TriangulationPoint triangulationPoint = next2.Point;
			next2.Triangle = null;
			global::Poly2Tri.DelaunayTriangle delaunayTriangle2;
			while (true)
			{
				tcx.RemoveFromList(delaunayTriangle);
				triangulationPoint = delaunayTriangle.PointCCWFrom(triangulationPoint);
				if (triangulationPoint == point)
				{
					break;
				}
				delaunayTriangle2 = delaunayTriangle.NeighborCCWFrom(triangulationPoint);
				delaunayTriangle.Clear();
				delaunayTriangle = delaunayTriangle2;
			}
			point = tcx.Front.Head.Next.Point;
			triangulationPoint = delaunayTriangle.PointCWFrom(tcx.Front.Head.Point);
			delaunayTriangle2 = delaunayTriangle.NeighborCWFrom(tcx.Front.Head.Point);
			delaunayTriangle.Clear();
			delaunayTriangle = delaunayTriangle2;
			while (triangulationPoint != point)
			{
				tcx.RemoveFromList(delaunayTriangle);
				triangulationPoint = delaunayTriangle.PointCCWFrom(triangulationPoint);
				delaunayTriangle2 = delaunayTriangle.NeighborCCWFrom(triangulationPoint);
				delaunayTriangle.Clear();
				delaunayTriangle = delaunayTriangle2;
			}
			tcx.Front.Head = tcx.Front.Head.Next;
			tcx.Front.Head.Prev = null;
			tcx.Front.Tail = tcx.Front.Tail.Prev;
			tcx.Front.Tail.Next = null;
		}

		private static void TurnAdvancingFrontConvex(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.AdvancingFrontNode b, global::Poly2Tri.AdvancingFrontNode c)
		{
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode = b;
			while (c != tcx.Front.Tail)
			{
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.ActiveNode = c;
				}
				if (global::Poly2Tri.TriangulationUtil.Orient2d(b.Point, c.Point, c.Next.Point) == global::Poly2Tri.Orientation.CCW)
				{
					Fill(tcx, c);
					c = c.Next;
				}
				else if (b != advancingFrontNode && global::Poly2Tri.TriangulationUtil.Orient2d(b.Prev.Point, b.Point, c.Point) == global::Poly2Tri.Orientation.CCW)
				{
					Fill(tcx, b);
					b = b.Prev;
				}
				else
				{
					b = c;
					c = c.Next;
				}
			}
		}

		private static void FinalizationPolygon(global::Poly2Tri.DTSweepContext tcx)
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = tcx.Front.Head.Next.Triangle;
			global::Poly2Tri.TriangulationPoint point = tcx.Front.Head.Next.Point;
			while (!delaunayTriangle.GetConstrainedEdgeCW(point))
			{
				global::Poly2Tri.DelaunayTriangle delaunayTriangle2 = delaunayTriangle.NeighborCCWFrom(point);
				if (delaunayTriangle2 == null)
				{
					break;
				}
				delaunayTriangle = delaunayTriangle2;
			}
			tcx.MeshClean(delaunayTriangle);
		}

		private static void FinalizationConstraints(global::Poly2Tri.DTSweepContext tcx)
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = tcx.Front.Head.Triangle;
			global::Poly2Tri.TriangulationPoint point = tcx.Front.Head.Point;
			while (!delaunayTriangle.GetConstrainedEdgeCW(point))
			{
				global::Poly2Tri.DelaunayTriangle delaunayTriangle2 = delaunayTriangle.NeighborCCWFrom(point);
				if (delaunayTriangle2 == null)
				{
					break;
				}
				delaunayTriangle = delaunayTriangle2;
			}
			tcx.MeshClean(delaunayTriangle);
		}

		private static global::Poly2Tri.AdvancingFrontNode PointEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.TriangulationPoint point)
		{
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode = tcx.LocateNode(point);
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = advancingFrontNode;
			}
			if (advancingFrontNode == null || point == null)
			{
				return null;
			}
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode2 = NewFrontTriangle(tcx, point, advancingFrontNode);
			if (point.X <= advancingFrontNode.Point.X + global::Poly2Tri.MathUtil.EPSILON)
			{
				Fill(tcx, advancingFrontNode);
			}
			tcx.AddNode(advancingFrontNode2);
			FillAdvancingFront(tcx, advancingFrontNode2);
			return advancingFrontNode2;
		}

		private static global::Poly2Tri.AdvancingFrontNode NewFrontTriangle(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.TriangulationPoint point, global::Poly2Tri.AdvancingFrontNode node)
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = new global::Poly2Tri.DelaunayTriangle(point, node.Point, node.Next.Point);
			delaunayTriangle.MarkNeighbor(node.Triangle);
			tcx.Triangles.Add(delaunayTriangle);
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode = new global::Poly2Tri.AdvancingFrontNode(point);
			advancingFrontNode.Next = node.Next;
			advancingFrontNode.Prev = node;
			node.Next.Prev = advancingFrontNode;
			node.Next = advancingFrontNode;
			tcx.AddNode(advancingFrontNode);
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = advancingFrontNode;
			}
			if (!Legalize(tcx, delaunayTriangle))
			{
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
			return advancingFrontNode;
		}

		private static void EdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			try
			{
				tcx.EdgeEvent.ConstrainedEdge = edge;
				tcx.EdgeEvent.Right = edge.P.X > edge.Q.X;
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.PrimaryTriangle = node.Triangle;
				}
				if (!IsEdgeSideOfTriangle(node.Triangle, edge.P, edge.Q))
				{
					FillEdgeEvent(tcx, edge, node);
					EdgeEvent(tcx, edge.P, edge.Q, node.Triangle, edge.Q);
				}
			}
			catch (global::Poly2Tri.PointOnEdgeException)
			{
				throw;
			}
		}

		private static void FillEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			if (tcx.EdgeEvent.Right)
			{
				FillRightAboveEdgeEvent(tcx, edge, node);
			}
			else
			{
				FillLeftAboveEdgeEvent(tcx, edge, node);
			}
		}

		private static void FillRightConcaveEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			Fill(tcx, node.Next);
			if (node.Next.Point != edge.P && global::Poly2Tri.TriangulationUtil.Orient2d(edge.Q, node.Next.Point, edge.P) == global::Poly2Tri.Orientation.CCW && global::Poly2Tri.TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == global::Poly2Tri.Orientation.CCW)
			{
				FillRightConcaveEdgeEvent(tcx, edge, node);
			}
		}

		private static void FillRightConvexEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			if (global::Poly2Tri.TriangulationUtil.Orient2d(node.Next.Point, node.Next.Next.Point, node.Next.Next.Next.Point) == global::Poly2Tri.Orientation.CCW)
			{
				FillRightConcaveEdgeEvent(tcx, edge, node.Next);
			}
			else if (global::Poly2Tri.TriangulationUtil.Orient2d(edge.Q, node.Next.Next.Point, edge.P) == global::Poly2Tri.Orientation.CCW)
			{
				FillRightConvexEdgeEvent(tcx, edge, node.Next);
			}
		}

		private static void FillRightBelowEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = node;
			}
			if (node.Point.X < edge.P.X)
			{
				if (global::Poly2Tri.TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == global::Poly2Tri.Orientation.CCW)
				{
					FillRightConcaveEdgeEvent(tcx, edge, node);
					return;
				}
				FillRightConvexEdgeEvent(tcx, edge, node);
				FillRightBelowEdgeEvent(tcx, edge, node);
			}
		}

		private static void FillRightAboveEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			while (node.Next.Point.X < edge.P.X)
			{
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.ActiveNode = node;
				}
				global::Poly2Tri.Orientation orientation = global::Poly2Tri.TriangulationUtil.Orient2d(edge.Q, node.Next.Point, edge.P);
				if (orientation == global::Poly2Tri.Orientation.CCW)
				{
					FillRightBelowEdgeEvent(tcx, edge, node);
				}
				else
				{
					node = node.Next;
				}
			}
		}

		private static void FillLeftConvexEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			if (global::Poly2Tri.TriangulationUtil.Orient2d(node.Prev.Point, node.Prev.Prev.Point, node.Prev.Prev.Prev.Point) == global::Poly2Tri.Orientation.CW)
			{
				FillLeftConcaveEdgeEvent(tcx, edge, node.Prev);
			}
			else if (global::Poly2Tri.TriangulationUtil.Orient2d(edge.Q, node.Prev.Prev.Point, edge.P) == global::Poly2Tri.Orientation.CW)
			{
				FillLeftConvexEdgeEvent(tcx, edge, node.Prev);
			}
		}

		private static void FillLeftConcaveEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			Fill(tcx, node.Prev);
			if (node.Prev.Point != edge.P && global::Poly2Tri.TriangulationUtil.Orient2d(edge.Q, node.Prev.Point, edge.P) == global::Poly2Tri.Orientation.CW && global::Poly2Tri.TriangulationUtil.Orient2d(node.Point, node.Prev.Point, node.Prev.Prev.Point) == global::Poly2Tri.Orientation.CW)
			{
				FillLeftConcaveEdgeEvent(tcx, edge, node);
			}
		}

		private static void FillLeftBelowEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.ActiveNode = node;
			}
			if (node.Point.X > edge.P.X)
			{
				if (global::Poly2Tri.TriangulationUtil.Orient2d(node.Point, node.Prev.Point, node.Prev.Prev.Point) == global::Poly2Tri.Orientation.CW)
				{
					FillLeftConcaveEdgeEvent(tcx, edge, node);
					return;
				}
				FillLeftConvexEdgeEvent(tcx, edge, node);
				FillLeftBelowEdgeEvent(tcx, edge, node);
			}
		}

		private static void FillLeftAboveEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DTSweepConstraint edge, global::Poly2Tri.AdvancingFrontNode node)
		{
			while (node.Prev.Point.X > edge.P.X)
			{
				if (tcx.IsDebugEnabled)
				{
					tcx.DTDebugContext.ActiveNode = node;
				}
				if (global::Poly2Tri.TriangulationUtil.Orient2d(edge.Q, node.Prev.Point, edge.P) == global::Poly2Tri.Orientation.CW)
				{
					FillLeftBelowEdgeEvent(tcx, edge, node);
				}
				else
				{
					node = node.Prev;
				}
			}
		}

		private static bool IsEdgeSideOfTriangle(global::Poly2Tri.DelaunayTriangle triangle, global::Poly2Tri.TriangulationPoint ep, global::Poly2Tri.TriangulationPoint eq)
		{
			int num = triangle.EdgeIndex(ep, eq);
			if (num == -1)
			{
				return false;
			}
			triangle.MarkConstrainedEdge(num);
			triangle = triangle.Neighbors[num];
			if (triangle != null)
			{
				triangle.MarkConstrainedEdge(ep, eq);
			}
			return true;
		}

		private static void EdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.TriangulationPoint ep, global::Poly2Tri.TriangulationPoint eq, global::Poly2Tri.DelaunayTriangle triangle, global::Poly2Tri.TriangulationPoint point)
		{
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.PrimaryTriangle = triangle;
			}
			if (IsEdgeSideOfTriangle(triangle, ep, eq))
			{
				return;
			}
			global::Poly2Tri.TriangulationPoint triangulationPoint = triangle.PointCCWFrom(point);
			global::Poly2Tri.Orientation orientation = global::Poly2Tri.TriangulationUtil.Orient2d(eq, triangulationPoint, ep);
			if (orientation == global::Poly2Tri.Orientation.Collinear)
			{
				if (triangle.Contains(eq) && triangle.Contains(triangulationPoint))
				{
					triangle.MarkConstrainedEdge(eq, triangulationPoint);
					tcx.EdgeEvent.ConstrainedEdge.Q = triangulationPoint;
					triangle = triangle.NeighborAcrossFrom(point);
					EdgeEvent(tcx, ep, triangulationPoint, triangle, triangulationPoint);
					if (!tcx.IsDebugEnabled)
					{
					}
					return;
				}
				throw new global::Poly2Tri.PointOnEdgeException("EdgeEvent - Point on constrained edge not supported yet", ep, eq, triangulationPoint);
			}
			global::Poly2Tri.TriangulationPoint triangulationPoint2 = triangle.PointCWFrom(point);
			global::Poly2Tri.Orientation orientation2 = global::Poly2Tri.TriangulationUtil.Orient2d(eq, triangulationPoint2, ep);
			if (orientation2 == global::Poly2Tri.Orientation.Collinear)
			{
				if (!triangle.Contains(eq) || !triangle.Contains(triangulationPoint2))
				{
					throw new global::Poly2Tri.PointOnEdgeException("EdgeEvent - Point on constrained edge not supported yet", ep, eq, triangulationPoint2);
				}
				triangle.MarkConstrainedEdge(eq, triangulationPoint2);
				tcx.EdgeEvent.ConstrainedEdge.Q = triangulationPoint2;
				triangle = triangle.NeighborAcrossFrom(point);
				EdgeEvent(tcx, ep, triangulationPoint2, triangle, triangulationPoint2);
				if (!tcx.IsDebugEnabled)
				{
				}
			}
			else if (orientation == orientation2)
			{
				triangle = ((orientation != global::Poly2Tri.Orientation.CW) ? triangle.NeighborCWFrom(point) : triangle.NeighborCCWFrom(point));
				EdgeEvent(tcx, ep, eq, triangle, point);
			}
			else
			{
				FlipEdgeEvent(tcx, ep, eq, triangle, point);
			}
		}

		private static void FlipEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.TriangulationPoint ep, global::Poly2Tri.TriangulationPoint eq, global::Poly2Tri.DelaunayTriangle t, global::Poly2Tri.TriangulationPoint p)
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = t.NeighborAcrossFrom(p);
			global::Poly2Tri.TriangulationPoint triangulationPoint = delaunayTriangle.OppositePoint(t, p);
			if (delaunayTriangle == null)
			{
				throw new global::System.InvalidOperationException("[BUG:FIXME] FLIP failed due to missing triangle");
			}
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.PrimaryTriangle = t;
				tcx.DTDebugContext.SecondaryTriangle = delaunayTriangle;
			}
			if (global::Poly2Tri.TriangulationUtil.InScanArea(p, t.PointCCWFrom(p), t.PointCWFrom(p), triangulationPoint))
			{
				RotateTrianglePair(t, p, delaunayTriangle, triangulationPoint);
				tcx.MapTriangleToNodes(t);
				tcx.MapTriangleToNodes(delaunayTriangle);
				if (p == eq && triangulationPoint == ep)
				{
					if (eq == tcx.EdgeEvent.ConstrainedEdge.Q && ep == tcx.EdgeEvent.ConstrainedEdge.P)
					{
						if (tcx.IsDebugEnabled)
						{
						}
						t.MarkConstrainedEdge(ep, eq);
						delaunayTriangle.MarkConstrainedEdge(ep, eq);
						Legalize(tcx, t);
						Legalize(tcx, delaunayTriangle);
					}
					else if (!tcx.IsDebugEnabled)
					{
					}
				}
				else
				{
					if (tcx.IsDebugEnabled)
					{
					}
					global::Poly2Tri.Orientation o = global::Poly2Tri.TriangulationUtil.Orient2d(eq, triangulationPoint, ep);
					t = NextFlipTriangle(tcx, o, t, delaunayTriangle, p, triangulationPoint);
					FlipEdgeEvent(tcx, ep, eq, t, p);
				}
			}
			else
			{
				global::Poly2Tri.TriangulationPoint newP = null;
				if (NextFlipPoint(ep, eq, delaunayTriangle, triangulationPoint, out newP))
				{
					FlipScanEdgeEvent(tcx, ep, eq, t, delaunayTriangle, newP);
					EdgeEvent(tcx, ep, eq, t, p);
				}
			}
		}

		private static bool NextFlipPoint(global::Poly2Tri.TriangulationPoint ep, global::Poly2Tri.TriangulationPoint eq, global::Poly2Tri.DelaunayTriangle ot, global::Poly2Tri.TriangulationPoint op, out global::Poly2Tri.TriangulationPoint newP)
		{
			newP = null;
			switch (global::Poly2Tri.TriangulationUtil.Orient2d(eq, op, ep))
			{
			case global::Poly2Tri.Orientation.CW:
				newP = ot.PointCCWFrom(op);
				return true;
			case global::Poly2Tri.Orientation.CCW:
				newP = ot.PointCWFrom(op);
				return true;
			case global::Poly2Tri.Orientation.Collinear:
				return false;
			default:
				throw new global::System.NotImplementedException("Orientation not handled");
			}
		}

		private static global::Poly2Tri.DelaunayTriangle NextFlipTriangle(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.Orientation o, global::Poly2Tri.DelaunayTriangle t, global::Poly2Tri.DelaunayTriangle ot, global::Poly2Tri.TriangulationPoint p, global::Poly2Tri.TriangulationPoint op)
		{
			int index;
			if (o == global::Poly2Tri.Orientation.CCW)
			{
				index = ot.EdgeIndex(p, op);
				ot.EdgeIsDelaunay[index] = true;
				Legalize(tcx, ot);
				ot.EdgeIsDelaunay.Clear();
				return t;
			}
			index = t.EdgeIndex(p, op);
			t.EdgeIsDelaunay[index] = true;
			Legalize(tcx, t);
			t.EdgeIsDelaunay.Clear();
			return ot;
		}

		private static void FlipScanEdgeEvent(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.TriangulationPoint ep, global::Poly2Tri.TriangulationPoint eq, global::Poly2Tri.DelaunayTriangle flipTriangle, global::Poly2Tri.DelaunayTriangle t, global::Poly2Tri.TriangulationPoint p)
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = t.NeighborAcrossFrom(p);
			global::Poly2Tri.TriangulationPoint triangulationPoint = delaunayTriangle.OppositePoint(t, p);
			if (delaunayTriangle == null)
			{
				throw new global::System.Exception("[BUG:FIXME] FLIP failed due to missing triangle");
			}
			if (tcx.IsDebugEnabled)
			{
				tcx.DTDebugContext.PrimaryTriangle = t;
				tcx.DTDebugContext.SecondaryTriangle = delaunayTriangle;
			}
			global::Poly2Tri.TriangulationPoint newP;
			if (global::Poly2Tri.TriangulationUtil.InScanArea(eq, flipTriangle.PointCCWFrom(eq), flipTriangle.PointCWFrom(eq), triangulationPoint))
			{
				FlipEdgeEvent(tcx, eq, triangulationPoint, delaunayTriangle, triangulationPoint);
			}
			else if (NextFlipPoint(ep, eq, delaunayTriangle, triangulationPoint, out newP))
			{
				FlipScanEdgeEvent(tcx, ep, eq, flipTriangle, delaunayTriangle, newP);
			}
		}

		private static void FillAdvancingFront(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.AdvancingFrontNode n)
		{
			global::Poly2Tri.AdvancingFrontNode next = n.Next;
			while (next.HasNext)
			{
				double num = HoleAngle(next);
				if (num > global::System.Math.PI / 2.0 || num < -global::System.Math.PI / 2.0)
				{
					break;
				}
				Fill(tcx, next);
				next = next.Next;
			}
			next = n.Prev;
			while (next.HasPrev)
			{
				double num = HoleAngle(next);
				if (num > global::System.Math.PI / 2.0 || num < -global::System.Math.PI / 2.0)
				{
					break;
				}
				Fill(tcx, next);
				next = next.Prev;
			}
			if (n.HasNext && n.Next.HasNext)
			{
				double num = BasinAngle(n);
				if (num < global::System.Math.PI * 3.0 / 4.0)
				{
					FillBasin(tcx, n);
				}
			}
		}

		private static void FillBasin(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.AdvancingFrontNode node)
		{
			if (global::Poly2Tri.TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == global::Poly2Tri.Orientation.CCW)
			{
				tcx.Basin.leftNode = node;
			}
			else
			{
				tcx.Basin.leftNode = node.Next;
			}
			tcx.Basin.bottomNode = tcx.Basin.leftNode;
			while (tcx.Basin.bottomNode.HasNext && tcx.Basin.bottomNode.Point.Y >= tcx.Basin.bottomNode.Next.Point.Y)
			{
				tcx.Basin.bottomNode = tcx.Basin.bottomNode.Next;
			}
			if (tcx.Basin.bottomNode != tcx.Basin.leftNode)
			{
				tcx.Basin.rightNode = tcx.Basin.bottomNode;
				while (tcx.Basin.rightNode.HasNext && tcx.Basin.rightNode.Point.Y < tcx.Basin.rightNode.Next.Point.Y)
				{
					tcx.Basin.rightNode = tcx.Basin.rightNode.Next;
				}
				if (tcx.Basin.rightNode != tcx.Basin.bottomNode)
				{
					tcx.Basin.width = tcx.Basin.rightNode.Point.X - tcx.Basin.leftNode.Point.X;
					tcx.Basin.leftHighest = tcx.Basin.leftNode.Point.Y > tcx.Basin.rightNode.Point.Y;
					FillBasinReq(tcx, tcx.Basin.bottomNode);
				}
			}
		}

		private static void FillBasinReq(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.AdvancingFrontNode node)
		{
			if (IsShallow(tcx, node))
			{
				return;
			}
			Fill(tcx, node);
			if (node.Prev == tcx.Basin.leftNode && node.Next == tcx.Basin.rightNode)
			{
				return;
			}
			if (node.Prev == tcx.Basin.leftNode)
			{
				if (global::Poly2Tri.TriangulationUtil.Orient2d(node.Point, node.Next.Point, node.Next.Next.Point) == global::Poly2Tri.Orientation.CW)
				{
					return;
				}
				node = node.Next;
			}
			else if (node.Next != tcx.Basin.rightNode)
			{
				node = ((!(node.Prev.Point.Y < node.Next.Point.Y)) ? node.Next : node.Prev);
			}
			else
			{
				global::Poly2Tri.Orientation orientation = global::Poly2Tri.TriangulationUtil.Orient2d(node.Point, node.Prev.Point, node.Prev.Prev.Point);
				if (orientation == global::Poly2Tri.Orientation.CCW)
				{
					return;
				}
				node = node.Prev;
			}
			FillBasinReq(tcx, node);
		}

		private static bool IsShallow(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.AdvancingFrontNode node)
		{
			double num = ((!tcx.Basin.leftHighest) ? (tcx.Basin.rightNode.Point.Y - node.Point.Y) : (tcx.Basin.leftNode.Point.Y - node.Point.Y));
			if (tcx.Basin.width > num)
			{
				return true;
			}
			return false;
		}

		private static double HoleAngle(global::Poly2Tri.AdvancingFrontNode node)
		{
			double x = node.Point.X;
			double y = node.Point.Y;
			double num = node.Next.Point.X - x;
			double num2 = node.Next.Point.Y - y;
			double num3 = node.Prev.Point.X - x;
			double num4 = node.Prev.Point.Y - y;
			return global::System.Math.Atan2(num * num4 - num2 * num3, num * num3 + num2 * num4);
		}

		private static double BasinAngle(global::Poly2Tri.AdvancingFrontNode node)
		{
			double x = node.Point.X - node.Next.Next.Point.X;
			double y = node.Point.Y - node.Next.Next.Point.Y;
			return global::System.Math.Atan2(y, x);
		}

		private static void Fill(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.AdvancingFrontNode node)
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = new global::Poly2Tri.DelaunayTriangle(node.Prev.Point, node.Point, node.Next.Point);
			delaunayTriangle.MarkNeighbor(node.Prev.Triangle);
			delaunayTriangle.MarkNeighbor(node.Triangle);
			tcx.Triangles.Add(delaunayTriangle);
			node.Prev.Next = node.Next;
			node.Next.Prev = node.Prev;
			tcx.RemoveNode(node);
			if (!Legalize(tcx, delaunayTriangle))
			{
				tcx.MapTriangleToNodes(delaunayTriangle);
			}
		}

		private static bool Legalize(global::Poly2Tri.DTSweepContext tcx, global::Poly2Tri.DelaunayTriangle t)
		{
			for (int i = 0; i < 3; i++)
			{
				if (t.EdgeIsDelaunay[i])
				{
					continue;
				}
				global::Poly2Tri.DelaunayTriangle delaunayTriangle = t.Neighbors[i];
				if (delaunayTriangle == null)
				{
					continue;
				}
				global::Poly2Tri.TriangulationPoint triangulationPoint = t.Points[i];
				global::Poly2Tri.TriangulationPoint triangulationPoint2 = delaunayTriangle.OppositePoint(t, triangulationPoint);
				int index = delaunayTriangle.IndexOf(triangulationPoint2);
				if (delaunayTriangle.EdgeIsConstrained[index] || delaunayTriangle.EdgeIsDelaunay[index])
				{
					t.SetConstrainedEdgeAcross(triangulationPoint, delaunayTriangle.EdgeIsConstrained[index]);
				}
				else if (global::Poly2Tri.TriangulationUtil.SmartIncircle(triangulationPoint, t.PointCCWFrom(triangulationPoint), t.PointCWFrom(triangulationPoint), triangulationPoint2))
				{
					t.EdgeIsDelaunay[i] = true;
					delaunayTriangle.EdgeIsDelaunay[index] = true;
					RotateTrianglePair(t, triangulationPoint, delaunayTriangle, triangulationPoint2);
					if (!Legalize(tcx, t))
					{
						tcx.MapTriangleToNodes(t);
					}
					if (!Legalize(tcx, delaunayTriangle))
					{
						tcx.MapTriangleToNodes(delaunayTriangle);
					}
					t.EdgeIsDelaunay[i] = false;
					delaunayTriangle.EdgeIsDelaunay[index] = false;
					return true;
				}
			}
			return false;
		}

		private static void RotateTrianglePair(global::Poly2Tri.DelaunayTriangle t, global::Poly2Tri.TriangulationPoint p, global::Poly2Tri.DelaunayTriangle ot, global::Poly2Tri.TriangulationPoint op)
		{
			global::Poly2Tri.DelaunayTriangle delaunayTriangle = t.NeighborCCWFrom(p);
			global::Poly2Tri.DelaunayTriangle delaunayTriangle2 = t.NeighborCWFrom(p);
			global::Poly2Tri.DelaunayTriangle delaunayTriangle3 = ot.NeighborCCWFrom(op);
			global::Poly2Tri.DelaunayTriangle delaunayTriangle4 = ot.NeighborCWFrom(op);
			bool constrainedEdgeCCW = t.GetConstrainedEdgeCCW(p);
			bool constrainedEdgeCW = t.GetConstrainedEdgeCW(p);
			bool constrainedEdgeCCW2 = ot.GetConstrainedEdgeCCW(op);
			bool constrainedEdgeCW2 = ot.GetConstrainedEdgeCW(op);
			bool delaunayEdgeCCW = t.GetDelaunayEdgeCCW(p);
			bool delaunayEdgeCW = t.GetDelaunayEdgeCW(p);
			bool delaunayEdgeCCW2 = ot.GetDelaunayEdgeCCW(op);
			bool delaunayEdgeCW2 = ot.GetDelaunayEdgeCW(op);
			t.Legalize(p, op);
			ot.Legalize(op, p);
			ot.SetDelaunayEdgeCCW(p, delaunayEdgeCCW);
			t.SetDelaunayEdgeCW(p, delaunayEdgeCW);
			t.SetDelaunayEdgeCCW(op, delaunayEdgeCCW2);
			ot.SetDelaunayEdgeCW(op, delaunayEdgeCW2);
			ot.SetConstrainedEdgeCCW(p, constrainedEdgeCCW);
			t.SetConstrainedEdgeCW(p, constrainedEdgeCW);
			t.SetConstrainedEdgeCCW(op, constrainedEdgeCCW2);
			ot.SetConstrainedEdgeCW(op, constrainedEdgeCW2);
			t.Neighbors.Clear();
			ot.Neighbors.Clear();
			if (delaunayTriangle != null)
			{
				ot.MarkNeighbor(delaunayTriangle);
			}
			if (delaunayTriangle2 != null)
			{
				t.MarkNeighbor(delaunayTriangle2);
			}
			if (delaunayTriangle3 != null)
			{
				t.MarkNeighbor(delaunayTriangle3);
			}
			if (delaunayTriangle4 != null)
			{
				ot.MarkNeighbor(delaunayTriangle4);
			}
			t.MarkNeighbor(ot);
		}
	}
}
