using System;

namespace Voxels.TowerDefense.TriFlow
{
	// Token: 0x02000803 RID: 2051
	public struct Addition
	{
		// Token: 0x060035AF RID: 13743 RVA: 0x000E679A File Offset: 0x000E4B9A
		public Addition(Data data)
		{
			this.navPos = data.navPos;
			this.data = data;
		}

		// Token: 0x04002475 RID: 9333
		public NavPos navPos;

		// Token: 0x04002476 RID: 9334
		public Data data;
	}
}
