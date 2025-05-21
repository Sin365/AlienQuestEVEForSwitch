namespace Poly2Tri
{
	public class PointSet : global::Poly2Tri.Point2DList, global::Poly2Tri.ITriangulatable, global::System.Collections.Generic.ICollection<global::Poly2Tri.TriangulationPoint>, global::System.Collections.Generic.IList<global::Poly2Tri.TriangulationPoint>, global::System.Collections.Generic.IEnumerable<global::Poly2Tri.TriangulationPoint>, global::System.Collections.IEnumerable
	{
		protected global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationPoint> mPointMap = new global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.TriangulationPoint>();

		protected double mPrecision = global::Poly2Tri.TriangulationPoint.kVertexCodeDefaultPrecision;

		public global::System.Collections.Generic.IList<global::Poly2Tri.TriangulationPoint> Points
		{
			get
			{
				return this;
			}
			private set
			{
			}
		}

		public global::System.Collections.Generic.IList<global::Poly2Tri.DelaunayTriangle> Triangles { get; private set; }

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

		public virtual global::Poly2Tri.TriangulationMode TriangulationMode
		{
			get
			{
				return global::Poly2Tri.TriangulationMode.Unconstrained;
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

		public PointSet(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> bounds)
		{
			foreach (global::Poly2Tri.TriangulationPoint bound in bounds)
			{
				Add(bound, -1, false);
				mBoundingBox.AddPoint(bound);
			}
			mEpsilon = CalculateEpsilon();
			mWindingOrder = global::Poly2Tri.Point2DList.WindingOrderType.Unknown;
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
			Add(p as global::Poly2Tri.TriangulationPoint, -1, false);
		}

		public virtual void Add(global::Poly2Tri.TriangulationPoint p)
		{
			Add(p, -1, false);
		}

		protected override void Add(global::Poly2Tri.Point2D p, int idx, bool constrainToBounds)
		{
			Add(p as global::Poly2Tri.TriangulationPoint, idx, constrainToBounds);
		}

		protected bool Add(global::Poly2Tri.TriangulationPoint p, int idx, bool constrainToBounds)
		{
			if (p == null)
			{
				return false;
			}
			if (constrainToBounds)
			{
				ConstrainPointToBounds(p);
			}
			if (mPointMap.ContainsKey(p.VertexCode))
			{
				return true;
			}
			mPointMap.Add(p.VertexCode, p);
			if (idx < 0)
			{
				mPoints.Add(p);
			}
			else
			{
				mPoints.Insert(idx, p);
			}
			return true;
		}

		public override void AddRange(global::System.Collections.Generic.IEnumerator<global::Poly2Tri.Point2D> iter, global::Poly2Tri.Point2DList.WindingOrderType windingOrder)
		{
			if (iter != null)
			{
				iter.Reset();
				while (iter.MoveNext())
				{
					Add(iter.Current);
				}
			}
		}

		public virtual bool AddRange(global::System.Collections.Generic.List<global::Poly2Tri.TriangulationPoint> points)
		{
			bool flag = true;
			foreach (global::Poly2Tri.TriangulationPoint point in points)
			{
				flag = Add(point, -1, false) && flag;
			}
			return flag;
		}

		public bool TryGetPoint(double x, double y, out global::Poly2Tri.TriangulationPoint p)
		{
			uint key = global::Poly2Tri.TriangulationPoint.CreateVertexCode(x, y, Precision);
			if (mPointMap.TryGetValue(key, out p))
			{
				return true;
			}
			return false;
		}

		public void Insert(int idx, global::Poly2Tri.TriangulationPoint item)
		{
			mPoints.Insert(idx, item);
		}

		public override bool Remove(global::Poly2Tri.Point2D p)
		{
			return mPoints.Remove(p);
		}

		public bool Remove(global::Poly2Tri.TriangulationPoint p)
		{
			return mPoints.Remove(p);
		}

		public override void RemoveAt(int idx)
		{
			if (idx >= 0 && idx < Count)
			{
				mPoints.RemoveAt(idx);
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

		protected bool ConstrainPointToBounds(global::Poly2Tri.Point2D p)
		{
			double x = p.X;
			double y = p.Y;
			p.X = global::System.Math.Max(MinX, p.X);
			p.X = global::System.Math.Min(MaxX, p.X);
			p.Y = global::System.Math.Max(MinY, p.Y);
			p.Y = global::System.Math.Min(MaxY, p.Y);
			return p.X != x || p.Y != y;
		}

		protected bool ConstrainPointToBounds(global::Poly2Tri.TriangulationPoint p)
		{
			double x = p.X;
			double y = p.Y;
			p.X = global::System.Math.Max(MinX, p.X);
			p.X = global::System.Math.Min(MaxX, p.X);
			p.Y = global::System.Math.Max(MinY, p.Y);
			p.Y = global::System.Math.Min(MaxY, p.Y);
			return p.X != x || p.Y != y;
		}

		public virtual void AddTriangle(global::Poly2Tri.DelaunayTriangle t)
		{
			Triangles.Add(t);
		}

		public void AddTriangles(global::System.Collections.Generic.IEnumerable<global::Poly2Tri.DelaunayTriangle> list)
		{
			foreach (global::Poly2Tri.DelaunayTriangle item in list)
			{
				AddTriangle(item);
			}
		}

		public void ClearTriangles()
		{
			Triangles.Clear();
		}

		public virtual bool Initialize()
		{
			return true;
		}

		public virtual void Prepare(global::Poly2Tri.TriangulationContext tcx)
		{
			if (Triangles == null)
			{
				Triangles = new global::System.Collections.Generic.List<global::Poly2Tri.DelaunayTriangle>(Points.Count);
			}
			else
			{
				Triangles.Clear();
			}
			tcx.Points.AddRange(Points);
		}
	}
}
