using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Server.Part
{
	// Token: 0x020000A9 RID: 169
	public class SteamConnections<ClientConnectionInfo>
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0001A64C File Offset: 0x00018A4C
		public SteamConnections(SteamServer<ClientConnectionInfo> master)
		{
			this._Master = master;
			this.ConnectedUsers = new List<SteamConnections<ClientConnectionInfo>.SteamConnectedUser>();
			this._callbackOnConnectAndAuthenticateResultPass = Callback<GSClientApprove_t>.CreateGameServer(new Callback<GSClientApprove_t>.DispatchDelegate(this.OnConnectAndAuthenticateResultPass));
			this._callbackOnConnectAndAuthenticateResultFail = Callback<GSClientDeny_t>.CreateGameServer(new Callback<GSClientDeny_t>.DispatchDelegate(this.OnConnectAndAuthenticateResultFail));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001A6AC File Offset: 0x00018AAC
		public bool IsConnectedUser(BaseUserInfo user)
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					if (this.ConnectedUsers[i].baseInfo == user)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001A728 File Offset: 0x00018B28
		public int GetConnection(ClientConnectionInfo connection)
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			int result;
			lock (lockConnections)
			{
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					if (this.ConnectedUsers[i].connection.Equals(connection))
					{
						return i;
					}
				}
				result = -1;
			}
			return result;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001A7B0 File Offset: 0x00018BB0
		public bool AddPlayer(BaseUserInfo user, ClientConnectionInfo connection)
		{
			bool result = false;
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				int connection2 = this.GetConnection(connection);
				if (connection2 < 0)
				{
					SteamConnections<ClientConnectionInfo>.SteamConnectedUser item = default(SteamConnections<ClientConnectionInfo>.SteamConnectedUser);
					item.connection = connection;
					item.guestsID = new List<CSteamID>();
					item.baseInfo = user;
					if (user.platformKey == "steam")
					{
						item.steamID = (CSteamID)user.userID;
						item.authenticated = authenticationState.WAITING;
						item.authenticationTimer = 20f;
						result = true;
						if (SteamAPICall_t.Invalid == SteamGameServer.ComputeNewPlayerCompatibility(item.steamID))
						{
							item.authenticated = authenticationState.FAILED_COMPATIBILITY;
							item.authenticationTimer = 1f;
							Debug.LogError("[Steamworks-Server] Failed compatibility on " + item.baseInfo.ToString() + ".");
							result = false;
						}
						else
						{
							Debug.LogError("[Steamworks-Server] Authentication " + item.baseInfo.ToString() + " started.");
						}
					}
					else
					{
						item.steamID = SteamGameServer.CreateUnauthenticatedUserConnection();
						item.authenticated = authenticationState.PASSED;
					}
					this.ConnectedUsers.Add(item);
				}
				else
				{
					SteamConnections<ClientConnectionInfo>.SteamConnectedUser value = this.ConnectedUsers[connection2];
					value.guestsID.Add(SteamGameServer.CreateUnauthenticatedUserConnection());
					this.ConnectedUsers[connection2] = value;
				}
			}
			return result;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001A940 File Offset: 0x00018D40
		public void RemovePlayer(ClientConnectionInfo connection)
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				int connection2 = this.GetConnection(connection);
				if (connection2 >= 0)
				{
					if (this.ConnectedUsers[connection2].guestsID.Count == 0)
					{
						if (this.ConnectedUsers[connection2].steamID != CSteamID.Nil)
						{
							SteamGameServer.SendUserDisconnect(this.ConnectedUsers[connection2].steamID);
						}
						Debug.LogError("[Steamworks-Server] User left " + this.ConnectedUsers[connection2].baseInfo.ToString() + ".");
						this.ConnectedUsers.RemoveAt(connection2);
					}
					else
					{
						Debug.LogError(string.Concat(new object[]
						{
							"[Steamworks-Server] User left ",
							this.ConnectedUsers[connection2].baseInfo.ToString(),
							" [",
							this.ConnectedUsers[connection2].guestsID.Count,
							"]."
						}));
						if (this.ConnectedUsers[connection2].guestsID[this.ConnectedUsers[connection2].guestsID.Count - 1] != CSteamID.Nil)
						{
							SteamGameServer.SendUserDisconnect(this.ConnectedUsers[connection2].guestsID[this.ConnectedUsers[connection2].guestsID.Count - 1]);
							this.ConnectedUsers[connection2].guestsID.RemoveAt(this.ConnectedUsers[connection2].guestsID.Count - 1);
						}
					}
				}
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001AB54 File Offset: 0x00018F54
		public bool RemoveConnection(ClientConnectionInfo lostConnection)
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				int connection = this.GetConnection(lostConnection);
				if (connection >= 0)
				{
					for (int i = 0; i < this.ConnectedUsers[connection].guestsID.Count; i++)
					{
						this.RemovePlayer(lostConnection);
					}
					this.RemovePlayer(lostConnection);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001ABDC File Offset: 0x00018FDC
		public bool ConnectAndAuthenticate(ulong userID, uint userIP, byte[] ticket, uint ticketSize)
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			bool result;
			lock (lockConnections)
			{
				CSteamID that = (CSteamID)userID;
				bool flag = SteamGameServer.SendUserConnectAndAuthenticate(userIP, ticket, ticketSize, out that);
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					SteamConnections<ClientConnectionInfo>.SteamConnectedUser value = this.ConnectedUsers[i];
					if (value.baseInfo.userID == userID && value.baseInfo.platformKey == "steam")
					{
						if (flag && value.baseInfo.userID == (ulong)that)
						{
							value.authenticationTimer = 20f;
							Debug.LogError("[Steamworks-Server] Authentication part 1 " + value.baseInfo.ToString() + " passed.");
							value.connectionResult = authenticationConnectionFirstResult.OK;
							value.authenticationTimer = 20f;
							value.authenticated = authenticationState.PASSED;
						}
						else
						{
							value.connectionResult = ((value.baseInfo.userID != (ulong)that) ? authenticationConnectionFirstResult.NOT_USER : authenticationConnectionFirstResult.TICKET);
							value.authenticated = authenticationState.FAILED_CONNECTION_FIRST;
							value.authenticationTimer = Mathf.Min(1f, value.authenticationTimer);
							Debug.LogError(string.Concat(new string[]
							{
								"[Steamworks-Server] Authentication connection part 1 ",
								value.baseInfo.ToString(),
								" IP: ",
								userIP.ToString(),
								" result:",
								that.ToString(),
								" failed."
							}));
						}
					}
					this.ConnectedUsers[i] = value;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001ADB8 File Offset: 0x000191B8
		protected void OnConnectAndAuthenticateResultPass(GSClientApprove_t message)
		{
			Debug.LogError("[Steamworks-Server] Authentication pass received for " + message.m_SteamID.ToString() + ".");
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					SteamConnections<ClientConnectionInfo>.SteamConnectedUser value = this.ConnectedUsers[i];
					if (value.baseInfo.userID == (ulong)message.m_SteamID && value.baseInfo.platformKey == "steam")
					{
						value.authenticationTimer = 20f;
						value.authenticated = authenticationState.PASSED;
						Debug.LogError("[Steamworks-Server] Authentication connection part 2 " + value.baseInfo.ToString() + " passed. Authentication Completed.");
						if (!SteamGameServer.BUpdateUserData(value.steamID, "[" + value.baseInfo.platformKey + "] " + value.baseInfo.userName, 0U))
						{
							Debug.LogError("[Steamworks-Server] Failed to Add " + value.baseInfo.ToString() + " data.");
							value.authenticated = authenticationState.FAILED_USERUPDATE;
							value.authenticationTimer = 1f;
						}
					}
					this.ConnectedUsers[i] = value;
				}
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001AF3C File Offset: 0x0001933C
		protected void OnConnectAndAuthenticateResultFail(GSClientDeny_t message)
		{
			Debug.LogError("[Steamworks-Server] Authentication fail received for " + message.m_SteamID.ToString() + ".");
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					SteamConnections<ClientConnectionInfo>.SteamConnectedUser value = this.ConnectedUsers[i];
					if (value.baseInfo.userID == (ulong)message.m_SteamID && value.baseInfo.platformKey == "steam")
					{
						value.authenticationTimer = 1f;
						value.authenticated = authenticationState.FAILED_CONNECTION_SECOND;
						value.denyReasonResult = message.m_eDenyReason;
						Debug.LogError("[Steamworks-Server] Authentication connection part 2 " + value.baseInfo.ToString() + " failed.");
					}
					this.ConnectedUsers[i] = value;
				}
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001B050 File Offset: 0x00019450
		public void InforceAuthentication()
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					if (this.ConnectedUsers[i].authenticated != authenticationState.PASSED)
					{
						SteamConnections<ClientConnectionInfo>.SteamConnectedUser value = this.ConnectedUsers[i];
						if (this._Master.CanDisconnectUser(value.connection))
						{
							value.authenticationTimer -= Time.unscaledDeltaTime;
							if (value.authenticationTimer < 0f)
							{
								switch (this.ConnectedUsers[i].authenticated)
								{
								case authenticationState.FAILED_MULTI:
									this._Master.DisconnectUser(this.ConnectedUsers[i].connection, "Multiple connections on user.");
									Debug.LogError("[Steamworks-Server] User " + value.baseInfo.ToString() + " Authentication FAILED_MULTI.");
									break;
								case authenticationState.WAITING:
									this._Master.DisconnectUser(value.connection, "Authentication timeout");
									Debug.LogError("[Steamworks-Server] User " + value.baseInfo.ToString() + " Authentication time out.");
									break;
								case authenticationState.FAILED_TICKET_FIRST:
									this._Master.DisconnectUser(this.ConnectedUsers[i].connection, this.DisconnectText(this.ConnectedUsers[i].authenticatedBeginResult));
									Debug.LogError(string.Concat(new string[]
									{
										"[Steamworks-Server] User ",
										value.baseInfo.ToString(),
										" Authentication FAILED_TICKET_FIRST (",
										this.ConnectedUsers[i].authenticatedBeginResult.ToString(),
										")."
									}));
									break;
								case authenticationState.FAILED_TICKET_SECOND:
									this._Master.DisconnectUser(this.ConnectedUsers[i].connection, this.DisconnectText(this.ConnectedUsers[i].authenticatedResult));
									Debug.LogError(string.Concat(new string[]
									{
										"[Steamworks-Server] User ",
										value.baseInfo.ToString(),
										" Authentication FAILED_TICKET_SECOND (",
										this.ConnectedUsers[i].authenticatedResult.ToString(),
										")."
									}));
									break;
								case authenticationState.FAILED_USERUPDATE:
									this._Master.DisconnectUser(this.ConnectedUsers[i].connection, "Failed to update user data.");
									Debug.LogError("[Steamworks-Server] User " + value.baseInfo.ToString() + " Authentication FAILED_USERUPDATE.");
									break;
								case authenticationState.PASSED:
									goto IL_48D;
								case authenticationState.FAILED_COMPATIBILITY:
									this._Master.DisconnectUser(value.connection, "None compatible user detected.");
									Debug.LogError("[Steamworks-Server] User " + value.baseInfo.ToString() + " FAILED_COMPATIBILITY.");
									break;
								case authenticationState.FAILED_CONNECTION_FIRST:
									this._Master.DisconnectUser(this.ConnectedUsers[i].connection, this.DisconnectText(this.ConnectedUsers[i].connectionResult));
									Debug.LogError(string.Concat(new string[]
									{
										"[Steamworks-Server] User ",
										value.baseInfo.ToString(),
										" Authentication FAILED_CONNECTION_FIRST (",
										this.ConnectedUsers[i].connectionResult.ToString(),
										")."
									}));
									break;
								case authenticationState.FAILED_CONNECTION_SECOND:
									this._Master.DisconnectUser(this.ConnectedUsers[i].connection, this.DisconnectText(this.ConnectedUsers[i].denyReasonResult));
									Debug.LogError(string.Concat(new string[]
									{
										"[Steamworks-Server] User ",
										value.baseInfo.ToString(),
										" Authentication FAILED_CONNECTION_SECOND (",
										this.ConnectedUsers[i].denyReasonResult.ToString(),
										")."
									}));
									break;
								default:
									goto IL_48D;
								}
								IL_4D4:
								if (this.RemoveConnection(this.ConnectedUsers[i].connection))
								{
									i--;
								}
								goto IL_50A;
								IL_48D:
								this._Master.DisconnectUser(value.connection, "Unkown connection problem");
								Debug.LogError("[Steamworks-Server] User " + value.baseInfo.ToString() + " Unkown connection problem.");
								goto IL_4D4;
							}
							this.ConnectedUsers[i] = value;
							IL_50A:;
						}
						else
						{
							value.authenticationTimer = Mathf.Min(value.authenticationTimer, 5f);
							this.ConnectedUsers[i] = value;
						}
					}
				}
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001B5D0 File Offset: 0x000199D0
		public string DisconnectText(EAuthSessionResponse result)
		{
			switch (result)
			{
			case EAuthSessionResponse.k_EAuthSessionResponseUserNotConnectedToSteam:
				return "You're not connected to steam";
			case EAuthSessionResponse.k_EAuthSessionResponseNoLicenseOrExpired:
				return "You have no license or it has expired";
			case EAuthSessionResponse.k_EAuthSessionResponseVACBanned:
				return "You're VAC banned";
			case EAuthSessionResponse.k_EAuthSessionResponseLoggedInElseWhere:
				return "You're logged on else where";
			case EAuthSessionResponse.k_EAuthSessionResponseVACCheckTimedOut:
				return "Failed to check VAC in permitted time";
			case EAuthSessionResponse.k_EAuthSessionResponseAuthTicketCanceled:
				return "Your ticket was canceled";
			case EAuthSessionResponse.k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed:
				return "Ticket already used";
			case EAuthSessionResponse.k_EAuthSessionResponseAuthTicketInvalid:
				return "Invalid ticket";
			case EAuthSessionResponse.k_EAuthSessionResponsePublisherIssuedBan:
				return "You have been banned by the publisher";
			default:
				return "Unkown authentication error";
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001B64C File Offset: 0x00019A4C
		public string DisconnectText(EBeginAuthSessionResult result)
		{
			switch (result)
			{
			case EBeginAuthSessionResult.k_EBeginAuthSessionResultInvalidTicket:
				return "Invalid ticket";
			case EBeginAuthSessionResult.k_EBeginAuthSessionResultDuplicateRequest:
				return "Duplicate request";
			case EBeginAuthSessionResult.k_EBeginAuthSessionResultInvalidVersion:
				return "Invalid steam version";
			case EBeginAuthSessionResult.k_EBeginAuthSessionResultGameMismatch:
				return "Game mismatch";
			case EBeginAuthSessionResult.k_EBeginAuthSessionResultExpiredTicket:
				return "Expired ticket";
			default:
				return "Unkown authentication error";
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001B6A0 File Offset: 0x00019AA0
		public string DisconnectText(EDenyReason result)
		{
			switch (result)
			{
			case EDenyReason.k_EDenyInvalid:
				return "Access Denied. Invalid connection";
			case EDenyReason.k_EDenyInvalidVersion:
				return "Access Denied. Invalid version";
			case EDenyReason.k_EDenyGeneric:
				return "Access Denied.";
			case EDenyReason.k_EDenyNotLoggedOn:
				return "Access Denied. Not logged on";
			case EDenyReason.k_EDenyNoLicense:
				return "Access Denied. No license found";
			case EDenyReason.k_EDenyCheater:
				return "Access Denied. Steam Reported Cheat.";
			case EDenyReason.k_EDenyLoggedInElseWhere:
				return "Access Denied. Logged in else where";
			case EDenyReason.k_EDenyIncompatibleAnticheat:
				return "Access Denied. Incompatible anticheat";
			case EDenyReason.k_EDenyMemoryCorruption:
				return "Access Denied. Memory corruption";
			case EDenyReason.k_EDenyIncompatibleSoftware:
				return "Access Denied. Incompatible software";
			case EDenyReason.k_EDenySteamConnectionLost:
				return "Access Denied. Steam connection lost";
			case EDenyReason.k_EDenySteamConnectionError:
				return "Access Denied. Steam connection error";
			case EDenyReason.k_EDenySteamResponseTimedOut:
			case EDenyReason.k_EDenySteamValidationStalled:
				return "Access Denied. Validation timeout";
			case EDenyReason.k_EDenySteamOwnerLeftGuestUser:
				return "Access Denied. Steam owner left";
			}
			return "Access Denied. Unkown connection error";
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001B751 File Offset: 0x00019B51
		public string DisconnectText(authenticationConnectionFirstResult result)
		{
			if (result == authenticationConnectionFirstResult.NOT_USER)
			{
				return "Connection Invalid. Failed User Check.";
			}
			if (result != authenticationConnectionFirstResult.TICKET)
			{
				return "Connection Invalid. Unkown connection error";
			}
			return "Connection Invalid. Invalid ticket.";
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001B778 File Offset: 0x00019B78
		public void SetAuthenticatedResult(ulong userID, EAuthSessionResponse why)
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					SteamConnections<ClientConnectionInfo>.SteamConnectedUser value = this.ConnectedUsers[i];
					if (value.baseInfo.userID == userID && value.baseInfo.platformKey == "steam")
					{
						value.authenticatedResult = why;
						if (value.authenticatedResult == EAuthSessionResponse.k_EAuthSessionResponseOK)
						{
							if (value.authenticated != authenticationState.PASSED)
							{
								value.authenticated = authenticationState.PASSED;
								Debug.LogError("[Steamworks-Server] Authentication part 2 " + value.baseInfo.ToString() + " passed.");
							}
						}
						else
						{
							value.authenticated = authenticationState.FAILED_TICKET_SECOND;
							value.authenticationTimer = Mathf.Min(1f, value.authenticationTimer);
							Debug.LogError("[Steamworks-Server] Authentication part 2 " + value.baseInfo.ToString() + " failed.");
						}
					}
					this.ConnectedUsers[i] = value;
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001B8B8 File Offset: 0x00019CB8
		public void SetAuthenticatedResult(ulong userID, EBeginAuthSessionResult why)
		{
			object lockConnections = SteamConnections<ClientConnectionInfo>._lockConnections;
			lock (lockConnections)
			{
				for (int i = 0; i < this.ConnectedUsers.Count; i++)
				{
					SteamConnections<ClientConnectionInfo>.SteamConnectedUser value = this.ConnectedUsers[i];
					if (value.baseInfo.userID == userID && value.baseInfo.platformKey == "steam")
					{
						value.authenticatedBeginResult = why;
						if (value.authenticatedBeginResult == EBeginAuthSessionResult.k_EBeginAuthSessionResultOK)
						{
							value.authenticationTimer = 20f;
							Debug.LogError("[Steamworks-Server] Authentication part 1 " + value.baseInfo.ToString() + " passed.");
						}
						else
						{
							value.authenticated = authenticationState.FAILED_TICKET_FIRST;
							value.authenticationTimer = Mathf.Min(1f, value.authenticationTimer);
							Debug.LogError("[Steamworks-Server] Authentication part 1 " + value.baseInfo.ToString() + " failed.");
						}
					}
					this.ConnectedUsers[i] = value;
				}
			}
		}

		// Token: 0x040002F6 RID: 758
		private SteamServer<ClientConnectionInfo> _Master;

		// Token: 0x040002F7 RID: 759
		private static object _lockConnections = new object();

		// Token: 0x040002F8 RID: 760
		public const float AUTHENTICATION_TIMER = 20f;

		// Token: 0x040002F9 RID: 761
		public const float AUTHENTICATION_FAIL_TIMER = 1f;

		// Token: 0x040002FA RID: 762
		public const float AUTHENTICATION_MIN_TIMER = 5f;

		// Token: 0x040002FB RID: 763
		public List<SteamConnections<ClientConnectionInfo>.SteamConnectedUser> ConnectedUsers = new List<SteamConnections<ClientConnectionInfo>.SteamConnectedUser>();

		// Token: 0x040002FC RID: 764
		protected Callback<GSClientApprove_t> _callbackOnConnectAndAuthenticateResultPass;

		// Token: 0x040002FD RID: 765
		protected Callback<GSClientDeny_t> _callbackOnConnectAndAuthenticateResultFail;

		// Token: 0x020000AA RID: 170
		public struct SteamConnectedUser
		{
			// Token: 0x040002FE RID: 766
			public BaseUserInfo baseInfo;

			// Token: 0x040002FF RID: 767
			public CSteamID steamID;

			// Token: 0x04000300 RID: 768
			public ClientConnectionInfo connection;

			// Token: 0x04000301 RID: 769
			public authenticationState authenticated;

			// Token: 0x04000302 RID: 770
			public EBeginAuthSessionResult authenticatedBeginResult;

			// Token: 0x04000303 RID: 771
			public EAuthSessionResponse authenticatedResult;

			// Token: 0x04000304 RID: 772
			public EDenyReason denyReasonResult;

			// Token: 0x04000305 RID: 773
			public authenticationConnectionFirstResult connectionResult;

			// Token: 0x04000306 RID: 774
			public float authenticationTimer;

			// Token: 0x04000307 RID: 775
			public List<CSteamID> guestsID;
		}
	}
}
