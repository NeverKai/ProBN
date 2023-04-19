using System;
using System.Net;
using System.Runtime.CompilerServices;
using CS.Platform.Steam.Server.Part;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CS.Platform.Steam.Server
{
	// Token: 0x020000AC RID: 172
	public class SteamServer<ClientConnectionInfo>
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x0001BB6C File Offset: 0x00019F6C
		public SteamServer()
		{
			if (SteamServer<ClientConnectionInfo>.action == null)
			{
				SteamServer<ClientConnectionInfo>.action = new SteamServer<ClientConnectionInfo>.DisconnectUserCall(SteamServer<ClientConnectionInfo>.DoNothingDisconnectUserCall);
			}
			this._DisconnectUser = SteamServer<ClientConnectionInfo>.action;
			if (SteamServer<ClientConnectionInfo>.action1 == null)
			{
				SteamServer<ClientConnectionInfo>.action1 = new SteamServer<ClientConnectionInfo>.CanDisconnectUserCall(SteamServer<ClientConnectionInfo>.NoCanDisconnectUser);
			}
			this._CanDisconnectUser = SteamServer<ClientConnectionInfo>.action1;
			//base..ctor();
			this._callbackSteamMessageFailed = Callback<GCMessageFailed_t>.CreateGameServer(new Callback<GCMessageFailed_t>.DispatchDelegate(this.OnSteamMessageFailed));
			this._callbackSteamConnectionFail = Callback<SteamServerConnectFailure_t>.CreateGameServer(new Callback<SteamServerConnectFailure_t>.DispatchDelegate(this.OnSteamConnectionFail));
			this._callbackSteamServerConnected = Callback<SteamServersConnected_t>.CreateGameServer(new Callback<SteamServersConnected_t>.DispatchDelegate(this.OnSteamServerConnected));
			this._callbackSteamServerDisconnected = Callback<SteamServersDisconnected_t>.CreateGameServer(new Callback<SteamServersDisconnected_t>.DispatchDelegate(this.OnSteamServerDisconnected));
			this._callbackSteamSecuritReceived = Callback<GSPolicyResponse_t>.CreateGameServer(new Callback<GSPolicyResponse_t>.DispatchDelegate(this.OnSteamSecuritReceived));
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001BC38 File Offset: 0x0001A038
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001BC40 File Offset: 0x0001A040
		public SteamServer<ClientConnectionInfo>.DisconnectUserCall DisconnectUser
		{
			get
			{
				return this._DisconnectUser;
			}
			set
			{
				this._DisconnectUser = value;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001BC49 File Offset: 0x0001A049
		private static void DoNothingDisconnectUserCall(ClientConnectionInfo connection, string reason)
		{
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001BC4B File Offset: 0x0001A04B
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0001BC53 File Offset: 0x0001A053
		public SteamServer<ClientConnectionInfo>.CanDisconnectUserCall CanDisconnectUser
		{
			get
			{
				return this._CanDisconnectUser;
			}
			set
			{
				this._CanDisconnectUser = value;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001BC5C File Offset: 0x0001A05C
		private static bool NoCanDisconnectUser(ClientConnectionInfo connection)
		{
			return true;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001BC5F File Offset: 0x0001A05F
		public static bool Initialized
		{
			get
			{
				return SteamServer<ClientConnectionInfo>._Initialized;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001BC68 File Offset: 0x0001A068
		public uint GetOwnIP
		{
			get
			{
				if (BitConverter.IsLittleEndian)
				{
					byte[] bytes = BitConverter.GetBytes(SteamGameServer.GetPublicIP());
					Array.Reverse(bytes);
					return BitConverter.ToUInt32(bytes, 0);
				}
				return SteamGameServer.GetPublicIP();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001BC9D File Offset: 0x0001A09D
		public ulong GetOwnID
		{
			get
			{
				return (ulong)SteamGameServer.GetSteamID();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001BCA9 File Offset: 0x0001A0A9
		public ushort GetGamePort
		{
			get
			{
				return this._gamePort;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001BCB1 File Offset: 0x0001A0B1
		public SteamConnections<ClientConnectionInfo> Connections
		{
			get
			{
				return this._Connections;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001BCB9 File Offset: 0x0001A0B9
		public SteamAuthenticator<ClientConnectionInfo> Authenticator
		{
			get
			{
				return this._Authenticator;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001BCC1 File Offset: 0x0001A0C1
		public SteamPostBox<ClientConnectionInfo> PostBox
		{
			get
			{
				return this._PostBox;
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001BCCC File Offset: 0x0001A0CC
		public void ServerStart(string sIP, ushort gamePort, ushort steamPort, ushort steamQueryPort, string serverVersion, string serverName, string productName, string gameDescription, bool dedicatedServer, bool passwordProtected, int maxPlayers, string region = "")
		{
			uint unIP = 0U;
			if (sIP != null)
			{
				IPAddress ipaddress = IPAddress.Parse(sIP);
				byte[] addressBytes = ipaddress.GetAddressBytes();
				unIP = BitConverter.ToUInt32(addressBytes, 0);
			}
			SteamServer<ClientConnectionInfo>._Initialized = GameServer.Init(unIP, steamPort, gamePort, steamQueryPort, EServerMode.eServerModeAuthenticationAndSecure, serverVersion);
			if (SteamServer<ClientConnectionInfo>._Initialized)
			{
				SteamGameServer.SetServerName(serverName);
				this._gamePort = gamePort;
				SteamGameServer.SetProduct(productName);
				SteamGameServer.SetGameDescription(gameDescription);
				SteamGameServer.SetModDir(productName);
				SteamGameServer.SetDedicatedServer(dedicatedServer);
				SteamGameServer.SetPasswordProtected(passwordProtected);
				SteamGameServer.SetMapName(SceneManager.GetActiveScene().name);
				SteamGameServer.SetMaxPlayerCount(maxPlayers);
				SteamGameServer.SetRegion(region);
				SteamGameServer.LogOnAnonymous();
				SteamGameServer.EnableHeartbeats(true);
				SteamGameServer.SetHeartbeatInterval(-1);
				SteamGameServer.ForceHeartbeat();
				if (this.Connections == null)
				{
					this._Connections = new SteamConnections<ClientConnectionInfo>(this);
				}
				if (this.Authenticator == null)
				{
					this._Authenticator = new SteamAuthenticator<ClientConnectionInfo>(this);
				}
				if (this.PostBox == null)
				{
					this._PostBox = new SteamPostBox<ClientConnectionInfo>(this);
				}
				Debug.LogError("[Steamworks-Server] Initialized server.");
			}
			else
			{
				Debug.LogError("[Steamworks-Server] Failed to initialize server.");
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001BDDE File Offset: 0x0001A1DE
		public void ServerShutdown()
		{
			if (SteamServer<ClientConnectionInfo>._Initialized)
			{
				SteamGameServer.LogOff();
				this._PostBox.StopMessageThread();
				this._PostBox.WaitForMessageThreadEnd();
				SteamServer<ClientConnectionInfo>._Initialized = false;
				Debug.LogError("[Steamworks-Server] Server shutdown.");
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001BE15 File Offset: 0x0001A215
		public void Update()
		{
			if (SteamServer<ClientConnectionInfo>._Initialized)
			{
				GameServer.RunCallbacks();
				this.Connections.InforceAuthentication();
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001BE31 File Offset: 0x0001A231
		public void SetMapName(string mapName)
		{
			SteamGameServer.SetMapName(mapName);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001BE39 File Offset: 0x0001A239
		public void SetMaxPlayers(int maxPlayers)
		{
			SteamGameServer.SetMaxPlayerCount(maxPlayers);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001BE41 File Offset: 0x0001A241
		protected void OnSteamMessageFailed(GCMessageFailed_t message)
		{
			Debug.LogError("[Steamworks-Server] Message failed to reach GC (It may be down temporarily).");
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001BE4D File Offset: 0x0001A24D
		protected void OnSteamConnectionFail(SteamServerConnectFailure_t message)
		{
			Debug.LogError("[Steamworks-Server] Failed steam connection. Reason: " + message.m_eResult);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001BE6A File Offset: 0x0001A26A
		protected void OnSteamServerConnected(SteamServersConnected_t message)
		{
			Debug.LogError("[Steamworks-Server] Steam connection astablished.");
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001BE76 File Offset: 0x0001A276
		protected void OnSteamServerDisconnected(SteamServersDisconnected_t message)
		{
			Debug.LogError("[Steamworks-Server] Steam disconnection. Reason: " + message.m_eResult);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001BE93 File Offset: 0x0001A293
		protected void OnSteamSecuritReceived(GSPolicyResponse_t message)
		{
			Debug.LogError("[Steamworks-Server] Steam securit: " + message.m_bSecure);
		}

		// Token: 0x0400030B RID: 779
		private SteamServer<ClientConnectionInfo>.DisconnectUserCall _DisconnectUser;

		// Token: 0x0400030C RID: 780
		private SteamServer<ClientConnectionInfo>.CanDisconnectUserCall _CanDisconnectUser;

		// Token: 0x0400030D RID: 781
		protected static bool _Initialized;

		// Token: 0x0400030E RID: 782
		private ushort _gamePort;

		// Token: 0x0400030F RID: 783
		private SteamConnections<ClientConnectionInfo> _Connections;

		// Token: 0x04000310 RID: 784
		private SteamAuthenticator<ClientConnectionInfo> _Authenticator;

		// Token: 0x04000311 RID: 785
		private SteamPostBox<ClientConnectionInfo> _PostBox;

		// Token: 0x04000312 RID: 786
		protected Callback<GCMessageFailed_t> _callbackSteamMessageFailed;

		// Token: 0x04000313 RID: 787
		protected Callback<SteamServerConnectFailure_t> _callbackSteamConnectionFail;

		// Token: 0x04000314 RID: 788
		protected Callback<SteamServersConnected_t> _callbackSteamServerConnected;

		// Token: 0x04000315 RID: 789
		protected Callback<SteamServersDisconnected_t> _callbackSteamServerDisconnected;

		// Token: 0x04000316 RID: 790
		protected Callback<GSPolicyResponse_t> _callbackSteamSecuritReceived;

		// Token: 0x04000317 RID: 791
		[CompilerGenerated]
		private static SteamServer<ClientConnectionInfo>.DisconnectUserCall action;

		// Token: 0x04000318 RID: 792
		[CompilerGenerated]
		private static SteamServer<ClientConnectionInfo>.CanDisconnectUserCall action1;

		// Token: 0x020000AD RID: 173
		// (Invoke) Token: 0x0600065B RID: 1627
		public delegate void DisconnectUserCall(ClientConnectionInfo connection, string reason);

		// Token: 0x020000AE RID: 174
		// (Invoke) Token: 0x0600065F RID: 1631
		public delegate bool CanDisconnectUserCall(ClientConnectionInfo connection);
	}
}
