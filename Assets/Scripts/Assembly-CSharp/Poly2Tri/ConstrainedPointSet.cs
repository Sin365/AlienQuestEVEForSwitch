namespace Poly2Tri
{
	public class ConstrainedPointSet : global::Poly2Tri.PointSet
	{
		protected global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationConstraint> mConstraintMap = new global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationConstraint>();

		protected global::System.Collections.Generic.List<global::Poly2Tri.Contour> mHoles = new global::System.Collections.Generic.List<global::Poly2Tri.Contour>();

		public override global::Poly2Tri.TriangulationMode TriangulationMode
		{
			get
			{
				return global::Poly2Tri.TriangulationMode.Constrained;
			}
		}

		public ConstrainedPointSet(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> bounds)
			: base(bounds)
		{
			AddBoundaryConstraints();
		}

		public ConstrainedPointSet(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> bounds, global::System.Collections.Generic.List<global::Poly2Tri.TriangulationConstraint> constraints)
			: base(bounds)
		{
			AddBoundaryConstraints();
			AddConstraints(constraints);
		}

		public ConstrainedPointSet(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> bounds, int[] indices)
			: base(bounds)
		{
			AddBoundaryConstraints();
			global::System.Collections.Generic.List<global::Poly2Tri.TriangulationConstraint> list = new global::System.Collections.Generic.List<global::Poly2Tri.TriangulationConstraint>();
			for (int i = 0; i < indices.Length; i += 2)
			{
				global::Poly2Tri.TriangulationConstraint item = new global::Poly2Tri.TriangulationConstraint(bounds[i], bounds[i + 1]);
				list.Add(item);
			}
			AddConstraints(list);
		}

		protected void AddBoundaryConstraints()
		{
			global::Poly2Tri.TriangulationPoint p = null;
			global::Poly2Tri.TriangulationPoint p2 = null;
			global::Poly2Tri.TriangulationPoint p3 = null;
			global::Poly2Tri.TriangulationPoint p4 = null;
			if (!TryGetPoint(MinX, MinY, out p))
			{
				p = new global::Poly2Tri.TriangulationPoint(MinX, MinY);
				Add(p);
			}
			if (!TryGetPoint(MaxX, MinY, out p2))
			{
				p2 = new global::Poly2Tri.TriangulationPoint(MaxX, MinY);
				Add(p2);
			}
			if (!TryGetPoint(MaxX, MaxY, out p3))
			{
				p3 = new global::Poly2Tri.TriangulationPoint(MaxX, MaxY);
				Add(p3);
			}
			if (!TryGetPoint(MinX, MaxY, out p4))
			{
				p4 = new global::Poly2Tri.TriangulationPoint(MinX, MaxY);
				Add(p4);
			}
			global::Poly2Tri.TriangulationConstraint tc = new global::Poly2Tri.TriangulationConstraint(p, p2);
			AddConstraint(tc);
			global::Poly2Tri.TriangulationConstraint tc2 = new global::Poly2Tri.TriangulationConstraint(p2, p3);
			AddConstraint(tc2);
			global::Poly2Tri.TriangulationConstraint tc3 = new global::Poly2Tri.TriangulationConstraint(p3, p4);
			AddConstraint(tc3);
			global::Poly2Tri.TriangulationConstraint tc4 = new global::Poly2Tri.TriangulationConstraint(p4, p);
			AddConstraint(tc4);
		}

		public override void Add(global::Poly2Tri.Point2D p)
		{
			Add(p as global::Poly2Tri.TriangulationPoint, -1, true);
		}

		public override void Add(global::Poly2Tri.TriangulationPoint p)
		{
			Add(p, -1, true);
		}

		public override bool AddRange(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> points)
		{
			bool flag = true;
			foreach (global::Poly2Tri.TriangulationPoint point in points)
			{
				flag = Add(point, -1, true) && flag;
			}
			return flag;
		}

		public bool AddHole(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> points, string name)
		{
			if (points == null)
			{
				return false;
			}
			global::System.Collections.Generic.List<global::Poly2Tri.Contour> list = new global::System.Collections.Generic.List<global::Poly2Tri.Contour>();
			int num = 0;
			global::Poly2Tri.Contour item = new global::Poly2Tri.Contour(this, points, global::Poly2Tri.Point2DList.WindingOrderType.Unknown);
			list.Add(item);
			if (mPoints.Count > 1)
			{
				int count = list[num].Count;
				for (int i = 0; i < count; i++)
				{
					ConstrainPointToBounds(list[num][i]);
				}
			}
			while (num < list.Count)
			{
				list[num].RemoveDuplicateNeighborPoints();
				list[num].WindingOrder = global::Poly2Tri.Point2DList.WindingOrderType.CCW;
				bool flag = true;
				global::Poly2Tri.Point2DList.PolygonError polygonError = list[num].CheckPolygon();
				while (flag && polygonError != global::Poly2Tri.Point2DList.PolygonError.None)
				{
					if ((polygonError & global::Poly2Tri.Point2DList.PolygonError.NotEnoughVertices) == global::Poly2Tri.Point2DList.PolygonError.NotEnoughVertices)
					{
						flag = false;
					}
					else if ((polygonError & global::Poly2Tri.Point2DList.PolygonError.NotSimple) == global::Poly2Tri.Point2DList.PolygonError.NotSimple)
					{
						global::System.Collections.Generic.List<global::Poly2Tri.Point2DList> list2 = global::Poly2Tri.PolygonUtil.SplitComplexPolygon(list[num], list[num].Epsilon);
						list.RemoveAt(num);
						foreach (global::Poly2Tri.Point2DList item2 in list2)
						{
							global::Poly2Tri.Contour contour = new global::Poly2Tri.Contour(this);
							contour.AddRange(item2);
							list.Add(contour);
						}
						polygonError = list[num].CheckPolygon();
					}
					else if ((polygonError & global::Poly2Tri.Point2DList.PolygonError.Degenerate) == global::Poly2Tri.Point2DList.PolygonError.Degenerate)
					{
						list[num].Simplify(base.Epsilon);
						polygonError = list[num].CheckPolygon();
					}
					else if ((polygonError & global::Poly2Tri.Point2DList.PolygonError.AreaTooSmall) == global::Poly2Tri.Point2DList.PolygonError.AreaTooSmall || (polygonError & global::Poly2Tri.Point2DList.PolygonError.SidesTooCloseToParallel) == global::Poly2Tri.Point2DList.PolygonError.SidesTooCloseToParallel || (polygonError & global::Poly2Tri.Point2DList.PolygonError.TooThin) == global::Poly2Tri.Point2DList.PolygonError.TooThin || (polygonError & global::Poly2Tri.Point2DList.PolygonError.Unknown) == global::Poly2Tri.Point2DList.PolygonError.Unknown)
					{
						flag = false;
					}
				}
				if (!flag && list[num].Count != 2)
				{
					list.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
			bool result = true;
			num = 0;
			while (num < list.Count)
			{
				int count2 = list[num].Count;
				if (count2 < 2)
				{
					num++;
					result = false;
					continue;
				}
				if (count2 == 2)
				{
					uint key = global::Poly2Tri.TriangulationConstraint.CalculateContraintCode(list[num][0], list[num][1]);
					global::Poly2Tri.TriangulationConstraint value = null;
					if (!mConstraintMap.TryGetValue(key, out value))
					{
						value = new global::Poly2Tri.TriangulationConstraint(list[num][0], list[num][1]);
						AddConstraint(value);
					}
				}
				else
				{
					global::Poly2Tri.Contour contour2 = new global::Poly2Tri.Contour(this, list[num], global::Poly2Tri.Point2DList.WindingOrderType.Unknown);
					contour2.WindingOrder = global::Poly2Tri.Point2DList.WindingOrderType.CCW;
					contour2.Name = name + ":" + num;
					mHoles.Add(contour2);
				}
				num++;
			}
			return result;
		}

		public bool AddConstraints(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationConstraint> constraints)
		{
			if (constraints == null || constraints.Count < 1)
			{
				return false;
			}
			bool flag = true;
			foreach (global::Poly2Tri.TriangulationConstraint constraint in constraints)
			{
				if (ConstrainPointToBounds(constraint.P) || ConstrainPointToBounds(constraint.Q))
				{
					constraint.CalculateContraintCode();
				}
				global::Poly2Tri.TriangulationConstraint value = null;
				if (!mConstraintMap.TryGetValue(constraint.ConstraintCode, out value))
				{
					value = constraint;
					flag = AddConstraint(value) && flag;
				}
			}
			return flag;
		}

		public bool AddConstraint(global::Poly2Tri.TriangulationConstraint tc)
		{
			if (tc == null || tc.P == null || tc.Q == null)
			{
				return false;
			}
			if (mConstraintMap.ContainsKey(tc.ConstraintCode))
			{
				return true;
			}
			global::Poly2Tri.TriangulationPoint p;
			if (TryGetPoint(tc.P.X, tc.P.Y, out p))
			{
				tc.P = p;
			}
			else
			{
				Add(tc.P);
			}
			if (TryGetPoint(tc.Q.X, tc.Q.Y, out p))
			{
				tc.Q = p;
			}
			else
			{
				Add(tc.Q);
			}
			mConstraintMap.Add(tc.ConstraintCode, tc);
			return true;
		}

		public bool TryGetConstraint(uint constraintCode, out global::Poly2Tri.TriangulationConstraint tc)
		{
			return mConstraintMap.TryGetValue(constraintCode, out tc);
		}

		public int GetNumConstraints()
		{
			return mConstraintMap.Count;
		}

		public global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationConstraint>.Enumerator GetConstraintEnumerator()
		{
			return mConstraintMap.GetEnumerator();
		}

		public int GetNumHoles()
		{
			int num = 0;
			foreach (global::Poly2Tri.Contour mHole in mHoles)
			{
				num += mHole.GetNumHoles(false);
			}
			return num;
		}

		public global::Poly2Tri.Contour GetHole(int idx)
		{
			if (idx < 0 || idx >= mHoles.Count)
			{
				return null;
			}
			return mHoles[idx];
		}

		public int GetActualHoles(out global::System.Collections.Generic.List<global::Poly2Tri.Contour> holes)
		{
			holes = new global::System.Collections.Generic.List<global::Poly2Tri.Contour>();
			foreach (global::Poly2Tri.Contour mHole in mHoles)
			{
				mHole.GetActualHoles(false, ref holes);
			}
			return holes.Count;
		}

		protected void InitializeHoles()
		{
			global::Poly2Tri.Contour.InitializeHoles(mHoles, this, this);
			foreach (global::Poly2Tri.Contour mHole in mHoles)
			{
				mHole.InitializeHoles(this);
			}
		}

		public override bool Initialize()
		{
			InitializeHoles();
			return base.Initialize();
		}

		public override void Prepare(global::Poly2Tri.TriangulationContext tcx)
		{
			if (Initialize())
			{
				base.Prepare(tcx);
				global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationConstraint>.Enumerator enumerator = mConstraintMap.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::Poly2Tri.TriangulationConstraint value = enumerator.Current.Value;
					tcx.NewConstraint(value.P, value.Q);
				}
			}
		}

		public override void AddTriangle(global::Poly2Tri.DelaunayTriangle t)
		{
			Triangles.Add(t);
		}
	}
}
