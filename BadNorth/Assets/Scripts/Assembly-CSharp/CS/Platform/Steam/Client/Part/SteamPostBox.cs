using System;
using CS.Platform.Base.Client.Part;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;
using Steamworks;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000047 RID: 71
	public class SteamPostBox : BasePostBox<ulong>
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000F368 File Offset: 0x0000D768
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
			this._newMessage = new byte[100000];
			this._callbackOnP2PSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
			this._callbackOnP2PSessionConnectFail = Callback<P2PSessionConnectFail_t>.Create(new Callback<P2PSessionConnectFail_t>.DispatchDelegate(this.OnP2PSessionConnectFail));
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000F3BF File Offset: 0x0000D7BF
		protected void OnP2PSessionRequest(P2PSessionRequest_t message)
		{
			SteamNetworking.AcceptP2PSessionWithUser(message.m_steamIDRemote);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000F3CE File Offset: 0x0000D7CE
		protected void OnP2PSessionConnectFail(P2PSessionConnectFail_t message)
		{
			Debug.LogError("[Steamworks] P2P Session Failed | User: {0} | Error: {1}", new object[]
			{
				message.m_steamIDRemote.ToString(),
				message.m_eP2PSessionError.ToString()
			});
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000F40C File Offset: 0x0000D80C
		public override void SendNewMessage(ulong userID, byte[] message, int size, bool reliable)
		{
			if (reliable)
			{
				SteamNetworking.SendP2PPacket((CSteamID)userID, message, (uint)size, EP2PSend.k_EP2PSendReliableWithBuffering, SteamElements.GetChannel((CSteamID)userID));
			}
			else
			{
				SteamNetworking.SendP2PPacket((CSteamID)userID, message, (uint)size, EP2PSend.k_EP2PSendUnreliableNoDelay, SteamElements.GetChannel((CSteamID)userID));
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F45C File Offset: 0x0000D85C
		protected override bool GetMessage(ref ulong senderID, out DataReader message)
		{
			uint val = 0U;
			uint num = 0U;
			if (!SteamNetworking.IsP2PPacketAvailable(out num, SteamElements.ClientChannel))
			{
				message = null;
				senderID = 0UL;
				return false;
			}
			byte[] array = new byte[num];
			CSteamID that;
			if (SteamNetworking.ReadP2PPacket(array, num, out val, out that, SteamElements.ClientChannel))
			{
				num = Math.Min(val, num);
				senderID = (ulong)that;
				message = new DataReader(array, false);
				return true;
			}
			message = null;
			senderID = 0UL;
			num = 0U;
			return false;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F4CC File Offset: 0x0000D8CC
		protected override void ParseMessage(ulong senderID, DataReader message)
		{
			PlatformMessageBase.SetToAfterFallbackID(message);
			switch (message.Flag)
			{
			case 2:
				this._Manager.Lobby.UseBeenKickedMessage((CSteamID)senderID);
				return;
			case 5:
				this._Manager.AddToNextUpdate(delegate
				{
					var name = string.Empty;
					// SteamFriends.GetFriendPersonaName((CSteamID) senderID);
					PlatformEvents.ReceivedUserMessage(new BaseUserInfo(senderID, "steam", 
						name), message);
				});
				return;
			case 6:
				this._Manager.AddToNextUpdate(delegate
				{
					if (this._Manager.Lobby.InLobby && this._Manager.Lobby.GetHostID == senderID)
					{
						BaseUserInfo user = default(BaseUserInfo);
						if (this._Manager.Lobby.UserIsInLobby(senderID, ref user) && this._Manager.Lobby.UserIsLobbyHost(user))
						{
							PlatformEvents.ReceivedLobbyHostMessage(message);
						}
					}
				});
				return;
			case 7:
				this._Manager.AddToNextUpdate(delegate
				{
					if (this._Manager.Lobby.LobbyHost)
					{
						BaseUserInfo user = default(BaseUserInfo);
						if (this._Manager.Lobby.UserIsInLobby(senderID, ref user) && this._Manager.IsLobbyHost)
						{
							PlatformEvents.ReceivedLobbyClientMessage(user, message);
						}
					}
				});
				return;
			case 8:
				this._Manager.AddToNextUpdate(delegate
				{
					if (this._Manager.Lobby.InLobby)
					{
						BaseUserInfo baseUserInfo = default(BaseUserInfo);
						if (this._Manager.Lobby.UserIsInLobby(senderID, ref baseUserInfo))
						{
							PlatformEvents.ReceivedLobbyUserMessage(baseUserInfo, message);
						}
						else
						{
							Debug.LogInfo("[Steamworks] Invalide Lobby Message: User not in lobby | User: {0}", new object[]
							{
								baseUserInfo
							});
						}
					}
				});
				return;
			}
			base.ParseMessage(senderID, message);
		}

		// Token: 0x04000138 RID: 312
		private SteamManager _Manager;

		// Token: 0x04000139 RID: 313
		protected Callback<P2PSessionRequest_t> _callbackOnP2PSessionRequest;

		// Token: 0x0400013A RID: 314
		protected Callback<P2PSessionConnectFail_t> _callbackOnP2PSessionConnectFail;
	}
}
