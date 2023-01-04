using System;

namespace Steamworks
{
	// Token: 0x02000201 RID: 513
	public static class SteamController
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00020C3C File Offset: 0x0001F03C
		public static bool Init()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_Init();
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00020C48 File Offset: 0x0001F048
		public static bool Shutdown()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_Shutdown();
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00020C54 File Offset: 0x0001F054
		public static void RunFrame()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_RunFrame();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00020C60 File Offset: 0x0001F060
		public static int GetConnectedControllers(ControllerHandle_t[] handlesOut)
		{
			InteropHelp.TestIfAvailableClient();
			if (handlesOut.Length != 16)
			{
				throw new ArgumentException("handlesOut must be the same size as Constants.STEAM_CONTROLLER_MAX_COUNT!");
			}
			return NativeMethods.ISteamController_GetConnectedControllers(handlesOut);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00020C82 File Offset: 0x0001F082
		public static bool ShowBindingPanel(ControllerHandle_t controllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_ShowBindingPanel(controllerHandle);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00020C90 File Offset: 0x0001F090
		public static ControllerActionSetHandle_t GetActionSetHandle(string pszActionSetName)
		{
			InteropHelp.TestIfAvailableClient();
			ControllerActionSetHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionSetName))
			{
				result = (ControllerActionSetHandle_t)NativeMethods.ISteamController_GetActionSetHandle(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00020CD8 File Offset: 0x0001F0D8
		public static void ActivateActionSet(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_ActivateActionSet(controllerHandle, actionSetHandle);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00020CE6 File Offset: 0x0001F0E6
		public static ControllerActionSetHandle_t GetCurrentActionSet(ControllerHandle_t controllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return (ControllerActionSetHandle_t)NativeMethods.ISteamController_GetCurrentActionSet(controllerHandle);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00020CF8 File Offset: 0x0001F0F8
		public static ControllerDigitalActionHandle_t GetDigitalActionHandle(string pszActionName)
		{
			InteropHelp.TestIfAvailableClient();
			ControllerDigitalActionHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionName))
			{
				result = (ControllerDigitalActionHandle_t)NativeMethods.ISteamController_GetDigitalActionHandle(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00020D40 File Offset: 0x0001F140
		public static ControllerDigitalActionData_t GetDigitalActionData(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetDigitalActionData(controllerHandle, digitalActionHandle);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00020D4E File Offset: 0x0001F14E
		public static int GetDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, EControllerActionOrigin[] originsOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetDigitalActionOrigins(controllerHandle, actionSetHandle, digitalActionHandle, originsOut);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00020D60 File Offset: 0x0001F160
		public static ControllerAnalogActionHandle_t GetAnalogActionHandle(string pszActionName)
		{
			InteropHelp.TestIfAvailableClient();
			ControllerAnalogActionHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionName))
			{
				result = (ControllerAnalogActionHandle_t)NativeMethods.ISteamController_GetAnalogActionHandle(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00020DA8 File Offset: 0x0001F1A8
		public static ControllerAnalogActionData_t GetAnalogActionData(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetAnalogActionData(controllerHandle, analogActionHandle);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00020DB6 File Offset: 0x0001F1B6
		public static int GetAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, EControllerActionOrigin[] originsOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetAnalogActionOrigins(controllerHandle, actionSetHandle, analogActionHandle, originsOut);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00020DC6 File Offset: 0x0001F1C6
		public static void StopAnalogActionMomentum(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_StopAnalogActionMomentum(controllerHandle, eAction);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00020DD4 File Offset: 0x0001F1D4
		public static void TriggerHapticPulse(ControllerHandle_t controllerHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_TriggerHapticPulse(controllerHandle, eTargetPad, usDurationMicroSec);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00020DE3 File Offset: 0x0001F1E3
		public static void TriggerRepeatedHapticPulse(ControllerHandle_t controllerHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_TriggerRepeatedHapticPulse(controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00020DF7 File Offset: 0x0001F1F7
		public static void TriggerVibration(ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_TriggerVibration(controllerHandle, usLeftSpeed, usRightSpeed);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00020E06 File Offset: 0x0001F206
		public static void SetLEDColor(ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_SetLEDColor(controllerHandle, nColorR, nColorG, nColorB, nFlags);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00020E18 File Offset: 0x0001F218
		public static int GetGamepadIndexForController(ControllerHandle_t ulControllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetGamepadIndexForController(ulControllerHandle);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00020E25 File Offset: 0x0001F225
		public static ControllerHandle_t GetControllerForGamepadIndex(int nIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (ControllerHandle_t)NativeMethods.ISteamController_GetControllerForGamepadIndex(nIndex);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00020E37 File Offset: 0x0001F237
		public static ControllerMotionData_t GetMotionData(ControllerHandle_t controllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetMotionData(controllerHandle);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00020E44 File Offset: 0x0001F244
		public static bool ShowDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle, float flScale, float flXPosition, float flYPosition)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_ShowDigitalActionOrigins(controllerHandle, digitalActionHandle, flScale, flXPosition, flYPosition);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00020E56 File Offset: 0x0001F256
		public static bool ShowAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle, float flScale, float flXPosition, float flYPosition)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_ShowAnalogActionOrigins(controllerHandle, analogActionHandle, flScale, flXPosition, flYPosition);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00020E68 File Offset: 0x0001F268
		public static string GetStringForActionOrigin(EControllerActionOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamController_GetStringForActionOrigin(eOrigin));
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00020E7A File Offset: 0x0001F27A
		public static string GetGlyphForActionOrigin(EControllerActionOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamController_GetGlyphForActionOrigin(eOrigin));
		}
	}
}
