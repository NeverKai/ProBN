using System;

namespace I2.Loc
{
	// Token: 0x020003DA RID: 986
	public class TranslationJob : IDisposable
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x000348CB File Offset: 0x00032CCB
		public virtual TranslationJob.eJobState GetState()
		{
			return this.mJobState;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x000348D3 File Offset: 0x00032CD3
		public virtual void Dispose()
		{
		}

		// Token: 0x04000DD8 RID: 3544
		public TranslationJob.eJobState mJobState;

		// Token: 0x020003DB RID: 987
		public enum eJobState
		{
			// Token: 0x04000DDA RID: 3546
			Running,
			// Token: 0x04000DDB RID: 3547
			Succeeded,
			// Token: 0x04000DDC RID: 3548
			Failed
		}
	}
}
