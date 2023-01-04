using System;

namespace ArcConsistency
{
	// Token: 0x02000005 RID: 5
	public struct Guess
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000030C8 File Offset: 0x000014C8
		public Guess(Domain domain, float value, float priority)
		{
			this.domain = domain;
			this.value = value;
			this.priority = priority;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000030DF File Offset: 0x000014DF
		public float cost
		{
			get
			{
				return -this.priority;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000030E8 File Offset: 0x000014E8
		public bool TakeGuess()
		{
			if (this.domain.values.Count > 1 && this.domain.values.Contains(this.value))
			{
				this.domain.values.Clear();
				this.domain.values.Add(this.value);
				return true;
			}
			return false;
		}

		// Token: 0x0400000B RID: 11
		public Domain domain;

		// Token: 0x0400000C RID: 12
		public float value;

		// Token: 0x0400000D RID: 13
		public float priority;
	}
}
