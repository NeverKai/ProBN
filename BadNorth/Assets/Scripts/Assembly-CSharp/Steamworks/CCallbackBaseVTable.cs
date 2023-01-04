using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200032B RID: 811
	[StructLayout(LayoutKind.Sequential)]
	internal class CCallbackBaseVTable
	{
		// Token: 0x04000C3E RID: 3134
		private const CallingConvention cc = CallingConvention.StdCall;

		// Token: 0x04000C3F RID: 3135
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public CCallbackBaseVTable.RunCRDel m_RunCallResult;

		// Token: 0x04000C40 RID: 3136
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public CCallbackBaseVTable.RunCBDel m_RunCallback;

		// Token: 0x04000C41 RID: 3137
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public CCallbackBaseVTable.GetCallbackSizeBytesDel m_GetCallbackSizeBytes;

		// Token: 0x0200032C RID: 812
		// (Invoke) Token: 0x06001218 RID: 4632
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void RunCBDel(IntPtr pvParam);

		// Token: 0x0200032D RID: 813
		// (Invoke) Token: 0x0600121C RID: 4636
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void RunCRDel(IntPtr pvParam, [MarshalAs(UnmanagedType.I1)] bool bIOFailure, ulong hSteamAPICall);

		// Token: 0x0200032E RID: 814
		// (Invoke) Token: 0x06001220 RID: 4640
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int GetCallbackSizeBytesDel();
	}
}
