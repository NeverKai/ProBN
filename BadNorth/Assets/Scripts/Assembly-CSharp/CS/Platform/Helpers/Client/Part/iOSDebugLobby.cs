using System;
using System.Collections.Generic;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Helpers.Client.Part
{
	// Token: 0x02000024 RID: 36
	internal class iOSDebugLobby : MonoBehaviour
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00004E64 File Offset: 0x00003264
		public void Awake()
		{
			this._Manager = base.gameObject.GetComponent<BasePlatformManager>();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004E77 File Offset: 0x00003277
		public bool IsInLobby
		{
			get
			{
				return this._inLobby;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004E7F File Offset: 0x0000327F
		public bool IsLobbyHost
		{
			get
			{
				return this._inLobby;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004E87 File Offset: 0x00003287
		public bool IsJoiningLobby
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004E8C File Offset: 0x0000328C
		public void CreateLobby()
		{
			if (this._leavingLobby)
			{
				this._WantsNewLobby = true;
			}
			else if (!this._JoiningLobby && !this._inLobby)
			{
				this._JoiningLobby = true;
				PlatformEvents.LobbyCreating();
				this._Manager.AddToNextUpdate(delegate
				{
					this._JoiningLobby = false;
					this._inLobby = true;
					PlatformEvents.LobbyNewHost(this._Manager.GetUserBaseInfo());
					PlatformEvents.LobbyUserJoined(this._Manager.GetUserBaseInfo());
					PlatformEvents.LobbyJoined();
					this._LobbyData.Clear();
					PlatformEvents.LobbyHost();
					if (this._leavingLobby)
					{
						this.LeaveLobby();
					}
				});
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004EEC File Offset: 0x000032EC
		public void LeaveLobby()
		{
			if ((this._JoiningLobby || this._inLobby) && !this._leavingLobby && !this._JoiningLobby)
			{
				this._leavingLobby = true;
				PlatformEvents.LobbyLeaving();
				this._leavingLobby = true;
				this._inLobby = false;
				this._JoiningLobby = false;
				this._Manager.AddToNextUpdate(delegate
				{
					PlatformEvents.LobbyLeft();
					this._Manager.AddToNextUpdate(delegate
					{
						this._leavingLobby = false;
						if (this._WantsNewLobby)
						{
							this._WantsNewLobby = false;
							this._Manager.AddToNextUpdate(delegate
							{
								this.CreateLobby();
							});
						}
					});
				});
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004F5D File Offset: 0x0000335D
		public bool GetLobbyData(string dataKey, out string dataOut)
		{
			if (this._inLobby && this._LobbyData.ContainsKey(dataKey))
			{
				dataOut = this._LobbyData[dataKey];
			}
			else
			{
				dataOut = string.Empty;
			}
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004F96 File Offset: 0x00003396
		public bool SetLobbyData(string dataKey, string dataIn)
		{
			if (!this._inLobby)
			{
				return false;
			}
			if (this._LobbyData.ContainsKey(dataKey))
			{
				this._LobbyData[dataKey] = dataIn;
			}
			else
			{
				this._LobbyData.Add(dataKey, dataIn);
			}
			return false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004FD6 File Offset: 0x000033D6
		public void SendLobbyData()
		{
			if (this._inLobby)
			{
				PlatformEvents.LobbyDataUpdated();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004FE8 File Offset: 0x000033E8
		public bool GetLobbyInfo(ref LobbyInformation lobbyInfo)
		{
			lobbyInfo.host.Blank();
			lobbyInfo.maxSlots = 0U;
			if (this._inLobby)
			{
				lobbyInfo.host = this._Manager.GetUserBaseInfo();
				lobbyInfo.maxSlots = 1U;
			}
			lobbyInfo.lobbyType = LOBBY_TYPE.PRIVATE;
			return false;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00005027 File Offset: 0x00003427
		public int TotalInLobby
		{
			get
			{
				return (!this._inLobby) ? 0 : 1;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000503B File Offset: 0x0000343B
		public bool GetLobbyUser(int index, ref BaseUserInfo userData)
		{
			if (index == 0)
			{
				userData = this._Manager.GetUserBaseInfo();
			}
			return false;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005055 File Offset: 0x00003455
		public Texture2D GetLobbyUserImage(BaseUserInfo info)
		{
			return new Texture2D(32, 32);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005060 File Offset: 0x00003460
		public bool SendLobbyMessage(BaseUserInfo userInfo, PlatformMessageBase message)
		{
			if (userInfo != this._Manager.GetUserBaseInfo())
			{
				return false;
			}
			this.SendLobbyMessage(message);
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005084 File Offset: 0x00003484
		public bool SendLobbyMessage(PlatformMessageBase message)
		{
			if (!this._inLobby)
			{
				return false;
			}
			if (message.MessageType == MessageTypes.LOBBY_CLIENT_MESSAGE)
			{
				this._Manager.AddToNextUpdate(delegate
				{
					PlatformEvents.ReceivedLobbyClientMessage(this._Manager.GetUserBaseInfo(), new DataReader(message.RawMessage, true));
				});
				return true;
			}
			if (message.MessageType == MessageTypes.LOBBY_HOST_MESSAGE)
			{
				this._Manager.AddToNextUpdate(delegate
				{
					if (this.IsInLobby)
					{
						PlatformEvents.ReceivedLobbyHostMessage(new DataReader(message.RawMessage, true));
					}
				});
			}
			else
			{
				this._Manager.AddToNextUpdate(delegate
				{
					if (this.IsInLobby)
					{
						PlatformEvents.ReceivedLobbyUserMessage(this._Manager.GetUserBaseInfo(), new DataReader(message.RawMessage, true));
					}
				});
			}
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005121 File Offset: 0x00003521
		public bool UserIsInLobby(BaseUserInfo user)
		{
			return this._Manager.GetUserBaseInfo() == user;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005134 File Offset: 0x00003534
		public bool UserIsLobbyHost(BaseUserInfo user)
		{
			return this._Manager.GetUserBaseInfo() == user;
		}

		// Token: 0x04000045 RID: 69
		private bool _inLobby;

		// Token: 0x04000046 RID: 70
		private bool _JoiningLobby;

		// Token: 0x04000047 RID: 71
		private bool _leavingLobby;

		// Token: 0x04000048 RID: 72
		private bool _WantsNewLobby;

		// Token: 0x04000049 RID: 73
		private BasePlatformManager _Manager;

		// Token: 0x0400004A RID: 74
		private Dictionary<string, string> _LobbyData = new Dictionary<string, string>();
	}
}
