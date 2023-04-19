using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007AC RID: 1964
	public class NavSpotController : MonoBehaviour, ISquadSetup
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000D8C5C File Offset: 0x000D705C
		public EnglishSquad enSquad
		{
			get
			{
				return this._enSquad;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000D8C69 File Offset: 0x000D7069
		public NavSpot navSpot
		{
			get
			{
				return this._navSpot;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000D8C71 File Offset: 0x000D7071
		public bool canMove
		{
			get
			{
				return !this.squadUpgradeManager.IsBlockingMove();
			}
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000D8C84 File Offset: 0x000D7084
		void ISquadSetup.SquadSetup(Squad squad)
		{
			this._enSquad.Target = (squad as EnglishSquad);
			this.squadFormation = base.GetComponent<NavSpotFormationSquad>();
			this.squadPather = base.GetComponent<EnglishPatherSquad>();
			this.squadUpgradeManager = this.enSquad.upgradeManager;
			this.squadPather.onPathTargetChanged += this.OnPathTargetChanged;
			this.SetNavSpot(NavSpot.GetNavSpot(squad.navPos.pos, false), false);
			squad.onAllDead += this.Squad_onAllDead;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000D8D10 File Offset: 0x000D7110
		public bool SetNavSpot(NavSpot newNavSpot, bool showPath = true)
		{
			NavSpot navSpot = this.navSpot;
			if (newNavSpot == navSpot)
			{
				return false;
			}
			if (navSpot && navSpot.occupant == this)
			{
				navSpot.occupant = null;
			}
			if (newNavSpot)
			{
				newNavSpot.occupant = this;
			}
			this._navSpot = newNavSpot;
			this.squadPather.SetPatherTarget(this._navSpot, showPath);
			this.squadFormation.SetNavSpot(this._navSpot);
			return true;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000D8D92 File Offset: 0x000D7192
		private void Squad_onAllDead()
		{
			if (this._navSpot)
			{
				this.ClearFromNavspot();
			}
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000D8DAA File Offset: 0x000D71AA
		private void OnPathTargetChanged(IPathTarget target)
		{
			if (target != this._navSpot)
			{
				this.ClearFromNavspot();
			}
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000D8DBE File Offset: 0x000D71BE
		private void OnDestroy()
		{
			this.ClearFromNavspot();
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000D8DC6 File Offset: 0x000D71C6
		private void ClearFromNavspot()
		{
			if (this._navSpot)
			{
				this._navSpot.occupant = null;
				this._navSpot = null;
			}
		}

		// Token: 0x04002295 RID: 8853
		private WeakReference<EnglishSquad> _enSquad = new WeakReference<EnglishSquad>(null);

		// Token: 0x04002296 RID: 8854
		private NavSpotFormationSquad squadFormation;

		// Token: 0x04002297 RID: 8855
		private EnglishPatherSquad squadPather;

		// Token: 0x04002298 RID: 8856
		private SquadUpgradeManager squadUpgradeManager;

		// Token: 0x04002299 RID: 8857
		private NavSpot _navSpot;
	}
}
