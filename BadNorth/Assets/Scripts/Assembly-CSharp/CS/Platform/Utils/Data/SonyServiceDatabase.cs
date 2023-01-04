using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x0200009A RID: 154
	public class SonyServiceDatabase : ScriptableObject
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x000196AD File Offset: 0x00017AAD
		public void ApplyRegionInfo()
		{
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000196B0 File Offset: 0x00017AB0
		public void ActivatePS4(string region)
		{
			this.activeRegion = -1;
			if (this.Labels != null)
			{
				for (int i = 0; i < this.Labels.Count; i++)
				{
					if (this.Labels[i].region == region)
					{
						this.activeRegion = i;
						return;
					}
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00019710 File Offset: 0x00017B10
		public int GetRegionIndex(string region)
		{
			if (this.Labels == null)
			{
				this.Labels = new List<SonyServiceDatabase.PS4StorageInfo>();
			}
			int i;
			for (i = 0; i < this.Labels.Count; i++)
			{
				if (this.Labels[i].region == region)
				{
					return i;
				}
			}
			this.Labels.Add(new SonyServiceDatabase.PS4StorageInfo(region));
			return i;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00019784 File Offset: 0x00017B84
		public void RemoveRegion(string region)
		{
			if (this.Labels == null)
			{
				this.Labels = new List<SonyServiceDatabase.PS4StorageInfo>();
			}
			for (int i = 0; i < this.Labels.Count; i++)
			{
				if (this.Labels[i].region == region)
				{
					this.Labels.RemoveAt(i);
					break;
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000197F4 File Offset: 0x00017BF4
		public static void ApplySonyServiceInfo(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/SonyService";
			}
			SonyServiceDatabase sonyServiceDatabase = Resources.Load(databaseLocation) as SonyServiceDatabase;
			if (sonyServiceDatabase == null)
			{
				Debug.LogError("[SSD] Failed to load service database | Location: {0}", new object[]
				{
					databaseLocation
				});
				return;
			}
			sonyServiceDatabase.ApplyRegionInfo();
		}

		// Token: 0x040002A6 RID: 678
		[SerializeField]
		public bool onlineEnabled = true;

		// Token: 0x040002A7 RID: 679
		[SerializeField]
		public bool dynamicUsers = true;

		// Token: 0x040002A8 RID: 680
		[SerializeField]
		public List<SonyServiceDatabase.PS4StorageInfo> Labels = new List<SonyServiceDatabase.PS4StorageInfo>();

		// Token: 0x040002A9 RID: 681
		[SerializeField]
		public int activeRegion = -1;

		// Token: 0x040002AA RID: 682
		public const string DefaultSave = "Assets/Coatsink/Resources/Platform/SonyService.asset";

		// Token: 0x0200009B RID: 155
		[Serializable]
		public class PS4StorageInfo
		{
			// Token: 0x060005F5 RID: 1525 RVA: 0x00019846 File Offset: 0x00017C46
			public PS4StorageInfo(string region)
			{
				this.region = region;
			}

			// Token: 0x040002AB RID: 683
			public string region = string.Empty;

			// Token: 0x040002AC RID: 684
			public int Matching2 = -1;

			// Token: 0x040002AD RID: 685
			public int sessionInvitation = -1;

			// Token: 0x040002AE RID: 686
			public int Presence = -1;

			// Token: 0x040002AF RID: 687
			public int NpService = -1;
		}
	}
}
