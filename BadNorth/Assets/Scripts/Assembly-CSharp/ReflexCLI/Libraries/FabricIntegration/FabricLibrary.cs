using System;
using System.Collections.Generic;
using System.Linq;
using Fabric;
using ReflexCLI.Attributes;

namespace ReflexCLI.Libraries.FabricIntegration
{
	// Token: 0x02000462 RID: 1122
	[ConsoleCommandClassCustomizer("Fabric")]
	public static class FabricLibrary
	{
		// Token: 0x06001988 RID: 6536 RVA: 0x000435AA File Offset: 0x000419AA
		[ConsoleCommand("")]
		private static void PostEvent(FabricEvent eventId)
		{
			if (FabricLibrary.EventExists(eventId))
			{
				FabricWrapper.PostEvent(eventId);
				return;
			}
			throw new Exception("event Id '" + eventId + "'does not exist");
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000435E3 File Offset: 0x000419E3
		[ConsoleCommand("")]
		private static bool EventExists(FabricEvent eventId)
		{
			return EventManager.Instance._eventList.Contains(eventId);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000435FC File Offset: 0x000419FC
		[ConsoleCommand("")]
		private static string ShowEventsLike(string subString)
		{
			string text = string.Empty;
			IEnumerable<string> enumerable = from t in EventManager.Instance._eventList
			where t.Contains(subString)
			select t;
			foreach (string str in enumerable)
			{
				text = text + str + "\n";
			}
			return text;
		}
	}
}
