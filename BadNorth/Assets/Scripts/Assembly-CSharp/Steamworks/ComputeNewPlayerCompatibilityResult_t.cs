using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200023F RID: 575
	[CallbackIdentity(211)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ComputeNewPlayerCompatibilityResult_t
	{
		// Token: 0x04000567 RID: 1383
		public const int k_iCallback = 211;

		// Token: 0x04000568 RID: 1384
		public EResult m_eResult;

		// Token: 0x04000569 RID: 1385
		public int m_cPlayersThatDontLikeCandidate;

		// Token: 0x0400056A RID: 1386
		public int m_cPlayersThatCandidateDoesntLike;

		// Token: 0x0400056B RID: 1387
		public int m_cClanPlayersThatDontLikeCandidate;

		// Token: 0x0400056C RID: 1388
		public CSteamID m_SteamIDCandidate;
	}
}
