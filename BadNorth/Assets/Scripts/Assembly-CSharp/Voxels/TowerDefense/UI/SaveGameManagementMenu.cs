using System;
using System.Collections.Generic;
using I2.Loc;
using Rewired;
using RTM.Input;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200090C RID: 2316
	public class SaveGameManagementMenu : UIMenu, IGameSetup
	{
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x0011654C File Offset: 0x0011494C
		// (set) Token: 0x06003E04 RID: 15876 RVA: 0x00116554 File Offset: 0x00114954
		public UIClickable selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				if (this._selected)
				{
					this.selected.selected = false;
				}
				if (value)
				{
					value.selected = true;
				}
				this._selected = value;
			}
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x0011658C File Offset: 0x0011498C
		void IGameSetup.OnGameAwake()
		{
			SaveGameManagementMenu._instance = this;
			base.gameObject.SetActive(false);
			this.saveGameWidgets = new LocalPool<SaveGameWidget>(base.GetComponentsInChildren<SaveGameWidget>(true), null);
			this.saveGameWidgets.ExpandTo(5);
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			this.gamepadPromptVisibility = this.gamepadPrompt.GetComponent<IUIVisibility>();
			Image gamePadIcon = this.gamepadPrompt.GetComponentInChildren<Image>();
			EUIPadAction padAction = EUIPadAction.Tertiary;
			InputHelpers.onControllerTypeChanged += delegate(ControllerType t)
			{
				if (t == ControllerType.Joystick && this.currentNavigable)
				{
					gamePadIcon.sprite = Singleton<UIManager>.instance.GetActionIcon(padAction);
				}
			};
			gamePadIcon.sprite = Singleton<UIManager>.instance.GetActionIcon(padAction);
			base.onFocusedNavigableChanged += delegate(IUINavigable n)
			{
				SaveGameWidget saveGameWidget = (n != null) ? n.transform.GetComponent<SaveGameWidget>() : null;
				bool flag = saveGameWidget && saveGameWidget.campaignMeta && InputHelpers.ControllerTypeIs(ControllerType.Joystick);
				this.gamepadPromptVisibility.SetVisible(flag, false);
				if (flag)
				{
					gamePadIcon.sprite = Singleton<UIManager>.instance.GetActionIcon(padAction);
				}
			};
			this.gamepadPromptVisibility.SetVisible(false, false);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x00116664 File Offset: 0x00114A64
		public static void Open()
		{
			SaveGameManagementMenu._instance.OpenMenu();
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00116670 File Offset: 0x00114A70
		public override void OpenMenu()
		{
			this.visibility.SetVisible(true, false);
			this.gamepadPromptVisibility.SetVisible(false, true);
			base.OpenMenu();
			base.transform.ForceChildLayoutUpdates(false);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x0011669E File Offset: 0x00114A9E
		public override void CloseMenu()
		{
			base.CloseMenu();
			this.visibility.SetVisible(false, false);
			this.gamepadPromptVisibility.SetVisible(false, false);
			this.selected = null;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x001166C7 File Offset: 0x00114AC7
		private void OnEnable()
		{
			this.UpdateWidgets();
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x001166CF File Offset: 0x00114ACF
		public void OnSaveDeleted()
		{
			this.selected = null;
			this.gamepadPromptVisibility.visible = false;
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x001166E4 File Offset: 0x00114AE4
		private void UpdateWidgets()
		{
			this.saveGameWidgets.ReturnAll();
			List<CampaignSaveMeta> campaigns = Profile.meta.campaigns;
			foreach (CampaignSaveMeta campaignMeta in campaigns)
			{
				this.saveGameWidgets.GetInstance().Setup(this, campaignMeta);
			}
			this.saveGameWidgets.inUse.Sort();
			for (int i = 0; i < this.saveGameWidgets.inUse.Count; i++)
			{
				this.saveGameWidgets.inUse[i].transform.SetSiblingIndex(i);
			}
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x001167AC File Offset: 0x00114BAC
		public void HandleLoadButton(CampaignSaveMeta campaignMeta)
		{
			if (campaignMeta)
			{
				FabricWrapper.PostEvent(SaveGameManagementMenu.loadAudio);
				MetaMenuHelpers.LoadGame(campaignMeta);
			}
			else
			{
				FabricWrapper.PostEvent(SaveGameManagementMenu.newGameAudio);
				NewGameOptionsPopup.OpenMenu(null);
			}
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x001167E1 File Offset: 0x00114BE1
		public void HandleClickOff()
		{
			if (this._selected)
			{
				this.selected = null;
			}
			else
			{
				this.HandleBackButton();
			}
		}

		// Token: 0x04002B4C RID: 11084
		[SerializeField]
		private Localize titleLoc;

		// Token: 0x04002B4D RID: 11085
		[SerializeField]
		private GameObject gamepadPrompt;

		// Token: 0x04002B4E RID: 11086
		private LocalPool<SaveGameWidget> saveGameWidgets;

		// Token: 0x04002B4F RID: 11087
		private static SaveGameManagementMenu _instance = null;

		// Token: 0x04002B50 RID: 11088
		private IUIVisibility visibility;

		// Token: 0x04002B51 RID: 11089
		private IUIVisibility gamepadPromptVisibility;

		// Token: 0x04002B52 RID: 11090
		private static FabricEventReference loadAudio = "UI/Menu/LoadSelect";

		// Token: 0x04002B53 RID: 11091
		private static FabricEventReference newGameAudio = "UI/Menu/NewGame";

		// Token: 0x04002B54 RID: 11092
		private static FabricEventReference overwriteAudio = "UI/Menu/Overwrite";

		// Token: 0x04002B55 RID: 11093
		private UIClickable _selected;
	}
}
