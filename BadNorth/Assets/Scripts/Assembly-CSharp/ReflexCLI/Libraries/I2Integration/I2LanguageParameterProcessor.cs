using System;
using System.Collections.Generic;
using I2.Loc;
using ReflexCLI.Parameters;

namespace ReflexCLI.Libraries.I2Integration
{
	// Token: 0x02000464 RID: 1124
	[ParameterProcessor(typeof(I2Language))]
	internal class I2LanguageParameterProcessor : ParameterProcessor
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x000436CC File Offset: 0x00041ACC
		public override object ConvertString(Type type, string inString)
		{
			return new I2Language(inString);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000436DC File Offset: 0x00041ADC
		public override List<Suggestion> GetSuggestions(Type type, string subStr, object[] attributes, int maxResults)
		{
			List<string> allLanguages = LocalizationManager.GetAllLanguages(true);
			List<Suggestion> list = new List<Suggestion>();
			int num = 0;
			int count = allLanguages.Count;
			while (num < count && list.Count < maxResults)
			{
				ParameterProcessor.AddHierarchically(list, allLanguages[num], subStr, '/');
				num++;
			}
			return list;
		}
	}
}
