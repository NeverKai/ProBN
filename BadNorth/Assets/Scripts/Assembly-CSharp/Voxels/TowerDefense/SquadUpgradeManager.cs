using System.Collections;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x020007E9 RID: 2025
	public class SquadUpgradeManager : SquadComponent, IEnumerable<UpgradeComponent>, IEnumerable
	{
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000E2AA7 File Offset: 0x000E0EA7
		// (set) Token: 0x06003495 RID: 13461 RVA: 0x000E2AAF File Offset: 0x000E0EAF
		public ActiveAbility currentAbility { get; private set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000E2AB8 File Offset: 0x000E0EB8
		// (set) Token: 0x06003497 RID: 13463 RVA: 0x000E2AC0 File Offset: 0x000E0EC0
		public ActiveAbility selectedAbility { get; private set; }

		// Token: 0x06003498 RID: 13464 RVA: 0x000E2ACC File Offset: 0x000E0ECC
		public T GetUpgrade<T>() where T : UpgradeComponent
		{
			foreach (UpgradeComponent upgradeComponent in this.upgrades)
			{
				T t = upgradeComponent as T;
				if (t)
				{
					return t;
				}
			}
			return (T)((object)null);
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000E2B4C File Offset: 0x000E0F4C
		private void Awake()
		{
			this.enSquad.Target = (EnglishSquad)base.squad;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000E2B64 File Offset: 0x000E0F64
		public bool IsBlockingMove()
		{
			return this.currentAbility && this.currentAbility.blocksMove;
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000E2B84 File Offset: 0x000E0F84
		public bool CanActivateAbility()
		{
			return this.currentAbility == null || this.currentAbility.isCancelable;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000E2BA5 File Offset: 0x000E0FA5
		public void OnAbilitySelected(ActiveAbility ability)
		{
			this.selectedAbility = ability;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000E2BAE File Offset: 0x000E0FAE
		public void OnAbilityDeselected(ActiveAbility ability)
		{
			this.selectedAbility = null;
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000E2BB7 File Offset: 0x000E0FB7
		public void OnAbilityActivated(ActiveAbility ability)
		{
			if (this.currentAbility)
			{
				this.currentAbility.Cancel();
			}
			this.currentAbility = ability;
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000E2BDB File Offset: 0x000E0FDB
		public void OnAbilityEnded(ActiveAbility ability)
		{
			this.currentAbility = null;
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000E2BE4 File Offset: 0x000E0FE4
		private void Update()
		{
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000E2BE8 File Offset: 0x000E0FE8
		public void AddUpgrade(GameObject upgradePrefab, int upgradeLevel)
		{
			UpgradeComponent component = upgradePrefab.GetComponent<UpgradeComponent>();
			this.AddUpgrade(component, upgradeLevel);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000E2C04 File Offset: 0x000E1004
		public void AddUpgrade(UpgradeComponent upgradeCompPrefab, int upgradeLevel)
		{
			UpgradeComponent upgradeComponent = base.gameObject.InstantiateChild(upgradeCompPrefab, null);
			upgradeComponent.DoSquadSpawnAction(this.enSquad, upgradeLevel);
			this.upgrades.Add(upgradeComponent);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000E2C3D File Offset: 0x000E103D
		private void OnDestroy()
		{
			this.upgrades = null;
			this.currentAbility = null;
			this.selectedAbility = null;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000E2C54 File Offset: 0x000E1054
		public IEnumerator<UpgradeComponent> GetEnumerator()
		{
			return this.upgrades.GetEnumerator();
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000E2C66 File Offset: 0x000E1066
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.upgrades.GetEnumerator();
		}

		// Token: 0x040023D8 RID: 9176
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("ability coordinator", EVerbosity.Quiet, 0);

		// Token: 0x040023D9 RID: 9177
		private WeakReference<EnglishSquad> enSquad = new WeakReference<EnglishSquad>(null);

		// Token: 0x040023DA RID: 9178
		private List<UpgradeComponent> upgrades = new List<UpgradeComponent>();
	}
}
