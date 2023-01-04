using System;
using UnityEngine;

namespace RTM.OnScreenDebug.Internals
{
	// Token: 0x020004CB RID: 1227
	public class OnScreenDebugMessageManager : MonoBehaviour
	{
		// Token: 0x06001EE4 RID: 7908 RVA: 0x0005326C File Offset: 0x0005166C
		public void AddMessage(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string text)
		{
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0005326E File Offset: 0x0005166E
		public void AddMessage<T0>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0)
		{
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00053270 File Offset: 0x00051670
		public void AddMessage<T0, T1>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1)
		{
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00053272 File Offset: 0x00051672
		public void AddMessage<T0, T1, T2>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2)
		{
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00053274 File Offset: 0x00051674
		public void AddMessage<T0, T1, T2, T3>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00053276 File Offset: 0x00051676
		public void AddMessage<T0, T1, T2, T3, T4>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x00053278 File Offset: 0x00051678
		public void AddMessage<T0, T1, T2, T3, T4, T5>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0005327A File Offset: 0x0005167A
		public void AddException(Exception e, string prefix, float duration)
		{
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0005327C File Offset: 0x0005167C
		public void AddException(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Exception e, string prefix)
		{
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0005327E File Offset: 0x0005167E
		public void Clear(DebugChannelGroup group)
		{
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00053280 File Offset: 0x00051680
		public void Clear(DebugChannelGroup group, int channel)
		{
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00053282 File Offset: 0x00051682
		public void Clear(DebugChannelGroup group, int channelMin, int channelMax)
		{
		}

		// Token: 0x04001339 RID: 4921
		[SerializeField]
		[HideInInspector]
		private OnScreenDebugMessageDisplay Display;

		// Token: 0x0400133A RID: 4922
		[SerializeField]
		[HideInInspector]
		private OnScreenDebugMessageBackground Background;
	}
}
