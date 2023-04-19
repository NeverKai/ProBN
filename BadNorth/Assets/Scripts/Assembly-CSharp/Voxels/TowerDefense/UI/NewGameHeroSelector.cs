using System;
using System.Collections.Generic;
using System.Diagnostics;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008F9 RID: 2297
	internal class NewGameHeroSelector : UIMenu
	{
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06003D0A RID: 15626 RVA: 0x00110ECC File Offset: 0x0010F2CC
		// (remove) Token: 0x06003D0B RID: 15627 RVA: 0x00110F04 File Offset: 0x0010F304
		
		public event Action<bool> onHoverChanged = delegate(bool A_0)
		{
		};

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x00110F3A File Offset: 0x0010F33A
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x00110F42 File Offset: 0x0010F342
		public bool pointerHover { get; private set; }

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06003D0E RID: 15630 RVA: 0x00110F4C File Offset: 0x0010F34C
		// (remove) Token: 0x06003D0F RID: 15631 RVA: 0x00110F84 File Offset: 0x0010F384
		
		public event Action onParentOpened = delegate()
		{
		};

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06003D10 RID: 15632 RVA: 0x00110FBC File Offset: 0x0010F3BC
		// (remove) Token: 0x06003D11 RID: 15633 RVA: 0x00110FF4 File Offset: 0x0010F3F4
		
		public event Action onParentClosed = delegate()
		{
		};

		// Token: 0x06003D12 RID: 15634 RVA: 0x0011102C File Offset: 0x0010F42C
		public void Init(NewGameOptionsPopup rootMenu, NewGameHeroSelector other, int idx, bool showDeluxe)
		{
			this.rootMenu = rootMenu;
			this.other = other;
			this.idx = idx;
			this.heroCustomizer.Init(false);
			this.itemCarousel.Initialize(string.Empty);
			this.traitCarousel.Initialize(string.Empty);
			foreach (NewGameTabAnimator newGameTabAnimator in base.GetComponentsInChildren<NewGameTabAnimator>(true))
			{
				newGameTabAnimator.Init(this);
			}
			foreach (UpgradeCarouselDashAnimator upgradeCarouselDashAnimator in base.GetComponentsInChildren<UpgradeCarouselDashAnimator>(true))
			{
				upgradeCarouselDashAnimator.Init();
			}
			this.itemCarousel.onSelectedUpgradeChanged += this.UpdateItem;
			this.traitCarousel.onSelectedUpgradeChanged += this.UpdateTrait;
			InputHelpers.onControllerTypeChanged += this.InputHelpers_onControllerTypeChanged;
			UIPointerReceiver component = base.GetComponent<UIPointerReceiver>();
			this.pointerHover = false;
			component.onStateChanged += delegate(UIPointerReceiver.State x)
			{
				bool pointerHover = this.pointerHover;
				this.pointerHover = (x != UIPointerReceiver.State.None);
				if (pointerHover != this.pointerHover)
				{
					this.onHoverChanged(this.pointerHover);
				}
			};
			if (showDeluxe)
			{
				this.deluxeDLCWidget.Initialize("SETTINGS/DLC/DELUXE", () => this.heroCustomizer.hasDeluxe, delegate(bool x)
				{
					this.heroCustomizer.hasDeluxe = x;
					return true;
				}).SetSuccessAudio(FabricID.settingChange);
				this.deluxeDLCWidget.ForceUpdate();
			}
			else
			{
				this.deluxeDLCWidget.gameObject.SetActive(false);
			}
			Func<UpgradeCarousel, HeroUpgradeDefinition, bool, bool> forceChange = delegate(UpgradeCarousel c, HeroUpgradeDefinition u, bool snap)
			{
				bool flag = c.SetValue(u, snap);
				if (flag)
				{
					c.Flash(0.15f);
				}
				return flag;
			};
			this.traitCarousel.forceOtherToChange = ((HeroUpgradeDefinition u, bool s) => forceChange(other.traitCarousel, u, s));
			this.itemCarousel.forceOtherToChange = ((HeroUpgradeDefinition u, bool s) => forceChange(other.itemCarousel, u, s));
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x001111F9 File Offset: 0x0010F5F9
		private void InputHelpers_onControllerTypeChanged(ControllerType controllerType)
		{
			if (base.isOpen && controllerType != ControllerType.Joystick)
			{
				this.CloseMenu();
			}
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x00111214 File Offset: 0x0010F614
		public void MaybeSetupHeroes(int seed, List<HeroUpgradeDefinition> traits, List<HeroUpgradeDefinition> items)
		{
			if (this.needsReset)
			{
				if (!this.monoHero)
				{
					this.monoHero = HeroGeneratorUI.instance.GetStartingMonoHero();
				}
				this.heroCustomizer.Setup(seed, this.idx, this.monoHero);
				this.other.heroCustomizer.SetOtherHero(this.heroCustomizer.heroDef);
				this.itemCarousel.Setup(items, this.needsReset);
				this.itemCarousel.SetUnavailableUpgrade(this.other.itemCarousel.selectedUpgrade);
				this.itemCarousel.SetRandom(true);
				this.traitCarousel.Setup(traits, this.needsReset);
				this.traitCarousel.SetUnavailableUpgrade(this.other.traitCarousel.selectedUpgrade);
				this.traitCarousel.SetRandom(true);
				this.heroCustomizer.hasDeluxe = this.rootMenu.hasDeluxe;
				this.deluxeDLCWidget.ForceUpdate();
				this.heroCustomizer.Generate(false);
				this.needsReset = false;
			}
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x00111328 File Offset: 0x0010F728
		public void OnParentMenuOpened(List<HeroUpgradeDefinition> traits, List<HeroUpgradeDefinition> items, bool hasDeluxeDLC)
		{
			this.itemCarousel.Setup(items, this.needsReset);
			this.itemCarousel.SetUnavailableUpgrade(this.other.itemCarousel.selectedUpgrade);
			this.traitCarousel.Setup(traits, this.needsReset);
			this.traitCarousel.SetUnavailableUpgrade(this.other.traitCarousel.selectedUpgrade);
			this.other.traitCarousel.onSelectedUpgradeChanged += this.traitCarousel.SetUnavailableUpgrade;
			this.other.itemCarousel.onSelectedUpgradeChanged += this.itemCarousel.SetUnavailableUpgrade;
			this.lastNavigable = null;
			this.currentNavigable = null;
			this.UpdateDLC(hasDeluxeDLC);
			this.onParentOpened();
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x001113FB File Offset: 0x0010F7FB
		public void UpdateDLC(bool hasDeluxeDLC)
		{
			this.deluxeDLCWidget.GetComponent<UIInteractable>().disabled = !hasDeluxeDLC;
			this.deluxeDLCWidget.ForceUpdate();
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x0011141C File Offset: 0x0010F81C
		public void OnParentMenuClosed()
		{
			this.other.traitCarousel.onSelectedUpgradeChanged -= this.traitCarousel.SetUnavailableUpgrade;
			this.other.itemCarousel.onSelectedUpgradeChanged -= this.itemCarousel.SetUnavailableUpgrade;
			this.onParentClosed();
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x00111476 File Offset: 0x0010F876
		public override void OpenMenu()
		{
			base.OpenMenu();
			FabricWrapper.PostEvent(this.heroCustomizer.heroSelectAudio);
			FabricWrapper.PostEvent(NewGameHeroSelector.focusCardAudio);
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x0011149A File Offset: 0x0010F89A
		private void UpdateItem(HeroUpgradeDefinition upgradeDef)
		{
			this.heroCustomizer.SetItem(upgradeDef);
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x001114A8 File Offset: 0x0010F8A8
		private void UpdateTrait(HeroUpgradeDefinition upgradeDef)
		{
			this.heroCustomizer.SetTrait(upgradeDef);
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x001114B6 File Offset: 0x0010F8B6
		public HeroDefinition ExtractHero(out HeroUpgradeDefinition trait, out HeroUpgradeDefinition item)
		{
			this.needsReset = true;
			this.other.heroCustomizer.SetOtherHero(null);
			return this.heroCustomizer.ExtractHero(out trait, out item);
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x001114E0 File Offset: 0x0010F8E0
		public void HandleTab(bool isRightTab)
		{
			int num = (!isRightTab) ? 1 : 0;
			if (num == this.idx)
			{
				this.CloseMenu();
				FabricWrapper.PostEvent(NewGameHeroSelector.focusCardAudio);
			}
			else
			{
				this.other.OpenMenu();
				this.CloseMenu();
			}
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x0011152E File Offset: 0x0010F92E
		public override bool HandleBackButton()
		{
			this.rootMenu.CloseMenu();
			FabricWrapper.PostEvent(FabricID.uiBack);
			return true;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x00111547 File Offset: 0x0010F947
		public void HandleDLCStoreOpenAction()
		{
			FabricWrapper.PostEvent((!this.rootMenu.OpenDLCStore()) ? FabricID.uiError : FabricID.uiButtonClick);
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x0011156E File Offset: 0x0010F96E
		protected override IUINavigable GetDefaultNavigable()
		{
			return (!this.lastNavigable) ? this.heroCustomizer.GetComponent<IUINavigable>() : this.lastNavigable.Get();
		}

		// Token: 0x04002A91 RID: 10897
		[SerializeField]
		private HeroCustomizer heroCustomizer;

		// Token: 0x04002A92 RID: 10898
		[SerializeField]
		private UpgradeCarousel itemCarousel;

		// Token: 0x04002A93 RID: 10899
		[SerializeField]
		private UpgradeCarousel traitCarousel;

		// Token: 0x04002A94 RID: 10900
		[SerializeField]
		public BoolWidget deluxeDLCWidget;

		// Token: 0x04002A95 RID: 10901
		private NewGameOptionsPopup rootMenu;

		// Token: 0x04002A96 RID: 10902
		private NewGameHeroSelector other;

		// Token: 0x04002A97 RID: 10903
		private int idx;

		// Token: 0x04002A98 RID: 10904
		private bool needsReset = true;

		// Token: 0x04002A99 RID: 10905
		private MonoHero monoHero;

		// Token: 0x04002A9C RID: 10908
		private static FabricEventReference focusCardAudio = "UI/Menu/UpgradeSwitchCharacter";
	}
}
