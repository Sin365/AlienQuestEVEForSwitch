namespace Poly2Tri
{
	public class Polygon : global::Poly2Tri.Point2DList, global::Poly2Tri.ITriangulatable, global::System.Collections.Generic.ICollection<global::Poly2Tri.TriangulationPoint>, global::System.Collections.Generic.IList<global::Poly2Tri.TriangulationPoint>, global::System.Collections.Generic.IEnumerable<global::Poly2Tri.TriangulationPoint>, global::System.Collections.IEnumerable
	{
		protected global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationPoint> mPointMap = new global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationPoint>();

		protected global::System.Collections.Generic.List<global::Poly2Tri.DelaunayTriangle> mTriangles;

		private double mPrecision = global::Poly2Tri.TriangulationPoint.kVertexCodeDefaultPrecision;

		protected global::System.Collections.Generic.List<global::Poly2Tri.Polygon> mHoles;

		protected global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> mSteinerPoints;

		protected global::Poly2Tri.PolygonPoint _last;

		public global::System.Collections.Generic.IList<global::Poly2Tri.TriangulationPoint> Points
		{
			get
			{
				return this;
			}
		}

		public global::System.Collections.Generic.IList<global::Poly2Tri.DelaunayTriangle> Triangles
		{
			get
			{
				return mTriangles;
			}
		}

		public global::Poly2Tri.TriangulationMode TriangulationMode
		{
			get
			{
				return global::Poly2Tri.TriangulationMode.Polygon;
			}
		}

		public string FileName { get; set; }

		public bool DisplayFlipX { get; set; }

		public bool DisplayFlipY { get; set; }

		public float DisplayRotate { get; set; }

		public double Precision
		{
			get
			{
				return mPrecision;
			}
			set
			{
				mPrecision = value;
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

		public global::System.Collections.Generic.IList<global::Poly2Tri.Polygon> Holes
		{
			get
			{
				return mHoles;
			}
		}

		public Polygon(global::System.Collections.Generic.IList<global::Poly2Tri.PolygonPoint> points)
		{
			if (points.Count < 3)
			{
				throw new global::System.ArgumentException("List has fewer than 3 points", "points");
			}
			AddRange(points, global::Poly2Tri.Point2DList.WindingOrderType.Unknown);
		}

		public Polygon(global::System.Collections.Generic.IEnumerable<global::Poly2Tri.PolygonPoint> points)
			: this((points as global::System.Collections.Generic.IList<global::Poly2Tri.PolygonPoint>) ?? global::System.Linq.Enumerable.ToArray(points))
		{
		}

		public Polygon(params global::Poly2Tri.PolygonPoint[] points)
			: this((global::System.Collections.Generic.IList<global::Poly2Tri.PolygonPoint>)points)
		{
		}

		global::System.Collections.Generic.IEnumerator<global::Poly2Tri.TriangulationPoint> global::System.Collections.Generic.IEnumerable<global::Poly2Tri.TriangulationPoint>.GetEnumerator()
		{
			return new global::Poly2Tri.TriangulationPointEnumerator(mPoints);
		}

		public int IndexOf(global::Poly2Tri.TriangulationPoint p)
		{
			return mPoints.IndexOf(p);
		}

		public override void Add(global::Poly2Tri.Point2D p)
		{
			Add(p, -1, true);
		}

		public void Add(global::Poly2Tri.TriangulationPoint p)
		{
			Add(p, -1, true);
		}

		public void Add(global::Poly2Tri.PolygonPoint p)
		{
			Add(p, -1, true);
		}

		protected override void Add(global::Poly2Tri.Point2D p, int idx, bool bCalcWindingOrderAndEpsilon)
		{
			global::Poly2Tri.TriangulationPoint triangulationPoint = p as global::Poly2Tri.TriangulationPoint;
			if (triangulationPoint == null || mPointMap.ContainsKey(triangulationPoint.VertexCode))
			{
				return;
			}
			mPointMap.Add(triangulationPoint.VertexCode, triangulationPoint);
			base.Add(p, idx, bCalcWindingOrderAndEpsilon);
			global::Poly2Tri.PolygonPoint polygonPoint = p as global::Poly2Tri.PolygonPoint;
			if (polygonPoint != null)
			{
				polygonPoint.Previous = _last;
				if (_last != null)
				{
					polygonPoint.Next = _last.Next;
					_last.Next = polygonPoint;
				}
				_last = polygonPoint;
			}
		}

		public void AddRange(global::System.Collections.Generic.IList<global::Poly2Tri.PolygonPoint> points, global::Poly2Tri.Point2DList.WindingOrderType windingOrder)
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
			return base.Remove(p);
		}

		public void RemovePoint(global::Poly2Tri.PolygonPoint p)
		{
			global::Poly2Tri.PolygonPoint next = p.Next;
			global::Poly2Tri.PolygonPoint previous = p.Previous;
			previous.Next = next;
			next.Previous = previous;
			mPoints.Remove(p);
			mBoundingBox.Clear();
			foreach (global::Poly2Tri.PolygonPoint mPoint in mPoints)
			{
				mBoundingBox.AddPoint(mPoint);
			}
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

		public void AddSteinerPoint(global::Poly2Tri.TriangulationPoint point)
		{
			if (mSteinerPoints == null)
			{
				mSteinerPoints = new global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint>();
			}
			mSteinerPoints.Add(point);
		}

		public void AddSteinerPoints(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> points)
		{
			if (mSteinerPoints == null)
			{
				mSteinerPoints = new global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint>();
			}
			mSteinerPoints.AddRange(points);
		}

		public void ClearSteinerPoints()
		{
			if (mSteinerPoints != null)
			{
				mSteinerPoints.Clear();
			}
		}

		public void AddHole(global::Poly2Tri.Polygon poly)
		{
			if (mHoles == null)
			{
				mHoles = new global::System.Collections.Generic.List<global::Poly2Tri.Polygon>();
			}
			mHoles.Add(poly);
		}

		public void AddTriangle(global::Poly2Tri.DelaunayTriangle t)
		{
			mTriangles.Add(t);
		}

		public void AddTriangles(global::System.Collections.Generic.IEnumerable<global::Poly2Tri.DelaunayTriangle> list)
		{
			mTriangles.AddRange(list);
		}

		public void ClearTriangles()
		{
			if (mTriangles != null)
			{
				mTriangles.Clear();
			}
		}

		public bool IsPointInside(global::Poly2Tri.TriangulationPoint p)
		{
			return global::Poly2Tri.PolygonUtil.PointInPolygon2D(this, p);
		}

		public void Prepare(global::Poly2Tri.TriangulationContext tcx)
		{
			if (mTriangles == null)
			{
				mTriangles = new global::System.Collections.Generic.List<global::Poly2Tri.DelaunayTriangle>(mPoints.Count);
			}
			else
			{
				mTriangles.Clear();
			}
			for (int i = 0; i < mPoints.Count - 1; i++)
			{
				tcx.NewConstraint(this[i], this[i + 1]);
			}
			tcx.NewConstraint(this[0], this[Count - 1]);
			tcx.Points.AddRange(this);
			if (mHoles != null)
			{
				foreach (global::Poly2Tri.Polygon mHole in mHoles)
				{
					for (int j = 0; j < mHole.mPoints.Count - 1; j++)
					{
						tcx.NewConstraint(mHole[j], mHole[j + 1]);
					}
					tcx.NewConstraint(mHole[0], mHole[mHole.Count - 1]);
					tcx.Points.AddRange(mHole);
				}
			}
			if (mSteinerPoints != null)
			{
				tcx.Points.AddRange(mSteinerPoints);
			}
		}
	}
}
