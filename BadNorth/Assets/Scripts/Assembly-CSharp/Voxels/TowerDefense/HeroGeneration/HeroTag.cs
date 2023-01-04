using System;
using ControlledRandomness;
using UnityEngine;
using UnityEngine.Serialization;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x0200076F RID: 1903
	public class HeroTag : MonoBehaviour, MonoHero.IHeroSetup
	{
		// Token: 0x06003162 RID: 12642 RVA: 0x000CC56F File Offset: 0x000CA96F
		bool MonoHero.IHeroSetup.HeroSetup(MonoHero monoHero, PropertyBank propertyBank)
		{
			monoHero.AddTaggedObject(this.key, base.gameObject);
			return true;
		}

		// Token: 0x04002120 RID: 8480
		[FormerlySerializedAs("tag")]
		public string key = string.Empty;
	}
}
