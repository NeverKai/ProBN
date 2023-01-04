using System;
using System.Diagnostics;

namespace RTM.Utilities
{
	// Token: 0x020004E5 RID: 1253
	public class CPUStaller
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x00056A6C File Offset: 0x00054E6C
		public void StallForMs(float ms)
		{
			this.stopwatch.Reset();
			this.stopwatch.Start();
			float num = ms * CPUStaller.msToTicks;
			while ((float)this.stopwatch.ElapsedTicks < num)
			{
			}
		}

		// Token: 0x040013F9 RID: 5113
		private static float msToTicks = 10000f;

		// Token: 0x040013FA RID: 5114
		private Stopwatch stopwatch = new Stopwatch();
	}
}
