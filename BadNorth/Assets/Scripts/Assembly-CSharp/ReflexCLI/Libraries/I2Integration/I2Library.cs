using System.Collections.Generic;
using I2.Loc;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense;

namespace ReflexCLI.Libraries.I2Integration
{
	// Token: 0x02000465 RID: 1125
	[ConsoleCommandClassCustomizer("Localization")]
	internal static class I2Library
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0004372E File Offset: 0x00041B2E
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x0004373A File Offset: 0x00041B3A
		[ConsoleCommand("")]
		private static I2Language CurrentLanguage
		{
			get
			{
				return LocalizationManager.CurrentLanguage;
			}
			set
			{
				if (LocalizationManager.HasLanguage(value, true, true, true))
				{
					LocalizationManager.CurrentLanguage = value;
				}
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0004375C File Offset: 0x00041B5C
		[ConsoleCommand("")]
		private static string CycleLanguage()
		{
			List<string> allLanguages = LocalizationManager.GetAllLanguages(true);
			int i = 0;
			int count = allLanguages.Count;
			while (i < count)
			{
				string a = allLanguages[i];
				if (a == LocalizationManager.CurrentLanguage)
				{
					break;
				}
				i++;
			}
			i = (i + 1) % count;
			LocalizationManager.CurrentLanguage = allLanguages[i];
			return LocalizationManager.CurrentLanguage;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000437BD File Offset: 0x00041BBD
		[DebugSetting("Cycle Language", DebugSettingLocation.All)]
		private static void CycleLanguageSetting()
		{
			I2Library.CycleLanguage();
			Debug.LogFormat("Changing Langauge To '{0}'", new object[]
			{
				LocalizationManager.CurrentLanguage
			});
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000437E0 File Offset: 0x00041BE0
		[ConsoleCommand("")]
		private static string Translate(I2Term term, I2Language language)
		{
			string term2 = term;
			string overrideLanguage = language;
			return ScriptLocalization.Get(term2, true, 0, true, false, null, overrideLanguage);
		}

		// Token: 0x04000FCD RID: 4045
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("LocalizationHelpers", EVerbosity.Normal, 0);
	}
}
