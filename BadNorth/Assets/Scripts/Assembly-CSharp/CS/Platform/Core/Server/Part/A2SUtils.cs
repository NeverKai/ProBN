using System;
using System.Text;

namespace CS.Platform.Core.Server.Part
{
	// Token: 0x020000A5 RID: 165
	public static class A2SUtils
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x0001A444 File Offset: 0x00018844
		public static void AddOffset(int[] members, int start, int amount)
		{
			for (int i = start; i < members.Length; i++)
			{
				members[i] += amount;
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001A474 File Offset: 0x00018874
		public static void AddString(string value, ref byte[] target, ref int offset)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, target, offset, bytes.Length);
			offset += bytes.Length;
			target[offset] = 0;
			offset++;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001A4B0 File Offset: 0x000188B0
		public static int InjectString(string value, ref byte[] target, int startOriginal, int endOriginal)
		{
			if (A2SUtils._tempPacket == null)
			{
				A2SUtils._tempPacket = new byte[A2SUtils.SendPacketSize];
			}
			Buffer.BlockCopy(target, 0, A2SUtils._tempPacket, 0, target.Length);
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, target, startOriginal, bytes.Length);
			target[startOriginal + bytes.Length] = 0;
			Buffer.BlockCopy(A2SUtils._tempPacket, endOriginal, target, startOriginal + bytes.Length + 1, target.Length - (startOriginal + bytes.Length + 1));
			return startOriginal + bytes.Length + 1 - endOriginal;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001A534 File Offset: 0x00018934
		public static int InjectBytes(byte[] bytes, ref byte[] target, int startOriginal, int endOriginal)
		{
			if (A2SUtils._tempPacket == null)
			{
				A2SUtils._tempPacket = new byte[A2SUtils.SendPacketSize];
			}
			Buffer.BlockCopy(target, 0, A2SUtils._tempPacket, 0, target.Length);
			Buffer.BlockCopy(bytes, 0, target, startOriginal, bytes.Length);
			Buffer.BlockCopy(A2SUtils._tempPacket, endOriginal, target, startOriginal + bytes.Length, target.Length - (startOriginal + bytes.Length));
			return startOriginal + bytes.Length - endOriginal;
		}

		// Token: 0x040002E3 RID: 739
		private static byte[] _tempPacket;

		// Token: 0x040002E4 RID: 740
		public static int PacketMaxSize = 1400;

		// Token: 0x040002E5 RID: 741
		public static int SendPacketSize = 1408;
	}
}
