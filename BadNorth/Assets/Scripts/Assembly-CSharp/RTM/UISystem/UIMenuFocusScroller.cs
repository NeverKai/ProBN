using System;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RTM.UISystem
{
	// Token: 0x020004DB RID: 1243
	public class UIMenuFocusScroller : UIBehaviour
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x00055647 File Offset: 0x00053A47
		private bool wantSnap
		{
			get
			{
				return this.snapFrames > 0;
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00055654 File Offset: 0x00053A54
		protected override void Awake()
		{
			base.Awake();
			this.scrollRect = ((!this.scrollRect) ? base.GetComponentInChildren<ScrollRect>(true) : this.scrollRect);
			this.content = this.scrollRect.content;
			this.viewPort = this.scrollRect.viewport;
			this.uiMenu = ((!this.uiMenu) ? this.GetDisabledComponentInParent<UIMenu>() : this.uiMenu);
			this.uiMenu.onFocusedNavigableChanged += this.OnFocusedNavigableChanged;
			this.uiMenu.forceScrollToNavigable += this.ScrollTo;
			this.uiMenu.onClosed += delegate()
			{
				this.snapFrames = 0;
			};
			this.uiMenu.onOpened += delegate()
			{
				if (!this.uiMenu.rememberFocusOnClose)
				{
					this.snapFrames = 3;
				}
			};
			if (Platform.Is(EPlatform.PC))
			{
				this.scrollRect.movementType = ScrollRect.MovementType.Clamped;
			}
			base.enabled = false;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00055752 File Offset: 0x00053B52
		private void OnFocusedNavigableChanged(IUINavigable navigable)
		{
			this.navigable = new WeakInterfaceReference<IUINavigable>(navigable);
			base.enabled = (navigable != null && navigable.transform.IsDescendentOf(this.content));
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00055780 File Offset: 0x00053B80
		private void LateUpdate()
		{
			if (this.wantSnap)
			{
				IUINavigable iuinavigable = this.navigable.Get();
				if (iuinavigable != null)
				{
					this.scrollRect.normalizedPosition = this.GetTargetNormalizedPos(iuinavigable);
				}
				else
				{
					this.scrollRect.normalizedPosition = new Vector2(0f, 1f);
				}
				this.snapFrames--;
				return;
			}
			if (!this.navigable)
			{
				base.enabled = false;
				return;
			}
			Vector2 normalizedPosition = this.scrollRect.normalizedPosition;
			Vector2 targetNormalizedPos = this.GetTargetNormalizedPos(this.navigable.Get());
			Vector2 vector = ExtraMath.ExpLerpTowards(normalizedPosition, targetNormalizedPos, 0.0001f, Time.unscaledDeltaTime);
			vector = Vector2.MoveTowards(vector, targetNormalizedPos, 0.2f * Time.unscaledDeltaTime);
			this.scrollRect.normalizedPosition = vector;
			if (vector == targetNormalizedPos)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00055864 File Offset: 0x00053C64
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			base.enabled = true;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00055874 File Offset: 0x00053C74
		private Vector2 GetTargetNormalizedPos(IUINavigable iNavigable)
		{
			RectTransform rectTransform = iNavigable.transform as RectTransform;
			Rect rect = rectTransform.rect;
			Rect rect2 = this.viewPort.rect;
			Rect rect3 = this.content.rect;
			Vector2 pivot = this.content.pivot;
			Vector3 vector = rectTransform.TransformPoint(rect.center);
			vector = this.content.InverseTransformPoint(vector);
			vector.x -= (1f - pivot.x) * rect3.width;
			vector.y -= (1f - pivot.y) * rect3.height;
			Vector2 vector2 = -vector - rect2.size * 0.5f;
			Vector2 vector3 = rect3.size - rect2.size;
			Vector3 v = new Vector3((vector3.x <= 0f) ? 0f : (1f - Mathf.Clamp(vector2.x / vector3.x, 0f, 1f)), (vector3.y <= 0f) ? 0f : (1f - Mathf.Clamp(vector2.y / vector3.y, 0f, 1f)));
			return v;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000559EC File Offset: 0x00053DEC
		public void ScrollTo(IUINavigable navigable, bool snap)
		{
			this.OnFocusedNavigableChanged(navigable);
			this.snapFrames = 3;
			base.enabled = true;
		}

		// Token: 0x040013B6 RID: 5046
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("UIMenuFocusScroller", EVerbosity.Quiet, 100);

		// Token: 0x040013B7 RID: 5047
		private const int SNAP_FRAMES = 3;

		// Token: 0x040013B8 RID: 5048
		[SerializeField]
		private ScrollRect scrollRect;

		// Token: 0x040013B9 RID: 5049
		private RectTransform viewPort;

		// Token: 0x040013BA RID: 5050
		private RectTransform content;

		// Token: 0x040013BB RID: 5051
		[SerializeField]
		private UIMenu uiMenu;

		// Token: 0x040013BC RID: 5052
		private WeakInterfaceReference<IUINavigable> navigable = null;

		// Token: 0x040013BD RID: 5053
		private int snapFrames;
	}
}
