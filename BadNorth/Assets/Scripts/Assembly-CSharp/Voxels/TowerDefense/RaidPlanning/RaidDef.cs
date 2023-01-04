using System;
using System.Collections.Generic;
using System.Linq;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005AC RID: 1452
	[Serializable]
	public class RaidDef
	{
		// Token: 0x060025D6 RID: 9686 RVA: 0x0007796C File Offset: 0x00075D6C
		public int GetBounty()
		{
			return this.waves.Sum((WaveDef wave) => wave.GetBounty());
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00077996 File Offset: 0x00075D96
		public float GetDuration()
		{
			return this.waves.Sum((WaveDef x) => x.timePadding);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000779C0 File Offset: 0x00075DC0
		public bool IsValid()
		{
			for (int i = 0; i < this.waves.Count; i++)
			{
				if (!this.waves[i].IsValid())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001819 RID: 6169
		public List<WaveDef> waves = new List<WaveDef>();

		// Token: 0x0400181A RID: 6170
		public float maxTimeToCallNext = 15f;
	}
}
