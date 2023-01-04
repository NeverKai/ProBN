using System;

namespace Steamworks
{
	// Token: 0x02000303 RID: 771
	public enum EResult
	{
		// Token: 0x04000A6C RID: 2668
		k_EResultOK = 1,
		// Token: 0x04000A6D RID: 2669
		k_EResultFail,
		// Token: 0x04000A6E RID: 2670
		k_EResultNoConnection,
		// Token: 0x04000A6F RID: 2671
		k_EResultInvalidPassword = 5,
		// Token: 0x04000A70 RID: 2672
		k_EResultLoggedInElsewhere,
		// Token: 0x04000A71 RID: 2673
		k_EResultInvalidProtocolVer,
		// Token: 0x04000A72 RID: 2674
		k_EResultInvalidParam,
		// Token: 0x04000A73 RID: 2675
		k_EResultFileNotFound,
		// Token: 0x04000A74 RID: 2676
		k_EResultBusy,
		// Token: 0x04000A75 RID: 2677
		k_EResultInvalidState,
		// Token: 0x04000A76 RID: 2678
		k_EResultInvalidName,
		// Token: 0x04000A77 RID: 2679
		k_EResultInvalidEmail,
		// Token: 0x04000A78 RID: 2680
		k_EResultDuplicateName,
		// Token: 0x04000A79 RID: 2681
		k_EResultAccessDenied,
		// Token: 0x04000A7A RID: 2682
		k_EResultTimeout,
		// Token: 0x04000A7B RID: 2683
		k_EResultBanned,
		// Token: 0x04000A7C RID: 2684
		k_EResultAccountNotFound,
		// Token: 0x04000A7D RID: 2685
		k_EResultInvalidSteamID,
		// Token: 0x04000A7E RID: 2686
		k_EResultServiceUnavailable,
		// Token: 0x04000A7F RID: 2687
		k_EResultNotLoggedOn,
		// Token: 0x04000A80 RID: 2688
		k_EResultPending,
		// Token: 0x04000A81 RID: 2689
		k_EResultEncryptionFailure,
		// Token: 0x04000A82 RID: 2690
		k_EResultInsufficientPrivilege,
		// Token: 0x04000A83 RID: 2691
		k_EResultLimitExceeded,
		// Token: 0x04000A84 RID: 2692
		k_EResultRevoked,
		// Token: 0x04000A85 RID: 2693
		k_EResultExpired,
		// Token: 0x04000A86 RID: 2694
		k_EResultAlreadyRedeemed,
		// Token: 0x04000A87 RID: 2695
		k_EResultDuplicateRequest,
		// Token: 0x04000A88 RID: 2696
		k_EResultAlreadyOwned,
		// Token: 0x04000A89 RID: 2697
		k_EResultIPNotFound,
		// Token: 0x04000A8A RID: 2698
		k_EResultPersistFailed,
		// Token: 0x04000A8B RID: 2699
		k_EResultLockingFailed,
		// Token: 0x04000A8C RID: 2700
		k_EResultLogonSessionReplaced,
		// Token: 0x04000A8D RID: 2701
		k_EResultConnectFailed,
		// Token: 0x04000A8E RID: 2702
		k_EResultHandshakeFailed,
		// Token: 0x04000A8F RID: 2703
		k_EResultIOFailure,
		// Token: 0x04000A90 RID: 2704
		k_EResultRemoteDisconnect,
		// Token: 0x04000A91 RID: 2705
		k_EResultShoppingCartNotFound,
		// Token: 0x04000A92 RID: 2706
		k_EResultBlocked,
		// Token: 0x04000A93 RID: 2707
		k_EResultIgnored,
		// Token: 0x04000A94 RID: 2708
		k_EResultNoMatch,
		// Token: 0x04000A95 RID: 2709
		k_EResultAccountDisabled,
		// Token: 0x04000A96 RID: 2710
		k_EResultServiceReadOnly,
		// Token: 0x04000A97 RID: 2711
		k_EResultAccountNotFeatured,
		// Token: 0x04000A98 RID: 2712
		k_EResultAdministratorOK,
		// Token: 0x04000A99 RID: 2713
		k_EResultContentVersion,
		// Token: 0x04000A9A RID: 2714
		k_EResultTryAnotherCM,
		// Token: 0x04000A9B RID: 2715
		k_EResultPasswordRequiredToKickSession,
		// Token: 0x04000A9C RID: 2716
		k_EResultAlreadyLoggedInElsewhere,
		// Token: 0x04000A9D RID: 2717
		k_EResultSuspended,
		// Token: 0x04000A9E RID: 2718
		k_EResultCancelled,
		// Token: 0x04000A9F RID: 2719
		k_EResultDataCorruption,
		// Token: 0x04000AA0 RID: 2720
		k_EResultDiskFull,
		// Token: 0x04000AA1 RID: 2721
		k_EResultRemoteCallFailed,
		// Token: 0x04000AA2 RID: 2722
		k_EResultPasswordUnset,
		// Token: 0x04000AA3 RID: 2723
		k_EResultExternalAccountUnlinked,
		// Token: 0x04000AA4 RID: 2724
		k_EResultPSNTicketInvalid,
		// Token: 0x04000AA5 RID: 2725
		k_EResultExternalAccountAlreadyLinked,
		// Token: 0x04000AA6 RID: 2726
		k_EResultRemoteFileConflict,
		// Token: 0x04000AA7 RID: 2727
		k_EResultIllegalPassword,
		// Token: 0x04000AA8 RID: 2728
		k_EResultSameAsPreviousValue,
		// Token: 0x04000AA9 RID: 2729
		k_EResultAccountLogonDenied,
		// Token: 0x04000AAA RID: 2730
		k_EResultCannotUseOldPassword,
		// Token: 0x04000AAB RID: 2731
		k_EResultInvalidLoginAuthCode,
		// Token: 0x04000AAC RID: 2732
		k_EResultAccountLogonDeniedNoMail,
		// Token: 0x04000AAD RID: 2733
		k_EResultHardwareNotCapableOfIPT,
		// Token: 0x04000AAE RID: 2734
		k_EResultIPTInitError,
		// Token: 0x04000AAF RID: 2735
		k_EResultParentalControlRestricted,
		// Token: 0x04000AB0 RID: 2736
		k_EResultFacebookQueryError,
		// Token: 0x04000AB1 RID: 2737
		k_EResultExpiredLoginAuthCode,
		// Token: 0x04000AB2 RID: 2738
		k_EResultIPLoginRestrictionFailed,
		// Token: 0x04000AB3 RID: 2739
		k_EResultAccountLockedDown,
		// Token: 0x04000AB4 RID: 2740
		k_EResultAccountLogonDeniedVerifiedEmailRequired,
		// Token: 0x04000AB5 RID: 2741
		k_EResultNoMatchingURL,
		// Token: 0x04000AB6 RID: 2742
		k_EResultBadResponse,
		// Token: 0x04000AB7 RID: 2743
		k_EResultRequirePasswordReEntry,
		// Token: 0x04000AB8 RID: 2744
		k_EResultValueOutOfRange,
		// Token: 0x04000AB9 RID: 2745
		k_EResultUnexpectedError,
		// Token: 0x04000ABA RID: 2746
		k_EResultDisabled,
		// Token: 0x04000ABB RID: 2747
		k_EResultInvalidCEGSubmission,
		// Token: 0x04000ABC RID: 2748
		k_EResultRestrictedDevice,
		// Token: 0x04000ABD RID: 2749
		k_EResultRegionLocked,
		// Token: 0x04000ABE RID: 2750
		k_EResultRateLimitExceeded,
		// Token: 0x04000ABF RID: 2751
		k_EResultAccountLoginDeniedNeedTwoFactor,
		// Token: 0x04000AC0 RID: 2752
		k_EResultItemDeleted,
		// Token: 0x04000AC1 RID: 2753
		k_EResultAccountLoginDeniedThrottle,
		// Token: 0x04000AC2 RID: 2754
		k_EResultTwoFactorCodeMismatch,
		// Token: 0x04000AC3 RID: 2755
		k_EResultTwoFactorActivationCodeMismatch,
		// Token: 0x04000AC4 RID: 2756
		k_EResultAccountAssociatedToMultiplePartners,
		// Token: 0x04000AC5 RID: 2757
		k_EResultNotModified,
		// Token: 0x04000AC6 RID: 2758
		k_EResultNoMobileDevice,
		// Token: 0x04000AC7 RID: 2759
		k_EResultTimeNotSynced,
		// Token: 0x04000AC8 RID: 2760
		k_EResultSmsCodeFailed,
		// Token: 0x04000AC9 RID: 2761
		k_EResultAccountLimitExceeded,
		// Token: 0x04000ACA RID: 2762
		k_EResultAccountActivityLimitExceeded,
		// Token: 0x04000ACB RID: 2763
		k_EResultPhoneActivityLimitExceeded,
		// Token: 0x04000ACC RID: 2764
		k_EResultRefundToWallet,
		// Token: 0x04000ACD RID: 2765
		k_EResultEmailSendFailure,
		// Token: 0x04000ACE RID: 2766
		k_EResultNotSettled,
		// Token: 0x04000ACF RID: 2767
		k_EResultNeedCaptcha,
		// Token: 0x04000AD0 RID: 2768
		k_EResultGSLTDenied,
		// Token: 0x04000AD1 RID: 2769
		k_EResultGSOwnerDenied,
		// Token: 0x04000AD2 RID: 2770
		k_EResultInvalidItemType,
		// Token: 0x04000AD3 RID: 2771
		k_EResultIPBanned,
		// Token: 0x04000AD4 RID: 2772
		k_EResultGSLTExpired,
		// Token: 0x04000AD5 RID: 2773
		k_EResultInsufficientFunds,
		// Token: 0x04000AD6 RID: 2774
		k_EResultTooManyPending
	}
}
