using System;

namespace CS.Platform.Utils.Data
{
	// Token: 0x0200004F RID: 79
	public class DataStream
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x000116CC File Offset: 0x0000FACC
		public DataStream()
		{
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000116D4 File Offset: 0x0000FAD4
		public DataStream(byte[] data, bool copy)
		{
			this.ApplyDataSteam(data, copy);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000116E5 File Offset: 0x0000FAE5
		public DataStream(int bufferSize)
		{
			this.ReadyStream(bufferSize);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000116F4 File Offset: 0x0000FAF4
		public DataStream(DataStream baseStream, bool copy)
		{
			this.ApplyDataSteam(baseStream.DataBuffer, copy);
			this.BufferPoint = baseStream.BufferPoint;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00011716 File Offset: 0x0000FB16
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00011726 File Offset: 0x0000FB26
		public int BufferPoint
		{
			get
			{
				return Math.Max(-1, this._bufferPoint - 6);
			}
			set
			{
				this._bufferPoint = 6 + Math.Max(0, value);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00011737 File Offset: 0x0000FB37
		public int DataSize
		{
			get
			{
				return (int)this._dataSize;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00011740 File Offset: 0x0000FB40
		public byte[] DataBuffer
		{
			get
			{
				if (this._dataSizeChange)
				{
					Buffer.BlockCopy(BitConverter.GetBytes(this._dataSize), 0, this._data, 2, 4);
					if (this._dataNeedsFlipped)
					{
						Data.EndianFlip(this._data, 4U, 2U, 1U);
					}
					this._dataSizeChange = false;
				}
				return this._data;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00011797 File Offset: 0x0000FB97
		public int RawBufferPoint
		{
			get
			{
				return this._bufferPoint;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001179F File Offset: 0x0000FB9F
		public byte DataAt(int index)
		{
			if (this._data != null && 0 <= index && index < this._data.Length)
			{
				return this._data[index];
			}
			return 0;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000117CB File Offset: 0x0000FBCB
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x000117E2 File Offset: 0x0000FBE2
		public byte Flag
		{
			get
			{
				if (this._data != null)
				{
					return this._data[1];
				}
				return 0;
			}
			protected set
			{
				if (this._data == null || this._data[1] != value)
				{
					this.ReadyStream(0);
					this._data[1] = value;
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00011810 File Offset: 0x0000FC10
		private void ReadyStream(int bufferSize = 0)
		{
			if (this._data == null)
			{
				this._data = new byte[6 + bufferSize];
				this._data[0] = ((!BitConverter.IsLittleEndian) ? byte.MaxValue : 0);
				Buffer.BlockCopy(BitConverter.GetBytes(0U), 0, this._data, 2, 4);
				this._bufferPoint = 6;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011870 File Offset: 0x0000FC70
		public bool ApplyDataSteam(byte[] newData, bool copy)
		{
			if (newData == null || newData.Length < 6)
			{
				return false;
			}
			this._dataNeedsFlipped = (newData[0] == 0 && !BitConverter.IsLittleEndian);
			this._bufferPoint = 6;
			this._dataSize = BitConverter.ToUInt32(newData, 2);
			if (this._dataNeedsFlipped)
			{
				this._dataSize = (this._dataSize >> 16 | this._dataSize << 16);
				this._dataSize = ((this._dataSize & 16711935U) << 8 | (this._dataSize & 4278255360U) >> 8);
			}
			if ((ulong)this._dataSize > (ulong)((long)newData.Length))
			{
				int dataSize = (int)this._dataSize;
				int num = newData.Length;
				Debug.LogError("[PD] Appling data with size larger then byte array | Thinks: {0} | Is: {1}", new object[]
				{
					dataSize,
					num
				});
				this._dataSize = (uint)newData.Length;
			}
			if (copy)
			{
				this._data = new byte[this._dataSize];
				Buffer.BlockCopy(newData, 0, this._data, 0, this._data.Length);
			}
			else
			{
				this._data = newData;
			}
			return true;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00011984 File Offset: 0x0000FD84
		protected void AddData(byte[] data, int amount = 0)
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			if (amount == 0 || amount < data.Length)
			{
				amount = data.Length;
			}
			if (this._data == null)
			{
				this.ReadyStream(amount);
			}
			if (this._data.Length < this._bufferPoint + amount)
			{
				byte[] array = new byte[this._bufferPoint + amount];
				Buffer.BlockCopy(this._data, 0, array, 0, this._bufferPoint);
				this._data = array;
			}
			Buffer.BlockCopy(data, 0, this._data, this._bufferPoint, amount);
			this._bufferPoint += amount;
			if ((ulong)this._dataSize < (ulong)((long)this._bufferPoint))
			{
				this._dataSize = checked((uint)this._bufferPoint);
				this._dataSizeChange = true;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00011A50 File Offset: 0x0000FE50
		protected void AddData(byte data)
		{
			if (this._data == null)
			{
				this.ReadyStream(1);
			}
			if (this._data.Length < this._bufferPoint + 1)
			{
				byte[] array = new byte[this._bufferPoint + 1];
				Buffer.BlockCopy(this._data, 0, array, 0, this._bufferPoint);
				this._data = array;
			}
			this._data[this._bufferPoint] = data;
			this._bufferPoint++;
			if ((ulong)this._dataSize < (ulong)((long)this._bufferPoint))
			{
				this._dataSize = checked((uint)this._bufferPoint);
				this._dataSizeChange = true;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011AF0 File Offset: 0x0000FEF0
		public void ClearStream()
		{
			this.ReadyStream(0);
			this.BufferPoint = 0;
			this._dataSize = 6U;
			this._dataSizeChange = true;
		}

		// Token: 0x04000176 RID: 374
		public const int HEADER_TYPE_START = 1;

		// Token: 0x04000177 RID: 375
		public const int HEADER_DATA_AMOUNT = 2;

		// Token: 0x04000178 RID: 376
		public const int HEADER_DATA_START = 6;

		// Token: 0x04000179 RID: 377
		protected bool _dataNeedsFlipped;

		// Token: 0x0400017A RID: 378
		protected byte[] _data;

		// Token: 0x0400017B RID: 379
		protected int _bufferPoint;

		// Token: 0x0400017C RID: 380
		protected uint _dataSize;

		// Token: 0x0400017D RID: 381
		protected bool _dataSizeChange;
	}
}
