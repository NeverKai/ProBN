using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000537 RID: 1335
	public class LevelPreparePhase : MonoBehaviour, IslandGameplayManager.IAwake
	{
		// Token: 0x060022D5 RID: 8917 RVA: 0x00066AC4 File Offset: 0x00064EC4
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00066ACD File Offset: 0x00064ECD
		private void OnEnable()
		{
			this.startTime = Time.time + UnityEngine.Random.Range(this.minDelay, this.maxDelay);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00066AEC File Offset: 0x00064EEC
		private void Update()
		{
			if (Time.time >= this.startTime)
			{
				this.manager.CallFirstWave();
			}
		}

		// Token: 0x04001541 RID: 5441
		[SerializeField]
		private float minDelay = 3.5f;

		// Token: 0x04001542 RID: 5442
		private float maxDelay = 4.5f;

		// Token: 0x04001543 RID: 5443
		private IslandGameplayManager manager;

		// Token: 0x04001544 RID: 5444
		private float startTime = float.MinValue;
	}
}
