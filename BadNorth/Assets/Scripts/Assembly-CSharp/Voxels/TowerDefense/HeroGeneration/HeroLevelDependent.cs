using System;
using ControlledRandomness;
using UnityEngine;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x0200076A RID: 1898
	public class HeroLevelDependent : MonoBehaviour, MonoHero.IHeroSetup
	{
		// Token: 0x06003151 RID: 12625 RVA: 0x000CC0F6 File Offset: 0x000CA4F6
		[ContextMenu("SetLevelToSiblingIndex")]
		private void SetLevelToSiblingIndex()
		{
			this.maxLevel = base.transform.GetSiblingIndex();
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000CC10C File Offset: 0x000CA50C
		private void SetLevel(int squadLevel)
		{
			int num = Mathf.Max(0, squadLevel - 1);
			if (this && base.gameObject)
			{
				base.gameObject.SetActive(num >= this.minLevel && num <= this.maxLevel);
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000CC164 File Offset: 0x000CA564
		bool MonoHero.IHeroSetup.HeroSetup(MonoHero monoHero, PropertyBank propertyBank)
		{
			this.SetLevel(monoHero.heroDef.squadLevel);
			monoHero.onLevel = (Action<int>)Delegate.Combine(monoHero.onLevel, new Action<int>(this.SetLevel));
			return true;
		}

		// Token: 0x04002117 RID: 8471
		[SerializeField]
		public int minLevel;

		// Token: 0x04002118 RID: 8472
		[SerializeField]
		public int maxLevel = 3;
	}
}
