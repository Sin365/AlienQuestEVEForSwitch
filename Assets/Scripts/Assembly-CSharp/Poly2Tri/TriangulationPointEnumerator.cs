namespace Poly2Tri
{
	public class TriangulationPointEnumerator : global::System.Collections.IEnumerator, global::System.IDisposable, global::System.Collections.Generic.IEnumerator<global::Poly2Tri.TriangulationPoint>
	{
		protected global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> mPoints;

		protected int position = -1;

		object global::System.Collections.IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public global::Poly2Tri.TriangulationPoint Current
		{
			get
			{
				if (position < 0 || position >= mPoints.Count)
				{
					return null;
				}
				return mPoints[position] as global::Poly2Tri.TriangulationPoint;
			}
		}

		public TriangulationPointEnumerator(global::System.Collections.Generic.IList<global::Poly2Tri.Point2D> points)
		{
			mPoints = points;
		}

		void global::System.IDisposable.Dispose()
		{
		}

		public bool MoveNext()
		{
			position++;
			return position < mPoints.Count;
		}

		public void Reset()
		{
			position = -1;
		}
	}
}
