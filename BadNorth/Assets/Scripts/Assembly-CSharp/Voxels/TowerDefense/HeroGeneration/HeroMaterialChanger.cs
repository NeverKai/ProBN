using System;
using ControlledRandomness;
using UnityEngine;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x0200076B RID: 1899
	public class HeroMaterialChanger : MonoBehaviour, MonoHero.IHeroSetup
	{
		// Token: 0x06003155 RID: 12629 RVA: 0x000CC1A4 File Offset: 0x000CA5A4
		bool MonoHero.IHeroSetup.HeroSetup(MonoHero monoHero, PropertyBank propertyBank)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				SpriteRenderer component = base.transform.GetChild(i).GetComponent<SpriteRenderer>();
				if (component)
				{
					component.sharedMaterial = this.material;
				}
			}
			return true;
		}

		// Token: 0x04002119 RID: 8473
		[SerializeField]
		private Material material;
	}
}
