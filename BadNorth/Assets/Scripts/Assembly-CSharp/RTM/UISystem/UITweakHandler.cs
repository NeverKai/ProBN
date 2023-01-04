using System;
using System.Diagnostics;
using RTM.UISystem.Internal;
using UnityEngine;
using UnityEngine.Events;

namespace RTM.UISystem
{
	// Token: 0x020004E0 RID: 1248
	public class UITweakHandler : MonoBehaviour
	{
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x0600200E RID: 8206 RVA: 0x00056718 File Offset: 0x00054B18
		// (remove) Token: 0x0600200F RID: 8207 RVA: 0x00056750 File Offset: 0x00054B50
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> onVerticalChange = delegate(int A_0)
		{
		};

		// Token: 0x06002010 RID: 8208 RVA: 0x00056788 File Offset: 0x00054B88
		private void Awake()
		{
			IUINavigable component = base.GetComponent<IUINavigable>();
			component.onConsumedNavigation += this.OnConsumedNavigation;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000567B0 File Offset: 0x00054BB0
		private void OnConsumedNavigation(Vector2 input)
		{
			if (Helpers.IsVertical(input))
			{
				int num = this.ToInt(input.y);
				this.onVerticalChange(num);
				if (num > 0)
				{
					this.onVerticalIncrementEvent.Invoke();
				}
				else
				{
					this.onVerticalDecrementEvent.Invoke();
				}
			}
			else
			{
				int num2 = this.ToInt(input.x);
				this.onHorizontalChange(num2);
				if (num2 > 0)
				{
					this.onHorizontalIncrementEvent.Invoke();
				}
				else
				{
					this.onHorizontalDecrementEvent.Invoke();
				}
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00056845 File Offset: 0x00054C45
		private int ToInt(float inputAxis)
		{
			return (inputAxis <= 0f) ? -1 : 1;
		}

		// Token: 0x040013E4 RID: 5092
		[SerializeField]
		private UnityEvent onHorizontalDecrementEvent = new UnityEvent();

		// Token: 0x040013E5 RID: 5093
		[SerializeField]
		private UnityEvent onHorizontalIncrementEvent = new UnityEvent();

		// Token: 0x040013E6 RID: 5094
		public Action<int> onHorizontalChange = delegate(int A_0)
		{
		};

		// Token: 0x040013E7 RID: 5095
		[SerializeField]
		private UnityEvent onVerticalDecrementEvent = new UnityEvent();

		// Token: 0x040013E8 RID: 5096
		[SerializeField]
		private UnityEvent onVerticalIncrementEvent = new UnityEvent();
	}
}
