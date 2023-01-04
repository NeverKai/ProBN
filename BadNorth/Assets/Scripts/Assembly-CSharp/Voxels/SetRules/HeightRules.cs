using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x02000645 RID: 1605
	[RequireComponent(typeof(ModuleSet))]
	public class HeightRules : SetRule, IAllowPlacement
	{
		// Token: 0x060028E9 RID: 10473 RVA: 0x0008C0F0 File Offset: 0x0008A4F0
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.allowPlacement.Add(this);
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0008C14C File Offset: 0x0008A54C
		bool IAllowPlacement.AllowPlacement(Vector3 offset, Placement placement, MultiWave multiWave)
		{
			float num = (float)multiWave.size.y * this.multiplyMin + (float)this.addMin;
			float num2 = (float)multiWave.size.y * this.multiplyMax + (float)this.addMax;
			return offset.y >= num && offset.y <= num2;
		}

		// Token: 0x04001A96 RID: 6806
		[Header("Min")]
		public int addMin;

		// Token: 0x04001A97 RID: 6807
		[Range(0f, 1f)]
		public float multiplyMin;

		// Token: 0x04001A98 RID: 6808
		[Header("Max")]
		public int addMax;

		// Token: 0x04001A99 RID: 6809
		[Range(0f, 1f)]
		public float multiplyMax = 1f;
	}
}
