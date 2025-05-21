namespace Poly2Tri
{
	public class TriangulationPoint : global::Poly2Tri.Point2D
	{
		public static readonly double kVertexCodeDefaultPrecision = 3.0;

		protected uint mVertexCode;

		public override double X
		{
			get
			{
				return mX;
			}
			set
			{
				if (value != mX)
				{
					mX = value;
					mVertexCode = CreateVertexCode(mX, mY, kVertexCodeDefaultPrecision);
				}
			}
		}

		public override double Y
		{
			get
			{
				return mY;
			}
			set
			{
				if (value != mY)
				{
					mY = value;
					mVertexCode = CreateVertexCode(mX, mY, kVertexCodeDefaultPrecision);
				}
			}
		}

		public uint VertexCode
		{
			get
			{
				return mVertexCode;
			}
		}

		public global::System.Collections.Generic.List<global::Poly2Tri.DTSweepConstraint> Edges { get; private set; }

		public bool HasEdges
		{
			get
			{
				return Edges != null;
			}
		}

		public TriangulationPoint(double x, double y)
			: this(x, y, kVertexCodeDefaultPrecision)
		{
		}

		public TriangulationPoint(double x, double y, double precision)
			: base(x, y)
		{
			mVertexCode = CreateVertexCode(x, y, precision);
		}

		public override string ToString()
		{
			return base.ToString() + ":{" + mVertexCode + "}";
		}

		public override int GetHashCode()
		{
			return (int)mVertexCode;
		}

		public override bool Equals(object obj)
		{
			global::Poly2Tri.TriangulationPoint triangulationPoint = obj as global::Poly2Tri.TriangulationPoint;
			if (triangulationPoint != null)
			{
				return mVertexCode == triangulationPoint.VertexCode;
			}
			return base.Equals(obj);
		}

		public override void Set(double x, double y)
		{
			if (x != mX || y != mY)
			{
				mX = x;
				mY = y;
				mVertexCode = CreateVertexCode(mX, mY, kVertexCodeDefaultPrecision);
			}
		}

		public static uint CreateVertexCode(double x, double y, double precision)
		{
			float value = (float)global::Poly2Tri.MathUtil.RoundWithPrecision(x, precision);
			float value2 = (float)global::Poly2Tri.MathUtil.RoundWithPrecision(y, precision);
			uint nInitialValue = global::Poly2Tri.MathUtil.Jenkins32Hash(global::System.BitConverter.GetBytes(value), 0u);
			return global::Poly2Tri.MathUtil.Jenkins32Hash(global::System.BitConverter.GetBytes(value2), nInitialValue);
		}

		public void AddEdge(global::Poly2Tri.DTSweepConstraint e)
		{
			if (Edges == null)
			{
				Edges = new global::System.Collections.Generic.List<global::Poly2Tri.DTSweepConstraint>();
			}
			Edges.Add(e);
		}

		public bool HasEdge(global::Poly2Tri.TriangulationPoint p)
		{
			global::Poly2Tri.DTSweepConstraint edge = null;
			return GetEdge(p, out edge);
		}

		public bool GetEdge(global::Poly2Tri.TriangulationPoint p, out global::Poly2Tri.DTSweepConstraint edge)
		{
			edge = null;
			if (Edges == null || Edges.Count < 1 || p == null || p.Equals(this))
			{
				return false;
			}
			foreach (global::Poly2Tri.DTSweepConstraint edge2 in Edges)
			{
				if ((edge2.P.Equals(this) && edge2.Q.Equals(p)) || (edge2.P.Equals(p) && edge2.Q.Equals(this)))
				{
					edge = edge2;
					return true;
				}
			}
			return false;
		}

		public static global::Poly2Tri.Point2D ToPoint2D(global::Poly2Tri.TriangulationPoint p)
		{
			return p;
		}
	}
}
