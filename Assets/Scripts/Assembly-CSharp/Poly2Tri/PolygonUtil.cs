namespace Poly2Tri
{
	public class PolygonUtil
	{
		public enum PolyUnionError
		{
			None = 0,
			NoIntersections = 1,
			Poly1InsidePoly2 = 2,
			InfiniteLoop = 3
		}

		[global::System.Flags]
		public enum PolyOperation : uint
		{
			None = 0u,
			Union = 1u,
			Intersect = 2u,
			Subtract = 4u
		}

		public static global::Poly2Tri.Point2DList.WindingOrderType CalculateWindingOrder(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> l)
		{
			double num = 0.0;
			for (int i = 0; i < l.Count; i++)
			{
				int index = (i + 1) % l.Count;
				num += l[i].X * l[index].Y;
				num -= l[i].Y * l[index].X;
			}
			num /= 2.0;
			if (num < 0.0)
			{
				return global::Poly2Tri.Point2DList.WindingOrderType.CW;
			}
			if (num > 0.0)
			{
				return global::Poly2Tri.Point2DList.WindingOrderType.CCW;
			}
			return global::Poly2Tri.Point2DList.WindingOrderType.Unknown;
		}

		public static bool PolygonsAreSame2D(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly1, global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly2)
		{
			int count = poly1.Count;
			int count2 = poly2.Count;
			if (count != count2)
			{
				return false;
			}
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(0.0, 0.0);
			for (int i = 0; i < count2; i++)
			{
				point2D.Set(poly1[0]);
				point2D.Subtract(poly2[i]);
				if (!(point2D.MagnitudeSquared() < 0.0001))
				{
					continue;
				}
				int num = i;
				bool flag = false;
				bool flag2;
				do
				{
					flag2 = true;
					for (int j = 1; j < count; j++)
					{
						if (!flag)
						{
							i++;
						}
						else
						{
							i--;
							if (i < 0)
							{
								i = count2 - 1;
							}
						}
						point2D.Set(poly1[j]);
						point2D.Subtract(poly2[i % count2]);
						if (point2D.MagnitudeSquared() >= 0.0001)
						{
							if (flag)
							{
								return false;
							}
							i = num;
							flag = true;
							flag2 = false;
							break;
						}
					}
				}
				while (!flag2);
				return true;
			}
			return false;
		}

		public static bool PointInPolygon2D(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> polygon, global::Poly2Tri.Point2D p)
		{
			if (polygon == null || polygon.Count < 3)
			{
				return false;
			}
			int count = polygon.Count;
			global::Poly2Tri.Point2D point2D = polygon[count - 1];
			bool flag = ((point2D.Y >= p.Y) ? true : false);
			global::Poly2Tri.Point2D point2D2 = null;
			bool flag2 = false;
			for (int i = 0; i < count; i++)
			{
				point2D2 = polygon[i];
				bool flag3 = ((point2D2.Y >= p.Y) ? true : false);
				if (flag != flag3 && (point2D2.Y - p.Y) * (point2D.X - point2D2.X) >= (point2D2.X - p.X) * (point2D.Y - point2D2.Y) == flag3)
				{
					flag2 = !flag2;
				}
				flag = flag3;
				point2D = point2D2;
			}
			return flag2;
		}

		public static bool PolygonsIntersect2D(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly1, global::Poly2Tri.Rect2D boundRect1, global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly2, global::Poly2Tri.Rect2D boundRect2)
		{
			if (poly1 == null || poly1.Count < 3 || boundRect1 == null || poly2 == null || poly2.Count < 3 || boundRect2 == null)
			{
				return false;
			}
			if (!boundRect1.Intersects(boundRect2))
			{
				return false;
			}
			double epsilon = global::System.Math.Max(global::System.Math.Min(boundRect1.Width, boundRect2.Width) * 0.0010000000474974513, global::Poly2Tri.MathUtil.EPSILON);
			int count = poly1.Count;
			int count2 = poly2.Count;
			for (int i = 0; i < count; i++)
			{
				int num = i + 1;
				if (num == count)
				{
					num = 0;
				}
				for (int j = 0; j < count2; j++)
				{
					int num2 = j + 1;
					if (num2 == count2)
					{
						num2 = 0;
					}
					global::Poly2Tri.Point2D pIntersectionPt = null;
					if (global::Poly2Tri.TriangulationUtil.LinesIntersect2D(poly1[i], poly1[num], poly2[j], poly2[num2], ref pIntersectionPt, epsilon))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool PolygonContainsPolygon(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly1, global::Poly2Tri.Rect2D boundRect1, global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly2, global::Poly2Tri.Rect2D boundRect2)
		{
			return PolygonContainsPolygon(poly1, boundRect1, poly2, boundRect2, true);
		}

		public static bool PolygonContainsPolygon(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly1, global::Poly2Tri.Rect2D boundRect1, global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly2, global::Poly2Tri.Rect2D boundRect2, bool runIntersectionTest)
		{
			if (poly1 == null || poly1.Count < 3 || poly2 == null || poly2.Count < 3)
			{
				return false;
			}
			if (runIntersectionTest)
			{
				if (poly1.Count == poly2.Count && PolygonsAreSame2D(poly1, poly2))
				{
					return false;
				}
				if (PolygonsIntersect2D(poly1, boundRect1, poly2, boundRect2))
				{
					return false;
				}
			}
			if (PointInPolygon2D(poly1, poly2[0]))
			{
				return true;
			}
			return false;
		}

		public static void ClipPolygonToEdge2D(global::Poly2Tri.Point2D edgeBegin, global::Poly2Tri.Point2D edgeEnd, global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly, out global::System.Collections.Generic.List<global::Poly2Tri.Point2D> outPoly)
		{
			outPoly = null;
			if (edgeBegin == null || edgeEnd == null || poly == null || poly.Count < 3)
			{
				return;
			}
			outPoly = new global::System.Collections.Generic.List<global::Poly2Tri.Point2D>();
			int index = poly.Count - 1;
			global::Poly2Tri.Point2D ptRayVector = new global::Poly2Tri.Point2D(edgeEnd.X - edgeBegin.X, edgeEnd.Y - edgeBegin.Y);
			bool flag = global::Poly2Tri.TriangulationUtil.Orient2d(edgeBegin, edgeEnd, poly[index]) == global::Poly2Tri.Orientation.CW;
			global::Poly2Tri.Point2D point2D = new global::Poly2Tri.Point2D(0.0, 0.0);
			for (int i = 0; i < poly.Count; i++)
			{
				bool flag2 = global::Poly2Tri.TriangulationUtil.Orient2d(edgeBegin, edgeEnd, poly[i]) == global::Poly2Tri.Orientation.CW;
				if (flag2)
				{
					if (flag)
					{
						outPoly.Add(poly[i]);
					}
					else
					{
						point2D.Set(poly[i].X - poly[index].X, poly[i].Y - poly[index].Y);
						global::Poly2Tri.Point2D ptIntersection = new global::Poly2Tri.Point2D(0.0, 0.0);
						if (global::Poly2Tri.TriangulationUtil.RaysIntersect2D(poly[index], point2D, edgeBegin, ptRayVector, ref ptIntersection))
						{
							outPoly.Add(ptIntersection);
							outPoly.Add(poly[i]);
						}
					}
				}
				else if (flag)
				{
					point2D.Set(poly[i].X - poly[index].X, poly[i].Y - poly[index].Y);
					global::Poly2Tri.Point2D ptIntersection2 = new global::Poly2Tri.Point2D(0.0, 0.0);
					if (global::Poly2Tri.TriangulationUtil.RaysIntersect2D(poly[index], point2D, edgeBegin, ptRayVector, ref ptIntersection2))
					{
						outPoly.Add(ptIntersection2);
					}
				}
				index = i;
				flag = flag2;
			}
		}

		public static void ClipPolygonToPolygon(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> poly, global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> clipPoly, out global::System.Collections.Generic.List<global::Poly2Tri.Point2D> outPoly)
		{
			outPoly = null;
			if (poly != null && poly.Count >= 3 && clipPoly != null && clipPoly.Count >= 3)
			{
				outPoly = new global::System.Collections.Generic.List<global::Poly2Tri.Point2D>(poly);
				int count = clipPoly.Count;
				int index = count - 1;
				for (int i = 0; i < count; i++)
				{
					global::System.Collections.Generic.List<global::Poly2Tri.Point2D> outPoly2 = null;
					global::Poly2Tri.Point2D edgeBegin = clipPoly[index];
					global::Poly2Tri.Point2D edgeEnd = clipPoly[i];
					ClipPolygonToEdge2D(edgeBegin, edgeEnd, outPoly, out outPoly2);
					outPoly.Clear();
					outPoly.AddRange(outPoly2);
					index = i;
				}
			}
		}

		public static global::Poly2Tri.PolygonUtil.PolyUnionError PolygonUnion(global::Poly2Tri.Point2DList polygon1, global::Poly2Tri.Point2DList polygon2, out global::Poly2Tri.Point2DList union)
		{
			global::Poly2Tri.PolygonOperationContext polygonOperationContext = new global::Poly2Tri.PolygonOperationContext();
			polygonOperationContext.Init(global::Poly2Tri.PolygonUtil.PolyOperation.Union, polygon1, polygon2);
			PolygonUnionInternal(polygonOperationContext);
			union = polygonOperationContext.Union;
			return polygonOperationContext.mError;
		}

		protected static void PolygonUnionInternal(global::Poly2Tri.PolygonOperationContext ctx)
		{
			global::Poly2Tri.Point2DList union = ctx.Union;
			if (ctx.mStartingIndex == -1)
			{
				switch (ctx.mError)
				{
				case global::Poly2Tri.PolygonUtil.PolyUnionError.NoIntersections:
				case global::Poly2Tri.PolygonUtil.PolyUnionError.InfiniteLoop:
					return;
				case global::Poly2Tri.PolygonUtil.PolyUnionError.Poly1InsidePoly2:
					union.AddRange(ctx.mOriginalPolygon2);
					return;
				}
			}
			global::Poly2Tri.Point2DList point2DList = ctx.mPoly1;
			global::Poly2Tri.Point2DList point2DList2 = ctx.mPoly2;
			global::System.Collections.Generic.List<int> list = ctx.mPoly1VectorAngles;
			global::Poly2Tri.Point2D point2D = ctx.mPoly1[ctx.mStartingIndex];
			int num = ctx.mStartingIndex;
			int num2 = -1;
			union.Clear();
			do
			{
				union.Add(point2DList[num]);
				foreach (global::Poly2Tri.EdgeIntersectInfo mIntersection in ctx.mIntersections)
				{
					if (!point2DList[num].Equals(mIntersection.IntersectionPoint, point2DList.Epsilon))
					{
						continue;
					}
					int num3 = point2DList2.IndexOf(mIntersection.IntersectionPoint);
					int index = point2DList2.NextIndex(num3);
					global::Poly2Tri.Point2D point = point2DList2[index];
					bool flag = false;
					if (list[index] == -1)
					{
						flag = ctx.PointInPolygonAngle(point, point2DList);
						list[index] = (flag ? 1 : 0);
					}
					else
					{
						flag = list[index] == 1;
					}
					if (flag)
					{
						continue;
					}
					if (point2DList == ctx.mPoly1)
					{
						point2DList = ctx.mPoly2;
						list = ctx.mPoly2VectorAngles;
						point2DList2 = ctx.mPoly1;
						if (num2 < 0)
						{
							num2 = num3;
						}
					}
					else
					{
						point2DList = ctx.mPoly1;
						list = ctx.mPoly1VectorAngles;
						point2DList2 = ctx.mPoly2;
					}
					num = num3;
					break;
				}
				num = point2DList.NextIndex(num);
				if (point2DList == ctx.mPoly1)
				{
					if (num == 0)
					{
						break;
					}
				}
				else if (num2 >= 0 && num == num2)
				{
					break;
				}
			}
			while (point2DList[num] != point2D && union.Count <= ctx.mPoly1.Count + ctx.mPoly2.Count);
			if (union.Count > ctx.mPoly1.Count + ctx.mPoly2.Count)
			{
				ctx.mError = global::Poly2Tri.PolygonUtil.PolyUnionError.InfiniteLoop;
			}
		}

		public static global::Poly2Tri.PolygonUtil.PolyUnionError PolygonIntersect(global::Poly2Tri.Point2DList polygon1, global::Poly2Tri.Point2DList polygon2, out global::Poly2Tri.Point2DList intersectOut)
		{
			global::Poly2Tri.PolygonOperationContext polygonOperationContext = new global::Poly2Tri.PolygonOperationContext();
			polygonOperationContext.Init(global::Poly2Tri.PolygonUtil.PolyOperation.Intersect, polygon1, polygon2);
			PolygonIntersectInternal(polygonOperationContext);
			intersectOut = polygonOperationContext.Intersect;
			return polygonOperationContext.mError;
		}

		protected static void PolygonIntersectInternal(global::Poly2Tri.PolygonOperationContext ctx)
		{
			global::Poly2Tri.Point2DList intersect = ctx.Intersect;
			if (ctx.mStartingIndex == -1)
			{
				switch (ctx.mError)
				{
				case global::Poly2Tri.PolygonUtil.PolyUnionError.NoIntersections:
				case global::Poly2Tri.PolygonUtil.PolyUnionError.InfiniteLoop:
					return;
				case global::Poly2Tri.PolygonUtil.PolyUnionError.Poly1InsidePoly2:
					intersect.AddRange(ctx.mOriginalPolygon2);
					return;
				}
			}
			global::Poly2Tri.Point2DList point2DList = ctx.mPoly1;
			global::Poly2Tri.Point2DList point2DList2 = ctx.mPoly2;
			global::System.Collections.Generic.List<int> list = ctx.mPoly1VectorAngles;
			int num = ctx.mPoly1.IndexOf(ctx.mIntersections[0].IntersectionPoint);
			global::Poly2Tri.Point2D point2D = ctx.mPoly1[num];
			int num2 = num;
			int num3 = -1;
			intersect.Clear();
			while (!intersect.Contains(point2DList[num]))
			{
				intersect.Add(point2DList[num]);
				foreach (global::Poly2Tri.EdgeIntersectInfo mIntersection in ctx.mIntersections)
				{
					if (!point2DList[num].Equals(mIntersection.IntersectionPoint, point2DList.Epsilon))
					{
						continue;
					}
					int num4 = point2DList2.IndexOf(mIntersection.IntersectionPoint);
					int index = point2DList2.NextIndex(num4);
					global::Poly2Tri.Point2D point = point2DList2[index];
					bool flag = false;
					if (list[index] == -1)
					{
						flag = ctx.PointInPolygonAngle(point, point2DList);
						list[index] = (flag ? 1 : 0);
					}
					else
					{
						flag = list[index] == 1;
					}
					if (!flag)
					{
						continue;
					}
					if (point2DList == ctx.mPoly1)
					{
						point2DList = ctx.mPoly2;
						list = ctx.mPoly2VectorAngles;
						point2DList2 = ctx.mPoly1;
						if (num3 < 0)
						{
							num3 = num4;
						}
					}
					else
					{
						point2DList = ctx.mPoly1;
						list = ctx.mPoly1VectorAngles;
						point2DList2 = ctx.mPoly2;
					}
					num = num4;
					break;
				}
				num = point2DList.NextIndex(num);
				if (point2DList == ctx.mPoly1)
				{
					if (num == num2)
					{
						break;
					}
				}
				else if (num3 >= 0 && num == num3)
				{
					break;
				}
				if (point2DList[num] == point2D || intersect.Count > ctx.mPoly1.Count + ctx.mPoly2.Count)
				{
					break;
				}
			}
			if (intersect.Count > ctx.mPoly1.Count + ctx.mPoly2.Count)
			{
				ctx.mError = global::Poly2Tri.PolygonUtil.PolyUnionError.InfiniteLoop;
			}
		}

		public static global::Poly2Tri.PolygonUtil.PolyUnionError PolygonSubtract(global::Poly2Tri.Point2DList polygon1, global::Poly2Tri.Point2DList polygon2, out global::Poly2Tri.Point2DList subtract)
		{
			global::Poly2Tri.PolygonOperationContext polygonOperationContext = new global::Poly2Tri.PolygonOperationContext();
			polygonOperationContext.Init(global::Poly2Tri.PolygonUtil.PolyOperation.Subtract, polygon1, polygon2);
			PolygonSubtractInternal(polygonOperationContext);
			subtract = polygonOperationContext.Subtract;
			return polygonOperationContext.mError;
		}

		public static void PolygonSubtractInternal(global::Poly2Tri.PolygonOperationContext ctx)
		{
			global::Poly2Tri.Point2DList subtract = ctx.Subtract;
			if (ctx.mStartingIndex == -1)
			{
				switch (ctx.mError)
				{
				case global::Poly2Tri.PolygonUtil.PolyUnionError.NoIntersections:
				case global::Poly2Tri.PolygonUtil.PolyUnionError.Poly1InsidePoly2:
				case global::Poly2Tri.PolygonUtil.PolyUnionError.InfiniteLoop:
					return;
				}
			}
			global::Poly2Tri.Point2DList point2DList = ctx.mPoly1;
			global::Poly2Tri.Point2DList point2DList2 = ctx.mPoly2;
			global::System.Collections.Generic.List<int> list = ctx.mPoly1VectorAngles;
			global::Poly2Tri.Point2D point2D = ctx.mPoly1[ctx.mStartingIndex];
			int index = ctx.mStartingIndex;
			subtract.Clear();
			bool flag = true;
			do
			{
				subtract.Add(point2DList[index]);
				foreach (global::Poly2Tri.EdgeIntersectInfo mIntersection in ctx.mIntersections)
				{
					if (!point2DList[index].Equals(mIntersection.IntersectionPoint, point2DList.Epsilon))
					{
						continue;
					}
					int num = point2DList2.IndexOf(mIntersection.IntersectionPoint);
					if (flag)
					{
						int index2 = point2DList2.PreviousIndex(num);
						global::Poly2Tri.Point2D point = point2DList2[index2];
						bool flag2 = false;
						if (list[index2] == -1)
						{
							flag2 = ctx.PointInPolygonAngle(point, point2DList);
							list[index2] = (flag2 ? 1 : 0);
						}
						else
						{
							flag2 = list[index2] == 1;
						}
						if (flag2)
						{
							if (point2DList == ctx.mPoly1)
							{
								point2DList = ctx.mPoly2;
								list = ctx.mPoly2VectorAngles;
								point2DList2 = ctx.mPoly1;
							}
							else
							{
								point2DList = ctx.mPoly1;
								list = ctx.mPoly1VectorAngles;
								point2DList2 = ctx.mPoly2;
							}
							index = num;
							flag = !flag;
							break;
						}
						continue;
					}
					int index3 = point2DList2.NextIndex(num);
					global::Poly2Tri.Point2D point2 = point2DList2[index3];
					bool flag3 = false;
					if (list[index3] == -1)
					{
						flag3 = ctx.PointInPolygonAngle(point2, point2DList);
						list[index3] = (flag3 ? 1 : 0);
					}
					else
					{
						flag3 = list[index3] == 1;
					}
					if (flag3)
					{
						continue;
					}
					if (point2DList == ctx.mPoly1)
					{
						point2DList = ctx.mPoly2;
						list = ctx.mPoly2VectorAngles;
						point2DList2 = ctx.mPoly1;
					}
					else
					{
						point2DList = ctx.mPoly1;
						list = ctx.mPoly1VectorAngles;
						point2DList2 = ctx.mPoly2;
					}
					index = num;
					flag = !flag;
					break;
				}
				index = ((!flag) ? point2DList.PreviousIndex(index) : point2DList.NextIndex(index));
			}
			while (point2DList[index] != point2D && subtract.Count <= ctx.mPoly1.Count + ctx.mPoly2.Count);
			if (subtract.Count > ctx.mPoly1.Count + ctx.mPoly2.Count)
			{
				ctx.mError = global::Poly2Tri.PolygonUtil.PolyUnionError.InfiniteLoop;
			}
		}

		public static global::Poly2Tri.PolygonUtil.PolyUnionError PolygonOperation(global::Poly2Tri.PolygonUtil.PolyOperation operations, global::Poly2Tri.Point2DList polygon1, global::Poly2Tri.Point2DList polygon2, out global::System.Collections.Generic.Dictionary<uint, global::Poly2Tri.Point2DList> results)
		{
			global::Poly2Tri.PolygonOperationContext polygonOperationContext = new global::Poly2Tri.PolygonOperationContext();
			polygonOperationContext.Init(operations, polygon1, polygon2);
			results = polygonOperationContext.mOutput;
			return PolygonOperation(polygonOperationContext);
		}

		public static global::Poly2Tri.PolygonUtil.PolyUnionError PolygonOperation(global::Poly2Tri.PolygonOperationContext ctx)
		{
			if ((ctx.mOperations & global::Poly2Tri.PolygonUtil.PolyOperation.Union) == global::Poly2Tri.PolygonUtil.PolyOperation.Union)
			{
				PolygonUnionInternal(ctx);
			}
			if ((ctx.mOperations & global::Poly2Tri.PolygonUtil.PolyOperation.Intersect) == global::Poly2Tri.PolygonUtil.PolyOperation.Intersect)
			{
				PolygonIntersectInternal(ctx);
			}
			if ((ctx.mOperations & global::Poly2Tri.PolygonUtil.PolyOperation.Subtract) == global::Poly2Tri.PolygonUtil.PolyOperation.Subtract)
			{
				PolygonSubtractInternal(ctx);
			}
			return ctx.mError;
		}

		public static global::System.Collections.Generic.List<global::Poly2Tri.Point2DList> SplitComplexPolygon(global::Poly2Tri.Point2DList verts, double epsilon)
		{
			int count = verts.Count;
			int num = 0;
			global::System.Collections.Generic.List<global::Poly2Tri.SplitComplexPolygonNode> list = new global::System.Collections.Generic.List<global::Poly2Tri.SplitComplexPolygonNode>();
			for (int i = 0; i < verts.Count; i++)
			{
				global::Poly2Tri.SplitComplexPolygonNode item = new global::Poly2Tri.SplitComplexPolygonNode(new global::Poly2Tri.Point2D(verts[i].X, verts[i].Y));
				list.Add(item);
			}
			for (int j = 0; j < verts.Count; j++)
			{
				int index = ((j != count - 1) ? (j + 1) : 0);
				int index2 = ((j != 0) ? (j - 1) : (count - 1));
				list[j].AddConnection(list[index]);
				list[j].AddConnection(list[index2]);
			}
			num = list.Count;
			bool flag = true;
			int num2 = 0;
			while (flag)
			{
				flag = false;
				int num3 = 0;
				while (!flag && num3 < num)
				{
					int num4 = 0;
					while (!flag && num4 < list[num3].NumConnected)
					{
						int num5 = 0;
						while (!flag && num5 < num)
						{
							if (num5 != num3 && !(list[num5] == list[num3][num4]))
							{
								int num6 = 0;
								while (!flag && num6 < list[num5].NumConnected)
								{
									if (!(list[num5][num6] == list[num3][num4]) && !(list[num5][num6] == list[num3]))
									{
										global::Poly2Tri.Point2D pIntersectionPt = new global::Poly2Tri.Point2D();
										if (global::Poly2Tri.TriangulationUtil.LinesIntersect2D(list[num3].Position, list[num3][num4].Position, list[num5].Position, list[num5][num6].Position, true, true, true, ref pIntersectionPt, epsilon))
										{
											flag = true;
											global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode = new global::Poly2Tri.SplitComplexPolygonNode(pIntersectionPt);
											int num7 = list.IndexOf(splitComplexPolygonNode);
											if (num7 >= 0 && num7 < list.Count)
											{
												splitComplexPolygonNode = list[num7];
											}
											else
											{
												list.Add(splitComplexPolygonNode);
												num = list.Count;
											}
											global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode2 = list[num3];
											global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode3 = list[num3][num4];
											global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode4 = list[num5];
											global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode5 = list[num5][num6];
											splitComplexPolygonNode3.RemoveConnection(splitComplexPolygonNode2);
											splitComplexPolygonNode2.RemoveConnection(splitComplexPolygonNode3);
											splitComplexPolygonNode5.RemoveConnection(splitComplexPolygonNode4);
											splitComplexPolygonNode4.RemoveConnection(splitComplexPolygonNode5);
											if (!splitComplexPolygonNode.Position.Equals(splitComplexPolygonNode2.Position, epsilon))
											{
												splitComplexPolygonNode.AddConnection(splitComplexPolygonNode2);
												splitComplexPolygonNode2.AddConnection(splitComplexPolygonNode);
											}
											if (!splitComplexPolygonNode.Position.Equals(splitComplexPolygonNode4.Position, epsilon))
											{
												splitComplexPolygonNode.AddConnection(splitComplexPolygonNode4);
												splitComplexPolygonNode4.AddConnection(splitComplexPolygonNode);
											}
											if (!splitComplexPolygonNode.Position.Equals(splitComplexPolygonNode3.Position, epsilon))
											{
												splitComplexPolygonNode.AddConnection(splitComplexPolygonNode3);
												splitComplexPolygonNode3.AddConnection(splitComplexPolygonNode);
											}
											if (!splitComplexPolygonNode.Position.Equals(splitComplexPolygonNode5.Position, epsilon))
											{
												splitComplexPolygonNode.AddConnection(splitComplexPolygonNode5);
												splitComplexPolygonNode5.AddConnection(splitComplexPolygonNode);
											}
										}
									}
									num6++;
								}
							}
							num5++;
						}
						num4++;
					}
					num3++;
				}
				num2++;
			}
			bool flag2 = true;
			int num8 = num;
			double num9 = epsilon * epsilon;
			while (flag2)
			{
				flag2 = false;
				for (int k = 0; k < num; k++)
				{
					if (list[k].NumConnected == 0)
					{
						continue;
					}
					for (int l = k + 1; l < num; l++)
					{
						if (list[l].NumConnected == 0)
						{
							continue;
						}
						global::Poly2Tri.Point2D point2D = list[k].Position - list[l].Position;
						if (!(point2D.MagnitudeSquared() <= num9))
						{
							continue;
						}
						if (num8 <= 3)
						{
							throw new global::System.Exception("Eliminated so many duplicate points that resulting polygon has < 3 vertices!");
						}
						num8--;
						flag2 = true;
						global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode6 = list[k];
						global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode7 = list[l];
						int numConnected = splitComplexPolygonNode7.NumConnected;
						for (int m = 0; m < numConnected; m++)
						{
							global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode8 = splitComplexPolygonNode7[m];
							if (splitComplexPolygonNode8 != splitComplexPolygonNode6)
							{
								splitComplexPolygonNode6.AddConnection(splitComplexPolygonNode8);
								splitComplexPolygonNode8.AddConnection(splitComplexPolygonNode6);
							}
							splitComplexPolygonNode8.RemoveConnection(splitComplexPolygonNode7);
						}
						splitComplexPolygonNode7.ClearConnections();
						list.RemoveAt(l);
						num--;
					}
				}
			}
			double num10 = double.MaxValue;
			double num11 = double.MinValue;
			int index3 = -1;
			for (int n = 0; n < num; n++)
			{
				if (list[n].Position.Y < num10 && list[n].NumConnected > 1)
				{
					num10 = list[n].Position.Y;
					index3 = n;
					num11 = list[n].Position.X;
				}
				else if (list[n].Position.Y == num10 && list[n].Position.X > num11 && list[n].NumConnected > 1)
				{
					index3 = n;
					num11 = list[n].Position.X;
				}
			}
			global::Poly2Tri.Point2D incomingDir = new global::Poly2Tri.Point2D(1.0, 0.0);
			global::System.Collections.Generic.List<global::Poly2Tri.Point2D> list2 = new global::System.Collections.Generic.List<global::Poly2Tri.Point2D>();
			global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode9 = list[index3];
			global::Poly2Tri.SplitComplexPolygonNode splitComplexPolygonNode10 = splitComplexPolygonNode9;
			global::Poly2Tri.SplitComplexPolygonNode rightestConnection = splitComplexPolygonNode9.GetRightestConnection(incomingDir);
			if (rightestConnection == null)
			{
				return SplitComplexPolygonCleanup(verts);
			}
			list2.Add(splitComplexPolygonNode10.Position);
			while (rightestConnection != splitComplexPolygonNode10)
			{
				if (list2.Count > 4 * num)
				{
					throw new global::System.Exception("nodes should never be visited four times apiece (proof?), so we've probably hit a loop...crap");
				}
				list2.Add(rightestConnection.Position);
				global::Poly2Tri.SplitComplexPolygonNode incoming = splitComplexPolygonNode9;
				splitComplexPolygonNode9 = rightestConnection;
				rightestConnection = splitComplexPolygonNode9.GetRightestConnection(incoming);
				if (rightestConnection == null)
				{
					return SplitComplexPolygonCleanup(list2);
				}
			}
			if (list2.Count < 1)
			{
				return SplitComplexPolygonCleanup(verts);
			}
			return SplitComplexPolygonCleanup(list2);
		}

		private static global::System.Collections.Generic.List<global::Poly2Tri.Point2DList> SplitComplexPolygonCleanup(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> orig)
		{
			global::System.Collections.Generic.List<global::Poly2Tri.Point2DList> list = new global::System.Collections.Generic.List<global::Poly2Tri.Point2DList>();
			global::Poly2Tri.Point2DList point2DList = new global::Poly2Tri.Point2DList(orig);
			list.Add(point2DList);
			int i = 0;
			for (int num = list.Count; i < num; i++)
			{
				int num2 = list[i].Count;
				for (int j = 0; j < num2; j++)
				{
					for (int k = j + 1; k < num2; k++)
					{
						if (list[i][j].Equals(list[i][k], point2DList.Epsilon))
						{
							int num3 = k - j;
							global::Poly2Tri.Point2DList point2DList2 = new global::Poly2Tri.Point2DList();
							for (int l = j + 1; l <= k; l++)
							{
								point2DList2.Add(list[i][l]);
							}
							list[i].RemoveRange(j + 1, num3);
							list.Add(point2DList2);
							num++;
							num2 -= num3;
							k = j + 1;
						}
					}
				}
				list[i].Simplify();
			}
			return list;
		}
	}
}
