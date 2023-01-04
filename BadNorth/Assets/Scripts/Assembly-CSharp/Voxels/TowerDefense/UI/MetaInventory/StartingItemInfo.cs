using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI.MetaInventory
{
	// Token: 0x020008F0 RID: 2288
	[AddComponentMenu("Meta Inventory - StartingItemInfo")]
	internal class StartingItemInfo : InfoPanelEntry<StartingItemInfo>
	{
		// Token: 0x06003CAE RID: 15534 RVA: 0x0010EC8C File Offset: 0x0010D08C
		public void Setup(HeroUpgradeDefinition upgradeDef, bool isUnlocked, float appearDelay)
		{
			float value = (!isUnlocked) ? 0.6f : 1f;
			this.descriptionText.color = this.descriptionText.color.SetA(value);
			HeroUpgradeType upgradeType = upgradeDef.upgradeType;
			this.descriptionLocalize.Term = ((!isUnlocked) ? upgradeType.startItemLockedTerm : upgradeType.startItemUnlockedTerm);
		}

		// Token: 0x04002A58 RID: 10840
		[SerializeField]
		private Localize descriptionLocalize;

		// Token: 0x04002A59 RID: 10841
		[SerializeField]
		private Text descriptionText;
	}
}
