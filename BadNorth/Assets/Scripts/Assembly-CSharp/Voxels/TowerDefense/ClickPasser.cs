using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Voxels.TowerDefense
{
	// Token: 0x0200072A RID: 1834
	public class ClickPasser : StateListener, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06002F9B RID: 12187 RVA: 0x000C1648 File Offset: 0x000BFA48
		public static RaycastHit GetHitAtMouse(PointerEventData eventData, LayerMask mask)
		{
			Vector2 position = eventData.position;
			position.x /= (float)Screen.width;
			position.y /= (float)Screen.height;
			Ray ray = Singleton<LevelCamera>.instance.cameraRef.ViewportPointToRay(position);
			RaycastHit result;
			Physics.Raycast(ray, out result, float.PositiveInfinity, mask);
			return result;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000C16B0 File Offset: 0x000BFAB0
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("ClickPasser");
			if (!this.active)
			{
				return;
			}
			if (!eventData.dragging && !eventData.used)
			{
				Debug.Log("Passing Click");
				RaycastHit hitAtMouse = ClickPasser.GetHitAtMouse(eventData, this.clickMask0);
				if (hitAtMouse.collider == null)
				{
					return;
				}
				bool flag;
				EnglishSquad squadFromRaycast = Singleton<SquadSelector>.instance.GetSquadFromRaycast(hitAtMouse, out flag);
				if (squadFromRaycast)
				{
					Singleton<SquadSelector>.instance.SelectSquad(squadFromRaycast, false);
					eventData.Use();
				}
				else
				{
					IPassedClick component = hitAtMouse.collider.GetComponent<IPassedClick>();
					if (component != null)
					{
						component.OnPassedClick(this, eventData, hitAtMouse);
					}
					eventData.Use();
				}
			}
		}

		// Token: 0x04001FC3 RID: 8131
		[FormerlySerializedAs("clickMask")]
		public LayerMask clickMask0;
	}
}
