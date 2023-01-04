using System;

namespace CS.Platform.Core.Server.Part
{
	// Token: 0x020000A0 RID: 160
	public class A2SRules
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00019CCF File Offset: 0x000180CF
		public byte[] sendPacket
		{
			get
			{
				return this._sendServerInfo;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00019CD7 File Offset: 0x000180D7
		public int SendPacketSize
		{
			get
			{
				return this._serverInfoOffsets[9];
			}
		}

		// Token: 0x040002BF RID: 703
		private byte[] _sendServerInfo = new byte[A2SUtils.PacketMaxSize];

		// Token: 0x040002C0 RID: 704
		private int[] _serverInfoOffsets = new int[10];
	}
}
