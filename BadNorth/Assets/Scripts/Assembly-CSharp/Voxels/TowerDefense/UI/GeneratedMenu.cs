using System;
using System.Diagnostics;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008BC RID: 2236
	public abstract class GeneratedMenu : UIMenu, IGameSetup
	{
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06003AC2 RID: 15042 RVA: 0x0006F044 File Offset: 0x0006D444
		// (remove) Token: 0x06003AC3 RID: 15043 RVA: 0x0006F07C File Offset: 0x0006D47C
		
		protected event Action slaveUpdates = delegate()
		{
		};

		// Token: 0x06003AC4 RID: 15044 RVA: 0x0006F0B2 File Offset: 0x0006D4B2
		void IGameSetup.OnGameAwake()
		{
			this.MaybeInitialize();
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x0006F0BA File Offset: 0x0006D4BA
		protected virtual void Awake()
		{
			this.MaybeInitialize();
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x0006F0C2 File Offset: 0x0006D4C2
		private void MaybeInitialize()
		{
			if (!this.initialized)
			{
				this.Initialize();
			}
			this.initialized = true;
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x0006F0DC File Offset: 0x0006D4DC
		protected virtual void Initialize()
		{
			this.ClearWidgets();
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x0006F0E4 File Offset: 0x0006D4E4
		public virtual ButtonWidget AddButton(string labelLocTerm, Func<bool> action, Transform overrideParentTransform = null, ButtonWidget overrideButtonPrefab = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			ButtonWidget original = (!overrideButtonPrefab) ? this.textButtonPrefab : overrideButtonPrefab;
			ButtonWidget buttonWidget = transform.gameObject.InstantiateChild(original, null).Initialize(labelLocTerm, action);
			this.slaveUpdates += buttonWidget.SlaveUpdate;
			return buttonWidget;
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x0006F14C File Offset: 0x0006D54C
		public virtual ButtonWidget AddButton(string labelLocTerm, Func<bool> action, Sprite icon, Transform overrideParentTransform = null, IconButtonWidget overrideButtonPrefab = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			IconButtonWidget original = (!overrideButtonPrefab) ? this.textIconButtonPrefab : overrideButtonPrefab;
			IconButtonWidget iconButtonWidget = transform.gameObject.InstantiateChild(original, null).Initialize(labelLocTerm, action, icon);
			this.slaveUpdates += iconButtonWidget.SlaveUpdate;
			return iconButtonWidget;
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x0006F1B8 File Offset: 0x0006D5B8
		public virtual BoolWidget AddBoolWidget(string labelLocTerm, Func<bool> getAction, Func<bool, bool> setAction, Transform overrideParentTransform = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			ValueWidget<bool> valueWidget = transform.gameObject.InstantiateChild(this.boolWidgetPrefab, null).Initialize(labelLocTerm, getAction, setAction);
			this.slaveUpdates += valueWidget.SlaveUpdate;
			return valueWidget as BoolWidget;
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x0006F214 File Offset: 0x0006D614
		public virtual IntWidget AddIntWidget(string labelLocTerm, Func<int> getAction, Func<int, bool> setAction, Transform overrideParentTransform = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			ValueWidget<int> valueWidget = transform.gameObject.InstantiateChild(this.intWidgetPrefab, null).Initialize(labelLocTerm, getAction, setAction);
			this.slaveUpdates += valueWidget.SlaveUpdate;
			return valueWidget as IntWidget;
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x0006F270 File Offset: 0x0006D670
		public virtual MultiSelectWidget AddMultiSelectWidget(string labelLocTerm, string[] values, bool wantsLocalizedValues, Func<int> getAction, Func<int, bool> setAction, Transform overrideParentTransform = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			ValueWidget<int> valueWidget = transform.gameObject.InstantiateChild(this.multiSelectWidgetPrefab, null).SetValues(values, wantsLocalizedValues).Initialize(labelLocTerm, getAction, setAction);
			this.slaveUpdates += valueWidget.SlaveUpdate;
			return valueWidget as MultiSelectWidget;
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x0006F2D4 File Offset: 0x0006D6D4
		public virtual MultiSelectWidget AddOnOffWidget(string labelLocTerm, Func<bool> getAction, Func<bool, bool> setAction, Transform overrideParentTransform = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			ValueWidget<int> valueWidget = transform.gameObject.InstantiateChild(this.multiSelectWidgetPrefab, null).SetOnOff().Initialize(labelLocTerm, () => (!getAction()) ? 0 : 1, (int v) => setAction(v != 0));
			this.slaveUpdates += valueWidget.SlaveUpdate;
			return valueWidget as MultiSelectWidget;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0006F360 File Offset: 0x0006D760
		public virtual MultiSelectWidget AddYesNoWidget(string labelLocTerm, Func<bool> getAction, Func<bool, bool> setAction, Transform overrideParentTransform = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			ValueWidget<int> valueWidget = transform.gameObject.InstantiateChild(this.multiSelectWidgetPrefab, null).SetYesNo().Initialize(labelLocTerm, () => (!getAction()) ? 0 : 1, (int v) => setAction(v != 0));
			this.slaveUpdates += valueWidget.SlaveUpdate;
			return valueWidget as MultiSelectWidget;
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x0006F3EC File Offset: 0x0006D7EC
		public static void ForceUpdateAll(Transform parent)
		{
			foreach (Widget widget in parent.GetComponentsInChildren<Widget>())
			{
				widget.ForceUpdate();
			}
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x0006F41E File Offset: 0x0006D81E
		public virtual void ForceUpdateAll()
		{
			GeneratedMenu.ForceUpdateAll(this.widgetParent);
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x0006F42B File Offset: 0x0006D82B
		protected virtual void ClearWidgets()
		{
			if (this.widgetParent)
			{
				this.widgetParent.DestroyChildren();
			}
			this.slaveUpdates = delegate()
			{
			};
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x0006F46B File Offset: 0x0006D86B
		protected virtual void OnEnable()
		{
			this.slaveUpdates();
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x0006F478 File Offset: 0x0006D878
		protected virtual void Update()
		{
			this.slaveUpdates();
		}

		// Token: 0x040028D8 RID: 10456
		[Space]
		[Header("GeneratedMenu")]
		[SerializeField]
		protected RectTransform widgetParent;

		// Token: 0x040028D9 RID: 10457
		[Header("Widget Prefabs")]
		[SerializeField]
		protected ButtonWidget textButtonPrefab;

		// Token: 0x040028DA RID: 10458
		[SerializeField]
		protected IconButtonWidget textIconButtonPrefab;

		// Token: 0x040028DB RID: 10459
		[SerializeField]
		protected BoolWidget boolWidgetPrefab;

		// Token: 0x040028DC RID: 10460
		[SerializeField]
		protected IntWidget intWidgetPrefab;

		// Token: 0x040028DD RID: 10461
		[SerializeField]
		protected MultiSelectWidget multiSelectWidgetPrefab;

		// Token: 0x040028DF RID: 10463
		private bool initialized;
	}
}
