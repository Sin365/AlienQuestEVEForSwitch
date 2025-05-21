namespace Poly2Tri
{
	public class AdvancingFront
	{
		public global::Poly2Tri.AdvancingFrontNode Head;

		public global::Poly2Tri.AdvancingFrontNode Tail;

		protected global::Poly2Tri.AdvancingFrontNode Search;

		public AdvancingFront(global::Poly2Tri.AdvancingFrontNode head, global::Poly2Tri.AdvancingFrontNode tail)
		{
			Head = head;
			Tail = tail;
			Search = head;
			AddNode(head);
			AddNode(tail);
		}

		public void AddNode(global::Poly2Tri.AdvancingFrontNode node)
		{
		}

		public void RemoveNode(global::Poly2Tri.AdvancingFrontNode node)
		{
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (global::Poly2Tri.AdvancingFrontNode advancingFrontNode = Head; advancingFrontNode != Tail; advancingFrontNode = advancingFrontNode.Next)
			{
				stringBuilder.Append(advancingFrontNode.Point.X).Append("->");
			}
			stringBuilder.Append(Tail.Point.X);
			return stringBuilder.ToString();
		}

		private global::Poly2Tri.AdvancingFrontNode FindSearchNode(double x)
		{
			return Search;
		}

		public global::Poly2Tri.AdvancingFrontNode LocateNode(global::Poly2Tri.TriangulationPoint point)
		{
			return LocateNode(point.X);
		}

		private global::Poly2Tri.AdvancingFrontNode LocateNode(double x)
		{
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode = FindSearchNode(x);
			if (x < advancingFrontNode.Value)
			{
				while ((advancingFrontNode = advancingFrontNode.Prev) != null)
				{
					if (x >= advancingFrontNode.Value)
					{
						Search = advancingFrontNode;
						return advancingFrontNode;
					}
				}
			}
			else
			{
				while ((advancingFrontNode = advancingFrontNode.Next) != null)
				{
					if (x < advancingFrontNode.Value)
					{
						Search = advancingFrontNode.Prev;
						return advancingFrontNode.Prev;
					}
				}
			}
			return null;
		}

		public global::Poly2Tri.AdvancingFrontNode LocatePoint(global::Poly2Tri.TriangulationPoint point)
		{
			double x = point.X;
			global::Poly2Tri.AdvancingFrontNode advancingFrontNode = FindSearchNode(x);
			double x2 = advancingFrontNode.Point.X;
			if (x == x2)
			{
				if (point != advancingFrontNode.Point)
				{
					if (point == advancingFrontNode.Prev.Point)
					{
						advancingFrontNode = advancingFrontNode.Prev;
					}
					else
					{
						if (point != advancingFrontNode.Next.Point)
						{
							throw new global::System.Exception("Failed to find Node for given afront point");
						}
						advancingFrontNode = advancingFrontNode.Next;
					}
				}
			}
			else if (x < x2)
			{
				while ((advancingFrontNode = advancingFrontNode.Prev) != null && point != advancingFrontNode.Point)
				{
				}
			}
			else
			{
				while ((advancingFrontNode = advancingFrontNode.Next) != null && point != advancingFrontNode.Point)
				{
				}
			}
			Search = advancingFrontNode;
			return advancingFrontNode;
		}
	}
}
