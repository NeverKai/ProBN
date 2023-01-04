using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x0200047A RID: 1146
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class TouchJoystickExample : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEventSystemHandler
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0004705A File Offset: 0x0004545A
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x00047062 File Offset: 0x00045462
		public Vector2 position { get; private set; }

		// Token: 0x06001A47 RID: 6727 RVA: 0x0004706B File Offset: 0x0004546B
		private void Start()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld)
			{
				this.allowMouseControl = false;
			}
			this.StoreOrigValues();
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00047088 File Offset: 0x00045488
		private void Update()
		{
			if ((float)Screen.width != this.origScreenResolution.x || (float)Screen.height != this.origScreenResolution.y || Screen.orientation != this.origScreenOrientation)
			{
				this.Restart();
				this.StoreOrigValues();
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000470DD File Offset: 0x000454DD
		private void Restart()
		{
			this.hasFinger = false;
			(base.transform as RectTransform).anchoredPosition = this.origAnchoredPosition;
			this.position = Vector2.zero;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00047108 File Offset: 0x00045508
		private void StoreOrigValues()
		{
			this.origAnchoredPosition = (base.transform as RectTransform).anchoredPosition;
			this.origWorldPosition = base.transform.position;
			this.origScreenResolution = new Vector2((float)Screen.width, (float)Screen.height);
			this.origScreenOrientation = Screen.orientation;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00047160 File Offset: 0x00045560
		private void UpdateValue(Vector3 value)
		{
			Vector3 a = this.origWorldPosition - value;
			a.y = -a.y;
			a /= (float)this.radius;
			this.position = new Vector2(-a.x, a.y);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000471B1 File Offset: 0x000455B1
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.hasFinger)
			{
				return;
			}
			if (!this.allowMouseControl && TouchJoystickExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.hasFinger = true;
			this.lastFingerId = eventData.pointerId;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000471EE File Offset: 0x000455EE
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (eventData.pointerId != this.lastFingerId)
			{
				return;
			}
			if (!this.allowMouseControl && TouchJoystickExample.IsMousePointerId(eventData.pointerId))
			{
				return;
			}
			this.Restart();
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00047224 File Offset: 0x00045624
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.hasFinger || eventData.pointerId != this.lastFingerId)
			{
				return;
			}
			Vector3 vector = new Vector3(eventData.position.x - this.origWorldPosition.x, eventData.position.y - this.origWorldPosition.y);
			vector = Vector3.ClampMagnitude(vector, (float)this.radius);
			Vector3 vector2 = this.origWorldPosition + vector;
			base.transform.position = vector2;
			this.UpdateValue(vector2);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000472B7 File Offset: 0x000456B7
		private static bool IsMousePointerId(int id)
		{
			return id == -1 || id == -2 || id == -3;
		}

		// Token: 0x0400103F RID: 4159
		public bool allowMouseControl = true;

		// Token: 0x04001040 RID: 4160
		public int radius = 50;

		// Token: 0x04001041 RID: 4161
		private Vector2 origAnchoredPosition;

		// Token: 0x04001042 RID: 4162
		private Vector3 origWorldPosition;

		// Token: 0x04001043 RID: 4163
		private Vector2 origScreenResolution;

		// Token: 0x04001044 RID: 4164
		private ScreenOrientation origScreenOrientation;

		// Token: 0x04001045 RID: 4165
		[NonSerialized]
		private bool hasFinger;

		// Token: 0x04001046 RID: 4166
		[NonSerialized]
		private int lastFingerId;
	}
}
