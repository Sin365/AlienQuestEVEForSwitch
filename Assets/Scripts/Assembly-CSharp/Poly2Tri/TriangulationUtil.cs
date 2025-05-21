namespace Poly2Tri
{
	public class TriangulationUtil
	{
		public static bool SmartIncircle(global::Poly2Tri.Point2D pa, global::Poly2Tri.Point2D pb, global::Poly2Tri.Point2D pc, global::Poly2Tri.Point2D pd)
		{
			double x = pd.X;
			double y = pd.Y;
			double num = pa.X - x;
			double num2 = pa.Y - y;
			double num3 = pb.X - x;
			double num4 = pb.Y - y;
			double num5 = num * num4;
			double num6 = num3 * num2;
			double num7 = num5 - num6;
			if (num7 <= 0.0)
			{
				return false;
			}
			double num8 = pc.X - x;
			double num9 = pc.Y - y;
			double num10 = num8 * num2;
			double num11 = num * num9;
			double num12 = num10 - num11;
			if (num12 <= 0.0)
			{
				return false;
			}
			double num13 = num3 * num9;
			double num14 = num8 * num4;
			double num15 = num * num + num2 * num2;
			double num16 = num3 * num3 + num4 * num4;
			double num17 = num8 * num8 + num9 * num9;
			double num18 = num15 * (num13 - num14) + num16 * num12 + num17 * num7;
			return num18 > 0.0;
		}

		public static bool InScanArea(global::Poly2Tri.Point2D pa, global::Poly2Tri.Point2D pb, global::Poly2Tri.Point2D pc, global::Poly2Tri.Point2D pd)
		{
			double x = pd.X;
			double y = pd.Y;
			double num = pa.X - x;
			double num2 = pa.Y - y;
			double num3 = pb.X - x;
			double num4 = pb.Y - y;
			double num5 = num * num4;
			double num6 = num3 * num2;
			double num7 = num5 - num6;
			if (num7 <= 0.0)
			{
				return false;
			}
			double num8 = pc.X - x;
			double num9 = pc.Y - y;
			double num10 = num8 * num2;
			double num11 = num * num9;
			double num12 = num10 - num11;
			if (num12 <= 0.0)
			{
				return false;
			}
			return true;
		}

		public static global::Poly2Tri.Orientation Orient2d(global::Poly2Tri.Point2D pa, global::Poly2Tri.Point2D pb, global::Poly2Tri.Point2D pc)
		{
			double num = (pa.X - pc.X) * (pb.Y - pc.Y);
			double num2 = (pa.Y - pc.Y) * (pb.X - pc.X);
			double num3 = num - num2;
			if (num3 > 0.0 - global::Poly2Tri.MathUtil.EPSILON && num3 < global::Poly2Tri.MathUtil.EPSILON)
			{
				return global::Poly2Tri.Orientation.Collinear;
			}
			if (num3 > 0.0)
			{
				return global::Poly2Tri.Orientation.CCW;
			}
			return global::Poly2Tri.Orientation.CW;
		}

		public static bool PointInBoundingBox(double xmin, double xmax, double ymin, double ymax, global::Poly2Tri.Point2D p)
		{
			return p.X > xmin && p.X < xmax && p.Y > ymin && p.Y < ymax;
		}

		public static bool PointOnLineSegment2D(global::Poly2Tri.Point2D lineStart, global::Poly2Tri.Point2D lineEnd, global::Poly2Tri.Point2D p, double epsilon)
		{
			return PointOnLineSegment2D(lineStart.X, lineStart.Y, lineEnd.X, lineEnd.Y, p.X, p.Y, epsilon);
		}

		public static bool PointOnLineSegment2D(double x1, double y1, double x2, double y2, double x, double y, double epsilon)
		{
			if (global::Poly2Tri.MathUtil.IsValueBetween(x, x1, x2, epsilon) && global::Poly2Tri.MathUtil.IsValueBetween(y, y1, y2, epsilon))
			{
				if (global::Poly2Tri.MathUtil.AreValuesEqual(x2 - x1, 0.0, epsilon))
				{
					return true;
				}
				double num = (y2 - y1) / (x2 - x1);
				double num2 = 0.0 - num * x1 + y1;
				double val = y - (num * x + num2);
				return global::Poly2Tri.MathUtil.AreValuesEqual(val, 0.0, epsilon);
			}
			return false;
		}

		public static bool RectsIntersect(global::Poly2Tri.Rect2D r1, global::Poly2Tri.Rect2D r2)
		{
			return r1.Right > r2.Left && r1.Left < r2.Right && r1.Bottom > r2.Top && r1.Top < r2.Bottom;
		}

		public static bool LinesIntersect2D(global::Poly2Tri.Point2D ptStart0, global::Poly2Tri.Point2D ptEnd0, global::Poly2Tri.Point2D ptStart1, global::Poly2Tri.Point2D ptEnd1, bool firstIsSegment, bool secondIsSegment, bool coincidentEndPointCollisions, ref global::Poly2Tri.Point2D pIntersectionPt, double epsilon)
		{
			double num = (ptEnd0.X - ptStart0.X) * (ptStart1.Y - ptEnd1.Y) - (ptStart1.X - ptEnd1.X) * (ptEnd0.Y - ptStart0.Y);
			if (global::System.Math.Abs(num) < epsilon)
			{
				return false;
			}
			double num2 = (ptStart1.X - ptStart0.X) * (ptStart1.Y - ptEnd1.Y) - (ptStart1.X - ptEnd1.X) * (ptStart1.Y - ptStart0.Y);
			double num3 = (ptEnd0.X - ptStart0.X) * (ptStart1.Y - ptStart0.Y) - (ptStart1.X - ptStart0.X) * (ptEnd0.Y - ptStart0.Y);
			double num4 = 1.0 / num;
			double num5 = num2 * num4;
			double num6 = num3 * num4;
			if ((!firstIsSegment || (num5 >= 0.0 && num5 <= 1.0)) && (!secondIsSegment || (num6 >= 0.0 && num6 <= 1.0)) && (coincidentEndPointCollisions || (!global::Poly2Tri.MathUtil.AreValuesEqual(0.0, num5, epsilon) && !global::Poly2Tri.MathUtil.AreValuesEqual(0.0, num6, epsilon))))
			{
				if (pIntersectionPt != null)
				{
					pIntersectionPt.X = ptStart0.X + num5 * (ptEnd0.X - ptStart0.X);
					pIntersectionPt.Y = ptStart0.Y + num5 * (ptEnd0.Y - ptStart0.Y);
				}
				return true;
			}
			return false;
		}

		public static bool LinesIntersect2D(global::Poly2Tri.Point2D ptStart0, global::Poly2Tri.Point2D ptEnd0, global::Poly2Tri.Point2D ptStart1, global::Poly2Tri.Point2D ptEnd1, ref global::Poly2Tri.Point2D pIntersectionPt, double epsilon)
		{
			return LinesIntersect2D(ptStart0, ptEnd0, ptStart1, ptEnd1, true, true, false, ref pIntersectionPt, epsilon);
		}

		public static double LI2DDotProduct(global::Poly2Tri.Point2D v0, global::Poly2Tri.Point2D v1)
		{
			return v0.X * v1.X + v0.Y * v1.Y;
		}

		public static bool RaysIntersect2D(global::Poly2Tri.Point2D ptRayOrigin0, global::Poly2Tri.Point2D ptRayVector0, global::Poly2Tri.Point2D ptRayOrigin1, global::Poly2Tri.Point2D ptRayVector1, ref global::Poly2Tri.Point2D ptIntersection)
		{
			double num = 0.01;
			if (ptIntersection != null)
			{
				global::Poly2Tri.Point2D v = new global::Poly2Tri.Point2D(ptRayOrigin1.X - ptRayOrigin0.X, ptRayOrigin1.Y - ptRayOrigin0.Y);
				global::Poly2Tri.Point2D v2 = new global::Poly2Tri.Point2D(0.0 - ptRayVector1.Y, ptRayVector1.X);
				double num2 = LI2DDotProduct(ptRayVector0, v2);
				if (global::System.Math.Abs(num2) < num)
				{
					return false;
				}
				double num3 = LI2DDotProduct(v, v2);
				double num4 = num3 / num2;
				ptIntersection.X = ptRayOrigin0.X + ptRayVector0.X * num4;
				ptIntersection.Y = ptRayOrigin0.Y + ptRayVector0.Y * num4;
				return true;
			}
			double value = ptRayVector1.X - ptRayVector0.X;
			if (global::System.Math.Abs(value) > num)
			{
				value = ptRayVector1.Y - ptRayVector0.Y;
				if (global::System.Math.Abs(value) > num)
				{
					return true;
				}
			}
			return false;
		}
	}
}
