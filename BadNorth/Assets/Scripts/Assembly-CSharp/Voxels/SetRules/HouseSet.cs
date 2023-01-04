using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense;

namespace Voxels.SetRules
{
	// Token: 0x02000617 RID: 1559
	[RequireComponent(typeof(ModuleSet))]
	public class HouseSet : SetRule
	{
		// Token: 0x06002809 RID: 10249 RVA: 0x00082DBE File Offset: 0x000811BE
		public override void OnPreProcess(Module module)
		{
			module.goldCount = module.GetComponentInChildren<House>(true).coinCount;
			module.house = true;
			module.forcedNavigability = true;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x00082DE0 File Offset: 0x000811E0
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			HouseSet.HouseCounter obj = new HouseSet.HouseCounter(this);
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.FillRules(obj);
			}
		}

		// Token: 0x040019B5 RID: 6581
		public float houseSetScore = 1f;

		// Token: 0x02000618 RID: 1560
		private class HouseCounter : IOnPlaced, IOnDominoCreated
		{
			// Token: 0x0600280B RID: 10251 RVA: 0x00082E40 File Offset: 0x00081240
			public HouseCounter(HouseSet houseSet)
			{
				this.houseSet = houseSet;
			}

			// Token: 0x0600280C RID: 10252 RVA: 0x00082E50 File Offset: 0x00081250
			bool IOnPlaced.OnPlaced(Domino domino, MultiWave multiWave)
			{
				Bounds bounds = domino.GetBounds();
				bounds.extents += new Vector3(4f, 0f, 4f);
				for (int i = 0; i < multiWave.allDominos.Count; i++)
				{
					Domino domino2 = multiWave.allDominos[i];
					if (domino2.placement.firstModule == domino.placement.firstModule && domino2.GetBounds().Intersects(bounds))
					{
						domino2.score *= 0.4f;
					}
				}
				return true;
			}

			// Token: 0x0600280D RID: 10253 RVA: 0x00082EFC File Offset: 0x000812FC
			void IOnDominoCreated.OnDominoAdded(Domino domino, MultiWave multiWave)
			{
				if (domino.offset.y == 0 && multiWave.size.y > 1)
				{
					domino.defaultScore *= 0.4f;
				}
				domino.defaultScore *= 1f + (float)domino.placement.claims.Count * 0.1f;
				domino.defaultScore *= 1f + (float)domino.offset.y * 0.1f;
			}

			// Token: 0x040019B6 RID: 6582
			private HouseSet houseSet;
		}
	}
}
