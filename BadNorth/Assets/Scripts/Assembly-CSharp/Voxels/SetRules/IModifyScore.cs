using System;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x0200063C RID: 1596
	public interface IModifyScore
	{
		// Token: 0x060028B6 RID: 10422
		float ModifyScore(Vector3 pos, MultiWave multiWave, float inScore);
	}
}
