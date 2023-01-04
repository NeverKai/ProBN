using System;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x0200086E RID: 2158
	[ConsoleCommandClassCustomizer("Environment")]
	internal static class EnviromentDebug
	{
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000F47ED File Offset: 0x000F2BED
		// (set) Token: 0x06003885 RID: 14469 RVA: 0x000F47FF File Offset: 0x000F2BFF
		[ConsoleCommand("hour")]
		private static int hour
		{
			get
			{
				return (int)Singleton<EnvironmentManager>.instance.day.hour;
			}
			set
			{
				Singleton<EnvironmentManager>.instance.day.hour = (float)value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000F4812 File Offset: 0x000F2C12
		// (set) Token: 0x06003887 RID: 14471 RVA: 0x000F4823 File Offset: 0x000F2C23
		[ConsoleCommand("snowfall")]
		private static float snowfall
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.snowfall;
			}
			set
			{
				Singleton<WorldWeather>.instance.weatherSystem.snowfall = value;
				EnvironmentManager instance = Singleton<EnvironmentManager>.instance;
				instance.day.value = instance.day.value + 1f;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000F4850 File Offset: 0x000F2C50
		// (set) Token: 0x06003889 RID: 14473 RVA: 0x000F4861 File Offset: 0x000F2C61
		[ConsoleCommand("rainfall")]
		private static float rainfall
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.rainfall;
			}
			set
			{
				Singleton<WorldWeather>.instance.weatherSystem.rainfall = value;
				EnvironmentManager instance = Singleton<EnvironmentManager>.instance;
				instance.day.value = instance.day.value + 1f;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600388A RID: 14474 RVA: 0x000F488E File Offset: 0x000F2C8E
		// (set) Token: 0x0600388B RID: 14475 RVA: 0x000F489F File Offset: 0x000F2C9F
		[ConsoleCommand("cloudCoverage")]
		private static float cloudCoverage
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.cloudCoverage;
			}
			set
			{
				Singleton<WorldWeather>.instance.weatherSystem.cloudCoverage = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600388C RID: 14476 RVA: 0x000F48B1 File Offset: 0x000F2CB1
		// (set) Token: 0x0600388D RID: 14477 RVA: 0x000F48C2 File Offset: 0x000F2CC2
		[ConsoleCommand("thunder")]
		private static float thunder
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.thunder;
			}
			set
			{
				Singleton<WorldWeather>.instance.weatherSystem.thunder = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x000F48D4 File Offset: 0x000F2CD4
		// (set) Token: 0x0600388F RID: 14479 RVA: 0x000F48E5 File Offset: 0x000F2CE5
		[ConsoleCommand("wind")]
		private static float wind
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.wind;
			}
			set
			{
				Singleton<WorldWeather>.instance.weatherSystem.wind = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000F48F7 File Offset: 0x000F2CF7
		// (set) Token: 0x06003891 RID: 14481 RVA: 0x000F4908 File Offset: 0x000F2D08
		[ConsoleCommand("snowCoverage")]
		private static float snowCoverage
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.snowCoverage;
			}
			set
			{
				Singleton<WorldWeather>.instance.weatherSystem.snowCoverage = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000F491A File Offset: 0x000F2D1A
		// (set) Token: 0x06003893 RID: 14483 RVA: 0x000F492B File Offset: 0x000F2D2B
		[ConsoleCommand("temperature")]
		private static float temperature
		{
			get
			{
				return Singleton<WorldWeather>.instance.weatherSystem.temperature;
			}
			set
			{
				Singleton<WorldWeather>.instance.weatherSystem.temperature = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000F493D File Offset: 0x000F2D3D
		// (set) Token: 0x06003895 RID: 14485 RVA: 0x000F4949 File Offset: 0x000F2D49
		[ConsoleCommand("daysPerSecond")]
		private static float daysPerSecond
		{
			get
			{
				return Singleton<EnvironmentManager>.instance.daysPerSecond;
			}
			set
			{
				Singleton<EnvironmentManager>.instance.daysPerSecond = value;
			}
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000F4956 File Offset: 0x000F2D56
		[DebugSetting("Cycle Rain", "循环降雨", DebugSettingLocation.Level)]
		private static void CycleRain()
		{
			EnviromentDebug.rainfall = ((EnviromentDebug.rainfall < 1f) ? Mathf.Clamp(EnviromentDebug.rainfall + 0.1f, 0f, 1f) : 0f);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000F4990 File Offset: 0x000F2D90
		[DebugSetting("Cycle Snowfall", DebugSettingLocation.Level)]
		private static void CycleSnowfall()
		{
			EnviromentDebug.snowfall = ((EnviromentDebug.snowfall < 1f) ? Mathf.Clamp(EnviromentDebug.snowfall + 0.1f, 0f, 1f) : 0f);
		}

		// Token: 0x0400267E RID: 9854
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("Environment Debug", EVerbosity.Normal, 0);
	}
}
