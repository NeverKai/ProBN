using System;

namespace RTM.Utilities
{
	// Token: 0x020004EB RID: 1259
	public class WeakReference<T> where T : class
	{
		// Token: 0x0600203E RID: 8254 RVA: 0x00056DF8 File Offset: 0x000551F8
		public WeakReference(T target = default(T))
		{
			this.Reference = new WeakReference(target);
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x00056E11 File Offset: 0x00055211
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x00056E28 File Offset: 0x00055228
		public T Target
		{
			get
			{
				return this.Reference.Target as T;
			}
			set
			{
				this.Reference.Target = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00056E3B File Offset: 0x0005523B
		public bool IsAlive
		{
			get
			{
				return this.Reference.IsAlive;
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00056E48 File Offset: 0x00055248
		public static implicit operator WeakReference<T>(T target)
		{
			return new WeakReference<T>(target);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00056E50 File Offset: 0x00055250
		public static implicit operator T(WeakReference<T> weakReference)
		{
			return (weakReference != null) ? weakReference.Target : ((T)((object)null));
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00056E69 File Offset: 0x00055269
		public static implicit operator bool(WeakReference<T> weakReference)
		{
			return weakReference != null && weakReference.Target != null;
		}

		// Token: 0x0400140D RID: 5133
		private WeakReference Reference;
	}
}
