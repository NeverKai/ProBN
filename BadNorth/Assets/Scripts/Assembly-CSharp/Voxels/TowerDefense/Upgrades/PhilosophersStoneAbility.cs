using System;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CoinDispensing;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000856 RID: 2134
	internal class PhilosophersStoneAbility : UpgradeComponent
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x000F0BF1 File Offset: 0x000EEFF1
		public int numCoins
		{
			get
			{
				return this.numCoinsAtLevel[base.upgradeLevel];
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000F0C00 File Offset: 0x000EF000
		protected override void DoSquadSpawnAction_Implementation()
		{
			this.coinSpawnTransform = base.squad.heroAgent.gameObject.AddEmptyChild(null).transform;
			base.squad.onSquadSpawnComplete += this.Squad_onSquadSpawnComplete;
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000F0C3A File Offset: 0x000EF03A
		private void Squad_onSquadSpawnComplete()
		{
			this.coinSpawnTransform.localPosition = Vector3.zero;
			this.coinSpawnTransform.position = this.coinSpawnTransform.position + Vector3.up;
		}

		// Token: 0x04002605 RID: 9733
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("PhilosophersStone", EVerbosity.Quiet, 0);

		// Token: 0x04002606 RID: 9734
		[SerializeField]
		private int[] numCoinsAtLevel = new int[]
		{
			1,
			2,
			3
		};

		// Token: 0x04002607 RID: 9735
		[NonSerialized]
		public List<DispensedCoin> coins = new List<DispensedCoin>(8);

		// Token: 0x04002608 RID: 9736
		[NonSerialized]
		public Transform coinSpawnTransform;
	}
}
