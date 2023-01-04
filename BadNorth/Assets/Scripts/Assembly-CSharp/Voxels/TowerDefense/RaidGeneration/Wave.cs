using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fabric;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.RaidGeneration
{
	// Token: 0x020005BB RID: 1467
	public class Wave : MonoBehaviour
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x0007958C File Offset: 0x0007798C
		public ShipGroup randomGroup
		{
			get
			{
				return this.shipGroups[UnityEngine.Random.Range(0, this.shipGroups.Count)];
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x000795AA File Offset: 0x000779AA
		public int bounty
		{
			get
			{
				return this.shipGroups.Sum((ShipGroup x) => x.bounty);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000795D4 File Offset: 0x000779D4
		public float launchDuration
		{
			get
			{
				float num = this.shipGroups.Max((ShipGroup g) => g.timeOffset + g.landings.Max((Landing l) => l.timeOffset));
				float num2 = this.shipGroups.Min((ShipGroup g) => g.timeOffset + g.landings.Min((Landing l) => l.timeOffset));
				return num - num2;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x00079636 File Offset: 0x00077A36
		public UnityEngine.Color color
		{
			get
			{
				return UnityEngine.Color.HSVToRGB(this.waveStartTime / 60f % 1f, 0.7f, 1f);
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0007965C File Offset: 0x00077A5C
		private void Awake()
		{
			this.timeSpreadGroup = UnityEngine.Random.Range(0f, 4f) * UnityEngine.Random.Range(0f, 4f);
			this.timeSpreadShip = UnityEngine.Random.Range(0f, 4f) * UnityEngine.Random.Range(0f, 4f);
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000796B4 File Offset: 0x00077AB4
		public ShipGroup AddShipGroup(ShipGroup shipGroup)
		{
			if (shipGroup.wave)
			{
				shipGroup.wave.shipGroups.Remove(shipGroup);
			}
			this.shipGroups.Add(shipGroup);
			shipGroup.wave = this;
			shipGroup.transform.SetParent(base.transform);
			this.RefreshLandings();
			return shipGroup;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00079710 File Offset: 0x00077B10
		public void RefreshLandings()
		{
			this.landings = (from x in base.GetComponentsInChildren<Landing>(true)
			orderby x.startTime
			select x).ToList<Landing>();
			foreach (Landing landing in this.landings)
			{
				landing.gameObject.SetActive(true);
				landing.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000797B4 File Offset: 0x00077BB4
		public void Reset()
		{
			if (this.triggered && !this.hasArrived)
			{
				FabricWrapper.PostEvent(this.approachAudioId, EventAction.StopSound, base.gameObject);
			}
			this.triggered = false;
			this.hasArrived = false;
			this.haveAllLaunched = false;
			this.haveAllSpawned = false;
			this.landings = null;
			foreach (ShipGroup shipGroup in this.shipGroups)
			{
				shipGroup.Reset();
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x0007985C File Offset: 0x00077C5C
		public void OnShipArrival()
		{
			if (!this.hasArrived)
			{
				FabricWrapper.PostEvent(this.approachAudioId, EventAction.StopSound, base.gameObject);
				FabricWrapper.PostEvent(this.arriveAudioId, base.gameObject);
				this.hasArrived = true;
				if (Singleton<CampaignManager>.instance && Singleton<CampaignManager>.instance.campaign && Singleton<CampaignManager>.instance.campaign.campaignSave)
				{
					CampaignSave campaignSave = Singleton<CampaignManager>.instance.campaign.campaignSave;
					foreach (ShipGroup shipGroup in this.shipGroups)
					{
						foreach (Landing landing in shipGroup.landings)
						{
							foreach (ShipLoad shipLoad in landing.shipLoads)
							{
								shipLoad.vikingRef.Saw();
							}
						}
					}
				}
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000799CC File Offset: 0x00077DCC
		private void Update()
		{
			if (this.triggered && !this.hasArrived && this.landings != null)
			{
				float num = 0f;
				foreach (Landing landing in this.landings)
				{
					if (landing.spawnedShip && landing.spawnedShip.interpolator > num)
					{
						num = landing.spawnedShip.interpolator;
					}
				}
				if (Mathf.Abs(this.prevClosestValue - num) > 0.05f)
				{
					EventManager.Instance.SetParameter(this.approachAudioId, "Intensity", num, base.gameObject);
					this.prevClosestValue = num;
				}
			}
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00079AB8 File Offset: 0x00077EB8
		public IEnumerator BeginWave()
		{
			this.triggered = true;
			using ("waveSoundStart")
			{
				FabricWrapper.PostEvent(this.approachAudioId, base.gameObject);
			}
			float startTime = Time.time - this.landings[0].startTime;
			for (int i = 0; i < this.landings.Count; i++)
			{
				Landing landing = this.landings[i];
				float t = landing.startTime + startTime;
				while (Time.time < t)
				{
					yield return null;
				}
				Longship longship = landing.Launch();
			}
			this.landings[this.landings.Count - 1].spawnedShip.onShipArrival += this.OnShipArrival;
			this.haveAllLaunched = true;
			foreach (Landing landing2 in this.landings)
			{
				while (!landing2.spawnedShip.haveAllSpawned)
				{
					yield return null;
				}
			}
			this.haveAllSpawned = true;
			yield return null;
			yield break;
		}

		// Token: 0x0400184A RID: 6218
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("Wave", EVerbosity.Quiet, 0);

		// Token: 0x0400184B RID: 6219
		public List<ShipGroup> shipGroups = new List<ShipGroup>();

		// Token: 0x0400184C RID: 6220
		public float waveStartTime = -1f;

		// Token: 0x0400184D RID: 6221
		public float timeSpreadGroup;

		// Token: 0x0400184E RID: 6222
		public float timeSpreadShip;

		// Token: 0x0400184F RID: 6223
		private List<Landing> landings;

		// Token: 0x04001850 RID: 6224
		public bool triggered;

		// Token: 0x04001851 RID: 6225
		private bool hasArrived;

		// Token: 0x04001852 RID: 6226
		public bool haveAllLaunched;

		// Token: 0x04001853 RID: 6227
		public bool haveAllSpawned;

		// Token: 0x04001854 RID: 6228
		public Raid raid;

		// Token: 0x04001855 RID: 6229
		public FabricEventReference approachAudioId;

		// Token: 0x04001856 RID: 6230
		public FabricEventReference arriveAudioId;

		// Token: 0x04001857 RID: 6231
		private float prevClosestValue = -1f;
	}
}
