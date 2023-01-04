using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007DF RID: 2015
	public class SpriteStamperRoot : MonoBehaviour, IIslandWipe
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x000E20BC File Offset: 0x000E04BC
		public void Add(SpriteStampDef stamp)
		{
			if (!this.trunk)
			{
				this.trunk = SpriteStamperPool.instance.GetSpriteStamperTrunk(this.maxCount, this.loose);
				this.trunk.transform.SetParent(base.transform, false);
			}
			this.trunk.Add(stamp);
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000E2118 File Offset: 0x000E0518
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			this.ReturnTrunk();
			yield return new GenInfo("Shedding stuck arrows", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000E2133 File Offset: 0x000E0533
		public void ReturnTrunk()
		{
			if (this.trunk)
			{
				this.trunk.ReturnToPool();
				this.trunk = null;
			}
		}

		// Token: 0x040023A3 RID: 9123
		private Dictionary<int, SpriteStamper> dict = new Dictionary<int, SpriteStamper>();

		// Token: 0x040023A4 RID: 9124
		private SpriteStamperTrunk trunk;

		// Token: 0x040023A5 RID: 9125
		public int maxCount = 128;

		// Token: 0x040023A6 RID: 9126
		public bool loose = true;
	}
}
