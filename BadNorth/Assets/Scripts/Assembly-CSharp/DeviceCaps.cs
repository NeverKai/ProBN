using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020005DF RID: 1503
public class DeviceCaps
{
	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x06002702 RID: 9986 RVA: 0x0007D72E File Offset: 0x0007BB2E
	public static bool isLowendDevice
	{
		get
		{
			DeviceCaps.Get();
			return DeviceCaps.lowEndDevice || DeviceCaps.veryLowEndDevice;
		}
	}

	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x06002703 RID: 9987 RVA: 0x0007D747 File Offset: 0x0007BB47
	public static bool isVeryLowendDevice
	{
		get
		{
			DeviceCaps.Get();
			return DeviceCaps.veryLowEndDevice;
		}
	}

	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x06002704 RID: 9988 RVA: 0x0007D753 File Offset: 0x0007BB53
	public static bool hasLowResolutionScreen
	{
		get
		{
			return (float)Screen.height < 800f;
		}
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x0007D762 File Offset: 0x0007BB62
	private static void Get()
	{
		if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2)
		{
			DeviceCaps.lowEndDevice = true;
		}
	}

	// Token: 0x04001901 RID: 6401
	private static bool lowEndDevice;

	// Token: 0x04001902 RID: 6402
	private static bool veryLowEndDevice;

	// Token: 0x04001903 RID: 6403
	private static bool noAAonLowEndDevice;
}
