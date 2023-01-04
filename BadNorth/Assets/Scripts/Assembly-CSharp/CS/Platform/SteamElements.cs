using System;
using Steamworks;

namespace CS.Platform
{
	// Token: 0x02000057 RID: 87
	public class SteamElements
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0001220E File Offset: 0x0001060E
		public static int GetChannel(CSteamID theID)
		{
			if (theID.BGameServerAccount())
			{
				return SteamElements.ServerChannel;
			}
			return SteamElements.ClientChannel;
		}

		// Token: 0x04000197 RID: 407
		public static int MaxTicketSize = 1300;

		// Token: 0x04000198 RID: 408
		public static int ClientChannel = 1;

		// Token: 0x04000199 RID: 409
		public static int ServerChannel = 2;

		// Token: 0x0400019A RID: 410
		public static int VoiceChannel = 3;
	}
}
