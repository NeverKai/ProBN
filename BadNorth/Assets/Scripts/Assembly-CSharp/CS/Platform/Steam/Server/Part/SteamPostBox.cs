using System;
using CS.Platform.Base.Client.Part;
using CS.Platform.Utils.Data;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Server.Part
{
	// Token: 0x020000AB RID: 171
	public class SteamPostBox<ClientConnectionInfo> : BasePostBox<CSteamID>
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0001B9F0 File Offset: 0x00019DF0
		public SteamPostBox(SteamServer<ClientConnectionInfo> manager)
		{
			this._Master = manager;
			this._callbackOnP2PSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
			this._callbackOnP2PSessionConnectFail = Callback<P2PSessionConnectFail_t>.Create(new Callback<P2PSessionConnectFail_t>.DispatchDelegate(this.OnP2PSessionConnectFail));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001BA2D File Offset: 0x00019E2D
		protected void OnP2PSessionRequest(P2PSessionRequest_t message)
		{
			SteamGameServerNetworking.AcceptP2PSessionWithUser(message.m_steamIDRemote);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001BA3C File Offset: 0x00019E3C
		protected void OnP2PSessionConnectFail(P2PSessionConnectFail_t message)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"[Steamworks-Server] Failed to message user: '",
				message.m_steamIDRemote.ToString(),
				"' because of error '",
				message.m_eP2PSessionError.ToString(),
				"'."
			}));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001BA9C File Offset: 0x00019E9C
		public override void SendNewMessage(CSteamID userID, byte[] message, int size, bool reliable)
		{
			checked
			{
				if (size < 1000000)
				{
					if (reliable)
					{
						SteamGameServerNetworking.SendP2PPacket(userID, message, (uint)size, EP2PSend.k_EP2PSendReliable, SteamElements.GetChannel(userID));
					}
					else
					{
						SteamGameServerNetworking.SendP2PPacket(userID, message, (uint)size, EP2PSend.k_EP2PSendUnreliableNoDelay, SteamElements.GetChannel(userID));
					}
				}
				else
				{
					Debug.LogFormat("[Steamworks-Server] Failed to send message as size max is '1MB' and message was '" + size.ToString() + "'.", new object[0]);
				}
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001BB10 File Offset: 0x00019F10
		protected override bool GetMessage(ref CSteamID senderID, out DataReader message)
		{
			uint num = 0U;
			uint num2 = 0U;
			message = null;
			if (SteamGameServerNetworking.IsP2PPacketAvailable(out num, SteamElements.ServerChannel))
			{
				byte[] array = new byte[num];
				SteamGameServerNetworking.ReadP2PPacket(array, num, out num2, out senderID, SteamElements.ServerChannel);
				message = new DataReader(array, false);
				return true;
			}
			return false;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001BB59 File Offset: 0x00019F59
		protected override void ParseMessage(CSteamID senderID, DataReader message)
		{
			PlatformMessageBase.SetToAfterFallbackID(message);
			base.ParseMessage(senderID, message);
		}

		// Token: 0x04000308 RID: 776
		private SteamServer<ClientConnectionInfo> _Master;

		// Token: 0x04000309 RID: 777
		protected Callback<P2PSessionRequest_t> _callbackOnP2PSessionRequest;

		// Token: 0x0400030A RID: 778
		protected Callback<P2PSessionConnectFail_t> _callbackOnP2PSessionConnectFail;
	}
}
