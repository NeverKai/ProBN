using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020008E0 RID: 2272
	internal class LocalizeGameObject : MonoBehaviour
	{
		// Token: 0x06003C3D RID: 15421 RVA: 0x0010C34F File Offset: 0x0010A74F
		private void Start()
		{
			UserSettings.onUpdated += this.UpdateVisibility;
			this.UpdateVisibility(Profile.userSettings);
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0010C36D File Offset: 0x0010A76D
		private void OnDestroy()
		{
			UserSettings.onUpdated -= this.UpdateVisibility;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x0010C380 File Offset: 0x0010A780
		private void UpdateVisibility(UserSettings settings)
		{
			bool flag = false;
			foreach (LocalizeGameObject.LanguageMap languageMap in this.overrides)
			{
				bool flag2 = languageMap.languages.Contains(settings.language);
				languageMap.target.SetActive(flag2);
				flag = (flag || flag2);
			}
			this.defaultTarget.SetActive(!flag);
		}

		// Token: 0x040029F7 RID: 10743
		[SerializeField]
		private GameObject defaultTarget;

		// Token: 0x040029F8 RID: 10744
		[SerializeField]
		private List<LocalizeGameObject.LanguageMap> overrides;

		// Token: 0x020008E1 RID: 2273
		[Serializable]
		private struct LanguageMap
		{
			// Token: 0x040029F9 RID: 10745
			public GameObject target;

			// Token: 0x040029FA RID: 10746
			public List<UserSettings.Language> languages;
		}
	}
}
