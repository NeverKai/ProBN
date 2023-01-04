using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x0200077F RID: 1919
	public interface IIslandFirstEnter
	{
		// Token: 0x060031CC RID: 12748
		IEnumerator<GenInfo> OnIslandFirstEnter(Island island);
	}
}
