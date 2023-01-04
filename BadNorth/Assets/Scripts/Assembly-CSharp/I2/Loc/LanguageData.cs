using System;

namespace I2.Loc
{
	// Token: 0x020003E2 RID: 994
	[Serializable]
	public class LanguageData
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x0003525C File Offset: 0x0003365C
		public bool IsEnabled()
		{
			return (this.Flags & 1) == 0;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00035269 File Offset: 0x00033669
		public void SetEnabled(bool bEnabled)
		{
			if (bEnabled)
			{
				this.Flags = (byte)((int)this.Flags & -2);
			}
			else
			{
				this.Flags |= 1;
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00035295 File Offset: 0x00033695
		public bool IsLoaded()
		{
			return (this.Flags & 4) == 0;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000352A2 File Offset: 0x000336A2
		public bool CanBeUnloaded()
		{
			return (this.Flags & 2) == 0;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000352AF File Offset: 0x000336AF
		public void SetLoaded(bool loaded)
		{
			if (loaded)
			{
				this.Flags = (byte)((int)this.Flags & -5);
			}
			else
			{
				this.Flags |= 4;
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x000352DB File Offset: 0x000336DB
		public void SetCanBeUnLoaded(bool allowUnloading)
		{
			if (allowUnloading)
			{
				this.Flags = (byte)((int)this.Flags & -3);
			}
			else
			{
				this.Flags |= 2;
			}
		}

		// Token: 0x04000DF8 RID: 3576
		public string Name;

		// Token: 0x04000DF9 RID: 3577
		public string Code;

		// Token: 0x04000DFA RID: 3578
		public byte Flags;

		// Token: 0x04000DFB RID: 3579
		[NonSerialized]
		public bool Compressed;
	}
}
