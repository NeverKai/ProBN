using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000749 RID: 1865
	public class CreditsGroupUI : MonoBehaviour
	{
		// Token: 0x060030A9 RID: 12457 RVA: 0x000C6CE8 File Offset: 0x000C50E8
		public void Setup(CreditsGroupUI.Def def)
		{
			Text component = this.titleText.GetComponent<Text>();
			if (!string.IsNullOrEmpty(def.titleTerm))
			{
				this.titleText.Term = def.titleTerm;
			}
			else if (!string.IsNullOrEmpty(def.titleUntranslated))
			{
				component.text = def.titleUntranslated;
			}
			else
			{
				this.titleText.gameObject.SetActive(false);
			}
			base.gameObject.name = component.text;
			if (!string.IsNullOrEmpty(def.names))
			{
				this.namesText.text = def.names;
			}
			else
			{
				this.namesText.gameObject.SetActive(false);
			}
		}

		// Token: 0x0400207A RID: 8314
		public CreditsGroupUI.Type type = CreditsGroupUI.Type.subgroup;

		// Token: 0x0400207B RID: 8315
		[SerializeField]
		private Localize titleText;

		// Token: 0x0400207C RID: 8316
		[SerializeField]
		private Text namesText;

		// Token: 0x0200074A RID: 1866
		public enum Type
		{
			// Token: 0x0400207E RID: 8318
			company,
			// Token: 0x0400207F RID: 8319
			group,
			// Token: 0x04002080 RID: 8320
			subgroup
		}

		// Token: 0x0200074B RID: 1867
		[Serializable]
		public class Def
		{
			// Token: 0x04002081 RID: 8321
			public string titleUntranslated = string.Empty;

			// Token: 0x04002082 RID: 8322
			[TermsPopup("")]
			public string titleTerm = string.Empty;

			// Token: 0x04002083 RID: 8323
			public CreditsGroupUI.Type type = CreditsGroupUI.Type.subgroup;

			// Token: 0x04002084 RID: 8324
			[TextArea(2, 15)]
			public string names = string.Empty;
		}
	}
}
