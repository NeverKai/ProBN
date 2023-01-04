using System;

namespace Voxels.SetRules
{
	// Token: 0x02000639 RID: 1593
	public interface IOnLast
	{
		// Token: 0x060028B3 RID: 10419
		bool OnLast(Domino domino, MultiWave multiWave, int side, Claim claim, Slot slot);
	}
}
