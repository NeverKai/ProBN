using System;
using System.Collections.Generic;

namespace I2.Loc
{
	// Token: 0x020003CC RID: 972
	public class SpecializationManager : BaseSpecializationManager
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x0002D418 File Offset: 0x0002B818
		private SpecializationManager()
		{
			this.InitializeSpecializations();
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0002D428 File Offset: 0x0002B828
		public static string GetSpecializedText(string text, string specialization = null)
		{
			int num = text.IndexOf("[i2s_");
			if (num < 0)
			{
				return text;
			}
			if (string.IsNullOrEmpty(specialization))
			{
				specialization = SpecializationManager.Singleton.GetCurrentSpecialization();
			}
			while (!string.IsNullOrEmpty(specialization) && specialization != "Any")
			{
				string text2 = "[i2s_" + specialization + "]";
				int num2 = text.IndexOf(text2);
				if (num2 >= 0)
				{
					num2 += text2.Length;
					int num3 = text.IndexOf("[i2s_", num2);
					if (num3 < 0)
					{
						num3 = text.Length;
					}
					return text.Substring(num2, num3 - num2);
				}
				specialization = SpecializationManager.Singleton.GetFallbackSpecialization(specialization);
			}
			return text.Substring(0, num);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0002D4EC File Offset: 0x0002B8EC
		public static string SetSpecializedText(string text, string newText, string specialization)
		{
			if (string.IsNullOrEmpty(specialization))
			{
				specialization = "Any";
			}
			if ((text == null || !text.Contains("[i2s_")) && specialization == "Any")
			{
				return newText;
			}
			Dictionary<string, string> specializations = SpecializationManager.GetSpecializations(text, null);
			specializations[specialization] = newText;
			return SpecializationManager.SetSpecializedText(specializations);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0002D54C File Offset: 0x0002B94C
		public static string SetSpecializedText(Dictionary<string, string> specializations)
		{
			string text;
			if (!specializations.TryGetValue("Any", out text))
			{
				text = string.Empty;
			}
			foreach (KeyValuePair<string, string> keyValuePair in specializations)
			{
				if (keyValuePair.Key != "Any" && !string.IsNullOrEmpty(keyValuePair.Value))
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"[i2s_",
						keyValuePair.Key,
						"]",
						keyValuePair.Value
					});
				}
			}
			return text;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0002D614 File Offset: 0x0002BA14
		public static Dictionary<string, string> GetSpecializations(string text, Dictionary<string, string> buffer = null)
		{
			if (buffer == null)
			{
				buffer = new Dictionary<string, string>();
			}
			else
			{
				buffer.Clear();
			}
			if (text == null)
			{
				buffer["Any"] = string.Empty;
				return buffer;
			}
			int num = text.IndexOf("[i2s_");
			if (num < 0)
			{
				num = text.Length;
			}
			buffer["Any"] = text.Substring(0, num);
			for (int i = num; i < text.Length; i = num)
			{
				i += "[i2s_".Length;
				int num2 = text.IndexOf(']', i);
				if (num2 < 0)
				{
					break;
				}
				string key = text.Substring(i, num2 - i);
				i = num2 + 1;
				num = text.IndexOf("[i2s_", i);
				if (num < 0)
				{
					num = text.Length;
				}
				string value = text.Substring(i, num - i);
				buffer[key] = value;
			}
			return buffer;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0002D6F8 File Offset: 0x0002BAF8
		public static void AppendSpecializations(string text, List<string> list = null)
		{
			if (text == null)
			{
				return;
			}
			if (list == null)
			{
				list = new List<string>();
			}
			if (!list.Contains("Any"))
			{
				list.Add("Any");
			}
			int i = 0;
			while (i < text.Length)
			{
				i = text.IndexOf("[i2s_", i);
				if (i < 0)
				{
					break;
				}
				i += "[i2s_".Length;
				int num = text.IndexOf(']', i);
				if (num < 0)
				{
					break;
				}
				string item = text.Substring(i, num - i);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
		}

		// Token: 0x04000DB4 RID: 3508
		public static SpecializationManager Singleton = new SpecializationManager();
	}
}
