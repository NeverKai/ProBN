using System;

namespace RTM.Utilities
{
	// Token: 0x020004EA RID: 1258
	public struct Tuple<T1, T2>
	{
		// Token: 0x0600203D RID: 8253 RVA: 0x00056DE8 File Offset: 0x000551E8
		public Tuple(T1 item1, T2 item2)
		{
			this.item1 = item1;
			this.item2 = item2;
		}

		// Token: 0x0400140B RID: 5131
		public T1 item1;

		// Token: 0x0400140C RID: 5132
		public T2 item2;
	}
}
