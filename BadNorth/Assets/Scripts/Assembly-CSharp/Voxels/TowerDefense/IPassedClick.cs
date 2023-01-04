using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense
{
	// Token: 0x0200072B RID: 1835
	public interface IPassedClick
	{
		// Token: 0x06002F9D RID: 12189
		void OnPassedClick(ClickPasser clickPasser, PointerEventData eventData, RaycastHit raycastHit);
	}
}
