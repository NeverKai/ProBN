using System;
using System.Collections;
using Fabric;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.Audio;
using Voxels.TowerDefense.WorldEnvironment;

namespace Voxels.TowerDefense
{
	// Token: 0x02000029 RID: 41
	public class IslandAmbiance : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000584A File Offset: 0x00003C4A
		public Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005857 File Offset: 0x00003C57
		public EnvironmentManager environment
		{
			get
			{
				return Singleton<EnvironmentManager>.instance;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000585E File Offset: 0x00003C5E
		public WorldWeather.WeatherSystem weather
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000586A File Offset: 0x00003C6A
		public Year year
		{
			get
			{
				return this.environment.year;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005878 File Offset: 0x00003C78
		public Season season
		{
			get
			{
				return this.environment.year.season;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005898 File Offset: 0x00003C98
		public Month month
		{
			get
			{
				return this.environment.year.month;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000058B8 File Offset: 0x00003CB8
		public float hour
		{
			get
			{
				return this.environment.day.hour;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000058CC File Offset: 0x00003CCC
		public float wind
		{
			get
			{
				return this.weather.wind;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000058E8 File Offset: 0x00003CE8
		public float cloudCoverage
		{
			get
			{
				return this.weather.cloudCoverage;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005904 File Offset: 0x00003D04
		public float rain
		{
			get
			{
				return this.weather.rainfall;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005920 File Offset: 0x00003D20
		public float snowFall
		{
			get
			{
				return this.weather.snowfall;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000593C File Offset: 0x00003D3C
		public float snowCoverage
		{
			get
			{
				return this.weather.snowCoverage;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005958 File Offset: 0x00003D58
		private void Update()
		{
			if (Mathf.Abs(this.prevWind - this.wind) > 0.05f)
			{
				EventManager.Instance.SetParameter(this.windAmb, "Intensity", this.wind, null);
				this.prevWind = this.wind;
			}
			bool flag = false;
			bool flag2 = !string.IsNullOrEmpty(this.battleMusic);
			foreach (Agent agent in this.island.vikings.agents)
			{
				VikingAgent component = agent.GetComponent<VikingAgent>();
				if (component.type == VikingAgent.Type.Tank)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				if (!flag2)
				{
					this.StartBattleMusic("Mus/WhileFighting");
				}
			}
			else if (flag2)
			{
				this.EndBattleMusic();
			}
			this.waterfallUpdate.MoveNext();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005A6C File Offset: 0x00003E6C
		private IEnumerator WaterfallUpdate()
		{
			float duckDelta = 1f - this.waterfallDuckVolume;
			float duckInSpeed = duckDelta / this.waterfallDuckInTime;
			float duckOutSpeed = duckDelta / this.waterfallDuckOutTime;
			WaterfallSplash[] waterfalls = this.island.GetComponentsInChildren<WaterfallSplash>(true);
			if (waterfalls.Length <= 0)
			{
				yield break;
			}
			FabricWrapper.PostEvent(this.waterfallAmb);
			LevelCamera levelCamera = this.manager.levelCamera;
			foreach (WaterfallSplash waterfallSplash in waterfalls)
			{
				waterfallSplash.visibility = WaterfallSplash.Visibility.Hidden;
			}
			float materVol = 1f;
			for (;;)
			{
				foreach (WaterfallSplash waterfall in waterfalls)
				{
					WaterfallSplash.Visibility vis = waterfall.visibility;
					waterfall.UpdateVisibility(levelCamera.cameraRef);
					float targetVol = (!this.waterfallDucked) ? 1f : this.waterfallDuckVolume;
					materVol = Mathf.MoveTowards(materVol, targetVol, Time.unscaledDeltaTime * ((targetVol >= materVol) ? duckOutSpeed : duckInSpeed));
					this.UpdateWaterfallVolume(waterfalls, materVol);
					yield return null;
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005A88 File Offset: 0x00003E88
		private void UpdateWaterfallVolume(WaterfallSplash[] waterfalls, float volumeMultiplier)
		{
			float num = 0f;
			foreach (WaterfallSplash waterfallSplash in waterfalls)
			{
				num += waterfallSplash.volume;
			}
			num *= volumeMultiplier;
			if (this.prevWaterfallVolume != num)
			{
				EventManager.Instance.SetParameter(this.waterfallAmb, "Intensity", num, null);
				this.prevWaterfallVolume = num;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005AF4 File Offset: 0x00003EF4
		private void OnEnable()
		{
			this.InvokeRepeating(new Action(this.TriggerIntroMusic), this.introMusicDelayTime, this.introMusicRepeatTime);
			FabricWrapper.PostEvent(this.windAmb);
			EventManager.Instance.SetParameter(this.windAmb, "Intensity", this.wind, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005B4D File Offset: 0x00003F4D
		private void OnDisable()
		{
			this.EndIntroMusic();
			this.EndBattleMusic();
			FabricWrapper.PostEvent(this.windAmb, EventAction.StopSound);
			this.unpaused.TransitionTo(0.1f);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005B78 File Offset: 0x00003F78
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.manager = manager;
			manager.states.running.OnActivate += this.OnCallFirstWave;
			manager.states.running.OnDeactivate += delegate()
			{
				this.waterfallDucked = false;
			};
			manager.states.EndOfLevel.OnChange += this.EndOfLevel;
			TimeManager.onTimeScaleChanged += this.OnTimeScaleChanged;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005BF4 File Offset: 0x00003FF4
		private void OnTimeScaleChanged(float timeScale)
		{
			EventManager.Instance.SetParameter(this.slomoAmbiance, "Slow", (timeScale <= 0f || timeScale >= 1f) ? 0f : 1f, null);
			if (CinematicCamera.isActive || !base.isActiveAndEnabled)
			{
				return;
			}
			if (timeScale < 1f)
			{
				this.paused.TransitionTo(0.1f);
			}
			else
			{
				this.unpaused.TransitionTo(0.1f);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005C88 File Offset: 0x00004088
		private void EndOfLevel(bool active)
		{
			EventAction eventAction = (!active) ? EventAction.StopSound : EventAction.PlaySound;
			switch (this.manager.endOfLevel.reason)
			{
			case Voxels.TowerDefense.EndOfLevel.Reason.Won:
				FabricWrapper.PostEvent(this.winMusic, eventAction);
				break;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005CE4 File Offset: 0x000040E4
		private void OnCallFirstWave()
		{
			this.EndIntroMusic();
			this.waterfallDucked = true;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005CF4 File Offset: 0x000040F4
		private string SelectIntroMusic()
		{
			string text = string.Empty;
			if (this.island.levelNode.isEnd)
			{
				text = "Mus/LastWorldMusic";
			}
			else if (this.island.levelNode.checkpointAvailable)
			{
				text = "Mus/Checkpoint";
			}
			else
			{
				text = this.island.levelNode.levelState.GetReferencedString(LevelObjectReference.Key.IntroMusic);
			}
			return (!string.IsNullOrEmpty(text)) ? text : "Mus/GreenMeadows";
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005D74 File Offset: 0x00004174
		private void EndIntroMusic()
		{
			if (!string.IsNullOrEmpty(this.introMusic))
			{
				FabricWrapper.PostEvent(this.introMusic, EventAction.StopSound);
				this.introMusic = string.Empty;
			}
			this.CancelInvoke(new Action(this.TriggerIntroMusic));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005DB0 File Offset: 0x000041B0
		private void TriggerIntroMusic()
		{
			this.introMusic = this.SelectIntroMusic();
			FabricWrapper.PostEvent(this.introMusic);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005DCA File Offset: 0x000041CA
		private void StartBattleMusic(string newBattleMusic)
		{
			this.EndBattleMusic();
			this.battleMusic = newBattleMusic;
			FabricWrapper.PostEvent(this.battleMusic);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005DE5 File Offset: 0x000041E5
		private void EndBattleMusic()
		{
			if (!string.IsNullOrEmpty(this.battleMusic))
			{
				FabricWrapper.PostEvent(this.battleMusic, EventAction.StopSound);
			}
			this.battleMusic = string.Empty;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005E10 File Offset: 0x00004210
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
			this.waterfallDucked = false;
			this.waterfallUpdate = this.WaterfallUpdate();
			this.waterfallUpdate.MoveNext();
			FabricWrapper.PostEvent(this.slomoAmbiance);
			EventManager.Instance.SetParameter(this.slomoAmbiance, "Slow", 0f, null);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005E75 File Offset: 0x00004275
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this._island.Target = null;
			this.waterfallUpdate = null;
			FabricWrapper.PostEvent(this.waterfallAmb, EventAction.StopSound);
			FabricWrapper.PostEvent(this.slomoAmbiance, EventAction.StopSound);
		}

		// Token: 0x04000055 RID: 85
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("IslandAmbiance", EVerbosity.Quiet, 100);

		// Token: 0x04000056 RID: 86
		[SerializeField]
		private AudioMixerSnapshot paused;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		private AudioMixerSnapshot unpaused;

		// Token: 0x04000058 RID: 88
		[SerializeField]
		private float waterfallDuckVolume = 0.5f;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private float waterfallDuckInTime = 4f;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private float waterfallDuckOutTime = 4f;

		// Token: 0x0400005B RID: 91
		private bool waterfallDucked;

		// Token: 0x0400005C RID: 92
		private FabricEventReference winMusic = "Mus/ResultScreen";

		// Token: 0x0400005D RID: 93
		private FabricEventReference waterfallAmb = "Amb/Waterfall";

		// Token: 0x0400005E RID: 94
		private FabricEventReference windAmb = "Amb/Wind";

		// Token: 0x0400005F RID: 95
		private FabricEventReference slomoAmbiance = "Amb/Slow";

		// Token: 0x04000060 RID: 96
		private float introMusicDelayTime;

		// Token: 0x04000061 RID: 97
		private float introMusicRepeatTime = 60f;

		// Token: 0x04000062 RID: 98
		private RTM.Utilities.WeakReference<Island> _island = new RTM.Utilities.WeakReference<Island>(null);

		// Token: 0x04000063 RID: 99
		private IslandGameplayManager manager;

		// Token: 0x04000064 RID: 100
		private string introMusic = string.Empty;

		// Token: 0x04000065 RID: 101
		private string battleMusic = string.Empty;

		// Token: 0x04000066 RID: 102
		private IEnumerator waterfallUpdate;

		// Token: 0x04000067 RID: 103
		private float prevWind = -1f;

		// Token: 0x04000068 RID: 104
		private float prevWaterfallVolume = -1f;
	}
}
