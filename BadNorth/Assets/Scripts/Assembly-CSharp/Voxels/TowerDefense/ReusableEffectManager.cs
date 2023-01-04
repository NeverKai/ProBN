using System;
using System.Collections;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000583 RID: 1411
	public class ReusableEffectManager : MonoBehaviour, IslandGameplayManager.ISetupIslandCoroutine
	{
		// Token: 0x06002481 RID: 9345 RVA: 0x0007287C File Offset: 0x00070C7C
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			if (this.particleContainer == null)
			{
				this.particleContainer = base.gameObject.AddEmptyChild("Particles").transform;
				foreach (ReusableParticle particle in this.particles)
				{
					particle.GetOrCreate(this.particleContainer);
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x0400170A RID: 5898
		[SerializeField]
		private ReusableParticle[] particles;

		// Token: 0x0400170B RID: 5899
		private Transform particleContainer;

		// Token: 0x0400170C RID: 5900
		private Transform windParticleContainer;

		// Token: 0x0400170D RID: 5901
		private Transform animContainer;
	}
}
