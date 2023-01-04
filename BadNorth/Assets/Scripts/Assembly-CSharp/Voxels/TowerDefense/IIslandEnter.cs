using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000780 RID: 1920
	public interface IIslandEnter
	{
		// Token: 0x060031CD RID: 12749
		IEnumerator<GenInfo> OnIslandEnter(Island island);
	}
}
