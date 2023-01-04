using System;
using System.Collections.Generic;

namespace CS.Platform.Core.Server.Part
{
	// Token: 0x0200009F RID: 159
	public class A2SPlayers
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x00019C30 File Offset: 0x00018030
		public A2SPlayers()
		{
			this._sendPlayerInfo[0] = 68;
			this._sendPlayerInfo[1] = 0;
			this._playerInfoOffsets[0] = 2;
			this._playerInfoOffsets[1] = 2;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00019C8F File Offset: 0x0001808F
		public byte[] sendPacket
		{
			get
			{
				return this._sendPlayerInfo;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00019C97 File Offset: 0x00018097
		public int SendPacketSize
		{
			get
			{
				return this._playerInfoOffsets[this._playerInfoOffsets.Length - 1];
			}
		}

		// Token: 0x040002BC RID: 700
		private byte[] _sendPlayerInfo = new byte[A2SUtils.PacketMaxSize];

		// Token: 0x040002BD RID: 701
		private int[] _playerInfoOffsets = new int[2];

		// Token: 0x040002BE RID: 702
		private List<BaseUserInfo> connectedUsers = new List<BaseUserInfo>();
	}
}
