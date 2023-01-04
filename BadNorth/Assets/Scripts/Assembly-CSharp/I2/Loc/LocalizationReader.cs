﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003EC RID: 1004
	public class LocalizationReader
	{
		// Token: 0x060016E7 RID: 5863 RVA: 0x000385D4 File Offset: 0x000369D4
		public static Dictionary<string, string> ReadTextAsset(TextAsset asset)
		{
			string text = Encoding.UTF8.GetString(asset.bytes, 0, asset.bytes.Length);
			text = text.Replace("\r\n", "\n");
			text = text.Replace("\r", "\n");
			StringReader stringReader = new StringReader(text);
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			string line;
			while ((line = stringReader.ReadLine()) != null)
			{
				string text2;
				string value;
				string text3;
				string text4;
				string text5;
				if (LocalizationReader.TextAsset_ReadLine(line, out text2, out value, out text3, out text4, out text5))
				{
					if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(value))
					{
						dictionary[text2] = value;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00038680 File Offset: 0x00036A80
		public static bool TextAsset_ReadLine(string line, out string key, out string value, out string category, out string comment, out string termType)
		{
			key = string.Empty;
			category = string.Empty;
			comment = string.Empty;
			termType = string.Empty;
			value = string.Empty;
			int num = line.LastIndexOf("//");
			if (num >= 0)
			{
				comment = line.Substring(num + 2).Trim();
				comment = LocalizationReader.DecodeString(comment);
				line = line.Substring(0, num);
			}
			int num2 = line.IndexOf("=");
			if (num2 < 0)
			{
				return false;
			}
			key = line.Substring(0, num2).Trim();
			value = line.Substring(num2 + 1).Trim();
			value = value.Replace("\r\n", "\n").Replace("\n", "\\n");
			value = LocalizationReader.DecodeString(value);
			if (key.Length > 2 && key[0] == '[')
			{
				int num3 = key.IndexOf(']');
				if (num3 >= 0)
				{
					termType = key.Substring(1, num3 - 1);
					key = key.Substring(num3 + 1);
				}
			}
			LocalizationReader.ValidateFullTerm(ref key);
			return true;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00038798 File Offset: 0x00036B98
		public static string ReadCSVfile(string Path, Encoding encoding)
		{
			string text = string.Empty;
			using (StreamReader streamReader = new StreamReader(Path, encoding))
			{
				text = streamReader.ReadToEnd();
			}
			text = text.Replace("\r\n", "\n");
			text = text.Replace("\r", "\n");
			return text;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00038800 File Offset: 0x00036C00
		public static List<string[]> ReadCSV(string Text, char Separator = ',')
		{
			int i = 0;
			List<string[]> list = new List<string[]>();
			while (i < Text.Length)
			{
				string[] array = LocalizationReader.ParseCSVline(Text, ref i, Separator);
				if (array == null)
				{
					break;
				}
				list.Add(array);
			}
			return list;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00038844 File Offset: 0x00036C44
		private static string[] ParseCSVline(string Line, ref int iStart, char Separator)
		{
			List<string> list = new List<string>();
			int length = Line.Length;
			int num = iStart;
			bool flag = false;
			while (iStart < length)
			{
				char c = Line[iStart];
				if (flag)
				{
					if (c == '"')
					{
						if (iStart + 1 >= length || Line[iStart + 1] != '"')
						{
							flag = false;
						}
						else if (iStart + 2 < length && Line[iStart + 2] == '"')
						{
							flag = false;
							iStart += 2;
						}
						else
						{
							iStart++;
						}
					}
				}
				else if (c == '\n' || c == Separator)
				{
					LocalizationReader.AddCSVtoken(ref list, ref Line, iStart, ref num);
					if (c == '\n')
					{
						iStart++;
						break;
					}
				}
				else if (c == '"')
				{
					flag = true;
				}
				iStart++;
			}
			if (iStart > num)
			{
				LocalizationReader.AddCSVtoken(ref list, ref Line, iStart, ref num);
			}
			return list.ToArray();
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00038944 File Offset: 0x00036D44
		private static void AddCSVtoken(ref List<string> list, ref string Line, int iEnd, ref int iWordStart)
		{
			string text = Line.Substring(iWordStart, iEnd - iWordStart);
			iWordStart = iEnd + 1;
			text = text.Replace("\"\"", "\"");
			if (text.Length > 1 && text[0] == '"' && text[text.Length - 1] == '"')
			{
				text = text.Substring(1, text.Length - 2);
			}
			list.Add(text);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000389BC File Offset: 0x00036DBC
		public static List<string[]> ReadI2CSV(string Text)
		{
			string[] separator = new string[]
			{
				"[*]"
			};
			string[] separator2 = new string[]
			{
				"[ln]"
			};
			List<string[]> list = new List<string[]>();
			foreach (string text in Text.Split(separator2, StringSplitOptions.None))
			{
				list.Add(text.Split(separator, StringSplitOptions.None));
			}
			return list;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00038A28 File Offset: 0x00036E28
		public static void ValidateFullTerm(ref string Term)
		{
			Term = Term.Replace('\\', '/');
			int num = Term.IndexOf('/');
			if (num < 0)
			{
				return;
			}
			int startIndex;
			while ((startIndex = Term.LastIndexOf('/')) != num)
			{
				Term = Term.Remove(startIndex, 1);
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00038A78 File Offset: 0x00036E78
		public static string EncodeString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			return str.Replace("\r\n", "<\\n>").Replace("\r", "<\\n>").Replace("\n", "<\\n>");
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00038AC4 File Offset: 0x00036EC4
		public static string DecodeString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			return str.Replace("<\\n>", "\r\n");
		}
	}
}
