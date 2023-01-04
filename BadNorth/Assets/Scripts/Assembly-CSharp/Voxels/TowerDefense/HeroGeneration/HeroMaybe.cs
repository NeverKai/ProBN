using System;
using System.Globalization;
using System.Linq;
using ControlledRandomness;
using Spriteshop;
using UnityEngine;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x0200076C RID: 1900
	public class HeroMaybe : SeededComponent, MonoHero.IHeroSetup
	{
		// Token: 0x06003157 RID: 12631 RVA: 0x000CC20C File Offset: 0x000CA60C
		bool MonoHero.IHeroSetup.HeroSetup(MonoHero monoHero, PropertyBank propertyBank)
		{
			base.Seed(monoHero.seed);
			GenOption component = base.gameObject.GetComponent<GenOption>();
			bool flag;
			if (component && !propertyBank.Allowed(component))
			{
				flag = false;
			}
			else
			{
				string[] splitName = base.GetComponent<PsdGroup>().splitName;
				string text = splitName.FirstOrDefault((string x) => x[0] == '#');
				if (text == null)
				{
					text = base.name;
				}
				string text2 = splitName.FirstOrDefault((string x) => x[0] == '?');
				float prob;
				if (text2 == null)
				{
					prob = 0.5f;
				}
				else
				{
					if (text2.Length == 1)
					{
						Debug.LogError("String is too short, " + base.name + ", " + text2);
					}
					prob = float.Parse(text2.Substring(1), CultureInfo.InvariantCulture);
				}
				flag = propertyBank.PickBool(text, () => UnityEngine.Random.value < prob);
			}
			if (!flag)
			{
				base.gameObject.transform.SetParent(null);
				UnityEngine.Object.Destroy(base.gameObject);
				return false;
			}
			return true;
		}

		// Token: 0x0400211A RID: 8474
		[SerializeField]
		[Range(0f, 1f)]
		private float probability = 0.5f;
	}
}
