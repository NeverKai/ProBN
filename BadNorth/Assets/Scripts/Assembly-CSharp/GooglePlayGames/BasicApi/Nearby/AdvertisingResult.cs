using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020003A5 RID: 933
	public struct AdvertisingResult
	{
		// Token: 0x06001512 RID: 5394 RVA: 0x0002BADC File Offset: 0x00029EDC
		public AdvertisingResult(ResponseStatus status, string localEndpointName)
		{
			this.mStatus = status;
			this.mLocalEndpointName = Misc.CheckNotNull<string>(localEndpointName);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0002BAF1 File Offset: 0x00029EF1
		public bool Succeeded
		{
			get
			{
				return this.mStatus == ResponseStatus.Success;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0002BAFC File Offset: 0x00029EFC
		public ResponseStatus Status
		{
			get
			{
				return this.mStatus;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0002BB04 File Offset: 0x00029F04
		public string LocalEndpointName
		{
			get
			{
				return this.mLocalEndpointName;
			}
		}

		// Token: 0x04000D41 RID: 3393
		private readonly ResponseStatus mStatus;

		// Token: 0x04000D42 RID: 3394
		private readonly string mLocalEndpointName;
	}
}
