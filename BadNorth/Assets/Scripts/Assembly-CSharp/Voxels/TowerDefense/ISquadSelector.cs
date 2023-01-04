using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A4 RID: 1700
	public interface ISquadSelector
	{
		// Token: 0x06002BEB RID: 11243
		EnglishSquad GetSelectableSquad();

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002BEC RID: 11244
		bool wantsHoverEffect { get; }
	}
}
