using System;
using System.Collections;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.UI;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.Tutorial
{
	// Token: 0x0200053F RID: 1343
	public class StartLevelTutorial : MonoBehaviour, IslandUIManager.IAwake, IslandGameplayManager.ISetupIslandCoroutine, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x06002303 RID: 8963 RVA: 0x00068A37 File Offset: 0x00066E37
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.uiManager = manager;
			this.gameplayManager = this.uiManager.gameplayManager;
			this.notifications = this.gameplayManager.notificationManager;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x00068A62 File Offset: 0x00066E62
		private float dt
		{
			get
			{
				return (!this.gameplayManager.levelPauser.isPaused) ? Time.unscaledDeltaTime : 0f;
			}
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00068A88 File Offset: 0x00066E88
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			this.config = default(StartLevelTutorial.Config);
			this.config.island = island;
			this.config.inputConfig = this.SelectInputConfig();
			this.config.skipTutorial = island.levelNode.campaign.campaignSave.prefs.skipTutorial;
			this.cameraTutorial = this.CameraTutorial();
			this.selectTutorial = this.SelectTutorial();
			this.defendHousesTutorial = this.DefendHousesTutorial();
			this.teaseCoinsRoutine = this.gameplayManager.coinDispenser.TeaseCoinsRoutine(2f);
			this.teaseCoinsRoutine.MoveNext();
			yield return null;
			yield break;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00068AAC File Offset: 0x00066EAC
		private StartLevelTutorialDef SelectInputConfig()
		{
			if (Platform.Is(EPlatform.Console))
			{
				return this.SelectJoystickConfig();
			}
			if (InputHelpers.ControllerTypeIs(ControllerType.Joystick))
			{
				return this.SelectJoystickConfig();
			}
			if (Profile.userSettings.cursorBehaviour == UserSettings.CursorBehaviour.Touch)
			{
				return this.touchConfig;
			}
			return this.mouseConfig;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00068B00 File Offset: 0x00066F00
		private StartLevelTutorialDef SelectJoystickConfig()
		{
			UserSettings.GamepadLayout gamepadLayout = Profile.userSettings.gamepadLayout;
			return (gamepadLayout != UserSettings.GamepadLayout.QuickSelect) ? this.joystickConfig : this.altJoystickConfig;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00068B30 File Offset: 0x00066F30
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.config = default(StartLevelTutorial.Config);
			this.cameraTutorial = null;
			this.selectTutorial = null;
			this.defendHousesTutorial = null;
			this.teaseCoinsRoutine = null;
			this.switchTouchTutorial = null;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00068B70 File Offset: 0x00066F70
		private void OnEnable()
		{
			this.config.archerSquad = (this.gameplayManager.squadSelector.squads[0] as EnglishSquad);
			this.config.infantrySquad = (this.gameplayManager.squadSelector.squads[1] as EnglishSquad);
			this.config.archerNavSpot = this.config.archerSquad.navSpotOccupant.navSpot;
			this.config.infantryNavSpot = this.config.infantrySquad.navSpotOccupant.navSpot;
			base.StartCoroutine(this.TutorialCoroutine());
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x00068C18 File Offset: 0x00067018
		private IEnumerator TutorialCoroutine()
		{
			this.titleState.SetActive(true);
			for (float t = 0f; t < 3f; t += this.dt)
			{
				yield return null;
			}
			this.titleState.SetActive(false);
			if (!this.config.skipTutorial)
			{
				for (float t2 = 0f; t2 < 0.05f; t2 += this.dt)
				{
					yield return null;
				}
				yield return this.cameraTutorial;
				yield return this.selectTutorial;
				yield return this.switchTouchTutorial;
				yield return this.defendHousesTutorial;
			}
			this.gameplayManager.CallFirstWave();
			yield return null;
			yield break;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00068C33 File Offset: 0x00067033
		private void SuccessAudio()
		{
			FabricWrapper.PostEvent(this.successAudioId);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00068C44 File Offset: 0x00067044
		private IEnumerator CameraTutorial()
		{
			IslandUINotification notification = null;
			LevelCamera camera = this.uiManager.gameplayManager.levelCamera;
			notification = this.PostMessage(this.config.inputConfig.cameraMove);
			float startYaw = camera.yaw;
			float currYaw = startYaw;
			float cummYaw = 0f;
			while (cummYaw < 90f)
			{
				cummYaw += Mathf.Abs(Mathf.DeltaAngle(currYaw, camera.yaw));
				currYaw = camera.yaw;
				yield return null;
			}
			notification.Close();
			this.SuccessAudio();
			for (float t = 0f; t < 0.5f; t += this.dt)
			{
				yield return null;
			}
			notification = this.PostMessage(this.config.inputConfig.cameraZoom);
			float startZoom = camera.GetOrthoWidth();
			float currZoom = startZoom;
			float cummZoom = 0f;
			while (cummZoom < 0.3f)
			{
				cummZoom += Mathf.Abs(1f - camera.GetOrthoWidth() / currZoom);
				currZoom = camera.GetOrthoWidth();
				yield return null;
			}
			notification.Close();
			this.SuccessAudio();
			for (float t2 = 0f; t2 < 0.5f; t2 += this.dt)
			{
				yield return null;
			}
			yield return null;
			yield break;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00068C60 File Offset: 0x00067060
		private IEnumerator SelectTutorial()
		{
			IslandUINotification notification = null;
			SquadSelector squadSelector = this.gameplayManager.squadSelector;
			this.selectableState.SetActive(true);
			bool mustSelectBoth = this.config.inputConfig.mustSelectBoth;
			for (;;)
			{
				IL_86:
				bool archersMoved = this.config.archerSquad.navSpotOccupant.navSpot != this.config.archerNavSpot;
				if (this.config.infantrySquad.navSpotOccupant.navSpot != this.config.infantryNavSpot || archersMoved)
				{
					break;
				}
				for (float t = 0f; t < 0.25f; t += this.dt)
				{
					yield return null;
				}
				notification = this.PostMessage(this.config.inputConfig.selectSquad);
				while (squadSelector.selectedSquad == null)
				{
					yield return null;
				}
				if (mustSelectBoth)
				{
					bool selectedArchers = false;
					bool selectedInfantry = false;
					while (!selectedInfantry || !selectedArchers)
					{
						yield return null;
						if (squadSelector.selectedSquad == null)
						{
							notification.Close();
							goto IL_86;
						}
						selectedInfantry |= (squadSelector.selectedSquad == this.config.infantrySquad);
						selectedArchers |= (squadSelector.selectedSquad == this.config.archerSquad);
					}
				}
				mustSelectBoth = false;
				notification.Close();
				this.SuccessAudio();
				for (float t2 = 0f; t2 < 0.4f; t2 += this.dt)
				{
					yield return null;
				}
				EnglishSquad selectedSquad = squadSelector.selectedSquad;
				if (selectedSquad)
				{
					if (!string.IsNullOrEmpty(this.config.inputConfig.moveCursor.messageTerm))
					{
						notification = this.PostMessage(this.config.inputConfig.moveCursor);
						for (;;)
						{
							selectedSquad = squadSelector.selectedSquad;
							if (!selectedSquad)
							{
								break;
							}
							NavSpot currNavSpot = selectedSquad.navSpotOccupant.navSpot;
							JoystickMoveAbility jsMove = selectedSquad.upgradeManager.GetUpgrade<JoystickMoveAbility>();
							NavSpot targetNavSpot = jsMove.GetNavSpotTarget();
							if (targetNavSpot)
							{
								Vector3 vector = currNavSpot.transform.position - targetNavSpot.transform.position;
								if (Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.z)) > 1.5f)
								{
									goto Block_12;
								}
							}
							yield return null;
						}
						notification.Close();
						continue;
						Block_12:
						notification.Close();
						this.SuccessAudio();
						for (float t3 = 0f; t3 < 0.3f; t3 += this.dt)
						{
							yield return null;
						}
					}
					notification = this.PostMessage(this.config.inputConfig.moveSquad);
					NavSpot navSpot = selectedSquad.navSpotOccupant.navSpot;
					while (selectedSquad.navSpotOccupant.navSpot == navSpot)
					{
						yield return null;
						if (selectedSquad.navSpotOccupant.navSpot != navSpot)
						{
							break;
						}
						if (!squadSelector.selectedSquad)
						{
							notification.Close();
							goto IL_86;
						}
						if (squadSelector.selectedSquad != selectedSquad)
						{
							selectedSquad = squadSelector.selectedSquad;
							navSpot = selectedSquad.navSpotOccupant.navSpot;
						}
					}
					goto IL_5CA;
				}
			}
			goto IL_5D5;
			IL_5CA:
			notification.Close();
			IL_5D5:
			this.SuccessAudio();
			for (float t4 = 0f; t4 < 1f; t4 += this.dt)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00068C7C File Offset: 0x0006707C
		private IEnumerator DefendHousesTutorial()
		{
			IslandUINotification notification = this.notifications.PostMessage("TUTORIAL/GENERIC/DEFEND_HOUSES", IslandUINotification.Priority.Tutorial, 0f);
			yield return this.teaseCoinsRoutine;
			for (float t = 0f; t < 2.5f; t += this.dt)
			{
				yield return null;
			}
			notification.Close();
			for (float t2 = 0f; t2 < 0.5f; t2 += this.dt)
			{
				yield return null;
			}
			yield return null;
			yield break;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00068C98 File Offset: 0x00067098
		private IslandUINotification PostMessage(Notification notification)
		{
			return this.gameplayManager.notificationManager.PostMessage(notification, IslandUINotification.Priority.Tutorial, this.icons.GetSprite(notification.icon), this.icons.GetSprite(notification.secondaryIcon), false, 0f);
		}

		// Token: 0x04001568 RID: 5480
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("StartLevelTutorial", EVerbosity.Quiet, 100);

		// Token: 0x04001569 RID: 5481
		[SerializeField]
		private State titleState;

		// Token: 0x0400156A RID: 5482
		[SerializeField]
		private State selectableState;

		// Token: 0x0400156B RID: 5483
		[SerializeField]
		private TutorialIcons icons;

		// Token: 0x0400156C RID: 5484
		[Header("config")]
		[SerializeField]
		private StartLevelTutorialDef mouseConfig;

		// Token: 0x0400156D RID: 5485
		[SerializeField]
		private StartLevelTutorialDef touchConfig;

		// Token: 0x0400156E RID: 5486
		[SerializeField]
		private StartLevelTutorialDef joystickConfig;

		// Token: 0x0400156F RID: 5487
		[SerializeField]
		private StartLevelTutorialDef altJoystickConfig;

		// Token: 0x04001570 RID: 5488
		private IslandUIManager uiManager;

		// Token: 0x04001571 RID: 5489
		private IslandGameplayManager gameplayManager;

		// Token: 0x04001572 RID: 5490
		private IslandUINotificationManager notifications;

		// Token: 0x04001573 RID: 5491
		private FabricEventReference successAudioId = "UI/InGame/TutorialCorrect";

		// Token: 0x04001574 RID: 5492
		private IEnumerator cameraTutorial;

		// Token: 0x04001575 RID: 5493
		private IEnumerator selectTutorial;

		// Token: 0x04001576 RID: 5494
		private IEnumerator teaseCoinsRoutine;

		// Token: 0x04001577 RID: 5495
		private IEnumerator defendHousesTutorial;

		// Token: 0x04001578 RID: 5496
		private IEnumerator switchTouchTutorial;

		// Token: 0x04001579 RID: 5497
		private StartLevelTutorial.Config config;

		// Token: 0x0400157A RID: 5498
		private const IslandUINotification.Priority prio = IslandUINotification.Priority.Tutorial;

		// Token: 0x02000540 RID: 1344
		private struct Config
		{
			// Token: 0x0400157B RID: 5499
			public bool skipTutorial;

			// Token: 0x0400157C RID: 5500
			public StartLevelTutorialDef inputConfig;

			// Token: 0x0400157D RID: 5501
			public Island island;

			// Token: 0x0400157E RID: 5502
			public EnglishSquad archerSquad;

			// Token: 0x0400157F RID: 5503
			public EnglishSquad infantrySquad;

			// Token: 0x04001580 RID: 5504
			public NavSpot archerNavSpot;

			// Token: 0x04001581 RID: 5505
			public NavSpot infantryNavSpot;
		}
	}
}
