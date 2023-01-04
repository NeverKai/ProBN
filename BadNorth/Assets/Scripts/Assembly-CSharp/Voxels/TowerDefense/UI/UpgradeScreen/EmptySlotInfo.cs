using System;
using I2.Loc;
using UnityEngine;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000926 RID: 2342
	public class EmptySlotInfo : SelectableInfo
	{
		// Token: 0x06003EF8 RID: 16120 RVA: 0x0011C152 File Offset: 0x0011A552
		public EmptySlotInfo Setup(HeroUpgradeType def)
		{
			this.header.Term = def.nameTerm;
			this.body.Term = def.descriptionTerm;
			return this;
		}

		// Token: 0x04002BFF RID: 11263
		[SerializeField]
		private Localize header;

		// Token: 0x04002C00 RID: 11264
		[SerializeField]
		private Localize body;
	}
}
