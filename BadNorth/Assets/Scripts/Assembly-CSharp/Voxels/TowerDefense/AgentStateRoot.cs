using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007F2 RID: 2034
	[Serializable]
	public class AgentStateRoot
	{
		// Token: 0x06003530 RID: 13616 RVA: 0x000E4FA3 File Offset: 0x000E33A3
		public AgentStateRoot(int capacity = 4)
		{
			this.capacity = capacity;
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000E4FB9 File Offset: 0x000E33B9
		public void SetDirty()
		{
			this.dirty = true;
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000E4FC2 File Offset: 0x000E33C2
		public void MaybeSetup()
		{
			if (this._rootState == null)
			{
				this._rootState = new AgentState(this);
				this.activeStates = new List<AgentState>(this.capacity);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000E4FEC File Offset: 0x000E33EC
		public AgentState rootState
		{
			get
			{
				this.MaybeSetup();
				return this._rootState;
			}
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000E4FFA File Offset: 0x000E33FA
		public void ForceDrawUpdate()
		{
			this.updateHash++;
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000E500C File Offset: 0x000E340C
		public void Update()
		{
			if (this.dirty)
			{
				using (new ScopedProfiler("BuildStateList", null))
				{
					this.activeStates.Clear();
					AgentStateRoot.stack.Push(this.rootState);
					while (AgentStateRoot.stack.Count > 0)
					{
						AgentState agentState = AgentStateRoot.stack.Pop();
						if (agentState.active)
						{
							if (agentState.hasUpdate)
							{
								this.activeStates.Add(agentState);
							}
							for (int i = agentState.children.Count - 1; i >= 0; i--)
							{
								AgentStateRoot.stack.Push(agentState.children[i]);
							}
						}
					}
					this.dirty = false;
				}
			}
			for (int j = 0; j < this.activeStates.Count; j++)
			{
				AgentState agentState2 = this.activeStates[j];
				agentState2.DirectUpdate();
				if (this.dirty)
				{
					using (new ScopedProfiler("RebuildStateList", null))
					{
						this.activeStates.Clear();
						AgentStateRoot.stack.Push(this.rootState);
						while (AgentStateRoot.stack.Count > 0)
						{
							AgentState agentState3 = AgentStateRoot.stack.Pop();
							if (agentState3.active && agentState3.hasUpdate)
							{
								this.activeStates.Add(agentState3);
							}
							for (int k = agentState3.children.Count - 1; k >= 0; k--)
							{
								AgentStateRoot.stack.Push(agentState3.children[k]);
							}
							if (agentState3 == agentState2)
							{
								j = this.activeStates.Count;
							}
						}
						this.dirty = false;
					}
				}
			}
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000E520C File Offset: 0x000E360C
		public void UpdateEmpties()
		{
			this.rootState.UpdateEmpties();
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000E5219 File Offset: 0x000E3619
		public void OnDestroy()
		{
			if (this._rootState != null)
			{
				this._rootState.OnDestroy();
				this._rootState = null;
				this.activeStates.Clear();
			}
		}

		// Token: 0x04002423 RID: 9251
		[SerializeField]
		private int updateHash;

		// Token: 0x04002424 RID: 9252
		private int capacity;

		// Token: 0x04002425 RID: 9253
		private List<AgentState> activeStates;

		// Token: 0x04002426 RID: 9254
		private static Stack<AgentState> stack = new Stack<AgentState>();

		// Token: 0x04002427 RID: 9255
		private bool dirty = true;

		// Token: 0x04002428 RID: 9256
		private AgentState _rootState;
	}
}
