using System;
using System.Collections.Generic;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Helpers.Client.Part
{
	// Token: 0x02000022 RID: 34
	internal class AndroidDebugLobby : MonoBehaviour
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00004887 File Offset: 0x00002C87
		public void Awake()
		{
			this._Manager = base.gameObject.GetComponent<BasePlatformManager>();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000489A File Offset: 0x00002C9A
		public bool IsInLobby
		{
			get
			{
				return this._inLobby;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000048A2 File Offset: 0x00002CA2
		public bool IsLobbyHost
		{
			get
			{
				return this._inLobby;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000048AA File Offset: 0x00002CAA
		public bool IsJoiningLobby
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000048B0 File Offset: 0x00002CB0
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

		// Token: 0x0600006F RID: 111 RVA: 0x00004910 File Offset: 0x00002D10
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

		// Token: 0x06000070 RID: 112 RVA: 0x00004981 File Offset: 0x00002D81
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

		// Token: 0x06000071 RID: 113 RVA: 0x000049BA File Offset: 0x00002DBA
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

		// Token: 0x06000072 RID: 114 RVA: 0x000049FA File Offset: 0x00002DFA
		public void SendLobbyData()
		{
			if (this._inLobby)
			{
				PlatformEvents.LobbyDataUpdated();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004A0C File Offset: 0x00002E0C
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004A4B File Offset: 0x00002E4B
		public int TotalInLobby
		{
			get
			{
				return (!this._inLobby) ? 0 : 1;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004A5F File Offset: 0x00002E5F
		public bool GetLobbyUser(int index, ref BaseUserInfo userData)
		{
			if (index == 0)
			{
				userData = this._Manager.GetUserBaseInfo();
			}
			return false;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004A79 File Offset: 0x00002E79
		public Texture2D GetLobbyUserImage(BaseUserInfo info)
		{
			return new Texture2D(32, 32);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004A84 File Offset: 0x00002E84
		public bool SendLobbyMessage(BaseUserInfo userInfo, PlatformMessageBase message)
		{
			if (userInfo != this._Manager.GetUserBaseInfo())
			{
				return false;
			}
			this.SendLobbyMessage(message);
			return true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004AA8 File Offset: 0x00002EA8
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

		// Token: 0x06000079 RID: 121 RVA: 0x00004B45 File Offset: 0x00002F45
		public bool UserIsInLobby(BaseUserInfo user)
		{
			return this._Manager.GetUserBaseInfo() == user;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004B58 File Offset: 0x00002F58
		public bool UserIsLobbyHost(BaseUserInfo user)
		{
			return this._Manager.GetUserBaseInfo() == user;
		}

		// Token: 0x0400003E RID: 62
		private bool _inLobby;

		// Token: 0x0400003F RID: 63
		private bool _JoiningLobby;

		// Token: 0x04000040 RID: 64
		private bool _leavingLobby;

		// Token: 0x04000041 RID: 65
		private bool _WantsNewLobby;

		// Token: 0x04000042 RID: 66
		private BasePlatformManager _Manager;

		// Token: 0x04000043 RID: 67
		private Dictionary<string, string> _LobbyData = new Dictionary<string, string>();
	}
}
