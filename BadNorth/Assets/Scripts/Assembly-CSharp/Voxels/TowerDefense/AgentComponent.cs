using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000680 RID: 1664
	public abstract class AgentComponent : MonoBehaviour, IAgentSetup
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0005EF17 File Offset: 0x0005D317
		public Agent agent
		{
			get
			{
				if (!this._agent)
				{
					this._agent.Target = base.gameObject.GetComponentInParentIncludingInactive<Agent>();
				}
				return this._agent;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x0005EF4A File Offset: 0x0005D34A
		public Squad squad
		{
			get
			{
				return this.agent.squad;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0005EF57 File Offset: 0x0005D357
		public EnglishSquad enSquad
		{
			get
			{
				if (!this._enSquad)
				{
					this._enSquad.Target = (this.squad as EnglishSquad);
				}
				return this._enSquad;
			}
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x0005EF8A File Offset: 0x0005D38A
		public virtual void Setup()
		{
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0005EF8C File Offset: 0x0005D38C
		public void SetEnabled(bool value)
		{
			base.enabled = value;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0005EF95 File Offset: 0x0005D395
		public void Setup(Agent agent)
		{
			this._agent.Target = agent;
			this.Setup();
		}

		// Token: 0x04001BC1 RID: 7105
		private WeakReference<Agent> _agent = new WeakReference<Agent>(null);

		// Token: 0x04001BC2 RID: 7106
		private WeakReference<EnglishSquad> _enSquad = new WeakReference<EnglishSquad>(null);
	}
}
