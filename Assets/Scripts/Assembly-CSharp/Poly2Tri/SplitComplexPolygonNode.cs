namespace Poly2Tri
{
	public class SplitComplexPolygonNode
	{
		private global::System.Collections.Generic.List<global::Poly2Tri.SplitComplexPolygonNode> mConnected = new global::System.Collections.Generic.List<global::Poly2Tri.SplitComplexPolygonNode>();

		private global::Poly2Tri.Point2D mPosition;

		public int NumConnected
		{
			get
			{
				return mConnected.Count;
			}
		}

		public global::Poly2Tri.Point2D Position
		{
			get
			{
				return mPosition;
			}
			set
			{
				mPosition = value;
			}
		}

		public global::Poly2Tri.SplitComplexPolygonNode this[int index]
		{
			get
			{
				return mConnected[index];
			}
		}

		public SplitComplexPolygonNode()
		{
		}

		public SplitComplexPolygonNode(global::Poly2Tri.Point2D pos)
		{
			mPosition = pos;
		}

		public override bool Equals(object obj)
		{
			global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode = obj as global::Poly2Tri.SplitComplexPolygonNode;
			if (splitComplexPolygonNode == null)
			{
				return base.Equals(obj);
			}
			return Equals(splitComplexPolygonNode);
		}

		public bool Equals(global::Poly2Tri.SplitComplexPolygonNode pn)
		{
			if ((object)pn == null)
			{
				return false;
			}
			if (mPosition == null || pn.Position == null)
			{
				return false;
			}
			return mPosition.Equals(pn.Position);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(256);
			stringBuilder.Append(mPosition.ToString());
			stringBuilder.Append(" -> ");
			for (int i = 0; i < NumConnected; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mConnected[i].Position.ToString());
			}
			return stringBuilder.ToString();
		}

		private bool IsRighter(double sinA, double cosA, double sinB, double cosB)
		{
			if (sinA < 0.0)
			{
				if (sinB > 0.0 || cosA <= cosB)
				{
					return true;
				}
				return false;
			}
			if (sinB < 0.0 || cosA <= cosB)
			{
				return false;
			}
			return true;
		}

		private int remainder(int x, int modulus)
		{
			int i;
			for (i = x % modulus; i < 0; i += modulus)
			{
			}
			return i;
		}

		public void AddConnection(global::Poly2Tri.SplitComplexPolygonNode toMe)
		{
			if (!mConnected.Contains(toMe) && toMe != this)
			{
				mConnected.Add(toMe);
			}
		}

		public void RemoveConnection(global::Poly2Tri.SplitComplexPolygonNode fromMe)
		{
			mConnected.Remove(fromMe);
		}

		private void RemoveConnectionByIndex(int index)
		{
			if (index >= 0 && index < mConnected.Count)
			{
				mConnected.RemoveAt(index);
			}
		}

		public void ClearConnections()
		{
			mConnected.Clear();
		}

		private bool IsConnectedTo(global::Poly2Tri.SplitComplexPolygonNode me)
		{
			return mConnected.Contains(me);
		}

		public global::Poly2Tri.SplitComplexPolygonNode GetRightestConnection(global::Poly2Tri.SplitComplexPolygonNode incoming)
		{
			if (NumConnected == 0)
			{
				throw new global::System.Exception("the connection graph is inconsistent");
			}
			if (NumConnected == 1)
			{
				return incoming;
			}
			global::Poly2Tri.Point2D point2D = mPosition - incoming.mPosition;
			double num = point2D.Magnitude();
			point2D.Normalize();
			if (num <= global::Poly2Tri.MathUtil.EPSILON)
			{
				throw new global::System.Exception("Length too small");
			}
			global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode = null;
			for (int i = 0; i < NumConnected; i++)
			{
				if (mConnected[i] == incoming)
				{
					continue;
				}
				global::Poly2Tri.Point2D point2D2 = mConnected[i].mPosition - mPosition;
				double num2 = point2D2.MagnitudeSquared();
				point2D2.Normalize();
				if (num2 <= global::Poly2Tri.MathUtil.EPSILON * global::Poly2Tri.MathUtil.EPSILON)
				{
					throw new global::System.Exception("Length too small");
				}
				double cosA = global::Poly2Tri.Point2D.Dot(point2D, point2D2);
				double sinA = global::Poly2Tri.Point2D.Cross(point2D, point2D2);
				if (splitComplexPolygonNode != null)
				{
					global::Poly2Tri.Point2D point2D3 = splitComplexPolygonNode.mPosition - mPosition;
					point2D3.Normalize();
					double cosB = global::Poly2Tri.Point2D.Dot(point2D, point2D3);
					double sinB = global::Poly2Tri.Point2D.Cross(point2D, point2D3);
					if (IsRighter(sinA, cosA, sinB, cosB))
					{
						splitComplexPolygonNode = mConnected[i];
					}
				}
				else
				{
					splitComplexPolygonNode = mConnected[i];
				}
			}
			return splitComplexPolygonNode;
		}

		public global::Poly2Tri.SplitComplexPolygonNode GetRightestConnection(global::Poly2Tri.Point2D incomingDir)
		{
			global::Poly2Tri.Point2D pos = mPosition - incomingDir;
			global::Poly2Tri.SplitComplexPolygonNode incoming = new global::Poly2Tri.SplitComplexPolygonNode(pos);
			return GetRightestConnection(incoming);
		}

		public static bool operator ==(global::Poly2Tri.SplitComplexPolygonNode lhs, global::Poly2Tri.SplitComplexPolygonNode rhs)
		{
			if ((object)lhs != null)
			{
				return lhs.Equals(rhs);
			}
			if ((object)rhs == null)
			{
				return true;
			}
			return false;
		}

		public static bool operator !=(global::Poly2Tri.SplitComplexPolygonNode lhs, global::Poly2Tri.SplitComplexPolygonNode rhs)
		{
			if ((object)lhs != null)
			{
				return !lhs.Equals(rhs);
			}
			if ((object)rhs == null)
			{
				return false;
			}
			return true;
		}
	}
}
