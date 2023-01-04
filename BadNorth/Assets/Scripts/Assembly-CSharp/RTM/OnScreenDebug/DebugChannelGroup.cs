using System;
using UnityEngine;

namespace RTM.OnScreenDebug
{
	// Token: 0x020004C7 RID: 1223
	[Serializable]
	public class DebugChannelGroup
	{
		// Token: 0x06001EDC RID: 7900 RVA: 0x000531FC File Offset: 0x000515FC
		public DebugChannelGroup(string name, EVerbosity verbosity = EVerbosity.Normal, int autochannelBase = 0)
		{
			this.Name = name;
			this.Verbosity = verbosity;
			this.AutoChannel = autochannelBase;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0005321C File Offset: 0x0005161C
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0005323A File Offset: 0x0005163A
		public int AutoChannel
		{
			get
			{
				return this._AutoChannel++;
			}
			set
			{
				this._AutoChannel = value;
			}
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00053243 File Offset: 0x00051643
		public static implicit operator bool(DebugChannelGroup group)
		{
			return group != null;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0005324C File Offset: 0x0005164C
		public static implicit operator int(DebugChannelGroup group)
		{
			return group.AutoChannel;
		}

		// Token: 0x04001330 RID: 4912
		public readonly string Name;

		// Token: 0x04001331 RID: 4913
		[SerializeField]
		public EVerbosity Verbosity;

		// Token: 0x04001332 RID: 4914
		private int _AutoChannel;
	}
}
