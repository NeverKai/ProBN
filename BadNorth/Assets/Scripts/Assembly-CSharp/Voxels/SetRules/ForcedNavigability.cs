using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x02000644 RID: 1604
	[RequireComponent(typeof(ModuleSet))]
	public class ForcedNavigability : SetRule
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x0008C0CF File Offset: 0x0008A4CF
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0008C0D1 File Offset: 0x0008A4D1
		public override void OnPreProcess(Module module)
		{
			module.forcedNavigability = true;
		}
	}
}
