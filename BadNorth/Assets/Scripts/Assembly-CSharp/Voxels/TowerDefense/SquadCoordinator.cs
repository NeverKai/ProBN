using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007E7 RID: 2023
	public abstract class SquadCoordinator : MonoBehaviour
	{
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000D7376 File Offset: 0x000D5776
		public Squad squad
		{
			get
			{
				return this._squad;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000D7383 File Offset: 0x000D5783
		public EnglishSquad enSquad
		{
			get
			{
				return this._enSquad;
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000D7390 File Offset: 0x000D5790
		public virtual void Setup(Squad squad)
		{
			this._squad.Target = squad;
			this._enSquad.Target = (squad as EnglishSquad);
			squad.onAgentCreated += this.OnAgentCreated;
			squad.onAgentRemoved += this.OnAgentRemoved;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000D73E0 File Offset: 0x000D57E0
		protected virtual void OnAgentCreated(Agent agent)
		{
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000D73E2 File Offset: 0x000D57E2
		protected virtual void OnAgentRemoved(Agent agent)
		{
		}

		// Token: 0x040023D4 RID: 9172
		private WeakReference<Squad> _squad = new WeakReference<Squad>(null);

		// Token: 0x040023D5 RID: 9173
		private WeakReference<EnglishSquad> _enSquad = new WeakReference<EnglishSquad>(null);
	}
}
