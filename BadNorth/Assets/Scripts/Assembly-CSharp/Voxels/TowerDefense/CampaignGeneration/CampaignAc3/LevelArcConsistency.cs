using System;
using System.Collections;
using System.Collections.Generic;
using ArcConsistency;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x02000700 RID: 1792
	public class LevelArcConsistency : Singleton<LevelArcConsistency>
	{
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000B4F80 File Offset: 0x000B3380
		private ArcConsistencySolver ac3
		{
			get
			{
				if (!this._ac3)
				{
					this._ac3 = base.gameObject.GetOrAddComponent<ArcConsistencySolver>();
				}
				return this._ac3;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000B4FA9 File Offset: 0x000B33A9
		private LevelAssigner[] assigners
		{
			get
			{
				if (this._assigners == null)
				{
					this._assigners = base.GetComponentsInChildren<LevelAssigner>(true);
				}
				return this._assigners;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x000B4FC9 File Offset: 0x000B33C9
		public int index
		{
			get
			{
				return (int)this.levelState.index;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x000B4FD6 File Offset: 0x000B33D6
		public int width
		{
			get
			{
				return (int)this.levelState.width;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x000B4FE3 File Offset: 0x000B33E3
		public int height
		{
			get
			{
				return (int)this.levelState.height;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000B4FF0 File Offset: 0x000B33F0
		public int enemyCount
		{
			get
			{
				return (int)this.protoLevel.enemyTypes;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000B4FFD File Offset: 0x000B33FD
		public float fraction
		{
			get
			{
				return (float)this.index / ((float)this.count - 1f);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x000B5014 File Offset: 0x000B3414
		public float month
		{
			get
			{
				return (this.fraction * 12f + 4f) % 12f;
			}
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000B5030 File Offset: 0x000B3430
		public IEnumerator<object> AssignThingsToCampaign(CampaignSave campaignSave, ProtoCampaign protoCampaign)
		{
			this.count = campaignSave.levelStates.Count;
			this.noiseOffset.x = (float)UnityEngine.Random.Range(-10, 10);
			this.noiseOffset.y = (float)UnityEngine.Random.Range(-10, 10);
			this.noiseScale = 1.4f / campaignSave.levelStates[campaignSave.levelStates.Count - 1].pos.x;
			for (int i = 0; i < campaignSave.levelStates.Count; i++)
			{
				this.levelState = campaignSave.levelStates[i];
				this.protoLevel = protoCampaign.nodes[i];
				IEnumerator enumerator = this.ac3.Resolve();
				while (enumerator.MoveNext())
				{
					yield return null;
				}
				foreach (LevelAssigner levelAssigner in this.assigners)
				{
					this.levelState.objectReferences.AddRange(levelAssigner.MaybeAssign());
				}
				yield return null;
			}
			this.levelState = null;
			yield break;
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000B505C File Offset: 0x000B345C
		public float GetNoise(int id)
		{
			float num = (float)id % 3.1415927f;
			float x = this.levelState.posX * this.noiseScale + this.noiseOffset.x + num;
			float y = this.levelState.posY * this.noiseScale + this.noiseOffset.y;
			return Mathf.PerlinNoise(x, y);
		}

		// Token: 0x04001EAD RID: 7853
		private ArcConsistencySolver _ac3;

		// Token: 0x04001EAE RID: 7854
		private LevelAssigner[] _assigners;

		// Token: 0x04001EAF RID: 7855
		public LevelState levelState;

		// Token: 0x04001EB0 RID: 7856
		public Node protoLevel;

		// Token: 0x04001EB1 RID: 7857
		public int count = 50;

		// Token: 0x04001EB2 RID: 7858
		private Vector2 noiseOffset;

		// Token: 0x04001EB3 RID: 7859
		private float noiseScale;
	}
}
