using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000659 RID: 1625
	public struct StructArray2<T>
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x0008FE1F File Offset: 0x0008E21F
		public int Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000579 RID: 1401
		public T this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.x;
				}
				if (index != 1)
				{
					return default(T);
				}
				return this.y;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException();
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x0008FE8C File Offset: 0x0008E28C
		public void SetNull()
		{
			this.x = default(T);
			this.y = default(T);
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x0008FEB7 File Offset: 0x0008E2B7
		public bool Contains(T t)
		{
			return this.x.Equals(t) || this.y.Equals(t);
		}

		// Token: 0x04001ADF RID: 6879
		public T x;

		// Token: 0x04001AE0 RID: 6880
		public T y;
	}
}
