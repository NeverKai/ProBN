using System;
using System.Collections.Generic;
using CS.Lights;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200055B RID: 1371
	public class SquadBannerManager : MonoBehaviour, IslandUIManager.IAwake, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x060023B0 RID: 9136 RVA: 0x0006EAD4 File Offset: 0x0006CED4
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.islandUIManager = manager;
			IslandGameplayManager gameplayManager = manager.gameplayManager;
			gameplayManager.squadSpawner.onSquadSpawned += this.OnSquadSpawned;
			gameplayManager.squadSelector.onSquadSelectionChanged += this.OnSquadSelectionChanged;
			this.bannerPool.Init(this.squadBannerReference, null);
			this.bannerPool.ExpandTo(6);
			this.squadLight = base.GetComponent<SquadLight>();
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x0006EB48 File Offset: 0x0006CF48
		private void OnSquadSelectionChanged(EnglishSquad squad)
		{
			if (this.banner)
			{
				this.banner.Close();
				this.squadLight.SetActive(false);
			}
			this.banner = ((!squad) ? null : this.banners[squad]);
			if (this.banner)
			{
				this.banner.Open();
				this.squadLight.SetActive(true);
				this.squadLight.SetSquadColour(squad.hero.color);
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x0006EBDC File Offset: 0x0006CFDC
		private void OnSquadSpawned(EnglishSquad squad)
		{
			SquadSelectedBanner instance = this.bannerPool.GetInstance();
			instance.Setup(this.islandUIManager, squad);
			this.banners.Add(squad, instance);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0006EC0F File Offset: 0x0006D00F
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.bannerPool.ReturnAll();
			this.banners.Clear();
		}

		// Token: 0x0400164B RID: 5707
		[Header("SceneReferences")]
		[SerializeField]
		private SquadSelectedBanner squadBannerReference;

		// Token: 0x0400164C RID: 5708
		private IslandUIManager islandUIManager;

		// Token: 0x0400164D RID: 5709
		private Dictionary<EnglishSquad, SquadSelectedBanner> banners = new Dictionary<EnglishSquad, SquadSelectedBanner>();

		// Token: 0x0400164E RID: 5710
		private SquadSelectedBanner banner;

		// Token: 0x0400164F RID: 5711
		private LocalPool<SquadSelectedBanner> bannerPool = new LocalPool<SquadSelectedBanner>();

		// Token: 0x04001650 RID: 5712
		public SquadLight squadLight;
	}
}
