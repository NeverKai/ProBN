using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200083D RID: 2109
	public class UpgradePanelHeroSummary : MonoBehaviour
	{
		// Token: 0x060036E2 RID: 14050 RVA: 0x000EBA58 File Offset: 0x000E9E58
		public void SetHero(HeroDefinition heroDef)
		{
			this.portraitImage.sprite = heroDef.graphics.iconBackground;
			this.nameText.text = heroDef.nameTerm;
			this.classLevelText.text = heroDef.GetClassDisplayTerm();
		}

		// Token: 0x04002547 RID: 9543
		[SerializeField]
		private Image portraitImage;

		// Token: 0x04002548 RID: 9544
		[SerializeField]
		private Text nameText;

		// Token: 0x04002549 RID: 9545
		[SerializeField]
		private Text classLevelText;
	}
}
