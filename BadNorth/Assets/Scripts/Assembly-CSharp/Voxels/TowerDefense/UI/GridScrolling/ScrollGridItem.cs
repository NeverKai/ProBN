using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.GridScrolling
{
	// Token: 0x02000922 RID: 2338
	[RequireComponent(typeof(RectTransform))]
	public class ScrollGridItem : UIBehaviour, IComparable<ScrollGridItem>
	{
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x0011BC5C File Offset: 0x0011A05C
		// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x0011BC64 File Offset: 0x0011A064
		public ScrollGrid grid
		{
			get
			{
				return this._grid;
			}
			set
			{
				this._grid = value;
				this.hasGrid.SetActive(value);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x0011BC7F File Offset: 0x0011A07F
		private AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x0011BC8C File Offset: 0x0011A08C
		// (set) Token: 0x06003ED7 RID: 16087 RVA: 0x0011BC94 File Offset: 0x0011A094
		public AgentState inFocusWindow { get; private set; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x0011BC9D File Offset: 0x0011A09D
		// (set) Token: 0x06003ED9 RID: 16089 RVA: 0x0011BCA5 File Offset: 0x0011A0A5
		public AgentState onGrid { get; private set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x0011BCAE File Offset: 0x0011A0AE
		// (set) Token: 0x06003EDB RID: 16091 RVA: 0x0011BCB6 File Offset: 0x0011A0B6
		public AnimatedState hasGrid { get; private set; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x0011BCBF File Offset: 0x0011A0BF
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x0011BCC7 File Offset: 0x0011A0C7
		public TargetAnimator visAnim { get; private set; }

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x0011BCD0 File Offset: 0x0011A0D0
		// (set) Token: 0x06003EDF RID: 16095 RVA: 0x0011BCD8 File Offset: 0x0011A0D8
		public TargetAnimator marginAnim { get; private set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x0011BCE1 File Offset: 0x0011A0E1
		// (set) Token: 0x06003EE1 RID: 16097 RVA: 0x0011BCE9 File Offset: 0x0011A0E9
		public float visibility { get; private set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x0011BCF2 File Offset: 0x0011A0F2
		// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x0011BCFA File Offset: 0x0011A0FA
		public float margin { get; private set; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x0011BD03 File Offset: 0x0011A103
		// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x0011BD0B File Offset: 0x0011A10B
		public float focus { get; private set; }

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0011BD14 File Offset: 0x0011A114
		public bool MaybeSetFocus()
		{
			if (this.grid)
			{
				this.grid.SetFocus(this);
				return true;
			}
			return false;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0011BD35 File Offset: 0x0011A135
		protected override void Awake()
		{
			this.MaybeInitialize();
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0011BD40 File Offset: 0x0011A140
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			Action<float> setFunc = delegate(float x)
			{
				this.visibility = x;
				canvasGroup.interactable = (x > 0.5f);
				canvasGroup.blocksRaycasts = (x > 0.5f);
			};
			this.moveAnim = new TargetAnimator<Vector2>("Move", () => this.transform.localPosition, delegate(Vector2 x)
			{
				this.transform.localPosition = x;
			}, this.rootState, LerpTowards2.standard);
			this.visAnim = new TargetAnimator("Visibility", new Func<float>(this.get_visibility), setFunc, this.rootState, LerpTowards.standard);
			this.marginAnim = new TargetAnimator("Margin", new Func<float>(this.get_margin), delegate(float x)
			{
				this.margin = x;
			}, this.rootState, LerpTowards.standard);
			this.hasGrid = new AnimatedState("HasGrid", this.rootState, false, false);
			this.onGrid = new AgentState("OnGrid", this.hasGrid, false, false);
			this.inFocusWindow = new AgentState("InFocusWindow", this.onGrid, false, false);
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x0011BE5C File Offset: 0x0011A25C
		private void Update()
		{
			this.rootState.Update();
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x0011BE6C File Offset: 0x0011A26C
		public void Uproot()
		{
			this.moveAnim.SetTarget(this.moveAnim.current, null, null, null, 0f, null);
			this.visAnim.SetTarget(this.visAnim.current, null, null, null, 0f, null);
			this.marginAnim.SetTarget(this.marginAnim.current, null, null, null, 0f, null);
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x0011BED6 File Offset: 0x0011A2D6
		public void Downroot()
		{
			this.moveAnim.ForceToTarget();
			this.visAnim.ForceToTarget();
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x0011BEEE File Offset: 0x0011A2EE
		public void SetGridPos(Vector2 pos)
		{
			this.moveAnim.SetTargetOrCurrent(pos);
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x0011BEFC File Offset: 0x0011A2FC
		public void SetVisibility(float newVisibility)
		{
			this.visAnim.SetTargetOrCurrent(newVisibility);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x0011BF0A File Offset: 0x0011A30A
		public void SetMargin(float newMargin)
		{
			this.marginAnim.SetTargetOrCurrent(newMargin);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x0011BF18 File Offset: 0x0011A318
		int IComparable<ScrollGridItem>.CompareTo(ScrollGridItem other)
		{
			return this.sortOrder.CompareTo(other.sortOrder);
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x0011BF2C File Offset: 0x0011A32C
		private void OnDrawGizmos()
		{
			if (this.moveAnim != null)
			{
				Gizmos.matrix = base.transform.parent.localToWorldMatrix;
				Gizmos.DrawLine(this.moveAnim.current, this.moveAnim.target);
			}
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x0011BF7E File Offset: 0x0011A37E
		public void ForceToTarget()
		{
			this.moveAnim.ForceToTarget();
			this.visAnim.ForceToTarget();
			this.marginAnim.ForceToTarget();
		}

		// Token: 0x04002BE7 RID: 11239
		private ScrollGrid _grid;

		// Token: 0x04002BE8 RID: 11240
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002BE9 RID: 11241
		private Vector2 endPos;

		// Token: 0x04002BEA RID: 11242
		private Vector2 startPos;

		// Token: 0x04002BEE RID: 11246
		private float move;

		// Token: 0x04002BEF RID: 11247
		public TargetAnimator<Vector2> moveAnim;

		// Token: 0x04002BF5 RID: 11253
		public float sortOrder;

		// Token: 0x04002BF6 RID: 11254
		private bool initialized;
	}
}
