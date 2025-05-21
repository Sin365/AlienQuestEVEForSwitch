namespace Poly2Tri
{
	public struct FixedArray3<T> : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable where T : class
	{
		public T _0;

		public T _1;

		public T _2;

		public T this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return _0;
				case 1:
					return _1;
				case 2:
					return _2;
				default:
					throw new global::System.IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					_0 = value;
					break;
				case 1:
					_1 = value;
					break;
				case 2:
					_2 = value;
					break;
				default:
					throw new global::System.IndexOutOfRangeException();
				}
			}
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Contains(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] != null)
				{
					T val = this[i];
					if (val.Equals(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		public int IndexOf(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] != null)
				{
					T val = this[i];
					if (val.Equals(value))
					{
						return i;
					}
				}
			}
			return -1;
		}

		public void Clear()
		{
			_0 = (_1 = (_2 = (T)null));
		}

		public void Clear(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] != null)
				{
					T val = this[i];
					if (val.Equals(value))
					{
						this[i] = (T)null;
					}
				}
			}
		}

		private global::System.Collections.Generic.IEnumerable<T> Enumerate()
		{
			for (int i = 0; i < 3; i++)
			{
				yield return this[i];
			}
		}

		public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			return Enumerate().GetEnumerator();
		}
	}
}
