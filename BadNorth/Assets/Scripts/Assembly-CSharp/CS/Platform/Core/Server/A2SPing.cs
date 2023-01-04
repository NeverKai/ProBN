using System;
using System.Net;
using System.Net.Sockets;

namespace CS.Platform.Core.Server
{
	// Token: 0x0200009E RID: 158
	public class A2SPing
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x00019B88 File Offset: 0x00017F88
		public A2SPing()
		{
			this._PingInfoBuffer[0] = byte.MaxValue;
			this._PingInfoBuffer[1] = byte.MaxValue;
			this._PingInfoBuffer[2] = byte.MaxValue;
			this._PingInfoBuffer[3] = byte.MaxValue;
			this._PingInfoBuffer[4] = 106;
			for (int i = 5; i < 13; i++)
			{
				this._PingInfoBuffer[i] = 48;
			}
			this._PingInfoBuffer[this._PingInfoBuffer.Length - 1] = 0;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00019C15 File Offset: 0x00018015
		public void Send(ref UdpClient port, ref IPEndPoint target)
		{
			port.Send(this._PingInfoBuffer, this._PingInfoBuffer.Length, target);
		}

		// Token: 0x040002B9 RID: 697
		private const int kHeaderSize = 5;

		// Token: 0x040002BA RID: 698
		private const int kPayloadSize = 15;

		// Token: 0x040002BB RID: 699
		private byte[] _PingInfoBuffer = new byte[20];
	}
}
