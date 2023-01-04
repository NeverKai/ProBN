using System;

namespace Voxels.TowerDefense
{
	// Token: 0x0200089A RID: 2202
	public interface ITargeter
	{
		// Token: 0x06003995 RID: 14741
		bool IsTargetable(NavSpot origin, NavSpot target, ref int currErrorId);

		// Token: 0x06003996 RID: 14742
		string GetErrorTerm(int errorId);
	}
}
