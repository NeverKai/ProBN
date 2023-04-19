using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Voxels.TowerDefense
{
	// Token: 0x020007F7 RID: 2039
	[ConsoleCommandClassCustomizer("States")]
	public class State : MonoBehaviour, IGameSetup
	{
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000E5CC1 File Offset: 0x000E40C1
		// (set) Token: 0x06003570 RID: 13680 RVA: 0x000E5CCE File Offset: 0x000E40CE
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				base.gameObject.SetActive(value);
			}
		}

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06003571 RID: 13681 RVA: 0x000E5CDC File Offset: 0x000E40DC
		// (remove) Token: 0x06003572 RID: 13682 RVA: 0x000E5D14 File Offset: 0x000E4114
		
		public event State.VoidDelegate OnActivate = delegate()
		{
		};

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06003573 RID: 13683 RVA: 0x000E5D4C File Offset: 0x000E414C
		// (remove) Token: 0x06003574 RID: 13684 RVA: 0x000E5D84 File Offset: 0x000E4184
		
		public event State.VoidDelegate OnDeactivate = delegate()
		{
		};

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06003575 RID: 13685 RVA: 0x000E5DBC File Offset: 0x000E41BC
		// (remove) Token: 0x06003576 RID: 13686 RVA: 0x000E5DF4 File Offset: 0x000E41F4
		
		public event State.VoidDelegate OnUpdate = delegate()
		{
		};

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06003577 RID: 13687 RVA: 0x000E5E2C File Offset: 0x000E422C
		// (remove) Token: 0x06003578 RID: 13688 RVA: 0x000E5E64 File Offset: 0x000E4264
		
		public event State.BoolDelegate OnChange = delegate(bool A_0)
		{
		};

		// Token: 0x06003579 RID: 13689 RVA: 0x000E5E9A File Offset: 0x000E429A
		public void Enlist(MonoBehaviour iOneStateChange)
		{
			this.stateListeners.Add(iOneStateChange as IOneStateChange);
			this.stateListenersMono.Add(iOneStateChange);
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000E5EB9 File Offset: 0x000E42B9
		public void OnValidate()
		{
			this.CollectSubStates();
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000E5EC4 File Offset: 0x000E42C4
		void IGameSetup.OnGameAwake()
		{
			this.CollectSubStates();
			for (int i = 0; i < this.stateListeners.Count; i++)
			{
				this.stateListeners[i].OnOneStateChange(Singleton<Stack>.instance, this);
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000E5F0A File Offset: 0x000E430A
		private void Update()
		{
			this.OnUpdate();
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000E5F18 File Offset: 0x000E4318
		[ContextMenu("Collect Sub States")]
		public void CollectSubStates()
		{
			this.children = (from x in base.GetComponentsInChildren<State>(true)
			where x.transform.parent == base.transform
			select x).ToArray<State>();
			for (int i = 0; i < this.children.Length; i++)
			{
				this.children[i].CollectSubStates();
			}
			if (base.transform.parent)
			{
				this.siblings = (from x in base.transform.parent.GetComponentsInChildren<State>(true)
				where x.transform.parent == base.transform.parent && x != this
				select x).ToArray<State>();
				this.parent = base.transform.parent.GetComponent<State>();
			}
			else
			{
				this.siblings = new State[0];
				this.parent = null;
			}
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000E5FDE File Offset: 0x000E43DE
		[ContextMenu("SetActive(True)")]
		public void SetActiveTrue()
		{
			this.SetActive(true);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000E5FE7 File Offset: 0x000E43E7
		[ContextMenu("SetActive(False)")]
		public void SetActiveFalse()
		{
			this.SetActive(false);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000E5FF0 File Offset: 0x000E43F0
		public void Toggle()
		{
			this.SetActive(!this.active);
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000E6001 File Offset: 0x000E4401
		public void Reboot()
		{
			this.SetActive(false);
			this.SetActive(true);
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000E6011 File Offset: 0x000E4411
		[ConsoleCommand("")]
		public void SetActive(bool newActive)
		{
			if (this.SetActiveInternal(newActive))
			{
				Singleton<Stack>.instance.BroadcastAllStateChange();
			}
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000E602C File Offset: 0x000E442C
		private bool SetActiveInternal(bool newActive)
		{
			if (newActive == this.active)
			{
				return false;
			}
			this.active = newActive;
			if (this.active)
			{
				if (this.exclusive)
				{
					for (int i = 0; i < this.siblings.Length; i++)
					{
						this.siblings[i].SetActiveInternal(false);
					}
				}
				if (this.parent)
				{
					this.parent.SetActiveInternal(true);
				}
				this.OnActivate();
				this.onActivate.Invoke();
				this.OnUpdate();
			}
			else
			{
				for (int j = 0; j < this.children.Length; j++)
				{
					this.children[j].SetActiveInternal(false);
				}
				this.OnDeactivate();
				this.onDeactivate.Invoke();
			}
			for (int k = 0; k < this.stateListeners.Count; k++)
			{
				this.stateListeners[k].OnOneStateChange(Singleton<Stack>.instance, this);
			}
			this.OnChange(this.active);
			return true;
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000E6154 File Offset: 0x000E4554
		public static bool GetCompliance(List<State.Rule> rules)
		{
			for (int i = 0; i < rules.Count; i++)
			{
				if (!rules[i].valid)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002449 RID: 9289
		public bool exclusive;

		// Token: 0x0400244A RID: 9290
		public State[] children;

		// Token: 0x0400244B RID: 9291
		public State[] siblings;

		// Token: 0x0400244C RID: 9292
		public State parent;

		// Token: 0x0400244D RID: 9293
		public UnityEvent onActivate;

		// Token: 0x0400244E RID: 9294
		public UnityEvent onDeactivate;

		// Token: 0x0400244F RID: 9295
		private List<IOneStateChange> stateListeners = new List<IOneStateChange>();

		// Token: 0x04002450 RID: 9296
		public List<MonoBehaviour> stateListenersMono = new List<MonoBehaviour>();

		// Token: 0x020007F8 RID: 2040
		// (Invoke) Token: 0x0600358C RID: 13708
		public delegate void VoidDelegate();

		// Token: 0x020007F9 RID: 2041
		// (Invoke) Token: 0x06003590 RID: 13712
		public delegate void BoolDelegate(bool active);

		// Token: 0x020007FA RID: 2042
		[Serializable]
		public class Rule
		{
			// Token: 0x06003594 RID: 13716 RVA: 0x000E61EC File Offset: 0x000E45EC
			public void OnValidate()
			{
				this.text = this.type.ToString() + " ";
				List<string> list = new List<string>();
				for (int i = 0; i < this.states.Length; i++)
				{
					if (this.states[i])
					{
						list.Add(this.states[i].name);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (j > 0)
					{
						this.text += " OR ";
					}
					this.text += list[j];
				}
			}

			// Token: 0x170007B4 RID: 1972
			// (get) Token: 0x06003595 RID: 13717 RVA: 0x000E62AC File Offset: 0x000E46AC
			public bool valid
			{
				get
				{
					State.Rule.Type type = this.type;
					if (type == State.Rule.Type.Needs)
					{
						for (int i = 0; i < this.states.Length; i++)
						{
							if (this.states[i] && this.states[i].active)
							{
								return true;
							}
						}
						return false;
					}
					if (type != State.Rule.Type.CantHave)
					{
						return true;
					}
					for (int j = 0; j < this.states.Length; j++)
					{
						if (this.states[j] && this.states[j].active)
						{
							return false;
						}
					}
					return true;
				}
			}

			// Token: 0x04002459 RID: 9305
			[HideInInspector]
			public string text;

			// Token: 0x0400245A RID: 9306
			public State.Rule.Type type;

			// Token: 0x0400245B RID: 9307
			public State[] states = new State[0];

			// Token: 0x020007FB RID: 2043
			public enum Type
			{
				// Token: 0x0400245D RID: 9309
				Needs,
				// Token: 0x0400245E RID: 9310
				CantHave
			}
		}
	}
}
