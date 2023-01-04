using System;
using System.Diagnostics;
using RTM.OnScreenDebug.Internals;
using UnityEngine;

namespace RTM.OnScreenDebug
{
	// Token: 0x020004CC RID: 1228
	public static class OnScreenDebugMessage
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00053284 File Offset: 0x00051684
		private static OnScreenDebugMessageManager Manager
		{
			get
			{
				return OnScreenDebugMessage._Manager;
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0005328B File Offset: 0x0005168B
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string text)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddMessage(group, channel, duration, verbosity, color, text);
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000532AE File Offset: 0x000516AE
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddMessage<T0>(group, channel, duration, verbosity, color, format, arg0);
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000532D4 File Offset: 0x000516D4
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddMessage<T0, T1>(group, channel, duration, verbosity, color, format, arg0, arg1);
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00053308 File Offset: 0x00051708
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddMessage<T0, T1, T2>(group, channel, duration, verbosity, color, format, arg0, arg1, arg2);
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0005333C File Offset: 0x0005173C
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2, T3>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddMessage<T0, T1, T2, T3>(group, channel, duration, verbosity, color, format, arg0, arg1, arg2, arg3);
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00053374 File Offset: 0x00051774
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2, T3, T4>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddMessage<T0, T1, T2, T3, T4>(group, channel, duration, verbosity, color, format, arg0, arg1, arg2, arg3, arg4);
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000533AC File Offset: 0x000517AC
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2, T3, T4, T5>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Color color, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddMessage<T0, T1, T2, T3, T4, T5>(group, channel, duration, verbosity, color, format, arg0, arg1, arg2, arg3, arg4, arg5);
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000533E6 File Offset: 0x000517E6
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, string text)
		{
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000533E8 File Offset: 0x000517E8
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, string format, T0 arg0)
		{
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000533EA File Offset: 0x000517EA
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, string format, T0 arg0, T1 arg1)
		{
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000533EC File Offset: 0x000517EC
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, string format, T0 arg0, T1 arg1, T2 arg2)
		{
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000533EE File Offset: 0x000517EE
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2, T3>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000533F0 File Offset: 0x000517F0
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2, T3, T4>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000533F2 File Offset: 0x000517F2
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Add<T0, T1, T2, T3, T4, T5>(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000533F4 File Offset: 0x000517F4
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void AddException(Exception e, string prefix = null, float duration = 30f)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddException(e, prefix, duration);
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x00053412 File Offset: 0x00051812
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void AddException(DebugChannelGroup group, int channel, float duration, EVerbosity verbosity, Exception e, string prefix = null)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.AddException(group, channel, duration, verbosity, e, prefix);
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00053435 File Offset: 0x00051835
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Clear(DebugChannelGroup group)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.Clear(group);
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00053451 File Offset: 0x00051851
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Clear(DebugChannelGroup group, int channel)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.Clear(group, channel);
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0005346E File Offset: 0x0005186E
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		public static void Clear(DebugChannelGroup group, int channelMin, int channelMax)
		{
			if (OnScreenDebugMessage.Manager)
			{
				OnScreenDebugMessage.Manager.Clear(group, channelMin, channelMax);
			}
		}

		// Token: 0x0400133B RID: 4923
		private static Color DefaultColor = Color.white;

		// Token: 0x0400133C RID: 4924
		private static OnScreenDebugMessageManager _Manager = null;
	}
}
