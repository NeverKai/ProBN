using RTM.Pools;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200093B RID: 2363
	public class WorldSpaceCursorIconPool : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x06003FE5 RID: 16357 RVA: 0x001222C4 File Offset: 0x001206C4
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.icons = new LocalPool<WorldSpaceCursorIcon>(base.GetComponentsInChildren<WorldSpaceCursorIcon>(true), null);
			this.icons.ExpandTo(8);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x001222E8 File Offset: 0x001206E8
		public WorldSpaceCursorIcon GetInstance(EnglishSquad squad)
		{
			if (this.lastUsed && this.lastUsed.isJustDeactivated && squad && squad == this.lastOwner)
			{
				this.lastUsed.Reactivate();
			}
			else
			{
				this.lastUsed = this.icons.GetInstance();
				this.lastOwner.Target = squad;
			}
			return this.lastUsed;
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x00122369 File Offset: 0x00120769
		public void OnSetup(Island island)
		{
			this.icons.ReturnAll();
			this.lastUsed = null;
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x0012237D File Offset: 0x0012077D
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.icons.ReturnAll();
			this.lastUsed = null;
			this.lastOwner.Target = null;
		}

		// Token: 0x04002CD2 RID: 11474
		private LocalPool<WorldSpaceCursorIcon> icons;

		// Token: 0x04002CD3 RID: 11475
		private WorldSpaceCursorIcon lastUsed;

		// Token: 0x04002CD4 RID: 11476
		private WeakReference<EnglishSquad> lastOwner = new WeakReference<EnglishSquad>(null);
	}
}
