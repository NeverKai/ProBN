using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	[Serializable]
	public class AgentStateRoot
	{
		public AgentStateRoot(int capacity = 4)
		{
			this.capacity = capacity;
		}

		public void SetDirty()
		{
			this.dirty = true;
		}

		public void MaybeSetup()
		{
			if (this._rootState == null)
			{
				this._rootState = new AgentState(this);
				this.activeStates = new List<AgentState>(capacity);
			}
		}

		public AgentState rootState
		{
			get
			{
				this.MaybeSetup();
				return this._rootState;
			}
		}

		public void ForceDrawUpdate()
		{
			this.updateHash++;
		}

		public void Update()
		{
			if (this.dirty)
			{
				using (new ScopedProfiler("BuildStateList", null))
				{
					this.activeStates.Clear();
					stack.Push(this.rootState);
					while (stack.Count > 0)
					{
						AgentState agentState = stack.Pop();
						if (agentState.active)
						{
							if (agentState.hasUpdate)
							{
								this.activeStates.Add(agentState);
							}
							for (int i = agentState.children.Count - 1; i >= 0; i--)
							{
								stack.Push(agentState.children[i]);
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
						stack.Push(this.rootState);
						while (stack.Count > 0)
						{
							AgentState agentState3 = stack.Pop();
							if (agentState3.active && agentState3.hasUpdate)
							{
								this.activeStates.Add(agentState3);
							}
							for (int k = agentState3.children.Count - 1; k >= 0; k--)
							{
								stack.Push(agentState3.children[k]);
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

		public void UpdateEmpties()
		{
			this.rootState.UpdateEmpties();
		}

		public void OnDestroy()
		{
			if (this._rootState != null)
			{
				this._rootState.OnDestroy();
				this._rootState = null;
				this.activeStates.Clear();
			}
		}

		[SerializeField]
		private int updateHash;

		private int capacity;

		private List<AgentState> activeStates;

		private static Stack<AgentState> stack = new Stack<AgentState>();

		private bool dirty = true;

		private AgentState _rootState;
	}
}
