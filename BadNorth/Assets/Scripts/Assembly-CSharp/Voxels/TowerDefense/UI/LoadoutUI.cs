using System;
using System.Collections.Generic;
using I2.Loc;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008DC RID: 2268
	public class LoadoutUI : UIMenu, IslandUIManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland, IslandGameplayManager.ILeaveIsland
	{
		// Token: 0x06003C04 RID: 15364 RVA: 0x0010AFB4 File Offset: 0x001093B4
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.manager = manager;
			this.heroBanners = base.GetComponentsInChildren<LoadoutUIListBanner>(true);
			this.getNextDeployableHero = new Func<HeroDefinition, int, HeroDefinition>(this.GetNextDeployableHero);
			this.handleListHeroClicked = new Action<LoadoutUIListBanner>(this.HandleListHeroClicked);
			this.handleListHeroClose = new Action<LoadoutUIListBanner>(this.HandleListHeroClose);
			this.buttonContainerVisibility = this.buttonContainer.GetComponent<IUIVisibility>();
			this.screenVisibility = base.GetComponent<IUIVisibility>();
			this.screenVisibility.SetVisible(false, true);
			base.gameObject.SetActive(false);
			this.loadoutState.OnChange += this.UpdateState;
			this.pauseState.OnChange += this.UpdateVisibility;
			foreach (LoadoutUIListBanner loadoutUIListBanner in this.heroBanners)
			{
				loadoutUIListBanner.onHeroChanged += this.OnHeroChanged;
			}
			this.maxSlots = this.heroBanners.Length;
			Platform.onPlatformUpdated += this.UpdateButtonVisibility;
			InputHelpers.onControllerTypeChanged += delegate(ControllerType x)
			{
				this.UpdateButtonVisibility();
			};
			this.deployedHeroes = new List<int>(this.maxSlots);
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0010B0E1 File Offset: 0x001094E1
		private void OnHeroChanged()
		{
			this.deployButtonClickable.disabled = !this.CanDeploy();
			base.transform.ForceChildLayoutUpdates(false);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x0010B103 File Offset: 0x00109503
		private void UpdateState(bool active)
		{
			if (active)
			{
				this.OpenMenu();
			}
			else
			{
				this.CloseMenu();
			}
			this.UpdateVisibility(active);
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x0010B123 File Offset: 0x00109523
		private void UpdateVisibility(bool b)
		{
			this.screenVisibility.SetVisible(this.loadoutState.active && !this.pauseState.active, false);
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x0010B154 File Offset: 0x00109554
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.island.Target = island;
			if (island.levelNode.wantsTutorial)
			{
				return;
			}
			int i = 0;
			int num = this.maxSlots;
			HeroDefinition heroDefinition = island.levelNode.heroDefinition;
			if (heroDefinition && heroDefinition.recruitable)
			{
				num--;
				LoadoutUIListBanner loadoutUIListBanner = this.heroBanners[num];
				loadoutUIListBanner.Setup(heroDefinition, null, null, null);
				this.localCommanderTitle.gameObject.SetActive(true);
			}
			else
			{
				this.localCommanderTitle.gameObject.SetActive(false);
			}
			foreach (int idx in this.deployedHeroes)
			{
				this.AddToOrderedHeroes(idx);
			}
			foreach (int idx2 in Profile.campaign.oldDeployOrder)
			{
				this.AddToOrderedHeroes(idx2);
			}
			foreach (HeroDefinition hero in Profile.campaign.heroes)
			{
				this.AddToOrderedHeroes(hero);
			}
			foreach (HeroDefinition heroDefinition2 in Profile.campaign.heroes)
			{
				if (heroDefinition2.available && !heroDefinition2.availableThisTurn && !this.orderedHeroes.Contains(heroDefinition2))
				{
					this.orderedHeroes.Add(heroDefinition2);
				}
			}
			foreach (HeroDefinition heroDefinition3 in this.orderedHeroes)
			{
				if (heroDefinition3 == null || heroDefinition3.availableThisTurn)
				{
					LoadoutUIListBanner loadoutUIListBanner2 = this.heroBanners[i];
					loadoutUIListBanner2.Setup(heroDefinition3, this.handleListHeroClicked, this.getNextDeployableHero, this.handleListHeroClose);
					i++;
					if (i == num)
					{
						break;
					}
				}
			}
			while (i < num)
			{
				LoadoutUIListBanner loadoutUIListBanner3 = this.heroBanners[i];
				loadoutUIListBanner3.Setup(null, this.handleListHeroClicked, this.getNextDeployableHero, this.handleListHeroClose);
				i++;
			}
			while (this.orderedHeroes.Remove(null))
			{
			}
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0010B438 File Offset: 0x00109838
		private void AddToOrderedHeroes(int idx)
		{
			this.AddToOrderedHeroes((idx >= 0) ? Profile.campaign.heroes[idx] : null);
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x0010B45D File Offset: 0x0010985D
		private void AddToOrderedHeroes(HeroDefinition hero)
		{
			if (!hero || (hero.availableThisTurn && !this.orderedHeroes.Contains(hero)))
			{
				this.orderedHeroes.Add(hero);
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x0010B494 File Offset: 0x00109894
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.orderedHeroes.Clear();
			foreach (LoadoutUIListBanner loadoutUIListBanner in this.heroBanners)
			{
				loadoutUIListBanner.Setup(null, null, null, null);
			}
			this.screenVisibility.SetVisible(false, true);
			this.island.Target = null;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x0010B4EE File Offset: 0x001098EE
		void IslandGameplayManager.ILeaveIsland.OnLeave(Island island)
		{
			this.orderedHeroes.Clear();
			this.deployedHeroes.Clear();
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x0010B508 File Offset: 0x00109908
		private void GetAvailableHeroes(ref List<HeroDefinition> availableHeroes, bool includeTired, HeroDefinition start = null)
		{
			availableHeroes.Clear();
			foreach (HeroDefinition heroDefinition in this.orderedHeroes)
			{
				if ((!includeTired) ? heroDefinition.availableThisTurn : heroDefinition.available)
				{
					availableHeroes.Add(heroDefinition);
				}
			}
			foreach (LoadoutUIListBanner loadoutUIListBanner in this.heroBanners)
			{
				if (loadoutUIListBanner.heroDef && loadoutUIListBanner.heroDef != start)
				{
					availableHeroes.Remove(loadoutUIListBanner.heroDef);
				}
			}
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x0010B5D8 File Offset: 0x001099D8
		private HeroDefinition GetNextDeployableHero(HeroDefinition heroDef, int direction)
		{
			direction = ((direction <= 0) ? -1 : 1);
			this.GetAvailableHeroes(ref this.heroListCache, false, heroDef);
			this.heroListCache.Add(null);
			int count = this.heroListCache.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.heroListCache[i] == heroDef)
				{
					i = (i + this.heroListCache.Count + direction) % count;
					return this.heroListCache[i];
				}
			}
			return heroDef;
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x0010B660 File Offset: 0x00109A60
		public void HandleListHeroClicked(LoadoutUIListBanner loadoutListBanner)
		{
			this.GetAvailableHeroes(ref this.heroListCache, true, null);
			if (loadoutListBanner.heroDef != null || this.heroListCache.Count > 0)
			{
				base.transform.ForceChildLayoutUpdates(false);
				this.replacePopup.OpenMenu(this.listTransform, loadoutListBanner, this.heroListCache);
				if (!InputHelpers.ControllerTypeIsNavigable())
				{
					FabricWrapper.PostEvent((!loadoutListBanner.heroDef) ? FabricID.uiButtonClick : loadoutListBanner.heroDef.voice.portraitSelectAudio);
				}
				else
				{
					FabricWrapper.PostEvent(FabricID.uiButtonClick);
				}
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
			this.heroListCache.Clear();
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x0010B720 File Offset: 0x00109B20
		public void HandleListHeroClose(LoadoutUIListBanner loadoutListBanner)
		{
			loadoutListBanner.Setup(null, this.handleListHeroClicked, this.getNextDeployableHero, this.handleListHeroClose);
			if (this.replacePopup.isOpen)
			{
				this.replacePopup.CloseMenu();
			}
			FabricWrapper.PostEvent(this.clearAudio);
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x0010B76D File Offset: 0x00109B6D
		public override void OpenMenu()
		{
			base.OpenMenu();
			this.buttonContainerVisibility.SetVisible(true, true);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x0010B782 File Offset: 0x00109B82
		public override void CloseMenu()
		{
			if (this.replacePopup.isOpen)
			{
				this.replacePopup.CloseMenu();
			}
			base.CloseMenu();
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x0010B7A5 File Offset: 0x00109BA5
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.manager.islandViewfinder.Push(this.cameraTarget, null);
			this.UpdateButtonVisibility();
			this.deployButtonClickable.disabled = !this.CanDeploy();
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x0010B7DE File Offset: 0x00109BDE
		protected override void OnLostFocus()
		{
			base.OnLostFocus();
			this.UpdateButtonVisibility();
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x0010B7EC File Offset: 0x00109BEC
		private void UpdateButtonVisibility()
		{
			bool flag = Profile.userSettings.cursorBehaviour == UserSettings.CursorBehaviour.Touch || InputHelpers.ControllerTypeIs(ControllerType.Joystick);
			bool visible = base.isFocussed || !flag;
			this.buttonContainerVisibility.SetVisible(visible, false);
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x0010B834 File Offset: 0x00109C34
		public void HandleClearHeroButton()
		{
			if (InputHelpers.ControllerTypeIs(ControllerType.Joystick))
			{
				foreach (LoadoutUIListBanner loadoutUIListBanner in this.heroBanners)
				{
					UINavigable component = loadoutUIListBanner.clickable.GetComponent<UINavigable>();
					if (component.hasFocus)
					{
						HeroDefinition heroDef = loadoutUIListBanner.heroDef;
						if (heroDef && heroDef != this.island.Target.levelNode.heroDefinition)
						{
							loadoutUIListBanner.Setup(null, this.handleListHeroClicked, this.getNextDeployableHero, this.handleListHeroClose);
							FabricWrapper.PostEvent(FabricID.uiButtonClick);
						}
						else
						{
							FabricWrapper.PostEvent(FabricID.uiError);
						}
						return;
					}
				}
			}
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x0010B8E7 File Offset: 0x00109CE7
		public void HandleExitToMapButton()
		{
			if (!InputHelpers.ControllerTypeIs(ControllerType.Keyboard))
			{
				FabricWrapper.PostEvent(FabricID.exitLevel);
				this.manager.gameplayManager.levelLeaver.ExitLevel();
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x0010B924 File Offset: 0x00109D24
		public void HandleDeployButton()
		{
			bool flag = this.CanDeploy(ref this.heroListCache);
			if (flag)
			{
				this.manager.islandViewfinder.Remove(this.cameraTarget, null);
				IslandGameplayManager gameplayManager = this.manager.gameplayManager;
				gameplayManager.squadSpawner.SpawnEnglishSquads(this.heroListCache);
				this.deployedHeroes.Clear();
				for (int i = 0; i < this.maxSlots; i++)
				{
					HeroDefinition heroDefinition = (!this.heroListCache.IsValidIndex(i)) ? null : this.heroListCache[i];
					HeroDefinition heroDefinition2 = this.island.Target.levelNode.heroDefinition;
					heroDefinition2 = ((!heroDefinition2 || !heroDefinition2.recruitable) ? null : heroDefinition2);
					if (heroDefinition == null || heroDefinition != heroDefinition2)
					{
						this.deployedHeroes.Add((!heroDefinition) ? -1 : heroDefinition.id);
					}
				}
			}
			this.heroListCache.Clear();
			FabricWrapper.PostEvent((!flag) ? FabricID.uiError : FabricID.uiButtonClick);
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0010BA50 File Offset: 0x00109E50
		private bool CanDeploy(ref List<HeroDefinition> heroes)
		{
			HeroDefinition heroDefinition = this.island.Target.levelNode.heroDefinition;
			HeroDefinition heroDefinition2 = (!heroDefinition || !heroDefinition.recruitable) ? null : heroDefinition;
			bool flag = false;
			int num = (!heroDefinition2) ? 1 : 2;
			heroes.Clear();
			foreach (LoadoutUIListBanner loadoutUIListBanner in this.heroBanners)
			{
				HeroDefinition heroDef = loadoutUIListBanner.heroDef;
				if (heroDef)
				{
					if (!heroDef.availableThisTurn && heroDef != heroDefinition2)
					{
						flag = true;
					}
					else
					{
						heroes.Add(loadoutUIListBanner.heroDef);
						if (heroDef == heroDefinition2)
						{
							heroDefinition2 = null;
						}
					}
				}
			}
			return heroes.Count >= num && heroDefinition2 == null && !flag;
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0010BB38 File Offset: 0x00109F38
		private bool CanDeploy()
		{
			bool result = this.CanDeploy(ref this.heroListCache);
			this.heroListCache.Clear();
			return result;
		}

		// Token: 0x040029C6 RID: 10694
		[Header("References")]
		[SerializeField]
		private State loadoutState;

		// Token: 0x040029C7 RID: 10695
		[SerializeField]
		private State pauseState;

		// Token: 0x040029C8 RID: 10696
		[SerializeField]
		private RectTransform listTransform;

		// Token: 0x040029C9 RID: 10697
		[SerializeField]
		private LoadoutReplacePopup replacePopup;

		// Token: 0x040029CA RID: 10698
		[SerializeField]
		private RectTransform cameraTarget;

		// Token: 0x040029CB RID: 10699
		[SerializeField]
		private Localize localCommanderTitle;

		// Token: 0x040029CC RID: 10700
		[SerializeField]
		private Transform buttonContainer;

		// Token: 0x040029CD RID: 10701
		private IUIVisibility buttonContainerVisibility;

		// Token: 0x040029CE RID: 10702
		[SerializeField]
		private UIClickable deployButtonClickable;

		// Token: 0x040029CF RID: 10703
		private IUIVisibility screenVisibility;

		// Token: 0x040029D0 RID: 10704
		private IslandUIManager manager;

		// Token: 0x040029D1 RID: 10705
		private LoadoutUIListBanner[] heroBanners;

		// Token: 0x040029D2 RID: 10706
		private List<HeroDefinition> heroListCache = new List<HeroDefinition>();

		// Token: 0x040029D3 RID: 10707
		private RTM.Utilities.WeakReference<Island> island = new RTM.Utilities.WeakReference<Island>(null);

		// Token: 0x040029D4 RID: 10708
		private LoadoutUIListBanner focusBanner;

		// Token: 0x040029D5 RID: 10709
		private List<HeroDefinition> orderedHeroes = new List<HeroDefinition>();

		// Token: 0x040029D6 RID: 10710
		private List<int> deployedHeroes;

		// Token: 0x040029D7 RID: 10711
		private int maxSlots;

		// Token: 0x040029D8 RID: 10712
		private FabricEventReference clearAudio = "UI/InGame/UnitDeselect";

		// Token: 0x040029D9 RID: 10713
		private Func<HeroDefinition, int, HeroDefinition> getNextDeployableHero;

		// Token: 0x040029DA RID: 10714
		private Action<LoadoutUIListBanner> handleListHeroClicked;

		// Token: 0x040029DB RID: 10715
		private Action<LoadoutUIListBanner> handleListHeroClose;
	}
}
