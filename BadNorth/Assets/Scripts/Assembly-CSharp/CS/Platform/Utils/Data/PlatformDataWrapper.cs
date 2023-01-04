using System;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000052 RID: 82
	public class PlatformDataWrapper
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00008967 File Offset: 0x00006D67
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00008981 File Offset: 0x00006D81
		protected byte Flag
		{
			get
			{
				if (this._dataStream == null)
				{
					return 0;
				}
				return this._dataStream.Flag;
			}
			set
			{
				if (this._dataStream == null)
				{
					this._dataStream = new DataWriter(10);
				}
				this._dataStream.SetFlag(value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003DE RID: 990 RVA: 0x000089A7 File Offset: 0x00006DA7
		public byte[] RawMessage
		{
			get
			{
				this.RefreshMessage();
				return this._dataStream.DataBuffer;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003DF RID: 991 RVA: 0x000089BA File Offset: 0x00006DBA
		public DataWriter Message
		{
			get
			{
				this.RefreshMessage();
				return this._dataStream;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x000089C8 File Offset: 0x00006DC8
		public int RawMessageSize
		{
			get
			{
				this.RefreshMessage();
				return this._dataStream.DataSize;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000089DB File Offset: 0x00006DDB
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x000089E3 File Offset: 0x00006DE3
		public bool Dirty
		{
			get
			{
				return this._dirty;
			}
			protected set
			{
				this._dirty = value;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000089EC File Offset: 0x00006DEC
		protected void RefreshMessage()
		{
			if (this._dirty)
			{
				if (this._dataStream == null)
				{
					this._dataStream = new DataWriter(0);
				}
				this._dataStream.ClearStream();
				this.Serialize(this._dataStream);
				this._dirty = false;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00008A3C File Offset: 0x00006E3C
		public byte[] CopyMessage()
		{
			this.RefreshMessage();
			byte[] array = new byte[this._dataStream.DataSize];
			Buffer.BlockCopy(this._dataStream.DataBuffer, 0, array, 0, this._dataStream.DataSize);
			return array;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00008A7F File Offset: 0x00006E7F
		public void ApplyData(DataReader read)
		{
			this.Deserialize(read);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00008A88 File Offset: 0x00006E88
		protected virtual void Serialize(DataWriter writer)
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00008A8A File Offset: 0x00006E8A
		protected virtual void Deserialize(DataReader reader)
		{
		}

		// Token: 0x0400017E RID: 382
		private DataWriter _dataStream;

		// Token: 0x0400017F RID: 383
		private bool _dirty = true;
	}
}
