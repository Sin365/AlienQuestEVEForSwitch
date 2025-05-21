namespace Poly2Tri
{
	public class Point2DList : global::System.Collections.Generic.IEnumerable<global::Poly2Tri.Point2D>, global::System.Collections.Generic.ICollection<global::Poly2Tri.Point2D>, global::System.Collections.Generic.IList<global::Poly2Tri.Point2D>, global::System.Collections.IEnumerable
	{
		public enum WindingOrderType
		{
			CW = 0,
			CCW = 1,
			Unknown = 2,
			Default = 1
		}

		[global::System.Flags]
		public enum PolygonError : uint
		{
			None = 0u,
			NotEnoughVertices = 1u,
			NotConvex = 2u,
			NotSimple = 4u,
			AreaTooSmall = 8u,
			SidesTooCloseToParallel = 0x10u,
			TooThin = 0x20u,
			Degenerate = 0x40u,
			Unknown = 0x40000000u
		}

		public static readonly int kMaxPolygonVertices = 100000;

		public static readonly double kLinearSlop = 0.005;

		public static readonly double kAngularSlop = 1.0 / (90.0 * global::System.Math.PI);

		protected global::System.Collections.Generic.List<global::Poly2Tri.Point2D> mPoints = new global::System.Collections.Generic.List<global::Poly2Tri.Point2D>();

		protected global::Poly2Tri.Rect2D mBoundingBox = new global::Poly2Tri.Rect2D();

		protected global::Poly2Tri.Point2DList.WindingOrderType mWindingOrder = global::Poly2Tri.Point2DList.WindingOrderType.Unknown;

		protected double mEpsilon = global::Poly2Tri.MathUtil.EPSILON;

		public global::Poly2Tri.Rect2D BoundingBox
		{
			get
			{
				return mBoundingBox;
			}
		}

		public global::Poly2Tri.Point2DList.WindingOrderType WindingOrder
		{
			get
			{
				return mWindingOrder;
			}
			set
			{
				if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown)
				{
					mWindingOrder = CalculateWindingOrder();
				}
				if (value != mWindingOrder)
				{
					mPoints.Reverse();
					mWindingOrder = value;
				}
			}
		}

		public double Epsilon
		{
			get
			{
				return mEpsilon;
			}
		}

		public global::Poly2Tri.Point2D this[int index]
		{
			get
			{
				return mPoints[index];
			}
			set
			{
				mPoints[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return mPoints.Count;
			}
		}

		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public Point2DList()
		{
		}

		public Point2DList(int capacity)
		{
			mPoints.Capacity = capacity;
		}

		public Point2DList(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> l)
		{
			AddRange(l.GetEnumerator(), global::Poly2Tri.Point2DList.WindingOrderType.Unknown);
		}

		public Point2DList(global::Poly2Tri.Point2DList l)
		{
			int count = l.Count;
			for (int i = 0; i < count; i++)
			{
				mPoints.Add(l[i]);
			}
			mBoundingBox.Set(l.BoundingBox);
			mEpsilon = l.Epsilon;
			mWindingOrder = l.WindingOrder;
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return mPoints.GetEnumerator();
		}

		global::System.Collections.Generic.IEnumerator<global::Poly2Tri.Point2D> global::System.Collections.Generic.IEnumerable<global::Poly2Tri.Point2D>.GetEnumerator()
		{
			return new global::Poly2Tri.Point2DEnumerator(mPoints);
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = 0; i < Count; i++)
			{
				stringBuilder.Append(this[i].ToString());
				if (i < Count - 1)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}

		public void Clear()
		{
			mPoints.Clear();
			mBoundingBox.Clear();
			mEpsilon = global::Poly2Tri.MathUtil.EPSILON;
			mWindingOrder = global::Poly2Tri.Point2DList.WindingOrderType.Unknown;
		}

		public int IndexOf(global::Poly2Tri.Point2D p)
		{
			return mPoints.IndexOf(p);
		}

		public virtual void Add(global::Poly2Tri.Point2D p)
		{
			Add(p, -1, true);
		}

		protected virtual void Add(global::Poly2Tri.Point2D p, int idx, bool bCalcWindingOrderAndEpsilon)
		{
			if (idx < 0)
			{
				mPoints.Add(p);
			}
			else
			{
				mPoints.Insert(idx, p);
			}
			mBoundingBox.AddPoint(p);
			if (bCalcWindingOrderAndEpsilon)
			{
				if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown)
				{
					mWindingOrder = CalculateWindingOrder();
				}
				mEpsilon = CalculateEpsilon();
			}
		}

		public virtual void AddRange(global::Poly2Tri.Point2DList l)
		{
			AddRange(l.mPoints.GetEnumerator(), l.WindingOrder);
		}

		public virtual void AddRange(global::System.Collections.Generic.IEnumerator<global::Poly2Tri.Point2D> iter, global::Poly2Tri.Point2DList.WindingOrderType windingOrder)
		{
			if (iter == null)
			{
				return;
			}
			if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown && Count == 0)
			{
				mWindingOrder = windingOrder;
			}
			bool flag = WindingOrder != global::Poly2Tri.Point2DList.WindingOrderType.Unknown && windingOrder != global::Poly2Tri.Point2DList.WindingOrderType.Unknown && WindingOrder != windingOrder;
			bool flag2 = true;
			int count = mPoints.Count;
			iter.Reset();
			while (iter.MoveNext())
			{
				if (!flag2)
				{
					flag2 = true;
					mPoints.Add(iter.Current);
				}
				else if (flag)
				{
					mPoints.Insert(count, iter.Current);
				}
				else
				{
					mPoints.Add(iter.Current);
				}
				mBoundingBox.AddPoint(iter.Current);
			}
			if (mWindingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown && windingOrder == global::Poly2Tri.Point2DList.WindingOrderType.Unknown)
			{
				mWindingOrder = CalculateWindingOrder();
			}
			mEpsilon = CalculateEpsilon();
		}

		public virtual void Insert(int idx, global::Poly2Tri.Point2D item)
		{
			Add(item, idx, true);
		}

		public virtual bool Remove(global::Poly2Tri.Point2D p)
		{
			if (mPoints.Remove(p))
			{
				CalculateBounds();
				mEpsilon = CalculateEpsilon();
				return true;
			}
			return false;
		}

		public virtual void RemoveAt(int idx)
		{
			if (idx >= 0 && idx < Count)
			{
				mPoints.RemoveAt(idx);
				CalculateBounds();
				mEpsilon = CalculateEpsilon();
			}
		}

		public virtual void RemoveRange(int idxStart, int count)
		{
			if (idxStart >= 0 && idxStart < Count && count != 0)
			{
				mPoints.RemoveRange(idxStart, count);
				CalculateBounds();
				mEpsilon = CalculateEpsilon();
			}
		}

		public bool Contains(global::Poly2Tri.Point2D p)
		{
			return mPoints.Contains(p);
		}

		public void CopyTo(global::Poly2Tri.Point2D[] array, int arrayIndex)
		{
			int num = global::System.Math.Min(Count, array.Length - arrayIndex);
			for (int i = 0; i < num; i++)
			{
				array[arrayIndex + i] = mPoints[i];
			}
		}

		public void CalculateBounds()
		{
			mBoundingBox.Clear();
			foreach (global::Poly2Tri.Point2D mPoint in mPoints)
			{
				mBoundingBox.AddPoint(mPoint);
			}
		}

		public double CalculateEpsilon()
		{
			return global::System.Math.Max(global::System.Math.Min(mBoundingBox.Width, mBoundingBox.Height) * 0.0010000000474974513, global::Poly2Tri.MathUtil.EPSILON);
		}

		public global::Poly2Tri.Point2DList.WindingOrderType CalculateWindingOrder()
		{
			double signedArea = GetSignedArea();
			if (signedArea < 0.0)
			{
				return global::Poly2Tri.Point2DList.WindingOrderType.CW;
			}
			if (signedArea > 0.0)
			{
				return global::Poly2Tri.Point2DList.WindingOrderType.CCW;
			}
			return global::Poly2Tri.Point2DList.WindingOrderType.Unknown;
		}

		public int NextIndex(int index)
		{
			if (index == Count - 1)
			{
				return 0;
			}
			return index + 1;
		}

		public int PreviousIndex(int index)
		{
			if (index == 0)
			{
				return Count - 1;
			}
			return index - 1;
		}

		public double GetSignedArea()
		{
			double num = 0.0;
			for (int i = 0; i < Count; i++)
			{
				int index = (i + 1) % Count;
				num += this[i].X * this[index].Y;
				num -= this[i].Y * this[index].X;
			}
			return num / 2.0;
		}

		public double GetArea()
		{
			double num = 0.0;
			for (int i = 0; i < Count; i++)
			{
				int index = (i + 1) % Count;
				num += this[i].X * this[index].Y;
				num -= this[i].Y * this[index].X;
			}
			num /= 2.0;
			return (!(num < 0.0)) ? num : (0.0 - num);
		}

		public global::Poly2Tri.Point2D GetCentroid()
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D();
			double num = 0.0;
			global::Poly2Tri.Point2D point2D2 = new global::Poly2Tri.Point2D();
			for (int i = 0; i < Count; i++)
			{
				global::Poly2Tri.Point2D point2D3 = point2D2;
				global::Poly2Tri.Point2D point2D4 = this[i];
				global::Poly2Tri.Point2D point2D5 = ((i + 1 >= Count) ? this[0] : this[i + 1]);
				global::Poly2Tri.Point2D lhs = point2D4 - point2D3;
				global::Poly2Tri.Point2D rhs = point2D5 - point2D3;
				double num2 = global::Poly2Tri.Point2D.Cross(lhs, rhs);
				double num3 = 0.5 * num2;
				num += num3;
				point2D += num3 * (1.0 / 3.0) * (point2D3 + point2D4 + point2D5);
			}
			return point2D * (1.0 / num);
		}

		public void Translate(global::Poly2Tri.Point2D vector)
		{
			for (int i = 0; i < Count; i++)
			{
				global::Poly2Tri.Point2DList point2DList2;
				global::Poly2Tri.Point2DList point2DList = (point2DList2 = this);
				int index2;
				int index = (index2 = i);
				global::Poly2Tri.Point2D point2D = point2DList2[index2];
				point2DList[index] = point2D + vector;
			}
		}

		public void Scale(global::Poly2Tri.Point2D value)
		{
			for (int i = 0; i < Count; i++)
			{
				global::Poly2Tri.Point2DList point2DList2;
				global::Poly2Tri.Point2DList point2DList = (point2DList2 = this);
				int index2;
				int index = (index2 = i);
				global::Poly2Tri.Point2D point2D = point2DList2[index2];
				point2DList[index] = point2D * value;
			}
		}

		public void Rotate(double radians)
		{
			double num = global::System.Math.Cos(radians);
			double num2 = global::System.Math.Sin(radians);
			foreach (global::Poly2Tri.Point2D mPoint in mPoints)
			{
				double x = mPoint.X;
				mPoint.X = x * num - mPoint.Y * num2;
				mPoint.Y = x * num2 + mPoint.Y * num;
			}
		}

		public bool IsDegenerate()
		{
			if (Count < 3)
			{
				return false;
			}
			if (Count < 3)
			{
				return false;
			}
			for (int i = 0; i < Count; i++)
			{
				int index = PreviousIndex(i);
				if (mPoints[index].Equals(mPoints[i], Epsilon))
				{
					return true;
				}
				int index2 = PreviousIndex(index);
				global::Poly2Tri.Orientation orientation = global::Poly2Tri.TriangulationUtil.Orient2d(mPoints[index2], mPoints[index], mPoints[i]);
				if (orientation == global::Poly2Tri.Orientation.Collinear)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsConvex()
		{
			bool flag = false;
			for (int i = 0; i < Count; i++)
			{
				int index = ((i != 0) ? (i - 1) : (Count - 1));
				int index2 = i;
				int index3 = ((i != Count - 1) ? (i + 1) : 0);
				double num = this[index2].X - this[index].X;
				double num2 = this[index2].Y - this[index].Y;
				double num3 = this[index3].X - this[index2].X;
				double num4 = this[index3].Y - this[index2].Y;
				double num5 = num * num4 - num3 * num2;
				bool flag2 = ((num5 >= 0.0) ? true : false);
				if (i == 0)
				{
					flag = flag2;
				}
				else if (flag != flag2)
				{
					return false;
				}
			}
			return true;
		}

		public bool IsSimple()
		{
			for (int i = 0; i < Count; i++)
			{
				int index = NextIndex(i);
				for (int j = i + 1; j < Count; j++)
				{
					int index2 = NextIndex(j);
					global::Poly2Tri.Point2D pIntersectionPt = null;
					if (global::Poly2Tri.TriangulationUtil.LinesIntersect2D(mPoints[i], mPoints[index], mPoints[j], mPoints[index2], ref pIntersectionPt, mEpsilon))
					{
						return false;
					}
				}
			}
			return true;
		}

		public global::Poly2Tri.Point2DList.PolygonError CheckPolygon()
		{
			global::Poly2Tri.Point2DList.PolygonError polygonError = global::Poly2Tri.Point2DList.PolygonError.None;
			if (Count < 3 || Count > kMaxPolygonVertices)
			{
				return polygonError | global::Poly2Tri.Point2DList.PolygonError.NotEnoughVertices;
			}
			if (IsDegenerate())
			{
				polygonError |= global::Poly2Tri.Point2DList.PolygonError.Degenerate;
			}
			if (!IsSimple())
			{
				polygonError |= global::Poly2Tri.Point2DList.PolygonError.NotSimple;
			}
			if (GetArea() < global::Poly2Tri.MathUtil.EPSILON)
			{
				polygonError |= global::Poly2Tri.Point2DList.PolygonError.AreaTooSmall;
			}
			if ((polygonError & global::Poly2Tri.Point2DList.PolygonError.NotSimple) != global::Poly2Tri.Point2DList.PolygonError.NotSimple)
			{
				bool flag = false;
				global::Poly2Tri.Point2DList.WindingOrderType windingOrder = global::Poly2Tri.Point2DList.WindingOrderType.CCW;
				global::Poly2Tri.Point2DList.WindingOrderType windingOrderType = global::Poly2Tri.Point2DList.WindingOrderType.CW;
				if (WindingOrder == windingOrderType)
				{
					WindingOrder = windingOrder;
					flag = true;
				}
				global::Poly2Tri.Point2D[] array = new global::Poly2Tri.Point2D[Count];
				global::Poly2Tri.Point2DList point2DList = new global::Poly2Tri.Point2DList(Count);
				for (int i = 0; i < Count; i++)
				{
					point2DList.Add(new global::Poly2Tri.Point2D(this[i].X, this[i].Y));
					int index = i;
					int index2 = NextIndex(i);
					global::Poly2Tri.Point2D lhs = new global::Poly2Tri.Point2D(this[index2].X - this[index].X, this[index2].Y - this[index].Y);
					array[i] = global::Poly2Tri.Point2D.Perpendicular(lhs, 1.0);
					array[i].Normalize();
				}
				for (int j = 0; j < Count; j++)
				{
					int num = PreviousIndex(j);
					double a = global::Poly2Tri.Point2D.Cross(array[num], array[j]);
					a = global::Poly2Tri.MathUtil.Clamp(a, -1.0, 1.0);
					float value = (float)global::System.Math.Asin(a);
					if ((double)global::System.Math.Abs(value) <= kAngularSlop)
					{
						polygonError |= global::Poly2Tri.Point2DList.PolygonError.SidesTooCloseToParallel;
						break;
					}
				}
				if (flag)
				{
					WindingOrder = windingOrderType;
				}
			}
			return polygonError;
		}

		public static string GetErrorString(global::Poly2Tri.Point2DList.PolygonError error)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(256);
			if (error == global::Poly2Tri.Point2DList.PolygonError.None)
			{
				stringBuilder.AppendFormat("No errors.\n");
			}
			else
			{
				if ((error & global::Poly2Tri.Point2DList.PolygonError.NotEnoughVertices) == global::Poly2Tri.Point2DList.PolygonError.NotEnoughVertices)
				{
					stringBuilder.AppendFormat("NotEnoughVertices: must have between 3 and {0} vertices.\n", kMaxPolygonVertices);
				}
				if ((error & global::Poly2Tri.Point2DList.PolygonError.NotConvex) == global::Poly2Tri.Point2DList.PolygonError.NotConvex)
				{
					stringBuilder.AppendFormat("NotConvex: Polygon is not convex.\n");
				}
				if ((error & global::Poly2Tri.Point2DList.PolygonError.NotSimple) == global::Poly2Tri.Point2DList.PolygonError.NotSimple)
				{
					stringBuilder.AppendFormat("NotSimple: Polygon is not simple (i.e. it intersects itself).\n");
				}
				if ((error & global::Poly2Tri.Point2DList.PolygonError.AreaTooSmall) == global::Poly2Tri.Point2DList.PolygonError.AreaTooSmall)
				{
					stringBuilder.AppendFormat("AreaTooSmall: Polygon's area is too small.\n");
				}
				if ((error & global::Poly2Tri.Point2DList.PolygonError.SidesTooCloseToParallel) == global::Poly2Tri.Point2DList.PolygonError.SidesTooCloseToParallel)
				{
					stringBuilder.AppendFormat("SidesTooCloseToParallel: Polygon's sides are too close to parallel.\n");
				}
				if ((error & global::Poly2Tri.Point2DList.PolygonError.TooThin) == global::Poly2Tri.Point2DList.PolygonError.TooThin)
				{
					stringBuilder.AppendFormat("TooThin: Polygon is too thin or core shape generation would move edge past centroid.\n");
				}
				if ((error & global::Poly2Tri.Point2DList.PolygonError.Degenerate) == global::Poly2Tri.Point2DList.PolygonError.Degenerate)
				{
					stringBuilder.AppendFormat("Degenerate: Polygon is degenerate (contains collinear points or duplicate coincident points).\n");
				}
				if ((error & global::Poly2Tri.Point2DList.PolygonError.Unknown) == global::Poly2Tri.Point2DList.PolygonError.Unknown)
				{
					stringBuilder.AppendFormat("Unknown: Unknown Polygon error!.\n");
				}
			}
			return stringBuilder.ToString();
		}

		public void RemoveDuplicateNeighborPoints()
		{
			int num = Count;
			int num2 = num - 1;
			int num3 = 0;
			while (num > 1 && num3 < num)
			{
				if (mPoints[num2].Equals(mPoints[num3]))
				{
					int index = global::System.Math.Max(num2, num3);
					mPoints.RemoveAt(index);
					num--;
					if (num2 >= num)
					{
						num2 = num - 1;
					}
				}
				else
				{
					num2 = NextIndex(num2);
					num3++;
				}
			}
		}

		public void Simplify()
		{
			Simplify(0.0);
		}

		public void Simplify(double bias)
		{
			if (Count < 3)
			{
				return;
			}
			int num = 0;
			int num2 = Count;
			double num3 = bias * bias;
			while (num < num2 && num2 >= 3)
			{
				int index = PreviousIndex(num);
				int index2 = NextIndex(num);
				global::Poly2Tri.Point2D point2D = this[index];
				global::Poly2Tri.Point2D point2D2 = this[num];
				global::Poly2Tri.Point2D pc = this[index2];
				if ((point2D - point2D2).MagnitudeSquared() <= num3)
				{
					RemoveAt(num);
					num2--;
					continue;
				}
				global::Poly2Tri.Orientation orientation = global::Poly2Tri.TriangulationUtil.Orient2d(point2D, point2D2, pc);
				if (orientation == global::Poly2Tri.Orientation.Collinear)
				{
					RemoveAt(num);
					num2--;
				}
				else
				{
					num++;
				}
			}
		}

		public void MergeParallelEdges(double tolerance)
		{
			if (Count <= 3)
			{
				return;
			}
			bool[] array = new bool[Count];
			int num = Count;
			for (int i = 0; i < Count; i++)
			{
				int index = ((i != 0) ? (i - 1) : (Count - 1));
				int index2 = i;
				int index3 = ((i != Count - 1) ? (i + 1) : 0);
				double num2 = this[index2].X - this[index].X;
				double num3 = this[index2].Y - this[index].Y;
				double num4 = this[index3].Y - this[index2].X;
				double num5 = this[index3].Y - this[index2].Y;
				double num6 = global::System.Math.Sqrt(num2 * num2 + num3 * num3);
				double num7 = global::System.Math.Sqrt(num4 * num4 + num5 * num5);
				if ((!(num6 > 0.0) || !(num7 > 0.0)) && num > 3)
				{
					array[i] = true;
					num--;
				}
				num2 /= num6;
				num3 /= num6;
				num4 /= num7;
				num5 /= num7;
				double value = num2 * num5 - num4 * num3;
				double num8 = num2 * num4 + num3 * num5;
				if (global::System.Math.Abs(value) < tolerance && num8 > 0.0 && num > 3)
				{
					array[i] = true;
					num--;
				}
				else
				{
					array[i] = false;
				}
			}
			if (num == Count || num == 0)
			{
				return;
			}
			int num9 = 0;
			global::Poly2Tri.Point2DList point2DList = new global::Poly2Tri.Point2DList(this);
			Clear();
			for (int j = 0; j < point2DList.Count; j++)
			{
				if (!array[j] && num != 0 && num9 != num)
				{
					if (num9 >= num)
					{
						throw new global::System.Exception("Point2DList::MergeParallelEdges - currIndex[ " + num9.ToString() + "] >= newNVertices[" + num + "]");
					}
					mPoints.Add(point2DList[j]);
					mBoundingBox.AddPoint(point2DList[j]);
					num9++;
				}
			}
			mWindingOrder = CalculateWindingOrder();
			mEpsilon = CalculateEpsilon();
		}

		public void ProjectToAxis(global::Poly2Tri.Point2D axis, out double min, out double max)
		{
			max = (min = global::Poly2Tri.Point2D.Dot(axis, this[0]));
			for (int i = 0; i < Count; i++)
			{
				double num = global::Poly2Tri.Point2D.Dot(this[i], axis);
				if (num < min)
				{
					min = num;
				}
				else if (num > max)
				{
					max = num;
				}
			}
		}
	}
}
