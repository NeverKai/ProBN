using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	// Token: 0x0200040D RID: 1037
	public static class I2Utils
	{
		// Token: 0x06001807 RID: 6151 RVA: 0x0003CC7C File Offset: 0x0003B07C
		public static string ReverseText(string source)
		{
			int length = source.Length;
			char[] array = new char[length];
			for (int i = 0; i < length; i++)
			{
				array[length - 1 - i] = source[i];
			}
			return new string(array);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0003CCC0 File Offset: 0x0003B0C0
		public static string RemoveNonASCII(string text, bool allowCategory = false)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			int num = 0;
			char[] array = new char[text.Length];
			bool flag = false;
			foreach (char c in text.Trim().ToCharArray())
			{
				char c2 = ' ';
				if ((allowCategory && (c == '\\' || c == '"' || c == '/')) || char.IsLetterOrDigit(c) || ".-_$#@*()[]{}+:?!&',^=<>~`".IndexOf(c) >= 0)
				{
					c2 = c;
				}
				if (char.IsWhiteSpace(c2))
				{
					if (!flag)
					{
						if (num > 0)
						{
							array[num++] = ' ';
						}
						flag = true;
					}
				}
				else
				{
					flag = false;
					array[num++] = c2;
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0003CD93 File Offset: 0x0003B193
		public static string GetValidTermName(string text, bool allowCategory = false)
		{
			if (text == null)
			{
				return null;
			}
			text = I2Utils.RemoveTags(text);
			return I2Utils.RemoveNonASCII(text, allowCategory);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0003CDAC File Offset: 0x0003B1AC
		public static string SplitLine(string line, int maxCharacters)
		{
			if (maxCharacters <= 0 || line.Length < maxCharacters)
			{
				return line;
			}
			char[] array = line.ToCharArray();
			bool flag = true;
			bool flag2 = false;
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				if (flag)
				{
					num++;
					if (array[i] == '\n')
					{
						num = 0;
					}
					if (num >= maxCharacters && char.IsWhiteSpace(array[i]))
					{
						array[i] = '\n';
						flag = false;
						flag2 = false;
					}
				}
				else if (!char.IsWhiteSpace(array[i]))
				{
					flag = true;
					num = 0;
				}
				else if (array[i] != '\n')
				{
					array[i] = '\0';
				}
				else
				{
					if (!flag2)
					{
						array[i] = '\0';
					}
					flag2 = true;
				}
				i++;
			}
			return new string((from c in array
			where c != '\0'
			select c).ToArray<char>());
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0003CE90 File Offset: 0x0003B290
		public static bool FindNextTag(string line, int iStart, out int tagStart, out int tagEnd)
		{
			tagStart = -1;
			tagEnd = -1;
			int length = line.Length;
			for (tagStart = iStart; tagStart < length; tagStart++)
			{
				if (line[tagStart] == '[' || line[tagStart] == '(' || line[tagStart] == '{' || line[tagStart] == '<')
				{
					break;
				}
			}
			if (tagStart == length)
			{
				return false;
			}
			bool flag = false;
			for (tagEnd = tagStart + 1; tagEnd < length; tagEnd++)
			{
				char c = line[tagEnd];
				if (c == ']' || c == ')' || c == '}' || c == '>')
				{
					return !flag || I2Utils.FindNextTag(line, tagEnd + 1, out tagStart, out tagEnd);
				}
				if (c > 'ÿ')
				{
					flag = true;
				}
			}
			return false;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0003CF72 File Offset: 0x0003B372
		public static string RemoveTags(string text)
		{
			return Regex.Replace(text, "\\{\\[(.*?)]}|\\[(.*?)]|\\<(.*?)>", string.Empty);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0003CF84 File Offset: 0x0003B384
		public static bool RemoveResourcesPath(ref string sPath)
		{
			int num = sPath.IndexOf("\\Resources\\");
			int num2 = sPath.IndexOf("\\Resources/");
			int num3 = sPath.IndexOf("/Resources\\");
			int num4 = sPath.IndexOf("/Resources/");
			int num5 = Mathf.Max(new int[]
			{
				num,
				num2,
				num3,
				num4
			});
			bool result = false;
			if (num5 >= 0)
			{
				sPath = sPath.Substring(num5 + 11);
				result = true;
			}
			else
			{
				num5 = sPath.LastIndexOfAny(LanguageSourceData.CategorySeparators);
				if (num5 > 0)
				{
					sPath = sPath.Substring(num5 + 1);
				}
			}
			string extension = Path.GetExtension(sPath);
			if (!string.IsNullOrEmpty(extension))
			{
				sPath = sPath.Substring(0, sPath.Length - extension.Length);
			}
			return result;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0003D056 File Offset: 0x0003B456
		public static bool IsPlaying()
		{
			return Application.isPlaying;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0003D068 File Offset: 0x0003B468
		public static string GetPath(this Transform tr)
		{
			Transform parent = tr.parent;
			if (tr == null)
			{
				return tr.name;
			}
			return parent.GetPath() + "/" + tr.name;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0003D0A5 File Offset: 0x0003B4A5
		public static Transform FindObject(string objectPath)
		{
			return I2Utils.FindObject(SceneManager.GetActiveScene(), objectPath);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0003D0B4 File Offset: 0x0003B4B4
		public static Transform FindObject(Scene scene, string objectPath)
		{
			GameObject[] rootGameObjects = scene.GetRootGameObjects();
			for (int i = 0; i < rootGameObjects.Length; i++)
			{
				Transform transform = rootGameObjects[i].transform;
				if (transform.name == objectPath)
				{
					return transform;
				}
				if (objectPath.StartsWith(transform.name + "/"))
				{
					return I2Utils.FindObject(transform, objectPath.Substring(transform.name.Length + 1));
				}
			}
			return null;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0003D134 File Offset: 0x0003B534
		public static Transform FindObject(Transform root, string objectPath)
		{
			for (int i = 0; i < root.childCount; i++)
			{
				Transform child = root.GetChild(i);
				if (child.name == objectPath)
				{
					return child;
				}
				if (objectPath.StartsWith(child.name + "/"))
				{
					return I2Utils.FindObject(child, objectPath.Substring(child.name.Length + 1));
				}
			}
			return null;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0003D1B0 File Offset: 0x0003B5B0
		public static H FindInParents<H>(Transform tr) where H : Component
		{
			if (!tr)
			{
				return (H)((object)null);
			}
			H component = tr.GetComponent<H>();
			while (!component && tr)
			{
				component = tr.GetComponent<H>();
				tr = tr.parent;
			}
			return component;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0003D208 File Offset: 0x0003B608
		public static string GetCaptureMatch(Match match)
		{
			for (int i = match.Groups.Count - 1; i >= 0; i--)
			{
				if (match.Groups[i].Success)
				{
					return match.Groups[i].ToString();
				}
			}
			return match.ToString();
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0003D261 File Offset: 0x0003B661
		public static void SendWebRequest(UnityWebRequest www)
		{
			www.SendWebRequest();
		}

		// Token: 0x04000EB0 RID: 3760
		public const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

		// Token: 0x04000EB1 RID: 3761
		public const string NumberChars = "0123456789";

		// Token: 0x04000EB2 RID: 3762
		public const string ValidNameSymbols = ".-_$#@*()[]{}+:?!&',^=<>~`";
	}
}
