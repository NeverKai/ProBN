using System;
using System.Collections.Generic;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000045 RID: 69
	public class SteamLobby : MonoBehaviour
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D588 File Offset: 0x0000B988
		public bool InLobby
		{
			get
			{
				object @lock = SteamLobby._lock;
				bool inLobby;
				lock (@lock)
				{
					inLobby = this._InLobby;
				}
				return inLobby;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000D5C8 File Offset: 0x0000B9C8
		public bool JoiningLobby
		{
			get
			{
				object @lock = SteamLobby._lock;
				bool joiningLobby;
				lock (@lock)
				{
					joiningLobby = this._JoiningLobby;
				}
				return joiningLobby;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000D608 File Offset: 0x0000BA08
		public bool LobbyHost
		{
			get
			{
				object @lock = SteamLobby._lock;
				bool result;
				lock (@lock)
				{
					result = (this._InLobby && this._LobbyHost);
				}
				return result;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000D654 File Offset: 0x0000BA54
		public Guid CurrentLobbyID
		{
			get
			{
				if (this._cacheID == Guid.Empty && (ulong)this._LobbyID != 0UL)
				{
					this._cacheID = Lobby.UlongToGuid((ulong)this._LobbyID);
				}
				return this._cacheID;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000D6A4 File Offset: 0x0000BAA4
		public ulong GetHostID
		{
			get
			{
				object @lock = SteamLobby._lock;
				ulong result;
				lock (@lock)
				{
					result = (ulong)this._LobbyHostID;
				}
				return result;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000D6E8 File Offset: 0x0000BAE8
		public bool GettingInviteList
		{
			get
			{
				object @lock = SteamLobby._lock;
				bool result;
				lock (@lock)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D720 File Offset: 0x0000BB20
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
			this._CurrentLobby.host.Set(0UL, "steam", string.Empty);
			this._callbackOnLobbyCreated = Callback<LobbyCreated_t>.Create(new Callback<LobbyCreated_t>.DispatchDelegate(this.OnLobbyCreated));
			this._callbackOnLobbyEntered = Callback<LobbyEnter_t>.Create(new Callback<LobbyEnter_t>.DispatchDelegate(this.OnLobbyEntered));
			this._callbackOnLobbyChatUpdate = Callback<LobbyChatUpdate_t>.Create(new Callback<LobbyChatUpdate_t>.DispatchDelegate(this.OnLobbyChatUpdate));
			this._callbackOnRequestUserInformation = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(this.OnRequestUserInformation));
			this._callbackOnLobbyDataUpdated = Callback<LobbyDataUpdate_t>.Create(new Callback<LobbyDataUpdate_t>.DispatchDelegate(this.OnLobbyDataUpdated));
			this._callbackOnLobbyChatUpdated = Callback<LobbyChatMsg_t>.Create(new Callback<LobbyChatMsg_t>.DispatchDelegate(this.OnLobbyChatUpdated));
			this._kickMessage.MessageType = MessageTypes.BEENKICKED;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D7EC File Offset: 0x0000BBEC
		public void CreateLobby(LOBBY_TYPE type, uint max)
		{
			if (!BasePlatformManager.HasEntitlement)
			{
				return;
			}
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (!this.JoiningLobby)
				{
					if (this.InLobby)
					{
						if (!this._leavingLobby)
						{
							this.LeaveLobby();
						}
						this._WantsNewLobby = delegate()
						{
							this.CreateLobby(type, max);
						};
					}
					else
					{
						this._JoiningLobby = true;
						this._LobbyHost = true;
						PlatformEvents.LobbyCreating();
						switch (type)
						{
						case LOBBY_TYPE.PUBLIC:
							SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, (int)max);
							break;
						case LOBBY_TYPE.PRIVATE:
							SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, (int)max);
							break;
						case LOBBY_TYPE.FRIENDS_ONLY:
							SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, (int)max);
							break;
						default:
							this._JoiningLobby = false;
							this._LobbyHost = false;
							PlatformEvents.LobbyCreationFailed(ErrorCode.LOBBY_CREATE_UNSUPPORTED_TYPE);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D904 File Offset: 0x0000BD04
		private void OnLobbyCreated(LobbyCreated_t message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (message.m_eResult == EResult.k_EResultOK)
				{
					this._LobbyHost = true;
				}
				else
				{
					if (message.m_eResult == EResult.k_EResultNoConnection)
					{
						PlatformEvents.LobbyCreationFailed(ErrorCode.LOBBY_CONNECTION_FAILED);
					}
					else
					{
						PlatformEvents.LobbyCreationFailed(ErrorCode.LOBBY_CREATE_UNKNOWN);
					}
					CS.Platform.Utils.Debug.LogError("[Steamworks] Failed to create lobby", new object[0]);
					this._JoiningLobby = false;
					this._LobbyHost = false;
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D990 File Offset: 0x0000BD90
		private void OnLobbyDataUpdated(LobbyDataUpdate_t message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (message.m_ulSteamIDLobby == (ulong)this._LobbyID)
				{
					string empty = string.Empty;
					if (this.GetLobbyData(Lobby.LobbyDataUpdateFlag, out empty) && empty != this._lobbyLastUpdatedFlag)
					{
						this._lobbyLastUpdatedFlag = empty;
						PlatformEvents.LobbyDataUpdated();
					}
				}
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000DA14 File Offset: 0x0000BE14
		private void OnLobbyEntered(LobbyEnter_t message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				this._JoiningLobby = false;
				if (message.m_EChatRoomEnterResponse == 1U)
				{
					this._InLobby = true;
					this._LobbyID = (CSteamID)message.m_ulSteamIDLobby;
					int friendCountFromSource = SteamFriends.GetFriendCountFromSource(this._LobbyID);
					this._LobbyHostID = SteamMatchmaking.GetLobbyOwner(this._LobbyID);
					this._LobbyHost = (this._LobbyHostID == this._Manager.UserInfo.LoggedUserID);
					if (!SteamFriends.RequestUserInformation(this._LobbyHostID, true))
					{
						this._CurrentLobby.host.Set((ulong)this._LobbyHostID, null, SteamFriends.GetFriendPersonaName(this._LobbyHostID));
						PlatformEvents.LobbyNewHost(this._CurrentLobby.host);
					}
					else
					{
						char c = '\b';
						this._CurrentLobby.host.Set((ulong)this._LobbyHostID, null, string.Empty + c.ToString());
					}
					this._CurrentLobby.maxSlots = (uint)SteamMatchmaking.GetLobbyMemberLimit(this._LobbyID);
					for (int i = 0; i < friendCountFromSource; i++)
					{
						this.AddLobbyMember(SteamFriends.GetFriendFromSourceByIndex(this._LobbyID, i));
					}
					PlatformEvents.LobbyJoined();
					if (this._LobbyHost)
					{
						this.BecameHost();
					}
				}
				else
				{
					CS.Platform.Utils.Debug.LogError("[Steamworks] Failed to join lobby", new object[0]);
					PlatformEvents.LobbyJoiningFailed(ErrorCode.LOBBY_JOIN_UNKNOWN);
				}
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000DBB0 File Offset: 0x0000BFB0
		private void OnLobbyChatUpdate(LobbyChatUpdate_t message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				this._JoiningLobby = false;
				if (message.m_ulSteamIDLobby == (ulong)this._LobbyID)
				{
					if (message.m_rgfChatMemberStateChange == 1U)
					{
						this.AddLobbyMember((CSteamID)message.m_ulSteamIDUserChanged);
					}
					else
					{
						if ((ulong)this._LobbyHostID == message.m_ulSteamIDUserChanged)
						{
							this._LobbyHostID = SteamMatchmaking.GetLobbyOwner(this._LobbyID);
							if (!SteamFriends.RequestUserInformation(this._LobbyHostID, false))
							{
								this._CurrentLobby.host.Set((ulong)this._LobbyHostID, null, SteamFriends.GetFriendPersonaName(this._LobbyHostID));
								if (this._LobbyHostID != this._Manager.UserInfo.LoggedUserID)
								{
									PlatformEvents.LobbyNewHost(this._CurrentLobby.host);
								}
							}
							else
							{
								char c = '\b';
								this._CurrentLobby.host.Set((ulong)this._LobbyHostID, null, string.Empty + c);
							}
							if (!this._LobbyHost && this._LobbyHostID == this._Manager.UserInfo.LoggedUserID)
							{
								this._LobbyHost = true;
								this.BecameHost();
								PlatformEvents.LobbyNewHost(this._CurrentLobby.host);
							}
							else
							{
								this._LobbyHost = (this._LobbyHostID == this._Manager.UserInfo.LoggedUserID);
							}
						}
						for (int i = 0; i < this._LobbyMembers.Count; i++)
						{
							if ((ulong)this._LobbyMembers[i].userID == message.m_ulSteamIDUserChanged)
							{
								ulong userID = (ulong)this._LobbyMembers[i].userID;
								string userName = this._LobbyMembers[i].userName;
								this._LobbyMembers.RemoveAt(i);
								i = this._LobbyMembers.Count;
								PlatformEvents.LobbyUserLeft(new BaseUserInfo(userID, "steam", userName));
							}
						}
					}
				}
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000DE04 File Offset: 0x0000C204
		private void OnRequestUserInformation(PersonaStateChange_t message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				int num = this._LobbyMembers.Count - 1;
				while (0 <= num)
				{
					SteamLobby.steamLobbyMemeberInformation value = this._LobbyMembers[num];
					if ((ulong)value.userID == message.m_ulSteamID)
					{
						if (value.userName == string.Empty + '\b')
						{
							value.userName = SteamFriends.GetFriendPersonaName(value.userID);
							this._Manager.Utilities.LoadProfileImage(value.userID, ref value.userImage);
							this._LobbyMembers[num] = value;
							PlatformEvents.LobbyUserJoined(new BaseUserInfo((ulong)value.userID, "steam", value.userName));
						}
						break;
					}
					num--;
				}
				if ((ulong)this._LobbyHostID == message.m_ulSteamID)
				{
					string friendPersonaName = SteamFriends.GetFriendPersonaName((CSteamID)message.m_ulSteamID);
					if (friendPersonaName != this._CurrentLobby.host.userName)
					{
						this._CurrentLobby.host.Set((ulong)this._LobbyHostID, null, friendPersonaName);
						PlatformEvents.LobbyNewHost(this._CurrentLobby.host);
					}
				}
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000DF80 File Offset: 0x0000C380
		private void OnLobbyChatUpdated(LobbyChatMsg_t message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (message.m_ulSteamIDLobby == (ulong)this._LobbyID)
				{
					CSteamID x;
					EChatEntryType echatEntryType;
					int lobbyChatEntry = SteamMatchmaking.GetLobbyChatEntry(this._LobbyID, (int)message.m_iChatID, out x, this._LobbyMessageData, this._LobbyMessageData.Length, out echatEntryType);
					if (echatEntryType == EChatEntryType.k_EChatEntryTypeChatMsg)
					{
						if (lobbyChatEntry >= 4)
						{
							DataReader dataReader = new DataReader(this._LobbyMessageData, false);
							if (dataReader.Flag == 6)
							{
								if (x == this._LobbyHostID)
								{
									PlatformEvents.ReceivedLobbyHostMessage(dataReader);
								}
							}
							else if ((dataReader.Flag == 7 && this.LobbyHost) || dataReader.Flag == 5)
							{
								for (int i = 0; i < this._LobbyMembers.Count; i++)
								{
									if (x == this._LobbyMembers[i].userID)
									{
										if (dataReader.Flag == 7)
										{
											PlatformEvents.ReceivedLobbyClientMessage(new BaseUserInfo((ulong)this._LobbyMembers[i].userID, "steam", this._LobbyMembers[i].userName), dataReader);
										}
										else if (dataReader.Flag == 5)
										{
											PlatformEvents.ReceivedLobbyUserMessage(new BaseUserInfo((ulong)this._LobbyMembers[i].userID, "steam", this._LobbyMembers[i].userName), dataReader);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E154 File Offset: 0x0000C554
		public void JoinLobby(Guid guidLobbyID)
		{
			ulong lobbyID = Lobby.GuidToUlong(guidLobbyID);
			this.JoinLobby(lobbyID);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E170 File Offset: 0x0000C570
		public void JoinLobby(ulong lobbyID)
		{
			if (!BasePlatformManager.HasEntitlement)
			{
				return;
			}
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (!this.JoiningLobby)
				{
					this._JoiningLobby = true;
					if (this.InLobby)
					{
						this.LeaveLobby();
						this._WantsLobbyID = (CSteamID)lobbyID;
					}
					else
					{
						PlatformEvents.LobbyJoining();
						SteamMatchmaking.JoinLobby((CSteamID)lobbyID);
					}
				}
				else if (this._WantsLobbyID == (CSteamID)lobbyID)
				{
					PlatformEvents.LobbyJoining();
					SteamMatchmaking.JoinLobby((CSteamID)lobbyID);
					this._WantsLobbyID = CSteamID.Nil;
					this._JoiningLobby = false;
				}
				else
				{
					this._WantsLobbyID = (CSteamID)lobbyID;
				}
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E244 File Offset: 0x0000C644
		public void LeaveLobby()
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				PlatformEvents.LobbyLeaving();
				this._leavingLobby = true;
				SteamMatchmaking.LeaveLobby(this._LobbyID);
				this._LobbyMembers.Clear();
				this._LobbyID = (CSteamID)0UL;
				this._cacheID = Guid.Empty;
				this._InLobby = false;
				this._LobbyHost = false;
				this._LobbyHostID = (CSteamID)0UL;
				this._Manager.AddToNextUpdate(delegate
				{
					PlatformEvents.LobbyLeft();
					this._Manager.AddToNextUpdate(delegate
					{
						this._leavingLobby = false;
						if (this._WantsLobbyID != CSteamID.Nil)
						{
							this._Manager.AddToNextUpdate(delegate
							{
								this.JoinLobby((ulong)this._WantsLobbyID);
							});
							this._WantsNewLobby = null;
						}
						else if (this._WantsNewLobby != null)
						{
							this._WantsNewLobby();
							this._WantsNewLobby = null;
						}
					});
				});
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E2E8 File Offset: 0x0000C6E8
		public void BecameHost()
		{
			if (this._LobbyData != null)
			{
				this._LobbyData.Clear();
			}
			string empty = string.Empty;
			if (this.GetLobbyData(Lobby.LobbyDataUpdateFlag, out empty))
			{
				this._lobbyUpdatedFlag = empty;
			}
			else
			{
				this._lobbyUpdatedFlag = string.Empty + '\0';
			}
			PlatformEvents.LobbyHost();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E34C File Offset: 0x0000C74C
		public bool GetLobbyData(string dataKey, out string dataOut)
		{
			bool flag = false;
			dataOut = string.Empty;
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this.LobbyHost)
				{
					if (this._LobbyData != null)
					{
						flag = this._LobbyData.TryGetValue(dataKey, out dataOut);
					}
					if (!flag)
					{
						dataOut = SteamMatchmaking.GetLobbyData(this._LobbyID, dataKey);
						flag = (dataOut != string.Empty);
					}
				}
				else
				{
					dataOut = SteamMatchmaking.GetLobbyData(this._LobbyID, dataKey);
					flag = (dataOut != string.Empty);
				}
			}
			return flag;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E3F0 File Offset: 0x0000C7F0
		public void SendLobbyData()
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this.LobbyHost)
				{
					Dictionary<string, string>.Enumerator enumerator = this._LobbyData.GetEnumerator();
					while (enumerator.MoveNext())
					{
						CSteamID lobbyID = this._LobbyID;
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						string key = keyValuePair.Key;
						KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
						SteamMatchmaking.SetLobbyData(lobbyID, key, keyValuePair2.Value);
					}
					enumerator.Dispose();
					short num = (short)this._lobbyUpdatedFlag[0];
					this._lobbyUpdatedFlag = string.Empty + (char)(num + 1);
					SteamMatchmaking.SetLobbyData(this._LobbyID, Lobby.LobbyDataUpdateFlag, this._lobbyUpdatedFlag);
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E4C0 File Offset: 0x0000C8C0
		public bool SetLobbyData(string dataKey, string dataIn)
		{
			bool result = false;
			if (this.LobbyHost)
			{
				object @lock = SteamLobby._lock;
				lock (@lock)
				{
					if (this._LobbyData == null)
					{
						this._LobbyData = new Dictionary<string, string>();
					}
					if (this._LobbyData.ContainsKey(dataKey))
					{
						result = true;
						this._LobbyData[dataKey] = dataIn;
					}
					else if (dataKey == Lobby.LobbyDataUpdateFlag)
					{
						CS.Platform.Utils.Debug.LogWarning("[CorePlatform] dataKey {0} can not be as lobby data as is already in use as the LobbyDataUpdateFlag", new object[]
						{
							dataKey
						});
					}
					else
					{
						result = true;
						this._LobbyData.Add(dataKey, dataIn);
					}
				}
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E578 File Offset: 0x0000C978
		public void SetLobbyType(LOBBY_TYPE newType)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this.LobbyHost && this._CurrentLobby.lobbyType != newType)
				{
					this._CurrentLobby.lobbyType = newType;
					switch (this._CurrentLobby.lobbyType)
					{
					case LOBBY_TYPE.PUBLIC:
						SteamMatchmaking.SetLobbyType(this._LobbyID, ELobbyType.k_ELobbyTypePublic);
						break;
					case LOBBY_TYPE.PRIVATE:
						SteamMatchmaking.SetLobbyType(this._LobbyID, ELobbyType.k_ELobbyTypePrivate);
						break;
					case LOBBY_TYPE.FRIENDS_ONLY:
						SteamMatchmaking.SetLobbyType(this._LobbyID, ELobbyType.k_ELobbyTypeFriendsOnly);
						break;
					}
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E634 File Offset: 0x0000CA34
		public bool InviteUserToParty(ulong userID)
		{
			bool result = false;
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this._InLobby)
				{
					result = SteamMatchmaking.InviteUserToLobby(this._LobbyID, (CSteamID)userID);
				}
			}
			return result;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E68C File Offset: 0x0000CA8C
		public bool ShowPlatformLobbyMenu()
		{
			if (SteamUtils.IsOverlayEnabled())
			{
				if (this._InLobby)
				{
					SteamFriends.ActivateGameOverlayInviteDialog(this._LobbyID);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E6B4 File Offset: 0x0000CAB4
		private void AddLobbyMember(CSteamID userID)
		{
			bool flag = false;
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				for (int i = 0; i < this._LobbyMembers.Count; i++)
				{
					if (this._LobbyMembers[i].userID == userID)
					{
						i = this._LobbyMembers.Count;
						flag = true;
					}
				}
				if (!flag)
				{
					SteamLobby.steamLobbyMemeberInformation item = default(SteamLobby.steamLobbyMemeberInformation);
					item.userID = userID;
					if (!SteamFriends.RequestUserInformation(item.userID, true))
					{
						item.userName = SteamFriends.GetFriendPersonaName(item.userID);
						this._Manager.Utilities.LoadProfileImage(item.userID, ref item.userImage);
						this._LobbyMembers.Add(item);
						PlatformEvents.LobbyUserJoined(new BaseUserInfo((ulong)item.userID, "steam", item.userName));
					}
					else
					{
						char c = '\b';
						item.userName = string.Empty + c;
						item.userImage = CS.Platform.Utils.Random.CreateNewImageF(true, 64, 64, TextureFormat.RGBA32, false);
						this._LobbyMembers.Add(item);
					}
				}
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E80C File Offset: 0x0000CC0C
		public BaseUserInfo GetLobbyHost()
		{
			object @lock = SteamLobby._lock;
			BaseUserInfo host;
			lock (@lock)
			{
				host = this._CurrentLobby.host;
			}
			return host;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E850 File Offset: 0x0000CC50
		public bool GetLobbyInfo(ref LobbyInformation lobbyInfo)
		{
			lobbyInfo.maxSlots = 0U;
			lobbyInfo.lobbyType = LOBBY_TYPE.PRIVATE;
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this.InLobby)
				{
					lobbyInfo = this._CurrentLobby;
				}
			}
			return false;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000E8AC File Offset: 0x0000CCAC
		public int TotalInLobby
		{
			get
			{
				object @lock = SteamLobby._lock;
				int result;
				lock (@lock)
				{
					if (this.InLobby)
					{
						result = this._LobbyMembers.Count;
					}
					else
					{
						result = 0;
					}
				}
				return result;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E900 File Offset: 0x0000CD00
		public bool GetLobbyUser(int index, ref BaseUserInfo userData)
		{
			bool result = false;
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (index < this.TotalInLobby)
				{
					result = true;
					userData.Set((ulong)this._LobbyMembers[index].userID, "steam", this._LobbyMembers[index].userName);
				}
			}
			return result;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E980 File Offset: 0x0000CD80
		public bool HasLobbyMemberID(CSteamID userID)
		{
			bool result = false;
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this.InLobby)
				{
					int totalInLobby = this.TotalInLobby;
					for (int i = 0; i < totalInLobby; i++)
					{
						if (userID == this._LobbyMembers[i].userID)
						{
							result = true;
							i = totalInLobby;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000EA00 File Offset: 0x0000CE00
		public Texture2D GetLobbyUserImage(int index)
		{
			Texture2D result = null;
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (0 <= index && index < this.TotalInLobby)
				{
					result = this._LobbyMembers[index].userImage;
				}
			}
			return result;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000EA60 File Offset: 0x0000CE60
		public Texture2D GetLobbyUserImage(BaseUserInfo info)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				for (int i = 0; i < this._LobbyMembers.Count; i++)
				{
					if ((ulong)this._LobbyMembers[i].userID == info.userID)
					{
						return this._LobbyMembers[i].userImage;
					}
				}
			}
			return null;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000EAF8 File Offset: 0x0000CEF8
		public bool HasUser(BaseUserInfo info)
		{
			if (this._LobbyMembers != null && this.InLobby && info.platformKey == "steam")
			{
				for (int i = 0; i < this._LobbyMembers.Count; i++)
				{
					if (this._LobbyMembers[i].userID == (CSteamID)info.userID)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000EB7C File Offset: 0x0000CF7C
		public bool KickUser(BaseUserInfo info)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this._LobbyHost && info.userID != (ulong)this._Manager.UserInfo.LoggedUserID)
				{
					for (int i = 0; i < this._LobbyMembers.Count; i++)
					{
						if (info.userID == (ulong)this._LobbyMembers[i].userID)
						{
							this._Manager.PostBox.SendMessage(info.userID, this._kickMessage, true);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000EC48 File Offset: 0x0000D048
		public void UseBeenKickedMessage(CSteamID senderID)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (senderID == this._LobbyHostID)
				{
					this._Manager.AddToNextUpdate(delegate
					{
						this.LeaveLobby();
					});
				}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000ECA8 File Offset: 0x0000D0A8
		public bool SendLobbyMessage(BaseUserInfo user, PlatformMessageBase message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this.InLobby)
				{
					if (message.MessageType == MessageTypes.LOBBY_HOST_MESSAGE && !this.LobbyHost)
					{
						return false;
					}
					if (user.platformKey == "steam")
					{
						if (user.userID == this._Manager.GetUserID)
						{
							if (message.MessageType != MessageTypes.LOBBY_CLIENT_MESSAGE)
							{
								DataReader copyData = new DataReader(message.RawMessage, true);
								if (message.MessageType == MessageTypes.LOBBY_HOST_MESSAGE)
								{
									this._Manager.AddToNextUpdate(delegate
									{
										if (this.LobbyHost)
										{
											PlatformEvents.ReceivedLobbyHostMessage(copyData);
										}
									});
								}
								else
								{
									this._Manager.AddToNextUpdate(delegate
									{
										if (this.InLobby)
										{
											PlatformEvents.ReceivedLobbyUserMessage(this._Manager.GetUserBaseInfo(), copyData);
										}
									});
								}
								return true;
							}
							if (this.LobbyHost)
							{
								DataReader copyData = new DataReader(message.RawMessage, true);
								this._Manager.AddToNextUpdate(delegate
								{
									if (this.LobbyHost)
									{
										PlatformEvents.ReceivedLobbyClientMessage(this._Manager.GetUserBaseInfo(), copyData);
									}
								});
								return true;
							}
							return false;
						}
						else if (0 <= this._LobbyMembers.FindIndex((SteamLobby.steamLobbyMemeberInformation x) => x.userID == (CSteamID)user.userID))
						{
							this._Manager.PostBox.SendMessage(user.userID, message, true);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000EE64 File Offset: 0x0000D264
		public bool SendLobbyMessage(PlatformMessageBase message)
		{
			object @lock = SteamLobby._lock;
			lock (@lock)
			{
				if (this.InLobby)
				{
					if (message.MessageType == MessageTypes.LOBBY_HOST_MESSAGE && !this.LobbyHost)
					{
						return false;
					}
					if (message.MessageType == MessageTypes.LOBBY_CLIENT_MESSAGE && this.LobbyHost)
					{
						DataReader copyData = new DataReader(message.RawMessage, true);
						this._Manager.AddToNextUpdate(delegate
						{
							if (this.LobbyHost)
							{
								PlatformEvents.ReceivedLobbyClientMessage(this._Manager.GetUserBaseInfo(), copyData);
							}
						});
						return true;
					}
					if (message.MessageType == MessageTypes.LOBBY_CLIENT_MESSAGE)
					{
						this._Manager.PostBox.SendMessage(this._CurrentLobby.host.userID, message, true);
						return true;
					}
					for (int i = 0; i < this._LobbyMembers.Count; i++)
					{
						if (this._LobbyMembers[i].userID != this._Manager.UserInfo.LoggedUserID)
						{
							this._Manager.PostBox.SendMessage((ulong)this._LobbyMembers[i].userID, message, true);
						}
						else
						{
							DataReader copyData = new DataReader(message.RawMessage, true);
							if (message.MessageType == MessageTypes.LOBBY_HOST_MESSAGE)
							{
								this._Manager.AddToNextUpdate(delegate
								{
									if (this.LobbyHost)
									{
										PlatformEvents.ReceivedLobbyHostMessage(copyData);
									}
								});
							}
							else
							{
								this._Manager.AddToNextUpdate(delegate
								{
									if (this.InLobby)
									{
										PlatformEvents.ReceivedLobbyUserMessage(this._Manager.GetUserBaseInfo(), copyData);
									}
								});
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000F040 File Offset: 0x0000D440
		public bool UserIsInLobby(BaseUserInfo user)
		{
			if (user.platformKey == "steam")
			{
				for (int i = 0; i < this._LobbyMembers.Count; i++)
				{
					if ((ulong)this._LobbyMembers[i].userID == user.userID)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F0A8 File Offset: 0x0000D4A8
		public bool UserIsInLobby(ulong userID, ref BaseUserInfo user)
		{
			for (int i = 0; i < this._LobbyMembers.Count; i++)
			{
				if ((ulong)this._LobbyMembers[i].userID == userID)
				{
					user.Set(userID, "steam", this._LobbyMembers[i].userName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000F113 File Offset: 0x0000D513
		public bool UserIsLobbyHost(BaseUserInfo user)
		{
			return user.userID == (ulong)this._LobbyHostID && user.platformKey == "steam";
		}

		// Token: 0x0400011C RID: 284
		private SteamManager _Manager;

		// Token: 0x0400011D RID: 285
		private bool _InLobby;

		// Token: 0x0400011E RID: 286
		private bool _JoiningLobby;

		// Token: 0x0400011F RID: 287
		private bool _LobbyHost;

		// Token: 0x04000120 RID: 288
		private static object _lock = new object();

		// Token: 0x04000121 RID: 289
		private Guid _cacheID = Guid.Empty;

		// Token: 0x04000122 RID: 290
		private LobbyInformation _CurrentLobby;

		// Token: 0x04000123 RID: 291
		private CSteamID _LobbyID;

		// Token: 0x04000124 RID: 292
		private CSteamID _LobbyHostID;

		// Token: 0x04000125 RID: 293
		private string _lobbyUpdatedFlag = string.Empty + '\0';

		// Token: 0x04000126 RID: 294
		private string _lobbyLastUpdatedFlag = string.Empty + '\0';

		// Token: 0x04000127 RID: 295
		private Dictionary<string, string> _LobbyData;

		// Token: 0x04000128 RID: 296
		private List<SteamLobby.steamLobbyMemeberInformation> _LobbyMembers = new List<SteamLobby.steamLobbyMemeberInformation>();

		// Token: 0x04000129 RID: 297
		private byte[] _LobbyMessageData = new byte[4000];

		// Token: 0x0400012A RID: 298
		private byte[] _LobbySendMessageData = new byte[4000];

		// Token: 0x0400012B RID: 299
		private PlatformMessageBase _kickMessage = new PlatformMessageBase();

		// Token: 0x0400012C RID: 300
		protected Callback<LobbyCreated_t> _callbackOnLobbyCreated;

		// Token: 0x0400012D RID: 301
		protected Callback<LobbyDataUpdate_t> _callbackOnLobbyDataUpdated;

		// Token: 0x0400012E RID: 302
		protected Callback<LobbyEnter_t> _callbackOnLobbyEntered;

		// Token: 0x0400012F RID: 303
		protected Callback<LobbyChatUpdate_t> _callbackOnLobbyChatUpdate;

		// Token: 0x04000130 RID: 304
		protected Callback<PersonaStateChange_t> _callbackOnRequestUserInformation;

		// Token: 0x04000131 RID: 305
		protected Callback<LobbyChatMsg_t> _callbackOnLobbyChatUpdated;

		// Token: 0x04000132 RID: 306
		private CSteamID _WantsLobbyID = CSteamID.Nil;

		// Token: 0x04000133 RID: 307
		private Action _WantsNewLobby;

		// Token: 0x04000134 RID: 308
		private bool _leavingLobby;

		// Token: 0x02000046 RID: 70
		private struct steamLobbyMemeberInformation
		{
			// Token: 0x04000135 RID: 309
			public CSteamID userID;

			// Token: 0x04000136 RID: 310
			public string userName;

			// Token: 0x04000137 RID: 311
			public Texture2D userImage;
		}
	}
}
