using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000324 RID: 804
	public struct MatchMakingKeyValuePair_t
	{
		// Token: 0x060011EE RID: 4590 RVA: 0x000268DA File Offset: 0x00024CDA
		private MatchMakingKeyValuePair_t(string strKey, string strValue)
		{
			this.m_szKey = strKey;
			this.m_szValue = strValue;
		}

		// Token: 0x04000C27 RID: 3111
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szKey;

		// Token: 0x04000C28 RID: 3112
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szValue;
	}
}
