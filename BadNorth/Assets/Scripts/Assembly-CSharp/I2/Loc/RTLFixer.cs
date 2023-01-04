using System;

namespace I2.Loc
{
	// Token: 0x02000415 RID: 1045
	public class RTLFixer
	{
		// Token: 0x06001832 RID: 6194 RVA: 0x0003D8ED File Offset: 0x0003BCED
		public static string Fix(string str)
		{
			return RTLFixer.Fix(str, false, true);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0003D8F8 File Offset: 0x0003BCF8
		public static string Fix(string str, bool rtl)
		{
			if (rtl)
			{
				return RTLFixer.Fix(str);
			}
			string[] array = str.Split(new char[]
			{
				' '
			});
			string text = string.Empty;
			string text2 = string.Empty;
			foreach (string text3 in array)
			{
				if (char.IsLower(text3.ToLower()[text3.Length / 2]))
				{
					text = text + RTLFixer.Fix(text2) + text3 + " ";
					text2 = string.Empty;
				}
				else
				{
					text2 = text2 + text3 + " ";
				}
			}
			if (text2 != string.Empty)
			{
				text += RTLFixer.Fix(text2);
			}
			return text;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0003D9B8 File Offset: 0x0003BDB8
		public static string Fix(string str, bool showTashkeel, bool useHinduNumbers)
		{
			string text = HindiFixer.Fix(str);
			if (text != str)
			{
				return text;
			}
			RTLFixerTool.showTashkeel = showTashkeel;
			RTLFixerTool.useHinduNumbers = useHinduNumbers;
			if (str.Contains("\n"))
			{
				str = str.Replace("\n", Environment.NewLine);
			}
			if (!str.Contains(Environment.NewLine))
			{
				return RTLFixerTool.FixLine(str);
			}
			string[] separator = new string[]
			{
				Environment.NewLine
			};
			string[] array = str.Split(separator, StringSplitOptions.None);
			if (array.Length == 0)
			{
				return RTLFixerTool.FixLine(str);
			}
			if (array.Length == 1)
			{
				return RTLFixerTool.FixLine(str);
			}
			string text2 = RTLFixerTool.FixLine(array[0]);
			int i = 1;
			if (array.Length > 1)
			{
				while (i < array.Length)
				{
					text2 = text2 + Environment.NewLine + RTLFixerTool.FixLine(array[i]);
					i++;
				}
			}
			return text2;
		}
	}
}
