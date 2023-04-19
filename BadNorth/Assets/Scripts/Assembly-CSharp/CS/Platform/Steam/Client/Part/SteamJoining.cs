using System;
using System.Net;
using CS.Platform.Utils;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000044 RID: 68
	public class SteamJoining : MonoBehaviour
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000CFAC File Offset: 0x0000B3AC
		public virtual bool HasGame
		{
			get
			{
				object @lock = SteamJoining._lock;
				bool gameInvite;
				lock (@lock)
				{
					gameInvite = this._GameInvite;
				}
				return gameInvite;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000CFEC File Offset: 0x0000B3EC
		public string ServerIP
		{
			get
			{
				object @lock = SteamJoining._lock;
				string serverIP;
				lock (@lock)
				{
					serverIP = this._ServerIP;
				}
				return serverIP;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000D02C File Offset: 0x0000B42C
		public string ServerPort
		{
			get
			{
				object @lock = SteamJoining._lock;
				string serverPort;
				lock (@lock)
				{
					serverPort = this._ServerPort;
				}
				return serverPort;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000D06C File Offset: 0x0000B46C
		public string InvitedUser
		{
			get
			{
				object @lock = SteamJoining._lock;
				string inviteFrom;
				lock (@lock)
				{
					inviteFrom = this._InviteFrom;
				}
				return inviteFrom;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000D0AC File Offset: 0x0000B4AC
		public string ServerPassword
		{
			get
			{
				return this._serverPassword;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000D0B4 File Offset: 0x0000B4B4
		public virtual bool HasLobby
		{
			get
			{
				object @lock = SteamJoining._lock;
				bool hasLobby;
				lock (@lock)
				{
					hasLobby = this._HasLobby;
				}
				return hasLobby;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000D0F4 File Offset: 0x0000B4F4
		public string InvitedHost
		{
			get
			{
				object @lock = SteamJoining._lock;
				string inviteHost;
				lock (@lock)
				{
					inviteHost = this._InviteHost;
				}
				return inviteHost;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000D134 File Offset: 0x0000B534
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
			this._callbackUserInited = Callback<GameRichPresenceJoinRequested_t>.Create(new Callback<GameRichPresenceJoinRequested_t>.DispatchDelegate(this.OnJoinUser));
			this._callbackOnLobbyInviteRecieved = Callback<LobbyInvite_t>.Create(new Callback<LobbyInvite_t>.DispatchDelegate(this.OnLobbyInviteRecieved));
			this._callbackOnGameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnGameLobbyJoinRequested));
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D194 File Offset: 0x0000B594
		public void SetJoinOnMeInfo(string serverIP, ushort port)
		{
			byte[] addressBytes = IPAddress.Parse(serverIP).GetAddressBytes();
			// SteamFriends.SetRichPresence("connect", serverIP + ":" + port.ToString());
			// SteamFriends.SetRichPresence("status", "Online Gangbeast");
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D1E0 File Offset: 0x0000B5E0
		public void SetJoinOnMeInfo(CSteamID serverID, uint serverIP, ushort serverPort)
		{
			SteamUser.AdvertiseGame(serverID, serverIP, serverPort);
			// SteamFriends.SetRichPresence("connect", serverIP + ":" + serverPort.ToString());
			// SteamFriends.SetRichPresence("status", "Online Gangbeast");
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D22D File Offset: 0x0000B62D
		public void ClearJoinMeInfo()
		{
			// SteamFriends.SetRichPresence("connect", null);
			// SteamFriends.SetRichPresence("status", null);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000D248 File Offset: 0x0000B648
		private void OnJoinUser(GameRichPresenceJoinRequested_t inviteString)
		{
			object @lock = SteamJoining._lock;
			lock (@lock)
			{
				this._InviteFrom = string.Empty;
					// SteamFriends.GetFriendPersonaName(inviteString.m_steamIDFriend);
				int num = inviteString.m_rgchConnect.IndexOf(":");
				this._ServerIP = inviteString.m_rgchConnect.Substring(0, num);
				this._ServerPort = inviteString.m_rgchConnect.Substring(num + 1);
				this._GameInvite = true;
				this.JoinInvitedGame();
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D2DC File Offset: 0x0000B6DC
		public bool JoinInvitedLobby()
		{
			bool result = false;
			object @lock = SteamJoining._lock;
			lock (@lock)
			{
				if (this.HasLobby)
				{
					result = true;
					this._Manager.Lobby.JoinLobby(this._invitedLobbyID);
					this._invitedLobbyID = 0UL;
					this._HasLobby = false;
					this._InviteHost = string.Empty;
					PlatformEvents.UsedLobbyInviteEvent();
				}
			}
			return result;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000D358 File Offset: 0x0000B758
		public bool JoinInvitedGame()
		{
			bool result = false;
			object @lock = SteamJoining._lock;
			lock (@lock)
			{
				if (this.HasGame)
				{
					result = true;
					PlatformEvents.JoinGame(this._ServerIP, Convert.ToInt32(this._ServerPort), this._serverPassword);
					this._GameInvite = false;
					this._ServerIP = string.Empty;
					this._ServerPort = string.Empty;
					this._InviteFrom = string.Empty;
					this._serverPassword = string.Empty;
					PlatformEvents.UsedGameInviteEvent();
				}
			}
			return result;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D3F4 File Offset: 0x0000B7F4
		protected void OnLobbyInviteRecieved(LobbyInvite_t message)
		{
			if (message.m_ulGameID == this._Manager.AppID)
			{
				object @lock = SteamJoining._lock;
				lock (@lock)
				{
					this._HasLobby = true;
					this._invitedLobbyID = message.m_ulSteamIDLobby;
					this._InviteHost = string.Empty;
						// SteamFriends.GetFriendPersonaName((CSteamID)message.m_ulSteamIDUser);
					PlatformEvents.ReceivedLobbyInvite();
				}
			}
			else
			{
				CS.Platform.Utils.Debug.LogInfo("[Steamworks] Invite for diferent app", new object[0]);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D488 File Offset: 0x0000B888
		protected void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t message)
		{
			object @lock = SteamJoining._lock;
			lock (@lock)
			{
				this._HasLobby = true;
				this._invitedLobbyID = (ulong)message.m_steamIDLobby;
				this._InviteHost = string.Empty;
					// SteamFriends.GetFriendPersonaName(message.m_steamIDFriend);
				this.JoinInvitedLobby();
			}
		}

		// Token: 0x0400010F RID: 271
		private SteamManager _Manager;

		// Token: 0x04000110 RID: 272
		private static object _lock = new object();

		// Token: 0x04000111 RID: 273
		private bool _GameInvite;

		// Token: 0x04000112 RID: 274
		private string _ServerIP = string.Empty;

		// Token: 0x04000113 RID: 275
		private string _ServerPort = string.Empty;

		// Token: 0x04000114 RID: 276
		private string _InviteFrom = string.Empty;

		// Token: 0x04000115 RID: 277
		private string _serverPassword = string.Empty;

		// Token: 0x04000116 RID: 278
		private bool _HasLobby;

		// Token: 0x04000117 RID: 279
		public string _InviteHost = string.Empty;

		// Token: 0x04000118 RID: 280
		private ulong _invitedLobbyID;

		// Token: 0x04000119 RID: 281
		protected Callback<GameRichPresenceJoinRequested_t> _callbackUserInited;

		// Token: 0x0400011A RID: 282
		protected Callback<LobbyInvite_t> _callbackOnLobbyInviteRecieved;

		// Token: 0x0400011B RID: 283
		protected Callback<GameLobbyJoinRequested_t> _callbackOnGameLobbyJoinRequested;
	}
}
