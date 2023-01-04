using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006FA RID: 1786
	public interface IHeroGraphics
	{
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002E51 RID: 11857
		Sprite flag { get; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002E52 RID: 11858
		Sprite agentSpriteMinion { get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002E53 RID: 11859
		Sprite agentSpriteHero { get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002E54 RID: 11860
		Sprite iconHead { get; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002E55 RID: 11861
		Sprite iconBackground { get; }
	}
}
