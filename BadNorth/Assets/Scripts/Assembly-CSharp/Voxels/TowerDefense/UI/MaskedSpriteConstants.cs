using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008EA RID: 2282
	[AssetPath("Assets/Settings/Resources/")]
	[Serializable]
	public class MaskedSpriteConstants : ScriptableObjectSingleton<MaskedSpriteConstants>
	{
		// Token: 0x04002A28 RID: 10792
		public Color SelectionBorderColor = Color.yellow;

		// Token: 0x04002A29 RID: 10793
		public Sprite heroMask;
	}
}
