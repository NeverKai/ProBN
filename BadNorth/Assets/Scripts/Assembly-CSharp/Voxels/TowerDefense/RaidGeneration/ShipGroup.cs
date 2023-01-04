using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense.RaidGeneration
{
	// Token: 0x020005B8 RID: 1464
	public class ShipGroup : MonoBehaviour
	{
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x0007914F File Offset: 0x0007754F
		public float startTime
		{
			get
			{
				return this.wave.waveStartTime + this.timeOffset * this.wave.timeSpreadGroup;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x0007916F File Offset: 0x0007756F
		public Landing randomLanding
		{
			get
			{
				return this.landings[UnityEngine.Random.Range(0, this.landings.Count)];
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x0007918D File Offset: 0x0007758D
		public int bounty
		{
			get
			{
				return this.landings.Sum((Landing x) => x.bounty);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000791B7 File Offset: 0x000775B7
		public bool anyPlaced
		{
			get
			{
				return this.landings.Any((Landing x) => x.placed);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x000791E4 File Offset: 0x000775E4
		public Squad squad
		{
			get
			{
				if (!this._squad)
				{
					this._squad = ScriptableObjectSingleton<PrefabManager>.instance.vikingSquad.SpawnGetFromPrefab(this.wave.raid.island.vikings);
				}
				return this._squad;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00079234 File Offset: 0x00077634
		public Vector3 avPos
		{
			get
			{
				Vector3 a = Vector3.zero;
				int num = 0;
				foreach (Landing landing in this.landings)
				{
					if (landing.placed)
					{
						a += landing.transform.position;
						num++;
					}
				}
				if (num == 0)
				{
					return Vector3.zero;
				}
				return a / (float)num;
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000792C8 File Offset: 0x000776C8
		public ShipGroup Duplicate()
		{
			return this.wave.AddShipGroup(UnityEngine.Object.Instantiate<GameObject>(base.gameObject, base.transform.parent).GetComponent<ShipGroup>());
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000792F0 File Offset: 0x000776F0
		public Landing AddLanding(Landing landing)
		{
			if (landing.shipGroup)
			{
				landing.shipGroup.landings.Remove(landing);
			}
			this.landings.Add(landing);
			landing.shipGroup = this;
			landing.transform.SetParent(base.transform);
			return landing;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x00079344 File Offset: 0x00077744
		public void Reset()
		{
			this._squad = null;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0007934D File Offset: 0x0007774D
		public void OnDestroy()
		{
			this.wave = null;
			this.landings = null;
			this._squad = null;
		}

		// Token: 0x04001840 RID: 6208
		public float timeOffset;

		// Token: 0x04001841 RID: 6209
		public Wave wave;

		// Token: 0x04001842 RID: 6210
		public List<Landing> landings = new List<Landing>();

		// Token: 0x04001843 RID: 6211
		private Squad _squad;
	}
}
