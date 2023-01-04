using System;
using ReflexCLI.Attributes;
using ReflexCLI.User;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense
{
	// Token: 0x020006B0 RID: 1712
	public class CameraController : MonoBehaviour, IslandGameplayManager.IAwake, CursorManager.IDragListener, CursorManager.ICursor
	{
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x000A4420 File Offset: 0x000A2820
		public bool isDragging
		{
			get
			{
				return this.cameraDragger && this.cameraDragger.isDragging;
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000A4440 File Offset: 0x000A2840
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.rwPlayer = ReInput.players.GetPlayer(this.rwPlayerId);
			this.levelCamera = manager.levelCamera;
			this.cameraDragger = this.levelCamera.GetComponent<CameraDragger>();
			this.cameraSpinner = this.levelCamera.GetComponent<CameraSpinner>();
			this.cameraSeeker = this.levelCamera.GetComponent<CameraSeeker>();
			this.cameraZoomer = this.levelCamera.GetComponent<CameraZoomer>();
			this.cameraRotator = this.levelCamera.GetComponent<CameraRotator>();
			this.pointerRationalizer = manager.pointerRationalizer;
			this.pointerRationalizer.onButtonDown += this.OnButtonDown;
			this.pointerRationalizer.onClick += this.OnClick;
			CursorManager cursorManager = manager.cursorManager;
			cursorManager.Add(this);
			manager.levelPauser.onPauseChanged += delegate(bool p)
			{
				if (p)
				{
					this.cameraSpinner.EndSpin();
					this.cameraSeeker.EndSeek();
				}
			};
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000A4524 File Offset: 0x000A2924
		private void Update()
		{
			if (!ConsoleStatus.IsConsoleOpen())
			{
				if (!this.isDragging)
				{
					this.UpdateButtonYaw();
					this.UpdateButtonPan();
				}
				if (!InputHelpers.ControllerTypeIs(ControllerType.Mouse) || this.pointerRationalizer.state != PointerRationalizer.State.None)
				{
					float num = -this.rwPlayer.GetAxis(this.cameraZoomWheelAxis);
					float num2 = -this.rwPlayer.GetAxis(this.cameraZoomAnalogAxis);
					float num3 = -this.rwPlayer.GetAxis(this.cameraZoomDigitalAxis);
					float num4 = Time.unscaledDeltaTime * 60f;
					if (num3 != 0f)
					{
						num3 = Mathf.Lerp(this.zoomLerp, num3, 15f);
						this.cameraZoomer.ZoomBy(num3 * 0.5f * num4);
						if (num3 * this.cameraZoomButtonDownTime < 0f)
						{
							this.cameraZoomButtonDownTime = 0f;
						}
						else
						{
							this.cameraZoomButtonDownTime += Mathf.Sign(num3) * Time.unscaledDeltaTime;
						}
						num = (num2 = 0f);
					}
					else
					{
						if (this.cameraZoomButtonDownTime != 0f && Mathf.Abs(this.cameraZoomButtonDownTime) < 0.175f && !Profile.userSettings.suppressCameraZoomSnap)
						{
							float target = this.cameraZoomer.target;
							bool flag = this.cameraZoomButtonDownTime < 0f;
							this.cameraZoomer.ZoomTo((!flag) ? 1000f : 0f);
							if (this.cameraZoomer.target != target)
							{
								FabricWrapper.PostEvent((!flag) ? this.zoomSnapOut : this.zoomSnapIn);
							}
							else
							{
								FabricWrapper.PostEvent(FabricID.uiError);
							}
						}
						this.cameraZoomButtonDownTime = 0f;
					}
					if (num2 != 0f)
					{
						num2 = Mathf.Lerp(this.zoomLerp, num2, 15f);
						this.cameraZoomer.ZoomBy(num2 * 0.4f * num4);
						num = 0f;
					}
					if (num != 0f)
					{
						this.cameraZoomer.ZoomBy(num);
					}
				}
			}
			Billboard.UpdateRotations();
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000A4750 File Offset: 0x000A2B50
		private void UpdateButtonYaw()
		{
			bool invertGamePadCameraX = Profile.userSettings.invertGamePadCameraX;
			float num = this.rwPlayer.GetAxis(this.cameraYawAxis) * ((!invertGamePadCameraX) ? 1f : -1f);
			if (this.rwPlayer.GetButtonDown(this.cameraYawAxis) || this.rwPlayer.GetNegativeButtonDown(this.cameraYawAxis))
			{
				this.cameraSpinner.EndSpin();
				this.cameraSeeker.EndIfOpposed(num);
				this.cameraYawButtonDownTime = Time.unscaledTime;
			}
			if (this.rwPlayer.GetButton(this.cameraYawAxis) || this.rwPlayer.GetNegativeButton(this.cameraYawAxis))
			{
				if (Time.unscaledTime - this.cameraYawButtonDownTime >= 0.175f)
				{
					this.cameraSeeker.EndSeek();
				}
				float axis = this.rwPlayer.GetAxis(this.cameraPanVerticalAxis);
				num = this.AddDeadzone(num, this.ComputeDeadzone(axis)) * CameraController.cameraSensitivity;
				this.cameraRotator.SetInput(num);
			}
			else if ((this.rwPlayer.GetButtonUp(this.cameraYawAxis) || this.rwPlayer.GetNegativeButtonUp(this.cameraYawAxis)) && !InputHelpers.ControllerTypeIs(ControllerType.Joystick) && !Profile.userSettings.suppressCameraRotateSnap)
			{
				this.cameraSpinner.EndSpin();
				if (Time.unscaledTime - this.cameraYawButtonDownTime < 0.175f)
				{
					this.cameraSeeker.SeekToLatch(this.cameraRotator.velocity, 1);
					FabricWrapper.PostEvent((this.cameraRotator.velocity <= 0f) ? this.rotateSnapRight : this.rotateSnapLeft);
					this.cameraRotator.EndRotationImmediate();
				}
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000A4940 File Offset: 0x000A2D40
		private void UpdateButtonPan()
		{
			bool invertGamePadCameraY = Profile.userSettings.invertGamePadCameraY;
			float num = this.rwPlayer.GetAxis(this.cameraPanVerticalAxis) * ((!invertGamePadCameraY) ? 1f : -1f);
			float axis = this.rwPlayer.GetAxis(this.cameraYawAxis);
			num = this.AddDeadzone(num, this.ComputeDeadzone(axis)) * CameraController.cameraSensitivity;
			this.panLerp = Mathf.Lerp(this.panLerp, num, Time.unscaledDeltaTime * 15f);
			if (this.panLerp != 0f)
			{
				this.levelCamera.MovePositionBy(Vector3.up * this.panLerp * this.levelCamera.GetOrthoHeight() * 0.75f * Time.unscaledDeltaTime);
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000A4A1D File Offset: 0x000A2E1D
		private void OnButtonDown(PointerEventData.InputButton button, Vector2 screenPos)
		{
			this.cameraSpinner.EndSpin();
			this.cameraSeeker.EndSeek();
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000A4A38 File Offset: 0x000A2E38
		private void OnClick(PointerEventData.InputButton button, Vector2 screenPos)
		{
			this.cameraSpinner.EndSpin();
			this.cameraSeeker.EndSeek();
			this.zoomLerp = (this.panLerp = 0f);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000A4A70 File Offset: 0x000A2E70
		private float ComputeDeadzone(float complimentaryAxis)
		{
			float f = this.AddDeadzone(complimentaryAxis, 0.15f);
			return Mathf.Lerp(0.15f, 0.275f, Mathf.Abs(f));
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000A4AA0 File Offset: 0x000A2EA0
		private float AddDeadzone(float axis, float deadzone)
		{
			float num = Mathf.Abs(axis);
			if (num < deadzone)
			{
				return 0f;
			}
			float num2 = Mathf.Sign(axis);
			return Mathf.InverseLerp(deadzone, 1f, num) * num2;
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000A4AD8 File Offset: 0x000A2ED8
		void CursorManager.ICursor.SetActive(bool active)
		{
			if (!active)
			{
				Vector2 vector;
				this.cameraDragger.OnDragEnd(out vector);
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000A4AF8 File Offset: 0x000A2EF8
		void CursorManager.IDragListener.OnDragStart(PointerEventData.InputButton button)
		{
			this.cameraDragger.OnDragStart();
			this.zoomLerp = (this.panLerp = 0f);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000A4B24 File Offset: 0x000A2F24
		void CursorManager.IDragListener.OnDragEnd(PointerEventData.InputButton button)
		{
			Vector2 vector;
			this.cameraDragger.OnDragEnd(out vector);
			this.cameraSpinner.BeginSpin(vector.x);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000A4B50 File Offset: 0x000A2F50
		void CursorManager.IDragListener.OnDrag(PointerEventData.InputButton button, Vector2 delta)
		{
			if (Input.touchCount == 2)
			{
				this.cameraZoomer.PinchZoom(Input.touches[0].position, Input.touches[1].position);
			}
			else
			{
				this.cameraDragger.OnDrag(delta);
			}
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000A4BA4 File Offset: 0x000A2FA4
		void CursorManager.IDragListener.OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position)
		{
		}

		// Token: 0x04001CF2 RID: 7410
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CameraController", EVerbosity.Quiet, 0);

		// Token: 0x04001CF3 RID: 7411
		private const float cameraTapTime = 0.175f;

		// Token: 0x04001CF4 RID: 7412
		private int rwPlayerId;

		// Token: 0x04001CF5 RID: 7413
		private Player rwPlayer;

		// Token: 0x04001CF6 RID: 7414
		private PointerRationalizer pointerRationalizer;

		// Token: 0x04001CF7 RID: 7415
		private RewiredActionReference cameraYawAxis = "CameraYaw";

		// Token: 0x04001CF8 RID: 7416
		private RewiredActionReference cameraPanVerticalAxis = "CameraPanVertical";

		// Token: 0x04001CF9 RID: 7417
		private RewiredActionReference cameraZoomWheelAxis = "CameraZoomWheel";

		// Token: 0x04001CFA RID: 7418
		private RewiredActionReference cameraZoomAnalogAxis = "CameraZoomAnalog";

		// Token: 0x04001CFB RID: 7419
		private RewiredActionReference cameraZoomDigitalAxis = "CameraZoomDigital";

		// Token: 0x04001CFC RID: 7420
		private FabricEventReference rotateSnapLeft = "UI/InGame/TapLeft";

		// Token: 0x04001CFD RID: 7421
		private FabricEventReference rotateSnapRight = "UI/InGame/TapRight";

		// Token: 0x04001CFE RID: 7422
		private FabricEventReference zoomSnapIn = "UI/InGame/ZoomIn";

		// Token: 0x04001CFF RID: 7423
		private FabricEventReference zoomSnapOut = "UI/InGame/ZoomOut";

		// Token: 0x04001D00 RID: 7424
		private float cameraYawButtonDownTime = float.MinValue;

		// Token: 0x04001D01 RID: 7425
		private float cameraZoomButtonDownTime;

		// Token: 0x04001D02 RID: 7426
		private float panLerp;

		// Token: 0x04001D03 RID: 7427
		private float zoomLerp;

		// Token: 0x04001D04 RID: 7428
		private LevelCamera levelCamera;

		// Token: 0x04001D05 RID: 7429
		private CameraDragger cameraDragger;

		// Token: 0x04001D06 RID: 7430
		private CameraSeeker cameraSeeker;

		// Token: 0x04001D07 RID: 7431
		private CameraSpinner cameraSpinner;

		// Token: 0x04001D08 RID: 7432
		private CameraZoomer cameraZoomer;

		// Token: 0x04001D09 RID: 7433
		private CameraRotator cameraRotator;

		// Token: 0x04001D0A RID: 7434
		[ConsoleCommand("")]
		private static float cameraSensitivity = 1f;
	}
}
