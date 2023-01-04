using System;

namespace ReflexCLI.Libraries.FabricIntegration
{
	// Token: 0x02000460 RID: 1120
	public struct FabricEvent
	{
		// Token: 0x06001981 RID: 6529 RVA: 0x0004351B File Offset: 0x0004191B
		public FabricEvent(string eventName)
		{
			this.eventName = eventName;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00043524 File Offset: 0x00041924
		public override string ToString()
		{
			return this.eventName;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0004352C File Offset: 0x0004192C
		public static implicit operator FabricEvent(string inStr)
		{
			return new FabricEvent(inStr);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00043534 File Offset: 0x00041934
		public static implicit operator string(FabricEvent FabricEvent)
		{
			return FabricEvent.eventName;
		}

		// Token: 0x04000FCB RID: 4043
		public string eventName;
	}
}
