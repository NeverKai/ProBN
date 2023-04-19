using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace CS.Platform.Core.Server
{
	// Token: 0x020000A1 RID: 161
	public class A2SServerInfo
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00019D16 File Offset: 0x00018116
		public void Initialise()
		{
			this._ResponceHeader = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue
			};
			this._Data.Header = 73;
			this._Data.EDF = 0;                                                                        
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00019D48 File Offset: 0x00018148
		private int GenerateBuffer()
		{
			Array.Clear(this._SendBuffer, 0, this._SendBuffer.Length);
			int result = 0;
			this.InsertData<byte>(ref this._SendBuffer, this._ResponceHeader, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.Header, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.Protocol, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this.ConvertStringToNullTerminatedBytes(this._Data.ServerName), ref result);
			this.InsertData<byte>(ref this._SendBuffer, this.ConvertStringToNullTerminatedBytes(this._Data.Map), ref result);
			this.InsertData<byte>(ref this._SendBuffer, this.ConvertStringToNullTerminatedBytes(this._Data.Folder), ref result);
			this.InsertData<byte>(ref this._SendBuffer, this.ConvertStringToNullTerminatedBytes(this._Data.GameName), ref result);
			this.InsertData<byte>(ref this._SendBuffer, BitConverter.GetBytes(this._Data.ID), ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.Players, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.MaxPlayers, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.Bots, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.ServerType, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.Environment, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.Visibility, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.VAC, ref result);
			this.InsertData<byte>(ref this._SendBuffer, this.ConvertStringToNullTerminatedBytes(this._Data.Version), ref result);
			this.InsertData<byte>(ref this._SendBuffer, this._Data.EDF, ref result);
			if ((this._Data.EDF & 128) == 128)
			{
				this.InsertData<byte>(ref this._SendBuffer, BitConverter.GetBytes(this._Data.GamePort), ref result);
			}
			if ((this._Data.EDF & 16) == 16)
			{
				this.InsertData<byte>(ref this._SendBuffer, BitConverter.GetBytes(this._Data.SteamID), ref result);
			}
			if ((this._Data.EDF & 64) == 64)
			{
				this.InsertData<byte>(ref this._SendBuffer, BitConverter.GetBytes(this._Data.SourceTVPort), ref result);
				this.InsertData<byte>(ref this._SendBuffer, this.ConvertStringToNullTerminatedBytes(this._Data.SourceTVName), ref result);
			}
			if ((this._Data.EDF & 32) == 32)
			{
				this.InsertData<byte>(ref this._SendBuffer, this.ConvertStringToNullTerminatedBytes(this._Data.Keywords), ref result);
			}
			if ((this._Data.EDF & 1) == 1)
			{
				this.InsertData<byte>(ref this._SendBuffer, BitConverter.GetBytes(this._Data.GameID), ref result);
			}
			return result;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001A056 File Offset: 0x00018456
		private void InsertData<T>(ref T[] array, T item, ref int index)
		{
			if (index >= array.Length)
			{
				Debug.LogError("InsertData Failed. Array isn't long enough for the item");
				return;
			}
			array[index] = item;
			index++;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001A080 File Offset: 0x00018480
		private void InsertData<T>(ref T[] array, T[] items, ref int index)
		{
			if (index + items.Length >= array.Length)
			{
				Debug.LogError("InsertData Failed. Array isn't long enough for these items");
				return;
			}
			for (int i = 0; i < items.Length; i++)
			{
				array[index + i] = items[i];
			}
			index += items.Length;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001A0D8 File Offset: 0x000184D8
		private byte[] ConvertStringToNullTerminatedBytes(string str)
		{
			if (str == null)
			{
				return new byte[1];
			}
			byte[] array = Encoding.UTF8.GetBytes(str);
			if (array[array.Length - 1] == 0)
			{
				return array;
			}
			byte[] array2 = array;
			array = new byte[array2.Length + 1];
			Buffer.BlockCopy(array2, 0, array, 0, array2.Length);
			array[array.Length - 1] = 0;
			return array;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001A130 File Offset: 0x00018530
		public void Send(ref UdpClient port, ref IPEndPoint target)
		{
			int bytes = this.GenerateBuffer();
			port.Send(this._SendBuffer, bytes, target);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001A155 File Offset: 0x00018555
		public void SetServerName(string value)
		{
			this._Data.ServerName = value;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001A163 File Offset: 0x00018563
		public void SetCurrentMap(string value)
		{
			this._Data.Map = value;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001A171 File Offset: 0x00018571
		public void SetFolder(string value)
		{
			this._Data.Folder = value;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001A17F File Offset: 0x0001857F
		public void SetGameName(string value)
		{
			this._Data.GameName = value;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001A18D File Offset: 0x0001858D
		public void SetServerGameVersion(string value)
		{
			this._Data.Version = value;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001A19B File Offset: 0x0001859B
		public void SetKeyWords(string value)
		{
			if ((this._Data.EDF & 32) != 32)
			{
				this._Data.EDF = (byte) (this._Data.EDF | 32);
			}
			this._Data.Keywords = value;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001A1D3 File Offset: 0x000185D3
		public void SetProtocol(byte value)
		{
			this._Data.Protocol = value;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001A1E1 File Offset: 0x000185E1
		public void SetGameID(short value)
		{
			this._Data.ID = value;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001A1EF File Offset: 0x000185EF
		public void SetPlayers(byte value)
		{
			this._Data.Players = value;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001A1FD File Offset: 0x000185FD
		public void SetPlayers(int value)
		{
			if (value > 255)
			{
				value = 255;
				Debug.LogWarningFormat("[A2S] Warning! Server players is type byte so truncated to '255' from '{0}", new object[]
				{
					value
				});
			}
			this._Data.Players = BitConverter.GetBytes(value)[0];
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001A23D File Offset: 0x0001863D
		public void SetMaxPlayers(byte value)
		{
			this._Data.MaxPlayers = value;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001A24B File Offset: 0x0001864B
		public void SetMaxPlayers(int value)
		{
			if (value > 255)
			{
				value = 255;
				Debug.LogWarningFormat("[BaseA2S] Warning! Server max players is type byte so truncated to '255' from '{0}", new object[]
				{
					value
				});
			}
			this._Data.MaxPlayers = BitConverter.GetBytes(value)[0];
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001A28B File Offset: 0x0001868B
		public void SetBots(byte value)
		{
			this._Data.Bots = value;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001A29C File Offset: 0x0001869C
		public void SetServerType(A2SServerInfo.ServerType value)
		{
			if (value != A2SServerInfo.ServerType.Dedicated)
			{
				if (value != A2SServerInfo.ServerType.NonDedicated)
				{
					if (value == A2SServerInfo.ServerType.Proxy)
					{
						this._Data.ServerType = 112;
					}
				}
				else
				{
					this._Data.ServerType = 108;
				}
			}
			else
			{
				this._Data.ServerType = 100;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001A2F8 File Offset: 0x000186F8
		public void SetServerOS(A2SServerInfo.ServerOS value)
		{
			if (value != A2SServerInfo.ServerOS.Linux)
			{
				if (value != A2SServerInfo.ServerOS.Windows)
				{
					if (value == A2SServerInfo.ServerOS.Mac)
					{
						this._Data.Environment = 109;
					}
				}
				else
				{
					this._Data.Environment = 119;
				}
			}
			else
			{
				this._Data.Environment = 108;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001A354 File Offset: 0x00018754
		public void SetPrivate(bool value)
		{
			this._Data.Visibility = (byte) ((!value) ? 0 : 1);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001A36E File Offset: 0x0001876E
		public void SetVac(bool value)
		{
			this._Data.VAC = (byte) ((!value) ? 0 : 1);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001A388 File Offset: 0x00018788
		public void SetGamePort(short value)
		{
			if ((this._Data.EDF & 128) != 128)
			{
				this._Data.EDF = (byte) (this._Data.EDF | 128);
			}
			this._Data.GamePort = value;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001A3D4 File Offset: 0x000187D4
		public void SetSteamServerID(long value)
		{
			if ((this._Data.EDF & 16) != 16)
			{
				this._Data.EDF = (byte) (this._Data.EDF | 16);
			}
			this._Data.SteamID = value;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001A40C File Offset: 0x0001880C
		public void SetGameID(long value)
		{
			if ((this._Data.EDF & 1) != 1)
			{
				this._Data.EDF = (byte) (this._Data.EDF | 1);
			}
			this._Data.GameID = value;
		}

		// Token: 0x040002C1 RID: 705
		private const int kPacketSize = 1400;

		// Token: 0x040002C2 RID: 706
		private byte[] _ResponceHeader;

		// Token: 0x040002C3 RID: 707
		private byte[] _SendBuffer = new byte[1400];

		// Token: 0x040002C4 RID: 708
		private A2SServerInfo.A2SServerInfoData _Data = default(A2SServerInfo.A2SServerInfoData);

		// Token: 0x020000A2 RID: 162
		public struct A2SServerInfoData
		{
			// Token: 0x040002C5 RID: 709
			public byte Header;

			// Token: 0x040002C6 RID: 710
			public byte Protocol;

			// Token: 0x040002C7 RID: 711
			public string ServerName;

			// Token: 0x040002C8 RID: 712
			public string Map;

			// Token: 0x040002C9 RID: 713
			public string Folder;

			// Token: 0x040002CA RID: 714
			public string GameName;

			// Token: 0x040002CB RID: 715
			public short ID;

			// Token: 0x040002CC RID: 716
			public byte Players;

			// Token: 0x040002CD RID: 717
			public byte MaxPlayers;

			// Token: 0x040002CE RID: 718
			public byte Bots;

			// Token: 0x040002CF RID: 719
			public byte ServerType;

			// Token: 0x040002D0 RID: 720
			public byte Environment;

			// Token: 0x040002D1 RID: 721
			public byte Visibility;

			// Token: 0x040002D2 RID: 722
			public byte VAC;

			// Token: 0x040002D3 RID: 723
			public string Version;

			// Token: 0x040002D4 RID: 724
			public byte EDF;

			// Token: 0x040002D5 RID: 725
			public short GamePort;

			// Token: 0x040002D6 RID: 726
			public long SteamID;

			// Token: 0x040002D7 RID: 727
			public short SourceTVPort;

			// Token: 0x040002D8 RID: 728
			public string SourceTVName;

			// Token: 0x040002D9 RID: 729
			public string Keywords;

			// Token: 0x040002DA RID: 730
			public long GameID;
		}

		// Token: 0x020000A3 RID: 163
		public enum ServerType
		{
			// Token: 0x040002DC RID: 732
			Dedicated,
			// Token: 0x040002DD RID: 733
			NonDedicated,
			// Token: 0x040002DE RID: 734
			Proxy
		}

		// Token: 0x020000A4 RID: 164
		public enum ServerOS
		{
			// Token: 0x040002E0 RID: 736
			Linux,
			// Token: 0x040002E1 RID: 737
			Windows,
			// Token: 0x040002E2 RID: 738
			Mac
		}
	}
}
