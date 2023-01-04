using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x0200064D RID: 1613
	[RequireComponent(typeof(ModuleSet))]
	public class ProfileLimits : SetRule, IAllowPlacement
	{
		// Token: 0x060028FA RID: 10490 RVA: 0x0008C958 File Offset: 0x0008AD58
		public override void GetRules(MultiWave multiwave, List<Wrapper> wrappers)
		{
			foreach (Wrapper wrapper in wrappers)
			{
				wrapper.allowPlacement.Add(this);
			}
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0008C9B4 File Offset: 0x0008ADB4
		bool IAllowPlacement.AllowPlacement(Vector3 offset, Placement placement, MultiWave multiWave)
		{
			Vector2 a = new Vector2(offset.x / (float)(multiWave.size.x - 1), offset.z / (float)(multiWave.size.z - 1));
			a = ExtraMath.Abs(a * 2f - Vector2.one);
			float x = Mathf.Max(a.x, a.y);
			float y = offset.y / (float)multiWave.size.y;
			Vector2 vector = new Vector2(x, y);
			return vector.sqrMagnitude <= 1f;
		}
	}
}
