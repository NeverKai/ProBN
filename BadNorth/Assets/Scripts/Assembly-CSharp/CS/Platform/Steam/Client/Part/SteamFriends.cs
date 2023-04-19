using System;
using System.Threading;
using CS.Platform.Utils;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000043 RID: 67
	public class SteamFriends : MonoBehaviour
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000CD58 File Offset: 0x0000B158
		public BaseUserInfo[] Friends
		{
			get
			{
				object locker = this._locker;
				BaseUserInfo[] result;
				lock (locker)
				{
					if (!this._gening)
					{
						result = this._friends;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CDA8 File Offset: 0x0000B1A8
		public void Awake()
		{
			this._manager = base.GetComponent<SteamManager>();
			this._thread = new ThreadHandler("FriendFinder");
			this._thread.RunThreadOnce = true;
			this._thread.Priority = System.Threading.ThreadPriority.Lowest;
			this._thread.AddPart(new Action(this.UpdateListLogic));
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CE00 File Offset: 0x0000B200
		public bool UpdateList()
		{
			if (!this._thread.Running)
			{
				this._thread.Start();
				return true;
			}
			return false;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CE20 File Offset: 0x0000B220
		private void UpdateListLogic()
		{
			object locker = this._locker;
			lock (locker)
			{
				this._gening = true;
			}
			// int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
			// if (this._friends == null || friendCount != this._friends.Length)
			// {
			// 	this._friends = new BaseUserInfo[friendCount];
			// }
			// for (int i = 0; i < friendCount; i++)
			// {
			// 	CSteamID friendByIndex = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
			// 	if (friendByIndex == CSteamID.Nil)
			// 	{
			// 		this._friends[i] = default(BaseUserInfo);
			// 	}
			// 	else
			// 	{
			// 		this._friends[i] = new BaseUserInfo((ulong)friendByIndex, "steam", SteamFriends.GetFriendPersonaName(friendByIndex));
			// 	}
			// }
			BaseUserInfo[] newUsers = this._friends;
			object locker2 = this._locker;
			lock (locker2)
			{
				this._gening = false;
			}
			this._manager.AddToNextUpdate(delegate
			{
				PlatformEvents.FriendsListUpdated(newUsers);
			});
		}

		// Token: 0x0400010A RID: 266
		private SteamManager _manager;

		// Token: 0x0400010B RID: 267
		private ThreadHandler _thread;

		// Token: 0x0400010C RID: 268
		private object _locker = new object();

		// Token: 0x0400010D RID: 269
		private bool _gening;

		// Token: 0x0400010E RID: 270
		private BaseUserInfo[] _friends;
	}
}
