using System;

namespace Voxels.TowerDefense
{
	// Token: 0x0200065A RID: 1626
	public struct StructArray3<T>
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x0008FEEF File Offset: 0x0008E2EF
		public int Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700057B RID: 1403
		public T this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				default:
					return default(T);
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x0008FF88 File Offset: 0x0008E388
		public void SetNull()
		{
			this.x = default(T);
			this.y = default(T);
			this.z = default(T);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x0008FFC4 File Offset: 0x0008E3C4
		public bool Contains(T t)
		{
			return this.x.Equals(t) || this.y.Equals(t) || this.z.Equals(t);
		}

		// Token: 0x04001AE1 RID: 6881
		public T x;

		// Token: 0x04001AE2 RID: 6882
		public T y;

		// Token: 0x04001AE3 RID: 6883
		public T z;
	}
}
