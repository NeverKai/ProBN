using System;
using System.Diagnostics;
using System.Linq;
using RTM.OnScreenDebug;
using UnityEngine;

// Token: 0x02000571 RID: 1393
public static class Platform
{
	// Token: 0x0600242F RID: 9263 RVA: 0x000718C8 File Offset: 0x0006FCC8
	static Platform()
	{
		Platform._onPlatformUpdated = delegate()
		{
		};
		Platform._platform = Platform.GetPlatform(Application.platform);
		Platform.PrintChange(60f);
		Platform.onPlatformUpdated += delegate()
		{
			Platform.PrintChange(10f);
		};
	}

	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x06002430 RID: 9264 RVA: 0x00071921 File Offset: 0x0006FD21
	public static EPlatform platform
	{
		get
		{
			return Platform._platform;
		}
	}

	// Token: 0x06002431 RID: 9265 RVA: 0x00071928 File Offset: 0x0006FD28
	public static bool Is(EPlatform targetPlatform)
	{
		EPlatform eplatform = targetPlatform & Platform.platform;
		return eplatform != (EPlatform)0;
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x00071944 File Offset: 0x0006FD44
	private static EPlatform GetPlatform(RuntimePlatform runtimePlatform)
	{
		return Platform.GetRealPlatform(runtimePlatform);
	}

	// Token: 0x06002433 RID: 9267 RVA: 0x0007194C File Offset: 0x0006FD4C
	private static EPlatform GetRealPlatform(RuntimePlatform runtimePlatform)
	{
		switch (runtimePlatform)
		{
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
			return EPlatform.Mac;
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WindowsEditor:
			return EPlatform.Windows;
		default:
			switch (runtimePlatform)
			{
			case RuntimePlatform.PS4:
				return EPlatform.PS4;
			default:
				if (runtimePlatform != RuntimePlatform.LinuxEditor)
				{
					if (runtimePlatform != RuntimePlatform.Switch)
					{
						return EPlatform.Windows;
					}
					return EPlatform.SwitchHandheld;
				}
				break;
			case RuntimePlatform.XboxOne:
				return EPlatform.XboxOne;
			}
			break;
		case RuntimePlatform.IPhonePlayer:
			return (!Platform.IsTablet()) ? EPlatform.IOSPhone : EPlatform.IOSTablet;
		case RuntimePlatform.Android:
			return (!Platform.IsTablet()) ? EPlatform.AndroidPhone : EPlatform.AndroidTablet;
		case RuntimePlatform.LinuxPlayer:
			break;
		}
		return EPlatform.Linux;
	}

	// Token: 0x14000082 RID: 130
	// (add) Token: 0x06002434 RID: 9268 RVA: 0x00071A04 File Offset: 0x0006FE04
	// (remove) Token: 0x06002435 RID: 9269 RVA: 0x00071A38 File Offset: 0x0006FE38
	
	private static event Action _onPlatformUpdated;

	// Token: 0x14000083 RID: 131
	// (add) Token: 0x06002436 RID: 9270 RVA: 0x00071A6C File Offset: 0x0006FE6C
	// (remove) Token: 0x06002437 RID: 9271 RVA: 0x00071A89 File Offset: 0x0006FE89
	public static event Action onPlatformUpdated
	{
		add
		{
			if (!Platform._onPlatformUpdated.GetInvocationList().Contains(value))
			{
				Platform._onPlatformUpdated += value;
			}
		}
		remove
		{
			Platform._onPlatformUpdated -= value;
		}
	}

	// Token: 0x06002438 RID: 9272 RVA: 0x00071A94 File Offset: 0x0006FE94
	private static bool IsTablet()
	{
		float dpi = Screen.dpi;
		if (dpi > 0f)
		{
			float num = (float)Screen.height / Screen.dpi;
			float num2 = (float)Screen.width / Screen.dpi;
			float num3 = Mathf.Sqrt(num * num + num2 * num2);
			return num3 >= 8.3f;
		}
		return false;
	}

	// Token: 0x06002439 RID: 9273 RVA: 0x00071AE6 File Offset: 0x0006FEE6
	private static void PrintChange(float time)
	{
	}

	// Token: 0x040016DF RID: 5855
	private static DebugChannelGroup dbgGroup = new DebugChannelGroup("Platform", EVerbosity.Quiet, 0);

	// Token: 0x040016E0 RID: 5856
	private static EPlatform _platform;
}
