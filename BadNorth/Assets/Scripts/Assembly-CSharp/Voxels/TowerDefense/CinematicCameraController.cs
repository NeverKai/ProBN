using System;
using System.Collections.Generic;
using ReflexCLI.Attributes;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006BF RID: 1727
	[ConsoleCommandClassCustomizer("CinematicCamera")]
	public class CinematicCameraController : LevelCameraComponent, IGameSetup
	{
		// Token: 0x06002CBB RID: 11451 RVA: 0x000A6A00 File Offset: 0x000A4E00
		void IGameSetup.OnGameAwake()
		{
			CinematicCameraController.instance = this;
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000A6A08 File Offset: 0x000A4E08
		private void Start()
		{
			this.rwPlayer = ReInput.players.GetPlayer(this.rwPlayerId);
			if (ReInput.controllers.joystickCount < 1)
			{
				Debug.Log("Need to have a gamepad connected for cinematic camera");
			}
			else if (ReInput.controllers.joystickCount > 1)
			{
				IList<Joystick> joysticks = ReInput.controllers.Joysticks;
				for (int i = 0; i < joysticks.Count; i++)
				{
					Joystick joystick = joysticks[i];
					if (!ReInput.controllers.IsControllerAssigned(joystick.type, joystick.id))
					{
						this.rwPlayer.controllers.AddController(joystick, true);
						break;
					}
				}
			}
			else
			{
				Player.ControllerHelper controllers = this.rwPlayer.controllers;
				controllers.AddController<Joystick>(0, true);
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000A6AD0 File Offset: 0x000A4ED0
		protected override void UpdateInternal()
		{
			if (this.rwPlayer.GetButtonDown(this.cameraRtzId))
			{
				this.rtz_Y = (this.rtz_XZ = true);
			}
			if (this.rwPlayer.GetButtonDown(this.pauseId))
			{
				Singleton<IslandGameplayManager>.instance.levelPauser.SetPause(!Singleton<IslandGameplayManager>.instance.levelPauser.isPaused, true);
			}
			if (this.rwPlayer.GetButtonDown(this.slomoId))
			{
				if (TimeManager.HasTimeScaleRequest(this))
				{
					TimeManager.RemoveTimeScale(this);
				}
				else
				{
					TimeManager.RequestTimeScale(this, this.slomoRatio);
				}
			}
			Vector3 vector = new Vector3(this.rwPlayer.GetAxis(this.panCameraXAxisId), this.rwPlayer.GetAxis("CameraPanVertical"), this.rwPlayer.GetAxis(this.panCameraZAxisId));
			vector = Vector3.Scale(vector, this.panSpeed);
			if (vector.x != 0f || vector.y != 0f)
			{
				this.rtz_XZ = false;
			}
			if (vector.y != 0f)
			{
				this.rtz_Y = false;
			}
			Vector3 rotatedVectorHorizontal = base.levelCamera.GetRotatedVectorHorizontal(vector);
			Vector3 b = rotatedVectorHorizontal * base.deltaTime;
			base.levelCamera.position += b;
			if (this.rtz_XZ)
			{
				Vector3 vector2 = new Vector3(0f, (!this.rtz_Y) ? base.levelCamera.position.y : 0f, 0f);
				base.levelCamera.position = Vector3.Lerp(base.levelCamera.position, vector2, 7f * Time.unscaledDeltaTime);
				base.levelCamera.position = Vector3.MoveTowards(base.levelCamera.position, vector2, 0.25f * Time.unscaledDeltaTime);
			}
			float num = -this.rwPlayer.GetAxis("CameraZoomAnalog");
			if (num != 0f)
			{
				this.zoomer.ZoomBy(num * this.zoomSpeed);
			}
			float num2 = -this.rwPlayer.GetAxis("CameraYaw");
			this.rotator.SetInput(num2 * this.rotateSpeed);
			if (num2 != 0f)
			{
				this.seeker.EndSeek();
			}
			else if (this.rwPlayer.GetButtonDown(this.cameraSnapId))
			{
				this.seeker.SeekToLatch(-1f, 2);
			}
			else if (this.rwPlayer.GetNegativeButtonDown(this.cameraSnapId))
			{
				this.seeker.SeekToLatch(1f, 2);
			}
			else if (this.rwPlayer.GetButtonDown(this.cameraSnap45Id))
			{
				this.seeker.SeekToLatch(-1f, 1);
			}
			else if (this.rwPlayer.GetNegativeButtonDown(this.cameraSnap45Id))
			{
				this.seeker.SeekToLatch(1f, 1);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000A6E12 File Offset: 0x000A5212
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x000A6E1E File Offset: 0x000A521E
		[ConsoleCommand("")]
		private static Vector3 PanSpeed
		{
			get
			{
				return CinematicCameraController.instance.panSpeed;
			}
			set
			{
				CinematicCameraController.instance.panSpeed = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000A6E2B File Offset: 0x000A522B
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x000A6E3C File Offset: 0x000A523C
		[ConsoleCommand("")]
		private static float XPanSpeed
		{
			get
			{
				return CinematicCameraController.instance.panSpeed.x;
			}
			set
			{
				CinematicCameraController.instance.panSpeed.x = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x000A6E4E File Offset: 0x000A524E
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000A6E5F File Offset: 0x000A525F
		[ConsoleCommand("")]
		private static float YPanSpeed
		{
			get
			{
				return CinematicCameraController.instance.panSpeed.y;
			}
			set
			{
				CinematicCameraController.instance.panSpeed.y = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000A6E71 File Offset: 0x000A5271
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x000A6E82 File Offset: 0x000A5282
		[ConsoleCommand("")]
		private static float ZPanSpeed
		{
			get
			{
				return CinematicCameraController.instance.panSpeed.z;
			}
			set
			{
				CinematicCameraController.instance.panSpeed.z = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000A6E94 File Offset: 0x000A5294
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x000A6EA0 File Offset: 0x000A52A0
		[ConsoleCommand("")]
		private static float RotateSpeed
		{
			get
			{
				return CinematicCameraController.instance.rotateSpeed;
			}
			set
			{
				CinematicCameraController.instance.rotateSpeed = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000A6EAD File Offset: 0x000A52AD
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x000A6EB9 File Offset: 0x000A52B9
		[ConsoleCommand("")]
		private static float ZoomSpeed
		{
			get
			{
				return CinematicCameraController.instance.zoomSpeed;
			}
			set
			{
				CinematicCameraController.instance.zoomSpeed = value;
			}
		}

		// Token: 0x04001D5B RID: 7515
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("cineCam", EVerbosity.Quiet, 0);

		// Token: 0x04001D5C RID: 7516
		private static CinematicCameraController instance;

		// Token: 0x04001D5D RID: 7517
		[Header("Component References")]
		[SerializeField]
		private CameraRotator rotator;

		// Token: 0x04001D5E RID: 7518
		[SerializeField]
		private CameraZoomer zoomer;

		// Token: 0x04001D5F RID: 7519
		[SerializeField]
		private CameraSeeker seeker;

		// Token: 0x04001D60 RID: 7520
		[Header("Tuning")]
		private Vector3 panSpeed = new Vector3(2f, 2f, 2f);

		// Token: 0x04001D61 RID: 7521
		[SerializeField]
		[Range(0f, 1f)]
		private float slomoRatio = 0.2f;

		// Token: 0x04001D62 RID: 7522
		[Header("Rewired Setup")]
		[SerializeField]
		private int rwPlayerId;

		// Token: 0x04001D63 RID: 7523
		private Player rwPlayer;

		// Token: 0x04001D64 RID: 7524
		private float rotateSpeed = 1f;

		// Token: 0x04001D65 RID: 7525
		private float zoomSpeed = 0.2f;

		// Token: 0x04001D66 RID: 7526
		private bool rtz_XZ;

		// Token: 0x04001D67 RID: 7527
		private bool rtz_Y;

		// Token: 0x04001D68 RID: 7528
		private RewiredActionReference panCameraXAxisId = "CineCameraPanX";

		// Token: 0x04001D69 RID: 7529
		private RewiredActionReference panCameraZAxisId = "CineCameraPanZ";

		// Token: 0x04001D6A RID: 7530
		private RewiredActionReference cameraSnapId = "CineCameraSnapRotate";

		// Token: 0x04001D6B RID: 7531
		private RewiredActionReference cameraSnap45Id = "CineCameraSnapRotate45";

		// Token: 0x04001D6C RID: 7532
		private RewiredActionReference cameraRtzId = "CineCameraRTZ";

		// Token: 0x04001D6D RID: 7533
		private RewiredActionReference pauseId = "Pause";

		// Token: 0x04001D6E RID: 7534
		private RewiredActionReference slomoId = "Slomo";
	}
}
