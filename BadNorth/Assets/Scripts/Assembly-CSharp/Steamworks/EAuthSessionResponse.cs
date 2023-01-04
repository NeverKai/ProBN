using System;

namespace Steamworks
{
	// Token: 0x02000307 RID: 775
	public enum EAuthSessionResponse
	{
		// Token: 0x04000AFB RID: 2811
		k_EAuthSessionResponseOK,
		// Token: 0x04000AFC RID: 2812
		k_EAuthSessionResponseUserNotConnectedToSteam,
		// Token: 0x04000AFD RID: 2813
		k_EAuthSessionResponseNoLicenseOrExpired,
		// Token: 0x04000AFE RID: 2814
		k_EAuthSessionResponseVACBanned,
		// Token: 0x04000AFF RID: 2815
		k_EAuthSessionResponseLoggedInElseWhere,
		// Token: 0x04000B00 RID: 2816
		k_EAuthSessionResponseVACCheckTimedOut,
		// Token: 0x04000B01 RID: 2817
		k_EAuthSessionResponseAuthTicketCanceled,
		// Token: 0x04000B02 RID: 2818
		k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed,
		// Token: 0x04000B03 RID: 2819
		k_EAuthSessionResponseAuthTicketInvalid,
		// Token: 0x04000B04 RID: 2820
		k_EAuthSessionResponsePublisherIssuedBan
	}
}
