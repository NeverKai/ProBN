using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007F3 RID: 2035
	public class AgentState
	{
		// Token: 0x06003539 RID: 13625 RVA: 0x000E5250 File Offset: 0x000E3650
		public AgentState(AgentStateRoot stateRoot)
		{
			this.name = "Root";
			this.nameCache = "AgentState_" + this.name;
			this.SetTimestamps();
			this.parent = null;
			this.exclusive = false;
			this.active = false;
			this.active = true;
			this.stateRoot = stateRoot;
			this.UpdateDraw();
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000E5378 File Offset: 0x000E3778
		public AgentState(string name, AgentStateRoot stateRoot, bool active, bool exclusive) : this(name, stateRoot.rootState, active, exclusive)
		{
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000E538C File Offset: 0x000E378C
		public AgentState(string name, AgentState parentState, bool active, bool exclusive)
		{
			this.name = name;
			this.nameCache = "AgentState_" + name;
			this.SetTimestamps();
			this.parent = parentState;
			this.stateRoot = this.rootState.stateRoot;
			this.exclusive = exclusive;
			this.active = false;
			this.active = active;
			this.UpdateDraw();
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000E54B6 File Offset: 0x000E38B6
		// (set) Token: 0x0600353D RID: 13629 RVA: 0x000E54BE File Offset: 0x000E38BE
		public string name { get; private set; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000E54C7 File Offset: 0x000E38C7
		// (set) Token: 0x0600353F RID: 13631 RVA: 0x000E54CF File Offset: 0x000E38CF
		public bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				this.SetActive(value);
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000E54D9 File Offset: 0x000E38D9
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000E54E1 File Offset: 0x000E38E1
		public float timeStamp { get; private set; }

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000E54EA File Offset: 0x000E38EA
		public float timeSinceChange
		{
			get
			{
				return Time.time - this.timeStamp;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000E54F8 File Offset: 0x000E38F8
		public float timeSinceActivation
		{
			get
			{
				return (!this.active) ? 0f : this.timeSinceChange;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000E5515 File Offset: 0x000E3915
		public float timeSinceDeactivation
		{
			get
			{
				return (!this.active) ? this.timeSinceChange : 0f;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000E5532 File Offset: 0x000E3932
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x000E553A File Offset: 0x000E393A
		public float unscaledTimeStamp { get; private set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000E5543 File Offset: 0x000E3943
		public float unscaledTimeSinceChange
		{
			get
			{
				return Time.unscaledTime - this.unscaledTimeStamp;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000E5551 File Offset: 0x000E3951
		public float unscaledTimeSinceActivation
		{
			get
			{
				return (!this.active) ? 0f : this.unscaledTimeSinceChange;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x000E556E File Offset: 0x000E396E
		public float unscaledTimeSinceDeactivation
		{
			get
			{
				return (!this.active) ? this.unscaledTimeSinceChange : 0f;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000E558B File Offset: 0x000E398B
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000E5594 File Offset: 0x000E3994
		public AgentState parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				if (this._parent == value)
				{
					return;
				}
				if (this._parent != null)
				{
					this._parent.children.Remove(this);
				}
				this._parent = value;
				if (this._parent != null)
				{
					this._parent.children.Add(this);
				}
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000E55EE File Offset: 0x000E39EE
		public List<AgentState> siblings
		{
			get
			{
				return this.parent.children;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000E55FB File Offset: 0x000E39FB
		// (set) Token: 0x0600354E RID: 13646 RVA: 0x000E5603 File Offset: 0x000E3A03
		public bool hasUpdate { get; private set; }

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x0600354F RID: 13647 RVA: 0x000E560C File Offset: 0x000E3A0C
		// (remove) Token: 0x06003550 RID: 13648 RVA: 0x000E5658 File Offset: 0x000E3A58
		public event Action OnUpdate
		{
			add
			{
				this._OnUpdate = (Action)Delegate.Combine(this._OnUpdate, value);
				if (!this.hasUpdate)
				{
					if (this.stateRoot != null)
					{
						this.stateRoot.SetDirty();
					}
					this.hasUpdate = true;
				}
			}
			remove
			{
				this._OnUpdate = (Action)Delegate.Remove(this._OnUpdate, value);
			}
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000E5671 File Offset: 0x000E3A71
		[ContextMenu("SetActive(true)")]
		public void SetActiveTrue()
		{
			this.SetActive(true);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000E567B File Offset: 0x000E3A7B
		[ContextMenu("SetActive(false)")]
		public void SetActiveFalse()
		{
			this.SetActive(false);
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000E5685 File Offset: 0x000E3A85
		public AgentState rootState
		{
			get
			{
				return (this.parent != null) ? this.parent.rootState : this;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000E56A4 File Offset: 0x000E3AA4
		public bool hasActiveChildren
		{
			get
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i].active)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000E56E8 File Offset: 0x000E3AE8
		public bool hasActiveExclusiveChildren
		{
			get
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					if (this.children[i].active && this.children[i].exclusive)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000E5740 File Offset: 0x000E3B40
		public void OnDestroy()
		{
			for (int i = this.children.Count - 1; i >= 0; i--)
			{
				this.children[i].OnDestroy();
			}
			this.parent = null;
			this.OnEmpty = null;
			this.OnActivate = null;
			this.OnDeactivate = null;
			this._OnUpdate = null;
			this.OnChange = null;
			this.children.Clear();
			this.children = null;
			this.OnDebugString.Clear();
			this.OnDebugString = null;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000E57CC File Offset: 0x000E3BCC
		public void UpdateEmpties()
		{
			if (this._active)
			{
				this.MaybeEmpty();
				foreach (AgentState agentState in this.children)
				{
					agentState.UpdateEmpties();
				}
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000E5838 File Offset: 0x000E3C38
		public bool SetActive(bool newActive)
		{
			if (newActive == this._active)
			{
				return false;
			}
			this.SetActiveInternal(newActive);
			if (this.stateRoot != null)
			{
				this.stateRoot.SetDirty();
			}
			if (newActive)
			{
				this.MaybeEmpty();
			}
			else if (this.parent != null && this.parent.active)
			{
				this.parent.MaybeEmpty();
			}
			this.UpdateDraw();
			return true;
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000E58AE File Offset: 0x000E3CAE
		private void UpdateDraw()
		{
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000E58B0 File Offset: 0x000E3CB0
		private void SetTimestamps()
		{
			this.timeStamp = Time.time;
			this.unscaledTimeStamp = Time.unscaledTime;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000E58C8 File Offset: 0x000E3CC8
		private void SetActiveInternal(bool newActive)
		{
			if (newActive == this._active)
			{
				return;
			}
			this.SetTimestamps();
			if (newActive)
			{
				if (this.exclusive && this.parent == null)
				{
					Debug.LogError("exclusive && !parent " + this.name);
				}
				if (this.exclusive)
				{
					for (int i = 0; i < this.siblings.Count; i++)
					{
						if (this.siblings[i] != this && this.siblings[i].exclusive)
						{
							this.siblings[i].SetActiveInternal(false);
						}
					}
				}
				if (this.parent != null)
				{
					this.parent.SetActiveInternal(true);
				}
				this._active = true;
				this.OnActivate();
				this.OnChange(true);
			}
			else
			{
				for (int j = 0; j < this.children.Count; j++)
				{
					this.children[j].SetActiveInternal(false);
				}
				this._active = false;
				this.OnDeactivate();
				this.OnChange(false);
			}
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000E5A00 File Offset: 0x000E3E00
		public void SubscribeToActivate(Action onActivate)
		{
			if (this.active)
			{
				onActivate();
			}
			this.OnActivate = (Action)Delegate.Combine(this.OnActivate, onActivate);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000E5A2A File Offset: 0x000E3E2A
		public void Subscribe(Action<bool> onChange)
		{
			onChange(this.active);
			this.OnChange = (Action<bool>)Delegate.Combine(this.OnChange, onChange);
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000E5A4F File Offset: 0x000E3E4F
		private void MaybeEmpty()
		{
			if (!this.hasActiveExclusiveChildren)
			{
				this.OnEmpty();
			}
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000E5A67 File Offset: 0x000E3E67
		public override string ToString()
		{
			return this.nameCache;
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000E5A70 File Offset: 0x000E3E70
		public void DirectUpdate()
		{
			using (new ScopedProfiler(this.nameCache, null))
			{
				this._OnUpdate();
			}
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000E5ABC File Offset: 0x000E3EBC
		public void Update()
		{
			if (this._active)
			{
				using (new ScopedProfiler(this.nameCache, null))
				{
					this._OnUpdate();
				}
				for (int i = 0; i < this.children.Count; i++)
				{
					this.children[i].Update();
				}
			}
		}

		// Token: 0x0400242A RID: 9258
		private string nameCache;

		// Token: 0x0400242B RID: 9259
		private bool _active;

		// Token: 0x0400242C RID: 9260
		public bool exclusive;

		// Token: 0x0400242F RID: 9263
		[Header("References")]
		private AgentState _parent;

		// Token: 0x04002430 RID: 9264
		public List<AgentState> children = new List<AgentState>();

		// Token: 0x04002431 RID: 9265
		public Action OnEmpty = delegate()
		{
		};

		// Token: 0x04002432 RID: 9266
		public Action OnActivate = delegate()
		{
		};

		// Token: 0x04002433 RID: 9267
		public Action OnDeactivate = delegate()
		{
		};

		// Token: 0x04002434 RID: 9268
		public Action<bool> OnChange = delegate(bool A_0)
		{
		};

		// Token: 0x04002436 RID: 9270
		public Action _OnUpdate = delegate()
		{
		};

		// Token: 0x04002437 RID: 9271
		public List<Func<string>> OnDebugString = new List<Func<string>>();

		// Token: 0x04002438 RID: 9272
		public readonly AgentStateRoot stateRoot;
	}
}
