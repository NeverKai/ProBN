using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200060A RID: 1546
	[RequireComponent(typeof(ModuleSet))]
	public class EdgeModules : ModuleProcessor
	{
		// Token: 0x060027D4 RID: 10196 RVA: 0x0008164D File Offset: 0x0007FA4D
		public bool Fits(int key, int direction)
		{
			return this.keys[direction] == key;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0008165C File Offset: 0x0007FA5C
		public override void PostProcessModules(Module[] modules)
		{
			ModuleSet component = base.GetComponent<ModuleSet>();
			Module module = component.modules[0];
			Claim claim = module.orientations[0].claims[0];
			this.keys = new int[6];
			for (int i = 0; i < 6; i++)
			{
				this.keys[i] = claim.keys[i];
			}
		}

		// Token: 0x04001980 RID: 6528
		public int[] keys;
	}
}
