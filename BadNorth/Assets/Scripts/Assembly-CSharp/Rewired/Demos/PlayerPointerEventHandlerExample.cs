using System;
using System.Collections.Generic;
using System.Text;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000485 RID: 1157
	[AddComponentMenu("")]
	public sealed class PlayerPointerEventHandlerExample : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IScrollHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x06001A94 RID: 6804 RVA: 0x00048F39 File Offset: 0x00047339
		private void Log(string o)
		{
			this.log.Add(o);
			if (this.log.Count > 10)
			{
				this.log.RemoveAt(0);
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00048F68 File Offset: 0x00047368
		private void Update()
		{
			if (this.text != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.log)
				{
					stringBuilder.AppendLine(value);
				}
				this.text.text = stringBuilder.ToString();
			}
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00048FF0 File Offset: 0x000473F0
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerEnter:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00049060 File Offset: 0x00047460
		public void OnPointerExit(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerExit:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000490D0 File Offset: 0x000474D0
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerUp:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00049154 File Offset: 0x00047554
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerDown:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000491D8 File Offset: 0x000475D8
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnPointerClick:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0004925C File Offset: 0x0004765C
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnScroll:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000492CC File Offset: 0x000476CC
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnBeginDrag:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00049350 File Offset: 0x00047750
		public void OnDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnDrag:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000493D4 File Offset: 0x000477D4
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new object[]
				{
					"OnEndDrag:  Player = ",
					playerPointerEventData.playerId,
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex,
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex
				}));
			}
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00049458 File Offset: 0x00047858
		private static string GetSourceName(PlayerPointerEventData playerEventData)
		{
			if (playerEventData.sourceType == PointerEventType.Mouse)
			{
				if (playerEventData.mouseSource is Behaviour)
				{
					return (playerEventData.mouseSource as Behaviour).name;
				}
			}
			else if (playerEventData.sourceType == PointerEventType.Touch && playerEventData.touchSource is Behaviour)
			{
				return (playerEventData.touchSource as Behaviour).name;
			}
			return null;
		}

		// Token: 0x040010A8 RID: 4264
		public Text text;

		// Token: 0x040010A9 RID: 4265
		private const int logLength = 10;

		// Token: 0x040010AA RID: 4266
		private List<string> log = new List<string>();
	}
}
