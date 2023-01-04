using System;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000824 RID: 2084
	internal interface IUIVisibility
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06003660 RID: 13920
		// (set) Token: 0x06003661 RID: 13921
		bool visible { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06003662 RID: 13922
		float alpha { get; }

		// Token: 0x06003663 RID: 13923
		void SetVisible(bool visible, bool snap = false);
	}
}
