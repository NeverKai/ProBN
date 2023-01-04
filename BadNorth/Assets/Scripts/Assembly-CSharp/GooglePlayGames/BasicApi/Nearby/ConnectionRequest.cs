using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020003A6 RID: 934
	public struct ConnectionRequest
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x0002BB0C File Offset: 0x00029F0C
		public ConnectionRequest(string remoteEndpointId, string remoteEndpointName, string serviceId, byte[] payload)
		{
			Logger.d("Constructing ConnectionRequest");
			this.mRemoteEndpoint = new EndpointDetails(remoteEndpointId, remoteEndpointName, serviceId);
			this.mPayload = Misc.CheckNotNull<byte[]>(payload);
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0002BB33 File Offset: 0x00029F33
		public EndpointDetails RemoteEndpoint
		{
			get
			{
				return this.mRemoteEndpoint;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0002BB3B File Offset: 0x00029F3B
		public byte[] Payload
		{
			get
			{
				return this.mPayload;
			}
		}

		// Token: 0x04000D43 RID: 3395
		private readonly EndpointDetails mRemoteEndpoint;

		// Token: 0x04000D44 RID: 3396
		private readonly byte[] mPayload;
	}
}
