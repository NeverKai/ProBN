using System;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense
{
	// Token: 0x0200072C RID: 1836
	public interface IPassedDoubleClick
	{
		// Token: 0x06002F9E RID: 12190
		void OnPassedDoubleClick(ClickPasser clickPasser, PointerEventData eventData);
	}
}
