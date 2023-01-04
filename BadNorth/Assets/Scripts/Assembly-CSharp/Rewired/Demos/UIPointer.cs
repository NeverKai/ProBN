using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000486 RID: 1158
	[AddComponentMenu("")]
	[RequireComponent(typeof(RectTransform))]
	public sealed class UIPointer : UIBehaviour
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x000494DA File Offset: 0x000478DA
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x000494E2 File Offset: 0x000478E2
		public bool autoSort
		{
			get
			{
				return this._autoSort;
			}
			set
			{
				if (value == this._autoSort)
				{
					return;
				}
				this._autoSort = value;
				if (value)
				{
					base.transform.SetAsLastSibling();
				}
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0004950C File Offset: 0x0004790C
		protected override void Awake()
		{
			base.Awake();
			Graphic[] componentsInChildren = base.GetComponentsInChildren<Graphic>();
			foreach (Graphic graphic in componentsInChildren)
			{
				graphic.raycastTarget = false;
			}
			if (this._hideHardwarePointer)
			{
				Cursor.visible = false;
			}
			if (this._autoSort)
			{
				base.transform.SetAsLastSibling();
			}
			this.GetDependencies();
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00049574 File Offset: 0x00047974
		private void Update()
		{
			if (this._autoSort && base.transform.GetSiblingIndex() < base.transform.parent.childCount - 1)
			{
				base.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000495AE File Offset: 0x000479AE
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.GetDependencies();
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000495BC File Offset: 0x000479BC
		protected override void OnCanvasGroupChanged()
		{
			base.OnCanvasGroupChanged();
			this.GetDependencies();
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000495CC File Offset: 0x000479CC
		public void OnScreenPositionChanged(Vector2 screenPosition)
		{
			if (this._canvasRectTransform == null)
			{
				return;
			}
			Rect rect = this._canvasRectTransform.rect;
			Vector2 anchoredPosition = Camera.main.ScreenToViewportPoint(screenPosition);
			anchoredPosition.x = anchoredPosition.x * rect.width - this._canvasRectTransform.pivot.x * rect.width;
			anchoredPosition.y = anchoredPosition.y * rect.height - this._canvasRectTransform.pivot.y * rect.height;
			(base.transform as RectTransform).anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00049684 File Offset: 0x00047A84
		private void GetDependencies()
		{
			Canvas componentInChildren = base.transform.root.GetComponentInChildren<Canvas>();
			this._canvasRectTransform = ((!(componentInChildren != null)) ? null : componentInChildren.GetComponent<RectTransform>());
		}

		// Token: 0x040010AB RID: 4267
		[Tooltip("Should the hardware pointer be hidden?")]
		[SerializeField]
		private bool _hideHardwarePointer = true;

		// Token: 0x040010AC RID: 4268
		[Tooltip("Sets the pointer to the last sibling in the parent hierarchy. Do not enable this on multiple UIPointers under the same parent transform or they will constantly fight each other for dominance.")]
		[SerializeField]
		private bool _autoSort = true;

		// Token: 0x040010AD RID: 4269
		[NonSerialized]
		private RectTransform _canvasRectTransform;
	}
}
