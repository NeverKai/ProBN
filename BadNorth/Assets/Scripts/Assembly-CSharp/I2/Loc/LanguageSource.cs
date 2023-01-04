using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003E3 RID: 995
	[AddComponentMenu("I2/Localization/Source")]
	[ExecuteInEditMode]
	public class LanguageSource : MonoBehaviour, ISerializationCallbackReceiver, ILanguageSource
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00035367 File Offset: 0x00033767
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x0003536F File Offset: 0x0003376F
		public LanguageSourceData SourceData
		{
			get
			{
				return this.mSource;
			}
			set
			{
				this.mSource = value;
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06001686 RID: 5766 RVA: 0x00035378 File Offset: 0x00033778
		// (remove) Token: 0x06001687 RID: 5767 RVA: 0x000353B0 File Offset: 0x000337B0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<LanguageSourceData, bool, string> Event_OnSourceUpdateFromGoogle;

		// Token: 0x06001688 RID: 5768 RVA: 0x000353E6 File Offset: 0x000337E6
		private void Awake()
		{
			this.mSource.owner = this;
			this.mSource.Awake();
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000353FF File Offset: 0x000337FF
		private void OnDestroy()
		{
			this.NeverDestroy = false;
			if (!this.NeverDestroy)
			{
				this.mSource.OnDestroy();
			}
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00035420 File Offset: 0x00033820
		public string GetSourceName()
		{
			string text = base.gameObject.name;
			Transform parent = base.transform.parent;
			while (parent)
			{
				text = parent.name + "_" + text;
				parent = parent.parent;
			}
			return text;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0003546F File Offset: 0x0003386F
		public void OnBeforeSerialize()
		{
			this.version = 1;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00035478 File Offset: 0x00033878
		public void OnAfterDeserialize()
		{
			if (this.version == 0 || this.mSource == null)
			{
				this.mSource = new LanguageSourceData();
				this.mSource.owner = this;
				this.mSource.UserAgreesToHaveItOnTheScene = this.UserAgreesToHaveItOnTheScene;
				this.mSource.UserAgreesToHaveItInsideThePluginsFolder = this.UserAgreesToHaveItInsideThePluginsFolder;
				this.mSource.IgnoreDeviceLanguage = this.IgnoreDeviceLanguage;
				this.mSource._AllowUnloadingLanguages = this._AllowUnloadingLanguages;
				this.mSource.CaseInsensitiveTerms = this.CaseInsensitiveTerms;
				this.mSource.OnMissingTranslation = this.OnMissingTranslation;
				this.mSource.mTerm_AppName = this.mTerm_AppName;
				this.mSource.GoogleLiveSyncIsUptoDate = this.GoogleLiveSyncIsUptoDate;
				this.mSource.Google_WebServiceURL = this.Google_WebServiceURL;
				this.mSource.Google_SpreadsheetKey = this.Google_SpreadsheetKey;
				this.mSource.Google_SpreadsheetName = this.Google_SpreadsheetName;
				this.mSource.Google_LastUpdatedVersion = this.Google_LastUpdatedVersion;
				this.mSource.GoogleUpdateFrequency = this.GoogleUpdateFrequency;
				this.mSource.GoogleUpdateDelay = this.GoogleUpdateDelay;
				this.mSource.Event_OnSourceUpdateFromGoogle += this.Event_OnSourceUpdateFromGoogle;
				if (this.mLanguages != null && this.mLanguages.Count > 0)
				{
					this.mSource.mLanguages.Clear();
					this.mSource.mLanguages.AddRange(this.mLanguages);
					this.mLanguages.Clear();
				}
				if (this.Assets != null && this.Assets.Count > 0)
				{
					this.mSource.Assets.Clear();
					this.mSource.Assets.AddRange(this.Assets);
					this.Assets.Clear();
				}
				if (this.mTerms != null && this.mTerms.Count > 0)
				{
					this.mSource.mTerms.Clear();
					for (int i = 0; i < this.mTerms.Count; i++)
					{
						this.mSource.mTerms.Add(this.mTerms[i]);
					}
					this.mTerms.Clear();
				}
				this.version = 1;
				this.Event_OnSourceUpdateFromGoogle = null;
			}
		}

		// Token: 0x04000DFC RID: 3580
		public LanguageSourceData mSource = new LanguageSourceData();

		// Token: 0x04000DFD RID: 3581
		public int version;

		// Token: 0x04000DFE RID: 3582
		public bool NeverDestroy;

		// Token: 0x04000DFF RID: 3583
		public bool UserAgreesToHaveItOnTheScene;

		// Token: 0x04000E00 RID: 3584
		public bool UserAgreesToHaveItInsideThePluginsFolder;

		// Token: 0x04000E01 RID: 3585
		public bool GoogleLiveSyncIsUptoDate = true;

		// Token: 0x04000E02 RID: 3586
		public List<UnityEngine.Object> Assets = new List<UnityEngine.Object>();

		// Token: 0x04000E03 RID: 3587
		public string Google_WebServiceURL;

		// Token: 0x04000E04 RID: 3588
		public string Google_SpreadsheetKey;

		// Token: 0x04000E05 RID: 3589
		public string Google_SpreadsheetName;

		// Token: 0x04000E06 RID: 3590
		public string Google_LastUpdatedVersion;

		// Token: 0x04000E07 RID: 3591
		public LanguageSourceData.eGoogleUpdateFrequency GoogleUpdateFrequency = LanguageSourceData.eGoogleUpdateFrequency.Weekly;

		// Token: 0x04000E08 RID: 3592
		public float GoogleUpdateDelay = 5f;

		// Token: 0x04000E0A RID: 3594
		public List<LanguageData> mLanguages = new List<LanguageData>();

		// Token: 0x04000E0B RID: 3595
		public bool IgnoreDeviceLanguage;

		// Token: 0x04000E0C RID: 3596
		public LanguageSourceData.eAllowUnloadLanguages _AllowUnloadingLanguages;

		// Token: 0x04000E0D RID: 3597
		public List<TermData> mTerms = new List<TermData>();

		// Token: 0x04000E0E RID: 3598
		public bool CaseInsensitiveTerms;

		// Token: 0x04000E0F RID: 3599
		public LanguageSourceData.MissingTranslationAction OnMissingTranslation = LanguageSourceData.MissingTranslationAction.Fallback;

		// Token: 0x04000E10 RID: 3600
		public string mTerm_AppName;
	}
}
