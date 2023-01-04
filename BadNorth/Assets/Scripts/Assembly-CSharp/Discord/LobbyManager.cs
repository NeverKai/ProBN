using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Discord
{
	// Token: 0x02000148 RID: 328
	public class LobbyManager
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0001DB8C File Offset: 0x0001BF8C
		internal LobbyManager(IntPtr ptr, IntPtr eventsPtr, ref LobbyManager.FFIEvents events)
		{
			if (eventsPtr == IntPtr.Zero)
			{
				throw new ResultException(Result.InternalError);
			}
			this.InitEvents(eventsPtr, ref events);
			this.MethodsPtr = ptr;
			if (this.MethodsPtr == IntPtr.Zero)
			{
				throw new ResultException(Result.InternalError);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001DBE1 File Offset: 0x0001BFE1
		private LobbyManager.FFIMethods Methods
		{
			get
			{
				if (this.MethodsStructure == null)
				{
					this.MethodsStructure = Marshal.PtrToStructure(this.MethodsPtr, typeof(LobbyManager.FFIMethods));
				}
				return (LobbyManager.FFIMethods)this.MethodsStructure;
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600082B RID: 2091 RVA: 0x0001DC14 File Offset: 0x0001C014
		// (remove) Token: 0x0600082C RID: 2092 RVA: 0x0001DC4C File Offset: 0x0001C04C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.LobbyUpdateHandler OnLobbyUpdate;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600082D RID: 2093 RVA: 0x0001DC84 File Offset: 0x0001C084
		// (remove) Token: 0x0600082E RID: 2094 RVA: 0x0001DCBC File Offset: 0x0001C0BC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.LobbyDeleteHandler OnLobbyDelete;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x0600082F RID: 2095 RVA: 0x0001DCF4 File Offset: 0x0001C0F4
		// (remove) Token: 0x06000830 RID: 2096 RVA: 0x0001DD2C File Offset: 0x0001C12C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.MemberConnectHandler OnMemberConnect;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000831 RID: 2097 RVA: 0x0001DD64 File Offset: 0x0001C164
		// (remove) Token: 0x06000832 RID: 2098 RVA: 0x0001DD9C File Offset: 0x0001C19C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.MemberUpdateHandler OnMemberUpdate;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000833 RID: 2099 RVA: 0x0001DDD4 File Offset: 0x0001C1D4
		// (remove) Token: 0x06000834 RID: 2100 RVA: 0x0001DE0C File Offset: 0x0001C20C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.MemberDisconnectHandler OnMemberDisconnect;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000835 RID: 2101 RVA: 0x0001DE44 File Offset: 0x0001C244
		// (remove) Token: 0x06000836 RID: 2102 RVA: 0x0001DE7C File Offset: 0x0001C27C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.LobbyMessageHandler OnLobbyMessage;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000837 RID: 2103 RVA: 0x0001DEB4 File Offset: 0x0001C2B4
		// (remove) Token: 0x06000838 RID: 2104 RVA: 0x0001DEEC File Offset: 0x0001C2EC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.SpeakingHandler OnSpeaking;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000839 RID: 2105 RVA: 0x0001DF24 File Offset: 0x0001C324
		// (remove) Token: 0x0600083A RID: 2106 RVA: 0x0001DF5C File Offset: 0x0001C35C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LobbyManager.NetworkMessageHandler OnNetworkMessage;

		// Token: 0x0600083B RID: 2107 RVA: 0x0001DF94 File Offset: 0x0001C394
		private void InitEvents(IntPtr eventsPtr, ref LobbyManager.FFIEvents events)
		{
			events.OnLobbyUpdate = delegate(IntPtr ptr, long lobbyId)
			{
				if (this.OnLobbyUpdate != null)
				{
					this.OnLobbyUpdate(lobbyId);
				}
			};
			events.OnLobbyDelete = delegate(IntPtr ptr, long lobbyId, uint reason)
			{
				if (this.OnLobbyDelete != null)
				{
					this.OnLobbyDelete(lobbyId, reason);
				}
			};
			events.OnMemberConnect = delegate(IntPtr ptr, long lobbyId, long userId)
			{
				if (this.OnMemberConnect != null)
				{
					this.OnMemberConnect(lobbyId, userId);
				}
			};
			events.OnMemberUpdate = delegate(IntPtr ptr, long lobbyId, long userId)
			{
				if (this.OnMemberUpdate != null)
				{
					this.OnMemberUpdate(lobbyId, userId);
				}
			};
			events.OnMemberDisconnect = delegate(IntPtr ptr, long lobbyId, long userId)
			{
				if (this.OnMemberDisconnect != null)
				{
					this.OnMemberDisconnect(lobbyId, userId);
				}
			};
			events.OnLobbyMessage = delegate(IntPtr ptr, long lobbyId, long userId, IntPtr dataPtr, int dataLen)
			{
				if (this.OnLobbyMessage != null)
				{
					byte[] array = new byte[dataLen];
					Marshal.Copy(dataPtr, array, 0, dataLen);
					this.OnLobbyMessage(lobbyId, userId, array);
				}
			};
			events.OnSpeaking = delegate(IntPtr ptr, long lobbyId, long userId, bool speaking)
			{
				if (this.OnSpeaking != null)
				{
					this.OnSpeaking(lobbyId, userId, speaking);
				}
			};
			events.OnNetworkMessage = delegate(IntPtr ptr, long lobbyId, long userId, byte channelId, IntPtr dataPtr, int dataLen)
			{
				if (this.OnNetworkMessage != null)
				{
					byte[] array = new byte[dataLen];
					Marshal.Copy(dataPtr, array, 0, dataLen);
					this.OnNetworkMessage(lobbyId, userId, channelId, array);
				}
			};
			Marshal.StructureToPtr(events, eventsPtr, false);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001E044 File Offset: 0x0001C444
		public LobbyTransaction GetLobbyCreateTransaction()
		{
			LobbyTransaction result = default(LobbyTransaction);
			Result result2 = this.Methods.GetLobbyCreateTransaction(this.MethodsPtr, ref result.MethodsPtr);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001E088 File Offset: 0x0001C488
		public LobbyTransaction GetLobbyUpdateTransaction(long lobbyId)
		{
			LobbyTransaction result = default(LobbyTransaction);
			Result result2 = this.Methods.GetLobbyUpdateTransaction(this.MethodsPtr, lobbyId, ref result.MethodsPtr);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001E0D0 File Offset: 0x0001C4D0
		public LobbyMemberTransaction GetMemberUpdateTransaction(long lobbyId, long userId)
		{
			LobbyMemberTransaction result = default(LobbyMemberTransaction);
			Result result2 = this.Methods.GetMemberUpdateTransaction(this.MethodsPtr, lobbyId, userId, ref result.MethodsPtr);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001E118 File Offset: 0x0001C518
		public void CreateLobby(LobbyTransaction transaction, LobbyManager.CreateLobbyHandler callback)
		{
			LobbyManager.FFIMethods.CreateLobbyCallback createLobbyCallback = delegate(IntPtr ptr, Result result, ref Lobby lobby)
			{
				Utility.Release(ptr);
				callback(result, ref lobby);
			};
			this.Methods.CreateLobby(this.MethodsPtr, transaction.MethodsPtr, Utility.Retain<LobbyManager.FFIMethods.CreateLobbyCallback>(createLobbyCallback), createLobbyCallback);
			transaction.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001E174 File Offset: 0x0001C574
		public void UpdateLobby(long lobbyId, LobbyTransaction transaction, LobbyManager.UpdateLobbyHandler callback)
		{
			LobbyManager.FFIMethods.UpdateLobbyCallback updateLobbyCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.UpdateLobby(this.MethodsPtr, lobbyId, transaction.MethodsPtr, Utility.Retain<LobbyManager.FFIMethods.UpdateLobbyCallback>(updateLobbyCallback), updateLobbyCallback);
			transaction.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001E1D0 File Offset: 0x0001C5D0
		public void DeleteLobby(long lobbyId, LobbyManager.DeleteLobbyHandler callback)
		{
			LobbyManager.FFIMethods.DeleteLobbyCallback deleteLobbyCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.DeleteLobby(this.MethodsPtr, lobbyId, Utility.Retain<LobbyManager.FFIMethods.DeleteLobbyCallback>(deleteLobbyCallback), deleteLobbyCallback);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001E218 File Offset: 0x0001C618
		public void ConnectLobby(long lobbyId, string secret, LobbyManager.ConnectLobbyHandler callback)
		{
			LobbyManager.FFIMethods.ConnectLobbyCallback connectLobbyCallback = delegate(IntPtr ptr, Result result, ref Lobby lobby)
			{
				Utility.Release(ptr);
				callback(result, ref lobby);
			};
			this.Methods.ConnectLobby(this.MethodsPtr, lobbyId, secret, Utility.Retain<LobbyManager.FFIMethods.ConnectLobbyCallback>(connectLobbyCallback), connectLobbyCallback);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001E264 File Offset: 0x0001C664
		public void ConnectLobbyWithActivitySecret(string activitySecret, LobbyManager.ConnectLobbyWithActivitySecretHandler callback)
		{
			LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretCallback connectLobbyWithActivitySecretCallback = delegate(IntPtr ptr, Result result, ref Lobby lobby)
			{
				Utility.Release(ptr);
				callback(result, ref lobby);
			};
			this.Methods.ConnectLobbyWithActivitySecret(this.MethodsPtr, activitySecret, Utility.Retain<LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretCallback>(connectLobbyWithActivitySecretCallback), connectLobbyWithActivitySecretCallback);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001E2AC File Offset: 0x0001C6AC
		public void DisconnectLobby(long lobbyId, LobbyManager.DisconnectLobbyHandler callback)
		{
			LobbyManager.FFIMethods.DisconnectLobbyCallback disconnectLobbyCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.DisconnectLobby(this.MethodsPtr, lobbyId, Utility.Retain<LobbyManager.FFIMethods.DisconnectLobbyCallback>(disconnectLobbyCallback), disconnectLobbyCallback);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001E2F4 File Offset: 0x0001C6F4
		public Lobby GetLobby(long lobbyId)
		{
			Lobby result = default(Lobby);
			Result result2 = this.Methods.GetLobby(this.MethodsPtr, lobbyId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001E334 File Offset: 0x0001C734
		public string GetLobbyActivitySecret(long lobbyId)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			Result result = this.Methods.GetLobbyActivitySecret(this.MethodsPtr, lobbyId, stringBuilder);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001E37C File Offset: 0x0001C77C
		public string GetLobbyMetadataValue(long lobbyId, string key)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			Result result = this.Methods.GetLobbyMetadataValue(this.MethodsPtr, lobbyId, key, stringBuilder);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001E3C4 File Offset: 0x0001C7C4
		public string GetLobbyMetadataKey(long lobbyId, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			Result result = this.Methods.GetLobbyMetadataKey(this.MethodsPtr, lobbyId, index, stringBuilder);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001E40C File Offset: 0x0001C80C
		public int LobbyMetadataCount(long lobbyId)
		{
			int result = 0;
			Result result2 = this.Methods.LobbyMetadataCount(this.MethodsPtr, lobbyId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001E448 File Offset: 0x0001C848
		public int MemberCount(long lobbyId)
		{
			int result = 0;
			Result result2 = this.Methods.MemberCount(this.MethodsPtr, lobbyId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001E484 File Offset: 0x0001C884
		public long GetMemberUserId(long lobbyId, int index)
		{
			long result = 0L;
			Result result2 = this.Methods.GetMemberUserId(this.MethodsPtr, lobbyId, index, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001E4C0 File Offset: 0x0001C8C0
		public User GetMemberUser(long lobbyId, long userId)
		{
			User result = default(User);
			Result result2 = this.Methods.GetMemberUser(this.MethodsPtr, lobbyId, userId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001E504 File Offset: 0x0001C904
		public string GetMemberMetadataValue(long lobbyId, long userId, string key)
		{
			StringBuilder stringBuilder = new StringBuilder(4096);
			Result result = this.Methods.GetMemberMetadataValue(this.MethodsPtr, lobbyId, userId, key, stringBuilder);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001E550 File Offset: 0x0001C950
		public string GetMemberMetadataKey(long lobbyId, long userId, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			Result result = this.Methods.GetMemberMetadataKey(this.MethodsPtr, lobbyId, userId, index, stringBuilder);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001E59C File Offset: 0x0001C99C
		public int MemberMetadataCount(long lobbyId, long userId)
		{
			int result = 0;
			Result result2 = this.Methods.MemberMetadataCount(this.MethodsPtr, lobbyId, userId, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001E5D8 File Offset: 0x0001C9D8
		public void UpdateMember(long lobbyId, long userId, LobbyMemberTransaction transaction, LobbyManager.UpdateMemberHandler callback)
		{
			LobbyManager.FFIMethods.UpdateMemberCallback updateMemberCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.UpdateMember(this.MethodsPtr, lobbyId, userId, transaction.MethodsPtr, Utility.Retain<LobbyManager.FFIMethods.UpdateMemberCallback>(updateMemberCallback), updateMemberCallback);
			transaction.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001E638 File Offset: 0x0001CA38
		public void SendLobbyMessage(long lobbyId, byte[] data, LobbyManager.SendLobbyMessageHandler callback)
		{
			LobbyManager.FFIMethods.SendLobbyMessageCallback sendLobbyMessageCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.SendLobbyMessage(this.MethodsPtr, lobbyId, data, data.Length, Utility.Retain<LobbyManager.FFIMethods.SendLobbyMessageCallback>(sendLobbyMessageCallback), sendLobbyMessageCallback);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001E684 File Offset: 0x0001CA84
		public LobbySearchQuery GetSearchQuery()
		{
			LobbySearchQuery result = default(LobbySearchQuery);
			Result result2 = this.Methods.GetSearchQuery(this.MethodsPtr, ref result.MethodsPtr);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001E6C8 File Offset: 0x0001CAC8
		public void Search(LobbySearchQuery query, LobbyManager.SearchHandler callback)
		{
			LobbyManager.FFIMethods.SearchCallback searchCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.Search(this.MethodsPtr, query.MethodsPtr, Utility.Retain<LobbyManager.FFIMethods.SearchCallback>(searchCallback), searchCallback);
			query.MethodsPtr = IntPtr.Zero;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001E724 File Offset: 0x0001CB24
		public int LobbyCount()
		{
			int result = 0;
			this.Methods.LobbyCount(this.MethodsPtr, ref result);
			return result;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001E750 File Offset: 0x0001CB50
		public long GetLobbyId(int index)
		{
			long result = 0L;
			Result result2 = this.Methods.GetLobbyId(this.MethodsPtr, index, ref result);
			if (result2 != Result.Ok)
			{
				throw new ResultException(result2);
			}
			return result;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001E78C File Offset: 0x0001CB8C
		public void ConnectVoice(long lobbyId, LobbyManager.ConnectVoiceHandler callback)
		{
			LobbyManager.FFIMethods.ConnectVoiceCallback connectVoiceCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.ConnectVoice(this.MethodsPtr, lobbyId, Utility.Retain<LobbyManager.FFIMethods.ConnectVoiceCallback>(connectVoiceCallback), connectVoiceCallback);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001E7D4 File Offset: 0x0001CBD4
		public void DisconnectVoice(long lobbyId, LobbyManager.DisconnectVoiceHandler callback)
		{
			LobbyManager.FFIMethods.DisconnectVoiceCallback disconnectVoiceCallback = delegate(IntPtr ptr, Result result)
			{
				Utility.Release(ptr);
				callback(result);
			};
			this.Methods.DisconnectVoice(this.MethodsPtr, lobbyId, Utility.Retain<LobbyManager.FFIMethods.DisconnectVoiceCallback>(disconnectVoiceCallback), disconnectVoiceCallback);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001E81C File Offset: 0x0001CC1C
		public void ConnectNetwork(long lobbyId)
		{
			Result result = this.Methods.ConnectNetwork(this.MethodsPtr, lobbyId);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001E854 File Offset: 0x0001CC54
		public void DisconnectNetwork(long lobbyId)
		{
			Result result = this.Methods.DisconnectNetwork(this.MethodsPtr, lobbyId);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001E88C File Offset: 0x0001CC8C
		public void FlushNetwork()
		{
			Result result = this.Methods.FlushNetwork(this.MethodsPtr);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001E8C0 File Offset: 0x0001CCC0
		public void OpenNetworkChannel(long lobbyId, byte channelId, bool reliable)
		{
			Result result = this.Methods.OpenNetworkChannel(this.MethodsPtr, lobbyId, channelId, reliable);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001E8F8 File Offset: 0x0001CCF8
		public void SendNetworkMessage(long lobbyId, long userId, byte channelId, byte[] data)
		{
			Result result = this.Methods.SendNetworkMessage(this.MethodsPtr, lobbyId, userId, channelId, data, data.Length);
			if (result != Result.Ok)
			{
				throw new ResultException(result);
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001E938 File Offset: 0x0001CD38
		public IEnumerable<User> GetMemberUsers(long lobbyID)
		{
			int num = this.MemberCount(lobbyID);
			List<User> list = new List<User>();
			for (int i = 0; i < num; i++)
			{
				list.Add(this.GetMemberUser(lobbyID, this.GetMemberUserId(lobbyID, i)));
			}
			return list;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001E97B File Offset: 0x0001CD7B
		public void SendLobbyMessage(long lobbyID, string data, LobbyManager.SendLobbyMessageHandler handler)
		{
			this.SendLobbyMessage(lobbyID, Encoding.UTF8.GetBytes(data), handler);
		}

		// Token: 0x0400046C RID: 1132
		private IntPtr MethodsPtr;

		// Token: 0x0400046D RID: 1133
		private object MethodsStructure;

		// Token: 0x02000149 RID: 329
		internal struct FFIEvents
		{
			// Token: 0x04000476 RID: 1142
			internal LobbyManager.FFIEvents.LobbyUpdateHandler OnLobbyUpdate;

			// Token: 0x04000477 RID: 1143
			internal LobbyManager.FFIEvents.LobbyDeleteHandler OnLobbyDelete;

			// Token: 0x04000478 RID: 1144
			internal LobbyManager.FFIEvents.MemberConnectHandler OnMemberConnect;

			// Token: 0x04000479 RID: 1145
			internal LobbyManager.FFIEvents.MemberUpdateHandler OnMemberUpdate;

			// Token: 0x0400047A RID: 1146
			internal LobbyManager.FFIEvents.MemberDisconnectHandler OnMemberDisconnect;

			// Token: 0x0400047B RID: 1147
			internal LobbyManager.FFIEvents.LobbyMessageHandler OnLobbyMessage;

			// Token: 0x0400047C RID: 1148
			internal LobbyManager.FFIEvents.SpeakingHandler OnSpeaking;

			// Token: 0x0400047D RID: 1149
			internal LobbyManager.FFIEvents.NetworkMessageHandler OnNetworkMessage;

			// Token: 0x0200014A RID: 330
			// (Invoke) Token: 0x06000868 RID: 2152
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void LobbyUpdateHandler(IntPtr ptr, long lobbyId);

			// Token: 0x0200014B RID: 331
			// (Invoke) Token: 0x0600086C RID: 2156
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void LobbyDeleteHandler(IntPtr ptr, long lobbyId, uint reason);

			// Token: 0x0200014C RID: 332
			// (Invoke) Token: 0x06000870 RID: 2160
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void MemberConnectHandler(IntPtr ptr, long lobbyId, long userId);

			// Token: 0x0200014D RID: 333
			// (Invoke) Token: 0x06000874 RID: 2164
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void MemberUpdateHandler(IntPtr ptr, long lobbyId, long userId);

			// Token: 0x0200014E RID: 334
			// (Invoke) Token: 0x06000878 RID: 2168
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void MemberDisconnectHandler(IntPtr ptr, long lobbyId, long userId);

			// Token: 0x0200014F RID: 335
			// (Invoke) Token: 0x0600087C RID: 2172
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void LobbyMessageHandler(IntPtr ptr, long lobbyId, long userId, IntPtr dataPtr, int dataLen);

			// Token: 0x02000150 RID: 336
			// (Invoke) Token: 0x06000880 RID: 2176
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SpeakingHandler(IntPtr ptr, long lobbyId, long userId, bool speaking);

			// Token: 0x02000151 RID: 337
			// (Invoke) Token: 0x06000884 RID: 2180
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void NetworkMessageHandler(IntPtr ptr, long lobbyId, long userId, byte channelId, IntPtr dataPtr, int dataLen);
		}

		// Token: 0x02000152 RID: 338
		internal struct FFIMethods
		{
			// Token: 0x0400047E RID: 1150
			internal LobbyManager.FFIMethods.GetLobbyCreateTransactionMethod GetLobbyCreateTransaction;

			// Token: 0x0400047F RID: 1151
			internal LobbyManager.FFIMethods.GetLobbyUpdateTransactionMethod GetLobbyUpdateTransaction;

			// Token: 0x04000480 RID: 1152
			internal LobbyManager.FFIMethods.GetMemberUpdateTransactionMethod GetMemberUpdateTransaction;

			// Token: 0x04000481 RID: 1153
			internal LobbyManager.FFIMethods.CreateLobbyMethod CreateLobby;

			// Token: 0x04000482 RID: 1154
			internal LobbyManager.FFIMethods.UpdateLobbyMethod UpdateLobby;

			// Token: 0x04000483 RID: 1155
			internal LobbyManager.FFIMethods.DeleteLobbyMethod DeleteLobby;

			// Token: 0x04000484 RID: 1156
			internal LobbyManager.FFIMethods.ConnectLobbyMethod ConnectLobby;

			// Token: 0x04000485 RID: 1157
			internal LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretMethod ConnectLobbyWithActivitySecret;

			// Token: 0x04000486 RID: 1158
			internal LobbyManager.FFIMethods.DisconnectLobbyMethod DisconnectLobby;

			// Token: 0x04000487 RID: 1159
			internal LobbyManager.FFIMethods.GetLobbyMethod GetLobby;

			// Token: 0x04000488 RID: 1160
			internal LobbyManager.FFIMethods.GetLobbyActivitySecretMethod GetLobbyActivitySecret;

			// Token: 0x04000489 RID: 1161
			internal LobbyManager.FFIMethods.GetLobbyMetadataValueMethod GetLobbyMetadataValue;

			// Token: 0x0400048A RID: 1162
			internal LobbyManager.FFIMethods.GetLobbyMetadataKeyMethod GetLobbyMetadataKey;

			// Token: 0x0400048B RID: 1163
			internal LobbyManager.FFIMethods.LobbyMetadataCountMethod LobbyMetadataCount;

			// Token: 0x0400048C RID: 1164
			internal LobbyManager.FFIMethods.MemberCountMethod MemberCount;

			// Token: 0x0400048D RID: 1165
			internal LobbyManager.FFIMethods.GetMemberUserIdMethod GetMemberUserId;

			// Token: 0x0400048E RID: 1166
			internal LobbyManager.FFIMethods.GetMemberUserMethod GetMemberUser;

			// Token: 0x0400048F RID: 1167
			internal LobbyManager.FFIMethods.GetMemberMetadataValueMethod GetMemberMetadataValue;

			// Token: 0x04000490 RID: 1168
			internal LobbyManager.FFIMethods.GetMemberMetadataKeyMethod GetMemberMetadataKey;

			// Token: 0x04000491 RID: 1169
			internal LobbyManager.FFIMethods.MemberMetadataCountMethod MemberMetadataCount;

			// Token: 0x04000492 RID: 1170
			internal LobbyManager.FFIMethods.UpdateMemberMethod UpdateMember;

			// Token: 0x04000493 RID: 1171
			internal LobbyManager.FFIMethods.SendLobbyMessageMethod SendLobbyMessage;

			// Token: 0x04000494 RID: 1172
			internal LobbyManager.FFIMethods.GetSearchQueryMethod GetSearchQuery;

			// Token: 0x04000495 RID: 1173
			internal LobbyManager.FFIMethods.SearchMethod Search;

			// Token: 0x04000496 RID: 1174
			internal LobbyManager.FFIMethods.LobbyCountMethod LobbyCount;

			// Token: 0x04000497 RID: 1175
			internal LobbyManager.FFIMethods.GetLobbyIdMethod GetLobbyId;

			// Token: 0x04000498 RID: 1176
			internal LobbyManager.FFIMethods.ConnectVoiceMethod ConnectVoice;

			// Token: 0x04000499 RID: 1177
			internal LobbyManager.FFIMethods.DisconnectVoiceMethod DisconnectVoice;

			// Token: 0x0400049A RID: 1178
			internal LobbyManager.FFIMethods.ConnectNetworkMethod ConnectNetwork;

			// Token: 0x0400049B RID: 1179
			internal LobbyManager.FFIMethods.DisconnectNetworkMethod DisconnectNetwork;

			// Token: 0x0400049C RID: 1180
			internal LobbyManager.FFIMethods.FlushNetworkMethod FlushNetwork;

			// Token: 0x0400049D RID: 1181
			internal LobbyManager.FFIMethods.OpenNetworkChannelMethod OpenNetworkChannel;

			// Token: 0x0400049E RID: 1182
			internal LobbyManager.FFIMethods.SendNetworkMessageMethod SendNetworkMessage;

			// Token: 0x02000153 RID: 339
			// (Invoke) Token: 0x06000888 RID: 2184
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLobbyCreateTransactionMethod(IntPtr methodsPtr, ref IntPtr transaction);

			// Token: 0x02000154 RID: 340
			// (Invoke) Token: 0x0600088C RID: 2188
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLobbyUpdateTransactionMethod(IntPtr methodsPtr, long lobbyId, ref IntPtr transaction);

			// Token: 0x02000155 RID: 341
			// (Invoke) Token: 0x06000890 RID: 2192
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetMemberUpdateTransactionMethod(IntPtr methodsPtr, long lobbyId, long userId, ref IntPtr transaction);

			// Token: 0x02000156 RID: 342
			// (Invoke) Token: 0x06000894 RID: 2196
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void CreateLobbyCallback(IntPtr ptr, Result result, ref Lobby lobby);

			// Token: 0x02000157 RID: 343
			// (Invoke) Token: 0x06000898 RID: 2200
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void CreateLobbyMethod(IntPtr methodsPtr, IntPtr transaction, IntPtr callbackData, LobbyManager.FFIMethods.CreateLobbyCallback callback);

			// Token: 0x02000158 RID: 344
			// (Invoke) Token: 0x0600089C RID: 2204
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void UpdateLobbyCallback(IntPtr ptr, Result result);

			// Token: 0x02000159 RID: 345
			// (Invoke) Token: 0x060008A0 RID: 2208
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void UpdateLobbyMethod(IntPtr methodsPtr, long lobbyId, IntPtr transaction, IntPtr callbackData, LobbyManager.FFIMethods.UpdateLobbyCallback callback);

			// Token: 0x0200015A RID: 346
			// (Invoke) Token: 0x060008A4 RID: 2212
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void DeleteLobbyCallback(IntPtr ptr, Result result);

			// Token: 0x0200015B RID: 347
			// (Invoke) Token: 0x060008A8 RID: 2216
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void DeleteLobbyMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.DeleteLobbyCallback callback);

			// Token: 0x0200015C RID: 348
			// (Invoke) Token: 0x060008AC RID: 2220
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ConnectLobbyCallback(IntPtr ptr, Result result, ref Lobby lobby);

			// Token: 0x0200015D RID: 349
			// (Invoke) Token: 0x060008B0 RID: 2224
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ConnectLobbyMethod(IntPtr methodsPtr, long lobbyId, [MarshalAs(UnmanagedType.LPStr)] string secret, IntPtr callbackData, LobbyManager.FFIMethods.ConnectLobbyCallback callback);

			// Token: 0x0200015E RID: 350
			// (Invoke) Token: 0x060008B4 RID: 2228
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ConnectLobbyWithActivitySecretCallback(IntPtr ptr, Result result, ref Lobby lobby);

			// Token: 0x0200015F RID: 351
			// (Invoke) Token: 0x060008B8 RID: 2232
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ConnectLobbyWithActivitySecretMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string activitySecret, IntPtr callbackData, LobbyManager.FFIMethods.ConnectLobbyWithActivitySecretCallback callback);

			// Token: 0x02000160 RID: 352
			// (Invoke) Token: 0x060008BC RID: 2236
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void DisconnectLobbyCallback(IntPtr ptr, Result result);

			// Token: 0x02000161 RID: 353
			// (Invoke) Token: 0x060008C0 RID: 2240
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void DisconnectLobbyMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.DisconnectLobbyCallback callback);

			// Token: 0x02000162 RID: 354
			// (Invoke) Token: 0x060008C4 RID: 2244
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLobbyMethod(IntPtr methodsPtr, long lobbyId, ref Lobby lobby);

			// Token: 0x02000163 RID: 355
			// (Invoke) Token: 0x060008C8 RID: 2248
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLobbyActivitySecretMethod(IntPtr methodsPtr, long lobbyId, StringBuilder secret);

			// Token: 0x02000164 RID: 356
			// (Invoke) Token: 0x060008CC RID: 2252
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLobbyMetadataValueMethod(IntPtr methodsPtr, long lobbyId, [MarshalAs(UnmanagedType.LPStr)] string key, StringBuilder value);

			// Token: 0x02000165 RID: 357
			// (Invoke) Token: 0x060008D0 RID: 2256
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLobbyMetadataKeyMethod(IntPtr methodsPtr, long lobbyId, int index, StringBuilder key);

			// Token: 0x02000166 RID: 358
			// (Invoke) Token: 0x060008D4 RID: 2260
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result LobbyMetadataCountMethod(IntPtr methodsPtr, long lobbyId, ref int count);

			// Token: 0x02000167 RID: 359
			// (Invoke) Token: 0x060008D8 RID: 2264
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result MemberCountMethod(IntPtr methodsPtr, long lobbyId, ref int count);

			// Token: 0x02000168 RID: 360
			// (Invoke) Token: 0x060008DC RID: 2268
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetMemberUserIdMethod(IntPtr methodsPtr, long lobbyId, int index, ref long userId);

			// Token: 0x02000169 RID: 361
			// (Invoke) Token: 0x060008E0 RID: 2272
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetMemberUserMethod(IntPtr methodsPtr, long lobbyId, long userId, ref User user);

			// Token: 0x0200016A RID: 362
			// (Invoke) Token: 0x060008E4 RID: 2276
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetMemberMetadataValueMethod(IntPtr methodsPtr, long lobbyId, long userId, [MarshalAs(UnmanagedType.LPStr)] string key, StringBuilder value);

			// Token: 0x0200016B RID: 363
			// (Invoke) Token: 0x060008E8 RID: 2280
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetMemberMetadataKeyMethod(IntPtr methodsPtr, long lobbyId, long userId, int index, StringBuilder key);

			// Token: 0x0200016C RID: 364
			// (Invoke) Token: 0x060008EC RID: 2284
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result MemberMetadataCountMethod(IntPtr methodsPtr, long lobbyId, long userId, ref int count);

			// Token: 0x0200016D RID: 365
			// (Invoke) Token: 0x060008F0 RID: 2288
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void UpdateMemberCallback(IntPtr ptr, Result result);

			// Token: 0x0200016E RID: 366
			// (Invoke) Token: 0x060008F4 RID: 2292
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void UpdateMemberMethod(IntPtr methodsPtr, long lobbyId, long userId, IntPtr transaction, IntPtr callbackData, LobbyManager.FFIMethods.UpdateMemberCallback callback);

			// Token: 0x0200016F RID: 367
			// (Invoke) Token: 0x060008F8 RID: 2296
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SendLobbyMessageCallback(IntPtr ptr, Result result);

			// Token: 0x02000170 RID: 368
			// (Invoke) Token: 0x060008FC RID: 2300
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SendLobbyMessageMethod(IntPtr methodsPtr, long lobbyId, byte[] data, int dataLen, IntPtr callbackData, LobbyManager.FFIMethods.SendLobbyMessageCallback callback);

			// Token: 0x02000171 RID: 369
			// (Invoke) Token: 0x06000900 RID: 2304
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetSearchQueryMethod(IntPtr methodsPtr, ref IntPtr query);

			// Token: 0x02000172 RID: 370
			// (Invoke) Token: 0x06000904 RID: 2308
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SearchCallback(IntPtr ptr, Result result);

			// Token: 0x02000173 RID: 371
			// (Invoke) Token: 0x06000908 RID: 2312
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void SearchMethod(IntPtr methodsPtr, IntPtr query, IntPtr callbackData, LobbyManager.FFIMethods.SearchCallback callback);

			// Token: 0x02000174 RID: 372
			// (Invoke) Token: 0x0600090C RID: 2316
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void LobbyCountMethod(IntPtr methodsPtr, ref int count);

			// Token: 0x02000175 RID: 373
			// (Invoke) Token: 0x06000910 RID: 2320
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result GetLobbyIdMethod(IntPtr methodsPtr, int index, ref long lobbyId);

			// Token: 0x02000176 RID: 374
			// (Invoke) Token: 0x06000914 RID: 2324
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ConnectVoiceCallback(IntPtr ptr, Result result);

			// Token: 0x02000177 RID: 375
			// (Invoke) Token: 0x06000918 RID: 2328
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void ConnectVoiceMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.ConnectVoiceCallback callback);

			// Token: 0x02000178 RID: 376
			// (Invoke) Token: 0x0600091C RID: 2332
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void DisconnectVoiceCallback(IntPtr ptr, Result result);

			// Token: 0x02000179 RID: 377
			// (Invoke) Token: 0x06000920 RID: 2336
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate void DisconnectVoiceMethod(IntPtr methodsPtr, long lobbyId, IntPtr callbackData, LobbyManager.FFIMethods.DisconnectVoiceCallback callback);

			// Token: 0x0200017A RID: 378
			// (Invoke) Token: 0x06000924 RID: 2340
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result ConnectNetworkMethod(IntPtr methodsPtr, long lobbyId);

			// Token: 0x0200017B RID: 379
			// (Invoke) Token: 0x06000928 RID: 2344
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result DisconnectNetworkMethod(IntPtr methodsPtr, long lobbyId);

			// Token: 0x0200017C RID: 380
			// (Invoke) Token: 0x0600092C RID: 2348
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result FlushNetworkMethod(IntPtr methodsPtr);

			// Token: 0x0200017D RID: 381
			// (Invoke) Token: 0x06000930 RID: 2352
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result OpenNetworkChannelMethod(IntPtr methodsPtr, long lobbyId, byte channelId, bool reliable);

			// Token: 0x0200017E RID: 382
			// (Invoke) Token: 0x06000934 RID: 2356
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			internal delegate Result SendNetworkMessageMethod(IntPtr methodsPtr, long lobbyId, long userId, byte channelId, byte[] data, int dataLen);
		}

		// Token: 0x0200017F RID: 383
		// (Invoke) Token: 0x06000938 RID: 2360
		public delegate void CreateLobbyHandler(Result result, ref Lobby lobby);

		// Token: 0x02000180 RID: 384
		// (Invoke) Token: 0x0600093C RID: 2364
		public delegate void UpdateLobbyHandler(Result result);

		// Token: 0x02000181 RID: 385
		// (Invoke) Token: 0x06000940 RID: 2368
		public delegate void DeleteLobbyHandler(Result result);

		// Token: 0x02000182 RID: 386
		// (Invoke) Token: 0x06000944 RID: 2372
		public delegate void ConnectLobbyHandler(Result result, ref Lobby lobby);

		// Token: 0x02000183 RID: 387
		// (Invoke) Token: 0x06000948 RID: 2376
		public delegate void ConnectLobbyWithActivitySecretHandler(Result result, ref Lobby lobby);

		// Token: 0x02000184 RID: 388
		// (Invoke) Token: 0x0600094C RID: 2380
		public delegate void DisconnectLobbyHandler(Result result);

		// Token: 0x02000185 RID: 389
		// (Invoke) Token: 0x06000950 RID: 2384
		public delegate void UpdateMemberHandler(Result result);

		// Token: 0x02000186 RID: 390
		// (Invoke) Token: 0x06000954 RID: 2388
		public delegate void SendLobbyMessageHandler(Result result);

		// Token: 0x02000187 RID: 391
		// (Invoke) Token: 0x06000958 RID: 2392
		public delegate void SearchHandler(Result result);

		// Token: 0x02000188 RID: 392
		// (Invoke) Token: 0x0600095C RID: 2396
		public delegate void ConnectVoiceHandler(Result result);

		// Token: 0x02000189 RID: 393
		// (Invoke) Token: 0x06000960 RID: 2400
		public delegate void DisconnectVoiceHandler(Result result);

		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x06000964 RID: 2404
		public delegate void LobbyUpdateHandler(long lobbyId);

		// Token: 0x0200018B RID: 395
		// (Invoke) Token: 0x06000968 RID: 2408
		public delegate void LobbyDeleteHandler(long lobbyId, uint reason);

		// Token: 0x0200018C RID: 396
		// (Invoke) Token: 0x0600096C RID: 2412
		public delegate void MemberConnectHandler(long lobbyId, long userId);

		// Token: 0x0200018D RID: 397
		// (Invoke) Token: 0x06000970 RID: 2416
		public delegate void MemberUpdateHandler(long lobbyId, long userId);

		// Token: 0x0200018E RID: 398
		// (Invoke) Token: 0x06000974 RID: 2420
		public delegate void MemberDisconnectHandler(long lobbyId, long userId);

		// Token: 0x0200018F RID: 399
		// (Invoke) Token: 0x06000978 RID: 2424
		public delegate void LobbyMessageHandler(long lobbyId, long userId, byte[] data);

		// Token: 0x02000190 RID: 400
		// (Invoke) Token: 0x0600097C RID: 2428
		public delegate void SpeakingHandler(long lobbyId, long userId, bool speaking);

		// Token: 0x02000191 RID: 401
		// (Invoke) Token: 0x06000980 RID: 2432
		public delegate void NetworkMessageHandler(long lobbyId, long userId, byte channelId, byte[] data);
	}
}
