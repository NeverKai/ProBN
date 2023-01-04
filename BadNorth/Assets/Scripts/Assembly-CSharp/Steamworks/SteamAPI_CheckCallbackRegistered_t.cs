using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200035E RID: 862
	// (Invoke) Token: 0x060012F7 RID: 4855
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void SteamAPI_CheckCallbackRegistered_t(int iCallbackNum);
}
