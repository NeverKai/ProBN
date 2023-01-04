using System;
using UnityEngine;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x02000761 RID: 1889
	internal interface ITreePlanter
	{
		// Token: 0x06003139 RID: 12601
		void OnTreePlanted(Tree tree, Shoot shoot, Vector3 normal);
	}
}
