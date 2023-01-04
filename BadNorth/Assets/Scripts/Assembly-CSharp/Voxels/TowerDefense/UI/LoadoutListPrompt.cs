using System;
using I2.Loc;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008DA RID: 2266
	public class LoadoutListPrompt : MonoBehaviour
	{
		// Token: 0x06003BEC RID: 15340 RVA: 0x0010A7B9 File Offset: 0x00108BB9
		private void Awake()
		{
			this.buttonImage.sprite = Singleton<UIManager>.instance.GetActionIcon(EUIPadAction.Submit);
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x0010A7D1 File Offset: 0x00108BD1
		public void Setup(HeroDefinition hero)
		{
			if (hero)
			{
				this.actionText.Term = "LOADOUT/PROMPT_REPLACE";
			}
			else
			{
				this.actionText.Term = "LOADOUT/PROMPT_ASSIGN";
			}
		}

		// Token: 0x040029B3 RID: 10675
		[SerializeField]
		private Image buttonImage;

		// Token: 0x040029B4 RID: 10676
		[SerializeField]
		private Localize actionText;
	}
}
