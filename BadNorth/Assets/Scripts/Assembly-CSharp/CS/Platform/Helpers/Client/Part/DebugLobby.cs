using System;
using System.Collections.Generic;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform.Helpers.Client.Part
{
	// Token: 0x0200003C RID: 60
	internal class DebugLobby : MonoBehaviour
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000BB87 File Offset: 0x00009F87
		public void Awake()
		{
			this._Manager = base.gameObject.GetComponent<BasePlatformManager>();
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000BB9A File Offset: 0x00009F9A
		public bool IsInLobby
		{
			get
			{
				return this._inLobby;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000BBA2 File Offset: 0x00009FA2
		public bool IsLobbyHost
		{
			get
			{
				return this._inLobby;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000BBAA File Offset: 0x00009FAA
		public bool IsJoiningLobby
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000BBB0 File Offset: 0x00009FB0
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

		// Token: 0x0600024E RID: 590 RVA: 0x0000BC10 File Offset: 0x0000A010
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

		// Token: 0x0600024F RID: 591 RVA: 0x0000BC81 File Offset: 0x0000A081
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

		// Token: 0x06000250 RID: 592 RVA: 0x0000BCBA File Offset: 0x0000A0BA
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

		// Token: 0x06000251 RID: 593 RVA: 0x0000BCFA File Offset: 0x0000A0FA
		public void SendLobbyData()
		{
			if (this._inLobby)
			{
				PlatformEvents.LobbyDataUpdated();
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000BD0C File Offset: 0x0000A10C
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000BD4B File Offset: 0x0000A14B
		public int TotalInLobby
		{
			get
			{
				return (!this._inLobby) ? 0 : 1;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000BD5F File Offset: 0x0000A15F
		public bool GetLobbyUser(int index, ref BaseUserInfo userData)
		{
			if (index == 0)
			{
				userData = this._Manager.GetUserBaseInfo();
			}
			return false;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000BD79 File Offset: 0x0000A179
		public Texture2D GetLobbyUserImage(BaseUserInfo info)
		{
			return new Texture2D(32, 32);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000BD84 File Offset: 0x0000A184
		public bool SendLobbyMessage(BaseUserInfo userInfo, PlatformMessageBase message)
		{
			if (userInfo != this._Manager.GetUserBaseInfo())
			{
				return false;
			}
			this.SendLobbyMessage(message);
			return true;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000BDA8 File Offset: 0x0000A1A8
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

		// Token: 0x06000258 RID: 600 RVA: 0x0000BE45 File Offset: 0x0000A245
		public bool UserIsInLobby(BaseUserInfo user)
		{
			return this._Manager.GetUserBaseInfo() == user;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000BE58 File Offset: 0x0000A258
		public bool UserIsLobbyHost(BaseUserInfo user)
		{
			return this._Manager.GetUserBaseInfo() == user;
		}

		// Token: 0x040000EE RID: 238
		private bool _inLobby;

		// Token: 0x040000EF RID: 239
		private bool _JoiningLobby;

		// Token: 0x040000F0 RID: 240
		private bool _leavingLobby;

		// Token: 0x040000F1 RID: 241
		private bool _WantsNewLobby;

		// Token: 0x040000F2 RID: 242
		private BasePlatformManager _Manager;

		// Token: 0x040000F3 RID: 243
		private Dictionary<string, string> _LobbyData = new Dictionary<string, string>();
	}
}
