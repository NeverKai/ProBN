using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000CF RID: 207
	public struct Lobby
	{
		// Token: 0x040003C4 RID: 964
		public long Id;

		// Token: 0x040003C5 RID: 965
		public LobbyType Type;

		// Token: 0x040003C6 RID: 966
		public long OwnerId;

		// Token: 0x040003C7 RID: 967
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Secret;

		// Token: 0x040003C8 RID: 968
		public uint Capacity;

		// Token: 0x040003C9 RID: 969
		public bool Locked;
	}
}
