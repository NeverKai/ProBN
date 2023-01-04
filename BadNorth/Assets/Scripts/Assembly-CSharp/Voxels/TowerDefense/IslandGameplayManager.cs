using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using CS.Platform;
using CS.VT;
using I2.Loc;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.CoinDispensing;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.UI;
using Voxels.TowerDefense.UI.UpgradeScreen;

namespace Voxels.TowerDefense
{
	// Token: 0x0200052B RID: 1323
	public class IslandGameplayManager : Singleton<IslandGameplayManager>, IGameSetup
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x00063982 File Offset: 0x00061D82
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x0006398A File Offset: 0x00061D8A
		public IslandStateTree states { get; private set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x00063993 File Offset: 0x00061D93
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0006399B File Offset: 0x00061D9B
		public LevelCamera levelCamera { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000639A4 File Offset: 0x00061DA4
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x000639AC File Offset: 0x00061DAC
		public LevelPauser levelPauser { get; private set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000639B5 File Offset: 0x00061DB5
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x000639BD File Offset: 0x00061DBD
		public SquadSpawner squadSpawner { get; private set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x000639C6 File Offset: 0x00061DC6
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x000639CE File Offset: 0x00061DCE
		public SquadSelector squadSelector { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000639D7 File Offset: 0x00061DD7
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x000639DF File Offset: 0x00061DDF
		public PointerRationalizer pointerRationalizer { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000639E8 File Offset: 0x00061DE8
		// (set) Token: 0x0600225E RID: 8798 RVA: 0x000639F0 File Offset: 0x00061DF0
		public CursorManager cursorManager { get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000639F9 File Offset: 0x00061DF9
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x00063A01 File Offset: 0x00061E01
		public CoinDispenser coinDispenser { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x00063A0A File Offset: 0x00061E0A
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x00063A12 File Offset: 0x00061E12
		public NavigatorNavSpotPool navigatorNavSpotPool { get; private set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x00063A1B File Offset: 0x00061E1B
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x00063A23 File Offset: 0x00061E23
		public NavSpotPoolManager navSpotPoolManager { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x00063A2C File Offset: 0x00061E2C
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x00063A34 File Offset: 0x00061E34
		public IslandUINotificationManager notificationManager { get; private set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00063A3D File Offset: 0x00061E3D
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x00063A45 File Offset: 0x00061E45
		public WorldSpaceCursorIconPool worldSpaceCursorIconPool { get; private set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x00063A4E File Offset: 0x00061E4E
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x00063A56 File Offset: 0x00061E56
		public EndOfLevel endOfLevel { get; private set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x00063A5F File Offset: 0x00061E5F
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x00063A67 File Offset: 0x00061E67
		public LevelLeaver levelLeaver { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x00063A70 File Offset: 0x00061E70
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x00063A78 File Offset: 0x00061E78
		public NavSpotTargetManager nsTargetManager { get; private set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x00063A81 File Offset: 0x00061E81
		public CameraScreenshotter userScreenShotter
		{
			get
			{
				return this._userScreenshotter;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x00063A89 File Offset: 0x00061E89
		public AudioEventBuffer footstepsAudio
		{
			get
			{
				return this._footstepsAudio;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x00063A91 File Offset: 0x00061E91
		public AudioEventBuffer combatAudio
		{
			get
			{
				return this._combatAudio;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x00063A99 File Offset: 0x00061E99
		public Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00063AA8 File Offset: 0x00061EA8
		void IGameSetup.OnGameAwake()
		{
			this.states = base.GetComponentInChildren<IslandStateTree>(true);
			this.levelCamera = base.GetComponentInChildren<LevelCamera>(true);
			this.levelPauser = base.GetComponentInChildren<LevelPauser>(true);
			this.squadSpawner = base.GetComponentInChildren<SquadSpawner>(true);
			this.squadSelector = base.GetComponentInChildren<SquadSelector>(true);
			this.pointerRationalizer = base.GetComponentInChildren<PointerRationalizer>(true);
			this.cursorManager = base.GetComponentInChildren<CursorManager>(true);
			this.coinDispenser = base.GetComponentInChildren<CoinDispenser>(true);
			this.navigatorNavSpotPool = base.GetComponentInChildren<NavigatorNavSpotPool>(true);
			this.navSpotPoolManager = base.GetComponentInChildren<NavSpotPoolManager>(true);
			this.notificationManager = base.GetComponentInChildren<IslandUINotificationManager>(true);
			this.worldSpaceCursorIconPool = base.GetComponentInChildren<WorldSpaceCursorIconPool>(true);
			this.endOfLevel = base.GetComponentInChildren<EndOfLevel>(true);
			this.levelLeaver = base.GetComponentInChildren<LevelLeaver>(true);
			this.nsTargetManager = base.GetComponentInChildren<NavSpotTargetManager>(true);
			foreach (IslandGameplayManager.IAwake awake in base.transform.GetComponentsInChildren<IslandGameplayManager.IAwake>(true))
			{
				awake.OnAwake(this);
			}
			this.setupCoroutines = base.GetComponentsInChildren<IslandGameplayManager.ISetupIslandCoroutine>(true);
			this.setups = base.GetComponentsInChildren<IslandGameplayManager.ISetupIsland>(true);
			this.wipers = base.GetComponentsInChildren<IslandGameplayManager.IWipeIsland>(true);
			this.leavers = base.GetComponentsInChildren<IslandGameplayManager.ILeaveIsland>(true);
			Singleton<AudioStateListener>.instance.BindLevelStates(this);
			DemoAttractMode.onAttractModeTriggered += this.OnAttractModeTriggered;
			CorePlatformUtils.onForcePrimUserOut += this.OnAttractModeTriggered;
			CampaignManager.onExitCampaign += this.OnExitCampaign;
			this.richPresenseScreenshots = new SmartShuffler<string>(new string[]
			{
				"ISLAND_SCREENSHOT_01",
				"ISLAND_SCREENSHOT_02",
				"ISLAND_SCREENSHOT_03",
				"ISLAND_SCREENSHOT_04",
				"ISLAND_SCREENSHOT_05",
				"ISLAND_SCREENSHOT_06",
				"ISLAND_SCREENSHOT_07",
				"ISLAND_SCREENSHOT_08"
			});
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00063C68 File Offset: 0x00062068
		private void OnExitCampaign(CampaignManager campaignManager, Campaign campaign)
		{
			foreach (IslandGameplayManager.IExitCampaign exitCampaign in base.GetComponentsInChildren<IslandGameplayManager.IExitCampaign>(true))
			{
				exitCampaign.OnExitCampaign(campaignManager, campaign);
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00063C9D File Offset: 0x0006209D
		protected override void Awake()
		{
			base.Awake();
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00063CA5 File Offset: 0x000620A5
		public void EnterIsland(Island island)
		{
			this.states.root.SetActive(true);
			this._island = island;
			this.PlayIsland();
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00063CCA File Offset: 0x000620CA
		public void EnglishDeployBegin()
		{
			this.states.Spawning.SetActive(true);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00063CDD File Offset: 0x000620DD
		public void EnglishDeployComplete()
		{
			if (this.island.levelNode.wantsTutorial)
			{
				this.states.tutorial.SetActive(true);
			}
			else
			{
				this.states.Prepare.SetActive(true);
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00063D1B File Offset: 0x0006211B
		public void CallFirstWave()
		{
			this.states.running.SetActive(true);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00063D2E File Offset: 0x0006212E
		private void OnAttractModeTriggered()
		{
			if (this.island)
			{
				this.levelLeaver.ExitToMainMenu();
			}
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00063D4C File Offset: 0x0006214C
		private void PlayIsland()
		{
			string text = Profile.campaign.PercentComplete().ToString();
			string locTerm = Profile.activeCampaignMeta.prefs.difficulty.GetLocTerm();
			string text2 = this.island.raid.TotalNumberRaiders().ToString();
			if (this.island.levelNode.isStart)
			{
				BasePlatformManager.Instance.SetRichPresenceDetails("IN_FIRST_LEVEL", null);
			}
			else if (this.island.levelNode.isEnd)
			{
				string[] parameter = new string[]
				{
					text2,
					locTerm
				};
				BasePlatformManager.Instance.SetRichPresenceDetails("IN_FINAL_LEVEL", parameter);
			}
			else if (UnityEngine.Random.Range(0, 2) == 1)
			{
				string[] parameter2 = new string[]
				{
					text2,
					locTerm
				};
				BasePlatformManager.Instance.SetRichPresenceDetails("IN_LEVEL", parameter2);
			}
			else
			{
				string[] parameter3 = new string[]
				{
					text2,
					text
				};
				BasePlatformManager.Instance.SetRichPresenceDetails("IN_LEVEL_ALT", parameter3);
			}
			BasePlatformManager.Instance.SetRichPresenceLargeImage(this.richPresenseScreenshots.Get());
			BasePlatformManager.Instance.SendRichPresence();
			LoadingScreen.BeginLoadingPhase(ScriptLocalization.LOAD_SCREEN.INIT_GAMEPLAY, new Action(this.EnterLevel), new IEnumerator[]
			{
				CoroutineUtils.GenerateTimer(15f, this.PlayIslandRoutine())
			});
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00063EB0 File Offset: 0x000622B0
		private void EnterLevel()
		{
			if (this.island.levelNode.wantsTutorial)
			{
				this.states.root.SetActive(true);
				this.squadSpawner.SpawnEnglishSquads(from x in Profile.campaign.heroes
				where x.available
				select x);
			}
			else
			{
				this.states.loadout.SetActive(true);
			}
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00063F30 File Offset: 0x00062330
		private IEnumerator<GenInfo> PlayIslandRoutine()
		{
			this.CacheProfileStates(this.island.levelNode.campaign.campaignSave);
			IEnumerator<GenInfo> routine = this.island.PlayIsland();
			while (routine.MoveNext())
			{
				GenInfo genInfo = routine.Current;
				yield return genInfo;
			}
			foreach (IslandGameplayManager.ISetupIslandCoroutine setup in this.setupCoroutines)
			{
				IEnumerator r = setup.OnSetup(this.island);
				while (r.MoveNext())
				{
					yield return new GenInfo("IslandGamplayManager.SetupCoroutines", GenInfo.Mode.interruptable);
				}
			}
			foreach (IslandGameplayManager.ISetupIsland setup2 in this.setups)
			{
				setup2.OnSetup(this.island);
				yield return new GenInfo("IslandGamplayManager.Setups", GenInfo.Mode.interruptable);
			}
			yield break;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00063F4C File Offset: 0x0006234C
		private IEnumerator<GenInfo> ResetIslandRoutine()
		{
			CampaignSave campaignSave = this.island.levelNode.campaign.campaignSave;
			this.RestoreProfileStates(this.island.levelNode.campaign.campaignSave);
			yield return new GenInfo("Restore HeroDefs", GenInfo.Mode.interruptable);
			foreach (HeroDefinition hero in campaignSave.heroes)
			{
				IEnumerator enumerator2 = hero.monoHero.UpdateAliveSprite(hero.alive, true).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object r = enumerator2.Current;
						yield return new GenInfo("Updating MonoHeroes", GenInfo.Mode.interruptable);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator2 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			SuperUpgradeMenu.instance.RefreshHeroDefs();
			yield return new GenInfo("refreshing upgrades menu", GenInfo.Mode.interruptable);
			this.island.gameObject.SetActive(false);
			yield return new GenInfo("resetting island", GenInfo.Mode.nonInterupptable);
			this.island.gameObject.SetActive(true);
			yield return new GenInfo("resetting island", GenInfo.Mode.interruptable);
			IEnumerator<GenInfo> r2 = this.island.ResetIsland();
			while (r2.MoveNext())
			{
				GenInfo genInfo = r2.Current;
				yield return genInfo;
			}
			Profile.campaign.stats.levelRestarts++;
			Profile.userSave.stats.levelRestarts++;
			yield break;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00063F68 File Offset: 0x00062368
		public IEnumerator<GenInfo> ReplayIslandRoutine()
		{
			IEnumerator<GenInfo> r = this.WipeIslandRoutine();
			while (r.MoveNext())
			{
				GenInfo genInfo = r.Current;
				yield return genInfo;
			}
			IEnumerator<GenInfo> r2 = this.ResetIslandRoutine();
			while (r2.MoveNext())
			{
				GenInfo genInfo2 = r2.Current;
				yield return genInfo2;
			}
			this.states.root.SetActive(true);
			IEnumerator<GenInfo> r3 = this.PlayIslandRoutine();
			while (r3.MoveNext())
			{
				GenInfo genInfo3 = r3.Current;
				yield return genInfo3;
			}
			this.EnterLevel();
			yield break;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00063F84 File Offset: 0x00062384
		private IEnumerator<GenInfo> WipeIslandRoutine()
		{
			this.levelPauser.SetPause(false, false);
			this.states.root.SetActive(false);
			foreach (IslandGameplayManager.IWipeIsland setup in this.wipers)
			{
				setup.OnWipe(this.island);
				yield return default(GenInfo);
			}
			IEnumerator<GenInfo> r = this.island.WipeIsland();
			while (r.MoveNext())
			{
				GenInfo genInfo = r.Current;
				yield return genInfo;
			}
			AgentEnumerators.ClearStaticList();
			yield break;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00063FA0 File Offset: 0x000623A0
		public IEnumerator<GenInfo> LeaveIslandRoutine(bool reset)
		{
			IEnumerator<GenInfo> r = this.WipeIslandRoutine();
			while (r.MoveNext())
			{
				GenInfo genInfo = r.Current;
				yield return genInfo;
			}
			if (reset)
			{
				IEnumerator<GenInfo> r2 = this.ResetIslandRoutine();
				while (r2.MoveNext())
				{
					GenInfo genInfo2 = r2.Current;
					yield return genInfo2;
				}
			}
			IEnumerator<GenInfo> r3 = this.island.LeaveIsland();
			while (r3.MoveNext())
			{
				GenInfo genInfo3 = r3.Current;
				yield return genInfo3;
			}
			foreach (IslandGameplayManager.ILeaveIsland leaver in this.leavers)
			{
				leaver.OnLeave(this.island);
				yield return new GenInfo("Leaving island", GenInfo.Mode.interruptable);
			}
			this.ClearProfileStates();
			this.island.gameObject.SetActive(false);
			this._island.Target = null;
			yield break;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00063FC2 File Offset: 0x000623C2
		public static void RequestFootstepsAudio(FabricEventReference eventRef, GameObject gameObject)
		{
			Singleton<IslandGameplayManager>.instance.footstepsAudio.RequestEvent(eventRef, gameObject);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00063FD5 File Offset: 0x000623D5
		public static void RequestFootstepsAudio(string eventName, GameObject gameObject)
		{
			Singleton<IslandGameplayManager>.instance.footstepsAudio.RequestEvent(eventName, gameObject);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00063FE8 File Offset: 0x000623E8
		public static void RequestCombatAudio(FabricEventReference eventRef, GameObject gameObject)
		{
			Singleton<IslandGameplayManager>.instance.combatAudio.RequestEvent(eventRef.name, gameObject);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00064000 File Offset: 0x00062400
		public static void RequestCombatAudio(string eventName, GameObject gameObject)
		{
			Singleton<IslandGameplayManager>.instance.combatAudio.RequestEvent(eventName, gameObject);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00064013 File Offset: 0x00062413
		public static void RequestCombatAudio(string eventNameSuffix, string eventNamePrefix, GameObject gameObject)
		{
			Singleton<IslandGameplayManager>.instance.combatAudio.RequestEvent(eventNameSuffix, eventNamePrefix, gameObject);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00064027 File Offset: 0x00062427
		private void CacheProfileStates(CampaignSave campaignSave)
		{
			this.ClearProfileStates();
			this.formatter.Serialize(this.heroStateCache, campaignSave.heroes);
			this.formatter.Serialize(this.campaignStatsCache, campaignSave.stats);
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x00064060 File Offset: 0x00062460
		private void RestoreProfileStates(CampaignSave campaignSave)
		{
			this.heroStateCache.Position = 0L;
			List<HeroDefinition> list = (List<HeroDefinition>)this.formatter.Deserialize(this.heroStateCache);
			int i = 0;
			int count = campaignSave.heroes.Count;
			while (i < count)
			{
				list[i].monoHero = campaignSave.heroes[i].monoHero;
				list[i].monoHero.heroDef = list[i];
				i++;
			}
			campaignSave.heroes = list;
			this.campaignStatsCache.Position = 0L;
			campaignSave.stats = (CampaignStats)this.formatter.Deserialize(this.campaignStatsCache);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00064114 File Offset: 0x00062514
		private void ClearProfileStates()
		{
			this.heroStateCache.SetLength(0L);
			this.heroStateCache.Position = 0L;
			this.campaignStatsCache.SetLength(0L);
			this.campaignStatsCache.Position = 0L;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0006414C File Offset: 0x0006254C
		[DebugSetting("Win Level", "完成关卡", DebugSettingLocation.Level)]
		public static void WinLevel()
		{
			Singleton<IslandGameplayManager>.instance.levelPauser.SetPause(false, true);
			Singleton<IslandGameplayManager>.instance.island.painter.DebugPaint();
			foreach (VikingReference vikingReference in Singleton<IslandGameplayManager>.instance.island.levelNode.enemies)
			{
				Singleton<IslandGameplayManager>.instance.island.levelNode.campaign.campaignSave.Saw(vikingReference.type);
			}
			Singleton<IslandGameplayManager>.instance.endOfLevel.AllVikingsKilled();
			foreach (Agent agent in Singleton<IslandGameplayManager>.instance.island.vikings.agents)
			{
				agent.KillImmediate();
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00064260 File Offset: 0x00062660
		[DebugSetting("Lose Level", "关卡失败", DebugSettingLocation.Level)]
		public static void LoseLevel()
		{
			Singleton<IslandGameplayManager>.instance.levelPauser.SetPause(false, true);
			foreach (Agent agent in Singleton<IslandGameplayManager>.instance.island.english.agents)
			{
				agent.KillImmediate();
			}
			Singleton<IslandGameplayManager>.instance.endOfLevel.AllEnglishKilled();
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000642EC File Offset: 0x000626EC
		[DebugSetting("Exit Level", "退出关卡", DebugSettingLocation.Level)]
		public static void DBGExitLevel()
		{
			Singleton<IslandGameplayManager>.instance.levelLeaver.ExitLevel();
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000642FD File Offset: 0x000626FD
		[DebugSetting("Restart Level", "重玩关卡", DebugSettingLocation.Level)]
		public static void DBGRestartLevel()
		{
			Singleton<IslandGameplayManager>.instance.levelLeaver.ReplayLevel();
		}

		// Token: 0x040014FC RID: 5372
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("LevelIslandManager", EVerbosity.Quiet, 0);

		// Token: 0x0400150C RID: 5388
		[SerializeField]
		private CameraScreenshotter _userScreenshotter;

		// Token: 0x0400150D RID: 5389
		[SerializeField]
		private AudioEventBuffer _footstepsAudio;

		// Token: 0x0400150E RID: 5390
		[SerializeField]
		private AudioEventBuffer _combatAudio;

		// Token: 0x0400150F RID: 5391
		private WeakReference<Island> _island = new WeakReference<Island>(null);

		// Token: 0x04001510 RID: 5392
		private IslandGameplayManager.ISetupIslandCoroutine[] setupCoroutines;

		// Token: 0x04001511 RID: 5393
		private IslandGameplayManager.ISetupIsland[] setups;

		// Token: 0x04001512 RID: 5394
		private IslandGameplayManager.IWipeIsland[] wipers;

		// Token: 0x04001513 RID: 5395
		private IslandGameplayManager.ILeaveIsland[] leavers;

		// Token: 0x04001514 RID: 5396
		private SmartShuffler<string> richPresenseScreenshots;

		// Token: 0x04001515 RID: 5397
		private MemoryStream heroStateCache = new MemoryStream();

		// Token: 0x04001516 RID: 5398
		private MemoryStream campaignStatsCache = new MemoryStream();

		// Token: 0x04001517 RID: 5399
		private BinaryFormatter formatter = new BinaryFormatter();

		// Token: 0x0200052C RID: 1324
		public interface IAwake
		{
			// Token: 0x0600228F RID: 8847
			void OnAwake(IslandGameplayManager manager);
		}

		// Token: 0x0200052D RID: 1325
		public interface ISetupIsland
		{
			// Token: 0x06002290 RID: 8848
			void OnSetup(Island island);
		}

		// Token: 0x0200052E RID: 1326
		public interface ISetupIslandCoroutine
		{
			// Token: 0x06002291 RID: 8849
			IEnumerator OnSetup(Island island);
		}

		// Token: 0x0200052F RID: 1327
		public interface IWipeIsland
		{
			// Token: 0x06002292 RID: 8850
			void OnWipe(Island island);
		}

		// Token: 0x02000530 RID: 1328
		public interface ILeaveIsland
		{
			// Token: 0x06002293 RID: 8851
			void OnLeave(Island island);
		}

		// Token: 0x02000531 RID: 1329
		public interface IExitCampaign
		{
			// Token: 0x06002294 RID: 8852
			void OnExitCampaign(CampaignManager campaignManager, Campaign campaign);
		}
	}
}
