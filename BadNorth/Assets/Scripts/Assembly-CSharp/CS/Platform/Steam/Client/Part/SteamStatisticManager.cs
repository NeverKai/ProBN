using System;
using CS.Platform.Utils;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000048 RID: 72
	public class SteamStatisticManager : MonoBehaviour
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000F7A0 File Offset: 0x0000DBA0
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
			this._callbackUserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
			this._callbackUserStatsStored = Callback<UserStatsStored_t>.Create(new Callback<UserStatsStored_t>.DispatchDelegate(this.OnUserStatsStored));
			this._callbackUserStatsUnloaded = Callback<UserStatsUnloaded_t>.Create(new Callback<UserStatsUnloaded_t>.DispatchDelegate(this.OnUserStatsUnloaded));
			if (!SteamUserStats.RequestCurrentStats())
			{
				CS.Platform.Utils.Debug.LogError("[STEAMSTATS] RequestCurrentStats Failed", new object[0]);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000F818 File Offset: 0x0000DC18
		public void ChangeStatistic(string statisticName, float amountChanged)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMSTATS] ChangeStatistic Failed: Trial Version", new object[0]);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F82A File Offset: 0x0000DC2A
		public void SetStatistic(string statisticName, float value)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMSTATS] SetStatistic Failed: Trial Version", new object[0]);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F83C File Offset: 0x0000DC3C
		public bool GetStatistic(string statisticName, out float value)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMSTATS] GetStatistic Failed: Trial Version", new object[0]);
			value = 0f;
			return false;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F856 File Offset: 0x0000DC56
		private void OnUserStatsReceived(UserStatsReceived_t userStats)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMSTATS] OnUserStatsReceived Failed: Trial Version", new object[0]);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000F868 File Offset: 0x0000DC68
		private void OnUserStatsStored(UserStatsStored_t userStats)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMSTATS] OnUserStatsStored Failed: Trial Version", new object[0]);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000F87A File Offset: 0x0000DC7A
		private void OnUserStatsUnloaded(UserStatsUnloaded_t userStats)
		{
			CS.Platform.Utils.Debug.LogWarning("[STEAMSTATS] OnUserStatsUnloaded Failed: Trial Version", new object[0]);
		}

		// Token: 0x0400013B RID: 315
		private SteamManager _Manager;

		// Token: 0x0400013C RID: 316
		private bool _UserStatsLoaded;

		// Token: 0x0400013D RID: 317
		protected Callback<UserStatsReceived_t> _callbackUserStatsReceived;

		// Token: 0x0400013E RID: 318
		protected Callback<UserStatsStored_t> _callbackUserStatsStored;

		// Token: 0x0400013F RID: 319
		protected Callback<UserStatsUnloaded_t> _callbackUserStatsUnloaded;
	}
}
