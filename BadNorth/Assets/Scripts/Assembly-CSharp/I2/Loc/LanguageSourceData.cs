using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	// Token: 0x020003E4 RID: 996
	[ExecuteInEditMode]
	[Serializable]
	public class LanguageSourceData
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x00035740 File Offset: 0x00033B40
		public void UpdateAssetDictionary()
		{
			this.Assets.RemoveAll((UnityEngine.Object x) => x == null);
			IEnumerable<IGrouping<string, UnityEngine.Object>> source = from o in this.Assets.Distinct<UnityEngine.Object>()
			group o by o.name;
			Func<IGrouping<string, UnityEngine.Object>, string> keySelector = (IGrouping<string, UnityEngine.Object> g) => g.Key;
			if (LanguageSourceData.<>f__mg$cache0 == null)
			{
				LanguageSourceData.<>f__mg$cache0 = new Func<IGrouping<string, UnityEngine.Object>, UnityEngine.Object>(Enumerable.First<UnityEngine.Object>);
			}
			this.mAssetDictionary = source.ToDictionary(keySelector, LanguageSourceData.<>f__mg$cache0);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000357E8 File Offset: 0x00033BE8
		public UnityEngine.Object FindAsset(string Name)
		{
			if (this.Assets != null)
			{
				if (this.mAssetDictionary == null || this.mAssetDictionary.Count != this.Assets.Count)
				{
					this.UpdateAssetDictionary();
				}
				UnityEngine.Object result;
				if (this.mAssetDictionary.TryGetValue(Name, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00035842 File Offset: 0x00033C42
		public bool HasAsset(UnityEngine.Object Obj)
		{
			return this.Assets.Contains(Obj);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00035850 File Offset: 0x00033C50
		public void AddAsset(UnityEngine.Object Obj)
		{
			if (this.Assets.Contains(Obj))
			{
				return;
			}
			this.Assets.Add(Obj);
			this.UpdateAssetDictionary();
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00035878 File Offset: 0x00033C78
		public string Export_I2CSV(string Category, char Separator = ',', bool specializationsAsRows = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Key[*]Type[*]Desc");
			foreach (LanguageData languageData in this.mLanguages)
			{
				stringBuilder.Append("[*]");
				if (!languageData.IsEnabled())
				{
					stringBuilder.Append('$');
				}
				stringBuilder.Append(GoogleLanguages.GetCodedLanguage(languageData.Name, languageData.Code));
			}
			stringBuilder.Append("[ln]");
			this.mTerms.Sort((TermData a, TermData b) => string.CompareOrdinal(a.Term, b.Term));
			int count = this.mLanguages.Count;
			bool flag = true;
			foreach (TermData termData in this.mTerms)
			{
				string term;
				if (string.IsNullOrEmpty(Category) || (Category == LanguageSourceData.EmptyCategory && termData.Term.IndexOfAny(LanguageSourceData.CategorySeparators) < 0))
				{
					term = termData.Term;
				}
				else
				{
					if (!termData.Term.StartsWith(Category + "/") || !(Category != termData.Term))
					{
						continue;
					}
					term = termData.Term.Substring(Category.Length + 1);
				}
				if (!flag)
				{
					stringBuilder.Append("[ln]");
				}
				flag = false;
				if (!specializationsAsRows)
				{
					LanguageSourceData.AppendI2Term(stringBuilder, count, term, termData, Separator, null);
				}
				else
				{
					List<string> allSpecializations = termData.GetAllSpecializations();
					for (int i = 0; i < allSpecializations.Count; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append("[ln]");
						}
						string forceSpecialization = allSpecializations[i];
						LanguageSourceData.AppendI2Term(stringBuilder, count, term, termData, Separator, forceSpecialization);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00035ACC File Offset: 0x00033ECC
		private static void AppendI2Term(StringBuilder Builder, int nLanguages, string Term, TermData termData, char Separator, string forceSpecialization)
		{
			LanguageSourceData.AppendI2Text(Builder, Term);
			if (!string.IsNullOrEmpty(forceSpecialization) && forceSpecialization != "Any")
			{
				Builder.Append("[");
				Builder.Append(forceSpecialization);
				Builder.Append("]");
			}
			Builder.Append("[*]");
			Builder.Append(termData.TermType.ToString());
			Builder.Append("[*]");
			Builder.Append(termData.Description);
			for (int i = 0; i < Mathf.Min(nLanguages, termData.Languages.Length); i++)
			{
				Builder.Append("[*]");
				string text = termData.Languages[i];
				if (!string.IsNullOrEmpty(forceSpecialization))
				{
					text = termData.GetTranslation(i, forceSpecialization, false);
				}
				LanguageSourceData.AppendI2Text(Builder, text);
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00035BAD File Offset: 0x00033FAD
		private static void AppendI2Text(StringBuilder Builder, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (text.StartsWith("'") || text.StartsWith("="))
			{
				Builder.Append('\'');
			}
			Builder.Append(text);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00035BEC File Offset: 0x00033FEC
		private string Export_Language_to_Cache(int langIndex, bool fillTermWithFallback)
		{
			if (!this.mLanguages[langIndex].IsLoaded())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.mTerms.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append("[i2t]");
				}
				TermData termData = this.mTerms[i];
				stringBuilder.Append(termData.Term);
				stringBuilder.Append("=");
				string text = termData.Languages[langIndex];
				if (this.OnMissingTranslation == LanguageSourceData.MissingTranslationAction.Fallback && string.IsNullOrEmpty(text) && this.TryGetFallbackTranslation(termData, out text, langIndex, null, true))
				{
					stringBuilder.Append("[i2fb]");
					if (fillTermWithFallback)
					{
						termData.Languages[langIndex] = text;
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00035CD4 File Offset: 0x000340D4
		public string Export_CSV(string Category, char Separator = ',', bool specializationsAsRows = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = this.mLanguages.Count;
			stringBuilder.AppendFormat("Key{0}Type{0}Desc", Separator);
			foreach (LanguageData languageData in this.mLanguages)
			{
				stringBuilder.Append(Separator);
				if (!languageData.IsEnabled())
				{
					stringBuilder.Append('$');
				}
				LanguageSourceData.AppendString(stringBuilder, GoogleLanguages.GetCodedLanguage(languageData.Name, languageData.Code), Separator);
			}
			stringBuilder.Append("\n");
			this.mTerms.Sort((TermData a, TermData b) => string.CompareOrdinal(a.Term, b.Term));
			foreach (TermData termData in this.mTerms)
			{
				string term;
				if (string.IsNullOrEmpty(Category) || (Category == LanguageSourceData.EmptyCategory && termData.Term.IndexOfAny(LanguageSourceData.CategorySeparators) < 0))
				{
					term = termData.Term;
				}
				else
				{
					if (!termData.Term.StartsWith(Category + "/") || !(Category != termData.Term))
					{
						continue;
					}
					term = termData.Term.Substring(Category.Length + 1);
				}
				if (specializationsAsRows)
				{
					foreach (string specialization in termData.GetAllSpecializations())
					{
						LanguageSourceData.AppendTerm(stringBuilder, count, term, termData, specialization, Separator);
					}
				}
				else
				{
					LanguageSourceData.AppendTerm(stringBuilder, count, term, termData, null, Separator);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00035F20 File Offset: 0x00034320
		private static void AppendTerm(StringBuilder Builder, int nLanguages, string Term, TermData termData, string specialization, char Separator)
		{
			LanguageSourceData.AppendString(Builder, Term, Separator);
			if (!string.IsNullOrEmpty(specialization) && specialization != "Any")
			{
				Builder.AppendFormat("[{0}]", specialization);
			}
			Builder.Append(Separator);
			Builder.Append(termData.TermType.ToString());
			Builder.Append(Separator);
			LanguageSourceData.AppendString(Builder, termData.Description, Separator);
			for (int i = 0; i < Mathf.Min(nLanguages, termData.Languages.Length); i++)
			{
				Builder.Append(Separator);
				string text = termData.Languages[i];
				if (!string.IsNullOrEmpty(specialization))
				{
					text = termData.GetTranslation(i, specialization, false);
				}
				LanguageSourceData.AppendTranslation(Builder, text, Separator, null);
			}
			Builder.Append("\n");
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00035FF8 File Offset: 0x000343F8
		private static void AppendString(StringBuilder Builder, string Text, char Separator)
		{
			if (string.IsNullOrEmpty(Text))
			{
				return;
			}
			Text = Text.Replace("\\n", "\n");
			if (Text.IndexOfAny((Separator + "\n\"").ToCharArray()) >= 0)
			{
				Text = Text.Replace("\"", "\"\"");
				Builder.AppendFormat("\"{0}\"", Text);
			}
			else
			{
				Builder.Append(Text);
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00036070 File Offset: 0x00034470
		private static void AppendTranslation(StringBuilder Builder, string Text, char Separator, string tags)
		{
			if (string.IsNullOrEmpty(Text))
			{
				return;
			}
			Text = Text.Replace("\\n", "\n");
			if (Text.IndexOfAny((Separator + "\n\"").ToCharArray()) >= 0)
			{
				Text = Text.Replace("\"", "\"\"");
				Builder.AppendFormat("\"{0}{1}\"", tags, Text);
			}
			else
			{
				Builder.Append(tags);
				Builder.Append(Text);
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000360F4 File Offset: 0x000344F4
		public UnityWebRequest Export_Google_CreateWWWcall(eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace)
		{
			string value = this.Export_Google_CreateData();
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("key", this.Google_SpreadsheetKey);
			wwwform.AddField("action", "SetLanguageSource");
			wwwform.AddField("data", value);
			wwwform.AddField("updateMode", UpdateMode.ToString());
			UnityWebRequest unityWebRequest = UnityWebRequest.Post(LocalizationManager.GetWebServiceURL(this), wwwform);
			I2Utils.SendWebRequest(unityWebRequest);
			return unityWebRequest;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00036168 File Offset: 0x00034568
		private string Export_Google_CreateData()
		{
			List<string> categories = this.GetCategories(true, null);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string text in categories)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append("<I2Loc>");
				}
				bool flag2 = true;
				string category = text;
				bool specializationsAsRows = flag2;
				string value = this.Export_I2CSV(category, ',', specializationsAsRows);
				stringBuilder.Append(text);
				stringBuilder.Append("<I2Loc>");
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00036220 File Offset: 0x00034620
		public string Import_CSV(string Category, string CSVstring, eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace, char Separator = ',')
		{
			List<string[]> csv = LocalizationReader.ReadCSV(CSVstring, Separator);
			return this.Import_CSV(Category, csv, UpdateMode);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00036240 File Offset: 0x00034640
		public string Import_I2CSV(string Category, string I2CSVstring, eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace)
		{
			List<string[]> csv = LocalizationReader.ReadI2CSV(I2CSVstring);
			return this.Import_CSV(Category, csv, UpdateMode);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00036260 File Offset: 0x00034660
		public string Import_CSV(string Category, List<string[]> CSV, eSpreadsheetUpdateMode UpdateMode = eSpreadsheetUpdateMode.Replace)
		{
			string[] array = CSV[0];
			int num = 1;
			int num2 = -1;
			int num3 = -1;
			string[] texts = new string[]
			{
				"Key"
			};
			string[] texts2 = new string[]
			{
				"Type"
			};
			string[] texts3 = new string[]
			{
				"Desc",
				"Description"
			};
			if (array.Length > 1 && this.ArrayContains(array[0], texts))
			{
				if (UpdateMode == eSpreadsheetUpdateMode.Replace)
				{
					this.ClearAllData();
				}
				if (array.Length > 2)
				{
					if (this.ArrayContains(array[1], texts2))
					{
						num2 = 1;
						num = 2;
					}
					if (this.ArrayContains(array[1], texts3))
					{
						num3 = 1;
						num = 2;
					}
				}
				if (array.Length > 3)
				{
					if (this.ArrayContains(array[2], texts2))
					{
						num2 = 2;
						num = 3;
					}
					if (this.ArrayContains(array[2], texts3))
					{
						num3 = 2;
						num = 3;
					}
				}
				int num4 = Mathf.Max(array.Length - num, 0);
				int[] array2 = new int[num4];
				for (int i = 0; i < num4; i++)
				{
					if (string.IsNullOrEmpty(array[i + num]))
					{
						array2[i] = -1;
					}
					else
					{
						string text = array[i + num];
						bool flag = true;
						if (text.StartsWith("$"))
						{
							flag = false;
							text = text.Substring(1);
						}
						string text2;
						string text3;
						GoogleLanguages.UnPackCodeFromLanguageName(text, out text2, out text3);
						int num5;
						if (!string.IsNullOrEmpty(text3))
						{
							num5 = this.GetLanguageIndexFromCode(text3, true, false);
						}
						else
						{
							num5 = this.GetLanguageIndex(text2, true, false);
						}
						if (num5 < 0)
						{
							LanguageData languageData = new LanguageData();
							languageData.Name = text2;
							languageData.Code = text3;
							languageData.Flags = (byte)(0 | ((!flag) ? 1 : 0));
							this.mLanguages.Add(languageData);
							num5 = this.mLanguages.Count - 1;
						}
						array2[i] = num5;
					}
				}
				num4 = this.mLanguages.Count;
				int j = 0;
				int count = this.mTerms.Count;
				while (j < count)
				{
					TermData termData = this.mTerms[j];
					if (termData.Languages.Length < num4)
					{
						Array.Resize<string>(ref termData.Languages, num4);
						Array.Resize<byte>(ref termData.Flags, num4);
					}
					j++;
				}
				int k = 1;
				int count2 = CSV.Count;
				while (k < count2)
				{
					array = CSV[k];
					string text4 = (!string.IsNullOrEmpty(Category)) ? (Category + "/" + array[0]) : array[0];
					string text5 = null;
					if (text4.EndsWith("]"))
					{
						int num6 = text4.LastIndexOf('[');
						if (num6 > 0)
						{
							text5 = text4.Substring(num6 + 1, text4.Length - num6 - 2);
							if (text5 == "touch")
							{
								text5 = "Touch";
							}
							text4 = text4.Remove(num6);
						}
					}
					LanguageSourceData.ValidateFullTerm(ref text4);
					if (!string.IsNullOrEmpty(text4))
					{
						TermData termData2 = this.GetTermData(text4, false);
						if (termData2 == null)
						{
							termData2 = new TermData();
							termData2.Term = text4;
							termData2.Languages = new string[this.mLanguages.Count];
							termData2.Flags = new byte[this.mLanguages.Count];
							for (int l = 0; l < this.mLanguages.Count; l++)
							{
								termData2.Languages[l] = string.Empty;
							}
							this.mTerms.Add(termData2);
							this.mDictionary.Add(text4, termData2);
						}
						else if (UpdateMode == eSpreadsheetUpdateMode.AddNewTerms)
						{
							goto IL_462;
						}
						if (num2 > 0)
						{
							termData2.TermType = LanguageSourceData.GetTermType(array[num2]);
						}
						if (num3 > 0)
						{
							termData2.Description = array[num3];
						}
						int num7 = 0;
						while (num7 < array2.Length && num7 < array.Length - num)
						{
							if (!string.IsNullOrEmpty(array[num7 + num]))
							{
								int num8 = array2[num7];
								if (num8 >= 0)
								{
									string text6 = array[num7 + num];
									if (text6 == "-")
									{
										text6 = string.Empty;
									}
									else if (text6 == string.Empty)
									{
										text6 = null;
									}
									termData2.SetTranslation(num8, text6, text5);
								}
							}
							num7++;
						}
					}
					IL_462:
					k++;
				}
				if (Application.isPlaying)
				{
					this.SaveLanguages(this.HasUnloadedLanguages(), PersistentStorage.eFileType.Temporal);
				}
				return string.Empty;
			}
			return "Bad Spreadsheet Format.\nFirst columns should be 'Key', 'Type' and 'Desc'";
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000366FC File Offset: 0x00034AFC
		private bool ArrayContains(string MainText, params string[] texts)
		{
			int i = 0;
			int num = texts.Length;
			while (i < num)
			{
				if (MainText.IndexOf(texts[i], StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00036734 File Offset: 0x00034B34
		public static eTermType GetTermType(string type)
		{
			int i = 0;
			int num = 9;
			while (i <= num)
			{
				eTermType eTermType = (eTermType)i;
				if (string.Equals(eTermType.ToString(), type, StringComparison.OrdinalIgnoreCase))
				{
					return (eTermType)i;
				}
				i++;
			}
			return eTermType.Text;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00036774 File Offset: 0x00034B74
		private void Import_Language_from_Cache(int langIndex, string langData, bool useFallback, bool onlyCurrentSpecialization)
		{
			int num;
			for (int i = 0; i < langData.Length; i = num + 5)
			{
				num = langData.IndexOf("[i2t]", i);
				if (num < 0)
				{
					num = langData.Length;
				}
				int num2 = langData.IndexOf("=", i);
				if (num2 >= num)
				{
					return;
				}
				string term = langData.Substring(i, num2 - i);
				i = num2 + 1;
				TermData termData = this.GetTermData(term, false);
				if (termData != null)
				{
					string text = null;
					if (i != num)
					{
						text = langData.Substring(i, num - i);
						if (text.StartsWith("[i2fb]"))
						{
							text = ((!useFallback) ? null : text.Substring(6));
						}
						if (onlyCurrentSpecialization && text != null)
						{
							text = SpecializationManager.GetSpecializedText(text, null);
						}
					}
					termData.Languages[langIndex] = text;
				}
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00036848 File Offset: 0x00034C48
		public static void FreeUnusedLanguages()
		{
			LanguageSourceData languageSourceData = LocalizationManager.Sources[0];
			int languageIndex = languageSourceData.GetLanguageIndex(LocalizationManager.CurrentLanguage, true, true);
			for (int i = 0; i < languageSourceData.mTerms.Count; i++)
			{
				TermData termData = languageSourceData.mTerms[i];
				for (int j = 0; j < termData.Languages.Length; j++)
				{
					if (j != languageIndex)
					{
						termData.Languages[j] = null;
					}
				}
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000368C8 File Offset: 0x00034CC8
		public void Import_Google_FromCache()
		{
			if (this.GoogleUpdateFrequency == LanguageSourceData.eGoogleUpdateFrequency.Never)
			{
				return;
			}
			if (!I2Utils.IsPlaying())
			{
				return;
			}
			string sourcePlayerPrefName = this.GetSourcePlayerPrefName();
			string text = PersistentStorage.LoadFile(PersistentStorage.eFileType.Persistent, "I2Source_" + sourcePlayerPrefName + ".loc", false);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (text.StartsWith("[i2e]", StringComparison.Ordinal))
			{
				text = StringObfucator.Decode(text.Substring(5, text.Length - 5));
			}
			bool flag = false;
			string text2 = this.Google_LastUpdatedVersion;
			if (PersistentStorage.HasSetting("I2SourceVersion_" + sourcePlayerPrefName))
			{
				text2 = PersistentStorage.GetSetting_String("I2SourceVersion_" + sourcePlayerPrefName, this.Google_LastUpdatedVersion);
				flag = this.IsNewerVersion(this.Google_LastUpdatedVersion, text2);
			}
			if (!flag)
			{
				PersistentStorage.DeleteFile(PersistentStorage.eFileType.Persistent, "I2Source_" + sourcePlayerPrefName + ".loc", false);
				PersistentStorage.DeleteSetting("I2SourceVersion_" + sourcePlayerPrefName);
				return;
			}
			if (text2.Length > 19)
			{
				text2 = string.Empty;
			}
			this.Google_LastUpdatedVersion = text2;
			this.Import_Google_Result(text, eSpreadsheetUpdateMode.Replace, false);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000369D4 File Offset: 0x00034DD4
		private bool IsNewerVersion(string currentVersion, string newVersion)
		{
			long num;
			long num2;
			return !string.IsNullOrEmpty(newVersion) && (string.IsNullOrEmpty(currentVersion) || (!long.TryParse(newVersion, out num) || !long.TryParse(currentVersion, out num2)) || num > num2);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00036A1C File Offset: 0x00034E1C
		public void Import_Google(bool ForceUpdate, bool justCheck)
		{
			if (!ForceUpdate && this.GoogleUpdateFrequency == LanguageSourceData.eGoogleUpdateFrequency.Never)
			{
				return;
			}
			if (!I2Utils.IsPlaying())
			{
				return;
			}
			LanguageSourceData.eGoogleUpdateFrequency googleUpdateFrequency = this.GoogleUpdateFrequency;
			string sourcePlayerPrefName = this.GetSourcePlayerPrefName();
			if (!ForceUpdate && googleUpdateFrequency != LanguageSourceData.eGoogleUpdateFrequency.Always)
			{
				string setting_String = PersistentStorage.GetSetting_String("LastGoogleUpdate_" + sourcePlayerPrefName, string.Empty);
				try
				{
					DateTime d;
					if (DateTime.TryParse(setting_String, out d))
					{
						double totalDays = (DateTime.Now - d).TotalDays;
						switch (googleUpdateFrequency)
						{
						case LanguageSourceData.eGoogleUpdateFrequency.Daily:
							if (totalDays < 1.0)
							{
								return;
							}
							break;
						case LanguageSourceData.eGoogleUpdateFrequency.Weekly:
							if (totalDays < 8.0)
							{
								return;
							}
							break;
						case LanguageSourceData.eGoogleUpdateFrequency.Monthly:
							if (totalDays < 31.0)
							{
								return;
							}
							break;
						case LanguageSourceData.eGoogleUpdateFrequency.OnlyOnce:
							return;
						}
					}
				}
				catch (Exception)
				{
				}
			}
			PersistentStorage.SetSetting_String("LastGoogleUpdate_" + sourcePlayerPrefName, DateTime.Now.ToString());
			CoroutineManager.Start(this.Import_Google_Coroutine(justCheck));
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00036B58 File Offset: 0x00034F58
		private string GetSourcePlayerPrefName()
		{
			if (this.owner == null)
			{
				return null;
			}
			string text = (this.owner as UnityEngine.Object).name;
			if (!string.IsNullOrEmpty(this.Google_SpreadsheetKey))
			{
				text += this.Google_SpreadsheetKey;
			}
			if (Array.IndexOf<string>(LocalizationManager.GlobalSources, (this.owner as UnityEngine.Object).name) >= 0)
			{
				return text;
			}
			return SceneManager.GetActiveScene().name + "_" + text;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00036BDC File Offset: 0x00034FDC
		private IEnumerator Import_Google_Coroutine(bool JustCheck)
		{
			UnityWebRequest www = this.Import_Google_CreateWWWcall(false, JustCheck);
			if (www == null)
			{
				yield break;
			}
			while (!www.isDone)
			{
				yield return null;
			}
			bool notError = string.IsNullOrEmpty(www.error);
			string wwwText = null;
			if (notError)
			{
				byte[] data = www.downloadHandler.data;
				wwwText = Encoding.UTF8.GetString(data, 0, data.Length);
				bool flag = string.IsNullOrEmpty(wwwText) || wwwText == "\"\"";
				if (JustCheck)
				{
					if (!flag)
					{
						UnityEngine.Debug.LogWarning("Spreadsheet is not up-to-date and Google Live Synchronization is enabled\nWhen playing in the device the Spreadsheet will be downloaded and translations may not behave as what you see in the editor.\nTo fix this, Import or Export replace to Google");
						this.GoogleLiveSyncIsUptoDate = false;
					}
					yield break;
				}
				if (!flag)
				{
					this.mDelayedGoogleData = wwwText;
					LanguageSourceData.eGoogleUpdateSynchronization googleUpdateSynchronization = this.GoogleUpdateSynchronization;
					if (googleUpdateSynchronization != LanguageSourceData.eGoogleUpdateSynchronization.AsSoonAsDownloaded)
					{
						if (googleUpdateSynchronization != LanguageSourceData.eGoogleUpdateSynchronization.Manual)
						{
							if (googleUpdateSynchronization == LanguageSourceData.eGoogleUpdateSynchronization.OnSceneLoaded)
							{
								SceneManager.sceneLoaded += this.ApplyDownloadedDataOnSceneLoaded;
							}
						}
					}
					else
					{
						this.ApplyDownloadedDataFromGoogle();
					}
					yield break;
				}
			}
			if (this.Event_OnSourceUpdateFromGoogle != null)
			{
				this.Event_OnSourceUpdateFromGoogle(this, false, www.error);
			}
			UnityEngine.Debug.Log("Language Source was up-to-date with Google Spreadsheet");
			yield break;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00036BFE File Offset: 0x00034FFE
		private void ApplyDownloadedDataOnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			SceneManager.sceneLoaded -= this.ApplyDownloadedDataOnSceneLoaded;
			this.ApplyDownloadedDataFromGoogle();
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00036C18 File Offset: 0x00035018
		public void ApplyDownloadedDataFromGoogle()
		{
			if (string.IsNullOrEmpty(this.mDelayedGoogleData))
			{
				return;
			}
			string value = this.Import_Google_Result(this.mDelayedGoogleData, eSpreadsheetUpdateMode.Replace, true);
			if (string.IsNullOrEmpty(value))
			{
				if (this.Event_OnSourceUpdateFromGoogle != null)
				{
					this.Event_OnSourceUpdateFromGoogle(this, true, string.Empty);
				}
				LocalizationManager.LocalizeAll(true);
				UnityEngine.Debug.Log("Done Google Sync");
			}
			else
			{
				if (this.Event_OnSourceUpdateFromGoogle != null)
				{
					this.Event_OnSourceUpdateFromGoogle(this, false, string.Empty);
				}
				UnityEngine.Debug.Log("Done Google Sync: source was up-to-date");
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00036CAC File Offset: 0x000350AC
		public UnityWebRequest Import_Google_CreateWWWcall(bool ForceUpdate, bool justCheck)
		{
			if (!this.HasGoogleSpreadsheet())
			{
				return null;
			}
			string text = PersistentStorage.GetSetting_String("I2SourceVersion_" + this.GetSourcePlayerPrefName(), this.Google_LastUpdatedVersion);
			if (text.Length > 19)
			{
				text = string.Empty;
			}
			if (this.IsNewerVersion(text, this.Google_LastUpdatedVersion))
			{
				this.Google_LastUpdatedVersion = text;
			}
			string uri = string.Format("{0}?key={1}&action=GetLanguageSource&version={2}", LocalizationManager.GetWebServiceURL(this), this.Google_SpreadsheetKey, (!ForceUpdate) ? this.Google_LastUpdatedVersion : "0");
			UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri);
			I2Utils.SendWebRequest(unityWebRequest);
			return unityWebRequest;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00036D49 File Offset: 0x00035149
		public bool HasGoogleSpreadsheet()
		{
			return !string.IsNullOrEmpty(this.Google_WebServiceURL) && !string.IsNullOrEmpty(this.Google_SpreadsheetKey) && !string.IsNullOrEmpty(LocalizationManager.GetWebServiceURL(this));
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00036D7C File Offset: 0x0003517C
		public string Import_Google_Result(string JsonString, eSpreadsheetUpdateMode UpdateMode, bool saveInPlayerPrefs = false)
		{
			string result;
			try
			{
				string empty = string.Empty;
				if (string.IsNullOrEmpty(JsonString) || JsonString == "\"\"")
				{
					result = empty;
				}
				else
				{
					int num = JsonString.IndexOf("version=", StringComparison.Ordinal);
					int num2 = JsonString.IndexOf("script_version=", StringComparison.Ordinal);
					if (num < 0 || num2 < 0)
					{
						result = "Invalid Response from Google, Most likely the WebService needs to be updated";
					}
					else
					{
						num += "version=".Length;
						num2 += "script_version=".Length;
						string text = JsonString.Substring(num, JsonString.IndexOf(",", num, StringComparison.Ordinal) - num);
						int num3 = int.Parse(JsonString.Substring(num2, JsonString.IndexOf(",", num2, StringComparison.Ordinal) - num2));
						if (text.Length > 19)
						{
							text = string.Empty;
						}
						if (num3 != LocalizationManager.GetRequiredWebServiceVersion())
						{
							result = "The current Google WebService is not supported.\nPlease, delete the WebService from the Google Drive and Install the latest version.";
						}
						else if (saveInPlayerPrefs && !this.IsNewerVersion(this.Google_LastUpdatedVersion, text))
						{
							result = "LanguageSource is up-to-date";
						}
						else
						{
							if (saveInPlayerPrefs)
							{
								string sourcePlayerPrefName = this.GetSourcePlayerPrefName();
								PersistentStorage.SaveFile(PersistentStorage.eFileType.Persistent, "I2Source_" + sourcePlayerPrefName + ".loc", "[i2e]" + StringObfucator.Encode(JsonString), true);
								PersistentStorage.SetSetting_String("I2SourceVersion_" + sourcePlayerPrefName, text);
								PersistentStorage.ForceSaveSettings();
							}
							this.Google_LastUpdatedVersion = text;
							if (UpdateMode == eSpreadsheetUpdateMode.Replace)
							{
								this.ClearAllData();
							}
							int i = JsonString.IndexOf("[i2category]", StringComparison.Ordinal);
							while (i > 0)
							{
								i += "[i2category]".Length;
								int num4 = JsonString.IndexOf("[/i2category]", i, StringComparison.Ordinal);
								string category = JsonString.Substring(i, num4 - i);
								num4 += "[/i2category]".Length;
								int num5 = JsonString.IndexOf("[/i2csv]", num4, StringComparison.Ordinal);
								string i2CSVstring = JsonString.Substring(num4, num5 - num4);
								i = JsonString.IndexOf("[i2category]", num5, StringComparison.Ordinal);
								this.Import_I2CSV(category, i2CSVstring, UpdateMode);
								if (UpdateMode == eSpreadsheetUpdateMode.Replace)
								{
									UpdateMode = eSpreadsheetUpdateMode.Merge;
								}
							}
							this.GoogleLiveSyncIsUptoDate = true;
							if (I2Utils.IsPlaying())
							{
								this.SaveLanguages(true, PersistentStorage.eFileType.Temporal);
							}
							if (!string.IsNullOrEmpty(empty))
							{
								this.Editor_SetDirty();
							}
							result = empty;
						}
					}
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning(ex);
				result = ex.ToString();
			}
			return result;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00036FE8 File Offset: 0x000353E8
		public int GetLanguageIndex(string language, bool AllowDiscartingRegion = true, bool SkipDisabled = true)
		{
			int i = 0;
			int count = this.mLanguages.Count;
			while (i < count)
			{
				if ((!SkipDisabled || this.mLanguages[i].IsEnabled()) && string.Compare(this.mLanguages[i].Name, language, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
				i++;
			}
			if (AllowDiscartingRegion)
			{
				int num = -1;
				int num2 = 0;
				int j = 0;
				int count2 = this.mLanguages.Count;
				while (j < count2)
				{
					if (!SkipDisabled || this.mLanguages[j].IsEnabled())
					{
						int commonWordInLanguageNames = LanguageSourceData.GetCommonWordInLanguageNames(this.mLanguages[j].Name, language);
						if (commonWordInLanguageNames > num2)
						{
							num2 = commonWordInLanguageNames;
							num = j;
						}
					}
					j++;
				}
				if (num >= 0)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000370CC File Offset: 0x000354CC
		public LanguageData GetLanguageData(string language, bool AllowDiscartingRegion = true)
		{
			int languageIndex = this.GetLanguageIndex(language, AllowDiscartingRegion, false);
			return (languageIndex >= 0) ? this.mLanguages[languageIndex] : null;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000370FC File Offset: 0x000354FC
		public bool IsCurrentLanguage(int languageIndex)
		{
			return LocalizationManager.CurrentLanguage == this.mLanguages[languageIndex].Name;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0003711C File Offset: 0x0003551C
		public int GetLanguageIndexFromCode(string Code, bool exactMatch = true, bool ignoreDisabled = false)
		{
			int i = 0;
			int count = this.mLanguages.Count;
			while (i < count)
			{
				if (!ignoreDisabled || this.mLanguages[i].IsEnabled())
				{
					if (string.Compare(this.mLanguages[i].Code, Code, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return i;
					}
				}
				i++;
			}
			if (!exactMatch)
			{
				int j = 0;
				int count2 = this.mLanguages.Count;
				while (j < count2)
				{
					if (!ignoreDisabled || this.mLanguages[j].IsEnabled())
					{
						if (string.Compare(this.mLanguages[j].Code, 0, Code, 0, 2, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return j;
						}
					}
					j++;
				}
			}
			return -1;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000371F0 File Offset: 0x000355F0
		public static int GetCommonWordInLanguageNames(string Language1, string Language2)
		{
			if (string.IsNullOrEmpty(Language1) || string.IsNullOrEmpty(Language2))
			{
				return 0;
			}
			char[] separator = "( )-/\\".ToCharArray();
			string[] array = Language1.ToLower().Split(separator);
			string[] array2 = Language2.ToLower().Split(separator);
			int num = 0;
			foreach (string value in array)
			{
				if (!string.IsNullOrEmpty(value) && array2.Contains(value))
				{
					num++;
				}
			}
			foreach (string value2 in array2)
			{
				if (!string.IsNullOrEmpty(value2) && array.Contains(value2))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000372BD File Offset: 0x000356BD
		public static bool AreTheSameLanguage(string Language1, string Language2)
		{
			Language1 = LanguageSourceData.GetLanguageWithoutRegion(Language1);
			Language2 = LanguageSourceData.GetLanguageWithoutRegion(Language2);
			return string.Compare(Language1, Language2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000372DC File Offset: 0x000356DC
		public static string GetLanguageWithoutRegion(string Language)
		{
			int num = Language.IndexOfAny("(/\\[,{".ToCharArray());
			if (num < 0)
			{
				return Language;
			}
			return Language.Substring(0, num).Trim();
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00037310 File Offset: 0x00035710
		public void AddLanguage(string LanguageName)
		{
			this.AddLanguage(LanguageName, GoogleLanguages.GetLanguageCode(LanguageName, false));
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00037320 File Offset: 0x00035720
		public void AddLanguage(string LanguageName, string LanguageCode)
		{
			if (this.GetLanguageIndex(LanguageName, false, true) >= 0)
			{
				return;
			}
			LanguageData languageData = new LanguageData();
			languageData.Name = LanguageName;
			languageData.Code = LanguageCode;
			this.mLanguages.Add(languageData);
			int count = this.mLanguages.Count;
			int i = 0;
			int count2 = this.mTerms.Count;
			while (i < count2)
			{
				Array.Resize<string>(ref this.mTerms[i].Languages, count);
				Array.Resize<byte>(ref this.mTerms[i].Flags, count);
				i++;
			}
			this.Editor_SetDirty();
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000373BC File Offset: 0x000357BC
		public void RemoveLanguage(string LanguageName)
		{
			int languageIndex = this.GetLanguageIndex(LanguageName, false, false);
			if (languageIndex < 0)
			{
				return;
			}
			int count = this.mLanguages.Count;
			int i = 0;
			int count2 = this.mTerms.Count;
			while (i < count2)
			{
				for (int j = languageIndex + 1; j < count; j++)
				{
					this.mTerms[i].Languages[j - 1] = this.mTerms[i].Languages[j];
					this.mTerms[i].Flags[j - 1] = this.mTerms[i].Flags[j];
				}
				Array.Resize<string>(ref this.mTerms[i].Languages, count - 1);
				Array.Resize<byte>(ref this.mTerms[i].Flags, count - 1);
				i++;
			}
			this.mLanguages.RemoveAt(languageIndex);
			this.Editor_SetDirty();
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000374B8 File Offset: 0x000358B8
		public List<string> GetLanguages(bool skipDisabled = true)
		{
			List<string> list = new List<string>();
			int i = 0;
			int count = this.mLanguages.Count;
			while (i < count)
			{
				if (!skipDisabled || this.mLanguages[i].IsEnabled())
				{
					list.Add(this.mLanguages[i].Name);
				}
				i++;
			}
			return list;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00037520 File Offset: 0x00035920
		public List<string> GetLanguagesCode(bool allowRegions = true, bool skipDisabled = true)
		{
			List<string> list = new List<string>();
			int i = 0;
			int count = this.mLanguages.Count;
			while (i < count)
			{
				if (!skipDisabled || this.mLanguages[i].IsEnabled())
				{
					string text = this.mLanguages[i].Code;
					if (!allowRegions && text != null && text.Length > 2)
					{
						text = text.Substring(0, 2);
					}
					if (!string.IsNullOrEmpty(text) && !list.Contains(text))
					{
						list.Add(text);
					}
				}
				i++;
			}
			return list;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000375C4 File Offset: 0x000359C4
		public bool IsLanguageEnabled(string Language)
		{
			int languageIndex = this.GetLanguageIndex(Language, false, true);
			return languageIndex >= 0 && this.mLanguages[languageIndex].IsEnabled();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000375F8 File Offset: 0x000359F8
		public void EnableLanguage(string Language, bool bEnabled)
		{
			int languageIndex = this.GetLanguageIndex(Language, false, true);
			if (languageIndex >= 0)
			{
				this.mLanguages[languageIndex].SetEnabled(bEnabled);
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00037628 File Offset: 0x00035A28
		public bool AllowUnloadingLanguages()
		{
			return this._AllowUnloadingLanguages != LanguageSourceData.eAllowUnloadLanguages.Never;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00037638 File Offset: 0x00035A38
		private string GetSavedLanguageFileName(int languageIndex)
		{
			if (languageIndex < 0)
			{
				return null;
			}
			return string.Concat(new string[]
			{
				"LangSource_",
				this.GetSourcePlayerPrefName(),
				"_",
				this.mLanguages[languageIndex].Name,
				".loc"
			});
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00037690 File Offset: 0x00035A90
		public void LoadLanguage(int languageIndex, bool UnloadOtherLanguages, bool useFallback, bool onlyCurrentSpecialization, bool forceLoad)
		{
			if (!this.AllowUnloadingLanguages())
			{
				return;
			}
			if (!PersistentStorage.CanAccessFiles())
			{
				return;
			}
			if (languageIndex >= 0 && (forceLoad || !this.mLanguages[languageIndex].IsLoaded()))
			{
				string savedLanguageFileName = this.GetSavedLanguageFileName(languageIndex);
				string text = PersistentStorage.LoadFile(PersistentStorage.eFileType.Temporal, savedLanguageFileName, false);
				if (!string.IsNullOrEmpty(text))
				{
					this.Import_Language_from_Cache(languageIndex, text, useFallback, onlyCurrentSpecialization);
					this.mLanguages[languageIndex].SetLoaded(true);
				}
			}
			if (UnloadOtherLanguages && I2Utils.IsPlaying())
			{
				for (int i = 0; i < this.mLanguages.Count; i++)
				{
					if (i != languageIndex)
					{
						this.UnloadLanguage(i);
					}
				}
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0003774C File Offset: 0x00035B4C
		public void LoadAllLanguages(bool forceLoad = false)
		{
			for (int i = 0; i < this.mLanguages.Count; i++)
			{
				this.LoadLanguage(i, false, false, false, forceLoad);
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00037780 File Offset: 0x00035B80
		public void UnloadLanguage(int languageIndex)
		{
			if (!this.AllowUnloadingLanguages())
			{
				return;
			}
			if (!PersistentStorage.CanAccessFiles())
			{
				return;
			}
			if (!I2Utils.IsPlaying() || !this.mLanguages[languageIndex].IsLoaded() || !this.mLanguages[languageIndex].CanBeUnloaded() || this.IsCurrentLanguage(languageIndex) || !PersistentStorage.HasFile(PersistentStorage.eFileType.Temporal, this.GetSavedLanguageFileName(languageIndex), true))
			{
				return;
			}
			foreach (TermData termData in this.mTerms)
			{
				termData.Languages[languageIndex] = null;
			}
			this.mLanguages[languageIndex].SetLoaded(false);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00037860 File Offset: 0x00035C60
		public void SaveLanguages(bool unloadAll, PersistentStorage.eFileType fileLocation = PersistentStorage.eFileType.Temporal)
		{
			if (!this.AllowUnloadingLanguages())
			{
				return;
			}
			if (!PersistentStorage.CanAccessFiles())
			{
				return;
			}
			for (int i = 0; i < this.mLanguages.Count; i++)
			{
				string text = this.Export_Language_to_Cache(i, this.IsCurrentLanguage(i));
				if (!string.IsNullOrEmpty(text))
				{
					PersistentStorage.SaveFile(PersistentStorage.eFileType.Temporal, this.GetSavedLanguageFileName(i), text, true);
				}
			}
			if (unloadAll)
			{
				for (int j = 0; j < this.mLanguages.Count; j++)
				{
					if (unloadAll && !this.IsCurrentLanguage(j))
					{
						this.UnloadLanguage(j);
					}
				}
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0003790C File Offset: 0x00035D0C
		public bool HasUnloadedLanguages()
		{
			for (int i = 0; i < this.mLanguages.Count; i++)
			{
				if (!this.mLanguages[i].IsLoaded())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00037950 File Offset: 0x00035D50
		public List<string> GetCategories(bool OnlyMainCategory = false, List<string> Categories = null)
		{
			if (Categories == null)
			{
				Categories = new List<string>();
			}
			foreach (TermData termData in this.mTerms)
			{
				string categoryFromFullTerm = LanguageSourceData.GetCategoryFromFullTerm(termData.Term, OnlyMainCategory);
				if (!Categories.Contains(categoryFromFullTerm))
				{
					Categories.Add(categoryFromFullTerm);
				}
			}
			Categories.Sort();
			return Categories;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000379DC File Offset: 0x00035DDC
		public static string GetKeyFromFullTerm(string FullTerm, bool OnlyMainCategory = false)
		{
			int num = (!OnlyMainCategory) ? FullTerm.LastIndexOfAny(LanguageSourceData.CategorySeparators) : FullTerm.IndexOfAny(LanguageSourceData.CategorySeparators);
			return (num >= 0) ? FullTerm.Substring(num + 1) : FullTerm;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00037A24 File Offset: 0x00035E24
		public static string GetCategoryFromFullTerm(string FullTerm, bool OnlyMainCategory = false)
		{
			int num = (!OnlyMainCategory) ? FullTerm.LastIndexOfAny(LanguageSourceData.CategorySeparators) : FullTerm.IndexOfAny(LanguageSourceData.CategorySeparators);
			return (num >= 0) ? FullTerm.Substring(0, num) : LanguageSourceData.EmptyCategory;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00037A6C File Offset: 0x00035E6C
		public static void DeserializeFullTerm(string FullTerm, out string Key, out string Category, bool OnlyMainCategory = false)
		{
			int num = (!OnlyMainCategory) ? FullTerm.LastIndexOfAny(LanguageSourceData.CategorySeparators) : FullTerm.IndexOfAny(LanguageSourceData.CategorySeparators);
			if (num < 0)
			{
				Category = LanguageSourceData.EmptyCategory;
				Key = FullTerm;
			}
			else
			{
				Category = FullTerm.Substring(0, num);
				Key = FullTerm.Substring(num + 1);
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00037AC8 File Offset: 0x00035EC8
		public void UpdateDictionary(bool force = false)
		{
			if (!force && this.mDictionary != null && this.mDictionary.Count == this.mTerms.Count)
			{
				return;
			}
			StringComparer stringComparer = (!this.CaseInsensitiveTerms) ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
			if (this.mDictionary.Comparer != stringComparer)
			{
				this.mDictionary = new Dictionary<string, TermData>(stringComparer);
			}
			else
			{
				this.mDictionary.Clear();
			}
			int i = 0;
			int count = this.mTerms.Count;
			while (i < count)
			{
				TermData termData = this.mTerms[i];
				LanguageSourceData.ValidateFullTerm(ref termData.Term);
				this.mDictionary[termData.Term] = this.mTerms[i];
				this.mTerms[i].Validate();
				i++;
			}
			if (I2Utils.IsPlaying())
			{
				this.SaveLanguages(true, PersistentStorage.eFileType.Temporal);
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00037BC0 File Offset: 0x00035FC0
		public string GetTranslation(string term, string overrideLanguage = null, string overrideSpecialization = null, bool skipDisabled = false, bool allowCategoryMistmatch = false)
		{
			string result;
			if (this.TryGetTranslation(term, out result, overrideLanguage, overrideSpecialization, skipDisabled, allowCategoryMistmatch))
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00037BE8 File Offset: 0x00035FE8
		public bool TryGetTranslation(string term, out string Translation, string overrideLanguage = null, string overrideSpecialization = null, bool skipDisabled = false, bool allowCategoryMistmatch = false)
		{
			int languageIndex = this.GetLanguageIndex((overrideLanguage != null) ? overrideLanguage : LocalizationManager.CurrentLanguage, true, false);
			if (languageIndex >= 0 && (!skipDisabled || this.mLanguages[languageIndex].IsEnabled()))
			{
				TermData termData = this.GetTermData(term, allowCategoryMistmatch);
				if (termData != null)
				{
					Translation = termData.GetTranslation(languageIndex, overrideSpecialization, true);
					if (Translation == "---")
					{
						Translation = string.Empty;
						return true;
					}
					if (!string.IsNullOrEmpty(Translation))
					{
						return true;
					}
					Translation = null;
				}
				if (this.OnMissingTranslation == LanguageSourceData.MissingTranslationAction.ShowWarning)
				{
					Translation = string.Format("<!-Missing Translation [{0}]-!>", term);
					return true;
				}
				if (this.OnMissingTranslation == LanguageSourceData.MissingTranslationAction.Fallback && termData != null)
				{
					return this.TryGetFallbackTranslation(termData, out Translation, languageIndex, overrideSpecialization, skipDisabled);
				}
				if (this.OnMissingTranslation == LanguageSourceData.MissingTranslationAction.Empty)
				{
					Translation = string.Empty;
					return true;
				}
				if (this.OnMissingTranslation == LanguageSourceData.MissingTranslationAction.ShowTerm)
				{
					Translation = term;
					return true;
				}
			}
			Translation = null;
			return false;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00037CE0 File Offset: 0x000360E0
		private bool TryGetFallbackTranslation(TermData termData, out string Translation, int langIndex, string overrideSpecialization = null, bool skipDisabled = false)
		{
			string text = this.mLanguages[langIndex].Code;
			if (!string.IsNullOrEmpty(text))
			{
				if (text.Contains('-'))
				{
					text = text.Substring(0, text.IndexOf('-'));
				}
				for (int i = 0; i < this.mLanguages.Count; i++)
				{
					if (i != langIndex && this.mLanguages[i].Code.StartsWith(text) && (!skipDisabled || this.mLanguages[i].IsEnabled()))
					{
						Translation = termData.GetTranslation(i, overrideSpecialization, true);
						if (!string.IsNullOrEmpty(Translation))
						{
							return true;
						}
					}
				}
			}
			for (int j = 0; j < this.mLanguages.Count; j++)
			{
				if (j != langIndex && (!skipDisabled || this.mLanguages[j].IsEnabled()) && (text == null || !this.mLanguages[j].Code.StartsWith(text)))
				{
					Translation = termData.GetTranslation(j, overrideSpecialization, true);
					if (!string.IsNullOrEmpty(Translation))
					{
						return true;
					}
				}
			}
			Translation = null;
			return false;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00037E1D File Offset: 0x0003621D
		public TermData AddTerm(string term)
		{
			return this.AddTerm(term, eTermType.Text, true);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00037E28 File Offset: 0x00036228
		public TermData GetTermData(string term, bool allowCategoryMistmatch = false)
		{
			if (string.IsNullOrEmpty(term))
			{
				return null;
			}
			if (this.mDictionary.Count == 0)
			{
				this.UpdateDictionary(false);
			}
			TermData result;
			if (this.mDictionary.TryGetValue(term, out result))
			{
				return result;
			}
			TermData termData = null;
			if (allowCategoryMistmatch)
			{
				string keyFromFullTerm = LanguageSourceData.GetKeyFromFullTerm(term, false);
				foreach (KeyValuePair<string, TermData> keyValuePair in this.mDictionary)
				{
					if (keyValuePair.Value.IsTerm(keyFromFullTerm, true))
					{
						if (termData != null)
						{
							return null;
						}
						termData = keyValuePair.Value;
					}
				}
				return termData;
			}
			return termData;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00037EFC File Offset: 0x000362FC
		public bool ContainsTerm(string term)
		{
			return this.GetTermData(term, false) != null;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00037F0C File Offset: 0x0003630C
		public List<string> GetTermsList(string Category = null)
		{
			if (this.mDictionary.Count != this.mTerms.Count)
			{
				this.UpdateDictionary(false);
			}
			if (string.IsNullOrEmpty(Category))
			{
				return new List<string>(this.mDictionary.Keys);
			}
			List<string> list = new List<string>();
			for (int i = 0; i < this.mTerms.Count; i++)
			{
				string term = this.mTerms[i].Term;
				if (term.Length > Category.Length && term[Category.Length] == '/' && term.StartsWith(Category))
				{
					list.Add(term);
				}
			}
			return list;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00037FC4 File Offset: 0x000363C4
		public TermData AddTerm(string NewTerm, eTermType termType, bool SaveSource = true)
		{
			LanguageSourceData.ValidateFullTerm(ref NewTerm);
			NewTerm = NewTerm.Trim();
			if (this.mLanguages.Count == 0)
			{
				this.AddLanguage("English", "en");
			}
			TermData termData = this.GetTermData(NewTerm, false);
			if (termData == null)
			{
				termData = new TermData();
				termData.Term = NewTerm;
				termData.TermType = termType;
				termData.Languages = new string[this.mLanguages.Count];
				termData.Flags = new byte[this.mLanguages.Count];
				this.mTerms.Add(termData);
				this.mDictionary.Add(NewTerm, termData);
			}
			return termData;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0003806C File Offset: 0x0003646C
		public void RemoveTerm(string term)
		{
			int i = 0;
			int count = this.mTerms.Count;
			while (i < count)
			{
				if (this.mTerms[i].Term == term)
				{
					this.mTerms.RemoveAt(i);
					this.mDictionary.Remove(term);
					return;
				}
				i++;
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000380D0 File Offset: 0x000364D0
		public static void ValidateFullTerm(ref string Term)
		{
			Term = Term.Replace('\\', '/');
			Term = Term.Trim();
			if (Term.StartsWith(LanguageSourceData.EmptyCategory, StringComparison.Ordinal) && Term.Length > LanguageSourceData.EmptyCategory.Length && Term[LanguageSourceData.EmptyCategory.Length] == '/')
			{
				Term = Term.Substring(LanguageSourceData.EmptyCategory.Length + 1);
			}
			Term = I2Utils.GetValidTermName(Term, true);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00038152 File Offset: 0x00036552
		public UnityEngine.Object ownerObject
		{
			get
			{
				return this.owner as UnityEngine.Object;
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060016D2 RID: 5842 RVA: 0x00038160 File Offset: 0x00036560
		// (remove) Token: 0x060016D3 RID: 5843 RVA: 0x00038198 File Offset: 0x00036598
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<LanguageSourceData, bool, string> Event_OnSourceUpdateFromGoogle;

		// Token: 0x060016D4 RID: 5844 RVA: 0x000381CE File Offset: 0x000365CE
		public void Awake()
		{
			LocalizationManager.AddSource(this);
			this.UpdateDictionary(false);
			this.UpdateAssetDictionary();
			LocalizationManager.LocalizeAll(true);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000381E9 File Offset: 0x000365E9
		public void OnDestroy()
		{
			LocalizationManager.RemoveSource(this);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000381F4 File Offset: 0x000365F4
		public bool IsEqualTo(LanguageSourceData Source)
		{
			if (Source.mLanguages.Count != this.mLanguages.Count)
			{
				return false;
			}
			int i = 0;
			int count = this.mLanguages.Count;
			while (i < count)
			{
				if (Source.GetLanguageIndex(this.mLanguages[i].Name, true, true) < 0)
				{
					return false;
				}
				i++;
			}
			if (Source.mTerms.Count != this.mTerms.Count)
			{
				return false;
			}
			for (int j = 0; j < this.mTerms.Count; j++)
			{
				if (Source.GetTermData(this.mTerms[j].Term, false) == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000382B8 File Offset: 0x000366B8
		internal bool ManagerHasASimilarSource()
		{
			int i = 0;
			int count = LocalizationManager.Sources.Count;
			while (i < count)
			{
				LanguageSourceData languageSourceData = LocalizationManager.Sources[i];
				if (languageSourceData != null && languageSourceData.IsEqualTo(this) && languageSourceData != this)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0003830A File Offset: 0x0003670A
		public void ClearAllData()
		{
			this.mTerms.Clear();
			this.mLanguages.Clear();
			this.mDictionary.Clear();
			this.mAssetDictionary.Clear();
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00038338 File Offset: 0x00036738
		public bool IsGlobalSource()
		{
			return this.mIsGlobalSource;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00038340 File Offset: 0x00036740
		public void Editor_SetDirty()
		{
		}

		// Token: 0x04000E11 RID: 3601
		private string mDelayedGoogleData;

		// Token: 0x04000E12 RID: 3602
		public static string EmptyCategory = "Default";

		// Token: 0x04000E13 RID: 3603
		public static char[] CategorySeparators = "/\\".ToCharArray();

		// Token: 0x04000E14 RID: 3604
		[NonSerialized]
		public ILanguageSource owner;

		// Token: 0x04000E15 RID: 3605
		public bool UserAgreesToHaveItOnTheScene;

		// Token: 0x04000E16 RID: 3606
		public bool UserAgreesToHaveItInsideThePluginsFolder;

		// Token: 0x04000E17 RID: 3607
		public bool GoogleLiveSyncIsUptoDate = true;

		// Token: 0x04000E18 RID: 3608
		[NonSerialized]
		public bool mIsGlobalSource;

		// Token: 0x04000E19 RID: 3609
		public List<TermData> mTerms = new List<TermData>();

		// Token: 0x04000E1A RID: 3610
		public bool CaseInsensitiveTerms;

		// Token: 0x04000E1B RID: 3611
		[NonSerialized]
		public Dictionary<string, TermData> mDictionary = new Dictionary<string, TermData>(StringComparer.Ordinal);

		// Token: 0x04000E1C RID: 3612
		public LanguageSourceData.MissingTranslationAction OnMissingTranslation = LanguageSourceData.MissingTranslationAction.Fallback;

		// Token: 0x04000E1D RID: 3613
		public string mTerm_AppName;

		// Token: 0x04000E1E RID: 3614
		public List<LanguageData> mLanguages = new List<LanguageData>();

		// Token: 0x04000E1F RID: 3615
		public bool IgnoreDeviceLanguage;

		// Token: 0x04000E20 RID: 3616
		public LanguageSourceData.eAllowUnloadLanguages _AllowUnloadingLanguages;

		// Token: 0x04000E21 RID: 3617
		public string Google_WebServiceURL;

		// Token: 0x04000E22 RID: 3618
		public string Google_SpreadsheetKey;

		// Token: 0x04000E23 RID: 3619
		public string Google_SpreadsheetName;

		// Token: 0x04000E24 RID: 3620
		public string Google_LastUpdatedVersion;

		// Token: 0x04000E25 RID: 3621
		public LanguageSourceData.eGoogleUpdateFrequency GoogleUpdateFrequency = LanguageSourceData.eGoogleUpdateFrequency.Weekly;

		// Token: 0x04000E26 RID: 3622
		public LanguageSourceData.eGoogleUpdateFrequency GoogleInEditorCheckFrequency = LanguageSourceData.eGoogleUpdateFrequency.Daily;

		// Token: 0x04000E27 RID: 3623
		public LanguageSourceData.eGoogleUpdateSynchronization GoogleUpdateSynchronization = LanguageSourceData.eGoogleUpdateSynchronization.OnSceneLoaded;

		// Token: 0x04000E28 RID: 3624
		public float GoogleUpdateDelay;

		// Token: 0x04000E2A RID: 3626
		public List<UnityEngine.Object> Assets = new List<UnityEngine.Object>();

		// Token: 0x04000E2B RID: 3627
		[NonSerialized]
		public Dictionary<string, UnityEngine.Object> mAssetDictionary = new Dictionary<string, UnityEngine.Object>(StringComparer.Ordinal);

		// Token: 0x04000E2C RID: 3628
		[CompilerGenerated]
		private static Func<IGrouping<string, UnityEngine.Object>, UnityEngine.Object> <>f__mg$cache0;

		// Token: 0x020003E8 RID: 1000
		public enum MissingTranslationAction
		{
			// Token: 0x04000E39 RID: 3641
			Empty,
			// Token: 0x04000E3A RID: 3642
			Fallback,
			// Token: 0x04000E3B RID: 3643
			ShowWarning,
			// Token: 0x04000E3C RID: 3644
			ShowTerm
		}

		// Token: 0x020003E9 RID: 1001
		public enum eAllowUnloadLanguages
		{
			// Token: 0x04000E3E RID: 3646
			Never,
			// Token: 0x04000E3F RID: 3647
			OnlyInDevice,
			// Token: 0x04000E40 RID: 3648
			EditorAndDevice
		}

		// Token: 0x020003EA RID: 1002
		public enum eGoogleUpdateFrequency
		{
			// Token: 0x04000E42 RID: 3650
			Always,
			// Token: 0x04000E43 RID: 3651
			Never,
			// Token: 0x04000E44 RID: 3652
			Daily,
			// Token: 0x04000E45 RID: 3653
			Weekly,
			// Token: 0x04000E46 RID: 3654
			Monthly,
			// Token: 0x04000E47 RID: 3655
			OnlyOnce
		}

		// Token: 0x020003EB RID: 1003
		public enum eGoogleUpdateSynchronization
		{
			// Token: 0x04000E49 RID: 3657
			Manual,
			// Token: 0x04000E4A RID: 3658
			OnSceneLoaded,
			// Token: 0x04000E4B RID: 3659
			AsSoonAsDownloaded
		}
	}
}
