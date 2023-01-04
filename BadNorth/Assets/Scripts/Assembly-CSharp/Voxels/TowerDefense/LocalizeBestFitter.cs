using System;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020008DF RID: 2271
	internal class LocalizeBestFitter : MonoBehaviour, IGameSetup
	{
		// Token: 0x06003C3A RID: 15418 RVA: 0x0010C2DC File Offset: 0x0010A6DC
		private void OnLanguageChanged()
		{
			UserSettings.Language languageEnumFromI2Code = UserSettings.GetLanguageEnumFromI2Code(LocalizationManager.CurrentLanguage);
			this.text.resizeTextForBestFit = this.bestFitLanguages.Contains(languageEnumFromI2Code);
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x0010C30B File Offset: 0x0010A70B
		void IGameSetup.OnGameAwake()
		{
			this.text = base.GetComponent<Text>();
			this.localize = base.GetComponent<Localize>();
			this.localize.LocalizeEvent.AddListener(new UnityAction(this.OnLanguageChanged));
			this.OnLanguageChanged();
		}

		// Token: 0x040029F4 RID: 10740
		[SerializeField]
		private List<UserSettings.Language> bestFitLanguages = new List<UserSettings.Language>();

		// Token: 0x040029F5 RID: 10741
		private Text text;

		// Token: 0x040029F6 RID: 10742
		private Localize localize;
	}
}
