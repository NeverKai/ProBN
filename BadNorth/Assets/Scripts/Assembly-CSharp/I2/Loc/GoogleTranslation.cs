using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

namespace I2.Loc
{
	// Token: 0x020003D1 RID: 977
	public static class GoogleTranslation
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x00031A19 File Offset: 0x0002FE19
		public static bool CanTranslate()
		{
			return LocalizationManager.Sources.Count > 0 && !string.IsNullOrEmpty(LocalizationManager.GetWebServiceURL(null));
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00031A3C File Offset: 0x0002FE3C
		public static void Translate(string text, string LanguageCodeFrom, string LanguageCodeTo, Action<string, string> OnTranslationReady)
		{
			LocalizationManager.InitializeIfNeeded();
			if (!GoogleTranslation.CanTranslate())
			{
				OnTranslationReady(null, "WebService is not set correctly or needs to be reinstalled");
				return;
			}
			if (LanguageCodeTo == LanguageCodeFrom)
			{
				OnTranslationReady(text, null);
				return;
			}
			Dictionary<string, TranslationQuery> queries = new Dictionary<string, TranslationQuery>();
			if (string.IsNullOrEmpty(LanguageCodeTo))
			{
				OnTranslationReady(string.Empty, null);
				return;
			}
			GoogleTranslation.CreateQueries(text, LanguageCodeFrom, LanguageCodeTo, queries);
			GoogleTranslation.Translate(queries, delegate(Dictionary<string, TranslationQuery> results, string error)
			{
				if (!string.IsNullOrEmpty(error) || results.Count == 0)
				{
					OnTranslationReady(null, error);
					return;
				}
				string arg = GoogleTranslation.RebuildTranslation(text, queries, LanguageCodeTo);
				OnTranslationReady(arg, null);
			}, true);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00031B08 File Offset: 0x0002FF08
		public static string ForceTranslate(string text, string LanguageCodeFrom, string LanguageCodeTo)
		{
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			GoogleTranslation.AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dictionary);
			TranslationJob_Main translationJob_Main = new TranslationJob_Main(dictionary, null);
			TranslationJob.eJobState state;
			do
			{
				state = translationJob_Main.GetState();
			}
			while (state == TranslationJob.eJobState.Running);
			if (state == TranslationJob.eJobState.Failed)
			{
				return null;
			}
			return GoogleTranslation.GetQueryResult(text, string.Empty, dictionary);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00031B58 File Offset: 0x0002FF58
		public static void Translate(Dictionary<string, TranslationQuery> requests, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady, bool usePOST = true)
		{
			GoogleTranslation.AddTranslationJob(new TranslationJob_Main(requests, OnTranslationReady));
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00031B68 File Offset: 0x0002FF68
		public static bool ForceTranslate(Dictionary<string, TranslationQuery> requests, bool usePOST = true)
		{
			TranslationJob_Main translationJob_Main = new TranslationJob_Main(requests, null);
			TranslationJob.eJobState state;
			do
			{
				state = translationJob_Main.GetState();
			}
			while (state == TranslationJob.eJobState.Running);
			return state != TranslationJob.eJobState.Failed;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00031BA0 File Offset: 0x0002FFA0
		public static List<string> ConvertTranslationRequest(Dictionary<string, TranslationQuery> requests, bool encodeGET)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, TranslationQuery> keyValuePair in requests)
			{
				TranslationQuery value = keyValuePair.Value;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("<I2Loc>");
				}
				stringBuilder.Append(GoogleLanguages.GetGoogleLanguageCode(value.LanguageCode));
				stringBuilder.Append(":");
				for (int i = 0; i < value.TargetLanguagesCode.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(GoogleLanguages.GetGoogleLanguageCode(value.TargetLanguagesCode[i]));
				}
				stringBuilder.Append("=");
				string text = (!(GoogleTranslation.TitleCase(value.Text) == value.Text)) ? value.Text : value.Text.ToLowerInvariant();
				if (!encodeGET)
				{
					stringBuilder.Append(text);
				}
				else
				{
					stringBuilder.Append(Uri.EscapeDataString(text));
					if (stringBuilder.Length > 4000)
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
				}
			}
			list.Add(stringBuilder.ToString());
			return list;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00031D24 File Offset: 0x00030124
		private static void AddTranslationJob(TranslationJob job)
		{
			GoogleTranslation.mTranslationJobs.Add(job);
			if (GoogleTranslation.mTranslationJobs.Count == 1)
			{
				CoroutineManager.Start(GoogleTranslation.WaitForTranslations());
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00031D4C File Offset: 0x0003014C
		private static IEnumerator WaitForTranslations()
		{
			while (GoogleTranslation.mTranslationJobs.Count > 0)
			{
				TranslationJob[] jobs = GoogleTranslation.mTranslationJobs.ToArray();
				foreach (TranslationJob translationJob in jobs)
				{
					if (translationJob.GetState() != TranslationJob.eJobState.Running)
					{
						GoogleTranslation.mTranslationJobs.Remove(translationJob);
					}
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00031D60 File Offset: 0x00030160
		public static string ParseTranslationResult(string html, Dictionary<string, TranslationQuery> requests)
		{
			if (!html.StartsWith("<!DOCTYPE html>") && !html.StartsWith("<HTML>"))
			{
				string[] array = html.Split(new string[]
				{
					"<I2Loc>"
				}, StringSplitOptions.None);
				string[] separator = new string[]
				{
					"<i2>"
				};
				int num = 0;
				string[] array2 = requests.Keys.ToArray<string>();
				foreach (string text in array2)
				{
					TranslationQuery value = GoogleTranslation.FindQueryFromOrigText(text, requests);
					string text2 = array[num++];
					if (value.Tags != null)
					{
						for (int j = value.Tags.Length - 1; j >= 0; j--)
						{
							text2 = text2.Replace(GoogleTranslation.GetGoogleNoTranslateTag(j), value.Tags[j]);
						}
					}
					value.Results = text2.Split(separator, StringSplitOptions.None);
					if (GoogleTranslation.TitleCase(text) == text)
					{
						for (int k = 0; k < value.Results.Length; k++)
						{
							value.Results[k] = GoogleTranslation.TitleCase(value.Results[k]);
						}
					}
					requests[value.OrigText] = value;
				}
				return null;
			}
			if (html.Contains("The script completed but did not return anything"))
			{
				return "The current Google WebService is not supported.\nPlease, delete the WebService from the Google Drive and Install the latest version.";
			}
			if (html.Contains("Service invoked too many times in a short time"))
			{
				return string.Empty;
			}
			return "There was a problem contacting the WebService. Please try again later\n" + html;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00031EDE File Offset: 0x000302DE
		public static bool IsTranslating()
		{
			return GoogleTranslation.mCurrentTranslations.Count > 0 || GoogleTranslation.mTranslationJobs.Count > 0;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00031F00 File Offset: 0x00030300
		public static void CancelCurrentGoogleTranslations()
		{
			GoogleTranslation.mCurrentTranslations.Clear();
			foreach (TranslationJob translationJob in GoogleTranslation.mTranslationJobs)
			{
				translationJob.Dispose();
			}
			GoogleTranslation.mTranslationJobs.Clear();
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00031F70 File Offset: 0x00030370
		public static void CreateQueries(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!text.Contains("[i2s_"))
			{
				GoogleTranslation.CreateQueries_Plurals(text, LanguageCodeFrom, LanguageCodeTo, dict);
				return;
			}
			Dictionary<string, string> specializations = SpecializationManager.GetSpecializations(text, null);
			foreach (KeyValuePair<string, string> keyValuePair in specializations)
			{
				GoogleTranslation.CreateQueries_Plurals(keyValuePair.Value, LanguageCodeFrom, LanguageCodeTo, dict);
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00031FF4 File Offset: 0x000303F4
		private static void CreateQueries_Plurals(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			bool flag = text.Contains("{[#");
			bool flag2 = text.Contains("[i2p_");
			if (!GoogleTranslation.HasParameters(text) || (!flag && !flag2))
			{
				GoogleTranslation.AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dict);
				return;
			}
			bool forceTag = flag;
			for (ePluralType ePluralType = ePluralType.Zero; ePluralType <= ePluralType.Plural; ePluralType++)
			{
				string pluralType = ePluralType.ToString();
				if (GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, pluralType))
				{
					string text2 = GoogleTranslation.GetPluralText(text, pluralType);
					int pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType);
					string pluralParameter = GoogleTranslation.GetPluralParameter(text2, forceTag);
					if (!string.IsNullOrEmpty(pluralParameter))
					{
						text2 = text2.Replace(pluralParameter, pluralTestNumber.ToString());
					}
					GoogleTranslation.AddQuery(text2, LanguageCodeFrom, LanguageCodeTo, dict);
				}
			}
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000320BC File Offset: 0x000304BC
		public static void AddQuery(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!dict.ContainsKey(text))
			{
				TranslationQuery value = new TranslationQuery
				{
					OrigText = text,
					LanguageCode = LanguageCodeFrom,
					TargetLanguagesCode = new string[]
					{
						LanguageCodeTo
					}
				};
				value.Text = text;
				GoogleTranslation.ParseNonTranslatableElements(ref value);
				dict[text] = value;
			}
			else
			{
				TranslationQuery value2 = dict[text];
				if (Array.IndexOf<string>(value2.TargetLanguagesCode, LanguageCodeTo) < 0)
				{
					value2.TargetLanguagesCode = value2.TargetLanguagesCode.Concat(new string[]
					{
						LanguageCodeTo
					}).Distinct<string>().ToArray<string>();
				}
				dict[text] = value2;
			}
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00032174 File Offset: 0x00030574
		private static string GetTranslation(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!dict.ContainsKey(text))
			{
				return null;
			}
			TranslationQuery translationQuery = dict[text];
			int num = Array.IndexOf<string>(translationQuery.TargetLanguagesCode, LanguageCodeTo);
			if (num < 0)
			{
				return string.Empty;
			}
			if (translationQuery.Results == null)
			{
				return string.Empty;
			}
			return translationQuery.Results[num];
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000321D0 File Offset: 0x000305D0
		private static TranslationQuery FindQueryFromOrigText(string origText, Dictionary<string, TranslationQuery> dict)
		{
			foreach (KeyValuePair<string, TranslationQuery> keyValuePair in dict)
			{
				if (keyValuePair.Value.OrigText == origText)
				{
					return keyValuePair.Value;
				}
			}
			return default(TranslationQuery);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00032254 File Offset: 0x00030654
		public static bool HasParameters(string text)
		{
			int num = text.IndexOf("{[");
			return num >= 0 && text.IndexOf("]}", num) > 0;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00032288 File Offset: 0x00030688
		public static string GetPluralParameter(string text, bool forceTag)
		{
			int num = text.IndexOf("{[#");
			if (num < 0)
			{
				if (forceTag)
				{
					return null;
				}
				num = text.IndexOf("{[");
			}
			if (num < 0)
			{
				return null;
			}
			int num2 = text.IndexOf("]}", num + 2);
			if (num2 < 0)
			{
				return null;
			}
			return text.Substring(num, num2 - num + 2);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000322EC File Offset: 0x000306EC
		public static string GetPluralText(string text, string pluralType)
		{
			pluralType = "[i2p_" + pluralType + "]";
			int num = text.IndexOf(pluralType);
			if (num >= 0)
			{
				num += pluralType.Length;
				int num2 = text.IndexOf("[i2p_", num);
				if (num2 < 0)
				{
					num2 = text.Length;
				}
				return text.Substring(num, num2 - num);
			}
			num = text.IndexOf("[i2p_");
			if (num < 0)
			{
				return text;
			}
			if (num > 0)
			{
				return text.Substring(0, num);
			}
			num = text.IndexOf("]");
			if (num < 0)
			{
				return text;
			}
			num++;
			int num3 = text.IndexOf("[i2p_", num);
			if (num3 < 0)
			{
				num3 = text.Length;
			}
			return text.Substring(num, num3 - num);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000323AC File Offset: 0x000307AC
		private static int FindClosingTag(string tag, MatchCollection matches, int startIndex)
		{
			int i = startIndex;
			int count = matches.Count;
			while (i < count)
			{
				string captureMatch = I2Utils.GetCaptureMatch(matches[i]);
				if (captureMatch[0] == '/' && tag.StartsWith(captureMatch.Substring(1)))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00032404 File Offset: 0x00030804
		private static string GetGoogleNoTranslateTag(int tagNumber)
		{
			if (tagNumber < 70)
			{
				return "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++".Substring(0, tagNumber + 1);
			}
			string text = string.Empty;
			for (int i = -1; i < tagNumber; i++)
			{
				text += "+";
			}
			return text;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00032450 File Offset: 0x00030850
		private static void ParseNonTranslatableElements(ref TranslationQuery query)
		{
			MatchCollection matchCollection = Regex.Matches(query.Text, "\\{\\[(.*?)]}|\\[(.*?)]|\\<(.*?)>");
			if (matchCollection == null || matchCollection.Count == 0)
			{
				return;
			}
			string text = query.Text;
			List<string> list = new List<string>();
			int i = 0;
			int count = matchCollection.Count;
			while (i < count)
			{
				string captureMatch = I2Utils.GetCaptureMatch(matchCollection[i]);
				int num = GoogleTranslation.FindClosingTag(captureMatch, matchCollection, i);
				if (num < 0)
				{
					string text2 = matchCollection[i].ToString();
					if (text2.StartsWith("{[") && text2.EndsWith("]}"))
					{
						text = text.Replace(text2, GoogleTranslation.GetGoogleNoTranslateTag(list.Count) + " ");
						list.Add(text2);
					}
				}
				else if (captureMatch == "i2nt")
				{
					string text3 = query.Text.Substring(matchCollection[i].Index, matchCollection[num].Index - matchCollection[i].Index + matchCollection[num].Length);
					text = text.Replace(text3, GoogleTranslation.GetGoogleNoTranslateTag(list.Count) + " ");
					list.Add(text3);
				}
				else
				{
					string text4 = matchCollection[i].ToString();
					text = text.Replace(text4, GoogleTranslation.GetGoogleNoTranslateTag(list.Count) + " ");
					list.Add(text4);
					string text5 = matchCollection[num].ToString();
					text = text.Replace(text5, GoogleTranslation.GetGoogleNoTranslateTag(list.Count) + " ");
					list.Add(text5);
				}
				i++;
			}
			query.Text = text;
			query.Tags = list.ToArray();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00032620 File Offset: 0x00030A20
		public static string GetQueryResult(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!dict.ContainsKey(text))
			{
				return null;
			}
			TranslationQuery translationQuery = dict[text];
			if (translationQuery.Results == null || translationQuery.Results.Length < 0)
			{
				return null;
			}
			if (string.IsNullOrEmpty(LanguageCodeTo))
			{
				return translationQuery.Results[0];
			}
			int num = Array.IndexOf<string>(translationQuery.TargetLanguagesCode, LanguageCodeTo);
			if (num < 0)
			{
				return null;
			}
			return translationQuery.Results[num];
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00032698 File Offset: 0x00030A98
		public static string RebuildTranslation(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			if (!text.Contains("[i2s_"))
			{
				return GoogleTranslation.RebuildTranslation_Plural(text, dict, LanguageCodeTo);
			}
			Dictionary<string, string> specializations = SpecializationManager.GetSpecializations(text, null);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> keyValuePair in specializations)
			{
				dictionary[keyValuePair.Key] = GoogleTranslation.RebuildTranslation_Plural(keyValuePair.Value, dict, LanguageCodeTo);
			}
			return SpecializationManager.SetSpecializedText(dictionary);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00032730 File Offset: 0x00030B30
		private static string RebuildTranslation_Plural(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			bool flag = text.Contains("{[#");
			bool flag2 = text.Contains("[i2p_");
			if (!GoogleTranslation.HasParameters(text) || (!flag && !flag2))
			{
				return GoogleTranslation.GetTranslation(text, LanguageCodeTo, dict);
			}
			StringBuilder stringBuilder = new StringBuilder();
			string b = null;
			bool forceTag = flag;
			for (ePluralType ePluralType = ePluralType.Plural; ePluralType >= ePluralType.Zero; ePluralType--)
			{
				string text2 = ePluralType.ToString();
				if (GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, text2))
				{
					string text3 = GoogleTranslation.GetPluralText(text, text2);
					int pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType);
					string pluralParameter = GoogleTranslation.GetPluralParameter(text3, forceTag);
					if (!string.IsNullOrEmpty(pluralParameter))
					{
						text3 = text3.Replace(pluralParameter, pluralTestNumber.ToString());
					}
					string text4 = GoogleTranslation.GetTranslation(text3, LanguageCodeTo, dict);
					if (!string.IsNullOrEmpty(pluralParameter))
					{
						text4 = text4.Replace(pluralTestNumber.ToString(), pluralParameter);
					}
					if (ePluralType == ePluralType.Plural)
					{
						b = text4;
					}
					else
					{
						if (text4 == b)
						{
							goto IL_117;
						}
						stringBuilder.AppendFormat("[i2p_{0}]", text2);
					}
					stringBuilder.Append(text4);
				}
				IL_117:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00032868 File Offset: 0x00030C68
		public static string UppercaseFirst(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			char[] array = s.ToLower().ToCharArray();
			array[0] = char.ToUpper(array[0]);
			return new string(array);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000328A3 File Offset: 0x00030CA3
		public static string TitleCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
		}

		// Token: 0x04000DC3 RID: 3523
		private static List<UnityWebRequest> mCurrentTranslations = new List<UnityWebRequest>();

		// Token: 0x04000DC4 RID: 3524
		private static List<TranslationJob> mTranslationJobs = new List<TranslationJob>();
	}
}
