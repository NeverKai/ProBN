using System;
using ReflexCLI.Attributes;
using UnityEngine;

namespace ReflexCLI.Libraries
{
	// Token: 0x02000459 RID: 1113
	[ConsoleCommandClassCustomizer("Display")]
	internal static class DisplayCommands
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00043281 File Offset: 0x00041681
		private static Resolution res
		{
			get
			{
				return Screen.currentResolution;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00043288 File Offset: 0x00041688
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x0004328F File Offset: 0x0004168F
		[ConsoleCommand("")]
		private static Resolution ScreenResolution
		{
			get
			{
				return DisplayCommands.res;
			}
			set
			{
				Screen.SetResolution(value.width, value.height, Screen.fullScreen);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x000432A9 File Offset: 0x000416A9
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x000432B0 File Offset: 0x000416B0
		[ConsoleCommand("")]
		private static bool FullScreen
		{
			get
			{
				return Screen.fullScreen;
			}
			set
			{
				Screen.fullScreen = value;
			}
		}
	}
}
