using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.SetRules
{
	// Token: 0x02000674 RID: 1652
	[RequireComponent(typeof(ModuleSet))]
	public abstract class SetRule : MonoBehaviour
	{
		// Token: 0x06002A13 RID: 10771 RVA: 0x00082D26 File Offset: 0x00081126
		public virtual void OnPreProcess(Module module)
		{
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00082D28 File Offset: 0x00081128
		public ModuleSet moduleSet
		{
			get
			{
				return this._moduleSet;
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00082D30 File Offset: 0x00081130
		private void Awake()
		{
			this._moduleSet = base.GetComponent<ModuleSet>();
		}

		// Token: 0x06002A16 RID: 10774
		public abstract void GetRules(MultiWave multiwave, List<Wrapper> wrappers);

		// Token: 0x06002A17 RID: 10775 RVA: 0x00082D40 File Offset: 0x00081140
		protected bool RemoveRemaining(MultiWave multiWave)
		{
			for (int i = 0; i < multiWave.allDominos.Count; i++)
			{
				Domino domino = multiWave.allDominos[i];
				if (domino.state == Domino.State.idle && this.moduleSet.ContainsModule(domino.placement.firstModule) && !multiWave.RemoveDomino(domino))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001B75 RID: 7029
		private ModuleSet _moduleSet;
	}
}
