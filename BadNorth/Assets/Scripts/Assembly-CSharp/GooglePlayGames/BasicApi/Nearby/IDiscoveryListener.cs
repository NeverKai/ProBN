using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020003AB RID: 939
	public interface IDiscoveryListener
	{
		// Token: 0x0600152B RID: 5419
		void OnEndpointFound(EndpointDetails discoveredEndpoint);

		// Token: 0x0600152C RID: 5420
		void OnEndpointLost(string lostEndpointId);
	}
}
