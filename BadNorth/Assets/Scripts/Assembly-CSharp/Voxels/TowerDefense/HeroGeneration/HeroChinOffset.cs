using System;
using ControlledRandomness;
using Spriteshop;
using UnityEngine;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x02000767 RID: 1895
	public class HeroChinOffset : MonoBehaviour, MonoHero.IHeroSetup
	{
		// Token: 0x0600314B RID: 12619 RVA: 0x000CBFB4 File Offset: 0x000CA3B4
		bool MonoHero.IHeroSetup.HeroSetup(MonoHero monoHero, PropertyBank propertyBank)
		{
			PsdGroup component = base.GetComponent<PsdGroup>();
			float y = this.standardHeight - component.outerRect.yMin;
			base.transform.parent.position += new Vector3(0f, y, 0f);
			return true;
		}

		// Token: 0x0400210E RID: 8462
		public float standardHeight = -0.92f;
	}
}
