using System;

namespace Voxels
{
	// Token: 0x02000615 RID: 1557
	public struct GenInfo
	{
		// Token: 0x06002801 RID: 10241 RVA: 0x00082CBE File Offset: 0x000810BE
		public GenInfo(string text, GenInfo.Mode mode = GenInfo.Mode.interruptable)
		{
			this.text = text;
			this.mode = mode;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x00082CCE File Offset: 0x000810CE
		public bool interruptable
		{
			get
			{
				return this.mode == GenInfo.Mode.interruptable;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x00082CD9 File Offset: 0x000810D9
		public bool nonInterupptable
		{
			get
			{
				return this.mode == GenInfo.Mode.nonInterupptable;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x00082CE4 File Offset: 0x000810E4
		public bool forceInterrupt
		{
			get
			{
				return this.mode == GenInfo.Mode.forceInterrupt;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x00082CEF File Offset: 0x000810EF
		public bool broken
		{
			get
			{
				return this.mode == GenInfo.Mode.broken;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x00082CFA File Offset: 0x000810FA
		public string creatingMethod
		{
			get
			{
				return "[GenInfo GENINFO_DIAGNOSTICs Disabled]";
			}
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x00082D01 File Offset: 0x00081101
		public override string ToString()
		{
			return string.Format("GenInfo - {0} ({1})", this.text, this.mode);
		}

		// Token: 0x040019AE RID: 6574
		public string text;

		// Token: 0x040019AF RID: 6575
		public GenInfo.Mode mode;

		// Token: 0x02000616 RID: 1558
		public enum Mode
		{
			// Token: 0x040019B1 RID: 6577
			interruptable,
			// Token: 0x040019B2 RID: 6578
			nonInterupptable,
			// Token: 0x040019B3 RID: 6579
			forceInterrupt,
			// Token: 0x040019B4 RID: 6580
			broken
		}
	}
}
