using System.Collections.Generic;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.UI.UpgradeScreen;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000934 RID: 2356
	public class SuperUpgradesCampaignProxy : MonoBehaviour, IGameSetup, CampaignManager.INewCampaign, CampaignManager.IExitCampaign, IUpgradesProxy
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x001201D8 File Offset: 0x0011E5D8
		// (set) Token: 0x06003F69 RID: 16233 RVA: 0x001201E0 File Offset: 0x0011E5E0
		public SuperUpgradeMenu menu { get; private set; }

		// Token: 0x06003F6A RID: 16234 RVA: 0x001201E9 File Offset: 0x0011E5E9
		void IGameSetup.OnGameAwake()
		{
			this.menu = SuperUpgradesCampaignProxy.FindMenu();
			if (this.campaignSave && this.menu)
			{
				this.menu.OnNewCampaign(this);
			}
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x00120224 File Offset: 0x0011E624
		private static SuperUpgradeMenu FindMenu()
		{
			Scene scene;
			if (ExtraSceneManager.GetSceneByName("Upgrades", out scene))
			{
				foreach (GameObject gameObject in scene.GetRootGameObjects())
				{
					SuperUpgradeMenu componentInChildren = gameObject.GetComponentInChildren<SuperUpgradeMenu>(true);
					if (componentInChildren)
					{
						return componentInChildren;
					}
				}
			}
			return null;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x0012027C File Offset: 0x0011E67C
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			this.campaignSave.Target = campaign.campaignSave;
			this.upgradesPurchased = false;
			if (!this.menu)
			{
				SuperUpgradesCampaignProxy.FindMenu();
			}
			this.menu.OnNewCampaign(this);
			this.upgradesDisplay.SetAvailable(this.menu.upgradesAvailable, true);
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x001202DA File Offset: 0x0011E6DA
		void CampaignManager.IExitCampaign.OnCampaignExit(CampaignManager manager, Campaign campaign)
		{
			this.campaignSave.Target = null;
			this.upgradesPurchased = false;
			this.menu.OnCampaignExit();
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x001202FA File Offset: 0x0011E6FA
		List<HeroDefinition> IUpgradesProxy.heroes
		{
			get
			{
				return this.campaignSave.Target.heroes;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x0012030C File Offset: 0x0011E70C
		List<SerializableHeroUpgrade> IUpgradesProxy.inventory
		{
			get
			{
				return this.campaignSave.Target.inventory;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x0012031E File Offset: 0x0011E71E
		// (set) Token: 0x06003F71 RID: 16241 RVA: 0x00120330 File Offset: 0x0011E730
		int IUpgradesProxy.coinBank
		{
			get
			{
				return this.campaignSave.Target.coinBank;
			}
			set
			{
				this.campaignSave.Target.coinBank = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06003F72 RID: 16242 RVA: 0x00120343 File Offset: 0x0011E743
		Transform IUpgradesProxy.campaignCoinTransform
		{
			get
			{
				return this.campaignMap.coinTransform;
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x00120350 File Offset: 0x0011E750
		void IUpgradesProxy.RemoveFromInventory(HeroUpgradeDefinition def)
		{
			List<SerializableHeroUpgrade> inventory = this.campaignSave.Target.inventory;
			foreach (SerializableHeroUpgrade serializableHeroUpgrade in inventory)
			{
				if (serializableHeroUpgrade.definition == def)
				{
					inventory.Remove(serializableHeroUpgrade);
					break;
				}
			}
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x001203D0 File Offset: 0x0011E7D0
		void IUpgradesProxy.OnUpgradePurchased()
		{
			this.upgradesPurchased = true;
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x001203D9 File Offset: 0x0011E7D9
		void IUpgradesProxy.OnMenuClosed()
		{
			if (this.upgradesPurchased)
			{
				Profile.SaveCampaign(false);
			}
			this.upgradesPurchased = false;
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x001203F3 File Offset: 0x0011E7F3
		void IUpgradesProxy.OnMenuOpened()
		{
			this.campaignMap.SetNextTurnVisible(false);
			TrialTurnsRemainDisplay.SetVisible(false, false);
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x00120408 File Offset: 0x0011E808
		GameOverReason IUpgradesProxy.gameOverReason
		{
			get
			{
				return (!this.campaignSave) ? GameOverReason.None : this.campaignSave.Target.gameOverReason;
			}
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x00120430 File Offset: 0x0011E830
		public void OpenMenu()
		{
			if (!this.menu.isOpen)
			{
				this.menu.OpenMenu();
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x0012044D File Offset: 0x0011E84D
		public void CloseMenu()
		{
			if (this.menu.isOpen)
			{
				this.menu.CloseMenu();
			}
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x0012046A File Offset: 0x0011E86A
		internal void HandleMapTab(int direction)
		{
			if (this.menu)
			{
				this.menu.HandleMapTab(direction);
			}
		}

		// Token: 0x04002C80 RID: 11392
		[SerializeField]
		public CampaignMapUI campaignMap;

		// Token: 0x04002C81 RID: 11393
		[SerializeField]
		private CampaignMapUpgradesDisplay upgradesDisplay;

		// Token: 0x04002C83 RID: 11395
		private WeakReference<CampaignSave> campaignSave = new WeakReference<CampaignSave>(null);

		// Token: 0x04002C84 RID: 11396
		private bool upgradesPurchased;
	}
}
