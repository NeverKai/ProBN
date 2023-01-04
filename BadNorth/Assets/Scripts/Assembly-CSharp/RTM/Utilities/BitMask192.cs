using System;

namespace RTM.Utilities
{
	// Token: 0x020004E2 RID: 1250
	public struct BitMask192
	{
		// Token: 0x06002016 RID: 8214 RVA: 0x00056868 File Offset: 0x00054C68
		public bool Get(int idx)
		{
			int num;
			int num2;
			ulong num3;
			this.GetIndeces(idx, out num, out num2, out num3);
			switch (num)
			{
			case 0:
				return (this.a & num3) != 0UL;
			case 1:
				return (this.b & num3) != 0UL;
			case 2:
				return (this.c & num3) != 0UL;
			default:
				throw new IndexOutOfRangeException(string.Format("index out of range ({0} / {1}) - using {2} registers", idx, num, 3));
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000568EC File Offset: 0x00054CEC
		public void Set(int idx, bool value)
		{
			int num;
			int num2;
			ulong num3;
			this.GetIndeces(idx, out num, out num2, out num3);
			switch (num)
			{
			case 0:
				this.a = ((!value) ? (this.a & ~num3) : (this.a | num3));
				break;
			case 1:
				this.b = ((!value) ? (this.b & ~num3) : (this.b | num3));
				break;
			case 2:
				this.c = ((!value) ? (this.c & ~num3) : (this.c | num3));
				break;
			default:
				throw new IndexOutOfRangeException(string.Format("index out of range ({0} / {1}) - using {2} registers", idx, num, 3));
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x000569B4 File Offset: 0x00054DB4
		public bool anySet
		{
			get
			{
				return (this.a | this.b | this.c) != 0UL;
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000569D1 File Offset: 0x00054DD1
		private void GetIndeces(int idx, out int registerIdx, out int subIdx, out ulong subMask)
		{
			registerIdx = idx / 64;
			subIdx = idx - registerIdx * 64;
			subMask = 1UL << subIdx;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000569ED File Offset: 0x00054DED
		public override string ToString()
		{
			return this.ToString(192);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000569FC File Offset: 0x00054DFC
		public string ToString(int bitsToShow)
		{
			bitsToShow = Math.Min(bitsToShow, 192);
			char[] array = new char[bitsToShow];
			for (int i = 0; i < bitsToShow; i++)
			{
				array[bitsToShow - 1 - i] = ((!this.Get(i)) ? '0' : '1');
			}
			return new string(array);
		}

		// Token: 0x040013EC RID: 5100
		private const int numRegisters = 3;

		// Token: 0x040013ED RID: 5101
		private const int registerSize = 64;

		// Token: 0x040013EE RID: 5102
		private const int numBits = 192;

		// Token: 0x040013EF RID: 5103
		private ulong a;

		// Token: 0x040013F0 RID: 5104
		private ulong b;

		// Token: 0x040013F1 RID: 5105
		private ulong c;
	}
}
