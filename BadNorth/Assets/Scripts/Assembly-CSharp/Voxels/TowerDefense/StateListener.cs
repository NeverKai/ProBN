using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007FC RID: 2044
	public abstract class StateListener : MonoBehaviour, IGameSetup
	{
		// Token: 0x06003597 RID: 13719 RVA: 0x000C151C File Offset: 0x000BF91C
		public virtual void OnGameAwake()
		{
			for (int i = 0; i < this.rules.Count; i++)
			{
				State.Rule rule = this.rules[i];
				for (int j = 0; j < rule.states.Length; j++)
				{
					if (rule.states[j])
					{
						rule.states[j].OnChange += this.OnOneStateChange;
					}
					else
					{
						Debug.LogError("State Not found " + base.name);
					}
				}
			}
			this.OnOneStateChange(true);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000C15B8 File Offset: 0x000BF9B8
		public void OnOneStateChange(bool b)
		{
			bool compliance = State.GetCompliance(this.rules);
			if (this.first || this.active != compliance)
			{
				this.active = compliance;
				this.OnActiveChange(this.active);
				this.first = false;
			}
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000C1602 File Offset: 0x000BFA02
		public virtual void OnActiveChange(bool active)
		{
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000C1604 File Offset: 0x000BFA04
		private void OnValidate()
		{
			for (int i = 0; i < this.rules.Count; i++)
			{
				this.rules[i].OnValidate();
			}
		}

		// Token: 0x0400245F RID: 9311
		public List<State.Rule> rules = new List<State.Rule>
		{
			new State.Rule()
		};

		// Token: 0x04002460 RID: 9312
		public bool first = true;

		// Token: 0x04002461 RID: 9313
		public bool active = true;
	}
}
