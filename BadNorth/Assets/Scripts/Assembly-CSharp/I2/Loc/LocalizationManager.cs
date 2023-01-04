using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003F0 RID: 1008
	public static class LocalizationManager
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x00039958 File Offset: 0x00037D58
		public static void InitializeIfNeeded()
		{
			if (string.IsNullOrEmpty(LocalizationManager.mCurrentLanguage) || LocalizationManager.Sources.Count == 0)
			{
				LocalizationManager.AutoLoadGlobalParamManagers();
				LocalizationManager.UpdateSources();
				LocalizationManager.SelectStartupLanguage();
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00039988 File Offset: 0x00037D88
		public static string GetVersion()
		{
			return "2.8.12 f1";
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0003998F File Offset: 0x00037D8F
		public static int GetRequiredWebServiceVersion()
		{
			return 5;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00039994 File Offset: 0x00037D94
		public static string GetWebServiceURL(LanguageSourceData source = null)
		{
			if (source != null && !string.IsNullOrEmpty(source.Google_WebServiceURL))
			{
				return source.Google_WebServiceURL;
			}
			LocalizationManager.InitializeIfNeeded();
			for (int i = 0; i < LocalizationManager.Sources.Count; i++)
			{
				if (LocalizationManager.Sources[i] != null && !string.IsNullOrEmpty(LocalizationManager.Sources[i].Google_WebServiceURL))
				{
					return LocalizationManager.Sources[i].Google_WebServiceURL;
				}
			}
			return string.Empty;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x00039A1E File Offset: 0x00037E1E
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x00039A2C File Offset: 0x00037E2C
		public static string CurrentLanguage
		{
			get
			{
				LocalizationManager.InitializeIfNeeded();
				return LocalizationManager.mCurrentLanguage;
			}
			set
			{
				LocalizationManager.InitializeIfNeeded();
				string supportedLanguage = LocalizationManager.GetSupportedLanguage(value, false);
				if (!string.IsNullOrEmpty(supportedLanguage) && LocalizationManager.mCurrentLanguage != supportedLanguage)
				{
					LocalizationManager.SetLanguageAndCode(supportedLanguage, LocalizationManager.GetLanguageCode(supportedLanguage), true, false);
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x00039A6F File Offset: 0x00037E6F
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x00039A7C File Offset: 0x00037E7C
		public static string CurrentLanguageCode
		{
			get
			{
				LocalizationManager.InitializeIfNeeded();
				return LocalizationManager.mLanguageCode;
			}
			set
			{
				LocalizationManager.InitializeIfNeeded();
				if (LocalizationManager.mLanguageCode != value)
				{
					string languageFromCode = LocalizationManager.GetLanguageFromCode(value, true);
					if (!string.IsNullOrEmpty(languageFromCode))
					{
						LocalizationManager.SetLanguageAndCode(languageFromCode, value, true, false);
					}
				}
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00039ABC File Offset: 0x00037EBC
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x00039B38 File Offset: 0x00037F38
		public static string CurrentRegion
		{
			get
			{
				string currentLanguage = LocalizationManager.CurrentLanguage;
				int num = currentLanguage.IndexOfAny("/\\".ToCharArray());
				if (num > 0)
				{
					return currentLanguage.Substring(num + 1);
				}
				num = currentLanguage.IndexOfAny("[(".ToCharArray());
				int num2 = currentLanguage.LastIndexOfAny("])".ToCharArray());
				if (num > 0 && num != num2)
				{
					return currentLanguage.Substring(num + 1, num2 - num - 1);
				}
				return string.Empty;
			}
			set
			{
				string text = LocalizationManager.CurrentLanguage;
				int num = text.IndexOfAny("/\\".ToCharArray());
				if (num > 0)
				{
					LocalizationManager.CurrentLanguage = text.Substring(num + 1) + value;
					return;
				}
				num = text.IndexOfAny("[(".ToCharArray());
				int num2 = text.LastIndexOfAny("])".ToCharArray());
				if (num > 0 && num != num2)
				{
					text = text.Substring(num);
				}
				LocalizationManager.CurrentLanguage = text + "(" + value + ")";
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00039BC8 File Offset: 0x00037FC8
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00039C08 File Offset: 0x00038008
		public static string CurrentRegionCode
		{
			get
			{
				string currentLanguageCode = LocalizationManager.CurrentLanguageCode;
				int num = currentLanguageCode.IndexOfAny(" -_/\\".ToCharArray());
				return (num >= 0) ? currentLanguageCode.Substring(num + 1) : string.Empty;
			}
			set
			{
				string text = LocalizationManager.CurrentLanguageCode;
				int num = text.IndexOfAny(" -_/\\".ToCharArray());
				if (num > 0)
				{
					text = text.Substring(0, num);
				}
				LocalizationManager.CurrentLanguageCode = text + "-" + value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00039C4D File Offset: 0x0003804D
		public static CultureInfo CurrentCulture
		{
			get
			{
				return LocalizationManager.mCurrentCulture;
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00039C54 File Offset: 0x00038054
		public static void SetLanguageAndCode(string LanguageName, string LanguageCode, bool RememberLanguage = true, bool Force = false)
		{
			if (LocalizationManager.mCurrentLanguage != LanguageName || LocalizationManager.mLanguageCode != LanguageCode || Force)
			{
				if (RememberLanguage)
				{
					PersistentStorage.SetSetting_String("I2 Language", LanguageName);
				}
				LocalizationManager.mCurrentLanguage = LanguageName;
				LocalizationManager.mLanguageCode = LanguageCode;
				LocalizationManager.mCurrentCulture = LocalizationManager.CreateCultureForCode(LanguageCode);
				if (LocalizationManager.mChangeCultureInfo)
				{
					LocalizationManager.SetCurrentCultureInfo();
				}
				LocalizationManager.IsRight2Left = LocalizationManager.IsRTL(LocalizationManager.mLanguageCode);
				LocalizationManager.HasJoinedWords = GoogleLanguages.LanguageCode_HasJoinedWord(LocalizationManager.mLanguageCode);
				LocalizationManager.LocalizeAll(Force);
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00039CE4 File Offset: 0x000380E4
		private static CultureInfo CreateCultureForCode(string code)
		{
			CultureInfo result;
			try
			{
				result = CultureInfo.CreateSpecificCulture(code);
			}
			catch (Exception)
			{
				result = CultureInfo.InvariantCulture;
			}
			return result;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00039D1C File Offset: 0x0003811C
		public static void EnableChangingCultureInfo(bool bEnable)
		{
			if (!LocalizationManager.mChangeCultureInfo && bEnable)
			{
				LocalizationManager.SetCurrentCultureInfo();
			}
			LocalizationManager.mChangeCultureInfo = bEnable;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00039D39 File Offset: 0x00038139
		private static void SetCurrentCultureInfo()
		{
			Thread.CurrentThread.CurrentCulture = LocalizationManager.mCurrentCulture;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00039D4C File Offset: 0x0003814C
		private static void SelectStartupLanguage()
		{
			if (LocalizationManager.Sources.Count == 0)
			{
				return;
			}
			string setting_String = PersistentStorage.GetSetting_String("I2 Language", string.Empty);
			string currentDeviceLanguage = LocalizationManager.GetCurrentDeviceLanguage(false);
			if (!string.IsNullOrEmpty(setting_String) && LocalizationManager.HasLanguage(setting_String, true, false, true))
			{
				LocalizationManager.SetLanguageAndCode(setting_String, LocalizationManager.GetLanguageCode(setting_String), true, false);
				return;
			}
			if (!LocalizationManager.Sources[0].IgnoreDeviceLanguage)
			{
				string supportedLanguage = LocalizationManager.GetSupportedLanguage(currentDeviceLanguage, true);
				if (!string.IsNullOrEmpty(supportedLanguage))
				{
					LocalizationManager.SetLanguageAndCode(supportedLanguage, LocalizationManager.GetLanguageCode(supportedLanguage), false, false);
					return;
				}
			}
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				if (LocalizationManager.Sources[i].mLanguages.Count > 0)
				{
					for (int j = 0; j < LocalizationManager.Sources[i].mLanguages.Count; j++)
					{
						if (LocalizationManager.Sources[i].mLanguages[j].IsEnabled())
						{
							LocalizationManager.SetLanguageAndCode(LocalizationManager.Sources[i].mLanguages[j].Name, LocalizationManager.Sources[i].mLanguages[j].Code, false, false);
							return;
						}
					}
				}
				i++;
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00039EA4 File Offset: 0x000382A4
		public static bool HasLanguage(string Language, bool AllowDiscartingRegion = true, bool Initialize = true, bool SkipDisabled = true)
		{
			if (Initialize)
			{
				LocalizationManager.InitializeIfNeeded();
			}
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				if (LocalizationManager.Sources[i].GetLanguageIndex(Language, false, SkipDisabled) >= 0)
				{
					return true;
				}
				i++;
			}
			if (AllowDiscartingRegion)
			{
				int j = 0;
				int count2 = LocalizationManager.Sources.Count;
				while (j < count2)
				{
					if (LocalizationManager.Sources[j].GetLanguageIndex(Language, true, SkipDisabled) >= 0)
					{
						return true;
					}
					j++;
				}
			}
			return false;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00039F34 File Offset: 0x00038334
		public static string GetSupportedLanguage(string Language, bool ignoreDisabled = false)
		{
			string languageCode = GoogleLanguages.GetLanguageCode(Language, false);
			if (!string.IsNullOrEmpty(languageCode))
			{
				int i = 0;
				int count = LocalizationManager.Sources.Count;
				while (i < count)
				{
					int languageIndexFromCode = LocalizationManager.Sources[i].GetLanguageIndexFromCode(languageCode, true, ignoreDisabled);
					if (languageIndexFromCode >= 0)
					{
						return LocalizationManager.Sources[i].mLanguages[languageIndexFromCode].Name;
					}
					i++;
				}
				int j = 0;
				int count2 = LocalizationManager.Sources.Count;
				while (j < count2)
				{
					int languageIndexFromCode2 = LocalizationManager.Sources[j].GetLanguageIndexFromCode(languageCode, false, ignoreDisabled);
					if (languageIndexFromCode2 >= 0)
					{
						return LocalizationManager.Sources[j].mLanguages[languageIndexFromCode2].Name;
					}
					j++;
				}
			}
			int k = 0;
			int count3 = LocalizationManager.Sources.Count;
			while (k < count3)
			{
				int languageIndex = LocalizationManager.Sources[k].GetLanguageIndex(Language, false, ignoreDisabled);
				if (languageIndex >= 0)
				{
					return LocalizationManager.Sources[k].mLanguages[languageIndex].Name;
				}
				k++;
			}
			int l = 0;
			int count4 = LocalizationManager.Sources.Count;
			while (l < count4)
			{
				int languageIndex2 = LocalizationManager.Sources[l].GetLanguageIndex(Language, true, ignoreDisabled);
				if (languageIndex2 >= 0)
				{
					return LocalizationManager.Sources[l].mLanguages[languageIndex2].Name;
				}
				l++;
			}
			return string.Empty;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0003A0CC File Offset: 0x000384CC
		public static string GetLanguageCode(string Language)
		{
			if (LocalizationManager.Sources.Count == 0)
			{
				LocalizationManager.UpdateSources();
			}
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				int languageIndex = LocalizationManager.Sources[i].GetLanguageIndex(Language, true, true);
				if (languageIndex >= 0)
				{
					return LocalizationManager.Sources[i].mLanguages[languageIndex].Code;
				}
				i++;
			}
			return string.Empty;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0003A148 File Offset: 0x00038548
		public static string GetLanguageFromCode(string Code, bool exactMatch = true)
		{
			if (LocalizationManager.Sources.Count == 0)
			{
				LocalizationManager.UpdateSources();
			}
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				int languageIndexFromCode = LocalizationManager.Sources[i].GetLanguageIndexFromCode(Code, exactMatch, false);
				if (languageIndexFromCode >= 0)
				{
					return LocalizationManager.Sources[i].mLanguages[languageIndexFromCode].Name;
				}
				i++;
			}
			return string.Empty;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0003A1C4 File Offset: 0x000385C4
		public static List<string> GetAllLanguages(bool SkipDisabled = true)
		{
			if (LocalizationManager.Sources.Count == 0)
			{
				LocalizationManager.UpdateSources();
			}
			List<string> Languages = new List<string>();
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				Languages.AddRange(from x in LocalizationManager.Sources[i].GetLanguages(SkipDisabled)
				where !Languages.Contains(x)
				select x);
				i++;
			}
			return Languages;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0003A248 File Offset: 0x00038648
		public static List<string> GetAllLanguagesCode(bool allowRegions = true, bool SkipDisabled = true)
		{
			List<string> Languages = new List<string>();
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				Languages.AddRange(from x in LocalizationManager.Sources[i].GetLanguagesCode(allowRegions, SkipDisabled)
				where !Languages.Contains(x)
				select x);
				i++;
			}
			return Languages;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0003A2B8 File Offset: 0x000386B8
		public static bool IsLanguageEnabled(string Language)
		{
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				if (!LocalizationManager.Sources[i].IsLanguageEnabled(Language))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0003A2FC File Offset: 0x000386FC
		private static void LoadCurrentLanguage()
		{
			for (int i = 0; i < LocalizationManager.Sources.Count; i++)
			{
				int languageIndex = LocalizationManager.Sources[i].GetLanguageIndex(LocalizationManager.mCurrentLanguage, true, false);
				LocalizationManager.Sources[i].LoadLanguage(languageIndex, true, true, true, false);
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0003A351 File Offset: 0x00038751
		public static void PreviewLanguage(string NewLanguage)
		{
			LocalizationManager.mCurrentLanguage = NewLanguage;
			LocalizationManager.mLanguageCode = LocalizationManager.GetLanguageCode(LocalizationManager.mCurrentLanguage);
			LocalizationManager.IsRight2Left = LocalizationManager.IsRTL(LocalizationManager.mLanguageCode);
			LocalizationManager.HasJoinedWords = GoogleLanguages.LanguageCode_HasJoinedWord(LocalizationManager.mLanguageCode);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0003A388 File Offset: 0x00038788
		public static void AutoLoadGlobalParamManagers()
		{
			foreach (LocalizationParamsManager localizationParamsManager in UnityEngine.Object.FindObjectsOfType<LocalizationParamsManager>())
			{
				if (localizationParamsManager._IsGlobalManager && !LocalizationManager.ParamManagers.Contains(localizationParamsManager))
				{
					UnityEngine.Debug.Log(localizationParamsManager);
					LocalizationManager.ParamManagers.Add(localizationParamsManager);
				}
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0003A3DF File Offset: 0x000387DF
		public static void ApplyLocalizationParams(ref string translation, bool allowLocalizedParameters = true)
		{
			LocalizationManager.ApplyLocalizationParams(ref translation, (string p) => LocalizationManager.GetLocalizationParam(p, null), allowLocalizedParameters);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0003A408 File Offset: 0x00038808
		public static void ApplyLocalizationParams(ref string translation, GameObject root, bool allowLocalizedParameters = true)
		{
			LocalizationManager.ApplyLocalizationParams(ref translation, (string p) => LocalizationManager.GetLocalizationParam(p, root), allowLocalizedParameters);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0003A438 File Offset: 0x00038838
		public static void ApplyLocalizationParams(ref string translation, Dictionary<string, object> parameters, bool allowLocalizedParameters = true)
		{
			LocalizationManager.ApplyLocalizationParams(ref translation, delegate(string p)
			{
				object result = null;
				if (parameters.TryGetValue(p, out result))
				{
					return result;
				}
				return null;
			}, allowLocalizedParameters);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0003A468 File Offset: 0x00038868
		public static void ApplyLocalizationParams(ref string translation, LocalizationManager._GetParam getParam, bool allowLocalizedParameters = true)
		{
			if (translation == null)
			{
				return;
			}
			string text = null;
			int num = translation.Length;
			int num2 = 0;
			while (num2 >= 0 && num2 < translation.Length)
			{
				int num3 = translation.IndexOf("{[", num2);
				if (num3 < 0)
				{
					break;
				}
				int num4 = translation.IndexOf("]}", num3);
				if (num4 < 0)
				{
					break;
				}
				int num5 = translation.IndexOf("{[", num3 + 1);
				if (num5 > 0 && num5 < num4)
				{
					num2 = num5;
				}
				else
				{
					int num6 = (translation[num3 + 2] != '#') ? 2 : 3;
					string param = translation.Substring(num3 + num6, num4 - num3 - num6);
					string text2 = (string)getParam(param);
					if (text2 != null && allowLocalizedParameters)
					{
						LanguageSourceData languageSourceData;
						TermData termData = LocalizationManager.GetTermData(text2, out languageSourceData);
						if (termData != null)
						{
							int languageIndex = languageSourceData.GetLanguageIndex(LocalizationManager.CurrentLanguage, true, true);
							if (languageIndex >= 0)
							{
								text2 = termData.GetTranslation(languageIndex, null, false);
							}
						}
						string oldValue = translation.Substring(num3, num4 - num3 + 2);
						translation = translation.Replace(oldValue, text2);
						int n = 0;
						if (int.TryParse(text2, out n))
						{
							text = GoogleLanguages.GetPluralType(LocalizationManager.CurrentLanguageCode, n).ToString();
						}
						num2 = num3 + text2.Length;
					}
					else
					{
						num2 = num4 + 2;
					}
				}
			}
			if (text != null)
			{
				string text3 = "[i2p_" + text + "]";
				int num7 = translation.IndexOf(text3, StringComparison.OrdinalIgnoreCase);
				if (num7 < 0)
				{
					num7 = 0;
				}
				else
				{
					num7 += text3.Length;
				}
				num = translation.IndexOf("[i2p_", num7 + 1, StringComparison.OrdinalIgnoreCase);
				if (num < 0)
				{
					num = translation.Length;
				}
				translation = translation.Substring(num7, num - num7);
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0003A658 File Offset: 0x00038A58
		internal static string GetLocalizationParam(string ParamName, GameObject root)
		{
			if (root)
			{
				MonoBehaviour[] components = root.GetComponents<MonoBehaviour>();
				int i = 0;
				int num = components.Length;
				while (i < num)
				{
					ILocalizationParamsManager localizationParamsManager = components[i] as ILocalizationParamsManager;
					if (localizationParamsManager != null && components[i].enabled)
					{
						string parameterValue = localizationParamsManager.GetParameterValue(ParamName);
						if (parameterValue != null)
						{
							return parameterValue;
						}
					}
					i++;
				}
			}
			int j = 0;
			int count = LocalizationManager.ParamManagers.Count;
			while (j < count)
			{
				string parameterValue = LocalizationManager.ParamManagers[j].GetParameterValue(ParamName);
				if (parameterValue != null)
				{
					return parameterValue;
				}
				j++;
			}
			return null;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0003A700 File Offset: 0x00038B00
		private static string GetPluralType(MatchCollection matches, string langCode, LocalizationManager._GetParam getParam)
		{
			int i = 0;
			int count = matches.Count;
			while (i < count)
			{
				Match match = matches[i];
				string value = match.Groups[match.Groups.Count - 1].Value;
				string text = (string)getParam(value);
				if (text != null)
				{
					int n = 0;
					if (int.TryParse(text, out n))
					{
						return GoogleLanguages.GetPluralType(langCode, n).ToString();
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0003A795 File Offset: 0x00038B95
		public static string ApplyRTLfix(string line)
		{
			return LocalizationManager.ApplyRTLfix(line, 0, true);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0003A7A0 File Offset: 0x00038BA0
		public static string ApplyRTLfix(string line, int maxCharacters, bool ignoreNumbers)
		{
			if (string.IsNullOrEmpty(line))
			{
				return line;
			}
			char c = line[0];
			if (c == '!' || c == '.' || c == '?')
			{
				line = line.Substring(1) + c;
			}
			int num = -1;
			int num2 = 0;
			int num3 = 40000;
			num2 = 0;
			List<string> list = new List<string>();
			while (I2Utils.FindNextTag(line, num2, out num, out num2))
			{
				string str = "@@" + (char)(num3 + list.Count) + "@@";
				list.Add(line.Substring(num, num2 - num + 1));
				line = line.Substring(0, num) + str + line.Substring(num2 + 1);
				num2 = num + 5;
			}
			line = line.Replace("\r\n", "\n");
			line = I2Utils.SplitLine(line, maxCharacters);
			line = RTLFixer.Fix(line, true, !ignoreNumbers);
			for (int i = 0; i < list.Count; i++)
			{
				int length = line.Length;
				for (int j = 0; j < length; j++)
				{
					if (line[j] == '@' && line[j + 1] == '@' && (int)line[j + 2] >= num3 && line[j + 3] == '@' && line[j + 4] == '@')
					{
						int num4 = (int)line[j + 2] - num3;
						if (num4 % 2 == 0)
						{
							num4++;
						}
						else
						{
							num4--;
						}
						if (num4 >= list.Count)
						{
							num4 = list.Count - 1;
						}
						line = line.Substring(0, j) + list[num4] + line.Substring(j + 5);
						break;
					}
				}
			}
			return line;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0003A986 File Offset: 0x00038D86
		public static string FixRTL_IfNeeded(string text, int maxCharacters = 0, bool ignoreNumber = false)
		{
			if (LocalizationManager.IsRight2Left)
			{
				return LocalizationManager.ApplyRTLfix(text, maxCharacters, ignoreNumber);
			}
			return text;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0003A99C File Offset: 0x00038D9C
		public static bool IsRTL(string Code)
		{
			return Array.IndexOf<string>(LocalizationManager.LanguagesRTL, Code) >= 0;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0003A9AF File Offset: 0x00038DAF
		public static bool UpdateSources()
		{
			LocalizationManager.UnregisterDeletededSources();
			LocalizationManager.RegisterSourceInResources();
			LocalizationManager.RegisterSceneSources();
			return LocalizationManager.Sources.Count > 0;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0003A9D0 File Offset: 0x00038DD0
		private static void UnregisterDeletededSources()
		{
			for (int i = LocalizationManager.Sources.Count - 1; i >= 0; i--)
			{
				if (LocalizationManager.Sources[i] == null)
				{
					LocalizationManager.RemoveSource(LocalizationManager.Sources[i]);
				}
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0003AA1C File Offset: 0x00038E1C
		private static void RegisterSceneSources()
		{
			LanguageSource[] array = (LanguageSource[])Resources.FindObjectsOfTypeAll(typeof(LanguageSource));
			foreach (LanguageSource languageSource in array)
			{
				if (!LocalizationManager.Sources.Contains(languageSource.mSource))
				{
					if (languageSource.mSource.owner == null)
					{
						languageSource.mSource.owner = languageSource;
					}
					LocalizationManager.AddSource(languageSource.mSource);
				}
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0003AA94 File Offset: 0x00038E94
		private static void RegisterSourceInResources()
		{
			foreach (string name in LocalizationManager.GlobalSources)
			{
				LanguageSourceAsset asset = ResourceManager.pInstance.GetAsset<LanguageSourceAsset>(name);
				if (asset && !LocalizationManager.Sources.Contains(asset.mSource))
				{
					if (!asset.mSource.mIsGlobalSource)
					{
						asset.mSource.mIsGlobalSource = true;
					}
					asset.mSource.owner = asset;
					LocalizationManager.AddSource(asset.mSource);
				}
			}
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0003AB20 File Offset: 0x00038F20
		internal static void AddSource(LanguageSourceData Source)
		{
			if (LocalizationManager.Sources.Contains(Source))
			{
				return;
			}
			LocalizationManager.Sources.Add(Source);
			if (Source.HasGoogleSpreadsheet() && Source.GoogleUpdateFrequency != LanguageSourceData.eGoogleUpdateFrequency.Never)
			{
				Source.Import_Google_FromCache();
				bool justCheck = false;
				if (Source.GoogleUpdateDelay > 0f)
				{
					CoroutineManager.Start(LocalizationManager.Delayed_Import_Google(Source, Source.GoogleUpdateDelay, justCheck));
				}
				else
				{
					Source.Import_Google(false, justCheck);
				}
			}
			for (int i = 0; i < Source.mLanguages.Count<LanguageData>(); i++)
			{
				Source.mLanguages[i].SetLoaded(true);
			}
			if (Source.mDictionary.Count == 0)
			{
				Source.UpdateDictionary(true);
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0003ABE0 File Offset: 0x00038FE0
		private static IEnumerator Delayed_Import_Google(LanguageSourceData source, float delay, bool justCheck)
		{
			yield return new WaitForSeconds(delay);
			if (source != null)
			{
				source.Import_Google(false, justCheck);
			}
			yield break;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0003AC09 File Offset: 0x00039009
		internal static void RemoveSource(LanguageSourceData Source)
		{
			LocalizationManager.Sources.Remove(Source);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0003AC17 File Offset: 0x00039017
		public static bool IsGlobalSource(string SourceName)
		{
			return Array.IndexOf<string>(LocalizationManager.GlobalSources, SourceName) >= 0;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0003AC2C File Offset: 0x0003902C
		public static LanguageSourceData GetSourceContaining(string term, bool fallbackToFirst = true)
		{
			if (!string.IsNullOrEmpty(term))
			{
				int i = 0;
				int count = LocalizationManager.Sources.Count;
				while (i < count)
				{
					if (LocalizationManager.Sources[i].GetTermData(term, false) != null)
					{
						return LocalizationManager.Sources[i];
					}
					i++;
				}
			}
			return (!fallbackToFirst || LocalizationManager.Sources.Count <= 0) ? null : LocalizationManager.Sources[0];
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0003ACAC File Offset: 0x000390AC
		public static UnityEngine.Object FindAsset(string value)
		{
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				UnityEngine.Object @object = LocalizationManager.Sources[i].FindAsset(value);
				if (@object)
				{
					return @object;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0003ACF8 File Offset: 0x000390F8
		public static void ApplyDownloadedDataFromGoogle()
		{
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				LocalizationManager.Sources[i].ApplyDownloadedDataFromGoogle();
				i++;
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0003AD32 File Offset: 0x00039132
		public static string GetCurrentDeviceLanguage(bool force = false)
		{
			if (force || string.IsNullOrEmpty(LocalizationManager.mCurrentDeviceLanguage))
			{
				LocalizationManager.DetectDeviceLanguage();
			}
			return LocalizationManager.mCurrentDeviceLanguage;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0003AD54 File Offset: 0x00039154
		private static void DetectDeviceLanguage()
		{
			LocalizationManager.mCurrentDeviceLanguage = Application.systemLanguage.ToString();
			if (LocalizationManager.mCurrentDeviceLanguage == "ChineseSimplified")
			{
				LocalizationManager.mCurrentDeviceLanguage = "Chinese (Simplified)";
			}
			if (LocalizationManager.mCurrentDeviceLanguage == "ChineseTraditional")
			{
				LocalizationManager.mCurrentDeviceLanguage = "Chinese (Traditional)";
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0003ADB8 File Offset: 0x000391B8
		public static void RegisterTarget(ILocalizeTargetDescriptor desc)
		{
			if (LocalizationManager.mLocalizeTargets.FindIndex((ILocalizeTargetDescriptor x) => x.Name == desc.Name) != -1)
			{
				return;
			}
			for (int i = 0; i < LocalizationManager.mLocalizeTargets.Count; i++)
			{
				if (LocalizationManager.mLocalizeTargets[i].Priority > desc.Priority)
				{
					LocalizationManager.mLocalizeTargets.Insert(i, desc);
					return;
				}
			}
			LocalizationManager.mLocalizeTargets.Add(desc);
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x0600174A RID: 5962 RVA: 0x0003AE4C File Offset: 0x0003924C
		// (remove) Token: 0x0600174B RID: 5963 RVA: 0x0003AE80 File Offset: 0x00039280
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event LocalizationManager.OnLocalizeCallback OnLocalizeEvent;

		// Token: 0x0600174C RID: 5964 RVA: 0x0003AEB4 File Offset: 0x000392B4
		public static string GetTranslation(string Term, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null)
		{
			string result = null;
			LocalizationManager.TryGetTranslation(Term, out result, FixForRTL, maxLineLengthForRTL, ignoreRTLnumbers, applyParameters, localParametersRoot, overrideLanguage);
			return result;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0003AED6 File Offset: 0x000392D6
		public static string GetTermTranslation(string Term, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null)
		{
			return LocalizationManager.GetTranslation(Term, FixForRTL, maxLineLengthForRTL, ignoreRTLnumbers, applyParameters, localParametersRoot, overrideLanguage);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0003AEE8 File Offset: 0x000392E8
		public static bool TryGetTranslation(string Term, out string Translation, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null)
		{
			Translation = null;
			if (string.IsNullOrEmpty(Term))
			{
				return false;
			}
			LocalizationManager.InitializeIfNeeded();
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				if (LocalizationManager.Sources[i].TryGetTranslation(Term, out Translation, overrideLanguage, null, false, false))
				{
					if (applyParameters)
					{
						LocalizationManager.ApplyLocalizationParams(ref Translation, localParametersRoot, true);
					}
					if (LocalizationManager.IsRight2Left && FixForRTL)
					{
						Translation = LocalizationManager.ApplyRTLfix(Translation, maxLineLengthForRTL, ignoreRTLnumbers);
					}
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0003AF74 File Offset: 0x00039374
		public static T GetTranslatedObject<T>(string Term, Localize optionalLocComp = null) where T : UnityEngine.Object
		{
			if (optionalLocComp != null)
			{
				return optionalLocComp.FindTranslatedObject<T>(Term);
			}
			T t = LocalizationManager.FindAsset(Term) as T;
			if (t)
			{
				return t;
			}
			return ResourceManager.pInstance.GetAsset<T>(Term);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0003AFC8 File Offset: 0x000393C8
		public static string GetAppName(string languageCode)
		{
			if (!string.IsNullOrEmpty(languageCode))
			{
				for (int i = 0; i < LocalizationManager.Sources.Count; i++)
				{
					if (!string.IsNullOrEmpty(LocalizationManager.Sources[i].mTerm_AppName))
					{
						int languageIndexFromCode = LocalizationManager.Sources[i].GetLanguageIndexFromCode(languageCode, false, false);
						if (languageIndexFromCode >= 0)
						{
							TermData termData = LocalizationManager.Sources[i].GetTermData(LocalizationManager.Sources[i].mTerm_AppName, false);
							if (termData != null)
							{
								string translation = termData.GetTranslation(languageIndexFromCode, null, false);
								if (!string.IsNullOrEmpty(translation))
								{
									return translation;
								}
							}
						}
					}
				}
			}
			return Application.productName;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0003B083 File Offset: 0x00039483
		public static void LocalizeAll(bool Force = false)
		{
			LocalizationManager.LoadCurrentLanguage();
			if (!Application.isPlaying)
			{
				LocalizationManager.DoLocalizeAll(Force);
				return;
			}
			LocalizationManager.mLocalizeIsScheduledWithForcedValue = (LocalizationManager.mLocalizeIsScheduledWithForcedValue || Force);
			if (LocalizationManager.mLocalizeIsScheduled)
			{
				return;
			}
			CoroutineManager.Start(LocalizationManager.Coroutine_LocalizeAll());
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0003B0C0 File Offset: 0x000394C0
		private static IEnumerator Coroutine_LocalizeAll()
		{
			LocalizationManager.mLocalizeIsScheduled = true;
			yield return null;
			LocalizationManager.mLocalizeIsScheduled = false;
			bool force = LocalizationManager.mLocalizeIsScheduledWithForcedValue;
			LocalizationManager.mLocalizeIsScheduledWithForcedValue = false;
			LocalizationManager.DoLocalizeAll(force);
			yield break;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0003B0D4 File Offset: 0x000394D4
		private static void DoLocalizeAll(bool Force = false)
		{
			Localize[] array = (Localize[])Resources.FindObjectsOfTypeAll(typeof(Localize));
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				Localize localize = array[i];
				localize.OnLocalize(Force);
				i++;
			}
			if (LocalizationManager.OnLocalizeEvent != null)
			{
				LocalizationManager.OnLocalizeEvent();
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0003B12C File Offset: 0x0003952C
		public static List<string> GetCategories()
		{
			List<string> list = new List<string>();
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				LocalizationManager.Sources[i].GetCategories(false, list);
				i++;
			}
			return list;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0003B170 File Offset: 0x00039570
		public static List<string> GetTermsList(string Category = null)
		{
			if (LocalizationManager.Sources.Count == 0)
			{
				LocalizationManager.UpdateSources();
			}
			if (LocalizationManager.Sources.Count == 1)
			{
				return LocalizationManager.Sources[0].GetTermsList(Category);
			}
			HashSet<string> hashSet = new HashSet<string>();
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				hashSet.UnionWith(LocalizationManager.Sources[i].GetTermsList(Category));
				i++;
			}
			return new List<string>(hashSet);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0003B1F4 File Offset: 0x000395F4
		public static TermData GetTermData(string term)
		{
			LocalizationManager.InitializeIfNeeded();
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				TermData termData = LocalizationManager.Sources[i].GetTermData(term, false);
				if (termData != null)
				{
					return termData;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0003B240 File Offset: 0x00039640
		public static TermData GetTermData(string term, out LanguageSourceData source)
		{
			LocalizationManager.InitializeIfNeeded();
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				TermData termData = LocalizationManager.Sources[i].GetTermData(term, false);
				if (termData != null)
				{
					source = LocalizationManager.Sources[i];
					return termData;
				}
				i++;
			}
			source = null;
			return null;
		}

		// Token: 0x04000E78 RID: 3704
		private static string mCurrentLanguage;

		// Token: 0x04000E79 RID: 3705
		private static string mLanguageCode;

		// Token: 0x04000E7A RID: 3706
		private static CultureInfo mCurrentCulture;

		// Token: 0x04000E7B RID: 3707
		private static bool mChangeCultureInfo = false;

		// Token: 0x04000E7C RID: 3708
		public static bool IsRight2Left = false;

		// Token: 0x04000E7D RID: 3709
		public static bool HasJoinedWords = false;

		// Token: 0x04000E7E RID: 3710
		public static List<ILocalizationParamsManager> ParamManagers = new List<ILocalizationParamsManager>();

		// Token: 0x04000E7F RID: 3711
		private static string[] LanguagesRTL = new string[]
		{
			"ar-DZ",
			"ar",
			"ar-BH",
			"ar-EG",
			"ar-IQ",
			"ar-JO",
			"ar-KW",
			"ar-LB",
			"ar-LY",
			"ar-MA",
			"ar-OM",
			"ar-QA",
			"ar-SA",
			"ar-SY",
			"ar-TN",
			"ar-AE",
			"ar-YE",
			"he",
			"ur",
			"ji"
		};

		// Token: 0x04000E80 RID: 3712
		public static List<LanguageSourceData> Sources = new List<LanguageSourceData>();

		// Token: 0x04000E81 RID: 3713
		public static string[] GlobalSources = new string[]
		{
			"I2Languages"
		};

		// Token: 0x04000E82 RID: 3714
		private static string mCurrentDeviceLanguage;

		// Token: 0x04000E83 RID: 3715
		public static List<ILocalizeTargetDescriptor> mLocalizeTargets = new List<ILocalizeTargetDescriptor>();

		// Token: 0x04000E85 RID: 3717
		private static bool mLocalizeIsScheduled = false;

		// Token: 0x04000E86 RID: 3718
		private static bool mLocalizeIsScheduledWithForcedValue = false;

		// Token: 0x04000E87 RID: 3719
		public static bool HighlightLocalizedTargets = false;

		// Token: 0x020003F1 RID: 1009
		// (Invoke) Token: 0x0600175B RID: 5979
		public delegate object _GetParam(string param);

		// Token: 0x020003F2 RID: 1010
		// (Invoke) Token: 0x0600175F RID: 5983
		public delegate void OnLocalizeCallback();
	}
}
