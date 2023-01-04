using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000479 RID: 1145
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchButtonExample : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00046FB2 File Offset: 0x000453B2
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x00046FBA File Offset: 0x000453BA
		public bool isPressed { get; private set; }

		// Token: 0x06001A3F RID: 6719 RVA: 0x00046FC3 File Offset: 0x000453C3
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00046FD7 File Offset: 0x000453D7
		private void Restart()
		{
			this.isPressed = false;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00046FE0 File Offset: 0x000453E0
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = true;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00047005 File Offset: 0x00045405
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (!this.allowMouseControl && TouchButtonExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.isPressed = false;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0004702A File Offset: 0x0004542A
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x0400103D RID: 4157
		public bool allowMouseControl = true;
	}
}
