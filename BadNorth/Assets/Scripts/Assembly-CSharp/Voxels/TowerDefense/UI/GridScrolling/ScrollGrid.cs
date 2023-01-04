using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.GridScrolling
{
	// Token: 0x0200091D RID: 2333
	[RequireComponent(typeof(RectTransform))]
	public class ScrollGrid : UIBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x0011A62E File Offset: 0x00118A2E
		private Vector2 forward
		{
			get
			{
				return (this.direction != ScrollGrid.Direction.Horizontal) ? Vector2.down : Vector2.right;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x0011A64A File Offset: 0x00118A4A
		private Vector2 tangent
		{
			get
			{
				return ExtraMath.Rotate2D90(this.forward);
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x0011A657 File Offset: 0x00118A57
		// (set) Token: 0x06003E96 RID: 16022 RVA: 0x0011A65F File Offset: 0x00118A5F
		public int windowMin { get; private set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003E97 RID: 16023 RVA: 0x0011A668 File Offset: 0x00118A68
		// (set) Token: 0x06003E98 RID: 16024 RVA: 0x0011A670 File Offset: 0x00118A70
		public int windowMax { get; private set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x0011A679 File Offset: 0x00118A79
		private float windowCenter
		{
			get
			{
				return (float)(this.windowMin + this.windowMax) / 2f;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003E9A RID: 16026 RVA: 0x0011A68F File Offset: 0x00118A8F
		// (set) Token: 0x06003E9B RID: 16027 RVA: 0x0011A6C6 File Offset: 0x00118AC6
		private float relativeOffsetTarget
		{
			get
			{
				return (this.items.Count != 0) ? (((float)this.offsetTarget - this.windowCenter) / (float)this.items.Count) : 0f;
			}
			set
			{
				this.offsetTarget = Mathf.RoundToInt(value * (float)this.items.Count + this.windowCenter);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003E9C RID: 16028 RVA: 0x0011A6E8 File Offset: 0x00118AE8
		// (set) Token: 0x06003E9D RID: 16029 RVA: 0x0011A71E File Offset: 0x00118B1E
		private float relativeOffsetCurrrent
		{
			get
			{
				return (this.items.Count != 0) ? ((this.offsetCurrent - this.windowCenter) / (float)this.items.Count) : 0f;
			}
			set
			{
				this.offsetCurrent = value * (float)this.items.Count + this.windowCenter;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x0011A73B File Offset: 0x00118B3B
		// (set) Token: 0x06003E9F RID: 16031 RVA: 0x0011A744 File Offset: 0x00118B44
		public int offsetTarget
		{
			get
			{
				return this._offsetTarget;
			}
			set
			{
				this._offsetTarget = Mathf.Min(Math.Max(value, this.windowMax - this.items.Count + 1), this.windowMin);
				this.offsetAnim.SetTarget((float)this._offsetTarget, null, null, null, 0f, null);
				this.SetFocusAndWindow(this._offsetTarget);
				this.onSetOffsetTarget(this._offsetTarget);
				this.UpdateHitbox();
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x0011A7BA File Offset: 0x00118BBA
		// (set) Token: 0x06003EA1 RID: 16033 RVA: 0x0011A7C4 File Offset: 0x00118BC4
		private float offsetCurrent
		{
			get
			{
				return this._offsetCurrent;
			}
			set
			{
				this.MaybeInitialize();
				this._offsetCurrent = value;
				for (int i = 0; i < this.items.Count; i++)
				{
					ScrollGridItem scrollGridItem = this.items[i];
					float num = (float)i + value;
					scrollGridItem.SetGridPos(this.SamplePos(num));
					scrollGridItem.SetVisibility(this.SampleVisibility(num));
					scrollGridItem.SetMargin(this.SampleMargin(num));
					if (num > (float)this.windowMax)
					{
						scrollGridItem.transform.SetAsFirstSibling();
					}
				}
				for (int j = this.items.Count - 1; j >= 0; j--)
				{
					ScrollGridItem scrollGridItem2 = this.items[j];
					float num2 = (float)j + value;
					if (num2 < (float)this.windowMin)
					{
						scrollGridItem2.transform.SetAsFirstSibling();
					}
				}
			}
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x0011A898 File Offset: 0x00118C98
		private void UpdateHitbox()
		{
			if (this.items.Count > 0)
			{
				Vector2 lhs = this.SamplePos((float)this._offsetTarget);
				Vector2 rhs = this.SamplePos((float)(this._offsetTarget + this.items.Count - 1));
				Vector2 vector = Vector2.Min(lhs, rhs) - Vector2.one * 100f;
				Vector2 vector2 = Vector2.Max(lhs, rhs) + Vector2.one * 100f;
				this.graphic.rectTransform.localPosition = (vector + vector2) * 0.5f;
				this.graphic.rectTransform.sizeDelta = vector2 - vector;
			}
			else
			{
				this.graphic.rectTransform.sizeDelta = Vector2.zero;
			}
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0011A970 File Offset: 0x00118D70
		private void SetFocusAndWindow(int offset)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				ScrollGridItem scrollGridItem = this.items[i];
				int num = i + offset;
				scrollGridItem.onGrid.SetActive(num >= 0 && num < this.slots.Count);
				scrollGridItem.inFocusWindow.SetActive(num >= this.windowMin && num <= this.windowMax);
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x0011A9F5 File Offset: 0x00118DF5
		private AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x0011AA04 File Offset: 0x00118E04
		private void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			this.contents = new AgentState("Contents", this.rootState, false, false);
			this.graphic = base.gameObject.AddEmptyChild("Hitbox").AddComponent<EmptyGraphic>();
			this.graphic.rectTransform.anchorMin = Vector2.one / 2f;
			this.graphic.rectTransform.anchorMax = Vector2.one / 2f;
			this.graphic.transform.SetAsFirstSibling();
			this.contents.OnUpdate += delegate()
			{
				if (this.firstFrameWithContent)
				{
					this.firstFrameWithContent = false;
					this.offsetTarget = this.offsetTarget;
					this.offsetAnim.ForceToTarget();
				}
				if (this.firstFrameSinceRemove)
				{
					this.firstFrameSinceRemove = false;
					foreach (ScrollGridItem scrollGridItem in this.items)
					{
						scrollGridItem.Uproot();
					}
					this.offsetAnim.ForceToTarget();
				}
			};
			AgentState agentState = this.contents;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				this._offsetTarget = 0;
				this.firstFrameWithContent = true;
				this.graphic.raycastTarget = true;
			}));
			AgentState agentState2 = this.contents;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				this.graphic.raycastTarget = false;
				this._offsetTarget = 0;
				this._offsetCurrent = 0f;
				this.firstFrameSinceRemove = false;
			}));
			this.snapping = new AgentState("Snapping", this.contents, false, true);
			AgentState agentState3 = this.contents;
			agentState3.OnEmpty = (Action)Delegate.Combine(agentState3.OnEmpty, new Action(this.snapping.SetActiveTrue));
			this.dragging = new AgentState("Dragging", this.contents, false, true);
			this.offsetAnim = new TargetAnimator(new Func<float>(this.get_offsetCurrent), delegate(float x)
			{
				this.offsetCurrent = x;
			}, this.snapping, new LerpTowards(14f, 0.2f));
			AgentState rootState = this.rootState;
			rootState.OnActivate = (Action)Delegate.Combine(rootState.OnActivate, new Action(delegate()
			{
				this.contents.SetActive(this.items.Count > 0);
			}));
			this.rootState.SetActive(base.gameObject.activeInHierarchy);
			this.rootState.UpdateEmpties();
			this.offsetTarget = this.offsetTarget;
			this.offsetCurrent = this.offsetCurrent;
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x0011ABFF File Offset: 0x00118FFF
		public void TabLeft()
		{
			this.ShiftOffsetTarget(1);
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x0011AC09 File Offset: 0x00119009
		public void TabRight()
		{
			this.ShiftOffsetTarget(-1);
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x0011AC14 File Offset: 0x00119014
		public bool ShiftOffsetTarget(int distance)
		{
			int offsetTarget = this.offsetTarget;
			this.offsetTarget = offsetTarget + distance;
			bool flag = offsetTarget != this.offsetTarget;
			if (!flag)
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
			return flag;
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x0011AC50 File Offset: 0x00119050
		protected override void OnEnable()
		{
			this.OnRectTransformDimensionsChange();
			this.rootState.SetActive(true);
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x0011AC65 File Offset: 0x00119065
		protected override void OnDisable()
		{
			base.OnDisable();
			this.rootState.SetActive(false);
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x0011AC7A File Offset: 0x0011907A
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x0011AC88 File Offset: 0x00119088
		public void AddItem(ScrollGridItem item)
		{
			this.MaybeInitialize();
			item.MaybeInitialize();
			this.offsetAnim.ForceActive();
			if (item.grid)
			{
				item.grid.RemoveItem(item);
			}
			item.visAnim.ForceActive();
			this.items.Add(item);
			item.transform.SetParent(this.itemContainer);
			item.grid = this;
			this.contents.SetActive(true);
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x0011AD04 File Offset: 0x00119104
		public void RemoveItem(ScrollGridItem item)
		{
			if (this.items.Remove(item))
			{
				item.transform.SetParent(this.trashcan);
			}
			item.grid = null;
			item.Uproot();
			if (this.items.Count > 0)
			{
				this.offsetTarget = this.offsetTarget;
			}
			else
			{
				this.contents.SetActive(false);
			}
			this.firstFrameSinceRemove = true;
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x0011AD78 File Offset: 0x00119178
		public void Clear()
		{
			foreach (ScrollGridItem scrollGridItem in this.items)
			{
				scrollGridItem.grid = null;
				scrollGridItem.transform.SetParent(this.trashcan);
			}
			this.items.Clear();
			this.contents.SetActive(false);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0011AE00 File Offset: 0x00119200
		public void ChangeSetup(ScrollGrid.Def def, bool sort, ScrollGridItem itemToFocus = null)
		{
			this.gridSetup = def;
			float relativeOffsetCurrrent = this.relativeOffsetCurrrent;
			float relativeOffsetTarget = this.relativeOffsetTarget;
			foreach (ScrollGridItem scrollGridItem in this.items)
			{
				scrollGridItem.Uproot();
			}
			if (sort)
			{
				this.items.Sort();
			}
			this.SetupOnline();
			if (itemToFocus)
			{
				this.offsetTarget = this.GetItemFocusIndex(itemToFocus);
				this.offsetCurrent = (float)this.offsetTarget;
			}
			else
			{
				this.relativeOffsetTarget = relativeOffsetTarget;
				this.relativeOffsetCurrrent = this.relativeOffsetTarget;
			}
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0011AEC4 File Offset: 0x001192C4
		public int GetIndexOf(ScrollGridItem item)
		{
			return this.items.IndexOf(item);
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x0011AED4 File Offset: 0x001192D4
		public void ForceAllToTarget()
		{
			this.offsetCurrent = (float)this.offsetTarget;
			foreach (ScrollGridItem scrollGridItem in this.items)
			{
				scrollGridItem.ForceToTarget();
			}
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x0011AF3C File Offset: 0x0011933C
		public int GetItemFocusIndex(ScrollGridItem item)
		{
			int num = this.items.IndexOf(item);
			return Mathf.Clamp(this.offsetTarget, this.windowMin - num, this.windowMax - num);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x0011AF71 File Offset: 0x00119371
		public void SetFocus(ScrollGridItem item)
		{
			if (this.snapping.active)
			{
				this.offsetTarget = this.GetItemFocusIndex(item);
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x0011AF90 File Offset: 0x00119390
		private Vector2 startPos
		{
			get
			{
				return this.slots[0].rect.center;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06003EB5 RID: 16053 RVA: 0x0011AFB8 File Offset: 0x001193B8
		private Vector2 endPos
		{
			get
			{
				return this.slots[this.slots.Count - 1].rect.center;
			}
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0011AFEC File Offset: 0x001193EC
		private void PopulateSlots()
		{
			this.slots.Clear();
			Rect worldSpaceRect = ((!this.gridSetup.center) ? (base.transform as RectTransform) : this.gridSetup.center).GetWorldSpaceRect();
			foreach (ScrollGridField scrollGridField in this.gridSetup.fields)
			{
				if (scrollGridField)
				{
					RectTransform rt = scrollGridField.transform as RectTransform;
					foreach (Rect rect in scrollGridField.GetRects())
					{
						this.slots.Add(new GridSlot(rt, new Rect(base.transform.InverseTransformPoint(rect.position), base.transform.InverseTransformVector(rect.size)), Vector2.Dot(rect.center - worldSpaceRect.center, this.forward), worldSpaceRect.Contains(rect.center)));
					}
				}
			}
			this.slots.Sort();
			this.windowMin = int.MaxValue;
			this.windowMax = int.MinValue;
			for (int j = 0; j < this.slots.Count; j++)
			{
				if (this.slots[j].inFocus)
				{
					this.windowMin = Mathf.Min(this.windowMin, j);
					this.windowMax = Mathf.Max(this.windowMax, j);
				}
			}
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0011B1C0 File Offset: 0x001195C0
		private void SetupOnline()
		{
			this.MaybeInitialize();
			this.PopulateSlots();
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x0011B1CE File Offset: 0x001195CE
		[ContextMenu("Setup")]
		private void SetupOffline()
		{
			this.PopulateSlots();
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0011B1D8 File Offset: 0x001195D8
		protected override void OnRectTransformDimensionsChange()
		{
			this.MaybeInitialize();
			float relativeOffsetCurrrent = this.relativeOffsetCurrrent;
			float relativeOffsetTarget = this.relativeOffsetTarget;
			this.PopulateSlots();
			if (this.contents != null && this.contents.active)
			{
				this.relativeOffsetCurrrent = relativeOffsetCurrrent;
				this.relativeOffsetTarget = relativeOffsetTarget;
			}
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x0011B228 File Offset: 0x00119628
		private Vector2 SamplePos(float t)
		{
			float f = Mathf.Clamp(t, 0f, (float)(this.slots.Count - 1));
			int num = Mathf.FloorToInt(f);
			int index = Mathf.CeilToInt(f);
			Vector2 center = this.slots[num].rect.center;
			Vector2 center2 = this.slots[index].rect.center;
			return Vector2.Lerp(center, center2, t - (float)num) + this.forward * this.SampleMargin(t) * 32f;
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0011B2C1 File Offset: 0x001196C1
		private float SampleVisibility(float t)
		{
			return ExtraMath.RemapValue(t, -1f, 0f) * ExtraMath.RemapValue(t, (float)this.slots.Count, (float)(this.slots.Count - 1));
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x0011B2F4 File Offset: 0x001196F4
		private float SampleMargin(float t)
		{
			t -= Mathf.Clamp(t, 0f, (float)(this.slots.Count - 1));
			t = (1f - Mathf.Abs(1f / Mathf.Pow(2f, Mathf.Abs(t)))) * Mathf.Sign(t);
			return t;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x0011B349 File Offset: 0x00119749
		private float SampleFocus(float t)
		{
			return ExtraMath.RemapValue(t - (float)this.windowMin, -1f, 0f) * ExtraMath.RemapValue(t - (float)this.windowMax, 1f, 0f);
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x0011B37C File Offset: 0x0011977C
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			foreach (GridSlot gridSlot in this.slots)
			{
				Gizmos.color = ((!gridSlot.inFocus) ? Color.white.SetA(0.5f) : Color.yellow);
				Gizmos.DrawWireCube(gridSlot.rect.center, gridSlot.rect.size - Vector2.one * 10f);
			}
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x0011B448 File Offset: 0x00119848
		private float ScreenPosToOffset(Vector2 pos)
		{
			if (this.slots.Count == 0)
			{
				return 0f;
			}
			Vector2 center = this.slots[0].rect.center;
			Vector2 center2 = this.slots[this.slots.Count - 1].rect.center;
			Vector2 forward = this.forward;
			float num = Vector2.Dot(forward, center2 - center) / (float)(this.slots.Count - 1);
			Vector2 vector = base.transform.InverseTransformPoint(pos);
			vector -= center;
			float num2 = Vector2.Dot(vector, this.forward);
			return num2 / num;
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x0011B50C File Offset: 0x0011990C
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.items.Count - 1 > this.windowMax - this.windowMin)
			{
				this.startOffset = this.ScreenPosToOffset(eventData.position) - this.offsetCurrent;
				this.dragging.SetActive(true);
			}
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0011B560 File Offset: 0x00119960
		public void OnDrag(PointerEventData eventData)
		{
			if (this.dragging.active)
			{
				this.offsetCurrent = this.ScreenPosToOffset(eventData.position) - this.startOffset;
				this.SetFocusAndWindow(Mathf.RoundToInt(this.offsetCurrent));
				this.onSetOffsetDragging(this.offsetCurrent);
			}
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x0011B5B8 File Offset: 0x001199B8
		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.dragging.active)
			{
				float num = Vector2.Dot(this.forward, this.slots[0].rect.center - this.slots[this.slots.Count - 1].rect.center) / (float)this.slots.Count;
				this.snapping.SetActive(true);
				this.offsetTarget = Mathf.RoundToInt(this.offsetCurrent - 0.2f * Vector2.Dot(this.forward, eventData.delta / Time.deltaTime) / num);
			}
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x0011B673 File Offset: 0x00119A73
		public ScrollGridItem GetCenterItem(int offset)
		{
			return this.items[Mathf.Clamp(this.windowMin - offset, 0, this.items.Count - 1)];
		}

		// Token: 0x04002BC2 RID: 11202
		[SerializeField]
		private ScrollGrid.Direction direction;

		// Token: 0x04002BC3 RID: 11203
		private Graphic graphic;

		// Token: 0x04002BC4 RID: 11204
		[SerializeField]
		private ScrollGrid.Def gridSetup;

		// Token: 0x04002BC5 RID: 11205
		[SerializeField]
		private Transform itemContainer;

		// Token: 0x04002BC6 RID: 11206
		[SerializeField]
		private Transform trashcan;

		// Token: 0x04002BC7 RID: 11207
		private List<GridSlot> slots = new List<GridSlot>();

		// Token: 0x04002BC8 RID: 11208
		private List<ScrollGridItem> items = new List<ScrollGridItem>();

		// Token: 0x04002BCB RID: 11211
		public Action<int> onSetOffsetTarget = delegate(int A_0)
		{
		};

		// Token: 0x04002BCC RID: 11212
		public Action<float> onSetOffsetDragging = delegate(float A_0)
		{
		};

		// Token: 0x04002BCD RID: 11213
		private int _offsetTarget;

		// Token: 0x04002BCE RID: 11214
		private float _offsetCurrent;

		// Token: 0x04002BCF RID: 11215
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002BD0 RID: 11216
		public AgentState contents;

		// Token: 0x04002BD1 RID: 11217
		public AgentState snapping;

		// Token: 0x04002BD2 RID: 11218
		public AgentState dragging;

		// Token: 0x04002BD3 RID: 11219
		public TargetAnimator offsetAnim;

		// Token: 0x04002BD4 RID: 11220
		private bool firstFrameWithContent = true;

		// Token: 0x04002BD5 RID: 11221
		private bool firstFrameSinceRemove;

		// Token: 0x04002BD6 RID: 11222
		private bool initialized;

		// Token: 0x04002BD7 RID: 11223
		private float startOffset;

		// Token: 0x0200091E RID: 2334
		[Serializable]
		public class Def
		{
			// Token: 0x04002BDA RID: 11226
			public RectTransform center;

			// Token: 0x04002BDB RID: 11227
			public ScrollGridField[] fields;
		}

		// Token: 0x0200091F RID: 2335
		private enum Direction
		{
			// Token: 0x04002BDD RID: 11229
			Horizontal,
			// Token: 0x04002BDE RID: 11230
			Vertical
		}
	}
}
