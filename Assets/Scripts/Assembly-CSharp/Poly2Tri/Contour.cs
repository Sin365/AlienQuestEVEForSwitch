namespace Poly2Tri
{
	public class Contour : global::Poly2Tri.Point2DList, global::Poly2Tri.ITriangulatable, global::System.Collections.Generic.ICollection<global::Poly2Tri.TriangulationPoint>, global::System.Collections.Generic.IList<global::Poly2Tri.TriangulationPoint>, global::System.Collections.Generic.IEnumerable<global::Poly2Tri.TriangulationPoint>, global::System.Collections.IEnumerable
	{
		private global::System.Collections.Generic.List<global::Poly2Tri.Contour> mHoles = new global::System.Collections.Generic.List<global::Poly2Tri.Contour>();

		private global::Poly2Tri.ITriangulatable mParent;

		private string mName = string.Empty;

		public new global::Poly2Tri.TriangulationPoint this[int index]
		{
			get
			{
				return mPoints[index] as global::Poly2Tri.TriangulationPoint;
			}
			set
			{
				mPoints[index] = value;
			}
		}

		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}

		public global::System.Collections.Generic.IList<global::Poly2Tri.DelaunayTriangle> Triangles
		{
			get
			{
				throw new global::System.NotImplementedException("PolyHole.Triangles should never get called");
			}
			private set
			{
			}
		}

		public global::Poly2Tri.TriangulationMode TriangulationMode
		{
			get
			{
				return mParent.TriangulationMode;
			}
		}

		public string FileName
		{
			get
			{
				return mParent.FileName;
			}
			set
			{
			}
		}

		public bool DisplayFlipX
		{
			get
			{
				return mParent.DisplayFlipX;
			}
			set
			{
			}
		}

		public bool DisplayFlipY
		{
			get
			{
				return mParent.DisplayFlipY;
			}
			set
			{
			}
		}

		public float DisplayRotate
		{
			get
			{
				return mParent.DisplayRotate;
			}
			set
			{
			}
		}

		public double Precision
		{
			get
			{
				return mParent.Precision;
			}
			set
			{
			}
		}

		public double MinX
		{
			get
			{
				return mBoundingBox.MinX;
			}
		}

		public double MaxX
		{
			get
			{
				return mBoundingBox.MaxX;
			}
		}

		public double MinY
		{
			get
			{
				return mBoundingBox.MinY;
			}
		}

		public double MaxY
		{
			get
			{
				return mBoundingBox.MaxY;
			}
		}

		public global::Poly2Tri.Rect2D Bounds
		{
			get
			{
				return mBoundingBox;
			}
		}

		public Contour(global::Poly2Tri.ITriangulatable parent)
		{
			mParent = parent;
		}

		public Contour(global::Poly2Tri.ITriangulatable parent, global::System.Collections.Generic.IList<global::Poly2Tri.TriangulationPoint> points, global::Poly2Tri.Point2DList.WindingOrderType windingOrder)
		{
			mParent = parent;
			AddRange(points, windingOrder);
		}

		global::System.Collections.Generic.IEnumerator<global::Poly2Tri.TriangulationPoint> global::System.Collections.Generic.IEnumerable<global::Poly2Tri.TriangulationPoint>.GetEnumerator()
		{
			return new global::Poly2Tri.TriangulationPointEnumerator(mPoints);
		}

		public override string ToString()
		{
			return mName + " : " + base.ToString();
		}

		public int IndexOf(global::Poly2Tri.TriangulationPoint p)
		{
			return mPoints.IndexOf(p);
		}

		public void Add(global::Poly2Tri.TriangulationPoint p)
		{
			Add(p, -1, true);
		}

		protected override void Add(global::Poly2Tri.Point2D p, int idx, bool bCalcWindingOrderAndEpsilon)
		{
			global::Poly2Tri.TriangulationPoint triangulationPoint = null;
			triangulationPoint = ((!(p is global::Poly2Tri.TriangulationPoint)) ? new global::Poly2Tri.TriangulationPoint(p.X, p.Y) : (p as global::Poly2Tri.TriangulationPoint));
			if (idx < 0)
			{
				mPoints.Add(triangulationPoint);
			}
			else
			{
				mPoints.Insert(idx, triangulationPoint);
			}
			mBoundingBox.AddPoint(triangulationPoint);
			if (bCalcWindingOrderAndEpsilon)
			{
				if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown)
				{
					mWindingOrder = CalculateWindingOrder();
				}
				mEpsilon = CalculateEpsilon();
			}
		}

		public override void AddRange(global::System.Collections.Generic.IEnumerator<global::Poly2Tri.Point2D> iter, global::Poly2Tri.Point2DList.WindingOrderType windingOrder)
		{
			if (iter == null)
			{
				return;
			}
			if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown && Count == 0)
			{
				mWindingOrder = windingOrder;
			}
			bool flag = base.WindingOrder != global::Poly2Tri.Point2DList.WindingOrderType.Unknown && windingOrder != global::Poly2Tri.Point2DList.WindingOrderType.Unknown && base.WindingOrder != windingOrder;
			bool flag2 = true;
			int count = mPoints.Count;
			iter.Reset();
			while (iter.MoveNext())
			{
				global::Poly2Tri.TriangulationPoint triangulationPoint = null;
				triangulationPoint = ((!(iter.Current is global::Poly2Tri.TriangulationPoint)) ? new global::Poly2Tri.TriangulationPoint(iter.Current.X, iter.Current.Y) : (iter.Current as global::Poly2Tri.TriangulationPoint));
				if (!flag2)
				{
					flag2 = true;
					mPoints.Add(triangulationPoint);
				}
				else if (flag)
				{
					mPoints.Insert(count, triangulationPoint);
				}
				else
				{
					mPoints.Add(triangulationPoint);
				}
				mBoundingBox.AddPoint(iter.Current);
			}
			if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown && windingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown)
			{
				mWindingOrder = CalculateWindingOrder();
			}
			mEpsilon = CalculateEpsilon();
		}

		public void AddRange(global::System.Collections.Generic.IList<global::Poly2Tri.TriangulationPoint> points, global::Poly2Tri.Point2DList.WindingOrderType windingOrder)
		{
			if (points == null || points.Count < 1)
			{
				return;
			}
			if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown && Count == 0)
			{
				mWindingOrder = windingOrder;
			}
			int count = points.Count;
			bool flag = base.WindingOrder != global::Poly2Tri.Point2DList.WindingOrderType.Unknown && windingOrder != global::Poly2Tri.Point2DList.WindingOrderType.Unknown && base.WindingOrder != windingOrder;
			for (int i = 0; i < count; i++)
			{
				int index = i;
				if (flag)
				{
					index = points.Count - i - 1;
				}
				Add(points[index], -1, false);
			}
			if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown)
			{
				mWindingOrder = CalculateWindingOrder();
			}
			mEpsilon = CalculateEpsilon();
		}

		public void Insert(int idx, global::Poly2Tri.TriangulationPoint p)
		{
			Add(p, idx, true);
		}

		public bool Remove(global::Poly2Tri.TriangulationPoint p)
		{
			return Remove((global::Poly2Tri.Point2D)p);
		}

		public bool Contains(global::Poly2Tri.TriangulationPoint p)
		{
			return mPoints.Contains(p);
		}

		public void CopyTo(global::Poly2Tri.TriangulationPoint[] array, int arrayIndex)
		{
			int num = global::System.Math.Min(Count, array.Length - arrayIndex);
			for (int i = 0; i < num; i++)
			{
				array[arrayIndex + i] = mPoints[i] as global::Poly2Tri.TriangulationPoint;
			}
		}

		protected void AddHole(global::Poly2Tri.Contour c)
		{
			c.mParent = this;
			mHoles.Add(c);
		}

		public int GetNumHoles(bool parentIsHole)
		{
			int num = ((!parentIsHole) ? 1 : 0);
			foreach (global::Poly2Tri.Contour mHole in mHoles)
			{
				num += mHole.GetNumHoles(!parentIsHole);
			}
			return num;
		}

		public int GetNumHoles()
		{
			return mHoles.Count;
		}

		public global::Poly2Tri.Contour GetHole(int idx)
		{
			if (idx < 0 || idx >= mHoles.Count)
			{
				return null;
			}
			return mHoles[idx];
		}

		public void GetActualHoles(bool parentIsHole, ref global::System.Collections.Generic.List<global::Poly2Tri.Contour> holes)
		{
			if (parentIsHole)
			{
				holes.Add(this);
			}
			foreach (global::Poly2Tri.Contour mHole in mHoles)
			{
				mHole.GetActualHoles(!parentIsHole, ref holes);
			}
		}

		public global::System.Collections.Generic.List<global::Poly2Tri.Contour>.Enumerator GetHoleEnumerator()
		{
			return mHoles.GetEnumerator();
		}

		public void InitializeHoles(global::Poly2Tri.ConstrainedPointSet cps)
		{
			InitializeHoles(mHoles, this, cps);
			foreach (global::Poly2Tri.Contour mHole in mHoles)
			{
				mHole.InitializeHoles(cps);
			}
		}

		public static void InitializeHoles(global::System.Collections.Generic.List<global::Poly2Tri.Contour> holes, global::Poly2Tri.ITriangulatable parent, global::Poly2Tri.ConstrainedPointSet cps)
		{
			int num = holes.Count;
			int i;
			for (i = 0; i < num; i++)
			{
				int num2 = i + 1;
				while (num2 < num)
				{
					if (global::Poly2Tri.PolygonUtil.PolygonsAreSame2D(holes[i], holes[num2]))
					{
						holes.RemoveAt(num2);
						num--;
					}
					else
					{
						num2++;
					}
				}
			}
			i = 0;
			while (i < num)
			{
				bool flag = true;
				int num3 = i + 1;
				while (num3 < num)
				{
					if (global::Poly2Tri.PolygonUtil.PolygonContainsPolygon(holes[i], holes[i].Bounds, holes[num3], holes[num3].Bounds, false))
					{
						holes[i].AddHole(holes[num3]);
						holes.RemoveAt(num3);
						num--;
						continue;
					}
					if (global::Poly2Tri.PolygonUtil.PolygonContainsPolygon(holes[num3], holes[num3].Bounds, holes[i], holes[i].Bounds, false))
					{
						holes[num3].AddHole(holes[i]);
						holes.RemoveAt(i);
						num--;
						flag = false;
						break;
					}
					if (global::Poly2Tri.PolygonUtil.PolygonsIntersect2D(holes[i], holes[i].Bounds, holes[num3], holes[num3].Bounds))
					{
						global::Poly2Tri.PolygonOperationContext polygonOperationContext = new global::Poly2Tri.PolygonOperationContext();
						if (!polygonOperationContext.Init(global::Poly2Tri.PolygonUtil.PolyOperation.Union | global::Poly2Tri.PolygonUtil.PolyOperation.Intersect, holes[i], holes[num3]))
						{
							if (polygonOperationContext.mError == global::Poly2Tri.PolygonUtil.PolyUnionError.Poly1InsidePoly2)
							{
								holes[num3].AddHole(holes[i]);
								holes.RemoveAt(i);
								num--;
								flag = false;
								break;
							}
							throw new global::System.Exception("PolygonOperationContext.Init had an error during initialization");
						}
						if (global::Poly2Tri.PolygonUtil.PolygonOperation(polygonOperationContext) != global::Poly2Tri.PolygonUtil.PolyUnionError.None)
						{
							throw new global::System.Exception("PolygonOperation had an error!");
						}
						global::Poly2Tri.Point2DList union = polygonOperationContext.Union;
						global::Poly2Tri.Point2DList intersect = polygonOperationContext.Intersect;
						global::Poly2Tri.Contour contour = new global::Poly2Tri.Contour(parent);
						contour.AddRange(union);
						contour.Name = "(" + holes[i].Name + " UNION " + holes[num3].Name + ")";
						contour.WindingOrder = global::Poly2Tri.Point2DList.WindingOrderType.CCW;
						int numHoles = holes[i].GetNumHoles();
						for (int j = 0; j < numHoles; j++)
						{
							contour.AddHole(holes[i].GetHole(j));
						}
						numHoles = holes[num3].GetNumHoles();
						for (int k = 0; k < numHoles; k++)
						{
							contour.AddHole(holes[num3].GetHole(k));
						}
						global::Poly2Tri.Contour contour2 = new global::Poly2Tri.Contour(contour);
						contour2.AddRange(intersect);
						contour2.Name = "(" + holes[i].Name + " INTERSECT " + holes[num3].Name + ")";
						contour2.WindingOrder = global::Poly2Tri.Point2DList.WindingOrderType.CCW;
						contour.AddHole(contour2);
						holes[i] = contour;
						holes.RemoveAt(num3);
						num--;
						num3 = i + 1;
					}
					else
					{
						num3++;
					}
				}
				if (flag)
				{
					i++;
				}
			}
			num = holes.Count;
			for (i = 0; i < num; i++)
			{
				int count = holes[i].Count;
				for (int l = 0; l < count; l++)
				{
					int index = holes[i].NextIndex(l);
					uint constraintCode = global::Poly2Tri.TriangulationConstraint.CalculateContraintCode(holes[i][l], holes[i][index]);
					global::Poly2Tri.TriangulationConstraint tc = null;
					if (!cps.TryGetConstraint(constraintCode, out tc))
					{
						tc = new global::Poly2Tri.TriangulationConstraint(holes[i][l], holes[i][index]);
						cps.AddConstraint(tc);
					}
					if (holes[i][l].VertexCode == tc.P.VertexCode)
					{
						holes[i][l] = tc.P;
					}
					else if (holes[i][index].VertexCode == tc.P.VertexCode)
					{
						holes[i][index] = tc.P;
					}
					if (holes[i][l].VertexCode == tc.Q.VertexCode)
					{
						holes[i][l] = tc.Q;
					}
					else if (holes[i][index].VertexCode == tc.Q.VertexCode)
					{
						holes[i][index] = tc.Q;
					}
				}
			}
		}

		public void Prepare(global::Poly2Tri.TriangulationContext tcx)
		{
			throw new global::System.NotImplementedException("PolyHole.Prepare should never get called");
		}

		public void AddTriangle(global::Poly2Tri.DelaunayTriangle t)
		{
			throw new global::System.NotImplementedException("PolyHole.AddTriangle should never get called");
		}

		public void AddTriangles(global::System.Collections.Generic.IEnumerable<global::Poly2Tri.DelaunayTriangle> list)
		{
			throw new global::System.NotImplementedException("PolyHole.AddTriangles should never get called");
		}

		public void ClearTriangles()
		{
			throw new global::System.NotImplementedException("PolyHole.ClearTriangles should never get called");
		}

		public global::Poly2Tri.Point2D FindPointInContour()
		{
			if (Count < 3)
			{
				return null;
			}
			global::Poly2Tri.Point2D centroid = GetCentroid();
			if (IsPointInsideContour(centroid))
			{
				return centroid;
			}
			global::System.Random random = new global::System.Random();
			do
			{
				centroid.X = random.NextDouble() * (MaxX - MinX) + MinX;
				centroid.Y = random.NextDouble() * (MaxY - MinY) + MinY;
			}
			while (!IsPointInsideContour(centroid));
			return centroid;
		}

		public bool IsPointInsideContour(global::Poly2Tri.Point2D p)
		{
			if (global::Poly2Tri.PolygonUtil.PointInPolygon2D(this, p))
			{
				foreach (global::Poly2Tri.Contour mHole in mHoles)
				{
					if (mHole.IsPointInsideContour(p))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}
