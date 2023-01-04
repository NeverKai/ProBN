using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020003A9 RID: 937
	public struct EndpointDetails
	{
		// Token: 0x06001525 RID: 5413 RVA: 0x0002BBEF File Offset: 0x00029FEF
		public EndpointDetails(string endpointId, string name, string serviceId)
		{
			this.mEndpointId = Misc.CheckNotNull<string>(endpointId);
			this.mName = Misc.CheckNotNull<string>(name);
			this.mServiceId = Misc.CheckNotNull<string>(serviceId);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0002BC15 File Offset: 0x0002A015
		public string EndpointId
		{
			get
			{
				return this.mEndpointId;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0002BC1D File Offset: 0x0002A01D
		public string Name
		{
			get
			{
				return this.mName;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0002BC25 File Offset: 0x0002A025
		public string ServiceId
		{
			get
			{
				return this.mServiceId;
			}
		}

		// Token: 0x04000D51 RID: 3409
		private readonly string mEndpointId;

		// Token: 0x04000D52 RID: 3410
		private readonly string mName;

		// Token: 0x04000D53 RID: 3411
		private readonly string mServiceId;
	}
}
