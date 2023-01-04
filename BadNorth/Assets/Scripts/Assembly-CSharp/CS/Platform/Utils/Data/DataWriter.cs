using System;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000050 RID: 80
	public class DataWriter : DataStream
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00011B0E File Offset: 0x0000FF0E
		public DataWriter(int bufferSize) : base(bufferSize)
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00011B17 File Offset: 0x0000FF17
		public DataWriter(DataStream baseStream, bool copy) : base(baseStream, copy)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00011B21 File Offset: 0x0000FF21
		public DataWriter(byte[] data, bool copy) : base(data, copy)
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00011B2B File Offset: 0x0000FF2B
		public void SetFlag(byte type)
		{
			base.Flag = type;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00011B34 File Offset: 0x0000FF34
		public void WriteInt16(short value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 2U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011B64 File Offset: 0x0000FF64
		public void WriteInt32(int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 4U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00011B94 File Offset: 0x0000FF94
		public void WriteInt64(long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 8U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011BC4 File Offset: 0x0000FFC4
		public void WriteUInt16(ushort value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 2U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011BF4 File Offset: 0x0000FFF4
		public void WriteUInt32(uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 4U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011C24 File Offset: 0x00010024
		public void WriteUInt64(ulong value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 8U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011C54 File Offset: 0x00010054
		public void WriteSingle(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 4U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011C84 File Offset: 0x00010084
		public void WriteDouble(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 8U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00011CB4 File Offset: 0x000100B4
		public void WriteChar(char value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 2U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011CE4 File Offset: 0x000100E4
		public void WriteString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.WriteInt32(0);
			}
			else
			{
				this.WriteInt32(value.Length);
				for (int i = 0; i < value.Length; i++)
				{
					this.WriteChar(value[i]);
				}
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00011D38 File Offset: 0x00010138
		public void WriteBool(bool value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (this._dataNeedsFlipped)
			{
				Data.EndianFlip(bytes, 1U, 0U, 1U);
			}
			base.AddData(bytes, 0);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00011D68 File Offset: 0x00010168
		public void WriteData(DataStream value)
		{
			if (value != null && value.DataSize > 0)
			{
				this.WriteInt32(value.DataSize);
				base.AddData(value.DataBuffer, value.DataSize);
			}
			else
			{
				this.WriteInt32(0);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00011DA6 File Offset: 0x000101A6
		public void WriteData(byte[] value)
		{
			if (value != null && value.Length > 0)
			{
				this.WriteInt32(value.Length);
				base.AddData(value, value.Length);
			}
			else
			{
				this.WriteInt32(0);
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011DD6 File Offset: 0x000101D6
		public void WriteByte(byte value)
		{
			base.AddData(value);
		}
	}
}
