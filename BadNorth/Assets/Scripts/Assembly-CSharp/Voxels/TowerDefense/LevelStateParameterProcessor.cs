using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ReflexCLI.Parameters;

namespace Voxels.TowerDefense
{
	// Token: 0x0200059A RID: 1434
	[ParameterProcessor(typeof(LevelState))]
	internal class LevelStateParameterProcessor : ParameterProcessor
	{
		// Token: 0x06002559 RID: 9561 RVA: 0x00075D2C File Offset: 0x0007412C
		public override List<Suggestion> GetSuggestions(Type type, string subStr, object[] attributes, int maxResults)
		{
			if (!Profile.campaign)
			{
				return null;
			}
			List<Suggestion> list = new List<Suggestion>();
			foreach (LevelState levelState in Profile.campaign.levelStates)
			{
				string displayName = LevelStateParameterProcessor.GetDisplayName(levelState);
				if (ParameterProcessor.StringStartsWith(displayName, subStr))
				{
					list.Add(displayName);
				}
			}
			return list;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00075DBC File Offset: 0x000741BC
		public override object ConvertString(Type type, string inString)
		{
			if (!Profile.campaign)
			{
				return null;
			}
			foreach (LevelState levelState in Profile.campaign.levelStates)
			{
				if (ParameterProcessor.StringEquals(inString, LevelStateParameterProcessor.GetDisplayName(levelState)))
				{
					return levelState;
				}
			}
			return null;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00075E44 File Offset: 0x00074244
		public static string GetDisplayName(LevelState levelState)
		{
			return LevelStateParameterProcessor.charFilter.Replace(levelState.dbgName, string.Empty);
		}

		// Token: 0x040017BE RID: 6078
		private static Regex charFilter = new Regex("[*'\"\\-,.&#^@\\s\\(\\)]");
	}
}
