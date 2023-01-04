using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200092A RID: 2346
	public interface IUpgradesProxy
	{
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003F04 RID: 16132
		List<HeroDefinition> heroes { get; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003F05 RID: 16133
		// (set) Token: 0x06003F06 RID: 16134
		int coinBank { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003F07 RID: 16135
		List<SerializableHeroUpgrade> inventory { get; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003F08 RID: 16136
		GameOverReason gameOverReason { get; }

		// Token: 0x06003F09 RID: 16137
		void RemoveFromInventory(HeroUpgradeDefinition def);

		// Token: 0x06003F0A RID: 16138
		void OnMenuOpened();

		// Token: 0x06003F0B RID: 16139
		void OnMenuClosed();

		// Token: 0x06003F0C RID: 16140
		void OnUpgradePurchased();

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003F0D RID: 16141
		Transform campaignCoinTransform { get; }
	}
}
