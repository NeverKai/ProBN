using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007F4 RID: 2036
	public class Stack : Singleton<Stack>
	{
		// Token: 0x06003568 RID: 13672 RVA: 0x000E5B54 File Offset: 0x000E3F54
		protected override void Awake()
		{
			base.Awake();
			this.iAllStateChanges = base.GetComponentsInChildren<IAllStateChange>(true);
			this.iOneStateChanges = base.GetComponentsInChildren<IOneStateChange>(true);
			for (int i = 0; i < this.iOneStateChanges.Length; i++)
			{
				this.iOneStateChanges[i].OnStateEnlist();
			}
			this.states = base.GetComponentsInChildren<State>(true);
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000E5BB4 File Offset: 0x000E3FB4
		private void Start()
		{
			if (!Application.isEditor)
			{
				this.stateNormal.SetActive(true);
			}
			this.startState.SetActive(true);
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000E5BD8 File Offset: 0x000E3FD8
		public void BroadcastAllStateChange()
		{
			for (int i = 0; i < this.iAllStateChanges.Length; i++)
			{
				this.iAllStateChanges[i].OnAllStateChange(this);
			}
		}

		// Token: 0x0400243E RID: 9278
		public State startState;

		// Token: 0x0400243F RID: 9279
		public IAllStateChange[] iAllStateChanges;

		// Token: 0x04002440 RID: 9280
		public IOneStateChange[] iOneStateChanges;

		// Token: 0x04002441 RID: 9281
		public State[] states;

		// Token: 0x04002442 RID: 9282
		[Header("Named States")]
		public State stateMeta;

		// Token: 0x04002443 RID: 9283
		public State stateHome;

		// Token: 0x04002444 RID: 9284
		public State stateCampaign;

		// Token: 0x04002445 RID: 9285
		public State stateDebug;

		// Token: 0x04002446 RID: 9286
		public State stateNormal;

		// Token: 0x04002447 RID: 9287
		public State stateLoading;

		// Token: 0x04002448 RID: 9288
		[Header("To Remove")]
		public State stateLevel;
	}
}
