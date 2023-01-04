using System;
using System.Net;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000040 RID: 64
	public class SteamAuthenticator : MonoBehaviour
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000C347 File Offset: 0x0000A747
		public bool ConnectedToServer
		{
			get
			{
				return this._serverID != CSteamID.Nil;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000C359 File Offset: 0x0000A759
		public uint ConnectedServerIP
		{
			get
			{
				return this._serverIP;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000C361 File Offset: 0x0000A761
		public ushort ConnectedServerPort
		{
			get
			{
				return this._serverPort;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000C369 File Offset: 0x0000A769
		public CSteamID ConnectedServerIF
		{
			get
			{
				return this._serverID;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C371 File Offset: 0x0000A771
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C37F File Offset: 0x0000A77F
		public void CreateAuthenticationTicket(out byte[] message, out uint ticketSize)
		{
			if (this._currentAuthTicket != HAuthTicket.Invalid)
			{
				this.FreeTicket();
			}
			message = new byte[SteamElements.MaxTicketSize];
			this._currentAuthTicket = SteamUser.GetAuthSessionTicket(message, message.Length, out ticketSize);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C3BA File Offset: 0x0000A7BA
		public void FreeTicket()
		{
			SteamUser.CancelAuthTicket(this._currentAuthTicket);
			this._currentAuthTicket = HAuthTicket.Invalid;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C3D4 File Offset: 0x0000A7D4
		public void CreateAuthenticationTicketForServer(out byte[] message, out int ticketSize, CSteamID serverID, uint serverIP, ushort serverPort, bool secure)
		{
			if (this.ConnectedToServer)
			{
				this.DisconnectServer();
			}
			this._serverIP = serverIP;
			this._serverPort = serverPort;
			this._serverID = serverID;
			message = new byte[SteamElements.MaxTicketSize];
			ticketSize = SteamUser.InitiateGameConnection(message, message.Length, this._serverID, this._serverIP, this._serverPort, true);
			this._Manager.Join.SetJoinOnMeInfo(this._serverID, this._serverIP, this._serverPort);
			this._Manager.Utilities.SetUpJoinInfo(new IPAddress((long)((ulong)this._serverIP)).ToString(), this._serverPort.ToString());
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C489 File Offset: 0x0000A889
		public void DisconnectServer()
		{
			if (this.ConnectedToServer)
			{
				SteamUser.TerminateGameConnection(this._serverIP, this._serverPort);
			}
			this._Manager.Utilities.SetUpJoinInfo(string.Empty, string.Empty);
		}

		// Token: 0x040000FB RID: 251
		private SteamManager _Manager;

		// Token: 0x040000FC RID: 252
		private HAuthTicket _currentAuthTicket = HAuthTicket.Invalid;

		// Token: 0x040000FD RID: 253
		private uint _serverIP;

		// Token: 0x040000FE RID: 254
		private ushort _serverPort;

		// Token: 0x040000FF RID: 255
		private CSteamID _serverID = CSteamID.Nil;
	}
}
