using System;

namespace CS.Lights
{
	// Token: 0x0200038E RID: 910
	[Flags]
	public enum DeviceType
	{
		// Token: 0x04000CE4 RID: 3300
		UNKNOWN = 0,
		// Token: 0x04000CE5 RID: 3301
		NOTEBOOK = 1,
		// Token: 0x04000CE6 RID: 3302
		DESKTOP = 2,
		// Token: 0x04000CE7 RID: 3303
		SERVER = 4,
		// Token: 0x04000CE8 RID: 3304
		DISPLAY = 8,
		// Token: 0x04000CE9 RID: 3305
		MOUSE = 16,
		// Token: 0x04000CEA RID: 3306
		KEYBOARD = 32,
		// Token: 0x04000CEB RID: 3307
		GAMEPAD = 64,
		// Token: 0x04000CEC RID: 3308
		SPEAKER = 128,
		// Token: 0x04000CED RID: 3309
		CUSTOM = 256,
		// Token: 0x04000CEE RID: 3310
		OTHER = 512,
		// Token: 0x04000CEF RID: 3311
		ALL = 1023
	}
}
