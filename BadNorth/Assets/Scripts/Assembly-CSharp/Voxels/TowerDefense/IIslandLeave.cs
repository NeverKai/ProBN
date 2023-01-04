using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000784 RID: 1924
	public interface IIslandLeave
	{
		// Token: 0x060031D1 RID: 12753
		IEnumerator<GenInfo> OnIslandLeave(Island island);
	}
}
