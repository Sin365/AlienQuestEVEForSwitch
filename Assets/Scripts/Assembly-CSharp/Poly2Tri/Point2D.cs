namespace Poly2Tri
{
	public class Point2D : global::System.IComparable<global::Poly2Tri.Point2D>
	{
		protected double mX;

		protected double mY;

		public virtual double X
		{
			get
			{
				return mX;
			}
			set
			{
				mX = value;
			}
		}

		public virtual double Y
		{
			get
			{
				return mY;
			}
			set
			{
				mY = value;
			}
		}

		public float Xf
		{
			get
			{
				return (float)X;
			}
		}

		public float Yf
		{
			get
			{
				return (float)Y;
			}
		}

		public Point2D()
		{
			mX = 0.0;
			mY = 0.0;
		}

		public Point2D(double x, double y)
		{
			mX = x;
			mY = y;
		}

		public Point2D(global::Poly2Tri.Point2D p)
		{
			mX = p.X;
			mY = p.Y;
		}

		public override string ToString()
		{
			return "[" + X + "," + Y + "]";
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			global::Poly2Tri.Point2D point2D = obj as global::Poly2Tri.Point2D;
			if (point2D != null)
			{
				return Equals(point2D);
			}
			return base.Equals(obj);
		}

		public bool Equals(global::Poly2Tri.Point2D p)
		{
			return Equals(p, 0.0);
		}

		public bool Equals(global::Poly2Tri.Point2D p, double epsilon)
		{
			if (p == null || !global::Poly2Tri.MathUtil.AreValuesEqual(X, p.X, epsilon) || !global::Poly2Tri.MathUtil.AreValuesEqual(Y, p.Y, epsilon))
			{
				return false;
			}
			return true;
		}

		public int CompareTo(global::Poly2Tri.Point2D other)
		{
			if (Y < other.Y)
			{
				return -1;
			}
			if (Y > other.Y)
			{
				return 1;
			}
			if (X < other.X)
			{
				return -1;
			}
			if (X > other.X)
			{
				return 1;
			}
			return 0;
		}

		public virtual void Set(double x, double y)
		{
			X = x;
			Y = y;
		}

		public virtual void Set(global::Poly2Tri.Point2D p)
		{
			X = p.X;
			Y = p.Y;
		}

		public void Add(global::Poly2Tri.Point2D p)
		{
			X += p.X;
			Y += p.Y;
		}

		public void Add(double scalar)
		{
			X += scalar;
			Y += scalar;
		}

		public void Subtract(global::Poly2Tri.Point2D p)
		{
			X -= p.X;
			Y -= p.Y;
		}

		public void Subtract(double scalar)
		{
			X -= scalar;
			Y -= scalar;
		}

		public void Multiply(global::Poly2Tri.Point2D p)
		{
			X *= p.X;
			Y *= p.Y;
		}

		public void Multiply(double scalar)
		{
			X *= scalar;
			Y *= scalar;
		}

		public void Divide(global::Poly2Tri.Point2D p)
		{
			X /= p.X;
			Y /= p.Y;
		}

		public void Divide(double scalar)
		{
			X /= scalar;
			Y /= scalar;
		}

		public void Negate()
		{
			X = 0.0 - X;
			Y = 0.0 - Y;
		}

		public double Magnitude()
		{
			return global::System.Math.Sqrt(X * X + Y * Y);
		}

		public double MagnitudeSquared()
		{
			return X * X + Y * Y;
		}

		public double MagnitudeReciprocal()
		{
			return 1.0 / Magnitude();
		}

		public void Normalize()
		{
			Multiply(MagnitudeReciprocal());
		}

		public double Dot(global::Poly2Tri.Point2D p)
		{
			return X * p.X + Y * p.Y;
		}

		public double Cross(global::Poly2Tri.Point2D p)
		{
			return X * p.Y - Y * p.X;
		}

		public void Clamp(global::Poly2Tri.Point2D low, global::Poly2Tri.Point2D high)
		{
			X = global::System.Math.Max(low.X, global::System.Math.Min(X, high.X));
			Y = global::System.Math.Max(low.Y, global::System.Math.Min(Y, high.Y));
		}

		public void Abs()
		{
			X = global::System.Math.Abs(X);
			Y = global::System.Math.Abs(Y);
		}

		public void Reciprocal()
		{
			if (X != 0.0 && Y != 0.0)
			{
				X = 1.0 / X;
				Y = 1.0 / Y;
			}
		}

		public void Translate(global::Poly2Tri.Point2D vector)
		{
			Add(vector);
		}

		public void Translate(double x, double y)
		{
			X += x;
			Y += y;
		}

		public void Scale(global::Poly2Tri.Point2D vector)
		{
			Multiply(vector);
		}

		public void Scale(double scalar)
		{
			Multiply(scalar);
		}

		public void Scale(double x, double y)
		{
			X *= x;
			Y *= y;
		}

		public void Rotate(double radians)
		{
			double num = global::System.Math.Cos(radians);
			double num2 = global::System.Math.Sin(radians);
			double x = X;
			double y = Y;
			X = x * num - y * num2;
			Y = x * num2 + y * num;
		}

		public void RotateDegrees(double degrees)
		{
			double radians = degrees * global::System.Math.PI / 180.0;
			Rotate(radians);
		}

		public static double Dot(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			return lhs.X * rhs.X + lhs.Y * rhs.Y;
		}

		public static double Cross(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			return lhs.X * rhs.Y - lhs.Y * rhs.X;
		}

		public static global::Poly2Tri.Point2D Clamp(global::Poly2Tri.Point2D a, global::Poly2Tri.Point2D low, global::Poly2Tri.Point2D high)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(a);
			point2D.Clamp(low, high);
			return point2D;
		}

		public static global::Poly2Tri.Point2D Min(global::Poly2Tri.Point2D a, global::Poly2Tri.Point2D b)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D();
			point2D.X = global::System.Math.Min(a.X, b.X);
			point2D.Y = global::System.Math.Min(a.Y, b.Y);
			return point2D;
		}

		public static global::Poly2Tri.Point2D Max(global::Poly2Tri.Point2D a, global::Poly2Tri.Point2D b)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D();
			point2D.X = global::System.Math.Max(a.X, b.X);
			point2D.Y = global::System.Math.Max(a.Y, b.Y);
			return point2D;
		}

		public static global::Poly2Tri.Point2D Abs(global::Poly2Tri.Point2D a)
		{
			return new global::Poly2Tri.Point2D(global::System.Math.Abs(a.X), global::System.Math.Abs(a.Y));
		}

		public static global::Poly2Tri.Point2D Reciprocal(global::Poly2Tri.Point2D a)
		{
			return new global::Poly2Tri.Point2D(1.0 / a.X, 1.0 / a.Y);
		}

		public static global::Poly2Tri.Point2D Perpendicular(global::Poly2Tri.Point2D lhs, double scalar)
		{
			return new global::Poly2Tri.Point2D(lhs.Y * scalar, lhs.X * (0.0 - scalar));
		}

		public static global::Poly2Tri.Point2D Perpendicular(double scalar, global::Poly2Tri.Point2D rhs)
		{
			return new global::Poly2Tri.Point2D((0.0 - scalar) * rhs.Y, scalar * rhs.X);
		}

		public static global::Poly2Tri.Point2D operator +(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Add(rhs);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator +(global::Poly2Tri.Point2D lhs, double scalar)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Add(scalar);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator -(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Subtract(rhs);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator -(global::Poly2Tri.Point2D lhs, double scalar)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Subtract(scalar);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator *(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Multiply(rhs);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator *(global::Poly2Tri.Point2D lhs, double scalar)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Multiply(scalar);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator *(double scalar, global::Poly2Tri.Point2D lhs)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Multiply(scalar);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator /(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Divide(rhs);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator /(global::Poly2Tri.Point2D lhs, double scalar)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(lhs);
			point2D.Divide(scalar);
			return point2D;
		}

		public static global::Poly2Tri.Point2D operator -(global::Poly2Tri.Point2D p)
		{
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(p);
			point2D.Negate();
			return point2D;
		}

		public static bool operator <(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			return lhs.CompareTo(rhs) == -1;
		}

		public static bool operator >(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			return lhs.CompareTo(rhs) == 1;
		}

		public static bool operator <=(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			return (lhs.CompareTo(rhs) <= 0) ? true : false;
		}

		public static bool operator >=(global::Poly2Tri.Point2D lhs, global::Poly2Tri.Point2D rhs)
		{
			return (lhs.CompareTo(rhs) >= 0) ? true : false;
		}
	}
}
