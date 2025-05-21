namespace Poly2Tri
{
	public class Rect2D
	{
		private double mMinX;

		private double mMaxX;

		private double mMinY;

		private double mMaxY;

		public double MinX
		{
			get
			{
				return mMinX;
			}
			set
			{
				mMinX = value;
			}
		}

		public double MaxX
		{
			get
			{
				return mMaxX;
			}
			set
			{
				mMaxX = value;
			}
		}

		public double MinY
		{
			get
			{
				return mMinY;
			}
			set
			{
				mMinY = value;
			}
		}

		public double MaxY
		{
			get
			{
				return mMaxY;
			}
			set
			{
				mMaxY = value;
			}
		}

		public double Left
		{
			get
			{
				return mMinX;
			}
			set
			{
				mMinX = value;
			}
		}

		public double Right
		{
			get
			{
				return mMaxX;
			}
			set
			{
				mMaxX = value;
			}
		}

		public double Top
		{
			get
			{
				return mMaxY;
			}
			set
			{
				mMaxY = value;
			}
		}

		public double Bottom
		{
			get
			{
				return mMinY;
			}
			set
			{
				mMinY = value;
			}
		}

		public double Width
		{
			get
			{
				return Right - Left;
			}
		}

		public double Height
		{
			get
			{
				return Top - Bottom;
			}
		}

		public bool Empty
		{
			get
			{
				return Left == Right || Top == Bottom;
			}
		}

		public Rect2D()
		{
			Clear();
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			global::Poly2Tri.Rect2D rect2D = obj as global::Poly2Tri.Rect2D;
			if (rect2D != null)
			{
				return Equals(rect2D);
			}
			return base.Equals(obj);
		}

		public bool Equals(global::Poly2Tri.Rect2D r)
		{
			return Equals(r, global::Poly2Tri.MathUtil.EPSILON);
		}

		public bool Equals(global::Poly2Tri.Rect2D r, double epsilon)
		{
			if (!global::Poly2Tri.MathUtil.AreValuesEqual(MinX, r.MinX, epsilon))
			{
				return false;
			}
			if (!global::Poly2Tri.MathUtil.AreValuesEqual(MaxX, r.MaxX))
			{
				return false;
			}
			if (!global::Poly2Tri.MathUtil.AreValuesEqual(MinY, r.MinY, epsilon))
			{
				return false;
			}
			if (!global::Poly2Tri.MathUtil.AreValuesEqual(MaxY, r.MaxY, epsilon))
			{
				return false;
			}
			return true;
		}

		public void Clear()
		{
			MinX = double.MaxValue;
			MaxX = double.MinValue;
			MinY = double.MaxValue;
			MaxY = double.MinValue;
		}

		public void Set(double xmin, double xmax, double ymin, double ymax)
		{
			MinX = xmin;
			MaxX = xmax;
			MinY = ymin;
			MaxY = ymax;
			Normalize();
		}

		public void Set(global::Poly2Tri.Rect2D b)
		{
			MinX = b.MinX;
			MaxX = b.MaxX;
			MinY = b.MinY;
			MaxY = b.MaxY;
		}

		public void SetSize(double w, double h)
		{
			Right = Left + w;
			Top = Bottom + h;
		}

		public bool Contains(double x, double y)
		{
			return x > Left && y > Bottom && x < Right && y < Top;
		}

		public bool Contains(global::Poly2Tri.Point2D p)
		{
			return Contains(p.X, p.Y);
		}

		public bool Contains(global::Poly2Tri.Rect2D r)
		{
			return Left < r.Left && Right > r.Right && Top < r.Top && Bottom > r.Bottom;
		}

		public bool ContainsInclusive(double x, double y)
		{
			return x >= Left && y >= Top && x <= Right && y <= Bottom;
		}

		public bool ContainsInclusive(double x, double y, double epsilon)
		{
			return x + epsilon >= Left && y + epsilon >= Top && x - epsilon <= Right && y - epsilon <= Bottom;
		}

		public bool ContainsInclusive(global::Poly2Tri.Point2D p)
		{
			return ContainsInclusive(p.X, p.Y);
		}

		public bool ContainsInclusive(global::Poly2Tri.Point2D p, double epsilon)
		{
			return ContainsInclusive(p.X, p.Y, epsilon);
		}

		public bool ContainsInclusive(global::Poly2Tri.Rect2D r)
		{
			return Left <= r.Left && Right >= r.Right && Top <= r.Top && Bottom >= r.Bottom;
		}

		public bool ContainsInclusive(global::Poly2Tri.Rect2D r, double epsilon)
		{
			return Left - epsilon <= r.Left && Right + epsilon >= r.Right && Top - epsilon <= r.Top && Bottom + epsilon >= r.Bottom;
		}

		public bool Intersects(global::Poly2Tri.Rect2D r)
		{
			return Right > r.Left && Left < r.Right && Bottom < r.Top && Top > r.Bottom;
		}

		public global::Poly2Tri.Point2D GetCenter()
		{
			return new global::Poly2Tri.Point2D((Left + Right) / 2.0, (Bottom + Top) / 2.0);
		}

		public bool IsNormalized()
		{
			return Right >= Left && Bottom <= Top;
		}

		public void Normalize()
		{
			if (Left > Right)
			{
				global::Poly2Tri.MathUtil.Swap(ref mMinX, ref mMaxX);
			}
			if (Bottom < Top)
			{
				global::Poly2Tri.MathUtil.Swap(ref mMinY, ref mMaxY);
			}
		}

		public void AddPoint(global::Poly2Tri.Point2D p)
		{
			MinX = global::System.Math.Min(MinX, p.X);
			MaxX = global::System.Math.Max(MaxX, p.X);
			MinY = global::System.Math.Min(MinY, p.Y);
			MaxY = global::System.Math.Max(MaxY, p.Y);
		}

		public void Inflate(double w, double h)
		{
			Left -= w;
			Top += h;
			Right += w;
			Bottom -= h;
		}

		public void Inflate(double left, double top, double right, double bottom)
		{
			Left -= left;
			Top += top;
			Right += right;
			Bottom -= bottom;
		}

		public void Offset(double w, double h)
		{
			Left += w;
			Top += h;
			Right += w;
			Bottom += h;
		}

		public void SetPosition(double x, double y)
		{
			double num = Right - Left;
			double num2 = Bottom - Top;
			Left = x;
			Bottom = y;
			Right = x + num;
			Top = y + num2;
		}

		public bool Intersection(global::Poly2Tri.Rect2D r1, global::Poly2Tri.Rect2D r2)
		{
			if (!global::Poly2Tri.TriangulationUtil.RectsIntersect(r1, r2))
			{
				double num = (Bottom = 0.0);
				num = (Top = num);
				num = (Right = num);
				Left = num;
				return false;
			}
			Left = ((!(r1.Left > r2.Left)) ? r2.Left : r1.Left);
			Top = ((!(r1.Top < r2.Top)) ? r2.Top : r1.Top);
			Right = ((!(r1.Right < r2.Right)) ? r2.Right : r1.Right);
			Bottom = ((!(r1.Bottom > r2.Bottom)) ? r2.Bottom : r1.Bottom);
			return true;
		}

		public void Union(global::Poly2Tri.Rect2D r1, global::Poly2Tri.Rect2D r2)
		{
			if (r2.Right == r2.Left || r2.Bottom == r2.Top)
			{
				Set(r1);
				return;
			}
			if (r1.Right == r1.Left || r1.Bottom == r1.Top)
			{
				Set(r2);
				return;
			}
			Left = ((!(r1.Left < r2.Left)) ? r2.Left : r1.Left);
			Top = ((!(r1.Top > r2.Top)) ? r2.Top : r1.Top);
			Right = ((!(r1.Right > r2.Right)) ? r2.Right : r1.Right);
			Bottom = ((!(r1.Bottom < r2.Bottom)) ? r2.Bottom : r1.Bottom);
		}
	}
}
