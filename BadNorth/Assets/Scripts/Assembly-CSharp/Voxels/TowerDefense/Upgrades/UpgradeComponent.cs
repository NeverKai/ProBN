using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200083C RID: 2108
	public abstract class UpgradeComponent : MonoBehaviour
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000EB9BA File Offset: 0x000E9DBA
		public EnglishSquad squad
		{
			get
			{
				return this._squad;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000EB9C7 File Offset: 0x000E9DC7
		public Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000EB9D4 File Offset: 0x000E9DD4
		// (set) Token: 0x060036DB RID: 14043 RVA: 0x000EB9DC File Offset: 0x000E9DDC
		public int upgradeLevel { get; private set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x000EB9E5 File Offset: 0x000E9DE5
		// (set) Token: 0x060036DD RID: 14045 RVA: 0x000EB9ED File Offset: 0x000E9DED
		public string nameCache { get; private set; }

		// Token: 0x060036DE RID: 14046 RVA: 0x000EB9F6 File Offset: 0x000E9DF6
		public void DoSquadSpawnAction(EnglishSquad squad, int upgradeLevel)
		{
			this._squad = squad;
			this._island = squad.faction.island;
			this.upgradeLevel = upgradeLevel;
			this.nameCache = base.name;
			this.DoSquadSpawnAction_Implementation();
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000EBA33 File Offset: 0x000E9E33
		public virtual void LevelEnded()
		{
		}

		// Token: 0x060036E0 RID: 14048
		protected abstract void DoSquadSpawnAction_Implementation();

		// Token: 0x04002543 RID: 9539
		private WeakReference<EnglishSquad> _squad = new WeakReference<EnglishSquad>(null);

		// Token: 0x04002544 RID: 9540
		private WeakReference<Island> _island = new WeakReference<Island>(null);
	}
}
