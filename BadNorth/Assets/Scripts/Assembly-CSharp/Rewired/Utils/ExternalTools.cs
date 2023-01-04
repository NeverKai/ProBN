using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Rewired.Internal;
using Rewired.Utils.Interfaces;
using Rewired.Utils.Platforms.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Utils
{
	// Token: 0x020004AE RID: 1198
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ExternalTools : IExternalTools
	{
		// Token: 0x06001DF6 RID: 7670 RVA: 0x00050943 File Offset: 0x0004ED43
		public void Destroy()
		{
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x00050945 File Offset: 0x0004ED45
		public bool isEditorPaused
		{
			get
			{
				return this._isEditorPaused;
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06001DF8 RID: 7672 RVA: 0x0005094D File Offset: 0x0004ED4D
		// (remove) Token: 0x06001DF9 RID: 7673 RVA: 0x00050966 File Offset: 0x0004ED66
		public event Action<bool> EditorPausedStateChangedEvent
		{
			add
			{
				this._EditorPausedStateChangedEvent = (Action<bool>)Delegate.Combine(this._EditorPausedStateChangedEvent, value);
			}
			remove
			{
				this._EditorPausedStateChangedEvent = (Action<bool>)Delegate.Remove(this._EditorPausedStateChangedEvent, value);
			}
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0005097F File Offset: 0x0004ED7F
		public object GetPlatformInitializer()
		{
			return Main.GetPlatformInitializer();
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00050986 File Offset: 0x0004ED86
		public string GetFocusedEditorWindowTitle()
		{
			return string.Empty;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0005098D File Offset: 0x0004ED8D
		public bool IsEditorSceneViewFocused()
		{
			return false;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00050990 File Offset: 0x0004ED90
		public bool LinuxInput_IsJoystickPreconfigured(string name)
		{
			return false;
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06001DFE RID: 7678 RVA: 0x00050994 File Offset: 0x0004ED94
		// (remove) Token: 0x06001DFF RID: 7679 RVA: 0x000509CC File Offset: 0x0004EDCC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<uint, bool> XboxOneInput_OnGamepadStateChange;

		// Token: 0x06001E00 RID: 7680 RVA: 0x00050A02 File Offset: 0x0004EE02
		public int XboxOneInput_GetUserIdForGamepad(uint id)
		{
			return 0;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00050A05 File Offset: 0x0004EE05
		public ulong XboxOneInput_GetControllerId(uint unityJoystickId)
		{
			return 0UL;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00050A09 File Offset: 0x0004EE09
		public bool XboxOneInput_IsGamepadActive(uint unityJoystickId)
		{
			return false;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00050A0C File Offset: 0x0004EE0C
		public string XboxOneInput_GetControllerType(ulong xboxControllerId)
		{
			return string.Empty;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00050A13 File Offset: 0x0004EE13
		public uint XboxOneInput_GetJoystickId(ulong xboxControllerId)
		{
			return 0U;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00050A16 File Offset: 0x0004EE16
		public void XboxOne_Gamepad_UpdatePlugin()
		{
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00050A18 File Offset: 0x0004EE18
		public bool XboxOne_Gamepad_SetGamepadVibration(ulong xboxOneJoystickId, float leftMotor, float rightMotor, float leftTriggerLevel, float rightTriggerLevel)
		{
			return false;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00050A1B File Offset: 0x0004EE1B
		public void XboxOne_Gamepad_PulseVibrateMotor(ulong xboxOneJoystickId, int motorInt, float startLevel, float endLevel, ulong durationMS)
		{
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00050A1D File Offset: 0x0004EE1D
		public Vector3 PS4Input_GetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00050A24 File Offset: 0x0004EE24
		public Vector3 PS4Input_GetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00050A2B File Offset: 0x0004EE2B
		public Vector4 PS4Input_GetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00050A32 File Offset: 0x0004EE32
		public void PS4Input_GetLastTouchData(int id, out int touchNum, out int touch0x, out int touch0y, out int touch0id, out int touch1x, out int touch1y, out int touch1id)
		{
			touchNum = 0;
			touch0x = 0;
			touch0y = 0;
			touch0id = 0;
			touch1x = 0;
			touch1y = 0;
			touch1id = 0;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00050A4E File Offset: 0x0004EE4E
		public void PS4Input_GetPadControllerInformation(int id, out float touchpixelDensity, out int touchResolutionX, out int touchResolutionY, out int analogDeadZoneLeft, out int analogDeadZoneright, out int connectionType)
		{
			touchpixelDensity = 0f;
			touchResolutionX = 0;
			touchResolutionY = 0;
			analogDeadZoneLeft = 0;
			analogDeadZoneright = 0;
			connectionType = 0;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00050A6A File Offset: 0x0004EE6A
		public void PS4Input_PadSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00050A6C File Offset: 0x0004EE6C
		public void PS4Input_PadSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00050A6E File Offset: 0x0004EE6E
		public void PS4Input_PadSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00050A70 File Offset: 0x0004EE70
		public void PS4Input_PadSetLightBar(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00050A72 File Offset: 0x0004EE72
		public void PS4Input_PadResetLightBar(int id)
		{
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00050A74 File Offset: 0x0004EE74
		public void PS4Input_PadSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00050A76 File Offset: 0x0004EE76
		public void PS4Input_PadResetOrientation(int id)
		{
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00050A78 File Offset: 0x0004EE78
		public bool PS4Input_PadIsConnected(int id)
		{
			return false;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00050A7B File Offset: 0x0004EE7B
		public void PS4Input_GetUsersDetails(int slot, object loggedInUser)
		{
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00050A7D File Offset: 0x0004EE7D
		public int PS4Input_GetDeviceClassForHandle(int handle)
		{
			return -1;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00050A80 File Offset: 0x0004EE80
		public string PS4Input_GetDeviceClassString(int intValue)
		{
			return null;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00050A83 File Offset: 0x0004EE83
		public int PS4Input_PadGetUsersHandles2(int maxControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00050A86 File Offset: 0x0004EE86
		public Vector3 PS4Input_GetLastMoveAcceleration(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00050A8D File Offset: 0x0004EE8D
		public Vector3 PS4Input_GetLastMoveGyro(int id, int index)
		{
			return Vector3.zero;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00050A94 File Offset: 0x0004EE94
		public int PS4Input_MoveGetButtons(int id, int index)
		{
			return 0;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00050A97 File Offset: 0x0004EE97
		public int PS4Input_MoveGetAnalogButton(int id, int index)
		{
			return 0;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00050A9A File Offset: 0x0004EE9A
		public bool PS4Input_MoveIsConnected(int id, int index)
		{
			return false;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00050A9D File Offset: 0x0004EE9D
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles, int[] secondaryHandles)
		{
			return 0;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00050AA0 File Offset: 0x0004EEA0
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers, int[] primaryHandles)
		{
			return 0;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00050AA3 File Offset: 0x0004EEA3
		public int PS4Input_MoveGetUsersMoveHandles(int maxNumberControllers)
		{
			return 0;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00050AA6 File Offset: 0x0004EEA6
		public IntPtr PS4Input_MoveGetControllerInputForTracking()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00050AAD File Offset: 0x0004EEAD
		public void PS4Input_GetSpecialControllerInformation(int id, int padIndex, object controllerInformation)
		{
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00050AAF File Offset: 0x0004EEAF
		public Vector3 PS4Input_SpecialGetLastAcceleration(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00050AB6 File Offset: 0x0004EEB6
		public Vector3 PS4Input_SpecialGetLastGyro(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00050ABD File Offset: 0x0004EEBD
		public Vector4 PS4Input_SpecialGetLastOrientation(int id)
		{
			return Vector4.zero;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00050AC4 File Offset: 0x0004EEC4
		public int PS4Input_SpecialGetUsersHandles(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00050AC7 File Offset: 0x0004EEC7
		public int PS4Input_SpecialGetUsersHandles2(int maxNumberControllers, int[] handles)
		{
			return 0;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00050ACA File Offset: 0x0004EECA
		public bool PS4Input_SpecialIsConnected(int id)
		{
			return false;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00050ACD File Offset: 0x0004EECD
		public void PS4Input_SpecialResetLightSphere(int id)
		{
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00050ACF File Offset: 0x0004EECF
		public void PS4Input_SpecialResetOrientation(int id)
		{
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00050AD1 File Offset: 0x0004EED1
		public void PS4Input_SpecialSetAngularVelocityDeadbandState(int id, bool bEnable)
		{
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00050AD3 File Offset: 0x0004EED3
		public void PS4Input_SpecialSetLightSphere(int id, int red, int green, int blue)
		{
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00050AD5 File Offset: 0x0004EED5
		public void PS4Input_SpecialSetMotionSensorState(int id, bool bEnable)
		{
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00050AD7 File Offset: 0x0004EED7
		public void PS4Input_SpecialSetTiltCorrectionState(int id, bool bEnable)
		{
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00050AD9 File Offset: 0x0004EED9
		public void PS4Input_SpecialSetVibration(int id, int largeMotor, int smallMotor)
		{
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00050ADB File Offset: 0x0004EEDB
		public void GetDeviceVIDPIDs(out List<int> vids, out List<int> pids)
		{
			vids = new List<int>();
			pids = new List<int>();
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00050AEB File Offset: 0x0004EEEB
		public int GetAndroidAPILevel()
		{
			return -1;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00050AEE File Offset: 0x0004EEEE
		public bool UnityUI_Graphic_GetRaycastTarget(object graphic)
		{
			return !(graphic as Graphic == null) && (graphic as Graphic).raycastTarget;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00050B0E File Offset: 0x0004EF0E
		public void UnityUI_Graphic_SetRaycastTarget(object graphic, bool value)
		{
			if (graphic as Graphic == null)
			{
				return;
			}
			(graphic as Graphic).raycastTarget = value;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x00050B2E File Offset: 0x0004EF2E
		public bool UnityInput_IsTouchPressureSupported
		{
			get
			{
				return Input.touchPressureSupported;
			}
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00050B35 File Offset: 0x0004EF35
		public float UnityInput_GetTouchPressure(ref Touch touch)
		{
			return touch.pressure;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00050B3D File Offset: 0x0004EF3D
		public float UnityInput_GetTouchMaximumPossiblePressure(ref Touch touch)
		{
			return touch.maximumPossiblePressure;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00050B45 File Offset: 0x0004EF45
		public IControllerTemplate CreateControllerTemplate(Guid typeGuid, object payload)
		{
			return ControllerTemplateFactory.Create(typeGuid, payload);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00050B4E File Offset: 0x0004EF4E
		public Type[] GetControllerTemplateTypes()
		{
			return ControllerTemplateFactory.templateTypes;
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00050B55 File Offset: 0x0004EF55
		public Type[] GetControllerTemplateInterfaceTypes()
		{
			return ControllerTemplateFactory.templateInterfaceTypes;
		}

		// Token: 0x040012DB RID: 4827
		private bool _isEditorPaused;

		// Token: 0x040012DC RID: 4828
		private Action<bool> _EditorPausedStateChangedEvent;
	}
}
