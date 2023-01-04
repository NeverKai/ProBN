using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000682 RID: 1666
	public interface IBehaviour
	{
		// Token: 0x06002AA1 RID: 10913
		float Bid();

		// Token: 0x06002AA2 RID: 10914
		bool Execute();

		// Token: 0x06002AA3 RID: 10915
		bool GetInterruptable();
	}
}
