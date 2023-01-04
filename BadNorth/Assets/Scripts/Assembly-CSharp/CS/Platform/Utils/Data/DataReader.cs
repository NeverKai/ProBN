using System;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000051 RID: 81
	public class DataReader : DataStream
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00011DDF File Offset: 0x000101DF
		public DataReader()
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00011DE7 File Offset: 0x000101E7
		public DataReader(DataStream baseStream, bool copy) : base(baseStream, copy)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00011DF1 File Offset: 0x000101F1
		public DataReader(byte[] data, bool copy) : base(data, copy)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00011DFC File Offset: 0x000101FC
		public ushort ReadUInt16()
		{
			ushort num = 0;
			if ((long)(this._bufferPoint + 2) <= (long)((ulong)this._dataSize))
			{
				num = BitConverter.ToUInt16(this._data, this._bufferPoint);
				if (this._dataNeedsFlipped)
				{
					num = (ushort)((int)(num & 255) << 8 | (num & 65280) >> 8);
				}
				this._bufferPoint += 2;
			}
			return num;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00011E64 File Offset: 0x00010264
		public uint ReadUInt32()
		{
			uint num = 0U;
			if ((long)(this._bufferPoint + 4) <= (long)((ulong)this._dataSize))
			{
				num = BitConverter.ToUInt32(this._data, this._bufferPoint);
				if (this._dataNeedsFlipped)
				{
					num = (num >> 16 | num << 16);
					num = ((num & 16711935U) << 8 | (num & 4278255360U) >> 8);
				}
				this._bufferPoint += 4;
			}
			return num;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00011ED4 File Offset: 0x000102D4
		public ulong ReadUInt64()
		{
			ulong num = 0UL;
			if ((long)(this._bufferPoint + 8) <= (long)((ulong)this._dataSize))
			{
				num = BitConverter.ToUInt64(this._data, this._bufferPoint);
				if (this._dataNeedsFlipped)
				{
					num = (num >> 32 | num << 32);
					num = ((num & 18446462603027742720UL) << 16 | (num & 281470681808895UL) >> 16);
					num = ((num & 71777214294589695UL) << 8 | (num & 18374966859414961920UL) >> 8);
				}
				this._bufferPoint += 8;
			}
			return num;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00011F69 File Offset: 0x00010369
		public short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00011F72 File Offset: 0x00010372
		public int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00011F7A File Offset: 0x0001037A
		public long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00011F82 File Offset: 0x00010382
		public float ReadSingle()
		{
			return this.ReadUInt32();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00011F8C File Offset: 0x0001038C
		public double ReadDouble()
		{
			return this.ReadUInt64();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011F96 File Offset: 0x00010396
		public char ReadChar()
		{
			return (char)this.ReadUInt16();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00011FA0 File Offset: 0x000103A0
		public string ReadString()
		{
			string text = string.Empty;
			int num = this.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				if ((long)(this._bufferPoint + 2) > (long)((ulong)this._dataSize))
				{
					Debug.LogError("[PDR] String data starved | Expected: {0} | Got: {1} | Data: {2} | At: {3}", new object[]
					{
						num,
						text.Length,
						this._data.Length,
						this._bufferPoint
					});
					break;
				}
				text += this.ReadChar();
			}
			return text;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012040 File Offset: 0x00010440
		public bool ReadBool()
		{
			bool result = false;
			if ((long)(this._bufferPoint + 1) <= (long)((ulong)this._dataSize))
			{
				result = BitConverter.ToBoolean(this._data, this._bufferPoint);
				this._bufferPoint++;
			}
			return result;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012088 File Offset: 0x00010488
		public byte[] ReadData()
		{
			int num = this.ReadInt32();
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._bufferPoint, array, 0, num);
			this._bufferPoint += num;
			return array;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000120D0 File Offset: 0x000104D0
		public byte ReadByte()
		{
			byte result = 0;
			if ((long)(this._bufferPoint + 1) <= (long)((ulong)this._dataSize))
			{
				result = this._data[this._bufferPoint];
				this._bufferPoint++;
			}
			return result;
		}
	}
}
