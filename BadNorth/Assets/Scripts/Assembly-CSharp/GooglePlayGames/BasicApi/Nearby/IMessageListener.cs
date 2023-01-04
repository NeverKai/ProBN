using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020003AA RID: 938
	public interface IMessageListener
	{
		// Token: 0x06001529 RID: 5417
		void OnMessageReceived(string remoteEndpointId, byte[] data, bool isReliableMessage);

		// Token: 0x0600152A RID: 5418
		void OnRemoteEndpointDisconnected(string remoteEndpointId);
	}
}
