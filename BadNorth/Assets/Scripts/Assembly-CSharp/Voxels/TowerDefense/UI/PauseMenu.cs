using System;
using CS.Platform;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008BF RID: 2239
	public class PauseMenu : GeneratedMenu, IslandUIManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x06003AF8 RID: 15096 RVA: 0x00105DEC File Offset: 0x001041EC
		protected override void Initialize()
		{
			base.Initialize();
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, false);
			this.AddButton("UI/PAUSE/RESUME", delegate()
			{
				this.Resume();
				return true;
			}, null, null);
			this.AddButton("UI/COMMON/SETTINGS", delegate()
			{
				UserSettingsMenu.Open(this.manager);
				this.visibility.SetVisible(false, false);
				return true;
			}, null, null).SetSuccessAudio("UI/Menu/Settings");
			this.AddButton("UI/GAMEPLAY/RESTART", delegate()
			{
				this.gameplayManager.levelLeaver.ReplayLevel();
				return true;
			}, null, null).SetSuccessAudio("UI/InGame/Restart").SetVisibilityCallback(new Func<bool>(this.AllowRestart));
			this.AddButton("UI/PAUSE/EXIT_LEVEL", delegate()
			{
				this.gameplayManager.levelLeaver.ExitLevel();
				return true;
			}, null, null).SetSuccessAudio(FabricID.exitLevel).SetVisibilityCallback(new Func<bool>(this.AllowSafeExit));
			this.AddButton("UI/PAUSE/ABANDON_LEVEL", delegate()
			{
				this.AbandonLevelConfirm();
				return true;
			}, null, null).SetVisibilityCallback(new Func<bool>(this.AllowAbandon));
			this.AddButton("UI/CAMPAIGN/EXIT_MAIN", delegate()
			{
				this.gameplayManager.levelLeaver.ExitToMainMenu();
				return true;
			}, null, null).SetSuccessAudio("UI/InGame/MainMenu").SetVisibilityCallback(new Func<bool>(this.AllowExitToMainMenu));
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x00105F2F File Offset: 0x0010432F
		public bool AllowExitToMainMenu()
		{
			return this.blockExitToMap;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x00105F38 File Offset: 0x00104338
		public bool AllowSafeExit()
		{
			LevelNode levelNode = this.gameplayManager.island.levelNode;
			return !(levelNode != null) || ((this.gameplayManager.states.loadout.active || this.allowReplays) && !this.blockExitToMap);
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x00105F95 File Offset: 0x00104395
		public bool AllowRestart()
		{
			return this.allowReplays && !this.gameplayManager.states.loadout.active;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x00105FC0 File Offset: 0x001043C0
		public bool AllowAbandon()
		{
			LevelNode levelNode = this.gameplayManager.island.levelNode;
			return !(levelNode != null) || (!this.AllowSafeExit() && !this.blockExitToMap && !levelNode.isEnd);
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x0010600E File Offset: 0x0010440E
		public void AbandonLevelConfirm()
		{
			ModalOverlay.GetInstance().Initialize("UI/PAUSE/ABANDON_WARNING/TITLE", "UI/PAUSE/ABANDON_WARNING/MESSAGE", true).AddOKButton(delegate
			{
				this.gameplayManager.levelLeaver.AbandonLevel();
				return true;
			}).SetSuccessAudio("UI/InGame/MainMenu");
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00106046 File Offset: 0x00104446
		public void Resume()
		{
			this.levelPauser.SetPause(false, true);
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00106058 File Offset: 0x00104458
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			base.gameObject.SetActive(false);
			this.manager = manager;
			this.gameplayManager = manager.gameplayManager;
			this.viewfinder = manager.islandViewfinder;
			this.levelPauser = this.gameplayManager.levelPauser;
			this.levelPauser.onPauseChanged += this.OnPauseChanged;
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x001060B8 File Offset: 0x001044B8
		private void OnPauseChanged(bool paused)
		{
			if (!BasePlatformManager.Instance.IsShowingMessage())
			{
				if (paused)
				{
					this.OpenMenu();
				}
				else
				{
					this.CloseMenu();
				}
			}
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x001060E0 File Offset: 0x001044E0
		public override void OpenMenu()
		{
			this.visibility.SetVisible(true, false);
			this.viewfinder.Push(this.viewfinderTrans, null);
			base.OpenMenu();
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00106107 File Offset: 0x00104507
		public override void CloseMenu()
		{
			if (base.isOpen)
			{
				base.CloseMenu();
				this.viewfinder.Remove(this.viewfinderTrans, null);
				this.visibility.SetVisible(false, false);
			}
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x00106139 File Offset: 0x00104539
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.visibility.SetVisible(true, false);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x0010614E File Offset: 0x0010454E
		protected override void OnLostFocus()
		{
			base.OnLostFocus();
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x00106156 File Offset: 0x00104556
		public override bool HandleBackButton()
		{
			this.levelPauser.SetPause(false, true);
			return true;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x00106166 File Offset: 0x00104566
		protected override void ClearWidgets()
		{
			base.ClearWidgets();
			if (this.debugWidgetContainer)
			{
				this.debugWidgetContainer.DestroyChildren();
			}
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x0010618C File Offset: 0x0010458C
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.visibility.SetVisible(false, true);
			LevelNode levelNode = island.levelNode;
			Campaign campaign = (!levelNode) ? null : levelNode.campaign;
			this.allowReplays = (campaign && campaign.campaignSave.prefs.allowReplays);
			this.blockExitToMap = (levelNode.isStart && !levelNode.IsPlayed());
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x00106209 File Offset: 0x00104609
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.visibility.SetVisible(false, true);
			this.allowReplays = false;
			this.blockExitToMap = false;
		}

		// Token: 0x040028F4 RID: 10484
		[Header("PauseMenu")]
		[SerializeField]
		private RectTransform viewfinderTrans;

		// Token: 0x040028F5 RID: 10485
		[SerializeField]
		private RectTransform debugWidgetContainer;

		// Token: 0x040028F6 RID: 10486
		private IslandUIManager manager;

		// Token: 0x040028F7 RID: 10487
		private IslandGameplayManager gameplayManager;

		// Token: 0x040028F8 RID: 10488
		private IslandViewfinder viewfinder;

		// Token: 0x040028F9 RID: 10489
		private LevelPauser levelPauser;

		// Token: 0x040028FA RID: 10490
		private bool allowReplays;

		// Token: 0x040028FB RID: 10491
		private bool blockExitToMap;

		// Token: 0x040028FC RID: 10492
		private IUIVisibility visibility;
	}
}
