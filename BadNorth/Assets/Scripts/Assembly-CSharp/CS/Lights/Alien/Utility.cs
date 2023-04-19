using System;
//using AlienFXManagedWrapper;
using UnityEngine;

namespace CS.Lights.Alien
{
	// Token: 0x0200038A RID: 906
	public static class Utility
	{
		// Token: 0x060014B4 RID: 5300 RVA: 0x0002A794 File Offset: 0x00028B94
		// public static LFX_DeviceType DeviceToAlien(DeviceType type)
		// {
		// 	if ((type & DeviceType.NOTEBOOK) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Notebook;
		// 	}
		// 	if ((type & DeviceType.DESKTOP) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Desktop;
		// 	}
		// 	if ((type & DeviceType.SERVER) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Server;
		// 	}
		// 	if ((type & DeviceType.DISPLAY) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Display;
		// 	}
		// 	if ((type & DeviceType.MOUSE) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Mouse;
		// 	}
		// 	if ((type & DeviceType.KEYBOARD) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Keyboard;
		// 	}
		// 	if ((type & DeviceType.GAMEPAD) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Gamepad;
		// 	}
		// 	if ((type & DeviceType.SPEAKER) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Speaker;
		// 	}
		// 	if ((type & DeviceType.CUSTOM) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Custom;
		// 	}
		// 	if ((type & DeviceType.OTHER) != DeviceType.UNKNOWN)
		// 	{
		// 		return LFX_DeviceType.LFX_DevType_Other;
		// 	}
		// 	return LFX_DeviceType.LFX_DevType_Unknown;
		// }
		//
		// // Token: 0x060014B5 RID: 5301 RVA: 0x0002A820 File Offset: 0x00028C20
		// public static DeviceType AlienToDevice(LFX_DeviceType type)
		// {
		// 	switch (type)
		// 	{
		// 	case LFX_DeviceType.LFX_DevType_Unknown:
		// 		break;
		// 	case LFX_DeviceType.LFX_DevType_Notebook:
		// 		return DeviceType.NOTEBOOK;
		// 	case LFX_DeviceType.LFX_DevType_Desktop:
		// 		return DeviceType.DESKTOP;
		// 	case LFX_DeviceType.LFX_DevType_Server:
		// 		return DeviceType.SERVER;
		// 	case LFX_DeviceType.LFX_DevType_Display:
		// 		return DeviceType.DISPLAY;
		// 	case LFX_DeviceType.LFX_DevType_Mouse:
		// 		return DeviceType.MOUSE;
		// 	case LFX_DeviceType.LFX_DevType_Keyboard:
		// 		return DeviceType.KEYBOARD;
		// 	case LFX_DeviceType.LFX_DevType_Gamepad:
		// 		return DeviceType.GAMEPAD;
		// 	case LFX_DeviceType.LFX_DevType_Speaker:
		// 		return DeviceType.SPEAKER;
		// 	default:
		// 		if (type == LFX_DeviceType.LFX_DevType_Custom)
		// 		{
		// 			return DeviceType.CUSTOM;
		// 		}
		// 		if (type == LFX_DeviceType.LFX_DevType_Other)
		// 		{
		// 			return DeviceType.OTHER;
		// 		}
		// 		break;
		// 	}
		// 	return DeviceType.UNKNOWN;
		// }
		//
		// // Token: 0x060014B6 RID: 5302 RVA: 0x0002A898 File Offset: 0x00028C98
		// public static LFX_ColorStruct ColourToAlien(Color colour)
		// {
		// 	LFX_ColorStruct result;
		// 	result.red = (byte)(colour.r * 255f);
		// 	result.green = (byte)(colour.g * 255f);
		// 	result.blue = (byte)(colour.b * 255f);
		// 	result.brightness = (byte)(colour.a * 255f);
		// 	return result;
		// }

		// Token: 0x060014B7 RID: 5303 RVA: 0x0002A8FC File Offset: 0x00028CFC
		public static uint ColourToUInt(Color colour)
		{
			return (uint)(18446744073692774400UL | (ulong)((ulong)((uint)(colour.r * colour.a * 255f)) << 16) | (ulong)((ulong)((uint)(colour.g * colour.a * 255f)) << 8) | (ulong)((uint)(colour.b * colour.a * 255f)));
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0002A960 File Offset: 0x00028D60
		// public static Vector3Int GetPoint(LFX_Position pos)
		// {
		// 	Vector3Int zero = Vector3Int.zero;
		// 	if ((pos & LFX_Position.LFX_All_Left) != (LFX_Position)0U)
		// 	{
		// 		zero.x = 0;
		// 	}
		// 	else
		// 	{
		// 		zero.x = (((pos & LFX_Position.LFX_All_Right) == (LFX_Position)0U) ? 1 : 2);
		// 	}
		// 	if ((pos & LFX_Position.LFX_All_Lower) != (LFX_Position)0U)
		// 	{
		// 		zero.y = 0;
		// 	}
		// 	else
		// 	{
		// 		zero.y = (((pos & LFX_Position.LFX_All_Upper) == (LFX_Position)0U) ? 1 : 2);
		// 	}
		// 	if ((pos & LFX_Position.LFX_All_Front) != (LFX_Position)0U)
		// 	{
		// 		zero.y = 0;
		// 	}
		// 	else
		// 	{
		// 		zero.y = (((pos & LFX_Position.LFX_All_Rear) == (LFX_Position)0U) ? 1 : 2);
		// 	}
		// 	return zero;
		// }
	}
}
