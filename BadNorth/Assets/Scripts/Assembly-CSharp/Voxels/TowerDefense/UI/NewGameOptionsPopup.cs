using System;
using System.Collections.Generic;
using System.Globalization;
using CS.Platform;
using RTM.OnScreenDebug;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008FC RID: 2300
	public class NewGameOptionsPopup : GeneratedMenu
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x0011207C File Offset: 0x0011047C
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x00112084 File Offset: 0x00110484
		public bool hasDeluxe { get; private set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x0011208D File Offset: 0x0011048D
		// (set) Token: 0x06003D36 RID: 15670 RVA: 0x00112095 File Offset: 0x00110495
		public bool canShowStore { get; private set; }

		// Token: 0x06003D37 RID: 15671 RVA: 0x0011209E File Offset: 0x0011049E
		protected override void ClearWidgets()
		{
			base.ClearWidgets();
			this.settingsContainer.DestroyChildren();
			this.buttonContainer.DestroyChildren();
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x001120BC File Offset: 0x001104BC
		protected override void Initialize()
		{
			base.Initialize();
			NewGameOptionsPopup.instance = this;
			this.visibility = this.rootTransform.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			string[] values = new string[]
			{
				Difficulty.None.GetLocTerm(),
				Difficulty.Easy.GetLocTerm(),
				Difficulty.Normal.GetLocTerm(),
				Difficulty.Hard.GetLocTerm(),
				Difficulty.VeryHard.GetLocTerm()
			};
			this.difficultySetting = this.AddMultiSelectWidget("SETTINGS/DIFFICULTY/NAME", values, true, () => (int)this.difficulty, delegate(int d)
			{
				this.difficulty = (Difficulty)d;
				return true;
			}, this.settingsContainer);
			this.difficultySetting.SetMinMax(1, 4);
			this.difficultySetting.SetSuccessAudio(FabricID.settingChange);
			BoolWidget boolWidget = this.AddBoolWidget("SETTINGS/ALLOW_REPLAY_LEVEL", () => this.allowReplayLevel, delegate(bool r)
			{
				this.allowReplayLevel = r;
				return true;
			}, this.settingsContainer);
			boolWidget.SetSuccessAudio(FabricID.settingChange);
			BoolWidget boolWidget2 = this.AddBoolWidget("SETTINGS/TUTORIAL", () => this.tutorial, delegate(bool t)
			{
				this.tutorial = t;
				return true;
			}, this.settingsContainer);
			boolWidget2.SetSuccessAudio(FabricID.settingChange);
			ButtonWidget buttonWidget = this.AddButton("UI/SAVE_LOAD/BEGIN", new Func<bool>(this.HandleOKButton), this.buttonContainer, null);
			buttonWidget.SetSuccessAudio(NewGameOptionsPopup.newGame);
			this.defaultNavigable = buttonWidget.GetComponentInChildren<IUINavigable>(true);
			UiBehaviorDelegates component = this.maxWidthReference.GetComponent<UiBehaviorDelegates>();
			component.onRectTransformDimensionsChange += this.OnRectTransformDimensionsChange;
			this.heroSelectors = this.rootTransform.GetComponentsInChildren<NewGameHeroSelector>(true);
			this.heroSelectors[0].Init(this, this.heroSelectors[1], 0, this.showDeluxe);
			this.heroSelectors[1].Init(this, this.heroSelectors[0], 1, this.showDeluxe);
			PlatformEvents.OnPlatformGamePauseEvent += this.OnPlatformGamePauseEvent;
			PlatformEvents.OnGameSetup += this.OnGameSetup;
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x001122AC File Offset: 0x001106AC
		private void OnRectTransformDimensionsChange()
		{
			Rect rect = this.maxWidthReference.rect;
			Rect rect2 = ((RectTransform)this.maxWidthControl.parent).rect;
			Rect rect3 = this.maxWidthControl.rect;
			this.maxWidthControl.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Min(rect.width, rect2.width));
			rect3.position = new Vector3(rect.position.x, rect.position.y);
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x00112336 File Offset: 0x00110736
		public bool HandleOKButton()
		{
			this.NewGame(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
			return true;
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x0011234E File Offset: 0x0011074E
		public void HandleEnterSeedButton()
		{
			ModalOverlay.GetInstance().InitializeTextInput("New Campaign", "Enter a campaign seed as decimal or hex (starting 0x)", "Seed", new Func<string, bool>(this.ProcessSeed), true);
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x00112378 File Offset: 0x00110778
		private bool ProcessSeed(string seedStr)
		{
			if (seedStr.StartsWith("0x"))
			{
				seedStr = seedStr.Substring(2);
				int seed;
				if (int.TryParse(seedStr, NumberStyles.HexNumber, CultureInfo.CurrentCulture.NumberFormat, out seed))
				{
					this.NewGame(seed);
					return true;
				}
				return false;
			}
			else
			{
				int seed;
				if (int.TryParse(seedStr, out seed))
				{
					this.NewGame(seed);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x001123DC File Offset: 0x001107DC
		private void NewGame(int seed)
		{
			if (this.overwriting)
			{
				MetaMenuHelpers.DeleteSave(this.overwriting, delegate
				{
					this.StartCampaign(seed);
				});
				this.overwriting = null;
			}
			else
			{
				this.StartCampaign(seed);
			}
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x0011243C File Offset: 0x0011083C
		private void StartCampaign(int campaignSeed)
		{
			Profile.userSave.campaignPrefs = new CampaignPrefs(this.difficulty, !this.tutorial, this.allowReplayLevel);
			switch (this.difficulty)
			{
			case Difficulty.Easy:
				FabricWrapper.PostEvent(NewGameOptionsPopup.newGameEasy);
				break;
			case Difficulty.Normal:
				break;
			case Difficulty.Hard:
				FabricWrapper.PostEvent(NewGameOptionsPopup.newGameHard);
				break;
			case Difficulty.VeryHard:
				FabricWrapper.PostEvent(NewGameOptionsPopup.newGameVeryHard);
				break;
			default:
				throw new NotImplementedException(string.Format("Unknown difficulty '{0}'", this.difficulty));
			}
			CampaignSave campaignSave = Profile.CreateNewCampaign(campaignSeed);
			campaignSave.heroes = new List<HeroDefinition>(2);
			this.TransferHeroToCampaign(this.heroSelectors[0], campaignSave);
			this.TransferHeroToCampaign(this.heroSelectors[1], campaignSave);
			MetaMenuHelpers.NewGame(campaignSave);
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x00112518 File Offset: 0x00110918
		private void TransferHeroToCampaign(NewGameHeroSelector selector, CampaignSave campaign)
		{
			HeroUpgradeDefinition traitDef;
			HeroUpgradeDefinition heroUpgradeDefinition;
			HeroDefinition heroDefinition = selector.ExtractHero(out traitDef, out heroUpgradeDefinition);
			if (heroUpgradeDefinition && heroUpgradeDefinition.typeEnum == HeroUpgradeTypeEnum.Consumable)
			{
				campaign.inventory.Add(heroUpgradeDefinition);
				heroUpgradeDefinition = null;
			}
			heroDefinition.SetupHeroState(true, traitDef, null, heroUpgradeDefinition);
			heroDefinition.id = campaign.heroes.Count;
			campaign.heroes.Add(heroDefinition);
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x00112581 File Offset: 0x00110981
		public static void OpenMenu(CampaignSaveMeta overwriting = null)
		{
			NewGameOptionsPopup.instance.OpenMenuInternal(overwriting);
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x00112590 File Offset: 0x00110990
		private void OpenMenuInternal(CampaignSaveMeta overwriting = null)
		{
			this.overwriting = overwriting;
			UserSave userSave = Profile.userSave;
			Difficulty maxDifficulty = userSave.maxDifficulty;
			CampaignPrefs campaignPrefs = userSave.campaignPrefs;
			this.difficulty = ((campaignPrefs.difficulty <= maxDifficulty) ? campaignPrefs.difficulty : maxDifficulty);
			this.tutorial = !campaignPrefs.skipTutorial;
			this.allowReplayLevel = campaignPrefs.allowReplays;
			this.visibility.SetVisible(true, false);
			base.transform.ForceChildLayoutUpdates(false);
			this.difficultySetting.SetMinMax(1, (int)maxDifficulty);
			this.UpdateHasDeluxe();
			this.canShowStore = BasePlatformManager.Instance.CanShowDLCStore("DELUXE_ED_CONTENT");
			List<HeroUpgradeDefinition> startingItems = Profile.userSave.inventory.startingItems;
			List<HeroUpgradeDefinition> startingTraits = Profile.userSave.inventory.startingTraits;
			this.MaybeSetupHeroes();
			this.heroSelectors[0].OnParentMenuOpened(startingTraits, startingItems, this.hasDeluxe);
			this.heroSelectors[1].OnParentMenuOpened(startingTraits, startingItems, this.hasDeluxe);
			base.OpenMenu();
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x00112690 File Offset: 0x00110A90
		public void UpdateHasDeluxe()
		{
			this.hasDeluxe = false;
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x0011269C File Offset: 0x00110A9C
		private void OnPlatformGamePauseEvent(bool effect)
		{
			if (base.isOpen && !effect)
			{
				this.UpdateHasDeluxe();
				foreach (NewGameHeroSelector newGameHeroSelector in this.heroSelectors)
				{
					newGameHeroSelector.UpdateDLC(this.hasDeluxe);
				}
			}
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x001126EB File Offset: 0x00110AEB
		private void OnGameSetup()
		{
			PlatformEvents.OnGameSetup += this.OnGameSetup;
			this.MaybeSetupHeroes();
			Singleton<Stack>.instance.stateMeta.OnActivate += this.MaybeSetupHeroes;
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x00112720 File Offset: 0x00110B20
		private void MaybeSetupHeroes()
		{
			this.UpdateHasDeluxe();
			int num = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			List<HeroUpgradeDefinition> startingItems = Profile.userSave.inventory.startingItems;
			List<HeroUpgradeDefinition> startingTraits = Profile.userSave.inventory.startingTraits;
			this.heroSelectors[0].MaybeSetupHeroes(num, startingTraits, startingItems);
			this.heroSelectors[1].MaybeSetupHeroes(num + 1073741823, startingTraits, startingItems);
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x00112789 File Offset: 0x00110B89
		public override void CloseMenu()
		{
			this.visibility.SetVisible(false, false);
			this.overwriting = null;
			this.heroSelectors[0].OnParentMenuClosed();
			this.heroSelectors[1].OnParentMenuClosed();
			base.CloseMenu();
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x001127BF File Offset: 0x00110BBF
		public void HandleClickOff()
		{
			this.HandleBackButton();
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x001127C8 File Offset: 0x00110BC8
		public void HandleTabLeft()
		{
			this.heroSelectors[0].OpenMenu();
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x001127D7 File Offset: 0x00110BD7
		public void HandleTabRight()
		{
			this.heroSelectors[1].OpenMenu();
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x001127E6 File Offset: 0x00110BE6
		public bool OpenDLCStore()
		{
			BasePlatformManager.Instance.ShowDLCStore("DELUXE_ED_CONTENT");
			return true;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x001127F8 File Offset: 0x00110BF8
		protected override IUINavigable GetDefaultNavigable()
		{
			if (this.lastNavigable)
			{
				return this.lastNavigable.Get();
			}
			return (this.defaultNavigable == null) ? base.GetDefaultNavigable() : this.defaultNavigable;
		}

		// Token: 0x04002AB4 RID: 10932
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("NewGameOptionsPopup", EVerbosity.Quiet, 100);

		// Token: 0x04002AB5 RID: 10933
		private const string DELUXE_ED_CONTENT = "DELUXE_ED_CONTENT";

		// Token: 0x04002AB6 RID: 10934
		public readonly bool showDeluxe;

		// Token: 0x04002AB7 RID: 10935
		[Header("Containers")]
		[SerializeField]
		private RectTransform settingsContainer;

		// Token: 0x04002AB8 RID: 10936
		[SerializeField]
		private RectTransform buttonContainer;

		// Token: 0x04002AB9 RID: 10937
		[SerializeField]
		private RectTransform rootTransform;

		// Token: 0x04002ABA RID: 10938
		[SerializeField]
		private RectTransform maxWidthReference;

		// Token: 0x04002ABB RID: 10939
		[SerializeField]
		private RectTransform maxWidthControl;

		// Token: 0x04002ABC RID: 10940
		private static NewGameOptionsPopup instance = null;

		// Token: 0x04002ABD RID: 10941
		private IUIVisibility visibility;

		// Token: 0x04002ABE RID: 10942
		private IUINavigable defaultNavigable;

		// Token: 0x04002ABF RID: 10943
		private NewGameHeroSelector[] heroSelectors;

		// Token: 0x04002AC0 RID: 10944
		private MultiSelectWidget difficultySetting;

		// Token: 0x04002AC1 RID: 10945
		private CampaignSaveMeta overwriting;

		// Token: 0x04002AC2 RID: 10946
		private Difficulty difficulty;

		// Token: 0x04002AC3 RID: 10947
		private bool tutorial;

		// Token: 0x04002AC4 RID: 10948
		private bool allowReplayLevel;

		// Token: 0x04002AC5 RID: 10949
		private static FabricEventReference newGame = "Mus/NewCampaign";

		// Token: 0x04002AC6 RID: 10950
		private static FabricEventReference newGameEasy = "UI/Menu/NewGameEasy";

		// Token: 0x04002AC7 RID: 10951
		private static FabricEventReference newGameHard = "UI/Menu/NewGameHard";

		// Token: 0x04002AC8 RID: 10952
		private static FabricEventReference newGameVeryHard = "UI/Menu/NewGameVeryHard";
	}
}
