using System;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000053 RID: 83
	public class PlatformMessageBase : PlatformDataWrapper
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00008A94 File Offset: 0x00006E94
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00008A9C File Offset: 0x00006E9C
		public MessageTypes MessageType
		{
			get
			{
				return (MessageTypes)base.Flag;
			}
			set
			{
				base.Flag = checked((byte)value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00008AA6 File Offset: 0x00006EA6
		public ulong FallbackID
		{
			set
			{
				if (this._fallbackID != value)
				{
					this._fallbackID = value;
					base.Dirty = true;
				}
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00008AC2 File Offset: 0x00006EC2
		protected override void Serialize(DataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUInt64(this._fallbackID);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00008AD7 File Offset: 0x00006ED7
		protected override void Deserialize(DataReader reader)
		{
			base.Deserialize(reader);
			this.FallbackID = reader.ReadUInt64();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00008AEC File Offset: 0x00006EEC
		public static void SetToAfterFallbackID(DataReader message)
		{
			if (message != null)
			{
				message.BufferPoint = 8;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00008AFC File Offset: 0x00006EFC
		public static ulong ReadFallbackID(byte[] message)
		{
			ulong num = 0UL;
			if (message != null && 14 <= message.Length)
			{
				num = BitConverter.ToUInt64(message, 6);
				if (message[0] == 0 && !BitConverter.IsLittleEndian)
				{
					num = (num >> 32 | num << 32);
					num = ((num & 18446462603027742720UL) << 16 | (num & 281470681808895UL) >> 16);
					num = ((num & 71777214294589695UL) << 8 | (num & 18374966859414961920UL) >> 8);
				}
			}
			return num;
		}

		// Token: 0x04000180 RID: 384
		private ulong _fallbackID;
	}
}
