using System;
using System.Collections.Generic;
using Fabric;
using ReflexCLI.Parameters;

namespace ReflexCLI.Libraries.FabricIntegration
{
	// Token: 0x02000461 RID: 1121
	[ParameterProcessor(typeof(FabricEvent))]
	public class FabricEventParameterProcessor : ParameterProcessor
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x00043545 File Offset: 0x00041945
		public override object ConvertString(Type type, string inString)
		{
			return new FabricEvent(inString);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00043554 File Offset: 0x00041954
		public override List<Suggestion> GetSuggestions(Type type, string subStr, object[] attributes, int maxResults)
		{
			List<string> eventList = EventManager.Instance._eventList;
			List<Suggestion> list = new List<Suggestion>();
			int num = 0;
			int count = eventList.Count;
			while (num < count && list.Count < maxResults)
			{
				ParameterProcessor.AddHierarchically(list, eventList[num], subStr, '/');
				num++;
			}
			return list;
		}
	}
}
