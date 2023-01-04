using System;

namespace Voxels.TowerDefense
{
	// Token: 0x0200069A RID: 1690
	public abstract class SwordsmanComponent : AgentComponent
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x0009F634 File Offset: 0x0009DA34
		private Swordsman swordsman
		{
			get
			{
				if (!this._swordsman)
				{
					this._swordsman = base.GetComponent<Swordsman>();
				}
				return this._swordsman;
			}
		}

		// Token: 0x04001C67 RID: 7271
		private Swordsman _swordsman;
	}
}
