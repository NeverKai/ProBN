using System;
using Steamworks;

namespace CS.Platform.Steam.Server.Part
{
	// Token: 0x020000A6 RID: 166
	public class SteamAuthenticator<ClientConnectionInfo>
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0001A5B2 File Offset: 0x000189B2
		public SteamAuthenticator(SteamServer<ClientConnectionInfo> manager)
		{
			this._Master = manager;
			this._callbackValidateAuthTicketResponse = Callback<ValidateAuthTicketResponse_t>.Create(new Callback<ValidateAuthTicketResponse_t>.DispatchDelegate(this.OnAuthSessionTicketResponse));
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001A5D8 File Offset: 0x000189D8
		public void TakeTicket(BaseUserInfo userInfo, byte[] ticket, int ticketSize)
		{
			if (this._Master.Connections.IsConnectedUser(userInfo))
			{
				EBeginAuthSessionResult why = SteamUser.BeginAuthSession(ticket, ticketSize, (CSteamID)userInfo.userID);
				this._Master.Connections.SetAuthenticatedResult(userInfo.userID, why);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001A627 File Offset: 0x00018A27
		private void OnAuthSessionTicketResponse(ValidateAuthTicketResponse_t response)
		{
			this._Master.Connections.SetAuthenticatedResult((ulong)response.m_SteamID, response.m_eAuthSessionResponse);
		}

		// Token: 0x040002E6 RID: 742
		private SteamServer<ClientConnectionInfo> _Master;

		// Token: 0x040002E7 RID: 743
		protected Callback<ValidateAuthTicketResponse_t> _callbackValidateAuthTicketResponse;
	}
}
