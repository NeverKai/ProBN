using System;
using System.Net;
using System.Net.Sockets;
using CS.Platform.Core.Server.Part;
using CS.Platform.Utils;
using UnityEngine;

namespace CS.Platform.Core.Server
{
	// Token: 0x0200009D RID: 157
	public class BaseA2S
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x00019884 File Offset: 0x00017C84
		public BaseA2S(short gamePort, short queryPort, string serverVersion, string serverName, string productName, string gameDescription, string gameFolder, long gameID, A2SServerInfo.ServerType serverType, bool privateServer, int maxPlayers)
		{
			this._Initialized = true;
			try
			{
				this._udpClient = new UdpClient((int)queryPort);
			}
			catch (SocketException ex)
			{
				CS.Platform.Utils.Debug.LogError("[BaseA2S] BaseA2S fail: {0} | Nat: {1} | Sock: {2} | Inner: {3} | InnerMess: {4}", new object[]
				{
					ex.Message,
					ex.NativeErrorCode,
					ex.SocketErrorCode,
					ex.InnerException != null,
					(ex.InnerException == null) ? string.Empty : ex.InnerException.Message
				});
				this._Initialized = false;
			}
			this._serverInfo.Initialise();
			this._serverInfo.SetServerName(serverName);
			this._serverInfo.SetCurrentMap("NA");
			this._serverInfo.SetFolder(gameFolder);
			this._serverInfo.SetGameName(productName);
			this._serverInfo.SetServerGameVersion(serverVersion);
			this._serverInfo.SetKeyWords(gameDescription);
			this._serverInfo.SetProtocol(0);
			this._serverInfo.SetGameID(gameID);
			this._serverInfo.SetPlayers(0);
			this._serverInfo.SetMaxPlayers(maxPlayers);
			this._serverInfo.SetBots(0);
			this._serverInfo.SetServerType(serverType);
			this._serverInfo.SetPrivate(privateServer);
			this._serverInfo.SetVac(false);
			this._serverInfo.SetGamePort(gamePort);
			this._serverInfo.SetServerOS(A2SServerInfo.ServerOS.Windows);
			UnityEngine.Debug.LogErrorFormat("[BaseA2S] Prepped A2S on platform: {0}", new object[]
			{
				A2SServerInfo.ServerOS.Windows
			});
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00019A54 File Offset: 0x00017E54
		public A2SServerInfo ServerInfo
		{
			get
			{
				return this._serverInfo;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00019A5C File Offset: 0x00017E5C
		public A2SPlayers PlayerInfo
		{
			get
			{
				return this._playerInfo;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00019A64 File Offset: 0x00017E64
		public A2SRules RuleInfo
		{
			get
			{
				return this._ruleInfo;
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00019A6C File Offset: 0x00017E6C
		public void StartA2S()
		{
			if (this._Initialized)
			{
				this._udpClient.BeginReceive(new AsyncCallback(this.ReceiveMessage), null);
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019A92 File Offset: 0x00017E92
		public void Destroy()
		{
			this._udpClient.Close();
			this._udpClient = null;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00019AA8 File Offset: 0x00017EA8
		private void ReceiveMessage(IAsyncResult ar)
		{
			byte[] array = this._udpClient.EndReceive(ar, ref this._ipEndPoint);
			UnityEngine.Debug.LogError("GOT A2S DATA: " + array.ToString());
			this.NewMessage(array);
			this._udpClient.BeginReceive(new AsyncCallback(this.ReceiveMessage), null);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00019B00 File Offset: 0x00017F00
		private void NewMessage(byte[] message)
		{
			byte b = message[4];
			if (this._ipEndPoint != null)
			{
				switch (b)
				{
				case 84:
					this._serverInfo.Send(ref this._udpClient, ref this._ipEndPoint);
					break;
				case 85:
					break;
				case 86:
					break;
				default:
					if (b == 105)
					{
						this._pingInfo.Send(ref this._udpClient, ref this._ipEndPoint);
					}
					break;
				}
			}
		}

		// Token: 0x040002B2 RID: 690
		private A2SServerInfo _serverInfo = new A2SServerInfo();

		// Token: 0x040002B3 RID: 691
		private A2SPlayers _playerInfo = new A2SPlayers();

		// Token: 0x040002B4 RID: 692
		private A2SRules _ruleInfo = new A2SRules();

		// Token: 0x040002B5 RID: 693
		private A2SPing _pingInfo = new A2SPing();

		// Token: 0x040002B6 RID: 694
		private bool _Initialized;

		// Token: 0x040002B7 RID: 695
		private UdpClient _udpClient;

		// Token: 0x040002B8 RID: 696
		private IPEndPoint _ipEndPoint;
	}
}
