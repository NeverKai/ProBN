using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x02000589 RID: 1417
	[Serializable]
	public class CampaignStats
	{
		// Token: 0x060024C7 RID: 9415 RVA: 0x00073B60 File Offset: 0x00071F60
		public CampaignStats()
		{
			this.InitVikingTypesKilled();
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00073B78 File Offset: 0x00071F78
		public void KilledViking(VikingAgent.Type type)
		{
			this.vikingsKilled++;
			if (this.vikingTypesKilled != null)
			{
				List<int> list;
				(list = this.vikingTypesKilled)[(int)type] = list[(int)type] + 1;
			}
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00073BB7 File Offset: 0x00071FB7
		public int GetVikingsKilled()
		{
			return this.vikingsKilled;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00073BBF File Offset: 0x00071FBF
		public int GetVikingsKilled(VikingAgent.Type type)
		{
			return (this.vikingTypesKilled != null) ? this.vikingTypesKilled[(int)type] : -1;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00073BDE File Offset: 0x00071FDE
		public void InitVikingTypesKilled()
		{
			if (this.vikingTypesKilled == null)
			{
				this.vikingTypesKilled = new List<int>(VikingAgent.numTypes);
			}
			this.ResizeVikingTypesKilled();
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00073C04 File Offset: 0x00072004
		private void ResizeVikingTypesKilled()
		{
			if (this.vikingTypesKilled == null)
			{
				return;
			}
			int numTypes = VikingAgent.numTypes;
			this.vikingTypesKilled.Capacity = numTypes;
			while (this.vikingTypesKilled.Count < numTypes)
			{
				this.vikingTypesKilled.Add(0);
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00073C51 File Offset: 0x00072051
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			if (this.version == 0)
			{
				this.heroesDied /= 2;
			}
			this.version = 1;
			this.ResizeVikingTypesKilled();
		}

		// Token: 0x04001750 RID: 5968
		private const int currVersion = 1;

		// Token: 0x04001751 RID: 5969
		private int version = 1;

		// Token: 0x04001752 RID: 5970
		private int vikingsKilled;

		// Token: 0x04001753 RID: 5971
		public int englishKilled;

		// Token: 0x04001754 RID: 5972
		public int heroesRecruited;

		// Token: 0x04001755 RID: 5973
		public int heroesDied;

		// Token: 0x04001756 RID: 5974
		public int islandsVisited;

		// Token: 0x04001757 RID: 5975
		public int islandsDefended;

		// Token: 0x04001758 RID: 5976
		public int islandsLost;

		// Token: 0x04001759 RID: 5977
		public int islandsFled;

		// Token: 0x0400175A RID: 5978
		public int uniqueIslandsVisited;

		// Token: 0x0400175B RID: 5979
		public int uniqueIslandsDefended;

		// Token: 0x0400175C RID: 5980
		public int uniqueIslandsLost;

		// Token: 0x0400175D RID: 5981
		public int coinsCollected;

		// Token: 0x0400175E RID: 5982
		public int checkpointsSaved;

		// Token: 0x0400175F RID: 5983
		public int checkpointsLost;

		// Token: 0x04001760 RID: 5984
		public int levelRestarts;

		// Token: 0x04001761 RID: 5985
		[ObjectDumper.EnumIndexedCollectionAttribute(typeof(VikingAgent.Type))]
		private List<int> vikingTypesKilled;
	}
}
