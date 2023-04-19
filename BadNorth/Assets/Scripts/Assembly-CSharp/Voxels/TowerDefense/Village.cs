using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000862 RID: 2146
	public class Village : IslandComponent, IIslandEnter, IIslandWipe, IIslandProcessor, IIslandReset
	{
		// Token: 0x0600383C RID: 14396 RVA: 0x000F2AB5 File Offset: 0x000F0EB5
		public string GetProcessMessage(MultiWave multiWave)
		{
			return "Building village";
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x0600383D RID: 14397 RVA: 0x000F2ABC File Offset: 0x000F0EBC
		// (remove) Token: 0x0600383E RID: 14398 RVA: 0x000F2AF4 File Offset: 0x000F0EF4
		
		public event Village.HouseDelegate onHouseDestroyed = delegate(House A_0)
		{
		};

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x0600383F RID: 14399 RVA: 0x000F2B2C File Offset: 0x000F0F2C
		// (remove) Token: 0x06003840 RID: 14400 RVA: 0x000F2B64 File Offset: 0x000F0F64
		
		public event Village.VoidDelegate onHouseDestroyedVoid = delegate()
		{
		};

		// Token: 0x06003841 RID: 14401 RVA: 0x000F2B9C File Offset: 0x000F0F9C
		public float SampleDistanceToHouse(NavPos navPos)
		{
			float result = float.MaxValue;
			for (int i = 0; i < this.houses.Length; i++)
			{
				result = Mathf.Min(new float[]
				{
					this.houses[i].distanceField.SampleDistance(navPos)
				});
			}
			return result;
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000F2BEC File Offset: 0x000F0FEC
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			this.houses = island.moduleContainer.GetComponentsInChildren<House>();
			this.houseCount = this.houses.Length;
			NavigationMesh navMesh = island.navMesh;
			foreach (House house in this.houses)
			{
				house.transform.position += island.voxelSpace.GetPosOffset(house.transform.position);
				house.transform.SetParent(base.transform);
				if (!house.Check(navMesh, this))
				{
					yield return new GenInfo("House broken", GenInfo.Mode.broken);
				}
				if (!this.checkpointHouse)
				{
					this.checkpointHouse = house.GetComponent<CheckpointHouse>();
					if (this.checkpointHouse)
					{
						this.checkpointHouse.house = house;
					}
				}
			}
			for (int i = 0; i < this.houseCount; i++)
			{
				House house2 = this.houses[i];
				house2.Setup(navMesh, this);
				yield return new GenInfo("Building house", GenInfo.Mode.interruptable);
			}
			CampaignDifficultySettings difficultySettings = island.levelNode.diffiucltySettings;
			this.torchDamage = difficultySettings.torchDamage;
			this.timerPerArsonist = difficultySettings.timerPerArsonist;
			yield return new GenInfo("Building village", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000F2C0E File Offset: 0x000F100E
		public void HousePillaged(House house)
		{
			this.onHouseDestroyed(house);
			this.onHouseDestroyedVoid();
			this.houseCount--;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000F2C38 File Offset: 0x000F1038
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			for (int i = 0; i < this.houses.Length; i++)
			{
				this.houses[i].Reset();
			}
			this.houseCount = this.houses.Length;
			yield return new GenInfo("Villge reset", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000F2C54 File Offset: 0x000F1054
		IEnumerator<GenInfo> IIslandReset.OnIslandReset(Island island)
		{
			this.SetupHousesAndCountCoints(island);
			yield return new GenInfo("Reapply House States", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000F2C78 File Offset: 0x000F1078
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			this.SetupHousesAndCountCoints(island);
			yield return new GenInfo("Reapply House States", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000F2C9C File Offset: 0x000F109C
		private void SetupHousesAndCountCoints(Island island)
		{
			LevelNode levelNode = island.levelNode;
			LevelState levelState = levelNode.levelState;
			this.houseCount = this.houses.Length;
			for (int i = 0; i < this.houses.Length; i++)
			{
				House house = this.houses[i];
				HouseState houseState = levelState.houses[i];
				house.coinCount = houseState.availableCoins;
				if (houseState.condition == HouseState.Condition.Pillaged)
				{
					house.StartLevelPillaged();
				}
			}
		}

		// Token: 0x04002650 RID: 9808
		public House[] houses;

		// Token: 0x04002651 RID: 9809
		[NonSerialized]
		public AnimationCurve timerPerArsonist;

		// Token: 0x04002652 RID: 9810
		[NonSerialized]
		public float torchDamage;

		// Token: 0x04002653 RID: 9811
		public int houseCount;

		// Token: 0x04002654 RID: 9812
		public CheckpointHouse checkpointHouse;

		// Token: 0x02000863 RID: 2147
		// (Invoke) Token: 0x0600384B RID: 14411
		public delegate void HouseDelegate(House house);

		// Token: 0x02000864 RID: 2148
		// (Invoke) Token: 0x0600384F RID: 14415
		public delegate void VoidDelegate();
	}
}
