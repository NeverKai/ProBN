using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x0200064E RID: 1614
	[RequireComponent(typeof(ModuleSet))]
	public class ScoreModifier : SetRule
	{
		// Token: 0x060028FD RID: 10493 RVA: 0x0008CA68 File Offset: 0x0008AE68
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.defaultScore *= this.score;
			}
		}

		// Token: 0x04001AAD RID: 6829
		public float score = 1f;
	}
}
