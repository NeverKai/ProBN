using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	// Token: 0x020003EF RID: 1007
	[AddComponentMenu("I2/Localization/Localize Dropdown")]
	public class LocalizeDropdown : MonoBehaviour
	{
		// Token: 0x06001710 RID: 5904 RVA: 0x000397A2 File Offset: 0x00037BA2
		public void Start()
		{
			LocalizationManager.OnLocalizeEvent += this.OnLocalize;
			this.OnLocalize();
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000397BB File Offset: 0x00037BBB
		public void OnDestroy()
		{
			LocalizationManager.OnLocalizeEvent -= this.OnLocalize;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000397CE File Offset: 0x00037BCE
		private void OnEnable()
		{
			if (this._Terms.Count == 0)
			{
				this.FillValues();
			}
			this.OnLocalize();
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000397EC File Offset: 0x00037BEC
		public void OnLocalize()
		{
			if (!base.enabled || base.gameObject == null || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (string.IsNullOrEmpty(LocalizationManager.CurrentLanguage))
			{
				return;
			}
			this.UpdateLocalization();
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0003983C File Offset: 0x00037C3C
		private void FillValues()
		{
			Dropdown component = base.GetComponent<Dropdown>();
			if (component == null && I2Utils.IsPlaying())
			{
				return;
			}
			foreach (Dropdown.OptionData optionData in component.options)
			{
				this._Terms.Add(optionData.text);
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000398C0 File Offset: 0x00037CC0
		public void UpdateLocalization()
		{
			Dropdown component = base.GetComponent<Dropdown>();
			if (component == null)
			{
				return;
			}
			component.options.Clear();
			foreach (string term in this._Terms)
			{
				string translation = LocalizationManager.GetTranslation(term, true, 0, true, false, null, null);
				component.options.Add(new Dropdown.OptionData(translation));
			}
			component.RefreshShownValue();
		}

		// Token: 0x04000E77 RID: 3703
		public List<string> _Terms = new List<string>();
	}
}
