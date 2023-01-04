using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000766 RID: 1894
	public class GameMaster : Singleton<GameMaster>
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000CBF80 File Offset: 0x000CA380
		// (set) Token: 0x06003147 RID: 12615 RVA: 0x000CBF87 File Offset: 0x000CA387
		public static bool isApplicationQuitting { get; private set; }

		// Token: 0x06003148 RID: 12616 RVA: 0x000CBF8F File Offset: 0x000CA38F
		private void Start()
		{
			ScenePreloader.Start();
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000CBF96 File Offset: 0x000CA396
		private void OnApplicationQuit()
		{
			GameMaster.isApplicationQuitting = true;
		}
	}
}
