namespace Poly2Tri
{
	public class TriangulationConstraint : global::Poly2Tri.Edge
	{
		private uint mContraintCode;

		public global::Poly2Tri.TriangulationPoint P
		{
			get
			{
				return mP as global::Poly2Tri.TriangulationPoint;
			}
			set
			{
				if (value != null && mP != value)
				{
					mP = value;
					CalculateContraintCode();
				}
			}
		}

		public global::Poly2Tri.TriangulationPoint Q
		{
			get
			{
				return mQ as global::Poly2Tri.TriangulationPoint;
			}
			set
			{
				if (value != null && mQ != value)
				{
					mQ = value;
					CalculateContraintCode();
				}
			}
		}

		public uint ConstraintCode
		{
			get
			{
				return mContraintCode;
			}
		}

		public TriangulationConstraint(global::Poly2Tri.TriangulationPoint p1, global::Poly2Tri.TriangulationPoint p2)
		{
			mP = p1;
			mQ = p2;
			if (p1.Y > p2.Y)
			{
				mQ = p1;
				mP = p2;
			}
			else if (p1.Y == p2.Y)
			{
				if (p1.X > p2.X)
				{
					mQ = p1;
					mP = p2;
				}
				else if (p1.X != p2.X)
				{
				}
			}
			CalculateContraintCode();
		}

		public override string ToString()
		{
			return "[P=" + P.ToString() + ", Q=" + Q.ToString() + " : {" + mContraintCode + "}]";
		}

		public void CalculateContraintCode()
		{
			mContraintCode = CalculateContraintCode(P, Q);
		}

		public static uint CalculateContraintCode(global::Poly2Tri.TriangulationPoint p, global::Poly2Tri.TriangulationPoint q)
		{
			if (p == null || p == null)
			{
				throw new global::System.ArgumentNullException();
			}
			uint nInitialValue = global::Poly2Tri.MathUtil.Jenkins32Hash(global::System.BitConverter.GetBytes(p.VertexCode), 0u);
			return global::Poly2Tri.MathUtil.Jenkins32Hash(global::System.BitConverter.GetBytes(q.VertexCode), nInitialValue);
		}
	}
}
