using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace I2.Loc
{
	// Token: 0x020003ED RID: 1005
	[AddComponentMenu("I2/Localization/I2 Localize")]
	public class Localize : MonoBehaviour
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00038B65 File Offset: 0x00036F65
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x00038B6D File Offset: 0x00036F6D
		public string Term
		{
			get
			{
				return this.mTerm;
			}
			set
			{
				this.SetTerm(value);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00038B76 File Offset: 0x00036F76
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x00038B7E File Offset: 0x00036F7E
		public string SecondaryTerm
		{
			get
			{
				return this.mTermSecondary;
			}
			set
			{
				this.SetTerm(null, value);
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00038B88 File Offset: 0x00036F88
		private void Awake()
		{
			this.UpdateAssetDictionary();
			this.FindTarget();
			if (this.LocalizeOnAwake)
			{
				this.OnLocalize(false);
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00038BA9 File Offset: 0x00036FA9
		private void OnEnable()
		{
			this.OnLocalize(false);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00038BB2 File Offset: 0x00036FB2
		public bool HasCallback()
		{
			return this.LocalizeCallBack.HasCallback() || this.LocalizeEvent.GetPersistentEventCount() > 0;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00038BD4 File Offset: 0x00036FD4
		public void OnLocalize(bool Force = false)
		{
			if (!Force && (!base.enabled || base.gameObject == null || !base.gameObject.activeInHierarchy))
			{
				return;
			}
			if (string.IsNullOrEmpty(LocalizationManager.CurrentLanguage))
			{
				return;
			}
			if (!this.AlwaysForceLocalize && !Force && !this.HasCallback() && this.LastLocalizedLanguage == LocalizationManager.CurrentLanguage)
			{
				return;
			}
			this.LastLocalizedLanguage = LocalizationManager.CurrentLanguage;
			if (string.IsNullOrEmpty(this.FinalTerm) || string.IsNullOrEmpty(this.FinalSecondaryTerm))
			{
				this.GetFinalTerms(out this.FinalTerm, out this.FinalSecondaryTerm);
			}
			bool flag = I2Utils.IsPlaying() && this.HasCallback();
			if (!flag && string.IsNullOrEmpty(this.FinalTerm) && string.IsNullOrEmpty(this.FinalSecondaryTerm))
			{
				return;
			}
			Localize.CallBackTerm = this.FinalTerm;
			Localize.CallBackSecondaryTerm = this.FinalSecondaryTerm;
			Localize.MainTranslation = ((!string.IsNullOrEmpty(this.FinalTerm) && !(this.FinalTerm == "-")) ? LocalizationManager.GetTranslation(this.FinalTerm, false, 0, true, false, null, null) : null);
			Localize.SecondaryTranslation = ((!string.IsNullOrEmpty(this.FinalSecondaryTerm) && !(this.FinalSecondaryTerm == "-")) ? LocalizationManager.GetTranslation(this.FinalSecondaryTerm, false, 0, true, false, null, null) : null);
			if (!flag && string.IsNullOrEmpty(this.FinalTerm) && string.IsNullOrEmpty(Localize.SecondaryTranslation))
			{
				return;
			}
			Localize.CurrentLocalizeComponent = this;
			this.LocalizeCallBack.Execute(this);
			this.LocalizeEvent.Invoke();
			LocalizationManager.ApplyLocalizationParams(ref Localize.MainTranslation, base.gameObject, this.AllowLocalizedParameters);
			if (!this.FindTarget())
			{
				return;
			}
			bool flag2 = LocalizationManager.IsRight2Left && !this.IgnoreRTL;
			if (Localize.MainTranslation != null)
			{
				switch (this.PrimaryTermModifier)
				{
				case Localize.TermModification.ToUpper:
					Localize.MainTranslation = Localize.MainTranslation.ToUpper();
					break;
				case Localize.TermModification.ToLower:
					Localize.MainTranslation = Localize.MainTranslation.ToLower();
					break;
				case Localize.TermModification.ToUpperFirst:
					Localize.MainTranslation = GoogleTranslation.UppercaseFirst(Localize.MainTranslation);
					break;
				case Localize.TermModification.ToTitle:
					Localize.MainTranslation = GoogleTranslation.TitleCase(Localize.MainTranslation);
					break;
				}
				if (!string.IsNullOrEmpty(this.TermPrefix))
				{
					Localize.MainTranslation = ((!flag2) ? (this.TermPrefix + Localize.MainTranslation) : (Localize.MainTranslation + this.TermPrefix));
				}
				if (!string.IsNullOrEmpty(this.TermSuffix))
				{
					Localize.MainTranslation = ((!flag2) ? (Localize.MainTranslation + this.TermSuffix) : (this.TermSuffix + Localize.MainTranslation));
				}
				if (this.AddSpacesToJoinedLanguages && LocalizationManager.HasJoinedWords && !string.IsNullOrEmpty(Localize.MainTranslation))
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(Localize.MainTranslation[0]);
					int i = 1;
					int length = Localize.MainTranslation.Length;
					while (i < length)
					{
						stringBuilder.Append(' ');
						stringBuilder.Append(Localize.MainTranslation[i]);
						i++;
					}
					Localize.MainTranslation = stringBuilder.ToString();
				}
				if (flag2 && this.mLocalizeTarget.AllowMainTermToBeRTL() && !string.IsNullOrEmpty(Localize.MainTranslation))
				{
					Localize.MainTranslation = LocalizationManager.ApplyRTLfix(Localize.MainTranslation, this.MaxCharactersInRTL, this.IgnoreNumbersInRTL);
				}
			}
			if (Localize.SecondaryTranslation != null)
			{
				switch (this.SecondaryTermModifier)
				{
				case Localize.TermModification.ToUpper:
					Localize.SecondaryTranslation = Localize.SecondaryTranslation.ToUpper();
					break;
				case Localize.TermModification.ToLower:
					Localize.SecondaryTranslation = Localize.SecondaryTranslation.ToLower();
					break;
				case Localize.TermModification.ToUpperFirst:
					Localize.SecondaryTranslation = GoogleTranslation.UppercaseFirst(Localize.SecondaryTranslation);
					break;
				case Localize.TermModification.ToTitle:
					Localize.SecondaryTranslation = GoogleTranslation.TitleCase(Localize.SecondaryTranslation);
					break;
				}
				if (flag2 && this.mLocalizeTarget.AllowSecondTermToBeRTL() && !string.IsNullOrEmpty(Localize.SecondaryTranslation))
				{
					Localize.SecondaryTranslation = LocalizationManager.ApplyRTLfix(Localize.SecondaryTranslation);
				}
			}
			if (LocalizationManager.HighlightLocalizedTargets)
			{
				Localize.MainTranslation = "LOC:" + this.FinalTerm;
			}
			this.mLocalizeTarget.DoLocalize(this, Localize.MainTranslation, Localize.SecondaryTranslation);
			Localize.CurrentLocalizeComponent = null;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00039098 File Offset: 0x00037498
		public static void ClearLocalizeTargets()
		{
			for (int i = 0; i < Localize.mLocalizeTargets.Count; i++)
			{
				if (Localize.mLocalizeTargets[i] != null)
				{
					UnityEngine.Object.DestroyImmediate(Localize.mLocalizeTargets[i]);
					Localize.mLocalizeTargets[i] = null;
				}
			}
			Localize.mLocalizeTargets.Clear();
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000390FC File Offset: 0x000374FC
		public bool FindTarget()
		{
			if (this.mLocalizeTarget != null && this.mLocalizeTarget.IsValid(this))
			{
				return true;
			}
			if (this.mLocalizeTarget != null)
			{
				UnityEngine.Object.DestroyImmediate(this.mLocalizeTarget);
				this.mLocalizeTarget = null;
				this.mLocalizeTargetName = null;
			}
			if (!string.IsNullOrEmpty(this.mLocalizeTargetName))
			{
				foreach (ILocalizeTargetDescriptor localizeTargetDescriptor in LocalizationManager.mLocalizeTargets)
				{
					if (this.mLocalizeTargetName == localizeTargetDescriptor.GetTargetType().ToString())
					{
						if (localizeTargetDescriptor.CanLocalize(this))
						{
							this.mLocalizeTarget = localizeTargetDescriptor.CreateTarget(this);
						}
						if (this.mLocalizeTarget != null)
						{
							Localize.mLocalizeTargets.Add(this.mLocalizeTarget);
							return true;
						}
					}
				}
			}
			foreach (ILocalizeTargetDescriptor localizeTargetDescriptor2 in LocalizationManager.mLocalizeTargets)
			{
				if (localizeTargetDescriptor2.CanLocalize(this))
				{
					this.mLocalizeTarget = localizeTargetDescriptor2.CreateTarget(this);
					this.mLocalizeTargetName = localizeTargetDescriptor2.GetTargetType().ToString();
					if (this.mLocalizeTarget != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00039298 File Offset: 0x00037698
		public void GetFinalTerms(out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = string.Empty;
			secondaryTerm = string.Empty;
			if (!this.FindTarget())
			{
				return;
			}
			if (this.mLocalizeTarget != null)
			{
				this.mLocalizeTarget.GetFinalTerms(this, this.mTerm, this.mTermSecondary, out primaryTerm, out secondaryTerm);
				primaryTerm = I2Utils.GetValidTermName(primaryTerm, false);
			}
			if (!string.IsNullOrEmpty(this.mTerm))
			{
				primaryTerm = this.mTerm;
			}
			if (!string.IsNullOrEmpty(this.mTermSecondary))
			{
				secondaryTerm = this.mTermSecondary;
			}
			if (primaryTerm != null)
			{
				primaryTerm = primaryTerm.Trim();
			}
			if (secondaryTerm != null)
			{
				secondaryTerm = secondaryTerm.Trim();
			}
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00039344 File Offset: 0x00037744
		public string GetMainTargetsText()
		{
			string text = null;
			string text2 = null;
			if (this.mLocalizeTarget != null)
			{
				this.mLocalizeTarget.GetFinalTerms(this, null, null, out text, out text2);
			}
			return (!string.IsNullOrEmpty(text)) ? text : this.mTerm;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0003938F File Offset: 0x0003778F
		public void SetFinalTerms(string Main, string Secondary, out string primaryTerm, out string secondaryTerm, bool RemoveNonASCII)
		{
			primaryTerm = ((!RemoveNonASCII) ? Main : I2Utils.GetValidTermName(Main, false));
			secondaryTerm = Secondary;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000393AC File Offset: 0x000377AC
		public void SetTerm(string primary)
		{
			if (!string.IsNullOrEmpty(primary))
			{
				this.mTerm = primary;
				this.FinalTerm = primary;
			}
			this.OnLocalize(true);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000393DC File Offset: 0x000377DC
		public void SetTerm(string primary, string secondary)
		{
			if (!string.IsNullOrEmpty(primary))
			{
				this.mTerm = primary;
				this.FinalTerm = primary;
			}
			this.mTermSecondary = secondary;
			this.FinalSecondaryTerm = secondary;
			this.OnLocalize(true);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0003941C File Offset: 0x0003781C
		internal T GetSecondaryTranslatedObj<T>(ref string mainTranslation, ref string secondaryTranslation) where T : UnityEngine.Object
		{
			string text;
			string text2;
			this.DeserializeTranslation(mainTranslation, out text, out text2);
			T t = (T)((object)null);
			if (!string.IsNullOrEmpty(text2))
			{
				t = this.GetObject<T>(text2);
				if (t != null)
				{
					mainTranslation = text;
					secondaryTranslation = text2;
				}
			}
			if (t == null)
			{
				t = this.GetObject<T>(secondaryTranslation);
			}
			return t;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00039484 File Offset: 0x00037884
		public void UpdateAssetDictionary()
		{
			this.TranslatedObjects.RemoveAll((UnityEngine.Object x) => x == null);
			this.mAssetDictionary = this.TranslatedObjects.Distinct<UnityEngine.Object>().ToDictionary((UnityEngine.Object o) => o.name);
			IEnumerable<IGrouping<string, UnityEngine.Object>> source = from o in this.TranslatedObjects.Distinct<UnityEngine.Object>()
			group o by o.name;
			Func<IGrouping<string, UnityEngine.Object>, string> keySelector = (IGrouping<string, UnityEngine.Object> g) => g.Key;
			if (Localize.<>f__mg$cache0 == null)
			{
				Localize.<>f__mg$cache0 = new Func<IGrouping<string, UnityEngine.Object>, UnityEngine.Object>(Enumerable.First<UnityEngine.Object>);
			}
			this.mAssetDictionary = source.ToDictionary(keySelector, Localize.<>f__mg$cache0);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00039560 File Offset: 0x00037960
		internal T GetObject<T>(string Translation) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(Translation))
			{
				return (T)((object)null);
			}
			return this.GetTranslatedObject<T>(Translation);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00039588 File Offset: 0x00037988
		private T GetTranslatedObject<T>(string Translation) where T : UnityEngine.Object
		{
			return this.FindTranslatedObject<T>(Translation);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000395A0 File Offset: 0x000379A0
		private void DeserializeTranslation(string translation, out string value, out string secondary)
		{
			if (!string.IsNullOrEmpty(translation) && translation.Length > 1 && translation[0] == '[')
			{
				int num = translation.IndexOf(']');
				if (num > 0)
				{
					secondary = translation.Substring(1, num - 1);
					value = translation.Substring(num + 1);
					return;
				}
			}
			value = translation;
			secondary = string.Empty;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00039604 File Offset: 0x00037A04
		public T FindTranslatedObject<T>(string value) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(value))
			{
				return (T)((object)null);
			}
			if (this.mAssetDictionary == null || this.mAssetDictionary.Count != this.TranslatedObjects.Count)
			{
				this.UpdateAssetDictionary();
			}
			foreach (KeyValuePair<string, UnityEngine.Object> keyValuePair in this.mAssetDictionary)
			{
				if (keyValuePair.Value is T && value.EndsWith(keyValuePair.Key, StringComparison.OrdinalIgnoreCase) && string.Compare(value, keyValuePair.Key, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return (T)((object)keyValuePair.Value);
				}
			}
			T t = LocalizationManager.FindAsset(value) as T;
			if (t)
			{
				return t;
			}
			return ResourceManager.pInstance.GetAsset<T>(value);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00039714 File Offset: 0x00037B14
		public bool HasTranslatedObject(UnityEngine.Object Obj)
		{
			return this.TranslatedObjects.Contains(Obj) || ResourceManager.pInstance.HasAsset(Obj);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00039734 File Offset: 0x00037B34
		public void AddTranslatedObject(UnityEngine.Object Obj)
		{
			if (this.TranslatedObjects.Contains(Obj))
			{
				return;
			}
			this.TranslatedObjects.Add(Obj);
			this.UpdateAssetDictionary();
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0003975A File Offset: 0x00037B5A
		public void SetGlobalLanguage(string Language)
		{
			LocalizationManager.CurrentLanguage = Language;
		}

		// Token: 0x04000E4C RID: 3660
		public string mTerm = string.Empty;

		// Token: 0x04000E4D RID: 3661
		public string mTermSecondary = string.Empty;

		// Token: 0x04000E4E RID: 3662
		[NonSerialized]
		public string FinalTerm;

		// Token: 0x04000E4F RID: 3663
		[NonSerialized]
		public string FinalSecondaryTerm;

		// Token: 0x04000E50 RID: 3664
		public Localize.TermModification PrimaryTermModifier;

		// Token: 0x04000E51 RID: 3665
		public Localize.TermModification SecondaryTermModifier;

		// Token: 0x04000E52 RID: 3666
		public string TermPrefix;

		// Token: 0x04000E53 RID: 3667
		public string TermSuffix;

		// Token: 0x04000E54 RID: 3668
		public bool LocalizeOnAwake = true;

		// Token: 0x04000E55 RID: 3669
		private string LastLocalizedLanguage;

		// Token: 0x04000E56 RID: 3670
		public bool IgnoreRTL;

		// Token: 0x04000E57 RID: 3671
		public int MaxCharactersInRTL;

		// Token: 0x04000E58 RID: 3672
		public bool IgnoreNumbersInRTL = true;

		// Token: 0x04000E59 RID: 3673
		public bool CorrectAlignmentForRTL = true;

		// Token: 0x04000E5A RID: 3674
		public bool AddSpacesToJoinedLanguages;

		// Token: 0x04000E5B RID: 3675
		public bool AllowLocalizedParameters = true;

		// Token: 0x04000E5C RID: 3676
		public List<UnityEngine.Object> TranslatedObjects = new List<UnityEngine.Object>();

		// Token: 0x04000E5D RID: 3677
		[NonSerialized]
		public Dictionary<string, UnityEngine.Object> mAssetDictionary = new Dictionary<string, UnityEngine.Object>(StringComparer.Ordinal);

		// Token: 0x04000E5E RID: 3678
		public UnityEvent LocalizeEvent = new UnityEvent();

		// Token: 0x04000E5F RID: 3679
		public static string MainTranslation;

		// Token: 0x04000E60 RID: 3680
		public static string SecondaryTranslation;

		// Token: 0x04000E61 RID: 3681
		public static string CallBackTerm;

		// Token: 0x04000E62 RID: 3682
		public static string CallBackSecondaryTerm;

		// Token: 0x04000E63 RID: 3683
		public static Localize CurrentLocalizeComponent;

		// Token: 0x04000E64 RID: 3684
		public bool AlwaysForceLocalize;

		// Token: 0x04000E65 RID: 3685
		[SerializeField]
		public EventCallback LocalizeCallBack = new EventCallback();

		// Token: 0x04000E66 RID: 3686
		public bool mGUI_ShowReferences;

		// Token: 0x04000E67 RID: 3687
		public bool mGUI_ShowTems = true;

		// Token: 0x04000E68 RID: 3688
		public bool mGUI_ShowCallback;

		// Token: 0x04000E69 RID: 3689
		public ILocalizeTarget mLocalizeTarget;

		// Token: 0x04000E6A RID: 3690
		public string mLocalizeTargetName;

		// Token: 0x04000E6B RID: 3691
		private static List<ILocalizeTarget> mLocalizeTargets = new List<ILocalizeTarget>();

		// Token: 0x04000E6C RID: 3692
		[CompilerGenerated]
		private static Func<IGrouping<string, UnityEngine.Object>, UnityEngine.Object> <>f__mg$cache0;

		// Token: 0x020003EE RID: 1006
		public enum TermModification
		{
			// Token: 0x04000E72 RID: 3698
			DontModify,
			// Token: 0x04000E73 RID: 3699
			ToUpper,
			// Token: 0x04000E74 RID: 3700
			ToLower,
			// Token: 0x04000E75 RID: 3701
			ToUpperFirst,
			// Token: 0x04000E76 RID: 3702
			ToTitle
		}
	}
}
