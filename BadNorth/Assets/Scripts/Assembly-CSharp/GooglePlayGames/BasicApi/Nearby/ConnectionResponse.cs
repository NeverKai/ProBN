using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020003A7 RID: 935
	public struct ConnectionResponse
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x0002BB43 File Offset: 0x00029F43
		private ConnectionResponse(long localClientId, string remoteEndpointId, ConnectionResponse.Status code, byte[] payload)
		{
			this.mLocalClientId = localClientId;
			this.mRemoteEndpointId = Misc.CheckNotNull<string>(remoteEndpointId);
			this.mResponseStatus = code;
			this.mPayload = Misc.CheckNotNull<byte[]>(payload);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0002BB6C File Offset: 0x00029F6C
		public long LocalClientId
		{
			get
			{
				return this.mLocalClientId;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0002BB74 File Offset: 0x00029F74
		public string RemoteEndpointId
		{
			get
			{
				return this.mRemoteEndpointId;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0002BB7C File Offset: 0x00029F7C
		public ConnectionResponse.Status ResponseStatus
		{
			get
			{
				return this.mResponseStatus;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0002BB84 File Offset: 0x00029F84
		public byte[] Payload
		{
			get
			{
				return this.mPayload;
			}
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0002BB8C File Offset: 0x00029F8C
		public static ConnectionResponse Rejected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.Rejected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0002BB9B File Offset: 0x00029F9B
		public static ConnectionResponse NetworkNotConnected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorNetworkNotConnected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0002BBAA File Offset: 0x00029FAA
		public static ConnectionResponse InternalError(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorInternal, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0002BBB9 File Offset: 0x00029FB9
		public static ConnectionResponse EndpointNotConnected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorEndpointNotConnected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0002BBC8 File Offset: 0x00029FC8
		public static ConnectionResponse Accepted(long localClientId, string remoteEndpointId, byte[] payload)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.Accepted, payload);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0002BBD3 File Offset: 0x00029FD3
		public static ConnectionResponse AlreadyConnected(long localClientId, string remoteEndpointId)
		{
			return new ConnectionResponse(localClientId, remoteEndpointId, ConnectionResponse.Status.ErrorAlreadyConnected, ConnectionResponse.EmptyPayload);
		}

		// Token: 0x04000D45 RID: 3397
		private static readonly byte[] EmptyPayload = new byte[0];

		// Token: 0x04000D46 RID: 3398
		private readonly long mLocalClientId;

		// Token: 0x04000D47 RID: 3399
		private readonly string mRemoteEndpointId;

		// Token: 0x04000D48 RID: 3400
		private readonly ConnectionResponse.Status mResponseStatus;

		// Token: 0x04000D49 RID: 3401
		private readonly byte[] mPayload;

		// Token: 0x020003A8 RID: 936
		public enum Status
		{
			// Token: 0x04000D4B RID: 3403
			Accepted,
			// Token: 0x04000D4C RID: 3404
			Rejected,
			// Token: 0x04000D4D RID: 3405
			ErrorInternal,
			// Token: 0x04000D4E RID: 3406
			ErrorNetworkNotConnected,
			// Token: 0x04000D4F RID: 3407
			ErrorEndpointNotConnected,
			// Token: 0x04000D50 RID: 3408
			ErrorAlreadyConnected
		}
	}
}
