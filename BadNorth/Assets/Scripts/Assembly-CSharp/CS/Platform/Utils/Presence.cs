using System;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x0200007C RID: 124
	public static class Presence
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x000161EE File Offset: 0x000145EE
		public static void PreseneceSetup(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/PresenceData";
			}
			Presence._presenceInfomation = (Resources.Load(databaseLocation) as PlatformPresenceDatabase);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00016212 File Offset: 0x00014612
		public static string GetConnecter()
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.StatusConnectionType;
			}
			return null;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00016230 File Offset: 0x00014630
		public static string GetSteamPresence(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetSteamPresence(key);
			}
			return null;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001624F File Offset: 0x0001464F
		public static string GetPS4Presence(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetPS4Presence(key);
			}
			return null;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001626E File Offset: 0x0001466E
		public static string GetDiscordPresence(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetDiscordPresence(key);
			}
			return null;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001628D File Offset: 0x0001468D
		public static string GetXboxPresence(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetXboxPresence(key);
			}
			return null;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000162AC File Offset: 0x000146AC
		public static string GetSteamStatus(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetSteamStatus(key);
			}
			return null;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000162CB File Offset: 0x000146CB
		public static string GetPS4Status(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetPS4Status(key);
			}
			return null;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000162EA File Offset: 0x000146EA
		public static string GetDiscordStatus(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetDiscordStatus(key);
			}
			return null;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00016309 File Offset: 0x00014709
		public static string GetDiscordImage(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetDiscordImage(key);
			}
			return null;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00016328 File Offset: 0x00014728
		public static string GetDiscordImageText(string key)
		{
			if (Presence._presenceInfomation != null)
			{
				return Presence._presenceInfomation.GetDiscordImageText(key);
			}
			return null;
		}

		// Token: 0x04000231 RID: 561
		private static PlatformPresenceDatabase _presenceInfomation;
	}
}
