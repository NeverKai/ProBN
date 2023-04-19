using System;
using System.Collections.Generic;
using System.Diagnostics;
using CS.Platform;
using I2.Loc;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense;

namespace RTM.Input
{
	// Token: 0x020004B6 RID: 1206
	public class InputHelpers : MonoBehaviour
	{
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06001E91 RID: 7825 RVA: 0x00051994 File Offset: 0x0004FD94
		// (remove) Token: 0x06001E92 RID: 7826 RVA: 0x000519C8 File Offset: 0x0004FDC8
		
		public static event Action OnActiveControllerLost;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06001E93 RID: 7827 RVA: 0x000519FC File Offset: 0x0004FDFC
		// (remove) Token: 0x06001E94 RID: 7828 RVA: 0x00051A30 File Offset: 0x0004FE30
		
		public static event Action<ControllerType> onControllerTypeChanged;

		// Token: 0x06001E95 RID: 7829 RVA: 0x00051A64 File Offset: 0x0004FE64
		public static ControllerType GetControllerType()
		{
			Controller controller = InputHelpers.GetController();
			return (controller == null) ? InputHelpers.GetDefaultControllerType() : controller.type;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00051A8D File Offset: 0x0004FE8D
		public static Controller GetController()
		{
			if (!ReInput.isReady)
			{
				return null;
			}
			return (InputHelpers.rwPlayer == null) ? null : InputHelpers.rwPlayer.controllers.GetLastActiveController();
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00051ABA File Offset: 0x0004FEBA
		public static bool ControllerTypeIs(ControllerType type)
		{
			return InputHelpers.GetControllerType() == type;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00051AC4 File Offset: 0x0004FEC4
		public static bool ControllerTypeIsNavigable(ControllerType type)
		{
			return type != ControllerType.Mouse;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00051ACD File Offset: 0x0004FECD
		public static bool ControllerTypeIsNavigable()
		{
			return InputHelpers.ControllerTypeIsNavigable(InputHelpers.GetControllerType());
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x00051AD9 File Offset: 0x0004FED9
		public static bool IsSelectInverted()
		{
			return Platform.Is(EPlatform.Switch) || (BasePlatformManager.Instance && BasePlatformManager.Instance.IsSelectInverted());
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00051B09 File Offset: 0x0004FF09
		private void Awake()
		{
			ReInput.ControllerPreDisconnectEvent += this.OnControllerDisconnected;
			InputHelpers.rwPlayer = ReInput.players.GetPlayer(0);
			InputHelpers.controllerType = InputHelpers.GetDefaultControllerType();
			this.Update();
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00051B3C File Offset: 0x0004FF3C
		private void OnDestroy()
		{
			ReInput.ControllerPreDisconnectEvent -= this.OnControllerDisconnected;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00051B50 File Offset: 0x0004FF50
		private void Update()
		{
			if (InputHelpers.SearchForJoystick && InputHelpers.rwPlayer.controllers.joystickCount == 0)
			{
				InputHelpers.TakeFreeJoystick();
			}
			ControllerType controllerType = InputHelpers.controllerType;
			InputHelpers.controller = ((InputHelpers.rwPlayer == null) ? null : InputHelpers.rwPlayer.controllers.GetLastActiveController());
			InputHelpers.controllerType = ((InputHelpers.controller == null) ? InputHelpers.GetDefaultControllerType() : InputHelpers.controller.type);
			if (InputHelpers.controllerType != controllerType)
			{
				using (new ScopedProfiler("OnInputTypeChangedDelegate", null))
				{
					InputHelpers.onControllerTypeChanged(InputHelpers.controllerType);
				}
				if (InputHelpers.controllerType == ControllerType.Joystick)
				{
					CursorWrapper.BlockVisibility(this);
				}
				else
				{
					CursorWrapper.UnblockVisibility(this);
				}
			}
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00051C34 File Offset: 0x00050034
		private static void TakeFreeJoystick()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (joysticks[i].GetAnyButtonDown() || joysticks[i].GetLastTimeAnyAxisChanged() == ReInput.time.unscaledTime)
				{
					InputHelpers.rwPlayer.controllers.AddController(joysticks[i], true);
					RewiredHelpers.ApplyPendingSwaps(ControllerType.Joystick, joysticks[i].id);
					break;
				}
			}
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00051CBD File Offset: 0x000500BD
		private static ControllerType GetDefaultControllerType()
		{
			return ControllerType.Mouse;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00051CC0 File Offset: 0x000500C0
		private void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (ReInput.controllers.IsControllerAssignedToPlayer(args.controllerType, args.controllerId, InputHelpers.rwPlayer.id))
			{
				if (args.controllerType != ControllerType.Joystick)
				{
					return;
				}
				BasePlatformManager.Instance.ShowSystemMessageOK(LocalizationManager.GetTranslation("SYSTEM/INPUT/CONTOLLER_LOST", true, 0, true, true, null, null), null);
				if (InputHelpers.OnActiveControllerLost != null)
				{
					InputHelpers.OnActiveControllerLost();
				}
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00051D2F File Offset: 0x0005012F
		public static PointerEventData.InputButton ProcessMouseButton(PointerEventData.InputButton button)
		{
			return button;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00051D32 File Offset: 0x00050132
		// Note: this type is marked as 'beforefieldinit'.
		static InputHelpers()
		{
			InputHelpers.OnActiveControllerLost = null;
			InputHelpers.onControllerTypeChanged = delegate(ControllerType A_0)
			{
			};
		}

		// Token: 0x040012FE RID: 4862
		public static bool SearchForJoystick = true;

		// Token: 0x040012FF RID: 4863
		private static Player rwPlayer = null;

		// Token: 0x04001300 RID: 4864
		private static Controller controller = null;

		// Token: 0x04001301 RID: 4865
		private static ControllerType controllerType = ControllerType.Mouse;
	}
}
