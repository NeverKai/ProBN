using System;
using System.Collections;
using System.Collections.Generic;
using RTM.Pools;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x02000538 RID: 1336
	public class NavigatorNavSpotPool : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIslandCoroutine, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x060022D9 RID: 8921 RVA: 0x00066B2A File Offset: 0x00064F2A
		public void SetDirty()
		{
			base.enabled = true;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00066B34 File Offset: 0x00064F34
		public void LateUpdate()
		{
			EnglishSquad selectedSquad = this.squadSelector.selectedSquad;
			if (selectedSquad)
			{
				SquadUpgradeManager upgradeManager = selectedSquad.upgradeManager;
				if (upgradeManager.IsBlockingMove())
				{
					this.SetVisibility(false);
				}
				else if (upgradeManager.selectedAbility && !(upgradeManager.selectedAbility is JoystickMoveAbility))
				{
					this.SetVisibility(false);
				}
				else
				{
					this.SetVisibility(true);
				}
			}
			else
			{
				this.SetVisibility(false);
			}
			base.enabled = false;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00066BBC File Offset: 0x00064FBC
		private void SetVisibility(bool visible)
		{
			if (this.visible == visible)
			{
				return;
			}
			using ("SetNavSpotVisibility")
			{
				foreach (TargetNavSpot targetNavSpot in this.pool.inUse)
				{
					targetNavSpot.SetVisible(visible);
					targetNavSpot.DoFlash();
				}
			}
			this.visible = visible;
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00066C64 File Offset: 0x00065064
		public void SetHover(NavSpot navSpot)
		{
			this.SetHover(this.hoverSpot, false);
			this.SetHover(navSpot, true);
			this.hoverSpot.Target = navSpot;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x00066C8C File Offset: 0x0006508C
		private void SetHover(NavSpot ns, bool hover)
		{
			NavSpot navSpot = (!this.squadSelector.selectedSquad) ? null : this.squadSelector.selectedSquad.navSpotOccupant.navSpot;
			if (navSpot && navSpot == ns)
			{
				hover = false;
			}
			TargetNavSpot targetNavSpot = null;
			if (ns && this.dictionary.TryGetValue(ns, out targetNavSpot))
			{
				targetNavSpot.SetHover(hover);
			}
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x00066D0B File Offset: 0x0006510B
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			base.enabled = false;
			this.pool = new LocalPool<TargetNavSpot>(base.GetComponentInChildren<TargetNavSpot>(true), null);
			this.squadSelector = manager.squadSelector;
			this.squadSelector.onSquadSelectionChanged += delegate(EnglishSquad s)
			{
				this.SetDirty();
			};
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x00066D4C File Offset: 0x0006514C
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			base.enabled = false;
			this.dictionary.Clear();
			foreach (NavSpot navSpot in island.navSpotter.navSpots)
			{
				TargetNavSpot tns = this.pool.GetInstance();
				tns.Setup(navSpot);
				this.dictionary.Add(navSpot, tns);
				yield return null;
			}
			yield break;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00066D6E File Offset: 0x0006516E
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.hoverSpot.Target = null;
			this.dictionary.Clear();
			this.pool.ReturnAll();
			this.visible = false;
			base.enabled = false;
		}

		// Token: 0x04001545 RID: 5445
		private LocalPool<TargetNavSpot> pool;

		// Token: 0x04001546 RID: 5446
		private Dictionary<NavSpot, TargetNavSpot> dictionary = new Dictionary<NavSpot, TargetNavSpot>(64);

		// Token: 0x04001547 RID: 5447
		private WeakReference<NavSpot> hoverSpot = new WeakReference<NavSpot>(null);

		// Token: 0x04001548 RID: 5448
		private SquadSelector squadSelector;

		// Token: 0x04001549 RID: 5449
		private bool visible;
	}
}
