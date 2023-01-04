using System;
using CS.Platform.Utils;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x0200003F RID: 63
	public class SteamAchievementManager : MonoBehaviour
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000C2A4 File Offset: 0x0000A6A4
		public void Awake()
		{
			this._manager = base.GetComponent<SteamManager>();
			this._callbackUserAchievementStored = Callback<UserAchievementStored_t>.Create(new Callback<UserAchievementStored_t>.DispatchDelegate(this.OnUserAchievementStored));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C2C9 File Offset: 0x0000A6C9
		public void UnlockAchievement(string achievementName)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMACHIEVE] UnlockAchievement Failed: Trial Version | Key: {0}", new object[]
			{
				achievementName
			});
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C2DF File Offset: 0x0000A6DF
		public bool GetAchievement(string achievementName)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMACHIEVE] GetAchievement Failed: Trial Version | Key: {0}", new object[]
			{
				achievementName
			});
			return false;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C2F6 File Offset: 0x0000A6F6
		private void OnUserAchievementStored(UserAchievementStored_t userStats)
		{
			if (userStats.m_nGameID == this._manager.AppID)
			{
				CS.Platform.Utils.Debug.LogInfo("[STEAMACHIEVE] OnUserAchievementStored: Unlocked | Key: {0}", new object[]
				{
					userStats.m_rgchAchievementName
				});
			}
		}

		// Token: 0x040000F9 RID: 249
		private SteamManager _manager;

		// Token: 0x040000FA RID: 250
		protected Callback<UserAchievementStored_t> _callbackUserAchievementStored;
	}
}
