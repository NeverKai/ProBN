using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000554 RID: 1364
	public class IslandUIBase : MonoBehaviour, IslandUIManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x0006DB9F File Offset: 0x0006BF9F
		public Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0006DBAC File Offset: 0x0006BFAC
		public virtual void OnAwake(IslandUIManager manager)
		{
			this.uiManager = manager;
			this.gameplayManager = manager.gameplayManager;
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0006DBC1 File Offset: 0x0006BFC1
		public virtual void OnSetup(Island island)
		{
			this._island.Target = island;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0006DBCF File Offset: 0x0006BFCF
		public virtual void OnWipe(Island island)
		{
			this._island.Target = null;
		}

		// Token: 0x04001619 RID: 5657
		protected IslandUIManager uiManager;

		// Token: 0x0400161A RID: 5658
		protected IslandGameplayManager gameplayManager;

		// Token: 0x0400161B RID: 5659
		private WeakReference<Island> _island = new WeakReference<Island>(null);
	}
}
