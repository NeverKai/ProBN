using System;

namespace CS.Platform
{
	// Token: 0x0200005A RID: 90
	public struct LobbyInformation
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x00012245 File Offset: 0x00010645
		public void Blank()
		{
			this.host.Blank();
			this.maxSlots = 0U;
			this.lobbyType = LOBBY_TYPE.PUBLIC;
		}

		// Token: 0x040001A3 RID: 419
		public BaseUserInfo host;

		// Token: 0x040001A4 RID: 420
		public uint maxSlots;

		// Token: 0x040001A5 RID: 421
		public LOBBY_TYPE lobbyType;
	}
}
