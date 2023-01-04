using System;
using System.Collections.Generic;
using System.Linq;
using ControlledRandomness;
using Spriteshop;
using UnityEngine;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x0200076D RID: 1901
	public class HeroPick : SeededComponent, MonoHero.IHeroSetup
	{
		// Token: 0x0600315B RID: 12635 RVA: 0x000CC38C File Offset: 0x000CA78C
		bool MonoHero.IHeroSetup.HeroSetup(MonoHero monoHero, PropertyBank propertyBank)
		{
			base.Seed(monoHero.seed);
			string text = base.name.Split(new char[]
			{
				' '
			}).FirstOrDefault((string x) => x[0] == '#');
			if (text == null)
			{
				text = base.name;
			}
			else
			{
				text = text.Substring(1);
			}
			List<GenOption> list = new List<GenOption>();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				PsdGroup component = child.GetComponent<PsdGroup>();
				if (component)
				{
					GenOption orAddComponent = child.gameObject.GetOrAddComponent<GenOption>();
					string text2 = component.splitName.FirstOrDefault((string x) => x[0] == '#');
					if (text2 == null)
					{
						text2 = base.name;
					}
					else
					{
						text2 = text2.Substring(1);
					}
					orAddComponent.tags.Add(new Tag(text, text2));
					list.Add(orAddComponent);
				}
			}
			GenOption genOption = propertyBank.Pick<GenOption>(list, UnityEngine.Random.value);
			if (!genOption)
			{
				if (list.Count == 0)
				{
					return true;
				}
				genOption = list.FirstOrDefault<GenOption>();
			}
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j] != genOption)
				{
					list[j].transform.SetParent(null);
					UnityEngine.Object.Destroy(list[j].gameObject);
				}
			}
			return true;
		}
	}
}
