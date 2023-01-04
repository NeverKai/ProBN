using System;
using System.Collections.Generic;
using ReflexCLI.Parameters;

namespace Voxels.TowerDefense
{
	// Token: 0x02000597 RID: 1431
	[ParameterProcessor(typeof(HeroDefinition))]
	public class HeroDefParameterProcessor : ParameterProcessor
	{
		// Token: 0x0600254E RID: 9550 RVA: 0x00075A14 File Offset: 0x00073E14
		public override List<Suggestion> GetSuggestions(Type type, string subStr, object[] attributes, int maxResults)
		{
			if (!Profile.campaign)
			{
				return null;
			}
			List<Suggestion> list = new List<Suggestion>();
			foreach (HeroDefinition heroDefinition in Profile.campaign.heroes)
			{
				string dbgName = heroDefinition.dbgName;
				if (ParameterProcessor.StringStartsWith(dbgName, subStr))
				{
					list.Add(dbgName);
				}
			}
			return list;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00075AA4 File Offset: 0x00073EA4
		public override object ConvertString(Type type, string inString)
		{
			if (!Profile.campaign)
			{
				return null;
			}
			foreach (HeroDefinition heroDefinition in Profile.campaign.heroes)
			{
				if (ParameterProcessor.StringEquals(inString, heroDefinition.dbgName))
				{
					return heroDefinition;
				}
			}
			return null;
		}
	}
}
