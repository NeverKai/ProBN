using System;
using System.Diagnostics;
using CS.Platform;
using ReflexCLI.User;
using Rewired;
using RTM.Input;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000536 RID: 1334
	public class LevelPauser : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00066724 File Offset: 0x00064B24
		public bool isPaused
		{
			get
			{
				return this._isPaused;
			}
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060022C4 RID: 8900 RVA: 0x0006672C File Offset: 0x00064B2C
		// (remove) Token: 0x060022C5 RID: 8901 RVA: 0x00066764 File Offset: 0x00064B64
		
		public event Action<bool> onPauseChanged = delegate(bool A_0)
		{
		};

		// Token: 0x060022C6 RID: 8902 RVA: 0x0006679C File Offset: 0x00064B9C
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.gameplayManager = manager;
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			foreach (string text in commandLineArgs)
			{
				if (text.ToLower() == "noautopause")
				{
					UnityEngine.Debug.Log("Disabling auto-pause behaviour");
					this.autoPause = false;
					return;
				}
			}
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000667F7 File Offset: 0x00064BF7
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.SetPause_Internal(false);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x00066800 File Offset: 0x00064C00
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.SetPause_Internal(false);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00066809 File Offset: 0x00064C09
		private void Awake()
		{
			this.rwPlayer = ReInput.players.GetPlayer(this.rwPlayerId);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00066821 File Offset: 0x00064C21
		private void OnEnable()
		{
			InputHelpers.OnActiveControllerLost += this.OnActiveControllerLost;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x00066834 File Offset: 0x00064C34
		private void OnDisable()
		{
			InputHelpers.OnActiveControllerLost -= this.OnActiveControllerLost;
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x00066847 File Offset: 0x00064C47
		public void Update()
		{
			this.CheckPauseInputs();
			this.wasShowingMessage = BasePlatformManager.Instance.IsShowingMessage();
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x00066860 File Offset: 0x00064C60
		private void SetPause_Internal(bool pause)
		{
			if (this._isPaused == pause || this.pauseFrame == Time.frameCount)
			{
				return;
			}
			this._isPaused = pause;
			if (pause)
			{
				TimeManager.RequestTimeScale(this, 0f);
				this.gameplayManager.states.pause.SetActive(this._isPaused);
				this.onPauseChanged(this._isPaused);
			}
			else
			{
				this.onPauseChanged(this._isPaused);
				this.gameplayManager.states.pause.SetActive(this._isPaused);
				TimeManager.RemoveTimeScale(this);
			}
			foreach (House house in this.gameplayManager.island.village.houses)
			{
				house.SetSuppressAudio(this._isPaused);
			}
			this.pauseFrame = Time.frameCount;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0006694A File Offset: 0x00064D4A
		public void SetPause(bool pause, bool uiAudio = true)
		{
			if (uiAudio)
			{
				FabricWrapper.PostEvent((!pause) ? "UI/InGame/Resume" : "UI/InGame/Pause");
			}
			this.SetPause_Internal(pause);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00066974 File Offset: 0x00064D74
		private void CheckPauseInputs()
		{
			if (ConsoleStatus.IsConsoleOpen())
			{
				return;
			}
			if (this.isPaused)
			{
				if (this.wasShowingMessage && !BasePlatformManager.Instance.IsShowingMessage())
				{
					this.SetPause(false, true);
				}
				if (!BasePlatformManager.Instance.IsShowingMessage() && this.rwPlayer.GetButtonDown(this.unpauseButton))
				{
					this.SetPause(false, true);
				}
				if (this.wasGUIActiveOnPause && !BasePlatformManager.Instance.GUIActive)
				{
					this.SetPause(false, true);
				}
			}
			else if (this.CanPause())
			{
				if (this.rwPlayer.GetButtonDown(this.pauseButton))
				{
					this.SetPause(true, true);
				}
				if (BasePlatformManager.Instance.GUIActive)
				{
					this.wasGUIActiveOnPause = true;
					this.SetPause(true, true);
				}
				else
				{
					this.wasGUIActiveOnPause = false;
				}
			}
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00066A6A File Offset: 0x00064E6A
		private bool CanPause()
		{
			return !this.isPaused;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00066A75 File Offset: 0x00064E75
		private void OnActiveControllerLost()
		{
			this.SetPause(true, true);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x00066A7F File Offset: 0x00064E7F
		public void OnApplicationFocus(bool focus)
		{
			if (!focus && this.autoPause)
			{
				this.SetPause_Internal(true);
			}
		}

		// Token: 0x04001535 RID: 5429
		[SerializeField]
		private int rwPlayerId;

		// Token: 0x04001536 RID: 5430
		private Player rwPlayer;

		// Token: 0x04001537 RID: 5431
		private RewiredActionReference pauseButton = "Pause";

		// Token: 0x04001538 RID: 5432
		private RewiredActionReference unpauseButton = "Unpause";

		// Token: 0x04001539 RID: 5433
		private bool _isPaused;

		// Token: 0x0400153A RID: 5434
		private IslandGameplayManager gameplayManager;

		// Token: 0x0400153B RID: 5435
		private int pauseFrame = -2147483647;

		// Token: 0x0400153C RID: 5436
		private bool autoPause = true;

		// Token: 0x0400153D RID: 5437
		private bool wasGUIActiveOnPause;

		// Token: 0x0400153E RID: 5438
		private bool wasShowingMessage;
	}
}
