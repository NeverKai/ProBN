using System;
using System.Collections.Generic;
using System.Linq;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.UI.UpgradeScreen;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000935 RID: 2357
	public class SuperUpgradesTestbed : MonoBehaviour, IUpgradesProxy
	{
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x001204A6 File Offset: 0x0011E8A6
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x001204AE File Offset: 0x0011E8AE
		public SuperUpgradeMenu upgradeScreen { get; protected set; }

		// Token: 0x06003F7E RID: 16254 RVA: 0x001204B8 File Offset: 0x0011E8B8
		private void Start()
		{
			SuperUpgradesTestbed.nameTerms = HeroNameTerms.male;
			foreach (GameObject gameObject in ExtraSceneManager.GetSceneByName("Upgrades").GetRootGameObjects())
			{
				this.upgradeScreen = gameObject.GetComponentInChildren<SuperUpgradeMenu>(true);
				if (this.upgradeScreen)
				{
					break;
				}
			}
			for (int j = 0; j < 10; j++)
			{
				this._heroes.Add(this.MakeFakeHero(j));
			}
			HeroUpgradeDefinition[] array = (from x in ResourceList<HeroUpgradeDefinition>.list
			where x.typeEnum == HeroUpgradeTypeEnum.Item || x.typeEnum == HeroUpgradeTypeEnum.Consumable
			select x).ToArray<HeroUpgradeDefinition>();
			for (int k = 0; k < 5; k++)
			{
				this._inventory.Add(array[UnityEngine.Random.Range(0, array.Length - 1)]);
			}
			this.upgradeScreen.OnNewCampaign(this);
			UINavigable[] componentsInChildren = base.GetComponentsInChildren<UINavigable>(true);
			for (int l = 0; l < componentsInChildren.Length; l++)
			{
				UINavigable navigable = componentsInChildren[l];
				//SuperUpgradesTestbed $this = this;
				navigable.onConsumedNavigation += delegate(Vector2 v)
				{
					this.OnConsumedNavigation(navigable, v);
				};
			}
			foreach (UIClickable uiclickable in base.GetComponentsInChildren<UIClickable>(true))
			{
				uiclickable.onClick += this.OnClick;
			}
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x00120647 File Offset: 0x0011EA47
		private void OnClick()
		{
			if (this.upgradeScreen.isOpen)
			{
				this.upgradeScreen.CloseMenu();
			}
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x00120664 File Offset: 0x0011EA64
		private void OnConsumedNavigation(UINavigable navigable, Vector2 dir)
		{
			if (dir.y < Mathf.Abs(dir.x))
			{
				this.OpenHeroMenu();
			}
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x00120684 File Offset: 0x0011EA84
		public void OpenHeroMenu()
		{
			this.upgradeScreen.OpenMenu(this.upgradeScreen.GetComponentInChildren<PortraitToken>());
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0012069C File Offset: 0x0011EA9C
		private HeroDefinition MakeFakeHero(int id)
		{
			HeroDefinition heroDefinition = new HeroDefinition(id);
			heroDefinition.color = Color.HSVToRGB(UnityEngine.Random.value, 0.4f, 0.8f);
			heroDefinition.nameTerm = SuperUpgradesTestbed.nameTerms[UnityEngine.Random.Range(0, SuperUpgradesTestbed.nameTerms.Length - 1)];
			heroDefinition.recruited = true;
			heroDefinition.alive = true;
			heroDefinition.timesUsedThisTurn = ((UnityEngine.Random.value >= 0.3f) ? (byte)0 : (byte)1);
			heroDefinition.voice = ResourceList<HeroVoice>.list.GetRandomByProbability((HeroVoice x) => 1f, 0f);
			heroDefinition.alive = (UnityEngine.Random.value > 0.1f);
			return heroDefinition;
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x00120755 File Offset: 0x0011EB55
		List<HeroDefinition> IUpgradesProxy.heroes
		{
			get
			{
				return this._heroes;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x0012075D File Offset: 0x0011EB5D
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x00120765 File Offset: 0x0011EB65
		int IUpgradesProxy.coinBank { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x0012076E File Offset: 0x0011EB6E
		List<SerializableHeroUpgrade> IUpgradesProxy.inventory
		{
			get
			{
				return this._inventory;
			}
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x00120778 File Offset: 0x0011EB78
		void IUpgradesProxy.RemoveFromInventory(HeroUpgradeDefinition def)
		{
			foreach (SerializableHeroUpgrade serializableHeroUpgrade in this._inventory)
			{
				if (serializableHeroUpgrade.definition == def)
				{
					this._inventory.Remove(serializableHeroUpgrade);
					break;
				}
			}
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x001207F0 File Offset: 0x0011EBF0
		void IUpgradesProxy.OnUpgradePurchased()
		{
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x001207F2 File Offset: 0x0011EBF2
		void IUpgradesProxy.OnMenuClosed()
		{
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x001207F4 File Offset: 0x0011EBF4
		void IUpgradesProxy.OnMenuOpened()
		{
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x001207F6 File Offset: 0x0011EBF6
		Transform IUpgradesProxy.campaignCoinTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x001207FE File Offset: 0x0011EBFE
		GameOverReason IUpgradesProxy.gameOverReason
		{
			get
			{
				return GameOverReason.None;
			}
		}

		// Token: 0x04002C87 RID: 11399
		private static string[] nameTerms;

		// Token: 0x04002C88 RID: 11400
		private List<HeroDefinition> _heroes = new List<HeroDefinition>();

		// Token: 0x04002C89 RID: 11401
		private List<SerializableHeroUpgrade> _inventory = new List<SerializableHeroUpgrade>();
	}
}
