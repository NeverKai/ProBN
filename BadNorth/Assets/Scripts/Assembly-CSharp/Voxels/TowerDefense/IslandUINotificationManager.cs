using System;
using System.Linq;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200055A RID: 1370
	public class IslandUINotificationManager : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x060023A8 RID: 9128 RVA: 0x0006E998 File Offset: 0x0006CD98
		private IslandUINotification GetInstance()
		{
			this.currentMessageIdx++;
			return this.notifications.GetInstance();
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x0006E9B4 File Offset: 0x0006CDB4
		private void Sort()
		{
			this.notifications.inUse = this.notifications.inUse.OrderBy(IslandUINotification.order).ToList<IslandUINotification>();
			foreach (IslandUINotification islandUINotification in this.notifications.inUse)
			{
				islandUINotification.transform.SetAsLastSibling();
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0006EA40 File Offset: 0x0006CE40
		public IslandUINotification PostMessage(string locTerm, IslandUINotification.Priority priority, float duration)
		{
			return this.PostMessage(locTerm, priority, null, null, false, duration);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0006EA50 File Offset: 0x0006CE50
		public IslandUINotification PostMessage(string locTerm, IslandUINotification.Priority priority, Sprite iconLeft = null, Sprite iconRight = null, bool distanceField = false, float duration = 0f)
		{
			IslandUINotification result = this.GetInstance().Setup(this.currentMessageIdx, locTerm, priority, iconLeft, iconRight, distanceField, duration);
			this.Sort();
			return result;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0006EA7F File Offset: 0x0006CE7F
		private void RemoveMessages()
		{
			this.notifications.ReturnAll();
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0006EA8C File Offset: 0x0006CE8C
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.RemoveMessages();
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0006EA94 File Offset: 0x0006CE94
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.notifications = new LocalPool<IslandUINotification>(base.GetComponentsInChildren<IslandUINotification>(true), null);
			this.notifications.ExpandTo(4);
		}

		// Token: 0x04001649 RID: 5705
		private LocalPool<IslandUINotification> notifications;

		// Token: 0x0400164A RID: 5706
		private int currentMessageIdx;
	}
}
