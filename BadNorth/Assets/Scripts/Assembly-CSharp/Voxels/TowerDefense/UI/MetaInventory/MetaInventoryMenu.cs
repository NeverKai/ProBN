using System;
using System.Collections.Generic;
using System.Linq;
using Fabric;
using I2.Loc;
using Rewired;
using RTM;
using RTM.Input;
using RTM.OnScreenDebug;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI.MetaInventory
{
	// Token: 0x020008EE RID: 2286
	[RequireComponent(typeof(UiBehaviorDelegates))]
	public class MetaInventoryMenu : UIMenu, IGameSetup
	{
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x0010E2AF File Offset: 0x0010C6AF
		// (set) Token: 0x06003C9A RID: 15514 RVA: 0x0010E2B8 File Offset: 0x0010C6B8
		private UpgradeToken selectedToken
		{
			get
			{
				return this._selectedToken;
			}
			set
			{
				if (this.selectedToken == value)
				{
					return;
				}
				if (this._selectedToken)
				{
					this._selectedToken.clickable.selected = false;
				}
				this._selectedToken = value;
				if (this._selectedToken)
				{
					this._selectedToken.clickable.selected = true;
				}
				this.buttonClickable.disabled = (!this._selectedToken || !this._selectedToken.isStarting || !this.originalToken || this.selectedToken.group != this.originalToken.group);
				this.buttonVisibility.visible = (this.selectedToken && this.originalToken && this.selectedToken.group == this.originalToken.group);
				if (this.buttonVisibility.visible)
				{
					if (this._selectedToken.upgradeDef.typeEnum == HeroUpgradeTypeEnum.Trait)
					{
						this.buttonText.Term = ((!(this._selectedToken == this.originalToken)) ? "UI/HERO_MANAGEMENT/ASSIGN_TRAIT" : "UI/HERO_MANAGEMENT/UNASSIGN_TRAIT");
					}
					else
					{
						this.buttonText.Term = ((!(this._selectedToken == this.originalToken)) ? "UI/HERO_MANAGEMENT/EQUIP_ITEM" : "UI/HERO_MANAGEMENT/UNEQUIP_ITEM");
					}
				}
			}
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x0010E450 File Offset: 0x0010C850
		void IGameSetup.OnGameAwake()
		{
			MetaInventoryMenu.instance = this;
			this.goSetActiveFalse = delegate()
			{
				base.gameObject.SetActive(false);
			};
			List<HeroUpgradeDefinition> list = ResourceList<HeroUpgradeDefinition>.list;
			this.inventoryGroups = new List<InventoryGroup>(this.groups.Length);
			this.tokens = new List<UpgradeToken>(list.Count);
			List<HeroUpgradeDefinition> list2 = ListPool<HeroUpgradeDefinition>.GetList(list.Count);
			foreach (MetaInventoryMenu.Group group in this.groups)
			{
				list2.Clear();
				foreach (HeroUpgradeDefinition heroUpgradeDefinition in list)
				{
					if (group.types.Contains(heroUpgradeDefinition.typeEnum))
					{
						list2.Add(heroUpgradeDefinition);
					}
				}
				this.InitGroup(group, list2);
			}
			list2.ReturnToListPool<HeroUpgradeDefinition>();
			this.infoPanelPool = new LocalPool<InfoPanel>(this.infoPanelExample, this.infoPanelExample.transform.parent);
			UiBehaviorDelegates component = this.maxWidthReference.GetComponent<UiBehaviorDelegates>();
			component.onRectTransformDimensionsChange += this.OnRectTransformDimensionsChange;
			base.GetComponent<IUIVisibility>().SetVisible(false, true);
			this.buttonVisibility = this.buttonContainer.GetComponent<IUIVisibility>();
			this.buttonVisibility.SetVisible(false, true);
			this.buttonText = this.buttonContainer.GetComponentInChildren<Localize>(true);
			this.buttonClickable = this.buttonContainer.GetComponentInChildren<UIClickable>(true);
			this.buttonClickable.onClick += delegate()
			{
				bool flag = this.EquipUpgrade(this._selectedToken);
				FabricWrapper.PostEvent((!flag) ? FabricID.uiError : FabricID.uiButtonClick);
			};
			base.gameObject.SetActive(false);
			base.transform.MarkChildLayoutsForRebuild(false);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x0010E608 File Offset: 0x0010CA08
		private void OnRectTransformDimensionsChange()
		{
			Rect rect = this.maxWidthReference.rect;
			Rect rect2 = ((RectTransform)this.maxWidthControl.parent).rect;
			Rect rect3 = this.maxWidthControl.rect;
			this.maxWidthControl.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Min(rect.width, rect2.width));
			rect3.position = new Vector3(rect.position.x, rect.position.y);
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x0010E698 File Offset: 0x0010CA98
		private void InitGroup(MetaInventoryMenu.Group group, List<HeroUpgradeDefinition> upgrades)
		{
			InventoryGroup group2 = group.group;
			UpgradeToken[] componentsInChildren = group2.GetComponentsInChildren<UpgradeToken>(true);
			foreach (UpgradeToken upgradeToken in componentsInChildren)
			{
				upgradeToken.transform.SetParent(base.transform, false);
				UnityEngine.Object.Destroy(upgradeToken.gameObject);
			}
			group2.Init();
			group2.titleLocalize.Term = group.titleTerm;
			this.inventoryGroups.Add(group2);
			Transform transform = group2.gridLayout.transform;
			for (int j = 0; j < upgrades.Count; j++)
			{
				HeroUpgradeDefinition upgradeDef = upgrades[j];
				UpgradeToken upgradeToken2 = UnityEngine.Object.Instantiate<UpgradeToken>(group.tokenPrefab, transform, false);
				upgradeToken2.Setup(this, group2, upgradeDef, j, upgrades.Count);
				this.tokens.Add(upgradeToken2);
			}
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x0010E775 File Offset: 0x0010CB75
		public static void OpenMenuStatic(HeroUpgradeDefinition upgradeDef = null, Action<HeroUpgradeDefinition> closeCallback = null)
		{
			MetaInventoryMenu.instance.OpenMenu(upgradeDef, closeCallback);
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x0010E783 File Offset: 0x0010CB83
		public override void OpenMenu()
		{
			this.OpenMenu(null, null);
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x0010E790 File Offset: 0x0010CB90
		public void OpenMenu(HeroUpgradeDefinition equippedUpgrade, Action<HeroUpgradeDefinition> equipCallback = null)
		{
			foreach (UpgradeToken upgradeToken in this.tokens)
			{
				upgradeToken.Refresh();
			}
			this.selectedToken = null;
			if (equippedUpgrade != null)
			{
				foreach (UpgradeToken upgradeToken2 in this.tokens)
				{
					if (upgradeToken2.upgradeDef == equippedUpgrade)
					{
						this.selectedToken = (this.originalToken = upgradeToken2);
						break;
					}
				}
			}
			if (this._selectedToken)
			{
				this.buttonVisibility.SetVisible(true, true);
				this.buttonClickable.disabled = false;
			}
			else
			{
				this.selectedToken = this.tokens[0];
			}
			this.SetupInfoPanel(this.selectedToken);
			FabricWrapper.PostEvent(this.ambiance);
			base.OpenMenu();
			base.GetComponent<IUIVisibility>().SetVisible(true, false);
			base.transform.ForceChildLayoutUpdates(true);
			base.ScrollTo(this.selectedToken.GetComponent<UINavigable>(), true);
			this.equipCallback = equipCallback;
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x0010E8FC File Offset: 0x0010CCFC
		public override void CloseMenu()
		{
			this.selectedToken = null;
			this.equipCallback = null;
			this.originalToken = null;
			base.GetComponent<IUIVisibility>().SetVisible(false, false);
			FabricWrapper.PostEvent(this.ambiance, EventAction.StopSound);
			base.CloseMenu();
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x0010E933 File Offset: 0x0010CD33
		public void OnDisable()
		{
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x0010E935 File Offset: 0x0010CD35
		protected override IUINavigable GetDefaultNavigable()
		{
			if (this.selectedToken != null)
			{
				return this.selectedToken.GetComponent<IUINavigable>();
			}
			return base.GetDefaultNavigable();
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x0010E95C File Offset: 0x0010CD5C
		public override void Navigate(Vector2 navigationNormal)
		{
			if (!this.currentNavigable)
			{
				this.currentNavigable = new WeakInterfaceReference<IUINavigable>((!this.selectedToken) ? null : this.selectedToken.GetComponent<IUINavigable>());
			}
			base.Navigate(navigationNormal);
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x0010E9AC File Offset: 0x0010CDAC
		private void SetupInfoPanel(UpgradeToken upgradeToken)
		{
			if (this.currentInfoPanel && this.currentInfoPanel.upgradeToken == upgradeToken)
			{
				return;
			}
			HeroUpgradeDefinition upgradeDef = upgradeToken.upgradeDef;
			int maxLevel;
			bool flag;
			bool isStartItem;
			bool isKnown = Profile.userSave.inventory.Get(upgradeDef, out maxLevel, out flag, out isStartItem);
			Vector2 vector = (!this.currentInfoPanel) ? Vector2.zero : ((this.currentInfoPanel.upgradeToken.transform.position - upgradeToken.transform.position).normalized * 20f);
			if (this.currentInfoPanel)
			{
				this.currentInfoPanel.visible.SetActive(false);
				this.currentInfoPanel.posAnim.SetTarget(vector, null, null, null, 0f, null);
			}
			this.currentInfoPanel = this.infoPanelPool.GetInstance();
			this.currentInfoPanel.posAnim.SetCurrent(-vector);
			this.currentInfoPanel.posAnim.SetTarget(Vector2.zero, null, null, null, 0f, null);
			this.currentInfoPanel.transform.SetAsLastSibling();
			this.currentInfoPanel.Setup(upgradeToken, maxLevel, isKnown, isStartItem);
			this.currentInfoPanel.visible.SetActive(true);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x0010EB10 File Offset: 0x0010CF10
		internal bool HandleClick(UpgradeToken upgradeToken)
		{
			if (!InputHelpers.ControllerTypeIs(ControllerType.Mouse))
			{
				return this.EquipUpgrade(upgradeToken);
			}
			if (this.selectedToken != upgradeToken)
			{
				this.selectedToken = upgradeToken;
				this.SetupInfoPanel(upgradeToken);
				return true;
			}
			FabricWrapper.PostEvent(FabricID.uiError);
			return false;
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x0010EB60 File Offset: 0x0010CF60
		private bool EquipUpgrade(UpgradeToken token)
		{
			if (this.originalToken != null && token != null)
			{
				if (token == this.originalToken)
				{
					this.equipCallback(null);
					FabricWrapper.PostEvent(FabricID.uiBack);
					this.CloseMenu();
					return true;
				}
				if (this.originalToken.group == token.group && this.equipCallback != null)
				{
					this.equipCallback(token.upgradeDef);
					FabricWrapper.PostEvent(token.upgradeDef.uiPurchaseAudioId);
					this.CloseMenu();
					return true;
				}
			}
			FabricWrapper.PostEvent(FabricID.uiError);
			return false;
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x0010EC16 File Offset: 0x0010D016
		internal void HandleFocusChange(UpgradeToken upgradeToken)
		{
			this.selectedToken = upgradeToken;
			this.SetupInfoPanel(upgradeToken);
		}

		// Token: 0x04002A41 RID: 10817
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("MetaInventoryMenu", EVerbosity.Quiet, 100);

		// Token: 0x04002A42 RID: 10818
		[Space]
		[Header("MetaInvetory")]
		[SerializeField]
		private InfoPanel infoPanelExample;

		// Token: 0x04002A43 RID: 10819
		private LocalPool<InfoPanel> infoPanelPool;

		// Token: 0x04002A44 RID: 10820
		private InfoPanel currentInfoPanel;

		// Token: 0x04002A45 RID: 10821
		[SerializeField]
		private RectTransform maxWidthReference;

		// Token: 0x04002A46 RID: 10822
		[SerializeField]
		private RectTransform maxWidthControl;

		// Token: 0x04002A47 RID: 10823
		[SerializeField]
		private RectTransform buttonContainer;

		// Token: 0x04002A48 RID: 10824
		[SerializeField]
		private MetaInventoryMenu.Group[] groups = new MetaInventoryMenu.Group[1];

		// Token: 0x04002A49 RID: 10825
		private Action goSetActiveFalse;

		// Token: 0x04002A4A RID: 10826
		private static MetaInventoryMenu instance;

		// Token: 0x04002A4B RID: 10827
		private List<InventoryGroup> inventoryGroups;

		// Token: 0x04002A4C RID: 10828
		private List<UpgradeToken> tokens;

		// Token: 0x04002A4D RID: 10829
		private UpgradeToken _selectedToken;

		// Token: 0x04002A4E RID: 10830
		private UpgradeToken originalToken;

		// Token: 0x04002A4F RID: 10831
		private Action<HeroUpgradeDefinition> equipCallback;

		// Token: 0x04002A50 RID: 10832
		private IUIVisibility buttonVisibility;

		// Token: 0x04002A51 RID: 10833
		private UIClickable buttonClickable;

		// Token: 0x04002A52 RID: 10834
		private Localize buttonText;

		// Token: 0x04002A53 RID: 10835
		private FabricEventReference ambiance = "Amb/Codec";

		// Token: 0x020008EF RID: 2287
		[Serializable]
		private class Group
		{
			// Token: 0x04002A54 RID: 10836
			[TermsPopup("")]
			public string titleTerm;

			// Token: 0x04002A55 RID: 10837
			public InventoryGroup group;

			// Token: 0x04002A56 RID: 10838
			public UpgradeToken tokenPrefab;

			// Token: 0x04002A57 RID: 10839
			public HeroUpgradeTypeEnum[] types = new HeroUpgradeTypeEnum[1];
		}
	}
}
