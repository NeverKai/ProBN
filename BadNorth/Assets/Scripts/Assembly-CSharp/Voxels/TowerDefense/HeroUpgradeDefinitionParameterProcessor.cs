using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ReflexCLI.Parameters;
using Voxels.TowerDefense.Reflex;

namespace Voxels.TowerDefense
{
	// Token: 0x02000598 RID: 1432
	[ParameterProcessor(typeof(HeroUpgradeDefinition))]
	public class HeroUpgradeDefinitionParameterProcessor : ParameterProcessor
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x00075B34 File Offset: 0x00073F34
		public override List<Suggestion> GetSuggestions(Type type, string subStr, object[] attributes, int maxResults)
		{
			List<HeroUpgradeDefinition> list = ResourceList<HeroUpgradeDefinition>.list;
			List<Suggestion> list2 = new List<Suggestion>();
			List<HeroUpgradeTypeEnum> list3 = new List<HeroUpgradeTypeEnum>();
			if (attributes != null)
			{
				int i = 0;
				int num = attributes.Length;
				while (i < num)
				{
					UpgradeTypeAttribute upgradeTypeAttribute = attributes[i] as UpgradeTypeAttribute;
					if (upgradeTypeAttribute != null)
					{
						list3.Add(upgradeTypeAttribute.type);
					}
					i++;
				}
			}
			foreach (HeroUpgradeDefinition heroUpgradeDefinition in list)
			{
				if (list3.Count == 0 || list3.Contains(heroUpgradeDefinition.typeEnum))
				{
					string displayName = HeroUpgradeDefinitionParameterProcessor.GetDisplayName(heroUpgradeDefinition);
					if (ParameterProcessor.StringStartsWith(displayName, subStr))
					{
						list2.Add(displayName);
					}
				}
			}
			list2.Sort((Suggestion a, Suggestion b) => a.Display.CompareTo(b.Display));
			return list2;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00075C40 File Offset: 0x00074040
		public override object ConvertString(Type type, string inString)
		{
			List<HeroUpgradeDefinition> list = ResourceList<HeroUpgradeDefinition>.list;
			foreach (HeroUpgradeDefinition heroUpgradeDefinition in list)
			{
				if (inString == HeroUpgradeDefinitionParameterProcessor.GetDisplayName(heroUpgradeDefinition))
				{
					return heroUpgradeDefinition;
				}
			}
			return null;
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00075CB4 File Offset: 0x000740B4
		public static string GetDisplayName(HeroUpgradeDefinition upgrade)
		{
			return (!string.IsNullOrEmpty(upgrade.dbgName)) ? HeroUpgradeDefinitionParameterProcessor.charFilter.Replace(upgrade.dbgName, string.Empty) : upgrade.uniqueId;
		}

		// Token: 0x040017BB RID: 6075
		private static Regex charFilter = new Regex("[*'\"\\-,.&#^@\\s\\(\\)]");
	}
}
