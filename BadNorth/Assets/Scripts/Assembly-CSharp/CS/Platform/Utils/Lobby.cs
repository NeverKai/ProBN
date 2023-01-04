using System;
using CS.Platform.Utils.Data;
using UnityEngine.Networking;

namespace CS.Platform.Utils
{
	// Token: 0x02000071 RID: 113
	public static class Lobby
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x00015294 File Offset: 0x00013694
		public static Guid Deserialize(NetworkReader reader)
		{
			return new Guid(reader.ReadUInt32(), reader.ReadUInt16(), reader.ReadUInt16(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000152E8 File Offset: 0x000136E8
		public static void Serialize(NetworkWriter writer, Guid lobbyID)
		{
			byte[] array = lobbyID.ToByteArray();
			writer.Write(BitConverter.ToUInt32(array, 0));
			writer.Write(BitConverter.ToUInt16(array, 4));
			writer.Write(BitConverter.ToUInt16(array, 6));
			writer.Write(array[6]);
			writer.Write(array[7]);
			writer.Write(array[8]);
			writer.Write(array[9]);
			writer.Write(array[10]);
			writer.Write(array[11]);
			writer.Write(array[12]);
			writer.Write(array[13]);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00015374 File Offset: 0x00013774
		public static Guid UlongToGuid(ulong lobbyID)
		{
			byte[] bytes = BitConverter.GetBytes(lobbyID);
			if (BitConverter.IsLittleEndian)
			{
				Data.EndianFlip(bytes, 8U, 0U, 1U);
			}
			return new Guid(0, 0, 0, bytes);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000153A4 File Offset: 0x000137A4
		public static ulong GuidToUlong(Guid lobbyID)
		{
			byte[] array = lobbyID.ToByteArray();
			if (BitConverter.IsLittleEndian)
			{
				Data.EndianFlip(array, 8U, 8U, 1U);
			}
			return BitConverter.ToUInt64(array, 8);
		}

		// Token: 0x04000224 RID: 548
		public static string LobbyDataUpdateFlag = "LOBBY_UPDATE";
	}
}
