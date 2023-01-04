using System;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x0200063D RID: 1597
	public interface IAllowPlacement
	{
		// Token: 0x060028B7 RID: 10423
		bool AllowPlacement(Vector3 offset, Placement placement, MultiWave multiWave);
	}
}
