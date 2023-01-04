using System;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007E1 RID: 2017
	public class UpdatingBatchedSprite : BatchedSprite
	{
		// Token: 0x0600343E RID: 13374 RVA: 0x000E246E File Offset: 0x000E086E
		private void LateUpdate()
		{
			if (this.updateScale)
			{
				base.UpdateScale();
			}
			if (this.updateColor)
			{
				base.UpdateColor();
			}
		}

		// Token: 0x040023AA RID: 9130
		[SerializeField]
		private bool updateScale = true;

		// Token: 0x040023AB RID: 9131
		[SerializeField]
		private bool updateColor = true;
	}
}
