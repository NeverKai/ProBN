using System;
using System.Collections.Generic;
using I2.Loc;
using ReflexCLI.Parameters;

namespace ReflexCLI.Libraries.I2Integration
{
	// Token: 0x02000467 RID: 1127
	[ParameterProcessor(typeof(I2Term))]
	internal class I2TermParameterProcessor : ParameterProcessor
	{
		// Token: 0x0600199D RID: 6557 RVA: 0x00043844 File Offset: 0x00041C44
		public override object ConvertString(Type type, string inString)
		{
			return new I2Term(inString);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00043854 File Offset: 0x00041C54
		public override List<Suggestion> GetSuggestions(Type type, string subStr, object[] attributes, int maxResults)
		{
			List<string> termsList = LocalizationManager.GetTermsList(null);
			List<Suggestion> list = new List<Suggestion>();
			int num = 0;
			int count = termsList.Count;
			while (num < count && list.Count < maxResults)
			{
				ParameterProcessor.AddHierarchically(list, termsList[num], subStr, '/');
				num++;
			}
			return list;
		}
	}
}
